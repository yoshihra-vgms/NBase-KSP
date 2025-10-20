using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace NBaseData.DS
{
    public class DirectMsRoleDacProxy : IMsRoleDacProxy
    {
        #region IMsRoleDacProxy メンバ

        public List<NBaseData.DAC.MsRole> MsRole_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            return MsRole.GetRecords(loginUser);
        }


        public MsUserBumon MsUserBumon_GetRecord(MsUser loginUser)
        {
            return MsUserBumon.GetRecordByUserID(loginUser, loginUser.MsUserID);
        }

        #endregion
    }
}
