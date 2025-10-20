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
using NBaseCommon;
using NBaseData.DAC;
using NBaseData.DS;

namespace NBaseMaster.MsUser
{
    public partial class ユーザー管理新規登録Form : SeninSearchClientForm
    {
        private List<NBaseData.DAC.MsBumon> bumonList;
        private NBaseData.DAC.MsSenin msSenin;

        public ユーザー管理新規登録Form()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "ユーザー管理新規", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            MakeDropDownList();

            // デフォルトは「事務所」ユーザ
            UserKbn_radioButton1.Checked = true;
            button選択.Enabled = false;
            buttonクリア.Enabled = false;
            msSenin = null;

        }

        private void MakeDropDownList()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                bumonList = serviceClient.MsBumon_GetRecords(NBaseCommon.Common.LoginUser);
                foreach (NBaseData.DAC.MsBumon b in bumonList)
                {
                    Bumon_DropDownList.Items.Add(new ListItem(b.BumonName, b.MsBumonID));
                }
                Bumon_DropDownList.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 「登録」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Btn_Click(object sender, EventArgs e)
        {
            NBaseData.DAC.MsUser user = new NBaseData.DAC.MsUser();

            user.MsUserID = UserID_textBox.Text;
            if (user.MsUserID.Length < 1)
            {
                Message.Showエラー("ユーザＩＤを入力してください。");
                return;
            }
            user.Sei = Sei_textBox.Text;
            if (Sei_textBox.Text.Length < 1)
            {
                Message.Showエラー("氏名(姓)を入力してください。");
                return;
            }
            user.Mei = Mei_textBox.Text;
            if (Mei_textBox.Text.Length < 1)
            {
                Message.Showエラー("氏名(名)を入力してください。");
                return;
            }
            user.SeiKana = Sei_Kn_textBox.Text;
            user.MeiKana = Mei_Kn_textBox.Text;
            user.Sex = (Sex_radioButton1.Checked ? 0 : 1);
            user.LoginID = LoginID_textBox.Text;
            if (LoginID_textBox.Text.Length < 1)
            {
                Message.Showエラー("ログインIDを入力してください。");
                return;
            }
            user.Password = PassWord_textBox.Text;
            if (PassWord_textBox.Text.Length < 1)
            {
                Message.Showエラー("パスワードを入力してください。");
                return;
            }
            user.MailAddress = MailAddress_textBox.Text;

            user.UserKbn = (UserKbn_radioButton1.Checked ? 0 : 1);
            user.AdminFlag = (AdminFlag_radioButton1.Checked ? 0 : 1);
            user.RenewDate = DateTime.Today;
            user.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;


            user.DocFlag_CEO = (checkBox_経営責任者.Checked ? 1 : 0); 
            user.DocFlag_Admin = (checkBox_管理責任者.Checked ? 1 : 0);

            user.DocFlag_MsiFerry = (checkBox_海務監督_旅客.Checked ? 1 : 0);
            user.DocFlag_CrewFerry = (checkBox_船員担当者_旅客.Checked ? 1 : 0);
            user.DocFlag_TsiFerry = (checkBox_工務監督_旅客.Checked ? 1 : 0);

            user.DocFlag_Officer = (checkBox_役員.Checked ? 1 : 0);
            user.DocFlag_GL = (checkBox_GL.Checked ? 1 : 0);
            user.DocFlag_TL = (checkBox_TL.Checked ? 1 : 0);



            NBaseData.DAC.MsUserBumon Ubumon = new NBaseData.DAC.MsUserBumon();
            Ubumon.MsUserBumonID = Guid.NewGuid().ToString();

            Ubumon.MsBumonID = ((ListItem)Bumon_DropDownList.SelectedItem).Value;
            Ubumon.MsUserID = user.MsUserID;
            Ubumon.RenewDate = DateTime.Today;
            Ubumon.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            Ubumon.SendFlag = 0;
            Ubumon.VesselID = 0;

            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                //ユーザＩＤが重複していないか調べる
                NBaseData.DAC.MsUser ExistUser = serviceClient.MsUser_GetRecordsByUserID(NBaseCommon.Common.LoginUser, user.MsUserID);
                if (ExistUser != null)
                {
                    Message.Showエラー("ユーザＩＤはすでに登録されています");
                    return;
                }
                ExistUser = serviceClient.MsUser_GetRecordsByLoginID(NBaseCommon.Common.LoginUser, user.LoginID);
                if (ExistUser != null)
                {
                    Message.Showエラー("ログインＩＤはすでに登録されています");
                    return;
                }
                ret = serviceClient.BLC_ユーザ情報更新処理_新規作成(NBaseCommon.Common.LoginUser, user, Ubumon, msSenin);
            }
            if (ret == false)
            {
                Message.Showエラー("更新に失敗しました。");
                return;
            }
            else
            {
                Message.Show確認("更新しました。");
                DialogResult = DialogResult.OK;
                this.Close();
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


        /// <summary>
        /// 「区分」-「船員」をクリックしたときに、「船員検索」画面を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserKbn_radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (UserKbn_radioButton2.Checked == true)
            {
                船員検索Form form = new 船員検索Form(this, true);
                form.SetSenin(this.msSenin);
                form.ShowDialog();

                button選択.Enabled = true;
                buttonクリア.Enabled = true;
            }
            else
            {
                this.msSenin = null;
                textBox_氏名コード.Text = "";

                button選択.Enabled = false;
                buttonクリア.Enabled = false;
            }
        }

        /// <summary>
        /// 船員検索からコールされる船員検索の実処理
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public override List<MsSenin> SearchMsSenin(MsSeninFilter filter)
        {
            List<MsSenin> result = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.MsSenin_GetRecordsByFilter(NBaseCommon.Common.LoginUser, filter);
            }
            return result;
        }

        /// <summary>
        /// 船員検索からコールされる船員選択の実処理
        /// </summary>
        /// <param name="senin"></param>
        public override bool SetMsSenin(MsSenin senin, bool check)
        {
            if (check)
            {
                if (senin.MsUserID != null && senin.MsUserID.Length > 0)
                {
                    if (MessageBox.Show("この船員は、別のユーザ情報に紐付けられています。よろしいですか。", "確認", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return false;
                    }
                }
            }
            this.msSenin = senin;
            textBox_氏名コード.Text = senin.ShimeiCode.Trim();
            
            return true;
        }

        private void button選択_Click(object sender, EventArgs e)
        {
            船員検索Form form = new 船員検索Form(this, true);
            form.SetSenin(this.msSenin);
            form.ShowDialog();
        }

        private void buttonクリア_Click(object sender, EventArgs e)
        {
            this.msSenin = null;
            textBox_氏名コード.Text = "";
        }
    }
}
