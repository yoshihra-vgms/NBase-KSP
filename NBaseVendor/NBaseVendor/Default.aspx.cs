using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Clear();
            LoginID_TextBox.Focus();
        }

        ServicePointManager.ServerCertificateValidationCallback =
          new RemoteCertificateValidationCallback(
            OnRemoteCertificateValidationCallback);
    }


    // 信頼できないSSL証明書を「問題なし」にするメソッド
    private bool OnRemoteCertificateValidationCallback(
      Object sender,
      X509Certificate certificate,
      X509Chain chain,
      SslPolicyErrors sslPolicyErrors)
    {
        return true;  // 「SSL証明書の使用は問題なし」と示す
    }

    protected void Login_Button_Click(object sender, EventArgs e)
    {
        // Common.Dataの初期化 20150917ADD
        ORMapping.Common.DBTYPE = ORMapping.Common.DB_TYPE.POSTGRESQL;

        //WebKey取得
        string webkey = Request.QueryString["WebKey"];

        if (webkey == null ||
            webkey == string.Empty)
        {
            // ALERT表示
            RegisterStartupScript("Result", "<script language='JavaScript'>alert('URLの指定が正しくありません');</script>");
            return;
        }

        #region データ取込(DB)
        // ODMK(担当者ID)
        NBaseData.DAC.OdMk odmk = null;

        // 担当者マスタ(ログインID/パスワード)
        NBaseData.DAC.MsCustomer msCustomer = null;

        using (NBaseService.ServiceClient serviceClient = new NBaseService.ServiceClient())
        {
            NBaseData.DAC.MsUser msUser = new NBaseData.DAC.MsUser();
            msUser.LoginID = "1";
            odmk = serviceClient.OdMk_GetRecordByWebKey(msUser, webkey);
            if (odmk != null)
            {
                msCustomer = serviceClient.MsCustomer_GetRecord(msUser, odmk.MsCustomerID);
            }
        }

        if (odmk == null ||
            msCustomer == null)
        {
            // 該当データなし
            // ALERT表示
            RegisterStartupScript("Result", "<script language='JavaScript'>alert('指定の見積が存在しません');</script>");

            return;
        }

        #endregion

        #region ユーザーID/パスワード判定

        string loginid = LoginID_TextBox.Text;
        string passwd = Password_TextBox.Text;

        if (loginid != msCustomer.LoginID ||
            passwd != msCustomer.Password)
        {
            // ID/Passwordが合っていない
            // ALERT表示
            RegisterStartupScript("Result", "<script language='JavaScript'>alert('ユーザIDまたはパスワードが違います');</script>");

            return;
        }

        // セッションに記録（CustomerID)
        Session[MakeSessionKey("CustomerID")] = msCustomer.MsCustomerID;

        #endregion

        // 2010.11.29 TEST用のコード
        //if (Session["test"] == null)
        //{
        //    Session["test"] = 0;
        //}

        // 画面移動
        // ページ移動
        string url = "../NBaseVendor/MitsumoriKaitou.aspx?WebKey=" + webkey;
        Response.Redirect(url);
    }

    private string MakeSessionKey(string key)
    {
        string retKey = key;
        string webkey = Request.QueryString["WebKey"];
        if (webkey == null ||
            webkey == string.Empty)
        {
            // ALERT表示
            RegisterStartupScript("Result", "<script language='JavaScript'>alert('URLの指定が正しくありません');</script>");
            return retKey;
        }
        retKey += "_" + webkey;
        return retKey;
    }
}
