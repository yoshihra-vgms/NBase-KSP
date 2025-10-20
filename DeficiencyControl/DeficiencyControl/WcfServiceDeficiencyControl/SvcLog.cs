using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

using DcCommon.DB.DAC;
using CIsl.DB.WingDAC;
using WcfServiceDeficiencyControl.Logic;

namespace WcfServiceDeficiencyControl
{
    /// <summary>
    /// サービスログ
    /// </summary>
    public class SvcLog
    {
        /// <summary>
        /// Userの出力
        /// </summary>
        /// <param name="user">無いときはnull</param>
        private static void WriteUser(MsUser user)
        {
            string s = "";
            if (user == null)
            {
                s = string.Format("host={0}", OperationHistoryCreator.CreateHostString());
                WriteLogData(s, false);
                return;
            }

            s = string.Format("UserID={0} host={1}", user.ms_user_id, OperationHistoryCreator.CreateHostString());
            WriteLogData(s, false);

        }

        /// <summary>
        /// 例外ログ書き込み
        /// </summary>
        /// <param name="e">Exception</param>
        /// <param name="s">不可情報</param>
        /// <param name="user">追加ユーザー情報、ないときはnull</param>
        /// <returns></returns>
        public static bool WriteLog(Exception e, string s, MsUser user = null)
        {
            WriteLogData("------" + s + " Exception------", true);
            WriteLogData("Message = " + e.Message, false);
            WriteLogData("Source = " + e.Source, false);
            WriteLogData("StackTrace = " + e.StackTrace, false);
            WriteLogData("TargetSite = " + e.TargetSite.Name, false);


            Exception epc = e.InnerException;
            while (epc != null)
            {
                WriteLogData("Innser Message = " + epc.Message, false);
                epc = epc.InnerException;
            }

            WriteUser(user);

            WriteLogData("------------------", false);

            return true;
        }

        /// <summary>
        /// 書き込み
        /// </summary>
        /// <param name="s"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static bool WriteLog(string s, MsUser user = null, bool f = true)
        {
            string str = s;

            if (user != null)
            {
                str = string.Format("UserID={0}:{1}", user.ms_user_id, s);
            }

            WriteLogData(str, f);
            return true;
        }

        /// <summary>
        /// ログファイル名の作成
        /// </summary>
        /// <returns></returns>
        private static string CreateLogFileName()
        {
            string ans = WebConfigManager.LogFilePath;

            string folder = Path.GetDirectoryName(WebConfigManager.LogFilePath);
            string name = Path.GetFileName(WebConfigManager.LogFilePath);

            //ファイル名と拡張子を取得
            string fname = Path.GetFileNameWithoutExtension(name);
            string ext = Path.GetExtension(name);

            //日付文字列作成
            fname += "_" + DateTime.Now.ToString("yyyyMMdd");


            //設定
            ans = folder + "\\" + fname + ext;
            

            return ans;
        }


        /// <summary>
        /// ログを書きこむ
        /// </summary>
        /// <param name="s">書き込む物体</param>        
        /// <param name="f">日付を付加する？ true=出力</param>
        /// <returns>成功可否</returns>
        private static bool WriteLogData(string s, bool f = true)
        {

            //ログ出力する？
            if (WebConfigManager.LogEnable == false)
            {
                return true;
            }
            

            string sw = s;

            //日付を追加する
            if (f == true)
            {
                string sd = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                sw = sd + "  " + s;
            }
            else
            {
                sw = "\t" + s;
            }

            try
            {
                //ログファイル名の取得
                string logname = CreateLogFileName();
                using (StreamWriter fp = new StreamWriter(logname, true))
                {

                    fp.WriteLine(sw);
                    System.Console.Out.WriteLine(sw);
                }

            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine(s + " 書き込み失敗:: mes=" + e.Message);
                return false;
            }

            return true;
        }
    }
}