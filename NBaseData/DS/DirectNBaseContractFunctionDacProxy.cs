using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace NBaseData.DS
{
    public class DirectNBaseContractFunctionDacProxy : INbaseContractFunctionDacProxy
    {
        #region INbaseContractFunctionDacProxy メンバ

        public List<NbaseContractFunction> NbaseContractFunction_GetRecords(MsUser loginUser)
        {
            List<NbaseContractFunction> roles = null;

            roles = NbaseContractFunction.GetRecords(loginUser);

            return roles;
        }

        #endregion
    }
}
