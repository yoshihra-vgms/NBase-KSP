using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CIsl.DB;


namespace DcCommon.DB.DAC.Search
{
    /// <summary>
    /// 予定実績の検索
    /// </summary>
    public class SchedulePlanSearchData : BaseSearchData
    {
        #region メンバ変数
        /// <summary>
        /// Vessel
        /// </summary>
        public decimal? ms_vessel_id = null;


        /// <summary>
        /// 予定日開始
        /// </summary>
        public DateTime? estimate_date_start = null;
        /// <summary>
        /// 予定日終了
        /// </summary>
        public DateTime? estimate_date_end = null;


        /// <summary>
        /// 実績日開始
        /// </summary>
        public DateTime? inspection_date_start = null;
        /// <summary>
        /// 実績日終了
        /// </summary>
        public DateTime? inspection_date_end = null;
        

        /// <summary>
        /// 有効期限開始
        /// </summary>
        public DateTime? expiry_date_start = null;
        /// <summary>
        /// 有効期限終了
        /// </summary>
        public DateTime? expiry_date_end = null;


        /// <summary>
        /// 有効期限基準日、出力用を想定 この基準日以後のデータを検索する
        /// </summary>
        public DateTime? output_expiry_date = null;
        //---------------------------------------------------------
        /// <summary>
        /// スケジュール種別ID
        /// </summary>
        public int? schedule_kind_id = null;

        /// <summary>
        /// スケジュール種別詳細ID
        /// </summary>
        public int? schedule_kind_detail_id = null;

        /// <summary>
        /// 実績が入っているものを含めるか true=実績も含めて全て false=実績が入っているものは除外する。主にalarmでの使用を想定
        /// </summary>
        public bool InspectionEnabled = true;

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
(dc_schedule_plan.ms_vessel_id = :ms_vessel_id)
";
                plist.Add(new SqlParamData() { Name = "ms_vessel_id", Value = this.ms_vessel_id });
            }

            {
                string datesql = "";

                #region 予定実績有効期限日の検索 どれかが引っかかればよいのでorで検索する

                //予定日
                if (this.estimate_date_start != null && this.estimate_date_end != null)
                {
                    DateTime estimate_date_start = this.GetSearchStartDate(this.estimate_date_start.Value);
                    DateTime estimate_date_end = this.GetSearchEndDate(this.estimate_date_end.Value);

                    datesql += @"
(dc_schedule_plan.estimate_date BETWEEN :estimate_date_start AND :estimate_date_end)
";
                    plist.Add(new SqlParamData() { Name = "estimate_date_start", Value = estimate_date_start });
                    plist.Add(new SqlParamData() { Name = "estimate_date_end", Value = estimate_date_end });
                }


                //実績日
                if (this.inspection_date_start != null && this.inspection_date_end != null)
                {
                    DateTime inspection_date_start = this.GetSearchStartDate(this.inspection_date_start.Value);
                    DateTime inspection_date_end = this.GetSearchEndDate(this.inspection_date_end.Value);

                    if (datesql.Length > 0)
                    {
                        datesql += @"
OR";
                    }

                    datesql += @"
(dc_schedule_plan.inspection_date BETWEEN :inspection_date_start AND :inspection_date_end)
";
                    plist.Add(new SqlParamData() { Name = "inspection_date_start", Value = inspection_date_start });
                    plist.Add(new SqlParamData() { Name = "inspection_date_end", Value = inspection_date_end });
                }

                //有効期限
                if (this.expiry_date_start != null && this.expiry_date_end != null)
                {
                    DateTime expiry_date_start = this.GetSearchStartDate(this.expiry_date_start.Value);
                    DateTime expiry_date_end = this.GetSearchEndDate(this.expiry_date_end.Value);

                    if (datesql.Length > 0)
                    {
                        datesql += @"
OR";
                    }

                    datesql += @"
(dc_schedule_plan.expiry_date BETWEEN :expiry_date_start AND :expiry_date_end)
";
                    plist.Add(new SqlParamData() { Name = "expiry_date_start", Value = expiry_date_start });
                    plist.Add(new SqlParamData() { Name = "expiry_date_end", Value = expiry_date_end });
                }

                #endregion


                if (datesql.Length > 0)
                {
                    ans += string.Format(@"
AND
({0})", datesql);
                }

            }



            //種別
            if (this.schedule_kind_id != null)
            {
                ans += @"
AND
(dc_schedule_plan.schedule_kind_id = :schedule_kind_id)
";
                plist.Add(new SqlParamData() { Name = "schedule_kind_id", Value = this.schedule_kind_id });
            }



            //種別詳細
            if (this.schedule_kind_detail_id != null)
            {
                ans += @"
AND
(dc_schedule_plan.schedule_kind_detail_id = :schedule_kind_detail_id)
";
                plist.Add(new SqlParamData() { Name = "schedule_kind_detail_id", Value = this.schedule_kind_detail_id });
            }


            //実績日を含むか可否
            if (this.InspectionEnabled == false)
            {
                ans += @"
AND
(dc_schedule_plan.inspection_date = :inspection_date)
";
                plist.Add(new SqlParamData() { Name = "inspection_date", Value = DcSchedulePlan.EDate });
            }


            //基準日以後の有効期限取得
            if (this.output_expiry_date != null)
            {
                ans += @"
AND
(dc_schedule_plan.expiry_date >= :output_expiry_date)
";
                plist.Add(new SqlParamData() { Name = "output_expiry_date", Value = this.output_expiry_date });
            }



            return ans;
        }
    }
}
