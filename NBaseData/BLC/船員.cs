using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping;
using NBaseData.DAC;
using NBaseUtil;
using System.Configuration;
using NBaseData.DS;

namespace NBaseData.BLC
{
    public class 船員
    {
        //public static string 船員_所属_内航 = "001001";
        //public static string 船員_所属_旅客 = "001002";
        //public static string 船員_所属_陸勤 = "001003";

        public static string 船員_連絡先_一次 = "111002";
        public static string 船員_連絡先_二次 = "111003";



        public static int 船員_年度休暇日数;
        public static string 船員_自社名;


        static 船員()
        {
            int.TryParse(System.Configuration.ConfigurationManager.AppSettings["船員_年度休暇日数"], out 船員_年度休暇日数);

            try
            {
                船員_自社名 = System.Configuration.ConfigurationManager.AppSettings["船員_自社名"];
            }
            catch
            {
                船員_自社名 = "不明";
            }
        }


        public static int BLC_船員登録(MsUser loginUser, SeninTableCache seninTableCache, 
                                       MsSenin senin,
                                       MsSeninAddress seninAddress,
                                       List<SiCard> cards, 
                                       List<SiMenjou> menjous,
                                       List<SiKazoku> kazokus,  
                                       List<SiRireki> rirekis, 
                                       List<SiShobyo> shobyos, 
                                       List<SiKenshin> kenshins,
                                       List<SiShobatsu> shobatsus)
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    //if (!MsUser_InsertOrUpdate(dbConnect, loginUser, senin))
                    //{
                    //    dbConnect.RollBack();
                    //    return -1;
                    //}

                    // Insert or Update MS_SENIN
                    //2021/07/20 m.yoshihara nullかどうか条件追加
                    //int msSeninId = MsSenin_InsertOrUpdate(dbConnect, loginUser, senin);
                    int msSeninId = 0;
                    if (cards != null || menjous != null || kazokus != null || rirekis != null || shobyos != null || kenshins != null || shobatsus != null)
                    {
                        msSeninId = senin.MsSeninID;
                    }
                    else
                    {
                        if (senin.RetireFlag == 1)
                        {
                            var orgSenin = MsSenin.GetRecord(dbConnect, loginUser, senin.MsSeninID);
                            if (orgSenin.RetireFlag != senin.RetireFlag)
                            {
                                if (menjous == null)
                                {
                                    menjous = SiMenjou.GetRecordsByMsSeninID(dbConnect, loginUser, senin.MsSeninID);
                                }
                                if (kenshins == null)
                                {
                                    kenshins = SiKenshin.GetRecordsByMsSeninID(dbConnect, loginUser, senin.MsSeninID);
                                }
                            }
                        }

                        msSeninId = MsSenin_InsertOrUpdate(dbConnect, loginUser, senin);

                        //WTM連携
                        if (System.Configuration.ConfigurationManager.AppSettings["WTMLinkageFlag"] == "1")
                        {
                            WTMLinkageProc.CreateCrewLinkFile(msSeninId, senin.FullName);
                        }
                    }

                    // 船員住所
                    if (seninAddress != null)
                    {
                        MsSeninAddress_InsertOrUpdate(dbConnect, loginUser, senin, seninAddress, msSeninId);
                    }

                    #region  Insert or Update SI_CARD
                    // 新規追加ユーザは、休暇管理のレコードを生成する.

                    if (cards != null && senin != null )
                    {
                        if (senin.IsNew())
                        {
                            SiCard_休暇管理_Insert(dbConnect, loginUser, seninTableCache, msSeninId, senin.Department, DateTime.Today, 0);
                        }

                        foreach (SiCard c in cards)
                        {
                            c.MsSeninID = msSeninId;

                            //WTM連携
                            if (System.Configuration.ConfigurationManager.AppSettings["WTMLinkageFlag"] == "1")
                            {
                                if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser, MsSiShubetsu.SiShubetsu.乗船) && c.MsVesselID > 0)
                                {
                                    //WTMLinkageProc.CreateSignOnOffLinkFile(c);

                                    // Update前のデータを確認する
                                    SiCard changeBeforeCard = SiCard.GetRecord(dbConnect, loginUser, c.SiCardID);
                                    if (changeBeforeCard != null)
                                    {
                                        // 船が変更されている場合
                                        if (c.MsVesselID != changeBeforeCard.MsVesselID)
                                        {
                                            // 変更前のWTMレコードを削除としたい
                                            changeBeforeCard.DeleteFlag = 1;
                                            WTMLinkageProc.CreateSignOnOffLinkFile(changeBeforeCard);

                                            // 変更後のWTMレコードを新規作成したいので、WTMのIDをクリアしておく
                                            c.WTMLinkageID = null;
                                        }

                                        else if (c.EndDate == DateTime.MinValue && changeBeforeCard.EndDate != DateTime.MinValue)
                                        {
                                            // 乗船データで、下船日がクリアされた場合

                                            // 変更前のWTMレコードを削除としたい
                                            changeBeforeCard.DeleteFlag = 1;
                                            WTMLinkageProc.CreateSignOnOffLinkFile(changeBeforeCard);

                                            // 変更後のWTMレコードを新規作成したいので、WTMのIDをクリアしておく
                                            c.WTMLinkageID = null;
                                        }
                                    }

                                    // 該当 SiCard の WTM連携
                                    if (c.CardMsSiShokumeiID == 0)
                                        c.CardMsSiShokumeiID = c.SiLinkShokumeiCards[0].MsSiShokumeiID;

                                    WTMLinkageProc.CreateSignOnOffLinkFile(c);
                                }
                                else if (StringUtils.Empty(c.WTMLinkageID) == false)
                                {
                                    // 種別が乗船ではないが、WTM連携IDがある場合。。
                                    SiCard changeBeforeCard = SiCard.GetRecord(dbConnect, loginUser, c.SiCardID);
                                    if (changeBeforeCard.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser, MsSiShubetsu.SiShubetsu.乗船))
                                    {
                                        // 変更前のWTMレコードを削除としたい
                                        changeBeforeCard.DeleteFlag = 1;
                                        WTMLinkageProc.CreateSignOnOffLinkFile(changeBeforeCard);
                                    }
                                }
                            }
                            SiCard_InsertOrUpdate(dbConnect, loginUser, c);

                            foreach (SiLinkShokumeiCard link in c.SiLinkShokumeiCards)
                            {
                                link.SiCardID = c.SiCardID;
                                SiLinkShokumeiCard_InsertOrUpdate(dbConnect, loginUser, link);
                            }

                            // 2017年度改造
                            SiBoardingSchedule schedule = null;
                            if (c.EndDate == DateTime.MinValue)
                            {
                                // 乗船
                                List<SiBoardingSchedule> tmp = SiBoardingSchedule.GetRecordsByPlan(dbConnect, loginUser);
                                if (tmp.Count() > 0 && tmp.Any(obj => obj.MsSeninID == c.MsSeninID))
                                {
                                    schedule = tmp.Where(obj => obj.MsSeninID == c.MsSeninID).First();
                                }
                            }
                            else
                            {
                                // 下船
                                schedule = SiBoardingSchedule.GetRecordBySignOffSiCardID(dbConnect, loginUser, c.SiCardID);
                            }
                            if (schedule != null)
                            {
                                schedule.Status = 1;
                                schedule.RenewDate = DateTime.Now;
                                schedule.RenewUserID = loginUser.MsUserID;

                                schedule.UpdateRecord(dbConnect, loginUser);
                            }

                            // 削除された場合、このデータを交代者としているデータを更新する
                            if (c.DeleteFlag == 1)
                            {
                                SiCard.RemoveReplacement(dbConnect, loginUser, c.SiCardID);
                            }
                        }
                    }
                    #endregion

                    #region Insert or Update SI_MENJOU, PT_ALARM_INFO
                    //2021/07/20 m.yoshihara nullかどうか条件追加
                    if (menjous != null)
                    {
                        foreach (SiMenjou m in menjous)
                        {
                            m.MsSeninID = msSeninId;
                            SiMenjou_InsertOrUpdate(dbConnect, loginUser, m, senin);
                        }
                    }
                    #endregion

                    #region Insert or Update SI_KAZOKU
                    //2021/07/20 m.yoshihara nullかどうか条件追加
                    if (kazokus != null)
                    {
                        foreach (SiKazoku k in kazokus)
                        {
                            k.MsSeninID = msSeninId;
                            SiKazoku_InsertOrUpdate(dbConnect, loginUser, k);
                        }
                    }
                    #endregion

                    #region Insert or Update SI_RIREKI
                    //2021/07/20 m.yoshihara nullかどうか条件追加
                    if (rirekis != null)
                    {
                        foreach (SiRireki r in rirekis)
                        {
                            r.MsSeninID = msSeninId;
                            SiRireki_InsertOrUpdate(dbConnect, loginUser, r);
                        }
                    }
                    #endregion

                    #region Insert or Update SI_SHOBYO
                    //2021/07/20 m.yoshihara nullかどうか条件追加
                    if (shobyos != null)
                    {
                        foreach (SiShobyo s in shobyos)
                        {
                            s.MsSeninID = msSeninId;
                            SiShobyo_InsertOrUpdate(dbConnect, loginUser, s);
                        }
                    }
                    #endregion

                    #region Insert or Update SI_KENSHIN
                    //2021/07/20 m.yoshihara nullかどうか条件追加
                    if (kenshins != null)
                    {
                        foreach (SiKenshin k in kenshins)
                        {
                            k.MsSeninID = msSeninId;
                            SiKenshin_InsertOrUpdate(dbConnect, loginUser, k, senin);
                        }
                    }
                    #endregion

                    #region Insert or Update SI_SHOBATSU
                    if (shobatsus != null)
                    {
                        foreach (SiShobatsu k in shobatsus)
                        {
                            k.MsSeninID = msSeninId;
                            SiShobatsu_InsertOrUpdate(dbConnect, loginUser, k);
                        }
                    }
                    #endregion

                    dbConnect.Commit();
                    return msSeninId;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    dbConnect.RollBack();
                    return -1;
                }
            }
        }
        private static void _交代情報削除(DBConnect dbConnect, MsUser loginUser, SiCard c)
        {
            SiCard pcard = SiCard.GetRecordParent(loginUser, c.SiCardID);
            if (pcard != null)
            {
                //関係を消す
                pcard.ReplacementID = "";
                pcard.UpdateRecord(dbConnect, loginUser);
            }
        }

        private static void SiCard_休暇管理_Insert(DBConnect dbConnect, MsUser loginUser, SeninTableCache seninTableCache, int msSeninId, string depertment, DateTime day, int zanDays)
        {
            // 「休暇買上」
            CreateSiCard(loginUser, msSeninId, seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.休暇買上), 0, day).InsertRecord(dbConnect, loginUser);

            // 「本年度休暇日数」
            CreateSiCard(loginUser, msSeninId, seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.本年度休暇日数), 船員_年度休暇日数, day).InsertRecord(dbConnect, loginUser);

            // 「前年度休暇繰越日数」
            CreateSiCard(loginUser, msSeninId, seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.前年度休暇繰越日数), zanDays, day).InsertRecord(dbConnect, loginUser);
        }

        #region
        private static bool MsUser_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, MsSenin senin)
        {
            bool ret = true;

            if (senin.MsUserID == null)
            {
                MsUser user = new MsUser();
                user.MsUserID = senin.ShimeiCode;
                user.LoginID = senin.ShimeiCode;
                user.Password = senin.ShimeiCode;
                user.UserKbn = 1;
                user.UserKbn = (int)MsUser.USER_KBN.船員;
                user.Sei = senin.Sei;
                user.Mei = senin.Mei;
                user.SeiKana = senin.SeiKana;
                user.MeiKana = senin.MeiKana;
                user.Sex = senin.Sex;

                user.RenewUserID = loginUser.MsUserID;
                user.RenewDate = DateTime.Now;

                senin.MsUserID = user.MsUserID;

                if (user.InsertRecord(dbConnect, loginUser))
                {
                    MsUserBumon userBumon = new MsUserBumon();
                    userBumon.MsUserBumonID = System.Guid.NewGuid().ToString();
                    userBumon.MsUserID = user.MsUserID;
                    userBumon.MsBumonID = MsBumon.ToId(MsBumon.MsBumonIdEnum.船員部);

                    userBumon.RenewUserID = loginUser.MsUserID;
                    userBumon.RenewDate = DateTime.Now;

                    ret = userBumon.InsertRecord(dbConnect, loginUser);
                }
                else
                {
                    ret = false;
                }
            }

            return ret;



            // 更新はしない
            //else
            //{
            //    MsUser user = MsUser.GetRecordsByUserID(dbConnect, loginUser, senin.MsUserID);

            //    if (user != null)
            //    {
            //        //user.LoginID = senin.LoginID;
            //        //user.Password = senin.Password;
            //        user.Sei = senin.Sei;
            //        user.Mei = senin.Mei;
            //        user.SeiKana = senin.SeiKana;
            //        user.MeiKana = senin.MeiKana;
            //        user.Sex = senin.Sex;

            //        user.RenewUserID = loginUser.MsUserID;
            //        user.RenewDate = DateTime.Now;

            //        return user.UpdateRecord(dbConnect, loginUser);
            //    }
            //    else
            //    {
            //        return true;
            //    }
            //}
        }
        #endregion


        private static int MsSenin_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, MsSenin s)
        {
            s.RenewUserID = loginUser.MsUserID;
            s.RenewDate = DateTime.Now;

            if (s.IsNew())
            {
                if (s.InsertRecord(dbConnect, loginUser))
                {
                    return Sequences.GetMsSeninId(dbConnect, loginUser);
                }
            }
            else
            {
                if (s.UpdateRecord(dbConnect, loginUser))
                {
                    return s.MsSeninID;
                }
            }

            return -1;
        }

        private static bool MsSeninAddress_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, MsSenin s, MsSeninAddress a, int msSeninId)
        {
            a.RenewUserID = loginUser.MsUserID;
            a.RenewDate = DateTime.Now;

            if (a.IsNew())
            {
                a.MsSeninID = msSeninId;
                return a.InsertRecord(dbConnect, loginUser);
            }
            else
            {
                return a.UpdateRecord(dbConnect, loginUser);
            }
        }

        private static bool SiCard_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, SiCard c)
        {
            c.RenewUserID = loginUser.MsUserID;
            c.RenewDate = DateTime.Now;


            if (c.MsSiShubetsuShousaiID == 0)
            {
                List<MsSiShubetsuShousai> shousaiList = MsSiShubetsuShousai.GetRecords(dbConnect, loginUser);

                if (c.MsVesselID > 0)
                {
                    if (shousaiList.Any(o => o.MsVesselID == c.MsVesselID))
                    {
                        c.MsSiShubetsuShousaiID = shousaiList.Where(o => o.MsVesselID == c.MsVesselID).FirstOrDefault().MsSiShubetsuShousaiID;
                    }
                }
                else
                {
                    if (shousaiList.Any(o => o.MsSiShubetsuID == c.MsSiShubetsuID))
                    {
                        c.MsSiShubetsuShousaiID = shousaiList.Where(o => o.MsSiShubetsuID == c.MsSiShubetsuID).FirstOrDefault().MsSiShubetsuShousaiID;
                    }
                }
            }

            if (c.IsNew())
            {
                c.SiCardID = System.Guid.NewGuid().ToString();
                return c.InsertRecord(dbConnect, loginUser);
            }
            else
            {
                return c.UpdateRecord(dbConnect, loginUser);
            }
        }


        private static bool SiLinkShokumeiCard_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, SiLinkShokumeiCard link)
        {
            link.RenewUserID = loginUser.MsUserID;
            link.RenewDate = DateTime.Now;

            if (link.IsNew())
            {
                link.SiLinkShokumeiCardID = System.Guid.NewGuid().ToString();
                return link.InsertRecord(dbConnect, loginUser);
            }
            else
            {
                return link.UpdateRecord(dbConnect, loginUser);
            }
        }


        private static bool SiMenjou_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, SiMenjou m, MsSenin senin)
        {
            m.RenewUserID = loginUser.MsUserID;
            m.RenewDate = DateTime.Now;

            if (m.IsNew())
            {
                m.SiMenjouID = System.Guid.NewGuid().ToString();

                if (!m.InsertRecord(dbConnect, loginUser))
                {
                    return false;
                }
            }
            else
            {
                if (!m.UpdateRecord(dbConnect, loginUser))
                {
                    return false;
                }
            }

            if (m.AlarmInfoList != null && m.AlarmInfoList.Count() > 0)
            {
                if (!PtAlarmInfo_InsertOrUpdate(dbConnect, loginUser, m.AlarmInfoList[0], m.SiMenjouID, senin.RetireFlag))
                {
                    return false;
                }
            }


            return true;
        }


        private static bool SiKazoku_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, SiKazoku k)
        {
            k.RenewUserID = loginUser.MsUserID;
            k.RenewDate = DateTime.Now;

            if (k.IsNew())
            {
                k.SiKazokuID = System.Guid.NewGuid().ToString();
                return k.InsertRecord(dbConnect, loginUser);
            }
            else
            {
                return k.UpdateRecord(dbConnect, loginUser);
            }
        }


        private static bool SiRireki_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, SiRireki r)
        {
            r.RenewUserID = loginUser.MsUserID;
            r.RenewDate = DateTime.Now;

            if (r.IsNew())
            {
                r.SiRirekiID = System.Guid.NewGuid().ToString();
                return r.InsertRecord(dbConnect, loginUser);
            }
            else
            {
                return r.UpdateRecord(dbConnect, loginUser);
            }
        }

        private static bool SiShobyo_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, SiShobyo s)
        {
            s.RenewUserID = loginUser.MsUserID;
            s.RenewDate = DateTime.Now;

            if (s.IsNew())
            {
                s.SiShobyoID = System.Guid.NewGuid().ToString();

                if (!s.InsertRecord(dbConnect, loginUser))
                {
                    return false;
                }
            }
            else
            {
                if (!s.UpdateRecord(dbConnect, loginUser))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool SiKenshin_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, SiKenshin k, MsSenin senin)
        {
            k.RenewUserID = loginUser.MsUserID;
            k.RenewDate = DateTime.Now;

            if (k.IsNew())
            {
                k.SiKenshinID = System.Guid.NewGuid().ToString();

                if (!k.InsertRecord(dbConnect, loginUser))
                {
                    return false;
                }
            }
            else
            {
                if (!k.UpdateRecord(dbConnect, loginUser))
                {
                    return false;
                }
            }

            if (k.AlarmInfoList != null && k.AlarmInfoList.Count() > 0)
            {
                if (!PtAlarmInfo_InsertOrUpdate(dbConnect, loginUser, k.AlarmInfoList[0], k.SiKenshinID, senin.RetireFlag))
                {
                    return false;
                }
            }
            return true;
        }


        private static bool SiShobatsu_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, SiShobatsu k)
        {
            k.RenewUserID = loginUser.MsUserID;
            k.RenewDate = DateTime.Now;

            if (k.IsNew())
            {
                k.SiShobatsuID = System.Guid.NewGuid().ToString();

                if (!k.InsertRecord(dbConnect, loginUser))
                {
                    return false;
                }
            }
            else
            {
                if (!k.UpdateRecord(dbConnect, loginUser))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool BLC_新規送金(MsUser loginUser, SiSoukin soukin)
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    // Insert or Update SI_SOUKIN
                    bool ret = SiSoukin_InsertOrUpdate(dbConnect, loginUser, soukin);

                    if (!ret)
                    {
                        dbConnect.RollBack();
                        return false;
                    }

                    // 事務所更新情報
                    ret = 事務所更新情報処理.船用金送金登録(dbConnect, loginUser, soukin);
                    if (!ret)
                    {
                        dbConnect.RollBack();
                        return false;
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


        public static bool SiJunbikin_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, SiJunbikin j)
        {
            j.RenewUserID = loginUser.MsUserID;
            j.RenewDate = DateTime.Now;

            if (j.IsNew())
            {
                j.SiJunbikinID = System.Guid.NewGuid().ToString();
                return j.InsertRecord(dbConnect, loginUser);
            }
            else
            {
                return j.UpdateRecord(dbConnect, loginUser);
            }
        }


        public static bool SiSoukin_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, SiSoukin s)
        {
            s.RenewUserID = loginUser.MsUserID;
            s.RenewDate = DateTime.Now;

            if (s.IsNew())
            {
                s.SiSoukinID = System.Guid.NewGuid().ToString();

                if (!s.InsertRecord(dbConnect, loginUser))
                {
                    return false;
                }
            }
            else
            {
                if (!s.UpdateRecord(dbConnect, loginUser))
                {
                    return false;
                }
            }

            if (!PtAlarmInfo_InsertOrUpdate(dbConnect, loginUser, s.AlarmInfoList[0], s.SiSoukinID))
            {
                return false;
            }

            return true;
        }

        private static bool PtAlarmInfo_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, PtAlarmInfo alarm, string sanshoumotoId)
        {
            return PtAlarmInfo_InsertOrUpdate(dbConnect, loginUser, alarm, sanshoumotoId, 0);
        }
        private static bool PtAlarmInfo_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, PtAlarmInfo alarm, string sanshoumotoId, int deleteFlag)
        {
            if (deleteFlag == 1)
            {
                alarm.DeleteFlag = 1;
            }
            alarm.RenewUserID = loginUser.MsUserID;
            alarm.RenewDate = DateTime.Now;

            if (alarm.IsNew())
            {
                alarm.PtAlarmInfoId = System.Guid.NewGuid().ToString();
                alarm.SanshoumotoId = sanshoumotoId;

                if (!alarm.InsertRecord(dbConnect, loginUser))
                {
                    return false;
                }
            }
            else
            {
                if (!alarm.UpdateRecord(dbConnect, loginUser))
                {
                    return false;
                }
            }

            return true;
        }


        public static bool BLC_配乗表配信(MsUser loginUser, SiHaijou haijou, SeninTableCache seninTableCache)
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    // Insert SI_HAIJOU
                    bool ret = SiHaijou_InsertOrUpdate(dbConnect, loginUser, haijou);

                    if (!ret)
                    {
                        dbConnect.RollBack();
                        return false;
                    }

                    // Insert SI_HAIJOU_ITEM
                    foreach (SiHaijouItem item in haijou.SiHaijouItems)
                    {
                        item.SiHaijouID = haijou.SiHaijouID;
                        ret = SiHaijouItem_InsertOrUpdate(dbConnect, loginUser, item);

                        if (!ret)
                        {
                            dbConnect.RollBack();
                            return false;
                        }
                    }

                    // 事務所更新情報
                    ret = 事務所更新情報処理.配乗表更新登録(dbConnect, loginUser, seninTableCache);
                    if (!ret)
                    {
                        dbConnect.RollBack();
                        return false;
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


        public static bool SiHaijou_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, SiHaijou h)
        {
            h.RenewUserID = loginUser.MsUserID;
            h.RenewDate = DateTime.Now;

            if (h.IsNew())
            {
                h.SiHaijouID = System.Guid.NewGuid().ToString();
                return h.InsertRecord(dbConnect, loginUser);
            }
            else
            {
                return h.UpdateRecord(dbConnect, loginUser);
            }
        }


        public static bool SiHaijouItem_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, SiHaijouItem i)
        {
            i.RenewUserID = loginUser.MsUserID;
            i.RenewDate = DateTime.Now;

            if (i.IsNew())
            {
                i.SiHaijouItemID = System.Guid.NewGuid().ToString();
                return i.InsertRecord(dbConnect, loginUser);
            }
            else
            {
                return i.UpdateRecord(dbConnect, loginUser);
            }
        }


        public static bool Is乗船(List<MsSiShubetsu> shubetsus, int msSiShubetsuId)
        {
            foreach (MsSiShubetsu s in shubetsus)
            {
                if (s.Name == "乗船" && s.MsSiShubetsuID == msSiShubetsuId)
                {
                    return true;
                }
            }

            return false;
        }


        public static bool Is有給(List<MsSiShubetsu> shubetsus, int msSiShubetsuId)
        {
            foreach (MsSiShubetsu s in shubetsus)
            {
                if (s.MsSiShubetsuID == msSiShubetsuId)
                {
                    return s.KyuukaFlag == 1;
                }
            }

            return false;
        }


        public static bool BLC_次年度休暇確定(MsUser loginUser, string shimeNen, SeninTableCache seninTableCache)
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    List<MsSenin> senins = MsSenin.GetRecords(dbConnect, loginUser);
                    DateTime date = DateTime.ParseExact(shimeNen + "0401", "yyyyMMdd", null);

                    foreach (MsSenin s in senins)
                    {
                        // 今年度残休暇を計算する.
                        // = (本年度休暇日数) + (前年度休暇繰越日数) - (KYUUKA_FLAG == 1 のレコードの日数の和) - (休暇買上日数)
                        SiCardFilter filter = new SiCardFilter();
                        filter.MsSeninID = s.MsSeninID;
                        filter.Start = DateTimeUtils.年度開始日(date);
                        filter.End = DateTimeUtils.年度終了日(date);

                        List<SiCard> cards = SiCard.GetRecordsByFilter(dbConnect, loginUser, filter);

                        int 残休暇日数 = Calc_残休暇日数(loginUser, dbConnect, seninTableCache, cards, date);


                        // SiCard_休暇管理_Insert を共通化して使用するように変更

                        //// 次年度の「休暇買上」「本年度休暇日数」「前年度休暇繰越日数」を生成.
                        //// 「休暇買上」
                        //CreateSiCard(loginUser, s.MsSeninID, seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.休暇買上), 0, date.AddYears(1)).InsertRecord(dbConnect, loginUser);
                        //// 「本年度休暇日数」
                        //CreateSiCard(loginUser, s.MsSeninID, seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.本年度休暇日数), 船員_年度休暇日数, date.AddYears(1)).InsertRecord(dbConnect, loginUser);

                        //// 「前年度休暇繰越日数」
                        //CreateSiCard(loginUser, s.MsSeninID, seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.前年度休暇繰越日数), 残休暇日数, date.AddYears(1)).InsertRecord(dbConnect, loginUser);

                        SiCard_休暇管理_Insert(dbConnect, loginUser, seninTableCache, s.MsSeninID, s.Department, date.AddYears(1), 残休暇日数);
                    }

                    // 年次締め.
                    SiNenjiShime nenjiShime = new SiNenjiShime();

                    nenjiShime.SiNenjiShimeID = System.Guid.NewGuid().ToString();
                    nenjiShime.Nen = shimeNen;
                    nenjiShime.MsUserID = loginUser.MsUserID;
                    nenjiShime.RenewDate = DateTime.Now;
                    nenjiShime.RenewUserID = loginUser.MsUserID;

                    nenjiShime.InsertRecord(dbConnect, loginUser);

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


        private static int Calc_残休暇日数(MsUser loginUser, DBConnect dbConnect, SeninTableCache seninTableCache, List<SiCard> cards, DateTime date)
        {
            int 残休暇日数 = 0;

            foreach (SiCard c in cards)
            {
                int kyuukaFlag = seninTableCache.GetMsSiShubetsu(loginUser, c.MsSiShubetsuID).KyuukaFlag;

                // 本年度休暇日数
                if (kyuukaFlag == (int)MsSiShubetsu.KyuukaFlagKind.本年度休暇日数)
                {
                    残休暇日数 += c.Days;
                }
                // 前年度休暇繰越日数
                else if (kyuukaFlag == (int)MsSiShubetsu.KyuukaFlagKind.前年度休暇繰越日数)
                {
                    残休暇日数 += c.Days;
                }
                // 休暇買上
                else if (kyuukaFlag == (int)MsSiShubetsu.KyuukaFlagKind.休暇買上)
                {
                    残休暇日数 -= c.Days;
                }
                // 休暇
                else if (kyuukaFlag == (int)MsSiShubetsu.KyuukaFlagKind.休暇)
                {
                    if (c.EndDate > DateTimeUtils.年度終了日(date)) // 休暇の終了日が年度終了日を超えている場合
                    {
                        船員カード分割(loginUser, dbConnect, c, date);
                    }
                    else if (DateTime.Today > DateTimeUtils.年度終了日(date)) // 操作日が年度終了日を超えている場合
                    {
                        if (c.StartDate < DateTimeUtils.年度終了日(date) && c.EndDate == DateTime.MinValue) // 休暇の開始が年度内で終了が未入力の場合
                        {
                            船員カード分割(loginUser, dbConnect, c, date);
                        }
                    }

                    残休暇日数 -= c.CalcDays;
                }
            }

            return 残休暇日数;
        }


        private static void 船員カード分割(MsUser loginUser, DBConnect dbConnect, SiCard c, DateTime date)
        {
            // 今年度のレコード更新.
            DateTime tmpEndDate = c.EndDate;

            c.EndDate = DateTimeUtils.年度終了日(date);
            c.Days = (c.EndDate - c.StartDate).Days + 1;

            c.RenewDate = DateTime.Now;
            c.RenewUserID = loginUser.MsUserID;

            c.UpdateRecord(dbConnect, loginUser);

            // 次年度のレコード生成.
            SiCard newCard = new SiCard();
            
            newCard.SiCardID = System.Guid.NewGuid().ToString();
            newCard.MsSeninID = c.MsSeninID;
            newCard.MsSiShubetsuID = c.MsSiShubetsuID;
            newCard.StartDate = DateTimeUtils.年度開始日(DateTimeUtils.年度終了日(date).AddDays(1));
            newCard.EndDate = tmpEndDate;
            newCard.Days = (newCard.EndDate - newCard.StartDate).Days + 1;

            newCard.RenewDate = DateTime.Now;
            newCard.RenewUserID = loginUser.MsUserID;

            newCard.InsertRecord(dbConnect, loginUser);
        }


        private static SiCard CreateSiCard(MsUser loginUser, int msSeninId, int msSiShubetsuId, int days, DateTime date)
        {
            SiCard card = new SiCard();

            card.SiCardID = System.Guid.NewGuid().ToString();

            card.MsSeninID = msSeninId;
            card.MsSiShubetsuID = msSiShubetsuId;

            card.StartDate = DateTimeUtils.年度開始日(date);
            card.EndDate = DateTimeUtils.年度終了日(date);
            card.Days = days;

            card.RenewUserID = loginUser.MsUserID;
            card.RenewDate = DateTime.Now;

            return card;
        }

        public static bool BLC_免状免許_アラーム削除(MsUser loginUser, SiMenjou menjou)
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    menjou.RenewUserID = loginUser.MsUserID;
                    menjou.RenewDate = DateTime.Now;

                    if (!menjou.UpdateRecord(dbConnect, loginUser))
                    {
                        return false;
                    }

                    foreach (PtAlarmInfo alarm in menjou.AlarmInfoList)
                    {
                        alarm.RenewUserID = loginUser.MsUserID;
                        alarm.RenewDate = DateTime.Now;

                        if (alarm.IsNew())
                        {
                            alarm.PtAlarmInfoId = System.Guid.NewGuid().ToString();
                            alarm.SanshoumotoId = menjou.SiMenjouID;

                            if (!alarm.InsertRecord(dbConnect, loginUser))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (!alarm.UpdateRecord(dbConnect, loginUser))
                            {
                                return false;
                            }
                        }
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


        public static List<MsSenin> BLC_船員検索(MsUser loginUser, SeninTableCache seninTableCache, MsSeninFilter filter)
        {
            filter.OrderByStr = "OrderByMsSiShokumeiIdSeiMei";

            // 常に「種別なし」をふくんで検索をする
            bool 種別無しIsTrue = true;
            if (filter.種別無し == false)
            {
                filter.種別無し = true;
                種別無しIsTrue = false;
            }


            // RetireFlagの置き換え
            if (filter.RetireFlag == (int)MsSeninFilter.enumRetire.EXCEPT)
            {
                filter.RetireFlag = 0;
            }
            else if (filter.RetireFlag == (int)MsSeninFilter.enumRetire.ONLY)
            {
                filter.RetireFlag = 1;
            }
            else
            {
                filter.RetireFlag = int.MinValue;
            }


            List<MsSenin> senins = MsSenin.GetRecordsByFilter(loginUser, filter);

            // 2012.03 「種別なし」の場合、
            // 翌日のカードを取得し、置き換える
            // （通常運用していれば、「種別なし」となるのは、該当日に下船した「乗船」カード
            //   この場合、翌日に「有給休暇」または「旅行日（転船）」が必ずあるはず。
            var 種別なしseninIds = from s in senins
                               where s.MsSiShubetsuID < 0
                               select s.MsSeninID;
            List<SiCard> cards = BLC_最新カード検索(loginUser, seninTableCache, 種別なしseninIds);
            foreach (MsSenin s in senins)
            {
                var card = from c in cards
                           where c.MsSeninID == s.MsSeninID
                           select c;
                if (card.Count<SiCard>() > 0)
                {
                    SiCard c = card.First<SiCard>();
                    s.MsSiShubetsuID = c.MsSiShubetsuID;
                    s.StartDate = c.StartDate;
                }
            }

            // 元々の検索条件に一致するもののみ返す
            List<MsSenin> ret = new List<MsSenin>();
            foreach (MsSenin s in senins)
            {
                if (種別無しIsTrue == false && s.MsSiShubetsuID < 0)
                {
                    // 元々、「種別なし」が検索条件でない場合、種別IDが０以下は無視する
                    continue;
                }

                if (s.MsSiShubetsuID < 0)
                {
                    if (種別無しIsTrue == true)
                    {
                        // 元々、「種別なし」が検索条件の場合、種別IDが０以下は対象とする
                        ret.Add(s);
                    }
                }
                else if (filter.MsSiShubetsuIDs.Contains(s.MsSiShubetsuID))
                {
                    // 元々、検索条件の種別は対象とする
                    ret.Add(s);
                }
            }



            var seninIds = from s in ret
                           select s.MsSeninID;

            Dictionary<int, Dictionary<int, int>> 合計日数Dic = BLC_船員合計日数(loginUser, seninTableCache, seninIds);

            foreach (MsSenin s in ret)
            {
                if (合計日数Dic.ContainsKey(s.MsSeninID))
                {
                    s.合計日数 = 合計日数Dic[s.MsSeninID];
                }
            }



            return ret;

        }


        public static List<SiCard> BLC_船員カード検索(MsUser loginUser, SeninTableCache seninTableCache, SiCardFilter filter)
        {
            List<SiCard> cards = SiCard.GetRecordsByFilter(loginUser, filter);

            var seninIds = from c in cards
                           select c.MsSeninID;

            Dictionary<int, Dictionary<int, int>> 合計日数Dic = BLC_船員合計日数(loginUser, seninTableCache, seninIds);

            foreach (SiCard s in cards)
            {
                if (合計日数Dic.ContainsKey(s.MsSeninID))
                {
                    s.合計日数 = 合計日数Dic[s.MsSeninID];
                    s.休暇残日 = Calc_休暇残日(loginUser, seninTableCache, 合計日数Dic[s.MsSeninID]);
                }
            }

            return cards;
        }
        public static List<SiCard> BLC_交代者検索(MsUser loginUser, SeninTableCache seninTableCache, SiCardFilter filter)
        {
            List<SiCard> cards = SiCard.GetRecordsByFilter(loginUser, filter);

            cards.ForEach(o => { o.CardMsSiShokumeiID = (o.SiLinkShokumeiCards != null ? o.SiLinkShokumeiCards[0].MsSiShokumeiID : int.MinValue); });

            if (filter.CardMsSiShokumeiID > 0)
            {
                cards = cards.Where(o => o.CardMsSiShokumeiID == filter.CardMsSiShokumeiID).ToList();
            }
            if (filter.Name != null)
            {
                cards = cards.Where(o => o.SeninName.Contains(filter.Name)).ToList();
            }


            // 
            List<SiCard> ret = new List<SiCard>();

            foreach (MsSiShokumei s in seninTableCache.GetMsSiShokumeiList(loginUser))
            {
                if (cards.Any(o => o.SeninMsSiShokumeiID == s.MsSiShokumeiID))
                {
                    ret.AddRange(cards.Where(o => o.SeninMsSiShokumeiID == s.MsSiShokumeiID));
                }
            }

            return ret;
        }



        private static int Calc_休暇残日(MsUser loginUser, SeninTableCache seninTableCache, Dictionary<int, int> dictionary)
        {
            int 残休暇日数 = 0;

            // 本年度休暇日数
            if (dictionary.ContainsKey(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.本年度休暇日数)))
            {
                残休暇日数 += dictionary[seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.本年度休暇日数)];
            }

            // 前年度休暇繰越日数
            if (dictionary.ContainsKey(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.前年度休暇繰越日数)))
            {
                残休暇日数 += dictionary[seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.前年度休暇繰越日数)];
            }

            // 休暇買上
            if (dictionary.ContainsKey(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.休暇買上)))
            {
                残休暇日数 -= dictionary[seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.休暇買上)];
            }

            // 休暇
            if (dictionary.ContainsKey(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.有給休暇)))
            {
                残休暇日数 -= dictionary[seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.有給休暇)];
            }

            return 残休暇日数;
        }


        public static List<MsSenin> BLC_船員検索_帳票(MsUser loginUser, DateTime date)
        {
            MsSeninFilter filter = new MsSeninFilter();

            filter.OrderByStr = "OrderByMsSiShokumeiIdSeiMei";
            filter.退職年度 = date;
            filter.joinSiCard = MsSeninFilter.JoinSiCard.NOT_JOIN;

            List<MsSenin> senins = MsSenin.GetRecordsByFilter(loginUser, filter);

            return senins;

        }

        public static Dictionary<int, Dictionary<int, int>> BLC_船員合計日数(MsUser loginUser, SeninTableCache seninTableCache, IEnumerable<int> msSeninIds)
        {
            return BLC_船員合計日数(loginUser, seninTableCache, msSeninIds, DateTimeUtils.年度開始日(), DateTimeUtils.年度終了日(), false);
        }
        public static Dictionary<int, Dictionary<int, int>> BLC_船員合計日数(MsUser loginUser, SeninTableCache seninTableCache, IEnumerable<int> msSeninIds, DateTime start, DateTime end, bool nendoOnly)
        {
            // <MsSeninId <MsSiShubetsuId, days>>
            Dictionary<int, Dictionary<int, int>> days = new Dictionary<int, Dictionary<int, int>>();

            SiCardFilter filter = new SiCardFilter();
            filter.MsSeninIDs.AddRange(msSeninIds);

            filter.Start = start;
            filter.End = end;

            List<SiCard> cards = SiCard.GetRecordsMinimum(loginUser, filter);

            foreach (SiCard c in cards)
            {
                // 2012.03 同日転船対応
                if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.転船) && c.Days < 0)
                {
                    // 同日転船の場合、レコードを無視する
                    continue;
                }

                int d = 0;

                if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.本年度休暇日数) ||
                    c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.前年度休暇繰越日数) ||
                    c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.休暇買上))
                {
                    d = c.Days;
                }
                else
                {
                    DateTime startDay = c.StartDate;
                    if (nendoOnly && c.StartDate < start)
                    {
                        startDay = start;
                    }

                    if (c.EndDate == DateTime.MinValue || (DateTimeUtils.ToFrom(c.StartDate) <= DateTime.Now && DateTime.Now < DateTimeUtils.ToTo(c.EndDate)))
                    {
                        d = (DateTimeUtils.ToTo(DateTime.Now) - DateTimeUtils.ToFrom(startDay)).Days;
                    }
                    else
                    {
                        d = (DateTimeUtils.ToTo(c.EndDate) - DateTimeUtils.ToFrom(startDay)).Days;
                    }
                }

                if (!days.ContainsKey(c.MsSeninID))
                {
                    days[c.MsSeninID] = new Dictionary<int, int>();
                }

                if (!days[c.MsSeninID].ContainsKey(c.MsSiShubetsuID))
                {
                    days[c.MsSeninID][c.MsSiShubetsuID] = 0;
                }

                days[c.MsSeninID][c.MsSiShubetsuID] += d;
            }

            return days;
        }

        #region ｸﾗｳﾄﾞ対応前のコード
        //public static bool BLC_月次計上確定(MsUser loginUser, string shimeNengetsu)
        //{
        //    bool ret = true;

        //    DateTime date = new DateTime(int.Parse(shimeNengetsu.Substring(0, 4)), int.Parse(shimeNengetsu.Substring(4, 2)), 1);
        //    string 船用金担当者 = System.Configuration.ConfigurationManager.AppSettings["概算担当者"];
        //    MsParameter msParam = MsParameter.GetRecord(loginUser, "船用金担当者");
        //    if (msParam != null)
        //    {
        //        船用金担当者 = msParam.Value;
        //    }

        //    Dictionary<int, MsKamoku> msSiKamokuIdToMsKamokuDic = GetMsSiKamokuIdToMsKamokuDic(loginUser);
        //    List<MsVessel> msVesselList = MsVessel.GetRecordsByHachuEnabled(loginUser);
        //    // 2014.03: 2013年度改造
        //    List<MsTax> msTaxList = MsTax.GetRecords(loginUser);

        //    using (DBConnect dbConnect = new DBConnect())  
        //    {
        //        dbConnect.BeginTransaction();

        //        try
        //        {
        //            // 
        //            foreach (MsVessel msVessel in msVesselList)
        //            {
        //                decimal 先月末残高 = SiJunbikin.Get_先月末残高(dbConnect, loginUser, date, msVessel.MsVesselID);
        //                // 2014.03 ２０１３年度改造
        //                //List<SiJunbikin> junbikins = SiJunbikin.GetRecordsByDateAndMsVesselID(dbConnect, loginUser, date, msVessel.MsVesselID);
        //                //SortedDictionary<int, SortedDictionary<int, KamokuRow>> kamokuRows = GroupByKamoku(先月末残高, junbikins, msSiKamokuIdToMsKamokuDic);
        //                Dictionary<decimal, List<SiJunbikin>> junbikins = SiJunbikin.Get科目別集計表データ(dbConnect, loginUser, date, msVessel.MsVesselID);
        //                SortedDictionary<int, SortedDictionary<int, KamokuRow>> kamokuRows = GroupByKamoku(msTaxList, 先月末残高, junbikins, msSiKamokuIdToMsKamokuDic);

        //                InsertIF(dbConnect, loginUser, date, msVessel, kamokuRows, 船用金担当者);
        //            }

        //            // 月次締めレコードを登録する
        //            SiGetsujiShime getsujiShime = new SiGetsujiShime();

        //            getsujiShime.SiGetsujiShimeID = System.Guid.NewGuid().ToString();
        //            getsujiShime.NenGetsu = shimeNengetsu;
        //            getsujiShime.MsUserID = loginUser.MsUserID;
        //            getsujiShime.RenewDate = DateTime.Now;
        //            getsujiShime.RenewUserID = loginUser.MsUserID;

        //            ret = getsujiShime.InsertRecord(dbConnect, loginUser);

        //            dbConnect.Commit();
        //            return true;
        //        }
        //        catch (Exception e)
        //        {
        //            dbConnect.RollBack();
        //            return false;
        //        }
        //    }

        //    return ret;
        //}
        //private static void InsertIF(DBConnect dbConnect, MsUser loginUser, DateTime date, MsVessel msVessel, SortedDictionary<int, SortedDictionary<int, KamokuRow>> kamokuRows, string 船用金担当者)
        //{
        //    string sumKanriNo = "";
        //    int lineNo = 0;
        //    int maxLineNo = 15; // 1つの集約管理番号につきレコードは15とする

        //    foreach (KeyValuePair<int, SortedDictionary<int, KamokuRow>> pair in kamokuRows)
        //    {
        //        // 全社費用の場合、船NoはNullを設定
        //        string VesselNo = "";
        //        if (pair.Key == (int)MsSiKamoku.費用種別.全社費用)
        //        {
        //            VesselNo = Tajsinseiif.全社共通船コード;
        //        }
        //        else
        //        {
        //            VesselNo = msVessel.VesselNo;
        //        }

        //        foreach (KamokuRow row in pair.Value.Values)
        //        {
        //            Tajsinseiif tajsinseiif = new Tajsinseiif();


        //            decimal kingaku = 0;
        //            decimal tax = 0;
        //            string syhzCd = "";
        //            if (row.KingakuIn > 0)
        //            {
        //                kingaku = row.KingakuIn;
        //                tax = row.TaxIn;
        //            }
        //            else
        //            {
        //                kingaku = row.KingakuOut;
        //                tax = row.TaxOut;
        //            }
        //            if (tax == 0)
        //            {
        //                syhzCd = tajsinseiif.概算_消費税コード_免税;
        //            }
        //            else
        //            {
        //                // 2014.03: 2013年度改造
        //                //syhzCd = tajsinseiif.概算_消費税コード_外税;
        //                syhzCd = row.TaxCode;
        //            }
        //            string kamokuNo = "";
        //            string utiwakeKamokuNo = "";
        //            string aiteKanjoKmkCd = "";
        //            string aiteUtiwakeKmkCd = "";
        //            string tekiyou = "";
        //            string meisai = "";
        //            if (row.KamokuName == "船用金繰越")
        //            {
        //                tekiyou = date.Month.ToString("00") + "月分 " + msVessel.VesselName + " 船内収支報告";
        //                meisai = date.Month.ToString("00") + "月分 船内収支報告" + " " + (date.AddMonths(-1).Month).ToString("00") + "月分繰越";

        //                aiteKanjoKmkCd = "14830";
        //                aiteUtiwakeKmkCd = "14830010";

        //                tax = decimal.MinValue;
        //                syhzCd = tajsinseiif.概算_消費税コード_対象外;
        //            }
        //            else if (row.KamokuName == "船用金補給")
        //            {
        //                tekiyou = date.Month.ToString("00") + "月分 " + msVessel.VesselName + " 船内収支報告";
        //                meisai = date.Month.ToString("00") + "月分 船内収支報告" + " " + row.JunbikinDate.ToString("MM/dd") + "受入";

        //                aiteKanjoKmkCd = "14830";
        //                aiteUtiwakeKmkCd = "14830010";

        //                tax = decimal.MinValue;
        //                syhzCd = tajsinseiif.概算_消費税コード_対象外;
        //            }
        //            else if (row.KamokuName == "船内準備金繰越")
        //            {
        //                tekiyou = date.Month.ToString("00") + "月分 " + msVessel.VesselName + " 船内収支報告";
        //                meisai = date.Month.ToString("00") + "月分 船内収支報告" + " 翌月繰越";

        //                kamokuNo = "14830";
        //                utiwakeKamokuNo = "14830010";

        //                tax = decimal.MinValue;
        //                syhzCd = tajsinseiif.概算_消費税コード_対象外;
        //            }
        //            else
        //            {
        //                tekiyou = date.Month.ToString("00") + "月分 " + msVessel.VesselName + " 船内収支報告";
        //                meisai = date.Month.ToString("00") + "月分 船内収支報告";

        //                kamokuNo = row.KamokuNo;
        //                utiwakeKamokuNo = row.UtiwakeKamokuNo;
        //            }

        //            // インターフェイスサーバーにInsertする
        //            #region
        //            // 集約管理番号の採番
        //            // 1つの集約管理番号につきレコードは15とする           
        //            if (lineNo == 0 || lineNo == maxLineNo)
        //            {
        //                sumKanriNo = Tajsinseiif.GetSumKanriNo(dbConnect, loginUser, "N" + DateTime.Now.ToString("yyyyMMdd"));
        //                lineNo = 0;
        //            }
        //            lineNo += 1;


        //            // 集約管理番号
        //            tajsinseiif.SumKanriNo = sumKanriNo;
        //            // 申請識別
        //            tajsinseiif.SinseiKanriNo = sumKanriNo + lineNo.ToString("00");
        //            // サブシステムコード
        //            tajsinseiif.SubsystemCd = tajsinseiif.概算_サブシステムコード;
        //            // 業務コード
        //            tajsinseiif.GyomuCd = tajsinseiif.概算_業務コード;
        //            // 取引コード
        //            tajsinseiif.TrhkCd = tajsinseiif.概算_取引コード;
        //            // 処理コード
        //            tajsinseiif.SyoriCd = tajsinseiif.概算_船内収支_処理コード;
        //            // 運用会社コード
        //            tajsinseiif.KisyCd = tajsinseiif.概算_運用会社コード;
        //            // 作成担当コード
        //            tajsinseiif.DatamkTntsyCd = 船用金担当者;
        //            // 船舶NO
        //            if (VesselNo != Tajsinseiif.全社共通船コード)
        //            {
        //                tajsinseiif.FuneNo = VesselNo;
        //            }
        //            // 申請担当者コード
        //            tajsinseiif.TnsyCd = 船用金担当者;
        //            // 計上日付
        //            tajsinseiif.KeijoYmd = date.AddMonths(1).AddDays(-1);
        //            // 取引先コード
        //            tajsinseiif.TrhkskCd = tajsinseiif.概算_取引先コード;
        //            // 通貨コード
        //            tajsinseiif.TukaCd = tajsinseiif.概算_通貨コード;
        //            // 基本摘要
        //            tajsinseiif.KihonTekiyo = tekiyou;
        //            // 承認パターンコード
        //            tajsinseiif.SyoninPatternCd = tajsinseiif.概算_承認パターンコード;
        //            // 行番号
        //            tajsinseiif.LineNo = String.Format("{0:0000}", lineNo);
        //            // 勘定科目
        //            tajsinseiif.KanjoKmkCd = kamokuNo;
        //            // 内訳科目
        //            tajsinseiif.UtiwakeKmkCd = utiwakeKamokuNo;
        //            // 相手勘定科目
        //            tajsinseiif.AiteKanjoKmkCd = aiteKanjoKmkCd;
        //            // 相手内訳科目
        //            tajsinseiif.AiteUtiwakeKmkCd = aiteUtiwakeKmkCd;
        //            // 消費税コード
        //            tajsinseiif.SyhzCd = syhzCd;
        //            // 金額
        //            tajsinseiif.Gaku = kingaku;
        //            // 消費税額
        //            tajsinseiif.SyohizeiGaku = tax;
        //            // 摘要
        //            tajsinseiif.MeisaiTekiyo = meisai;
        //            // 作成日時
        //            tajsinseiif.CreateDate = DateTime.Now;
        //            // 状態
        //            tajsinseiif.SyoriStatus = "00";
        //            // 外部システム識別コード
        //            tajsinseiif.ExtSystemCd = tajsinseiif.概算_更新プログラムID;
        //            // 更新プログラムID
        //            tajsinseiif.Updpgmid = tajsinseiif.概算_更新プログラムID;
        //            // 更新ユーザーID
        //            tajsinseiif.Upduserid = loginUser.LoginID;
        //            // 更新日時
        //            string nowTime = DateTime.Now.ToString("yyyyMMddhhmmssfff");
        //            tajsinseiif.UpddateYmdhms = Int64.Parse(nowTime);

        //            bool flag = tajsinseiif.InsertRecord(dbConnect, loginUser);

        //            #endregion
        //        }
        //    }
        //}
        #endregion

        public static bool BLC_月次計上確定(MsUser loginUser, string shimeNengetsu)
        {
            bool ret = true;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    // 月次締めレコードを登録する
                    SiGetsujiShime getsujiShime = new SiGetsujiShime();

                    getsujiShime.SiGetsujiShimeID = System.Guid.NewGuid().ToString();
                    getsujiShime.NenGetsu = shimeNengetsu;
                    getsujiShime.MsUserID = loginUser.MsUserID;
                    getsujiShime.RenewDate = DateTime.Now;
                    getsujiShime.RenewUserID = loginUser.MsUserID;

                    ret = getsujiShime.InsertRecord(dbConnect, loginUser);

                    dbConnect.Commit();
                }
                catch (Exception e)
                {
                    dbConnect.RollBack();
                    ret = false;
                }
            }

            return ret;
        }

        public static List<Tajsinseiif> Get_月次計上Records(MsUser loginUser, string shimeNengetsu)
        {
            List<Tajsinseiif> gaisanList = new List<Tajsinseiif>();

            DateTime date = new DateTime(int.Parse(shimeNengetsu.Substring(0, 4)), int.Parse(shimeNengetsu.Substring(4, 2)), 1);
            string 船用金担当者 = System.Configuration.ConfigurationManager.AppSettings["概算担当者"];
            MsParameter msParam = MsParameter.GetRecord(loginUser, "船用金担当者");
            if (msParam != null)
            {
                船用金担当者 = msParam.Value;
            }

            Dictionary<int, MsKamoku> msSiKamokuIdToMsKamokuDic = GetMsSiKamokuIdToMsKamokuDic(loginUser);
            List<MsVessel> msVesselList = MsVessel.GetRecordsByHachuEnabled(loginUser);
            // 2014.03: 2013年度改造
            List<MsTax> msTaxList = MsTax.GetRecords(loginUser);

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    // 
                    foreach (MsVessel msVessel in msVesselList)
                    {
                        decimal 先月末残高 = SiJunbikin.Get_先月末残高(dbConnect, loginUser, date, msVessel.MsVesselID);
                        //Dictionary<decimal, List<SiJunbikin>> junbikins = SiJunbikin.Get科目別集計表データ(dbConnect, loginUser, date, msVessel.MsVesselID);
                        SortedDictionary<int, List<SiJunbikin>> junbikins = SiJunbikin.Get科目別集計表データ(dbConnect, loginUser, date, msVessel.MsVesselID);
                        SortedDictionary<int, SortedDictionary<int, KamokuRow>> kamokuRows = GroupByKamoku(msTaxList, 先月末残高, junbikins, msSiKamokuIdToMsKamokuDic);

                       gaisanList.AddRange( _Get_月次計上Records(dbConnect, loginUser, date, msVessel, kamokuRows, 船用金担当者));
                    }
                }
                catch (Exception e)
                {
                    dbConnect.RollBack();
                }
            }

            return gaisanList;
        }

        private static List<Tajsinseiif> _Get_月次計上Records(DBConnect dbConnect, MsUser loginUser, DateTime date, MsVessel msVessel, SortedDictionary<int, SortedDictionary<int, KamokuRow>> kamokuRows, string 船用金担当者)
        {
            string sumKanriNo = "";
            int lineNo = 0;
            int maxLineNo = 15; // 1つの集約管理番号につきレコードは15とする

            List<Tajsinseiif> gaisanList = new List<Tajsinseiif>();

            foreach (KeyValuePair<int, SortedDictionary<int, KamokuRow>> pair in kamokuRows)
            {
                // 全社費用の場合、船NoはNullを設定
                string VesselNo = "";
                if (pair.Key == (int)MsSiKamoku.費用種別.全社費用)
                {
                    VesselNo = Tajsinseiif.全社共通船コード;
                }
                else
                {
                    VesselNo = msVessel.VesselNo;
                }

                foreach (KamokuRow row in pair.Value.Values)
                {
                    Tajsinseiif tajsinseiif = new Tajsinseiif();


                    decimal kingaku = 0;
                    decimal tax = 0;
                    string syhzCd = "";
                    if (row.KingakuIn > 0)
                    {
                        kingaku = row.KingakuIn;
                        tax = row.TaxIn;
                    }
                    else
                    {
                        kingaku = row.KingakuOut;
                        tax = row.TaxOut;
                    }
                    if (tax == 0)
                    {
                        syhzCd = tajsinseiif.概算_消費税コード_免税;
                    }
                    else
                    {
                        // 2014.03: 2013年度改造
                        //syhzCd = tajsinseiif.概算_消費税コード_外税;
                        syhzCd = row.TaxCode;
                    }
                    string kamokuNo = "";
                    string utiwakeKamokuNo = "";
                    string aiteKanjoKmkCd = "";
                    string aiteUtiwakeKmkCd = "";
                    string tekiyou = "";
                    string meisai = "";
                    if (row.KamokuName == "船用金繰越")
                    {
                        tekiyou = date.Month.ToString("00") + "月分 " + msVessel.VesselName + " 船内収支報告";
                        meisai = date.Month.ToString("00") + "月分 船内収支報告" + " " + (date.AddMonths(-1).Month).ToString("00") + "月分繰越";

                        aiteKanjoKmkCd = "14830";
                        aiteUtiwakeKmkCd = "14830010";

                        tax = decimal.MinValue;
                        syhzCd = tajsinseiif.概算_消費税コード_対象外;
                    }
                    else if (row.KamokuName == "船用金補給")
                    {
                        tekiyou = date.Month.ToString("00") + "月分 " + msVessel.VesselName + " 船内収支報告";
                        meisai = date.Month.ToString("00") + "月分 船内収支報告" + " " + row.JunbikinDate.ToString("MM/dd") + "受入";

                        aiteKanjoKmkCd = "14830";
                        aiteUtiwakeKmkCd = "14830010";

                        tax = decimal.MinValue;
                        syhzCd = tajsinseiif.概算_消費税コード_対象外;
                    }
                    else if (row.KamokuName == "船内準備金繰越")
                    {
                        tekiyou = date.Month.ToString("00") + "月分 " + msVessel.VesselName + " 船内収支報告";
                        meisai = date.Month.ToString("00") + "月分 船内収支報告" + " 翌月繰越";

                        kamokuNo = "14830";
                        utiwakeKamokuNo = "14830010";

                        tax = decimal.MinValue;
                        syhzCd = tajsinseiif.概算_消費税コード_対象外;
                    }
                    else
                    {
                        tekiyou = date.Month.ToString("00") + "月分 " + msVessel.VesselName + " 船内収支報告";
                        meisai = date.Month.ToString("00") + "月分 船内収支報告";

                        kamokuNo = row.KamokuNo;
                        utiwakeKamokuNo = row.UtiwakeKamokuNo;
                    }

                    // インターフェイスサーバーにInsertする
                    #region
                    // 集約管理番号の採番
                    // 1つの集約管理番号につきレコードは15とする           
                    if (lineNo == 0 || lineNo == maxLineNo)
                    {
                        //sumKanriNo = Tajsinseiif.GetSumKanriNo(dbConnect, loginUser, "N" + DateTime.Now.ToString("yyyyMMdd"));
                        sumKanriNo = System.Guid.NewGuid().ToString();
                        lineNo = 0;
                    }
                    lineNo += 1;


                    // 集約管理番号
                    tajsinseiif.SumKanriNo = sumKanriNo;
                    // 申請識別
                    //tajsinseiif.SinseiKanriNo = sumKanriNo + lineNo.ToString("00");
                    tajsinseiif.SinseiKanriNo = lineNo.ToString("00");
                    // サブシステムコード
                    tajsinseiif.SubsystemCd = tajsinseiif.概算_サブシステムコード;
                    // 業務コード
                    tajsinseiif.GyomuCd = tajsinseiif.概算_業務コード;
                    // 取引コード
                    tajsinseiif.TrhkCd = tajsinseiif.概算_取引コード;
                    // 処理コード
                    tajsinseiif.SyoriCd = tajsinseiif.概算_船内収支_処理コード;
                    // 運用会社コード
                    tajsinseiif.KisyCd = tajsinseiif.概算_運用会社コード;
                    // 作成担当コード
                    tajsinseiif.DatamkTntsyCd = 船用金担当者;
                    // 船舶NO
                    if (VesselNo != Tajsinseiif.全社共通船コード)
                    {
                        tajsinseiif.FuneNo = VesselNo;
                    }
                    // 申請担当者コード
                    tajsinseiif.TnsyCd = 船用金担当者;
                    // 計上日付
                    tajsinseiif.KeijoYmd = date.AddMonths(1).AddDays(-1);
                    // 取引先コード
                    tajsinseiif.TrhkskCd = tajsinseiif.概算_取引先コード;
                    // 通貨コード
                    tajsinseiif.TukaCd = tajsinseiif.概算_通貨コード;
                    // 基本摘要
                    tajsinseiif.KihonTekiyo = tekiyou;
                    // 承認パターンコード
                    tajsinseiif.SyoninPatternCd = tajsinseiif.概算_承認パターンコード;
                    // 行番号
                    tajsinseiif.LineNo = String.Format("{0:0000}", lineNo);
                    // 勘定科目
                    tajsinseiif.KanjoKmkCd = kamokuNo;
                    // 内訳科目
                    tajsinseiif.UtiwakeKmkCd = utiwakeKamokuNo;
                    // 相手勘定科目
                    tajsinseiif.AiteKanjoKmkCd = aiteKanjoKmkCd;
                    // 相手内訳科目
                    tajsinseiif.AiteUtiwakeKmkCd = aiteUtiwakeKmkCd;
                    // 消費税コード
                    tajsinseiif.SyhzCd = syhzCd;
                    // 金額
                    tajsinseiif.Gaku = kingaku;
                    // 消費税額
                    tajsinseiif.SyohizeiGaku = tax;
                    // 摘要
                    tajsinseiif.MeisaiTekiyo = meisai;
                    // 作成日時
                    tajsinseiif.CreateDate = DateTime.Now;
                    // 状態
                    tajsinseiif.SyoriStatus = "00";
                    // 外部システム識別コード
                    tajsinseiif.ExtSystemCd = tajsinseiif.概算_更新プログラムID;
                    // 更新プログラムID
                    tajsinseiif.Updpgmid = tajsinseiif.概算_更新プログラムID;
                    // 更新ユーザーID
                    tajsinseiif.Upduserid = loginUser.LoginID;
                    // 更新日時
                    string nowTime = DateTime.Now.ToString("yyyyMMddhhmmssfff");
                    tajsinseiif.UpddateYmdhms = Int64.Parse(nowTime);

                    //bool flag = tajsinseiif.InsertRecord(dbConnect, loginUser);

                    gaisanList.Add(tajsinseiif);

                    #endregion
                }
            }

            return gaisanList;
        }
       
        private static SortedDictionary<int, SortedDictionary<int, KamokuRow>> GroupByKamoku(List<MsTax> msTaxList, decimal 先月末残高, SortedDictionary<int, List<SiJunbikin>> junbikins, Dictionary<int, MsKamoku> msSiKamokuIdToMsKamokuDic)
        {
            decimal kingakuIn = 0;
            decimal kingakuOut = 0;

            SortedDictionary<int, SortedDictionary<int, KamokuRow>> kamokuRows = new SortedDictionary<int, SortedDictionary<int, KamokuRow>>();

            // 船内準備金繰越
            kamokuRows[(int)MsSiKamoku.費用種別.船員費用] = new SortedDictionary<int, KamokuRow>();
            kamokuRows[(int)MsSiKamoku.費用種別.船員費用][int.MinValue] = new KamokuRow();
            kamokuRows[(int)MsSiKamoku.費用種別.船員費用][int.MinValue].KamokuName = "船用金繰越";
            kamokuRows[(int)MsSiKamoku.費用種別.船員費用][int.MinValue].KingakuIn = 先月末残高;
            kingakuIn += 先月末残高;

            //foreach (decimal taxRate in junbikins.Keys)
            foreach (int taxId in junbikins.Keys)
            {
                var tax = msTaxList.Where(obj => obj.MsTaxID == taxId).First();

                //foreach (SiJunbikin j in junbikins[taxRate])
                foreach (SiJunbikin j in junbikins[taxId])
                {
                    if (j.KamokuName == "船用金補給")
                    {
                        j.MsSiKamokuId = int.MinValue + 1;// 船用金繰越の次に処理するため
                    }
                    if (!kamokuRows.ContainsKey(j.HiyouKind))
                    {
                        kamokuRows[j.HiyouKind] = new SortedDictionary<int, KamokuRow>();
                    }

                    int key = 0;
                    if (j.MsSiKamokuId > 0)
                    {
                        //key = int.Parse(j.MsSiKamokuId.ToString() + taxRate.ToString("0#"));
                        key = int.Parse(j.MsSiKamokuId.ToString() + tax.MsTaxID.ToString("0#"));
                    }
                    else
                    {
                        key = j.MsSiKamokuId;
                    }
                    if (!kamokuRows[j.HiyouKind].ContainsKey(key/*j.MsSiKamokuId*/))
                    {
                        kamokuRows[j.HiyouKind][key/*j.MsSiKamokuId*/] = new KamokuRow();
                        kamokuRows[j.HiyouKind][key/*j.MsSiKamokuId*/].KamokuName = j.KamokuName;
                    }

                    if (msSiKamokuIdToMsKamokuDic.ContainsKey(j.MsSiKamokuId))
                    {
                        MsKamoku kmk = msSiKamokuIdToMsKamokuDic[j.MsSiKamokuId];
                        if (kmk != null)
                        {
                            kamokuRows[j.HiyouKind][key/*j.MsSiKamokuId*/].KamokuNo = kmk.KamokuNo;
                            kamokuRows[j.HiyouKind][key/*j.MsSiKamokuId*/].UtiwakeKamokuNo = kmk.UtiwakeKamokuNo;
                        }
                    }
                    kamokuRows[j.HiyouKind][key/*j.MsSiKamokuId*/].JunbikinDate = j.JunbikinDate;
                    kamokuRows[j.HiyouKind][key/*j.MsSiKamokuId*/].KingakuOut += j.KingakuOut;
                    kamokuRows[j.HiyouKind][key/*j.MsSiKamokuId*/].TaxOut += j.TaxOut;
                    kamokuRows[j.HiyouKind][key/*j.MsSiKamokuId*/].KingakuIn += j.KingakuIn;
                    kamokuRows[j.HiyouKind][key/*j.MsSiKamokuId*/].TaxIn += j.TaxIn;
                    //kamokuRows[j.HiyouKind][key/*j.MsSiKamokuId*/].TaxRate = taxRate;
                    kamokuRows[j.HiyouKind][key/*j.MsSiKamokuId*/].TaxRate = tax.TaxRate;
                    //kamokuRows[j.HiyouKind][key/*j.MsSiKamokuId*/].TaxCode = msTaxList.Where(obj => obj.TaxRate == taxRate).First().TaxCode;
                    kamokuRows[j.HiyouKind][key/*j.MsSiKamokuId*/].TaxCode = tax.TaxCode;

                    kingakuIn += j.KingakuIn + j.TaxIn;
                    kingakuOut += j.KingakuOut + j.TaxOut;
                }
            }

            // 船内準備金繰越
            kamokuRows[int.MaxValue] = new SortedDictionary<int, KamokuRow>();
            kamokuRows[int.MaxValue][0] = new KamokuRow();
            kamokuRows[int.MaxValue][0].KamokuName = "船内準備金繰越";
            kamokuRows[int.MaxValue][0].KingakuIn = kingakuIn - kingakuOut;

            return kamokuRows;
        }
        private static Dictionary<int, MsKamoku> GetMsSiKamokuIdToMsKamokuDic(MsUser loginUser)
        {
            Dictionary<int, MsKamoku> ret = new Dictionary<int, MsKamoku>();

            List<MsKamoku> msKamokus = MsKamoku.GetRecords(loginUser);
            Hashtable msKamokuHash = new Hashtable();
            foreach (MsKamoku kmk in msKamokus)
            {
                msKamokuHash.Add(kmk.MsKamokuId, kmk);
            }

            List<MsSiKamoku> msSiKamokus = MsSiKamoku.GetRecords(loginUser);
            foreach (MsSiKamoku siKmk in msSiKamokus)
            {
                MsKamoku kmk = msKamokuHash[siKmk.MsKamokuId] as MsKamoku;
                ret.Add(siKmk.MsSiKamokuId, kmk);
            }

            return ret;
        }

        private class KamokuRow
        {
            public string KamokuName { get; set; }
            public string KamokuNo { get; set; }
            public string UtiwakeKamokuNo { get; set; }
            public DateTime JunbikinDate { get; set; }

            public decimal KingakuOut { get; set; }
            public decimal TaxOut { get; set; }
            public decimal KingakuIn { get; set; }
            public decimal TaxIn { get; set; }

            // 2014.03 ２０１３年度改造
            public decimal TaxRate { get; set; }
            public string TaxCode { get; set; }
        }

        public static List<SiCard> BLC_最新カード検索(MsUser loginUser, SeninTableCache seninTableCache, IEnumerable<int> msSeninIds)
        {
            SiCardFilter filter = new SiCardFilter();
            filter.MsSeninIDs.AddRange(msSeninIds);

            filter.Start = DateTimeUtils.年度開始日();
            filter.End = DateTimeUtils.年度終了日();

            List<SiCard> cards = SiCard.GetRecordsMinimum(loginUser, filter);

            List<SiCard> retCards = new List<SiCard>();

            foreach (int seninId in msSeninIds)
            {
                // 当日、下船しているデータがあるか
                var tmp1 = from c in cards
                           where c.EndDate.ToShortDateString() == DateTime.Now.ToShortDateString() && c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船)
                           select c;
                if (tmp1.Count<SiCard>() > 0)
                {
                    // 翌日のカードを検索
                    var tmp2 = from c in cards
                              where c.StartDate.ToShortDateString() == DateTime.Now.AddDays(1).ToShortDateString()
                              select c;

                    if (tmp2.Count<SiCard>() > 0)
                    {
                        foreach (SiCard c in tmp2)
                        {
                            retCards.Add(c);
                        }
                    }
                }
            }

            return retCards;
        }


        public static SiHaijou BLC_配乗表作成(MsUser loginUser, SeninTableCache seninTableCache, SiCardFilter filter)
        {
            SiHaijou haijou = new SiHaijou();
            List<SiCard> cards = null;

            // 予定を表示できるようにする
            DateTime fromDate = filter.Start;
            List<SiBoardingSchedule> boardingScheduleList = SiBoardingSchedule.GetRecordsByPlan(null, loginUser);


            cards = BLC_船員カード検索(loginUser, seninTableCache, filter);

            // 翌日のカードを検索
            // 下船した日は、上記検索で、対象とならないので。
            filter.Start = filter.Start.AddDays(1);
            filter.End = filter.End.AddDays(1);
            List<SiCard> tomorrowCards = BLC_船員カード検索(loginUser, seninTableCache, filter);

            // １日進めたので、戻しておく
            filter.Start = filter.Start.AddDays(-1);
            filter.End = filter.End.AddDays(-1);

            // 乗船中の船員の船員カードを取得しておく
            var 乗船seninIds = from s in cards
                               where s.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船)
                               select s.MsSeninID;
            SiCardFilter pFilter = new SiCardFilter();
            pFilter.MsSeninIDs.AddRange(乗船seninIds);
            pFilter.Start = DateTimeUtils.年度開始日().AddYears(-1); // さらに１年前くらいからのデータを検索する（年度をまたいでいるものもあるため）
            pFilter.End = DateTimeUtils.年度終了日();
  　        List<SiCard> prevCards = SiCard.GetRecordsMinimum(loginUser, pFilter);

           //// 有休中の船員の船員カードを取得しておく
           //var 有休seninIds = from s in cards
           //                 where s.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.有給休暇)
           //                 select s.MsSeninID;
           //pFilter = new SiCardFilter();
           //pFilter.MsSeninIDs.AddRange(有休seninIds);
           //pFilter.Start = DateTimeUtils.年度開始日().AddYears(-1); // さらに１年前くらいからのデータを検索する（年度をまたいでいるものもあるため）
           //pFilter.End = DateTimeUtils.年度終了日();
           //List<SiCard> prevCards2 = SiCard.GetRecordsMinimum(loginUser, pFilter);

            foreach (SiCard c in cards)
            {
                if (seninTableCache.Is_休暇管理(loginUser, c.MsSiShubetsuID))
                {
                    continue;
                }

                SiHaijouItem item = new SiHaijouItem();
                item.MsVesselID = c.MsVesselID;

                if (c.SiLinkShokumeiCards.Count > 0)
                {
                    foreach (SiLinkShokumeiCard link in c.SiLinkShokumeiCards)
                    {
                        item.MsSiShokumeiID = link.MsSiShokumeiID;
                        break;
                    }
                }
                else
                {
                    item.MsSiShokumeiID = c.SeninMsSiShokumeiID;
                }

                item.MsSiShubetsuID = c.MsSiShubetsuID;
                item.MsSeninID = c.MsSeninID;
                item.SeninName = c.SeninName;

                item.SiCardID = c.SiCardID;

                SetItemKind(c, item);


                //==============================================
                // 乗船中
                if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船))
                {
                    // 現在の乗船日数
                    if (c.EndDate == DateTime.MinValue || (DateTimeUtils.ToFrom(c.StartDate) <= filter.Start && filter.Start < DateTimeUtils.ToTo(c.EndDate)))
                    {
                        item.WorkDays = int.Parse(StringUtils.ToStr(c.StartDate, filter.Start));
                    }
                    else
                    {
                        item.WorkDays = int.Parse(StringUtils.ToStr(c.StartDate, c.EndDate));
                    }

                    // １つ前が「旅行日(転船)」さらに前が「乗船」の場合、日数を合計する
                    // 転船が一度とは限らないので、繰り返しデータを確認する
                    var tmpCards = from pc in prevCards
                                   where pc.MsSeninID == c.MsSeninID && pc.StartDate < c.StartDate
                                   orderby pc.StartDate descending, pc.EndDate descending
                                   select pc;
                    bool check旅行日_転船 = false;
                    foreach (SiCard pc in tmpCards)
                    {
                        if (pc.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.転船))
                        {
                            // 転船している場合、フラグを立てる
                            check旅行日_転船 = true;
                        }
                        if (pc.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船))
                        {
                            // 現在の乗船より前の乗船までの間に転船があった場合、乗船日をプラスする
                            if (check旅行日_転船)
                            {
                                item.WorkDays += int.Parse(StringUtils.ToStr(pc.StartDate, pc.EndDate));
                                check旅行日_転船 = false;
                            }
                            else
                            {
                                // 現在の乗船より前の乗船までの間に転船がなかった場合
                                // すなわち、別の乗船日なので、ループを抜ける
                                break;
                            }
                        }
                    }
                }
                //else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.有給休暇))
                //{
                //    if (c.EndDate == DateTime.MinValue || (DateTimeUtils.ToFrom(c.StartDate) <= filter.Start && filter.Start < DateTimeUtils.ToTo(c.EndDate)))
                //    {
                //        item.WorkDays = int.Parse(StringUtils.ToStr(c.StartDate, filter.Start));
                //    }
                //    else
                //    {
                //        item.WorkDays = int.Parse(StringUtils.ToStr(c.StartDate, c.EndDate));
                //    }

                //    // １つ前が「旅行日(研修・講習)」「その他(講習)」さらに前が「有給」の場合、日数を合計する
                //    // 一度とは限らないので、繰り返しデータを確認する
                //    var tmpCards = from pc in prevCards2
                //                   where pc.MsSeninID == c.MsSeninID && pc.StartDate < c.StartDate
                //                   orderby pc.StartDate descending, pc.EndDate descending
                //                   select pc;

                //    bool check旅行日_講習 = false;
                //    foreach (SiCard pc in tmpCards)
                //    {
                //        if (pc.MsSiShubetsuID != seninTableCache.MsSiShubetsu_旅行日_研修講習ID(loginUser) &&
                //            pc.MsSiShubetsuID != seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.研修) &&
                //            pc.MsSiShubetsuID != seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.有給休暇))
                //        {
                //            break;
                //        }
                //        if (pc.MsSiShubetsuID == seninTableCache.MsSiShubetsu_旅行日_研修講習ID(loginUser) || pc.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.研修))
                //        {
                //            // 「旅行日(研修・講習)」「その他(講習)」の場合、フラグを立てる
                //            check旅行日_講習 = true;
                //        }
                //        if (pc.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.有給休暇))
                //        {
                //            // 現在の有休より前の有休までの間に「旅行日(研修・講習)」「その他(講習)」があった場合、有休をプラスする
                //            if (check旅行日_講習)
                //            {
                //                item.WorkDays += int.Parse(StringUtils.ToStr(pc.StartDate, pc.EndDate));
                //                check旅行日_講習 = false;
                //            }
                //            else
                //            {
                //                // 現在の有休より前の有休までの間「旅行日(研修・講習)」「その他(講習)」がなかった場合
                //                // すなわち、別の有休なので、ループを抜ける
                //                break;
                //            }
                //        }
                //    }
                //}
                else if (c.合計日数 != null && c.合計日数.ContainsKey(c.MsSiShubetsuID))
                {
                    if (c.EndDate == DateTime.MinValue || (DateTimeUtils.ToFrom(c.StartDate) <= filter.Start && filter.Start < DateTimeUtils.ToTo(c.EndDate)))
                    {
                        item.WorkDays = int.Parse(StringUtils.ToStr(c.StartDate, filter.Start));
                    }
                    else
                    {
                        item.WorkDays = int.Parse(StringUtils.ToStr(c.StartDate, c.EndDate));
                    }
                }

                item.HoliDays = c.休暇残日;

                //if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser, MsSiShubetsu.SiShubetsu.乗船))
                //{
                //    if (boardingScheduleList.Any(obj => obj.SignOffSiCardID == c.SiCardID && obj.SignOnDate <= fromDate))
                //    {
                //        var sch = boardingScheduleList.Where(obj => obj.SignOffSiCardID == c.SiCardID && obj.SignOnDate <= fromDate).First();

                //        item.MsSeninID = sch.MsSeninID;
                //        item.MsSiShokumeiID = sch.MsSiShokumeiID;
                //        item.SeninName = sch.SignOnCrewName;

                //        item.SetItemKind(false, false, false);

                //        item.SiCardID = null;
                //        item.WorkDays = 0;
                //        item.HoliDays = 0;
                //    }


                //}
                if (filter.includeSchedule)
                {
                    if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser, MsSiShubetsu.SiShubetsu.乗船))
                    {
                        if (boardingScheduleList.Any(obj => obj.SignOffSiCardID == c.SiCardID && obj.SignOnDate <= fromDate))
                        {
                            var sch = boardingScheduleList.Where(obj => obj.SignOffSiCardID == c.SiCardID && obj.SignOnDate <= fromDate).First();

                            item.MsSeninID = sch.MsSeninID;
                            item.MsSiShokumeiID = sch.MsSiShokumeiID;
                            item.SeninName = sch.SignOnCrewName;

                            item.SetItemKind(false, false, false);

                            item.SiCardID = null;
                            item.WorkDays = 0;
                            item.HoliDays = 0;
                        }
                    }
                }


                haijou.SiHaijouItems.Add(item);
            }
            foreach (SiCard c in tomorrowCards)
            {
                var tmp = from todaysCard in cards
                          where todaysCard.MsSeninID == c.MsSeninID
                          select todaysCard.MsSeninID;
                if (tmp.Count<int>() > 0)
                {
                    continue;
                }
                if (seninTableCache.Is_休暇管理(loginUser, c.MsSiShubetsuID))
                {
                    continue;
                }

                SiHaijouItem item = new SiHaijouItem();
                item.MsVesselID = c.MsVesselID;
                item.MsSiShokumeiID = c.SeninMsSiShokumeiID;
                if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.転船))
                {
                    item.MsSiShubetsuID = seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.移動日);
                }
                else
                {
                    item.MsSiShubetsuID = c.MsSiShubetsuID;
                }
                item.MsSeninID = c.MsSeninID;
                item.SeninName = c.SeninName;

                if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.有給休暇))
                {
                    if (c.EndDate == DateTime.MinValue || (DateTimeUtils.ToFrom(c.StartDate) <= filter.Start && filter.Start < DateTimeUtils.ToTo(c.EndDate)))
                    {
                        item.WorkDays = int.Parse(StringUtils.ToStr(c.StartDate, filter.Start));
                    }
                    else
                    {
                        item.WorkDays = int.Parse(StringUtils.ToStr(c.StartDate, c.EndDate));
                    }
                }
                else if (c.合計日数 != null && c.合計日数.ContainsKey(c.MsSiShubetsuID))
                {
                    if (c.EndDate == DateTime.MinValue || (DateTimeUtils.ToFrom(c.StartDate) <= filter.Start && filter.Start < DateTimeUtils.ToTo(c.EndDate)))
                    {
                        item.WorkDays = int.Parse(StringUtils.ToStr(c.StartDate, filter.Start));
                    }
                    else
                    {
                        item.WorkDays = int.Parse(StringUtils.ToStr(c.StartDate, c.EndDate));
                    }
                }

                item.HoliDays = c.休暇残日;

                haijou.SiHaijouItems.Add(item);
            }
            return haijou;
        }

        private static void SetItemKind(SiCard c, SiHaijouItem item)
        {
            bool Is_兼 = false;
            bool Is_執 = false;
            bool Is_融 = false;

            if (c.SiLinkShokumeiCards.Count > 1)
            {
                Is_兼 = true;
            }

            if (c.SeninMsSiShokumeiID != item.MsSiShokumeiID) // 船員情報の職名と、乗船時の職が違う場合を執職とする
            {
                    Is_執 = true;
            }

            if (c.SeninKubun == 1)
            {
                Is_融 = true;
            }

            item.SetItemKind(Is_兼, Is_執, Is_融);
        }


        public static List<MsSenin> BLC_船員休暇検索(MsUser loginUser, SeninTableCache seninTableCache, MsSeninFilter filter)
        {
            filter.OrderByStr = "OrderByMsSiShokumeiIdSeiMei";

            // 常に「種別なし」をふくんで検索をする
            bool 種別無しIsTrue = true;
            if (filter.種別無し == false)
            {
                filter.種別無し = true;
                種別無しIsTrue = false;
            }

            List<MsSenin> senins = MsSenin.GetRecordsByFilter(loginUser, filter);

            //「種別なし」の場合、
            // 翌日のカードを取得し、置き換える
            // （通常運用していれば、「種別なし」となるのは、該当日に下船した「乗船」カード
            //   この場合、翌日に「有給休暇」または「旅行日（転船）」が必ずあるはず。
            var 種別なしseninIds = from s in senins
                               where s.MsSiShubetsuID < 0
                               select s.MsSeninID;
            List<SiCard> cards = BLC_最新カード検索(loginUser, seninTableCache, 種別なしseninIds);
            foreach (MsSenin s in senins)
            {
                var card = from c in cards
                           where c.MsSeninID == s.MsSeninID
                           select c;
                if (card.Count<SiCard>() > 0)
                {
                    SiCard c = card.First<SiCard>();
                    s.MsSiShubetsuID = c.MsSiShubetsuID;
                    s.StartDate = c.StartDate;
                }
            }

            var seninIds = from s in senins
                           select s.MsSeninID;

            // 指定年度で検索する
            DateTime year = DateTime.Parse( filter.Nendo + "/04/01" );
            Dictionary<int, Dictionary<int, int>> 合計日数Dic = BLC_船員合計日数(loginUser, seninTableCache, seninIds, DateTimeUtils.年度開始日(year), DateTimeUtils.年度終了日(year), true);

            foreach (MsSenin s in senins)
            {
                if (合計日数Dic.ContainsKey(s.MsSeninID))
                {
                    s.合計日数 = 合計日数Dic[s.MsSeninID];
                }
            }

            // 元々の検索条件に一致するもののみ返す
            List<MsSenin> ret = new List<MsSenin>();
            foreach (MsSenin s in senins)
            {
                if (種別無しIsTrue == false && s.MsSiShubetsuID < 0)
                {
                    // 元々、「種別なし」が検索条件でない場合、種別IDが０以下は無視する
                    continue;
                }

                if (s.MsSiShubetsuID < 0)
                {
                    if (種別無しIsTrue == true)
                    {
                        // 元々、「種別なし」が検索条件の場合、種別IDが０以下は対象とする
                        ret.Add(s);
                    }
                }
                else if (filter.MsSiShubetsuIDs.Contains(s.MsSiShubetsuID))
                {
                    // 元々、検索条件の種別は対象とする
                    ret.Add(s);
                }
            }
            return ret;

        }




        public static List<SiHaijouItem> BLC_乗船候補者検索(MsUser loginUser, SeninTableCache seninTableCache, SiCardFilter filter)
        {
            List<SiHaijouItem> haijouItems = new List<SiHaijouItem>();
            List<SiCard> cards = null;

            // 指定されている条件で検索
            cards = BLC_船員カード検索(loginUser, seninTableCache, filter);


            // 上記検索で取得できた船員の船員カードを取得しておく
            var seninIds = from s in cards
                             where s.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船)
                             select s.MsSeninID;

            SiCardFilter pFilter = new SiCardFilter();
            pFilter.MsSeninIDs.AddRange(seninIds);
            pFilter.Start = DateTime.Today.AddYears(-1);
            pFilter.End = DateTime.Today;
            //pFilter.MsSiShubetsuIDs.Add((seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船)));
            List<SiCard> prevCards = SiCard.GetRecordsMinimum(loginUser, pFilter);

System.Diagnostics.Debug.WriteLine("========================================");


            foreach (SiCard c in cards)
            {
                if (seninTableCache.Is_休暇管理(loginUser, c.MsSiShubetsuID))
                {
                    continue;
                }

                SiHaijouItem item = new SiHaijouItem();
                item.MsVesselID = c.MsVesselID;

                if (c.SiLinkShokumeiCards.Count > 0)
                {
                    foreach (SiLinkShokumeiCard link in c.SiLinkShokumeiCards)
                    {
                        item.MsSiShokumeiID = link.MsSiShokumeiID;
                        break;
                    }
                }
                else
                {
                    item.MsSiShokumeiID = c.SeninMsSiShokumeiID;
                }
                item.MsSiShubetsuID = c.MsSiShubetsuID;
                item.MsSeninID = c.MsSeninID;
                item.SeninName = c.SeninName;

                SetItemKind(c, item);

System.Diagnostics.Debug.WriteLine(item.SeninName);

                //==============================================
                // 乗船中じゃない船員
                if (c.MsSiShubetsuID != seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船))
                {
                    // １つ前の下船日の次の日からの日数を休暇数とする
                    var tmpCards = prevCards.Where(obj => obj.MsSeninID == c.MsSeninID && obj.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船) && obj.StartDate < c.StartDate).OrderByDescending(obj => obj.StartDate).ThenByDescending(obj => obj.EndDate);
                                 
                    if (tmpCards.Count() > 0)
                    {
                        SiCard tmpCard = tmpCards.First();
                        DateTime startDate = tmpCard.EndDate.AddDays(1);

System.Diagnostics.Debug.WriteLine("　[" + tmpCard.SiCardID + "][" + tmpCard.MsSiShubetsuID.ToString() + "]");
System.Diagnostics.Debug.WriteLine("　一つ前の乗船[" + seninTableCache.GetMsVesselName(loginUser, tmpCard.MsVesselID) + "[" + tmpCard.StartDate.ToShortDateString() + " - " + tmpCard.EndDate.ToShortDateString() + "]");

                        item.WorkDays = int.Parse(StringUtils.ToStr(startDate, DateTime.Now));
                    }
                    else
                    {
                        // 1年以内の乗船がない場合、この船員の乗船カードすべてを取得
                        pFilter = new SiCardFilter();
                        pFilter.MsSeninIDs.Add(c.MsSeninID);
                        pFilter.MsSiShubetsuIDs.Add((seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船)));
                        List<SiCard> thisCrewCards = SiCard.GetRecordsMinimum(loginUser, pFilter);

                        if (thisCrewCards.Count() > 0)
                        {
                            SiCard tmpCard = thisCrewCards.OrderByDescending(obj => obj.StartDate).ThenByDescending(obj => obj.EndDate).First();
                            DateTime startDate = tmpCard.EndDate.AddDays(1);

System.Diagnostics.Debug.WriteLine("　[" + tmpCard.SiCardID + "][" + tmpCard.MsSiShubetsuID.ToString() + "]");
System.Diagnostics.Debug.WriteLine("　一つ前の乗船[" + seninTableCache.GetMsVesselName(loginUser, tmpCard.MsVesselID) + "[" + tmpCard.StartDate.ToShortDateString() + " - " + tmpCard.EndDate.ToShortDateString() + "]");

                            item.WorkDays = int.Parse(StringUtils.ToStr(startDate, DateTime.Now));
                        }
                        else
                        {
                            // それでも乗船カードがない（＝一度も乗船していない）
                            item.WorkDays = int.Parse(StringUtils.ToStr(c.StartDate, DateTime.Now));
                        }

                    }

                }
                else 
                {   
                    // 乗船中の場合（転船させる候補者を探す場合）

                    // 現在の乗船日数
                    if (c.EndDate == DateTime.MinValue || (DateTimeUtils.ToFrom(c.StartDate) <= DateTime.Now && DateTime.Now < DateTimeUtils.ToTo(c.EndDate)))
                    {
                        item.WorkDays = int.Parse(StringUtils.ToStr(c.StartDate, DateTime.Now));
                    }
                    else
                    {
                        item.WorkDays = int.Parse(StringUtils.ToStr(c.StartDate, c.EndDate));
                    }

                    // １つ前が「旅行日(転船)」さらに前が「乗船」の場合、日数を合計する
                    // 転船が一度とは限らないので、繰り返しデータを確認する
                    var tmpCards = prevCards.Where(obj => obj.MsSeninID == c.MsSeninID && obj.StartDate < c.StartDate).OrderByDescending(obj => obj.StartDate).ThenByDescending(obj => obj.EndDate);
                    bool check旅行日_転船 = false;
                    foreach (SiCard pc in tmpCards)
                    {
                        if (pc.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.転船))
                        {
                            // 転船している場合、フラグを立てる
                            check旅行日_転船 = true;
                        }
                        if (pc.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船))
                        {
                            // 現在の乗船より前の乗船までの間に転船があった場合、乗船日をプラスする
                            if (check旅行日_転船)
                            {
                                item.WorkDays += int.Parse(StringUtils.ToStr(pc.StartDate, pc.EndDate));
                                check旅行日_転船 = false;
                            }
                            else
                            {
                                // 現在の乗船より前の乗船までの間に転船がなかった場合
                                // すなわち、別の乗船日なので、ループを抜ける
                                break;
                            }
                        }
                    }
                }

                haijouItems.Add(item);
            }



            return haijouItems;
        }






        public static List<SiSimulationDetail> BLC_GetCrewMatrix(MsUser loginUser, SeninTableCache seninTableCache, int msSeninId)
        {
            DateTime systemStartDate = DateTime.Parse("2009/04/01");

            MsSenin senin = MsSenin.GetRecord(loginUser, msSeninId);

            SiCardFilter filter = new SiCardFilter();
            filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船));
            filter.MsSeninID = msSeninId;
            filter.Start = systemStartDate;
            filter.End = DateTime.Today;
            List<SiCard> cards = SiCard.GetRecordsByFilter(loginUser, filter);

            List<SiRireki> rirekis = SiRireki.GetRecordsByMsSeninID(loginUser, msSeninId);

            return BLC_GetCrewMatrix(loginUser, seninTableCache, senin, systemStartDate, cards, rirekis);
        }

        public static List<SiSimulationDetail> BLC_GetCrewMatrix(MsUser loginUser, SeninTableCache seninTableCache, MsSenin senin, DateTime systemStartDate, List<SiCard> cards, List<SiRireki> rirekis)
        {

            List<SiSimulationDetail> ret = new List<SiSimulationDetail>();

            var companyList = (from c in cards
                               select c.CompanyName).Distinct<string>();

            foreach (string company in companyList)
            {
                int plusDays = 0;

                //System.Diagnostics.Debug.WriteLine("Company = " + company);
                var cardList = from c in cards
                               where c.CompanyName == company
                               select c;


                SiSimulationDetail detail = new SiSimulationDetail();
                detail.CompanyName = company;
                detail.MsSiShokumeiID = senin.MsSiShokumeiID;

                detail.MsSeninID = senin.MsSeninID;

                //================================================================================
                // YearsWithOperator
                //================================================================================
                decimal yearsWithOperator = 0; 
                DateTime minDate = DateTime.MinValue;
                DateTime maxDate = DateTime.MinValue;
                foreach (SiCard c in cards.OrderBy(obj => obj.StartDate))
                {
                    if (c.CompanyName == company)
                    {
                        //
                        SiCard workCard = new SiCard();
                        workCard.StartDate = c.StartDate;
                        workCard.EndDate = c.EndDate == DateTime.MinValue ? DateTime.Today : c.EndDate;

                        yearsWithOperator += CalcDays(workCard);
                    }
                }

                // 会社名が自社で、入社がシステム開始日より前の場合、プラスする日数を算出
                if (company == 船員_自社名 && senin.NyuushaDate != DateTime.MinValue && senin.NyuushaDate < systemStartDate)
                {
                    DateTime s = DateTimeUtils.年度開始日(senin.NyuushaDate); // 入社日のある年の年度開始日

                    plusDays = ((systemStartDate.Year - s.Year) * 270);
                    yearsWithOperator += plusDays;
                }


                //System.Diagnostics.Debug.WriteLine("==> yearsWithOperator : " + yearsWithOperator.ToString());

                detail.YearsWithOperator = yearsWithOperator;



                //================================================================================
                // YearsInRank
                //================================================================================
                decimal yearsInRank = 0;
                foreach (SiCard c in cardList)
                {
                    if (c.SiLinkShokumeiCards.Count() > 0)
                    {
                        foreach (SiLinkShokumeiCard link in c.SiLinkShokumeiCards)
                        {
                            if (link.MsSiShokumeiID == senin.MsSiShokumeiID)
                            {
                                yearsInRank += CalcDays(c);
                            }
                        }
                    }
                }

                // 会社名が自社で、入社がシステム開始日より前で、現職歴がシステム開始日より前の場合、プラスする日数を算出
                if (company == 船員_自社名 && senin.NyuushaDate < systemStartDate)
                {
                    if (rirekis.Any(obj => obj.MsSiShokumeiID == senin.MsSiShokumeiID && obj.RirekiDate < systemStartDate))
                    {
                        DateTime s = rirekis.Where(obj => obj.MsSiShokumeiID == senin.MsSiShokumeiID && obj.RirekiDate < systemStartDate).OrderBy(obj => obj.RirekiDate).First().RirekiDate;

                        yearsInRank += ((systemStartDate.Year - s.Year) * 270);
                    }
                }

                detail.YearsInRank = yearsInRank;



                //================================================================================
                // YearsOnTypeOfTanker
                //================================================================================
                foreach (MsCrewMatrixType type in seninTableCache.GetMsCrewMatrixTypeList(loginUser))
                {
                    SiSimulationDetailItem item = null;
                    var tmp = detail.Items.Where(obj => obj.CrewMatrixName == type.TypeName);
                    if (tmp.Count() > 0)
                    {
                        item = tmp.First();
                    }
                    else
                    {
                        item = new SiSimulationDetailItem();
                        item.CrewMatrixName = type.TypeName;

                        detail.Items.Add(item);
                    }

                    decimal yearsOnThisType = 0;
                    foreach (SiCard c in cardList)
                    {
                        if (c.MsCrewMatrixTypeID == type.MsCrewMatrixTypeID)
                        {
                            yearsOnThisType += CalcDays(c);
                        }
                    }

                    // 会社名が自社で、入社がシステム開始日より前で、GASの場合、プラスする日数を算出
                    if (company == 船員_自社名 && senin.NyuushaDate < systemStartDate && item.CrewMatrixName == "GAS")
                    {
                        yearsOnThisType += plusDays;
                    }

                    item.YearsOnThisType += yearsOnThisType;

                }

                ret.Add(detail);
            }

            return ret;
        }
  
        public static decimal CalcDays(SiCard c)
        {
            decimal d;
            if (c.EndDate == DateTime.MinValue || (DateTimeUtils.ToFrom(c.StartDate) <= DateTime.Now && DateTime.Now < DateTimeUtils.ToTo(c.EndDate)))
            {
                d = (DateTimeUtils.ToTo(DateTime.Now) - DateTimeUtils.ToFrom(c.StartDate)).Days;
            }
            else
            {
                d = (DateTimeUtils.ToTo(c.EndDate) - DateTimeUtils.ToFrom(c.StartDate)).Days;
            }
            if (d < 0)
                d = 0;

            return d;
        }
        public static decimal CalcYears(decimal days)
        {
            if (days < 0)
            {
                return 0;
            }
            int 年内日数 = 365;
            int 月内日数 = 31;

            //int y = (int)(days / 年内日数);

            //decimal zan = days - (y * 年内日数);
            //int m = (int)(zan / 月内日数);

            //return decimal.Parse(y.ToString() + "." + m.ToString());

            return days / 年内日数;
        }




        public static List<SiKazoku> BLC_家族表示順序更新(MsUser loginUser, int seninId, List<SiKazoku> kazokus)
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    foreach (SiKazoku k in kazokus)
                    {
                        k.MsSeninID = seninId;
                        SiKazoku_InsertOrUpdate(dbConnect, loginUser, k);
                    }

                    dbConnect.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    dbConnect.RollBack();
                    return null;
                }
            }

            return SiKazoku.GetRecordsByMsSeninID(loginUser, seninId);
        }

        public static int BLC_船員基本給登録(MsUser loginUser, MsSenin senin, SiSalary salary)
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    int msSeninId = 0;
                    if (senin.IsNew())
                    {
                        msSeninId = MsSenin_InsertOrUpdate(dbConnect, loginUser, senin);
                    }
                    else
                    {
                        msSeninId = senin.MsSeninID;
                    }

                    salary.MsSeninID = msSeninId;
                    salary.RenewUserID = loginUser.MsUserID;
                    salary.RenewDate = DateTime.Now;

                    if (salary.IsNew())
                    {
                        salary.SiSalaryID = System.Guid.NewGuid().ToString();
                        salary.InsertRecord(dbConnect, loginUser);
                    }
                    else
                    {
                        salary.UpdateRecord(dbConnect, loginUser);
                    }

                    dbConnect.Commit();
                    return msSeninId;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    dbConnect.RollBack();
                    return -1;
                }
            }
        }
        
        public static int BLC_船員既往歴登録(MsUser loginUser, MsSenin senin, SiKenshinPmhKa kenshinPmhKa)
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    int msSeninId = 0;
                    if (senin.IsNew())
                    {
                        msSeninId = MsSenin_InsertOrUpdate(dbConnect, loginUser, senin);
                    }
                    else
                    {
                        msSeninId = senin.MsSeninID;
                    }

                    kenshinPmhKa.MsSeninID = msSeninId;
                    kenshinPmhKa.RenewUserID = loginUser.MsUserID;
                    kenshinPmhKa.RenewDate = DateTime.Now;

                    if (kenshinPmhKa.IsNew())
                    {
                        kenshinPmhKa.SiKenshinPmhKaID = System.Guid.NewGuid().ToString();
                        kenshinPmhKa.InsertRecord(dbConnect, loginUser);
                    }
                    else
                    {
                        kenshinPmhKa.UpdateRecord(dbConnect, loginUser);
                    }

                    dbConnect.Commit();
                    return msSeninId;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    dbConnect.RollBack();
                    return -1;
                }
            }
        }

        public static int BLC_船員特記事項登録(MsUser loginUser, MsSenin senin, SiRemarks remarks)
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    int msSeninId = 0;
                    if (senin.IsNew())
                    {
                        msSeninId = MsSenin_InsertOrUpdate(dbConnect, loginUser, senin);
                    }
                    else
                    {
                        msSeninId = senin.MsSeninID;
                    }

                    remarks.MsSeninID = msSeninId;
                    remarks.RenewUserID = loginUser.MsUserID;
                    remarks.RenewDate = DateTime.Now;

                    if (remarks.IsNew())
                    {
                        remarks.SiRemarksID = System.Guid.NewGuid().ToString();
                        remarks.InsertRecord(dbConnect, loginUser);
                    }
                    else
                    {
                        remarks.UpdateRecord(dbConnect, loginUser);
                    }

                    dbConnect.Commit();
                    return msSeninId;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    dbConnect.RollBack();
                    return -1;
                }
            }
        }
        public static List<SiCard> BLC_船員カード検索2(MsUser loginUser, SeninTableCache seninTableCache, SiCardFilter filter)
        {
            List<SiCard> cards = SiCard.GetRecordsJoinMsUserByFilter(loginUser, filter);

            return cards;
        }

        public static bool BLC_船員カード登録(MsUser loginUser, SiCard card)
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    SiCard_InsertOrUpdate(dbConnect, loginUser, card);

                    if (card.EndDate == DateTime.MinValue)
                    {
                        foreach (SiLinkShokumeiCard link in card.SiLinkShokumeiCards)
                        {
                            link.SiCardID = card.SiCardID;
                            SiLinkShokumeiCard_InsertOrUpdate(dbConnect, loginUser, link);
                        }
                    }

                    dbConnect.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    dbConnect.RollBack();
                    return false;
                }
                return true;
            }
        }

    }
}
