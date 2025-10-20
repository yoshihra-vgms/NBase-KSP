using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseUtil;
using Yojitsu.DA;

namespace Yojitsu
{
    public partial class ダイジェスト表示Form : Form
    {
        private List<BgYosanHead> yosanHeads;

        /// <summary>
        /// Constants.TANIに対応する数字Rate
        /// </summary>
        private static readonly decimal[] TaniComboRate = {
												   1000m,
												   1000000m,

											   };


        public ダイジェスト表示Form(List<BgYosanHead> yosanHeads)
        {
            this.yosanHeads = yosanHeads;

            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("", "ダイジェスト出力", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            Init();
        }


        private void Init()
        {
            // 年度
            InitComboBox年度();
            // 期間
            InitComboBox期間();
            // 予算種別
            InitComboBox予算種別();
            // 単位
            InitComboBox単位();
            // 範囲
            InitComboBox範囲();
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

        private void InitComboBox期間()
        {
            int thisMonth = DateTime.Now.Month;
            int monthIndex = thisMonth;
            if (thisMonth < 4)
            {
                monthIndex += 8;
            }
            else
            {
                monthIndex -= 4;
            }
            int index = 0;
            foreach (string month in Constants.MONTH)
            {
                comboBox期間.Items.Add(month);
                if (index == monthIndex)
                {
                    comboBox期間.SelectedIndex = index;
                }
                index++;
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

        private void InitComboBox単位()
        {
            foreach (string kikan in Constants.TANI)
            {
                comboBox単位.Items.Add(kikan);
            }

            comboBox単位.SelectedIndex = 0;
        }

        private void InitComboBox範囲()
        {
            foreach (string hani in Constants.HANI)
            {
                comboBox範囲.Items.Add(hani);
            }

            comboBox範囲.SelectedIndex = 0;
        }


        private void button出力_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "ダイジェスト表示"; // デフォルト設定

            saveFileDialog1.Filter = saveFileDialog1.FileName + " (*.xls)|*.xls";
            string filename = MakeFileName();
            saveFileDialog1.FileName = filename + ".xls";

            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                ダイジェスト表示出力(saveFileDialog1.FileName);

                Dispose();
            }
        }

        private string MakeFileName()
        {
            string fileName = saveFileDialog1.FileName;

            // 選択年度
            int bussinessYear = Int32.Parse(comboBox年度.Text);

            // 関連BgYosanHead
            BgYosanHead head = this.GetSelectYosanHeadData(bussinessYear);

            // 選択期間(月)
            int month = Int32.Parse(comboBox期間.Text.Replace("月", ""));

            // 選択年
            int year = bussinessYear;
            if (month < 4)
            {
                year++; // １～３月は、翌年なので１プラス
            }

            // 今回の単位を取得
            decimal unit = 1.0m;
            unit = ダイジェスト表示Form.TaniComboRate[this.comboBox単位.SelectedIndex];

            // 範囲を取得
            string hani = this.comboBox範囲.SelectedItem as string;

            fileName += "_" + year.ToString() + month.ToString("00") + "_" + hani;

            return fileName;
        }

        private void ダイジェスト表示出力(string fileName)
        {
            // 選択年度
            int bussinessYear = Int32.Parse(comboBox年度.Text);
                        
            // 関連BgYosanHead
            BgYosanHead head = this.GetSelectYosanHeadData(bussinessYear);

            // 選択期間(月)
            int month = Int32.Parse(comboBox期間.Text.Replace("月",""));
            
            // 選択年
            int year = bussinessYear;
            if (month < 4)
            {
                year++; // １～３月は、翌年なので１プラス
            }

            // 今回の単位を取得
            decimal unit = 1.0m;
            unit = ダイジェスト表示Form.TaniComboRate[this.comboBox単位.SelectedIndex];

            // 範囲を取得
            string hani = this.comboBox範囲.SelectedItem as string;
            int syear = year;
            int smonth = month;
            if (hani == Constants.HANI[1])
            {
                syear = bussinessYear;
                smonth = 4;
            }

            byte[] excelData = null;

            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    excelData = serviceClient.BLC_Excelダイジェスト表示(NBaseCommon.Common.LoginUser, head, unit, syear.ToString(), smonth.ToString("00"), year.ToString(), month.ToString("00"));
                }
            }, "ダイジェスト表示を作成中です...");
            progressDialog.ShowDialog();
            if (excelData == null)
            {
                MessageBox.Show("ダイジェスト表示の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (System.IO.FileStream stream = new System.IO.FileStream(fileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite))
            {
                stream.Write(excelData, 0, excelData.Length);
            }

            MessageBox.Show("ファイルを出力しました", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void comboBox年度_SelectionChangeCommitted(object sender, EventArgs e)
        {
            InitComboBox予算種別();
        }

        /// <summary>
        /// 現在選択されてる状態から関連するBgYosanHeadを取得する
        /// 引数：選択年、種別
        /// 返り値：関連データ
        /// </summary>
        /// <returns></returns>
        private BgYosanHead GetSelectYosanHeadData(int year)
        {
            BgYosanHead ans = null;

            List<BgYosanHead> revlist = new List<BgYosanHead>();
            revlist.Clear();

            //データ検索
            foreach (BgYosanHead h in this.yosanHeads)
            {
                //年月と種別IDに一致するものを選択する
                if (h.Year == year &&
                    BgYosanSbt.ToName(h.YosanSbtID) == comboBox予算種別.Text)
                {
                    revlist.Add(h);
                }
            }

            int rev = -1;

            //最大のRevを探す
            foreach (BgYosanHead data in revlist)
            {
                if (rev < data.Revision)
                {
                    rev = data.Revision;
                    ans = data;
                }
            }

            return ans;
        }
    }
}
