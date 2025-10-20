using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.BLC;

namespace Hachu.Reports
{
    public partial class 貯蔵品リストForm : Form
    {
        string BaseFileName = "貯蔵品リスト";
        NBaseData.BLC.貯蔵品リスト.対象Enum kind;

        public class YearValue
        {
            public override string ToString()
            {
                return Year.ToString();
            }
            public int Year { get; set; }
        }
        public class MonthValue
        {
            public override string ToString()
            {
                return Month.ToString();
            }
            public int Month { get; set; }
        }
        protected string formNumber;
        public string FormNumber
        {
            get { return formNumber; }
        }
        protected string className;
        public string ClassName
        {
            get { return className; }
        }

        public 貯蔵品リストForm(string fn, string cn, NBaseData.BLC.貯蔵品リスト.対象Enum kind)
        {
            this.formNumber = fn;
            this.className = cn;
            this.kind = kind;

            InitializeComponent();
        }

        private void 貯蔵品リストForm_Load(object sender, EventArgs e)
        {
            this.Text = NBaseCommon.Common.WindowTitle(FormNumber, ClassName, ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            検索条件初期化();
        }

        /// <summary>
        /// 検索条件を初期化する
        /// </summary>
        #region private void 検索条件初期化()
        private void 検索条件初期化()
        {
            YearValue initialYear = null;
            MonthValue initialMonth = null;
            YearValue yearValue = null;
            MonthValue monthValue = null;
            int thisYear = DateTime.Now.Year;
            int thisMonth = DateTime.Now.Month;

            // 年ComboBox初期化
            comboBox年.Items.Clear();
            for (int y = thisYear; y >= (thisYear - 5); y--)
            {
                yearValue = new YearValue();
                yearValue.Year = y;
                comboBox年.Items.Add(yearValue);
                if (thisYear == y)
                {
                    initialYear = yearValue;
                }
            }

            // 月ComboBox初期化
            comboBox月.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                monthValue = new MonthValue();
                monthValue.Month = i;
                comboBox月.Items.Add(monthValue);
                if (thisMonth == i)
                {
                    initialMonth = monthValue;
                }
            }

            // 初期表示
            comboBox年.SelectedItem = initialYear;
            comboBox月.SelectedItem = initialMonth;
        }
        #endregion

        /// <summary>
        /// 「出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button出力_Click(object sender, EventArgs e)
        private void button出力_Click(object sender, EventArgs e)
        {
            YearValue selectedYear = comboBox年.SelectedItem as YearValue;
            MonthValue selectedMonth = comboBox月.SelectedItem as MonthValue;

            Output(selectedYear.Year, selectedMonth.Month);

            Close();
        }
        #endregion

        /// <summary>
        /// 出力処理
        /// </summary>
        /// <param name="selectedYear"></param>
        /// <param name="selectedMonth"></param>
        #region private void Output(int selectedYear, int selectedMonth)
        private void Output(int selectedYear, int selectedMonth)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = BaseFileName + " (*.xlsx)|*.xlsx";
            fd.FileName = BaseFileName + ".xlsx";
            NBaseUtil.FileUtils.SetDesktopFolder(fd);

            if (fd.ShowDialog() == DialogResult.OK)
            {
                string message = "";
                bool outputResult = false;
                try
                {
                    byte[] excelData = null;
                    NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                    {
                        excelData = MakeFile(selectedYear, selectedMonth);
                    }, "貯蔵品リストを作成中です...");
                    progressDialog.ShowDialog();
                    if (excelData == null)
                    {
                        MessageBox.Show("貯蔵品リストの出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    System.IO.FileStream filest = new System.IO.FileStream(fd.FileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
                    filest.Write(excelData, 0, excelData.Length);
                    filest.Close();

                    outputResult = true;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    outputResult = false;
                }
                if (outputResult == true)
                {
                    // 成功
                    message = "「" + fd.FileName + "」へ出力しました";
                    MessageBox.Show(message, "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // 失敗 
                    MessageBox.Show("貯蔵品リストの出力に失敗しました。\n (Err:" + message + ")", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        /// <summary>
        /// サーバに出力依頼、出力結果を取得する
        /// </summary>
        /// <param name="selectedYear"></param>
        /// <param name="selectedMonth"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        #region private byte[] MakeFile(int selectedYear, int selectedMonth)
        private byte[] MakeFile(int selectedYear, int selectedMonth)
        {
            byte[] excelData = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                excelData = serviceClient.BLC_貯蔵品リスト_取得(NBaseCommon.Common.LoginUser, selectedYear, selectedMonth, kind);
            }
            return excelData;
        }
        #endregion
    }
}
