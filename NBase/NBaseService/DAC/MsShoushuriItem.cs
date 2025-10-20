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
        List<NBaseData.DAC.MsShoushuriItem> MsShoushuriItem_GetRecordByMsVesselID(NBaseData.DAC.MsUser loginUser, int MsVesslID);

        [OperationContract]
        bool MsShoushuriItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsShoushuriItem info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.MsShoushuriItem> MsShoushuriItem_GetRecordByMsVesselID(NBaseData.DAC.MsUser loginUser, int MsVesslID)
        {
            List<NBaseData.DAC.MsShoushuriItem> ret;
            ret = NBaseData.DAC.MsShoushuriItem.GetRecordsByMsVesselID(loginUser, MsVesslID);
            return ret;
        }

        public bool MsShoushuriItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsShoushuriItem info)
        {
            return info.InsertRecord(loginUser);
        }
    }
}
