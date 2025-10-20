using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using ORMapping;

namespace NBaseData.BLC
{
    public class 船マスタ更新処理
    {
        public static bool 追加処理(MsUser loginUser, MsVessel vessel, List<MsVesselScheduleKindDetailEnable> vessellScheduleKindDetailEnableList)
        {
            bool ret = true;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    ret = vessel.InsertRecord(dbConnect, loginUser);
                    if (ret)
                    {
                        vessel.MsVesselID = Sequences.GetMsVesselId(dbConnect, loginUser);

                        if (vessel.YojitsuEnabled == 1)
                        {
                            ret = 予算作成処理(dbConnect, loginUser, vessel);
                        }
                    }

                    if (ret)
                    {
                        foreach (MsVesselScheduleKindDetailEnable vessellScheduleKindDetailEnable in vessellScheduleKindDetailEnableList)
                        {
                            vessellScheduleKindDetailEnable.MsVesselID = vessel.MsVesselID;
                            vessellScheduleKindDetailEnable.CreateMsUserID = loginUser.MsUserID;
                            vessellScheduleKindDetailEnable.UpdateMsUserID = loginUser.MsUserID;
                            ret = vessellScheduleKindDetailEnable.InsertRecord(dbConnect, loginUser);
                        }
                    }

                    if (ret)
                        dbConnect.Commit();
                    else
                        dbConnect.RollBack();
                }
                catch (Exception e)
                {
                    ret = false;
                    dbConnect.RollBack();
                }
            }
            return ret;
        }

        public static bool 更新処理(MsUser loginUser, MsVessel vessel, List<MsVesselScheduleKindDetailEnable> vessellScheduleKindDetailEnableList)
        {
            bool ret = true;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    ret = vessel.UpdateRecord(dbConnect, loginUser);

                    if (ret && vessel.YojitsuEnabled == 1)
                    {
                        ret = 予算更新処理(dbConnect, loginUser, vessel);
                    }

                    // 2019.12.18 削除してしまうと、チェックを戻しても文書との関連がなくなってしまって戻せなくなるので削除処理はなしとする
                    ////------------------------------------------------------
                    ////2017/5/30 m.yoshihara
                    //if (ret && vessel.DocumentEnabled == 0)
                    //{
                    //    //該当するvesselを削除する
                    //    DmPublisher.LogicalDeleteByMsVesseId(dbConnect, loginUser, vessel.MsVesselID);

                    //    DmKoukaiSaki.LogicalDeleteByMsVesseId(dbConnect, loginUser, vessel.MsVesselID);
                    //}
                    ////------------------------------------------------------


                    if (ret)
                    {
                        foreach(MsVesselScheduleKindDetailEnable vessellScheduleKindDetailEnable in vessellScheduleKindDetailEnableList)
                        {
                            if (vessellScheduleKindDetailEnable.IsNew())
                            {
                                vessellScheduleKindDetailEnable.MsVesselID = vessel.MsVesselID;
                                vessellScheduleKindDetailEnable.CreateMsUserID = loginUser.MsUserID;
                                vessellScheduleKindDetailEnable.UpdateMsUserID = loginUser.MsUserID;
                                ret = vessellScheduleKindDetailEnable.InsertRecord(dbConnect, loginUser);
                            }
                            else
                            {
                                vessellScheduleKindDetailEnable.UpdateMsUserID = loginUser.MsUserID;
                                ret = vessellScheduleKindDetailEnable.UpdateRecord(dbConnect, loginUser);

                            }
                        }
                    }
                    if (ret)
                        dbConnect.Commit();
                    else
                        dbConnect.RollBack();
                }
                catch(Exception e)
                {
                    ret = false;
                    dbConnect.RollBack();
                }
            }
            return ret;
        }

        private static bool 予算作成処理(DBConnect dbConnect, MsUser loginUser, MsVessel vessel)
        {
            bool ret = true;

            // 直近の BG_YOSAN_HEAD を取得.
            BgYosanHead yosanHead = BgYosanHead.GetRecord_直近(dbConnect, loginUser);

            for (int i = 0; i < 予実.GetYearRange(yosanHead.YosanSbtID); i++)
            {
                // KadouVesselを新規作成
                {
                    BgKadouVessel kv = new BgKadouVessel();

                    kv.YosanHeadID = yosanHead.YosanHeadID;
                    kv.MsVesselID = vessel.MsVesselID;
                    kv.Year = yosanHead.Year + i;
                    kv.KadouStartDate = new DateTime(yosanHead.Year + i, 4, 1, 0, 0, 0);
                    kv.KadouEndDate = new DateTime(yosanHead.Year + i + 1, 3, 31, 12, 0, 0);
                    kv.RenewDate = DateTime.Now;
                    kv.RenewUserID = loginUser.MsUserID;

                    kv.InsertRecord(dbConnect, loginUser);
                }

                // 船毎の予算情報（BG_VESSEL_YOSAN）を作成（Insert）する

                BgVesselYosan vy = 予実.BgVesselYosan_InsertRecord(loginUser, dbConnect, yosanHead.Year + i, yosanHead.YosanHeadID, vessel.MsVesselID);
                int vesselYosanID = Sequences.GetBgVesselYosanId(dbConnect, loginUser);

                // 予算項目（BG_YOSAN_ITEM）
                予実.BgYosanItem_InsertRecords(dbConnect, loginUser, yosanHead.YosanHeadID, yosanHead, i, vy, vesselYosanID);

                // 運航費予算詳細（BG_UNKOUHI）
                予実.BgUnkouhi_InsertRecord_新規(dbConnect, loginUser, vesselYosanID, vy.Year.ToString());
            }

            return ret;
        }

        private static bool 予算更新処理(DBConnect dbConnect, MsUser loginUser, MsVessel vessel)
        {
            bool ret = true;

            // 直近の BG_YOSAN_HEAD を取得.
            BgYosanHead yosanHead = BgYosanHead.GetRecord_直近(dbConnect, loginUser);

            // 稼働船情報の確認　⇒　あれば、予算情報が一度は作られているので、何もしない
            if (CheckKadouVessel(dbConnect, loginUser, yosanHead.YosanHeadID, vessel.MsVesselID))
                return ret;

            // 予算情報作成
            return 予算作成処理(dbConnect, loginUser, vessel);
        }

        private static bool CheckKadouVessel(DBConnect dbConnect, MsUser loginUser, int yosanHeadID, int msVesselID)
        {
            List<BgKadouVessel> kadouVessels = BgKadouVessel.GetRecordsByYosanHeadID(dbConnect, loginUser, yosanHeadID);
            var checkList = from kvs in kadouVessels
                            where kvs.MsVesselID == msVesselID
                            select kvs;
            if (checkList.Count<BgKadouVessel>() > 0)
                return true;
            else
                return false;

            //bool existsKadouVessel = false;
            //foreach (BgKadouVessel kv in kadouVessels)
            //{
            //    if (kv.MsVesselID == msVesselID)
            //    {
            //        existsKadouVessel = true;
            //        break;
            //    }
            //}
            //return existsKadouVessel;
        }
    }
}
