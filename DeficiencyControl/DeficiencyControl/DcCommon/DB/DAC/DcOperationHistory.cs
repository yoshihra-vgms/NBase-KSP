using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;
using CIsl.DB;
using DcCommon.DB.DAC.Search;
using CIsl.DB.WingDAC;

namespace DcCommon.DB.DAC
{
    /// <summary>
    /// ユーザー履歴
    /// </summary>
    public class DcOperationHistory : BaseDac
    {
        

        public override int ID
        {
            get
            {
                return this.dc_operation_history_id;
            }
        }


        #region メンバ変数
        /// <summary>
        /// ユーザー履歴ID
        /// </summary>
        public int dc_operation_history_id = EVal;


        /// <summary>
        /// 接続元
        /// </summary>
        public string host = "";

        /// <summary>
        /// UserID
        /// </summary>
        public string ms_user_id = "";

        /// <summary>
        /// 操作種別ID・・・現在このマスタは存在しません。必要に応じて作りましょう
        /// </summary>
        public int ms_user_operation_kind_id = EVal;


        /// <summary>
        /// 操作日付
        /// </summary>
        public DateTime date = EDate;


        public EUserOperationKind UserOperationKind
        {
            get
            {
                return (EUserOperationKind)this.ms_user_operation_kind_id;
            }
            set
            {
                this.ms_user_operation_kind_id = (int)value;
            }
        }


        #endregion

        /// <summary>
        /// 基本SELECT
        /// </summary>
        private static string DefaultSelect = @"
SELECT
dc_operation_history.dc_operation_history_id,
dc_operation_history.ms_user_id,
dc_operation_history.host,
dc_operation_history.ms_user_operation_kind_id,
dc_operation_history.date,

dc_operation_history.delete_flag,
dc_operation_history.create_ms_user_id,
dc_operation_history.update_ms_user_id,
dc_operation_history.create_date,
dc_operation_history.update_date

FROM
dc_operation_history
";




        /// <summary>
        /// レコード挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public override int InsertRecord(NpgsqlConnection cone, MsUser requser)
        {
            #region SQL
            string sql = @"
INSERT INTO dc_operation_history(
ms_user_id,
host,
ms_user_operation_kind_id,
date,

delete_flag,
create_ms_user_id,
update_ms_user_id

) VALUES (
:ms_user_id,
:host,
:ms_user_operation_kind_id,
now(),

:delete_flag,
:create_ms_user_id,
:update_ms_user_id
)
RETURNING dc_operation_history_id;
";
            #endregion

            int ans = EVal;
            try
            {
                this.delete_flag = false;
                this.create_ms_user_id = requser.ms_user_id;
                this.update_ms_user_id = requser.ms_user_id;

                List<SqlParamData> plist = new List<SqlParamData>();

                plist.Add(new SqlParamData() { Name = "ms_user_id", Value = this.ms_user_id });
                plist.Add(new SqlParamData() { Name = "host", Value = this.host });
                plist.Add(new SqlParamData() { Name = "ms_user_operation_kind_id", Value = this.ms_user_operation_kind_id });

                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "create_ms_user_id", Value = this.create_ms_user_id });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });

                //実行
                ans = (int)this.ExecuteScalar(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcOperationHistory InsertRecord", e);
            }

            return ans;
        }

    }
}
