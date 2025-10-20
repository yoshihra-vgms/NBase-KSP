using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<SiSoukin> SiSoukin_GetRecords(MsUser loginUser);


        [OperationContract]
        bool SiSoukin_InsertOrUpdate(MsUser loginUser, SiSoukin s);


        [OperationContract]
        List<SiSoukin> SiSoukin_GetRecordsByDateAndMsVesselID(MsUser loginUser, DateTime start, DateTime end, int msVesselId);


        [OperationContract]
        SiSoukin SiSoukin_GetRecordByJunbikinId(MsUser loginUser, string siJunbikinId);

        [OperationContract]
        List<SiSoukin> SiSoukin_GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id);


        [OperationContract]
        List<SiSoukin> SiSoukin_GetRecordsByMsUserID(MsUser loginUser, string ms_user_id);
    }

    public partial class Service
    {
        public List<SiSoukin> SiSoukin_GetRecords(MsUser loginUser)
        {
            return SiSoukin.GetRecords(loginUser);
        }


        public bool SiSoukin_InsertOrUpdate(MsUser loginUser, SiSoukin s)
        {
            s.RenewUserID = loginUser.MsUserID;
            s.RenewDate = DateTime.Now;
            
            if (s.IsNew())
            {
                s.SiSoukinID = System.Guid.NewGuid().ToString();
                return s.InsertRecord(loginUser);
            }
            else
            {
                return s.UpdateRecord(loginUser);
            }
        }


        public List<SiSoukin> SiSoukin_GetRecordsByDateAndMsVesselID(MsUser loginUser, DateTime start, DateTime end, int msVesselId)
        {
            return SiSoukin.GetRecordsByDateAndMsVesselID(loginUser, start, end, msVesselId);
        }


        public SiSoukin SiSoukin_GetRecordByJunbikinId(MsUser loginUser, string siJunbikinId)
        {
            return SiSoukin.GetRecordByJunbikinId(loginUser, siJunbikinId);
        }


        public List<SiSoukin> SiSoukin_GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id)
        {
            return SiSoukin.GetRecordsByMsVesselID(loginUser, ms_vessel_id);
        }


        public List<SiSoukin> SiSoukin_GetRecordsByMsUserID(MsUser loginUser, string ms_user_id)
        {
            return SiSoukin.GetRecordsByMsUserID(loginUser, ms_user_id);
        }
    }
}
