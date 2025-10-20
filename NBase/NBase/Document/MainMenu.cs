using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DS;
using NBaseCommon;

namespace Document
{
    public partial class MainMenu : ExForm
    {
        public MainMenu()
        {
            InitializeComponent();
            EnableComponents();
        }


        private void EnableComponents()
        {
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "文書管理", "管理記録登録", null))
            {
                button1.Enabled = true;
            }
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "文書管理", "公文書_規則登録", null))
            {
                button2.Enabled = true;
            }
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "文書管理", "文書管理状況確認", null))
            {
                button3.Enabled = true;
            }
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

        private void MainMenu_Load(object sender, EventArgs e)
        {
            this.ChangeParentFormSize(FormWindowState.Minimized);
            this.Activate();
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ChangeParentFormSize(FormWindowState.Normal);
        }
    }
}
