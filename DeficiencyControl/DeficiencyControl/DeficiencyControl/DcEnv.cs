using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Deployment.Application;

namespace DeficiencyControl
{
    /// <summary>
    /// 環境設定クラス
    /// </summary>
    public class DcEnv
    {
        /// <summary>
        /// 環境設定情報
        /// </summary>
        public class DcEnvData
        {
            /// <summary>
            /// アプリケーションルートフォルダ名
            /// </summary>
            public string AppRootFolderName = "DeficiencyControl_NBase";

            /// <summary>
            /// ログフォルダ名
            /// </summary>
            public string LogFolderName = "Log";
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DcEnv()
        {
        }

        
        /// <summary>
        /// 環境設定情報
        /// </summary>
        private DcEnvData EData = null;


        /// <summary>
        /// 環境初期化
        /// </summary>
        /// <param name="edata">環境設定情報</param>
        /// <returns>成功可否</returns>
        public bool InitEnv(DcEnvData edata)
        {
            this.EData = edata;

            //フォルダの初期化            
            bool fret = false;

            try
            {
                //アプリケーションフォルダ
                string appfol = this.ApplicationRootFolderPath;
                fret = Directory.Exists(appfol);
                if (fret == false)
                {
                    Directory.CreateDirectory(appfol);
                }

                //ログフォルダ
                string logfol = this.LogFolderPath;
                fret = Directory.Exists(logfol);
                if (fret == false)
                {
                    Directory.CreateDirectory(logfol);
                }
            }
            catch
            {
                return false;
            }
            

            return true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ドキュメントフォルダパス
        /// </summary>
        public string DocumentFolderPath
        {
            get
            {
                //ドキュメントパス取得
                string ans = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return ans;
            }
        }

        /// <summary>
        /// クリックワンスデータパスの取得
        /// </summary>
        public string ClickonceDataPath
        {
            get
            {
                //カレントパス
                string path = Directory.GetCurrentDirectory();

                //インストールされているなら・・・
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    path = ApplicationDeployment.CurrentDeployment.DataDirectory;
                }
                return path;
            }
        }

        /// <summary>
        /// アプリケーションルートフォルダ取得
        /// </summary>
        public string ApplicationRootFolderPath
        {
            get
            {
                string ans = this.DocumentFolderPath + "\\" + this.EData.AppRootFolderName;
                return ans;
            }
        }

        /// <summary>
        /// ログフォルダパス
        /// </summary>
        public string LogFolderPath
        {
            get
            {
                string ans = this.ApplicationRootFolderPath + "\\" + this.EData.LogFolderName;
                return ans;
            }
        }
    }
}
