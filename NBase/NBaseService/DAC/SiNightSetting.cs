using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using System.ServiceModel;
using NBaseData.DS;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<SiNightSetting> SiNightSetting_GetRecords(MsUser loginUser);
    }

    public partial class Service
    {
        public List<SiNightSetting> SiNightSetting_GetRecords(MsUser loginUser)
        {
            return SiNightSetting.GetRecords(loginUser);
        }
    }
}
