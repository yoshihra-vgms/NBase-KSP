using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NBaseCommon
{
    public class LoginFile
    {
        private static string GetLoginFilePath()
        {
            string loginFilePath = "";
            try
            {
                // 指摘事項管理用
                loginFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\NBase";
                if (Directory.Exists(loginFilePath) == false)
                {
                    Directory.CreateDirectory(loginFilePath);
                }
                loginFilePath += @"\login";
            }
            catch
            {
            }
            return loginFilePath;
        }

        public static void Write( string userId, string password )
        {
            try
            {
                // ファイルに書き込む
                string loginFilePath = GetLoginFilePath();
                if (loginFilePath.Length > 0)
                {
                    StreamWriter fileSw = new StreamWriter(new FileStream(loginFilePath, FileMode.Create));
                    fileSw.WriteLine("p1=" + userId);
                    fileSw.WriteLine("p2=" + password);
                    fileSw.Close();
                }
            }
            catch
            {
                // Exception発生時は無視
            }
        }
    }
}
