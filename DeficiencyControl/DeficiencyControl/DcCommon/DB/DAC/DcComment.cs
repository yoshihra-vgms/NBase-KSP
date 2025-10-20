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
    /// コメントテーブル
    /// dc_comment
    /// </summary>
    public class DcComment : BaseDac
    {
        public override int ID
        {
            get
            {
                return DBTableID.dc_comment;
            }
        }

        #region メンバ変数

        /// <summary>
        /// コメントID
        /// </summary>
        public int comment_id = EVal;

        #endregion


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
dc_comment.comment_id,

dc_comment.delete_flag,
dc_comment.create_ms_user_id,
dc_comment.update_ms_user_id,
dc_comment.create_date,
dc_comment.update_date
FROM
dc_comment

";

        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <returns></returns>
        public static List<DcComment> GetRecords(NpgsqlConnection cone)
        {
            List<DcComment> anslist = new List<DcComment>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
dc_comment.delete_flag = false

";

                //取得
                anslist = GetRecordsList<DcComment>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("DcComment GetRecords", e);
            }

            return anslist;
        }
            

        /// <summary>
        /// 対象のcommentを取得
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="comment_id"></param>
        /// <returns></returns>
        public static DcComment GetRecordByCommentID(NpgsqlConnection cone, int comment_id)
        {
            DcComment ans = null;

            try
            {
                string sql = DefaultSelect + @"
WHERE
dc_comment.delete_flag = false
AND
dc_comment.comment_id = :comment_id
";

                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "comment_id", Value = comment_id });


                ans = GetRecordData<DcComment>(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcComment GetRecordByCommentID", e);
            }


            return ans;
        }


        /// <summary>
        /// 挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public override int InsertRecord(NpgsqlConnection cone, MsUser requser)
        {
            int ans = EVal;
            try
            {

                #region SQL
                string sql = @"
INSERT INTO dc_comment(

delete_flag,
create_ms_user_id,
update_ms_user_id

)
VALUES (

:delete_flag,
:create_ms_user_id,
:update_ms_user_id
)
RETURNING comment_id;
";
                #endregion

                //必要な値の設定
                this.delete_flag = false;
                this.create_ms_user_id = requser.ms_user_id;
                this.update_ms_user_id = requser.ms_user_id;

                //パラメータADD
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "create_ms_user_id", Value = this.create_ms_user_id });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });

                //SQL実行！
                object ret = this.ExecuteScalar(cone, sql, plist);
                ans = (int)ret;

            }
            catch (Exception e)
            {
                throw new Exception("DcComment InsertRecord", e);
            }

            return ans;
            
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
UPDATE
dc_comment
SET

delete_flag = :delete_flag,
update_ms_user_id = :update_ms_user_id

WHERE
comment_id = :comment_id
";

                this.update_ms_user_id = requser.ms_user_id;

                //パラメータのADD


                plist.Add(new SqlParamData() { Name = "comment_id", Value = this.comment_id });

                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });


                //実行
                ret = this.ExecuteNonQuery(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcComment UpdateRecord", e);
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
                ret = this.ExecuteDelete(cone, requser, "dc_comment", "comment_id", this.comment_id);
            }
            catch (Exception e)
            {
                throw new Exception("DcComment DeleteRecord", e);
            }

            return ret;
        }
    }
}
