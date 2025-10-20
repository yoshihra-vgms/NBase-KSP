using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Senin.util
{
    public partial class SeninUserControl : UserControl
    {
        public SeninUserControl()
        {
            InitializeComponent();;
        }
        public SeninUserControl(string shokumei, string name, string date, string days)
        {
            InitializeComponent();

            textBox1.Text = shokumei;
            textBox2.Text = name;
            textBox3.Text = date;
            textBox4.Text = days;
        }
    }


}
