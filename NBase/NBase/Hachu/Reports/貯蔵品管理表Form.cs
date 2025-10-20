using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;

namespace Hachu.Reports
{
    public partial class 貯蔵品管理表Form : Form
    {
        private BaseData 出力対象データ = null;

        private static int FiscalYearStartMonth = 4;

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
                return Text;
            }
            public int FromMonth { get; set; }
            public int ToMonth { get; set; }
            public string Text { get; set; }
        }
        public 貯蔵品管理表Form(BaseData bd)
        {
            InitializeComponent();
            出力対象データ = bd;
        }

        private void 管理表Form_Load(object sender, EventArgs e)
        {
            this.Text = NBaseCommon.Common.WindowTitle(出力対象データ.FormNumber, 出力対象データ.ClassName, ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            検索条件初期化();
        }

        /// <summary>
        /// 検索条件を初期化する
        /// </summary>
        #region private void 検索条件初期化()
        private void 検索条件初期化()
        {
            // 2013.2: 2012年度改造
            //List<MsVessel> vessels = null;
            //using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            //{
            //    vessels = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);
            //}

            // 年ComboBox初期化
            comboBox年.Items.Clear();
            int thisYear = NBaseUtil.DateTimeUtils.年度開始日(DateTime.Today).Year;
            for (int y = thisYear; y >= (thisYear - 5); y--)
            {
                YearValue yearValue = new YearValue();
                yearValue.Year = y;
                comboBox年.Items.Add(yearValue);
            }

            // 月ComboBox初期化
            comboBox月.Items.Clear();
            MonthValue monthValue = new MonthValue();
            monthValue.FromMonth = 4;
            monthValue.ToMonth = 3;
            monthValue.Text = "年度（4月～3月）";
            comboBox月.Items.Add(monthValue);
            for (int i = 4; i <= 12; i++)
            {
                monthValue = new MonthValue();
                monthValue.FromMonth = i;
                monthValue.ToMonth = i;
                monthValue.Text = i.ToString() + " 月";
                comboBox月.Items.Add(monthValue);
            }
            for (int i = 1; i <= 3; i++)
            {
                monthValue = new MonthValue();
                monthValue.FromMonth = i;
                monthValue.ToMonth = i;
                monthValue.Text = i.ToString() + " 月";
                comboBox月.Items.Add(monthValue);
            }
            monthValue = new MonthValue();
            monthValue.FromMonth = 4;
            monthValue.ToMonth = 6;
            monthValue.Text = "四半期 （4月～6月）";
            comboBox月.Items.Add(monthValue);
            monthValue = new MonthValue();
            monthValue.FromMonth = 7;
            monthValue.ToMonth = 9;
            monthValue.Text = "四半期 （7月～9月）";
            comboBox月.Items.Add(monthValue);
            monthValue = new MonthValue();
            monthValue.FromMonth = 10;
            monthValue.ToMonth = 12;
            monthValue.Text = "四半期 （10月～12月）";
            comboBox月.Items.Add(monthValue);
            monthValue = new MonthValue();
            monthValue.FromMonth = 1;
            monthValue.ToMonth = 3;
            monthValue.Text = "四半期 （1月～3月）";
            comboBox月.Items.Add(monthValue);
            monthValue = new MonthValue();
            monthValue.FromMonth = 4;
            monthValue.ToMonth = 9;
            monthValue.Text = "上期 （4月～9月）";
            comboBox月.Items.Add(monthValue);
            monthValue = new MonthValue();
            monthValue.FromMonth = 10;
            monthValue.ToMonth = 3;
            monthValue.Text = "下期 （10月～3月）";
            comboBox月.Items.Add(monthValue);

            //// 船ComboBox初期化
            //comboBox船.Items.Clear();
            //foreach (MsVessel v in vessels)
            //{
            //    comboBox船.Items.Add(v);
            //}

            // 初期表示
            comboBox年.SelectedIndex = 0;
            comboBox月.SelectedIndex = 0;
            //comboBox船.SelectedIndex = -1;

            comboBox月.Visible = true;
            //comboBox船.Visible = true;
        }
        #endregion


        /// <summary>
        /// 「出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button出力_Click(object sender, EventArgs e)
        {
            //if (!(comboBox船.SelectedItem is MsVessel))
            //{
            //    MessageBox.Show("船を選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            YearValue selectedYear = comboBox年.SelectedItem as YearValue;
            MonthValue selectedMonth = comboBox月.SelectedItem as MonthValue;
            //MsVessel selectedVessel = comboBox船.SelectedItem as MsVessel;
            int fromMonth = selectedMonth.FromMonth;
            int toMonth = selectedMonth.ToMonth;
            int fromYear = selectedYear.Year;
            int toYear = selectedYear.Year;
            if ( fromMonth <  FiscalYearStartMonth && toMonth <  FiscalYearStartMonth)
            {
                fromYear ++;
                toYear ++;
            }
            else if ( fromMonth > toMonth )
            {
                toYear ++;
            }
            //出力対象データ.Output(selectedVessel, fromYear, fromMonth, toYear, toMonth);
            出力対象データ.Output(null, fromYear, fromMonth, toYear, toMonth);

            Close();
        }
    }
}
