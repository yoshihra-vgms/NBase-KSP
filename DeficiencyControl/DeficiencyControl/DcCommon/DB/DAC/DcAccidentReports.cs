using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;
using CIsl.DB;
using CIsl.DB.WingDAC;


namespace DcCommon.DB.DAC
{
    /// <summary>
    /// Accident報告書提出先テーブル
    /// dc_accident_reports
    /// </summary>
    public class DcAccidentReports : BaseDac
    {
        public override int TableID
        {
            get
            {
                return DBTableID.dc_accident_reports;
            }
        }


        public override int ID
        {
            get
            {
                return this.accident_reports_id;
            }
        }


        #region メンバ変数
        /// <summary>
        /// Accident報告書提出先テーブルID
        /// </summary>
        public int accident_reports_id = EVal;

        /// <summary>
        /// 親事故トラブルID
        /// </summary>
        public int accident_id = EVal;

        /// <summary>
        /// 表示順
        /// </summary>
        public int order_no = EVal;

        /// <summary>
        /// 提出先
        /// </summary>
        public string reports = "";

        
        #endregion

        /// <summary>
        /// 基本SELECT
        /// </summary>
        private static string DefaultSelect = @"
SELECT
dc_accident_reports.accident_reports_id,
dc_accident_reports.accident_id,
dc_accident_reports.order_no,
dc_accident_reports.reports,

dc_accident_reports.delete_flag,
dc_accident_reports.create_ms_user_id,
dc_accident_reports.update_ms_user_id,
dc_accident_reports.create_date,
dc_accident_reports.update_date

FROM
dc_accident_reports

";



        /// <summary>
        /// 事故トラブルに関連するデータを取得
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="accident_id"></param>
        /// <returns></returns>
        public static List<DcAccidentReports> GetRecordsByAccidentID(NpgsqlConnection cone, int accident_id)
        {
            List<DcAccidentReports> anslist = new List<DcAccidentReports>();

            try
            {
                //SQL
                string sql = DefaultSelect + @"
WHERE
dc_accident_reports.delete_flag = false
AND
dc_accident_reports.accident_id = :accident_id

ORDER BY
dc_accident_reports.order_no
";

                //検索条件付加
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "accident_id", Value = accident_id });


                //SQL取得
                anslist = GetRecordsList<DcAccidentReports>(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcActionCodeHistory GetRecordsByCommentItemID", e);
            }

            return anslist;
        }



        /// <summary>
        /// レコードの挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public override int InsertRecord(NpgsqlConnection cone, MsUser requser)
        {
            int ansid = EVal;
            List<SqlParamData> plist = new List<SqlParamData>();

            try
            {
                string sql = @"
INSERT INTO dc_accident_reports(
accident_id,
order_no,
reports,

delete_flag,
create_ms_user_id,
update_ms_user_id
) VALUES (
:accident_id,
:order_no,
:reports,

:delete_flag,
:create_ms_user_id,
:update_ms_user_id

)
RETURNING accident_reports_id;
";

                this.delete_flag = false;
                this.create_ms_user_id = requser.ms_user_id;
                this.update_ms_user_id = requser.ms_user_id;

                //パラメータのADD
                plist.Add(new SqlParamData() { Name = "accident_id", Value = this.accident_id });
                plist.Add(new SqlParamData() { Name = "order_no", Value = this.order_no });
                plist.Add(new SqlParamData() { Name = "reports", Value = this.reports });

                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "create_ms_user_id", Value = this.create_ms_user_id });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });

                //実行
                ansid = (int)this.ExecuteScalar(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcAccidentReports InsertRecord", e);
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
dc_accident_reports
SET
accident_id= :accident_id,
order_no= :order_no,
reports= :reports,

delete_flag= :delete_flag,
update_ms_user_id= :update_ms_user_id

WHERE
accident_reports_id= :accident_reports_id
";

                this.update_ms_user_id = requser.ms_user_id;

                //パラメータのADD
                plist.Add(new SqlParamData() { Name = "accident_id", Value = this.accident_id });
                plist.Add(new SqlParamData() { Name = "order_no", Value = this.order_no });
                plist.Add(new SqlParamData() { Name = "reports", Value = this.reports });

                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });


                plist.Add(new SqlParamData() { Name = "accident_reports_id", Value = this.accident_reports_id });

                //実行
                ret = this.ExecuteNonQuery(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcAccidentReports UpdateRecord", e);
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
                ret = this.ExecuteDelete(cone, requser, "dc_accident_reports", "accident_reports_id", this.accident_reports_id);
            }
            catch (Exception e)
            {
                throw new Exception("DcAccidentReports DeleteRecord", e);
            }

            return ret;
        }

    }
}
