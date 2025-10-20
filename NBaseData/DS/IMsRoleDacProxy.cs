using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace NBaseData.DS
{
    public interface IMsRoleDacProxy
    {
        List<MsRole> MsRole_GetRecords(MsUser loginUser);

        MsUserBumon MsUserBumon_GetRecord(MsUser loginUser);
    }
}
