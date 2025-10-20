using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using NBaseData.DAC;
using NBaseCommon;
using NBaseData.DS;

namespace NBaseMaster.MsUser
{
    public partial class ユーザー管理詳細Form : SeninSearchClientForm
    {
        //private List<NBaseData.DAC.MsUser> userList;
        private List<NBaseData.DAC.MsBumon> bumonList;
        private NBaseData.DAC.MsUserBumon Ubumon;
        private NBaseData.DAC.MsUser msUser;
        private NBaseData.DAC.MsSenin msSenin;

        public ユーザー管理詳細Form(NBaseData.DAC.MsUser target)
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "ユーザー管理詳細", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            msUser = target;
            msSenin = null;
            SetItems(msUser);
        }

        private void SetItems(NBaseData.DAC.MsUser target)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                bumonList = serviceClient.MsBumon_GetRecords(NBaseCommon.Common.LoginUser);

                UserID_textBox.Text = target.MsUserID;
                Sei_textBox.Text = target.Sei;
                Mei_textBox.Text = target.Mei;
                Sei_Kn_textBox.Text = target.SeiKana;
                Mei_Kn_textBox.Text = target.MeiKana;

                if (target.Sex == 0)
                {
                    Sex_radioButton1.Checked = true;
                }
                else
                {
                    Sex_radioButton2.Checked = true;
                }

                Login_textBox.Text = target.LoginID;
                PassWord_textBox.Text = target.Password;
                MailAddress_textBox.Text = target.MailAddress;

                if (target.UserKbn == 0)
                {
                    UserKbn_radioButton1.Checked = true;

                    button選択.Enabled = false;
                    buttonクリア.Enabled = false;
                }
                else
                {
                    UserKbn_radioButton2.CheckedChanged -= new EventHandler(UserKbn_radioButton2_CheckedChanged);
                    UserKbn_radioButton2.Checked = true;
                    UserKbn_radioButton2.CheckedChanged += new EventHandler(UserKbn_radioButton2_CheckedChanged);

                    button選択.Enabled = true;
                    buttonクリア.Enabled = true;
                }

                if (target.AdminFlag == 0)
                {
                    AdminFlag_radioButton1.Checked = true;
                }
                else
                {
                    AdminFlag_radioButton2.Checked = true;
                }



                if (target.DocFlag_CEO == 1)
                    checkBox_経営責任者.Checked = true;
                else
                    checkBox_経営責任者.Checked = false;

                if (target.DocFlag_Admin == 1)
                    checkBox_管理責任者.Checked = true;
                else
                    checkBox_管理責任者.Checked = false;

                if (target.DocFlag_MsiFerry == 1)
                    checkBox_海務監督_旅客.Checked = true;
                else
                    checkBox_海務監督_旅客.Checked = false;

                if (target.DocFlag_CrewFerry == 1)
                    checkBox_船員担当者_旅客.Checked = true;
                else
                    checkBox_船員担当者_旅客.Checked = false;

                if (target.DocFlag_TsiFerry == 1)
                    checkBox_工務監督_旅客.Checked = true;
                else
                    checkBox_工務監督_旅客.Checked = false;

                if (target.DocFlag_Officer == 1)
                    checkBox_役員.Checked = true;
                else
                    checkBox_役員.Checked = false;

                if (target.DocFlag_GL == 1)
                    checkBox_GL.Checked = true;
                else
                    checkBox_GL.Checked = false;

                if (target.DocFlag_TL == 1)
                    checkBox_TL.Checked = true;
                else
                    checkBox_TL.Checked = false;




                Bumon_DropDownList.Items.Add(new ListItem("", ""));
                foreach (NBaseData.DAC.MsBumon b in bumonList)
                {
                    Bumon_DropDownList.Items.Add(new ListItem(b.BumonName, b.MsBumonID));
                }

                //所属している部門を取得
                Ubumon = serviceClient.MsUserBumon_GetRecordsByUserID(NBaseCommon.Common.LoginUser, target.MsUserID);
                if (Ubumon == null)
                {
                    Ubumon = new NBaseData.DAC.MsUserBumon();
                    Bumon_DropDownList.SelectedIndex = 0;
                }
                else
                {
                    for (int i = 0; i < bumonList.Count; i++)
                    {
                        if (bumonList[i].MsBumonID == Ubumon.MsBumonID)
                        {
                            Bumon_DropDownList.SelectedIndex = i + 1;
                        }
                    }
                }

                // 船員情報
                if (target.UserKbn == 1)
                {
                    List<MsSenin> senins = serviceClient.MsSenin_GetRecordsByMsUserID(NBaseCommon.Common.LoginUser, target.MsUserID);
                    if (senins.Count > 0)
                    {
                        msSenin = senins[0];
                        textBox_氏名コード.Text = msSenin.ShimeiCode.Trim();
                    }
                }
            }
        }

        /// <summary>
        /// 「更新」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Btn_Click(object sender, EventArgs e)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                NBaseData.DAC.MsUser user;
                NBaseData.DAC.MsUserBumon UBumon;

                MasterItemsSet(out user, out UBumon);

                if (user.Sei.Length < 1)
                {
                    Message.Showエラー("氏名(姓)を入力してください。");
                    return;
                }
                if (user.Mei.Length < 1)
                {
                    Message.Showエラー("氏名(名)を入力してください。");
                    return;
                }
                if (user.LoginID.Length < 1)
                {
                    Message.Showエラー("ログインIDを入力してください。");
                    return;
                }
                if (user.Password.Length < 1)
                {
                    Message.Showエラー("パスワードを入力してください。");
                    return;
                }
                // 2012.03
                // 更新時にもログインIDの重複チェックをするように修正
                NBaseData.DAC.MsUser ExistUser = serviceClient.MsUser_GetRecordsByLoginID(NBaseCommon.Common.LoginUser, user.LoginID);
                if (ExistUser != null && ExistUser.MsUserID != user.MsUserID)
                {
                    Message.Showエラー("ログインＩＤはすでに登録されています");
                    return;
                }

                bool ret = serviceClient.BLC_ユーザ情報更新処理_更新(NBaseCommon.Common.LoginUser, user, UBumon, msSenin);
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
        }



        /// <summary>
        /// 削除チェック
        /// 引数：検索データ
        /// 返り値：削除可能→true
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckDeleteUsing(NBaseData.DAC.MsUser data)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                //MS_USER_BUMON-  /*GetRecordsByUserID*/
                //OD_MM-
                //OD_THI-

                //BG_YOSAN_HEAD-
                //BG_NRK_KANRYOU-
                //OD_GAISAN_KEIJO-

                //OD_GETSUJI_SHIME-
                //LOGIN_SESSION			/*意味不明につき保留。チェックするかどうかを確認するべし*/
                //MS_SHINSEI_TANTOU-		/*GetShinseiTantouってのを使うべし*/

                //////////

                //MS_SENIN-
                //SI_JUNBIKIN-
                //SI_SOUKIN-

                //SI_HAIJOU-
                //MS_USER_PASS_HIS		/*GetRecordsでマスタクラスごと渡す。*/
                //SI_GETSUJI_SHIME-
                //SI_NENJI_SHIME-


                //MS_USER_BUMON-  /*GetRecordsByUserID*/
                //OD_MM-
                //OD_THI-
                {
                    #region MsUserBumon
                    //List<MsUserBumon> bulist =
                    //    serviceClient.MsUserBumon_GetRecordsByUserIDList(NBaseCommon.Common.LoginUser, data.MsUserID);

                    ////一つ以上発見
                    //if (bulist.Count > 0)
                    //{
                    //    return false;
                    //}
                    #endregion

                    #region OdMm
                    List<OdMm> mmlist =
                        serviceClient.OdMm_GetRecordsByMsUserID(NBaseCommon.Common.LoginUser, data.MsUserID);

                    if (mmlist.Count > 0)
                    {
                        return false;
                    }

                    #endregion

                    #region OdThi
                    List<OdThi> thilist =
                        serviceClient.OdThi_GetRecordsByMsUserID(NBaseCommon.Common.LoginUser, data.MsUserID);

                    if (thilist.Count > 0)
                    {
                        return false;
                    }
                    #endregion
                }

                //BG_YOSAN_HEAD-
                //BG_NRK_KANRYOU-
                //OD_GAISAN_KEIJO-
                {
                    #region BgYosanHead
                    List<BgYosanHead> headlist =
                        serviceClient.BgYosanHead_GetRecordsByMsUserID(NBaseCommon.Common.LoginUser, data.MsUserID);

                    if (headlist.Count > 0)
                    {
                        return false;
                    }
                    #endregion

                    #region BgNrkKanryou
                    List<BgNrkKanryou> kanlist =
                        serviceClient.BgNrkKanryou_GetRecordsByMsUserID(NBaseCommon.Common.LoginUser, data.MsUserID);

                    if (kanlist.Count > 0)
                    {
                        return false;
                    }
                    #endregion

                    #region OdGaisanKeijo
                    List<OdGaisanKeijo> galist =
                        serviceClient.OdGaisanKeijo_GetRecordsByMsUserID(NBaseCommon.Common.LoginUser, data.MsUserID);

                    if (galist.Count > 0)
                    {
                        return false;
                    }

                    #endregion
                }


                //OD_GETSUJI_SHIME-
                //LOGIN_SESSION		        ?
                //MS_SHINSEI_TANTOU-
                {
                    #region OdGetsujiShime
                    List<OdGetsujiShime> odlist =
                        serviceClient.OdGetsujiShime_GetRecordsByMsUserID(NBaseCommon.Common.LoginUser, data.MsUserID);

                    if (odlist.Count > 0)
                    {
                        return false;
                    }
                    #endregion

                    #region MsShinseiTantou
                    NBaseData.DAC.MsUser userdata =
                        serviceClient.MsShinseiTantou_GetShinseiTantou(NBaseCommon.Common.LoginUser, data.MsUserID);

                    //発見
                    if (userdata != null)
                    {
                        return false;
                    }
                    #endregion
                }

                //MS_SENIN-
                //SI_JUNBIKIN-
                //SI_SOUKIN-
                {
                    #region MsSenin
                    List<MsSenin> selist =
                        serviceClient.MsSenin_GetRecordsByMsUserID(NBaseCommon.Common.LoginUser, data.MsUserID);

                    if (selist.Count > 0)
                    {
                        return false;
                    }
                    #endregion

                    #region SiJunbikin
                    List<SiJunbikin> julist =
                        serviceClient.SiJunbikin_GetRecordsByMsUserID(NBaseCommon.Common.LoginUser, data.MsUserID);

                    if (julist.Count > 0)
                    {
                        return false;
                    }
                    #endregion

                    #region SiSoukin
                    List<SiSoukin> solist =
                        serviceClient.SiSoukin_GetRecordsByMsUserID(NBaseCommon.Common.LoginUser, data.MsUserID);

                    if (solist.Count > 0)
                    {
                        return false;
                    }

                    #endregion
                }

                //SI_HAIJOU-
                //SI_GETSUJI_SHIME-
                //SI_NENJI_SHIME-
                {
                    #region SiHaijou
                    List<SiHaijou> hailist =
                        serviceClient.SiHaijou_GetRecordsByMsUserID(NBaseCommon.Common.LoginUser, data.MsUserID);

                    if (hailist.Count > 0)
                    {
                        return false;
                    }
                    #endregion


                    #region SiGetsujiShime
                    List<SiGetsujiShime> gslist =
                        serviceClient.SiGetsujiShime_GetRecordsByMsUserID(NBaseCommon.Common.LoginUser, data.MsUserID);

                    if (gslist.Count > 0)
                    {
                        return false;
                    }
                    #endregion

                    #region SiNenjiShime
                    List<SiNenjiShime> nslsit =
                        serviceClient.SiNenjiShime_GetRecordsByMsUserID(NBaseCommon.Common.LoginUser, data.MsUserID);

                    if (nslsit.Count > 0)
                    {
                        return false;
                    }
                    #endregion
                }
               //----------------------------------------------------------------------------------
            }

            return true;
        }


        /// <summary>
        /// 「削除」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Btn_Click(object sender, EventArgs e)
        {
            if (Message.Show問合せ("このユーザーを削除します。よろしいですか？") == false)
            {
                return;
            }

            //----------------------------------------------------------------------------------
            //削除チェックをする
            bool result = this.CheckDeleteUsing(this.msUser);
            if (result == false)
            {
                MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //----------------------------------------------------------------------------------

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                NBaseData.DAC.MsUser user;
                NBaseData.DAC.MsUserBumon UBumon;

                MasterItemsSet(out user, out UBumon);

                bool ret = serviceClient.BLC_ユーザ情報更新処理_削除(NBaseCommon.Common.LoginUser, user, UBumon, msSenin);
                if (ret == false)
                {
                    Message.Showエラー("削除に失敗しました。");
                    return;
                }
                else
                {
                    Message.Show確認("削除しました");
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
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


        private void MasterItemsSet(out NBaseData.DAC.MsUser user, out NBaseData.DAC.MsUserBumon UBumon)
        {
            user = msUser;
            user.Sei = Sei_textBox.Text;
            user.Mei = Mei_textBox.Text;
            user.SeiKana = Sei_Kn_textBox.Text;
            user.MeiKana = Mei_Kn_textBox.Text;
            user.Sex = (Sex_radioButton1.Checked ? 0 : 1);
            user.UserKbn = (UserKbn_radioButton1.Checked ? 0 : 1);
            user.LoginID = Login_textBox.Text;
            user.Password = PassWord_textBox.Text;
            user.MailAddress = MailAddress_textBox.Text;
            user.AdminFlag = (AdminFlag_radioButton1.Checked ? 0 : 1);
            user.RenewDate = DateTime.Now;
            user.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            user.DocFlag_CEO = (checkBox_経営責任者.Checked ? 1 : 0);
            user.DocFlag_Admin = (checkBox_管理責任者.Checked ? 1 : 0);

            user.DocFlag_MsiFerry = (checkBox_海務監督_旅客.Checked ? 1 : 0);
            user.DocFlag_CrewFerry = (checkBox_船員担当者_旅客.Checked ? 1 : 0);
            user.DocFlag_TsiFerry = (checkBox_工務監督_旅客.Checked ? 1 : 0);

            user.DocFlag_Officer = (checkBox_役員.Checked ? 1 : 0);
            user.DocFlag_GL = (checkBox_GL.Checked ? 1 : 0);
            user.DocFlag_TL = (checkBox_TL.Checked ? 1 : 0);


            user.MsUserID = msUser.MsUserID;

            UBumon = Ubumon;
            UBumon.MsUserID = msUser.MsUserID;
            UBumon.MsBumonID = ((ListItem)Bumon_DropDownList.SelectedItem).Value;
            UBumon.RenewDate = DateTime.Now;
            UBumon.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
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
                if (senin.MsUserID != null && senin.MsUserID.Length > 0 && senin.MsUserID != msUser.MsUserID)
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
