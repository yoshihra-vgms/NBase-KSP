using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;


namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// 発生状況マスタ
    /// ms_accident_situation
    /// </summary>
    public class MsAccidentSituation : BaseDac
    {
        #region メンバ変数
        public override int ID
        {
            get
            {
                return this.accident_situation_id;
            }
        }


        /// <summary>
        /// ID
        /// </summary>
        public int accident_situation_id = EVal;

        /// <summary>
        /// 発生状況名
        /// </summary>
        public string accident_situation_name = "";

        #endregion


        public override string ToString()
        {
            return this.accident_situation_name;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_accident_situation.accident_situation_id,
ms_accident_situation.accident_situation_name,

ms_accident_situation.delete_flag,
ms_accident_situation.create_ms_user_id,
ms_accident_situation.update_ms_user_id,
ms_accident_situation.create_date,
ms_accident_situation.update_date
FROM
ms_accident_situation

";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsAccidentSituation> GetRecords(NpgsqlConnection cone)
        {
            List<MsAccidentSituation> anslist = new List<MsAccidentSituation>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_accident_situation.delete_flag = false

ORDER BY
ms_accident_situation.accident_situation_id
";

                //取得
                anslist = GetRecordsList<MsAccidentSituation>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsAccidentSituation GetRecords", e);
            }

            return anslist;
        }
    }
}
