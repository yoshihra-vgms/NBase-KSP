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
        List<SiPersonalSchedule> SiPersonalSchedule_GetRecords(MsUser loginUser);

        [OperationContract]
        List<SiPersonalSchedule> SiPersonalSchedule_GetRecordsByMsSeninID(MsUser loginUser, int msSeninId);

        [OperationContract]
        List<SiPersonalSchedule> SiPersonalSchedule_SearchRecords(MsUser loginUser, int msSiShokumeiId, string name, DateTime fromDate, DateTime toDate);


        [OperationContract]
        bool SiPersonalSchedule_InsertOrUpdate(MsUser loginUser, SiPersonalSchedule s);
    }

    public partial class Service
    {
        public List<SiPersonalSchedule> SiPersonalSchedule_GetRecords(MsUser loginUser)
        {
            return SiPersonalSchedule.GetRecords(loginUser);
        }

        public List<SiPersonalSchedule> SiPersonalSchedule_GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            return SiPersonalSchedule.GetRecordsByMsSeninID(loginUser, msSeninId);
        }

        public List<SiPersonalSchedule> SiPersonalSchedule_SearchRecords(MsUser loginUser, int msSiShokumeiId, string name, DateTime fromDate, DateTime toDate)
        {
            return SiPersonalSchedule.SearchRecords(loginUser, msSiShokumeiId, name, fromDate, toDate);
        }

        public bool SiPersonalSchedule_InsertOrUpdate(MsUser loginUser, SiPersonalSchedule s)
        {
            s.RenewUserID = loginUser.MsUserID;
            s.RenewDate = DateTime.Now;

            if (s.IsNew())
            {
                s.SiPersonalScheduleID = System.Guid.NewGuid().ToString();
                return s.InsertRecord(loginUser);
            }
            else
            {
                return s.UpdateRecord(loginUser);
            }
        }

    }
}
