using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;


namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// AccidentImportanceマスタ
    /// ms_accident_importance
    /// </summary>
    public class MsAccidentImportance : BaseDac
    {
        #region メンバ変数

		public override int ID
        {
            get
            {
                return this.accident_importance_id;
            }
        }
		
        /// <summary>
        /// AccidentImportanceID
        /// </summary>
        public int accident_importance_id = EVal;

        /// <summary>
        /// AccidentImportance名
        /// </summary>
        public string accident_importance_name = "";

        #endregion


        public override string ToString()
        {
            return this.accident_importance_name;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_accident_importance.accident_importance_id,
ms_accident_importance.accident_importance_name,

ms_accident_importance.delete_flag,
ms_accident_importance.create_ms_user_id,
ms_accident_importance.update_ms_user_id,
ms_accident_importance.create_date,
ms_accident_importance.update_date

FROM
ms_accident_importance
";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsAccidentImportance> GetRecords(NpgsqlConnection cone)
        {
            List<MsAccidentImportance> anslist = new List<MsAccidentImportance>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_accident_importance.delete_flag = false
";

                //取得
                anslist = GetRecordsList<MsAccidentImportance>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsAccidentImportance GetRecords", e);
            }

            return anslist;
        }
    }
}
