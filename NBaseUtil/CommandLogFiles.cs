using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Configuration;


namespace NBaseUtil
{
    public class CommandLogFile
    {
        private static string GetLogFilePath()
        {
            string logFilePath = "";
            try
            {
                string basePath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
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

        public static void Write( string message )
        {
            try
            {
                // ファイルに書き込む
                string logFilePath = GetLogFilePath();
                if (logFilePath.Length > 0)
                {
                    StreamWriter logFileSw = new StreamWriter(new FileStream(logFilePath, FileMode.Append));

                    string logMessage = "[" + DateTime.Now.ToLongTimeString() + "]:" + message;

                    logFileSw.WriteLine(logMessage);
                    logFileSw.Close();
                }
            }
            catch
            {
                // Exception発生時は無視
            }
        }
    }
}
