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

namespace Hachu.Reports
{
    public partial class 管理表Form : Form
    {
        private BaseData 出力対象データ = null;

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




        public 管理表Form(BaseData bd)
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
            YearValue initialBusinessYear = null;
            MonthValue initialMonth = null;
            int thisYear = DateTime.Now.Year;
            int thisMonth = DateTime.Now.Month;

            // 年ComboBox初期化
            comboBox年度.Items.Clear();
            for (int y = thisYear; y >= (thisYear - 5); y--)
            {
                YearValue yearValue = new YearValue();
                yearValue.Year = y;
                comboBox年度.Items.Add(yearValue);
                if (y == NBaseUtil.DateTimeUtils.年度開始日().Year)
                {
                    initialBusinessYear = yearValue;
                }
            }

            // 月ComboBox初期化
            comboBox月.Items.Clear();
            for (int i = 0; i < 12; i++)
            {
                MonthValue monthValue = new MonthValue();
                monthValue.Month = int.Parse(DateTimeUtils.instance().MONTH[i]);
                comboBox月.Items.Add(monthValue);
                if (monthValue.Month == thisMonth)
                {
                    initialMonth = monthValue;
                }
            }

            // 初期表示
            comboBox年度.SelectedItem = initialBusinessYear;
            comboBox月.SelectedItem = initialMonth;

        }
        #endregion


        /// <summary>
        /// 「出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button出力_Click(object sender, EventArgs e)
        {
            YearValue selectedBusinessYear = comboBox年度.SelectedItem as YearValue;
            MonthValue selectedMonth = comboBox月.SelectedItem as MonthValue;

            // 開始は、年度の初め
            int fromYear = selectedBusinessYear.Year;
            int fromMonth = NBaseCommon.Common.FiscalYearStartMonth;

            // 選択された月までのデータが出力対象
            int toYear = selectedBusinessYear.Year;
            int toMonth = selectedMonth.Month;            
            if (toMonth < fromMonth)
            {
                toYear++;
            }

            fromYear = selectedBusinessYear.Year;

            出力対象データ.Output(fromYear, fromMonth, toYear, toMonth);

            Close();
        }
    }
}
