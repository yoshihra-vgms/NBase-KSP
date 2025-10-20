using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CIsl.DB.WingDAC;
using DcCommon;
using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;

namespace DcCommon.Output
{


    /// <summary>
    /// 事故トラブル出力種別
    /// </summary>
    public enum EAccidentOutputKind
    {
        船毎 = 0,
        IGT船,
        外航船,
        傭船,

        //-----------------------------------------------
        MAX

    }

    /// <summary>
    /// 事故トラブル出力条件データ
    /// </summary>
    public class AccidentOutputParameter
    {
        /// <summary>
        /// 出力種別
        /// </summary>
        public EAccidentOutputKind OutputKind = EAccidentOutputKind.MAX;

        /// <summary>
        /// 船ごとの場合の対象船データ　nullで全船 OutputKindが船毎のときだけ有効
        /// </summary>
        public decimal? ms_vessel_id = null;

        /// <summary>
        /// 開始年度
        /// </summary>
        public int StartYear;

        /// <summary>
        /// 終了年度
        /// </summary>
        public int EndYear;


    }

    /// <summary>
    /// 事故トラブル帳票出力データ
    /// </summary>
    public class AccidentOutputData
    {
        /// <summary>
        /// 年度
        /// </summary>
        public int Year = 0;

        /// <summary>
        /// 年度開始日？
        /// </summary>
        public DateTime StartDate;
        /// <summary>
        /// 年度終了日
        /// </summary>
        public DateTime EndDate;

        /// <summary>
        ///属するデータ 種別 [kind_of_accident_id, データ]
        /// </summary>
        public Dictionary<int, List<DcAccident>> KindDic = new Dictionary<int, List<DcAccident>>();

        /// <summary>
        /// 属するデータ状況 [accident_situation_id, データ]
        /// </summary>
        public Dictionary<int, List<DcAccident>> SituationDic = new Dictionary<int, List<DcAccident>>();
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
       

    }
}
