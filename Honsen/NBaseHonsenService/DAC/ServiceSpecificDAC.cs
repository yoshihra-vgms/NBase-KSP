using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Data;
using NBaseData.DAC;
using NBaseData.DS;
using ORMapping;
using NBaseCommon;

namespace NBaseHonsenService.DAC
{
    public class ServiceSpecificDAC
    {
        public static int 返信する最大レコード数 = 20000;

        public enum SnMessageLogMessageType { 受信, 送信 };

        #region internal static bool InsertSnMessageLog(int vesselId, SnMessageLogMessageType messageType, string message)
        internal static bool InsertSnMessageLog(int vesselId, SnMessageLogMessageType messageType, string message)
        {
            #region SQL
            string SQL = @"
insert into SN_MESSAGE_LOG
(
MESSAGE_ID,
MS_VESSEL_ID,
MESSAGE_TYPE,
MESSAGE,
RENEW_DATE
) 
values
(
Nextval( 'SEQ_MESSAGE_ID' ),
:MS_VESSEL_ID,
:MESSAGE_TYPE,
:MESSAGE,
:RENEW_DATE
)
";
            #endregion

            int ret = 0;

            using (NpgsqlConnection conn = new NpgsqlConnection(Common.接続文字列))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(SQL, conn))
                {
                    cmd.Parameters.Add("MS_VESSEL_ID", vesselId);
                    cmd.Parameters.Add("MESSAGE_TYPE", (int)messageType);
                    cmd.Parameters.Add("MESSAGE", message);
                    cmd.Parameters.Add("MESSAGE", DateTime.Now);

                    ret = cmd.ExecuteNonQuery();
                }

                conn.Close();
            }

            return ret > 0;
        }
        #endregion

        #region internal static int InsertTransactionToken(string token)
        internal static int InsertTransactionToken(string token)
        {
            int ret = 0;

            using (NpgsqlConnection conn = new NpgsqlConnection(Common.接続文字列))
            {
                conn.Open();

                string sql = "INSERT INTO SN_TRANSACTION_TOKEN(TRANSACTION_TOKEN) VALUES('" + token + "')";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    ret = cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

            return ret;
        }
        #endregion

        #region internal static bool ExistsTransactionToken(string token)
        internal static bool ExistsTransactionToken(string token)
        {
            //decimal cnt = 0;  // 2011.08.22:COUNTで帰ってくるデータ型が違っているみたい
            Int64 cnt = 0;

            using (NpgsqlConnection conn = new NpgsqlConnection(Common.接続文字列))
            {
                conn.Open();

                string sql = "SELECT COUNT(*) FROM SN_TRANSACTION_TOKEN WHERE TRANSACTION_TOKEN = '" + token + "'";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    //cnt = (decimal)cmd.ExecuteScalar();
                    cnt = (Int64)cmd.ExecuteScalar();
                }
                conn.Close();
            }

            return cnt > 0;
        }
        #endregion

        #region internal static DataSet GetSendDataSet(decimal maxDataNoOfVesselIdZero, decimal maxDataNo, int vesselId, List<SnTableInfo> tableList)
        internal static DataSet GetSendDataSet(decimal maxDataNoOfVesselIdZero, decimal maxDataNo, int vesselId, List<SnTableInfo> tableList, int curNo, int maxNo, int counter)
        {
            int vesselIdZero = int.Parse(Common.共通船番号);

            int cnt = 0;
            int range = (tableList.Count() / maxNo);
            int startPos = range * curNo;

            DataSet dataSet = new DataSet();

            //NBaseCommon.LogFile.NBaseHonsenServiceLogWrite("", "tableList -> " + tableList.Count().ToString() + "," + maxNo.ToString());

            try
            {
                //NBaseCommon.LogFile.NBaseHonsenServiceLogWrite("", "<<GetSendDataSet>>");
                using (NpgsqlConnection conn = new NpgsqlConnection(Common.接続文字列))
                {
                    conn.Open();

                    foreach (SnTableInfo t in tableList)
                    {
                        //NBaseCommon.LogFile.NBaseHonsenServiceLogWrite("", "データ同期(" + t.Name + ") -> " + cnt.ToString() + "," + startPos.ToString());
                        if (cnt < startPos)
                        {
                            cnt++;
                            continue;
                        }
                        if (cnt >= (startPos + range))
                        {
                            break;
                        }
                        cnt++;                

                        string sql = "";

//                        sql = @"
//SELECT 
//MS_SENIN.MS_SENIN_ID                    ,

//MS_SENIN.MS_USER_ID                     ,
//MS_SENIN.MS_SI_SHOKUMEI_ID              ,

//MS_SENIN.SEI                            ,
//MS_SENIN.MEI                            ,
//MS_SENIN.SEI_KANA                       ,
//MS_SENIN.MEI_KANA                       ,
//MS_SENIN.KUBUN                          ,
//MS_SENIN.SHIMEI_CODE                    ,
//MS_SENIN.HOKEN_NO                       ,
//MS_SENIN.SEX                            ,
//MS_SENIN.BIRTHDAY                       ,
//MS_SENIN.NENKIN_NO                      ,
//MS_SENIN.NYUUSHA_DATE                   ,
//MS_SENIN.POSTAL_NO                      ,
//MS_SENIN.GENJUUSHO                      ,
//MS_SENIN.HONSEKI                        ,
//MS_SENIN.TEL                            ,
//MS_SENIN.FAX                            ,
//MS_SENIN.MAIL                           ,
//MS_SENIN.KEITAI                         ,
//MS_SENIN.SONOTA                         ,
//MS_SENIN.PICTURE_DATE                   ,
//MS_SENIN.GAKUREKI                       ,
//MS_SENIN.ZENREKI                        ,
//MS_SENIN.SHOUKAISHA                     ,
//MS_SENIN.BANK_NAME1                     ,
//MS_SENIN.BRANCH_NAME1                   ,
//MS_SENIN.ACCOUNT_NO1                    ,
//MS_SENIN.BANK_NAME2                     ,
//MS_SENIN.BRANCH_NAME2                   ,
//MS_SENIN.ACCOUNT_NO2                    ,
//MS_SENIN.POSTAL_ACCOUNT_NO              ,
//MS_SENIN.CLOTH_UE                       ,
//MS_SENIN.CLOTH_SHITA                    ,
//MS_SENIN.CLOTH_KUTSU                    ,
//MS_SENIN.RETIRE_FLAG                    ,
//MS_SENIN.RETIRE_DATE                    ,

//MS_SENIN.MS_SENIN_COMPANY_ID            ,
//MS_SENIN.DEPARTMENT                     ,
//MS_SENIN.SEI_HIRAGANA                   ,
//MS_SENIN.MEI_HIRAGANA                   ,
//MS_SENIN.MEMBER_OF                      ,

//MS_SENIN.DELETE_FLAG,
//MS_SENIN.SEND_FLAG,
//MS_SENIN.VESSEL_ID,
//MS_SENIN.DATA_NO,
//MS_SENIN.USER_KEY,
//MS_SENIN.RENEW_DATE,
//MS_SENIN.RENEW_USER_ID,
//MS_SENIN.TS";

                        if (t.Name == "MS_SENIN")
                        {
                            sql = @"
SELECT 
MS_SENIN.MS_SENIN_ID                    ,

MS_SENIN.MS_USER_ID                     ,
MS_SENIN.MS_SI_SHOKUMEI_ID              ,

MS_SENIN.SEI                            ,
MS_SENIN.MEI                            ,
MS_SENIN.SEI_KANA                       ,
MS_SENIN.MEI_KANA                       ,
MS_SENIN.KUBUN                          ,
MS_SENIN.SHIMEI_CODE                    ,
MS_SENIN.HOKEN_NO                       ,
MS_SENIN.SEX                            ,
MS_SENIN.BIRTHDAY                       ,
MS_SENIN.NENKIN_NO                      ,
MS_SENIN.NYUUSHA_DATE                   ,
MS_SENIN.POSTAL_NO                      ,
MS_SENIN.GENJUUSHO                      ,
MS_SENIN.HONSEKI                        ,
MS_SENIN.TEL                            ,
MS_SENIN.FAX                            ,
MS_SENIN.MAIL                           ,
MS_SENIN.KEITAI                         ,
MS_SENIN.SONOTA                         ,
MS_SENIN.PICTURE_DATE                   ,
MS_SENIN.GAKUREKI                       ,
MS_SENIN.ZENREKI                        ,
MS_SENIN.SHOUKAISHA                     ,
MS_SENIN.BANK_NAME1                     ,
MS_SENIN.BRANCH_NAME1                   ,
MS_SENIN.ACCOUNT_NO1                    ,
MS_SENIN.BANK_NAME2                     ,
MS_SENIN.BRANCH_NAME2                   ,
MS_SENIN.ACCOUNT_NO2                    ,
MS_SENIN.POSTAL_ACCOUNT_NO              ,
MS_SENIN.CLOTH_UE                       ,
MS_SENIN.CLOTH_SHITA                    ,
MS_SENIN.CLOTH_KUTSU                    ,
MS_SENIN.RETIRE_FLAG                    ,
MS_SENIN.RETIRE_DATE                    ,

MS_SENIN.MS_SENIN_COMPANY_ID            ,
MS_SENIN.DEPARTMENT                     ,
MS_SENIN.SEI_HIRAGANA                   ,
MS_SENIN.MEI_HIRAGANA                   ,
MS_SENIN.MEMBER_OF                      ,

MS_SENIN.DELETE_FLAG,
MS_SENIN.SEND_FLAG,
MS_SENIN.VESSEL_ID,
MS_SENIN.DATA_NO,
MS_SENIN.USER_KEY,
MS_SENIN.RENEW_DATE,
MS_SENIN.RENEW_USER_ID,
MS_SENIN.TS
";
                            #region
                            if (vesselId == 0)
                            {
                                //                    sql = @"SELECT 
                                //MS_SENIN.MS_SENIN_ID                    ,

                                //MS_SENIN.MS_USER_ID                     ,
                                //MS_SENIN.MS_SI_SHOKUMEI_ID              ,

                                //MS_SENIN.SEI                            ,
                                //MS_SENIN.MEI                            ,
                                //MS_SENIN.SEI_KANA                       ,
                                //MS_SENIN.MEI_KANA                       ,
                                //MS_SENIN.KUBUN                          ,
                                //MS_SENIN.SHIMEI_CODE                    ,
                                //MS_SENIN.HOKEN_NO                       ,
                                //MS_SENIN.SEX                            ,
                                //MS_SENIN.BIRTHDAY                       ,
                                //MS_SENIN.NENKIN_NO                      ,
                                //MS_SENIN.NYUUSHA_DATE                   ,
                                //MS_SENIN.POSTAL_NO                      ,
                                //MS_SENIN.GENJUUSHO                      ,
                                //MS_SENIN.HONSEKI                        ,
                                //MS_SENIN.TEL                            ,
                                //MS_SENIN.FAX                            ,
                                //MS_SENIN.MAIL                           ,
                                //MS_SENIN.KEITAI                         ,
                                //MS_SENIN.SONOTA                         ,
                                //MS_SENIN.PICTURE_DATE                   ,
                                //MS_SENIN.GAKUREKI                       ,
                                //MS_SENIN.ZENREKI                        ,
                                //MS_SENIN.SHOUKAISHA                     ,
                                //MS_SENIN.BANK_NAME1                     ,
                                //MS_SENIN.BRANCH_NAME1                   ,
                                //MS_SENIN.ACCOUNT_NO1                    ,
                                //MS_SENIN.BANK_NAME2                     ,
                                //MS_SENIN.BRANCH_NAME2                   ,
                                //MS_SENIN.ACCOUNT_NO2                    ,
                                //MS_SENIN.POSTAL_ACCOUNT_NO              ,
                                //MS_SENIN.CLOTH_UE                       ,
                                //MS_SENIN.CLOTH_SHITA                    ,
                                //MS_SENIN.CLOTH_KUTSU                    ,
                                //MS_SENIN.RETIRE_FLAG                    ,
                                //MS_SENIN.RETIRE_DATE                    ,

                                //MS_SENIN.MS_SENIN_COMPANY_ID            ,
                                //MS_SENIN.DEPARTMENT                     ,
                                //MS_SENIN.SEI_HIRAGANA                   ,
                                //MS_SENIN.MEI_HIRAGANA                   ,
                                //MS_SENIN.MEMBER_OF                      ,

                                //MS_SENIN.DELETE_FLAG,
                                //MS_SENIN.SEND_FLAG,
                                //MS_SENIN.VESSEL_ID,
                                //MS_SENIN.DATA_NO,
                                //MS_SENIN.USER_KEY,
                                //MS_SENIN.RENEW_DATE,
                                //MS_SENIN.RENEW_USER_ID,
                                //MS_SENIN.TS

                                //                        " +
                                sql += "FROM " + t.Name +
                                       " WHERE VESSEL_ID = " + vesselIdZero +
                                       " AND DATA_NO > " + maxDataNoOfVesselIdZero.ToString();
                            }
                            else
                            {
                                //                    sql = @"SELECT 
                                //MS_SENIN.MS_SENIN_ID                    ,

                                //MS_SENIN.MS_USER_ID                     ,
                                //MS_SENIN.MS_SI_SHOKUMEI_ID              ,

                                //MS_SENIN.SEI                            ,
                                //MS_SENIN.MEI                            ,
                                //MS_SENIN.SEI_KANA                       ,
                                //MS_SENIN.MEI_KANA                       ,
                                //MS_SENIN.KUBUN                          ,
                                //MS_SENIN.SHIMEI_CODE                    ,
                                //MS_SENIN.HOKEN_NO                       ,
                                //MS_SENIN.SEX                            ,
                                //MS_SENIN.BIRTHDAY                       ,
                                //MS_SENIN.NENKIN_NO                      ,
                                //MS_SENIN.NYUUSHA_DATE                   ,
                                //MS_SENIN.POSTAL_NO                      ,
                                //MS_SENIN.GENJUUSHO                      ,
                                //MS_SENIN.HONSEKI                        ,
                                //MS_SENIN.TEL                            ,
                                //MS_SENIN.FAX                            ,
                                //MS_SENIN.MAIL                           ,
                                //MS_SENIN.KEITAI                         ,
                                //MS_SENIN.SONOTA                         ,
                                //MS_SENIN.PICTURE_DATE                   ,
                                //MS_SENIN.GAKUREKI                       ,
                                //MS_SENIN.ZENREKI                        ,
                                //MS_SENIN.SHOUKAISHA                     ,
                                //MS_SENIN.BANK_NAME1                     ,
                                //MS_SENIN.BRANCH_NAME1                   ,
                                //MS_SENIN.ACCOUNT_NO1                    ,
                                //MS_SENIN.BANK_NAME2                     ,
                                //MS_SENIN.BRANCH_NAME2                   ,
                                //MS_SENIN.ACCOUNT_NO2                    ,
                                //MS_SENIN.POSTAL_ACCOUNT_NO              ,
                                //MS_SENIN.CLOTH_UE                       ,
                                //MS_SENIN.CLOTH_SHITA                    ,
                                //MS_SENIN.CLOTH_KUTSU                    ,
                                //MS_SENIN.RETIRE_FLAG                    ,
                                //MS_SENIN.RETIRE_DATE                    ,

                                //MS_SENIN.MS_SENIN_COMPANY_ID            ,
                                //MS_SENIN.DEPARTMENT                     ,
                                //MS_SENIN.SEI_HIRAGANA                   ,
                                //MS_SENIN.MEI_HIRAGANA                   ,
                                //MS_SENIN.MEMBER_OF                      ,

                                //MS_SENIN.DELETE_FLAG,
                                //MS_SENIN.SEND_FLAG,
                                //MS_SENIN.VESSEL_ID,
                                //MS_SENIN.DATA_NO,
                                //MS_SENIN.USER_KEY,
                                //MS_SENIN.RENEW_DATE,
                                //MS_SENIN.RENEW_USER_ID,
                                //MS_SENIN.TS
                                //                        " +
                                sql += "FROM " + t.Name +
                                       " WHERE (VESSEL_ID = " + vesselIdZero +
                                       " AND DATA_NO > " + maxDataNoOfVesselIdZero.ToString() +
                                       " ) OR (VESSEL_ID = " + vesselId +
                                       " AND DATA_NO > " + maxDataNo.ToString() + ")";
                            }
                            #endregion
                        }
                        else if (t.Name == "DM_PUBLISHER" ||
                            t.Name == "DM_KOUKAI_SAKI" ||
                            t.Name == "DM_KAKUNIN_JOKYO" ||
                            t.Name == "DM_DOC_COMMENT" ||
                            t.Name == "DM_KANRYO_INFO" ||
                            t.Name == "DM_KANRI_KIROKU" ||
                            t.Name == "DM_KOUBUNSHO_KISOKU")
                        {
                            if (vesselId > 0)
                            {
                                sql = MakeSQL(t.Name, vesselId, maxDataNo);
                            }
                        }
                        else
                        {
                            if (vesselId == 0)
                            {
                                sql = "SELECT * FROM " + t.Name +
                                             " WHERE VESSEL_ID = " + vesselIdZero +
                                             " AND DATA_NO > " + maxDataNoOfVesselIdZero.ToString();
                            }
                            else
                            {
                                sql = "SELECT * FROM " + t.Name +
                                             " WHERE (VESSEL_ID = " + vesselIdZero +
                                             " AND DATA_NO > " + maxDataNoOfVesselIdZero.ToString() +
                                             " ) OR (VESSEL_ID = " + vesselId +
                                             " AND DATA_NO > " + maxDataNo.ToString() + ")";
                            }
                        }
                        if (sql.Length == 0)
                        {
                            continue;
                        }

                        Console.WriteLine("データ同期(" + t.Name + ") -> " + counter.ToString());
                        //NBaseCommon.LogFile.NBaseHonsenServiceLogWrite("", "データ同期(" + t.Name + ") -> " + counter.ToString());
                        //NBaseCommon.LogFile.NBaseHonsenServiceLogWrite("", "データ同期(" + t.Name + ") -> " + sql);

                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, conn))
                        {
                            DataTable dataTable = new DataTable(t.Name);

                            adapter.Fill(dataTable);

                            // レコードが存在する場合、追加する。
                            if (dataTable.Rows.Count > 0)
                            {
                                //dataSet.Tables.Add(dataTable);

                                // counter × 返信する最大レコード数 から 返信する最大レコード数 を有効とする
                                int startRow = (counter * 返信する最大レコード数);
                                int endRow = ((counter + 1) * 返信する最大レコード数);
                                if (dataTable.Rows.Count > endRow)
                                {
                                    int max = dataTable.Rows.Count;
                                    for (int i = endRow; i < max; i++)
                                    {
                                        dataTable.Rows.RemoveAt(endRow);
                                    }
                                }
                                if (startRow > 0)
                                {
                                    for (int i = 0; i < startRow; i++)
                                    {
                                        dataTable.Rows.RemoveAt(0);
                                    }
                                }
                                if (dataTable.Rows.Count > 0)
                                {
                                    dataSet.Tables.Add(dataTable);
                                }
                            }
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                NBaseCommon.LogFile.NBaseHonsenServiceLogWrite("", "Exception:" + ex.Message);
            }

            return dataSet;
        }
        #endregion


        #region internal static DataSet GetSnParameterSet()
        internal static DataSet GetSnParameterSet()
        {
            DataSet dataSet = new DataSet();

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(Common.接続文字列))
                {
                    conn.Open();

                    string sql = "SELECT * FROM SN_PARAMETER";

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, conn))
                    {
                        DataTable dataTable = new DataTable("SN_PARAMETER");

                        adapter.Fill(dataTable);

                        // レコードが存在する場合、追加する。
                        if (dataTable.Rows.Count > 0)
                        {
                            dataSet.Tables.Add(dataTable);
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                NBaseCommon.LogFile.NBaseHonsenServiceLogWrite("", "Exception:" + ex.Message);
            }

            return dataSet;
        }
        #endregion


        #region internal static void Send_手配メール(List<OdThi> odThis)
        internal static void Send_手配メール(List<OdThi> odThis)
        {
            if (System.Net.ServicePointManager.ServerCertificateValidationCallback == null)
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback += delegate (Object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;	// 無条件でオレオレ証明を信用する。危険！(senderのURIとか調べてチェックすべし！)
                };
            }

            using (NBaseService.ServiceClient serviceClient = new NBaseService.ServiceClient())
            {
                foreach (OdThi thi in odThis)
                {
                    MsUser user = new MsUser();
                    user.MsUserID = thi.RenewUserID;
                    try
                    {
                        serviceClient.BLC_手配依頼メール送信_同期用(user, thi.OdThiID);
                    }
                    catch (Exception e)
                    {
                        NBaseCommon.LogFile.Write("", "手配メール送信失敗:" + e.Message);
                        throw e;
                    }
                }
            }
        }
        #endregion

        #region internal static void Send_動静報告メール(List<DjDouseiHoukoku> houkokus)
        internal static void Send_動静報告メール(List<DjDouseiHoukoku> houkokus)
        {
            if (System.Net.ServicePointManager.ServerCertificateValidationCallback == null)
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback += delegate (Object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;	// 無条件でオレオレ証明を信用する。危険！(senderのURIとか調べてチェックすべし！)
                };
            }

            using (NBaseService.ServiceClient serviceClient = new NBaseService.ServiceClient())
            {
                foreach (DjDouseiHoukoku houkoku in houkokus)
                {
                    MsUser user = new MsUser();
                    user.MsUserID = houkoku.RenewUserID;
                    try
                    {
                        bool ret = serviceClient.BLC_動静報告メール送信_同期用(user, houkoku);
                        if (ret == false)
                        {
                            NBaseCommon.LogFile.Write("", "動静報告メール送信失敗:サーバ内部エラー");
                        }
                    }
                    catch (Exception e)
                    {
                        NBaseCommon.LogFile.Write("", "動静報告メール送信失敗:" + e.Message);
                        throw e;
                    }
                }
            }
        }
        #endregion

        #region internal static bool Is_本船更新情報_編集対象(ISyncTable syncTable)
        internal static bool Is_本船更新情報_編集対象(ISyncTable syncTable)
        {
            if (syncTable is OdThi && (syncTable as OdThi).Status == (int)OdThi.STATUS.手配依頼済)
            {
                return true;
            }

            return false;
        }
        #endregion

        #region internal static bool Is_報告書登録(ISyncTable syncTable)
        internal static bool Is_報告書登録(ISyncTable syncTable)
        {
            if (syncTable is DmKanriKiroku)
            {
                return true;
            }

            return false;
        }
        #endregion





        #region internal static DataSet GetSendDocumentDataSet(int syncMax, List<string> sendKanriKirokuIds, List<string> sendKoubunshoKisokuIds, List<string> requestHoukokushoIds, List<string> requestKanriKirokuIds, List<string> requestKoubunshoKisokuIds)
        internal static DataSet GetSendDocumentDataSet(
                        int syncMax,
                        List<string> sendKanriKirokuIds,
                        List<string> sendKoubunshoKisokuIds,
                        List<string> requestHoukokushoIds,
                        List<string> requestKanriKirokuIds,
                        List<string> requestKoubunshoKisokuIds,
                        List<string> requestAttachFileIds)
        {
            int cnt = syncMax;
            DataSet dataSet = new DataSet();
            List<MsDmTemplateFile> templateFiles = new List<MsDmTemplateFile>();

            string sql1 = MakeGetMsDmTemplateFileSQL(requestHoukokushoIds);
            string sql2 = MakeGetDmKanriKirokuFileSQL(requestKanriKirokuIds);
            string sql3 = MakeGetDmKoubunshoKisokuFileSQL(requestKoubunshoKisokuIds);
            string sql4 = MakeGetSendDmKanriKirokuFileSQL(sendKanriKirokuIds);
            string sql5 = MakeGetSendDmKoubunshoKisokuFileSQL(sendKoubunshoKisokuIds);

            string sql6 = MakeGetOdAttachFileSQL(requestAttachFileIds);
            
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(Common.接続文字列))
                {
                    conn.Open();
                    if (sql6 != null)
                    {
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql6, conn))
                        {
                            DataTable dataTable = new DataTable("OD_ATTACH_FILE");

                            adapter.Fill(dataTable);
                            if (dataTable.Rows.Count > 0)
                            {
                                if (dataTable.Rows.Count > cnt)
                                {
                                    int diff = dataTable.Rows.Count - cnt;
                                    for (int i = 0; i < diff; i++)
                                    {
                                        dataTable.Rows.RemoveAt(cnt);
                                    }
                                    cnt = 0;
                                }
                                else
                                {
                                    cnt = cnt - dataTable.Rows.Count;
                                }
                                dataSet.Tables.Add(dataTable);
                            }
                        }
                    }

                    if (sql1 != null)
                    {
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql1, conn))
                        {
                            DataTable dataTable = new DataTable("MS_DM_TEMPLATE_FILE");

                            adapter.Fill(dataTable);
                            if (dataTable.Rows.Count > 0)
                            {
                                if (dataTable.Rows.Count > cnt)
                                {
                                    int diff = dataTable.Rows.Count - cnt;
                                    for (int i = 0; i < diff; i++)
                                    {
                                        dataTable.Rows.RemoveAt(cnt);
                                    }
                                    cnt = 0;
                                }
                                else
                                {
                                    cnt = cnt - dataTable.Rows.Count;
                                }
                                dataSet.Tables.Add(dataTable);
                            }
                        }
                    }
                    if (sql2 != null && cnt > 0)
                    {
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql2, conn))
                        {
                            DataTable dataTable = new DataTable("DM_KANRI_KIROKU_FILE");

                            adapter.Fill(dataTable);
                            if (dataTable.Rows.Count > 0)
                            {
                                if (dataTable.Rows.Count > cnt)
                                {
                                    int diff = dataTable.Rows.Count - cnt;
                                    for (int i = 0; i < diff; i++)
                                    {
                                        dataTable.Rows.RemoveAt(cnt);
                                    }
                                    cnt = 0;
                                }
                                else
                                {
                                    cnt = cnt - dataTable.Rows.Count;
                                }
                                dataSet.Tables.Add(dataTable);
                            }
                        }
                    }
                    if (sql3 != null && cnt > 0)
                    {
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql3, conn))
                        {
                            DataTable dataTable = new DataTable("DM_KOUBUNSHO_KISOKU_FILE");

                            adapter.Fill(dataTable);
                            if (dataTable.Rows.Count > 0)
                            {
                                if (dataTable.Rows.Count > cnt)
                                {
                                    int diff = dataTable.Rows.Count - cnt;
                                    for (int i = 0; i < diff; i++)
                                    {
                                        dataTable.Rows.RemoveAt(cnt);
                                    }
                                    cnt = 0;
                                }
                                else
                                {
                                    cnt = cnt - dataTable.Rows.Count;
                                }
                                dataSet.Tables.Add(dataTable);
                            }
                        }
                    }

                    if (sql4 != null)
                    {
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql4, conn))
                        {
                            DataTable dataTable = new DataTable("DM_KANRI_KIROKU_FILE");

                            adapter.Fill(dataTable);
                            if (dataTable.Rows.Count > 0)
                            {
                                dataSet.Tables.Add(dataTable);
                            }
                        }
                    }
                    if (sql5 != null)
                    {
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql5, conn))
                        {
                            DataTable dataTable = new DataTable("DM_KOUBUNSHO_KISOKU_FILE");

                            adapter.Fill(dataTable);
                            if (dataTable.Rows.Count > 0)
                            {
                                dataSet.Tables.Add(dataTable);
                            }
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
            }

            return dataSet;
        }
        #endregion

        #region static string MakeGetMsDmTemplateFileSQL(List<string> ids)
        static string MakeGetMsDmTemplateFileSQL(List<string> ids)
        {
            if (ids == null)
            {
                return null;
            }
            if (ids.Count == 0)
            {
                return null;
            }

            string sql = @"
SELECT
MS_DM_TEMPLATE_FILE.*

FROM
MS_DM_TEMPLATE_FILE
WHERE (DELETE_FLAG = 0 or DELETE_FLAG is NULL)
AND MS_DM_HOUKOKUSHO_ID IN (
";
            string prm = "";
            foreach (string id in ids)
            {
                prm += ",'" + id + "'";
            }
            sql += prm.Substring(1) + ")";

            return sql;
        }
        #endregion
        #region static string MakeGetDmKanriKirokuFileSQL(List<string> ids)
        static string MakeGetDmKanriKirokuFileSQL(List<string> ids)
        {
            if (ids == null)
            {
                return null;
            }
            if (ids.Count == 0)
            {
                return null;
            }

            string sql = @"
SELECT
DM_KANRI_KIROKU_FILE.*

FROM
DM_KANRI_KIROKU_FILE
WHERE (DELETE_FLAG = 0 or DELETE_FLAG is NULL)
AND DM_KANRI_KIROKU_ID IN (
";
            string prm = "";
            foreach (string id in ids)
            {
                prm += ",'" + id + "'";
            }
            sql += prm.Substring(1) + ")";

            return sql;
        }
        #endregion
        #region static string MakeGetDmKoubunshoKisokuFileSQL(List<string> ids)
        static string MakeGetDmKoubunshoKisokuFileSQL(List<string> ids)
        {
            if (ids == null)
            {
                return null;
            }
            if (ids.Count == 0)
            {
                return null;
            }

            string sql = @"
SELECT
DM_KOUBUNSHO_KISOKU_FILE.*

FROM
DM_KOUBUNSHO_KISOKU_FILE
WHERE (DELETE_FLAG = 0 or DELETE_FLAG is NULL)
AND DM_KOUBUNSHO_KISOKU_ID IN (
";
            string prm = "";
            foreach (string id in ids)
            {
                prm += ",'" + id + "'";
            }
            sql += prm.Substring(1) + ")";

            return sql;
        }
        #endregion
        #region static string MakeGetSendDmKanriKirokuFileSQL(List<string> ids)
        static string MakeGetSendDmKanriKirokuFileSQL(List<string> ids)
        {
            if (ids == null)
            {
                return null;
            }
            if (ids.Count == 0)
            {
                return null;
            }

            string sql = @"
SELECT
DM_KANRI_KIROKU_FILE.*

FROM
DM_KANRI_KIROKU_FILE
WHERE (DELETE_FLAG = 0 or DELETE_FLAG is NULL)
AND DM_KANRI_KIROKU_FILE_ID IN (
";
            string prm = "";
            foreach (string id in ids)
            {
                prm += ",'" + id + "'";
            }
            sql += prm.Substring(1) + ")";

            return sql;
        }
        #endregion
        #region static string MakeGetSendDmKoubunshoKisokuFileSQL(List<string> ids)
        static string MakeGetSendDmKoubunshoKisokuFileSQL(List<string> ids)
        {
            if (ids == null)
            {
                return null;
            }
            if (ids.Count == 0)
            {
                return null;
            }

            string sql = @"
SELECT
DM_KOUBUNSHO_KISOKU_FILE.*

FROM
DM_KOUBUNSHO_KISOKU_FILE
WHERE (DELETE_FLAG = 0 or DELETE_FLAG is NULL)
AND DM_KOUBUNSHO_KISOKU_FILE_ID IN (
";
            string prm = "";
            foreach (string id in ids)
            {
                prm += ",'" + id + "'";
            }
            sql += prm.Substring(1) + ")";

            return sql;
        }
        #endregion

        #region static string MakeSQL(string tableName, int vesselId, decimal maxDataNo)
        static string MakeSQL(string tableName, int vesselId, decimal maxDataNo) 
        {
            string sql = "";

            //if (tableName == "DM_PUBLISHER" || tableName == "DM_KOUKAI_SAKI")
            //{
            //    sql = "SELECT * FROM " + tableName +
            //                 // 対象テーブルで更新されているもの
            //                 " WHERE DATA_NO > " + maxDataNo.ToString() +
            //                 // 自船が公開先に設定されているもの
            //                 " AND LINK_SAKI_ID IN " +
            //                 " ( SELECT LINK_SAKI_ID FROM DM_KOUKAI_SAKI WHERE MS_VESSEL_ID = " + vesselId + " ) "
            //                 ;
            //}
            //else 
            if (tableName == "DM_PUBLISHER" || tableName == "DM_KOUKAI_SAKI"||tableName == "DM_KAKUNIN_JOKYO" || tableName == "DM_DOC_COMMENT" || tableName == "DM_KANRYO_INFO")
            {
                //sql = "SELECT * FROM " + tableName +
                //    // 対象テーブルで更新されているもの
                //             " WHERE DATA_NO > " + maxDataNo.ToString() +
                //    // 自船が発行元 または 公開先に設定されているもの
                //             " AND ( " +
                //             " LINK_SAKI_ID IN ( SELECT LINK_SAKI_ID FROM DM_PUBLISHER WHERE MS_VESSEL_ID = " + vesselId + " ) " +
                //             " OR " +
                //             " LINK_SAKI_ID IN ( SELECT LINK_SAKI_ID FROM DM_KOUKAI_SAKI WHERE MS_VESSEL_ID = " + vesselId + " ) " +
                //             " )"
                //             ;
                sql = "SELECT " + tableName + ".* FROM " + tableName +
                    // 自船が発行元 または 公開先に設定されているもの
                             " INNER JOIN ( " +
                             " SELECT LINK_SAKI_ID FROM DM_PUBLISHER WHERE MS_VESSEL_ID = " + vesselId +
                             " UNION ALL " +
                             " SELECT LINK_SAKI_ID FROM DM_KOUKAI_SAKI WHERE MS_VESSEL_ID = " + vesselId + " ) TBL " +
                             " ON TBL.LINK_SAKI_ID = " + tableName + ".LINK_SAKI_ID " +
                    // 対象テーブルで更新されているもの
                             " WHERE " + tableName + ".DATA_NO > " + maxDataNo.ToString()
                             ;
            }
            else if (tableName == "DM_KANRI_KIROKU")
            {
                //sql = "SELECT * FROM DM_KANRI_KIROKU " +
                //             // 対象テーブルで更新されているもの
                //             " WHERE DATA_NO > " + maxDataNo.ToString() +
                //             // 自船が発行元 または 公開先に設定されているもの
                //             " AND DM_KANRI_KIROKU_ID IN ( SELECT LINK_SAKI_ID FROM DM_PUBLISHER WHERE MS_VESSEL_ID = " + vesselId + " ) " +
                //             " UNION " +
                //             " SELECT * FROM DM_KANRI_KIROKU " +
                //             " WHERE DATA_NO > " + maxDataNo.ToString() +
                //             " AND DM_KANRI_KIROKU_ID IN ( SELECT LINK_SAKI_ID FROM DM_KOUKAI_SAKI WHERE MS_VESSEL_ID = " + vesselId + " ) " 
                //             ;
                sql = "SELECT DM_KANRI_KIROKU.* FROM DM_KANRI_KIROKU " +
                    // 自船が発行元 または 公開先に設定されているもの
                             " INNER JOIN ( " +
                             " SELECT LINK_SAKI_ID FROM DM_PUBLISHER WHERE MS_VESSEL_ID = " + vesselId +
                             " UNION ALL " +
                             " SELECT LINK_SAKI_ID FROM DM_KOUKAI_SAKI WHERE MS_VESSEL_ID = " + vesselId + " ) TBL " +
                             " ON TBL.LINK_SAKI_ID = DM_KANRI_KIROKU.DM_KANRI_KIROKU_ID " +
                    // 対象テーブルで更新されているもの
                            " WHERE DM_KANRI_KIROKU.DATA_NO > " + maxDataNo.ToString();
            }
            else if (tableName == "DM_KOUBUNSHO_KISOKU")
            {
                //sql = "SELECT * FROM DM_KOUBUNSHO_KISOKU " +
                //             // 対象テーブルで更新されているもの
                //             " WHERE DATA_NO > " + maxDataNo.ToString() +
                //             // 自船が発行元 または 公開先に設定されているもの
                //             " AND ( " +
                //             " DM_KOUBUNSHO_KISOKU_ID IN ( SELECT LINK_SAKI_ID FROM DM_PUBLISHER WHERE MS_VESSEL_ID = " + vesselId + " ) " +
                //             " OR " +
                //             " DM_KOUBUNSHO_KISOKU_ID IN ( SELECT LINK_SAKI_ID FROM DM_KOUKAI_SAKI WHERE MS_VESSEL_ID = " + vesselId + " ) " +
                //             " )"
                //             ;
                sql = "SELECT DM_KOUBUNSHO_KISOKU.* FROM DM_KOUBUNSHO_KISOKU " +
                    // 自船が発行元 または 公開先に設定されているもの
                             " INNER JOIN ( " +
                             " SELECT LINK_SAKI_ID FROM DM_PUBLISHER WHERE MS_VESSEL_ID = " + vesselId +
                             " UNION ALL " +
                             " SELECT LINK_SAKI_ID FROM DM_KOUKAI_SAKI WHERE MS_VESSEL_ID = " + vesselId + " ) TBL " +
                             " ON TBL.LINK_SAKI_ID = DM_KOUBUNSHO_KISOKU.DM_KOUBUNSHO_KISOKU_ID " +
                    // 対象テーブルで更新されているもの
                            " WHERE DM_KOUBUNSHO_KISOKU.DATA_NO > " + maxDataNo.ToString();
            }


            return sql;
        }
        #endregion

        #region internal static int GetDocumentSyncMax()
        internal static int GetDocumentSyncMax()
        {
            int max = 5; // default
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(Common.接続文字列))
                {
                    conn.Open();

                    string sql = "SELECT PRM_2 FROM SN_PARAMETER";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        max = int.Parse((string)cmd.ExecuteScalar());
                    }
                    conn.Close();
                }
            }
            catch
            {
            }

            return max;
        }
        #endregion

        #region internal static DataSet GetSendAttachDataSet(int syncMax, List<string> sendAttachFileIds)
        internal static DataSet GetSendAttachDataSet(
                        int syncMax,
                        List<string> sendAttachFileIds)
        {
            int cnt = syncMax;
            DataSet dataSet = new DataSet();
            List<MsDmTemplateFile> templateFiles = new List<MsDmTemplateFile>();

            string sql1 = MakeGetOdAttachFileSQL(sendAttachFileIds);

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(Common.接続文字列))
                {
                    conn.Open();

                    if (sql1 != null)
                    {
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql1, conn))
                        {
                            DataTable dataTable = new DataTable("OD_ATTACH_FILE");

                            adapter.Fill(dataTable);
                            if (dataTable.Rows.Count > 0)
                            {
                                if (dataTable.Rows.Count > cnt)
                                {
                                    int diff = dataTable.Rows.Count - cnt;
                                    for (int i = 0; i < diff; i++)
                                    {
                                        dataTable.Rows.RemoveAt(cnt);
                                    }
                                    cnt = 0;
                                }
                                else
                                {
                                    cnt = cnt - dataTable.Rows.Count;
                                }
                                dataSet.Tables.Add(dataTable);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return dataSet;
        }
        static string MakeGetOdAttachFileSQL(List<string> ids)
        {
            if (ids == null)
            {
                return null;
            }
            if (ids.Count == 0)
            {
                return null;
            }

            string sql = @"
SELECT
OD_ATTACH_FILE.*

FROM
OD_ATTACH_FILE
WHERE (DELETE_FLAG = 0 or DELETE_FLAG is NULL)
AND OD_ATTACH_FILE_ID IN (
";
            string prm = "";
            foreach (string id in ids)
            {
                prm += ",'" + id + "'";
            }
            sql += prm.Substring(1) + ")";

            return sql;
        }
        #endregion




        #region internal static DataSet GetWtmSendDataSet(DBConnect dbConnect, string tableName, decimal maxDataNo, int vesselId, int counter)
        internal static DataSet GetWtmSendDataSet(DBConnect dbConnect, string tableName, decimal maxDataNo, int vesselId, int counter)
        {
            DataSet dataSet = new DataSet();

            try
            {
                using (NpgsqlConnection conn = dbConnect.NpgsqlConnection)
                {
                    //string tn = $"{tableName}_{Common.識別ID}";
                    //string sql = $"SELECT * FROM {tn} WHERE DATA_NO > {maxDataNo} ORDER BY DATA_NO";

                    string tn = $"{tableName}";
                    string sql = $"SELECT * FROM {tableName}_{Common.識別ID} WHERE DATA_NO > {maxDataNo} ORDER BY DATA_NO";

                    //LogFile.NBaseHonsenServiceLogWrite("", $"  sql:{sql}");

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, conn))
                    {
                        DataTable dataTable = new DataTable(tn);

                        adapter.Fill(dataTable);

                        //LogFile.NBaseHonsenServiceLogWrite("", $"    => {dataTable.Rows.Count} Records");

                        // レコードが存在する場合、追加する。
                        if (dataTable.Rows.Count > 0)
                        {
                            int startRow = (counter * 返信する最大レコード数);
                            int endRow = ((counter + 1) * 返信する最大レコード数);
                            if (dataTable.Rows.Count > endRow)
                            {
                                int max = dataTable.Rows.Count;
                                for (int i = endRow; i < max; i++)
                                {
                                    dataTable.Rows.RemoveAt(endRow);
                                }
                            }
                            if (startRow > 0)
                            {
                                for (int i = 0; i < startRow; i++)
                                {
                                    dataTable.Rows.RemoveAt(0);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                dataSet.Tables.Add(dataTable);
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                LogFile.NBaseHonsenServiceLogWrite("" , $"  GetWtmSendDataSet:Exception:{ex.Message}");
            }

            return dataSet;
        }
        #endregion

    }
}
