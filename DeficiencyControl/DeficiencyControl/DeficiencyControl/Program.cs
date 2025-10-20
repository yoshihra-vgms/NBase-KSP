using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Deployment.Application;
using System.Collections.Specialized;
using System.Web;
using DeficiencyControl.Files;
using DcCommon.DB;
using DeficiencyControl.Forms;
using System.IO;

namespace DeficiencyControl
{
    static class Program
    {
        /// <summary>
        /// ハンドルエラー 異常一括処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void ThUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                DcLog.WriteLog(ex, "Thread Unhandled Exception");
            }
            else
            {
                DcLog.WriteLog(e.ExceptionObject, "Thread Unhandled Exception");
            }

        }

        /// <summary>
        /// スレッド異常を一括取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void AppThException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            DcLog.WriteLog(e.Exception, "Application ThreadException");
        }


        /// <summary>
        /// クリックワンス自動ログイン true=せいこう false=失敗画面表示
        /// </summary>
        /// <returns></returns>
        static bool AutoLoginClickonce()
        {
            try
            {
                if (ApplicationDeployment.IsNetworkDeployed == false)
                {
                    DcLog.WriteLog("これはクリックワンスではありません。");
                    return false;
                }

                DcLog.WriteLog("Clickonce 自動ログイン処理開始!!");

                // クリックワンスURLログイン/ログインファイルでログイン
                string loginid = "";
                string password = "";

                try
                {
                    //クリックワンスURL解析
                    #region 引数解析
                    string paramurl = ApplicationDeployment.CurrentDeployment.ActivationUri.Query;
                    DcLog.WriteLog("ActivationUri=" + paramurl);

                    DcLog.WriteLog("クリックワンス引数------------------------");
                    NameValueCollection avec = HttpUtility.ParseQueryString(paramurl);
                    foreach (string key in avec.Keys)
                    {
                        //全引数の表示
                        string cp = string.Format("{0}={1}", key, avec[key]);
                        DcLog.WriteLog(cp, false);

                    }
                    DcLog.WriteLog("------------------------");
                    #endregion

                    //IDとパスワード取得
                    loginid = avec["p1"];
                    password = avec["p2"];
                }
                catch (Exception ex)
                {
                    DcLog.WriteLog(ex, "AutoLoginClickonce 引数解析");
                    DcLog.WriteLog("AutoLoginClickonce 引数解析失敗!!");

                    // ログインファイルでログイン
                    if (GetLoginFile(ref loginid, ref password) == false)
                    {
                        DcLog.WriteLog("AutoLoginClickonce ログインファイルでのログイン失敗!!");
                        throw ex;
                    }
                }

                //プロキシ設定の読み込みと表示
                ProxySetting setteing = ProxySetting.Read();
                ProxySetting.SetUseProxy(false);


                //サービス管理初期化とログイン
                //SvcManager.Init();
                //UserData udata = SvcManager.SvcMana.Login(loginid, password);
                UserData udata = SvcManager.InitLogin(loginid, password);
                if (udata == null)
                {
                    throw new Exception("User NULL");
                }

                //ログインユーザー設定
                DcGlobal.Global.LoginUser = udata;


            }
            catch (Exception ex)
            {
                DcLog.WriteLog(ex, "AutoLoginClickonce");

                DcLog.WriteLog("Clickonce 自動ログイン処理失敗!!");
                return false;
            }

            DcLog.WriteLog("Clickonce 自動ログイン処理成功!!");

            return true;
        }

        /// <summary>
        /// ログインファイルからuserIDとpasswordを取得
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        static bool GetLoginFile(ref string userID, ref string password)
        {
            userID = "";
            password = "";

            // ログインファイルパス取得
            string loginFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            loginFilePath += @"\WING";
            loginFilePath += @"\login";

            // ファイル存在確認
            if (File.Exists(loginFilePath) == false)
            {
                string msg = "ログインファイルが見つかりません。path=";
                msg += loginFilePath;
                DcLog.WriteLog(msg);
                return false;
            }

            // ログインファイルからID,Passを読む
            try
            {
                StreamReader sr = new StreamReader(loginFilePath);
                while (sr.EndOfStream == false)
                {
                    // ログインID取得
                    string l1 = sr.ReadLine();
                    string[] a1 = l1.Split('=');
                    userID = a1[1];

                    // パスワード取得
                    string l2 = sr.ReadLine();
                    string[] a2 = l2.Split('=');
                    password = a2[1];
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                DcLog.WriteLog(ex, "GetLoginFile");
                DcLog.WriteLog("GetLoginFile ファイル読み込み失敗!!");
                return false;
            }

            // 次回のためにログインファイルを削除する。
            File.Delete(loginFilePath);

            return true;
        }        

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //初期化
            AppConfig.Config.Init();

            //環境設定の初期化
            DcGlobal.Global.Env = new DcEnv();
            {
                DcEnv.DcEnvData edata = new DcEnv.DcEnvData();
                //必要なら設定を
                DcGlobal.Global.Env.InitEnv(edata);
            }

            //ログの初期化
            {
                string logfol = DcGlobal.Global.Env.LogFolderPath;
                DcLog.Log.Init(logfol, "IGT_DefCon.log", true);
            }

            //メッセージの初期化
            {
                string mesfilename = AppConfig.Config.ConfigData.MessageFilePath;
                DcMes.Mes.Init(mesfilename);
            }

            //-----------------------------
            //例外をまとめて処理する
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(AppThException);
            System.Threading.Thread.GetDomain().UnhandledException += new UnhandledExceptionEventHandler(ThUnhandledException);
            //-----------------------------

            DcLog.WriteLog("********************** IGT DeficiencyControl開始 **********************");
            DcLog.WriteLog("ExePath=" + Application.ExecutablePath, false);
            DcLog.WriteLog("CurrentDirectory=" + Environment.CurrentDirectory, false);
            DcLog.WriteLog("DocumentFolderPath=" + DcGlobal.Global.Env.DocumentFolderPath, false);
            DcLog.WriteLog("-------------------------------------", false);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //クリップワンスバージョン表示
            DcLog.WriteLog("ProductVersion=" + Application.ProductVersion, false);            
            if (ApplicationDeployment.IsNetworkDeployed == true)
            {
                Version cv = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                DcLog.WriteLog("ClickOnceVersion=" + cv.ToString(), false);
            }


            Form f = new LoginForm();
            //クリックワンス自動ログインに成功したらポータルを表示する
            bool autologin = AutoLoginClickonce();
            if (autologin == true)
            {

                f = new PortalForm();
            }
            

            Application.Run(f);
            

            //ログアウト処理      
            if (DcGlobal.Global.LoginUser != null)
            {
                SvcManager.SvcMana.LogoutUser(DcGlobal.Global.LoginUser.User);
            }
            

            DcLog.WriteLog("-------------------------------------", false);
            DcLog.WriteLog("********************** IGT DeficiencyControl完了 **********************");
        }
    }
}
