using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;



namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// Deficiencyカテゴリマスタ
    /// </summary>
    public class MsDeficiencyCategory : BaseDac
    {
        #region メンバ変数

        public override int ID
        {
            get
            {
                return this.deficiency_category_id;
            }
        }

        /// <summary>
        /// ID
        /// </summary>
        public int deficiency_category_id = EVal;

        /// <summary>
        /// 番号
        /// </summary>
        public string deficiency_category_no = "";

        /// <summary>
        /// 名前
        /// </summary>
        public string deficiency_category_name = "";

  
        #endregion


        public override string ToString()
        {
            return this.deficiency_category_no;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_deficiency_category.deficiency_category_id,
ms_deficiency_category.deficiency_category_no,
ms_deficiency_category.deficiency_category_name,

ms_deficiency_category.delete_flag,
ms_deficiency_category.create_ms_user_id,
ms_deficiency_category.update_ms_user_id,
ms_deficiency_category.create_date,
ms_deficiency_category.update_date

FROM
ms_deficiency_category

";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsDeficiencyCategory> GetRecords(NpgsqlConnection cone)
        {
            List<MsDeficiencyCategory> anslist = new List<MsDeficiencyCategory>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_deficiency_category.delete_flag = false

ORDER BY
ms_deficiency_category.deficiency_category_no
";

                //取得
                anslist = GetRecordsList<MsDeficiencyCategory>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsDeficiencyCategory GetRecords", e);
            }

            return anslist;
        }
    }
}
