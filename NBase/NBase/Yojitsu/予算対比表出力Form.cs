using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yojitsu.DA;
using NBaseData.DAC;
using Yojitsu.util;

namespace Yojitsu
{
    public partial class 予算対比表出力Form : Form
    {
        private BgYosanHead yosanHead;
        private List<BgYosanHead> yosanHeads;

        /// <summary>
        /// Constants.TANIに対応する数字Rate
        /// </summary>
        private static readonly decimal[] TaniComboRate = {
												   1000m,
												   1000000m,

											   };


        public 予算対比表出力Form(List<BgYosanHead> yosanHeads)
        {
            // 最新版
            this.yosanHead = yosanHeads[0];

            // 直近確定版と、前回最終版
            bool 直近Flag = false;
            bool 前回Flag = false;
            this.yosanHeads = new List<BgYosanHead>();
            foreach (BgYosanHead h in yosanHeads)
            {
                if (h.YosanHeadID == this.yosanHead.YosanHeadID)
                    continue;

                if (直近Flag == false && h.Year == this.yosanHead.Year && h.YosanSbtID == this.yosanHead.YosanSbtID)
                {
                    // 同じ年の同じ予算種別 ==> 直近確定版
                    this.yosanHeads.Add(h);
                    直近Flag = true;
                }
                if (前回Flag == false && h.YosanSbtID != this.yosanHead.YosanSbtID)
                {
                    // 予算種別が違う ==> 前回最終版
                    this.yosanHeads.Add(h);
                    前回Flag = true;
                    break;
                }
            }

            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("", "予算対比表出力", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            Init();
        }

        #region 初期化
        private void Init()
        {
            textBox_最新年度.Text = yosanHead.Year.ToString();
            textBox_最新種別.Text = BgYosanSbt.ToName(yosanHead.YosanSbtID);
            textBox_最新Rev.Text = MakeRevStr(yosanHead);

            // 年度
            InitComboBox年度();
            // 予算種別
            InitComboBox予算種別();
            // リビジョン
            InitComboBoxリビジョン();
            // 単位
            InitComboBox単位();
        }


        private void InitComboBox年度()
        {
            comboBox年度.Items.Clear();

            List<int> years = new List<int>();

            int i = 0;
            foreach (BgYosanHead h in yosanHeads)
            {
                if (!years.Contains(h.Year))
                {
                    comboBox年度.Items.Add(h.Year);
                    years.Add(h.Year);
                }
            }

            if (comboBox年度.Items.Count > 0)
            {
                comboBox年度.SelectedIndex = 0;
            }
        }

        private void InitComboBox予算種別()
        {
            comboBox予算種別.Items.Clear();

            foreach (BgYosanHead h in yosanHeads)
            {
                if (h.Year == Int32.Parse(comboBox年度.Text))
                {
                    if (!comboBox予算種別.Items.Contains(BgYosanSbt.ToName(h.YosanSbtID)))
                    {
                        comboBox予算種別.Items.Add(BgYosanSbt.ToName(h.YosanSbtID));
                    }
                }
            }

            if (comboBox予算種別.Items.Count > 0)
            {
                comboBox予算種別.SelectedIndex = 0;
            }
        }

        private void InitComboBoxリビジョン()
        {
            comboBoxリビジョン.Items.Clear();

            foreach (BgYosanHead h in yosanHeads)
            {
                if (h.Year == Int32.Parse(comboBox年度.Text) &&
                    BgYosanSbt.ToName(h.YosanSbtID) == comboBox予算種別.Text)
                {
                    comboBoxリビジョン.Items.Add(MakeRevStr(h));
                }
            }


            if (comboBoxリビジョン.Items.Count > 0)
            {
                comboBoxリビジョン.SelectedIndex = 0;
            }
        }

        private void InitComboBox単位()
        {
            foreach (string kikan in Constants.TANI)
            {
                comboBox単位.Items.Add(kikan);
            }

            comboBox単位.SelectedIndex = 0;
        }
        #endregion

        private BgYosanHead Get選択BgYosanHead()
        {
            int year = Int32.Parse(comboBox年度.Text);

            //予算頭選択
            foreach (BgYosanHead h in this.yosanHeads)
            {
                if (h.Year == year &&
                    BgYosanSbt.ToName(h.YosanSbtID) == comboBox予算種別.Text &&
                    h.Revision == Int32.Parse(comboBoxリビジョン.Text.Split(' ')[0]))
                {
                    return h;
                }
            }

            return null;
        }

        private string MakeRevStr(BgYosanHead head)
        {
            string revStr = head.Revision.ToString();
            if (!head.IsFixed())
            {
                revStr += " (未 Fix)";
            }
            else
            {
                revStr += " (" + head.FixDate.ToString("yyyy/MM/dd") + " Fix)";
            }
            return revStr;
        }

        private bool 予算比較出力(string filename)
        {
            List<MsVessel> vessels = DbAccessorFactory.FACTORY.MsVessel_GetRecords(NBaseCommon.Common.LoginUser);


            // 比較元の予算ヘッド
            //List<BgYosanHead> yosanHeads

            // 比較対象の予算ヘッド             
            BgYosanHead selectedYosanHead = this.Get選択BgYosanHead();

            //今回の単位を取得
            decimal unit = 1.0m;
            unit = 予算対比表出力Form.TaniComboRate[this.comboBox単位.SelectedIndex];

            string message = "";
            bool ret = true;
            try
            {
                byte[] excelData = null;

                NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        excelData = serviceClient.BLC_Excel予算対比表_取得(NBaseCommon.Common.LoginUser, yosanHead, selectedYosanHead, unit);
                    }
                }, "予算対比表を作成中です...");
                progressDialog.ShowDialog();

                if (excelData == null)
                {
                    MessageBox.Show("予算対比表の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "予算対比表", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    return false;
                }

                //バイナリをファイルに
                System.IO.FileStream filest = new System.IO.FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
                filest.Write(excelData, 0, excelData.Length);
                filest.Close();

                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
            catch (Exception ex)
            {
                //カーソルを通常に戻す
                this.Cursor = System.Windows.Forms.Cursors.Default;
                message = ex.Message;
                ret = false;
            }


            if (ret == false)
            {
                MessageBox.Show("予算対比表の出力に失敗しました。\n (Err:" + message + ")", "予算対比表", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            string smes = "「" + filename + "」に出力しました。";
            MessageBox.Show(smes, "予算対比表", MessageBoxButtons.OK, MessageBoxIcon.Information);


            return true;
        }

        private void button出力_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                this.予算比較出力(saveFileDialog1.FileName);

                Dispose();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void comboBox年度_SelectionChangeCommitted(object sender, EventArgs e)
        {
            InitComboBox予算種別();
            InitComboBoxリビジョン();
        }

        private void comboBox予算種別_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitComboBoxリビジョン();
        }
    }
}
