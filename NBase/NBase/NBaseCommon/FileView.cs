using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;


namespace NBaseCommon
{
    public class FileView
    {
        // システムで扱える最大ファイル名長
        private const int FILE_NAME_MAX_LENGTH = 100;


        private static string CreateBaseFolderPath()
        {
            //// 1. 実行中のメイン・アセンブリのフル・パスを取得する
            //Assembly asm = Assembly.GetEntryAssembly();
            //string fullPath = asm.Location;

            //// 2. フル・パスからディレクトリ・パス部分を抽出する
            //string dirPath = Path.GetDirectoryName(fullPath);

            // 一時フォルダを取得する
            // 環境変数： TMP → TEMP → USERPROFILE の順に調べて、最初に見つかった環境変数を使用する
            string dirPath = System.IO.Path.GetTempPath();

            // 3. 表示対象ファイルのベースフォルダ
            //string baseFilePath = dirPath + "\\viewFiles";
            string baseFilePath = dirPath + "viewFiles";
            if (Directory.Exists(baseFilePath) == false)
            {
                Directory.CreateDirectory(baseFilePath);
            }

            return baseFilePath;
        }

        public static void Remove()
        {
            // 表示対象ファイルのベースフォルダ
            string basePath = CreateBaseFolderPath();

            // 本日のフォルダ
            string todaysFolder = basePath + "\\" + DateTime.Today.Year + DateTime.Today.Month.ToString("00") + DateTime.Today.Day.ToString("00");

            // サブフォルダを取得
            List<string> deleteFolders = new List<string>();
            foreach (string dirName in Directory.GetDirectories(basePath))
            {
                if (dirName != todaysFolder)
                {
                    deleteFolders.Add(dirName);
                }
            }

            // 本日以外のサブフォルダを削除する
            foreach (string dirName in deleteFolders)
            {
                Directory.Delete(dirName, true);
            }        
        }

        public static bool CheckFileNameLength(string path)
        {
            bool ret = false;

            string fileName = Path.GetFileName(path);

            if (fileName.Length <= FILE_NAME_MAX_LENGTH)
            {
                ret = true;
            }
            return ret;
        }

        public static void View( string id, string fileName, byte[] fileData )
        {
            int counterMax = 10;

            try
            {
                // 表示対象ファイルのベースフォルダ ＋　当日の日付
                string basePath = CreateBaseFolderPath();
                string viewFilePath = basePath + "\\" + DateTime.Today.Year + DateTime.Today.Month.ToString("00") + DateTime.Today.Day.ToString("00");
                if (Directory.Exists(viewFilePath) == false)
                {
                    Directory.CreateDirectory(viewFilePath);
                }

                //　時分秒ミリ秒をファイル名に付加する
                string mmSecond = DateTime.Now.ToString("HHmmssfff");
                viewFilePath += "\\" + mmSecond + "_" + fileName;


                // ＤＢデータをファイルに書き込む
                FileStream filest = new FileStream(viewFilePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                filest.Write(fileData, 0, fileData.Length);
                filest.Close();

                // ファイルのアクセス確認
                // 最大１０秒待つ
                int counter = 0;
                while (true)
                {
                    if (File.Exists(viewFilePath) == true)
                    {
                        break;
                    }
                    Thread.Sleep(1000); // １秒停止
                    counter++;
                    if (counter == counterMax)
                    {
                        break;
                    }
                }

                // ファイルを関連アプリで開く
                Process myProcess = new Process();
                myProcess.StartInfo.FileName = viewFilePath;
                myProcess.Start();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static string CreateFile(string id, string fileName, byte[] fileData)
        {
            int counterMax = 10;
            string viewFilePath = "";
            try
            {
                // 表示対象ファイルのベースフォルダ ＋　当日の日付
                string basePath = CreateBaseFolderPath();
                viewFilePath = basePath + "\\" + DateTime.Today.Year + DateTime.Today.Month.ToString("00") + DateTime.Today.Day.ToString("00");
                if (Directory.Exists(viewFilePath) == false)
                {
                    Directory.CreateDirectory(viewFilePath);
                }

                //　時分秒ミリ秒をファイル名に付加する
                string mmSecond = DateTime.Now.ToString("HHmmssfff");
                viewFilePath += "\\" + mmSecond + "_" + fileName;


                // ＤＢデータをファイルに書き込む
                FileStream filest = new FileStream(viewFilePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                filest.Write(fileData, 0, fileData.Length);
                filest.Close();

                // ファイルのアクセス確認
                // 最大１０秒待つ
                int counter = 0;
                while (true)
                {
                    if (File.Exists(viewFilePath) == true)
                    {
                        break;
                    }
                    Thread.Sleep(1000); // １秒停止
                    counter++;
                    if (counter == counterMax)
                    {
                        break;
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return viewFilePath;
        }
    }
}
