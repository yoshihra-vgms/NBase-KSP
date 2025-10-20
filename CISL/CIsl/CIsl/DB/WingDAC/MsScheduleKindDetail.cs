using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;
using System.Drawing;



namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// スケジュール種別詳細マスタ
    /// </summary>
    public class MsScheduleKindDetail : BaseDac
    {
        #region メンバ変数
        public override int ID
        {
            get
            {
                return this.schedule_kind_detail_id;
            }
        }


        /// <summary>
        /// ID
        /// </summary>
        public int schedule_kind_detail_id = EVal;

        /// <summary>
        /// 親ID
        /// </summary>
        public int schedule_kind_id = EVal;


        /// <summary>
        /// 名前
        /// </summary>
        public string schedule_kind_detail_name = "";

        /// <summary>
        /// 色R
        /// </summary>
        public int color_r = EVal;

        /// <summary>
        /// 色G
        /// </summary>
        public int color_g = EVal;

        /// <summary>
        /// 色B
        /// </summary>
        public int color_b = EVal;

        #endregion


        public override string ToString()
        {
            return this.schedule_kind_detail_name;
        }

        /// <summary>
        /// データ色
        /// </summary>
        public Color DataColor
        {
            get
            {
                Color ans = SystemColors.Control;
                if (this.color_r != EVal && this.color_g != EVal && color_b != EVal)
                {
                    ans = Color.FromArgb(255, this.color_r, this.color_g, this.color_b);
                }
                return ans;
            }
            /*
            set
            {
                this.color_r = value.R;
                this.color_g = value.G;
                this.color_b = value.B;
            }*/
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_schedule_kind_detail.schedule_kind_detail_id,
ms_schedule_kind_detail.schedule_kind_id,
ms_schedule_kind_detail.schedule_kind_detail_name,
ms_schedule_kind_detail.color_r,
ms_schedule_kind_detail.color_g,
ms_schedule_kind_detail.color_b,

ms_schedule_kind_detail.delete_flag,
ms_schedule_kind_detail.create_ms_user_id,
ms_schedule_kind_detail.update_ms_user_id,
ms_schedule_kind_detail.create_date,
ms_schedule_kind_detail.update_date

FROM
ms_schedule_kind_detail


";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsScheduleKindDetail> GetRecords(NpgsqlConnection cone)
        {
            List<MsScheduleKindDetail> anslist = new List<MsScheduleKindDetail>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_schedule_kind_detail.delete_flag = false
";

                //取得
                anslist = GetRecordsList<MsScheduleKindDetail>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsScheduleKindDetail GetRecords", e);
            }

            return anslist;
        }
    }
}
