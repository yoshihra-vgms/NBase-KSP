using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;

namespace CIsl.DB.WingDAC
{
    /// <summary>
    ///ViqCodeマスタ
    /// </summary>
    public class MsViqCode : BaseDac
    {
        #region メンバ変数


        public override int ID
        {
            get
            {
                return this.viq_code_id;
            }
        }


        /// <summary>
        /// VIQCode ID
        /// </summary>
        public int viq_code_id = EVal;

        /// <summary>
        /// VIQ CodeNameID
        /// </summary>
        public int viq_code_name_id = EVal;

        /// <summary>
        /// VIQ Version ID
        /// </summary>
        public int viq_version_id = EVal;

        /// <summary>
        /// Code
        /// </summary>
        public string viq_code = "";

        /// <summary>
        /// 説明
        /// </summary>
        public string description = "";

        /// <summary>
        /// 説明英語
        /// </summary>
        public string description_eng = "";

        /// <summary>
        /// 順序
        /// </summary>
        public int order_no = EVal;
        #endregion


        public override string ToString()
        {
            return this.viq_code;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_viq_code.viq_code_id,
ms_viq_code.viq_code_name_id,
ms_viq_code.viq_version_id,
ms_viq_code.viq_code,
ms_viq_code.description,
ms_viq_code.description_eng,
ms_viq_code.order_no,

ms_viq_code.delete_flag,
ms_viq_code.create_ms_user_id,
ms_viq_code.update_ms_user_id,
ms_viq_code.create_date,
ms_viq_code.update_date
FROM
ms_viq_code


";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsViqCode> GetRecords(NpgsqlConnection cone)
        {
            List<MsViqCode> anslist = new List<MsViqCode>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_viq_code.delete_flag = false

ORDER BY
ms_viq_code.order_no
";

                //取得
                anslist = GetRecordsList<MsViqCode>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsViqCode GetRecords", e);
            }

            return anslist;
        }
    }
}
