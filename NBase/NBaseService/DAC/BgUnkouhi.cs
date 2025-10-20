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
        BgUnkouhi BgUnkouhi_GetRecord(MsUser loginUser, int yosanHeadId, int msVesselId, int year);

        [OperationContract]
        bool BgUnkouhi_UpdateRecord(MsUser loginUser, BgUnkouhi unkouhi);
    }

    public partial class Service
    {
        public BgUnkouhi BgUnkouhi_GetRecord(MsUser loginUser, int yosanHeadId, int msVesselId, int year)
        {
            return BgUnkouhi.GetRecordByYosanHeadIdAndMsVesselIdAndYear(loginUser, yosanHeadId, msVesselId, year);
        }


        public bool BgUnkouhi_UpdateRecord(MsUser loginUser, BgUnkouhi unkouhi)
        {
            return unkouhi.UpdateRecord(loginUser);
        }
    }
}
