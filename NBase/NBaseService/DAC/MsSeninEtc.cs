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
        MsSeninEtc MsSeninEtc_GetRecordsByMsSeninID(MsUser loginUser, int msSeninId);

        [OperationContract]
        bool MsSeninEtc_InsertOrUpdate(MsUser loginUser, int msSeninId, MsSeninEtc seninEtc);
    }

    public partial class Service
    {
        public MsSeninEtc MsSeninEtc_GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            return MsSeninEtc.GetRecordsByMsSeninID(loginUser, msSeninId);
        }

        public bool MsSeninEtc_InsertOrUpdate(MsUser loginUser, int msSeninId, MsSeninEtc seninEtc)
        {
            seninEtc.RenewUserID = loginUser.MsUserID;
            seninEtc.RenewDate = DateTime.Now;

            if (seninEtc.IsNew())
            {
                seninEtc.MsSeninID = msSeninId;
                return seninEtc.InsertRecord(loginUser);
            }
            else
            {
                return seninEtc.UpdateRecord(loginUser);
            }
        }
    }
}
