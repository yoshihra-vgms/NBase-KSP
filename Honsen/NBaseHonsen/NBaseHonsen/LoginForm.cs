using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using SyncClient;
using System.Net.NetworkInformation;
using NBaseData.DS;

namespace NBaseHonsen
{
    public partial class LoginForm : Form, IDataSyncObserver
    {
        public LoginForm()
        {
            InitializeComponent();
            
            // 動作環境を確認する
            string 本番環境 = "server";
            string 動作環境 = "-";

            動作環境 = 同期Client.SERVER_STRING.Substring(8);

            if (動作環境 == 本番環境)
            {
                NBaseCommon.Common.Is本番環境 = true;
            }
            else
            {
                NBaseCommon.Common.Is本番環境 = false;
                if (System.Configuration.ConfigurationManager.AppSettings["ModuleDivision"] == "Develop")
                {
                    NBaseCommon.Common.Is開発中 = true;
                    //this.BackColor = Color.DodgerBlue;
                }
                else
                {
                    //this.BackColor = Color.Plum;
                }
            }
            labelVersion.Text = 同期Client.VERSION_STRING;
            labelServer.Text = 同期Client.SERVER_STRING;


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


            Text = NBaseCommon.Common.WindowTitle("ログイン");
            var a = NetworkInterface.GetIsNetworkAvailable();
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                labelOffline.Text = "Offline";
            }
            else
            {
                labelOffline.Text = "";
            }
       }

        private void loginButt_Click(object sender, EventArgs e)
        {
            if (comboBox船.SelectedItem is MsVessel)
            {

                NBaseData.DAC.MsUser loginUser = MsUser.GetRecordsByLoginIDPassword(textBoxユーザID.Text, textBoxパスワード.Text);

                if (loginUser != null)
                {
                    NBaseData.DAC.MsUserBumon bumon = NBaseData.DAC.MsUserBumon.GetRecordByUserID(loginUser, loginUser.MsUserID);
                    loginUser.BumonID = bumon.MsBumonID;
                    
                    MsVessel selectedVessel = comboBox船.SelectedItem as MsVessel;

                    if (Is_乗船中(loginUser, selectedVessel))
                    {
                        同期Client.LOGIN_USER = loginUser;
                        同期Client.LOGIN_VESSEL = selectedVessel;
                        NBaseCommon.Common.LoginUser = loginUser;


                        同期Client.LAST_LOGIN_VESSEL_ID = global::NBaseHonsen.Properties.Settings.Default.ログイン船;

                        global::NBaseHonsen.Properties.Settings.Default.ログイン船 = selectedVessel.MsVesselID;
                        global::NBaseHonsen.Properties.Settings.Default.Save();

                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("選択された船に乗船中の船員、\nまたは事務所ユーザのみがログインできます。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("ユーザIDまたはパスワードが違います。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxパスワード.Text = null;
                }
            }
            else
            {
                MessageBox.Show("船を選択してください。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private bool Is_乗船中(MsUser loginUser, MsVessel selectedVessel)
        {
            // 事務所ユーザならログイン可.
            if (loginUser.UserKbn == (int)MsUser.USER_KBN.事務所)
            {
                return true;
            }

            // 乗船中のユーザならログイン可.
            SiCardFilter filter = new SiCardFilter();
            filter.MsVesselIDs.Add(selectedVessel.MsVesselID);
            filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(loginUser, MsSiShubetsu.SiShubetsu.乗船));
            filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(loginUser, MsSiShubetsu.SiShubetsu.乗船休暇));
            filter.Start = DateTime.Now;
            filter.End = DateTime.Now;
            //　船員管理なしの場合の対応
            //filter.RetireFlag = 0;

            List<SiCard> cards = SiCard.GetRecordsByFilter(loginUser, filter);

            foreach (SiCard c in cards)
            {
                //　船員管理なしの場合の対応
                if (c.SeninRetireFlag == 1)
                    continue;

                if (c.MsUserID == loginUser.MsUserID)
                {
                    NBaseCommon.Common.siCard = c;
                    return true;
                }
            }

            return false;
        }


        private void cancelButt_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


        private void LoginForm_Load(object sender, EventArgs e)
        {
            InitializeComboBox船();
        }


        private void InitializeComboBox船()
        {
            List<MsVessel> vessels = MsVessel.GetRecordsByHonsenEnabled(同期Client.LOGIN_USER);
            comboBox船.Items.Clear();

            foreach (MsVessel v in vessels)
            {
                comboBox船.Items.Add(v);

                if (global::NBaseHonsen.Properties.Settings.Default.ログイン船 == v.MsVesselID)
                {
                    comboBox船.SelectedItem = v;
                }
            }
        }

        #region IDataSyncObserver メンバ

        public void SyncStart()
        {
        }

        public void SyncFinish()
        {
        }


        public void Online()
        {
            try
            {
                Invoke(new MethodInvoker(
                    delegate()
                    {
                        labelOffline.Text = "";
                    }
                ));
            }
            catch (InvalidOperationException e)
            {
            }
        }


        public void Offline()
        {
            try
            {
                Invoke(new MethodInvoker(
                    delegate()
                    {
                        labelOffline.Text = "Offline";
                    }
                ));
            }
            catch (InvalidOperationException e)
            {
            }
        }


        public void Message(string message)
        {
        }

        public void Message2(string message)
        {
        }

        public void Message3(string message)
        {
        }

        #endregion
    }
}
