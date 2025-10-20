using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseCommon;

namespace NBaseHonsen.Document
{
    public partial class 文書メニュー : ExForm
    {
        public 文書メニュー()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            管理記録一覧Form form = new 管理記録一覧Form();
            form.parentForm = this; // 2012.02 : Add
            form.ShowDialog();
            this.WindowState = FormWindowState.Normal; // 2012.02 : Add
        }

        private void button2_Click(object sender, EventArgs e)
        {
            個別文書登録Form form = new 個別文書登録Form();
            form.parentForm = this; // 2012.02 : Add
            form.ShowDialog();
            this.WindowState = FormWindowState.Normal; // 2012.02 : Add
        }

        private void button3_Click(object sender, EventArgs e)
        {
            状況確認一覧Form form = new 状況確認一覧Form();
            form.parentForm = this; // 2012.02 : Add
            form.ShowDialog();
            this.WindowState = FormWindowState.Normal; // 2012.02 : Add
        }

        private void 文書メニュー_Load(object sender, EventArgs e)
        {
            this.ChangeParentFormSize(FormWindowState.Minimized);
            this.Activate();
        }

        private void 文書メニュー_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.ChangeParentFormSize(FormWindowState.Normal);
            this.ChangeParentFormSize(FormWindowState.Maximized);
        }
    }
}
