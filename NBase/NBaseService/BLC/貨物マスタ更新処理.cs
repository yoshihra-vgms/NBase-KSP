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
        bool BLC_貨物マスタ更新処理_追加処理(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsCargo Cargo);

        [OperationContract]
        bool BLC_貨物マスタ更新処理_更新処理(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsCargo Cargo);
    }

    public partial class Service
    {
        public bool BLC_貨物マスタ更新処理_追加処理(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsCargo Cargo)
        {
            return NBaseData.BLC.貨物マスタ更新処理.追加処理(loginUser, Cargo);
        }

        public bool BLC_貨物マスタ更新処理_更新処理(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsCargo Cargo)
        {
            return NBaseData.BLC.貨物マスタ更新処理.更新処理(loginUser, Cargo);
        }
    }
}
