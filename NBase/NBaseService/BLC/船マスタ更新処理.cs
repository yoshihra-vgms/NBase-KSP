using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC_船マスタ更新処理_追加処理(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVessel vessel, List<NBaseData.DAC.MsVesselScheduleKindDetailEnable> vessellScheduleKindDetailEnableList);

        [OperationContract]
        bool BLC_船マスタ更新処理_更新処理(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVessel vessel, List<NBaseData.DAC.MsVesselScheduleKindDetailEnable> vessellScheduleKindDetailEnableList);
    }

    public partial class Service
    {
        public bool BLC_船マスタ更新処理_追加処理(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVessel vessel, List<NBaseData.DAC.MsVesselScheduleKindDetailEnable> vessellScheduleKindDetailEnableList)
        {
            return NBaseData.BLC.船マスタ更新処理.追加処理(loginUser, vessel, vessellScheduleKindDetailEnableList);
        }

        public bool BLC_船マスタ更新処理_更新処理(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsVessel vessel, List<NBaseData.DAC.MsVesselScheduleKindDetailEnable> vessellScheduleKindDetailEnableList)
        {
            return NBaseData.BLC.船マスタ更新処理.更新処理(loginUser, vessel, vessellScheduleKindDetailEnableList);
        }
    }
}
