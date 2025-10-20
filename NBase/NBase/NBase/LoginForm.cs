using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Deployment.Application;
//using NBaseData.BLC.動静帳票;
//using NBaseData.BLC.アカサカ;
using NBaseData.DS;
using System.IO;

namespace NBase
{
    public partial class LoginForm : Form
    {
        public bool IsMaintenance = false;

        public LoginForm()
        {
            InitializeComponent();

            // 動作環境を確認する
            string 本番環境 = "server";
            string 動作環境 = "-";

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                動作環境 = serviceClient.Endpoint.Address.Uri.Host;
            }

            if (動作環境 == 本番環境)
            {
                NBaseCommon.Common.Is本番環境 = true;
            }
            else
            {
                NBaseCommon.Common.Is本番環境 = false;
                if (System.Configuration.ConfigurationManager.AppSettings["IsDevelop"] == "True")
                {
                    NBaseCommon.Common.Is開発中 = true;
                    //this.BackColor = Color.DodgerBlue;
                }
                else
                {
                    //this.BackColor = Color.Plum;
                }
            }

            if (NBaseUtil.Common.Gray())
            {
                this.BackColor = Color.DarkGray;
            }


            // Version
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                Version version = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                labelVersion.Text = "Version: " + Application.ProductVersion + "\n" +
                                 "ClickOnce Version: " + version.ToString() + "\n";
            }
            else
            {
                labelVersion.Text = "Version: " + Application.ProductVersion + "\n" +
                                 "ClickOnce Version: ----" + "\n";
            }
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                labelServer.Text = "Server: " + serviceClient.Endpoint.Address.Uri.Host;
            }

            labelCustomer.Text = NBaseCommon.Common.顧客名;
            if (NBaseCommon.Common.顧客名.IndexOf("Ver") > 0)
            {
                var name = NBaseCommon.Common.顧客名.Replace("Ver.", "").TrimEnd();
                int ilenb = System.Text.Encoding.GetEncoding(932).GetByteCount(name);
                if (ilenb > 8)
                {
                    Point p = labelCustomer.Location;
                    labelCustomer.Location = new Point(p.X - (8 * (ilenb - 8)), p.Y);
                }
            }


            this.Text = NBaseCommon.Common.WindowTitle("ログイン");
            UserID_textBox.Focus();

        }

        private void Login_button_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {

                string userId = UserID_textBox.Text;
                if (userId.IndexOf("_Maint") > 0)
                {
                    IsMaintenance = true;

                    userId = userId.Replace("_Maint", "");
                }



                // メンテナンスログインでない場合、フラグを確認
                if (IsMaintenance == false)
                {
                    bool inProgress = false;
                    string caption = null;
                    string message = null;

                    NBaseData.DAC.SnParameter sp = serviceClient.SnParameter_GetRecord(new NBaseData.DAC.MsUser());
                    if (sp.MaintenanceFlag == 1)
                    {
                        inProgress = true;
                        caption = "メンテナンス中";
                        message = sp.MaintenanceMessage;
                    }
                    else 
                    {
                        //if (ApplicationDeployment.IsNetworkDeployed)
                        //{
                        //    if (sp.ReleaseVersion != ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString())
                        //    {
                        //        inProgress = true;
                        //        caption = "メンテナンス完了";
                        //        message = "最新Versionがリリースされています。再起動してください。";
                        //    }
                        //}
                        //else　// この下のコードは、運用中には関係しないコードです
                        //{
                        //    if (sp.ReleaseVersion != Application.ProductVersion)
                        //    {
                        //        inProgress = true;
                        //        caption = "メンテナンス完了";
                        //        message = "最新Versionがリリースされています。再起動してください。";
                        //    }
                        //}
                    }
                   
                    if (inProgress)
                    {
                        MessageBox.Show(message, caption);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }



                NBaseData.DAC.MsUser loginUser = serviceClient.BLC_ログイン処理_ログインチェック(userId, Password_textBox.Text);

                if (loginUser != null)
                {
                    NBaseCommon.Common.LoginUser = loginUser;

                    #region パスワード有効期限のチェック
                    if (loginUser.LoginStatus == NBaseData.DAC.MsUser.LOGIN_STATUS.パスワード有効期限切れ間近)
                    {
                        //パスワード変更を促す
                        if (MessageBox.Show("パスワードを変更時から規定日数に近づいています\n速やかにパスワードを変更して下さい\nパスワードを変更しますか？",
                            "パスワード", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            Password form = new Password();
                            form.CloseButton.Enabled = true;
                            form.ShowDialog();
                        }
                    }
                    else if (loginUser.LoginStatus == NBaseData.DAC.MsUser.LOGIN_STATUS.パスワード有効期限切れ)
                    {
                        //パスワード変更を強制する
                        MessageBox.Show("パスワードを変更時から規定日数を経過しています\n速やかにパスワードを変更して下さい",
                            "パスワード", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Password form = new Password();
                        form.CloseButton.Enabled = false;
                        if (form.ShowDialog() == DialogResult.Cancel)
                        {
                            DialogResult = DialogResult.Cancel;
                            Close();
                            return;
                        }
                    }
                    #endregion

                    if (loginUser.UserKbn == (int)NBaseData.DAC.MsUser.USER_KBN.事務所)
                    {
                        if (MsRoleTableCache.instance().Enabled(loginUser, "ポータル", "ポータル", null))
                        {
                            // 2019.12
                            serviceClient.TraceLogging(loginUser, loginUser.MsUserID, loginUser.BumonID, "ログイン", NBaseCommon.Common.HostName);


                            // 指摘事項管理用
                            NBaseCommon.LoginFile.Write(loginUser.MsUserID, loginUser.Password);




                            NBaseCommon.Common.UserSettingsList = serviceClient.MsUserSettings_GetRecords(loginUser);



                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("ポータルを表示する権限がありません。", "確認");
                        }
                    }
                    else
                    {
                        // 船員はログイン不可
                        MessageBox.Show("ログインに失敗しました。", "確認");
                    }
                }
                else
                {
                    MessageBox.Show("ログインに失敗しました。", "確認");
                }

                this.Cursor = Cursors.Default;
            }
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
