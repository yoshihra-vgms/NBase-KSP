using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;

namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// スケジュール種別列挙  schedule_kind_idと連動
    /// </summary>
    public enum EScheduleKind
    {
        検船 = 1,
        SMS_ISSC,
        内部監査,
        入渠,

        DOC_NK審査,
        ISM,
        ISO,
        TMSA,



        //------------------------------------------
        MAX,

    }

    /// <summary>
    /// スケジュール種別マスタ
    /// ms_schedule_kind
    /// </summary>
    public class MsScheduleKind : BaseDac
    {
        #region メンバ変数
        public override int ID
        {
            get
            {
                return this.schedule_kind_id;
            }
        }


        /// <summary>
        /// ID
        /// </summary>
        public int schedule_kind_id = EVal;

        /// <summary>
        /// 親ID
        /// </summary>
        public int schedule_category_id = EVal;

        /// <summary>
        /// 名
        /// </summary>
        public string schedule_kind_name = "";

        #endregion


        public override string ToString()
        {
            return this.schedule_kind_name;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_schedule_kind.schedule_kind_id,
ms_schedule_kind.schedule_category_id,
ms_schedule_kind.schedule_kind_name,

ms_schedule_kind.delete_flag,
ms_schedule_kind.create_ms_user_id,
ms_schedule_kind.update_ms_user_id,
ms_schedule_kind.create_date,
ms_schedule_kind.update_date

FROM
ms_schedule_kind
";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsScheduleKind> GetRecords(NpgsqlConnection cone)
        {
            List<MsScheduleKind> anslist = new List<MsScheduleKind>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_schedule_kind.delete_flag = false
";

                //取得
                anslist = GetRecordsList<MsScheduleKind>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsScheduleKind GetRecords", e);
            }

            return anslist;
        }
    }
}
