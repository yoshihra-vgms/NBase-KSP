using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using DcCommon.DB.DAC;
using DcCommon.DB;
using DeficiencyControl.Logic;
using CIsl.DB.WingDAC;

namespace DeficiencyControl
{
    //いろいろな場所で使用するが、専用ファイルを作るまでもないちょっとしたデータクラスなどを定義する場所。
    //あまり大きいものは分離すること。

    public class Common
    {
        /// <summary>
        /// Excelファイルを選択させるときのFilter
        /// </summary>
        public static string ExcelFileter = "xlsxファイル(*.xlsx)|*.xlsx|すべてのファイル(*.*)|*.*";

        /// <summary>
        /// ExcelをImportするときのフィルター
        /// </summary>
        public static string ExcelImportFilter = "Excelファイル|*.xlsx";
    }
    
    /// <summary>
    /// ファイル表示一覧
    /// </summary>
    public class FileDispData
    {

        /// <summary>
        /// ファイルパス
        /// </summary>
        public string FilePath = "";

        /// <summary>
        /// 表示文字列
        /// </summary>
        public string DispText = "";

        /// <summary>
        /// これがnullなら新規。これがあるなら既存
        /// </summary>
        public DcAttachment Attachment = null;

        /// <summary>
        /// アイコン
        /// </summary>
        public Icon IconData = null;

        public override string ToString()
        {
            return Path.GetFileName(this.FilePath);
        }

    }

    /// <summary>
    /// 添付ファイルサイズが大きすぎた時に発生する例外　これをキャッチしたらメッセージを表示させること。
    /// </summary>
    public class AttachmentFileSizeOverException : Exception
    {
        public AttachmentFileSizeOverException(string mes = "")
            : base(mes)
        {
        }
    }


    /// <summary>
    /// 入力エラー時の例外
    /// </summary>
    public class ControlInputErrorException : Exception
    {
        public ControlInputErrorException(string mes = "")
            : base(mes)
        {
        }
    }

    /// <summary>
    /// DeficiencyCodeの説明文を表示したいときに使うもの
    /// </summary>
    public class DeficiencyCodeText
    {
        public DeficiencyCodeText(MsDeficiencyCode code)
        {
            this.DeficienyCode = code;
        }

        public MsDeficiencyCode DeficienyCode = null;

        public override string ToString()
        {
            return this.DeficienyCode.defective_item;
        }
    }








}

