using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;

namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// VIQ Noマスタ
    /// </summary>
    public class MsViqNo : BaseDac
    {
        #region メンバ変数


        public override int ID
        {
            get
            {
                return this.viq_no_id;
            }
        }


        /// <summary>
        /// Viq No ID
        /// </summary>
        public int viq_no_id = EVal;

        /// <summary>
        /// 親VIQ codeID
        /// </summary>
        public int viq_code_id = EVal;

        /// <summary>
        /// VIQ No
        /// </summary>
        public string viq_no = "";


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
            return this.viq_no;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_viq_no.viq_no_id,
ms_viq_no.viq_code_id,
ms_viq_no.viq_no,
ms_viq_no.description,
ms_viq_no.description_eng,
ms_viq_no.order_no,

ms_viq_no.delete_flag,
ms_viq_no.create_ms_user_id,
ms_viq_no.update_ms_user_id,
ms_viq_no.create_date,
ms_viq_no.update_date

FROM
ms_viq_no
";




        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone"></param>
        /// <returns></returns>
        public static List<MsViqNo> GetRecords(NpgsqlConnection cone)
        {
            List<MsViqNo> anslist = new List<MsViqNo>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_viq_no.delete_flag = false
ORDER BY
ms_viq_no.order_no
";

                //取得
                anslist = GetRecordsList<MsViqNo>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsViqNo GetRecords", e);
            }

            return anslist;
        }



        /// <summary>
        /// 対象のCodeに関するものを一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsViqNo> GetRecordsByViqCodeID(NpgsqlConnection cone, int viq_code_id)
        {
            List<MsViqNo> anslist = new List<MsViqNo>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_viq_no.delete_flag = false
AND
ms_viq_no.viq_code_id = :viq_code_id
ORDER BY
ms_viq_no.order_no
";

                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "viq_code_id", Value = viq_code_id });



                //取得
                anslist = GetRecordsList<MsViqNo>(cone, sql, plist);
            }
            catch (Exception e)
            {
                throw new Exception("MsViqNo GetRecords", e);
            }

            return anslist;
        }
    }
}
