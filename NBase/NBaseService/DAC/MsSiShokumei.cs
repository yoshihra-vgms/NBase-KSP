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
        List<MsSiShokumei> MsSiShokumei_GetRecords(MsUser loginUser);
    }

    public partial class Service
    {
        public List<MsSiShokumei> MsSiShokumei_GetRecords(MsUser loginUser)
        {
            return MsSiShokumei.GetRecords(loginUser);
        }
    }
}
