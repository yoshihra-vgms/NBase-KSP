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
        List<配乗シミュレーション> BLC_配乗シミュレーション_下船者検索(MsUser loginUser, DateTime baseDate, int vesselId, int shokumeiId, int days, 
                                                                       bool personalScheduleCheck);

        [OperationContract]
        List<配乗シミュレーション> BLC_配乗シミュレーション_乗船者検索(MsUser loginUser, DateTime baseDate, int vesselId, int shokumeiId, int days, 
                                                                       List<int> shubetsuIds,
                                                                       bool vesselCheck,
                                                                       bool shokumeiCheck,
                                                                       bool menjouCheck,
                                                                       bool koushuCheck,
                                                                       bool personalScheduleCheck,
                                                                       bool fellowPassengersCheck,
                                                                       bool experienceCheck);

        [OperationContract]
        SiBoardingSchedule BLC_配乗シミュレーション_交代者決定(MsUser loginUser, DateTime signOnDate, string signOffSiCardId, int vesselId, int shokumeiId, int signOnCrewId);

        [OperationContract]
        bool BLC_配乗シミュレーション_交代者解除(MsUser loginUser, string boardingScheduleId);

    }

    public partial class Service
    {
        public List<配乗シミュレーション> BLC_配乗シミュレーション_下船者検索(MsUser loginUser, DateTime baseDate, int vesselId, int shokumeiId, int days, bool personalScheduleCheck)
        {
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            return 配乗シミュレーション.BLC_下船者検索(loginUser, seninTableCache, baseDate, vesselId, shokumeiId, days, personalScheduleCheck);
        }


        public List<配乗シミュレーション> BLC_配乗シミュレーション_乗船者検索(MsUser loginUser, DateTime baseDate, int vesselId, int shokumeiId, int days, 
                                                                            List<int> shubetsuIds,
                                                                            bool vesselCheck,
                                                                            bool shokumeiCheck,
                                                                            bool menjouCheck,
                                                                            bool koushuCheck,
                                                                            bool personalScheduleCheck,
                                                                            bool fellowPassengersCheck,
                                                                            bool experienceCheck)
        {
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            return 配乗シミュレーション.BLC_乗船者検索(loginUser, seninTableCache, baseDate, vesselId, shokumeiId, days, shubetsuIds, vesselCheck, shokumeiCheck, menjouCheck, koushuCheck, personalScheduleCheck, fellowPassengersCheck, experienceCheck);

        }



        public SiBoardingSchedule BLC_配乗シミュレーション_交代者決定(MsUser loginUser, DateTime signOnDate, string signOffSiCardId, int vesselId, int shokumeiId, int signOnCrewId)
        {
            SiBoardingSchedule boardingSchedule = null;
            bool ret = true;

            boardingSchedule = SiBoardingSchedule.GetRecordBySignOffSiCardID(null, loginUser, signOffSiCardId);
            if (boardingSchedule != null)
            {
                boardingSchedule.MsSeninID = signOnCrewId;
                boardingSchedule.MsSiShokumeiID = shokumeiId;
                boardingSchedule.MsVesselID = vesselId;
                boardingSchedule.SignOnDate = signOnDate;

                boardingSchedule.RenewUserID = loginUser.MsUserID;
                boardingSchedule.RenewDate = DateTime.Now;

                ret = boardingSchedule.UpdateRecord(null, loginUser);
            }
            else
            {
                boardingSchedule = new SiBoardingSchedule();

                boardingSchedule.SiBoardingScheduleID = System.Guid.NewGuid().ToString();
                boardingSchedule.MsSeninID = signOnCrewId;
                boardingSchedule.MsSiShokumeiID = shokumeiId;
                boardingSchedule.MsVesselID = vesselId;
                boardingSchedule.SignOnDate = signOnDate;
                boardingSchedule.SignOffSiCardID = signOffSiCardId;
                boardingSchedule.Status = 0; // 予定

                boardingSchedule.RenewUserID = loginUser.MsUserID;
                boardingSchedule.RenewDate = DateTime.Now;

                ret = boardingSchedule.InsertRecord(null, loginUser);
            }

            if (ret)
            {
                return boardingSchedule;
            }
            else
            {
                return null;
            }
        }

        public bool BLC_配乗シミュレーション_交代者解除(MsUser loginUser, string boardingScheduleId)
        {
            bool ret = true;

            SiBoardingSchedule boardingSchedule = SiBoardingSchedule.GetRecordByBoardingScheduleID(null, loginUser, boardingScheduleId);
            if (boardingScheduleId != null)
            {
                boardingSchedule.DeleteFlag = 1;

                ret = boardingSchedule.UpdateRecord(null, loginUser);

            }

            return ret;
        }
    }
}