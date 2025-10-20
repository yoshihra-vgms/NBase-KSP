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
        MsSeninAddress MsSeninAddress_GetRecordsByMsSeninID(MsUser loginUser, int msSeninId);
    }

    public partial class Service
    {
        public MsSeninAddress MsSeninAddress_GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            return MsSeninAddress.GetRecordsByMsSeninID(loginUser, msSeninId);
        }
    }
}
