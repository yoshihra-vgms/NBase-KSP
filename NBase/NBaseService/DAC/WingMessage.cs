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
        List<WingMessage> WingMessage_GetRecords(MsUser loginUser);

        [OperationContract]
        bool WingMessage_InsertRecord(MsUser loginUser, WingMessage info);

        [OperationContract]
        bool WingMessage_UpdateRecord(MsUser loginUser, WingMessage info);
    }

    public partial class Service
    {
        public List<WingMessage> WingMessage_GetRecords(MsUser loginUser)
        {
            return WingMessage.GetRecords(loginUser);
        }

        public bool WingMessage_InsertRecord(MsUser loginUser, WingMessage info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool WingMessage_UpdateRecord(MsUser loginUser, WingMessage info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
