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
using NBaseCommon;
using NBaseData.DS;

namespace SyncClient
{
    /// <summary>
    /// 同期処理のためのクラス(クライアント側)
    /// </summary>
    public class 同期Client
    {
        //public enum LoginState {BEFORE_LOGIN, JUST_LOGIN, AFTER_LOGIN};
        //public static LoginState LOGIN_STATE = LoginState.BEFORE_LOGIN;
        public enum LoginState {JUST_START, BEFORE_LOGIN, JUST_LOGIN, AFTER_LOGIN };
        public static LoginState LOGIN_STATE = LoginState.JUST_START;

        public static MsUser LOGIN_USER;
        public static MsVessel LOGIN_VESSEL;

        // 前回ログイン時の船 ID.
        public static int LAST_LOGIN_VESSEL_ID;

        public static bool SYNC_SUSPEND;
        public static bool OFFLINE;
        
        public static string VERSION_STRING;
        public static string SERVER_STRING;
        
        public delegate void DataSyncEventHandler(object sender, 同期EventArgs e);
        public event DataSyncEventHandler dataSync;

        private bool runFlag = true;

        private ServiceReference1.IService1 serviceClient;
        private IClientDb clientDb;

        public static string MODULE_VERSION;//ClickOnceのVersion
        public static DateTime SYNC_DATE;
        public static decimal MAX_DATA_NO_OF_ZERO = 0;
        public static decimal MAX_DATA_NO = 0;
        public static string SYNC_MESSAGE;

        public static string BASE_FOLDER;

        public int SPLIT_COUNT = 94;// Configに設定されていない場合
        public int START_POS_OF_ZERO = 0;
        public int START_POS = 0;

        private static string GetMacAddress()
        {
            string macAddress = "";
            string hostname = System.Net.Dns.GetHostName();
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                if (adapter.OperationalStatus.Equals(OperationalStatus.Up))
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    foreach (IPAddressInformation ipinfo in properties.UnicastAddresses)
                    {
                        System.Net.IPAddress ip = ipinfo.Address;
                        if (!System.Net.IPAddress.IsLoopback(ip))
                        {
                            macAddress = adapter.GetPhysicalAddress().ToString();
                        }
                    }
                }
            }
            return macAddress;
        }
        public static string GetHostName()
        {
            string hostname = System.Net.Dns.GetHostName();
            return hostname;
        }

        static 同期Client()
        {
            // ログイン前はデフォルトのユーザ（HonsenDefaultUser）.
            LOGIN_USER = new MsUser();
            LOGIN_USER.LoginID = "HonsenDefaultUser";
            LOGIN_VESSEL = new MsVessel();
            LOGIN_VESSEL.MsVesselID = 0;
            
            // Version
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                Version version = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                MODULE_VERSION = version.ToString();
                VERSION_STRING = "Version: " + Application.ProductVersion + "\n" +
                                 "ClickOnce Version: " + MODULE_VERSION + "\n" +
                                 "PC: " + GetHostName();
            }
            else
            {
                MODULE_VERSION = "";
                VERSION_STRING = "Version: " + Application.ProductVersion + "\n" +
                                 "ClickOnce Version: ---- \n" +
                                 "PC: " + GetHostName(); //"MacAddress: " + GetMacAddress();
            }
        }

        public 同期Client()
            : this(WcfServiceWrapper.GetInstance().GetServiceClient(), new PostgresqlClientDB())
        {
        }
     

        public 同期Client(ServiceReference1.IService1 serviceClient)
            : this(serviceClient, new PostgresqlClientDB())
        {
        }

        public 同期Client(ServiceReference1.IService1 serviceClient, IClientDb clientDb)
        {
            this.serviceClient = serviceClient;
            this.clientDb = clientDb;

            InitServerString(serviceClient);

            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);

            this.CreateDb();

            this.AlterTables();
        }

        void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            同期EventArgs eventArgs = new 同期EventArgs();

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                eventArgs.Type = 同期EventArgs.EventType.オンライン;
                OFFLINE = false;
            }
            else
            {
                eventArgs.Type = 同期EventArgs.EventType.オフライン;
                OFFLINE = true;
            }
            
            dataSync(this, eventArgs);
        }

        private static void InitServerString(ServiceReference1.IService1 serviceClient)
        {
            // Server
            if (serviceClient is ServiceReference1.Service1Client)
            {
                ServiceReference1.Service1Client cli = serviceClient as ServiceReference1.Service1Client;
                SERVER_STRING = "Server: " + cli.Endpoint.Address.Uri.Host;
            }
            else
            {
                SERVER_STRING = "Server: ----";
            }
        }

        /// <summary>
        /// データベース作成
        /// </summary>
        private void CreateDb()
        {
            clientDb.CreateDb();
        }

        /// <summary>
        /// データベース変更
        /// </summary>
        private void AlterTables()
        {
            clientDb.AlterTables();
        }



        public void Stop()
        {
            runFlag = false;
        }

        public void Run()
        {
            System.Diagnostics.Debug.WriteLine("Run()");

            Console.WriteLine("同期Client：" + System.Threading.Thread.CurrentThread.GetHashCode());
            try
            {
                SPLIT_COUNT = Common.SplitCount;
            }
            catch
            {
            }

            while (runFlag)
            {
                Thread.Sleep(100);

                if (LOGIN_STATE == LoginState.AFTER_LOGIN)
                {
                    Thread.Sleep(Common.同期間隔);
                    SPLIT_COUNT = 1;　// ログイン後は、同期分割しないので１を設定
                }

                if (runFlag == false)
                {
                    break;
                }

                if (SYNC_SUSPEND || OFFLINE)
                {
                    continue;
                }

                List<string> receiveXml = null;
                try
                {
                    NotifySyncStart();

                    // 未送信のデータを取得して DataSet に格納する.
                    DataSet unsendDataSet = BuildUnsendDataSet();
                    // XML にシリアライズ.
                    string sendXml = ToXml(unsendDataSet);

                    // データ送受信.
                    //string receiveXml = SendAndReceive(sendXml); // 2012.04.10 旧Ver
                    receiveXml = SendAndReceive(sendXml);

                    // 2012.04.10 旧Ver
                    //// モジュールが更新されたとき.
                    //if (receiveXml.Contains("<DbVersion>"))
                    //{
                    //    updateModule();
                    //    break;
                    //}

                    // 受信したデータをメインスレッドに渡す.
                    //NotifyReceiveData(receiveXml);

                    同期EventArgs eventArgs = new 同期EventArgs();
                    eventArgs.Type = 同期EventArgs.EventType.同期完了;
                    eventArgs.Message = DateTime.Now.ToString("yyyyMMdd hh:mm:ss") + " イベント";
                    dataSync(this, eventArgs);

                    // ログイン前に一度同期したあとは待機する.
                    if (LOGIN_STATE == LoginState.BEFORE_LOGIN)
                    {
                        SYNC_SUSPEND = true;
                    }
                    else if (LOGIN_STATE == LoginState.JUST_LOGIN)
                    {
                        LOGIN_STATE = LoginState.AFTER_LOGIN;
                    }
                }
                catch (Exception e)
                {
                    //if (receiveXml != null)
                    //{
                    //    // 受信したところまでのデータをメインスレッドに渡す.
                    //    NotifyReceiveData(receiveXml);
                    //}

                    Console.WriteLine("同期処理中にエラー発生：" + e.Message);
                    同期EventArgs eventArgs = new 同期EventArgs();
                    eventArgs.Type = 同期EventArgs.EventType.同期エラー;
                    eventArgs.Message = "同期処理中にエラーが発生しました：" + e.Message;
                    dataSync(this, eventArgs);
                }
            }
        }
        private void NotifySyncStart()
        {
            同期EventArgs eventArgs = new 同期EventArgs();

            eventArgs.Type = 同期EventArgs.EventType.同期開始;
            eventArgs.Message = DateTime.Now.ToString("yyyyMMdd hh:mm:ss") + " イベント";

            if (dataSync != null)
            {
                // データ受信をメインスレッドに通知する.
                dataSync(this, eventArgs);
            }
        }

        private void NotifyReceiveData(string receiveXml)
        {
            同期EventArgs eventArgs = new 同期EventArgs();

            eventArgs.Type = 同期EventArgs.EventType.同期処理;
            eventArgs.Message = DateTime.Now.ToString("yyyyMMdd hh:mm:ss") + " イベント";

            DataSet dataSet = new DataSet();

            NotifyMessage("受信データ解析中...");
            using (System.IO.StringReader xmlSR = new System.IO.StringReader(receiveXml))
            {
                dataSet.ReadXml(xmlSR);
            }

            eventArgs.ds = dataSet;

            if (dataSync != null)
            {
                // データ受信をメインスレッドに通知する.
                dataSync(this, eventArgs);
            }

        }

        private void NotifyModuleUpdate()
        {
            同期EventArgs eventArgs = new 同期EventArgs();

            eventArgs.Type = 同期EventArgs.EventType.モジュール更新;
            eventArgs.Message = DateTime.Now.ToString("yyyyMMdd hh:mm:ss") + " イベント";

            if (dataSync != null)
            {
                // データ受信をメインスレッドに通知する.
                dataSync(this, eventArgs);
            }
        }

        private void NotifyMessage(string message)
        {
            同期EventArgs eventArgs = new 同期EventArgs();

            eventArgs.Type = 同期EventArgs.EventType.メッセージ;
            eventArgs.Message = message;

            if (dataSync != null)
            {
                // データ受信をメインスレッドに通知する.
                dataSync(this, eventArgs);
            }
        }

        private List<string> SendAndReceive(string sendXml)
        {
            System.Diagnostics.Debug.WriteLine("SendAndReceive()");

            //====== LOG ==========
            string LogStr = "";


            SYNC_MESSAGE = "同期処理中";

            Console.WriteLine("同期開始：" + DateTime.Now.ToString());


            //====== LOG ==========
            LogStr = "　　データ送受信中";
            LogFile.NBaseHonsenLogWrite(LogFile.同期, LOGIN_USER.FullName, LogStr);


            // 現在のデータ番号の取得(貰っているデータ番号の最大)
            decimal maxDataNoOfVesselIdZero;
            decimal maxDataNo;

            decimal[] maxDataNos = GetMaxDataNo();
            maxDataNoOfVesselIdZero = maxDataNos[0];
            maxDataNo = maxDataNos[1];
            START_POS_OF_ZERO = (int)maxDataNos[2];
            START_POS = (int)maxDataNos[3];

            if (LOGIN_STATE == LoginState.BEFORE_LOGIN)
            {
                if (maxDataNoOfVesselIdZero == 0)
                {
                }
                else
                {
                    START_POS_OF_ZERO = 0;
                }
            }
            else if (LOGIN_STATE == LoginState.JUST_LOGIN)
            {
                if (maxDataNo == 0)
                {
                    if (START_POS == SPLIT_COUNT)
                    {
                        START_POS = 0;
                    }
                }
                else
                {
                    START_POS = 0;
                }
            }
            else
            {
                START_POS = 0;
            }

            MAX_DATA_NO_OF_ZERO = maxDataNoOfVesselIdZero;
            MAX_DATA_NO = maxDataNo;

            Console.WriteLine("maxDataNoOfVesselIdZero：" + maxDataNoOfVesselIdZero.ToString());
            Console.WriteLine("maxDataNo：" + maxDataNo.ToString());
            Console.WriteLine("START_POS_OF_ZERO：" + START_POS_OF_ZERO.ToString());
            Console.WriteLine("START_POS：" + START_POS.ToString());

            if (LOGIN_VESSEL.MsVesselID != LAST_LOGIN_VESSEL_ID)
            {
                maxDataNo = 0;
                LAST_LOGIN_VESSEL_ID = LOGIN_VESSEL.MsVesselID;
            }
            
            // 船番号の取得.
            int vesselId = LOGIN_VESSEL.MsVesselID;

            int counter = 0; // 同期改善

            List<string> ret = new List<string>();
            string receiveXml = "";
            bool firstTime = true;
            int pos = 0;
            if (LOGIN_STATE == LoginState.BEFORE_LOGIN)
            {
                pos = START_POS_OF_ZERO;
            }
            else if (LOGIN_STATE == LoginState.JUST_LOGIN)
            {
                pos = START_POS;
            }
            try
            {
                Console.WriteLine("sendXml.Length：" + sendXml.Length.ToString());

                NotifyMessage("データ送受信中...");
                #region 2012.04.10 旧Ver
                //receiveXml = serviceClient.データ同期(
                //                    Common.スキーマのバージョン, 
                //                    sendXml, 
                //                    maxDataNoOfVesselIdZero, 
                //                    maxDataNo, 
                //                    vesselId,
                //                    GetHostName(),
                //                    MODULE_VERSION,
                //                    LOGIN_USER.MsUserID,
                //                    DateTime.Now
                //                    );

                //Console.WriteLine("receiveXml.Length：" + receiveXml.Length.ToString());

                //// serviceClient.データ同期で何かエラーが発生
                //if (receiveXml == null)
                //{
                //    SYNC_MESSAGE = "同期エラー(３):サーバ内でエラーが発生しました";

                //    同期EventArgs eventArgs = new 同期EventArgs();

                //    eventArgs.Type = 同期EventArgs.EventType.同期エラー;
                //    eventArgs.Message = "サーバ内でエラーが発生しました";
                //    dataSync(this, eventArgs);
                //    receiveXml = "<NewDataSet></NewDataSet>";
                //}
                //else
                //{
                //    // データの受信を持って、ポータル画面上の最新同期日時とする
                //    SYNC_DATE = DateTime.Now;
                //}
                #endregion

                for (; pos < SPLIT_COUNT; pos++)
                {
                    // ==> ここ追加
                    if (counter > 0)
                    {
                        // カウンターが０で無い場合、まだ、同じテーブルにデータがあるので、POSを戻す
                        pos--;
                    }
                    // <==> ここまで追加


                    Console.WriteLine("データ同期(" + pos.ToString() + "/" + SPLIT_COUNT.ToString() + ")");
                    if (SPLIT_COUNT > 0)
                    {
                        NotifyMessage("データ送受信中...  (" + (pos + 1).ToString() + "/" + SPLIT_COUNT.ToString() + ")");
                    }
                    if (firstTime)
                    {
                        firstTime = false;
                    }
                    else
                    {
                        sendXml = ToXml(DmyDataSet());
                    }


                    //====== LOG ==========
                    LogStr = "　　";
                    LogStr += "VesselName[" + LOGIN_VESSEL.VesselName + "]:";
                    LogStr += "HostName[" + GetHostName() + "]:";
                    LogStr += "MaxDataNoOfVesselIdZero[" + maxDataNoOfVesselIdZero.ToString() + "]:";
                    LogStr += "maxDataNo[" + maxDataNo.ToString() + "]:";
                    LogStr += "CurNo[" + pos.ToString() + "]:";
                    LogStr += "MaxNo[" + SPLIT_COUNT.ToString() + "]:";
                    LogStr += "Counter[" + counter.ToString() + "]";
                    LogFile.NBaseHonsenLogWrite(LogFile.同期, LOGIN_USER.FullName, LogStr);


                    receiveXml = serviceClient.データ同期(
                                        Common.スキーマのバージョン,
                                        sendXml,
                                        maxDataNoOfVesselIdZero,
                                        maxDataNo,
                                        vesselId,
                                        GetHostName(),
                                        MODULE_VERSION,
                                        LOGIN_USER.MsUserID,
                                        DateTime.Now,
                                        pos,
                                        SPLIT_COUNT
                                        , counter // 同期改善
                                        );

                    Console.WriteLine("receiveXml.Length：" + receiveXml.Length.ToString());

                    // モジュールが更新されたとき.
                    if (receiveXml.Contains("<DbVersion>"))
                    {
                        updateModule();
                        break;
                    }

                    //ret.Add(receiveXml);

                    // カウンターを取り出す（取り除く）
                    string removeStr = receiveXml.Substring(0, receiveXml.IndexOf("</counter>"));
                    string counterStr = removeStr.Replace("<counter>", "");
                    counter = int.Parse(counterStr);

                    string addXml = receiveXml.Replace(removeStr, "").Replace("</counter>", "");
                    //ret.Add(addXml);

                    NotifyReceiveData(addXml);
                }

                // serviceClient.データ同期で何かエラーが発生
                if (receiveXml == null)
                {
                    SYNC_MESSAGE = "同期エラー(３):サーバ内でエラーが発生しました";

                    同期EventArgs eventArgs = new 同期EventArgs();

                    eventArgs.Type = 同期EventArgs.EventType.同期エラー;
                    eventArgs.Message = "サーバ内でエラーが発生しました";
                    dataSync(this, eventArgs);
                    receiveXml = "<NewDataSet></NewDataSet>";
                    //ret.Add(receiveXml);
                    NotifyReceiveData(receiveXml);
                }
                else
                {
                    // データの受信を持って、ポータル画面上の最新同期日時とする
                    SYNC_DATE = DateTime.Now;
                }
            }
            catch (EndpointNotFoundException e)
            {
                SYNC_MESSAGE = "同期エラー(１):" + e.Message;

                Console.WriteLine("EndpointNotFoundException：" + e.Message);
                receiveXml = "<NewDataSet></NewDataSet>";
                //ret.Add(receiveXml);
                NotifyReceiveData(receiveXml);
            }
            catch (Exception e)
            {
                SYNC_MESSAGE = "同期エラー(２):" + e.Message;

                Console.WriteLine(e.Source + "：" + e.Message);
                同期EventArgs eventArgs = new 同期EventArgs();

                eventArgs.Type = 同期EventArgs.EventType.同期エラー;
                eventArgs.Message = e.Message;
                dataSync(this, eventArgs);
                receiveXml = "<NewDataSet></NewDataSet>";
                //ret.Add(receiveXml);
                NotifyReceiveData(receiveXml);
            }
            finally
            {
                if (LOGIN_STATE == LoginState.BEFORE_LOGIN)
                {
                    START_POS_OF_ZERO = pos;
                }
                else if (LOGIN_STATE == LoginState.JUST_LOGIN)
                {
                    START_POS = pos;
                }
            }
            Console.WriteLine("同期終了：" + SYNC_DATE.ToString());

            //return receiveXml;
            return ret;
        }

        private DataSet DmyDataSet()
        {
            return new DataSet();
        }
        private DataSet BuildUnsendDataSet()
        {
            return clientDb.BuildUnsendDataSet();
        }

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

        private decimal[] GetMaxDataNo()
        {
            return clientDb.GetMaxDataNo();
        }

        public void SetMaxDataNo(decimal maxDataNoOfVesselIdZero, decimal maxDataNo, DBConnect dbConnect)
        {
            if (maxDataNoOfVesselIdZero == 0 && maxDataNo == 0)
            {
                SYNC_MESSAGE = "サーバからの受信データはありません";
            }
            else
            {
                SYNC_MESSAGE = "同期完了";
            }
            Console.WriteLine("maxDataNoOfVesselIdZero：" + maxDataNoOfVesselIdZero.ToString());
            Console.WriteLine("maxDataNo：" + maxDataNo.ToString());

            // ＤＢに管理番号をセット
            if (LOGIN_STATE == LoginState.BEFORE_LOGIN)
            {
                // ログイン前
                if (START_POS_OF_ZERO != SPLIT_COUNT)
                {
                    clientDb.SetMaxDataNo(0, maxDataNo, (decimal)START_POS_OF_ZERO, (decimal)START_POS, dbConnect);
                }
                else
                {
                    clientDb.SetMaxDataNo(maxDataNoOfVesselIdZero, maxDataNo, (decimal)START_POS_OF_ZERO, (decimal)START_POS, dbConnect);
                }
            }
            else if ( LOGIN_STATE == LoginState.JUST_LOGIN)
            {
                // ログイン後
                if (START_POS != SPLIT_COUNT)
                {
                    clientDb.SetMaxDataNo(maxDataNoOfVesselIdZero, 0, (decimal)START_POS_OF_ZERO, (decimal)START_POS, dbConnect);
                }
                else
                {
                    clientDb.SetMaxDataNo(maxDataNoOfVesselIdZero, maxDataNo, (decimal)START_POS_OF_ZERO, (decimal)START_POS, dbConnect);
                }
            }
            else
            {
                clientDb.SetMaxDataNo(maxDataNoOfVesselIdZero, maxDataNo, (decimal)START_POS_OF_ZERO, (decimal)START_POS, dbConnect);
            }

            // ＤＢから管理番号を取得する
            decimal[] maxDataNos = GetMaxDataNo();
            MAX_DATA_NO_OF_ZERO = maxDataNos[0];
            MAX_DATA_NO = maxDataNos[1];
        }
        
        
        private void updateModule()
        {
            NotifyModuleUpdate();

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                try
                {
                    ApplicationDeployment currentDeploy = ApplicationDeployment.CurrentDeployment;

                    // 新しい更新が利用できる場合
                    if (currentDeploy.CheckForUpdate())
                    {
                        // モジュールのUpDate
                        currentDeploy.Update();

                        //// DB削除
                        //DeleteDb();
                    }

                    // 再起動
                    Application.Restart();
                }
                catch (DeploymentException exp)
                {
                    MessageBox.Show(exp.Message, "更新エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                }
            }
        }

        ///// <summary>
        ///// DBファイルを削除する
        ///// </summary>
        //private void DeleteDb()
        //{
        //    File.Delete(Common.DB名);
        //}



        public void LastSync()
        {
            if (OFFLINE)
            {
                return;
            }

            List<string> receiveXml = null;
            try
            {
                NotifySyncStart();

                // 未送信のデータを取得して DataSet に格納する.
                DataSet unsendDataSet = BuildUnsendDataSet();
                if (unsendDataSet.Tables.Count > 0)
                {
                    LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, "〇同期Client.LastSync");

                    foreach (DataTable table in unsendDataSet.Tables)
                    {
                        LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, table.TableName);

                        List<ISyncTable> models = MappingClass2.ToModel(table);
                        foreach (ISyncTable m in models)
                        {
                            string rowDataStr = "";
                            if (m is OdThi)
                            {
                                rowDataStr = "  ID[" + (m as OdThi).OdThiID + "]:";
                                rowDataStr += "MsVesselID[" + (m as OdThi).MsVesselID.ToString() + "]:";

                                rowDataStr += "VesselID[" + (m as OdThi).VesselID.ToString() + "]:";
                            }
                            else if (m is OdThiItem)
                            {
                                rowDataStr = "  ID[" + (m as OdThiItem).OdThiItemID + "]:";
                                rowDataStr += "OdThiID[" + (m as OdThiItem).OdThiID + "]:";

                                rowDataStr += "VesselID[" + (m as OdThiItem).VesselID.ToString() + "]:";
                            }
                            else if (m is OdThiShousaiItem)
                            {
                                rowDataStr = "  ID[" + (m as OdThiShousaiItem).OdThiShousaiItemID + "]:";
                                rowDataStr += "OdThiItemID[" + (m as OdThiShousaiItem).OdThiItemID + "]:";

                                rowDataStr += "VesselID[" + (m as OdThiShousaiItem).VesselID.ToString() + "]:";

                            }
                            else if (m is DmKanriKiroku)
                            {
                                rowDataStr = "  ID[" + (m as DmKanriKiroku).DmKanriKirokuID + "]:";

                                rowDataStr += "VesselID[" + (m as DmKanriKiroku).VesselID.ToString() + "]:";

                            }
                            else if (m is DmKoubunshoKisoku)
                            {
                                rowDataStr = "  ID[" + (m as DmKoubunshoKisoku).DmKoubunshoKisokuID + "]:";

                                rowDataStr += "VesselID[" + (m as DmKoubunshoKisoku).VesselID.ToString() + "]:";
                            }

                            if (rowDataStr.Length > 0)
                                LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, rowDataStr);
                        }
                    }

                    // XML にシリアライズ.
                    string sendXml = ToXml(unsendDataSet);

                    // データ送受信.
                    receiveXml = SendAndReceive(sendXml);

                    同期EventArgs eventArgs = new 同期EventArgs();
                    eventArgs.Type = 同期EventArgs.EventType.同期完了;
                    eventArgs.Message = DateTime.Now.ToString("yyyyMMdd hh:mm:ss") + " イベント";
                    dataSync(this, eventArgs);
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("同期処理中にエラー発生：" + e.Message);
                同期EventArgs eventArgs = new 同期EventArgs();
                eventArgs.Type = 同期EventArgs.EventType.同期エラー;
                eventArgs.Message = "同期処理中にエラーが発生しました：" + e.Message;
                dataSync(this, eventArgs);
            }
        }




        public void FirstSync()
        {
            System.Diagnostics.Debug.WriteLine("FirstSync() Start");
            if (OFFLINE)
            {
                return;
            }

            try
            {
                NotifySyncStart();


                string receiveXml = serviceClient.SyncSnParameter(
                                            LOGIN_VESSEL.MsVesselID,
                                            GetHostName()
                                            );


                同期EventArgs eventArgs = new 同期EventArgs();

                eventArgs.Type = 同期EventArgs.EventType.起動処理;
                eventArgs.Message = DateTime.Now.ToString("yyyyMMdd hh:mm:ss") + " イベント";

                DataSet dataSet = new DataSet();

                NotifyMessage("受信データ解析中...");
                using (System.IO.StringReader xmlSR = new System.IO.StringReader(receiveXml))
                {
                    dataSet.ReadXml(xmlSR);
                }

                eventArgs.ds = dataSet;

                if (dataSync != null)
                {
                    // データ受信をメインスレッドに通知する.
                    dataSync(this, eventArgs);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("同期処理中にエラー発生：" + e.Message);
                同期EventArgs eventArgs = new 同期EventArgs();
                eventArgs.Type = 同期EventArgs.EventType.同期エラー;
                eventArgs.Message = "同期処理中にエラーが発生しました：" + e.Message;
                dataSync(this, eventArgs);
            }
            System.Diagnostics.Debug.WriteLine("FirstSync() End");
        }
    }


    /// <summary>
    /// 同期に必要なデータを格納する為のクラス
    /// </summary>
    public class 同期EventArgs
    {
        //public enum EventType { 同期開始, 同期終了, モジュール更新, 同期エラー, オンライン, オフライン, メッセージ };
        public enum EventType { 同期開始, 同期終了, モジュール更新, 同期エラー, オンライン, オフライン, メッセージ, 同期処理, 同期完了, 起動処理 };

        public EventType Type;
        public string Message;
        public DataSet ds = new DataSet();

        public int tableCount = 0;
        public int tableNo = 0;
    }
}
