using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBaseHonsen
{
    public partial class MaintenanceMessageForm : Form
    {
        public string Caption
        {
            set
            {
                this.Text = value;
            }
        }
        public string Message
        {
            set
            {
                textBox_Message.Text = value.Replace("\n", "\r\n");
            }
        }

        public MaintenanceMessageForm()
        {
            InitializeComponent();
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MaintenanceMessageForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
