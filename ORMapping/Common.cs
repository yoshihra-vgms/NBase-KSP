using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ORMapping
{
    public class Common
    {
        public enum DB_TYPE
        {
            POSTGRESQL,
            ORACLE,
            POSTGRESQL_CLIENT // 20150902add SQLSERVERのreplace
        };

        public static DB_TYPE DBTYPE = DB_TYPE.POSTGRESQL;//20150917 初期値設定　元はpublic static DB_TYPE DBTYPE;

        public static readonly string DBCS_SU = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringSU"];

        public static readonly string DBCS;
        public static readonly string DB_NAME;
        public static readonly string USER_NAME;
        public static readonly string PASSWORD;


        public static bool EXECUTE_READER_LOG;
        public static bool EXECUTE_NON_QUERY_LOG;
        //public static int MAX_BINARY_SIZE = 5242880;
        public static int MAX_BINARY_SIZE = 10485760;

        static Common()
        {
            bool isDevelop = false;

            try
            {
                if (System.Configuration.ConfigurationManager.AppSettings["IsDevelop"] == "True")
                    isDevelop = true;
            }
            catch 
            {
                isDevelop = false;
            }

            try
            {
                DB_NAME = System.Configuration.ConfigurationManager.AppSettings["DBName"];
            }
            catch
            {
            }

            try
            {
                USER_NAME = System.Configuration.ConfigurationManager.AppSettings["UserName"];
            }
            catch
            {
            }

            try
            {
                PASSWORD = System.Configuration.ConfigurationManager.AppSettings["Password"];
            }
            catch
            {
            }


            try
            {
                DBCS = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            }
            catch
            {
            }

            if (DBCS == null)
            {
                DBCS = System.Configuration.ConfigurationManager.AppSettings["ConnectionString_Base"];

                if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["Test"]) == false)
                {
                    var val = System.Configuration.ConfigurationManager.AppSettings["Test"];

                    DB_NAME += $"_{val}";
                    USER_NAME += $"_{val}";
                }
                else if (isDevelop)
                {
                    DB_NAME += "_dev";
                    USER_NAME += "_dev";
                }

                DBCS += "User Id =" + USER_NAME  + ";Password=" + PASSWORD + ";Database=" + DB_NAME + ";";

            }

            bool.TryParse(System.Configuration.ConfigurationManager.AppSettings["検索Log"], out EXECUTE_READER_LOG);
            bool.TryParse(System.Configuration.ConfigurationManager.AppSettings["更新Log"], out EXECUTE_NON_QUERY_LOG);
        }

    }
}
