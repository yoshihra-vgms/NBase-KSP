using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CIsl.Files
{
    /// <summary>
    /// CSVファイル処理基底クラス
    /// </summary>
    [Serializable]
    public abstract class BaseCsvFile : BaseFile
    {
        /// <summary>
        /// 区切り文字列定義
        /// </summary>
        public const char DevChar = ',';


        /// <summary>
        /// Csvファイル読み込み 失敗=NULL
        /// </summary>
        /// <param name="filename">読み込みファイル名</param>
        /// <returns>作成物</returns>
        protected List<string[]> ReadCsv(string filename)
        {
            List<string[]> anslist = new List<string[]>();

            try
            {
                //ファイルを読み込み、
                using (FileStream fp = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fp))
                    {
                        //全行読み込み
                        while (sr.EndOfStream != true)
                        {
                            string sline = sr.ReadLine();

                            //ADD
                            string[] ss = sline.Split(DevChar);
                            anslist.Add(ss);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //Log.WriteLog(e);
                return null;
            }

            this.FileName = filename;

            return anslist;
        }

        /// <summary>
        /// CSVファイル書き込み
        /// </summary>
        /// <param name="filename">書き込みファイル名</param>
        /// <param name="datalist">書き込みデータ</param>
        /// <returns>成功可否</returns>
        protected bool WriteCsv(string filename, List<List<string>> datalist)
        {
            try
            {
                //ファイルOpen
                using (FileStream fp = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite))
                {

                    using (StreamWriter sw = new StreamWriter(fp))
                    {
                        //データを書き込み
                        foreach (List<string> slist in datalist)
                        {
                            string sline = "";
                            foreach (string s in slist)
                            {
                                sline += s;
                                sline += DevChar;
                            }
                            //最後のコンマを消す
                            sline = sline.Remove(sline.Length - 1);

                            //書き込み
                            sw.WriteLine(sline);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //Log.WriteLog(e);
                return false;
            }

            this.FileName = filename;

            return true;
        }

    }
}
