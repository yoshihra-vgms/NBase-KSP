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
    public partial class ユーザ検索Form : Form
    {
        private List<NBaseData.DAC.MsUser> userList;
        private List<NBaseData.DAC.MsBumon> bumonList;
        private NBaseData.DAC.MsUser user;
        public NBaseData.DAC.MsUser SelectedUser
        {
            get { return user; }
        }

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

        public ユーザ検索Form()
        {
            InitializeComponent();

            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "ユーザー検索", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
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
            dt.Columns.Add(new DataColumn("部門", typeof(string)));
            dt.Columns.Add(new DataColumn("区分", typeof(string)));

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
                row["部門"] = u.BumonName;

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
            if (Bumon_DropDownList.SelectedIndex == -1)
            {
                条件.UserBumonID = "-1";
            }
            else
            {
                条件.UserBumonID = ((ListItem)Bumon_DropDownList.SelectedItem).Value;
            }
            条件.AdminFlag = 2;

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
        /// 「検索条件クリア」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_button_Click(object sender, EventArgs e)
        {
            sei_textBox.Text = "";
            mei_textBox.Text = "";
            Bumon_DropDownList.SelectedIndex = -1;
            Jimusho_checkBox.Checked = true;
            Crew_checkBox.Checked = false;

            Init条件();
        }

        /// <summary>
        /// 「選択」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Choose_Btn_Click(object sender, EventArgs e)
        {
            user = dataGridView1.SelectedRows[0].Cells[0].Value as NBaseData.DAC.MsUser;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }


    }
}
