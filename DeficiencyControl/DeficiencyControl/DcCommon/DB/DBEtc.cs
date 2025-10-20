using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;
using DcCommon.DB.DAC;
using CIsl.DB;

namespace DcCommon.DB
{
    public class DBTableID
    {
        public const int none = -1;

        public const int dc_comment = 1;
        public const int dc_comment_item = 2;
        public const int dc_ci_psc_inspection = 3;
        public const int dc_attachment = 4;
        public const int dc_comment_attachment = 5;
        public const int dc_comment_item_attachment = 6;
        public const int dc_action_code_history = 7;
        public const int dc_accident = 8;
        public const int dc_accident_attachment = 9;
        public const int dc_accident_progress = 10;
        public const int dc_accident_progress_attachment = 11;
        public const int dc_accident_reports = 12;
        public const int dc_accident_reports_attachment = 13;


        public const int dc_moi = 14;
        public const int dc_moi_attachment = 15;
        public const int dc_moi_observation = 16;
        public const int dc_moi_observation_attachment = 17;


        public const int dc_schedule = 18;
        public const int dc_schedule_plan = 19;
        public const int dc_schedule_company = 20;
        public const int dc_schedule_other = 21;

        /*
        public const int dc_comment = 1;
        public const int dc_comment_item = 2;
        public const int dc_ci_psc_inspection = 3;
        public const int dc_progress = 4;
        public const int dc_progress_pending = 5;
        public const int dc_attachment = 6;
        public const int dc_comment_item_attachment = 7;
        public const int dc_progress_attachment = 8;
        public const int dc_alarm = 9;
        public const int dc_alarm_info = 10;
        public const int dc_action_code_history = 11;
        public const int dc_sire_member_reporting = 12;
        public const int dc_ci_class_audit = 13;
        public const int dc_ci_internal_audit = 14;
        public const int dc_ci_terminal_inspection = 15;
        public const int dc_terminal_history = 16;
        public const int dc_terminal_result = 17;
        public const int dc_ci_flag_inspection = 18;
        public const int dc_sire_member_reporting_attachment = 19;
        public const int dc_ocimf_download_by = 20;
        public const int dc_ci_navigational_audit = 21;

        public const int dc_plan = 22;

        public const int dc_comment_attachment = 23;

        public const int dc_insurance = 24;
        public const int dc_insurance_progress = 25;
        public const int dc_insurance_progress_pending = 26;
        public const int dc_insurance_attachment = 27;
        public const int dc_insurance_progress_attachment = 28;
        public const int dc_si_memo = 29;
        public const int dc_si_memo_progress = 30;
        public const int dc_si_memo_progress_pending = 31;
        public const int dc_si_memo_attachment = 32;
        public const int dc_si_memo_progress_attachment = 33;
        public const int dc_defect = 34;
        public const int dc_defect_progress = 35;
        public const int dc_defect_progress_pending = 36;
        public const int dc_defect_attachment = 37;
        public const int dc_defect_progress_attachment = 38;
        public const int dc_accident_report = 39;
        public const int dc_accident_report_ocimf_download_by = 40;
        public const int dc_accident_report_progress = 41;
        public const int dc_accident_report_progress_pending = 42;
        public const int dc_accident_report_attachment = 43;
        public const int dc_accident_report_progress_attachment = 44;
        public const int dc_ci_non_conformity = 45;
        public const int dc_insurance_cargo = 46;
        public const int dc_accident_report_sire_member_reporting = 47;
        public const int dc_accident_report_sire_member_reporting_attachment = 48;
        public const int dc_physical_condition = 49;
        public const int dc_physical_condition_detail = 50;
        public const int dc_physical_condition_attachment = 51;*/


    }

    /// <summary>
    /// DBの汎用クラス
    /// </summary>
    public class DBEtc
    {
        /// <summary>
        /// サーバーの日付を取得
        /// </summary>
        /// <returns></returns>
        public static DateTime CheckServerDate(DBConnect cone)
        {
            DateTime ans = BaseDac.EDate;

            string sql = "SELECT now() as date";

            using (Npgsql.NpgsqlCommand com = new Npgsql.NpgsqlCommand(sql, cone.DBCone))
            {
                using (Npgsql.NpgsqlDataReader dr = com.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ans = (DateTime)dr[0];
                        break;
                    }
                }
            }

            return ans;

        }

        
    }



    
}
