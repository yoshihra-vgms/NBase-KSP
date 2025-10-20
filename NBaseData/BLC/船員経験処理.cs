using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;

namespace NBaseData.BLC
{
    public class 船員経験処理
    {
        public static bool BLC_積荷実績登録(MsUser loginUser, SeninTableCache seninTableCache, List<DjDousei> douseiList)
        {
            bool ret = true;

            List<MsCargo> cargoList = MsCargo.GetRecords(loginUser);
            List<MsCargoGroup> cargoGroupList = MsCargoGroup.GetRecords(loginUser);
            Dictionary<int, string> cargoGroupDic = new Dictionary<int, string>();
            foreach(MsCargoGroup cg in cargoGroupList)
            {
                cargoGroupDic.Add(cg.MsCargoGroupID, cg.CargoGroupName);
            }

            List<SiExperienceCargo> experienceForeignList = SiExperienceCargo.GetRecords(loginUser);

            foreach(DjDousei dousei in douseiList)
            {
                // 対象とするのは「積」
                if (dousei.MsKanidouseiInfoShubetuID != MsKanidouseiInfoShubetu.積みID)
                    continue;

                // 対象とするのは「基幹連携済み」
                if (dousei.KikanRenkeiFlag != 1)
                    continue;

                // 今回の次航で積んだ積荷（積荷グループ）を経験値とする
                if (dousei.DjDouseiCargos == null)
                    continue;

                List<int> cargoGroupIds = new List<int>();

                foreach (DjDouseiCargo douseiCargo in dousei.ResultDjDouseiCargos)
                {
                    // 対象とするのは「実績」
                    if (douseiCargo.PlanResultFlag == (int)DjDouseiCargo.PLAN_RESULT_FLAG.PLAN)
                        continue;

                    // 積荷⇒積荷グループ
                    var cargo = cargoList.Where(obj => obj.MsCargoID == douseiCargo.MsCargoID).First();
                    if (cargo.MsCargoGroupID > 0)
                    {
                        if (cargoGroupIds.Contains(cargo.MsCargoGroupID) == false)
                        {
                            cargoGroupIds.Add(cargo.MsCargoGroupID);
                        }
                    }
                }
                if (cargoGroupIds.Count() == 0)
                    continue;

                CommandLogFile.Write("   動静日[" + dousei.DouseiDate.ToString() + "]");

                // この次航時に乗船していた船員情報
                SiCardFilter filter = new SiCardFilter();
                filter.MsVesselIDs.Add(dousei.MsVesselID);
                filter.Start = DateTimeUtils.ToFrom(dousei.DouseiDate).AddSeconds(1);
                filter.End = DateTimeUtils.ToTo(dousei.DouseiDate).AddSeconds(-1);
                filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船));

                List<SiCard> boardingCards = SiCard.GetRecordsByFilter(loginUser, filter);

                foreach(int cargoGroupId in cargoGroupIds)
                {

                    CommandLogFile.Write("   　　積荷[" + cargoGroupDic[cargoGroupId] + "]");

                    foreach(SiCard boardingCard in boardingCards)
                    {
                        CommandLogFile.Write("   　　   [" + seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, boardingCard.SiLinkShokumeiCards[0].MsSiShokumeiID) + "][" + boardingCard.SeninName + "]");


                        if (experienceForeignList.Any(obj => obj.MsSeninID == boardingCard.MsSeninID && obj.MsSiShokumeiID == boardingCard.SiLinkShokumeiCards[0].MsSiShokumeiID && obj.MsCargoGroupID == cargoGroupId))
                        {
                            var experienceForeign = experienceForeignList.Where(obj => obj.MsSeninID == boardingCard.MsSeninID && obj.MsSiShokumeiID == boardingCard.SiLinkShokumeiCards[0].MsSiShokumeiID && obj.MsCargoGroupID == cargoGroupId).First();

                            experienceForeign.Count++;

                            experienceForeign.RenewUserID = loginUser.MsUserID;
                            experienceForeign.RenewDate = DateTime.Now;

                            experienceForeign.UpdateRecord(loginUser);
                        }
                        else
                        {
                            SiExperienceCargo experienceForeign = new SiExperienceCargo();

                            experienceForeign.MsSeninID = boardingCard.MsSeninID;
                            experienceForeign.MsSiShokumeiID = boardingCard.SiLinkShokumeiCards[0].MsSiShokumeiID;
                            experienceForeign.MsCargoGroupID = cargoGroupId;
                            experienceForeign.Count = 1;

                            experienceForeign.RenewUserID = loginUser.MsUserID;
                            experienceForeign.RenewDate = DateTime.Now;

                            experienceForeign.InsertRecord(loginUser);


                            experienceForeignList.Add(experienceForeign);
                        }

                    }

                }

            }

            return ret;
        }
        
        
        public static bool BLC_外航実績登録(MsUser loginUser, SeninTableCache seninTableCache, List<DjDousei> douseiList)
        {
            bool ret = true;

            List<MsCargo> cargoList = MsCargo.GetRecords(loginUser);
            List<MsCargoGroup> cargoGroupList = MsCargoGroup.GetRecords(loginUser);
            Dictionary<int, string> cargoGroupDic = new Dictionary<int, string>();
            foreach (MsCargoGroup cg in cargoGroupList)
            {
                cargoGroupDic.Add(cg.MsCargoGroupID, cg.CargoGroupName);
            }
            List<MsBasho> bashoList = MsBasho.GetRecords(loginUser);
            Dictionary<string, string> gaichiDic = new Dictionary <string, string>();
            foreach (MsBasho b in bashoList)
            {
                if (b.GaichiFlag == 1)
                    gaichiDic.Add(b.MsBashoId, b.BashoName);
            }

            List<SiExperienceForeign> experienceForeignList = SiExperienceForeign.GetRecords(loginUser);

            foreach (DjDousei dousei in douseiList)
            {
                // 対象とするのは「積」
                if (dousei.MsKanidouseiInfoShubetuID != MsKanidouseiInfoShubetu.積みID)
                    continue;

                // 対象とするのは「基幹連携済み」
                if (dousei.KikanRenkeiFlag != 1)
                    continue;

                //　対象とするのは「外地」のみ
                if (gaichiDic.Keys.Contains(dousei.ResultMsBashoID) == false)
                    continue;

                // 今回の次航で積んだ積荷（積荷グループ）を経験値とする
                if (dousei.DjDouseiCargos == null)
                    continue;

                CommandLogFile.Write("   動静日[" + dousei.DouseiDate.ToString() + "]");

                bool existC5 = false;
                bool existNotC5 = false;

                foreach (DjDouseiCargo douseiCargo in dousei.ResultDjDouseiCargos)
                {
                    // 対象とするのは「実績」
                    if (douseiCargo.PlanResultFlag == (int)DjDouseiCargo.PLAN_RESULT_FLAG.PLAN)
                        continue;

                    // 積荷⇒積荷グループ
                    var cargo = cargoList.Where(obj => obj.MsCargoID == douseiCargo.MsCargoID).First();
                    if (cargo.MsCargoGroupID > 0)
                    {
                        if (MsCargoGroup.IsC5(cargo.MsCargoGroupID))
                        {
                            existC5 = true;
                        }
                        else
                        {
                            existNotC5 = true;
                        }
                        CommandLogFile.Write("     港[" + gaichiDic[dousei.ResultMsBashoID] + "] : 積荷[" + cargoGroupDic[cargo.MsCargoGroupID] + "]");
                    }
                }


                // この次航時に乗船していた船員情報
                SiCardFilter filter = new SiCardFilter();
                filter.MsVesselIDs.Add(dousei.MsVesselID);
                filter.Start = DateTimeUtils.ToFrom(dousei.DouseiDate).AddSeconds(1);
                filter.End = DateTimeUtils.ToTo(dousei.DouseiDate).AddSeconds(-1);
                filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船));

                List<SiCard> boardingCards = SiCard.GetRecordsByFilter(loginUser, filter);


                foreach (SiCard boardingCard in boardingCards)
                {
                    CommandLogFile.Write("   　　   [" + seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, boardingCard.SiLinkShokumeiCards[0].MsSiShokumeiID) + "][" + boardingCard.SeninName + "]");

                    if (existC5)
                    {
                        if (experienceForeignList.Any(obj => obj.MsSeninID == boardingCard.MsSeninID && obj.MsSiShokumeiID == boardingCard.SiLinkShokumeiCards[0].MsSiShokumeiID && obj.C5_Flag == 1))
                        {
                            var experienceForeign = experienceForeignList.Where(obj => obj.MsSeninID == boardingCard.MsSeninID && obj.MsSiShokumeiID == boardingCard.SiLinkShokumeiCards[0].MsSiShokumeiID && obj.C5_Flag == 1).First();

                            experienceForeign.Count++;

                            experienceForeign.RenewUserID = loginUser.MsUserID;
                            experienceForeign.RenewDate = DateTime.Now;

                            experienceForeign.UpdateRecord(loginUser);
                        }
                        else
                        {
                            SiExperienceForeign experienceForeign = new SiExperienceForeign();

                            experienceForeign.MsSeninID = boardingCard.MsSeninID;
                            experienceForeign.MsSiShokumeiID = boardingCard.SiLinkShokumeiCards[0].MsSiShokumeiID;
                            experienceForeign.C5_Flag = 1;
                            experienceForeign.Count = 1;

                            experienceForeign.RenewUserID = loginUser.MsUserID;
                            experienceForeign.RenewDate = DateTime.Now;

                            experienceForeign.InsertRecord(loginUser);


                            experienceForeignList.Add(experienceForeign);
                        }
                    }
                    if (existNotC5)
                    {
                        if (experienceForeignList.Any(obj => obj.MsSeninID == boardingCard.MsSeninID && obj.MsSiShokumeiID == boardingCard.SiLinkShokumeiCards[0].MsSiShokumeiID && obj.C5_Flag == 0))
                        {
                            var experienceForeign = experienceForeignList.Where(obj => obj.MsSeninID == boardingCard.MsSeninID && obj.MsSiShokumeiID == boardingCard.SiLinkShokumeiCards[0].MsSiShokumeiID && obj.C5_Flag == 0).First();

                            experienceForeign.Count++;

                            experienceForeign.RenewUserID = loginUser.MsUserID;
                            experienceForeign.RenewDate = DateTime.Now;

                            experienceForeign.UpdateRecord(loginUser);
                        }
                        else
                        {
                            SiExperienceForeign experienceForeign = new SiExperienceForeign();

                            experienceForeign.MsSeninID = boardingCard.MsSeninID;
                            experienceForeign.MsSiShokumeiID = boardingCard.SiLinkShokumeiCards[0].MsSiShokumeiID;
                            experienceForeign.C5_Flag = 0;
                            experienceForeign.Count = 1;

                            experienceForeign.RenewUserID = loginUser.MsUserID;
                            experienceForeign.RenewDate = DateTime.Now;

                            experienceForeign.InsertRecord(loginUser);


                            experienceForeignList.Add(experienceForeign);
                        }
                    }

                }

            }

            return ret;
        }
    }
}
