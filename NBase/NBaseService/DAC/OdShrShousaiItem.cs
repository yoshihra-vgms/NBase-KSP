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
using NBaseData.DAC;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<NBaseData.DAC.OdShrShousaiItem> OdShrShousaiItem_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.OdShrShousaiItem> OdShrShousaiItem_GetRecordByOdShrID(NBaseData.DAC.MsUser loginUser, string odShrID);

        [OperationContract]
        bool OdShrShousaiItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdShrShousaiItem info);

        [OperationContract]
        bool OdShrShousaiItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdShrShousaiItem info);

        [OperationContract]
        List<OdShrShousaiItem> OdShrShousaiItem_GetRecordsByMsVesselItemID(NBaseData.DAC.MsUser loginUser, string ms_vesselitem_id);

        [OperationContract]
        List<OdShrShousaiItem> OdShrShousaiItem_GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.OdShrShousaiItem> OdShrShousaiItem_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.OdShrShousaiItem> ret;
            ret = NBaseData.DAC.OdShrShousaiItem.GetRecords(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.OdShrShousaiItem> OdShrShousaiItem_GetRecordByOdShrID(NBaseData.DAC.MsUser loginUser, string odShrID)
        {
            List<NBaseData.DAC.OdShrShousaiItem> ret;
            ret = NBaseData.DAC.OdShrShousaiItem.GetRecordsByOdShrID(loginUser, odShrID);
            return ret;
        }

        public bool OdShrShousaiItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdShrShousaiItem info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool OdShrShousaiItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdShrShousaiItem info)
        {
            return info.UpdateRecord(loginUser);
        }

        public List<OdShrShousaiItem> OdShrShousaiItem_GetRecordsByMsVesselItemID(NBaseData.DAC.MsUser loginUser, string ms_vesselitem_id)
        {
            return OdShrShousaiItem.GetRecordsByMsVesselItemID(loginUser, ms_vesselitem_id);
        }

        public List<OdShrShousaiItem> OdShrShousaiItem_GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id)
        {
            return OdShrShousaiItem.GetRecordsByMsLoID(loginUser, ms_lo_id);
        }
    }
}
