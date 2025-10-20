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
        SiKenshinPmhKa SiKenshinPmhKa_GetRecordByMsSeninID(MsUser loginUser, int msSeninId);
    }

    public partial class Service
    {
        public SiKenshinPmhKa SiKenshinPmhKa_GetRecordByMsSeninID(MsUser loginUser, int msSeninId)
        {
            return SiKenshinPmhKa.GetRecordByMsSeninID(loginUser, msSeninId);
        }
    }
}
