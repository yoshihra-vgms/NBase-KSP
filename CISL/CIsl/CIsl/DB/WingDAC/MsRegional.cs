using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;

namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// 国マスタ Nation
    /// ms_regional
    /// </summary>
    public class MsRegional : BaseWingDac
    {
        #region メンバ変数
        /// <summary>
        /// 国コード
        /// </summary>
        public string ms_regional_code = "";

        /// <summary>
        /// 国名
        /// </summary>
        public string regional_name = "";        

        /// <summary>
        /// ?
        /// </summary>
        public decimal send_flag = EVal;

        /// <summary>
        /// ?
        /// </summary>
        public decimal vessel_id = EVal;


        /// <summary>
        /// ?
        /// </summary>
        public decimal data_no = EVal;

        /// <summary>
        /// ?
        /// </summary>
        public string user_key = "";        

   

        #endregion


        public override string ToString()
        {
            return this.regional_name;
        }

        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_regional.ms_regional_code,
ms_regional.regional_name,
ms_regional.delete_flag,
ms_regional.send_flag,
ms_regional.vessel_id,
ms_regional.data_no,
ms_regional.user_key,
ms_regional.renew_date,
ms_regional.renew_user_id,
ms_regional.ts
FROM
ms_regional
";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <returns></returns>
        public static List<MsRegional> GetRecords(NpgsqlConnection cone)
        {
            List<MsRegional> anslist = new List<MsRegional>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_regional.delete_flag = 0

";

                //取得
                anslist = GetRecordsList<MsRegional>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsRegional GetRecords", e);
            }

            return anslist;
        }
    }
}
