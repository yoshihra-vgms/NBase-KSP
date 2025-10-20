using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using System.Xml;
//using System.Data.SqlServerCe;
using System.Collections;
using System.IO;
using System.Configuration;
using System.Deployment.Application;
using System.Windows.Forms;
using NBaseData.DAC;
using System.ServiceModel;
using System.Net.NetworkInformation;
using ORMapping;
using NBaseData.DS;
using NBaseUtil;
using NBaseCommon;

namespace SyncClient
{
    /// <summary>
    /// 同期処理のためのクラス(クライアント側)
    /// </summary>
    public class 文書同期Client
    {       
        private bool runFlag = true;

        public static bool SYNC_SUSPEND;
        public static bool OFFLINE;
        public static DateTime SYNC_DATE;
        public static decimal MAX_DATA_NO_OF_ZERO = 0;
        public static decimal MAX_DATA_NO = 0;
        public static string SYNC_MESSAGE;

        public static int ファイル最大サイズ = 1024;//(byte)

        public static int 報告書雛形_未同期 = 0;
        public static int 報告書雛形_対象 　= 0;
        public static int 管理記録_未同期 = 0;
        public static int 管理記録_対象 = 0;
        public static int 公文書規則_未同期 = 0;
        public static int 公文書規則_対象 = 0;

        public static int 報告書雛形_未送信 = 0;
        public static int 管理記録_未送信 = 0;
        public static int 公文書規則_未送信 = 0;


        private ServiceReference1.IService1 serviceClient;
        private IClientDb clientDb;

        public 文書同期Client()
            : this(WcfServiceWrapper.GetInstance().GetServiceClient(), new PostgresqlClientDB())
        {
        }
     
        //public 文書同期Client()
        //    : this(new ServiceReference1.Service1Client(), new PostgresqlClientDB())
        //{
        //}

        public 文書同期Client(ServiceReference1.IService1 serviceClient, IClientDb clientDb)
        {
            this.serviceClient = serviceClient;
            this.clientDb = clientDb;

            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);
        }

        void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            同期EventArgs eventArgs = new 同期EventArgs();

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                OFFLINE = false;
            }
            else
            {
                OFFLINE = true;
            }          
        }

        public void Stop()
        {
            runFlag = false;
        }

        public void Run()
        {
            //====== LOG ==========
            string LogStr = "";

            Console.WriteLine("文書同期Client：" + System.Threading.Thread.CurrentThread.GetHashCode());
            int 同期間隔 = 0;
            int 最大同期数 = 5;
            int ミリ秒 = 60000;
            int ＭＢ = 1024;

            同期間隔 = 1 * ミリ秒;
            while (runFlag)
            {
                if (runFlag == false)
                {
                    break;
                }
                try
                {
                    //if (同期間隔 == 0)
                    //{
                    //    同期間隔 = 1 * ミリ秒;
                    //}
                    //else
                    //{
                        int[] Params = clientDb.GetDocumentParameter();
                        同期間隔 = Params[0] * ミリ秒;
                        最大同期数 = Params[1];
                        ファイル最大サイズ = (Params[2] * ＭＢ);
                    //}
                }
                catch
                {
                }
                Thread.Sleep(同期間隔);

                if (SYNC_SUSPEND || OFFLINE)
                {
                    continue;
                }

                //====== LOG ==========
                LogStr = "文書同期開始";
                LogFile.NBaseHonsenLogWrite(LogFile.文書, 同期Client.LOGIN_USER.FullName, LogStr);


                SYNC_MESSAGE = "同期処理開始:" + DateTime.Now.ToString();

                string receiveXml = null;
                try
                {
                    // 未送信のデータを取得して DataSet に格納する.
                    DataSet unsendDataSet = BuildUnsendDataSet();

                    DataSet unsendAttachFileDataSet = BuildUnsendAttachFileDataSet();

                    // １データづつ送信処理を実施する
                    try
                    {
                        foreach (DataTable dt in unsendAttachFileDataSet.Tables)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                DataTable oneDT = dt.Clone();
                                oneDT.ImportRow(dr);

                                DataSet oneDS = new DataSet();
                                oneDS.Tables.Add(oneDT);

                                // XML にシリアライズ.
                                string sendXml = ToXml(oneDS);

                                // データ送信.
                                // 送信したデータは、サーバでフラグをセットして戻ってくる
                                receiveXml = SendAttachFile(sendXml);

                                // 戻ってきたデータを登録する
                                NotifyReceiveData(receiveXml);
                            }
                        }


                        foreach (DataTable dt in unsendDataSet.Tables)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                DataTable oneDT = dt.Clone();
                                oneDT.ImportRow(dr);

                                DataSet oneDS = new DataSet();
                                oneDS.Tables.Add(oneDT);

                                // XML にシリアライズ.
                                string sendXml = ToXml(oneDS);

                                // データ送信.
                                // 送信したデータは、サーバでフラグをセットして戻ってくる
                                receiveXml = Send(sendXml);

                                // 戻ってきたデータを登録する
                                NotifyReceiveData(receiveXml);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception：" + e.Message);
                    }

                    // 差分データファイルの取得要求をするため差分のあるデータファイルを取得
                    #region
                    int 同期枠 = 最大同期数;
                    List<string> tmp = null;
                    List<string> requestOdAttachFileIds = new List<string>();
                    List<string> requestMsDmTemplateIds = new List<string>();
                    List<string> requestDmKanriKirokuIds = new List<string>();
                    List<string> requestDmKoubunshoKisokuIds = new List<string>();

                    // 添付ファイル
                    tmp = BuildDiffOdAttachFile();
                    for (int i = 0; i < tmp.Count; i++)
                    {
                        if (同期枠 == 0)
                        {
                            break;
                        }
                        同期枠--;
                        requestOdAttachFileIds.Add(tmp[i]);
                    }

                    // テンプレートファイル
                    tmp = BuildDiffMsDmTemplateFile();
                    for (int i = 0; i < tmp.Count; i++)
                    {
                        if (同期枠 == 0)
                        {
                            break;
                        }
                        同期枠--;
                        requestMsDmTemplateIds.Add(tmp[i]);
                    }
                    // 管理記録ファイル
                    if (同期枠 > 0)
                    {
                        tmp = BuildDiffDmKanriKirokuFile();
                        for (int i = 0; i < tmp.Count; i++)
                        {
                            if (同期枠 == 0)
                            {
                                break;
                            }
                            同期枠--;
                            requestDmKanriKirokuIds.Add(tmp[i]);
                        }
                    }
                    // 公文書_規則ファイル
                    if (同期枠 > 0)
                    {
                        tmp = BuildDiffDmKoubunshoKisokuFile();
                        for (int i = 0; i < tmp.Count; i++)
                        {
                            if (同期枠 == 0)
                            {
                                break;
                            }
                            同期枠--;
                            requestDmKoubunshoKisokuIds.Add(tmp[i]);
                        }
                    }
                    #endregion

                    // データ受信.
                    receiveXml = Receive(requestOdAttachFileIds, requestMsDmTemplateIds, requestDmKanriKirokuIds, requestDmKoubunshoKisokuIds);

                    // 受信したデータを登録する
                    NotifyReceiveData(receiveXml);

                    SYNC_MESSAGE = "同期処理完了:" + DateTime.Now.ToString();

                    LogStr = "文書同期完了";
                    LogFile.NBaseHonsenLogWrite(LogFile.文書, 同期Client.LOGIN_USER.FullName, LogStr);
                }
                catch(Exception e)
                {
                    Console.WriteLine("文書同期処理中にエラー発生：" + e.Message);

                    LogStr = "文書同期処理中にエラー発生：" + e.Message;
                    LogFile.NBaseHonsenLogWrite(LogFile.文書, 同期Client.LOGIN_USER.FullName, LogStr);
                }
            }
        }

        private string Send(string sendXml)
        {
            //====== LOG ==========
            string LogStr = "";
            LogStr = "　　文書送信開始";
            LogFile.NBaseHonsenLogWrite(LogFile.文書, 同期Client.LOGIN_USER.FullName, LogStr);

            Console.WriteLine("文書送信開始：" + DateTime.Now.ToString());

            // 船番号の取得.
            int vesselId = 同期Client.LOGIN_VESSEL.MsVesselID;
            // UserIDの取得.
            string userId = 同期Client.LOGIN_USER.MsUserID;

            string receiveXml = "";

            try
            {
                SYNC_MESSAGE = "同期処理中";

                Console.WriteLine("sendXml.Length：" + sendXml.Length.ToString());

                receiveXml = serviceClient.文書データ同期_送信(sendXml, vesselId, 同期Client.GetHostName(), 同期Client.LOGIN_USER.MsUserID);

                Console.WriteLine("receiveXml.Length：" + receiveXml.Length.ToString());

                if (receiveXml == null)
                {
                    SYNC_MESSAGE = "同期エラー(３):サーバ内でエラーが発生しました";
                    receiveXml = "<NewDataSet></NewDataSet>";
                    
                    //====== LOG ==========
                    LogStr = "　　同期エラー(３):サーバ内でエラーが発生";
                }
                else
                {
                    SYNC_DATE = DateTime.Now;
                }


                //====== LOG ==========
                LogStr = "　　文書送信終了";


            }
            catch (EndpointNotFoundException e)
            {
                SYNC_MESSAGE = "同期エラー(１):" + e.Message;

                Console.WriteLine("EndpointNotFoundException：" + e.Message);
                receiveXml = "<NewDataSet></NewDataSet>";

                //====== LOG ==========
                LogStr = "　　同期エラー(１): EndpointNotFoundException:" + e.Message;
            }
            catch (Exception e)
            {
                Console.WriteLine("同期エラー(２)：" + DateTime.Now.ToString());
                SYNC_MESSAGE = "同期エラー(２):" + e.Message;

                Console.WriteLine(e.Source + "：" + e.Message);
                receiveXml = "<NewDataSet></NewDataSet>";

                //====== LOG ==========
                LogStr = "　　同期エラー(２)：" + e.Message;
            }
            Console.WriteLine("文書送信終了：" + DateTime.Now.ToString());

            //====== LOG ==========
            LogFile.NBaseHonsenLogWrite(LogFile.文書, 同期Client.LOGIN_USER.FullName, LogStr);

            return receiveXml;
        }

        private string Receive(List<string> requestOdAttachFileIds, List<string> requestMsDmTemplateIds, List<string> requestDmKanriKirokuIds, List<string> requestDmKoubunshoKisokuIds)
        {
            //====== LOG ==========
            string LogStr = "";
            LogStr = "　　文書受信開始";
            LogFile.NBaseHonsenLogWrite(LogFile.文書, 同期Client.LOGIN_USER.FullName, LogStr);


            Console.WriteLine("文書受信開始：" + DateTime.Now.ToString());

            // 船番号の取得.
            int vesselId = 同期Client.LOGIN_VESSEL.MsVesselID;
            // UserIDの取得.
            string userId = 同期Client.LOGIN_USER.MsUserID;

            string receiveXml = "";

            try
            {
                receiveXml = serviceClient.文書データ同期_受信(
                                    requestOdAttachFileIds,
                                    requestMsDmTemplateIds,
                                    requestDmKanriKirokuIds,
                                    requestDmKoubunshoKisokuIds,
                                    vesselId,
                                    同期Client.GetHostName(), 
                                    同期Client.LOGIN_USER.MsUserID);

                Console.WriteLine("receiveXml.Length：" + receiveXml.Length.ToString());

                if (receiveXml == null)
                {
                    SYNC_MESSAGE = "同期エラー(３):サーバ内でエラーが発生しました";
                    receiveXml = "<NewDataSet></NewDataSet>";

                    //====== LOG ==========
                    LogStr = "　　同期エラー(３):サーバ内でエラーが発生";
                }
                else
                {
                    SYNC_DATE = DateTime.Now;
                }

                //====== LOG ==========
                LogStr = "　　文書受信終了";

            }
            catch (EndpointNotFoundException e)
            {
                SYNC_MESSAGE = "同期エラー(１):" + e.Message;

                Console.WriteLine("EndpointNotFoundException：" + e.Message);
                receiveXml = "<NewDataSet></NewDataSet>";

                //====== LOG ==========
                LogStr = "　　同期エラー(１): EndpointNotFoundException:" + e.Message;
            }
            catch (Exception e)
            {
                Console.WriteLine("同期エラー(２)：" + DateTime.Now.ToString());
                SYNC_MESSAGE = "同期エラー(２):" + e.Message;

                Console.WriteLine(e.Source + "：" + e.Message);
                receiveXml = "<NewDataSet></NewDataSet>";

                //====== LOG ==========
                LogStr = "　　同期エラー(２)：" + e.Message;
            }
            Console.WriteLine("文書受信終了：" + DateTime.Now.ToString());

            LogFile.NBaseHonsenLogWrite(LogFile.文書, 同期Client.LOGIN_USER.FullName, LogStr);

            return receiveXml;
        }

        private string SendAttachFile(string sendXml)
        {
            //====== LOG ==========
            string LogStr = "";
            LogStr = "　　添付送信開始";
            LogFile.NBaseHonsenLogWrite(LogFile.文書, 同期Client.LOGIN_USER.FullName, LogStr);


            Console.WriteLine("添付送信開始：" + DateTime.Now.ToString());

            // 船番号の取得.
            int vesselId = 同期Client.LOGIN_VESSEL.MsVesselID;
            // UserIDの取得.
            string userId = 同期Client.LOGIN_USER.MsUserID;

            string receiveXml = "";

            try
            {
                SYNC_MESSAGE = "同期処理中";

                Console.WriteLine("sendXml.Length：" + sendXml.Length.ToString());

                receiveXml = serviceClient.添付ファイル同期_送信(sendXml, vesselId, 同期Client.GetHostName(), 同期Client.LOGIN_USER.MsUserID);

                Console.WriteLine("receiveXml.Length：" + receiveXml.Length.ToString());

                if (receiveXml == null)
                {
                    SYNC_MESSAGE = "同期エラー(３):サーバ内でエラーが発生しました";
                    receiveXml = "<NewDataSet></NewDataSet>";

                    //====== LOG ==========
                    LogStr = "　　同期エラー(３):サーバ内でエラーが発生";
                }
                else
                {
                    SYNC_DATE = DateTime.Now;
                }

                //====== LOG ==========
                LogStr = "　　添付送信終了";
            }
            catch (EndpointNotFoundException e)
            {
                SYNC_MESSAGE = "同期エラー(１):" + e.Message;

                Console.WriteLine("EndpointNotFoundException：" + e.Message);
                receiveXml = "<NewDataSet></NewDataSet>";

                //====== LOG ==========
                LogStr = "　　同期エラー(１): EndpointNotFoundException:" + e.Message;
            }
            catch (Exception e)
            {
                Console.WriteLine("同期エラー(２)：" + DateTime.Now.ToString());
                SYNC_MESSAGE = "同期エラー(２):" + e.Message;

                Console.WriteLine(e.Source + "：" + e.Message);
                receiveXml = "<NewDataSet></NewDataSet>";

                //====== LOG ==========
                LogStr = "　　同期エラー(２)：" + e.Message;
            }
            Console.WriteLine("添付送信終了：" + DateTime.Now.ToString());

            //====== LOG ==========
            LogFile.NBaseHonsenLogWrite(LogFile.文書, 同期Client.LOGIN_USER.FullName, LogStr);

            return receiveXml;
        }

        #region 未使用
        //private string SendAndReceive(string sendXml, List<string> requestMsDmTemplateIds, List<string> requestDmKanriKirokuIds, List<string> requestDmKoubunshoKisokuIds)
        //{
        //    SYNC_MESSAGE = "同期処理中";

        //    Console.WriteLine("文書同期開始：" + DateTime.Now.ToString());

        //    // 船番号の取得.
        //    int vesselId = 同期Client.LOGIN_VESSEL.MsVesselID;
        //    // UserIDの取得.
        //    string userId = 同期Client.LOGIN_USER.MsUserID;

        //    string receiveXml = "";

        //    try
        //    {
        //        receiveXml = serviceClient.文書データ同期(
        //                            sendXml,
        //                            requestMsDmTemplateIds,
        //                            requestDmKanriKirokuIds,
        //                            requestDmKoubunshoKisokuIds,
        //                            vesselId,
        //                            userId
        //                            );
        //        if (receiveXml == null)
        //        {
        //            SYNC_MESSAGE = "同期エラー(３):サーバ内でエラーが発生しました";
        //            receiveXml = "<NewDataSet></NewDataSet>";
        //        }
        //        else
        //        {
        //            SYNC_DATE = DateTime.Now;
        //        }
        //    }
        //    catch (EndpointNotFoundException e)
        //    {
        //        SYNC_MESSAGE = "同期エラー(１):" + e.Message;

        //        Console.WriteLine("EndpointNotFoundException：" + e.Message);
        //        receiveXml = "<NewDataSet></NewDataSet>";
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("同期エラー(２)：" + DateTime.Now.ToString());
        //        SYNC_MESSAGE = "同期エラー(２):" + e.Message;

        //        Console.WriteLine(e.Source + "：" + e.Message);
        //        receiveXml = "<NewDataSet></NewDataSet>";
        //    }
        //    Console.WriteLine("文書同期終了：" + DateTime.Now.ToString());

        //    return receiveXml;
        //}
        #endregion


        private void NotifyReceiveData(string receiveXml)
        {
            string LogStr = "";

            if (receiveXml == null)
            {
                return;
            }
            DataSet dataSet = new DataSet();

            using (System.IO.StringReader xmlSR = new System.IO.StringReader(receiveXml))
            {
                dataSet.ReadXml(xmlSR);

                if (dataSet.Tables.Count == 0)
                {
                    LogStr = "　文書同期処理 : 対象なし";
                    LogFile.NBaseHonsenLogWrite(LogFile.文書, 同期Client.LOGIN_USER.FullName, LogStr);
                }
            }
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    foreach (DataTable table in dataSet.Tables)
                    {

                        Console.WriteLine("処理中：" + table.TableName);
                        List<ISyncTable> models = MappingClass2.ToModel(table);

                        //====== LOG ==========
                        LogStr = "　文書同期処理 : ";
                        LogStr += "処理中：" + table.TableName + " : Count = " + models.Count.ToString();
                        LogFile.NBaseHonsenLogWrite(LogFile.文書, 同期Client.LOGIN_USER.FullName, LogStr);

                        foreach (ISyncTable m in models)
                        {
                            long dataNo = SyncTableSaver.InsertOrUpdate(m, 同期Client.LOGIN_USER, StatusUtils.通信状況.同期済, dbConnect);

                            //====== LOG ==========
                            LogStr = "　　";
                            if (dataNo == -1)
                            {
                                LogStr += "登録ｴﾗｰ : ";
                            }
                            if (m is DmKanriKirokuFile)
                            {
                                LogStr += "DM_KANRI_KIROKU_FILE_ID = " + (m as DmKanriKirokuFile).DmKanriKirokuFileID;
                            }
                            else if (m is DmKoubunshoKisokuFile)
                            {
                                LogStr += "DM_KOUBUNSHO_KISOKU_FILE_ID = " + (m as DmKoubunshoKisokuFile).DmKoubunshoKisokuFileID;
                            }
                            else if (m is OdAttachFile)
                            {
                                LogStr += "OD_ATTACH_FILE_ID = " + (m as OdAttachFile).OdAttachFileID;
                            } 
                            LogFile.NBaseHonsenLogWrite(LogFile.文書, 同期Client.LOGIN_USER.FullName, LogStr);
                        }
                    }
                    dbConnect.Commit();
                }
                catch (Exception ex)
                {
                    dbConnect.RollBack();

                    LogStr = "　ｴﾗｰ : " + ex.Message;
                    LogFile.NBaseHonsenLogWrite(LogFile.文書, 同期Client.LOGIN_USER.FullName, LogStr);
                }
            }


        }

        /// <summary>
        /// 船→SERVER へ送信するデータをＤＢから取得し DataSetへ格納する
        /// </summary>
        /// <returns></returns>
        private DataSet BuildUnsendDataSet()
        {
            return clientDb.BuildUnsendDocumentFileDataSet(); // "DM_KANRI_KIROKU_FILE", "DM_KOUBUNSHO_KISOKU_FILE" の２テーブルが対象
        }
        private DataSet BuildUnsendAttachFileDataSet()
        {
            return clientDb.BuildUnsendAttachFileDataSet(); // "OD_ATTACH_FILE" の１テーブルが対象
        }

        /// <summary>
        /// SERVER へ送信要求をするデータをＤＢから取得する
        /// </summary>
        /// <returns></returns>
        private List<String> BuildDiffMsDmTemplateFile()
        {
            List<string> ret = new List<string>();

            //===============================
            // 差分雛形ファイル
            //===============================
            List<MsDmHoukokusho> msDmHoukokusho = MsDmHoukokusho.GetTargetRecords(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);
            List<MsDmHoukokusho> FileNotFoundTemplateFile = MsDmHoukokusho.GetDataFileNotFoundRecords(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);
            foreach (MsDmHoukokusho houkokusho in FileNotFoundTemplateFile)
            {
                ret.Add(houkokusho.MsDmHoukokushoID);
            }
            List<MsDmHoukokusho> UpdateDateDiffTemplateFile = MsDmHoukokusho.GetDataUpdateDateDiffRecords(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);
            foreach (MsDmHoukokusho houkokusho in UpdateDateDiffTemplateFile)
            {
                if (ret.Contains(houkokusho.MsDmHoukokushoID) == false)
                {
                    ret.Add(houkokusho.MsDmHoukokushoID);
                }
            }

            報告書雛形_対象 = msDmHoukokusho.Count;
            報告書雛形_未同期 = ret.Count;

            return ret;
        }
        private List<String> BuildDiffDmKanriKirokuFile()
        {
            List<string> ret = new List<string>();

            //===============================
            // 管理記録ファイル
            //===============================
            List<DmKanriKiroku> dmKanriKiroku = DmKanriKiroku.GetTargetRecords(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);
            List<DmKanriKiroku> FileNotFoundKanriKiroku = DmKanriKiroku.GetDataFileNotFoundRecords(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);
            ret.Clear();
            foreach (DmKanriKiroku kanriKiroku in FileNotFoundKanriKiroku)
            {
                ret.Add(kanriKiroku.DmKanriKirokuID);
            }
            List<DmKanriKiroku> UpdateDateDiffKanriKiroku = DmKanriKiroku.GetDataUpdateDateDiffRecords(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);
            foreach (DmKanriKiroku kanriKiroku in UpdateDateDiffKanriKiroku)
            {
                if (ret.Contains(kanriKiroku.DmKanriKirokuID) == false)
                {
                    ret.Add(kanriKiroku.DmKanriKirokuID);
                }
            }

            管理記録_対象 = dmKanriKiroku.Count;
            管理記録_未同期 = ret.Count;

            管理記録_対象 = dmKanriKiroku.Count;
            管理記録_未同期 = ret.Count;

            return ret;
        }
        private List<String> BuildDiffDmKoubunshoKisokuFile()
        {
            List<string> ret = new List<string>();

            //===============================
            // 公文書_規則ファイル
            //===============================
            List<DmKoubunshoKisoku> dmKoubunshoKisoku = DmKoubunshoKisoku.GetTargetRecords(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);
            List<DmKoubunshoKisoku> FileNotFoundKoubunshoKisoku = DmKoubunshoKisoku.GetDataFileNotFoundRecords(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID); ;
            ret.Clear();
            foreach (DmKoubunshoKisoku koubunshoKisok in FileNotFoundKoubunshoKisoku)
            {
                ret.Add(koubunshoKisok.DmKoubunshoKisokuID);
            }
            List<DmKoubunshoKisoku> UpdateDateDiffKoubunshoKisok = DmKoubunshoKisoku.GetDataUpdateDateDiffRecords(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);
            foreach (DmKoubunshoKisoku koubunshoKisok in UpdateDateDiffKoubunshoKisok)
            {
                if (ret.Contains(koubunshoKisok.DmKoubunshoKisokuID) == false)
                {
                    ret.Add(koubunshoKisok.DmKoubunshoKisokuID);
                }
            }

            公文書規則_対象 = dmKoubunshoKisoku.Count;
            公文書規則_未同期 = ret.Count;

            return ret;
        }

        private List<String> BuildDiffOdAttachFile()
        {
            List<string> ret = new List<string>();

            //===============================
            // 添付ファイル
            //===============================
            List<OdAttachFile> FileNotFoundAttachFile = OdAttachFile.GetFileNotFoundRecords(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);
            ret.Clear();
            foreach (OdAttachFile attachFile in FileNotFoundAttachFile)
            {
                ret.Add(attachFile.OdAttachFileID);
            }

            return ret;
        }

        public static void 同期最新状況()
        {
            List<string> ret = new List<string>();
            //===============================
            // 差分雛形ファイル
            //===============================
            List<MsDmHoukokusho> msDmHoukokusho = MsDmHoukokusho.GetTargetRecords(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);
            List<MsDmHoukokusho> FileNotFoundTemplateFile = MsDmHoukokusho.GetDataFileNotFoundRecords(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);
            foreach (MsDmHoukokusho houkokusho in FileNotFoundTemplateFile)
            {
                ret.Add(houkokusho.MsDmHoukokushoID);
            }
            List<MsDmHoukokusho> UpdateDateDiffTemplateFile = MsDmHoukokusho.GetDataUpdateDateDiffRecords(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);
            foreach (MsDmHoukokusho houkokusho in UpdateDateDiffTemplateFile)
            {
                if (ret.Contains(houkokusho.MsDmHoukokushoID) == false)
                {
                    ret.Add(houkokusho.MsDmHoukokushoID);
                }
            }

            報告書雛形_対象 = msDmHoukokusho.Count;
            報告書雛形_未同期 = ret.Count;

            //===============================
            // 管理記録ファイル
            //===============================
            List<DmKanriKiroku> dmKanriKiroku = DmKanriKiroku.GetTargetRecords(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);
            List<DmKanriKiroku> FileNotFoundKanriKiroku = DmKanriKiroku.GetDataFileNotFoundRecords(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);
            List<DmKanriKirokuFile> unsendKanriKiroku = DmKanriKirokuFile.Get未送信Records(同期Client.LOGIN_USER);
            ret.Clear();
            foreach (DmKanriKiroku kanriKiroku in FileNotFoundKanriKiroku)
            {
                ret.Add(kanriKiroku.DmKanriKirokuID);
            }
            List<DmKanriKiroku> UpdateDateDiffKanriKiroku = DmKanriKiroku.GetDataUpdateDateDiffRecords(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);
            foreach (DmKanriKiroku kanriKiroku in UpdateDateDiffKanriKiroku)
            {
                if (ret.Contains(kanriKiroku.DmKanriKirokuID) == false)
                {
                    ret.Add(kanriKiroku.DmKanriKirokuID);
                }
            }

            管理記録_対象 = dmKanriKiroku.Count;
            管理記録_未同期 = ret.Count;
            管理記録_未送信 = unsendKanriKiroku.Count;

            //===============================
            // 公文書_規則ファイル
            //===============================
            List<DmKoubunshoKisoku> dmKoubunshoKisoku = DmKoubunshoKisoku.GetTargetRecords(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);
            List<DmKoubunshoKisoku> FileNotFoundKoubunshoKisoku = DmKoubunshoKisoku.GetDataFileNotFoundRecords(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID); ;
            List<DmKoubunshoKisokuFile> unsendKoubunshoKisoku = DmKoubunshoKisokuFile.Get未送信Records(同期Client.LOGIN_USER);
            ret.Clear();
            foreach (DmKoubunshoKisoku koubunshoKisok in FileNotFoundKoubunshoKisoku)
            {
                ret.Add(koubunshoKisok.DmKoubunshoKisokuID);
            }
            List<DmKoubunshoKisoku> UpdateDateDiffKoubunshoKisok = DmKoubunshoKisoku.GetDataUpdateDateDiffRecords(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);
            foreach (DmKoubunshoKisoku koubunshoKisok in UpdateDateDiffKoubunshoKisok)
            {
                if (ret.Contains(koubunshoKisok.DmKoubunshoKisokuID) == false)
                {
                    ret.Add(koubunshoKisok.DmKoubunshoKisokuID);
                }
            }

            公文書規則_対象 = dmKoubunshoKisoku.Count;
            公文書規則_未同期 = ret.Count;
            公文書規則_未送信 = unsendKoubunshoKisoku.Count;
        }

        /// <summary>
        /// DataSet を XML にシリアライズ
        /// </summary>
        /// <param name="unsendDataSet"></param>
        /// <returns></returns>
        private string ToXml(DataSet unsendDataSet)
        {
            string xml = "";
            using (System.IO.StringWriter xmlSW = new System.IO.StringWriter())
            {
                unsendDataSet.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                xml = xmlSW.ToString();
            }
            return xml;
        }



        #region
        //public void Run()
        //{
        //    Console.WriteLine("文書同期Clien：" + System.Threading.Thread.CurrentThread.GetHashCode());
        //    int 同期間隔 = 0;
        //    int 最大同期数 = 5;
        //    int ミリ秒 = 60000;
        //    int ＭＢ = 1024;

        //    while (runFlag)
        //    {
        //        if (runFlag == false)
        //        {
        //            break;
        //        }
        //        try
        //        {
        //            if (同期間隔 == 0)
        //            {
        //                同期間隔 = 1 * ミリ秒;
        //            }
        //            else
        //            {
        //                int[] Params = clientDb.GetDocumentParameter();
        //                同期間隔 = Params[0] * ミリ秒;
        //                最大同期数 = Params[1];
        //                ファイル最大サイズ = (Params[2] * ＭＢ);
        //            }
        //        }
        //        catch
        //        {
        //        }
        //        Thread.Sleep(同期間隔);

        //        if (SYNC_SUSPEND || OFFLINE)
        //        {
        //            continue;
        //        }

        //        SYNC_MESSAGE = "同期処理開始:" + DateTime.Now.ToString();

        //        try
        //        {
        //            // 未送信のデータを取得して DataSet に格納する.
        //            DataSet unsendDataSet = BuildUnsendDataSet();

        //            // XML にシリアライズ.
        //            string sendXml = ToXml(unsendDataSet);

        //            // 差分データファイルの取得要求をするため差分のあるデータファイルを取得
        //            #region
        //            int 同期枠 = 最大同期数;
        //            List<string> tmp = null;
        //            List<string> requestMsDmTemplateIds = new List<string>();
        //            List<string> requestDmKanriKirokuIds = new List<string>();
        //            List<string> requestDmKoubunshoKisokuIds = new List<string>();

        //            // テンプレートファイル
        //            tmp = BuildDiffMsDmTemplateFile();
        //            for (int i = 0; i < tmp.Count; i++)
        //            {
        //                if (同期枠 == 0)
        //                {
        //                    break;
        //                }
        //                同期枠--;
        //                requestMsDmTemplateIds.Add(tmp[i]);
        //            }
        //            // 管理記録ファイル
        //            if (同期枠 > 0)
        //            {
        //                tmp = BuildDiffDmKanriKirokuFile();
        //                for (int i = 0; i < tmp.Count; i++)
        //                {
        //                    if (同期枠 == 0)
        //                    {
        //                        break;
        //                    }
        //                    同期枠--;
        //                    requestDmKanriKirokuIds.Add(tmp[i]);
        //                }
        //            }
        //            // 公文書_規則ファイル
        //            if (同期枠 > 0)
        //            {
        //                tmp = BuildDiffDmKoubunshoKisokuFile();
        //                for (int i = 0; i < tmp.Count; i++)
        //                {
        //                    if (同期枠 == 0)
        //                    {
        //                        break;
        //                    }
        //                    同期枠--;
        //                    requestDmKoubunshoKisokuIds.Add(tmp[i]);
        //                }
        //            }
        //            #endregion

        //            // データ送受信.
        //            string receiveXml = SendAndReceive(sendXml, requestMsDmTemplateIds, requestDmKanriKirokuIds, requestDmKoubunshoKisokuIds);

        //            // 受信したデータを登録する
        //            NotifyReceiveData(receiveXml);

        //            SYNC_MESSAGE = "同期処理完了:" + DateTime.Now.ToString();
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine("文書同期処理中にエラー発生：" + e.Message);
        //        }
        //    }
        //}
        #endregion



        public void LastSync()
        {
            if (OFFLINE)
            {
                return;
            }

            string LogStr = "";
            string receiveXml = null;
            try
            {
                // 未送信のデータを取得して DataSet に格納する.
                DataSet unsendDataSet = BuildUnsendDataSet();
                DataSet unsendAttachFileDataSet = BuildUnsendAttachFileDataSet();

                if (unsendDataSet.Tables.Count > 0)
                {
                    LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, "〇文書同期Client.LastSync（１）");
                    foreach (DataTable table in unsendDataSet.Tables)
                    {
                        LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, table.TableName);

                        List<ISyncTable> models = MappingClass2.ToModel(table);
                        foreach (ISyncTable m in models)
                        {
                            string rowDataStr = "";
                            if (m is DmKanriKirokuFile)
                            {
                                rowDataStr = " ID[" + (m as DmKanriKirokuFile).DmKanriKirokuID + "]:";

                                rowDataStr += "VesselID[" + (m as DmKanriKirokuFile).VesselID.ToString() + "]:";

                            }
                            else if (m is DmKoubunshoKisokuFile)
                            {
                                rowDataStr = " ID[" + (m as DmKoubunshoKisokuFile).DmKoubunshoKisokuID + "]:";

                                rowDataStr += "VesselID[" + (m as DmKoubunshoKisokuFile).VesselID.ToString() + "]:";
                            }
                            if (rowDataStr.Length > 0)
                                LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, rowDataStr);
                        }
                    }
                }


                if (unsendAttachFileDataSet.Tables.Count > 0)
                {
                    LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, "〇文書同期Client.LastSync（２）");
                    foreach (DataTable table in unsendAttachFileDataSet.Tables)
                    {
                        LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, table.TableName);

                        List<ISyncTable> models = MappingClass2.ToModel(table);
                        foreach (ISyncTable m in models)
                        {
                            string rowDataStr = "";
                            if (m is OdAttachFile)
                            {
                                rowDataStr = " ID[" + (m as OdAttachFile).OdAttachFileID + "]:";

                                rowDataStr += "VesselID[" + (m as OdAttachFile).VesselID.ToString() + "]:";

                            }
                            if (rowDataStr.Length > 0)
                                LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, rowDataStr);
                        }
                    }
                }

                // １データづつ送信処理を実施する
                try
                {
                    foreach (DataTable dt in unsendAttachFileDataSet.Tables)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            DataTable oneDT = dt.Clone();
                            oneDT.ImportRow(dr);

                            DataSet oneDS = new DataSet();
                            oneDS.Tables.Add(oneDT);

                            // XML にシリアライズ.
                            string sendXml = ToXml(oneDS);

                            // データ送信.
                            // 送信したデータは、サーバでフラグをセットして戻ってくる
                            receiveXml = SendAttachFile(sendXml);

                            // 戻ってきたデータを登録する
                            NotifyReceiveData(receiveXml);
                        }
                    }


                    foreach (DataTable dt in unsendDataSet.Tables)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            DataTable oneDT = dt.Clone();
                            oneDT.ImportRow(dr);

                            DataSet oneDS = new DataSet();
                            oneDS.Tables.Add(oneDT);

                            // XML にシリアライズ.
                            string sendXml = ToXml(oneDS);

                            // データ送信.
                            // 送信したデータは、サーバでフラグをセットして戻ってくる
                            receiveXml = Send(sendXml);

                            // 戻ってきたデータを登録する
                            NotifyReceiveData(receiveXml);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception：" + e.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("文書同期処理中にエラー発生：" + e.Message);

                LogStr = "文書同期処理中にエラー発生：" + e.Message;
                LogFile.NBaseHonsenLogWrite(LogFile.文書, 同期Client.LOGIN_USER.FullName, LogStr);
            }
        }
    }
}
