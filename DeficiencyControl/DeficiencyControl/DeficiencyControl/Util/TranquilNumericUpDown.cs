using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeficiencyControl.Util
{
    /// <summary>
    /// マウスホイール無効化NumericUpDown
    /// </summary>
    public class TranquilNumericUpDown : NumericUpDown
    {

        /// <summary>
        /// マウスホイールイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //無効化する
            //base.OnMouseWheel(e);
        }
    }
}
