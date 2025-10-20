using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using CIsl.Files;


namespace CIsl
{
    /// <summary>
    /// メッセージID MI_1～
    /// </summary>
    public enum EMessageID
    {
        MI_1 = 1,

        MI_2,
        MI_3,
        MI_4,
        MI_5,
        MI_6,
        MI_7,
        MI_8,
        MI_9,
        MI_10,
        MI_11,
        MI_12,
        MI_13,
        MI_14,
        MI_15,
        MI_16,
        MI_17,
        MI_18,
        MI_19,
        MI_20,
        MI_21,
        MI_22,
        MI_23,
        MI_24,
        MI_25,
        MI_26,
        MI_27,
        MI_28,
        MI_29,
        MI_30,
        MI_31,
        MI_32,
        MI_33,
        MI_34,
        MI_35,
        MI_36,
        MI_37,
        MI_38,
        MI_39,
        MI_40,
        MI_41,
        MI_42,
        MI_43,
        MI_44,
        MI_45,
        MI_46,
        MI_47,
        MI_48,
        MI_49,
        MI_50,
        MI_51,
        MI_52,
        MI_53,
        MI_54,
        MI_55,
        MI_56,
        MI_57,
        MI_58,
        MI_59,
        MI_60,

        MI_61,
        MI_62,
        MI_63,
        MI_64,
        MI_65,
        MI_66,
        MI_67,
        MI_68,
        MI_69,
        MI_70,

        MI_71,
        MI_72,
        MI_73,
        MI_74,
        MI_75,
        MI_76,
        MI_77,
        MI_78,
        MI_79,
        MI_80,

        MI_81,
        MI_82,
        MI_83,
        MI_84,
        MI_85,
        MI_86,
        MI_87,
        MI_88,
        MI_89,
        MI_90,

        MI_91,
        MI_92,
        MI_93,
        MI_94,
        MI_95,
        MI_96,
        MI_97,
        MI_98,
        MI_99,
        MI_100,

        MI_101,
        MI_102,
        MI_103,
        MI_104,
        MI_105,
        MI_106,
        MI_107,
        MI_108,
        MI_109,
        MI_110,

        MI_111,
        MI_112,
        MI_113,
        MI_114,
        MI_115,
        MI_116,
        MI_117,
        MI_118,
        MI_119,
        MI_120,

        MI_121,
        MI_122,
        MI_123,
        MI_124,
        MI_125,
        MI_126,
        MI_127,
        MI_128,
        MI_129,
        MI_130,

        MI_131,
        MI_132,
        MI_133,
        MI_134,
        MI_135,
        MI_136,
        MI_137,
        MI_138,
        MI_139,
        MI_140,

        MI_141,
        MI_142,
        MI_143,
        MI_144,
        MI_145,
        MI_146,
        MI_147,
        MI_148,
        MI_149,
        MI_150,

        MI_151,
        MI_152,
        MI_153,
        MI_154,
        MI_155,

        MI_156,
        MI_157,
        MI_158,
        MI_159,
        MI_160,

        MI_161,
        MI_162,
        MI_163,
        MI_164,
        MI_165,
        MI_166,
        MI_167,
        MI_168,
        MI_169,
        MI_170,

        MI_171,
        MI_172,
        MI_173,
        MI_174,
        MI_175,
        MI_176,
        MI_177,
        MI_178,
        MI_179,
        MI_180,

        MI_181,
        MI_182,
        MI_183,
        MI_184,
        MI_185,
        MI_186,
        MI_187,
        MI_188,
        MI_189,
        MI_190,

        MI_191,
        MI_192,
        MI_193,
        MI_194,
        MI_195,
        MI_196,
        MI_197,
        MI_198,
        MI_199,
        MI_200,



        //--------------------
        MI_MAX,
    };

    /// <summary>
    /// メッセージ管理クラス
    /// </summary>
    public class CIMes
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CIMes()
        {
        }

        #region シングルトン実装

        /// <summary>
        /// 実体
        /// </summary>
        private static CIMes Instance = null;

        /// <summary>
        /// 取得プロパティ
        /// </summary>
        public static CIMes Mes
        {
            get
            {
                if (CIMes.Instance == null)
                {
                    CIMes.Instance = new CIMes();
                }

                return CIMes.Instance;
            }
        }

        #endregion


        #region 定義

        /// <summary>
        /// メッセージがないときの文字列
        /// </summary>
        private const string NoMessageString = "no message";

        /// <summary>
        /// メッセージのタイトル
        /// </summary>
        private const string MessageCaption = "Error Message";

        #endregion

        /// <summary>
        /// メッセージ一覧
        /// </summary>
        private Dictionary<EMessageID, MessageData> MesDic = new Dictionary<EMessageID, MessageData>();



        /// <summary>
        /// デフォルトデータ作成
        /// </summary>
        /// <returns>成功可否</returns>
        private bool CreateDefault()
        {
            this.MesDic = new Dictionary<EMessageID, MessageData>();

            #region メッセージデータ
            MessageData[] initdata = {                                         
                                        new MessageData(EMessageID.MI_1,	"",	1),
                                        new MessageData(EMessageID.MI_2,	"",	1),
                                        new MessageData(EMessageID.MI_3,	"",	1),
                                        new MessageData(EMessageID.MI_4,	"",	1),
                                        new MessageData(EMessageID.MI_5,	"",	1),
                                        new MessageData(EMessageID.MI_6,	"",	1),
                                        new MessageData(EMessageID.MI_7,	"",	1),
                                        new MessageData(EMessageID.MI_8,	"",	1),
                                        new MessageData(EMessageID.MI_9,	"",	1),
                                        new MessageData(EMessageID.MI_10,	"",	1),
                                        new MessageData(EMessageID.MI_11,	"",	1),
                                        new MessageData(EMessageID.MI_12,	"",	1),
                                        new MessageData(EMessageID.MI_13,	"",	1),
                                        new MessageData(EMessageID.MI_14,	"",	1),
                                        new MessageData(EMessageID.MI_15,	"",	1),
                                        new MessageData(EMessageID.MI_16,	"",	1),
                                        new MessageData(EMessageID.MI_17,	"",	1),
                                        new MessageData(EMessageID.MI_18,	"",	1),
                                        new MessageData(EMessageID.MI_19,	"",	1),
                                        new MessageData(EMessageID.MI_20,	"",	1),
                                        new MessageData(EMessageID.MI_21,	"",	1),
                                        new MessageData(EMessageID.MI_22,	"",	1),
                                        new MessageData(EMessageID.MI_23,	"",	1),
                                        new MessageData(EMessageID.MI_24,	"",	1),
                                        new MessageData(EMessageID.MI_25,	"",	1),
                                        new MessageData(EMessageID.MI_26,	"",	1),
                                        new MessageData(EMessageID.MI_27,	"",	1),
                                        new MessageData(EMessageID.MI_28,	"",	1),
                                        new MessageData(EMessageID.MI_29,	"",	1),
                                        new MessageData(EMessageID.MI_30,	"",	1),
                                        new MessageData(EMessageID.MI_31,	"",	1),
                                        new MessageData(EMessageID.MI_32,	"",	1),
                                        new MessageData(EMessageID.MI_33,	"",	1),
                                        new MessageData(EMessageID.MI_34,	"",	1),
                                        new MessageData(EMessageID.MI_35,	"",	1),
                                        new MessageData(EMessageID.MI_36,	"",	1),
                                        new MessageData(EMessageID.MI_37,	"",	1),
                                        new MessageData(EMessageID.MI_38,	"",	1),
                                        new MessageData(EMessageID.MI_39,	"",	1),
                                        new MessageData(EMessageID.MI_40,	"",	1),
                                        new MessageData(EMessageID.MI_41,	"",	1),
                                        new MessageData(EMessageID.MI_42,	"",	1),
                                        new MessageData(EMessageID.MI_43,	"",	1),
                                        new MessageData(EMessageID.MI_44,	"",	1),
                                        new MessageData(EMessageID.MI_45,	"",	1),
                                        new MessageData(EMessageID.MI_46,	"",	1),
                                        new MessageData(EMessageID.MI_47,	"",	1),
                                        new MessageData(EMessageID.MI_48,	"",	1),
                                        new MessageData(EMessageID.MI_49,	"",	1),
                                        new MessageData(EMessageID.MI_50,	"",	1),
                                        new MessageData(EMessageID.MI_51,	"",	1),
                                        new MessageData(EMessageID.MI_52,	"",	1),
                                        new MessageData(EMessageID.MI_53,	"",	1),
                                        new MessageData(EMessageID.MI_54,	"",	1),
                                        new MessageData(EMessageID.MI_55,	"",	1),
                                        new MessageData(EMessageID.MI_56,	"",	1),
                                        new MessageData(EMessageID.MI_57,	"",	1),
                                        new MessageData(EMessageID.MI_58,	"",	1),
                                        new MessageData(EMessageID.MI_59,	"",	1),
                                        new MessageData(EMessageID.MI_60,	"",	1),
                                        new MessageData(EMessageID.MI_61,	"",	1),
                                        new MessageData(EMessageID.MI_62,	"",	1),
                                        new MessageData(EMessageID.MI_63,	"",	1),
                                        new MessageData(EMessageID.MI_64,	"",	1),
                                        new MessageData(EMessageID.MI_65,	"",	1),
                                        new MessageData(EMessageID.MI_66,	"",	1),
                                        new MessageData(EMessageID.MI_67,	"",	1),
                                        new MessageData(EMessageID.MI_68,	"",	1),
                                        new MessageData(EMessageID.MI_69,	"",	1),
                                        new MessageData(EMessageID.MI_70,	"",	1),
                                        new MessageData(EMessageID.MI_71,	"",	1),
                                        new MessageData(EMessageID.MI_72,	"",	1),
                                        new MessageData(EMessageID.MI_73,	"",	1),
                                        new MessageData(EMessageID.MI_74,	"",	1),
                                        new MessageData(EMessageID.MI_75,	"",	1),
                                        new MessageData(EMessageID.MI_76,	"",	1),
                                        new MessageData(EMessageID.MI_77,	"",	1),
                                        new MessageData(EMessageID.MI_78,	"",	1),
                                        new MessageData(EMessageID.MI_79,	"",	1),
                                        new MessageData(EMessageID.MI_80,	"",	1),
                                        new MessageData(EMessageID.MI_81,	"",	1),
                                        new MessageData(EMessageID.MI_82,	"",	1),
                                        new MessageData(EMessageID.MI_83,	"",	1),
                                        new MessageData(EMessageID.MI_84,	"",	1),
                                        new MessageData(EMessageID.MI_85,	"",	1),
                                        new MessageData(EMessageID.MI_86,	"",	1),
                                        new MessageData(EMessageID.MI_87,	"",	1),
                                        new MessageData(EMessageID.MI_88,	"",	1),
                                        new MessageData(EMessageID.MI_89,	"",	1),
                                        new MessageData(EMessageID.MI_90,	"",	1),
                                        new MessageData(EMessageID.MI_91,	"",	1),
                                        new MessageData(EMessageID.MI_92,	"",	1),
                                        new MessageData(EMessageID.MI_93,	"",	1),
                                        new MessageData(EMessageID.MI_94,	"",	1),
                                        new MessageData(EMessageID.MI_95,	"",	1),
                                        new MessageData(EMessageID.MI_96,	"",	1),
                                        new MessageData(EMessageID.MI_97,	"",	1),
                                        new MessageData(EMessageID.MI_98,	"",	1),
                                        new MessageData(EMessageID.MI_99,	"",	1),
                                        new MessageData(EMessageID.MI_100,	"",	1),
                                        new MessageData(EMessageID.MI_101,	"",	1),
                                        new MessageData(EMessageID.MI_102,	"",	1),
                                        new MessageData(EMessageID.MI_103,	"",	1),
                                        new MessageData(EMessageID.MI_104,	"",	1),
                                        new MessageData(EMessageID.MI_105,	"",	1),
                                        new MessageData(EMessageID.MI_106,	"",	1),
                                        new MessageData(EMessageID.MI_107,	"",	1),
                                        new MessageData(EMessageID.MI_108,	"",	1),
                                        new MessageData(EMessageID.MI_109,	"",	1),
                                        new MessageData(EMessageID.MI_110,	"",	1),
                                        new MessageData(EMessageID.MI_111,	"",	1),
                                        new MessageData(EMessageID.MI_112,	"",	1),
                                        new MessageData(EMessageID.MI_113,	"",	1),
                                        new MessageData(EMessageID.MI_114,	"",	1),
                                        new MessageData(EMessageID.MI_115,	"",	1),
                                        new MessageData(EMessageID.MI_116,	"",	1),
                                        new MessageData(EMessageID.MI_117,	"",	1),
                                        new MessageData(EMessageID.MI_118,	"",	1),
                                        new MessageData(EMessageID.MI_119,	"",	1),
                                        new MessageData(EMessageID.MI_120,	"",	1),
                                        new MessageData(EMessageID.MI_121,	"",	1),
                                        new MessageData(EMessageID.MI_122,	"",	1),
                                        new MessageData(EMessageID.MI_123,	"",	1),
                                        new MessageData(EMessageID.MI_124,	"",	1),
                                        new MessageData(EMessageID.MI_125,	"",	1),
                                        new MessageData(EMessageID.MI_126,	"",	1),
                                        new MessageData(EMessageID.MI_127,	"",	1),
                                        new MessageData(EMessageID.MI_128,	"",	1),
                                        new MessageData(EMessageID.MI_129,	"",	1),
                                        new MessageData(EMessageID.MI_130,	"",	1),
                                        new MessageData(EMessageID.MI_131,	"",	1),
                                        new MessageData(EMessageID.MI_132,	"",	1),
                                        new MessageData(EMessageID.MI_133,	"",	1),
                                        new MessageData(EMessageID.MI_134,	"",	1),
                                        new MessageData(EMessageID.MI_135,	"",	1),
                                        new MessageData(EMessageID.MI_136,	"",	1),
                                        new MessageData(EMessageID.MI_137,	"",	1),
                                        new MessageData(EMessageID.MI_138,	"",	1),
                                        new MessageData(EMessageID.MI_139,	"",	1),
                                        new MessageData(EMessageID.MI_140,	"",	1),
                                        new MessageData(EMessageID.MI_141,	"",	1),
                                        new MessageData(EMessageID.MI_142,	"",	1),
                                        new MessageData(EMessageID.MI_143,	"",	1),
                                        new MessageData(EMessageID.MI_144,	"",	1),
                                        new MessageData(EMessageID.MI_145,	"",	1),
                                        new MessageData(EMessageID.MI_146,	"",	1),
                                        new MessageData(EMessageID.MI_147,	"",	1),
                                        new MessageData(EMessageID.MI_148,	"",	1),
                                        new MessageData(EMessageID.MI_149,	"",	1),
                                        new MessageData(EMessageID.MI_150,	"",	1),
                                        new MessageData(EMessageID.MI_151,	"",	1),
                                        new MessageData(EMessageID.MI_152,	"",	1),
                                        new MessageData(EMessageID.MI_153,	"",	1),
                                        new MessageData(EMessageID.MI_154,	"",	1),
                                        new MessageData(EMessageID.MI_155,	"",	1),
                                        new MessageData(EMessageID.MI_156,	"",	1),
                                        new MessageData(EMessageID.MI_157,	"",	1),
                                        new MessageData(EMessageID.MI_158,	"",	1),
                                        new MessageData(EMessageID.MI_159,	"",	1),
                                        new MessageData(EMessageID.MI_160,	"",	1),
                                        new MessageData(EMessageID.MI_161,	"",	1),
                                        new MessageData(EMessageID.MI_162,	"",	1),
                                        new MessageData(EMessageID.MI_163,	"",	1),
                                        new MessageData(EMessageID.MI_164,	"",	1),
                                        new MessageData(EMessageID.MI_165,	"",	1),
                                        new MessageData(EMessageID.MI_166,	"",	1),
                                        new MessageData(EMessageID.MI_167,	"",	1),
                                        new MessageData(EMessageID.MI_168,	"",	1),
                                        new MessageData(EMessageID.MI_169,	"",	1),
                                        new MessageData(EMessageID.MI_170,	"",	1),
                                        new MessageData(EMessageID.MI_171,	"",	1),
                                        new MessageData(EMessageID.MI_172,	"",	1),
                                        new MessageData(EMessageID.MI_173,	"",	1),
                                        new MessageData(EMessageID.MI_174,	"",	1),
                                        new MessageData(EMessageID.MI_175,	"",	1),
                                        new MessageData(EMessageID.MI_176,	"",	1),
                                        new MessageData(EMessageID.MI_177,	"",	1),
                                        new MessageData(EMessageID.MI_178,	"",	1),
                                        new MessageData(EMessageID.MI_179,	"",	1),
                                        new MessageData(EMessageID.MI_180,	"",	1),
                                        new MessageData(EMessageID.MI_181,	"",	1),
                                        new MessageData(EMessageID.MI_182,	"",	1),
                                        new MessageData(EMessageID.MI_183,	"",	1),
                                        new MessageData(EMessageID.MI_184,	"",	1),
                                        new MessageData(EMessageID.MI_185,	"",	1),
                                        new MessageData(EMessageID.MI_186,	"",	1),
                                        new MessageData(EMessageID.MI_187,	"",	1),
                                        new MessageData(EMessageID.MI_188,	"",	1),
                                        new MessageData(EMessageID.MI_189,	"",	1),
                                        new MessageData(EMessageID.MI_190,	"",	1),
                                        new MessageData(EMessageID.MI_191,	"",	1),
                                        new MessageData(EMessageID.MI_192,	"",	1),
                                        new MessageData(EMessageID.MI_193,	"",	1),
                                        new MessageData(EMessageID.MI_194,	"",	1),
                                        new MessageData(EMessageID.MI_195,	"",	1),
                                        new MessageData(EMessageID.MI_196,	"",	1),
                                        new MessageData(EMessageID.MI_197,	"",	1),
                                        new MessageData(EMessageID.MI_198,	"",	1),
                                        new MessageData(EMessageID.MI_199,	"",	1),
                                        new MessageData(EMessageID.MI_200,	"",	1),

                                 };

            #endregion

            //ADD
            foreach (MessageData data in initdata)
            {
                this.MesDic.Add(data.ID, data);
            }

            return true;
        }


        /// <summary>
        /// 初期化関数 から文字指定でデフォルト初期化します
        /// </summary>
        /// <param name="filename">読み込みメッセージファイル</param>
        /// <returns>成功可否</returns>
        public bool Init(string filename = "")
        {
            //ある？
            bool fret = File.Exists(filename);

            //ファイル名がないか、ファイルがないとき
            if (filename.Length <= 0 || fret == false)
            {
                //デフォルト初期化を行う
                //this.CreateDefault();
                return false;
            }
            else
            {
                //必要ならファイル読み込み
                bool ret = this.ReadMessageFile(filename);
                if (ret == false)
                {
                    return false;
                }

            }

            return true;
        }

        /// <summary>
        /// メッセージファイルの読み込み
        /// </summary>
        /// <param name="filename">読み込みファイル名</param>
        /// <returns></returns>
        private bool ReadMessageFile(string filename)
        {
            //ファイル読み込み
            MessageFile mfile = new MessageFile();
            bool ret = mfile.ReadFile(filename);
            if (ret == false)
            {
                return false;
            }

            //データ登録
            this.MesDic = new Dictionary<EMessageID, MessageData>();
            foreach (MessageData mdata in mfile.MesDataList)
            {
                this.MesDic.Add(mdata.ID, mdata);
            }

            return true;
        }


        /// <summary>
        /// 対象のメッセージを表示
        /// </summary>
        /// <param name="pform">所持フォーム(無いならnull)</param>
        /// <param name="id">表示する物体</param>        
        /// <param name="bu">表示ボタン</param>
        /// <returns></returns>
        public static DialogResult ShowMessage(Form pform, EMessageID id, MessageBoxButtons bu = MessageBoxButtons.OK)
        {
            string mes = "";
            DialogResult ret = DialogResult.OK;

            //対象のメッセージ取得
            MessageData mdata = CIMes.Instance.MesDic[id];

            //見つからなかった
            if (mdata == null)
            {
                string emes = "GetMessage [" + id.ToString() + "] 定義されていません。";
                CILog.WriteLog(emes);

                mes = NoMessageString;
                ret = MessageBox.Show(mes, "", bu, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return ret;
            }

            //メッセージ文字列を取得し、表示
            mes = mdata.GetMessage();
            //ret = MessageBox.Show(mes, "", bu, mdata.Icon); 
            //ret = MessageBox.Show(mes, "", bu, mdata.Icon, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);     //最前面表示

            //MessageBoxOptions.DefaultDesktopOnlyを利用すると色が消える。VisualStyleがOFFになるため？
            //最前面フォームを利用する
            using (Form f = new Form())
            {
                //親の値を設定し、親の場所へ表示する。
                if (pform != null)
                {
                    f.StartPosition = FormStartPosition.Manual;
                    f.Location = pform.Location;
                    f.Size = pform.Size;

                }


                f.TopMost = true;
                ret = MessageBox.Show(f, mes, MessageCaption, bu, mdata.Icon);
            }

            //書き込み
            CILog.WriteLog("ShowMessage mes=" + mes + "  ret=" + ret.ToString());


            return ret;
        }

    }
}
