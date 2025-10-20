using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using System.ServiceModel;
using NBaseData.DS;
using ORMapping;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<SiCardPlanHead> SiCardPlanHead_GetRecordsByYearMonth(MsUser loginUser, DateTime dt, int vessel_kind);

    }

    public partial class Service
    {
        public List<SiCardPlanHead> SiCardPlanHead_GetRecordsByYearMonth(MsUser loginUser, DateTime dt, int vessel_kind)
        {
            return SiCardPlanHead.GetRecordsByYearMonth(loginUser, dt, vessel_kind);
        }

        
    }
}
