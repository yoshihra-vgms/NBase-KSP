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
        List<NBaseData.DAC.OdShrGassanHead> OdShrGassanHead_GetRecords(NBaseData.DAC.MsUser loginUser, string msCustomerId, string msThiIraiSbtId, string msThiIraiShousaiId, int msVesselId, int status);
        
        [OperationContract]
        NBaseData.DAC.OdShrGassanHead OdShrGassanHead_GetRecord(NBaseData.DAC.MsUser loginUser, string odShrGassanHeadId);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.OdShrGassanHead> OdShrGassanHead_GetRecords(NBaseData.DAC.MsUser loginUser, string msCustomerId, string msThiIraiSbtId, string msThiIraiShousaiId, int msVesselId, int status)
        {
            return NBaseData.DAC.OdShrGassanHead.GetRecords(loginUser, msCustomerId, msThiIraiSbtId, msThiIraiShousaiId, msVesselId, status);
        }

        public NBaseData.DAC.OdShrGassanHead OdShrGassanHead_GetRecord(NBaseData.DAC.MsUser loginUser, string odShrGassanHeadId)
        {
            return NBaseData.DAC.OdShrGassanHead.GetRecord(loginUser, odShrGassanHeadId);
        }
    }
}
