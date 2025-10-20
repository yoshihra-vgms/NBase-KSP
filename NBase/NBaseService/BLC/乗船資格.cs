using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using NBaseData.DAC;
using NBaseData.BLC;
using NBaseData.DS;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<MsVesselRankLicense> BLC_乗船資格_GetRecords(MsUser loginUser);

        [OperationContract]
        bool BLC_乗船資格_InsertOrUpdate(MsUser loginUser, MsVesselRankLicense info);
    }

    public partial class Service
    {
        public List<MsVesselRankLicense> BLC_乗船資格_GetRecords(MsUser loginUser)
        {
            return MsVesselRankLicense.GetRecords(loginUser);
        }


        public bool BLC_乗船資格_InsertOrUpdate(MsUser loginUser, MsVesselRankLicense info)
        {
            bool ret = true;
            List<MsVesselRankLicense> allInfo = MsVesselRankLicense.GetRecords(loginUser);

            info.RenewDate = DateTime.Now;
            info.RenewUserID = loginUser.MsUserID;

            if (allInfo.Any(obj => obj.MsVesselID == info.MsVesselID && obj.MsSiShokumeiID == info.MsSiShokumeiID && obj.MsSiMenjouID == info.MsSiMenjouID && obj.MsSiMenjouKindID == info.MsSiMenjouKindID))
            {
                var dst = allInfo.Where(obj => obj.MsVesselID == info.MsVesselID && obj.MsSiShokumeiID == info.MsSiShokumeiID && obj.MsSiMenjouID == info.MsSiMenjouID && obj.MsSiMenjouKindID == info.MsSiMenjouKindID).First();

                dst.DeleteFlag = info.DeleteFlag;

                ret = dst.UpdateRecord(loginUser);
            }
            else
            {
                ret = info.InsertRecord(loginUser);
            }

            return ret;
        }
    }
}