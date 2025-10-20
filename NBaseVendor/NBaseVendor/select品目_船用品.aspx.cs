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

public partial class select品目_船用品 : System.Web.UI.Page
{
    private string msUserID_Customer = "1";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showCategoryList(string.Empty);
        }
    }
    protected void Button_Search_Click(object sender, EventArgs e)
    {
        showCategoryList(string.Empty);
    }

    /// <summary>
    /// リストに一覧を表示する
    /// </summary>
    /// <param name="part">名称の一部(含むものをリストに載せる。空のときは全表示)</param>
    private void showCategoryList(string part)
    {
        #region 一覧の表示
        System.Collections.Generic.List<NBaseData.DAC.MsVesselItemCategory> categorys = null;
        using (NBaseService.ServiceClient serviceClient = new NBaseService.ServiceClient())
        {
            NBaseData.DAC.MsUser msUser = new NBaseData.DAC.MsUser();
            msUser.MsUserID = msUserID_Customer;

            categorys = serviceClient.MsVesselItemCategory_GetRecords(msUser);
        }

        ListBox_Items.Items.Clear();
        foreach (NBaseData.DAC.MsVesselItemCategory catg in categorys)
        {
            if (part == string.Empty || catg.CategoryName.Contains(part))
            {
                ListItem li = new ListItem(catg.CategoryName, catg.CategoryNumber.ToString());

                ListBox_Items.Items.Add(li);
            }
        }
        #endregion

        if (ListBox_Items.Items.Count > 0)
        {
            ListBox_Items.SelectedIndex = 0;
        }
    }
}
