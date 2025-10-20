using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DeficiencyControl
{
    /// <summary>
    /// ログ管理クラス
    /// </summary>
    public class DcLog
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private DcLog()
        {
        }

        #region シングルトン実装

        /// <summary>
        /// 実体
        /// </summary>
        private static DcLog Instance = null;

        /// <summary>
        /// 取得プロパティ
        /// </summary>
        public static DcLog Log
        {
            get
            {
                if (DcLog.Instance == null)
                {
                    DcLog.Instance = new DcLog();
                }

                return DcLog.Instance;
            }
        }

        #endregion


        #region メンバ変数

        /// <summary>
        /// ログファイル名
        /// </summary>
        private string LogFileName = "defcon.log";

        /// <summary>
        /// ログ出力可否
        /// </summary>
        private bool LogFlag = false;

        #endregion


        /// <summary>
        /// ログ初期化関数
        /// </summary>
        /// <param name="folder">出力フォルダパス</param>
        /// <param name="name">ログファイル名</param>
        /// <param name="outflag">出力可否</param>
        /// <returns>成功可否</returns>
        public bool Init(string folder, string name, bool outflag)
        {
            //ファイル名と拡張子を取得
            string fname = Path.GetFileNameWithoutExtension(name);
            string ext = Path.GetExtension(name);

            //日付文字列作成
            fname += "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");


            //設定
            this.LogFileName = folder + "\\" + fname + ext;
            this.LogFlag = outflag;

            return true;
        }

        /// <summary>
        /// ログを書きこむ
        /// </summary>
        /// <param name="s">書き込む物体</param>
        /// <param name="f">日付を付加する？ true=出力</param>
        /// <returns>成功可否</returns>
        private bool WriteLogData(string s, bool f = true)
        {

            //ログ出力する？
            if (this.LogFlag == false)
            {
                return true;
            }

            string sw = s;

            //日付を追加する
            if (f == true)
            {
                string sd = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                sw = sd + "  " + s;
            }
            else
            {
                sw = "\t" + s;
            }

            try
            {

                using (StreamWriter fp = new StreamWriter(this.LogFileName, true))
                {

                    fp.WriteLine(sw);

                }

            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine(s + " 書き込み失敗:: mes=" + e.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// ログの書き出し
        /// </summary>
        /// <param name="s">書き込み文字列</param>
        /// <param name="f">日付を付加する？ true=付加する</param>
        /// <returns></returns>
        public static bool WriteLog(string s, bool f = true)
        {
            lock (DcLog.Instance)
            {
                return DcLog.Log.WriteLogData(s, f);
            }
        }

        /// <summary>
        /// エラーログの書き出し
        /// </summary>
        /// <param name="e">文字列</param>
        /// <param name="s">追記する文字列</param>
        /// <returns></returns>
        public static bool WriteLog(Exception e, string s = "")
        {
            lock (DcLog.Instance)
            {
                DcLog.Log.WriteLogData("------" + s + "Exception------", true);
                DcLog.Log.WriteLogData("Message = " + e.Message, false);
                DcLog.Log.WriteLogData("Source = " + e.Source, false);
                DcLog.Log.WriteLogData("StackTrace = " + e.StackTrace, false);
                DcLog.Log.WriteLogData("TargetSite = " + e.TargetSite.Name, false);

                Exception epc = e.InnerException;
                while (epc != null)
                {
                    DcLog.Log.WriteLogData("Innser Message = " + epc.Message, false);
                    epc = epc.InnerException;
                }


                DcLog.Log.WriteLogData("------------------", false);

            }

            return true;
        }

        /// <summary>
        /// ログの書き出し
        /// </summary>
        /// <param name="obj">発生したクラス thisを指定する想定</param>
        /// <param name="s">書き込み文字列</param>
        /// <param name="f">日付を付加する？ true=付加する</param>
        /// <returns></returns>
        public static bool WriteLog(object src, string s, bool f = true)
        {
            lock (DcLog.Instance)
            {
                //string mes = src.ToString() + "::" + s;
                string mes = src.GetType().ToString() + " :: " + s;
                return DcLog.Log.WriteLogData(mes, f);
            }
        }

    }
}
