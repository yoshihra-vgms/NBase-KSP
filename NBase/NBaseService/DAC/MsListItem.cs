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
        List<MsListItem> MsListItem_GetRecords(MsUser loginUser, int kind);
    }

    public partial class Service
    {
        public List<MsListItem> MsListItem_GetRecords(MsUser loginUser, int kind)
        {
            return MsListItem.GetRecords(loginUser, kind);
        }
    }
}
