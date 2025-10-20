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
    /// 検船帳票出力種別 
    /// </summary>
    public enum EMoiExcelOutputKind
    {
        項目別指摘数,
        是正対応リスト,



        //////
        MAX,
    }

    /// <summary>
    /// 検船Excel帳票出力データ
    /// </summary>
    public class MoiExcelOutputParameter
    {
        /// <summary>
        /// 種類
        /// </summary>
        public EMoiExcelOutputKind Kind = EMoiExcelOutputKind.MAX;
             
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
    /// 検船帳票 カテゴリ出力データ
    /// </summary>
    public class MoiExcelOutputDataCategory
    {
        /// <summary>
        /// 開始年度
        /// </summary>
        public int StartYear = 0;

        /// <summary>
        /// 終了年度
        /// </summary>
        public int EndYear = 0;

        /// <summary>
        /// 指摘総数
        /// </summary>
        public int MoiCount = 0;

        /// <summary>
        /// 船総数
        /// </summary>
        public int VesselCount = 0;

        /// <summary>
        /// 帳票出力用 true:VIQ Versionの範囲外シートのため、シート削除
        /// </summary>
        public bool DeleteSheet = false;

        /// <summary>
        /// 平均指摘数 総数/船数
        /// </summary>
        public decimal AverageCount
        {
            get
            {
                decimal c = this.MoiCount;
                decimal v = this.VesselCount;
                if (this.VesselCount == 0)
                {
                    return 0;
                }

                decimal ans = c / v;
                return ans;
            }
        }


        /// <summary>
        /// ﾃﾞｰﾀ [viq_code_name_id, 属するデータ一式]
        /// </summary>
        public Dictionary<int, List<MoiData>> DataDic = new Dictionary<int, List<MoiData>>();
        
    }


    /// <summary>
    /// 検船帳票  一覧データ
    /// </summary>
    public class MoiExcelOutputDataList
    {


    }
}
