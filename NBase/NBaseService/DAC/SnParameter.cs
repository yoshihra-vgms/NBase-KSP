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
        NBaseData.DAC.SnParameter SnParameter_GetRecord(MsUser loginUserd);
    }

    public partial class Service
    {
        public NBaseData.DAC.SnParameter SnParameter_GetRecord(MsUser loginUser)
        {
            return NBaseData.DAC.SnParameter.GetRecord(loginUser);
        }
    }
}
