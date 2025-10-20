using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace NBaseUtil
{
    public class FileUtils
    {
        public static void SetDesktopFolder(OpenFileDialog dlg)
        {
            dlg.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
        }
        public static void SetDesktopFolder(SaveFileDialog dlg)
        {
            dlg.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
        }
        public static bool SizeCheck(byte[] data, int maxSize)
        {
            if (data.Length > maxSize)
            {
                return false;
            }
            return true;
        }
        public static byte[] ToBytes(string fileName)
        {
            byte[] bytBuffer;
      
            using (FileStream objFileStream = new FileStream(fileName, FileMode.Open))
            {
                long lngFileSize = objFileStream.Length;

                bytBuffer = new byte[(int)lngFileSize];
                objFileStream.Read(bytBuffer, 0, (int)lngFileSize);
                objFileStream.Close();
            }

            return bytBuffer;
        }

        /// <summary>
        /// 出力パスを返す
        /// 出力パスは「パス\\今日の日付」
        /// 今日の日付ディレクトリが無い場合は作成するが、エラーの場合は
        /// 引数のパスをそのまま返す
        /// </summary>
        /// <returns></returns>
        public static string CheckOutPath(string outpath)
        {
            // 昨日までのサブフォルダを削除する
            Remove(outpath);

            string path = outpath;

            DateTime today = DateTime.Today;
            path = path + DateTime.Today.ToString("yyMMdd");

            if (!System.IO.File.Exists(path))
            {
                try
                {
                    System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(path);
                }
                catch (UnauthorizedAccessException e)
                {
                    path = outpath;
                }
            }

            return path;
        }

        public static void Remove(string outpath)
        {
            // 本日のフォルダ
            string todaysFolder = outpath + DateTime.Today.ToString("yyMMdd");

            // サブフォルダを取得
            List<string> deleteFolders = new List<string>();
            foreach (string dirName in Directory.GetDirectories(outpath))
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
    }
}
