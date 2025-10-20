using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Windows.Forms;

using DcCommon.Files;

namespace DeficiencyControl.Files
{
    /// <summary>
    /// メッセージデータ
    /// </summary>
    public class MessageData
    {
        /*
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="id">メッセージID</param>
        /// <param name="s">メッセージ文字列</param>
        /// <param name="icon">アイコン</param>
        public MessageData(EMessageID id, string s, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            this.ID = id;
            this.Message = s;
            this.Icon = icon;
        }*/

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="id">メッセージID</param>
        /// <param name="s">メッセージ文字列</param>
        /// <param name="setno">設定アイコン番号 0=なし 1=エラー(赤×) 2=注意(黄色！) 3=インフォメーション 4=?</param>
        public MessageData(EMessageID id, string s, int setno = 0)
        {
            this.ID = id;
            this.Message = s;

            switch (setno)
            {
                case 0:
                    this.Icon = MessageBoxIcon.None;
                    break;

                case 1:
                    this.Icon = MessageBoxIcon.Error;
                    break;

                case 2:
                    this.Icon = MessageBoxIcon.Warning;
                    break;

                case 3:
                    this.Icon = MessageBoxIcon.Information;
                    break;

                case 4:
                    this.Icon = MessageBoxIcon.Question;
                    break;

                default:
                    this.Icon = MessageBoxIcon.None;
                    break;
            }

        }

        /// <summary>
        /// メッセージID
        /// </summary>
        public EMessageID ID = EMessageID.MI_MAX;

        /// <summary>
        /// メッセージ
        /// </summary>
        public string Message = "";


        /// <summary>
        /// 表示アイコン
        /// </summary>
        public MessageBoxIcon Icon = MessageBoxIcon.None;



        /// <summary>
        /// メッセージ文字列の取得
        /// </summary>            
        /// <returns></returns>
        public string GetMessage()
        {
            string ans = "";

            string mesid = "[" + this.ID.ToString() + "]";
            ans = mesid + " " + this.Message;

            return ans;
        }

    }


    /// <summary>
    /// メッセージファイル管理
    /// </summary>
    public class MessageFile : BaseCsvFile
    {
        /// <summary>
        /// このデータ
        /// </summary>
        public List<MessageData> MesDataList = null;


        /// <summary>
        /// ファイル読み込み
        /// </summary>
        /// <remarks>ID(0～),メッセージ,ボタンID　　であること。</remarks>
        /// <param name="filename">メッセージファイル名</param>
        /// <returns>成功可否</returns>
        public override bool ReadFile(string filename)
        {
            //ファイルを読み込み、this.MesDataListにデータを作成すること。

            //CSV読み込み
            List<string[]> fslist = this.ReadCsv(filename);
            if (fslist == null)
            {
                return false;
            }

            this.MesDataList = new List<MessageData>();

            try
            {
                foreach (string[] svec in fslist)
                {
                    //ID                    
                    EMessageID mid = (EMessageID)Enum.Parse(typeof(EMessageID), svec[0]);

                    //メッセージ
                    string mes = svec[1];

                    //ボタン
                    int bu = Convert.ToInt32(svec[2]);


                    MessageData mdata = new MessageData(mid, mes, bu);
                    this.MesDataList.Add(mdata);
                }
            }
            catch (Exception e)
            {
                return false;
            }



            return true;
        }

        /// <summary>
        /// ファイル書き込み(未実装)
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public override bool WriteFile(string filename)
        {
            return false;
        }
    }
}
