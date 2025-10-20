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
        List<NBaseData.DAC.MsVesselItem> MsVesselItem_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        NBaseData.DAC.MsVesselItem MsVesselItem_GetRecordsByMsVesselItemID(NBaseData.DAC.MsUser loginUser, string MsVesselItemId);

        [OperationContract]
        bool MsVesselItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVesselItem MsVesselItem);

        [OperationContract]
        bool MsVesselItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVesselItem MsVesselItem);

        [OperationContract]
        bool MsVesselItem_DeleteRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVesselItem MsVesselItem);

        [OperationContract]
        List<NBaseData.DAC.MsVesselItem> MsVesselItem_SearchRecords(NBaseData.DAC.MsUser loginUser, string msVesselItemId, string vesselItem, int CategoryNumber);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.MsVesselItem> MsVesselItem_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.MsVesselItem> ret;
            ret = NBaseData.DAC.MsVesselItem.GetRecords(loginUser);
            return ret;
        }

        public NBaseData.DAC.MsVesselItem MsVesselItem_GetRecordsByMsVesselItemID(NBaseData.DAC.MsUser loginUser, string MsVesselItemId)
        {
            NBaseData.DAC.MsVesselItem ret;
            ret = NBaseData.DAC.MsVesselItem.GetRecord(loginUser, MsVesselItemId);
            return ret;
        }

        public bool MsVesselItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVesselItem MsVesselItem)
        {
            MsVesselItem.InsertRecord(loginUser);
            return true;
        }

        public bool MsVesselItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVesselItem MsVesselItem)
        {
            MsVesselItem.UpdateRecord(loginUser);
            return true;
        }

        public bool MsVesselItem_DeleteRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVesselItem MsVesselItem)
        {
            MsVesselItem.DeleteRecord(loginUser);
            return true;
        }

        public List<NBaseData.DAC.MsVesselItem> MsVesselItem_SearchRecords(NBaseData.DAC.MsUser loginUser, string msVesselItemId, string vesselItem, int CategoryNumber)
        {
            List<NBaseData.DAC.MsVesselItem> ret;
            ret = NBaseData.DAC.MsVesselItem.SearchRecords(loginUser, msVesselItemId, vesselItem, CategoryNumber);
            return ret;
        }
    }
}
