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
        BgVesselYosan BgVesselYosan_GetRecordByYearAndYosanHeadIdAndMsVesselId(MsUser loginUser, int year, int yosanHeadId, int msVesselId);

        [OperationContract]
        List<BgVesselYosan> BgVesselYosan_GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id);
    }

    public partial class Service
    {
        public BgVesselYosan BgVesselYosan_GetRecordByYearAndYosanHeadIdAndMsVesselId(MsUser loginUser, int year, int yosanHeadId, int msVesselId)
        {
            return BgVesselYosan.GetRecordByYearAndYosanHeadIdAndMsVesselId(loginUser, year, yosanHeadId, msVesselId);
        }


        public List<BgVesselYosan> BgVesselYosan_GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id)
        {
            return BgVesselYosan.GetRecordsByMsVesselID(loginUser, ms_vessel_id);
        }
    }
}
