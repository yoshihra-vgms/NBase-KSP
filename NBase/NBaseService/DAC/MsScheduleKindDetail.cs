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
        List<MsScheduleKindDetail> MsScheduleKindDetail_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsScheduleKindDetail> MsScheduleKindDetail_SearchRecords(MsUser loginUser, int kindId, string name);


        [OperationContract]
        bool MsScheduleKindDetail_InsertOrUpdate(MsUser loginUser, MsScheduleKindDetail m);
    }

    public partial class Service
    {
        public List<MsScheduleKindDetail> MsScheduleKindDetail_GetRecords(MsUser loginUser)
        {
            return MsScheduleKindDetail.GetRecords(loginUser);
        }


        public List<MsScheduleKindDetail> MsScheduleKindDetail_SearchRecords(MsUser loginUser, int kindId, string name)
        {
            return MsScheduleKindDetail.SearchRecords(loginUser, kindId, name);
        }


        public bool MsScheduleKindDetail_InsertOrUpdate(MsUser loginUser, MsScheduleKindDetail m)
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
