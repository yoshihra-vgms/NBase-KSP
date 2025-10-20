using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yojitsu.DA;
using WingData.DAC;
using Yojitsu.util;

namespace Yojitsu
{
    public partial class 月次収支報告書出力Form : Form
    {
        private List<BgYosanHead> yosanHeads;

        /// <summary>
        /// Constants.TANIに対応する数字Rate
        /// </summary>
        private static readonly decimal[] TaniComboRate = {
												   1000m,
												   1000000m,

											   };


        public 月次収支報告書出力Form(List<BgYosanHead> yosanHeads)
        {
            this.yosanHeads = yosanHeads;

            InitializeComponent();
            this.Text = WingCommon.Common.WindowTitle("", "月次収支報告書出力");
            Init();
        }

        #region 初期化
        private void Init()
        {
            // 年度
            InitComboBox年度();
            // 期間
            InitComboBox期間();
            // 船
            InitComboBox船();
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


        private void InitComboBox船()
        {
            comboBox船.Items.Clear();

            comboBox船.Items.Add("全社");
            comboBox船.Items.Add("--------------------");

            foreach (MsVesselType v in DbAccessorFactory.FACTORY.MsVesselType_GetRecords(WingCommon.Common.LoginUser))
            {
                comboBox船.Items.Add(v);
            }

            comboBox船.Items.Add("--------------------");

            foreach (MsVessel v in DbAccessorFactory.FACTORY.MsVessel_GetRecords(WingCommon.Common.LoginUser))
            {
                comboBox船.Items.Add(v);
            }

            comboBox船.SelectedIndex = 0;
        }

        private void InitComboBox期間()
        {
            //foreach (string kikan in Constants.KIKAN)
            //{
            //    comboBox期間.Items.Add(kikan);
            //}
            // 2010.06.28 月次収支報告の対象期間は年度のみとする
            comboBox期間.Items.Add(Constants.KIKAN[0]);

            comboBox期間.SelectedIndex = 0;
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
                    string revStr = h.Revision.ToString();

                    if (!h.IsFixed())
                    {
                        revStr += " (未 Fix)";
                    }
                    else
                    {
                        revStr += " (" + h.FixDate.ToString("yyyy/MM/dd") + " Fix)";
                    }

                    comboBoxリビジョン.Items.Add(revStr);
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

        private bool 月次収支出力(string filename)
        {
            List<MsVessel> vessels = DbAccessorFactory.FACTORY.MsVessel_GetRecords(WingCommon.Common.LoginUser);

            //予算頭選択             
            BgYosanHead selectedYosanHead = this.Get選択BgYosanHead();

            //今回の単位を取得
            decimal unit = 1.0m;
            unit = 月次収支報告書出力Form.TaniComboRate[this.comboBox単位.SelectedIndex];

            string message = "";
            bool ret = true;
            try
            {
                byte[] excelData = null;

                int selectedIndex = this.comboBox船.SelectedIndex;
                MsVesselType selectedVesslType = null;
                MsVessel selectedVessl = null;
                if (comboBox船.SelectedItem is MsVesselType)
                {
                    selectedVesslType = (MsVesselType)comboBox船.SelectedItem;
                }
                else if (comboBox船.SelectedItem is MsVessel)
                {
                    selectedVessl = (MsVessel)comboBox船.SelectedItem;
                }
                bool 前年度実績check = checkBox前年度実績を出力.Checked;
                WingUtil.ProgressDialog progressDialog = new WingUtil.ProgressDialog(delegate
                {
                    using (WingServiceReferences.WingServer.ServiceClient sc = new WingServiceReferences.WingServer.ServiceClient())
                    {
                        // 全社
                        if (selectedIndex == 0)
                        {
                            excelData = sc.BLC_Excel月次収支報告書_取得_全社(WingCommon.Common.LoginUser,
                                selectedYosanHead, unit,
                                selectedIndex,
                                前年度実績check);
                        }
                        // グループ
                        else if (selectedVesslType != null)
                        {
                            excelData = sc.BLC_Excel月次収支報告書_取得_グループ(WingCommon.Common.LoginUser,
                               selectedYosanHead, unit,
                               selectedIndex, selectedVesslType,
                               前年度実績check);
                        }
                        // 船
                        else if (selectedVessl != null)
                        {
                            excelData = sc.BLC_Excel月次収支報告書_取得_船(WingCommon.Common.LoginUser,
                               selectedYosanHead, unit,
                               selectedIndex, selectedVessl,
                               前年度実績check);
                        }
                    }
                }, "月次収支報告書を作成中です...");
                progressDialog.ShowDialog();

                if (excelData == null)
                {
                    MessageBox.Show("月次収支報告書の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "月次収支", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    return false;
                }

                //バイナリをファイルに
                System.IO.FileStream filest = new System.IO.FileStream(filename, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
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
                MessageBox.Show("月次収支報告書の出力に失敗しました。\n (Err:" + message + ")", "月次収支", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            string smes = "「" + filename + "」に出力しました。";
            MessageBox.Show(smes, "月次収支報告書", MessageBoxButtons.OK, MessageBoxIcon.Information);


            return true;
        }

        private void button出力_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                this.月次収支出力(saveFileDialog1.FileName);

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
