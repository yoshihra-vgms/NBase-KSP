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
        List<MsSeninCompany> MsSeninCompany_GetRecords(MsUser loginUser);
        
    }

    public partial class Service
    {
        public List<MsSeninCompany> MsSeninCompany_GetRecords(MsUser loginUser)
        {
            List<MsSeninCompany> ret;
            ret = MsSeninCompany.GetRecords(loginUser);
            return ret;
        }
    }
}
