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
        SiLaborManagementRecordBook SiLaborManagementRecordBook_GetRecord(MsUser loginUser);
    }

    public partial class Service
    {
        public SiLaborManagementRecordBook SiLaborManagementRecordBook_GetRecord(MsUser loginUser)
        {
            return SiLaborManagementRecordBook.GetRecord(loginUser);
        }
    }
}
