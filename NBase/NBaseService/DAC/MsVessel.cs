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
        List<NBaseData.DAC.MsVessel> MsVessel_GetRecords(NBaseData.DAC.MsUser loginUser);
        
        [OperationContract]
        List<NBaseData.DAC.MsVessel> MsVessel_GetRecordsByHachuEnabled(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.MsVessel> MsVessel_GetRecordsByYojitsuEnabled(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        NBaseData.DAC.MsVessel MsVessel_GetRecordsByMsVesselID(NBaseData.DAC.MsUser loginUser, int msVesselID);

        [OperationContract]
        bool MsVessel_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVessel msVessel);

        [OperationContract]
        bool MsVessel_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVessel msVessel);

        [OperationContract]
        bool MsVessel_DeleteFlagRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVessel msVessel);

        [OperationContract]
        NBaseData.DAC.MsVessel MsVessel_GetRecordsByVesselNo(NBaseData.DAC.MsUser loginUser, string vesselNo);

        [OperationContract]
        List<NBaseData.DAC.MsVessel> MsVessel_SearchRecords(NBaseData.DAC.MsUser loginUser, string vesselNo, string vesselName);

        [OperationContract]
        List<NBaseData.DAC.MsVessel> MsVessel_GetRecordsByKanidouseiEnabled(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.MsVessel> MsVessel_GetRecordsByDocumentEnabled(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.MsVessel> MsVessel_GetRecordsBySeninEnabled(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.MsVessel> MsVessel_GetRecordsByKensaEnabled(NBaseData.DAC.MsUser loginUser);

    }

    public partial class Service
    {
        public List<NBaseData.DAC.MsVessel> MsVessel_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.MsVessel> ret;
            ret = NBaseData.DAC.MsVessel.GetRecords(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.MsVessel> MsVessel_GetRecordsByHachuEnabled(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.MsVessel> ret;
            ret = NBaseData.DAC.MsVessel.GetRecordsByHachuEnabled(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.MsVessel> MsVessel_GetRecordsByYojitsuEnabled(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.MsVessel> ret;
            ret = NBaseData.DAC.MsVessel.GetRecordsByYojitsuEnabled(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.MsVessel> MsVessel_SearchRecords(NBaseData.DAC.MsUser loginUser, string vesselNo, string vesselName)
        {
            List<NBaseData.DAC.MsVessel> ret;
            ret = NBaseData.DAC.MsVessel.SearchRecords(loginUser, vesselNo, vesselName);
            return ret;
        }

        public NBaseData.DAC.MsVessel MsVessel_GetRecordsByMsVesselID(NBaseData.DAC.MsUser loginUser, int msVesselID)
        {
            NBaseData.DAC.MsVessel ret;
            ret = NBaseData.DAC.MsVessel.GetRecordByMsVesselID(loginUser, msVesselID);
            return ret;
        }

        public NBaseData.DAC.MsVessel MsVessel_GetRecordsByVesselNo(NBaseData.DAC.MsUser loginUser, string vesselNo)
        {
            NBaseData.DAC.MsVessel ret;
            ret = NBaseData.DAC.MsVessel.GetRecordByVesselNo(loginUser, vesselNo);
            return ret;
        }

        public bool MsVessel_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVessel msVessel)
        {
            msVessel.InsertRecord(loginUser);
            return true;
        }

        public bool MsVessel_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVessel msVessel)
        {
            msVessel.UpdateRecord(loginUser);
            return true;
        }

        public bool MsVessel_DeleteFlagRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVessel msVessel)
        {
            msVessel.DeleteFlagRecord(loginUser);
            return true;
        }

        public List<NBaseData.DAC.MsVessel> MsVessel_GetRecordsByKanidouseiEnabled(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.MsVessel> ret;
            ret = NBaseData.DAC.MsVessel.GetRecordsByKanidouseiEnabled(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.MsVessel> MsVessel_GetRecordsByDocumentEnabled(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.MsVessel> ret;
            ret = NBaseData.DAC.MsVessel.GetRecordsByDocumentEnabled(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.MsVessel> MsVessel_GetRecordsBySeninEnabled(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.MsVessel> ret;
            ret = NBaseData.DAC.MsVessel.GetRecordsBySeninEnabled(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.MsVessel> MsVessel_GetRecordsByKensaEnabled(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.MsVessel> ret;
            ret = NBaseData.DAC.MsVessel.GetRecordsByKensaEnabled(loginUser);
            return ret;
        }
    }
}
