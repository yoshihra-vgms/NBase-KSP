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
        List<NBaseData.DAC.MsVesselSection> MsVesselSection_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        NBaseData.DAC.MsVesselSection MsVesselSection_GetRecord(NBaseData.DAC.MsUser loginUser, string id);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.MsVesselSection> MsVesselSection_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            return NBaseData.DAC.MsVesselSection.GetRecords(loginUser);
        }

        public NBaseData.DAC.MsVesselSection MsVesselSection_GetRecord(NBaseData.DAC.MsUser loginUser, string id)
        {
            return NBaseData.DAC.MsVesselSection.GetRecord(loginUser, id);
        }
    }
}
