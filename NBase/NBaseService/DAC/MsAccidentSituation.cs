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
        List<MsAccidentSituation> MsAccidentSituation_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsAccidentSituation> MsAccidentSituation_SearchRecords(MsUser loginUser, string name);


        [OperationContract]
        bool MsAccidentSituation_InsertOrUpdate(MsUser loginUser, MsAccidentSituation m);
    }

    public partial class Service
    {
        public List<MsAccidentSituation> MsAccidentSituation_GetRecords(MsUser loginUser)
        {
            return MsAccidentSituation.GetRecords(loginUser);
        }


        public List<MsAccidentSituation> MsAccidentSituation_SearchRecords(MsUser loginUser, string name)
        {
            return MsAccidentSituation.SearchRecords(loginUser, name);
        }


        public bool MsAccidentSituation_InsertOrUpdate(MsUser loginUser, MsAccidentSituation m)
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
