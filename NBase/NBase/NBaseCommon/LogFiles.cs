using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Configuration;


namespace NBaseCommon
{
    public class LogFile
    {

        public static int 同期 = 0;
        public static int 文書 = 1;

        private static string GetLogFilePath()
        {
            string logFilePath = "";
            try
            {
                string basePath = System.Configuration.ConfigurationManager.AppSettings["NBaseServiceLogPath"];
                if (Directory.Exists(basePath) == false)
                {
                    Directory.CreateDirectory(basePath);
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

        public static void Write( string userName, string message )
        {
            try
            {
                // ファイルに書き込む
                string logFilePath = GetLogFilePath();
                if (logFilePath.Length > 0)
                {
                    StreamWriter logFileSw = new StreamWriter(new FileStream(logFilePath, FileMode.Append));

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


        private static string GetLocalLogFilePath()
        {
            string logFilePath = "";
            try
            {
                string basePath = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (Directory.Exists(basePath) == false)
                {
                    Directory.CreateDirectory(basePath);
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

        public static void LocalLogWrite(string userName, string message)
        {
            try
            {
                // ファイルに書き込む
                string logFilePath = GetLocalLogFilePath();
                if (logFilePath.Length > 0)
                {
                    StreamWriter logFileSw = new StreamWriter(new FileStream(logFilePath, FileMode.Append));

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







        private static string GetNBaseHonsenServiceLogFilePath()
        {
            string logFilePath = "";
            try
            {
                string basePath = System.Configuration.ConfigurationManager.AppSettings["NBaseHonsenServiceLogPath"];
                if (Directory.Exists(basePath) == false)
                {
                    Directory.CreateDirectory(basePath);
                }

                // 古いファイルを削除する(1週間分残す)
                DeleteByDays(basePath, 7, 0, "yyyyMMdd");

                //　当日の日付をファイル名にする
                string fileName = DateTime.Today.Year + DateTime.Today.Month.ToString("00") + DateTime.Today.Day.ToString("00") + ".log";

                logFilePath = basePath + "\\" + fileName;
            }
            catch
            {
            }
            return logFilePath;
        }


        public static void NBaseHonsenServiceLogWrite(string userName, string message)
        {
            try
            {
                // ファイルに書き込む
                string logFilePath = GetNBaseHonsenServiceLogFilePath();
                if (logFilePath.Length > 0)
                {
                    StreamWriter logFileSw = new StreamWriter(new FileStream(logFilePath, FileMode.Append));

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


        public static void NBaseHonsenLogClear()
        {
            try
            {
                string basePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\NBaseHonsen\logs";
                if (Directory.Exists(basePath) == true)
                {
                    // 古いファイルを削除する(1週間分残す)
                    DeleteByDays(basePath, 7, 0, "yyyyMMdd");
                }
            }
            catch
            {
            }
        }

        private static string GetNBaseHonsenLogFilePath(int flag)
        {
            string logFilePath = "";
            try
            {
                string basePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\NBaseHonsen\logs";
                if (Directory.Exists(basePath) == false)
                {
                    Directory.CreateDirectory(basePath);
                }

                //　当日の日付をファイル名にする
                string fileName = DateTime.Today.Year + DateTime.Today.Month.ToString("00") + DateTime.Today.Day.ToString("00");
                if (flag == 同期)
                    logFilePath = basePath + "\\" + fileName + ".log";
                else
                    logFilePath = basePath + "\\" + fileName + "_doc.log";
            }
            catch
            {
            }
            return logFilePath;
        }


        public static void NBaseHonsenLogWrite(int flag, string userName, string message)
        {
            try
            {
                // ファイルに書き込む
                string logFilePath = GetNBaseHonsenLogFilePath(flag);
                if (logFilePath.Length > 0)
                {
                    StreamWriter logFileSw = new StreamWriter(new FileStream(logFilePath, FileMode.Append));

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







        public static void DeleteByDays(string path, int days, int startPos, string dateForm)
        {
            //var target = DateTime.Today.AddDays(-days);

            //try
            //{
            //    Directory.GetFiles(path)
            //      .Where(f => DateTime.ParseExact(
            //        Path.GetFileName(f).Substring(startPos, dateForm.Length),
            //        dateForm,
            //        System.Globalization.DateTimeFormatInfo.InvariantInfo) < target)
            //      .ToList()
            //      .ForEach(f => File.Delete(f));
            //}
            //catch (Exception ex)
            //{
            //    //Console.WriteLine(ex.Message);

            //}

        }

    }
}
