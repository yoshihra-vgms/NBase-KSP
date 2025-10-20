using Npgsql;
using ORMapping.PostgreSql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncClient
{
    public class WtmPostgresqlClientDB
    {
        public static string ConnectionKey { get; set; }

        public void CreateWtmDb()
        {
            // DBに接続できない場合、DBを作成する
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(Common.WtmBase接続文字列);

                conn.Open();
                conn.Close();
            }
            catch
            {
                try
                {

                    // ユーザ作成
                    string sql1 = $@"
CREATE ROLE ""{Common.WTM_USER_NAME}"" LOGIN
PASSWORD 'Wtm#Pwd'
NOSUPERUSER INHERIT NOCREATEDB NOCREATEROLE NOREPLICATION;"
;


                    // DB作成
                    string sql2 = $@"
CREATE DATABASE ""{Common.WTM_DB_NAME}""
WITH OWNER = ""{Common.WTM_USER_NAME}""
ENCODING = 'UTF8'
TABLESPACE = pg_default
LC_COLLATE = 'C'
LC_CTYPE = 'C'
TEMPLATE = template0
CONNECTION LIMIT = -1;"
;

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
                    using (NpgsqlConnection conn = new NpgsqlConnection(Common.WtmBase接続文字列))
                    {
                        conn.Open();

                        this.CreateTables(conn, Common.WtmBaseテーブル作成SQLファイル);

                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }



            // DBに接続できない場合、ユーザ、テーブルを作成する
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(Common.Wtm接続文字列);

                conn.Open();
                conn.Close();
            }
            catch
            {
                try
                {

                    // ユーザ作成
                    string sql1 = $@"
CREATE ROLE ""{Common.WTM_USER_NAME}_{Common.KEY.ToLower()}"" LOGIN
PASSWORD 'Wtm${Common.KEY}#Pwd'
NOSUPERUSER INHERIT NOCREATEDB NOCREATEROLE NOREPLICATION;"
;


                    // 権限
                    string sql2 = $@"GRANT CONNECT ON DATABASE ""{Common.WTM_DB_NAME}"" TO ""{Common.WTM_USER_NAME}_{Common.KEY.ToLower()}"";";

                    // 権限
                    string sql3 = $@"GRANT ""{Common.WTM_USER_NAME}_{Common.KEY.ToLower()}"" TO ""{Common.WTM_USER_NAME}"";";

                    // 権限
                    string sql4 = $@"REVOKE ""{Common.WTM_USER_NAME}_{Common.KEY.ToLower()}"" FROM ""{Common.WTM_USER_NAME}"";";

                    // 接続と実行
                    using (NpgsqlConnection conn = new NpgsqlConnection(Common.CreateDB接続文字列))
                    {
                        conn.Open();
                        using (NpgsqlCommand command = new NpgsqlCommand(sql1, conn))
                        {
                            command.ExecuteScalar();
                        }
                        //using (NpgsqlCommand command = new NpgsqlCommand(sql2, conn))
                        //{
                        //    command.ExecuteScalar();
                        //}

                        conn.Close();
                    }

                    // テーブル作成
                    using (NpgsqlConnection conn = new NpgsqlConnection(Common.WtmBase接続文字列))
                    {
                        conn.Open();

                        this.CreateTables(conn, Common.Wtmテーブル作成SQLファイル);

                        conn.Close();
                    }

                    using (NpgsqlConnection conn = new NpgsqlConnection(Common.Wtm接続文字列))
                    {
                        conn.Open();

                        using (NpgsqlCommand command = new NpgsqlCommand(sql3, conn))
                        {
                            command.ExecuteScalar();
                        }
                        conn.Close();
                    }

                    using (NpgsqlConnection conn = new NpgsqlConnection(Common.WtmBase接続文字列))
                    {
                        conn.Open();

                        this.CreateTables(conn, Common.WtmRoleSQLファイル);

                        conn.Close();
                    }

                    using (NpgsqlConnection conn = new NpgsqlConnection(Common.Wtm接続文字列))
                    {
                        conn.Open();

                        using (NpgsqlCommand command = new NpgsqlCommand(sql4, conn))
                        {
                            command.ExecuteScalar();
                        }
                        conn.Close();
                    }
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
        void CreateTables(NpgsqlConnection conn, string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
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
                        var cmd = buff.ToString();
                        cmd = cmd.Replace("#BASE_USER#", $"{Common.WTM_USER_NAME}");
                        cmd = cmd.Replace("#USER#", $"{Common.WTM_USER_NAME}_{Common.KEY.ToLower()}");
                        cmd = cmd.Replace("#TABLE_KEY#", $"{Common.WTM_TABLE_KEY.ToLower()}");
                        cmd = cmd.Replace("#SITE_ID#", $"{Common.WTM_TABLE_KEY.ToUpper()}");

                        using (NpgsqlCommand command = new NpgsqlCommand(cmd, conn))
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
        public void AlterWtmTables()
        {
            NpgsqlConnection conn = new NpgsqlConnection(Common.WtmBase接続文字列);

            if (File.Exists(Common.Wtm更新SQLファイル))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(Common.Wtm更新SQLファイル))
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
                                try
                                {
                                    using (NpgsqlCommand command = new NpgsqlCommand(buff.ToString(), conn))
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

                    File.Move(Common.Wtm更新SQLファイル, Common.Wtm更新SQLファイル + DateTime.Now.ToString("yyyyMMddHHmmss"));
                }
                catch
                {

                }
            }
        }


    }
}
