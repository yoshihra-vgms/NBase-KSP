using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using System.ServiceModel;
using NBaseData.DS;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<SiSalary> SiSalary_SearchRecords(MsUser loginUser, int msSeninId, DateTime start, DateTime end, bool kind0, bool kind1, bool kind2);
    }

    public partial class Service
    {
        public List<SiSalary> SiSalary_SearchRecords(MsUser loginUser, int msSeninId, DateTime start, DateTime end, bool kind0, bool kind1, bool kind2)
        {
            return SiSalary.SearchRecords(loginUser, msSeninId, start, end, kind0, kind1, kind2);
        }
    }
}
