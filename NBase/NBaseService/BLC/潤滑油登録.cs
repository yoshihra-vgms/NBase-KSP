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
using System.ServiceModel;
using NBaseData.DAC;
using NBaseData.BLC;
using System.Collections.Generic;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC_潤滑油登録(MsUser loginUser, MsLoVessel msLoVessel);
    }

    public partial class Service
    {
        public bool BLC_潤滑油登録(MsUser loginUser, MsLoVessel msLoVessel)
        {
            return 潤滑油.BLC_潤滑油登録(loginUser, msLoVessel);
        }
    }
}
