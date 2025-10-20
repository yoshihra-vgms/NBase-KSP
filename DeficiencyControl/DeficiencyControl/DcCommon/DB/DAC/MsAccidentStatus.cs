using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;
using CIsl.DB;

namespace DcCommon.DB.DAC
{
    /// <summary>
    /// MsAccidentStatus設定値
    /// </summary>
    public enum EAccidentStatus
    {
        Pending = 1,
        Complete,
    }

    /// <summary>
    /// Accident状態マスタ
    /// </summary>
    public class MsAccidentStatus : BaseDac
    {
        public MsAccidentStatus()
        {

        }


        #region メンバ変数

        public override int ID
        {
            get
            {
                return this.accident_status_id;
            }
        }

        /// <summary>
        /// ID
        /// </summary>
        public int accident_status_id = EVal;

        /// <summary>
        /// 名
        /// </summary>
        public string accident_status_name = "";

        #endregion


        public override string ToString()
        {
            return this.accident_status_name;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_accident_status.accident_status_id,
ms_accident_status.accident_status_name,

ms_accident_status.delete_flag,
ms_accident_status.create_ms_user_id,
ms_accident_status.update_ms_user_id,
ms_accident_status.create_date,
ms_accident_status.update_date

FROM
ms_accident_status

";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsAccidentStatus> GetRecords(NpgsqlConnection cone)
        {
            List<MsAccidentStatus> anslist = new List<MsAccidentStatus>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_accident_status.delete_flag = false

ORDER BY
ms_accident_status.accident_status_id
";

                //取得
                anslist = GetRecordsList<MsAccidentStatus>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsAccidentStatus GetRecords", e);
            }

            return anslist;
        }
    }
}
