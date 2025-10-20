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
        List<MsSiExcludeMenjouKind> MsSiExcludeMenjouKind_GetRecordsByMsSiMenjouKindID(MsUser loginUser, int msSiMenjouKindId);
    }

    public partial class Service
    {
        public List<MsSiExcludeMenjouKind> MsSiExcludeMenjouKind_GetRecordsByMsSiMenjouKindID(MsUser loginUser, int msSiMenjouKindId)
        {
            return MsSiExcludeMenjouKind.GetRecordsByMsSiMenjouKindID(loginUser, msSiMenjouKindId);
        }
    }
}
