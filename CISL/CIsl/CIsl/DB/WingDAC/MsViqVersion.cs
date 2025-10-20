using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;

namespace CIsl.DB.WingDAC
{
    /// <summary>
    ///VIQ Versionマスタ
    /// </summary>
    public class MsViqVersion : BaseDac
    {
        #region メンバ変数


        public override int ID
        {
            get
            {
                return this.viq_version_id;
            }
        }


        /// <summary>
        /// VIQ Version ID
        /// </summary>
        public int viq_version_id = EVal;

        /// <summary>
        /// Version
        /// </summary>
        public string viq_version = "";

        /// <summary>
        /// Versionの開始日
        /// </summary>
        public DateTime start_date = EDate;

        /// <summary>
        /// Versionの終了日
        /// </summary>
        public DateTime end_date = EDate;

        #endregion


        public override string ToString()
        {
            return this.viq_version;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_viq_version.viq_version_id,
ms_viq_version.viq_version,
ms_viq_version.start_date,
ms_viq_version.end_date,

ms_viq_version.delete_flag,
ms_viq_version.create_ms_user_id,
ms_viq_version.update_ms_user_id,
ms_viq_version.create_date,
ms_viq_version.update_date

FROM
ms_viq_version

";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsViqVersion> GetRecords(NpgsqlConnection cone)
        {
            List<MsViqVersion> anslist = new List<MsViqVersion>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_viq_version.delete_flag = false

ORDER BY
ms_viq_version.viq_version_id
";

                //取得
                anslist = GetRecordsList<MsViqVersion>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsViqVersion GetRecords", e);
            }

            return anslist;
        }
    }
}
