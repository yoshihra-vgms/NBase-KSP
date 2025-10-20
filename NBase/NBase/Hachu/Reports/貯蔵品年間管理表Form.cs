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
    public partial class 貯蔵品年間管理表Form : Form
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

        public 貯蔵品年間管理表Form(BaseData bd)
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

            //// 船ComboBox初期化
            //comboBox船.Items.Clear();
            //foreach (MsVessel v in vessels)
            //{
            //    comboBox船.Items.Add(v);
            //}

            // 初期表示
            comboBox年.SelectedIndex = 0;
            //comboBox船.SelectedIndex = -1;

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
            //MsVessel selectedVessel = comboBox船.SelectedItem as MsVessel;
            
            int fromYear = selectedYear.Year;
            //出力対象データ.Output(selectedVessel, fromYear);
            出力対象データ.Output(null, fromYear);

            Close();
        }
    }
}
