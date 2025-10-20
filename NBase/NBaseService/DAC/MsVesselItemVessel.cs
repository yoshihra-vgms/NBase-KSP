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
        List<NBaseData.DAC.MsVesselItemVessel> MsVesselItemVessel_GetRecordByMsVesselID(NBaseData.DAC.MsUser loginUser, int MsVesslID);
        
        [OperationContract]
        List<NBaseData.DAC.MsVesselItemVessel> MsVesselItemVessel_GetRecordByMsVesselIDVesselItemName(NBaseData.DAC.MsUser loginUser, int MsVesslID, int CategoryNumber, string VesselItemName);

        [OperationContract]
        List<NBaseData.DAC.MsVesselItemVessel> MsVesselItemVessel_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.MsVesselItemVessel> MsVesselItemVessel_GetRecordByMsVesselIDVesselItemName2(NBaseData.DAC.MsUser loginUser, int MsVesslID, int CategoryNumber, string VesselItemName);

        [OperationContract]
        bool MsVesselItemVessel_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVesselItemVessel msVesselItemVessel);
        
        [OperationContract]
        bool MsVesselItemVessel_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVesselItemVessel msVesselItemVessel);

        [OperationContract]
        bool MsVesselItemVessel_DeleteRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVesselItemVessel msVesselItemVessel);

        [OperationContract]
        List<MsVesselItemVessel> MsVesselItemVessel_GetRecordsByMsVesselItemID(MsUser loginUser, string msvesselitem_id);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.MsVesselItemVessel> MsVesselItemVessel_GetRecordByMsVesselID(NBaseData.DAC.MsUser loginUser, int MsVesslID)
        {
            List<NBaseData.DAC.MsVesselItemVessel> ret;
            ret = NBaseData.DAC.MsVesselItemVessel.GetRecordsByMsVesselID(loginUser, MsVesslID);
            return ret;
        }
        public List<NBaseData.DAC.MsVesselItemVessel> MsVesselItemVessel_GetRecordByMsVesselIDVesselItemName(NBaseData.DAC.MsUser loginUser, int MsVesslID, int CategoryNumber, string VesselItemName)
        {
            List<NBaseData.DAC.MsVesselItemVessel> ret;
            ret = NBaseData.DAC.MsVesselItemVessel.GetRecordsByMsVesselIDVesselItemName(loginUser, MsVesslID, CategoryNumber, VesselItemName);
            return ret;
        }

        public List<NBaseData.DAC.MsVesselItemVessel> MsVesselItemVessel_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.MsVesselItemVessel> ret;
            ret = NBaseData.DAC.MsVesselItemVessel.GetRecords(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.MsVesselItemVessel> MsVesselItemVessel_GetRecordByMsVesselIDVesselItemName2(NBaseData.DAC.MsUser loginUser, int MsVesslID, int CategoryNumber, string VesselItemName)
        {
            List<NBaseData.DAC.MsVesselItemVessel> ret;
            ret = NBaseData.DAC.MsVesselItemVessel.GetRecordsByMsVesselIDVesselItemName2(loginUser, MsVesslID, CategoryNumber, VesselItemName);
            return ret;
        }

        public bool MsVesselItemVessel_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVesselItemVessel msVesselItemVessel)
        {
            msVesselItemVessel.InsertRecord(loginUser);
            return true;
        }

        public bool MsVesselItemVessel_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVesselItemVessel msVesselItemVessel)
        {
            msVesselItemVessel.UpdateRecord(loginUser);
            return true;
        }

        public bool MsVesselItemVessel_DeleteRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVesselItemVessel msVesselItemVessel)
        {
            msVesselItemVessel.DeleteRecord(loginUser);
            return true;
        }


        public List<MsVesselItemVessel> MsVesselItemVessel_GetRecordsByMsVesselItemID(MsUser loginUser, string msvesselitem_id)
        {
            return MsVesselItemVessel.GetRecordsByMsVesselItemID(loginUser, msvesselitem_id);
        }
    }
}
