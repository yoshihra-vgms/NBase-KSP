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
        MsSeninCareer MsSeninCareer_GetRecordsByMsSeninID(MsUser loginUser, int msSeninId);
        [OperationContract]
        bool MsSeninCareer_InsertOrUpdate(MsUser loginUser, int msSeninId, MsSeninCareer seninCareer);
    }

    public partial class Service
    {
        public MsSeninCareer MsSeninCareer_GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            return MsSeninCareer.GetRecordsByMsSeninID(loginUser, msSeninId);
        }
        public bool MsSeninCareer_InsertOrUpdate(MsUser loginUser, int msSeninId, MsSeninCareer seninCareer)
        {
            seninCareer.RenewUserID = loginUser.MsUserID;
            seninCareer.RenewDate = DateTime.Now;

            if (seninCareer.IsNew())
            {
                seninCareer.MsSeninID = msSeninId;
                return seninCareer.InsertRecord(loginUser);
            }
            else
            {
                return seninCareer.UpdateRecord(loginUser);
            }
        }
    }
}
