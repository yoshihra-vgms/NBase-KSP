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
    /// データ件数表示コントロール
    /// </summary>
    public partial class DataCountControl : BaseControl
    {
        public DataCountControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// データ件数
        /// </summary>
        public int DataCount
        {
            set
            {
                this.labelCounter.Text = value.ToString();
            }

        }


        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataCountControl_Load(object sender, EventArgs e)
        {

        }
    }
}
