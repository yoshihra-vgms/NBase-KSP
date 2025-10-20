using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CIsl.DB;
using CIsl.DB.WingDAC;

namespace DcCommon.DB.DAC
{
    

    /// <summary>
    /// コメント添付ファイルテーブル
    /// dc_comment_item_attachment
    /// </summary>
    public class DcCommentItemAttachment : BaseDac
    {
        public override int TableID
        {
            get
            {
                return DBTableID.dc_comment_item_attachment;
            }
        }

        #region メンバ変数

        /// <summary>
        /// コメントアイテムID
        /// </summary>
        public int comment_item_id = EVal;

        /// <summary>
        /// 添付ファイルID
        /// </summary>
        public int attachment_id = EVal;

        #endregion


        /// <summary>
        /// レコード挿入
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="requser">ユーザー</param>
        /// <returns></returns>
        public override int InsertRecord(Npgsql.NpgsqlConnection cone, MsUser requser)
        {
            int ans = 0;
            try
            {

                #region SQL
                string sql = @"
INSERT INTO dc_comment_item_attachment(
comment_item_id,
attachment_id,

delete_flag,
create_ms_user_id,
update_ms_user_id
)
VALUES
(
:comment_item_id,
:attachment_id,

:delete_flag,
:create_ms_user_id,
:update_ms_user_id
)
RETURNING comment_item_id;
";
                #endregion

                //必要な値の設定
                this.delete_flag = false;
                this.create_ms_user_id = requser.ms_user_id;
                this.update_ms_user_id = requser.ms_user_id;

                //パラメータADD
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "comment_item_id", Value = this.comment_item_id });
                plist.Add(new SqlParamData() { Name = "attachment_id", Value = this.attachment_id });
                

                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "create_ms_user_id", Value = this.create_ms_user_id });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });

                //SQL実行！
                object ret = this.ExecuteScalar(cone, sql, plist);
                ans = (int)ret;

            }
            catch (Exception e)
            {
                throw new Exception("DcCommentItemAttachment InsertRecord", e);
            }

            return ans;
        }


        /// <summary>
        /// レコード削除
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public override bool DeleteRecord(Npgsql.NpgsqlConnection cone, MsUser requser)
        {
            bool ret = false;
            try
            {
                ret = this.ExecuteDelete(cone, requser, "dc_comment_item_attachment", "comment_item_id", this.comment_item_id, "attachment_id", this.attachment_id);
            }
            catch (Exception e)
            {
                throw new Exception("DcCommentItemAttachment DeleteRecord", e);
            }

            return ret;
        }
    }
}
