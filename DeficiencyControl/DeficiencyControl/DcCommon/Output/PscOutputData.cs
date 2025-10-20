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
    /// 事故トラブル出力条件データ
    /// </summary>
    public class PscOutputParameter
    {
        /// <summary>
        /// 対象船データ　nullで全船
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
    /// PSC 帳票データ
    /// </summary>
    public class PscOutputData
    {
        /// <summary>
        /// 集計データ一覧
        /// </summary>
        public List<PscOutputAggregateData> AggregateList = new List<PscOutputAggregateData>();

        
        /// <summary>
        /// 各船ごとの指摘事項まとめ [ms_vessel_id, データ]
        /// </summary>
        public Dictionary<decimal, List<PSCInspectionData>> PSCVesselDic = null;
    }


    /// <summary>
    /// PSC 集計帳票データ
    /// </summary>
    public class PscOutputAggregateData
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
        /// PSC訪船回数
        /// </summary>
        public int PscCount = 0;

        /// <summary>
        /// 指摘事項件数
        /// </summary>
        public int DeficiencyCount = 0;


        /// <summary>
        ///ActionCodeのデータ 種別 [action_code_id, データ]・・・
        /// </summary>
        public Dictionary<int, List<DcActionCodeHistory>> ActionCodeDic = new Dictionary<int, List<DcActionCodeHistory>>();


        /// <summary>
        /// DeficiencyCodeのデータ [deficiency_code_id, データ]
        /// </summary>
        public Dictionary<int, PscOutputDeficiencyAggregateData> DeficiencyCodeDic = new Dictionary<int, PscOutputDeficiencyAggregateData>();
    }


    /// <summary>
    /// Deficency集計データ
    /// </summary>
    public class PscOutputDeficiencyAggregateData
    {
        /// <summary>
        /// これのコード
        /// </summary>
        public MsDeficiencyCode Code = null;


        /// <summary>
        /// 属するデータ
        /// </summary>
        public List<DcCiPscInspection> PscLsit = new List<DcCiPscInspection>();
    }


    
}
