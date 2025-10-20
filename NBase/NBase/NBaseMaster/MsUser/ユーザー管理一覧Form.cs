using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using NBaseMaster.MsUser;

namespace NBaseMaster
{
    public partial class MsUserForm : Form
    {
        private List<NBaseData.DAC.MsUser> userList;
        private List<NBaseData.DAC.MsBumon> bumonList;
        private NBaseData.DAC.MsUserBumon Ubumon;

        private class 検索条件
        {
            public string LoginID;
            public int UserKbn;
            public int AdminFlag;
            public string UserBumonID;
            public string Sei;
            public string Mei;
        }
        private 検索条件 条件 = null;

        public MsUserForm()
        {
            InitializeComponent();

            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "ユーザー管理一覧", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            Init条件();
            MakeDropDownList();
            MakeGrid(null);
        }

        private void Init条件()
        {
            条件 = new 検索条件();
            条件.LoginID = "";
            条件.UserKbn = -1;
            条件.AdminFlag = -1;
            条件.UserBumonID = "";
            条件.Sei = "";
            条件.Mei = "";
        }
        private void MakeDropDownList()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                bumonList = serviceClient.MsBumon_GetRecords(NBaseCommon.Common.LoginUser);
                Bumon_DropDownList.Items.Add(new ListItem("", ""));
                foreach (NBaseData.DAC.MsBumon b in bumonList)
                {
                    Bumon_DropDownList.Items.Add(new ListItem(b.BumonName, b.MsBumonID));
                }
            }
        }

        private void MakeGrid(List<NBaseData.DAC.MsUser> USER)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("obj", typeof(NBaseData.DAC.MsUser)));
            dt.Columns.Add(new DataColumn("氏名", typeof(string)));
            dt.Columns.Add(new DataColumn("ログインID", typeof(string)));
            dt.Columns.Add(new DataColumn("区分", typeof(string)));
            dt.Columns.Add(new DataColumn("部門", typeof(string)));
            dt.Columns.Add(new DataColumn("管理者フラグ", typeof(string)));

            if (USER == null)
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    userList = serviceClient.MsUser_SearchRecords(NBaseCommon.Common.LoginUser, 条件.LoginID, 条件.UserKbn, 条件.AdminFlag, 条件.UserBumonID, 条件.Sei, 条件.Mei);
                }
            }
            else
            {
                userList = USER;
            }
            CreateRowsName(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
        }

        private void CreateRowsName(DataTable dt)
        {
            foreach (NBaseData.DAC.MsUser u in userList)
            {
                DataRow row = dt.NewRow();
                row["obj"] = u;
                row["氏名"] = u.Sei + u.Mei;
                row["ログインID"] = u.LoginID;

                string UserKbnName = "";
                if (u.UserKbn == 0)
                {
                    UserKbnName = "事務所";
                }
                else if (u.UserKbn == 1)
                {
                    UserKbnName = "船員";
                }
                row["区分"] = UserKbnName;
                row["部門"] = u.BumonName;

                string AdminFlagName = "";
                if (u.AdminFlag == 0)
                {
                    AdminFlagName = "通常ユーザ";
                }
                else if (u.AdminFlag == 1)
                {
                    AdminFlagName = "管理者";
                }
                row["管理者フラグ"] = AdminFlagName;

                dt.Rows.Add(row);
            }
        }
               
        /// <summary>
        /// 「検索」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Btn_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            // 検索条件
            #region
            条件.Sei = sei_textBox.Text;
            条件.Mei = mei_textBox.Text;
            条件.LoginID = LoginID_textBox.Text;
            if (Jimusho_checkBox.Checked == true && Crew_checkBox.Checked == true)
            {
                条件.UserKbn = 2;
            }
            else if (Jimusho_checkBox.Checked == true)
            {
                条件.UserKbn = 0;
            }
            else if (Crew_checkBox.Checked == true)
            {
                条件.UserKbn = 1;
            }
            else
            {
                条件.UserKbn = -1;
            }

            if (Office_checkBox.Checked == true && Admin_checkBox.Checked == true)
            {
                条件.AdminFlag = 2;
            }
            else if (Office_checkBox.Checked == true)
            {
                条件.AdminFlag = 0;
            }
            else if (Admin_checkBox.Checked == true)
            {
                条件.AdminFlag = 1;
            }
            else
            {
                条件.AdminFlag = -1;
            }
            if (Bumon_DropDownList.SelectedIndex == -1)
            {
                条件.UserBumonID = "-1";
            }
            else
            {
                条件.UserBumonID = ((ListItem)Bumon_DropDownList.SelectedItem).Value;
            }
            #endregion

            //検索
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                userList = serviceClient.MsUser_SearchRecords(NBaseCommon.Common.LoginUser, 条件.LoginID, 条件.UserKbn, 条件.AdminFlag, 条件.UserBumonID, 条件.Sei, 条件.Mei);
            }

            // 一覧へ表示
            MakeGrid(userList);

            this.Cursor = Cursors.Default;

            if (userList.Count == 0)
            {
                Message.Show確認("該当するユーザーがいません。");
            }
        }

        /// <summary>
        /// 「新規」「編集｣後の一覧の再表示（条件は、最後に検索した条件を使用）
        /// </summary>
        private void 再表示()
        {
            this.Cursor = Cursors.WaitCursor;

            //検索
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                userList = serviceClient.MsUser_SearchRecords(NBaseCommon.Common.LoginUser, 条件.LoginID, 条件.UserKbn, 条件.AdminFlag, 条件.UserBumonID, 条件.Sei, 条件.Mei);
            }

            // 一覧へ表示
            MakeGrid(userList);

            this.Cursor = Cursors.Default;

            if (userList.Count == 0)
            {
                Message.Show確認("該当するユーザーがいません。");
            }
        }

        /// <summary>
        /// 一覧のダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //string row = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            //foreach (NBaseData.DAC.MsUser u in userList)
            //{
            //    if (u.LoginID == row)
            //    {
            //        ユーザー管理詳細Form form = new ユーザー管理詳細Form(u);
            //        if (form.ShowDialog() == DialogResult.OK)
            //        {
            //            再表示();
            //        }
            //    }
            //}
            NBaseData.DAC.MsUser msUser = dataGridView1.SelectedRows[0].Cells[0].Value as NBaseData.DAC.MsUser;
            ユーザー管理詳細Form form = new ユーザー管理詳細Form(msUser);
            if (form.ShowDialog() == DialogResult.OK)
            {
                再表示();
            }
        }     

        /// <summary>
        /// 「新規」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Btn_Click(object sender, EventArgs e)
        {
            ユーザー管理新規登録Form form = new ユーザー管理新規登録Form();
            if (form.ShowDialog() == DialogResult.OK)
            {
                再表示();
            }
        }

        /// <summary>
        /// 「戻る」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Clear_button_Click(object sender, EventArgs e)
        {
            sei_textBox.Text = "";
            mei_textBox.Text = "";
            LoginID_textBox.Text = "";
            Bumon_DropDownList.SelectedIndex = -1;
            Jimusho_checkBox.Checked = true;
            Crew_checkBox.Checked = false;
            Office_checkBox.Checked = true;
            Admin_checkBox.Checked = true;

            Init条件();
        }
    }
}
