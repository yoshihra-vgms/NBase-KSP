#define NORMAL
//#define TEST    // Testサイト（DBがTestとなっている）
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WtmModelBase;
using WtmModels;

namespace WtmData
{
    public class AccessorCommon
    {
//#if NORMAL
//        public static string DB_NAME = "NBaseWtmDB";
//#else
//        public static string DB_NAME = "NBaseWtmDB_Test";
//#endif
        public static string DB_NAME = "NBaseWtmDB";


        public static string ConnectionKey { get; set; }

        public static string ConnectionString { get; set; } = null;


        public static ORMapping.DBConnect GetConnection()
        {
            if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["Test"]) == false)
            {
                var val = System.Configuration.ConfigurationManager.AppSettings["Test"];
                DB_NAME += $"_{val}";
            }
            else if (System.Configuration.ConfigurationManager.AppSettings["IsDevelop"] == "True")
            {
                DB_NAME += "_dev";
            }

            string connectionString = null;
            if (string.IsNullOrEmpty(ConnectionString))
            {
//#if NORMAL
//                if (ConnectionKey == "Std")
//                {
//                    connectionString = $"Server=localhost;Port=5432;User Id =WtmUser_{ConnectionKey};Password=Wtm${ConnectionKey.ToUpper()}#Pwd;Database={DB_NAME};CommandTimeout=180;";
//                }
//                else
//                {
//                    connectionString = $"Server=localhost;Port=5432;User Id =WtmUser_{ConnectionKey};Password=Wtm${ConnectionKey}#Pwd;Database={DB_NAME};CommandTimeout=180;";
//                }
//#else

//#if TEST
//                connectionString = $"Server=localhost;Port=5432;User Id =WtmUser_{ConnectionKey};Password=Wtm${ConnectionKey}#Pwd;Database={DB_NAME};CommandTimeout=180;";
//#else
//                connectionString = $"Server=nmd-server.japaneast.cloudapp.azure.com;Port=5432;User Id =WtmUser_{ConnectionKey};Password=Wtm${ConnectionKey}#Pwd;Database={DB_NAME};CommandTimeout=180;";
//#endif

//#endif

#if NORMAL
                // HonsenServiceの配置が nmd-server の場合（こちらが通常の配置位置）
                connectionString = $"Server=localhost;Port=5432;User Id =WtmUser_{ConnectionKey};Password=Wtm${ConnectionKey.ToUpper()}#Pwd;Database={DB_NAME};CommandTimeout=180;";
#else
                // HonsenServiceの配置が vgms-service1 の場合
                connectionString = $"Server=nmd-server.japaneast.cloudapp.azure.com;Port=5432;User Id =WtmUser_{ConnectionKey};Password=Wtm${ConnectionKey}#Pwd;Database={DB_NAME};CommandTimeout=180;";
#endif

            }
            else
            {
                connectionString = ConnectionString;
            }
            return new ORMapping.DBConnect(connectionString);
        }
    }
}
