using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace SyncClient
{
    public class FileCopy
    {

        public static void CopyDBFile()
        {
            List<string> sdfFiles = new List<string>();

            // 1. 実行中のメイン・アセンブリのフル・パスを取得する
            Assembly asm = Assembly.GetEntryAssembly();
            string fullPath = asm.Location;

            // 2. フル・パスからディレクトリ・パス部分を抽出する
            string dirPath = Path.GetDirectoryName(fullPath);

            // 3. カレントディレクトリ名を保持する
            string currentDirName = dirPath.Substring(dirPath.LastIndexOf("\\")+1);

            // 4. 親ディレクトリ
            string parentDirPath = dirPath.Replace("\\" + currentDirName, "");


            // 5. 兄弟ディレクトリ
            foreach (string dir in Directory.GetDirectories(parentDirPath))
            {
                // ディレクトリ名のみ取り出す
                string tmpDirName = dir.Substring(dir.LastIndexOf("\\") + 1);

                if (tmpDirName == currentDirName)
                {
                    continue;
                }

                foreach (string file in Directory.GetFiles(dir, "*.sdf"))
                {
                    // 
                    string fileName = Path.GetFileName(file);
                    if (fileName == Common.DB名)
                    {
                        sdfFiles.Add(file);
                    }
                }
            }
            

            // 最新のものを探す
            DateTime d1 = DateTime.MinValue;
            string d1File = "";
            foreach (string file in sdfFiles)
            {
                DateTime d = File.GetLastAccessTime(file);
                if (d > d1)
                {
                    d1 = d;
                    d1File = file;
                }
            }

            if (d1File.Length > 0)
            {
                // ファイルをコピーする
                File.Copy(d1File, dirPath + "\\" + Common.DB名);

                // バックアップ
                string myDocumentPaht = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                File.Copy(d1File, myDocumentPaht + "\\" + Common.DB名 + "_" + DateTime.Now.ToString("yyyyMMddHHmmssFFF"));
            }
        }
    }
}
