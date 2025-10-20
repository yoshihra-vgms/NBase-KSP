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
        List<MsAccidentKind> MsAccidentKind_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsAccidentKind> MsAccidentKind_SearchRecords(MsUser loginUser, string name);


        [OperationContract]
        bool MsAccidentKind_InsertOrUpdate(MsUser loginUser, MsAccidentKind m);
    }

    public partial class Service
    {
        public List<MsAccidentKind> MsAccidentKind_GetRecords(MsUser loginUser)
        {
            return MsAccidentKind.GetRecords(loginUser);
        }


        public List<MsAccidentKind> MsAccidentKind_SearchRecords(MsUser loginUser, string name)
        {
            return MsAccidentKind.SearchRecords(loginUser, name);
        }


        public bool MsAccidentKind_InsertOrUpdate(MsUser loginUser, MsAccidentKind m)
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
