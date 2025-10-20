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
using NBaseUtil;
using NBaseHonsen.util;
using SyncClient;

namespace NBaseHonsen.Document.BLC
{
    public class 管理記録処理
    {
        private static string 新規ID()
        {
            return System.Guid.NewGuid().ToString();
        }

        public static bool Honsen登録(MsUser loginUser, DmKanriKiroku kanriKiroku, DmKanriKirokuFile kanriKirokuFile, List<DmPublisher> publishers)
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

                ret = SyncTableSaver.InsertOrUpdate2(kanriKiroku, loginUser, StatusUtils.通信状況.未同期, dbConnect);

                if (ret)
                {
                    // 管理記録ファイルの登録
                    kanriKirokuFile.DmKanriKirokuFileID = 新規ID();
                    kanriKirokuFile.DmKanriKirokuID = kanriKiroku.DmKanriKirokuID;
                    kanriKirokuFile.RenewDate = 処理日時;
                    kanriKirokuFile.RenewUserID = 更新者;

                    ret = SyncTableSaver.InsertOrUpdate2(kanriKirokuFile, loginUser, StatusUtils.通信状況.未同期, dbConnect);

                    if (ret)
                    {
                        // 発行元の登録
                        foreach (DmPublisher publisher in publishers)
                        {
                            publisher.DmPublisherID = 新規ID();
                            publisher.LinkSakiID = kanriKiroku.DmKanriKirokuID;
                            publisher.RenewDate = 処理日時;
                            publisher.RenewUserID = 更新者;

                            ret = SyncTableSaver.InsertOrUpdate2(publisher, loginUser, StatusUtils.通信状況.未同期, dbConnect);

                            if (!ret)
                                break;
                        }

                        // 公開先の登録（公開先は報告書マスタに設定されているものをコピーする）
                        // 公開先 = 船 の場合、自船以外は公開先としない
                        // 公開先 != 船 は、すべて有効な公開先とする
                        foreach (DmKoukaiSaki houKKS in houkokushoKoukaisakis)
                        {
                            if (houKKS.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船 &&
                                houKKS.MsVesselID != 同期Client.LOGIN_VESSEL.MsVesselID)
                            {
                                continue;
                            }
                            houKKS.DmKoukaiSakiID = 新規ID();
                            houKKS.LinkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                            houKKS.LinkSakiID = kanriKiroku.DmKanriKirokuID;
                            houKKS.RenewDate = 処理日時;
                            houKKS.RenewUserID = 更新者;

                            ret = SyncTableSaver.InsertOrUpdate2(houKKS, loginUser, StatusUtils.通信状況.未同期, dbConnect);
                            if (!ret)
                            {
                                break;
                            }
                        }
                    }
                }

                // 2011.07.12 アラーム処理は、NBaseHonsenServer側に移行
                //if (ret)
                //{
                //    ret = アラーム登録(dbConnect, loginUser, kanriKiroku);
                //}

                if (ret)
                {
                    PtHonsenkoushinInfo honsenkoushinInfo = 本船更新情報.CreateInstance(kanriKiroku, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.管理文書登録);
                    ret = SyncTableSaver.InsertOrUpdate2(honsenkoushinInfo, loginUser, StatusUtils.通信状況.未同期, dbConnect);
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
        public static bool Honsen更新(MsUser loginUser, DmKanriKiroku kanriKiroku, DmKanriKirokuFile kanriKirokuFile, List<DmPublisher> publishers)
        {
            bool ret = true;
            DateTime 処理日時 = DateTime.Now;
            string 更新者 = loginUser.MsUserID;

            List<DmPublisher> org_publishers = DmPublisher.GetRecordsByLinkSakiID(loginUser, kanriKiroku.DmKanriKirokuID);

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                // 管理記録の登録
                kanriKiroku.RenewDate = 処理日時;
                kanriKiroku.RenewUserID = 更新者;

                ret = SyncTableSaver.InsertOrUpdate2(kanriKiroku, loginUser, StatusUtils.通信状況.未同期, dbConnect);

                if (ret)
                {
                    // 管理記録ファイルの登録
                    kanriKirokuFile.DmKanriKirokuID = kanriKiroku.DmKanriKirokuID;
                    kanriKirokuFile.RenewDate = 処理日時;
                    kanriKirokuFile.RenewUserID = 更新者;

                    ret = SyncTableSaver.InsertOrUpdate2(kanriKirokuFile, loginUser, StatusUtils.通信状況.未同期, dbConnect);

                    if (ret)
                    {
                        // 元の発行元は削除
                        foreach (DmPublisher publisher in org_publishers)
                        {
                            publisher.DeleteFlag = (int)NBaseData.DS.DocConstants.FlagEnum.ON;
                            publisher.RenewDate = 処理日時;
                            publisher.RenewUserID = 更新者;

                            ret = SyncTableSaver.InsertOrUpdate2(publisher, loginUser, StatusUtils.通信状況.未同期, dbConnect);

                            if (!ret)
                                break;
                        }

                        // 発行元の登録
                        foreach (DmPublisher publisher in publishers)
                        {
                            publisher.DmPublisherID = 新規ID();
                            publisher.LinkSakiID = kanriKiroku.DmKanriKirokuID;
                            publisher.RenewDate = 処理日時;
                            publisher.RenewUserID = 更新者;

                            ret = SyncTableSaver.InsertOrUpdate2(publisher, loginUser, StatusUtils.通信状況.未同期, dbConnect);

                            if (!ret)
                                break;
                        }
                    }
                }

                // 2011.07.12 アラーム処理は、NBaseHonsenServer側に移行
                //if (ret)
                //{
                //    ret = アラーム登録(dbConnect, loginUser, kanriKiroku);
                //}

                if (ret)
                {
                    PtHonsenkoushinInfo honsenkoushinInfo = 本船更新情報.CreateInstance(kanriKiroku, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.管理文書登録);
                    ret = SyncTableSaver.InsertOrUpdate2(honsenkoushinInfo, loginUser, StatusUtils.通信状況.未同期, dbConnect);
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
        public static bool Honsen更新(MsUser loginUser, DmKanriKiroku kanriKiroku)
        {
            bool ret = true;
            DateTime 処理日時 = DateTime.Now;
            string 更新者 = loginUser.MsUserID;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                // 管理記録の登録
                kanriKiroku.RenewDate = 処理日時;
                kanriKiroku.RenewUserID = 更新者;

                ret = SyncTableSaver.InsertOrUpdate2(kanriKiroku, loginUser, StatusUtils.通信状況.未同期, dbConnect);

                //if (ret)
                //{
                //    PtHonsenkoushinInfo honsenkoushinInfo = 本船更新情報.CreateInstance(kanriKiroku, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.管理文書登録);
                //    ret = SyncTableSaver.InsertOrUpdate2(honsenkoushinInfo, loginUser, StatusUtils.通信状況.未同期, dbConnect);
                //}

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

        // 2011.07.12 アラーム処理は、NBaseHonsenServer側に移行
        #region
        //private static bool アラーム登録(DBConnect dbConnect, MsUser loginUser, DmKanriKiroku kanriKiroku)
        //{
        //    bool ret = true;
        //    DateTime 処理日時 = DateTime.Now;
        //    string 更新者 = loginUser.MsUserID;

        //    PtAlarmInfo alarmInfo = null;
        //    PtDmAlarmInfo alarmDmInfo = PtDmAlarmInfo.GetRecord(loginUser, kanriKiroku.MsDmHoukokushoID, kanriKiroku.JikiNen, kanriKiroku.JikiTuki, (int)NBaseData.DS.DocConstants.RoleEnum.船, 同期Client.LOGIN_VESSEL.MsVesselID);
        //    if (alarmDmInfo == null)
        //    {
        //        return true;
        //    }

        //    // アラーム停止
        //    alarmInfo = PtAlarmInfo.GetRecordsByPtAlarmInfoId(loginUser, alarmDmInfo.PtAlarmInfoId);
        //    if (alarmInfo == null)
        //    {
        //        return true;
        //    }
        //    alarmInfo.AlarmShowFlag = (int)PtAlarmInfo.AlarmShowFlagEnum.アラームＯＦＦ;
        //    alarmInfo.AlarmStopDate = 処理日時;
        //    alarmInfo.AlarmStopUser = 更新者;
        //    alarmInfo.RenewDate = 処理日時;
        //    alarmInfo.RenewUserID = 更新者;
        //    ret = SyncTableSaver.InsertOrUpdate2(alarmInfo, loginUser, StatusUtils.通信状況.未同期, dbConnect);
        //    if (!ret)
        //    {
        //        return ret;
        //    }

        //    alarmDmInfo.DeleteFlag = (int)NBaseData.DS.DocConstants.FlagEnum.ON;
        //    alarmDmInfo.RenewDate = 処理日時;
        //    alarmDmInfo.RenewUserID = 更新者;
        //    ret = SyncTableSaver.InsertOrUpdate2(alarmDmInfo, loginUser, StatusUtils.通信状況.未同期, dbConnect);
        //    if (!ret)
        //    {
        //        return ret;
        //    }

        //    // 1年後の分を作成
        //    alarmInfo.PtAlarmInfoId = 新規ID();
        //    alarmInfo.AlarmShowFlag = (int)PtAlarmInfo.AlarmShowFlagEnum.アラームＯＮ;
        //    alarmInfo.HasseiDate = alarmInfo.HasseiDate.AddYears(1); // 1年後
        //    alarmInfo.RenewUserID = 更新者;
        //    alarmInfo.RenewDate = 処理日時;

        //    ret = SyncTableSaver.InsertOrUpdate2(alarmInfo, loginUser, StatusUtils.通信状況.未同期, dbConnect);
        //    if (!ret)
        //    {
        //        return ret;
        //    }
        //    alarmDmInfo.PtDmAlarmInfoID = 新規ID();
        //    alarmDmInfo.PtAlarmInfoId = alarmInfo.PtAlarmInfoId;
        //    alarmDmInfo.JikiNen = alarmDmInfo.JikiNen + 1;
        //    alarmDmInfo.DeleteFlag = (int)NBaseData.DS.DocConstants.FlagEnum.OFF;
        //    alarmDmInfo.RenewDate = 処理日時;
        //    alarmDmInfo.RenewUserID = 更新者;
        //    ret = SyncTableSaver.InsertOrUpdate2(alarmDmInfo, loginUser, StatusUtils.通信状況.未同期, dbConnect);
        //    if (!ret)
        //    {
        //        return ret;
        //    }

        //    return ret;
        //}
        #endregion
    }
}
