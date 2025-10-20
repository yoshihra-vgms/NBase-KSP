using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using Npgsql;
using CIsl.DB;
using CIsl.DB.WingDAC;

namespace DcCommon.DB.DAC
{
    /// <summary>
    /// アラーム色マスタ
    /// ms_alarm_color
    /// </summary>
    public class MsAlarmColor : BaseDac
    {
        public override int ID
        {
            get
            {
                return this.alarm_color_id;
            }
        }

        #region メンバ変数

        /// <summary>
        /// アラーム色ID
        /// </summary>
        public int alarm_color_id = EVal;

        /// <summary>
        /// 表示日数
        /// </summary>
        public int day_count = EVal;

        /// <summary>
        /// 赤
        /// </summary>
        public int color_r = EVal;

        /// <summary>
        /// 緑
        /// </summary>
        public int color_g = EVal;

        /// <summary>
        /// 青
        /// </summary>
        public int color_b = EVal;

        /// <summary>
        /// コメント
        /// </summary>
        public string comment = "";


        /// <summary>
        /// アラーム色取得
        /// </summary>
        public Color AlarmColor
        {
            get
            {
                return Color.FromArgb(this.color_r, this.color_g, this.color_b);
            }
        }
        
        #endregion

        
        


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_alarm_color.alarm_color_id,
ms_alarm_color.day_count,
ms_alarm_color.color_r,
ms_alarm_color.color_g,
ms_alarm_color.color_b,
ms_alarm_color.comment,
ms_alarm_color.delete_flag,
ms_alarm_color.create_ms_user_id,
ms_alarm_color.update_ms_user_id,
ms_alarm_color.create_date,
ms_alarm_color.update_date

FROM
ms_alarm_color

";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsAlarmColor> GetRecords(NpgsqlConnection cone)
        {
            List<MsAlarmColor> anslist = new List<MsAlarmColor>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_alarm_color.delete_flag = false
";

                //取得
                anslist = GetRecordsList<MsAlarmColor>(cone, sql);
            }
            catch(Exception e)
            {
                throw new Exception("MsAlarmColor GetRecords", e);

            }

            return anslist;
        }

        /// <summary>
        /// レコード挿入
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <param name="requser">ユーザー</param>
        /// <returns></returns>
        public override int InsertRecord(NpgsqlConnection cone, MsUser requser)
        {
            string sql = @"
INSERT INTO ms_alarm_color(
day_count,
color_r,
color_g,
color_b,
comment,

delete_flag,
create_ms_user_id,
update_ms_user_id
)
VALUES (
:day_count,
:color_r,
:color_g,
:color_b,
:comment,

:delete_flag,
:create_ms_user_id,
:update_ms_user_id
)
RETURNING alarm_color_id



";
            int ans = EVal;
            try
            {
                this.delete_flag = false;
                this.create_ms_user_id = requser.ms_user_id;
                this.update_ms_user_id = requser.ms_user_id;


                //パラメータ作成
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "day_count", Value = this.day_count });
                plist.Add(new SqlParamData() { Name = "color_r", Value = this.color_r });
                plist.Add(new SqlParamData() { Name = "color_g", Value = this.color_g });
                plist.Add(new SqlParamData() { Name = "color_b", Value = this.color_b });
                plist.Add(new SqlParamData() { Name = "comment", Value = this.comment });


                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "create_ms_user_id", Value = this.create_ms_user_id });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });

                //実行
                ans = (int)this.ExecuteScalar(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("MsAlarmColor InsertRecord", e);
            }

            return ans;
        }




        /// <summary>
        /// レコード更新
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="requser">更新者</param>
        /// <returns></returns>
        public override bool UpdateRecord(NpgsqlConnection cone, MsUser requser)
        {
            string sql = @"
UPDATE
ms_alarm_color
SET
day_count = :day_count,
color_r = :color_r,
color_g = :color_g,
color_b = :color_b,
comment = :comment,

delete_flag = :delete_flag,
update_ms_user_id = :update_ms_user_id

WHERE
alarm_color_id = :alarm_color_id

";
            bool ans = false;

            try
            {
                this.update_ms_user_id = requser.ms_user_id;


                //パラメータ作成
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "day_count", Value = this.day_count });
                plist.Add(new SqlParamData() { Name = "color_r", Value = this.color_r });
                plist.Add(new SqlParamData() { Name = "color_g", Value = this.color_g });
                plist.Add(new SqlParamData() { Name = "color_b", Value = this.color_b });
                plist.Add(new SqlParamData() { Name = "comment", Value = this.comment });

                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });

                plist.Add(new SqlParamData() { Name = "alarm_color_id", Value = this.alarm_color_id });

                //実行
                ans = this.ExecuteNonQuery(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("MsAlarmColor UpdateRecord", e);
            }

            return ans;

        }


        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="requser">更新者</param>
        /// <returns></returns>
        public override bool DeleteRecord(NpgsqlConnection cone, MsUser requser)
        {
            bool ret = true;
            try
            {
                ret = this.ExecuteDelete(cone, requser, "ms_alarm_color", "alarm_color_id", this.alarm_color_id);

            }
            catch (Exception e)
            {
                throw new Exception("MsAlarmColor DeleteRecord", e);
            }

            return ret;
        }
    }
}
