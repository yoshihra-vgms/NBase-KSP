using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;

namespace CIsl.DB.WingDAC
{
    /// <summary>
    ///ViqCode名前マスタ
    /// </summary>
    public class MsViqCodeName : BaseDac
    {
        #region メンバ変数


        public override int ID
        {
            get
            {
                return this.viq_code_name_id;
            }
        }


        /// <summary>
        /// VIQCode ID
        /// </summary>
        public int viq_code_name_id = EVal;

        /// <summary>
        /// Code
        /// </summary>
        public string viq_code_name = "";

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
            return this.viq_code_name;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_viq_code_name.viq_code_name_id,
ms_viq_code_name.viq_code_name,
ms_viq_code_name.description,
ms_viq_code_name.description_eng,
ms_viq_code_name.order_no,
ms_viq_code_name.delete_flag,
ms_viq_code_name.create_ms_user_id,
ms_viq_code_name.update_ms_user_id,
ms_viq_code_name.create_date,
ms_viq_code_name.update_date

FROM
ms_viq_code_name

";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsViqCodeName> GetRecords(NpgsqlConnection cone)
        {
            List<MsViqCodeName> anslist = new List<MsViqCodeName>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_viq_code_name.delete_flag = false

ORDER BY
ms_viq_code_name.order_no
";

                //取得
                anslist = GetRecordsList<MsViqCodeName>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsViqCodeName GetRecords", e);
            }

            return anslist;
        }
    }
}
