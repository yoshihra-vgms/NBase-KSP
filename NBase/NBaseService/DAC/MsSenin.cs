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
        List<MsSenin> MsSenin_GetRecords(MsUser loginUser);

        [OperationContract]
        List<MsSenin> MsSenin_GetRecordsByFilter(MsUser loginUser, MsSeninFilter filter);

        [OperationContract]
        MsSenin MsSenin_GetRecord(MsUser loginUser, int msSeninId);


        [OperationContract]
        bool MsSenin_InsertOrUpdate(MsUser loginUser, MsSenin s);


        [OperationContract]
        List<MsSenin> MsSenin_GetRecordsByMsUserID(MsUser loginUser, string ms_user_id);
    }

    public partial class Service
    {
        public List<MsSenin> MsSenin_GetRecords(MsUser loginUser)
        {
            return MsSenin.GetRecords(loginUser);
        }

        public List<MsSenin> MsSenin_GetRecordsByFilter(MsUser loginUser, MsSeninFilter filter)
        {
            return MsSenin.GetRecordsByFilter(loginUser, filter);
        }

        public MsSenin MsSenin_GetRecord(MsUser loginUser, int msSeninId)
        {
            return MsSenin.GetRecord(loginUser, msSeninId);
        }


        public bool MsSenin_InsertOrUpdate(MsUser loginUser, MsSenin s)
        {
            s.RenewUserID = loginUser.MsUserID;
            s.RenewDate = DateTime.Now;
            
            if (s.IsNew())
            {
                return s.InsertRecord(loginUser);
            }
            else
            {
                return s.UpdateRecord(loginUser);
            }
        }


        public List<MsSenin> MsSenin_GetRecordsByMsUserID(MsUser loginUser, string ms_user_id)
        {
            return MsSenin.GetRecordsByMsUserID(null, loginUser, ms_user_id);
        }
    }
}
