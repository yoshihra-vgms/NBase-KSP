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
        List<NBaseData.DAC.OdThiShousaiItem> OdThiShousaiItem_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.OdThiShousaiItem> OdThiShousaiItem_GetRecordByOdThiID(NBaseData.DAC.MsUser loginUser, string odThiID);

        [OperationContract]
        bool OdThiShousaiItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdThiShousaiItem info);

        [OperationContract]
        bool OdThiShousaiItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdThiShousaiItem info);

        [OperationContract]
        List<OdThiShousaiItem> OdThiShousaiItem_GetRecordsByMsVesselItemID(NBaseData.DAC.MsUser loginUser, string ms_vesselitem_id);

        [OperationContract]
        List<OdThiShousaiItem> OdThiShousaiItem_GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id);

        [OperationContract]
        List<NBaseData.DAC.OdThiShousaiItem> OdThiShousaiItem_GetRecordByThiIraiSbtID(NBaseData.DAC.MsUser loginUser, string thiIraiSbtId, int msVesselId);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.OdThiShousaiItem> OdThiShousaiItem_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.OdThiShousaiItem> ret;
            ret = NBaseData.DAC.OdThiShousaiItem.GetRecords(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.OdThiShousaiItem> OdThiShousaiItem_GetRecordByOdThiID(NBaseData.DAC.MsUser loginUser, string odThiID)
        {
            List<NBaseData.DAC.OdThiShousaiItem> ret;
            ret = NBaseData.DAC.OdThiShousaiItem.GetRecordsByOdThiID(loginUser, odThiID);
            return ret;
        }

        public bool OdThiShousaiItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdThiShousaiItem info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool OdThiShousaiItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdThiShousaiItem info)
        {
            return info.UpdateRecord(loginUser);
        }
        
        public List<OdThiShousaiItem> OdThiShousaiItem_GetRecordsByMsVesselItemID(NBaseData.DAC.MsUser loginUser, string ms_vesselitem_id)
        {
            return OdThiShousaiItem.GetRecordsByMsVesselItemID(loginUser, ms_vesselitem_id);
        }

        public List<OdThiShousaiItem> OdThiShousaiItem_GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id)
        {
            return OdThiShousaiItem.GetRecordsByMsLoID(loginUser, ms_lo_id);
        }

        public List<NBaseData.DAC.OdThiShousaiItem> OdThiShousaiItem_GetRecordByThiIraiSbtID(NBaseData.DAC.MsUser loginUser, string thiIraiSbtId, int msVesselId)
        {
            List<NBaseData.DAC.OdThiShousaiItem> ret;
            ret = NBaseData.DAC.OdThiShousaiItem.GetRecordByThiIraiSbtID(loginUser, thiIraiSbtId, msVesselId);
            return ret;
        }
    }
}
