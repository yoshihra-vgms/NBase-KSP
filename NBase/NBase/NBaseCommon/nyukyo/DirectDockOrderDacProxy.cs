using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseData.BLC;

namespace NBaseCommon.Nyukyo
{
    public class DirectDockOrderDacProxy : IDockOrderDacProxy
    {

        #region IDockOrderDacProxy メンバ

        public List<MsItemSbt> MsItemSbt_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            return MsItemSbt.GetRecords(loginUser);
        }

        public List<MsTani> MsTani_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            return MsTani.GetRecords(loginUser);
        }

        public List<OdThiItem> BLC_直近ドックオーダー品目(NBaseData.DAC.MsUser loginUser, int msVesselID, string msThiIraiShousaiID)
        {
            return 入渠.BLC_直近ドックオーダー品目(loginUser, msVesselID, msThiIraiShousaiID);
        }

        #endregion
    }
}
