using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Configuration;

namespace DeficiencyControl
{
    /// <summary>
    /// アプリケーション構成ファイル初期化
    /// </summary>
    public class AppConfig
    {
        private AppConfig()
        {
        }

        #region シングルトン
        /// <summary>
        /// 実体
        /// </summary>
        private static AppConfig Instance = null;

        /// <summary>
        /// 取得
        /// </summary>
        public static AppConfig Config
        {
            get
            {
                if (AppConfig.Instance == null)
                {
                    AppConfig.Instance = new AppConfig();
                }

                return AppConfig.Instance;
            }
        }
        #endregion

        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Appconfigデータ一覧
        /// </summary>
        public class AppConfigData
        {
            /// <summary>
            /// メッセージファイルパス
            /// </summary>
            public string MessageFilePath = "message.csv";

            /// <summary>
            /// 最大添付サイズ(MB)
            /// </summary>
            public long MaxAttachmentSizeMB = 15;

            /// <summary>
            /// 最大添付ファイルサイズをバイトで取得
            /// </summary>
            public long MaxAttachmentSizeB
            {
                get
                {
                    long ans = this.MaxAttachmentSizeMB * 1024 * 1024;
                    return ans;
                }
            }

            #region デバッグモード定義

            /// <summary>
            /// DeficiencyControl青モード FileCloseを一括管理するモード
            /// </summary>
            public bool DeficiencyControlBlueMode = false;

            #endregion

            #region DB定義値

            /// <summary>
            /// ms_action_code 99 free textのID値
            /// </summary>
            public int ms_action_code_99 = 16;

            /// <summary>
            /// ms_user adminの値
            /// </summary>
            public string ms_user_admin = "";
            #endregion
        }


        /// <summary>
        /// Appconfigデータ
        /// </summary>
        public AppConfigData ConfigData = null;


        #region 変換関数
        /// <summary>
        /// 設定値をboolで取得 0:false 1:成功
        /// </summary>
        /// <param name="key">取得AppCinfigのKey</param>
        /// <param name="def">デフォルト値</param>
        /// <returns></returns>
        private bool GetBool(string key, bool def = false)
        {
            bool ans = true;

            try
            {
                //値を取得して変換
                string s = ConfigurationManager.AppSettings[key];
                int n = Convert.ToInt32(s);
                if (n == 0)
                {
                    ans = false;
                }
            }
            catch (Exception e)
            {
                return def;
            }


            return ans;
        }

        /// <summary>
        /// 設定値をintで取得
        /// </summary>
        /// <param name="key">取得AppCinfigのKey</param>
        /// <param name="def">デフォルト値</param>
        /// <returns></returns>
        private int GetInt(string key, int def = 0)
        {
            int ans = def;

            try
            {
                //値を取得して変換
                string s = ConfigurationManager.AppSettings[key];
                ans = Convert.ToInt32(s);
            }
            catch (Exception e)
            {
                return def;
            }
            return ans;
        }

        /// <summary>
        /// 設定値を文字列で取得
        /// </summary>
        /// <param name="key">取得AppCinfigのKey</param>
        /// <param name="def">デフォルト値</param>
        /// <returns></returns>
        private string GetString(string key, string def = "")
        {
            string ans = def;

            try
            {
                ans = ConfigurationManager.AppSettings[key];
                if (ans == null)
                {
                    return def;
                }

            }
            catch (Exception e)
            {
                return def;
            }

            return ans;
        }

        #endregion


        /// <summary>
        /// 読み込み
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            this.ConfigData = new AppConfigData();
            AppConfigData cdata = this.ConfigData;

            //メッセージファイル
            cdata.MessageFilePath = GetString("MessageFilePath", cdata.MessageFilePath);

            //最大添付ファイルサイズ
            cdata.MaxAttachmentSizeMB = (long)GetInt("MaxAttachmentSizeMB", (int)cdata.MaxAttachmentSizeMB);

            //=============================================================================================================
            //DeficiencyControlデバッグモード
            //=============================================================================================================
            cdata.DeficiencyControlBlueMode = GetBool("DeficiencyControlBlueMode", false);

            //=============================================================================================================
            //DB
            //=============================================================================================================


            //MsActionCode
            cdata.ms_action_code_99 = GetInt("ms_action_code_99", cdata.ms_action_code_99);

            //MsUser
            cdata.ms_user_admin = GetString("ms_user_admin", cdata.ms_user_admin);


            return true;
        }

    }
}
