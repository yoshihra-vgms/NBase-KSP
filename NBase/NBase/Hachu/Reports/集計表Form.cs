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
    public partial class 集計表Form : Form
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




        public 集計表Form(BaseData bd)
        {
            InitializeComponent();
            出力対象データ = bd;
        }

        private void 集計表Form_Load(object sender, EventArgs e)
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
            YearValue initialYear = null;
            int thisYear = NBaseUtil.DateTimeUtils.年度開始日(DateTime.Today).Year;

            // 年ComboBox初期化
            comboBox年.Items.Clear();
            for (int y = thisYear; y >= (thisYear - 5); y--)
            {
                YearValue yearValue = new YearValue();
                yearValue.Year = y;
                comboBox年.Items.Add(yearValue);
                if (y == thisYear)
                {
                    initialYear = yearValue;
                }
            }

            // 初期表示
            comboBox年.SelectedItem = initialYear;
        }
        #endregion


        /// <summary>
        /// 「出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button出力_Click(object sender, EventArgs e)
        {
            // 選択されている年度
            YearValue selectedYear = comboBox年.SelectedItem as YearValue;
            int fromYear = selectedYear.Year;
            
            // 選択されている出力データ
            List<bool> targets = new List<bool>();
            targets.Add(checkBox1.Checked); // 第一四半期
            targets.Add(checkBox2.Checked); // 第二四半期
            targets.Add(checkBox3.Checked); // 第三四半期
            targets.Add(checkBox4.Checked); // 第四四半期
            bool chkOk = false;
            foreach (bool chk in targets)
            {
                if (chk)
                {
                    chkOk = true;
                    break;
                }
            }
            if (chkOk == false)
            {
                MessageBox.Show("出力データを選択してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 出力
            出力対象データ.Output(fromYear, targets);

            Close();
        }
    }
}
