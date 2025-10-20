using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Drawing;
using System.Windows.Forms;


using DcCommon.DB;
using DcCommon.DB.DAC;
using DeficiencyControl.Util;
using DeficiencyControl.Logic;
using CIsl.DB.WingDAC;
using CIsl.DB;
using System.Reflection;

namespace DeficiencyControl
{   


    /// <summary>
    /// 使用する色定義
    /// </summary>
    public class DeficiencyControlColor
    {
        /// <summary>
        /// エラー時の色
        /// </summary>
        public static Color EColor = Color.Pink;

        /// <summary>
        /// SingleLineCombo背景色
        /// </summary>
        public static Color SingleLineColorCombo = Color.FromArgb(150, 255, 255);


        /// <summary>
        /// DeficiencyBlueMode色
        /// </summary>
        public static Color DeficiencyControlBlueModeColor = Color.LightSkyBlue;
    }

    /// <summary>
    /// 全体使用グローバルクラス
    /// </summary>
    public class DcGlobal
    {
        private DcGlobal()
        {
        }

        #region シングルトン実装
        /// <summary>
        /// 実体
        /// </summary>
        private static DcGlobal Instance = null;

        /// <summary>
        /// 取得
        /// </summary>
        public static DcGlobal Global
        {
            get
            {
                if (DcGlobal.Instance == null)
                {
                    DcGlobal.Instance = new DcGlobal();
                    DcGlobal.Instance.Init();
                }
                return DcGlobal.Instance;
            }

        }
        #endregion


        #region 定数
        

        #endregion

        #region メンバ変数

        /// <summary>
        /// 環境設定
        /// </summary>
        public DcEnv Env = new DcEnv();

        /// <summary>
        /// ログインユーザー情報
        /// </summary>
        public UserData LoginUser = null;
        

        /// <summary>
        /// DBデータ
        /// </summary>
        public DBDataCache DBCache = null;

        #endregion


        /// <summary>
        /// ログインユーザーのIDを取得
        /// </summary>
        public string LoginUserID
        {
            get
            {
                return this.LoginUser.User.ms_user_id;
            }
        }

        /// <summary>
        /// ログインユーザーを取得
        /// </summary>
        public MsUser LoginMsUser
        {
            get
            {
                return this.LoginUser.User;
            }
        }

        /// <summary>
        /// コントロールカラーの現在値
        /// </summary>
        private static int ControlColorIndex = 0;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 初期化
        /// </summary>
        private void Init()
        {
               

        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// アイコンをbyte配列に変換する
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static byte[] IconToByteArray(Icon c)
        {
            byte[] ans = null;

            try
            {

                using (MemoryStream ms = new MemoryStream())
                {
                    //Icon情報をそのまま保存すると情報が消えるので一回bitmapにしてから保存する
                    using (Bitmap bit = new Bitmap(c.Size.Width, c.Size.Height))
                    {
                        using (Graphics g = Graphics.FromImage(bit))
                        {
                            g.DrawIcon(c, 0, 0);
                        }

                        bit.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    }

                    ans = ms.ToArray();
                }
            }
            catch(Exception e)
            {
                throw e;
            }

            return ans;
        }

        /// <summary>
        /// ファイルを読み込み、バイト配列に変換する
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static byte[] FileToByteArray(string filename)
        {
            byte[] ans = null;

            try
            {
                //全読み込み
                ans = File.ReadAllBytes(filename);

                ////開く
                //using (FileStream fp = new FileStream(filename, FileMode.Open))
                //{
                //    //ファイルサイズ分の領域確保
                //    ans = new byte[fp.Length];

                //    //読み込み
                //    fp.Read(ans, 0, ans.Length);

                //    fp.Close();
                //}
            }
            catch (Exception e)
            {
                throw new Exception("FileToByteArray", e);
            }


            return ans;
        }

        /// <summary>
        /// バイト配列書き込み
        /// </summary>
        /// <param name="path">保存パス</param>
        /// <param name="data">書き込みデータ</param>
        /// <returns></returns>
        public static bool ByteArrayToFile(string path, byte[] data)
        {
            try
            {
                //Create Open
                using (FileStream fp = new FileStream(path, FileMode.Create))
                {
                    //書き込み
                    fp.Write(data, 0, data.Length);
                    fp.Close();
                }
            }
            catch (Exception e)
            {                
                //return false;
                throw new Exception("ByteArrayToFile失敗 path=" + path, e);
            }

            return true;
        }


        /// <summary>
        /// 対象ファイルをデフォルトのアプリで開く
        /// </summary>
        /// <param name="filename">開くファイル</param>
        /// <returns></returns>
        public static bool ExecuteDefaultApplication(string filename)
        {
            //プロセス作成            
            System.Diagnostics.Process exepro = new System.Diagnostics.Process();
            exepro.StartInfo.FileName = filename;

            //実行
            bool ret = false;
            try
            {
                ret = exepro.Start();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// DateTimeの文字列変換を統一的に行うためのもの
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateTimeToString(DateTime date)
        {
            if (date == BaseDac.EDate)
            {
                return "";
            }

            string ans = date.ToString("yyyy/MM/dd");
            return ans;
        }



        /// <summary>
        /// コントロールを一覧で表示するときの色を取得する
        /// </summary>        
        /// <returns></returns>
        public static Color GetControlListColor()
        {
            //デフォルト
            Color ans = SystemColors.Control;


            int index = ControlColorIndex;
            
            //各種色を定義
            Color[] colvec = {
                              Color.FromArgb(255, 230, 230),
                              Color.FromArgb(230, 255, 230),
                              Color.FromArgb(230, 230, 255),

                              Color.FromArgb(255, 255, 230),
                              Color.FromArgb(255, 230, 255),
                              Color.FromArgb(230, 255, 255),

                              Color.FromArgb(255, 255, 255),

                          };

            //指定位置の色を計算
            int cpos = index % colvec.Length;
            ans = colvec[cpos];

            
            ControlColorIndex++;

            return ans;
        }


        /// <summary>
        /// 対象のコントロールのダブルバッファを有効にする
        /// </summary>
        /// <param name="control"></param>
        public static void EnableDoubleBuffering(Control control)
        {
            control.GetType().InvokeMember(
               "DoubleBuffered",
               BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
               null,
               control,
               new object[] { true });
        }

    }
}
