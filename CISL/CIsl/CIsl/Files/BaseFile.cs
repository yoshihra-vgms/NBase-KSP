using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace CIsl.Files
{
    /// <summary>
    /// ファイル基礎クラス
    /// </summary>
    [Serializable]
    public abstract class BaseFile
    {
        /// <summary>
        /// 自分の管理しているファイル名
        /// </summary>
        public string FileName = "";

        public abstract bool ReadFile(string filename);
        public abstract bool WriteFile(string filename);


        /// <summary>
        /// ファイルが開けるかを確認する。
        /// </summary>
        /// <param name="filename">確認ファイル名</param>
        /// <param name="fmode">モード</param>
        /// <param name="fac">モード</param>
        /// <returns></returns>
        protected bool CheckOpen(string filename, FileMode fmode = FileMode.Create, FileAccess fac = FileAccess.ReadWrite)
        {
            try
            {
                //ファイルOpen
                using (FileStream fp = new FileStream(filename, fmode, fac))
                {
                    using (StreamWriter sw = new StreamWriter(fp))
                    {

                    }
                }
            }
            catch (Exception e)
            {

                throw new Exception("WriteText失敗", e);
            }

            return true;
        }


        /// <summary>
        /// テキストファイルの読み込
        /// </summary>
        /// <param name="filename">読み込みファイル名</param>
        /// <param name="enc">エンコーディング</param>
        /// <returns>読み込みデータ　失敗null</returns>
        protected List<string> ReadText(string filename, Encoding enc)
        {
            List<string> anslist = new List<string>();

            try
            {
                //ファイルOpen
                using (FileStream fp = new FileStream(filename, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fp, enc))
                    {
                        //最後まで読み込み
                        while (sr.EndOfStream == false)
                        {
                            string s = sr.ReadLine();
                            anslist.Add(s);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception("ファイル読み込み失敗", e);
            }

            return anslist;
        }


        /// <summary>
        /// テキストファイルの書き込み 
        /// </summary>
        /// <param name="filename">書き込みファイル名</param>
        /// <param name="datalist">書き込みデータ</param>
        /// <param name="enc">エンコーディング</param>
        /// <param name="fmode">FileMode</param>
        /// <param name="fac">アクセスモード</param>
        /// <returns></returns>
        protected bool WriteText(string filename, List<string> datalist, Encoding enc, FileMode fmode = FileMode.Create, FileAccess fac = FileAccess.ReadWrite)
        {
            try
            {
                //ファイルOpen
                using (FileStream fp = new FileStream(filename, fmode, fac))
                {
                    using (StreamWriter sw = new StreamWriter(fp, enc))
                    {
                        //データを書き込み
                        foreach (string s in datalist)
                        {
                            //書き込み
                            string sline = s;
                            sw.WriteLine(sline);
                        }
                    }
                }
            }
            catch (Exception e)
            {

                throw new Exception("WriteText失敗", e);
            }

            //書き込み成功でファイル設定
            this.FileName = filename;

            return true;
        }

    }
}
