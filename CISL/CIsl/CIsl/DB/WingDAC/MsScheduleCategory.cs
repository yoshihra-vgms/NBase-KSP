using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;


namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// スケジュール区分 schedule_category_idと一致する
    /// </summary>
    public enum EScheduleCategory
    {
        予定実績 = 1,
        会社,
        その他,



        MAX

    }

    /// <summary>
    /// スケージュール区分マスタ
    /// ms_schedule_category

    /// </summary>
    public class MsScheduleCategory : BaseDac
    {
        #region メンバ変数
        public override int ID
        {
            get
            {
                return this.schedule_category_id;
            }
        }


        /// <summary>
        /// ID
        /// </summary>
        public int schedule_category_id = EVal;

        /// <summary>
        /// 名
        /// </summary>
        public string schedule_category_name = "";

        #endregion


        public override string ToString()
        {
            return this.schedule_category_name;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_schedule_category.schedule_category_id,
ms_schedule_category.schedule_category_name,

ms_schedule_category.delete_flag,
ms_schedule_category.create_ms_user_id,
ms_schedule_category.update_ms_user_id,
ms_schedule_category.create_date,
ms_schedule_category.update_date

FROM
ms_schedule_category
";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsScheduleCategory> GetRecords(NpgsqlConnection cone)
        {
            List<MsScheduleCategory> anslist = new List<MsScheduleCategory>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_schedule_category.delete_flag = false
";

                //取得
                anslist = GetRecordsList<MsScheduleCategory>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsScheduleCategory GetRecords", e);
            }

            return anslist;
        }
    }
}
