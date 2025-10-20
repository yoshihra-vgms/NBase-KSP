using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;
using CIsl.DB;
using DcCommon.DB.DAC.Search;
using CIsl.DB.WingDAC;

namespace DcCommon.DB.DAC
{
    /// <summary>
    /// 事故トラブルテーブル
    /// dc_accident
    /// </summary>
    public class DcAccident : BaseDac
    {

        public override int TableID
        {
            get
            {
                return DBTableID.dc_accident;
            }
        }

        public override int ID
        {
            get
            {
                return this.accident_id;
            }
        }


        #region メンバ変数
        /// <summary>
        /// 事故トラブルテーブルID
        /// </summary>
        public int accident_id = EVal;

        /// <summary>
        /// 発生日
        /// </summary>
        public DateTime date = EDate;

        /// <summary>
        /// PIC 担当者
        /// </summary>
        public string ms_user_id = "";

        /// <summary>
        /// 種類
        /// </summary>
        public int accident_kind_id = EVal;
        
        /// <summary>
        /// Kind of Accident
        /// </summary>
        public int kind_of_accident_id = EVal;

        /// <summary>
        /// 発生状況
        /// </summary>
        public int accident_situation_id = EVal;

        /// <summary>
        /// 発生場所
        /// </summary>
        public string ms_basho_id = "";

        /// <summary>
        /// 船ID
        /// </summary>
        public decimal ms_vessel_id = EVal;

        /// <summary>
        /// 国
        /// </summary>
        public string ms_regional_code = "";


        /// <summary>
        /// タイトル
        /// </summary>
        public string title = "";

        /// <summary>
        /// 報告書No
        /// </summary>
        public string accident_report_no = "";

        /// <summary>
        /// Importance
        /// </summary>
        public int accident_importance_id = EVal;

        /// <summary>
        /// 状態
        /// </summary>
        public int accident_status_id = EVal;

        /// <summary>
        /// 事故概要
        /// </summary>
        public string accident = "";

        /// <summary>
        /// 現場報告
        /// </summary>
        public string spot_report = "";

        /// <summary>
        /// 調査結果原因
        /// </summary>
        public string cause_of_accident = "";

        /// <summary>
        /// 再発防止策
        /// </summary>
        public string preventive_action = "";

        /// <summary>
        /// 運行への影響
        /// </summary>
        public string influence = "";

        /// <summary>
        /// 備考
        /// </summary>
        public string remarks = "";

        /// <summary>
        /// 検索文字列
        /// </summary>
        public string search_keyword = "";
        #endregion

        /// <summary>
        /// 基本SELECT
        /// </summary>
        private static string DefaultSelect = @"
SELECT
dc_accident.accident_id,
dc_accident.date,
dc_accident.ms_user_id,
dc_accident.accident_kind_id,
dc_accident.kind_of_accident_id,
dc_accident.accident_situation_id,
dc_accident.ms_basho_id,
dc_accident.ms_vessel_id,
dc_accident.ms_regional_code,
dc_accident.title,
dc_accident.accident_report_no,
dc_accident.accident_importance_id,
dc_accident.accident_status_id,
dc_accident.accident,
dc_accident.spot_report,
dc_accident.cause_of_accident,
dc_accident.preventive_action,
dc_accident.influence,
dc_accident.remarks,
dc_accident.search_keyword,

dc_accident.delete_flag,
dc_accident.create_ms_user_id,
dc_accident.update_ms_user_id,
dc_accident.create_date,
dc_accident.update_date

FROM
dc_accident

";



        /// <summary>
        /// Accidentの検索
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="sdata">検索データ</param>
        /// <returns></returns>
        public static List<DcAccident> GetRecordsBySearchData(NpgsqlConnection cone, AccidentSearchData sdata)
        {
            List<DcAccident> anslist = new List<DcAccident>();

            try
            {
                //SQL
                string sql = DefaultSelect + @"
WHERE
dc_accident.delete_flag = false
";

                //検索条件付加
                List<SqlParamData> paramlist = new List<SqlParamData>();
                sql += sdata.CreateSQLWhere(out paramlist);

                sql += @"
ORDER BY
dc_accident.update_date DESC
";

                //SQL取得
                anslist = GetRecordsList<DcAccident>(cone, sql, paramlist);

            }
            catch (Exception e)
            {
                throw new Exception("DcAccident GetRecordsBySearchData", e);
            }

            return anslist;
        }


        /// <summary>
        /// 対象のデータを取得する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="accident_id"></param>
        /// <returns></returns>
        public static DcAccident GetRecordsByAccidentID(NpgsqlConnection cone, int accident_id)
        {
            DcAccident ans = null;

            try
            {
                //SQL
                string sql = DefaultSelect + @"
WHERE
dc_accident.delete_flag = false
AND
dc_accident.accident_id = :accident_id
";

                //検索条件付加
                List<SqlParamData> paramlist = new List<SqlParamData>();
                paramlist.Add(new SqlParamData() { Name = "accident_id", Value = accident_id });
  

                //SQL取得
                ans = GetRecordData<DcAccident>(cone, sql, paramlist);

            }
            catch (Exception e)
            {
                throw new Exception("DcAccident GetRecordsByAccidentID", e);
            }

            return ans;
        }


        /// <summary>
        /// レコード挿入
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
INSERT INTO dc_accident(
date,
ms_user_id,
accident_kind_id,
kind_of_accident_id,
accident_situation_id,
ms_basho_id,
ms_vessel_id,
ms_regional_code,
title,
accident_report_no,
accident_importance_id,
accident_status_id,
accident,
spot_report,
cause_of_accident,
preventive_action,
influence,
remarks,
search_keyword,
delete_flag,
create_ms_user_id,
update_ms_user_id
) VALUES (
:date,
:ms_user_id,
:accident_kind_id,
:kind_of_accident_id,
:accident_situation_id,
:ms_basho_id,
:ms_vessel_id,
:ms_regional_code,
:title,
:accident_report_no,
:accident_importance_id,
:accident_status_id,
:accident,
:spot_report,
:cause_of_accident,
:preventive_action,
:influence,
:remarks,
:search_keyword,
:delete_flag,
:create_ms_user_id,
:update_ms_user_id
)
RETURNING accident_id;

";

                this.delete_flag = false;
                this.create_ms_user_id = requser.ms_user_id;
                this.update_ms_user_id = requser.ms_user_id;


                //パラメータのADD
                plist.Add(new SqlParamData() { Name = "date", Value = this.date });
                plist.Add(new SqlParamData() { Name = "ms_user_id", Value = this.ms_user_id });
                plist.Add(new SqlParamData() { Name = "accident_kind_id", Value = this.accident_kind_id });
                plist.Add(new SqlParamData() { Name = "kind_of_accident_id", Value = this.kind_of_accident_id });
                plist.Add(new SqlParamData() { Name = "accident_situation_id", Value = this.accident_situation_id });
                plist.Add(new SqlParamData() { Name = "ms_basho_id", Value = this.ms_basho_id });
                plist.Add(new SqlParamData() { Name = "ms_vessel_id", Value = this.ms_vessel_id });
                plist.Add(new SqlParamData() { Name = "ms_regional_code", Value = this.ms_regional_code });

                plist.Add(new SqlParamData() { Name = "title", Value = this.title });
                plist.Add(new SqlParamData() { Name = "accident_report_no", Value = this.accident_report_no });
                plist.Add(new SqlParamData() { Name = "accident_importance_id", Value = this.accident_importance_id });
                plist.Add(new SqlParamData() { Name = "accident_status_id", Value = this.accident_status_id });
                plist.Add(new SqlParamData() { Name = "accident", Value = this.accident });

                plist.Add(new SqlParamData() { Name = "spot_report", Value = this.spot_report });
                plist.Add(new SqlParamData() { Name = "cause_of_accident", Value = this.cause_of_accident });
                plist.Add(new SqlParamData() { Name = "preventive_action", Value = this.preventive_action });
                plist.Add(new SqlParamData() { Name = "influence", Value = this.influence });
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
                throw new Exception("DcAccident InsertRecord", e);
            }

            return ansid;
        }


        /// <summary>
        /// レコードの更新
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="requser">更新ユーザー</param>
        /// <returns></returns>
        public override bool UpdateRecord(NpgsqlConnection cone, MsUser requser)
        {
            bool ret = false;
            List<SqlParamData> plist = new List<SqlParamData>();

            try
            {
                string sql = @"
UPDATE
dc_accident
SET
date = :date,
ms_user_id = :ms_user_id,
accident_kind_id = :accident_kind_id,
kind_of_accident_id = :kind_of_accident_id,
accident_situation_id = :accident_situation_id,
ms_basho_id = :ms_basho_id,
ms_vessel_id = :ms_vessel_id,
ms_regional_code = :ms_regional_code,
title = :title,
accident_report_no = :accident_report_no,
accident_importance_id = :accident_importance_id,
accident_status_id = :accident_status_id,
accident = :accident,
spot_report = :spot_report,
cause_of_accident = :cause_of_accident,
preventive_action = :preventive_action,
influence = :influence,
remarks = :remarks,
search_keyword = :search_keyword,
delete_flag = :delete_flag,
update_ms_user_id = :update_ms_user_id

WHERE
accident_id = :accident_id;

";

                this.update_ms_user_id = requser.ms_user_id;

                //パラメータのADD
                plist.Add(new SqlParamData() { Name = "date", Value = this.date });
                plist.Add(new SqlParamData() { Name = "ms_user_id", Value = this.ms_user_id });
                plist.Add(new SqlParamData() { Name = "accident_kind_id", Value = this.accident_kind_id });
                plist.Add(new SqlParamData() { Name = "kind_of_accident_id", Value = this.kind_of_accident_id });
                plist.Add(new SqlParamData() { Name = "accident_situation_id", Value = this.accident_situation_id });
                plist.Add(new SqlParamData() { Name = "ms_basho_id", Value = this.ms_basho_id });
                plist.Add(new SqlParamData() { Name = "ms_vessel_id", Value = this.ms_vessel_id });
                plist.Add(new SqlParamData() { Name = "ms_regional_code", Value = this.ms_regional_code });

                plist.Add(new SqlParamData() { Name = "title", Value = this.title });
                plist.Add(new SqlParamData() { Name = "accident_report_no", Value = this.accident_report_no });
                plist.Add(new SqlParamData() { Name = "accident_importance_id", Value = this.accident_importance_id });
                plist.Add(new SqlParamData() { Name = "accident_status_id", Value = this.accident_status_id });
                plist.Add(new SqlParamData() { Name = "accident", Value = this.accident });

                plist.Add(new SqlParamData() { Name = "spot_report", Value = this.spot_report });
                plist.Add(new SqlParamData() { Name = "cause_of_accident", Value = this.cause_of_accident });
                plist.Add(new SqlParamData() { Name = "preventive_action", Value = this.preventive_action });
                plist.Add(new SqlParamData() { Name = "influence", Value = this.influence });
                plist.Add(new SqlParamData() { Name = "remarks", Value = this.remarks });
                plist.Add(new SqlParamData() { Name = "search_keyword", Value = this.search_keyword });

                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });


                plist.Add(new SqlParamData() { Name = "accident_id", Value = this.accident_id });

                //実行
                ret = this.ExecuteNonQuery(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcAccident UpdateRecord", e);
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
                ret = this.ExecuteDelete(cone, requser, "dc_accident", "accident_id", this.accident_id);
            }
            catch (Exception e)
            {
                throw new Exception("DcAccident DeleteRecord", e);
            }

            return ret;
        }




        /// <summary>
        /// Statusの更新(デバッグ用)
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="requser">要求者</param>
        /// <returns></returns>
        public bool UpdateStatus(NpgsqlConnection cone, MsUser requser)
        {
            bool ret = false;

            try
            {
                #region SQL
                string sql = @"
UPDATE
dc_accident

SET
accident_status_id = :accident_status_id

WHERE
accident_id = :accident_id;
";
                #endregion

                List<SqlParamData> plist = new List<SqlParamData>();

                this.delete_flag = false;
                this.update_ms_user_id = requser.ms_user_id;

                #region パラメータ挿入

                plist.Add(new SqlParamData() { Name = "accident_status_id", Value = this.accident_status_id });


                plist.Add(new SqlParamData() { Name = "accident_id", Value = this.accident_id });

                #endregion


                //実行！
                ret = ExecuteNonQuery(cone, sql, plist);
            }
            catch (Exception e)
            {
                throw new Exception("DcAccident UpdateStatus", e);
            }

            return ret;
        }


        //////////////////////////////////////////////////
        /// <summary>
        /// このデータがCompleteしているかを確認する
        /// </summary>
        /// <returns></returns>
        public bool CheckComplete()
        {
            if (this.accident_status_id == (int)EAccidentStatus.Complete)
            {
                return true;
            }

            return false;
        }

    }
}
