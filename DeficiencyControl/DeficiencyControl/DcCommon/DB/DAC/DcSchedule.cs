using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;
using CIsl.DB;
using CIsl.DB.WingDAC;
using DcCommon.DB.DAC.Search;
using System.Drawing;


namespace DcCommon.DB.DAC
{
    /// <summary>
    /// スケジュールテーブル
    /// dc_schedule
    /// </summary>
    /// <remarks>基底テーブル、挿入更新は書くべからず</remarks>
    public class DcSchedule : BaseDac
    {
        public override int TableID
        {
            get
            {
                return DBTableID.dc_schedule;
            }
        }


        public override int ID
        {
            get
            {
                return this.schedule_id;
            }
        }


        #region メンバ変数
        /// <summary>
        /// スケジュールID
        /// </summary>
        public int schedule_id = EVal;

        /// <summary>
        /// スケジュール区分ID
        /// </summary>
        public int schedule_category_id = EVal;

        /// <summary>
        /// スケジュール種別ID
        /// </summary>
        public int schedule_kind_id = EVal;

        /// <summary>
        /// スケジュール種別詳細ID
        /// </summary>
        public int schedule_kind_detail_id = EVal;

        /// <summary>
        /// 予定日
        /// </summary>
        public DateTime estimate_date = EDate;

        /// <summary>
        /// 実績日
        /// </summary>
        public DateTime inspection_date = EDate;

        /// <summary>
        /// 有効期限
        /// </summary>
        public DateTime expiry_date = EDate;

        /// <summary>
        /// 実績メモ
        /// </summary>
        public string record_memo = "";

        /// <summary>
        /// 色赤
        /// </summary>
        public int color_r = EVal;

        /// <summary>
        /// 色緑
        /// </summary>
        public int color_g = EVal;

        /// <summary>
        /// 色青
        /// </summary>
        public int color_b = EVal;

        #endregion

        /// <summary>
        /// データ色
        /// </summary>
        public Color DataColor
        {
            get
            {
                Color ans = SystemColors.Control;
                if (this.color_r != EVal && this.color_g != EVal && color_b != EVal)
                {
                    ans = Color.FromArgb(this.color_r, this.color_g, this.color_b);
                }
                return ans;
            }
            set
            {
                this.color_r = value.R;
                this.color_g = value.G;
                this.color_b = value.B;
            }
        }

        /// <summary>
        /// 基本SELECT
        /// </summary>
        private static string DefaultSelect = @"
SELECT
dc_schedule.schedule_id,
dc_schedule.schedule_category_id,
dc_schedule.schedule_kind_id,
dc_schedule.schedule_kind_detail_id,
dc_schedule.estimate_date,
dc_schedule.inspection_date,
dc_schedule.expiry_date,
dc_schedule.record_memo,
dc_schedule.color_r,
dc_schedule.color_g,
dc_schedule.color_b,

dc_schedule.delete_flag,
dc_schedule.create_ms_user_id,
dc_schedule.update_ms_user_id,
dc_schedule.create_date,
dc_schedule.update_date

FROM
dc_schedule
";


        /// <summary>
        /// レコード一覧取得
        /// </summary>
        /// <param name="cone"></param>
        /// <returns></returns>
        public static List<DcSchedule> GetRecords(NpgsqlConnection cone)
        {
            List<DcSchedule> anslist = new List<DcSchedule>();

            try
            {
                //SQL
                string sql = DefaultSelect + @"
WHERE
dc_schedule.delete_flag = false

ORDER BY
dc_schedule.estimate_date
";

                //検索条件付加
                //List<SqlParamData> plist = new List<SqlParamData>();
                //plist.Add(new SqlParamData() { Name = "comment_item_id", Value = comment_item_id });


                //SQL取得
                anslist = GetRecordsList<DcSchedule>(cone, sql);

            }
            catch (Exception e)
            {
                throw new Exception("DcSchedule GetRecords", e);
            }

            return anslist;
        }
    }
}
