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
        List<MsSiShokumeiShousai> MsSiShokumeiShousai_GetRecords(MsUser loginUser);
    }

    public partial class Service
    {
        public List<MsSiShokumeiShousai> MsSiShokumeiShousai_GetRecords(MsUser loginUser)
        {
            return MsSiShokumeiShousai.GetRecords(loginUser);
        }
    }
}
