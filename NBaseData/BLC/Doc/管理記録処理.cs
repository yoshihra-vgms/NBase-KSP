using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using ORMapping.Attrs;
using ORMapping;
using SqlMapper;
using System.Reflection;
using NBaseData.DAC;
using NBaseData.DS;
using ORMapping.Atts;
using System.Collections;

namespace NBaseData.BLC
{
    public class 管理記録処理
    {
        private static string 新規ID()
        {
            return System.Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 管理記録を登録する
        /// 　　[Wing] - 「管理記録登録」画面からCallされる
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="kanriKiroku"></param>
        /// <param name="kanriKirokuFile"></param>
        /// <param name="publishers"></param>
        /// <returns></returns>
        public static bool 登録(MsUser loginUser, DmKanriKiroku kanriKiroku, DmKanriKirokuFile kanriKirokuFile, List<DmPublisher> publishers)
        {
            bool ret = true;
            DateTime 処理日時 = DateTime.Now;
            string 更新者 = loginUser.MsUserID;

            List<DmKoukaiSaki> houkokushoKoukaisakis = DmKoukaiSaki.GetRecordsByLinkSakiID(loginUser, kanriKiroku.MsDmHoukokushoID);

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                // 管理記録の登録
                kanriKiroku.DmKanriKirokuID = 新規ID();
                kanriKiroku.RenewDate = 処理日時;
                kanriKiroku.RenewUserID = 更新者;

                ret = kanriKiroku.InsertRecord(dbConnect, loginUser);

                if (ret)
                {
                    // 管理記録ファイルの登録
                    kanriKirokuFile.DmKanriKirokuFileID = 新規ID();
                    kanriKirokuFile.DmKanriKirokuID = kanriKiroku.DmKanriKirokuID;
                    kanriKirokuFile.RenewDate = 処理日時;
                    kanriKirokuFile.RenewUserID = 更新者;


                    ret = kanriKirokuFile.InsertRecord(dbConnect, loginUser);

                    if (ret)
                    {
                        // 発行元の登録
                        foreach (DmPublisher publisher in publishers)
                        {
                            publisher.DmPublisherID = 新規ID();
                            publisher.LinkSakiID = kanriKiroku.DmKanriKirokuID;
                            publisher.RenewDate = 処理日時;
                            publisher.RenewUserID = 更新者;

                            ret = publisher.InsertRecord(dbConnect, loginUser);

                            if (!ret)
                                break;
                        }

                        // 公開先の登録（公開先は報告書マスタに設定されているものをコピーする）
                        foreach (DmKoukaiSaki houKKS in houkokushoKoukaisakis)
                        {
                            houKKS.DmKoukaiSakiID = 新規ID();
                            houKKS.LinkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                            houKKS.LinkSakiID = kanriKiroku.DmKanriKirokuID;
                            houKKS.RenewDate = 処理日時;
                            houKKS.RenewUserID = 更新者;

                            ret = houKKS.InsertRecord(dbConnect, loginUser);
                            if (!ret)
                            {
                                break;
                            }
                        }
                    }
                }

                if (ret)
                {
                    ret = アラーム登録(dbConnect, loginUser, kanriKiroku, publishers[0]);
                }

                if (ret)
                {
                    // 事務所更新情報
                    ret = 事務所更新情報処理.管理記録登録(dbConnect, loginUser, kanriKiroku, houkokushoKoukaisakis);
                }

                if (ret)
                {
                    dbConnect.Commit();
                }
                else
                {
                    dbConnect.RollBack();
                }

            }

            return ret;
        }

        /// <summary>
        /// 管理記録を登録する
        /// 　　[Wing] - 「管理記録登録」画面からCallされる
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="kanriKiroku"></param>
        /// <param name="kanriKirokuFile"></param>
        /// <returns></returns>
        public static bool 更新(MsUser loginUser, DmKanriKiroku kanriKiroku, DmKanriKirokuFile kanriKirokuFile)
        {
            bool ret = true;
            DateTime 処理日時 = DateTime.Now;
            string 更新者 = loginUser.MsUserID;
            List<DmKakuninJokyo> kakuninJokyos = DmKakuninJokyo.GetRecordsByLinkSaki(loginUser, (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録, kanriKiroku.DmKanriKirokuID);

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                // 管理記録の更新
                kanriKiroku.RenewDate = 処理日時;
                kanriKiroku.RenewUserID = 更新者;

                ret = kanriKiroku.UpdateRecord(dbConnect, loginUser);

                if (ret)
                {
                    // 管理記録ファイルの更新
                    kanriKirokuFile.DmKanriKirokuID = kanriKiroku.DmKanriKirokuID;
                    kanriKirokuFile.RenewDate = 処理日時;
                    kanriKirokuFile.RenewUserID = 更新者;

                    ret = kanriKirokuFile.UpdateRecord(dbConnect, loginUser);
                }
                if (ret)
                {
                    // 管理記録の更新は
                    // 既にある確認状況は削除とする
                    if (kakuninJokyos != null)
                    {
                        foreach (DmKakuninJokyo kj in kakuninJokyos)
                        {
                            kj.RenewDate = 処理日時;
                            kj.RenewUserID = 更新者;
                            kj.DeleteFlag = (int)NBaseData.DS.DocConstants.FlagEnum.ON;

                            ret = kj.UpdateRecord(dbConnect, loginUser);
                            if (!ret)
                                break;
                        }
                    }

                }

                if (ret)
                {
                    dbConnect.Commit();
                }
                else
                {
                    dbConnect.RollBack();
                }

            }

            return ret;
        }

        /// <summary>
        /// 管理記録登録時にアラームを登録する
        ///    管理記録処理.登録 からCallされる
        /// </summary>
        /// <param name="dbConnect"></param>
        /// <param name="loginUser"></param>
        /// <param name="kanriKiroku"></param>
        /// <param name="publisher"></param>
        /// <returns></returns>
        private static bool アラーム登録(DBConnect dbConnect, MsUser loginUser, DmKanriKiroku kanriKiroku, DmPublisher publisher)
        {
            bool ret = true;
            DateTime 処理日時 = DateTime.Now;
            string 更新者 = loginUser.MsUserID;

            PtAlarmInfo alarmInfo = null;
            PtDmAlarmInfo alarmDmInfo = null;
            int nen = kanriKiroku.JikiNen + 1;
            int tuki = kanriKiroku.JikiTuki;
            PtDmAlarmInfo nextAlarmDmInfo = null;
            #region 20210824 下記に変更
            //if (publisher.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者)
            //{
            //    alarmDmInfo = PtDmAlarmInfo.GetRecord(loginUser, kanriKiroku.MsDmHoukokushoID, kanriKiroku.JikiNen, kanriKiroku.JikiTuki, publisher.KoukaiSaki);
            //    nextAlarmDmInfo = PtDmAlarmInfo.GetRecord(loginUser, kanriKiroku.MsDmHoukokushoID, nen, tuki, publisher.KoukaiSaki);
            //}
            //else if (publisher.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.会長社長)
            //{
            //    alarmDmInfo = PtDmAlarmInfo.GetRecord(loginUser, kanriKiroku.MsDmHoukokushoID, kanriKiroku.JikiNen, kanriKiroku.JikiTuki, publisher.KoukaiSaki);
            //    nextAlarmDmInfo = PtDmAlarmInfo.GetRecord(loginUser, kanriKiroku.MsDmHoukokushoID, nen, tuki, publisher.KoukaiSaki);
            //}
            #endregion
            if (DocConstants.ClassItemList().Where(o => o.enumClass == DocConstants.ClassEnum.USER).Select(o => (int)o.enumRole).Contains(publisher.KoukaiSaki))
            {
                alarmDmInfo = PtDmAlarmInfo.GetRecord(loginUser, kanriKiroku.MsDmHoukokushoID, kanriKiroku.JikiNen, kanriKiroku.JikiTuki, publisher.KoukaiSaki);
                nextAlarmDmInfo = PtDmAlarmInfo.GetRecord(loginUser, kanriKiroku.MsDmHoukokushoID, nen, tuki, publisher.KoukaiSaki);
            }
            else if (publisher.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船)
            {
                alarmDmInfo = PtDmAlarmInfo.GetRecord(loginUser, kanriKiroku.MsDmHoukokushoID, kanriKiroku.JikiNen, kanriKiroku.JikiTuki, publisher.KoukaiSaki, publisher.MsVesselID);
                nextAlarmDmInfo = PtDmAlarmInfo.GetRecord(loginUser, kanriKiroku.MsDmHoukokushoID, nen, tuki, publisher.KoukaiSaki, publisher.MsVesselID);
            }
            else if (publisher.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.部門)
            {
                alarmDmInfo = PtDmAlarmInfo.GetRecord(loginUser, kanriKiroku.MsDmHoukokushoID, kanriKiroku.JikiNen, kanriKiroku.JikiTuki, publisher.KoukaiSaki, publisher.MsBumonID);
                nextAlarmDmInfo = PtDmAlarmInfo.GetRecord(loginUser, kanriKiroku.MsDmHoukokushoID, nen, tuki, publisher.KoukaiSaki, publisher.MsBumonID);
            }
            if (alarmDmInfo == null)
            {
                return true;
            }

            // アラーム停止
            alarmInfo = PtAlarmInfo.GetRecordsByPtAlarmInfoId(loginUser, alarmDmInfo.PtAlarmInfoId);
            if (alarmInfo == null)
            {
                return true;
            }
            alarmInfo.AlarmShowFlag = (int)PtAlarmInfo.AlarmShowFlagEnum.アラームＯＦＦ;
            alarmInfo.AlarmStopDate = 処理日時;
            alarmInfo.AlarmStopUser = 更新者;
            alarmInfo.RenewDate = 処理日時;
            alarmInfo.RenewUserID = 更新者;
            ret = alarmInfo.UpdateRecord(dbConnect, loginUser);
            if (!ret)
            {
                return ret;
            }

            alarmDmInfo.DeleteFlag = (int)NBaseData.DS.DocConstants.FlagEnum.ON;
            alarmDmInfo.RenewDate = 処理日時;
            alarmDmInfo.RenewUserID = 更新者;
            ret = alarmDmInfo.UpdateRecord(dbConnect, loginUser);
            if (!ret)
            {
                return ret;
            }

            if (nextAlarmDmInfo == null)
            {
                // 1年後の分を作成
                alarmInfo.PtAlarmInfoId = 新規ID();
                alarmInfo.AlarmShowFlag = (int)PtAlarmInfo.AlarmShowFlagEnum.アラームＯＮ;
                alarmInfo.HasseiDate = alarmInfo.HasseiDate.AddYears(1); // 1年後
                alarmInfo.AlarmStopDate = DateTime.MinValue;
                alarmInfo.AlarmStopUser = null;
                alarmInfo.RenewUserID = 更新者;
                alarmInfo.RenewDate = 処理日時;

                ret = alarmInfo.InsertRecord(dbConnect, loginUser);
                if (!ret)
                {
                    return ret;
                }
                alarmDmInfo.PtDmAlarmInfoID = 新規ID();
                alarmDmInfo.PtAlarmInfoId = alarmInfo.PtAlarmInfoId;
                alarmDmInfo.JikiNen = alarmDmInfo.JikiNen + 1;
                alarmDmInfo.DeleteFlag = (int)NBaseData.DS.DocConstants.FlagEnum.OFF;
                alarmDmInfo.RenewDate = 処理日時;
                alarmDmInfo.RenewUserID = 更新者;
                ret = alarmDmInfo.InsertRecord(dbConnect, loginUser);
                if (!ret)
                {
                    return ret;
                }
            }
            return ret;
        }

        #region 使用していない

        //public static bool 登録(MsUser loginUser, DmKanriKiroku kanriKiroku, DmKanriKirokuFile kanriKirokuFile, List<DmPublisher> publishers)
        //{
        //    bool ret = true;
        //    DateTime 処理日時 = DateTime.Now;
        //    string 更新者 = loginUser.MsUserID;

        //    using (DBConnect dbConnect = new DBConnect())
        //    {
        //        dbConnect.BeginTransaction();

        //        // 管理記録の登録
        //        kanriKiroku.DmKanriKirokuID = 新規ID();
        //        kanriKiroku.RenewDate = 処理日時;
        //        kanriKiroku.RenewUserID = 更新者;

        //        ret = kanriKiroku.InsertRecord(dbConnect, loginUser);

        //        if (ret)
        //        {
        //            // 管理記録ファイルの登録
        //            kanriKirokuFile.DmKanriKirokuFileID = 新規ID();
        //            kanriKirokuFile.DmKanriKirokuID = kanriKiroku.DmKanriKirokuID;
        //            kanriKirokuFile.RenewDate = 処理日時;
        //            kanriKirokuFile.RenewUserID = 更新者;


        //            ret = kanriKirokuFile.InsertRecord(dbConnect, loginUser);

        //            if (ret)
        //            {
        //                // 発行元の登録
        //                foreach (DmPublisher publisher in publishers)
        //                {
        //                    publisher.DmPublisherID = 新規ID();
        //                    publisher.LinkSakiID = kanriKiroku.DmKanriKirokuID;
        //                    publisher.RenewDate = 処理日時;
        //                    publisher.RenewUserID = 更新者;

        //                    ret = publisher.InsertRecord(dbConnect, loginUser);

        //                    if (!ret)
        //                        break;
        //                }
        //            }
        //        }

        //        if (ret)
        //        {
        //            dbConnect.Commit();
        //        }
        //        else
        //        {
        //            dbConnect.RollBack();
        //        }

        //    }

        //    return ret;
        //}
        
        //public static bool 削除(MsUser loginUser, DmKanriKiroku kanriKiroku)
        //{
        //    bool ret = true;
        //    DateTime 処理日時 = DateTime.Now;
        //    string 更新者 = loginUser.MsUserID;

        //    DmKanriKirokuFile kanriKirokuFile = DmKanriKirokuFile.GetRecordByKanriKirokuID(loginUser, kanriKiroku.DmKanriKirokuID);

        //    using (DBConnect dbConnect = new DBConnect())
        //    {
        //        dbConnect.BeginTransaction();

        //        // 管理記録の更新
        //        kanriKiroku.DeleteFlag = 1;
        //        kanriKiroku.RenewDate = 処理日時;
        //        kanriKiroku.RenewUserID = 更新者;

        //        ret = kanriKiroku.UpdateRecord(dbConnect, loginUser);

        //        if (ret)
        //        {
        //            // 管理記録ファイルの更新
        //            kanriKirokuFile.DeleteFlag = 1;
        //            kanriKirokuFile.RenewDate = 処理日時;
        //            kanriKirokuFile.RenewUserID = 更新者;

        //            ret = kanriKirokuFile.UpdateRecord(dbConnect, loginUser);
        //        }

        //        if (ret)
        //        {
        //            dbConnect.Commit();
        //        }
        //        else
        //        {
        //            dbConnect.RollBack();
        //        }

        //    }

        //    return ret;
        //}

        #endregion
    }
}
