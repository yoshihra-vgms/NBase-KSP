using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NBaseService
{
    public class MailSendExeHelper
    {
        public static void MakeDirections(string tos, string ccs, string header, string body, string attachFileName, string attachFilePath)
        {
            string filePath = "";
            try
            {
                // メール送信指示情報
                filePath = System.Configuration.ConfigurationManager.AppSettings["DirectionsFilePath"];
                if (Directory.Exists(filePath) == false)
                {
                    Directory.CreateDirectory(filePath);
                }
                filePath += @"\Directions.txt";
            }
            catch
            {
                return;
            }

            StreamWriter fileSw = new StreamWriter(new FileStream(filePath, FileMode.Create));
            fileSw.Write("[Tos]=" + tos);
            fileSw.Write("[Ccs]=" + ccs);
            fileSw.Write("[Header]=" + header);
            fileSw.Write("[Body]=" + body);
            fileSw.Write("[AttachFileName]=" + attachFileName);
            fileSw.Write("[AttachFilePath]=" + attachFilePath);
            fileSw.Close();

        }

        public static int Excecute()
        {
            int ret = -1;
            try
            {
                NBaseCommon.LogFile.Write("", "Excecute");

                string filePath = System.Configuration.ConfigurationManager.AppSettings["DirectionsFilePath"];
                string exePath = System.Configuration.ConfigurationManager.AppSettings["MailSendExePath"];
                if (exePath != null && exePath.Length > 0)
                {
                    var proc = new System.Diagnostics.Process();

                    proc.StartInfo.FileName = exePath;
                    proc.StartInfo.Arguments = filePath;
                    proc.StartInfo.CreateNoWindow = true; // コンソール・ウィンドウを開かない
                    proc.StartInfo.UseShellExecute = false; // シェル機能を使用しない
                    proc.Start();

                    proc.WaitForExit();

                    ret = proc.ExitCode;
                }
            }
            catch (Exception ex)
            {
                NBaseCommon.LogFile.Write("", $"ExcecuteException:{ex.Message}");
                ret = -1;
            }
            return ret;
        }
    }
}