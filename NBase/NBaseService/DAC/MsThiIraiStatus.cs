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
        List<NBaseData.DAC.MsThiIraiStatus> MsThiIraiStatus_GetRecords(NBaseData.DAC.MsUser loginUser);
        
        [OperationContract]
        NBaseData.DAC.MsThiIraiStatus MsThiIraiStatus_GetRecord(NBaseData.DAC.MsUser loginUser, string MsThiIraiStatusID);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.MsThiIraiStatus> MsThiIraiStatus_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.MsThiIraiStatus> ret;
            ret = NBaseData.DAC.MsThiIraiStatus.GetRecords(loginUser);
            return ret;
        }

        public NBaseData.DAC.MsThiIraiStatus MsThiIraiStatus_GetRecord(NBaseData.DAC.MsUser loginUser, string MsThiIraiStatusID)
        {
            NBaseData.DAC.MsThiIraiStatus ret;
            ret = NBaseData.DAC.MsThiIraiStatus.GetRecord(loginUser, MsThiIraiStatusID);
            return ret;
        }
    }
}
