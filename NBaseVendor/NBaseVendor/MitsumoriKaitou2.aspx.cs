using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class MitsumoriKaitou2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            string webKey = this.Request.QueryString["WebKey"];
            if (webKey != null && webKey != string.Empty)
            {
                #region データ取得と表示
                NBaseData.DAC.OdMk odMK = null;

                using (NBaseService.ServiceClient serviceClient = new NBaseService.ServiceClient())
                {
                    NBaseData.DAC.MsUser msUser = new NBaseData.DAC.MsUser();
                    msUser.LoginID = "1";
                    odMK = serviceClient.OdMk_GetRecordByWebKey(msUser, webKey);
                }

                if (odMK != null)
                {
                    Label_会社.Text = odMK.MsCustomerName;
                    Label_担当者.Text = odMK.Tantousha;
                }
                #endregion
            }
        }
    }

    // 2010.06.24 以下のコードは使用していないみたいなのでコメントアウト
    //protected void Button_OutputFile_Click(object sender, EventArgs e)
    //{
    //    Response.Clear();
    //    Response.AddHeader("Content-position", "attachment;filename=test.csv");
    //    Response.AddHeader("media-type", "application/octet-stream");
    //    System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift-JIS");
    //    Response.BinaryWrite(enc.GetBytes("A,B,C,D,E,F"));
    //    Response.End();
    //}
}
