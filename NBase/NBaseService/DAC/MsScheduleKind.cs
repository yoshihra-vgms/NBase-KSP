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
        List<MsScheduleKind> MsScheduleKind_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsScheduleKind> MsScheduleKind_SearchRecords(MsUser loginUser, int categoryId, string name);


        [OperationContract]
        bool MsScheduleKind_InsertOrUpdate(MsUser loginUser, MsScheduleKind m);
    }

    public partial class Service
    {
        public List<MsScheduleKind> MsScheduleKind_GetRecords(MsUser loginUser)
        {
            return MsScheduleKind.GetRecords(loginUser);
        }


        public List<MsScheduleKind> MsScheduleKind_SearchRecords(MsUser loginUser, int categoryId, string name)
        {
            return MsScheduleKind.SearchRecords(loginUser, categoryId, name);
        }


        public bool MsScheduleKind_InsertOrUpdate(MsUser loginUser, MsScheduleKind m)
        {
            m.UpdateMsUserID = loginUser.MsUserID;

            if (m.IsNew())
            {
                m.CreateMsUserID = loginUser.MsUserID;
                return m.InsertRecord(loginUser);
            }
            else
            {
                return m.UpdateRecord(loginUser);
            }
        }
    }
}
