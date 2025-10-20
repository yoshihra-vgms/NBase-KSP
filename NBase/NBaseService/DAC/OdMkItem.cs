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
        List<NBaseData.DAC.OdMkItem> OdMkItem_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.OdMkItem> OdMkItem_GetRecordsByOdMkID(NBaseData.DAC.MsUser loginUser, string odMkID);

        [OperationContract]
        NBaseData.DAC.OdMkItem OdMkItem_GetRecord(NBaseData.DAC.MsUser loginUser, string OdMkItemID);

        [OperationContract]
        bool OdMkItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMkItem info);

        [OperationContract]
        bool OdMkItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMkItem info);

        [OperationContract]
        bool OdMkItem_Delete(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMkItem info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.OdMkItem> OdMkItem_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.OdMkItem> ret;
            ret = NBaseData.DAC.OdMkItem.GetRecords(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.OdMkItem> OdMkItem_GetRecordsByOdMkID(NBaseData.DAC.MsUser loginUser, string odMkID)
        {
            List<NBaseData.DAC.OdMkItem> ret;
            ret = NBaseData.DAC.OdMkItem.GetRecordsByOdMkID(loginUser, odMkID);
            return ret;
        }

        public NBaseData.DAC.OdMkItem OdMkItem_GetRecord(NBaseData.DAC.MsUser loginUser, string OdMkItemID)
        {
            NBaseData.DAC.OdMkItem ret;
            ret = NBaseData.DAC.OdMkItem.GetRecord(loginUser, OdMkItemID);
            return ret;
        }

        public bool OdMkItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMkItem info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool OdMkItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMkItem info)
        {
            return info.UpdateRecord(loginUser);
        }

        public bool OdMkItem_Delete(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMkItem info)
        {
            //return info.DeleteRecord(loginUser);

            bool ret = false;
            NBaseData.DAC.OdMkItem item = NBaseData.DAC.OdMkItem.GetRecord(loginUser, info.OdMkItemID);
            if (item != null)
            {
                item.CancelFlag = 1;
                ret = item.UpdateRecord(loginUser);
            }

            return ret;
        }
    }
}
