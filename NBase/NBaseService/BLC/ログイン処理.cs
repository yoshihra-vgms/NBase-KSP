using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;
using NBaseData.DAC;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        NBaseData.DAC.MsUser BLC_ログイン処理_ログインチェック(string UserID, string Password);
    }

    public partial class Service
    {
        public NBaseData.DAC.MsUser BLC_ログイン処理_ログインチェック(string LoginID, string Password)
        {
            string[] パスワード変更日数 = System.Configuration.ConfigurationManager.AppSettings["パスワード変更日数"].Split(',');
            int PasswordChangeStart = Convert.ToInt32(パスワード変更日数[0]);
            int PasswordChangeEnd = Convert.ToInt32(パスワード変更日数[1]);

            NBaseData.DAC.MsUser user = NBaseData.DAC.MsUser.GetRecordsByLoginIDPassword(LoginID, Password);
            if (user != null)
            {
                #region 前回のパスワード更新を調べる
                MsUserPassHis his = MsUserPassHis.GetRecordByMaxDate(user.MsUserID, user.MsUserID);
                TimeSpan timeSpan = DateTime.Today - his.RenewDate;
                if (timeSpan.TotalDays >= PasswordChangeStart && timeSpan.TotalDays <= PasswordChangeEnd)
                {
                    user.LoginStatus = MsUser.LOGIN_STATUS.パスワード有効期限切れ間近;
                }
                else if (timeSpan.TotalDays > PasswordChangeEnd)
                {
                    user.LoginStatus = MsUser.LOGIN_STATUS.パスワード有効期限切れ;
                }

                #endregion

                NBaseData.DAC.MsUserBumon bumon = NBaseData.DAC.MsUserBumon.GetRecordByUserID(user, user.MsUserID);
                user.BumonID = bumon.MsBumonID;

                //認証成功
                user.SessionKey = セッションキー発行();
                NBaseData.BLC.LoginSession.新規作成(user);
                return user;
            }
            else
            {
                //認証失敗
                return null;
            }
        }

        private string セッションキー発行()
        {
            return Guid.NewGuid().ToString();
        }
    }
}