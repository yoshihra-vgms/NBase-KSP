using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;


namespace CIsl.DB.WingDAC
{

    /// <summary>
    /// Accident種類マスタ
    /// </summary>
    public class MsAccidentKind : BaseDac
    {
        #region メンバ変数


        public override int ID
        {
            get
            {
                return this.accident_kind_id;
            }
        }


        /// <summary>
        /// Accident種類ID
        /// </summary>
        public int accident_kind_id = EVal;

        /// <summary>
        /// Accident種類名
        /// </summary>
        public string accident_kind_name = "";

        #endregion


        public override string ToString()
        {
            return this.accident_kind_name;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_accident_kind.accident_kind_id,
ms_accident_kind.accident_kind_name,

ms_accident_kind.delete_flag,
ms_accident_kind.create_ms_user_id,
ms_accident_kind.update_ms_user_id,
ms_accident_kind.create_date,
ms_accident_kind.update_date
FROM
ms_accident_kind

";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsAccidentKind> GetRecords(NpgsqlConnection cone)
        {
            List<MsAccidentKind> anslist = new List<MsAccidentKind>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_accident_kind.delete_flag = false

ORDER BY
ms_accident_kind.accident_kind_id
";

                //取得
                anslist = GetRecordsList<MsAccidentKind>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsAccidentKind GetRecords", e);
            }

            return anslist;
        }
    }
}
