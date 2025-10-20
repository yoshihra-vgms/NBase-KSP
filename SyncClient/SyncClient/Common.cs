using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SyncClient
{
    public class Common
    {

        //
        // PostgreSQL接続文字列(スーパユーザ)
        //
        public static string CreateDB接続文字列
        {
            get
            {
                return ORMapping.Common.DBCS_SU;
            }
        }


        //
        // PostgreSQL接続文字列
        //       
        public static string 接続文字列
        {
            get
            {
                return ORMapping.Common.DBCS;
            }
        }

        //
        // PostgreSQL接続文字列(BK環境用)
        //    
        //public static string 接続文字列_DEV
        //{
        //    get
        //    {
        //        return ORMapping.Common.DBCS;
        //    }
        //}


        //public static string DB名
        //{
        //    get
        //    {
        //        return System.Configuration.ConfigurationManager.AppSettings["DBName"];
        //    }
        //}
        //public static string DB名_BK
        //{
        //    get
        //    {
        //        return System.Configuration.ConfigurationManager.AppSettings["DBName_BK"];
        //    }
        //}





        public static string テーブル作成SQLファイル
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MakeTableSql"];
            }
        }

        public static string テーブル更新SQLファイル
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["AlterTableSql"];
            }
        }



        public static int 同期間隔
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["SyncInterval"]);
            }
        }

        public static int SplitCount
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["SplitCount"]);
            }
        }



        public static int スキーマのバージョン
        {
            get
            {
                int schemaVersion = 0;
                try
                {
                    schemaVersion
                        = int.Parse(System.Configuration.ConfigurationManager.AppSettings["SchemaVersion"]);
                }
                catch
                {
                    schemaVersion = 0;
                }
                return schemaVersion;
            }
        }

        public static bool オフラインモード
        {
            get
            {
                return bool.Parse(System.Configuration.ConfigurationManager.AppSettings["Offline"]);
            }
        }



        public static readonly string DBCS;
        public static readonly string KEY;
        //public static readonly string DB_NAME;
        //public static readonly string USER_NAME;
        public static readonly string WTM_DB_NAME;
        public static readonly string WTM_USER_NAME;
        public static readonly string WTM_TABLE_KEY;
        public static readonly string WTM_NAMESPACE_KEY;

        public static readonly int WTM_SYNC_INTERVAL;


        static Common()
        {
            var n = 1;
            WTM_SYNC_INTERVAL = 60000 * n;

            try
            {
                DBCS = System.Configuration.ConfigurationManager.AppSettings["ConnectionString_Base"]; // NBase、Wtm共通

                KEY = System.Configuration.ConfigurationManager.AppSettings["ConnectionKey"];
                WTM_DB_NAME = System.Configuration.ConfigurationManager.AppSettings["WtmDBName"];
                WTM_USER_NAME = System.Configuration.ConfigurationManager.AppSettings["WtmUserName"];
                WTM_TABLE_KEY = System.Configuration.ConfigurationManager.AppSettings["WtmTableKey"];
                WTM_NAMESPACE_KEY = System.Configuration.ConfigurationManager.AppSettings["WtmNameSpaceKey"];

                if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["Test"]) == false)
                {
                    WTM_DB_NAME += "_test";
                    WTM_USER_NAME += "_test";
                }
                else if (System.Configuration.ConfigurationManager.AppSettings["IsDevelop"] == "True")
                {
                    WTM_DB_NAME += "_dev";
                    WTM_USER_NAME += "_dev";
                }
            }
            catch
            {
            }
        }



        //
        // PostgreSQL接続文字列
        //       
        public static string WtmBase接続文字列
        {
            get
            {
                return DBCS + $"User Id={WTM_USER_NAME}; Password=Wtm#Pwd; Database ={WTM_DB_NAME};";
            }
        }

        //
        // PostgreSQL接続文字列
        //       
        public static string Wtm接続文字列
        {
            get
            {
                //return DBCS + $"User Id={WTM_USER_NAME}_{KEY}; Password=Wtm${KEY}#Pwd; Database ={WTM_DB_NAME};";
                return DBCS + $"User Id={WTM_USER_NAME}_{KEY.ToLower()}; Password=Wtm${KEY}#Pwd; Database ={WTM_DB_NAME};";
            }
        }

        public static string WtmBaseテーブル作成SQLファイル
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MakeWtmBaseTableSql"];
            }
        }
        public static string Wtmテーブル作成SQLファイル
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MakeWtmTableSql"];
            }
        }
        public static string WtmRoleSQLファイル
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["AlterWtmRoleSql"];
            }
        }
        public static string Wtm更新SQLファイル
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["AlterWtmTableSql"];
            }
        }

    }

}
