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
        List<MsScheduleCategory> MsScheduleCategory_GetRecords(MsUser loginUser);

        [OperationContract]
        bool MsScheduleCategory_InsertOrUpdate(MsUser loginUser, MsScheduleCategory m);
    }

    public partial class Service
    {
        public List<MsScheduleCategory> MsScheduleCategory_GetRecords(MsUser loginUser)
        {
            return MsScheduleCategory.GetRecords(loginUser);
        }

        public bool MsScheduleCategory_InsertOrUpdate(MsUser loginUser, MsScheduleCategory m)
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
