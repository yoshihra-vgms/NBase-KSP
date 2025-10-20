#undef DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace ServiceReferences
{
    public class WcfServiceWrapper
    {

        private static string EndPointName = null;

        /// <summary>
        /// クラウド設定名
        /// </summary>
        private const string CloudName = "EndPoint_Cloud";

        /// <summary>
        /// ローカル設定名
        /// </summary>
        private const string LocalName = "EndPoint_Local";



        #region シングルトン実装
        /// <summary>
        /// 実態
        /// </summary>
        private static WcfServiceWrapper Instance = null;

        /// <summary>
        /// 管理取得 作成はInitLoginで行う
        /// </summary>
        public static WcfServiceWrapper GetInstance()
        {
            if (Instance == null)
            {
                Instance = new WcfServiceWrapper();
            }
            return Instance;
        }


        #endregion


        private WcfServiceWrapper()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["IsDevelop"] == "True")
                EndPointName = LocalName;   // 開発環境用（ﾛｰｶﾙｻｰﾊﾞへ接続） 
            else
                EndPointName = CloudName;   // 本番環境用（ｸﾗｳﾄﾞｻｰﾊﾞへ接続） 

            //EndPointName = LocalName; // ローカル接続
            //EndPointName = CloudName; // ｸﾗｳﾄﾞに接続
        }


        public ServiceReferences.NBaseService.ServiceClient GetServiceClient()
        {
            ServiceReferences.NBaseService.ServiceClient serviceClient = new ServiceReferences.NBaseService.ServiceClient(EndPointName);
#if DEBUG
            Write("", "接続先:" + EndPointName + ";" + serviceClient.Endpoint.Address.ToString());
#endif
            return serviceClient;
        }

        public static string ConnectedServerID
        {
            get { return EndPointName == CloudName ? "C" : "L"; }
        }



#if DEBUG
        private static string GetLogFilePath()
        {
            string logFilePath = "";
            try
            {
                string basePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
                if (System.IO.Directory.Exists(basePath) == false)
                {
                    System.IO.Directory.CreateDirectory(basePath);
                }

                //　当日の日付をファイル名にする
                string fileName = DateTime.Today.Year + DateTime.Today.Month.ToString("00") + DateTime.Today.Day.ToString("00") + ".log";

                logFilePath = basePath + "\\" + fileName;
            }
            catch
            {
            }
            return logFilePath;
        }

        public static void Write(string userName, string message)
        {
            try
            {
                // ファイルに書き込む
                string logFilePath = GetLogFilePath();
                if (logFilePath.Length > 0)
                {
                    System.IO.StreamWriter logFileSw = new System.IO.StreamWriter(new System.IO.FileStream(logFilePath, System.IO.FileMode.Append));

                    string logMessage = "[" + DateTime.Now.ToLongTimeString() + "]-[" + userName + "]:" + message;

                    logFileSw.WriteLine(logMessage);
                    logFileSw.Close();
                }
            }
            catch
            {
                // Exception発生時は無視
            }
        }
#endif
    }
}
