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
using ORMapping;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<PtJimushokoushinInfo> PtJimushokoushinInfo_GetRecordsByCondition(MsUser loginUser, PtJimushokoushinInfoCondition condition);
    }

    public partial class Service
    {
        public List<PtJimushokoushinInfo> PtJimushokoushinInfo_GetRecordsByCondition(MsUser loginUser, PtJimushokoushinInfoCondition condition)
        {
            return PtJimushokoushinInfo.GetRecordsByCondition(loginUser, condition);
        }

    }
}
