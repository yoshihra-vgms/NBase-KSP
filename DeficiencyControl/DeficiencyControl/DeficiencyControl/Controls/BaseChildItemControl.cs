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
    /// 子供アイテム制御コントロール
    /// </summary>
    public partial class BaseChildItemControl : BaseControl
    {
        public BaseChildItemControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Deleteチェック取得
        /// </summary>
        public bool DeleteCheck
        {
            get
            {
                return this.checkBoxDelete.Checked;
            }
        }

        private void BaseChildItemControl_Load(object sender, EventArgs e)
        {

        }
    }
}
