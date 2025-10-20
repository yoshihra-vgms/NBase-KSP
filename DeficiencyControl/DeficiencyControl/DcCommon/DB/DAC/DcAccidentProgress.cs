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
    /// 事故トラブル進捗テーブル
    /// dc_accident_progress
    /// </summary>
    public class DcAccidentProgress : BaseDac
    {
        public override int TableID
        {
            get
            {
                return DBTableID.dc_accident_progress;
            }
        }

        public override int ID
        {
            get
            {
                return this.accident_progress_id;
            }
        }


        #region メンバ変数

        /// <summary>
        /// 事故トラブル進捗テーブルID
        /// </summary>
        public int accident_progress_id = EVal;


        /// <summary>
        /// 親事故トラブルテーブルID
        /// </summary>
        public int accident_id = EVal;

        /// <summary>
        /// 更新日
        /// </summary>
        public DateTime date = EDate;


        /// <summary>
        /// /進捗状況
        /// </summary>
        public string progress = "";

        #endregion

        /// <summary>
        /// 基本SELECT
        /// </summary>
        private static string DefaultSelect = @"
SELECT
dc_accident_progress.accident_progress_id,
dc_accident_progress.accident_id,
dc_accident_progress.date,
dc_accident_progress.progress,

dc_accident_progress.delete_flag,
dc_accident_progress.create_ms_user_id,
dc_accident_progress.update_ms_user_id,
dc_accident_progress.create_date,
dc_accident_progress.update_date

FROM
dc_accident_progress

";


        /// <summary>
        /// 親事故トラブルに関連するレコードを一覧取得する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="accident_id"></param>
        /// <returns></returns>
        public static List<DcAccidentProgress> GetRecordsByAccidentID(NpgsqlConnection cone, int accident_id)
        {
            List<DcAccidentProgress> anslist = new List<DcAccidentProgress>();

            try
            {
                //SQL
                string sql = DefaultSelect + @"
WHERE
dc_accident_progress.delete_flag = false
AND
dc_accident_progress.accident_id = :accident_id

ORDER BY
dc_accident_progress.date ASC
";

                //検索条件付加
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "accident_id", Value = accident_id });


                //SQL取得
                anslist = GetRecordsList<DcAccidentProgress>(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcAccidentProgress GetRecordsByAccidentID", e);
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
INSERT INTO dc_accident_progress(
accident_id,
date,
progress,

delete_flag,
create_ms_user_id,
update_ms_user_id

) VALUES (
:accident_id,
:date,
:progress,

:delete_flag,
:create_ms_user_id,
:update_ms_user_id
)
RETURNING accident_progress_id;

";

                this.delete_flag = false;
                this.create_ms_user_id = requser.ms_user_id;
                this.update_ms_user_id = requser.ms_user_id;

                //パラメータのADD
                plist.Add(new SqlParamData() { Name = "accident_id", Value = this.accident_id });
                plist.Add(new SqlParamData() { Name = "date", Value = this.date });
                plist.Add(new SqlParamData() { Name = "progress", Value = this.progress });

                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "create_ms_user_id", Value = this.create_ms_user_id });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });

                //実行
                ansid = (int)this.ExecuteScalar(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcAccidentProgress InsertRecord", e);
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
dc_accident_progress

SET
accident_id = :accident_id,
date = :date,
progress = :progress,

delete_flag = :delete_flag,
update_ms_user_id = :update_ms_user_id

WHERE
accident_progress_id = :accident_progress_id;

";

                this.update_ms_user_id = requser.ms_user_id;

                //パラメータのADD
                plist.Add(new SqlParamData() { Name = "accident_id", Value = this.accident_id });
                plist.Add(new SqlParamData() { Name = "date", Value = this.date });
                plist.Add(new SqlParamData() { Name = "progress", Value = this.progress });

                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });


                plist.Add(new SqlParamData() { Name = "accident_progress_id", Value = this.accident_progress_id });

                //実行
                ret = this.ExecuteNonQuery(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcAccidentProgress UpdateRecord", e);
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
                ret = this.ExecuteDelete(cone, requser, "dc_accident_progress", "accident_progress_id", this.accident_progress_id);
            }
            catch (Exception e)
            {
                throw new Exception("DcAccidentProgress DeleteRecord", e);
            }

            return ret;
        }
    
    
    
    }







}
