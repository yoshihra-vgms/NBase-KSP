using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeficiencyControl.Util
{
    /// <summary>
    /// SingleLineComboのGrid表示フォーム
    /// </summary>
    public partial class SingleLineComboForm : Form
    {
        public SingleLineComboForm()
        {
            InitializeComponent();
        }

        //自分の親
        public SingleLineCombo SLCombo = null;

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                this.SLCombo.SelectItem();
            }
        }

        private void dataGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    //selectItem = true;
                    //dataGridView1.Visible = false;
                    this.SLCombo.AutoCompleteGridVisible(false);
                    break;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.SLCombo.SelectItem();
            
        }


        //フォーカスを抜けた時は消す
        private void SingleLineComboForm_Leave(object sender, EventArgs e)
        {
            this.SLCombo.AutoCompleteGridVisible(false);
        }
        //フォーカスを抜けた時は消す
        private void SingleLineComboForm_Deactivate(object sender, EventArgs e)
        {
            this.SLCombo.AutoCompleteGridVisible(false);
        }
    }
}
