using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CIsl.DB;
using CIsl.DB.WingDAC;


namespace DcCommon.DB.DAC
{

    /// <summary>
    /// 検船指摘事項添付ファイルテーブル
    /// </summary>
    public class DcMoiObservationAttachment : BaseDac
    {
        public override int TableID
        {
            get
            {
                return DBTableID.dc_moi_observation_attachment;
            }
        }


        #region メンバ変数

        /// <summary>
        /// 検船指摘事項 ID
        /// </summary>
        public int moi_observation_id = EVal;

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
INSERT INTO dc_moi_observation_attachment(
moi_observation_id,
attachment_id,

delete_flag,
create_ms_user_id,
update_ms_user_id
)
VALUES
(
:moi_observation_id,
:attachment_id,

:delete_flag,
:create_ms_user_id,
:update_ms_user_id
)
RETURNING moi_observation_id;
";
                #endregion

                //必要な値の設定
                this.delete_flag = false;
                this.create_ms_user_id = requser.ms_user_id;
                this.update_ms_user_id = requser.ms_user_id;

                //パラメータADD
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "moi_observation_id", Value = this.moi_observation_id });
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
                throw new Exception("DcMoiObservationAttachment InsertRecord", e);
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
                ret = this.ExecuteDelete(cone, requser, "dc_moi_observation_attachment", "moi_observation_id", this.moi_observation_id, "attachment_id", this.attachment_id);
            }
            catch (Exception e)
            {
                throw new Exception("DcMoiObservationAttachment DeleteRecord", e);
            }

            return ret;
        }
    }
}
