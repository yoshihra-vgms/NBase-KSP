using System;
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
        List<OdJryShousaiItem> OdJryShousaiItem_GetRecordsByMsVesselID_JryStatus_ShousaiID_Date
            (NBaseData.DAC.MsUser loginUser, string syubetsu, int status, int ms_vessel_id, DateTime startdate, DateTime enddate);
        
        [OperationContract]
        List<NBaseData.DAC.OdJryShousaiItem> OdJryShousaiItem_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.OdJryShousaiItem> OdJryShousaiItem_GetRecordByOdJryID(NBaseData.DAC.MsUser loginUser, string odJryID);

        [OperationContract]
        bool OdJryShousaiItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdJryShousaiItem info);

        [OperationContract]
        bool OdJryShousaiItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdJryShousaiItem info);



        [OperationContract]
        List<OdJryShousaiItem> OdJryShousaiItem_GetRecordsByMsVesselID_JryStatus_ShousaiID_Date_MsLoID
            (NBaseData.DAC.MsUser loginUser, string syubetsu, int status, int ms_vessel_id, DateTime startdate, DateTime enddate, string ms_lo_id);


        [OperationContract]
        List<OdJryShousaiItem> OdJryShousaiItem_GetRecordsByMsVesselID_JryStatus_ShousaiID_Date_MsVesselItemID
            (NBaseData.DAC.MsUser loginUser, string syubetsu, int status, int ms_vessel_id, DateTime startdate, DateTime enddate, string ms_ves_item_id);


        [OperationContract]
        List<OdJryShousaiItem> OdJryShousaiItem_GetRecordsByMsVesselItemID(NBaseData.DAC.MsUser loginUser, string ms_vesselitem_id);

        [OperationContract]
        List<OdJryShousaiItem> OdJryShousaiItem_GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id);
    }

    public partial class Service
    {
        public List<OdJryShousaiItem> OdJryShousaiItem_GetRecordsByMsVesselID_JryStatus_ShousaiID_Date
            (NBaseData.DAC.MsUser loginUser, string syubetsu, int status, int ms_vessel_id, DateTime startdate, DateTime enddate)
        {
            List<OdJryShousaiItem> ret = OdJryShousaiItem.GetRecordsByMsVesselID_JryStatus_ShousaiID_Date
                (loginUser, syubetsu, status, ms_vessel_id, startdate, enddate);

            return ret;
        }
        public List<NBaseData.DAC.OdJryShousaiItem> OdJryShousaiItem_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.OdJryShousaiItem> ret;
            ret = NBaseData.DAC.OdJryShousaiItem.GetRecords(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.OdJryShousaiItem> OdJryShousaiItem_GetRecordByOdJryID(NBaseData.DAC.MsUser loginUser, string odJryID)
        {
            List<NBaseData.DAC.OdJryShousaiItem> ret;
            ret = NBaseData.DAC.OdJryShousaiItem.GetRecordsByOdJryID(loginUser, odJryID);
            return ret;
        }

        public bool OdJryShousaiItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdJryShousaiItem info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool OdJryShousaiItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdJryShousaiItem info)
        {
            return info.UpdateRecord(loginUser);
        }



        public List<OdJryShousaiItem> OdJryShousaiItem_GetRecordsByMsVesselID_JryStatus_ShousaiID_Date_MsLoID
            (NBaseData.DAC.MsUser loginUser, string syubetsu, int status, int ms_vessel_id, DateTime startdate, DateTime enddate, string ms_lo_id)
        {
            return OdJryShousaiItem.GetRecordsByMsVesselID_JryStatus_ShousaiID_Date_MsLoID(loginUser, syubetsu, status, ms_vessel_id, startdate, enddate, ms_lo_id);
        }



        public List<OdJryShousaiItem> OdJryShousaiItem_GetRecordsByMsVesselID_JryStatus_ShousaiID_Date_MsVesselItemID
            (NBaseData.DAC.MsUser loginUser, string syubetsu, int status, int ms_vessel_id, DateTime startdate, DateTime enddate, string ms_ves_item_id)
        {
            return OdJryShousaiItem.GetRecordsByMsVesselID_JryStatus_ShousaiID_Date_MsVesselItemID(loginUser, syubetsu, status, ms_vessel_id, startdate, enddate, ms_ves_item_id);
        }

        public List<OdJryShousaiItem> OdJryShousaiItem_GetRecordsByMsVesselItemID(NBaseData.DAC.MsUser loginUser, string ms_vesselitem_id)
        {
            return OdJryShousaiItem.GetRecordsByMsVesselItemID(loginUser, ms_vesselitem_id);
        }


        public List<OdJryShousaiItem> OdJryShousaiItem_GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id)
        {
            return OdJryShousaiItem.GetRecordsByMsLoID(loginUser, ms_lo_id);
        }
    }
}
