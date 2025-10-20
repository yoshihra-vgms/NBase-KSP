using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;


namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// KindofAccidentマスタ
    /// ms_kind_of_accident
    /// </summary>
    public class MsKindOfAccident : BaseDac
    {
        #region メンバ変数
        public override int ID
        {
            get
            {
                return this.kind_of_accident_id;
            }
        }


        /// <summary>
        /// KindofAccidentID
        /// </summary>
        public int kind_of_accident_id = EVal;

        /// <summary>
        /// KindofAccident名
        /// </summary>
        public string kind_of_accident_name = "";

        #endregion


        public override string ToString()
        {
            return this.kind_of_accident_name;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_kind_of_accident.kind_of_accident_id,
ms_kind_of_accident.kind_of_accident_name,

ms_kind_of_accident.delete_flag,
ms_kind_of_accident.create_ms_user_id,
ms_kind_of_accident.update_ms_user_id,
ms_kind_of_accident.create_date,
ms_kind_of_accident.update_date

FROM
ms_kind_of_accident
";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsKindOfAccident> GetRecords(NpgsqlConnection cone)
        {
            List<MsKindOfAccident> anslist = new List<MsKindOfAccident>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_kind_of_accident.delete_flag = false
";

                //取得
                anslist = GetRecordsList<MsKindOfAccident>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsKindOfAccident GetRecords", e);
            }

            return anslist;
        }
    }
}
