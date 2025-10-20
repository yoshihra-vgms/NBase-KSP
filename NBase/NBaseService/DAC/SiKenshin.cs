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
        List<SiKenshin> SiKenshin_GetRecordsByMsSeninID(MsUser loginUser, int msSeninId);
    }

    public partial class Service
    {
        public List<SiKenshin> SiKenshin_GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            return SiKenshin.GetRecordsByMsSeninID(loginUser, msSeninId);
        }
    }
}
