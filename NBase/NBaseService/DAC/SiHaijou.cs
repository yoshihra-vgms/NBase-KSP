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
        SiHaijou SiHaijou_GetRecord_前回配信(MsUser loginUser);

        [OperationContract]
        List<SiHaijou> SiHaijou_GetRecordsByMsUserID(MsUser loginUser, string ms_user_id);
    }


    public partial class Service
    {
        public SiHaijou SiHaijou_GetRecord_前回配信(MsUser loginUser)
        {
            return SiHaijou.GetRecord_前回配信(loginUser);
        }

        public List<SiHaijou> SiHaijou_GetRecordsByMsUserID(MsUser loginUser, string ms_user_id)
        {
            return SiHaijou.GetRecordsByMsUserID(loginUser, ms_user_id);
        }
    }
}
