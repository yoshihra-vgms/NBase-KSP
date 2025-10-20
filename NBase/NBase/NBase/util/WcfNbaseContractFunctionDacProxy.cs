using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DS;
using NBaseData.DAC;

namespace NBase.util
{
    public class WcfNbaseContractFunctionDacProxy : INbaseContractFunctionDacProxy
    {
        #region INbaseContractFunctionDacProxy メンバ

        public List<NbaseContractFunction> NbaseContractFunction_GetRecords(MsUser loginUser)
        {
            List<NbaseContractFunction> roles = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                roles = serviceClient.NbaseContractFunction_GetRecords(loginUser);
            }

            return roles;
        }

        #endregion
    }
}
