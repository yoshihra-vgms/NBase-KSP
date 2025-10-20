using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIsl.DB;


namespace DcCommon.DB.DAC.Search
{
    /// <summary>
    /// 検船の検索情報
    /// </summary>
    public class MoiSearchData : BaseSearchData
    {
        #region メンバ変数
        /// <summary>
        /// Vessel
        /// </summary>
        public decimal? ms_vessel_id = null;

        /// <summary>
        /// 検船種別
        /// </summary>
        public int? inspection_category_id = null;

        /// <summary>
        /// Port
        /// </summary>
        public string ms_basho_id = "";

        /// <summary>
        /// 国
        /// </summary>
        public string ms_regional_code = "";

        /// <summary>
        /// 検船会社
        /// </summary>
        public string inspection_ms_customer_id = "";


        /// <summary>
        /// 検船員
        /// </summary>
        public string inspection_name = "";

        /// <summary>
        /// PIC
        /// </summary>
        public string ms_user_id = "";

        /// <summary>
        /// VIQ Version
        /// </summary>
        public int? viq_version_id = null;

        /// <summary>
        /// VIQ Code
        /// </summary>
        public int? viq_code_id = null;


        /// <summary>
        /// VIQ 番号ID
        /// </summary>
        public int? viq_no_id = null;

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



        /// <summary>
        /// 0件データを検索するか？ trueで検索を行う
        /// </summary>
        public bool ObservationZeroEnabledFlag = false;
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
(dc_moi.ms_vessel_id = :ms_vessel_id)
";
                plist.Add(new SqlParamData() { Name = "ms_vessel_id", Value = this.ms_vessel_id });
            }

            //検船種別
            if (this.inspection_category_id != null)
            {
                ans += @"
AND
(dc_moi.inspection_category_id = :inspection_category_id)
";
                plist.Add(new SqlParamData() { Name = "inspection_category_id", Value = this.inspection_category_id });
            }

            //Port
            if (this.ms_basho_id.Length > 0)
            {
                ans += @"
AND
(dc_moi.ms_basho_id = :ms_basho_id)
";
                plist.Add(new SqlParamData() { Name = "ms_basho_id", Value = this.ms_basho_id });
            }

            //国
            if (this.ms_regional_code.Length > 0)
            {
                ans += @"
AND
(dc_moi.ms_regional_code = :ms_regional_code)
";
                plist.Add(new SqlParamData() { Name = "ms_regional_code", Value = this.ms_regional_code });
            }

            //検船会社
            if (this.inspection_ms_customer_id.Length > 0)
            {
                ans += @"
AND
(dc_moi.inspection_ms_customer_id = :inspection_ms_customer_id)
";
                plist.Add(new SqlParamData() { Name = "inspection_ms_customer_id", Value = this.inspection_ms_customer_id });
            }

            //検船員
            if (this.inspection_name.Length > 0)
            {
                ans += @"
AND
(dc_moi.inspection_name LIKE :inspection_name)
";
                string inlk = GetLikeString(this.inspection_name);
                plist.Add(new SqlParamData() { Name = "inspection_name", Value = inlk });
            }


            //Date
            if (this.date_start != null && this.date_end != null)
            {
                DateTime start = this.GetSearchStartDate(this.date_start.Value);
                DateTime end = this.GetSearchEndDate(this.date_end.Value);

                ans += @"
AND
(dc_moi.date BETWEEN :date_start AND :date_end)
";
                plist.Add(new SqlParamData() { Name = "date_start", Value = start });
                plist.Add(new SqlParamData() { Name = "date_end", Value = end });
            }

            //PIC
            if (this.ms_user_id.Length > 0)
            {
                ans += @"
AND
(dc_moi.ms_user_id = :ms_user_id)
";
                plist.Add(new SqlParamData() { Name = "ms_user_id", Value = this.ms_user_id });
            }

            

            //VIQ Code
            if (this.viq_code_id != null)
            {
                ans += @"
AND
(dc_moi_observation.viq_code_id = :viq_code_id)
";
                plist.Add(new SqlParamData() { Name = "viq_code_id", Value = this.viq_code_id });
            }

            //VIQ No
            if (this.viq_no_id != null)
            {
                ans += @"
AND
(dc_moi_observation.viq_no_id = :viq_no_id)
";
                plist.Add(new SqlParamData() { Name = "viq_no_id", Value = this.viq_no_id });
            }


            //キーワード
            if (this.SearchKeyword.Length > 0)
            {
                ans += @"
AND
(dc_moi.search_keyword || dc_moi_observation.search_keyword)  LIKE :SearchKeyword
";
                string ks = this.GetLikeString(this.SearchKeyword);
                plist.Add(new SqlParamData() { Name = "SearchKeyword", Value = ks });
            }



            //0件フラグ
            if (this.ObservationZeroEnabledFlag == false)
            {
                ans += @"
AND
(dc_moi.observation != 0)
";
            }

            return ans;
        }
    }


}
