using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIsl.DB;


namespace DcCommon.DB.DAC.Search
{
    /// <summary>
    /// PSCInspection検索データ
    /// </summary>
    public class PscInspectionSearchData : BaseSearchData
    {
        #region メンバ変数

        /// <summary>
        /// Date開始
        /// </summary>
        public DateTime? date_start = null;
        /// <summary>
        /// Date終了
        /// </summary>
        public DateTime? date_end = null;

        /// <summary>
        /// 船ID
        /// </summary>
        public decimal? ms_vessel_id = null;

        //種別
        public int? item_kind_id = null;

        /// <summary>
        /// 港ID
        /// </summary>
        public string ms_basho_id = "";

        /// <summary>
        /// 国ID
        /// </summary>
        public string ms_regional_code = "";

        /// <summary>
        /// PIC
        /// </summary>
        public string ms_user_id = "";

        /// <summary>
        /// Pending検索可否
        /// </summary>
        public bool StatusPending = true;
        /// <summary>
        /// Complete検索可否
        /// </summary>
        public bool StatusComplete = true;

        /// <summary>
        /// 指摘事項コード
        /// </summary>
        public int? deficiency_code_id = null;

        /// <summary>
        /// AcitonCode
        /// </summary>
        public List<int> ActionCodeIDList = new List<int>();
        
        /// <summary>
        /// キーワード
        /// </summary>
        public string SearchKeyword = "";

        /// <summary>
        /// 0件データを検索するか？ trueで検索を行う
        /// </summary>
        public bool DeficiencyCountZeroEnabledFlag = false;
        #endregion

        /// <summary>
        /// SQL条件作成
        /// </summary>
        /// <param name="plist"></param>
        /// <returns></returns>
        public override string CreateSQLWhere(out List<SqlParamData> plist)
        {
            string ans = "";

            plist = new List<SqlParamData>();

            //Vessel
            if (this.ms_vessel_id != null)
            {
                ans += @"
AND
(dc_ci_psc_inspection.ms_vessel_id = :ms_vessel_id)
";
                plist.Add(new SqlParamData() { Name = "ms_vessel_id", Value = this.ms_vessel_id });
            }

            //Kind
            if (this.item_kind_id != null)
            {
                ans += @"
AND
(dc_ci_psc_inspection.item_kind_id = :item_kind_id)
";
                plist.Add(new SqlParamData() { Name = "item_kind_id", Value = this.item_kind_id });
            }


            //Port
            if (this.ms_basho_id.Length > 0)
            {
                ans += @"
AND
(dc_ci_psc_inspection.ms_basho_id = :ms_basho_id)
";
                plist.Add(new SqlParamData() { Name = "ms_basho_id", Value = this.ms_basho_id });
            }

            //Country
            if (this.ms_regional_code.Length > 0)
            {
                ans += @"
AND
(dc_ci_psc_inspection.ms_regional_code = :ms_regional_code)
";
                plist.Add(new SqlParamData() { Name = "ms_regional_code", Value = this.ms_regional_code });
            }


            //PIC
            if (this.ms_user_id.Length > 0)
            {
                ans += @"
AND
(dc_ci_psc_inspection.ms_user_id = :ms_user_id)
";
                plist.Add(new SqlParamData() { Name = "ms_user_id", Value = this.ms_user_id });
            }

            //Status
            {
                ans += @"
AND
((dc_ci_psc_inspection.status_id = :status_id_pending) OR (dc_ci_psc_inspection.status_id = :status_id_complete))
";

                int[] stidvec = {
                                MsStatus.EVal,
                                MsStatus.EVal,
                            };

                //このどちらかはtrueでないと検索ができないため、これで問題ないはず
                if (this.StatusPending == true)
                {
                    stidvec[0] = (int)EStatus.Pending;
                }
                if (this.StatusComplete == true)
                {
                    stidvec[1] = (int)EStatus.Complete;
                }

                plist.Add(new SqlParamData() { Name = "status_id_pending", Value = stidvec[0] });
                plist.Add(new SqlParamData() { Name = "status_id_complete", Value = stidvec[1] });
            }


            //Date
            if (this.date_start != null && this.date_end != null)
            {
                DateTime start = this.GetSearchStartDate(this.date_start.Value);
                DateTime end = this.GetSearchEndDate(this.date_end.Value);

                ans += @"
AND
(dc_ci_psc_inspection.date BETWEEN :date_start AND :date_end)
";
                plist.Add(new SqlParamData() { Name = "date_start", Value = start });
                plist.Add(new SqlParamData() { Name = "date_end", Value = end });
            }


            //DeficiencyCode
            if (this.deficiency_code_id != null)
            {
                ans += @"
AND
(dc_ci_psc_inspection.deficiency_code_id = :deficiency_code_id)
";
                plist.Add(new SqlParamData() { Name = "deficiency_code_id", Value = this.deficiency_code_id });
            }

            //ActionCode
            if (this.ActionCodeIDList.Count > 0)
            {
                int i = 0;
                foreach (int acid in this.ActionCodeIDList)
                {

                    string param = string.Format(":action_code_id_{0}", i);
                    string paramsql = string.Format(@"
AND
(
dc_ci_psc_inspection.comment_item_id IN (
    SELECT dc_action_code_history.comment_item_id
    FROM dc_action_code_history
    WHERE action_code_id = {0}
)
)
", param);

                    ans += paramsql;
                    plist.Add(new SqlParamData() { Name = param, Value = acid });

                    i++;

                }
            }

            //キーワード
            if (this.SearchKeyword.Length > 0)
            {
                ans += @"
AND
dc_ci_psc_inspection.search_keyword LIKE :SearchKeyword
";
                string ks = this.GetLikeString(this.SearchKeyword);
                plist.Add(new SqlParamData() { Name = "SearchKeyword", Value = ks });
            }




            //0件フラグ
            if (this.DeficiencyCountZeroEnabledFlag == false)
            {
                ans += @"
AND
(dc_ci_psc_inspection.deficinecy_count != 0)
";
            }

            return ans;
        }
    }
}
