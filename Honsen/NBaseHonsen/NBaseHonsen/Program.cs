using NBaseCommon;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;
using ORMapping;
using ORMapping.PostgreSql;
using SyncClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using WtmData;

namespace NBaseHonsen
{
    static class Program
    {
        static System.Threading.Mutex mutex = null;
        static 同期Client client = null;
        static Thread thread = null;
        static 文書同期Client client2 = null;
        static Thread thread2 = null;
        static WtmSyncClient client3 = null;
        static Thread thread3 = null;

        static Splash splash = null;
        static SyncSplash syncSplash = null;

        static LoginForm loginDialog = null;
        static PortalForm portalForm = null;


        static bool MaintenanceFlag = false;


        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew;
            //System.Threading.Mutex mutex =
            //    new System.Threading.Mutex(true, "NBaseHonsen", out createdNew);
            mutex = new System.Threading.Mutex(true, "NBaseHonsen", out createdNew);

            if (createdNew == false)
            {
                MessageBox.Show("既に NBaseHonsen プログラムは起動しています。");
                return;
            }
            NBaseCommon.FileView.Remove();

            LogFile.NBaseHonsenLogClear();


            //==============================================================================================================
            //// リクエストを投げる前に行う
            //if (System.Net.ServicePointManager.ServerCertificateValidationCallback == null)
            //{
            //    System.Net.ServicePointManager.ServerCertificateValidationCallback += delegate(Object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            //    System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
            //    {
            //        return true;	// 無条件でオレオレ証明を信用する。危険！(senderのURIとか調べてチェックすべし！)
            //    };
            //}

            NBaseUtil.SSLCertificateVaridation sslCertificateVaridation = new NBaseUtil.SSLCertificateVaridation();



            ORMapping.Common.DBTYPE = ORMapping.Common.DB_TYPE.POSTGRESQL_CLIENT;
            ORMapping.Common.EXECUTE_READER_LOG = false;
            ORMapping.Common.EXECUTE_NON_QUERY_LOG = false;

            NbaseContractFunctionTableCache.instance().DacProxy = new DirectNBaseContractFunctionDacProxy();
            MsRoleTableCache.instance().DacProxy = new DirectMsRoleDacProxy();
            SeninTableCache.instance().DacProxy = new DirectSeninDacProxy();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            UIConstants.DEFAULT_FONT = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
            NBaseCommon.Common.AppName = "NBaseHonsen";

            System.Threading.Thread.CurrentThread.Name = "Main thread";

            //同期Client client = new 同期Client();
            client = new 同期Client();
            同期Client.BASE_FOLDER = System.IO.Directory.GetCurrentDirectory();
            //Thread thread = null;


            //文書同期Client client2 = new 文書同期Client();
            client2 = new 文書同期Client();
            //Thread thread2 = null;

            //WtmSyncClient client3 = new WtmSyncClient();
            client3 = new WtmSyncClient();
            //Thread thread3 = null;


            if (ApplicationDeployment.IsNetworkDeployed == true)
            {
                Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            }




            // 起動処理
            if (!SyncClient.Common.オフラインモード)
            {
                client.dataSync += new SyncClient.同期Client.DataSyncEventHandler(DataSync);
                client.FirstSync();

                Thread.Sleep(1000);

                CheckMaintananceFlag();
            }
            同期Client.LOGIN_STATE = 同期Client.LoginState.BEFORE_LOGIN;

            if (MaintenanceFlag == false)
            {
                if (!SyncClient.Common.オフラインモード)
                {
                    // 同期スレッド生成＆起動
                    thread = new Thread(new ThreadStart(client.Run));
                    thread.Name = "Sync data thread";
                    thread.Start();

                    //client.dataSync += new SyncClient.同期Client.DataSyncEventHandler(DataSync); // 上の起動処理で実施に変更

                    if (!NetworkInterface.GetIsNetworkAvailable())
                    {
                        同期Client.OFFLINE = true;
                    }
                }
            }


            try
            {
                if (MaintenanceFlag == false)
                {
                    Console.WriteLine("Main Tread：" + System.Threading.Thread.CurrentThread.GetHashCode());

                    //Splash splash = new Splash();
                    splash = new Splash();

                    splash.ShowDialog();

                    ////if (同期Client.OFFLINE == false && client.START_POS_OF_ZERO != client.SPLIT_COUNT)
                    //if (client.START_POS_OF_ZERO != client.SPLIT_COUNT)
                    //{
                    //    MessageBox.Show("マスタデータの同期に失敗しました。",
                    //        "同期エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    //if (client.START_POS_OF_ZERO != client.SPLIT_COUNT)
                    if (MaintenanceFlag == false && client.START_POS_OF_ZERO != client.SPLIT_COUNT)
                    {
                        MessageBox.Show("マスタデータの同期に失敗しました。",
                        "同期エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (MaintenanceFlag == false)
                    {
                        // Login処理
                        //LoginForm loginDialog = new LoginForm();
                        loginDialog = new LoginForm();

                        if (loginDialog.ShowDialog() == DialogResult.OK)
                        {
                            同期Client.LOGIN_STATE = 同期Client.LoginState.JUST_LOGIN;
                            同期Client.SYNC_SUSPEND = false;

                            //SyncSplash syncSplash = new SyncSplash();
                            syncSplash = new SyncSplash();

                            if (syncSplash.ShowDialog() == DialogResult.OK)
                            {
                                //if (client.START_POS != client.SPLIT_COUNT)
                                if (MaintenanceFlag == false && client.START_POS != client.SPLIT_COUNT)
                                {
                                    MessageBox.Show("データの同期に失敗しました。",
                                    "同期エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                                if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.文書) == true &&
                                    同期Client.LOGIN_VESSEL.DocumentEnabled == 1)
                                {
                                    thread2 = new Thread(new ThreadStart(client2.Run));
                                    thread2.Name = "Sync DocumentData thread";
                                    thread2.Start();
                                }



                                // 勤怠管理
                                //{
                                if (MaintenanceFlag == false)
                                {
                                    bool loginByCrew = false;
                                    if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["VesselLoginByCrew"]) == false)
                                    {
                                        var val = System.Configuration.ConfigurationManager.AppSettings["VesselLoginByCrew"];
                                        loginByCrew = (val.ToUpper() == "TRUE") ? true : false;
                                    }

                                    if (loginByCrew == false || loginByCrew && NBaseCommon.Common.siCard != null)
                                    {
                                        WTM.Common.LoginUser = NBaseCommon.Common.LoginUser;

                                        // 設定読込
                                        NBaseCommon.Common.Read();
                                        WtmCommon.ReadConfig();

                                        WtmCommon.VesselMode = true;

                                        // Wtmアクセッサ
                                        WtmAccessor.Instance().DacProxy = new LocalAccessor(WtmCommon.ConnectionKey);


                                        AccessorCommon.ConnectionString = SyncClient.Common.Wtm接続文字列;


                                        WtmDac.SITE_ID = SyncClient.Common.KEY.ToUpper();
                                        WtmDac.MODULE_ID = WtmSyncClient.GetHostName();
                                        WtmDac.SEND_FLAG = (int)NBaseUtil.StatusUtils.通信状況.未同期;

                                        WTM.Common.Vessel = 同期Client.LOGIN_VESSEL;
                                        if (NBaseCommon.Common.siCard != null)
                                        {
                                            WTM.Common.Senin = MsSenin.GetRecord(WTM.Common.LoginUser, NBaseCommon.Common.siCard.MsSeninID);
                                        }

                                        thread3 = new Thread(new ThreadStart(client3.Run));
                                        thread3.Name = "Sync thread";
                                        thread3.Start();
                                    }
                                }


                                //PortalForm form = new PortalForm();
                                //Application.Run(form);

                                if (MaintenanceFlag == false)
                                {
                                    portalForm = new PortalForm();
                                    Application.Run(portalForm);
                                }
                            }
                        }
                    }

                }
            }
            finally
            {

                //if (!SyncClient.Common.オフラインモード)
                //{                      
                //    if (thread.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
                //    {
                //        thread.Abort();
                //    }
                //    else
                //    {
                //        client.Stop();
                //    }
                //    if (thread2 != null && thread2.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
                //    {
                //        thread2.Abort();
                //    }
                //    else 
                //    {
                //        client2.Stop();
                //    }
                //    if (thread3 != null && thread3.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
                //    {
                //        thread3.Abort();
                //    }
                //    else 
                //    {
                //        client3.Stop();
                //    }

                //    ProgressDialog progressDialog = new ProgressDialog(delegate
                //    {
                //        client.LastSync();
                //        client2.LastSync();

                //    }, "終了処理をしています...");
                //    progressDialog.ShowDialog();


                //    thread.Join();
                //    if (thread2 != null)
                //    {
                //        thread2.Join();
                //    }
                //    if (thread3 != null)
                //    {
                //        thread3.Join();
                //    }
                //}


                finalyProc();



                if (MaintenanceFlag)
                {
                    NBaseData.DAC.SnParameter sp = SnParameter.GetRecord(同期Client.LOGIN_USER);
                    MaintenanceMessageForm mmform = new MaintenanceMessageForm();
                    mmform.Caption = "メンテナンス中";
                    mmform.Message = sp.MaintenanceMessage;
                    mmform.ShowDialog();
                }


                //PostgresqlConnectionPool.Instance().Dispose();

                ////ミューテックスを解放する
                //mutex.ReleaseMutex();

                finalyDispose();
            }

        }


        static void finalyProc()
        {
            System.Diagnostics.Debug.WriteLine("finalyProc() start");
            if (!SyncClient.Common.オフラインモード)
            {
                if (splash != null)
                {
                    splash.SyncFinish();
                }
                if (syncSplash != null)
                {
                    syncSplash.SyncFinish();
                }
                if (thread != null && thread.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
                {
                    thread.Abort();
                }
                else
                {
                    client.Stop();
                }
                if (thread2 != null && thread2.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
                {
                    thread2.Abort();
                }
                else
                {
                    client2.Stop();
                }
                if (thread3 != null && thread3.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
                {
                    thread3.Abort();
                }
                else
                {
                    client3.Stop();
                }

                if (client != null)
                {
                    ProgressDialog progressDialog = new ProgressDialog(delegate
                    {
                        client.LastSync();

                    }, "終了処理をしています...");
                    progressDialog.ShowDialog();
                }
                if (client2 != null)
                {
                    ProgressDialog progressDialog = new ProgressDialog(delegate
                    {
                        client2.LastSync();

                    }, "終了処理をしています...");
                    progressDialog.ShowDialog();
                }

                if (thread != null)
                {
                    thread.Join();
                }
                if (thread2 != null)
                {
                    thread2.Join();
                }
                if (thread3 != null)
                {
                    thread3.Join();
                }
            }
            System.Diagnostics.Debug.WriteLine("finalyProc() end");
        }
        static void finalyDispose()
        {
            System.Diagnostics.Debug.WriteLine("finalyDispose()");
            PostgresqlConnectionPool.Instance().Dispose();

            //ミューテックスを解放する
            mutex.ReleaseMutex();
        }


        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageForm form = new MessageForm(e.Exception.ToString());
            form.ShowDialog();
            Application.Exit();
        }


        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageForm form = new MessageForm(e.ExceptionObject.ToString());
            form.ShowDialog();
            Application.Exit();
        }





        public static void DataSync(object sender, SyncClient.同期EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DataSync()");
            System.Diagnostics.Debug.WriteLine($"   {e.Type}");
            if (e.Type == 同期EventArgs.EventType.メッセージ)
            {
                System.Diagnostics.Debug.WriteLine($"       {e.Message}");
            }



            string LogStr = "";

            if (同期Client.LOGIN_STATE != 同期Client.LoginState.JUST_START)
            {
                CheckMaintananceFlag();
            }



            if (e.Type == 同期EventArgs.EventType.メッセージ)
            {
                DataSyncReporter.instance().NotifyMessage(e.Message);
            }
            else if (e.Type == 同期EventArgs.EventType.同期開始)
            {
                DataSyncReporter.instance().NotifySyncStart();
                //====== LOG ==========
                LogStr = "同期開始";
                LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, LogStr);
            }
            else if (e.Type == 同期EventArgs.EventType.同期処理)
            {

                if (MaintenanceFlag == true)
                {
                    System.Diagnostics.Debug.WriteLine(" => Maintenance中");

                    if (splash != null)
                    {
                        splash.SyncFinish();
                        splash = null;
                    }

                    if (syncSplash != null)
                    {
                        syncSplash.SyncFinish();
                        syncSplash = null;
                    }
                    return;
                }

                using (DBConnect dbConnect = new DBConnect())
                {
                    dbConnect.BeginTransaction();

                    try
                    {
                        decimal maxDataNoOfVesselIdZero = 0;
                        decimal maxDataNo = 0;
                        maxDataNoOfVesselIdZero = 同期Client.MAX_DATA_NO_OF_ZERO;
                        maxDataNo = 同期Client.MAX_DATA_NO;

                        int count = 1;

                        foreach (DataTable table in e.ds.Tables)
                        {
                            Console.WriteLine("処理中：" + table.TableName);
                            int percentage = (int)((float)count / (float)(e.ds.Tables.Count) * 100);

                            DataSyncReporter.instance().NotifyMessage("処理中：" + table.TableName + "...");
                            DataSyncReporter.instance().NotifyMessage2("データ変換中.   ");

                            List<ISyncTable> models = MappingClass2.ToModel(table);



                            //====== LOG ==========
                            LogStr = "　同期処理 : ";
                            LogStr += "処理中：" + table.TableName + " : Count = " + models.Count.ToString();
                            LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, LogStr);


                            int m_cnt = 0;
                            int unit = (models.Count / 10);
                            foreach (ISyncTable m in models)
                            {
                                m_cnt++;
                                if (m_cnt == 1)
                                {
                                    DataSyncReporter.instance().NotifyMessage2("データ保存中...   0%");
                                }
                                else if (m_cnt == models.Count)
                                {
                                    DataSyncReporter.instance().NotifyMessage2("データ保存中...   100%");
                                }
                                else if (unit > 0)
                                {
                                    if ((m_cnt % unit) == 0)
                                    {
                                        DataSyncReporter.instance().NotifyMessage2("データ保存中...   " + ((int)((float)m_cnt / (float)unit) * 10).ToString() + "%");
                                    }
                                }

                                long dataNo = SyncTableSaver.InsertOrUpdate(m, 同期Client.LOGIN_USER, StatusUtils.通信状況.同期済, dbConnect);


                                //====== LOG ==========
                                LogStr = "　　";
                                if (dataNo == -1)
                                {
                                    LogStr += "登録ｴﾗｰ : ";
                                }
                                LogStr += table.TableName + " = " + GetID(m);
                                LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, LogStr);


                                if (table.TableName == "DM_PUBLISHER" ||
                                    table.TableName == "DM_KOUKAI_SAKI" ||
                                    table.TableName == "DM_KAKUNIN_JOKYO" ||
                                    table.TableName == "DM_DOC_COMMENT" ||
                                    table.TableName == "DM_KANRYO_INFO" ||
                                    table.TableName == "DM_KANRI_KIROKU" ||
                                    table.TableName == "DM_KOUBUNSHO_KISOKU")
                                {
                                    if (maxDataNo < dataNo)
                                    {
                                        maxDataNo = dataNo;
                                    }
                                }
                                else
                                {
                                    if (m.VesselID > 0 && maxDataNo < dataNo)
                                    {
                                        maxDataNo = dataNo;
                                    }

                                    if (m.VesselID == 0 && maxDataNoOfVesselIdZero < dataNo)
                                    {
                                        maxDataNoOfVesselIdZero = dataNo;
                                    }
                                }
                            }
                            DataSyncReporter.instance().NotifyMessage3(table.TableName + "...  完了");

                            count++;
                        }
                        DataSyncReporter.instance().NotifyMessage2("");
                        Console.WriteLine("処理終了");

                        同期Client.MAX_DATA_NO_OF_ZERO = maxDataNoOfVesselIdZero;
                        同期Client.MAX_DATA_NO = maxDataNo;

                        //====== LOG ==========
                        LogStr = "　同期処理 : ";
                        LogStr += "MaxDataNoOfVesselIdZero[" + maxDataNoOfVesselIdZero.ToString() + "]:";
                        LogStr += "maxDataNo[" + maxDataNo.ToString() + "]:";
                        LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, LogStr);

                        dbConnect.Commit();
                    }
                    catch (Exception ex)
                    {
                        LogStr = "　同期処理 : ﾃﾞｰﾀ保存ｴﾗｰ : "　+ ex.Message;
                        LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, LogStr);

                        dbConnect.RollBack();
                    }
                }
            }
            else if (e.Type == 同期EventArgs.EventType.同期完了)
            {
                using (DBConnect dbConnect = new DBConnect())
                {
                    dbConnect.BeginTransaction();

                    try
                    {
                        decimal maxDataNoOfVesselIdZero = 同期Client.MAX_DATA_NO_OF_ZERO;
                        decimal maxDataNo = 同期Client.MAX_DATA_NO;

                        //====== LOG ==========
                        LogStr = "同期完了(DB更新前) : ";
                        LogStr += "MaxDataNoOfVesselIdZero[" + maxDataNoOfVesselIdZero.ToString() + "]:";
                        LogStr += "maxDataNo[" + maxDataNo.ToString() + "]:";
                        LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, LogStr);


                        同期Client client = sender as 同期Client;
                        client.SetMaxDataNo(maxDataNoOfVesselIdZero, maxDataNo, dbConnect);
                        同期Client.MAX_DATA_NO = maxDataNo;


                        //====== LOG ==========
                        LogStr = "同期完了(DB更新後) : ";
                        LogStr += "MaxDataNoOfVesselIdZero[" + maxDataNoOfVesselIdZero.ToString() + "]:";
                        LogStr += "maxDataNo[" + maxDataNo.ToString() + "]:";
                        LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, LogStr);


                        dbConnect.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbConnect.RollBack();
                    }
                }


                DataSyncReporter.instance().NotifySyncFinish();

                if (MaintenanceFlag == true)
                {
                    System.Diagnostics.Debug.WriteLine(" => Maintenance中");

                    client.Stop();
                    if (portalForm != null)
                    {
                        portalForm.AbortForm();
                    }
                    return;
                }
            }
            #region [同期終了]は利用していない
            //else if (e.Type == 同期EventArgs.EventType.同期終了)
            //{
            //    using (DBConnect dbConnect = new DBConnect())
            //    {
            //        dbConnect.BeginTransaction();

            //        try
            //        {
            //            decimal maxDataNoOfVesselIdZero = 0;
            //            decimal maxDataNo = 0;
            //            int count = 1;

            //            foreach (DataTable table in e.ds.Tables)
            //            {
            //                Console.WriteLine("処理中：" + table.TableName);
            //                int percentage = (int)((float)count / (float)(e.ds.Tables.Count) * 100);

            //                //DataSyncReporter.instance().NotifyMessage("データ変換中.   " + percentage + "%");
            //                DataSyncReporter.instance().NotifyMessage("処理中：" + table.TableName + "...  (" + count + "/" + e.ds.Tables.Count + ")");
            //                DataSyncReporter.instance().NotifyMessage2("データ変換中.   ");

            //                List<ISyncTable> models = MappingClass2.ToModel(table);

            //                //DataSyncReporter.instance().NotifyMessage("データ保存中..  " + percentage + "%");

            //                int m_cnt = 0;
            //                int unit = (models.Count / 10);
            //                foreach (ISyncTable m in models)
            //                {
            //                    m_cnt++;
            //                    //DataSyncReporter.instance().NotifyMessage2("データ保存中...   (" + m_cnt + "/" + models.Count + ")");
            //                    if (m_cnt == 1)
            //                    {
            //                        DataSyncReporter.instance().NotifyMessage2("データ保存中...   0%");
            //                    }
            //                    else if (m_cnt == models.Count)
            //                    {
            //                        DataSyncReporter.instance().NotifyMessage2("データ保存中...   100%");
            //                    }
            //                    else if (unit > 0)
            //                    {
            //                        if ((m_cnt % unit) == 0)
            //                        {
            //                            DataSyncReporter.instance().NotifyMessage2("データ保存中...   " + ((int)((float)m_cnt / (float)unit) * 10).ToString() + "%");
            //                        }
            //                    }

            //                    long dataNo = SyncTableSaver.InsertOrUpdate(m, 同期Client.LOGIN_USER, StatusUtils.通信状況.同期済, dbConnect);

            //                    if (table.TableName == "DM_PUBLISHER" ||
            //                        table.TableName == "DM_KOUKAI_SAKI" ||
            //                        table.TableName == "DM_DOC_COMMENT" ||
            //                        table.TableName == "DM_KANRYO_INFO" ||
            //                        table.TableName == "DM_KANRI_KIROKU" ||
            //                        table.TableName == "DM_KOUBUNSHO_KISOKU")
            //                    {
            //                        if (maxDataNo < dataNo)
            //                        {
            //                            maxDataNo = dataNo;
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if (m.VesselID > 0 && maxDataNo < dataNo)
            //                        {
            //                            maxDataNo = dataNo;
            //                        }

            //                        if (m.VesselID == 0 && maxDataNoOfVesselIdZero < dataNo)
            //                        {
            //                            maxDataNoOfVesselIdZero = dataNo;
            //                        }
            //                    }
            //                }

            //                DataSyncReporter.instance().NotifyMessage3(table.TableName + "...  完了");

            //                count++;
            //            }
            //            Console.WriteLine("処理終了");

            //            同期Client client = sender as 同期Client;
            //            client.SetMaxDataNo(maxDataNoOfVesselIdZero, maxDataNo, dbConnect);

            //            dbConnect.Commit();
            //        }
            //        catch (Exception ex)
            //        {
            //            dbConnect.RollBack();
            //        }
            //        finally
            //        {
            //            DataSyncReporter.instance().NotifySyncFinish();
            //        }
            //    }
            //}
            #endregion
            else if (e.Type == 同期EventArgs.EventType.モジュール更新)
            {
                DataSyncReporter.instance().NotifyModuleUpdate();
            }
            else if (e.Type == 同期EventArgs.EventType.同期エラー)
            {
                DataSyncReporter.instance().NotifySyncError(e.Message);

                //====== LOG ==========
                LogStr = "同期エラー";
                LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, LogStr);
            }
            else if (e.Type == 同期EventArgs.EventType.オンライン)
            {
                DataSyncReporter.instance().NotifyOnline();


                //====== LOG ==========
                LogStr = "オンライン";
                LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, LogStr);
            }
            else if (e.Type == 同期EventArgs.EventType.オフライン)
            {
                DataSyncReporter.instance().NotifyOffline();


                //====== LOG ==========
                LogStr = "オフライン";
                LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, LogStr);
            }
            else if (e.Type == 同期EventArgs.EventType.起動処理)
            {
                using (DBConnect dbConnect = new DBConnect())
                {
                    dbConnect.BeginTransaction();

                    try
                    {
                        int count = 1;

                        foreach (DataTable table in e.ds.Tables)
                        {
                            Console.WriteLine("処理中：" + table.TableName);

                            List<ISyncTable> models = MappingClass2.ToModel(table);

                            foreach (ISyncTable m in models)
                            {
                                long dataNo = SyncTableSaver.InsertOrUpdate(m, 同期Client.LOGIN_USER, StatusUtils.通信状況.同期済, dbConnect);
                            }

                            count++;
                        }
                        DataSyncReporter.instance().NotifyMessage2("");
                        Console.WriteLine("処理終了");

                        dbConnect.Commit();
                    }
                    catch (Exception ex)
                    {
                        LogStr = "　同期処理 : ﾃﾞｰﾀ保存ｴﾗｰ : " + ex.Message;
                        LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, LogStr);

                        dbConnect.RollBack();
                    }
                }
            }
        }

        private static string GetID(ISyncTable o)
        {
            string id = "";

            if (o is OdThi)
            {
                id = (o as OdThi).OdThiID;
            }
            else if (o is OdThiItem)
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

        private static void CheckMaintananceFlag()
        {
            NBaseData.DAC.SnParameter sp = SnParameter.GetRecord(同期Client.LOGIN_USER);
            if (sp.MaintenanceFlag == 1)
            {
                MaintenanceFlag = true;
            }
            else
            {
                MaintenanceFlag = false;
            }
        }

    }
}
