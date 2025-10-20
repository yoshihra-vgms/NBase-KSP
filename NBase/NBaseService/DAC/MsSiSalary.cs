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
        List<MsSiSalary> MsSiSalary_SearchRecords(MsUser loginUser, bool kind0, bool kind1, bool kind2);
    }

    public partial class Service
    {
        public List<MsSiSalary> MsSiSalary_SearchRecords(MsUser loginUser, bool kind0, bool kind1, bool kind2)
        {
            return MsSiSalary.SearchRecords(loginUser, kind0, kind1, kind2);
        }
    }
}
