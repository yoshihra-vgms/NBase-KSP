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
        List<MsKindOfAccident> MsKindOfAccident_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsKindOfAccident> MsKindOfAccident_SearchRecords(MsUser loginUser, string name);


        [OperationContract]
        bool MsKindOfAccident_InsertOrUpdate(MsUser loginUser, MsKindOfAccident m);
    }

    public partial class Service
    {
        public List<MsKindOfAccident> MsKindOfAccident_GetRecords(MsUser loginUser)
        {
            return MsKindOfAccident.GetRecords(loginUser);
        }


        public List<MsKindOfAccident> MsKindOfAccident_SearchRecords(MsUser loginUser, string name)
        {
            return MsKindOfAccident.SearchRecords(loginUser, name);
        }


        public bool MsKindOfAccident_InsertOrUpdate(MsUser loginUser, MsKindOfAccident m)
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
