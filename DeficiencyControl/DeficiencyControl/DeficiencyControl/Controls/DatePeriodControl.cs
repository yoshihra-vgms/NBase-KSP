using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeficiencyControl.Controls
{
    /// <summary>
    /// 期間選択用日付コントロール
    /// </summary>
    public partial class DatePeriodControl : UserControl
    {
        public DatePeriodControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// データ一括設定
        /// </summary>
        /// <param name="st"></param>
        /// <param name="ed"></param>
        /// <param name="chk"></param>
        public void SetDate(DateTime st, DateTime ed, bool chk)
        {
            this.linkedDatetimePickerDateStart.Value = st;
            this.linkedDatetimePickerDateEnd.Value = ed;

            this.linkedDatetimePickerDateStart.Checked = chk;
            this.linkedDatetimePickerDateEnd.Checked = chk;
        }
                

        private void DatePeriodControl_Load(object sender, EventArgs e)
        {

        }
    }
}
