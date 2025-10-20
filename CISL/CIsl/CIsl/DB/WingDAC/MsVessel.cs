using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;

namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// 船マスタ
    /// ms_vessel
    /// </summary>
    public class MsVessel : BaseWingDac
    {
        #region メンバ変数
        //とりあえず必要そうなものだけ

        /// <summary>
        /// ID
        /// </summary>
        public decimal ms_vessel_id = EVal;

        /// <summary>
        /// VesselNo
        /// </summary>
        public string vessel_no = "";

        /// <summary>
        /// 名前
        /// </summary>
        public string vessel_name = "";

        /// <summary>
        /// 船種ID
        /// </summary>
        public string ms_vessel_type_id = "";

        /// <summary>
        /// 順番
        /// </summary>
        public decimal show_order = EVal;

        /// <summary>
        /// 有効可否
        /// </summary>
        public decimal senin_enabled = EVal;

        /// <summary>
        /// Offcial番号
        /// </summary>
        public string official_number = "";

        /// <summary>
        /// 国
        /// </summary>
        public string nationality = "";


        /// <summary>
        /// 竣工日
        /// </summary>
        public DateTime completion_date = EDate;

        /// <summary>
        /// IMO番号
        /// </summary>
        public decimal imo_no = EVal;

        /// <summary>
        /// 種別ID
        /// </summary>
        public string ms_vessel_kind_id = "";

        /// <summary>
        /// 船カテゴリID
        /// </summary>
        public string ms_vessel_category_id = "";

        /// <summary>
        /// 指摘事項管理船表示順
        /// </summary>
        public decimal deficiency_order = EVal;

        #endregion


        public override string ToString()
        {
            return this.vessel_name;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_vessel.ms_vessel_id,
ms_vessel.vessel_no,
ms_vessel.vessel_name,
ms_vessel.dwt,
ms_vessel.capacity,
ms_vessel.hp_tel,
ms_vessel.tel,
ms_vessel.ms_vessel_type_id,
ms_vessel.delete_flag,
ms_vessel.send_flag,
ms_vessel.vessel_id,
ms_vessel.data_no,
ms_vessel.user_key,
ms_vessel.renew_date,
ms_vessel.renew_user_id,
ms_vessel.ts,
ms_vessel.hachu_enabled,
ms_vessel.yojitsu_enabled,
ms_vessel.honsen_enabled,
ms_vessel.owner,
ms_vessel.official_number,
ms_vessel.cargo_weight,
ms_vessel.akasaka_vessel_no,
ms_vessel.show_order,
ms_vessel.anniversary_date,
ms_vessel.nationality,
ms_vessel.kanidousei_enabled,
ms_vessel.dousei_report1,
ms_vessel.dousei_report2,
ms_vessel.dousei_report3,
ms_vessel.completion_date,
ms_vessel.mail_address,
ms_vessel.kaikei_bumon_code,
ms_vessel.document_enabled,
ms_vessel.grt,
ms_vessel.senin_enabled,
ms_vessel.kensa_enabled,
ms_vessel.yojitsu_results,
ms_vessel.hachu_results,
ms_vessel.senin_results,
ms_vessel.document_results,
ms_vessel.kensa_results,
ms_vessel.kanidousei_results,
ms_vessel.sales_person_id,
ms_vessel.marine_superintendent_id,
ms_vessel.ms_crew_matrix_type_id,
ms_vessel.kyuyo_renkei_no,
ms_vessel.imo_no,
ms_vessel.ms_vessel_kind_id,
ms_vessel.ms_vessel_category_id,
ms_vessel.deficiency_order

FROM
ms_vessel
";

        /// <summary>
        /// レコード取得
        /// </summary>
        /// <param name="cone"></param>
        /// <returns></returns>
        public static List<MsVessel> GetRecords(NpgsqlConnection cone)
        {
            List<MsVessel> anslist = new List<MsVessel>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_vessel.delete_flag = 0

ORDER BY
ms_vessel.deficiency_order

";

                //取得
                anslist = GetRecordsList<MsVessel>(cone, sql);
            }
            catch(Exception e)
            {
                throw new Exception("MsVessel GetRecords", e);
            }

            return anslist;
        }


        /// <summary>
        /// 対象船種別の船一覧を取得する
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="ms_vessel_kind_id">船種別ID</param>
        /// <returns></returns>
        public static List<MsVessel> GetRecordsByMsVesselKindID(NpgsqlConnection cone, string ms_vessel_kind_id)
        {
            List<MsVessel> anslist = new List<MsVessel>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"

WHERE
ms_vessel.delete_flag = 0

AND
ms_vessel.ms_vessel_kind_id = :ms_vessel_kind_id

ORDER BY
ms_vessel.deficiency_order
";

                //条件
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "ms_vessel_kind_id", Value = ms_vessel_kind_id });

                //取得
                anslist = GetRecordsList<MsVessel>(cone, sql, plist);
            }
            catch (Exception e)
            {
                throw new Exception("MsVessel GetRecordsByMsVesselKindID", e);
            }

            return anslist;
        }

    }
}
