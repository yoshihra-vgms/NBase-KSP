using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dousei
{
    public partial class 連携メッセージ : Form
    {
        public string Message = "";
        public 連携メッセージ()
        {
            InitializeComponent();
        }

        private void Close_button_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("画面を閉じますか?", "閉じる", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Close();
            }
        }

        private void 連携メッセージ_Load(object sender, EventArgs e)
        {
            Message_textBox.Text = Message;
        }
    }
}
