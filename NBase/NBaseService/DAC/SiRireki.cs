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
        List<SiRireki> SiRireki_GetRecordsByMsSeninID(MsUser loginUser, int msSeninId);
    }

    public partial class Service
    {
        public List<SiRireki> SiRireki_GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            return SiRireki.GetRecordsByMsSeninID(loginUser, msSeninId);
        }
    }
}
