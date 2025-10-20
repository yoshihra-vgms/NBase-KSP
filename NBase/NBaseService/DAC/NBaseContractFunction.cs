using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.ServiceModel;
using System.Collections.Generic;
using NBaseData.DAC;
using ORMapping;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<NbaseContractFunction> NbaseContractFunction_GetRecords(MsUser loginUser);
    }

    public partial class Service
    {
        public List<NbaseContractFunction> NbaseContractFunction_GetRecords(MsUser loginUser)
        {
            return NbaseContractFunction.GetRecords(loginUser);
        }
    }
}
