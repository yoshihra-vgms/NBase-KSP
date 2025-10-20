using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;


namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// スケジュール有効可否
    /// ms_vessel_schedule_kind_detail_enable
    /// </summary>
    public class MsVesselScheduleKindDetailEnable : BaseDac
    {
        #region メンバ変数
       


        /// <summary>
        /// 船ID
        /// </summary>
        public decimal ms_vessel_id = EVal;


        /// <summary>
        /// 種別
        /// </summary>
        public int schedule_kind_detail_id = EVal;

        /// <summary>
        /// 有効可否
        /// </summary>
        public bool enabled = false;

        #endregion



        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_vessel_schedule_kind_detail_enable.ms_vessel_id,
ms_vessel_schedule_kind_detail_enable.schedule_kind_detail_id,
ms_vessel_schedule_kind_detail_enable.enabled,

ms_vessel_schedule_kind_detail_enable.delete_flag,
ms_vessel_schedule_kind_detail_enable.create_ms_user_id,
ms_vessel_schedule_kind_detail_enable.update_ms_user_id,
ms_vessel_schedule_kind_detail_enable.create_date,
ms_vessel_schedule_kind_detail_enable.update_date
FROM
ms_vessel_schedule_kind_detail_enable

";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsVesselScheduleKindDetailEnable> GetRecords(NpgsqlConnection cone)
        {
            List<MsVesselScheduleKindDetailEnable> anslist = new List<MsVesselScheduleKindDetailEnable>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_vessel_schedule_kind_detail_enable.delete_flag = false
";

                //取得
                anslist = GetRecordsList<MsVesselScheduleKindDetailEnable>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsVesselScheduleKindDetailEnable GetRecords", e);
            }

            return anslist;
        }


        /// <summary>
        /// 対象船のデータを取得する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="ms_vessel_id"></param>
        /// <returns></returns>
        public static List<MsVesselScheduleKindDetailEnable> GetRecordsByMsVesselID(NpgsqlConnection cone, decimal ms_vessel_id)
        {
            List<MsVesselScheduleKindDetailEnable> anslist = new List<MsVesselScheduleKindDetailEnable>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_vessel_schedule_kind_detail_enable.delete_flag = false
AND
ms_vessel_schedule_kind_detail_enable.ms_vessel_id = :ms_vessel_id
";

                //条件
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "ms_vessel_id", Value = ms_vessel_id });

                //取得
                anslist = GetRecordsList<MsVesselScheduleKindDetailEnable>(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("MsVesselScheduleKindDetailEnable GetRecordsByMsVesselID", e);
            }

            return anslist;
        }
    }
}
