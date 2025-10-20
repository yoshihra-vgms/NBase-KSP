using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using System.IO;
using System.Threading;

namespace NBaseData.BLC
{
    public class 添付ファイル
    {
        public static bool 作成(MsUser loginUser, string odMkId)
        {
            bool ret = true;

            Remove();

            List<OdMkItem> odMkItemList = OdMkItem.GetRecordsByOdMkID(loginUser, odMkId);
            List<OdMkShousaiItem> odMkShousaiList = OdMkShousaiItem.GetRecordsByOdMkID(loginUser, odMkId);

            foreach (OdMkItem i in odMkItemList)
            {
                if (i.CancelFlag == 1)
                    continue;
                if (i.OdAttachFileID == null || i.OdAttachFileID.Length == 0)
                    continue;

                OdAttachFile attachFile = OdAttachFile.GetRecord(loginUser, i.OdAttachFileID);
                if (attachFile != null)
                {
                    CreateAttachFile(attachFile.OdAttachFileID, attachFile.FileName, attachFile.Data);
                }
            }
            foreach (OdMkShousaiItem i in odMkShousaiList)
            {
                if (i.CancelFlag == 1)
                    continue;
                if (i.OdAttachFileID == null || i.OdAttachFileID.Length == 0)
                    continue;

                OdAttachFile attachFile = OdAttachFile.GetRecord(loginUser, i.OdAttachFileID);
                if (attachFile != null)
                {
                    CreateAttachFile(attachFile.OdAttachFileID, attachFile.FileName, attachFile.Data);
                }
            }


            return ret;
        }

        private static string CreateBaseFolderPath()
        {
            string baseFilePath = System.Configuration.ConfigurationManager.AppSettings["VendorAttachFilePath"];
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

        public static string CreateAttachFile(string id, string fileName, byte[] fileData)
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

                //　IDをファイル名に付加する
                viewFilePath += "\\" + id + "_" + fileName;


                // データをファイルに書き込む
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
