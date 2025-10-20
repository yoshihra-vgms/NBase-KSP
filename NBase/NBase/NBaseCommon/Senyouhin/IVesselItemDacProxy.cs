using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace NBaseCommon.Senyouhin
{
    public interface IVesselItemDacProxy
    {
        List<MsVesselItemCategory> MsVesselItemCategory_GetRecords(NBaseData.DAC.MsUser loginUser);

        List<MsVesselItemVessel> MsVesselItemVessel_GetRecords(NBaseData.DAC.MsUser loginUser, int msVesselID);
        
        List<MsTani> MsTani_GetRecords(NBaseData.DAC.MsUser loginUser);
   }
}
