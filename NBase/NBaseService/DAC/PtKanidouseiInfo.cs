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
using System.ServiceModel;
using System.Collections.Generic;
using NBaseData.DAC;
using ORMapping;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        PtKanidouseiInfo PtKanidouseiInfo_GetRecordByPtKanidouseiInfoId(MsUser loginUser, string PtKanidouseiInfoId);

        [OperationContract]
        List<PtKanidouseiInfo> PtKanidouseiInfo_GetRecordByEventDate(NBaseData.DAC.MsUser loginUser, DateTime fromDatetime, DateTime toDatetime, int vesselId);

        [OperationContract]
        bool PtKanidouseiInfo_InsertRecord(NBaseData.DAC.MsUser loginUser, PtKanidouseiInfo info);

        [OperationContract]
        bool PtKanidouseiInfo_UpdateRecord(NBaseData.DAC.MsUser loginUser, PtKanidouseiInfo info);

        [OperationContract]
        bool PtKanidouseiInfo_DeleteRecord(NBaseData.DAC.MsUser loginUser, PtKanidouseiInfo info);

        [OperationContract]
        bool PtKanidouseiInfo_重複チェック(NBaseData.DAC.MsUser loginUser,string PtKanidouseiInfoId, int msVesselId, DateTime eventDate,int Koma);

        [OperationContract]
        List<PtKanidouseiInfo> PtKanidouseiInfo_GetRecordsByMsKichiID(NBaseData.DAC.MsUser loginUser, string ms_kichi_id);

        [OperationContract]
        List<PtKanidouseiInfo> PtKanidouseiInfo_GetRecordsByMsBashoID(NBaseData.DAC.MsUser loginUser, string ms_basho_id);

        [OperationContract]
        List<PtKanidouseiInfo> PtKanidouseiInfo_GetRecordsByMsVesselID(NBaseData.DAC.MsUser loginUser, int ms_vessel_id);
    }

    public partial class Service
    {
        public PtKanidouseiInfo PtKanidouseiInfo_GetRecordByPtKanidouseiInfoId(MsUser loginUser, string PtKanidouseiInfoId)
        {
            return PtKanidouseiInfo.GetRecordByPtKanidouseiInfoId(loginUser, PtKanidouseiInfoId);
        }

        public List<PtKanidouseiInfo> PtKanidouseiInfo_GetRecordByEventDate(NBaseData.DAC.MsUser loginUser, DateTime fromDatetime, DateTime toDatetime, int vesselId)
        {
            return PtKanidouseiInfo.GetRecordByEventDate(loginUser, fromDatetime, toDatetime, vesselId);
        }

        public bool PtKanidouseiInfo_InsertRecord(NBaseData.DAC.MsUser loginUser, PtKanidouseiInfo info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool PtKanidouseiInfo_UpdateRecord(NBaseData.DAC.MsUser loginUser, PtKanidouseiInfo info)
        {
            return info.UpdateRecord(loginUser);
        }

        public bool PtKanidouseiInfo_DeleteRecord(NBaseData.DAC.MsUser loginUser, PtKanidouseiInfo info)
        {
            return info.DeleteRecord(loginUser);
        }

        public bool PtKanidouseiInfo_重複チェック(NBaseData.DAC.MsUser loginUser, string PtKanidouseiInfoId, int msVesselId, DateTime eventDate, int Koma)
        {
            return PtKanidouseiInfo.重複チェック(loginUser, PtKanidouseiInfoId, msVesselId, eventDate, Koma);
        }

        public List<PtKanidouseiInfo> PtKanidouseiInfo_GetRecordsByMsKichiID(NBaseData.DAC.MsUser loginUser, string ms_kichi_id)
        {
            return PtKanidouseiInfo.GetRecordsByMsKichiID(loginUser, ms_kichi_id);
        }

        public List<PtKanidouseiInfo> PtKanidouseiInfo_GetRecordsByMsBashoID(NBaseData.DAC.MsUser loginUser, string ms_basho_id)
        {
            return PtKanidouseiInfo.GetRecordsByMsBashoID(loginUser, ms_basho_id);                
        }


        public List<PtKanidouseiInfo> PtKanidouseiInfo_GetRecordsByMsVesselID(NBaseData.DAC.MsUser loginUser, int ms_vessel_id)
        {
            return PtKanidouseiInfo.GetRecordsByMsVesselID(loginUser, ms_vessel_id);
        }
    }
}
