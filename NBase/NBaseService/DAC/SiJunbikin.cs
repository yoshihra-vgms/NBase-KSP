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
        List<SiJunbikin> SiJunbikin_GetRecords(MsUser loginUser);


        [OperationContract]
        SiJunbikin SiJunbikin_GetRecord(MsUser loginUser, string siJunbikinId);


        [OperationContract]
        bool SiJunbikin_InsertOrUpdate(MsUser loginUser, SiJunbikin s);


        [OperationContract]
        List<SiJunbikin> SiJunbikin_GetRecordsByDateAndMsVesselID(MsUser loginUser, DateTime date, int msVesselId);


        [OperationContract]
        decimal SiJunbikin_Get_先月末残高(MsUser loginUser, DateTime date, int msVesselId);


        [OperationContract]
        List<SiJunbikin> SiJunbikin_GetRecordsByMsSiHiyouKamokuID(MsUser loginUser, int sikamokuid);

        [OperationContract]
        List<SiJunbikin> SiJunbikin_GetRecordsByMsSiDaikoumokuID(MsUser loginUser, int sidaikoumoku_id);

        [OperationContract]
        List<SiJunbikin> SiJunbikin_GetRecordsByMsSIKamokuID(MsUser loginUser, int ms_si_kamoku_id);

        [OperationContract]
        List<SiJunbikin> SiJunbikin_GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id);

        [OperationContract]
        List<SiJunbikin> SiJunbikin_GetRecordsByMsUserID(MsUser loginUser, string ms_user_id);
    }

    public partial class Service
    {
        public List<SiJunbikin> SiJunbikin_GetRecords(MsUser loginUser)
        {
            return SiJunbikin.GetRecords(loginUser);
        }


        public SiJunbikin SiJunbikin_GetRecord(MsUser loginUser, string siJunbikinId)
        {
            return SiJunbikin.GetRecord(loginUser, siJunbikinId);
        }
        
        
        public bool SiJunbikin_InsertOrUpdate(MsUser loginUser, SiJunbikin s)
        {
            s.RenewUserID = loginUser.MsUserID;
            s.RenewDate = DateTime.Now;
            
            if (s.IsNew())
            {
                s.SiJunbikinID = System.Guid.NewGuid().ToString();
                return s.InsertRecord(loginUser);
            }
            else
            {
                return s.UpdateRecord(loginUser);
            }
        }


        public List<SiJunbikin> SiJunbikin_GetRecordsByDateAndMsVesselID(MsUser loginUser, DateTime date, int msVesselId)
        {
            return SiJunbikin.GetRecordsByDateAndMsVesselID(loginUser, date, msVesselId);
        }


        public decimal SiJunbikin_Get_先月末残高(MsUser loginUser, DateTime date, int msVesselId)
        {
            return SiJunbikin.Get_先月末残高(loginUser, date, msVesselId);
        }


        public List<SiJunbikin> SiJunbikin_GetRecordsByMsSiHiyouKamokuID(MsUser loginUser, int sikamokuid)
        {
            return SiJunbikin.GetRecordsByMsSiHiyouKamokuID(loginUser, sikamokuid);
        }


        public List<SiJunbikin> SiJunbikin_GetRecordsByMsSiDaikoumokuID(MsUser loginUser, int sidaikoumoku_id)
        {
            return SiJunbikin.GetRecordsByMsSiDaikoumokuID(loginUser, sidaikoumoku_id);
        }

        public List<SiJunbikin> SiJunbikin_GetRecordsByMsSIKamokuID(MsUser loginUser, int ms_si_kamoku_id)
        {
            return SiJunbikin.GetRecordsByMsSIKamokuID(loginUser, ms_si_kamoku_id);
        }


        public List<SiJunbikin> SiJunbikin_GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id)
        {
            return SiJunbikin.GetRecordsByMsVesselID(loginUser, ms_vessel_id);
        }

        public List<SiJunbikin> SiJunbikin_GetRecordsByMsUserID(MsUser loginUser, string ms_user_id)
        {
            return SiJunbikin.GetRecordsByMsUserID(loginUser, ms_user_id);
        }
    }
}
