//#define DEVELOP

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping;
using ORMapping.PostgreSql;
using Npgsql;
using System.IO;
using System.Data;
using NBaseData.DAC;
using NBaseCommon;

namespace SyncClient
{
    /// <summary>
    /// 
    /// SQLServerからPostgresqlに移行の為のクラス.Sql
    /// </summary>
    public class PostgresqlClientDB : IClientDb
    {

        #region IClientDb メンバー
        /// <summary>
        /// DB作成
        /// </summary>
        void IClientDb.CreateDb()
        {
            // DBに接続できない場合、DBを作成する
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(Common.接続文字列);

                conn.Open();
                conn.Close();
            }
            catch
            {
                try
                {




                    // ユーザ作成
                    string sql1 = @"
CREATE ROLE #USERNAME LOGIN
PASSWORD '#PWD'
NOSUPERUSER INHERIT NOCREATEDB NOCREATEROLE NOREPLICATION;"
;
                    sql1 = sql1.Replace("#USERNAME", ORMapping.Common.USER_NAME);
                    sql1 = sql1.Replace("#PWD", ORMapping.Common.PASSWORD);



                    // DB作成
                    string sql2 = @"
CREATE DATABASE ""#DBNAME""
WITH OWNER = ""#USERNAME""
ENCODING = 'UTF8'
TABLESPACE = pg_default
LC_COLLATE = 'C'
LC_CTYPE = 'C'
TEMPLATE = template0
CONNECTION LIMIT = -1;"
;
                    sql2 = sql2.Replace("#DBNAME", ORMapping.Common.DB_NAME);
                    sql2 = sql2.Replace("#USERNAME", ORMapping.Common.USER_NAME);



                    // 接続と実行
                    using (NpgsqlConnection conn = new NpgsqlConnection(Common.CreateDB接続文字列))
                    {
                        conn.Open();   
                        using (NpgsqlCommand command = new NpgsqlCommand(sql1, conn))
                        {
                            command.ExecuteScalar();                           
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand(sql2, conn))
                        {
                            command.ExecuteScalar();
                        }
                        conn.Close();
                    }

                    // テーブル作成
                    this.CreateTables();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }


        /// <summary>
        /// テーブル作成SQLファイルを読込、SQLを実行する。
        /// </summary>
        //void IClientDb.CreateTables()
        void CreateTables()
        {
            NpgsqlConnection con = PostgresqlConnectionPool.Instance().GetConnection(System.Threading.Thread.CurrentThread.GetHashCode());

            using (StreamReader sr = new StreamReader(Common.テーブル作成SQLファイル))
            {
                StringBuilder buff = new StringBuilder();

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine().Trim();

                    if (line.Length == 0 || line.StartsWith("#"))
                    {
                        continue;
                    }

                    buff.Append(line);

                    if (buff[buff.Length - 1] == ';')
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand(buff.ToString(), con))
                        {
                            command.ExecuteScalar();
                        }

                        buff.Remove(0, buff.Length);
                    }
                }
            }
        }


        /// <summary>
        /// テーブル更新SQLファイルを読込、SQLを実行する。
        /// </summary>
        void IClientDb.AlterTables()
        {
            NpgsqlConnection con = PostgresqlConnectionPool.Instance().GetConnection(System.Threading.Thread.CurrentThread.GetHashCode());

            if (File.Exists(Common.テーブル更新SQLファイル))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(Common.テーブル更新SQLファイル))
                    {
                        StringBuilder buff = new StringBuilder();

                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine().Trim();

                            if (line.Length == 0 || line.StartsWith("#"))
                            {
                                continue;
                            }

                            buff.Append(line);

                            if (buff[buff.Length - 1] == ';')
                            {
                                ////try
                                ////{
                                ////    using (NpgsqlCommand command = new NpgsqlCommand(buff.ToString(), con))
                                ////    {
                                ////        command.ExecuteScalar();
                                ////    }
                                ////}
                                ////catch
                                ////{
                                ////    // Alter ファイル内は、各SQLごとにTryCatchとして、エラーは無視する
                                ////}
                                //using (NpgsqlCommand command = new NpgsqlCommand(buff.ToString(), con))
                                //{
                                //    if (buff.ToString().IndexOf("update") == 0)
                                //    {
                                //        command.ExecuteNonQuery();
                                //    }
                                //    else
                                //    {
                                //        command.ExecuteScalar();
                                //    }
                                //}
                                //buff.Remove(0, buff.Length);




                                try
                                {
                                    using (NpgsqlCommand command = new NpgsqlCommand(buff.ToString(), con))
                                    {
                                        if (buff.ToString().IndexOf("update") == 0)
                                        {
                                            command.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            command.ExecuteScalar();
                                        }
                                    }
                                }
                                catch
                                {
                                    // Alter ファイル内は、各SQLごとにTryCatchとして、エラーは無視する
                                }
                                buff.Remove(0, buff.Length);
                            }
                        }
                    }

                    File.Move(Common.テーブル更新SQLファイル, Common.テーブル更新SQLファイル + DateTime.Now.ToString("yyyyMMddHHmmss"));
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        System.Data.DataSet IClientDb.BuildUnsendDataSet()
        {
            DataSet unsendDataSet = new DataSet();

            NpgsqlConnection con = PostgresqlConnectionPool.Instance().GetConnection(System.Threading.Thread.CurrentThread.GetHashCode());

            // サーバに送信するテーブル名一覧を取得.
            List<string> sendTableNames = BuildSendTableNames();


            //====== LOG ==========
            string LogStr = "";
            LogStr = "　　船⇒送信ﾃﾞｰﾀ";
            LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, LogStr);

            foreach (string tableName in sendTableNames)
            {
                AddUnsendData(LogFile.同期, unsendDataSet, con, tableName);
            }

            //====== LOG ==========
            if (unsendDataSet.Tables.Count == 0)
            {
                LogStr = "　　送信ﾃﾞｰﾀなし";
                LogFile.NBaseHonsenLogWrite(LogFile.同期, 同期Client.LOGIN_USER.FullName, LogStr);
            }

            return unsendDataSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        decimal[] IClientDb.GetMaxDataNo()
        {
            decimal[] maxDataNos = new decimal[4];

            NpgsqlConnection con = PostgresqlConnectionPool.Instance().GetConnection(System.Threading.Thread.CurrentThread.GetHashCode());

            string datanoSQL = @"
SELECT ID, DATA_NO
FROM DATA_NO
";
            //SQLを発行する
            using (NpgsqlCommand command_DataNo = new NpgsqlCommand(datanoSQL, con))
            {
                using (NpgsqlDataReader dr = command_DataNo.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int id = Convert.ToInt32(dr[0]);
                        decimal max = Convert.ToDecimal(dr[1]);

                        //if (id == 0)
                        //{
                        //    maxDataNos[0] = max;
                        //}
                        //else
                        //{
                        //    maxDataNos[1] = max;
                        //}
                        maxDataNos[id] = max;
                    }
                }
            }

            return maxDataNos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxDataNoOfVesselIdZero"></param>
        /// <param name="maxDataNo"></param>
        /// <param name="postionOfZero">StartPositionOfZero</param>
        /// <param name="postion">StartPostion</param>
        /// <param name="dbConnect"></param>
        void IClientDb.SetMaxDataNo(decimal maxDataNoOfVesselIdZero, decimal maxDataNo, decimal postionOfZero, decimal postion, ORMapping.DBConnect dbConnect)
        {
            if (maxDataNoOfVesselIdZero > 0)
            {
                SaveMaxDataNo(0, maxDataNoOfVesselIdZero, dbConnect);
            }

            if (maxDataNo > 0)
            {
                SaveMaxDataNo(1, maxDataNo, dbConnect);
            }

            if (postionOfZero > 0)
            {
                SaveMaxDataNo(2, postionOfZero, dbConnect);
            }

            if (postion > 0)
            {
                SaveMaxDataNo(3, postion, dbConnect);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        System.Data.DataSet IClientDb.BuildUnsendDocumentFileDataSet()
        {
            string[] tables = { "DM_KANRI_KIROKU_FILE", "DM_KOUBUNSHO_KISOKU_FILE" };
            DataSet unsendDataSet = new DataSet();

            NpgsqlConnection con = PostgresqlConnectionPool.Instance().GetConnection(System.Threading.Thread.CurrentThread.GetHashCode());

            //====== LOG ==========
            string LogStr = "";
            LogStr = "　　船⇒送信文書ﾃﾞｰﾀ";
            LogFile.NBaseHonsenLogWrite(LogFile.文書, 同期Client.LOGIN_USER.FullName, LogStr);

            for (int i = 0; i < tables.Length; i++)
            {
                AddUnsendData(LogFile.文書, unsendDataSet, con, tables[i]);
            }

            //====== LOG ==========
            if (unsendDataSet.Tables.Count == 0)
            {
                LogStr = "　　送信文書ﾃﾞｰﾀなし";
                LogFile.NBaseHonsenLogWrite(LogFile.文書, 同期Client.LOGIN_USER.FullName, LogStr);
            }

            return unsendDataSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int[] IClientDb.GetDocumentParameter()
        {
            int[] Params = new int[3];

            NpgsqlConnection con = PostgresqlConnectionPool.Instance().GetConnection(System.Threading.Thread.CurrentThread.GetHashCode());

            string SQL = @"
SELECT PRM_1, PRM_2, PRM_3
FROM SN_PARAMETER
";
            //SQLを発行する
            using (NpgsqlCommand command = new NpgsqlCommand(SQL, con))
            {
                using (NpgsqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Params[0] = Convert.ToInt32(dr[0]);
                        Params[1] = Convert.ToInt32(dr[1]);
                        Params[2] = Convert.ToInt32(dr[2]);
                    }
                }
            }

            return Params;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        System.Data.DataSet IClientDb.BuildUnsendAttachFileDataSet()
        {
            string[] tables = { "OD_ATTACH_FILE" };
            DataSet unsendDataSet = new DataSet();

            //====== LOG ==========
            string LogStr = "";
            LogStr = "　　船⇒送信添付ﾃﾞｰﾀ";
            LogFile.NBaseHonsenLogWrite(LogFile.文書, 同期Client.LOGIN_USER.FullName, LogStr);

            NpgsqlConnection con = PostgresqlConnectionPool.Instance().GetConnection(System.Threading.Thread.CurrentThread.GetHashCode());

            for (int i = 0; i < tables.Length; i++)
            {
                AddUnsendData(LogFile.文書, unsendDataSet, con, tables[i]);
            }

            //====== LOG ==========
            if (unsendDataSet.Tables.Count == 0)
            {
                LogStr = "　　送信添付ﾃﾞｰﾀなし";
                LogFile.NBaseHonsenLogWrite(LogFile.文書, 同期Client.LOGIN_USER.FullName, LogStr);
            }

            return unsendDataSet;
        }

        #endregion

        private List<string> BuildSendTableNames()
        {
            List<string> sendTableNames = new List<string>();
            List<SnTableInfo> tableInfos = SnTableInfo.GetRecords(同期Client.LOGIN_USER);

            foreach (SnTableInfo i in tableInfos)
            {
                sendTableNames.Add(i.Name);
            }

            return sendTableNames;
        }

        private void AddUnsendData(int flg, DataSet unsendDataSet, NpgsqlConnection con, string tableName)
        {
            // 未送信のデータを取得して、DataSet に追加する.
            string sql = @"select * from " + tableName
                + " where SEND_FLAG = 0";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            DataTable dt1 = new DataTable(tableName);

            da.FillSchema(dt1, SchemaType.Source);

            da.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {
                unsendDataSet.Tables.Add(dt1);

                //====== LOG ==========
                string LogStr = "";
                LogStr = "　　　";
                LogStr += tableName + " : Count = " + dt1.Rows.Count.ToString();
                LogFile.NBaseHonsenLogWrite(flg, 同期Client.LOGIN_USER.FullName, LogStr);
            }
        }

        private static void SaveMaxDataNo(int id, decimal maxDataNo, DBConnect dbConnect)
        {
            NpgsqlConnection con = PostgresqlConnectionPool.Instance().GetConnection(System.Threading.Thread.CurrentThread.GetHashCode());

            int count = 0;
            string numberSQL = @"
SELECT COUNT(*) 
FROM DATA_NO
WHERE ID = :id
";
            //SQLを発行する
            NpgsqlCommand command = new NpgsqlCommand(numberSQL, con);
            command.Transaction = dbConnect.NpgsqlTrans;

            NpgsqlParameter p = new NpgsqlParameter("id", id);
            p.DbType = System.Data.DbType.Int32;
            command.Parameters.Add(p);

            object obj = command.ExecuteScalar();

            try
            {
                count = Convert.ToInt32(obj);
            }
            catch
            {
                count = 0;
            }

            if (count > 0)
            {
                //update
                string sql = @"
UPDATE DATA_NO SET
  DATA_NO      = :data_no
WHERE ID = :id
";

                //SQLを発行する
                NpgsqlCommand command_DataNo = new NpgsqlCommand(sql, con);
                command_DataNo.Transaction = dbConnect.NpgsqlTrans;

                #region パラメータセット
                NpgsqlParameter p1 = new NpgsqlParameter("data_no", maxDataNo);
                NpgsqlParameter p2 = new NpgsqlParameter("id", id);
                p1.DbType = System.Data.DbType.Decimal;
                p2.DbType = System.Data.DbType.Int32;
                command_DataNo.Parameters.Add(p1);
                command_DataNo.Parameters.Add(p2);
                #endregion

                int lines = command_DataNo.ExecuteNonQuery();
            }
            else
            {
                // insert
                string sql = @"
insert into DATA_NO
(ID, DATA_NO)
VALUES
(:id, :data_no)
";

                //SQLを発行する
                NpgsqlCommand command_DataNo = new NpgsqlCommand(sql, con);
                command_DataNo.Transaction = dbConnect.NpgsqlTrans;

                #region パラメータセット
                NpgsqlParameter p1 = new NpgsqlParameter("data_no", maxDataNo);
                NpgsqlParameter p2 = new NpgsqlParameter("id", id);
                p1.DbType = System.Data.DbType.Decimal;
                p2.DbType = System.Data.DbType.Int32;
                command_DataNo.Parameters.Add(p1);
                command_DataNo.Parameters.Add(p2);
                #endregion

                int lines = command_DataNo.ExecuteNonQuery();
            }
        }

    }
}
