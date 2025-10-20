using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CIsl.DB;
using CIsl.DB.WingDAC;

namespace DcCommon.DB.DAC
{
    /// <summary>
    /// 事故トラブル進捗添付ファイル
    /// dc_accident_progress_attachment
    /// </summary>
    public class DcAccidentProgressAttachment : BaseDac
    {
        public override int TableID
        {
            get
            {
                return DBTableID.dc_accident_progress_attachment;
            }
        }

        #region メンバ変数

        /// <summary>
        /// Accident進捗ID
        /// </summary>
        public int accident_progress_id = EVal;

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
INSERT INTO dc_accident_progress_attachment(
accident_progress_id,
attachment_id,

delete_flag,
create_ms_user_id,
update_ms_user_id
)
VALUES
(
:accident_progress_id,
:attachment_id,

:delete_flag,
:create_ms_user_id,
:update_ms_user_id
)
RETURNING accident_progress_id;
";
                #endregion

                //必要な値の設定
                this.delete_flag = false;
                this.create_ms_user_id = requser.ms_user_id;
                this.update_ms_user_id = requser.ms_user_id;

                //パラメータADD
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "accident_progress_id", Value = this.accident_progress_id });
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
                throw new Exception("DcAccidentProgressAttachment InsertRecord", e);
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
                ret = this.ExecuteDelete(cone, requser, "dc_accident_progress_attachment", "accident_progress_id", this.accident_progress_id, "attachment_id", this.attachment_id);
            }
            catch (Exception e)
            {
                throw new Exception("DcAccidentProgressAttachment DeleteRecord", e);
            }

            return ret;
        }
    }
}
