using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;


namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// 船種別
    /// ms_vessel_kind
    /// </summary>
    public class MsVesselKind : BaseWingDac
    {
        #region メンバ変数
        //とりあえず必要そうなものだけ
        /// <summary>
        /// 種別ID
        /// </summary>
        public string ms_vessel_kind_id = "";

        /// <summary>
        /// 名称
        /// </summary>
        public string vessel_kind_name = "";



        #endregion


        public override string ToString()
        {
            return this.vessel_kind_name;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_vessel_kind.ms_vessel_kind_id,
ms_vessel_kind.vessel_kind_name,

ms_vessel_kind.delete_flag,
ms_vessel_kind.renew_date,
ms_vessel_kind.renew_user_id,
ms_vessel_kind.ts

FROM
ms_vessel_kind
";


        /// <summary>
        /// レコード取得
        /// </summary>
        /// <param name="cone"></param>
        /// <returns></returns>
        public static List<MsVesselKind> GetRecords(NpgsqlConnection cone)
        {
            List<MsVesselKind> anslist = new List<MsVesselKind>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_vessel_kind.delete_flag = 0
";

                //取得
                anslist = GetRecordsList<MsVesselKind>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsVesselKind GetRecords", e);
            }

            return anslist;
        }



        

    }
}
