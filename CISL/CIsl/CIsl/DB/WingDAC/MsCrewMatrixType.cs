using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;

namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// CrewMatrixマスタ
    /// ms_crew_matrix_type
    /// </summary>
    public class MsCrewMatrixType : BaseWingDac
    {
        #region メンバ変数

        /// <summary>
        /// ID
        /// </summary>
        public decimal ms_crew_matrix_type_id = EVal;

        /// <summary>
        /// 名前
        /// </summary>
        public string type_name = "";

        #endregion

        public override string ToString()
        {
            return this.type_name;
        }

        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_crew_matrix_type.ms_crew_matrix_type_id,
ms_crew_matrix_type.type_name,

ms_crew_matrix_type.delete_flag,
ms_crew_matrix_type.renew_date,
ms_crew_matrix_type.renew_user_id,
ms_crew_matrix_type.ts

FROM
ms_crew_matrix_type
";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <returns></returns>
        public static List<MsCrewMatrixType> GetRecords(NpgsqlConnection cone)
        {
            List<MsCrewMatrixType> anslist = new List<MsCrewMatrixType>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_crew_matrix_type.delete_flag = 0
";
                //取得
                anslist = GetRecordsList<MsCrewMatrixType>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsCrewMatrixType GetRecords", e);
            }

            return anslist;
        }

    }
}
