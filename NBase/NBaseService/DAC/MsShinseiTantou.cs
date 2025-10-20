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
using System.Collections.Generic;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        NBaseData.DAC.MsUser MsShinseiTantou_GetShinseiTantou(NBaseData.DAC.MsUser loginUser, string msUserID);
    }

    public partial class Service
    {
        public NBaseData.DAC.MsUser MsShinseiTantou_GetShinseiTantou(NBaseData.DAC.MsUser loginUser, string msUserID)
        {
            NBaseData.DAC.MsUser ret;
            ret = NBaseData.DAC.MsShinseiTantou.GetShinseiTantou(loginUser, msUserID);
            return ret;
        }
    }
}
