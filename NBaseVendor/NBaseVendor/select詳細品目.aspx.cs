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

public partial class select詳細品目 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int vesselNo = (int)Session[MakeSessionKey("VesselNo")];
        string str_category = Session[MakeSessionKey("Category")].ToString();
        int category = int.Parse(str_category);

        HiddenField_VesselNo.Value = vesselNo.ToString();
        HiddenField_Category.Value = str_category;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int id = (int)Session[MakeSessionKey("ID")];
        id = id + 1;
        Session[MakeSessionKey("ID")] = id;

        RegisterStartupScript("Result", "<script language='JavaScript'>SetValue('123');closeWindow();</script>");
    }
    protected void Button_Search_Click(object sender, EventArgs e)
    {
        // 検索
        // 条件取得
        string tmpvid = HiddenField_VesselNo.Value;
        int vesselID = int.Parse(tmpvid);
        string tmpctg = HiddenField_Category.Value;
        int category = int.Parse(tmpctg);
        string vesselItemName = TextBox_ItemName.Text;

        System.Collections.Generic.List<NBaseData.DAC.MsVesselItemVessel> msvivs = null;
        using (NBaseService.ServiceClient serviceClient = new NBaseService.ServiceClient())
        {
            NBaseData.DAC.MsUser user = new NBaseData.DAC.MsUser();
            user.MsUserID = "1";
            msvivs
                = serviceClient.MsVesselItemVessel_GetRecordByMsVesselIDVesselItemName(user,
                    vesselID, category, vesselItemName);

        }

        // リストへ登録
        foreach(NBaseData.DAC.MsVesselItemVessel viv in msvivs)
        {
            ListItem li = new ListItem(viv.VesselItemName, viv.MsVesselItemID);

            ListBox_Items.Items.Add(li);
        }
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
