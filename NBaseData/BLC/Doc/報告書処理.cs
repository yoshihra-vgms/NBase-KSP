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
    public class 報告書処理
    {
        private static string 新規ID()
        {
            return System.Guid.NewGuid().ToString();
        }

        public static bool 登録(MsUser loginUser, MsDmHoukokusho houkokusho, MsDmTemplateFile templateFile, List<DmPublisher> publishers, List<DmKoukaiSaki> koukaiSakis)
        {
            bool ret = true;
            DateTime 処理日時 = DateTime.Now;
            string 更新者 = loginUser.MsUserID;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                // 報告書マスタの登録
                houkokusho.MsDmHoukokushoID = 新規ID();
                houkokusho.RenewDate = 処理日時;
                houkokusho.RenewUserID = 更新者;

                ret = houkokusho.InsertRecord(dbConnect, loginUser);

                if (ret)
                {
                    // 雛形ファイルの登録
                    if (templateFile != null && templateFile.TemplateFileName != null && templateFile.TemplateFileName.Length > 0)
                    {
                        templateFile.MsDmTemplateFileID = 新規ID();
                        templateFile.MsDmHoukokushoID = houkokusho.MsDmHoukokushoID;
                        templateFile.RenewDate = 処理日時;
                        templateFile.RenewUserID = 更新者;

                        ret = templateFile.InsertRecord(dbConnect, loginUser);
                    }      
                    if (ret)
                    {
                        // 発行元の登録
                        foreach (DmPublisher publisher in publishers)
                        {
                            publisher.DmPublisherID = 新規ID();
                            publisher.LinkSakiID = houkokusho.MsDmHoukokushoID;
                            publisher.RenewDate = 処理日時;
                            publisher.RenewUserID = 更新者;

                            ret = publisher.InsertRecord(dbConnect, loginUser);

                            if (!ret)
                                break;
                        }
                        if (ret)
                        {
                            // 公開先の登録
                            foreach (DmKoukaiSaki kouakiSaki in koukaiSakis)
                            {
                                kouakiSaki.DmKoukaiSakiID = 新規ID();
                                kouakiSaki.LinkSakiID = houkokusho.MsDmHoukokushoID;
                                kouakiSaki.RenewDate = 処理日時;
                                kouakiSaki.RenewUserID = 更新者;

                                ret = kouakiSaki.InsertRecord(dbConnect, loginUser);

                                if (!ret)
                                    break;
                            }
                        }
                    }
                }

                if (ret && houkokusho.CheckTarget == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                {
                    ret = アラーム登録(dbConnect, loginUser, houkokusho, publishers);
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

        public static bool 更新(MsUser loginUser, MsDmHoukokusho houkokusho, MsDmTemplateFile templateFile, List<DmPublisher> publishers, List<DmKoukaiSaki> koukaiSakis)
        {
            bool ret = true;
            DateTime 処理日時 = DateTime.Now;
            string 更新者 = loginUser.MsUserID;
            MsDmTemplateFile deletedTemplateFile = null;

            //deletedTemplateFile = MsDmTemplateFile.GetRecordByHoukokushoID(loginUser, houkokusho.MsDmHoukokushoID);
            //if (templateFile == null || houkokusho.TemplateFileName == null || houkokusho.TemplateFileName.Length == 0)
            //{
            //    // deletedTemplateFile があれば削除対象
            //}
            //else if (deletedTemplateFile != null)
            //{
            //    if (templateFile.MsDmTemplateFileID != null && templateFile.MsDmTemplateFileID.Length > 0)
            //    {
            //        if (templateFile.MsDmTemplateFileID == deletedTemplateFile.MsDmTemplateFileID)
            //        {
            //            // deletedTemplateFile があって、templateFile とＩＤが一緒なら更新なのでクリアする
            //            deletedTemplateFile = null;
            //        }
            //    }
            //}

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                // 報告書マスタの更新
                houkokusho.RenewDate = 処理日時;
                houkokusho.RenewUserID = 更新者;

                ret = houkokusho.UpdateRecord(dbConnect, loginUser);

                if (ret)
                {
                    // 雛形ファイルの更新
                    string templateFileId = "";
                    if (templateFile != null && templateFile.TemplateFileName != null && templateFile.TemplateFileName.Length > 0)
                    {
                        if (templateFile.MsDmTemplateFileID != null && templateFile.MsDmTemplateFileID.Length > 0)
                        {
                            templateFile.RenewDate = 処理日時;
                            templateFile.RenewUserID = 更新者;

                            ret = templateFile.UpdateRecord(dbConnect, loginUser);
                        }
                        else
                        {
                            templateFile.MsDmTemplateFileID = 新規ID();
                            templateFile.MsDmHoukokushoID = houkokusho.MsDmHoukokushoID;
                            templateFile.RenewDate = 処理日時;
                            templateFile.RenewUserID = 更新者;

                            ret = templateFile.InsertRecord(dbConnect, loginUser);
                        }
                        templateFileId = templateFile.MsDmTemplateFileID;
                    }

                    // 2010.11.08 雛形ファイルは登録されていない場合もあるので、戻り値は無視する
                    //ret = MsDmTemplateFile.LogicalDelete(dbConnect, loginUser, houkokusho.MsDmHoukokushoID, templateFileId);
                    MsDmTemplateFile.LogicalDelete(dbConnect, loginUser, houkokusho.MsDmHoukokushoID, templateFileId);
                    if (ret)
                    {
                        // 更新前の発行元は、すべて削除とする
                        ret = DmPublisher.LogicalDelete(dbConnect, loginUser, houkokusho.MsDmHoukokushoID);

                        if (ret)
                        {
                            // 発行元の登録
                            foreach (DmPublisher publisher in publishers)
                            {
                                publisher.DmPublisherID = 新規ID();
                                publisher.LinkSakiID = houkokusho.MsDmHoukokushoID;
                                publisher.RenewDate = 処理日時;
                                publisher.RenewUserID = 更新者;

                                ret = publisher.InsertRecord(dbConnect, loginUser);

                                if (!ret)
                                    break;
                            }
                            if (ret)
                            {
                                // 更新前の公開先は、すべて削除とする
                                ret = DmKoukaiSaki.LogicalDelete(dbConnect, loginUser, houkokusho.MsDmHoukokushoID);

                                if (ret)
                                {
                                    // 公開先の登録
                                    foreach (DmKoukaiSaki kouakiSaki in koukaiSakis)
                                    {
                                        kouakiSaki.DmKoukaiSakiID = 新規ID();
                                        kouakiSaki.LinkSakiID = houkokusho.MsDmHoukokushoID;
                                        kouakiSaki.RenewDate = 処理日時;
                                        kouakiSaki.RenewUserID = 更新者;

                                        ret = kouakiSaki.InsertRecord(dbConnect, loginUser);

                                        if (!ret)
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                // 2011.05.30: Update 8Lines
                // アラームは必ず一旦削除する、その後、フラグがたっている場合、再度登録を実施するように修正
                //if (ret && houkokusho.CheckTarget == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                //{
                //    ret = アラーム削除(dbConnect, loginUser, houkokusho);
                //    if (ret)
                //    {
                //        ret = アラーム登録(dbConnect, loginUser, houkokusho, publishers);
                //    }
                //}
                if (ret)
                {
                    ret = アラーム削除(dbConnect, loginUser, houkokusho);
                    if (ret && houkokusho.CheckTarget == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                    {
                        ret = アラーム登録(dbConnect, loginUser, houkokusho, publishers);
                    }
                }
                // 2011.05.30: Update End

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
        
        public static bool 削除(MsUser loginUser, MsDmHoukokusho houkokusho)
        {
            bool ret = true;
            DateTime 処理日時 = DateTime.Now;
            string 更新者 = loginUser.MsUserID;

            MsDmTemplateFile templateFile = MsDmTemplateFile.GetRecordByHoukokushoID(loginUser, houkokusho.MsDmHoukokushoID);

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                // 報告書マスタの更新
                houkokusho.DeleteFlag = 1;
                houkokusho.RenewDate = 処理日時;
                houkokusho.RenewUserID = 更新者;

                ret = houkokusho.UpdateRecord(dbConnect, loginUser);

                if (ret)
                {
                    // 雛形ファイルの更新
                    if (templateFile != null && templateFile.TemplateFileName != null && templateFile.TemplateFileName.Length > 0)
                    {
                        templateFile.DeleteFlag = 1;
                        templateFile.RenewDate = 処理日時;
                        templateFile.RenewUserID = 更新者;

                        ret = templateFile.UpdateRecord(dbConnect, loginUser);
                    }

                    if (ret)
                    {
                        // 発行元は、すべて削除とする
                        ret = DmPublisher.LogicalDelete(dbConnect, loginUser, houkokusho.MsDmHoukokushoID);

                        if (ret)
                        {
                            // 公開先は、すべて削除とする
                            ret = DmKoukaiSaki.LogicalDelete(dbConnect, loginUser, houkokusho.MsDmHoukokushoID);
                        }
                    }
                }
                if (ret)
                {
                    ret = アラーム削除(dbConnect, loginUser, houkokusho);
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



        private static bool アラーム登録(DBConnect dbConnect, MsUser loginUser, MsDmHoukokusho houkokusho, List<DmPublisher> publishers)
        {
            bool ret = true;
            DateTime 処理日時 = DateTime.Now;
            string 更新者 = loginUser.MsUserID;
            List<MsVessel> vessels = MsVessel.GetRecordsByDocumentEnabled(loginUser);
            Hashtable hash_vessels = new Hashtable();
            foreach (MsVessel vessel in vessels)
            {
                hash_vessels.Add(vessel.MsVesselID, vessel.VesselName);
            }
            List<MsBumon> bumons = MsBumon.GetRecords(loginUser);
            Hashtable hash_bumons = new Hashtable();
            foreach (MsBumon bumon in bumons)
            {
                hash_bumons.Add(bumon.MsBumonID, bumon.BumonName);
            }
            Hashtable hash_jiki = new Hashtable();
            for (int i = 0; i < 12; i++)
            {
                int chk = int.Parse((houkokusho.Jiki[i]).ToString());
                if (i < 9)
                {
                    hash_jiki.Add(i + 4, chk);
                }
                else
                {
                    hash_jiki.Add(i - 8, chk);
                }
            }

            PtAlarmInfo alarmInfo = new PtAlarmInfo();
            PtDmAlarmInfo alarmDmInfo = new PtDmAlarmInfo();
            MsAlarmDate alarmDate = MsAlarmDate.GetRecord(dbConnect, loginUser, MsAlarmDate.MsAlarmDateIDNo.文書管理アラームID);

            alarmInfo.AlarmShowFlag = (int)NBaseData.DS.DocConstants.FlagEnum.ON;

            alarmInfo.MsPortalInfoShubetuId = ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.文書).ToString();
            alarmInfo.MsPortalInfoKoumokuId = ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.報告書).ToString();
            alarmInfo.MsPortalInfoKubunId = null;
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(dbConnect, loginUser, alarmInfo.MsPortalInfoShubetuId, alarmInfo.MsPortalInfoKoumokuId, alarmInfo.MsPortalInfoKubunId);

            alarmInfo.SanshoumotoId = null;
            //alarmInfo.Yuukoukigen = null;
            alarmInfo.Naiyou = infoFormat.Naiyou;
            alarmInfo.AlarmShowFlag = (int)PtAlarmInfo.AlarmShowFlagEnum.アラームＯＮ;
            alarmInfo.RenewUserID = 更新者;
            alarmInfo.RenewDate = 処理日時;

            alarmDmInfo.MsDmHoukokushoID = houkokusho.MsDmHoukokushoID;
            alarmDmInfo.RenewUserID = 更新者;
            alarmDmInfo.RenewDate = 処理日時;

            foreach (DmPublisher publisher in publishers)
            {
                DateTime thisMonth = DateTime.Parse(DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString("00") + "/01");
                for (int i = 0; i < 12; i++)
                {
                    int trgYear = thisMonth.Year;
                    int trgMonth = thisMonth.Month + i;
                    if (trgMonth > 12)
                    {
                        trgMonth -= 12;
                        trgYear++; 
                    }
                    if ((int)hash_jiki[trgMonth] == (int)NBaseData.DS.DocConstants.FlagEnum.OFF)
                    {
                        continue;
                    }

                    DateTime hasseiDate = thisMonth.AddMonths(i + 1).AddDays(-1); // 月末を求める
                    alarmInfo.HasseiDate = hasseiDate.AddDays(alarmDate.DayOffset); // 提出時期の月末 ＋ アラーム設定日

                    alarmInfo.MsVesselId = 0;
                    alarmInfo.VesselID = 0;
                    if (publisher.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船)
                    {
                        string vesselName = "";
                        if (hash_vessels.ContainsKey(publisher.MsVesselID))
                        {
                            vesselName = hash_vessels[publisher.MsVesselID] as string;
                        }
                        alarmInfo.Shousai = String.Format(infoFormat.Shousai, vesselName, houkokusho.BunruiName, houkokusho.BunshoName);
                        alarmInfo.MsVesselId = publisher.MsVesselID;
                        alarmInfo.VesselID = publisher.MsVesselID;
                    }
                    else if (publisher.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.部門)
                    {
                        string bumonName = "";
                        if (hash_bumons.ContainsKey(publisher.MsBumonID))
                        {
                            bumonName = hash_bumons[publisher.MsBumonID] as string;
                        }
                        alarmInfo.Shousai = String.Format(infoFormat.Shousai, bumonName, houkokusho.BunruiName, houkokusho.BunshoName);
                    }
                    #region 20210824 下記に変更
                    //else if (publisher.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.会長社長)
                    //{
                    //    alarmInfo.Shousai = String.Format(infoFormat.Shousai, "会長社長", houkokusho.BunruiName, houkokusho.BunshoName);
                    //}
                    //else if (publisher.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者)
                    //{
                    //    alarmInfo.Shousai = String.Format(infoFormat.Shousai, "管理責任者", houkokusho.BunruiName, houkokusho.BunshoName);
                    //}
                    #endregion
                    else
                    {
                        alarmInfo.Shousai = String.Format(infoFormat.Shousai, DocConstants.ClassName(publisher.KoukaiSaki), houkokusho.BunruiName, houkokusho.BunshoName);
                    }

                    List<PtDmAlarmInfo> checkDmAlarmInfos = PtDmAlarmInfo.GetSameRecords(dbConnect, loginUser, houkokusho.MsDmHoukokushoID, trgYear, trgMonth, publisher.KoukaiSaki, publisher.MsVesselID, publisher.MsBumonID);
                    if (checkDmAlarmInfos.Count == 0)
                    {
                        alarmInfo.PtAlarmInfoId = 新規ID();
                        ret = alarmInfo.InsertRecord(dbConnect, loginUser);
                        if (!ret)
                        {
                            break;
                        }

                        alarmDmInfo.PtDmAlarmInfoID = 新規ID();
                        alarmDmInfo.PtAlarmInfoId = alarmInfo.PtAlarmInfoId;
                        alarmDmInfo.KoukaiSaki = publisher.KoukaiSaki;
                        alarmDmInfo.MsVesselID = publisher.MsVesselID;
                        alarmDmInfo.MsBumonID = publisher.MsBumonID;
                        alarmDmInfo.JikiNen = trgYear;
                        alarmDmInfo.JikiTuki = trgMonth;
                        alarmDmInfo.VesselID = publisher.MsVesselID;
                        ret = alarmDmInfo.InsertRecord(dbConnect, loginUser);
                        if (!ret)
                        {
                            break;
                        }
                    }
                    else
                    {
                        PtDmAlarmInfo pdai = checkDmAlarmInfos[0]; // １番目のものを再利用

                        List<PtAlarmInfo> checkAlarmInfos = PtAlarmInfo.GetSameRecords(dbConnect, loginUser, pdai.PtAlarmInfoId);
                        if (checkAlarmInfos.Count == 0)
                        {
                            alarmInfo.PtAlarmInfoId = 新規ID();
                            ret = alarmInfo.InsertRecord(dbConnect, loginUser);
                            if (!ret)
                            {
                                break;
                            }

                            pdai.PtAlarmInfoId = alarmInfo.PtAlarmInfoId;
                            pdai.DeleteFlag = (int)PtAlarmInfo.CancelFlagEnum.ＯＦＦ;
                            pdai.RenewUserID = 更新者;
                            pdai.RenewDate = 処理日時;
                            ret = pdai.UpdateRecord(dbConnect, loginUser);
                            if (!ret)
                            {
                                break;
                            }
                        }
                        else
                        {
                            PtAlarmInfo pai = checkAlarmInfos[0]; // １番目のものを再利用
                            if (pai.AlarmShowFlag == (int)PtAlarmInfo.AlarmShowFlagEnum.アラームＯＦＦ)
                            {
                                // アラーム停止している場合、削除フラグを立てて
                                pai.DeleteFlag = (int)PtAlarmInfo.CancelFlagEnum.ＯＮ;
                                pai.RenewUserID = 更新者;
                                pai.RenewDate = 処理日時;
                                pai.UpdateRecord(dbConnect, loginUser);
                                if (!ret)
                                {
                                    break;
                                }

                                // 新たに PT_ALARM_INFO を登録
                                alarmInfo.PtAlarmInfoId = 新規ID();
                                ret = alarmInfo.InsertRecord(dbConnect, loginUser);
                                if (!ret)
                                {
                                    break;
                                }

                                pdai.PtAlarmInfoId = alarmInfo.PtAlarmInfoId;
                                pdai.DeleteFlag = (int)PtAlarmInfo.CancelFlagEnum.ＯＦＦ;
                                pdai.RenewUserID = 更新者;
                                pdai.RenewDate = 処理日時;
                                ret = pdai.UpdateRecord(dbConnect, loginUser);
                                if (!ret)
                                {
                                    break;
                                }

                            }
                            else
                            {
                                // アラーム停止していない場合、そのレコードを再利用
                                pai.HasseiDate = alarmInfo.HasseiDate;
                                pai.DeleteFlag = (int)PtAlarmInfo.CancelFlagEnum.ＯＦＦ;
                                pai.RenewUserID = 更新者;
                                pai.RenewDate = 処理日時;
                                pai.UpdateRecord(dbConnect, loginUser);
                                if (!ret)
                                {
                                    break;
                                }

                                pdai.PtAlarmInfoId = pai.PtAlarmInfoId;
                                pdai.DeleteFlag = (int)PtAlarmInfo.CancelFlagEnum.ＯＦＦ;
                                pdai.RenewUserID = 更新者;
                                pdai.RenewDate = 処理日時;
                                ret = pdai.UpdateRecord(dbConnect, loginUser);
                                if (!ret)
                                {
                                    break;
                                }
                            }
                        }
                    }


                }
                if (!ret)
                {
                    break;
                }
            }
            return ret;
        }

        private static bool アラーム削除(DBConnect dbConnect, MsUser loginUser, MsDmHoukokusho houkokusho)
        {
            bool ret = true;

            PtAlarmInfo.LogicalDelete(dbConnect, loginUser, houkokusho.MsDmHoukokushoID, DateTime.Today.Year, DateTime.Today.Month);
            PtDmAlarmInfo.LogicalDelete(dbConnect, loginUser, houkokusho.MsDmHoukokushoID, DateTime.Today.Year, DateTime.Today.Month);
            return ret;
        }
    }
}
