using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;


namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// Deficieny codeマスタ
    /// ms_deficiency_code
    /// </summary>
    public class MsDeficiencyCode : BaseDac
    {
        #region メンバ変数

        /// <summary>
        /// コードID
        /// </summary>
        public int deficiency_code_id = EVal;

        /// <summary>
        /// ステータス名
        /// </summary>
        public string deficiency_code_name = "";

        /// <summary>
        /// Defective Item
        /// </summary>
        public string defective_item = "";

        /// <summary>
        /// カテゴリID
        /// </summary>
        public int deficiency_category_id = EVal;
        #endregion


        public override string ToString()
        {
            return this.deficiency_code_name;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_deficiency_code.deficiency_code_id,
ms_deficiency_code.deficiency_code_name,
ms_deficiency_code.defective_item,
ms_deficiency_code.deficiency_category_id,
ms_deficiency_code.delete_flag,
ms_deficiency_code.create_ms_user_id,
ms_deficiency_code.update_ms_user_id,
ms_deficiency_code.create_date,
ms_deficiency_code.update_date
FROM
ms_deficiency_code
";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsDeficiencyCode> GetRecords(NpgsqlConnection cone)
        {
            List<MsDeficiencyCode> anslist = new List<MsDeficiencyCode>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_deficiency_code.delete_flag = false
ORDER BY
ms_deficiency_code.deficiency_code_name
";

                //取得
                anslist = GetRecordsList<MsDeficiencyCode>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsDeficiencyCode GetRecords", e);
            }

            return anslist;
        }


        /// <summary>
        /// 対象カテゴリのデータを取得する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="deficiency_category_id"></param>
        /// <returns></returns>
        public static List<MsDeficiencyCode> GetRecordsByDeficiencyCategoryID(NpgsqlConnection cone, int deficiency_category_id)
        {
            List<MsDeficiencyCode> anslist = new List<MsDeficiencyCode>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_deficiency_code.delete_flag = false
AND
ms_deficiency_code.deficiency_category_id = :deficiency_category_id

ORDER BY
ms_deficiency_code.deficiency_code_name
";

                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "deficiency_category_id", Value = deficiency_category_id });

                //取得
                anslist = GetRecordsList<MsDeficiencyCode>(cone, sql, plist);
            }
            catch (Exception e)
            {
                throw new Exception("GetRecordsByDeficiencyCategoryID GetRecords", e);
            }

            return anslist;
        }
    }
}
