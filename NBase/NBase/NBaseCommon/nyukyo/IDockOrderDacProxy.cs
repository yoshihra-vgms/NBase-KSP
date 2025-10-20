using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace NBaseCommon.Nyukyo
{
    public interface IDockOrderDacProxy
    {
        List<MsItemSbt> MsItemSbt_GetRecords(NBaseData.DAC.MsUser loginUser);
        
        List<MsTani> MsTani_GetRecords(NBaseData.DAC.MsUser loginUser);

        List<OdThiItem> BLC_直近ドックオーダー品目(MsUser loginUser, int msVesselID, string msThiIraiShousaiID);

   }
}
