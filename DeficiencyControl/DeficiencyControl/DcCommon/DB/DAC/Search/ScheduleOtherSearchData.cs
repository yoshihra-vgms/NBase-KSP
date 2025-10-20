using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CIsl.DB;

namespace DcCommon.DB.DAC.Search
{
    /// <summary>
    /// スケジュールのその他の検索
    /// </summary>
    public class ScheduleOtherSearchData : BaseSearchData
    {
        #region メンバ変数


        /// <summary>
        /// Date予定日開始
        /// </summary>
        public DateTime? estimate_date_start = null;
        /// <summary>
        /// Date予定日終了
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



            {
                string datesql = "";

                #region 予定実績有効期限日の検索 どれかが引っかかればよいのでorで検索する

                //予定日
                if (this.estimate_date_start != null && this.estimate_date_end != null)
                {
                    DateTime estimate_date_start = this.GetSearchStartDate(this.estimate_date_start.Value);
                    DateTime estimate_date_end = this.GetSearchEndDate(this.estimate_date_end.Value);

                    datesql += @"
(dc_schedule_other.estimate_date BETWEEN :estimate_date_start AND :estimate_date_end)
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
(dc_schedule_other.inspection_date BETWEEN :inspection_date_start AND :inspection_date_end)
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
(dc_schedule_other.expiry_date BETWEEN :expiry_date_start AND :expiry_date_end)
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



           




            return ans;
        }
    }
}
