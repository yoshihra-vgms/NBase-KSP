using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;
using CIsl.DB;
using CIsl.DB.WingDAC;
using DcCommon.DB.DAC.Search;

namespace DcCommon.DB.DAC
{
    /// <summary>
    /// その他スケジュールテーブル
    /// dc_schedule_other
    /// </summary>
    public class DcScheduleOther : DcSchedule
    {
        public override int TableID
        {
            get
            {
                return DBTableID.dc_schedule_other;
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
        /// イベントメモ
        /// </summary>
        public string event_memo = "";

        #endregion


        /// <summary>
        /// 基本SELECT
        /// </summary>
        private static string DefaultSelect = @"
SELECT
dc_schedule_other.schedule_id,
dc_schedule_other.schedule_category_id,
dc_schedule_other.schedule_kind_id,
dc_schedule_other.schedule_kind_detail_id,
dc_schedule_other.estimate_date,
dc_schedule_other.inspection_date,
dc_schedule_other.expiry_date,
dc_schedule_other.record_memo,
dc_schedule_other.color_r,
dc_schedule_other.color_g,
dc_schedule_other.color_b,

dc_schedule_other.delete_flag,
dc_schedule_other.create_ms_user_id,
dc_schedule_other.update_ms_user_id,
dc_schedule_other.create_date,
dc_schedule_other.update_date,

dc_schedule_other.event_memo

FROM
dc_schedule_other
";

       


        /// <summary>
        /// その他スケジュールの検索
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public static List<DcScheduleOther> GetRecordsBySearchData(NpgsqlConnection cone, ScheduleOtherSearchData sdata)
        {
            List<DcScheduleOther> anslist = new List<DcScheduleOther>();

            try
            {
                //SQL
                string sql = DefaultSelect + @"
WHERE
dc_schedule_other.delete_flag = false
";

                //検索条件付加
                List<SqlParamData> paramlist = new List<SqlParamData>();
                sql += sdata.CreateSQLWhere(out paramlist);

                sql += @"
ORDER BY
dc_schedule_other.estimate_date DESC
";

                //SQL取得
                anslist = GetRecordsList<DcScheduleOther>(cone, sql, paramlist);

            }
            catch (Exception e)
            {
                throw new Exception("DcScheduleOther GetRecordsBySearchData", e);
            }

            return anslist;
        }



        /// <summary>
        /// レコードの挿入
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="requser">実行ユーザー</param>
        /// <returns>成功可否</returns>
        public override int InsertRecord(NpgsqlConnection cone, MsUser requser)
        {
            int ansid = EVal;
            List<SqlParamData> plist = new List<SqlParamData>();

            try
            {
                string sql = @"
INSERT INTO dc_schedule_other(
schedule_category_id,
schedule_kind_id,
schedule_kind_detail_id,
estimate_date,
inspection_date,
expiry_date,
record_memo,
color_r,
color_g,
color_b,

delete_flag,
create_ms_user_id,
update_ms_user_id,

event_memo

) VALUES (
:schedule_category_id,
:schedule_kind_id,
:schedule_kind_detail_id,
:estimate_date,
:inspection_date,
:expiry_date,
:record_memo,
:color_r,
:color_g,
:color_b,

:delete_flag,
:create_ms_user_id,
:update_ms_user_id,

:event_memo

)RETURNING schedule_id;
";

                this.delete_flag = false;
                this.create_ms_user_id = requser.ms_user_id;
                this.update_ms_user_id = requser.ms_user_id;

                //パラメータのADD                
                plist.Add(new SqlParamData() { Name = "schedule_category_id", Value = this.schedule_category_id });
                plist.Add(new SqlParamData() { Name = "schedule_kind_id", Value = this.schedule_kind_id });
                plist.Add(new SqlParamData() { Name = "schedule_kind_detail_id", Value = this.schedule_kind_detail_id });

                plist.Add(new SqlParamData() { Name = "estimate_date", Value = this.estimate_date });
                plist.Add(new SqlParamData() { Name = "inspection_date", Value = this.inspection_date });
                plist.Add(new SqlParamData() { Name = "expiry_date", Value = this.expiry_date });
                plist.Add(new SqlParamData() { Name = "record_memo", Value = this.record_memo });
                plist.Add(new SqlParamData() { Name = "color_r", Value = this.color_r });
                plist.Add(new SqlParamData() { Name = "color_g", Value = this.color_g });
                plist.Add(new SqlParamData() { Name = "color_b", Value = this.color_b });

                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "create_ms_user_id", Value = this.create_ms_user_id });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });


                plist.Add(new SqlParamData() { Name = "event_memo", Value = this.event_memo });

                //実行
                ansid = (int)this.ExecuteScalar(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcScheduleOther InsertRecord", e);
            }

            return ansid;
        }




        /// <summary>
        /// レコード更新
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public override bool UpdateRecord(NpgsqlConnection cone, MsUser requser)
        {
            bool ret = false;
            List<SqlParamData> plist = new List<SqlParamData>();

            try
            {
                string sql = @"
UPDATE
dc_schedule_other

SET
schedule_category_id = :schedule_category_id,
schedule_kind_id = :schedule_kind_id,
schedule_kind_detail_id = :schedule_kind_detail_id,
estimate_date = :estimate_date,
inspection_date = :inspection_date,
expiry_date = :expiry_date,
record_memo = :record_memo,
color_r = :color_r,
color_g = :color_g,
color_b = :color_b,

delete_flag = :delete_flag,
update_ms_user_id = :update_ms_user_id,

event_memo = :event_memo

WHERE
schedule_id = :schedule_id
";

                this.update_ms_user_id = requser.ms_user_id;

                //パラメータのADD                
                plist.Add(new SqlParamData() { Name = "schedule_category_id", Value = this.schedule_category_id });
                plist.Add(new SqlParamData() { Name = "schedule_kind_id", Value = this.schedule_kind_id });
                plist.Add(new SqlParamData() { Name = "schedule_kind_detail_id", Value = this.schedule_kind_detail_id });

                plist.Add(new SqlParamData() { Name = "estimate_date", Value = this.estimate_date });
                plist.Add(new SqlParamData() { Name = "inspection_date", Value = this.inspection_date });
                plist.Add(new SqlParamData() { Name = "expiry_date", Value = this.expiry_date });
                plist.Add(new SqlParamData() { Name = "record_memo", Value = this.record_memo });
                plist.Add(new SqlParamData() { Name = "color_r", Value = this.color_r });
                plist.Add(new SqlParamData() { Name = "color_g", Value = this.color_g });
                plist.Add(new SqlParamData() { Name = "color_b", Value = this.color_b });

                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });


                plist.Add(new SqlParamData() { Name = "event_memo", Value = this.event_memo });

                plist.Add(new SqlParamData() { Name = "schedule_id", Value = this.schedule_id });

                //実行
                ret = this.ExecuteNonQuery(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcScheduleOther UpdateRecord", e);
            }

            return ret;
        }



        /// <summary>
        /// 削除実行
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="requser">実行ユーザー</param>
        /// <returns></returns>
        public override bool DeleteRecord(NpgsqlConnection cone, MsUser requser)
        {
            bool ret = false;
            try
            {
                ret = this.ExecuteDelete(cone, requser, "dc_schedule_other", "schedule_id", this.schedule_id);
            }
            catch (Exception e)
            {
                throw new Exception("DcScheduleOther DeleteRecord", e);
            }

            return ret;
        }
    }
}
