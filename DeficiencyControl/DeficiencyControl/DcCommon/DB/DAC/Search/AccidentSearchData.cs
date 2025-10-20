using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CIsl.DB;


namespace DcCommon.DB.DAC.Search
{
    /// <summary>
    /// Accident検索データ
    /// </summary>
    public class AccidentSearchData : BaseSearchData
    {
        #region メンバ変数
        /// <summary>
        /// Vessel
        /// </summary>
        public decimal? ms_vessel_id = null;

        /// <summary>
        /// 種類
        /// </summary>
        public int? accident_kind_id = null;

        /// <summary>
        /// Port
        /// </summary>
        public string ms_basho_id = "";

        /// <summary>
        /// 国
        /// </summary>
        public string ms_regional_code = "";


        /// <summary>
        /// PIC
        /// </summary>
        public string ms_user_id = "";


        /// <summary>
        /// 事故分類
        /// </summary>
        public int? kind_of_accident_id = null;

        /// <summary>
        /// 発生状況
        /// </summary>
        public int? accident_situation_id = null;


        /// <summary>
        /// Date開始
        /// </summary>
        public DateTime? date_start = null;
        /// <summary>
        /// Date終了
        /// </summary>
        public DateTime? date_end = null;


        /// <summary>
        /// キーワード
        /// </summary>
        public string SearchKeyword = "";

        #endregion



        /// <summary>
        /// 条件式の作成
        /// </summary>
        /// <param name="plist"></param>
        /// <returns></returns>
        public override string CreateSQLWhere(out List<SqlParamData> plist)
        {
            string ans = "";
            plist = new List<SqlParamData>();


            //vessel
            if (this.ms_vessel_id != null)
            {
                ans += @"
AND
(dc_accident.ms_vessel_id = :ms_vessel_id)
";
                plist.Add(new SqlParamData() { Name = "ms_vessel_id", Value = this.ms_vessel_id });
            }

            //種類
            if (this.accident_kind_id != null)
            {
                ans += @"
AND
(dc_accident.accident_kind_id = :accident_kind_id)
";
                plist.Add(new SqlParamData() { Name = "accident_kind_id", Value = this.accident_kind_id });
            }

            //Port
            if (this.ms_basho_id.Length > 0)
            {
                ans += @"
AND
(dc_accident.ms_basho_id = :ms_basho_id)
";
                plist.Add(new SqlParamData() { Name = "ms_basho_id", Value = this.ms_basho_id });
            }

            //国
            if (this.ms_regional_code.Length > 0)
            {
                ans += @"
AND
(dc_accident.ms_regional_code = :ms_regional_code)
";
                plist.Add(new SqlParamData() { Name = "ms_regional_code", Value = this.ms_regional_code });
            }

            //PIC
            if (this.ms_user_id.Length > 0)
            {
                ans += @"
AND
(dc_accident.ms_user_id = :ms_user_id)
";
                plist.Add(new SqlParamData() { Name = "ms_user_id", Value = this.ms_user_id });
            }


            //事故分類
            if (this.kind_of_accident_id != null)
            {
                ans += @"
AND
(dc_accident.kind_of_accident_id = :kind_of_accident_id)
";
                plist.Add(new SqlParamData() { Name = "kind_of_accident_id", Value = this.kind_of_accident_id });
            }

            //発生状況
            if (this.accident_situation_id != null)
            {
                ans += @"
AND
(dc_accident.accident_situation_id = :accident_situation_id)
";
                plist.Add(new SqlParamData() { Name = "accident_situation_id", Value = this.accident_situation_id });
            }


            //Date
            if (this.date_start != null && this.date_end != null)
            {
                DateTime start = this.GetSearchStartDate(this.date_start.Value);
                DateTime end = this.GetSearchEndDate(this.date_end.Value);

                ans += @"
AND
(dc_accident.date BETWEEN :date_start AND :date_end)
";
                plist.Add(new SqlParamData() { Name = "date_start", Value = start });
                plist.Add(new SqlParamData() { Name = "date_end", Value = end });
            }


            //キーワード
            if (this.SearchKeyword.Length > 0)
            {
                ans += @"
AND
dc_accident.search_keyword LIKE :SearchKeyword
";
                string ks = this.GetLikeString(this.SearchKeyword);
                plist.Add(new SqlParamData() { Name = "SearchKeyword", Value = ks });
            }


            return ans;
        }
    }
}
