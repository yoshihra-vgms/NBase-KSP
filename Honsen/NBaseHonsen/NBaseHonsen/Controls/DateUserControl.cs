using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NBaseHonsen.Controls
{
    public partial class DateUserControl : UserControl
    {
        public DateTime Value { get; set; }
        public bool IsJiseki = false;

        public DateUserControl()
        {
            InitializeComponent();
        }

        private void DateUserControl_Load(object sender, EventArgs e)
        {
            label1.Text = Value.ToString("MM/dd(ddd)");

            if (IsJiseki == true)
            {
                BackColor = Color.LightGray;
            }
            else
            {
                BackColor = Color.White;
            }
        }
    }
}
