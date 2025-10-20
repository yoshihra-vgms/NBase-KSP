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
        List<NBaseData.DAC.OdMmShousaiItem> OdMmShousaiItem_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.OdMmShousaiItem> OdMmShousaiItem_GetRecordByOdMmID(NBaseData.DAC.MsUser loginUser, string odMmID);

        [OperationContract]
        List<NBaseData.DAC.OdMmShousaiItem> OdMmShousaiItem_GetRecordByOdMkID(NBaseData.DAC.MsUser loginUser, string odMkID);

        //[OperationContract]
        //bool OdMmShousaiItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMmShousaiItem info);

        //[OperationContract]
        //bool OdMmShousaiItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMmShousaiItem info);

        [OperationContract]
        List<OdMmShousaiItem> OdMmShousaiItem_GetRecordsByMsVesselItemID(NBaseData.DAC.MsUser loginUser, string ms_vesselitem_id);

        [OperationContract]
        List<OdMmShousaiItem> OdMmShousaiItem_GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.OdMmShousaiItem> OdMmShousaiItem_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.OdMmShousaiItem> ret;
            ret = NBaseData.DAC.OdMmShousaiItem.GetRecords(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.OdMmShousaiItem> OdMmShousaiItem_GetRecordByOdMmID(NBaseData.DAC.MsUser loginUser, string odMmID)
        {
            List<NBaseData.DAC.OdMmShousaiItem> ret;
            ret = NBaseData.DAC.OdMmShousaiItem.GetRecordsByOdMmID(loginUser, odMmID);
            return ret;
        }

        public List<NBaseData.DAC.OdMmShousaiItem> OdMmShousaiItem_GetRecordByOdMkID(NBaseData.DAC.MsUser loginUser, string odMkID)
        {
            List<NBaseData.DAC.OdMmShousaiItem> ret;
            ret = NBaseData.DAC.OdMmShousaiItem.GetRecordsByOdMkID(loginUser, odMkID);
            return ret;
        }

        //public bool OdMmShousaiItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMmShousaiItem info)
        //{
        //    return info.InsertRecord(loginUser);
        //}

        //public bool OdMmShousaiItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMmShousaiItem info)
        //{
        //    return info.UpdateRecord(loginUser);
        //}


        public List<OdMmShousaiItem> OdMmShousaiItem_GetRecordsByMsVesselItemID(NBaseData.DAC.MsUser loginUser, string ms_vesselitem_id)
        {
            return OdMmShousaiItem.GetRecordsByMsVesselItemID(loginUser, ms_vesselitem_id);
        }

        public List<OdMmShousaiItem> OdMmShousaiItem_GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id)
        {
            return OdMmShousaiItem.GetRecordsByMsLoID(loginUser, ms_lo_id);
        }
            
    }
}
