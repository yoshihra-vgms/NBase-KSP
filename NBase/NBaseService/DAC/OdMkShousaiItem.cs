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
        List<NBaseData.DAC.OdMkShousaiItem> OdMkShousaiItem_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.OdMkShousaiItem> OdMkShousaiItem_GetRecordByOdMkID(NBaseData.DAC.MsUser loginUser, string odMkID);

        [OperationContract]
        NBaseData.DAC.OdMkShousaiItem OdMkShousaiItem_GetRecord(NBaseData.DAC.MsUser loginUser, string OdMkShousaiItemID);

        [OperationContract]
        bool OdMkShousaiItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMkShousaiItem info);

        [OperationContract]
        bool OdMkShousaiItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMkShousaiItem info);

        [OperationContract]
        bool OdMkShousaiItem_Delete(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMkShousaiItem info);


        [OperationContract]
        List<OdMkShousaiItem> OdMkShousaiItem_GetRecordsByMsVesselItemID(NBaseData.DAC.MsUser loginUser, string msvesselitem_id);

        [OperationContract]
        List<OdMkShousaiItem> OdMkShousaiItem_GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.OdMkShousaiItem> OdMkShousaiItem_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.OdMkShousaiItem> ret;
            ret = NBaseData.DAC.OdMkShousaiItem.GetRecords(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.OdMkShousaiItem> OdMkShousaiItem_GetRecordByOdMkID(NBaseData.DAC.MsUser loginUser, string odMkID)
        {
            List<NBaseData.DAC.OdMkShousaiItem> ret;
            ret = NBaseData.DAC.OdMkShousaiItem.GetRecordsByOdMkID(loginUser, odMkID);
            return ret;
        }

        public NBaseData.DAC.OdMkShousaiItem OdMkShousaiItem_GetRecord(NBaseData.DAC.MsUser loginUser, string OdMkShousaiItemID)
        {
            NBaseData.DAC.OdMkShousaiItem ret;
            ret = NBaseData.DAC.OdMkShousaiItem.GetRecord(loginUser, OdMkShousaiItemID);
            return ret;
        }

        public bool OdMkShousaiItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMkShousaiItem info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool OdMkShousaiItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMkShousaiItem info)
        {
            return info.UpdateRecord(loginUser);
        }

        public bool OdMkShousaiItem_Delete(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMkShousaiItem info)
        {
            //return info.DeleteRecord(loginUser);

            bool ret = false;
            NBaseData.DAC.OdMkShousaiItem item = NBaseData.DAC.OdMkShousaiItem.GetRecord(loginUser, info.OdMkShousaiItemID);
            if (item != null)
            {
                item.CancelFlag = 1;
                ret = item.UpdateRecord(loginUser);
            }

            return ret;
        }

        public List<OdMkShousaiItem> OdMkShousaiItem_GetRecordsByMsVesselItemID(NBaseData.DAC.MsUser loginUser, string msvesselitem_id)
        {
            return OdMkShousaiItem.GetRecordsByMsVesselItemID(loginUser, msvesselitem_id);
        }

        public List<OdMkShousaiItem> OdMkShousaiItem_GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id)
        {
            return OdMkShousaiItem.GetRecordsByMsLoID(loginUser, ms_lo_id);
        }
    }
}
