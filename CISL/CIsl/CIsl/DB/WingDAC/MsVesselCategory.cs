using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;

namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// 船カテゴリマスタ
    /// ms_vessel_category
    /// </summary>
    public class MsVesselCategory : BaseWingDac
    {
        #region メンバ変数
        /// <summary>
        /// 場所ID
        /// </summary>
        public string ms_vessel_category_id = "";

        /// <summary>
        /// 名前
        /// </summary>
        public string vessel_category_name = "";


        #endregion


        public override string ToString()
        {
            return this.vessel_category_name;
        }

        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_vessel_category.ms_vessel_category_id,
ms_vessel_category.vessel_category_name,

ms_vessel_category.delete_flag,
ms_vessel_category.renew_date,
ms_vessel_category.renew_user_id,
ms_vessel_category.ts

FROM
ms_vessel_category

";



        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <returns></returns>
        public static List<MsVesselCategory> GetRecords(NpgsqlConnection cone)
        {
            List<MsVesselCategory> anslist = new List<MsVesselCategory>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_vessel_category.delete_flag = 0

ORDER BY
ms_vessel_category.ms_vessel_category_id
";

                //取得
                anslist = GetRecordsList<MsVesselCategory>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsVesselCategory GetRecords", e);
            }

            return anslist;
        }
    }
}
