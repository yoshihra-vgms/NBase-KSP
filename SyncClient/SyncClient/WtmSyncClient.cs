using NBaseCommon;
using NBaseData.DS;
using NBaseUtil;
using Npgsql;
using ORMapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WtmData;
using WtmModelBase;

namespace SyncClient
{
    public class WtmSyncClient
    {
        private bool runFlag = true;


        public enum SyncStatus { JUST_START, AFTER_LOGIN, AFTER_FIRST_SYNC };
        public static SyncStatus SYNC_STATUS = SyncStatus.JUST_START;

        private ServiceReference1.IService1 serviceClient;
        private WtmPostgresqlClientDB clientDb;
        public static string GetHostName()
        {
            string hostname = System.Net.Dns.GetHostName();
            return hostname;
        }

        public WtmSyncClient()
            : this(new WtmPostgresqlClientDB())
        {
        }

        public WtmSyncClient(WtmPostgresqlClientDB clientDb)
        {
            this.serviceClient = WcfServiceWrapper.GetInstance().GetServiceClient();
            this.clientDb = clientDb;

            this.CreateDb();
            this.AlterTables();
        }

        /// <summary>
        /// データベース作成
        /// </summary>
        private void CreateDb()
        {
            clientDb.CreateWtmDb();
        }

        /// <summary>
        /// データベース変更
        /// </summary>
        private void AlterTables()
        {
            clientDb.AlterWtmTables();
        }

        public void Stop()
        {
            runFlag = false;
        }

        public void Run()
        {
            AccessorCommon.ConnectionKey = WtmCommon.ConnectionKey;
            AccessorCommon.ConnectionString = Common.Wtm接続文字列;

            while (runFlag)
            {

                if (SYNC_STATUS == SyncStatus.JUST_START)
                {
                    Thread.Sleep(100);
                }
                else
                {
                    Thread.Sleep(Common.WTM_SYNC_INTERVAL);
                }


                try
                {
                    // 
                    var syncTabls = WtmDac.GetSyncTables();
                    var masterMaxNoDic = new Dictionary<int, int>();
                    var dataMaxNoDic = new Dictionary<int, int>();

                    // 各テーブルのdate_noのMaxを取得
                    foreach (var syncTable in syncTabls)
                    {
                        var maxNo = WtmDac.GetMaxDateNo(syncTable.TableName);

                        if (syncTable.TypeID == SyncTables.TYPE_MASTER)
                        {
                            masterMaxNoDic.Add(syncTable.TableNo, maxNo);
                        }
                        else
                        {
                            dataMaxNoDic.Add(syncTable.TableNo, maxNo);
                        }
                    }


                    foreach (var syncTable in syncTabls)
                    {
                        int maxNo = 0;
                        if (syncTable.TypeID == SyncTables.TYPE_MASTER)
                        {
                            maxNo = masterMaxNoDic[syncTable.TableNo];
                        }
                        else
                        {
                            maxNo = dataMaxNoDic[syncTable.TableNo];
                        }
                        LogFile.NBaseHonsenLogWrite(LogFile.同期, WTM.Common.LoginUser.FullName, $"{syncTable.TableName}:MaxNo = {maxNo}");
                    }


                    int counter = 0;
                    string receiveXml = "";


var s = DateTime.Now;
System.Diagnostics.Debug.WriteLine($"Start:{s.TimeOfDay}");
                    var masterTables = syncTabls.Where(o => o.TypeID == SyncTables.TYPE_MASTER).OrderBy(o => o.TableNo);
                    var dataTables = syncTabls.Where(o => o.TypeID == SyncTables.TYPE_DATA).OrderBy(o => o.TableNo);

                    foreach (var syncTable in masterTables)
                    {
                        receiveXml = serviceClient.SyncMaster(syncTable.TableNo, masterMaxNoDic[syncTable.TableNo], WTM.Common.Vessel.MsVesselID, GetHostName(), WTM.Common.LoginUser.MsUserID, DateTime.Now, counter);

                        if (string.IsNullOrEmpty(receiveXml) == false)
                        {
                            // カウンターを取り出す（取り除く）
                            string removeStr = receiveXml.Substring(0, receiveXml.IndexOf("</counter>"));
                            string counterStr = removeStr.Replace("<counter>", "");
                            counter = int.Parse(counterStr);
                            string addXml = receiveXml.Replace(removeStr, "").Replace("</counter>", "");

                            NotifyReceiveData(addXml);
                        }
                    }



                    if (SYNC_STATUS == SyncStatus.JUST_START)
                    {
                        SYNC_STATUS = SyncStatus.AFTER_LOGIN;
                    }


                    counter = 0;

                    List<string> processed = new List<string>();

                    var minTableNo = dataTables.First().TableNo;
                    var maxTableNo = dataTables.Last().TableNo;
                    for (int tableNo = minTableNo; tableNo <= maxTableNo; tableNo++)
                    {
                        var syncTable = dataTables.Where(o => o.TableNo == tableNo).FirstOrDefault();
                        if (syncTable == null)
                            continue;


                        // 未送信のデータを取得して DataSet に格納する.
                        DataSet unsendDataSet = null;
                        if (processed.Contains(syncTable.TableName))
                        {
                            unsendDataSet = new DataSet();
                        }
                        else
                        {
                            unsendDataSet = BuildUnsendDataSet(syncTable.TableName);

                            processed.Add(syncTable.TableName);
                        }
                        // XML にシリアライズ.
                        string sendXml = ToXml(unsendDataSet);


                        // 送受信
                        receiveXml = serviceClient.SyncData(sendXml, syncTable.TableNo, dataMaxNoDic[syncTable.TableNo], WTM.Common.Vessel.MsVesselID, GetHostName(), WTM.Common.LoginUser.MsUserID, DateTime.Now, counter);

                        if (string.IsNullOrEmpty(receiveXml) == false)
                        {
                            // カウンターを取り出す（取り除く）
                            string removeStr = receiveXml.Substring(0, receiveXml.IndexOf("</counter>"));
                            string counterStr = removeStr.Replace("<counter>", "");
                            counter = int.Parse(counterStr);
                            string addXml = receiveXml.Replace(removeStr, "").Replace("</counter>", "");

                            NotifyReceiveData(addXml);
                        }

                        if (counter > 0)
                        {
                            // カウンターが０で無い場合、まだ、同じテーブルにデータがあるので、POSを戻す
                            tableNo--;
                        }
                    }

var f = DateTime.Now;
System.Diagnostics.Debug.WriteLine($"Finish:{f.TimeOfDay}");

System.Diagnostics.Debug.WriteLine($"({(f - s).TotalMinutes}");

                    SYNC_STATUS = SyncStatus.AFTER_FIRST_SYNC;
                }
                catch
                {

                }

            }
        }


 
        
        private void NotifyReceiveData(string receiveXml)
        {
            DataSet dataSet = new DataSet();

            using (System.IO.StringReader xmlSR = new System.IO.StringReader(receiveXml))
            {
                dataSet.ReadXml(xmlSR);
            }


            using (DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                dbConnect.BeginTransaction();

                try
                {
                    foreach (DataTable table in dataSet.Tables)
                    {
                        List<IWtmSyncTable> models = null;
                        models = WtmModels.MappingClass.ToWtmModel(table, Common.WTM_NAMESPACE_KEY);
                        if (models == null)
                            models = WtmModelBase.MappingClass.ToWtmModel(table);

                        if (models != null)
                        {
                            foreach (IWtmSyncTable m in models)
                            {
                                if (m.Exists(dbConnect, Common.WTM_TABLE_KEY))
                                {
                                    m.UpdateRecord(dbConnect, Common.WTM_TABLE_KEY, (int)NBaseUtil.StatusUtils.通信状況.同期済);
                                }
                                else
                                {
                                    m.InsertRecord(dbConnect, Common.WTM_TABLE_KEY, (int)NBaseUtil.StatusUtils.通信状況.同期済);
                                }
                            }
                        }

                    }
                    dbConnect.Commit();
                }
                catch (Exception ex)
                {
                    dbConnect.RollBack();
                }
            }
        }


        private DataSet BuildUnsendDataSet(string tableName)
        {
            DataSet unsendDataSet = new DataSet();

            using (var con = AccessorCommon.GetConnection().NpgsqlConnection)
            {
                // 未送信のデータを取得して、DataSet に追加する.
                string tn = $"{tableName}_{Common.WTM_TABLE_KEY}";

                string sql = $"select * from {tn} where SEND_FLAG = 0";

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, con))
                {
                    DataTable dt1 = new DataTable(tableName);

                    adapter.FillSchema(dt1, SchemaType.Source);

                    adapter.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {
                        unsendDataSet.Tables.Add(dt1);
                    }
                }
            }

            return unsendDataSet;
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
    }
}
