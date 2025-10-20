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
        List<SnSyncInfo> SnSyncInfo_GetRecords(MsUser loginUser);

        [OperationContract]
        SnSyncInfo SnSyncInfo_GetRecord(MsUser loginUser, string hostName);

        [OperationContract]
        bool SnSyncInfo_InsertRecord(MsUser loginUser, SnSyncInfo info);

        [OperationContract]
        bool SnSyncInfo_UpdateRecord(MsUser loginUser, SnSyncInfo info);
    }

    public partial class Service
    {
        public List<SnSyncInfo> SnSyncInfo_GetRecords(MsUser loginUser)
        {
            return SnSyncInfo.GetRecords(loginUser);
        }

        public SnSyncInfo SnSyncInfo_GetRecord(MsUser loginUser, string hostName)
        {
            return SnSyncInfo.GetRecord(loginUser, hostName);
        }

        public bool SnSyncInfo_InsertRecord(MsUser loginUser, SnSyncInfo info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool SnSyncInfo_UpdateRecord(MsUser loginUser, SnSyncInfo info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
