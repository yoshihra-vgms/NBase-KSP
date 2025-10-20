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
    /// 検船指摘事項テーブル
    /// dc_moi_observation
    /// </summary>
    public class DcMoiObservation : BaseDac
    {
        public override int TableID
        {
            get
            {
                return DBTableID.dc_moi_observation;
            }
        }

        public override int ID
        {
            get
            {
                return this.moi_observation_id;
            }
        }


        #region メンバ変数

        /// <summary>
        /// 検船指摘事項ID
        /// </summary>
        public int moi_observation_id = EVal;
        
        /// <summary>
        /// 親検船ID
        /// </summary>
        public int moi_id = EVal;

        /// <summary>
        /// 指摘事項番号
        /// </summary>
        public int observation_no = EVal;

        /// <summary>
        /// 検船状態ID
        /// </summary>
        public int moi_status_id = EVal;

        /// <summary>
        /// VIQ Code
        /// </summary>
        public int viq_code_id = EVal;

        /// <summary>
        /// VIQ No
        /// </summary>
        public int viq_no_id = EVal;

        /// <summary>
        /// 指摘事項
        /// </summary>
        public string observation = "";

        /// <summary>
        /// 根本原因
        /// </summary>
        public string root_cause = "";

        /// <summary>
        /// 1st コメント
        /// </summary>
        public string comment_1st = "";

        /// <summary>
        /// 1st コメントチェック
        /// </summary>
        public bool comment_1st_check = false;

        /// <summary>
        /// 2ndコメント
        /// </summary>
        public string comment_2nd = "";

        /// <summary>
        /// 2ndコメントチェック
        /// </summary>
        public bool comment_2nd_check = false;

        /// <summary>
        /// 再発防止対策
        /// </summary>
        public string preventive_action = "";

        /// <summary>
        /// 特記事項
        /// </summary>
        public string special_notes = "";

        /// <summary>
        /// 検索キーワード
        /// </summary>
        public string search_keyword = "";

        #endregion


        /// <summary>
        /// 書き込みコメント
        /// </summary>
        public string WriteComment
        {
            get
            {
                string ans = this.comment_1st;
                if (this.comment_2nd_check == true)
                {
                    ans = this.comment_2nd;
                }

                return ans;

            }
        }

        /// <summary>
        /// 基本SELECT
        /// </summary>
        private static string DefaultSelect = @"
SELECT
dc_moi_observation.moi_observation_id,
dc_moi_observation.moi_id,
dc_moi_observation.observation_no,
dc_moi_observation.moi_status_id,
dc_moi_observation.viq_code_id,
dc_moi_observation.viq_no_id,
dc_moi_observation.observation,
dc_moi_observation.root_cause,
dc_moi_observation.comment_1st,
dc_moi_observation.comment_1st_check,
dc_moi_observation.comment_2nd,
dc_moi_observation.comment_2nd_check,
dc_moi_observation.preventive_action,
dc_moi_observation.special_notes,
dc_moi_observation.search_keyword,

dc_moi_observation.delete_flag,
dc_moi_observation.create_ms_user_id,
dc_moi_observation.update_ms_user_id,
dc_moi_observation.create_date,
dc_moi_observation.update_date
FROM
dc_moi_observation

";




        /// <summary>
        /// 検索
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public static List<DcMoiObservation> GetRecordsBySearchData(NpgsqlConnection cone, MoiSearchData sdata)
        {
            List<DcMoiObservation> anslist = new List<DcMoiObservation>();

            try
            {
                //SQL
                string sql = DefaultSelect + @"
INNER JOIN dc_moi ON dc_moi_observation.moi_id = dc_moi.moi_id

WHERE
dc_moi.delete_flag = false
AND
dc_moi_observation.delete_flag = false

";

                //検索条件付加
                List<SqlParamData> paramlist = new List<SqlParamData>();
                sql += sdata.CreateSQLWhere(out paramlist);

                sql += @"
ORDER BY
dc_moi_observation.update_date
";

                //SQL取得
                anslist = GetRecordsList<DcMoiObservation>(cone, sql, paramlist);

            }
            catch (Exception e)
            {
                throw new Exception("DcMoiObservation GetRecordsBySearchData", e);
            }

            return anslist;
        }


        /// <summary>
        /// 親検船に関連するレコードを一覧取得する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="moi_id"></param>
        /// <returns></returns>
        public static List<DcMoiObservation> GetRecordsByMoiID(NpgsqlConnection cone, int moi_id)
        {
            List<DcMoiObservation> anslist = new List<DcMoiObservation>();

            try
            {
                //SQL
                string sql = DefaultSelect + @"
WHERE
dc_moi_observation.delete_flag = false
AND
dc_moi_observation.moi_id = :moi_id

ORDER BY
dc_moi_observation.observation_no
";

                //検索条件付加
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "moi_id", Value = moi_id });


                //SQL取得
                anslist = GetRecordsList<DcMoiObservation>(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcMoiObservation GetRecordsByMoiID", e);
            }

            return anslist;
        }



        /// <summary>
        /// 対象のデータを取得する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="moi_observation_id"></param>
        /// <returns></returns>
        public static DcMoiObservation GetRecordByMoiObservationID(NpgsqlConnection cone, int moi_observation_id)
        {
            DcMoiObservation ans = null;

            try
            {
                //SQL
                string sql = DefaultSelect + @"
WHERE
dc_moi_observation.delete_flag = false
AND
dc_moi_observation.moi_observation_id = :moi_observation_id

";

                //検索条件付加
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "moi_observation_id", Value = moi_observation_id });


                //SQL取得
                ans = GetRecordData<DcMoiObservation>(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcMoiObservation GetRecordByMoiObservationID", e);
            }

            return ans;
        }



        /// <summary>
        /// レコード挿入
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
INSERT INTO dc_moi_observation(
moi_id,
observation_no,
moi_status_id,
viq_code_id,
viq_no_id,
observation,
root_cause,
comment_1st,
comment_1st_check,
comment_2nd,
comment_2nd_check,
preventive_action,
special_notes,
search_keyword,

delete_flag,
create_ms_user_id,
update_ms_user_id
) VALUES (
:moi_id,
:observation_no,
:moi_status_id,
:viq_code_id,
:viq_no_id,
:observation,
:root_cause,
:comment_1st,
:comment_1st_check,
:comment_2nd,
:comment_2nd_check,
:preventive_action,
:special_notes,
:search_keyword,

:delete_flag,
:create_ms_user_id,
:update_ms_user_id
)
RETURNING moi_observation_id;
";

                this.delete_flag = false;
                this.create_ms_user_id = requser.ms_user_id;
                this.update_ms_user_id = requser.ms_user_id;

                //パラメータのADD
                plist.Add(new SqlParamData() { Name = "moi_id", Value = this.moi_id });
                plist.Add(new SqlParamData() { Name = "observation_no", Value = this.observation_no });
                plist.Add(new SqlParamData() { Name = "moi_status_id", Value = this.moi_status_id });
                plist.Add(new SqlParamData() { Name = "viq_code_id", Value = this.viq_code_id });
                plist.Add(new SqlParamData() { Name = "viq_no_id", Value = this.viq_no_id });
                plist.Add(new SqlParamData() { Name = "observation", Value = this.observation });
                plist.Add(new SqlParamData() { Name = "root_cause", Value = this.root_cause });
                plist.Add(new SqlParamData() { Name = "comment_1st", Value = this.comment_1st });
                plist.Add(new SqlParamData() { Name = "comment_1st_check", Value = this.comment_1st_check });
                plist.Add(new SqlParamData() { Name = "comment_2nd", Value = this.comment_2nd });
                plist.Add(new SqlParamData() { Name = "comment_2nd_check", Value = this.comment_2nd_check });
                plist.Add(new SqlParamData() { Name = "preventive_action", Value = this.preventive_action });
                plist.Add(new SqlParamData() { Name = "special_notes", Value = this.special_notes });
                plist.Add(new SqlParamData() { Name = "search_keyword", Value = this.search_keyword });


                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "create_ms_user_id", Value = this.create_ms_user_id });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });

                //実行
                ansid = (int)this.ExecuteScalar(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcMoiObservation InsertRecord", e);
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
dc_moi_observation
SET
moi_id = :moi_id,
observation_no = :observation_no,
moi_status_id = :moi_status_id,
viq_code_id = :viq_code_id,
viq_no_id = :viq_no_id,
observation = :observation,
root_cause = :root_cause,
comment_1st = :comment_1st,
comment_1st_check = :comment_1st_check,
comment_2nd = :comment_2nd,
comment_2nd_check = :comment_2nd_check,
preventive_action = :preventive_action,
special_notes = :special_notes,
search_keyword = :search_keyword,

delete_flag = :delete_flag,
update_ms_user_id = :update_ms_user_id

WHERE
moi_observation_id = :moi_observation_id
";

                this.update_ms_user_id = requser.ms_user_id;

                //パラメータのADD
                plist.Add(new SqlParamData() { Name = "moi_id", Value = this.moi_id });
                plist.Add(new SqlParamData() { Name = "observation_no", Value = this.observation_no });
                plist.Add(new SqlParamData() { Name = "moi_status_id", Value = this.moi_status_id });
                plist.Add(new SqlParamData() { Name = "viq_code_id", Value = this.viq_code_id });
                plist.Add(new SqlParamData() { Name = "viq_no_id", Value = this.viq_no_id });
                plist.Add(new SqlParamData() { Name = "observation", Value = this.observation });
                plist.Add(new SqlParamData() { Name = "root_cause", Value = this.root_cause });
                plist.Add(new SqlParamData() { Name = "comment_1st", Value = this.comment_1st });
                plist.Add(new SqlParamData() { Name = "comment_1st_check", Value = this.comment_1st_check });
                plist.Add(new SqlParamData() { Name = "comment_2nd", Value = this.comment_2nd });
                plist.Add(new SqlParamData() { Name = "comment_2nd_check", Value = this.comment_2nd_check });
                plist.Add(new SqlParamData() { Name = "preventive_action", Value = this.preventive_action });
                plist.Add(new SqlParamData() { Name = "special_notes", Value = this.special_notes });
                plist.Add(new SqlParamData() { Name = "search_keyword", Value = this.search_keyword });

                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });


                plist.Add(new SqlParamData() { Name = "moi_observation_id", Value = this.moi_observation_id });

                //実行
                ret = this.ExecuteNonQuery(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcMoiObservation UpdateRecord", e);
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
                ret = this.ExecuteDelete(cone, requser, "dc_moi_observation", "moi_observation_id", this.moi_observation_id);
            }
            catch (Exception e)
            {
                throw new Exception("DcMoiObservation DeleteRecord", e);
            }

            return ret;
        }



        /// <summary>
        /// Statusの更新(デバッグ用)
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="requser">要求者</param>
        /// <returns></returns>
        public bool UpdateStatus(NpgsqlConnection cone, MsUser requser)
        {
            bool ret = false;

            try
            {
                #region SQL
                string sql = @"
UPDATE
dc_moi_observation

SET
moi_status_id = :moi_status_id

WHERE
moi_observation_id = :moi_observation_id;
";
                #endregion

                List<SqlParamData> plist = new List<SqlParamData>();

                this.delete_flag = false;
                this.update_ms_user_id = requser.ms_user_id;

                #region パラメータ挿入

                plist.Add(new SqlParamData() { Name = "moi_status_id", Value = this.moi_status_id });


                plist.Add(new SqlParamData() { Name = "moi_observation_id", Value = this.moi_observation_id });

                #endregion


                //実行！
                ret = ExecuteNonQuery(cone, sql, plist);
            }
            catch (Exception e)
            {
                throw new Exception("DcMoiObservation UpdateStatus", e);
            }

            return ret;
        }
        ///////////////////////////////////////////////
        /// <summary>
        /// Completeか否かをチェックする
        /// </summary>
        /// <returns></returns>
        public bool CheckComplete()
        {
            if (this.moi_status_id == (int)EMoiStatus.Complete)
            {
                return true;
            }
            return false;
        }

    }
}
