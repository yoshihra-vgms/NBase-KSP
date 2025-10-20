using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Npgsql;
using CIsl.DB;


namespace DcCommon.DB.DAC
{
    /// <summary>
    /// 年度マスタ これは単に検索条件 年度IDは使用することは無いはず。　絶対日から検索によって年度を判断せよ
    /// ms_year
    /// </summary>
    public class MsYear : BaseDac
    {
        #region メンバ変数
        public override int ID
        {
            get
            {
                return this.year_id;
            }
        }


        /// <summary>
        /// 年度ID
        /// </summary>
        public int year_id = EVal;

        /// <summary>
        /// 年度
        /// </summary>
        public int year = EVal;

        /// <summary>
        /// 年度開始日
        /// </summary>
        public DateTime start_date = EDate;

        /// <summary>
        /// 年度終了日
        /// </summary>
        public DateTime end_date = EDate;
        #endregion


        public override string ToString()
        {
            string ans = string.Format("{0}年度", this.year);
            return ans;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_year.year_id,
ms_year.year,
ms_year.start_date,
ms_year.end_date,

ms_year.delete_flag,
ms_year.create_ms_user_id,
ms_year.update_ms_user_id,
ms_year.create_date,
ms_year.update_date

FROM
ms_year
";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsYear> GetRecords(NpgsqlConnection cone)
        {
            List<MsYear> anslist = new List<MsYear>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_year.delete_flag = false

ORDER BY
ms_year.year
";

                //取得
                anslist = GetRecordsList<MsYear>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsYear GetRecords", e);
            }

            return anslist;
        }
    }
}
