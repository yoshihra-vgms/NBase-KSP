using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DS;
using NBaseData.DAC;

namespace NBase.util
{
    public class WcfMsRoleDacProxy : IMsRoleDacProxy
    {
        #region IMsRoleDacProxy メンバ

        public List<MsRole> MsRole_GetRecords(MsUser loginUser)
        {
            List<MsRole> roles = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                roles = serviceClient.MsRole_GetRecords(loginUser);
            }

            return roles;
        }


        public MsUserBumon MsUserBumon_GetRecord(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsUserBumon_GetRecordsByUserID(loginUser, loginUser.MsUserID);
            }
        }

        #endregion
    }
}
