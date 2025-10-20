using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncClient
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

        // 使用しなくなったので削除
        ///// <summary>
        ///// ローカル設定名(BK)
        ///// </summary>
        //private const string LocalName_BK = "EndPoint_Local_BK";

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



            //EndPointName = CloudName;     // 本番環境用（ｸﾗｳﾄﾞｻｰﾊﾞへ接続） 

            //EndPointName = LocalName;       // 開発環境用（ﾛｰｶﾙｻｰﾊﾞへ接続） 
        }

        public ServiceReference1.Service1Client GetServiceClient()
        {
            return new ServiceReference1.Service1Client(EndPointName);
        }

        public static string ConnectedServerID
        {
            get { return EndPointName == CloudName ? "C" : "L"; }
        }
    }
}
