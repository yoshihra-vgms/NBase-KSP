using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.IO;
using System.Security.Cryptography;

using Npgsql;

using NBaseHonsenService.DAC;
using NBaseCommon;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;
using NBaseData.BLC;
using ORMapping;
using WtmData;
using WtmModelBase;

namespace NBaseHonsenService
{
    // メモ: ここでクラス名 "Service1" を変更する場合は、Web.config および関連する .svc ファイルで "Service1" への参照も更新する必要があります。
    public class Service1 : IService1
    {
        private string gVesselName;
        private string gHostName;

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        #region 2012.04.10 旧Ver
        //public string データ同期(
        //    int vesselSchemaVersion, 
        //    string xml, 
        //    decimal maxDataNoOfVesselIdZero, 
        //    decimal maxDataNo, 
        //    int vesselId,
        //    string hostName,
        //    string modueVersion,
        //    string userId,
        //    DateTime vesselDate
        //    )
        //{
        //    string returnXml = null;

        //    //================================
        //    // 同期状況データ更新
        //    //================================
        //    int vesselIdZero = int.Parse(Common.共通船番号);
        //    MsUser user = new MsUser();
        //    user.MsUserID = userId;
        //    SnSyncInfo syncInfo = null;
        //    try
        //    {
        //        syncInfo = SnSyncInfo.GetRecord(user, hostName);
        //        if (syncInfo == null)
        //        {
        //            syncInfo = new SnSyncInfo();
        //        }
        //        if (vesselId != vesselIdZero)
        //        {
        //            syncInfo.MsVesselID = vesselId;
        //        }
        //        syncInfo.MsUserID = userId;
        //        syncInfo.MaxDataNoOfVesselIdZero = (Int64)maxDataNoOfVesselIdZero;
        //        syncInfo.MaxDataNo = (Int64)maxDataNo;
        //        syncInfo.VesselDate = vesselDate;
        //        syncInfo.ModuleVersion = modueVersion;
        //        syncInfo.SyncStep = (int)SnSyncInfo.同期進捗.同期開始;
        //        if (syncInfo.HostName == null || syncInfo.HostName.Length == 0)
        //        {
        //            syncInfo.HostName = hostName;

        //            syncInfo.InsertRecord(user);
        //        }
        //        else
        //        {
        //            syncInfo.UpdateRecord(user);
        //        }
        //    }
        //    catch
        //    {
        //    }

        //    //================================
        //    // 受信XMLを保存する
        //    //================================
        //    try
        //    {
        //        ServiceSpecificDAC.InsertSnMessageLog(vesselId, ServiceSpecificDAC.SnMessageLogMessageType.受信, xml);
        //    }
        //    catch (Exception e)
        //    {
        //        WriteExceptionLog(e.Message);
        //    }


        //    //================================
        //    // 同期実処理
        //    //================================
        //    bool isReceiveSyncData = false;
        //    bool isSendSyncData = false;
        //    try
        //    {
        //        // 送信データから TRANSACTION_TOKEN を生成する.
        //        string token = StringUtils.CreateHash(xml);

        //        if (!ServiceSpecificDAC.ExistsTransactionToken(token))
        //        {
        //            // 船のデータを登録する
        //            isReceiveSyncData = InsertSendedData(user, vesselId, xml, token);
        //        }
        //        //================================
        //        // 同期状況データ更新
        //        //================================
        //        try
        //        {
        //            syncInfo.SyncStep = (int)SnSyncInfo.同期進捗.船データ登録;
        //            if (isReceiveSyncData)
        //            {
        //                syncInfo.FromVesselFlag = (int)SnSyncInfo.データFLAG.あり;
        //            }
        //            else
        //            {
        //                syncInfo.FromVesselFlag = (int)SnSyncInfo.データFLAG.なし;
        //            }
        //            syncInfo.UpdateRecord(user);
        //        }
        //        catch
        //        {
        //        }

        //        // サーバの Schema バージョンを取得する.
        //        int serverDbSchemaVersion = this.GetSchemaVersion();

        //        // 船DBとスキーマが異なる場合、サーバの Schema バージョンのみを返す.
        //        if (vesselSchemaVersion != serverDbSchemaVersion)
        //        {
        //            returnXml = "<DbVersion> " + serverDbSchemaVersion + "</DbVersion>";
        //        }
        //        // スキーマが同一の場合、船との差分データを取得する
        //        else
        //        {
        //            returnXml = this.BuildReturnData(maxDataNoOfVesselIdZero, maxDataNo, vesselId, ref isSendSyncData);
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        WriteExceptionLog(e.Message);
        //    }


        //    //================================
        //    // 送信XMLを保存する
        //    //================================
        //    try
        //    {
        //        ServiceSpecificDAC.InsertSnMessageLog(vesselId, ServiceSpecificDAC.SnMessageLogMessageType.送信, returnXml);
        //    }
        //    catch (Exception e)
        //    {
        //        WriteExceptionLog(e.Message);
        //    }

        //    //================================
        //    // 同期状況データ更新
        //    //================================
        //    try
        //    {
        //        syncInfo.ServerDate = DateTime.Now;
        //        syncInfo.SyncStep = (int)SnSyncInfo.同期進捗.サーバデータ送信;
        //        if (isSendSyncData)
        //        {
        //            syncInfo.FromServerFlag = (int)SnSyncInfo.データFLAG.あり;
        //        }
        //        else
        //        {
        //            syncInfo.FromServerFlag = (int)SnSyncInfo.データFLAG.なし;
        //        }
        //        syncInfo.UpdateRecord(user);
        //    }
        //    catch
        //    {
        //    }

        //    return returnXml;
        //}
        #endregion
        public string データ同期(
            int vesselSchemaVersion,
            string xml,
            decimal maxDataNoOfVesselIdZero,
            decimal maxDataNo,
            int vesselId,
            string hostName,
            string modueVersion,
            string userId,
            DateTime vesselDate,
            int curNo, int maxNo
            , int counter // 同期改善
            )
        {

            //LogFile.Write("","データ同期");

            string returnXml = null;

            //================================
            // 同期状況データ更新
            //================================
            int vesselIdZero = int.Parse(Common.共通船番号);
            MsUser user = new MsUser();
            if (userId != null)
                user = MsUser.GetRecordsByUserID(user, userId);
            user.MsUserID = userId;

            SnSyncInfo syncInfo = null;
            try
            {
                if (curNo == 0)
                {
                    syncInfo = SnSyncInfo.GetRecord(user, hostName);
                    if (syncInfo == null)
                    {
                        syncInfo = new SnSyncInfo();
                    }
                    if (vesselId != vesselIdZero)
                    {
                        syncInfo.MsVesselID = vesselId;
                    }
                    syncInfo.MsUserID = userId;
                    syncInfo.MaxDataNoOfVesselIdZero = (Int64)maxDataNoOfVesselIdZero;
                    syncInfo.MaxDataNo = (Int64)maxDataNo;
                    syncInfo.VesselDate = vesselDate;
                    syncInfo.ModuleVersion = modueVersion;
                    syncInfo.SyncStep = (int)SnSyncInfo.同期進捗.同期開始;
                    if (syncInfo.HostName == null || syncInfo.HostName.Length == 0)
                    {
                        syncInfo.HostName = hostName;

                        syncInfo.InsertRecord(user);
                    }
                    else
                    {
                        syncInfo.UpdateRecord(user);
                    }
                }


                // ===== LOG =====
                if (vesselId != vesselIdZero)
                {
                    MsVessel logVessel = MsVessel.GetRecordByMsVesselID(user, vesselId);
                    if (logVessel != null)
                        gVesselName = logVessel.VesselName;
                }
                gHostName = hostName;
                string LogStr = "データ同期:";
                LogStr += "VesselName[" + gVesselName + "]:";
                LogStr += "HostName[" + gHostName + "]:";
                LogStr += "ModuleVersion[" + modueVersion + "]:";
                LogStr += "MaxDataNoOfVesselIdZero[" + maxDataNoOfVesselIdZero.ToString() + "]:";
                LogStr += "maxDataNo[" + maxDataNo.ToString() + "]:";
                LogStr += "CurNo[" + curNo.ToString() + "]:";
                LogStr += "MaxNo[" + maxNo.ToString() + "]:";
                LogStr += "Counter[" + counter.ToString() + "]";

                LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);
            }
            catch
            {
            }

            #region
            //================================
            // 受信XMLを保存する
            //================================

            // 2011.08.22 エラーが発生するので、とりあえずコメントとし
            //            後ほど、調査を実施する

            //try
            //{
            //    ServiceSpecificDAC.InsertSnMessageLog(vesselId, ServiceSpecificDAC.SnMessageLogMessageType.受信, xml);
            //}
            //catch (Exception e)
            //{
            //    WriteExceptionLog(e.Message);
            //}
            #endregion

            //================================
            // 同期実処理
            //================================
            bool isReceiveSyncData = false;
            bool isSendSyncData = false;
            try
            {
                if (curNo == 0)
                {
                    // 送信データから TRANSACTION_TOKEN を生成する.
                    string token = StringUtils.CreateHash(xml);

                    if (!ServiceSpecificDAC.ExistsTransactionToken(token))
                    {
                        // ===== LOG =====
                        string LogStr = "";
                        LogStr += "VesselName[" + gVesselName + "]:";
                        LogStr += "HostName[" + gHostName + "]:";
                        LogStr += "船からのデータを登録する";
                        LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);

                        // 船のデータを登録する
                        isReceiveSyncData = InsertSendedData(user, vesselId, xml, token, 0);
                    }
                    else
                    {
                        DataSet ds = new DataSet();
                        using (StringReader xmlSR = new StringReader(xml))
                        {
                            ds.ReadXml(xmlSR);
                        }

                        // ===== LOG =====
                        string LogStr = "";
                        LogStr += "VesselName[" + gVesselName + "]:";
                        LogStr += "HostName[" + gHostName + "]:";
                        if (ds.Tables.Count == 0)
                        {
                            LogStr += "船からのデータがない";
                            LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);
                        }
                        else
                        {
                            // 船からのデータが登録済みとマークされているのに
                            // 船から送信されてきているということは、
                            // 船⇒サーバときているが、戻りでサーバ⇒船へとデータがいっていないということか？
                            // したがって、SendFlagが更新されないため、船で未送信となったままになっている

                            LogStr += "船からのデータは登録済み";
                            LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);

                            Debug(user, vesselId, xml, token, 0);

                            InsertSendedData2(user, vesselId, xml, token, 0);
                        }
                    }



                    //================================
                    // 同期状況データ更新
                    //================================
                    try
                    {
                        syncInfo.SyncStep = (int)SnSyncInfo.同期進捗.船データ登録;
                        if (isReceiveSyncData)
                        {
                            syncInfo.FromVesselFlag = (int)SnSyncInfo.データFLAG.あり;
                        }
                        else
                        {
                            syncInfo.FromVesselFlag = (int)SnSyncInfo.データFLAG.なし;
                        }
                        syncInfo.UpdateRecord(user);
                    }
                    catch
                    {
                    }
                }

                // サーバの Schema バージョンを取得する.
                int serverDbSchemaVersion = this.GetSchemaVersion();

                // 船DBとスキーマが異なる場合、サーバの Schema バージョンのみを返す.
                if (vesselSchemaVersion != serverDbSchemaVersion)
                {
                    returnXml = "<DbVersion> " + serverDbSchemaVersion + "</DbVersion>";
                }
                // スキーマが同一の場合、船との差分データを取得する
                else
                {
                    returnXml = this.BuildReturnData(user,maxDataNoOfVesselIdZero, maxDataNo, vesselId, ref isSendSyncData, curNo, maxNo, counter);
                }

            }
            catch (Exception e)
            {
                WriteExceptionLog(e.Message);

                // ===== LOG =====
                string LogStr = "　";
                LogStr += "VesselName[" + gVesselName + "]:";
                LogStr += "HostName[" + gHostName + "]:";
                LogStr += "データ同期 Exception :" + e.Message;
                LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);
            }

            #region
            //================================
            // 送信XMLを保存する
            //================================
            // 2011.08.22 エラーが発生するので、とりあえずコメントとし
            //            後ほど、調査を実施する
            //try
            //{
            //    ServiceSpecificDAC.InsertSnMessageLog(vesselId, ServiceSpecificDAC.SnMessageLogMessageType.送信, returnXml);
            //}
            //catch (Exception e)
            //{
            //    WriteExceptionLog(e.Message);
            //}
            #endregion

            //================================
            // 同期状況データ更新
            //================================
            try
            {
                if (curNo+1 == maxNo)
                {
                    syncInfo = SnSyncInfo.GetRecord(user, hostName);
                    syncInfo.ServerDate = DateTime.Now;
                    syncInfo.SyncStep = (int)SnSyncInfo.同期進捗.サーバデータ送信;
                    if (isSendSyncData)
                    {
                        syncInfo.FromServerFlag = (int)SnSyncInfo.データFLAG.あり;
                    }
                    else
                    {
                        syncInfo.FromServerFlag = (int)SnSyncInfo.データFLAG.なし;
                    }
                    syncInfo.UpdateRecord(user);
                }
            }
            catch
            {
            }

            return returnXml;
        }

        
        /// <summary>
        /// 船からの受信データをＤＢに登録する
        /// </summary>
        /// <param name="user"></param>
        /// <param name="vesselId"></param>
        /// <param name="xml"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        #region private bool InsertSendedData(MsUser user, int vesselId, string xml, string token, int logFlag)
        private bool InsertSendedData(MsUser user, int vesselId, string xml, string token, int logFlag)
        {
            bool isExistsSyncData = false;

            try
            {
                DataSet ds = new DataSet();
                using (StringReader xmlSR = new StringReader(xml))
                {
                    ds.ReadXml(xmlSR);
                }

                // メールを送信する OD_THI.
                List<OdThi> sendMailOdThis = new List<OdThi>();

                // 本船更新情報に手配依頼番号を追加する OD_THI.
                Dictionary<string, string> odThis = new Dictionary<string, string>();
                Dictionary<string, string> delOdThis = new Dictionary<string, string>();
                List<PtHonsenkoushinInfo> honsenKoushinInfos = new List<PtHonsenkoushinInfo>();

                List<DmKanriKiroku> stopAlarmKKs = new List<DmKanriKiroku>();

                // メールを送信する DJ_DOUSEI_HOUKOKU.
                List<DjDouseiHoukoku> sendMailDjHoukokus = new List<DjDouseiHoukoku>();
            
                // クラスにマッピング
                foreach (ISyncTable m in MappingClass2.ToModel(ds))
                {
                    if (SyncTableSaver.ValidateUserKey(m))
                    {
                        if (user == null)
                        {
                            user = new MsUser();
                            user.MsUserID = m.RenewUserID;
                        }

                        if (m is PtHonsenkoushinInfo)
                        {
                            honsenKoushinInfos.Add(m as PtHonsenkoushinInfo);

                            isExistsSyncData = true;
                        }
                        else
                        {
                            //WTM連携
                            if (System.Configuration.ConfigurationManager.AppSettings["WTMLinkageFlag"] == "1")
                            {
                                if (m is SiCard)
                                {
                                    if ((m as SiCard).MsSiShubetsuID == 1) // 乗船の場合のみ連携対象とする
                                    {
                                        var link = SiLinkShokumeiCard.GetRecordsBySiCardID(user, (m as SiCard).SiCardID);
                                        if (link != null && link.Count() > 0)
                                        {
                                            var card = (m as SiCard);
                                            card.CardMsSiShokumeiID = link[0].MsSiShokumeiID;
                                            WTMLinkageProc.CreateSignOnOffLinkFile((m as SiCard));


                                            LogFile.NBaseHonsenServiceLogWrite(user.FullName, $"<<SiCard登録>> Card->[{card.SiCardID}],WtmLinkageID->[{card.WTMLinkageID}]");
                                        }
                                    }
                                }
                                if (m is SiLinkShokumeiCard)
                                {
                                    var card = SiCard.GetRecord(user, (m as SiLinkShokumeiCard).SiCardID);
                                    if (card != null && card.MsSiShubetsuID == 1) // 乗船の場合のみ連携対象とする
                                    {
                                        card.CardMsSiShokumeiID = (m as SiLinkShokumeiCard).MsSiShokumeiID;
                                        WTMLinkageProc.CreateSignOnOffLinkFile(card);


                                        LogFile.NBaseHonsenServiceLogWrite(user.FullName, $"<<SiLinkShokumeiCard登録>> Card->[{card.SiCardID}],WtmLinkageID->[{card.WTMLinkageID}]");

                                        var updateRet = card.UpdateRecord(null, user);
                                        LogFile.NBaseHonsenServiceLogWrite(user.FullName, $"                           updateRet->[{updateRet}]");
                                    }
                                }
                            }

                            // ＤＢに登録する
                            long ret = SyncTableSaver.InsertOrUpdate(m, user, StatusUtils.通信状況.同期済);
                            if (m is SiCard)
                            {
                                LogFile.NBaseHonsenServiceLogWrite(user.FullName, $"<<SiCard登録>> Card->[{(m as SiCard).SiCardID}],ret->[{ret}]");
                            }

                            // ===== LOG =====
                            string LogStr = "　";
                            LogStr += logFlag == 0 ? "ﾃﾞｰﾀ同期  " : logFlag == 1 ?"文書ﾃﾞｰﾀ同期" : "添付ﾃﾞｰﾀ同期";
                            LogStr += "VesselName[" + gVesselName + "]:";
                            LogStr += "HostName[" + gHostName + "]:";
                            LogStr += "InsertOrUpdate[" + m.GetType().Name + "]:";
                            LogStr += "ret[" + ret.ToString() + "]:";
                            LogStr += "id[" + GetID(m) + "]:";
                            LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);

                            #region

                            if (ret > -1)
                            {

                                if (m is SiCard)
                                {
                                    SiCard c = (m as SiCard);
                                    SiBoardingSchedule schedule = null;
                                    if (c.EndDate == DateTime.MinValue)
                                    {
                                        // 乗船
                                        List<SiBoardingSchedule> tmp = SiBoardingSchedule.GetRecordsByPlan(null, user);
                                        if (tmp.Count() > 0 && tmp.Any(obj => obj.MsSeninID == c.MsSeninID))
                                        {
                                            schedule = tmp.Where(obj => obj.MsSeninID == c.MsSeninID).First();
                                        }

                                        //if (c.VesselName == null || c.VesselName.Length == 0)
                                        //{
                                        //    MsVessel vessel = MsVessel.GetRecordByMsVesselID(user, c.MsVesselID);

                                        //    SiCard upCard = SiCard.GetRecord(user, c.SiCardID);
                                        //    upCard.VesselName = vessel.VesselName;
                                        //    upCard.CompanyName = "川崎近海汽船";
                                        //    upCard.MsCrewMatrixTypeID = vessel.MsCrewMatrixTypeID;

                                        //    upCard.UpdateRecord(null, user);
                                        //}
                                    }
                                    else
                                    {
                                        // 下船
                                        schedule = SiBoardingSchedule.GetRecordBySignOffSiCardID(null, user, c.SiCardID);
                                    }
                                    if (schedule != null)
                                    {
                                        schedule.Status = 1;
                                        schedule.RenewDate = DateTime.Now;
                                        schedule.RenewUserID = user.MsUserID;

                                        schedule.UpdateRecord(user);
                                    }
                                }


                                if (m is OdThi)
                                {
                                    OdThi thi = m as OdThi;
                                    if (thi.CancelFlag != 1)
                                    {
                                        sendMailOdThis.Add(m as OdThi);
                                    }
                                }

                                if (ServiceSpecificDAC.Is_本船更新情報_編集対象(m))
                                {
                                    OdThi thi = m as OdThi;
                                    odThis[thi.OdThiID] = thi.TehaiIraiNo;
                                }

                                if (ServiceSpecificDAC.Is_報告書登録(m))
                                {
                                    stopAlarmKKs.Add(m as DmKanriKiroku);
                                }

                                if (m is DjDouseiHoukoku)
                                {
                                    sendMailDjHoukokus.Add(m as DjDouseiHoukoku);
                                }

                            }
                            else
                            {
                                if (ServiceSpecificDAC.Is_本船更新情報_編集対象(m))
                                {
                                    OdThi thi = m as OdThi;
                                    delOdThis[thi.OdThiID] = thi.TehaiIraiNo;
                                }
                            }
                            isExistsSyncData = true;

                            #endregion
                        }
                    }
                    else
                    {
                        string LogStr = "　";
                        LogStr += logFlag == 0 ? "ﾃﾞｰﾀ同期" : logFlag == 1 ?"文書ﾃﾞｰﾀ同期" : "添付ﾃﾞｰﾀ同期";
                        LogStr += "VesselName[" + gVesselName + "]:";
                        LogStr += "HostName[" + gHostName + "]:";
                        LogStr += "Table[" + m.GetType().Name + "]:";
                        LogStr += "id[" + GetID(m) + "]:";
                        LogStr += "UserKey[" + m.UserKey + "]:";
                        LogStr += "Hash[" + StringUtils.CreateHash(SyncTableSaver.GetHashSource(m)) + "] is ValidateUserKey Error !! ";
                        LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);
                    }
                }

                // 船から送信されてきた本船更新情報をＤＢに登録する
                Insert_本船更新情報(odThis, delOdThis, honsenKoushinInfos);


                管理記録アラーム処理(user, vesselId, stopAlarmKKs);


                // TRANSACTION_TOKEN をＤＢに登録する
                if (token != null && token.Length > 0 && ServiceSpecificDAC.ExistsTransactionToken(token) == false)
                {
                    ServiceSpecificDAC.InsertTransactionToken(token);
                }

                // 燃料メールの送信依頼をNBaseServiceにする
                ServiceSpecificDAC.Send_手配メール(sendMailOdThis);

                // 動静報告メールの送信依頼をNBaseServiceにする
                ServiceSpecificDAC.Send_動静報告メール(sendMailDjHoukokus);
            }
            catch (Exception ex)
            {
                // ===== LOG =====
                string LogStr = "　";
                LogStr += logFlag == 0 ? "ﾃﾞｰﾀ同期" : logFlag == 1 ? "文書ﾃﾞｰﾀ同期" : "添付ﾃﾞｰﾀ同期";
                LogStr += "VesselName[" + gVesselName + "]:";
                LogStr += "HostName[" + gHostName + "]:";
                LogStr += "InsertSendedData Exception :" + ex.Message;
                LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);
            }

            return isExistsSyncData;
        }
        #endregion

        #region private bool InsertSendedData2(MsUser user, int vesselId, string xml, string token, int logFlag)
        private bool InsertSendedData2(MsUser user, int vesselId, string xml, string token, int logFlag)
        {
            bool isExistsSyncData = false;

            try
            {
                DataSet ds = new DataSet();
                using (StringReader xmlSR = new StringReader(xml))
                {
                    ds.ReadXml(xmlSR);
                }

                // メールを送信する OD_THI.
                List<OdThi> sendMailOdThis = new List<OdThi>();

                // 本船更新情報に手配依頼番号を追加する OD_THI.
                Dictionary<string, string> odThis = new Dictionary<string, string>();
                Dictionary<string, string> delOdThis = new Dictionary<string, string>();
                List<PtHonsenkoushinInfo> honsenKoushinInfos = new List<PtHonsenkoushinInfo>();

                // 登録した手配のID
                List<string> updateThiIdList = new List<string>();
                List<string> updateThiItemIdList = new List<string>();

                // まずOD_THIを対象とする
                #region OD_THI
                foreach (ISyncTable m in MappingClass2.ToModel(ds))
                {
                    if ((m is OdThi) == false)
                        continue;

                    if (SyncTableSaver.ValidateUserKey(m) == false)
                        continue;

                    if (user == null)
                    {
                        user = new MsUser();
                        user.MsUserID = m.RenewUserID;
                    }

                    OdThi thi = m as OdThi;

                    // ＤＢに登録されている手配を取得
                    OdThi dbThi = OdThi.GetRecord(user, thi.OdThiID);
                    if (dbThi != null)
                    {
                        // 登録されている状態が「船保存」であれば、上書きして問題なし
                        if (dbThi.Status != 0)
                            continue;

                        thi.Ts = dbThi.Ts;
                    }

                    // ＤＢに登録する
                    long ret = SyncTableSaver.InsertOrUpdate(m, user, StatusUtils.通信状況.同期済);

                    // ===== LOG =====
                    string LogStr = "　";
                    LogStr += logFlag == 0 ? "ﾃﾞｰﾀ同期" : logFlag == 1 ? "文書ﾃﾞｰﾀ同期" : "添付ﾃﾞｰﾀ同期";
                    LogStr += "VesselName[" + gVesselName + "]:";
                    LogStr += "HostName[" + gHostName + "]:";
                    LogStr += "InsertOrUpdate[" + m.GetType().Name + "]:";
                    LogStr += "ret[" + ret.ToString() + "]:";
                    LogStr += "id[" + GetID(m) + "]:";
                    LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);

                    if (ret > -1)
                    {
                        updateThiIdList.Add(thi.OdThiID);

                        if (thi.CancelFlag != 1)
                        {
                            sendMailOdThis.Add(m as OdThi);
                        }

                        if (ServiceSpecificDAC.Is_本船更新情報_編集対象(m))
                        {
                            odThis[thi.OdThiID] = thi.TehaiIraiNo;
                        }

                    }
                    else
                    {
                        if (ServiceSpecificDAC.Is_本船更新情報_編集対象(m))
                        {
                            delOdThis[thi.OdThiID] = thi.TehaiIraiNo;
                        }
                    }
                    isExistsSyncData = true;
                }
                #endregion

                #region OD_THI_ITEM
                if (updateThiIdList.Count > 0)
                {
                    foreach (ISyncTable m in MappingClass2.ToModel(ds))
                    {
                        if (SyncTableSaver.ValidateUserKey(m) == false)
                            continue;

                        if ((m is OdThiItem) == false)
                            continue;

                        if (user == null)
                        {
                            user = new MsUser();
                            user.MsUserID = m.RenewUserID;
                        }

                        OdThiItem item = m as OdThiItem;

                        if (updateThiIdList.Contains(item.OdThiID) == false)
                            continue;

                        OdThiItem dbItem = OdThiItem.GetRecord(user, item.OdThiItemID);
                        if (dbItem == null)
                            continue;

                        m.Ts = dbItem.Ts;

                        // ＤＢに登録する
                        long ret = SyncTableSaver.InsertOrUpdate(m, user, StatusUtils.通信状況.同期済);

                        // ===== LOG =====
                        string LogStr = "　";
                        LogStr += logFlag == 0 ? "ﾃﾞｰﾀ同期" : logFlag == 1 ? "文書ﾃﾞｰﾀ同期" : "添付ﾃﾞｰﾀ同期";
                        LogStr += "VesselName[" + gVesselName + "]:";
                        LogStr += "HostName[" + gHostName + "]:";
                        LogStr += "InsertOrUpdate[" + m.GetType().Name + "]:";
                        LogStr += "ret[" + ret.ToString() + "]:";
                        LogStr += "id[" + GetID(m) + "]:";
                        LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);

                        if (ret > -1)
                        {
                            updateThiItemIdList.Add(item.OdThiItemID);
                        }
                    }
                }
                #endregion

                #region OD_THI_SHOUSA_ITEM
                if (updateThiItemIdList.Count > 0)
                {
                    foreach (ISyncTable m in MappingClass2.ToModel(ds))
                    {
                        if (SyncTableSaver.ValidateUserKey(m) == false)
                            continue;

                        if ((m is OdThiShousaiItem) == false)
                            continue;

                        if (user == null)
                        {
                            user = new MsUser();
                            user.MsUserID = m.RenewUserID;
                        }

                        OdThiShousaiItem shousai = m as OdThiShousaiItem;

                        if (updateThiItemIdList.Contains(shousai.OdThiItemID) == false)
                            continue;

                        OdThiShousaiItem dbShousai = OdThiShousaiItem.GetRecord(user, shousai.OdThiShousaiItemID);
                        if (dbShousai == null)
                            continue;

                        m.Ts = dbShousai.Ts;

                        // ＤＢに登録する
                        long ret = SyncTableSaver.InsertOrUpdate(m, user, StatusUtils.通信状況.同期済);

                        // ===== LOG =====
                        string LogStr = "　";
                        LogStr += logFlag == 0 ? "ﾃﾞｰﾀ同期" : logFlag == 1 ? "文書ﾃﾞｰﾀ同期" : "添付ﾃﾞｰﾀ同期";
                        LogStr += "VesselName[" + gVesselName + "]:";
                        LogStr += "HostName[" + gHostName + "]:";
                        LogStr += "InsertOrUpdate[" + m.GetType().Name + "]:";
                        LogStr += "ret[" + ret.ToString() + "]:";
                        LogStr += "id[" + GetID(m) + "]:";
                        LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);
                    }
                }
                #endregion

                // 船から送信されてきた本船更新情報をＤＢに登録する
                Insert_本船更新情報(odThis, delOdThis, honsenKoushinInfos);

                // 燃料メールの送信依頼をNBaseServiceにする
                ServiceSpecificDAC.Send_手配メール(sendMailOdThis);


                #region DM_KAKUNIN_JOKYO
                foreach (ISyncTable m in MappingClass2.ToModel(ds))
                {
                    string LogStr = "　";

                    if ((m is DmKakuninJokyo) == false)
                        continue;

                    if (m.UserKey == null)
                    {
                        //// UserKey がない場合、登録対象とする
                        //LogStr = "　";
                        //LogStr += "VesselName[" + gVesselName + "]:";
                        //LogStr += "HostName[" + gHostName + "]:";
                        //LogStr += "InsertSendedData2[" + m.GetType().Name + "]:";
                        //LogStr += "UserKey is null";
                        //LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);
                    }
                    else if (m.UserKey.Length == 0)
                    {
                        //// UserKey がない場合、登録対象とする
                        //LogStr = "　";
                        //LogStr += "VesselName[" + gVesselName + "]:";
                        //LogStr += "HostName[" + gHostName + "]:";
                        //LogStr += "InsertSendedData2[" + m.GetType().Name + "]:";
                        //LogStr += "UserKey Length == 0";
                        //LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);
                    }
                    else if (SyncTableSaver.ValidateUserKey(m) == false)
                    {
                        //LogStr = "　";
                        //LogStr += "VesselName[" + gVesselName + "]:";
                        //LogStr += "HostName[" + gHostName + "]:";
                        //LogStr += "InsertSendedData2[" + m.GetType().Name + "]:";
                        //LogStr += "ValidateUserKey Error";
                        //LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);

                        // UserKeyがある場合、Validateion=falseなら無視する
                        continue;
                    }

                    if (user == null)
                    {
                        user = new MsUser();
                        user.MsUserID = m.RenewUserID;
                    }

                    long ret = 0;
                    DmKakuninJokyo kj = m as DmKakuninJokyo;
                    DmKakuninJokyo dbKj = DmKakuninJokyo.GetRecord(user, kj.DmKakuninJokyoID);
                    if (dbKj != null)
                    {
                        //// 登録されている場合、無効とする
                        //LogStr = "　";
                        //LogStr += "VesselName[" + gVesselName + "]:";
                        //LogStr += "HostName[" + gHostName + "]:";
                        //LogStr += "InsertSendedData2[" + m.GetType().Name + "]:";
                        //LogStr += "id[" + GetID(m) + "]:は登録されている";
                        //LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);

                        //// ＤＢに登録する
                        //ret = SyncTableSaver.InsertOrUpdate(dbKj, user, StatusUtils.通信状況.同期済); //登録されているものを更新（Honsenに戻せるようにする）
                        continue;
                    }
                    else
                    {
                        // ＤＢに登録する
                        ret = SyncTableSaver.InsertOrUpdate(m, user, StatusUtils.通信状況.同期済);
                    }


                    // ===== LOG =====
                    LogStr = "　";
                    LogStr += logFlag == 0 ? "ﾃﾞｰﾀ同期" : logFlag == 1 ? "文書ﾃﾞｰﾀ同期" : "添付ﾃﾞｰﾀ同期";
                    LogStr += "VesselName[" + gVesselName + "]:";
                    LogStr += "HostName[" + gHostName + "]:";
                    LogStr += "InsertOrUpdate[" + m.GetType().Name + "]:";
                    LogStr += "ret[" + ret.ToString() + "]:";
                    LogStr += "id[" + GetID(m) + "]:";
                    LogStr += "ts[" + m.Ts + "]:";
                    LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);

                    isExistsSyncData = true;
                }
                #endregion

            }
            catch ( Exception ex )
            {
                // ===== LOG =====
                string LogStr = "　";
                LogStr += logFlag == 0 ? "ﾃﾞｰﾀ同期" : logFlag == 1 ? "文書ﾃﾞｰﾀ同期" : "添付ﾃﾞｰﾀ同期";
                LogStr += "VesselName[" + gVesselName + "]:";
                LogStr += "HostName[" + gHostName + "]:";
                LogStr += "InsertSendedData2 Exception :" + ex.Message;
                LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);
            }

            return isExistsSyncData;
        }
        #endregion




        /// <summary>
        /// 船との差分データを取得する
        /// </summary>
        /// <param name="maxDataNoOfVesselIdZero"></param>
        /// <param name="maxDataNo"></param>
        /// <param name="vesselId"></param>
        /// <returns></returns>
        #region private string BuildReturnData(MsUser user,decimal maxDataNoOfVesselIdZero, decimal maxDataNo, int vesselId, ref bool isExists, int curNo, int maxNo)
        #region 2012.04.10 旧Ver
        //private string BuildReturnData(decimal maxDataNoOfVesselIdZero, decimal maxDataNo, int vesselId, ref bool isExists)
        //{
        //    isExists = false;

        //    // 同期Tableリストを取得する
        //    List<SnTableInfo> tableList = SnTableInfo.GetRecords(new MsUser());

        //    DataSet dataSet = ServiceSpecificDAC.GetSendDataSet(maxDataNoOfVesselIdZero, maxDataNo, vesselId, tableList);

        //    if (dataSet.Tables.Count > 0)
        //    {
        //        isExists = true;
        //    }

        //    return ToXml(dataSet);
        //}
        #endregion
        //private string BuildReturnData(decimal maxDataNoOfVesselIdZero, decimal maxDataNo, int vesselId, ref bool isExists, int curNo, int maxNo)
        private string BuildReturnData(MsUser user,decimal maxDataNoOfVesselIdZero, decimal maxDataNo, int vesselId, ref bool isExists, int curNo, int maxNo, int counter)
        {
            string LogStr = "";
            LogStr += "VesselName[" + gVesselName + "]:";
            LogStr += "HostName[" + gHostName + "]:";
            LogStr += "サーバから船へ";

            isExists = false;

            // 同期Tableリストを取得する
            List<SnTableInfo> tableList = SnTableInfo.GetRecords(new MsUser());

            //DataSet dataSet = ServiceSpecificDAC.GetSendDataSet(maxDataNoOfVesselIdZero, maxDataNo, vesselId, tableList, curNo, maxNo);
            DataSet dataSet = ServiceSpecificDAC.GetSendDataSet(maxDataNoOfVesselIdZero, maxDataNo, vesselId, tableList, curNo, maxNo, counter);

            if (dataSet.Tables.Count > 0)
            {
                LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);

                isExists = true;

                foreach (ISyncTable m in MappingClass2.ToModel(dataSet))
                {
                    LogStr = "　";
                    LogStr += "VesselName[" + gVesselName + "]:";
                    LogStr += "HostName[" + gHostName + "]:";
                    LogStr += "ReturnData[" + m.GetType().Name + "]:";
                    LogStr += "id[" + GetID(m) + "]:";
                    LogStr += "DataNo[" + m.DataNo.ToString() + "]:";
                    LogStr += "Ts[" + m.Ts + "]:";
                    LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);
                }
            }
            else
            {
                LogStr += " : 戻りデータなし";
                LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);
            }

            string ret = ToXml(dataSet);

            // ==> ここから追加
            if (dataSet.Tables.Count > 0)
            {
                if (dataSet.Tables[0].Rows.Count < ServiceSpecificDAC.返信する最大レコード数)
                {
                    counter = 0;
                }
                else
                //if (dataSet.Tables.Count == ServiceSpecificDAC.返信する最大レコード数)
                {
                    counter++;
                }
            }
            else
            {
                counter = 0;
            }
            ret = "<counter>" + counter.ToString() + "</counter>" + ret;
            // <== ここまで追加

            dataSet.Dispose();
            return ret;
        }
        #endregion
        




        #region private static void Insert_本船更新情報(Dictionary<string, string> odThis, Dictionary<string, string> delOdThis, List<PtHonsenkoushinInfo> honsenKoushinInfos)
        private static void Insert_本船更新情報(Dictionary<string, string> odThis, Dictionary<string, string> delOdThis, List<PtHonsenkoushinInfo> honsenKoushinInfos)
        {
            foreach (PtHonsenkoushinInfo info in honsenKoushinInfos)
            {
                MsUser user = new MsUser();
                user.MsUserID = info.RenewUserID;

                if (info.MsPortalInfoShubetuId == ((int)MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString())
                {
                    if (info.MsPortalInfoKoumokuId == ((int)MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.手配依頼).ToString())
                    {
                        if (odThis.ContainsKey(info.SanshoumotoId))
                        {
                            if (odThis[info.SanshoumotoId] != null && odThis[info.SanshoumotoId].Length > 0)
                            {
                                info.KoushinNaiyou = String.Format(info.KoushinNaiyou, odThis[info.SanshoumotoId]);

                                SyncTableSaver.InsertOrUpdate(info, user, StatusUtils.通信状況.同期済);
                            }
                        }
                        if (delOdThis.ContainsKey(info.SanshoumotoId))
                        {
                            if (delOdThis[info.SanshoumotoId] != null && delOdThis[info.SanshoumotoId].Length > 0)
                            {
                                info.DeleteFlag = 1;
                                SyncTableSaver.InsertOrUpdate(info, user, StatusUtils.通信状況.同期済);
                            }
                        }
                    }
                    else
                    {
                        SyncTableSaver.InsertOrUpdate(info, user, StatusUtils.通信状況.同期済);
                    }
                }
                else
                {
                    SyncTableSaver.InsertOrUpdate(info, user, StatusUtils.通信状況.同期済);
                }
            }
        }
        #endregion

        #region private int GetSchemaVersion()
        private int GetSchemaVersion()
        {
            int version = 0;

            try
            {
                string ver = Common.スキーマバージョン;
                version = Convert.ToInt32(ver);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return version;
        }
        #endregion

        #region private static string ToXml(DataSet dataSet)
        private static string ToXml(DataSet dataSet)
        {
            string retXml = "";

            using (StringWriter xmlSW = new StringWriter())
            {
                dataSet.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                retXml = xmlSW.ToString();
            }

            return retXml;
        }
        #endregion

        #region private void WriteExceptionLog( string message )
        private void WriteExceptionLog( string message )
        {
            try
            {
                System.IO.StreamWriter sw = new StreamWriter("Error.log", true, Encoding.Default);
                sw.WriteLine(message);
                sw.Close();
            }
            catch
            {
            }
        }
        #endregion


        #region DELETE
        /// <summary>
        /// 　船から WCF でコールされる
        /// 　SERVER上で実行されるメソッド
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="requestHoukokushoIds"></param>
        /// <param name="requestKanriKirokuIds"></param>
        /// <param name="requestKoubunshoKisokuIds"></param>
        /// <param name="vesselId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        //public string 文書データ同期(
        //    string xml,
        //    List<string> requestHoukokushoIds,
        //    List<string> requestKanriKirokuIds,
        //    List<string> requestKoubunshoKisokuIds,
        //    int vesselId,
        //    string userId
        //    )
        //{

        //    string returnXml = null;

        //    //================================
        //    // 受信XMLを保存する
        //    //================================
        //    try
        //    {
        //        ServiceSpecificDAC.InsertSnMessageLog(vesselId, ServiceSpecificDAC.SnMessageLogMessageType.受信, xml);
        //    }
        //    catch (Exception e)
        //    {
        //        WriteExceptionLog(e.Message);
        //    }


        //    //================================
        //    // 同期実処理
        //    //================================
        //    bool isReceiveSyncData = false;
        //    bool isSendSyncData = false;
        //    try
        //    {
        //        // 送信データから TRANSACTION_TOKEN を生成する.
        //        string token = StringUtils.CreateHash(xml);

        //        //if (!ServiceSpecificDAC.ExistsTransactionToken(token))
        //        //{
        //            // 船のデータを登録する
        //            isReceiveSyncData = InsertSendedData(xml, token);
        //        //}
        //        List<string> sendKanriKirokuFileIds = null;
        //        List<string> sendKoubunshoKisokuFileIds = null;
        //        if (isReceiveSyncData)
        //        {
        //            sendKanriKirokuFileIds = GetSendKanriKirokuIds(xml);
        //            sendKoubunshoKisokuFileIds = GetSendKoubunshKisokuIds(xml);
        //        }

        //        returnXml = this.BuildReturnDocumentData(sendKanriKirokuFileIds, sendKoubunshoKisokuFileIds, requestHoukokushoIds, requestKanriKirokuIds, requestKoubunshoKisokuIds, ref isSendSyncData);

        //    }
        //    catch (Exception e)
        //    {
        //        WriteExceptionLog(e.Message);
        //    }


        //    //================================
        //    // 送信XMLを保存する
        //    //================================
        //    try
        //    {
        //        ServiceSpecificDAC.InsertSnMessageLog(vesselId, ServiceSpecificDAC.SnMessageLogMessageType.送信, returnXml);
        //    }
        //    catch (Exception e)
        //    {
        //        WriteExceptionLog(e.Message);
        //    }

        //    return returnXml;
        //}
        #endregion


        private List<string> GetSendKanriKirokuIds(string xml)
        {
            List<string> ids = new List<string>();

            DataSet ds = new DataSet();
            using (StringReader xmlSR = new StringReader(xml))
            {
                ds.ReadXml(xmlSR);
            }            
            //foreach (ISyncTable m in MappingClass.ToModel(ds))
            foreach (ISyncTable m in MappingClass2.ToModel(ds))
            {
                if (m is DmKanriKirokuFile)
                {
                    ids.Add(((DmKanriKirokuFile)m).DmKanriKirokuFileID);
                }
           }
            return ids;
        }
        private List<string> GetSendKoubunshKisokuIds(string xml)
        {
            List<string> ids = new List<string>();

            DataSet ds = new DataSet();
            using (StringReader xmlSR = new StringReader(xml))
            {
                ds.ReadXml(xmlSR);
            }
            //foreach (ISyncTable m in MappingClass.ToModel(ds))
            foreach (ISyncTable m in MappingClass2.ToModel(ds))
            {
                if (m is DmKoubunshoKisokuFile)
                {
                    ids.Add(((DmKoubunshoKisokuFile)m).DmKoubunshoKisokuFileID);
                }
            }
            return ids;
        }

        private string BuildReturnDocumentData(
                        List<string> sendKanriKirokuIds,
                        List<string> sendKoubunshoKisokuIds,
                        List<string> requestHoukokushoIds,
                        List<string> requestKanriKirokuIds,
                        List<string> requestKoubunshoKisokuIds,
                        List<string> requestAttachFileIds,
                        ref bool isExists)
        {
            isExists = false;
            int syncMax = ServiceSpecificDAC.GetDocumentSyncMax();
            DataSet dataSet = ServiceSpecificDAC.GetSendDocumentDataSet(syncMax, sendKanriKirokuIds, sendKoubunshoKisokuIds, requestHoukokushoIds, requestKanriKirokuIds, requestKoubunshoKisokuIds, requestAttachFileIds);

            if (dataSet.Tables.Count > 0)
            {
                isExists = true;
            }

            return ToXml(dataSet);
        }

        #region public string 文書データ同期_送信(string xml, int vesselId, string hostName, string userId)
        public string 文書データ同期_送信(string xml, int vesselId, string hostName, string userId)
        {
            // ===== LOG =====
            MsUser user = new MsUser();
            user = MsUser.GetRecordsByUserID(user, userId);
            MsVessel logVessel = MsVessel.GetRecordByMsVesselID(user, vesselId);
            if (logVessel != null)
                gVesselName = logVessel.VesselName;
            gHostName = hostName;
            string LogStr = "文書データ同期（船⇒サーバ）:";
            LogStr += "VesselName[" + gVesselName + "]:";
            LogStr += "HostName[" + gHostName + "]:";
            LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);

            string returnXml = null;

            #region
            //================================
            // 受信XMLを保存する
            //================================
            // 2011.08.22 エラーが発生するので、とりあえずコメントとし
            //try
            //{
            //    ServiceSpecificDAC.InsertSnMessageLog(vesselId, ServiceSpecificDAC.SnMessageLogMessageType.受信, xml);
            //}
            //catch (Exception e)
            //{
            //    WriteExceptionLog(e.Message);
            //}
            #endregion

            //================================
            // 同期実処理
            //================================
            bool isReceiveSyncData = false;
            bool isSendSyncData = false;
            try
            {
                // 送信データから TRANSACTION_TOKEN を生成する.
                string token = StringUtils.CreateHash(xml);
                
                // 船のデータを登録する
                isReceiveSyncData = InsertSendedData(user, vesselId, xml, token, 1);
                
                List<string> sendKanriKirokuFileIds = null;
                List<string> sendKoubunshoKisokuFileIds = null;
                if (isReceiveSyncData)
                {
                    sendKanriKirokuFileIds = GetSendKanriKirokuIds(xml);
                    sendKoubunshoKisokuFileIds = GetSendKoubunshKisokuIds(xml);
                }

                returnXml = this.BuildReturnDocumentData(sendKanriKirokuFileIds, sendKoubunshoKisokuFileIds, null, null, null, null, ref isSendSyncData);

                // ===== LOG =====
                LogStr = "文書データ同期（サーバ⇒船戻し）:";
                LogStr += "VesselName[" + gVesselName + "]:";
                LogStr += "HostName[" + gHostName + "]:";
                if (isReceiveSyncData && isSendSyncData)
                {
                    if (sendKanriKirokuFileIds != null && sendKanriKirokuFileIds.Count > 0)
                        LogStr += "戻りID:[" + sendKanriKirokuFileIds[0] + "]";
                    else if (sendKoubunshoKisokuFileIds != null && sendKoubunshoKisokuFileIds.Count > 0)
                        LogStr += "戻りID:[" + sendKoubunshoKisokuFileIds[0] + "]";
                    else
                        LogStr += "戻りIDが特定できない";
                }
                else
                {
                    LogStr += "戻りなし:";
                }
                LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);

            }
            catch (Exception e)
            {
                WriteExceptionLog(e.Message);
            }

            #region
            //================================
            // 送信XMLを保存する
            //================================
            // 2011.08.22 エラーが発生するので、とりあえずコメントとし
            //try
            //{
            //    ServiceSpecificDAC.InsertSnMessageLog(vesselId, ServiceSpecificDAC.SnMessageLogMessageType.送信, returnXml);
            //}
            //catch (Exception e)
            //{
            //    WriteExceptionLog(e.Message);
            //}
            #endregion

            return returnXml;
        }
        #endregion

        #region public string 文書データ同期_受信(
        public string 文書データ同期_受信(
            List<string> requestOdAttachFileIds,
            List<string> requestHoukokushoIds,
            List<string> requestKanriKirokuIds,
            List<string> requestKoubunshoKisokuIds,
            int vesselId, string hostName, string userId)
        {

            // ===== LOG =====
            MsUser user = new MsUser();
            user = MsUser.GetRecordsByUserID(user, userId);
            MsVessel logVessel = MsVessel.GetRecordByMsVesselID(user, vesselId);
            if (logVessel != null)
                gVesselName = logVessel.VesselName;
            gHostName = hostName;
            string LogStr = "文書データ同期（サーバ⇒船）:";
            LogStr += "VesselName[" + gVesselName + "]:";
            LogStr += "HostName[" + gHostName + "]:";
            LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);

            string returnXml = null;

            //================================
            // 指定されているIDのファイルを返信（送信）データとして準備する
            //================================
            bool isSendSyncData = false;
            try
            {
                returnXml = this.BuildReturnDocumentData(null, null, requestHoukokushoIds, requestKanriKirokuIds, requestKoubunshoKisokuIds, requestOdAttachFileIds, ref isSendSyncData);
            }
            catch (Exception e)
            {
                WriteExceptionLog(e.Message);
            }

            #region
            //================================
            // 送信XMLを保存する
            //================================
            // 2011.08.22 エラーが発生するので、とりあえずコメントとし
            //try
            //{
            //    ServiceSpecificDAC.InsertSnMessageLog(vesselId, ServiceSpecificDAC.SnMessageLogMessageType.送信, returnXml);
            //}
            //catch (Exception e)
            //{
            //    WriteExceptionLog(e.Message);
            //}
            #endregion

            return returnXml;
        }
        #endregion

        #region private static bool 管理記録アラーム処理(MsUser user, int vesselId, List<DmKanriKiroku> kanriKirokus)
        // 2011.07.12 
        private static bool 管理記録アラーム処理(MsUser user, int vesselId, List<DmKanriKiroku> kanriKirokus)
        {
            bool ret = true;
            DateTime 処理日時 = DateTime.Now;
            string 更新者 = user.MsUserID;

            foreach (DmKanriKiroku kk in kanriKirokus)
            {
                PtAlarmInfo alarmInfo = null;
                PtDmAlarmInfo alarmDmInfo = PtDmAlarmInfo.GetRecord(user, kk.MsDmHoukokushoID, kk.JikiNen, kk.JikiTuki, (int)NBaseData.DS.DocConstants.RoleEnum.船, vesselId);
                if (alarmDmInfo == null)
                {
                    return true;
                }

                // アラーム停止
                alarmInfo = PtAlarmInfo.GetRecordsByPtAlarmInfoId(user, alarmDmInfo.PtAlarmInfoId);
                if (alarmInfo == null)
                {
                    return true;
                }
                alarmInfo.AlarmShowFlag = (int)PtAlarmInfo.AlarmShowFlagEnum.アラームＯＦＦ;
                alarmInfo.AlarmStopDate = 処理日時;
                alarmInfo.AlarmStopUser = 更新者;
                alarmInfo.RenewDate = 処理日時;
                alarmInfo.RenewUserID = 更新者;
                ret = SyncTableSaver.InsertOrUpdate2(alarmInfo, user, StatusUtils.通信状況.未同期);
                if (!ret)
                {
                    return ret;
                }

                alarmDmInfo.DeleteFlag = (int)NBaseData.DS.DocConstants.FlagEnum.ON;
                alarmDmInfo.RenewDate = 処理日時;
                alarmDmInfo.RenewUserID = 更新者;
                ret = SyncTableSaver.InsertOrUpdate2(alarmDmInfo, user, StatusUtils.通信状況.未同期);
                if (!ret)
                {
                    return ret;
                }

                int nen = kk.JikiNen + 1;
                int tuki = kk.JikiTuki;
                PtDmAlarmInfo check = PtDmAlarmInfo.GetRecord(user, kk.MsDmHoukokushoID, nen, tuki, (int)NBaseData.DS.DocConstants.RoleEnum.船, vesselId);
                if (check == null)
                {
                    // 1年後の分を作成
                    alarmInfo.PtAlarmInfoId = 新規ID();
                    alarmInfo.AlarmShowFlag = (int)PtAlarmInfo.AlarmShowFlagEnum.アラームＯＮ;
                    alarmInfo.HasseiDate = alarmInfo.HasseiDate.AddYears(1); // 1年後
                    alarmInfo.AlarmStopDate = DateTime.MinValue;
                    alarmInfo.AlarmStopUser = null;
                    alarmInfo.RenewUserID = 更新者;
                    alarmInfo.RenewDate = 処理日時;

                    ret = SyncTableSaver.InsertOrUpdate2(alarmInfo, user, StatusUtils.通信状況.未同期);
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
                    ret = SyncTableSaver.InsertOrUpdate2(alarmDmInfo, user, StatusUtils.通信状況.未同期);
                    if (!ret)
                    {
                        return ret;
                    }
                }
            }
            return ret;
        }
        #endregion

        private static string 新規ID()
        {
            return System.Guid.NewGuid().ToString();
        }

        #region public string 添付ファイル同期_送信(string xml, int vesselId, string hostName, string userId)
        public string 添付ファイル同期_送信(string xml, int vesselId, string hostName, string userId)
        {
            // ===== LOG =====
            MsUser user = new MsUser();
            user = MsUser.GetRecordsByUserID(user, userId);
            MsVessel logVessel = MsVessel.GetRecordByMsVesselID(user, vesselId);
            if (logVessel != null)
                gVesselName = logVessel.VesselName;
            gHostName = hostName; 
            string LogStr = "添付データ同期（船⇒サーバ）:";
            LogStr += "VesselName[" + gVesselName + "]:";
            LogStr += "HostName[" + gHostName + "]:";
            LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);

            string returnXml = null;

            #region
            //================================
            // 受信XMLを保存する
            //================================
            // 2011.08.22 エラーが発生するので、とりあえずコメントとし
            //try
            //{
            //    ServiceSpecificDAC.InsertSnMessageLog(vesselId, ServiceSpecificDAC.SnMessageLogMessageType.受信, xml);
            //}
            //catch (Exception e)
            //{
            //    WriteExceptionLog(e.Message);
            //}
            #endregion

            //================================
            // 同期実処理
            //================================
            bool isReceiveSyncData = false;
            bool isSendSyncData = false;
            try
            {
                // 送信データから TRANSACTION_TOKEN を生成する.
                string token = StringUtils.CreateHash(xml);

                // 船のデータを登録する
                isReceiveSyncData = InsertSendedData(user, vesselId, xml, token, 2);

                List<string> sendAttachFileIds = null;
                if (isReceiveSyncData)
                {
                    sendAttachFileIds = GetSendAttachFileIds(xml);
                }

                returnXml = this.BuildReturnAttachData(sendAttachFileIds, ref isSendSyncData);

                // ===== LOG =====
                LogStr = "添付データ同期（サーバ⇒船戻し）:";
                LogStr += "VesselName[" + gVesselName + "]:";
                LogStr += "HostName[" + gHostName + "]:";
                if (isReceiveSyncData && isSendSyncData && sendAttachFileIds != null && sendAttachFileIds.Count> 0)
                {
                    LogStr += "戻りID:[" + sendAttachFileIds[0] + "]";
                }
                else
                {
                    LogStr += "戻りなし:";
                }
                LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogStr);
            }
            catch (Exception e)
            {
                WriteExceptionLog(e.Message);
            }

            #region
            //================================
            // 送信XMLを保存する
            //================================
            // 2011.08.22 エラーが発生するので、とりあえずコメントとし
            //try
            //{
            //    ServiceSpecificDAC.InsertSnMessageLog(vesselId, ServiceSpecificDAC.SnMessageLogMessageType.送信, returnXml);
            //}
            //catch (Exception e)
            //{
            //    WriteExceptionLog(e.Message);
            //}
            #endregion

            return returnXml;
        }
        #endregion

        #region private List<string> GetSendAttachFileIds(string xml)
        private List<string> GetSendAttachFileIds(string xml)
        {
            List<string> ids = new List<string>();

            DataSet ds = new DataSet();
            using (StringReader xmlSR = new StringReader(xml))
            {
                ds.ReadXml(xmlSR);
            }
            //foreach (ISyncTable m in MappingClass.ToModel(ds))
            foreach (ISyncTable m in MappingClass2.ToModel(ds))
            {
                if (m is OdAttachFile)
                {
                    ids.Add(((OdAttachFile)m).OdAttachFileID);
                }
            }
            return ids;
        }
        #endregion

        #region private string BuildReturnAttachData(List<string> sendAttachFileIds,ref bool isExists)
        private string BuildReturnAttachData(
                        List<string> sendAttachFileIds,
                        ref bool isExists)
        {
            isExists = false;
            int syncMax = ServiceSpecificDAC.GetDocumentSyncMax();
            DataSet dataSet = ServiceSpecificDAC.GetSendAttachDataSet(syncMax, sendAttachFileIds);

            if (dataSet.Tables.Count > 0)
            {
                isExists = true;
            }

            return ToXml(dataSet);
        }
        #endregion



        private string GetID(ISyncTable o)
        {
            string id = "";

            if ( o is OdThi )
            {
                id = (o as OdThi).OdThiID;
            }
            else if ( o is OdThiItem )
            {
                id = (o as OdThiItem).OdThiItemID;
            }
            else if (o is OdThiShousaiItem)
            {
                id = (o as OdThiShousaiItem).OdThiShousaiItemID;
            }
            else if (o is OdAttachFile)
            {
                id = (o as OdAttachFile).OdAttachFileID;
            }
            else if (o is OdJry)
            {
                id = (o as OdJry).OdJryID;
            }
            else if (o is OdJryShousaiItem)
            {
                id = (o as OdJryShousaiItem).OdJryShousaiItemID;
            }
            else if (o is OdChozoShousai)
            {
                id = (o as OdChozoShousai).OdChozoShousaiID;
            }
            else if (o is SiCard)
            {
                id = (o as SiCard).SiCardID;
            }
            else if (o is SiLinkShokumeiCard)
            {
                id = (o as SiLinkShokumeiCard).SiLinkShokumeiCardID;
            }
            else if (o is SiJunbikin)
            {
                id = (o as SiJunbikin).SiJunbikinID;
            }
            else if (o is SiSoukin)
            {
                id = (o as SiSoukin).SiSoukinID;
            }
            else if (o is SiKyuyoTeate)
            {
                id = (o as SiKyuyoTeate).SiKyuyoTeateID;
            }
            else if (o is DjDousei)
            {
                id = (o as DjDousei).DjDouseiID;
            }
            else if (o is DjDouseiCargo)
            {
                id = (o as DjDouseiCargo).DjDouseiCargoID;
            }
            else if (o is DjDouseiHoukoku)
            {
                id = (o as DjDouseiHoukoku).DjDouseiHoukokuID;
            }
            else if (o is DmDocComment)
            {
                id = (o as DmDocComment).DmDocCommentID;
            }
            else if (o is DmKakuninJokyo)
            {
                id = (o as DmKakuninJokyo).DmKakuninJokyoID;
            }
            else if (o is DmKanriKiroku)
            {
                id = (o as DmKanriKiroku).DmKanriKirokuID;
            }
            else if (o is DmKanriKirokuFile)
            {
                id = (o as DmKanriKirokuFile).DmKanriKirokuFileID;
            }
            else if (o is DmKanryoInfo)
            {
                id = (o as DmKanryoInfo).DmKanryoInfoID;
            }
            else if (o is DmKoubunshoKisoku)
            {
                id = (o as DmKoubunshoKisoku).DmKoubunshoKisokuID;
            }
            else if (o is DmKoubunshoKisokuFile)
            {
                id = (o as DmKoubunshoKisokuFile).DmKoubunshoKisokuFileID;
            }
            else if (o is DmKoukaiSaki)
            {
                id = (o as DmKoukaiSaki).DmKoukaiSakiID;
            }
            else if (o is DmPublisher)
            {
                id = (o as DmPublisher).DmPublisherID;
            }
            else if (o is PtAlarmInfo)
            {
                id = (o as PtAlarmInfo).PtAlarmInfoId;
            }
            else if (o is PtDmAlarmInfo)
            {
                id = (o as PtDmAlarmInfo).PtDmAlarmInfoID;
            }
            else if (o is PtHonsenkoushinInfo)
            {
                id = (o as PtHonsenkoushinInfo).PtHonsenkoushinInfoId;
            }
            else if (o is PtKanidouseiInfo)
            {
                id = (o as PtKanidouseiInfo).PtKanidouseiInfoId;
            }
            else if (o is MsShoushuriItem)
            {
                id = (o as MsShoushuriItem).MsSsItemID;
            }
            return id;
        }






        private void Debug(MsUser user, int vesselId, string xml, string token, int logFlag)
        {
            string LogHeader = "  VesselName[" + gVesselName + "]:HostName[" + gHostName + "]:";
            string LogStr = "";

            DataSet ds = new DataSet();
            using (StringReader xmlSR = new StringReader(xml))
            {
                ds.ReadXml(xmlSR);
            }

            // クラスにマッピング
            foreach (ISyncTable m in MappingClass2.ToModel(ds))
            {
                LogStr = "";

                if ((m is OdThi))
                {
                    if (SyncTableSaver.ValidateUserKey(m) == false)
                    {
                        LogStr = "od_thi_id[" + (m as OdThi).OdThiID + "] is ValidateUserKey Error !!";
                        LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogHeader + LogStr);
                        continue;
                    }

                    // 船からのデータ
                    LogStr = "船⇒DBへのデータ:";
                    LogStr += "OdThiId[" + (m as OdThi).OdThiID + "]:";
                    LogStr += "Status[" + (m as OdThi).Status.ToString() + "]:";
                    LogStr += "CancelFlg[" + (m as OdThi).CancelFlag.ToString() + "]:";
                    LogStr += "DataNo[" + (m as OdThi).DataNo.ToString() + "]:";
                    LogStr += "RenewDate[" + (m as OdThi).RenewDate.ToString() + "]:";
                    LogStr += "Ts[" + (m as OdThi).Ts + "]:";

                    LogStr += "MsThiIraiSbtId[" + ((m as OdThi).MsThiIraiSbtID == null ? "NULL" : (m as OdThi).MsThiIraiSbtID) + "]:";
                    LogStr += "MsThiIraiShousaiId[" + ((m as OdThi).MsThiIraiShousaiID == null ? "NULL" : (m as OdThi).MsThiIraiShousaiID) + "]:";
                    LogStr += "MsThiIraiStatusId[" + ((m as OdThi).MsThiIraiStatusID == null ? "NULL" : (m as OdThi).MsThiIraiStatusID) + "]:";

                    LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogHeader + LogStr);


                    OdThi dbThi = OdThi.GetRecord(user, (m as OdThi).OdThiID);
                    if (dbThi == null)
                    {
                        LogStr = "OdThiId[" + (m as OdThi).OdThiID + "] はDBに登録されていない";
                        LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogHeader + LogStr);
                        continue;
                    }

                    // DBから取得したODTHI
                    LogStr = "DB登録済みデータ:";
                    LogStr += "OdThiId[" + dbThi.OdThiID + "]:";
                    LogStr += "Status[" + dbThi.Status.ToString() + "]:";
                    LogStr += "CancelFlg[" + dbThi.CancelFlag.ToString() + "]:";
                    LogStr += "DataNo[" + dbThi.DataNo.ToString() + "]:";
                    LogStr += "RenewDate[" + dbThi.RenewDate.ToString() + "]:";
                    LogStr += "Ts[" + dbThi.Ts + "]:";
                    LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogHeader + LogStr);
                }
                else if ((m is DmKakuninJokyo))
                {
                    //// 船からのデータ
                    //LogStr = "船⇒DBへのデータ:";
                    //LogStr += "DmKakuninJokyoId[" + (m as DmKakuninJokyo).DmKakuninJokyoID + "]:";
                    //LogStr += "UserKey[" + (m as DmKakuninJokyo).UserKey + "]:";
                    //LogStr += "DataNo[" + (m as DmKakuninJokyo).DataNo.ToString() + "]:";
                    //LogStr += "RenewDate[" + (m as DmKakuninJokyo).RenewDate.ToString() + "]:";
                    //LogStr += "Ts[" + (m as DmKakuninJokyo).Ts + "]:";

                    //LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogHeader + LogStr);

                }
                else
                {
                    //if ((m is MsShoushuriItem))
                    //    continue;

                    //if ((m is MsSsShousaiItem))
                    //    continue;

                    // 船からのデータ
                    LogStr = "船⇒DBへのデータ:";
                    LogStr += "Table[" + m.GetType().Name + "]:";
                    LogStr += "id[" + GetID(m) + "]:";
                    LogStr += "ts[" + m.Ts + "]:";
                    LogStr += "dataNo[" + m.DataNo.ToString() + "]:";
                    LogFile.NBaseHonsenServiceLogWrite(user.FullName, LogHeader + LogStr);

                }
            }
        }







        public string SyncMaster(
                int syncTableNo,
                decimal maxDataNo,
                int vesselId,
                string hostName,
                string userId,
                DateTime vesselDate,
                int counter
                )
        {
            string returnXml = null;

            MsUser user = new MsUser();
            user = MsUser.GetRecordsByUserID(user, userId);

            //================================
            // 同期実処理
            //================================
            try
            {
                AccessorCommon.ConnectionKey = Common.KEY;

                using (var con = AccessorCommon.GetConnection())
                {
                    List<SyncTables> tableList = SyncTables.GetRecords(con, $"sync_tables_{Common.識別ID}");

                    var table = tableList.Where(o => o.TypeID == SyncTables.TYPE_MASTER && o.TableNo == syncTableNo).FirstOrDefault();

                    returnXml = this.BuildReturnData(user, table.TableName, maxDataNo, vesselId, counter);

                }
            }
            catch (Exception e)
            {
                WriteExceptionLog(e.Message);
            }

            return returnXml;
        }


        public string SyncData(
            string xml,
            int syncTableNo,
            decimal maxDataNo,
            int vesselId,
            string hostName,
            string userId,
            DateTime vesselDate,
            int counter
            )
        {

            string returnXml = null;

            MsUser user = new MsUser();
            user = MsUser.GetRecordsByUserID(user, userId);


            //================================
            // 同期実処理
            //================================
            bool isReceiveSyncData = false;
            try
            {
                AccessorCommon.ConnectionKey = Common.KEY;

                using (var con = AccessorCommon.GetConnection())
                {
                    List<SyncTables> tableList = SyncTables.GetRecords(con, $"sync_tables_{Common.識別ID}");

                    var table = tableList.Where(o => o.TypeID == SyncTables.TYPE_DATA && o.TableNo == syncTableNo).FirstOrDefault();

                    // 船のデータを登録する
                    isReceiveSyncData = SaveWtmReceivedData(xml);


                    returnXml = this.BuildReturnData(user, table.TableName, maxDataNo, vesselId, counter);
                }
            }
            catch (Exception e)
            {

                WriteExceptionLog(e.Message);
                LogFile.NBaseHonsenServiceLogWrite(user.FullName, $"  WtmSyncData:Exception:{e.Message}");
            }

            return returnXml;
        }





        /// <summary>
        /// 船からの受信データをＤＢに登録する
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        #region private bool SaveWtmReceivedData(string xml)
        private bool SaveWtmReceivedData(string xml)
        {
            bool isExistsSyncData = false;

            DataSet ds = new DataSet();
            using (StringReader xmlSR = new StringReader(xml))
            {
                ds.ReadXml(xmlSR);
            }

            if (ds.Tables.Count == 0)
                return isExistsSyncData;


            using (DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                dbConnect.BeginTransaction();

                bool EXECUTE_NON_QUERY_LOG = ORMapping.Common.EXECUTE_NON_QUERY_LOG;
                ORMapping.Common.EXECUTE_NON_QUERY_LOG = false;

                try
                {
                    foreach (DataTable table in ds.Tables)
                    {
                        List<IWtmSyncTable> models = null;
                        models = WtmModels.MappingClass.ToWtmModel(table, Common.WTM_NAMESPACE_KEY);
                        if (models == null)
                            models = WtmModelBase.MappingClass.ToWtmModel(table);

                        if (models != null)
                        {
                            foreach (IWtmSyncTable m in models)
                            {
                                if (m.Exists(dbConnect, Common.識別ID))
                                {
                                    m.UpdateRecord(dbConnect, Common.識別ID, (int)NBaseUtil.StatusUtils.通信状況.同期済);
                                }
                                else
                                {
                                    m.InsertRecord(dbConnect, Common.識別ID, (int)NBaseUtil.StatusUtils.通信状況.同期済);
                                }

                                isExistsSyncData = true;
                            }
                        }

                    }
                    dbConnect.Commit();
                }
                catch (Exception ex)
                {
                    dbConnect.RollBack();
                }
                finally
                {
                    ORMapping.Common.EXECUTE_NON_QUERY_LOG = EXECUTE_NON_QUERY_LOG;
                }
            }

            return isExistsSyncData;
        }
        #endregion





        /// <summary>
        /// 船へ送信するデータを取得する
        /// </summary>
        /// <returns></returns>
        #region private string BuildReturnData(MsUser user, string tableName, decimal maxDataNo, int vesselId, int counter)
        private string BuildReturnData(MsUser user, string tableName, decimal maxDataNo, int vesselId, int counter)
        {
            string ret = null;
            using (var con = AccessorCommon.GetConnection())
            {
                DataSet dataSet = ServiceSpecificDAC.GetWtmSendDataSet(con, tableName, maxDataNo, vesselId, counter);

                ret = ToXml(dataSet);

                if (dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0].Rows.Count < ServiceSpecificDAC.返信する最大レコード数)
                    {
                        counter = 0;
                    }
                    else
                    {
                        counter++;
                    }
                }
                else
                {
                    counter = 0;
                }
                ret = "<counter>" + counter.ToString() + "</counter>" + ret;

                dataSet.Dispose();
            }
            return ret;

        }
        #endregion





        public string SyncSnParameter(
                int vesselId,
                string hostName
                )
        {
            string returnXml = null;
            try
            {
                DataSet dataSet = ServiceSpecificDAC.GetSnParameterSet();

                returnXml = ToXml(dataSet);

                dataSet.Dispose();
            }
            catch (Exception e)
            {
                WriteExceptionLog(e.Message);
            }

            return returnXml;
        }

    }
}
