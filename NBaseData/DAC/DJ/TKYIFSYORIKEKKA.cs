using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ORMapping.Atts;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using NBaseData.DAC;
using NBaseData.DS;

namespace NBaseData.DAC
{
    /// <summary>
    /// 動静基幹連携：処理結果テーブル
    /// </summary>
    [DataContract()]
    [TableAttribute("TKYIFSYORIKEKKA")]
    public class TKYIFSYORIKEKKA
    {
        #region データメンバ
        /// <summary>
        /// 処理番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("OPE_NO")]
        public decimal OpeNo { get; set; }

        /// <summary>
        /// 処理開始日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("START_DATE")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 処理終了日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("END_DATE")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 処理実行ユーザ
        /// </summary>
        [DataMember]
        [ColumnAttribute("TNTSY_CD")]
        public string TntsyCD { get; set; }

        /// <summary>
        /// 実行処理
        /// </summary>
        [DataMember]
        [ColumnAttribute("SYORI")]
        public string Syori { get; set; }

        /// <summary>
        /// 処理結果
        /// </summary>
        [DataMember]
        [ColumnAttribute("STATUS")]
        public string Status { get; set; }

        /// <summary>
        /// 処理結果リストファイル（正常分）
        /// </summary>
        [DataMember]
        [ColumnAttribute("TRKM_OK_LISTFILE")]
        public string TrkmOKListFile { get; set; }


        /// <summary>
        /// 処理結果リストファイル（エラー分）
        /// </summary>
        [DataMember]
        [ColumnAttribute("TRKM_NG_LISTFILE")]
        public string TrkmErrListFIle { get; set; }

        #endregion

    }
}
