using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ServiceModel;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

using NBaseData.DS;
using NBase.util;


namespace NBase
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            //// リクエストを投げる前に行う
            SSLCertificateVaridation sslCertificateVaridation = new SSLCertificateVaridation();



#if Deficiency含む場合
            ///
            ///
            ///
            {
                //初期化
                DeficiencyControl.AppConfig.Config.Init();

                //環境設定の初期化
                DeficiencyControl.DcGlobal.Global.Env = new DeficiencyControl.DcEnv();
                {
                    DeficiencyControl.DcEnv.DcEnvData edata = new DeficiencyControl.DcEnv.DcEnvData();
                    //必要なら設定を
                    DeficiencyControl.DcGlobal.Global.Env.InitEnv(edata);
                }

                //ログの初期化
                {
                    string logfol = DeficiencyControl.DcGlobal.Global.Env.LogFolderPath;
                    DeficiencyControl.DcLog.Log.Init(logfol, "DefCon_NBase.log", true);
                }

                //メッセージの初期化
                {
                    string mesfilename = DeficiencyControl.AppConfig.Config.ConfigData.MessageFilePath;
                    DeficiencyControl.DcMes.Mes.Init(mesfilename);
                }

                //-----------------------------
                //例外をまとめて処理する
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(AppThException);
                System.Threading.Thread.GetDomain().UnhandledException += new UnhandledExceptionEventHandler(ThUnhandledException);
                //-----------------------------

                DeficiencyControl.DcLog.WriteLog("********************** DeficiencyControl開始 **********************");
                DeficiencyControl.DcLog.WriteLog("ExePath=" + Application.ExecutablePath, false);
                DeficiencyControl.DcLog.WriteLog("CurrentDirectory=" + Environment.CurrentDirectory, false);
                DeficiencyControl.DcLog.WriteLog("DocumentFolderPath=" + DeficiencyControl.DcGlobal.Global.Env.DocumentFolderPath, false);
                DeficiencyControl.DcLog.WriteLog("-------------------------------------", false);

            }
#endif

            MsRoleTableCache.instance().DacProxy = new WcfMsRoleDacProxy();
            NbaseContractFunctionTableCache.instance().DacProxy = new WcfNbaseContractFunctionDacProxy();
            NBaseCommon.FileView.Remove();


            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                serviceClient.TraceStart(NBaseCommon.Common.HostName);
            }


            NBaseCommon.Common.AppName = "NBase";
            LoginForm loginForm = new LoginForm();

            Application.Run(loginForm);
            
            if (loginForm.DialogResult == DialogResult.OK)
            {
                try
                {
                    //if (loginForm.IsMaintenance)
                    //    Application.Run(new Hachu.発注修正Form());
                    //else
                    //    Application.Run(new PortalForm());

                    Application.Run(new PortalForm());
                }
                catch (FaultException exc)
                {
                    if (exc.Code.Name == "ログインセッションエラー")
                    {
                        MessageBox.Show("一定時間操作を行わなかったため\nセッションタイムアウトが発生しました\n再度ログインを行って下さい", "セッションタイムアウト", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Restart();
                    }
                    else
                    {
                        MessageBox.Show(exc.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("キャンセルされました。プログラムを終了します。", "確認");
            }

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                if (NBaseCommon.Common.LoginUser != null)
                    serviceClient.TraceEnd(NBaseCommon.Common.LoginUser, NBaseCommon.Common.LoginUser.MsUserID, NBaseCommon.Common.LoginUser.BumonID, NBaseCommon.Common.HostName);
                else
                    serviceClient.TraceEnd(new NBaseData.DAC.MsUser(), "end", null, NBaseCommon.Common.HostName);
            }

            Application.Exit();
        }


        public class SSLCertificateVaridation
        {
            public SSLCertificateVaridation()
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(OnRemoteCertificateValidationCallback);

            }

            // 信頼できないSSL証明書を「問題なし」にするメソッド
            private bool OnRemoteCertificateValidationCallback(
              Object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
            {
                return true;  // 「SSL証明書の使用は問題なし」と示す
            }
        }



#if Deficiency含む場合
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
                DeficiencyControl.DcLog.WriteLog(ex, "Thread Unhandled Exception");
            }
            else
            {
                DeficiencyControl.DcLog.WriteLog(e.ExceptionObject, "Thread Unhandled Exception");
            }

        }

        /// <summary>
        /// スレッド異常を一括取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void AppThException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            DeficiencyControl.DcLog.WriteLog(e.Exception, "Application ThreadException");
        }
#endif
    }
}
