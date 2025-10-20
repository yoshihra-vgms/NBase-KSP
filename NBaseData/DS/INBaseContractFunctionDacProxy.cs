using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace NBaseData.DS
{
    public interface INbaseContractFunctionDacProxy
    {
        List<NbaseContractFunction> NbaseContractFunction_GetRecords(MsUser loginUser);
    }
}
