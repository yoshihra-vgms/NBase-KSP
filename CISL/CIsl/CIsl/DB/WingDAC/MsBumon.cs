using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;

namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// 部門マスタ 
    /// ms_bumon
    /// </summary>
    public class MsBumon : BaseWingDac
    {
        #region メンバ変数


        public string ms_bumon_id = "";
        public string bumon_name = "";
        public decimal send_flag = EVal;
        public decimal vessel_id = EVal;
        public decimal data_no = EVal;
        public string user_key = "";
        #endregion

        public override string ToString()
        {
            return this.bumon_name;
        }
        
        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_bumon.ms_bumon_id,
ms_bumon.bumon_name,
ms_bumon.send_flag,
ms_bumon.vessel_id,
ms_bumon.data_no,
ms_bumon.user_key,
ms_bumon.renew_date,
ms_bumon.renew_user_id,
ms_bumon.ts
FROM
ms_bumon

";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <returns></returns>
        public static List<MsBumon> GetRecords(NpgsqlConnection cone)
        {
            List<MsBumon> anslist = new List<MsBumon>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"


";

                //取得
                anslist = GetRecordsList<MsBumon>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsBumon GetRecords", e);
            }

            return anslist;
        }


        /// <summary>
        /// 対象ユーザーの部門を取得する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="ms_user_id"></param>
        /// <returns></returns>
        public static MsBumon GetRecordByMsUserID(NpgsqlConnection cone, string ms_user_id)
        {
            MsBumon ans = null;

            try
            {
                //SQ
                string sql = DefaultSelect + @"
LEFT JOIN ms_user_bumon ON ms_user_bumon.ms_bumon_id = ms_bumon.ms_bumon_id

WHERE 
ms_user_bumon.ms_user_id = :ms_user_id
";

                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "ms_user_id", Value = ms_user_id });

                //取得
                ans = GetRecordData<MsBumon>(cone, sql, plist);
            }
            catch (Exception e)
            {
                throw new Exception("MsBumon GetRecordByMsUserID", e);
            }

            return ans;
        }
    }
}
