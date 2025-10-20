using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using System.ServiceModel;
using NBaseData.DS;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        SiCard SiCard_GetRecord(MsUser loginUser, string siCardId);

        [OperationContract]
        List<SiCard> SiCard_GetRecords(MsUser loginUser);


        [OperationContract]
        List<SiCard> SiCard_GetRecordsByFilter(MsUser loginUser, SiCardFilter filter);


        [OperationContract]
        List<SiCard> SiCard_Get_期間重複(MsUser loginUser, int msSeninId, string siCardId, DateTime start, DateTime end);


        [OperationContract]
        SiCard SiCard_Get_船長(MsUser loginUser, int msVesselId);


        [OperationContract]
        List<SiCard> SiCard_GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id);
    }

    public partial class Service
    {
        public SiCard SiCard_GetRecord(MsUser loginUser, string siCardId)
        {
            return SiCard.GetRecord(loginUser, siCardId);
        }

        public List<SiCard> SiCard_GetRecords(MsUser loginUser)
        {
            return SiCard.GetRecords(loginUser);
        }


        public List<SiCard> SiCard_GetRecordsByFilter(MsUser loginUser, SiCardFilter filter)
        {
            return SiCard.GetRecordsByFilter(loginUser, filter);
        }


        public List<SiCard> SiCard_Get_期間重複(MsUser loginUser, int msSeninId, string siCardId, DateTime start, DateTime end)
        {
            return SiCard.Get_期間重複(loginUser, msSeninId, siCardId, start, end);
        }


        public SiCard SiCard_Get_船長(MsUser loginUser, int msVesselId)
        {
            return SiCard.Get_船長(loginUser, msVesselId);
        }

        //船ID
        public List<SiCard> SiCard_GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id)
        {
            return SiCard.GetRecordsByMsVesselID(loginUser, ms_vessel_id);
        }
    }
}
