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
        List<BgUchiwakeYosanItem> BgUchiwakeYosanItem_GetRecords_入渠(MsUser loginUser, int yosanHeadId, int msVesselId, string yearStart, string yearEnd);
    }

    public partial class Service
    {
        public List<BgUchiwakeYosanItem> BgUchiwakeYosanItem_GetRecords_入渠(MsUser loginUser, int yosanHeadId, int msVesselId, string yearStart, string yearEnd)
        {
            return BgUchiwakeYosanItem.GetRecords_入渠(loginUser, yosanHeadId, msVesselId, yearStart, yearEnd);
        }
    }
}
