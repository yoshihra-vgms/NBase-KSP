using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseData.BLC;
using NBaseCommon.Nyukyo;

namespace Hachu.Utils
{
    [Serializable]
    public class WCFDockOrderDacProxy : IDockOrderDacProxy
    {
        #region IDockOrderDacProxy メンバ

        public List<MsItemSbt> MsItemSbt_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<MsItemSbt> msItemSbts = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msItemSbts = serviceClient.MsItemSbt_GetRecords(loginUser);
            }
            return msItemSbts;
        }

        public List<MsTani> MsTani_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<MsTani> msTanis = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msTanis = serviceClient.MsTani_GetRecords(loginUser);
            }
            return msTanis;
        }

        public List<OdThiItem> BLC_直近ドックオーダー品目(NBaseData.DAC.MsUser loginUser, int msVesselID, string msThiIraiShousaiID)
        {
            List<OdThiItem> odThiItems = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                odThiItems = serviceClient.BLC_入渠_直近ドックオーダー品目(loginUser,  msVesselID, msThiIraiShousaiID);
            }
            return odThiItems;
        }

        #endregion
    }
}
