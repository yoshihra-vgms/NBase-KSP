using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBaseCommon
{
    public partial class MessageForm : Form
    {
        public string Message
        {
            set
            {
                textBox1.Text = value;
            }
        }

        public MessageBoxButtons Buttons
        {
            set
            {
                if (value == MessageBoxButtons.YesNo)
                {
                    buttonYes.Location = new Point(160, 129);
                    buttonNo.Location = new Point(253, 129);
                    buttonNo.Visible = true;
                }
                else
                {
                    buttonYes.Location = new Point(207, 129);
                    buttonNo.Visible = false;
                }
            }
        }
        

        public MessageForm()
        {
            InitializeComponent();
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }
    }
}
