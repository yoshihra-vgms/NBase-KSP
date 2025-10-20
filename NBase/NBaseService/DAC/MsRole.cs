using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using NBaseData.DAC;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<MsRole> MsRole_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsRole> MsRole_SearchRecords(MsUser loginUser, string name);


        [OperationContract]
        bool MsRole_InsertOrUpdate(MsUser loginUser, List<MsRole> roles);
    }

    public partial class Service
    {
        public List<MsRole> MsRole_GetRecords(MsUser loginUser)
        {
            return MsRole.GetRecords(loginUser);
        }


        public List<MsRole> MsRole_SearchRecords(MsUser loginUser, string name)
        {
            return MsRole.SearchRecords(loginUser, name);
        }


        public bool MsRole_InsertOrUpdate(MsUser loginUser, List<MsRole> roles)
        {
            return MsRole.InsertOrUpdate(loginUser, roles);
        }
    }
}
