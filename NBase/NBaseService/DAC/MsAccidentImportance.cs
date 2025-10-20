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
        List<MsAccidentImportance> MsAccidentImportance_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsAccidentImportance> MsAccidentImportance_SearchRecords(MsUser loginUser, string name);


        [OperationContract]
        bool MsAccidentImportance_InsertOrUpdate(MsUser loginUser, MsAccidentImportance m);
    }

    public partial class Service
    {
        public List<MsAccidentImportance> MsAccidentImportance_GetRecords(MsUser loginUser)
        {
            return MsAccidentImportance.GetRecords(loginUser);
        }


        public List<MsAccidentImportance> MsAccidentImportance_SearchRecords(MsUser loginUser, string name)
        {
            return MsAccidentImportance.SearchRecords(loginUser, name);
        }


        public bool MsAccidentImportance_InsertOrUpdate(MsUser loginUser, MsAccidentImportance m)
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
