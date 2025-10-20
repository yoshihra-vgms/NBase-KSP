using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using System.ServiceModel;
using ORMapping;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<SiAllowanceDetail> SiAllowanceDetail_GetRecords(MsUser loginUser, string allowanceName);
    }

    public partial class Service
    {
        public List<SiAllowanceDetail> SiAllowanceDetail_GetRecords(MsUser loginUser, string allowanceName)
        {
            List<SiAllowanceDetail> ret = SiAllowanceDetail.GetRecords(loginUser, allowanceName);
            return ret;
        }
    }
}
