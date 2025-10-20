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
using System.Collections.Generic;
using NBaseData.DAC;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<MsSiPresentationItem> MsSiPresentationItem_GetRecords(MsUser loginUser);
    }

    public partial class Service
    {
        public List<MsSiPresentationItem> MsSiPresentationItem_GetRecords(MsUser loginUser)
        {
            return MsSiPresentationItem.GetRecords(loginUser);
        }
    }
}
