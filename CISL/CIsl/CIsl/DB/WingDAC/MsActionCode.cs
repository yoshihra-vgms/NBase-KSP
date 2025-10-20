using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;

namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// ActionCodeマスタ
    /// ms_action_code
    /// </summary>
    public class MsActionCode : BaseDac
    {
        #region メンバ変数

        /// <summary>
        /// ActionCodeID
        /// </summary>
        public int action_code_id = EVal;

        /// <summary>
        /// ActionCode名
        /// </summary>
        public string action_code_name = "";


        /// <summary>
        /// アクションコードテキスト
        /// </summary>
        public string action_code_text = "";


        #endregion


        public override string ToString()
        {
            return this.action_code_name;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_action_code.action_code_id,
ms_action_code.action_code_name,
ms_action_code.action_code_text,

ms_action_code.delete_flag,
ms_action_code.create_ms_user_id,
ms_action_code.update_ms_user_id,
ms_action_code.create_date,
ms_action_code.update_date

FROM
ms_action_code
";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsActionCode> GetRecords(NpgsqlConnection cone)
        {
            List<MsActionCode> anslist = new List<MsActionCode>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_action_code.delete_flag = false

ORDER BY
ms_action_code.action_code_id
";

                //取得
                anslist = GetRecordsList<MsActionCode>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsActionCode GetRecords", e);
            }

            return anslist;
        }
    }
}
