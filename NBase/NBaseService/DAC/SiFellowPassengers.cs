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
        List<SiFellowPassengers> SiFellowPassengers_GetRecords(MsUser loginUser);

        [OperationContract]
        List<SiFellowPassengers> SiFellowPassengers_GetRecordsByMsSeninID(MsUser loginUser, int msSeninId);

        [OperationContract]
        List<SiFellowPassengers> SiFellowPassengers_SearchRecords(MsUser loginUser, int msSiShokumeiId, string name);


        [OperationContract]
        bool SiFellowPassengers_InsertOrUpdate(MsUser loginUser, SiFellowPassengers s);
    }

    public partial class Service
    {
        public List<SiFellowPassengers> SiFellowPassengers_GetRecords(MsUser loginUser)
        {
            return SiFellowPassengers.GetRecords(loginUser);
        }

        public List<SiFellowPassengers> SiFellowPassengers_GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            return SiFellowPassengers.GetRecordsByMsSeninID(loginUser, msSeninId);
        }

        public List<SiFellowPassengers> SiFellowPassengers_SearchRecords(MsUser loginUser, int msSiShokumeiId, string name)
        {
            return SiFellowPassengers.SearchRecords(loginUser, msSiShokumeiId, name);
        }

        public bool SiFellowPassengers_InsertOrUpdate(MsUser loginUser, SiFellowPassengers s)
        {
            s.RenewUserID = loginUser.MsUserID;
            s.RenewDate = DateTime.Now;

            if (s.IsNew())
            {
                s.SiFellowPassengersID = System.Guid.NewGuid().ToString();
                return s.InsertRecord(loginUser);
            }
            else
            {
                return s.UpdateRecord(loginUser);
            }
        }

    }
}
