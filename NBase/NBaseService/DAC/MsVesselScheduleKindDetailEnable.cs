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
        List<MsVesselScheduleKindDetailEnable> MsVesselScheduleKindDetailEnable_GetRecords(MsUser loginUser);

        [OperationContract]
        bool MsVesselScheduleKindDetailEnable_InsertOrUpdate(MsUser loginUser, MsVesselScheduleKindDetailEnable m);
    }

    public partial class Service
    {
        public List<MsVesselScheduleKindDetailEnable> MsVesselScheduleKindDetailEnable_GetRecords(MsUser loginUser)
        {
            return MsVesselScheduleKindDetailEnable.GetRecords(loginUser);
        }

        public bool MsVesselScheduleKindDetailEnable_InsertOrUpdate(MsUser loginUser, MsVesselScheduleKindDetailEnable m)
        {
            if (m.IsNew())
            {
                m.CreateMsUserID = loginUser.MsUserID;
                m.UpdateMsUserID = loginUser.MsUserID;
                return m.InsertRecord(loginUser);
            }
            else
            {
                m.UpdateMsUserID = loginUser.MsUserID;
                return m.UpdateRecord(loginUser);
            }
        }
    }
}
