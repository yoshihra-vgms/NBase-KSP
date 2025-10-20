using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using CIsl.DB;
using CIsl.DB.WingDAC;
using DcCommon.DB.DAC.Search;

namespace DcCommon.DB.DAC
{
    /// <summary>
    /// 検船テーブル
    /// dc_moi
    /// </summary>
    public class DcMoi : BaseDac
    {
        public override int TableID
        {
            get
            {
                return DBTableID.dc_moi;
            }
        }

        public override int ID
        {
            get
            {
                return this.moi_id;
            }
        }


        #region メンバ変数

        /// <summary>
        /// MOI ID
        /// </summary>
        public int moi_id = EVal;

        /// <summary>
        /// 船ID
        /// </summary>
        public decimal ms_vessel_id = EVal;

        /// <summary>
        /// PIC
        /// </summary>
        public string ms_user_id = "";

        /// <summary>
        /// Port Site
        /// </summary>
        public string ms_basho_id = "";

        /// <summary>
        /// Terminal 基地
        /// </summary>
        public string terminal = "";

        /// <summary>
        /// 国
        /// </summary>
        public string ms_regional_code = "";

        /// <summary>
        /// 受検日
        /// </summary>
        public DateTime date = EDate;

        /// <summary>
        /// レポート受領日
        /// </summary>
        public DateTime receipt_date = EDate;

        /// <summary>
        /// 指摘件数
        /// </summary>
        public int observation = EVal;

        /// <summary>
        /// 検船種別
        /// </summary>
        public int inspection_category_id = EVal;

        /// <summary>
        /// 申請先
        /// </summary>
        public string appointed_ms_customer_id = "";

        /// <summary>
        /// 検船実施会社
        /// </summary>
        public string inspection_ms_customer_id = "";

        /// <summary>
        /// 検船員
        /// </summary>
        public string inspection_name = "";

        /// <summary>
        /// 立会者
        /// </summary>
        public string attend = "";

        /// <summary>
        /// 備考
        /// </summary>
        public string remarks = "";

        /// <summary>
        /// 検索キーワード
        /// </summary>
        public string search_keyword = "";



        #endregion

        /// <summary>
        /// 基本SELECT
        /// </summary>
        private static string DefaultSelect = @"
SELECT
dc_moi.moi_id,
dc_moi.ms_vessel_id,
dc_moi.ms_user_id,
dc_moi.ms_basho_id,
dc_moi.terminal,
dc_moi.ms_regional_code,
dc_moi.date,
dc_moi.receipt_date,
dc_moi.observation,
dc_moi.inspection_category_id,
dc_moi.appointed_ms_customer_id,
dc_moi.inspection_ms_customer_id,
dc_moi.inspection_name,
dc_moi.attend,
dc_moi.remarks,
dc_moi.search_keyword,

dc_moi.delete_flag,
dc_moi.create_ms_user_id,
dc_moi.update_ms_user_id,
dc_moi.create_date,
dc_moi.update_date

FROM
dc_moi
";


        /// <summary>
        /// 対象のデータを取得する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="moi_id"></param>
        /// <returns></returns>
        public static DcMoi GetRecordByMoiID(NpgsqlConnection cone, int moi_id)
        {
            DcMoi ans = new DcMoi();

            try
            {
                //SQL
                string sql = DefaultSelect + @"
WHERE
dc_moi.delete_flag = false
AND
dc_moi.moi_id = :moi_id
";

                //検索条件付加
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "moi_id", Value = moi_id });


                //SQL取得
                ans = GetRecordData<DcMoi>(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcMoi GetRecordsByMoiID", e);
            }

            return ans;
        }



        /// <summary>
        /// レコードの挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public override int InsertRecord(NpgsqlConnection cone, MsUser requser)
        {
            int ansid = EVal;
            List<SqlParamData> plist = new List<SqlParamData>();

            try
            {
                string sql = @"
INSERT INTO dc_moi(
ms_vessel_id,
ms_user_id,
ms_basho_id,
terminal,
ms_regional_code,
date,
receipt_date,
observation,
inspection_category_id,
appointed_ms_customer_id,
inspection_ms_customer_id,
inspection_name,
attend,
remarks,
search_keyword,

delete_flag,
create_ms_user_id,
update_ms_user_id
) VALUES (
:ms_vessel_id,
:ms_user_id,
:ms_basho_id,
:terminal,
:ms_regional_code,
:date,
:receipt_date,
:observation,
:inspection_category_id,
:appointed_ms_customer_id,
:inspection_ms_customer_id,
:inspection_name,
:attend,
:remarks,
:search_keyword,

:delete_flag,
:create_ms_user_id,
:update_ms_user_id
)
RETURNING moi_id;
";

                this.delete_flag = false;
                this.create_ms_user_id = requser.ms_user_id;
                this.update_ms_user_id = requser.ms_user_id;

                //パラメータのADD
                plist.Add(new SqlParamData() { Name = "ms_vessel_id", Value = this.ms_vessel_id });
                plist.Add(new SqlParamData() { Name = "ms_user_id", Value = this.ms_user_id });
                plist.Add(new SqlParamData() { Name = "ms_basho_id", Value = this.ms_basho_id });
                plist.Add(new SqlParamData() { Name = "terminal", Value = this.terminal });
                plist.Add(new SqlParamData() { Name = "ms_regional_code", Value = this.ms_regional_code });

                plist.Add(new SqlParamData() { Name = "date", Value = this.date });
                plist.Add(new SqlParamData() { Name = "receipt_date", Value = this.receipt_date });
                plist.Add(new SqlParamData() { Name = "observation", Value = this.observation });
                plist.Add(new SqlParamData() { Name = "inspection_category_id", Value = this.inspection_category_id });
                plist.Add(new SqlParamData() { Name = "appointed_ms_customer_id", Value = this.appointed_ms_customer_id });
                plist.Add(new SqlParamData() { Name = "inspection_ms_customer_id", Value = this.inspection_ms_customer_id });
                plist.Add(new SqlParamData() { Name = "inspection_name", Value = this.inspection_name });
                plist.Add(new SqlParamData() { Name = "attend", Value = this.attend });
                plist.Add(new SqlParamData() { Name = "remarks", Value = this.remarks });
                plist.Add(new SqlParamData() { Name = "search_keyword", Value = this.search_keyword });


                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "create_ms_user_id", Value = this.create_ms_user_id });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });

                //実行
                ansid = (int)this.ExecuteScalar(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcMoi InsertRecord", e);
            }

            return ansid;
        }


        /// <summary>
        /// レコード更新
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public override bool UpdateRecord(NpgsqlConnection cone, MsUser requser)
        {
            bool ret = false;
            List<SqlParamData> plist = new List<SqlParamData>();

            try
            {
                string sql = @"
UPDATE
dc_moi
SET
ms_vessel_id = :ms_vessel_id,
ms_user_id = :ms_user_id,
ms_basho_id = :ms_basho_id,
terminal = :terminal,
ms_regional_code = :ms_regional_code,
date = :date,
receipt_date = :receipt_date,
observation = :observation,

inspection_category_id = :inspection_category_id,
appointed_ms_customer_id = :appointed_ms_customer_id,
inspection_ms_customer_id = :inspection_ms_customer_id,
inspection_name = :inspection_name,
attend = :attend,
remarks = :remarks,
search_keyword = :search_keyword,

delete_flag = :delete_flag,
update_ms_user_id = :update_ms_user_id

WHERE
moi_id = :moi_id
";

                this.update_ms_user_id = requser.ms_user_id;

                //パラメータのADD
                plist.Add(new SqlParamData() { Name = "ms_vessel_id", Value = this.ms_vessel_id });
                plist.Add(new SqlParamData() { Name = "ms_user_id", Value = this.ms_user_id });
                plist.Add(new SqlParamData() { Name = "ms_basho_id", Value = this.ms_basho_id });
                plist.Add(new SqlParamData() { Name = "terminal", Value = this.terminal });
                plist.Add(new SqlParamData() { Name = "ms_regional_code", Value = this.ms_regional_code });

                plist.Add(new SqlParamData() { Name = "date", Value = this.date });
                plist.Add(new SqlParamData() { Name = "receipt_date", Value = this.receipt_date });
                plist.Add(new SqlParamData() { Name = "observation", Value = this.observation });
                plist.Add(new SqlParamData() { Name = "inspection_category_id", Value = this.inspection_category_id });
                plist.Add(new SqlParamData() { Name = "appointed_ms_customer_id", Value = this.appointed_ms_customer_id });
                plist.Add(new SqlParamData() { Name = "inspection_ms_customer_id", Value = this.inspection_ms_customer_id });
                plist.Add(new SqlParamData() { Name = "inspection_name", Value = this.inspection_name });
                plist.Add(new SqlParamData() { Name = "attend", Value = this.attend });
                plist.Add(new SqlParamData() { Name = "remarks", Value = this.remarks });
                plist.Add(new SqlParamData() { Name = "search_keyword", Value = this.search_keyword });


                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });


                plist.Add(new SqlParamData() { Name = "moi_id", Value = this.moi_id });

                //実行
                ret = this.ExecuteNonQuery(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcMoi UpdateRecord", e);
            }

            return ret;
        }


        /// <summary>
        /// 削除実行
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="requser">実行ユーザー</param>
        /// <returns></returns>
        public override bool DeleteRecord(NpgsqlConnection cone, MsUser requser)
        {
            bool ret = false;
            try
            {
                ret = this.ExecuteDelete(cone, requser, "dc_moi", "moi_id", this.moi_id);
            }
            catch (Exception e)
            {
                throw new Exception("DcMoi DeleteRecord", e);
            }

            return ret;
        }

    }
}
