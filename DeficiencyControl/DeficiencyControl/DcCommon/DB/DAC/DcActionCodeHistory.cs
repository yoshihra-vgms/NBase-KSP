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
    /// アクションコード履歴テーブル
    /// dc_action_code_history
    /// </summary>
    public class DcActionCodeHistory : BaseDac
    {
        public override int TableID
        {
            get
            {
                return DBTableID.dc_action_code_history;
            }
        }


        public override int ID
        {
            get
            {
                return this.action_code_history_id;
            }
        }


        #region メンバ変数
        /// <summary>
        /// アクションコード履歴ID
        /// </summary>
        public int action_code_history_id = EVal;

        /// <summary>
        /// コメントアイテムID
        /// </summary>
        public int comment_item_id = EVal;

        /// <summary>
        /// アクションコードID
        /// </summary>
        public int action_code_id = EVal;

        /// <summary>
        /// アクションコードテキスト
        /// </summary>
        public string action_code_text = "";

        /// <summary>
        /// 順番
        /// </summary>
        public int order_no = EVal;
        #endregion

        /// <summary>
        /// 基本SELECT
        /// </summary>
        private static string DefaultSelect = @"

SELECT
dc_action_code_history.action_code_history_id,
dc_action_code_history.comment_item_id,
dc_action_code_history.action_code_id,
dc_action_code_history.action_code_text,
dc_action_code_history.order_no,

dc_action_code_history.delete_flag,
dc_action_code_history.create_ms_user_id,
dc_action_code_history.update_ms_user_id,
dc_action_code_history.create_date,
dc_action_code_history.update_date

FROM
dc_action_code_history

";



        /// <summary>
        /// コメントに関連する物体を全て取得
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="comment_item_id"></param>
        /// <returns></returns>
        public static List<DcActionCodeHistory> GetRecordsByCommentItemID(NpgsqlConnection cone, int comment_item_id)
        {
            List<DcActionCodeHistory> anslist = new List<DcActionCodeHistory>();

            try
            {
                //SQL
                string sql = DefaultSelect + @"
WHERE
dc_action_code_history.delete_flag = false
AND
dc_action_code_history.comment_item_id = :comment_item_id

ORDER BY
dc_action_code_history.order_no
";

                //検索条件付加
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "comment_item_id", Value = comment_item_id });


                //SQL取得
                anslist = GetRecordsList<DcActionCodeHistory>(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcActionCodeHistory GetRecordsByCommentItemID", e);
            }

            return anslist;
        }

        /// <summary>
        /// PSCによる検索
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public static List<DcActionCodeHistory> GetRecordsByPscSearchData(NpgsqlConnection cone, PscInspectionSearchData sdata)
        {
            List<DcActionCodeHistory> anslist = new List<DcActionCodeHistory>();

            try
            {
                //SQL
                string sql = DefaultSelect + @"

LEFT JOIN dc_ci_psc_inspection ON dc_ci_psc_inspection.comment_item_id = dc_action_code_history.comment_item_id

WHERE
dc_action_code_history.delete_flag = false
AND
dc_ci_psc_inspection.delete_flag = false
";

                //検索条件付加
                List<SqlParamData> paramlist = new List<SqlParamData>();
                sql += sdata.CreateSQLWhere(out paramlist);

                sql += @"
ORDER BY
dc_action_code_history.order_no
";

                //SQL取得
                anslist = GetRecordsList<DcActionCodeHistory>(cone, sql, paramlist);

            }
            catch (Exception e)
            {
                throw new Exception("DcActionCodeHistory GetRecordsByPscSearchData", e);
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
INSERT INTO dc_action_code_history
(
comment_item_id,
action_code_id,
action_code_text,
order_no,

delete_flag,
create_ms_user_id,
update_ms_user_id

)
VALUES (
:comment_item_id,
:action_code_id,
:action_code_text,
:order_no,

:delete_flag,
:create_ms_user_id,
:update_ms_user_id
)
RETURNING action_code_history_id;
";

                this.delete_flag = false;
                this.create_ms_user_id = requser.ms_user_id;
                this.update_ms_user_id = requser.ms_user_id;

                //パラメータのADD
                plist.Add(new SqlParamData() { Name = "comment_item_id", Value = this.comment_item_id });
                plist.Add(new SqlParamData() { Name = "action_code_id", Value = this.action_code_id });
                plist.Add(new SqlParamData() { Name = "action_code_text", Value = this.action_code_text });
                plist.Add(new SqlParamData() { Name = "order_no", Value = this.order_no });

                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "create_ms_user_id", Value = this.create_ms_user_id });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });

                //実行
                ansid = (int)this.ExecuteScalar(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcActionCodeHistory InsertRecord", e);
            }

            return ansid;
        }



        /// <summary>
        /// レコードの更新
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="requser">更新ユーザー</param>
        /// <returns></returns>
        public override bool UpdateRecord(NpgsqlConnection cone, MsUser requser)
        {
            bool ret = false;
            List<SqlParamData> plist = new List<SqlParamData>();

            try
            {
                string sql = @"
UPDATE dc_action_code_history
SET
comment_item_id = :comment_item_id,
action_code_id = :action_code_id,
action_code_text = :action_code_text,
order_no = :order_no,

delete_flag = :delete_flag,
update_ms_user_id = :update_ms_user_id

WHERE
action_code_history_id  =  :action_code_history_id
";

                this.update_ms_user_id = requser.ms_user_id;

                //パラメータのADD
                plist.Add(new SqlParamData() { Name = "comment_item_id", Value = this.comment_item_id });
                plist.Add(new SqlParamData() { Name = "action_code_id", Value = this.action_code_id });
                
                plist.Add(new SqlParamData() { Name = "action_code_text", Value = this.action_code_text });
                plist.Add(new SqlParamData() { Name = "order_no", Value = this.order_no });

                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });


                plist.Add(new SqlParamData() { Name = "action_code_history_id", Value = this.action_code_history_id });

                //実行
                ret = this.ExecuteNonQuery(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcActionCodeHistory UpdateRecord", e);
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
                ret = this.ExecuteDelete(cone, requser, "dc_action_code_history", "action_code_history_id", this.action_code_history_id);
            }
            catch (Exception e)
            {
                throw new Exception("DcActionCodeHistory DeleteRecord", e);
            }

            return ret;
        }

    }
}
