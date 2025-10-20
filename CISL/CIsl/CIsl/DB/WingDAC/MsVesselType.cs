using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;

namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// 船種マスタ
    /// </summary>
    public class MsVesselType : BaseWingDac
    {
        #region メンバ変数
        /// <summary>
        /// 船種ID
        /// </summary>
        public string ms_vessel_type_id = "";

        /// <summary>
        /// 船種名
        /// </summary>
        public string vessel_type_name = "";

    

        #endregion

        public override string ToString()
        {
            return this.vessel_type_name;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_vessel_type.ms_vessel_type_id,
ms_vessel_type.vessel_type_name,
ms_vessel_type.renew_date,
ms_vessel_type.renew_user_id,
ms_vessel_type.ts
FROM
ms_vessel_type
";

        /// <summary>
        /// レコード取得
        /// </summary>
        /// <param name="cone"></param>
        /// <returns></returns>
        public static List<MsVesselType> GetRecords(NpgsqlConnection cone)
        {
            List<MsVesselType> anslist = new List<MsVesselType>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"

ORDER BY
ms_vessel_type.ms_vessel_type_id

";

                //取得
                anslist = GetRecordsList<MsVesselType>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsVesselType GetRecords", e);
            }

            return anslist;
        }
    }
}
