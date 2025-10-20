using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using ORMapping;
using NBaseData.DS;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Configuration;

namespace NBaseData.BLC
{
    public partial class 予実
    {
        // 年予算を生成する年数.
        private static readonly int YEAR_RANGE_当初予算 = 20;
        //private static readonly int YEAR_RANGE_見直し予算 = 2;
        private static readonly int YEAR_RANGE_見直し予算 = 20;

        // 月予算を生成する年数.
        private static readonly int MONTH_RANGE_当初予算 = 1;
        private static readonly int MONTH_RANGE_見直し予算 = 1;


        public static int GetYearRange(int yosanSbtId)
        {
            if (yosanSbtId == (int)BgYosanSbt.BgYosanSbtEnum.当初)
            {
                return YEAR_RANGE_当初予算;
            }
            else if (yosanSbtId == (int)BgYosanSbt.BgYosanSbtEnum.見直し)
            {
                return YEAR_RANGE_見直し予算;
            }

            return 0;
        }


        public static int GetMonthRange(int yosanSbtId)
        {
            if (yosanSbtId == (int)BgYosanSbt.BgYosanSbtEnum.当初)
            {
                return MONTH_RANGE_当初予算;
            }
            else if (yosanSbtId == (int)BgYosanSbt.BgYosanSbtEnum.見直し)
            {
                return MONTH_RANGE_見直し予算;
            }

            return 0;
        }


        /// <summary>
        /// 当初予算
        ///   BgVesselYosan.COUNT    20(MS_VESSEL) * 20(20年) = 400
        ///   BgYosanItem.COUNT      20(MS_VESSEL) * 78(MS_HIMOKU) * 32(20年 + 初年度12ヶ月) = 49920
        /// 見直し予算
        ///   BgVesselYosan.COUNT    20(MS_VESSEL) * 2(2年) = 40
        ///   BgYosanItem.COUNT      20(MS_VESSEL) * 78(MS_HIMOKU) * 14(2年 + 初年度12ヶ月) = 21840
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="yosanHead"></param>
        /// <param name="kadouVessels"></param>
        /// <param name="yearRange"></param>
        /// <param name="monthRange"></param>
        /// <returns></returns>
        #region ORG
        //public static bool BLC_予算作成(MsUser loginUser, BgYosanHead yosanHead, List<BgKadouVessel> kadouVessels)
        //{
        //    using (DBConnect dbConnect = new DBConnect())
        //    {
        //        dbConnect.BeginTransaction();

        //        try
        //        {
        //            // 直近の BG_YOSAN_HEAD を取得.
        //            BgYosanHead lastYosanHead_当初 = BgYosanHead.GetRecord_直近(dbConnect, loginUser, (int)BgYosanSbt.BgYosanSbtEnum.当初);
        //            BgYosanHead lastYosanHead_見直し = BgYosanHead.GetRecord_直近(dbConnect, loginUser, (int)BgYosanSbt.BgYosanSbtEnum.見直し);

        //            // Insert BG_YOSAN_HEAD
        //            yosanHead.InsertRecord(dbConnect, loginUser);
        //            int yosanHeadID = Sequences.GetBgYosanHeadId(dbConnect, loginUser);

        //            // Insert BG_VESSEL_YOSAN
        //            List<MsVessel> vessels = MsVessel.GetRecordsByYojitsuEnabled(dbConnect, loginUser);

        //            foreach (MsVessel v in vessels)
        //            {
        //                for (int i = 0; i < GetYearRange(yosanHead.YosanSbtID); i++)
        //                {
        //                    BgVesselYosan vy = BgVesselYosan_InsertRecord(loginUser, dbConnect, yosanHead.Year + i, yosanHeadID, v.MsVesselID);

        //                    int vesselYosanID = Sequences.GetBgVesselYosanId(dbConnect, loginUser);

        //                    if (yosanHead.YosanSbtID == (int)BgYosanSbt.BgYosanSbtEnum.当初)
        //                    {
        //                        BgYosanItem_InsertRecords_新規(loginUser, GetMonthRange(yosanHead.YosanSbtID), dbConnect, i, vy, vesselYosanID);

        //                        // 運航費、修繕費について前年度予算をコピー.
        //                        int lastYosanHeadId = -1;

        //                        // 初年度は昨年度見直し予算から.
        //                        if (i == 0 && lastYosanHead_見直し != null)
        //                        {
        //                            lastYosanHeadId = lastYosanHead_見直し.YosanHeadID;
        //                        }
        //                        // 次年度以降は昨年度当初予算から.
        //                        else if (lastYosanHead_当初 != null)
        //                        {
        //                            lastYosanHeadId = lastYosanHead_当初.YosanHeadID;
        //                        }

        //                        if (i < GetYearRange(yosanHead.YosanSbtID) - 1 && BgUnkouhi.ExistsLastRecord(dbConnect, loginUser, lastYosanHeadId, vy.MsVesselID, vy.Year))
        //                        {
        //                            BgUnkouhi.InsertRecords_コピー(dbConnect, loginUser, vesselYosanID, lastYosanHeadId, vy.MsVesselID, vy.Year);
        //                        }
        //                        else
        //                        {
        //                            BgUnkouhi_InsertRecord_新規(dbConnect, loginUser, vesselYosanID, vy.Year.ToString());
        //                        }

        //                        BgUchiwakeYosanItem.InsertRecords_コピー(dbConnect, loginUser, vesselYosanID, lastYosanHeadId, vy.MsVesselID, vy.Year);
        //                    }
        //                    else
        //                    {
        //                        if (BgYosanItem.ExistsLastRecords(dbConnect, loginUser, lastYosanHead_当初.YosanHeadID, vy.MsVesselID, vy.Year))
        //                        {
        //                            BgYosanItem.InsertRecords_コピー(dbConnect, loginUser, vesselYosanID, lastYosanHead_当初.YosanHeadID, vy.MsVesselID, vy.Year);
        //                        }
        //                        else
        //                        {
        //                            BgYosanItem_InsertRecords_新規(loginUser, GetMonthRange(yosanHead.YosanSbtID), dbConnect, i, vy, vesselYosanID);
        //                        }

        //                        BgUnkouhi.InsertRecords_コピー(dbConnect, loginUser, vesselYosanID, lastYosanHead_当初.YosanHeadID, vy.MsVesselID, vy.Year);
        //                        BgUchiwakeYosanItem.InsertRecords_コピー(dbConnect, loginUser, vesselYosanID, lastYosanHead_当初.YosanHeadID, vy.MsVesselID, vy.Year);
        //                    }
        //                }

        //                if (yosanHead.YosanSbtID == (int)BgYosanSbt.BgYosanSbtEnum.見直し)
        //                {
        //                    // Insert BG_YOSAN_BIKOU
        //                    BgYosanBikou.InsertRecords_コピー(dbConnect, loginUser, lastYosanHead_当初.YosanHeadID, yosanHeadID,
        //                                                              v.MsVesselID);
        //                    // Insert BG_YOSAN_EXCEL
        //                    BgYosanExcel.InsertRecords_コピー(dbConnect, loginUser, lastYosanHead_当初.YosanHeadID, yosanHeadID,
        //                                                              v.MsVesselID);
        //                }
        //            }

        //            BgNrkKanryouAndBgYosanMemo_InsertRecords_新規(loginUser, dbConnect, yosanHeadID);

        //            BgRate_InsertRecords(loginUser, dbConnect, yosanHead, yosanHeadID, lastYosanHead_当初);

        //            // Insert BG_KADOU_VESSEL
        //            foreach (BgKadouVessel kv in kadouVessels)
        //            {
        //                kv.YosanHeadID = yosanHeadID;
        //                kv.RenewDate = DateTime.Now;
        //                kv.RenewUserID = loginUser.MsUserID;

        //                kv.InsertRecord(dbConnect, loginUser);
        //            }

        //            前Rev作成後にYojitsuEnabledした船のKadouVesselを新規作成(loginUser, yosanHead, dbConnect, yosanHeadID, vessels);

        //            dbConnect.Commit();
        //            return true;
        //        }
        //        catch (Exception e)
        //        {
        //            dbConnect.RollBack();
        //            return false;
        //        }
        //    }
        //}
        #endregion
        public static bool BLC_予算作成(MsUser loginUser, BgYosanHead yosanHead, List<BgKadouVessel> kadouVessels)
        {
            return inner_予算作成(loginUser, yosanHead.YosanSbtID, yosanHead, kadouVessels);
        }

        #region ORG
        //public static bool BLC_予算Revアップ(MsUser loginUser, BgYosanHead yosanHead)
        //{
        //    using (DBConnect dbConnect = new DBConnect())
        //    {
        //        dbConnect.BeginTransaction();

        //        try
        //        {
        //            // 直近の BG_YOSAN_HEAD を取得.
        //            BgYosanHead lastYosanHead = BgYosanHead.GetRecords(dbConnect, loginUser)[0];

        //            // Insert BG_YOSAN_HEAD
        //            yosanHead.ZenteiJouken = lastYosanHead.ZenteiJouken;

        //            yosanHead.InsertRecord(dbConnect, loginUser);
        //            int yosanHeadID = Sequences.GetBgYosanHeadId(dbConnect, loginUser);

        //            // Insert BG_VESSEL_YOSAN
        //            List<MsVessel> vessels = MsVessel.GetRecordsByYojitsuEnabled(dbConnect, loginUser);

        //            foreach (MsVessel v in vessels)
        //            {
        //                for (int i = 0; i < GetYearRange(yosanHead.YosanSbtID); i++)
        //                {
        //                    BgVesselYosan vy = BgVesselYosan_InsertRecord(loginUser, dbConnect, yosanHead.Year + i, yosanHeadID, v.MsVesselID);

        //                    int vesselYosanID = Sequences.GetBgVesselYosanId(dbConnect, loginUser);

        //                    if (!BgYosanItem.ExistsLastRecords(dbConnect, loginUser, lastYosanHead.YosanHeadID, vy.MsVesselID, vy.Year))
        //                    {
        //                        BgYosanItem_InsertRecords_新規(loginUser, GetMonthRange(yosanHead.YosanSbtID), dbConnect, i, vy, vesselYosanID);
        //                        BgUnkouhi_InsertRecord_新規(dbConnect, loginUser, vesselYosanID, vy.Year.ToString());
        //                    }
        //                    else
        //                    {
        //                        BgYosanItem.InsertRecords_コピー(dbConnect, loginUser, vesselYosanID, lastYosanHead.YosanHeadID, vy.MsVesselID, vy.Year);
        //                        BgUnkouhi.InsertRecords_コピー(dbConnect, loginUser, vesselYosanID, lastYosanHead.YosanHeadID, vy.MsVesselID, vy.Year);
        //                        BgUchiwakeYosanItem.InsertRecords_コピー(dbConnect, loginUser, vesselYosanID, lastYosanHead.YosanHeadID, vy.MsVesselID, vy.Year);
        //                    }
        //                }

        //                // Insert BG_YOSAN_BIKOU
        //                BgYosanBikou.InsertRecords_コピー(dbConnect, loginUser, lastYosanHead.YosanHeadID, yosanHeadID,
        //                                                          v.MsVesselID);

        //                // Insert BG_YOSAN_EXCEL
        //                BgYosanExcel.InsertRecords_コピー(dbConnect, loginUser, lastYosanHead.YosanHeadID, yosanHeadID,
        //                                                          v.MsVesselID);
        //            }

        //            BgNrkKanryouAndBgYosanMemo_InsertRecords_新規(loginUser, dbConnect, yosanHeadID);

        //            BgRate.InsertRecords_コピー(dbConnect, loginUser, yosanHeadID, lastYosanHead.YosanHeadID);
        //            BgKadouVessel.InsertRecords_コピー(dbConnect, loginUser, yosanHeadID, lastYosanHead.YosanHeadID);

        //            前Rev作成後にYojitsuEnabledした船のKadouVesselを新規作成(loginUser, yosanHead, dbConnect, yosanHeadID, vessels);

        //            dbConnect.Commit();
        //            return true;
        //        }
        //        catch (Exception e)
        //        {
        //            dbConnect.RollBack();
        //            return false;
        //        }
        //    }
        //}
        #endregion
        public static bool BLC_予算Revアップ(MsUser loginUser, BgYosanHead yosanHead)
        {
            // 直近の BG_YOSAN_HEAD を取得.
            BgYosanHead lastYosanHead = BgYosanHead.GetRecord_直近(loginUser);
            yosanHead.ZenteiJouken = lastYosanHead.ZenteiJouken;

            return inner_予算作成(loginUser, (int)BgYosanSbt.BgYosanSbtEnum.RevUp, yosanHead, null);
        }

        public static bool inner_予算作成(MsUser loginUser, int yosanSbt, BgYosanHead yosanHead, List<BgKadouVessel> kadouVessels)
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    // 直近の BG_YOSAN_HEAD を取得.
                    BgYosanHead lastYosanHead = BgYosanHead.GetRecord_直近(dbConnect, loginUser);

                    // 予算ヘッダー情報（BG_YOSAN_HEAD）を作成（Insert）する
                    yosanHead.InsertRecord(dbConnect, loginUser);
                    int yosanHeadID = Sequences.GetBgYosanHeadId(dbConnect, loginUser);

 
                    // 予実の対象となる船を取得
                    // 船マスタ（MS_VESSEL）の"予実"がチェックされているもの
                    List<MsVessel> vessels = MsVessel.GetRecordsByYojitsuEnabled(dbConnect, loginUser);

                    foreach (MsVessel v in vessels)
                    {
                        for (int i = 0; i < GetYearRange(yosanHead.YosanSbtID); i++)
                        {
                            // 船毎の予算情報（BG_VESSEL_YOSAN）を作成（Insert）する
                            BgVesselYosan vy = BgVesselYosan_InsertRecord(loginUser, dbConnect, yosanHead.Year + i, yosanHeadID, v.MsVesselID);
                            int vesselYosanID = Sequences.GetBgVesselYosanId(dbConnect, loginUser);

                            // 直近の予算をコピーする
                            // 直近の予算が無い場合、新規に作成する

                            // 予算項目（BG_YOSAN_ITEM）
                            //if (BgYosanItem.ExistsLastRecords(dbConnect, loginUser, lastYosanHead.YosanHeadID, vy.MsVesselID, vy.Year))
                            //{
                            //    BgYosanItem.InsertRecords_コピー(dbConnect, loginUser, vesselYosanID, lastYosanHead.YosanHeadID, vy.MsVesselID, vy.Year);
                            //}
                            //else
                            //{
                            //    BgYosanItem_InsertRecords_新規(loginUser, GetMonthRange(yosanHead.YosanSbtID), dbConnect, i, vy, vesselYosanID);
                            //}
                            BgYosanItem_InsertRecords(dbConnect, loginUser, lastYosanHead.YosanHeadID, yosanHead, i, vy, vesselYosanID);

                            // 運航費予算詳細（BG_UNKOUHI）
                            if (BgUnkouhi.ExistsLastRecord(dbConnect, loginUser, lastYosanHead.YosanHeadID, vy.MsVesselID, vy.Year))
                            {
                                BgUnkouhi.InsertRecords_コピー(dbConnect, loginUser, vesselYosanID, lastYosanHead.YosanHeadID, vy.MsVesselID, vy.Year);
                            }
                            else
                            {
                                BgUnkouhi_InsertRecord_新規(dbConnect, loginUser, vesselYosanID, vy.Year.ToString());
                            }

                            // 修繕費予算詳細（BG_UCHIWAKE_YOSAN）
                            if (BgUnkouhi.ExistsLastRecord(dbConnect, loginUser, lastYosanHead.YosanHeadID, vy.MsVesselID, vy.Year))
                            {
                                BgUchiwakeYosanItem.InsertRecords_コピー(dbConnect, loginUser, vesselYosanID, lastYosanHead.YosanHeadID, vy.MsVesselID, vy.Year);
                            }
                            // 値のあるところだけで良いので、無い場合に、わざわざ空レコードは作成しない
                            //else
                            //{
                            //    BgUchiwakeYosanItem_InsertRecord_新規(dbConnect, loginUser, vesselYosanID, vy.Year.ToString());
                            //}
                        }

                        // 当初予算作成の場合には、わざわざ空レコードは作成しない
                        if (yosanSbt != (int)BgYosanSbt.BgYosanSbtEnum.当初)
                        {
                            // Insert BG_YOSAN_BIKOU
                            BgYosanBikou.InsertRecords_コピー(dbConnect, loginUser, lastYosanHead.YosanHeadID, yosanHeadID, v.MsVesselID);
                            // Insert BG_YOSAN_EXCEL
                            BgYosanExcel.InsertRecords_コピー(dbConnect, loginUser, lastYosanHead.YosanHeadID, yosanHeadID, v.MsVesselID);
                        }
                    }

                    BgHankanhi.InsertRecords_コピー(dbConnect, loginUser, yosanHeadID, lastYosanHead.YosanHeadID, yosanHead.Year);

                    BgNrkKanryouAndBgYosanMemo_InsertRecords_新規(loginUser, dbConnect, yosanHeadID);

                    yosanHead.YosanHeadID = yosanHeadID;
                    BgRate_InsertRecords_コピー(loginUser, dbConnect, yosanHead, lastYosanHead.YosanHeadID);

                    if (kadouVessels != null)
                    {
                        // Insert BG_KADOU_VESSEL
                        foreach (BgKadouVessel kv in kadouVessels)
                        {
                            kv.YosanHeadID = yosanHeadID;
                            kv.RenewDate = DateTime.Now;
                            kv.RenewUserID = loginUser.MsUserID;

                            kv.InsertRecord(dbConnect, loginUser);
                        }
                    }
                    else
                    {
                        BgKadouVessel.InsertRecords_コピー(dbConnect, loginUser, yosanHeadID, lastYosanHead.YosanHeadID);
                    }

                    前Rev作成後にYojitsuEnabledした船のKadouVesselを新規作成(loginUser, yosanHead, dbConnect, yosanHeadID, vessels);

                    dbConnect.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    dbConnect.RollBack();
                    return false;
                }
            }
        }



        //private static void BgYosanItem_InsertRecords_新規(MsUser loginUser, int monthRange, DBConnect dbConnect, int i, BgVesselYosan vy, int vesselYosanID)
        //{
        //    // 年次予算
        //    string nengetsu = vy.Year.ToString();

        //    BgYosanItem.InsertRecords_新規(dbConnect, loginUser, vesselYosanID, nengetsu);

        //    // 月次予算
        //    if (i < monthRange)
        //    {
        //        for (int k = 0; k < Constants.PADDING_MONTHS.Length; k++)
        //        {
        //            if (k < 9)
        //            {
        //                nengetsu = vy.Year + Constants.PADDING_MONTHS[k];
        //            }
        //            else
        //            {
        //                nengetsu = (vy.Year + 1) + Constants.PADDING_MONTHS[k];
        //            }

        //            BgYosanItem.InsertRecords_新規(dbConnect, loginUser, vesselYosanID, nengetsu);
        //        }
        //    }
        //}

        //private static void BgYosanItem_InsertRecords(DBConnect dbConnect, MsUser loginUser, int lastYosanHeadID, BgYosanHead yosanHead, int i, BgVesselYosan vy, int vesselYosanID)
        public static void BgYosanItem_InsertRecords(DBConnect dbConnect, MsUser loginUser, int lastYosanHeadID, BgYosanHead yosanHead, int i, BgVesselYosan vy, int vesselYosanID)
        {
            // 年次予算
            string nengetsu = vy.Year.ToString();
            if (BgYosanItem.ExistsLastRecords(dbConnect, loginUser, lastYosanHeadID, vy.MsVesselID, vy.Year))
            {
                BgYosanItem.InsertRecords_コピー(dbConnect, loginUser, vesselYosanID, lastYosanHeadID, vy.MsVesselID, vy.Year);
            }
            else
            {
                BgYosanItem.InsertRecords_新規(dbConnect, loginUser, vesselYosanID, nengetsu);
            }

            // 月次予算
            if (yosanHead.YosanSbtID == (int)BgYosanSbt.BgYosanSbtEnum.当初 && 
                yosanHead.Revision == 0 && 
                i < GetMonthRange(yosanHead.YosanSbtID))
            {
                for (int k = 0; k < Constants.PADDING_MONTHS.Length; k++)
                {
                    if (k < 9)
                    {
                        nengetsu = vy.Year + Constants.PADDING_MONTHS[k];
                    }
                    else
                    {
                        nengetsu = (vy.Year + 1) + Constants.PADDING_MONTHS[k];
                    }
                    BgYosanItem.InsertRecords_新規(dbConnect, loginUser, vesselYosanID, nengetsu);
                }
            }
        }


        private static void 前Rev作成後にYojitsuEnabledした船のKadouVesselを新規作成(MsUser loginUser, BgYosanHead yosanHead, DBConnect dbConnect, int yosanHeadID, List<MsVessel> vessels)
        {
            List<BgKadouVessel> kadouVessels = BgKadouVessel.GetRecordsByYosanHeadID(dbConnect, loginUser, yosanHeadID);

            foreach (MsVessel v in vessels)
            {
                bool existsKadouVessel = false;

                foreach (BgKadouVessel kv in kadouVessels)
                {
                    if (kv.MsVesselID == v.MsVesselID)
                    {
                        existsKadouVessel = true;
                        break;
                    }
                }

                if (!existsKadouVessel)
                {
                    for (int i = 0; i < GetYearRange(yosanHead.YosanSbtID); i++)
                    {
                        BgKadouVessel kv = new BgKadouVessel();

                        kv.YosanHeadID = yosanHeadID;
                        kv.MsVesselID = v.MsVesselID;
                        kv.Year = yosanHead.Year + i;
                        kv.KadouStartDate = new DateTime(yosanHead.Year + i, 4, 1, 0, 0, 0);
                        kv.KadouEndDate = new DateTime(yosanHead.Year + i + 1, 3, 31, 12, 0, 0);
                        kv.RenewDate = DateTime.Now;
                        kv.RenewUserID = loginUser.MsUserID;

                        kv.InsertRecord(dbConnect, loginUser);
                    }
                }
            }
        }

        #region ORG
        //private static void BgRate_InsertRecords(MsUser loginUser, DBConnect dbConnect, BgYosanHead yosanHead, int yosanHeadId, BgYosanHead lastYosanHead)
        //{
        //    if (yosanHead.YosanSbtID == (int)BgYosanSbt.BgYosanSbtEnum.当初)
        //    {
        //        Dictionary<int, BgRate> lastRateDic = CreateDictionary_BgRate_直近(dbConnect, loginUser);

        //        for (int i = 0; i < GetYearRange(yosanHead.YosanSbtID); i++)
        //        {
        //            // Insert BG_RATE
        //            BgRate rate = new BgRate();

        //            rate.YosanHeadID = yosanHeadId;
        //            rate.Year = yosanHead.Year + i;
        //            rate.RenewDate = DateTime.Now;
        //            rate.RenewUserID = loginUser.MsUserID;

        //            if (lastRateDic.ContainsKey(yosanHead.Year + i))
        //            {
        //                rate.KamikiRate = lastRateDic[yosanHead.Year + i].KamikiRate;
        //                rate.ShimokiRate = lastRateDic[yosanHead.Year + i].ShimokiRate;
        //            }
        //            else
        //            {
        //                rate.KamikiRate = 0;
        //                rate.ShimokiRate = 0;
        //            }

        //            rate.InsertRecord(dbConnect, loginUser);
        //        }
        //    }
        //    else if (yosanHead.YosanSbtID == (int)BgYosanSbt.BgYosanSbtEnum.見直し)
        //    {
        //        BgRate.InsertRecords_見直し(dbConnect, loginUser, yosanHeadId, lastYosanHead.YosanHeadID, yosanHead.Year + GetYearRange(yosanHead.YosanSbtID) - 1);
        //    }
        //}
        #endregion
        private static void BgRate_InsertRecords_コピー(MsUser loginUser, DBConnect dbConnect, BgYosanHead yosanHead, int lastYosanHeadId)
        {
            // 直近予算のレート情報を取得
            Dictionary<int, BgRate> lastRateDic = CreateDictionary_BgRate_直近(dbConnect, loginUser, lastYosanHeadId);

            int startYear = yosanHead.Year;
            int range =  GetYearRange(yosanHead.YosanSbtID);
            for (int i = 0; i < range; i++)
            {
                BgRate rate = new BgRate();

                rate.YosanHeadID = yosanHead.YosanHeadID;
                rate.Year = startYear + i;
                rate.RenewDate = DateTime.Now;
                rate.RenewUserID = loginUser.MsUserID;

                if (lastRateDic.ContainsKey(startYear + i))
                {
                    rate.KamikiRate = lastRateDic[startYear + i].KamikiRate;
                    rate.ShimokiRate = lastRateDic[startYear + i].ShimokiRate;
                }
                else
                {
                    rate.KamikiRate = 0;
                    rate.ShimokiRate = 0;
                }

                rate.InsertRecord(dbConnect, loginUser);
            }
        }


        public static Dictionary<int, BgRate> CreateDictionary_BgRate_直近(DBConnect dbConnect, MsUser loginUser, int lastYosanHeadId)
        {
            Dictionary<int, BgRate> dic = new Dictionary<int, BgRate>();
            
            List<BgRate> rates = BgRate.GetRecordsByYosanHeadID(loginUser, lastYosanHeadId);

            foreach (BgRate r in rates)
            {
                dic[r.Year] = r;
            }

            return dic;
        }

        //private static bool BgUnkouhi_InsertRecord_新規(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int vesselYosanId, string nengetsu)
        public static bool BgUnkouhi_InsertRecord_新規(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int vesselYosanId, string nengetsu)
        {
            BgUnkouhi u = new BgUnkouhi();

            u.VesselYosanID = vesselYosanId;
            u.ObjectData = BlobUnkouhiList.ToBytes(new BlobUnkouhiList());
            u.RenewDate = DateTime.Now;
            u.RenewUserID = loginUser.MsUserID;

            return u.InsertRecord(dbConnect, loginUser);
        }

        private static bool BgUchiwakeYosanItem_InsertRecord_新規(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int vesselYosanId, string nengetsu)
        {
            BgUchiwakeYosanItem u = new BgUchiwakeYosanItem();

            u.VesselYosanID = vesselYosanId;
            u.RenewDate = DateTime.Now;
            u.RenewUserID = loginUser.MsUserID;

            return u.InsertRecord(dbConnect, loginUser);
        }

        
        //private static BgVesselYosan BgVesselYosan_InsertRecord(MsUser loginUser, DBConnect dbConnect, int year, int yosanHeadId, int msVesselId)
        public static BgVesselYosan BgVesselYosan_InsertRecord(MsUser loginUser, DBConnect dbConnect, int year, int yosanHeadId, int msVesselId)
        {
            BgVesselYosan vy = new BgVesselYosan();

            vy.Year = year;
            vy.YosanHeadID = yosanHeadId;
            vy.MsVesselID = msVesselId;
            vy.RenewDate = DateTime.Now;
            vy.RenewUserID = loginUser.MsUserID;

            vy.InsertRecord(dbConnect, loginUser);

            return vy;
        }


        private static void BgNrkKanryouAndBgYosanMemo_InsertRecords_新規(MsUser loginUser, DBConnect dbConnect, int yosanHeadID)
        {
            List<MsBumon> bumons = MsBumon.GetRecords(dbConnect, loginUser);

            foreach (MsBumon b in bumons)
            {
                // Insert BG_NRK_KANRYOU
                BgNrkKanryou kanryou = new BgNrkKanryou();
                kanryou.YosanHeadID = yosanHeadID;
                kanryou.MsBumonID = b.MsBumonID;
                kanryou.RenewDate = DateTime.Now;
                kanryou.RenewUserID = loginUser.MsUserID;

                kanryou.InsertRecord(dbConnect, loginUser);

                // Insert BG_YOSAN_MEMO
                BgYosanMemo memo = new BgYosanMemo();
                memo.YosanHeadID = yosanHeadID;
                memo.MsBumonID = b.MsBumonID;
                memo.RenewDate = DateTime.Now;
                memo.RenewUserID = loginUser.MsUserID;

                memo.InsertRecord(dbConnect, loginUser);
            }
        }


        public static bool BLC_予算保存(MsUser loginUser, List<BgYosanItem> yosanItems, BgNrkKanryou nrkKanryou)
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    foreach (BgYosanItem item in yosanItems)
                    {
                        item.UpdateRecord(dbConnect, loginUser);
                    }

                    nrkKanryou.UpdateRecord(dbConnect, loginUser);

                    dbConnect.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    dbConnect.RollBack();
                    return false;
                }
            }
        }


        public static bool BLC_販管費保存(MsUser loginUser, int year, BgYosanHead yosanHead, int eigyo, int kanri, int nenkan, int keiei,
                                          List<int> msVesselIds, List<decimal> amounts)
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    //割りかけもとの保存をする
                    割り掛けの保存(dbConnect, loginUser, year, yosanHead,
                        eigyo, kanri, nenkan, keiei);

                    dbConnect.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    dbConnect.RollBack();
                    return false;
                }
            }
        }


        /// <summary>
        /// 割り掛けの保存処理
        /// 引数：年、関連頭、営業割り掛け、管理割り掛け、販菅、経営指導料
        /// 返り値：成功したか？
        /// </summary>
        /// <returns></returns>
        private static bool 割り掛けの保存(DBConnect dbConnect, MsUser loginUser, int year, BgYosanHead head, int eigyo, int kanri, int nekan, int keiei)
        {
            BgHankanhi hankanhi = null;

            //今回のデータを取得する
            hankanhi = BgHankanhi.GetRecordByYosanHeadIDYear(dbConnect, loginUser, head.YosanHeadID, year);


            //無い時は新たにinsertする
            if (hankanhi == null)
            {
                hankanhi = new BgHankanhi();

                #region 挿入データ作成
                hankanhi.YosanHeadID = head.YosanHeadID;
                hankanhi.Year = year;

                hankanhi.EigyoKiso = eigyo;
                hankanhi.KanriKiso = kanri;
                hankanhi.NendoHankanhi = nekan;
                hankanhi.Keieishidouryou = keiei;
                hankanhi.RenewDate = DateTime.Now;
                hankanhi.RenewUserID = loginUser.MsUserID;
                hankanhi.Ts = "0";
                #endregion

                hankanhi.InsertRecord(dbConnect, loginUser);

            }
            //あるならUpDateを掛ける
            else
            {
                #region 更新データ作成
                hankanhi.YosanHeadID = head.YosanHeadID;
                hankanhi.Year = year;

                hankanhi.EigyoKiso = eigyo;
                hankanhi.KanriKiso = kanri;
                hankanhi.NendoHankanhi = nekan;
                hankanhi.Keieishidouryou = keiei;
                #endregion

                hankanhi.UpdateRecord(dbConnect, loginUser);
            }

            return true;
        }


        public static bool BLC_運航費保存(MsUser loginUser, int yosanHeadId, int msVesselId, int year, BgUnkouhi unkouhi, bool doCopy)
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    if (!doCopy)
                    {
                        unkouhi.UpdateRecord(dbConnect, loginUser);
                    }
                    else
                    {
                        BgUnkouhi.UpdateRecords_コピー(dbConnect, loginUser, yosanHeadId, msVesselId, year, unkouhi.ObjectData);
                    }

                    dbConnect.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    dbConnect.RollBack();
                    return false;
                }
            }
        }


        public static bool BLC_修繕費保存(MsUser loginUser, List<BgUchiwakeYosanItem> uchiwakeYosanItems, BgYosanBikou yosanBikou)
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    BgUchiwakeYosanItem.InsertOrUpdate(dbConnect, loginUser, uchiwakeYosanItems);

                    if (yosanBikou != null)
                    {
                        BgYosanBikou.InsertOrUpdate(dbConnect, loginUser, yosanBikou);
                    }

                    dbConnect.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    dbConnect.RollBack();
                    return false;
                }
            }
        }


        public static bool BLC_実績取込(MsUser loginUser)
        {
            Process p = null;

            try
            {
                string batchFilePath = System.Configuration.ConfigurationManager.AppSettings["予実実績取込バッチPath"];

                p = Process.Start(batchFilePath);
                p.WaitForExit();
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                if (p != null)
                {
                    p.Close();
                    p.Dispose();
                }
            }

            return true;
        }
    }
}
