using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;


namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// 検船種別マスタ
    /// </summary>
    public class MsInspectionCategory : BaseDac
    {
        #region メンバ変数


        public override int ID
        {
            get
            {
                return this.inspection_category_id;
            }
        }


        /// <summary>
        /// 検船種別ID
        /// </summary>
        public int inspection_category_id = EVal;

        /// <summary>
        /// 検船種別名
        /// </summary>
        public string inspection_category_name = "";

        #endregion


        public override string ToString()
        {
            return this.inspection_category_name;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_inspection_category.inspection_category_id,
ms_inspection_category.inspection_category_name,

ms_inspection_category.delete_flag,
ms_inspection_category.create_ms_user_id,
ms_inspection_category.update_ms_user_id,
ms_inspection_category.create_date,
ms_inspection_category.update_date

FROM
ms_inspection_category
";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsInspectionCategory> GetRecords(NpgsqlConnection cone)
        {
            List<MsInspectionCategory> anslist = new List<MsInspectionCategory>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_inspection_category.delete_flag = false

ORDER BY
ms_inspection_category.inspection_category_id
";

                //取得
                anslist = GetRecordsList<MsInspectionCategory>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsInspectionCategory GetRecords", e);
            }

            return anslist;
        }
    }
}
