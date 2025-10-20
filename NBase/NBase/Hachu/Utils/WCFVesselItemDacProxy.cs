using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseData.BLC;
using NBaseCommon.Senyouhin;

namespace Hachu.Utils
{
    [Serializable]
    public class WCFVesselItemDacProxy : IVesselItemDacProxy
    {
        #region IVesselItemDacProxy メンバ

        public List<MsVesselItemCategory> MsVesselItemCategory_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<MsVesselItemCategory> msVesselItemCategorys = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msVesselItemCategorys = serviceClient.MsVesselItemCategory_GetRecords(loginUser);
            }
            return msVesselItemCategorys;
        }

        public List<MsVesselItemVessel> MsVesselItemVessel_GetRecords(NBaseData.DAC.MsUser loginUser, int msVesselId)
        {
            List<MsVesselItemVessel> msVesselItemVessels = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msVesselItemVessels = serviceClient.MsVesselItemVessel_GetRecordByMsVesselID(loginUser, msVesselId);
            }
            return msVesselItemVessels;
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

        #endregion
    }
}
