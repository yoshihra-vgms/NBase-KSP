using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;
using CIsl.DB;


namespace DcCommon.DB.DAC
{
    /// <summary>
    /// 検船状態定義
    /// </summary>
    public enum EMoiStatus
    {
        Pending = 1,
        Complete,
    }

    /// <summary>
    /// 検船状態マスタ
    /// </summary>
    public class MsMoiStatus : BaseDac
    {
        #region メンバ変数


        public override int ID
        {
            get
            {
                return this.moi_status_id;
            }
        }


        /// <summary>
        /// 検船状態ID
        /// </summary>
        public int moi_status_id = EVal;

        /// <summary>
        /// 検船状態名
        /// </summary>
        public string moi_status_name = "";

        #endregion


        public override string ToString()
        {
            return this.moi_status_name;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_moi_status.moi_status_id,
ms_moi_status.moi_status_name,

ms_moi_status.delete_flag,
ms_moi_status.create_ms_user_id,
ms_moi_status.update_ms_user_id,
ms_moi_status.create_date,
ms_moi_status.update_date
FROM
ms_moi_status


";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsMoiStatus> GetRecords(NpgsqlConnection cone)
        {
            List<MsMoiStatus> anslist = new List<MsMoiStatus>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_moi_status.delete_flag = false

ORDER BY
ms_moi_status.moi_status_id
";

                //取得
                anslist = GetRecordsList<MsMoiStatus>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsMoiStatus GetRecords", e);
            }

            return anslist;
        }
    }
}
