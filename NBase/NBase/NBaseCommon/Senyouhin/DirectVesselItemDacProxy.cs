using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseData.BLC;

namespace NBaseCommon.Senyouhin
{
    public class DirectVesselItemDacProxy : IVesselItemDacProxy
    {

        #region DirectVesselItemDacProxy メンバ

        public List<MsVesselItemCategory> MsVesselItemCategory_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            return MsVesselItemCategory.GetRecords(loginUser);
        }

        public List<MsVesselItemVessel> MsVesselItemVessel_GetRecords(NBaseData.DAC.MsUser loginUser, int msVesselId)
        {
            return MsVesselItemVessel.GetRecordsByMsVesselID(loginUser, msVesselId);
        }

        public List<MsTani> MsTani_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            return MsTani.GetRecords(loginUser);
        }

        #endregion
    }
}
