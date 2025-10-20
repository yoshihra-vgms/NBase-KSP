using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NBaseHonsen
{
    public partial class MessageForm : Form
    {
        public MessageForm(string message)
        {
            InitializeComponent();

            textBox1.Text = message;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
