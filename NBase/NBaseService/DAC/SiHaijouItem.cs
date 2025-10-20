using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<SiHaijouItem> SiHaijouItem_GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id);
    }


    public partial class Service
    {
        public List<SiHaijouItem> SiHaijouItem_GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id)
        {
            return SiHaijouItem.GetRecordsByMsVesselID(loginUser, ms_vessel_id);
        }
    }
}
