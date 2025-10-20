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
    /// PSC Inspectionアイテムテーブル
    /// dc_ci_psc_inspection
    /// </summary>
    public class DcCiPscInspection : DcCommentItem
    {
        

        #region メンバ変数


        public override int ID
        {
            get
            {
                return this.comment_item_id;
            }
        }

        public override int TableID
        {
            get
            {
                return DBTableID.dc_ci_psc_inspection;

            }
        }

        /// <summary>
        /// Deficiency件数
        /// </summary>
        public int deficinecy_count = EVal;

        /// <summary>
        /// Port 港 場所ID
        /// </summary>
        public string ms_basho_id = "";

        /// <summary>
        /// 国ID
        /// </summary>
        public string ms_regional_code = "";

        /// <summary>
        /// 備考
        /// </summary>
        public string comment_remarks = "";

        /// <summary>
        /// 会社横展開有無
        /// </summary>
        public bool share_to_our_fleet = false;

        /// <summary>
        /// 会社横展開日付
        /// </summary>
        public DateTime share_to_our_fleet_date = EDate;

        /// <summary>
        /// このデータの番号 DeficiencyNo
        /// </summary>
        public int deficinecy_no = EVal;

        /// <summary>
        /// Status
        /// </summary>
        public int status_id = EVal;

        /// <summary>
        /// 入力者 PIC
        /// </summary>
        public string ms_user_id = "";

        /// <summary>
        /// 本船対応
        /// </summary>
        public string action_taken_by_vessel = "";

        /// <summary>
        /// NK対応 NK窓口部署
        /// </summary>
        public string class_involved_nk_department = "";

        /// <summary>
        /// NK対応 NK窓口氏名
        /// </summary>
        public string class_involved_nk_name = "";


        /// <summary>
        /// NK対応 コメント
        /// </summary>
        public string class_involved = "";

        /// <summary>
        /// 指摘事項コード 
        /// </summary>
        public int deficiency_code_id = EVal;

        /// <summary>
        /// 指摘事項
        /// </summary>
        public string deficiency = "";

        /// <summary>
        /// 原因
        /// </summary>
        public string cause_of_deficiency = "";

        /// <summary>
        /// 会社対応
        /// </summary>
        public string action_taken_by_company = "";

        /// <summary>
        /// 是正措置
        /// </summary>
        public string corrective_action = "";

        /// <summary>
        /// 備考 Remarks
        /// </summary>
        public string item_remarks = "";


        #endregion



        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
dc_ci_psc_inspection.comment_item_id,
dc_ci_psc_inspection.comment_id,
dc_ci_psc_inspection.ms_vessel_id,
dc_ci_psc_inspection.ms_crew_matrix_type_id,
dc_ci_psc_inspection.item_kind_id,
dc_ci_psc_inspection.date,
dc_ci_psc_inspection.search_keyword,
dc_ci_psc_inspection.delete_flag,
dc_ci_psc_inspection.create_ms_user_id,
dc_ci_psc_inspection.update_ms_user_id,
dc_ci_psc_inspection.create_date,
dc_ci_psc_inspection.update_date,
dc_ci_psc_inspection.deficinecy_count,
dc_ci_psc_inspection.ms_basho_id,
dc_ci_psc_inspection.ms_regional_code,
dc_ci_psc_inspection.comment_remarks,
dc_ci_psc_inspection.share_to_our_fleet,
dc_ci_psc_inspection.share_to_our_fleet_date,
dc_ci_psc_inspection.deficinecy_no,
dc_ci_psc_inspection.status_id,
dc_ci_psc_inspection.ms_user_id,
dc_ci_psc_inspection.action_taken_by_vessel,
dc_ci_psc_inspection.class_involved_nk_department,
dc_ci_psc_inspection.class_involved_nk_name,
dc_ci_psc_inspection.class_involved,
dc_ci_psc_inspection.deficiency_code_id,
dc_ci_psc_inspection.deficiency,
dc_ci_psc_inspection.cause_of_deficiency,
dc_ci_psc_inspection.action_taken_by_company,
dc_ci_psc_inspection.corrective_action,
dc_ci_psc_inspection.item_remarks


FROM
dc_ci_psc_inspection
";





        /// <summary>
        /// 全データを取得
        /// </summary>
        /// <param name="cone"></param>
        /// <returns></returns>
        public static List<DcCiPscInspection> GetRecords(NpgsqlConnection cone)
        {
            List<DcCiPscInspection> anslist = new List<DcCiPscInspection>();

            try
            {
                //SQL
                string sql = DefaultSelect + @"
WHERE
dc_ci_psc_inspection.delete_flag = false

ORDER BY
dc_ci_psc_inspection.date
";

                

                //取得
                anslist = GetRecordsList<DcCiPscInspection>(cone, sql);


            }
            catch (Exception e)
            {
                throw new Exception("DcCiPscInspection GetRecords", e);
            }

            return anslist;
        }

        /// <summary>
        /// 対象をIDで取得
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="comment_item_id">ID</param>
        /// <returns></returns>
        public new static DcCiPscInspection GetRecordByCommentItemID(NpgsqlConnection cone, int comment_item_id)
        {
            DcCiPscInspection ans = null;

            try
            {
                //SQL
                string sql = DefaultSelect + @"
WHERE
dc_ci_psc_inspection.delete_flag = false
AND
dc_ci_psc_inspection.comment_item_id = :comment_item_id
";

                //パラメータ
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "comment_item_id", Value = comment_item_id });

                //取得
                ans = GetRecordData<DcCiPscInspection>(cone, sql, plist);


            }
            catch (Exception e)
            {
                throw new Exception("DcCiPscInspection GetRecordByCommentItemID", e);
            }

            return ans;
        }


        /// <summary>
        /// コメント親に関連するデータの取得
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="comment_id"></param>
        /// <returns></returns>
        public static List<DcCiPscInspection> GetRecordsByCommentID(NpgsqlConnection cone, int comment_id)
        {
            List<DcCiPscInspection> anslist = new List<DcCiPscInspection>();

            try
            {
                //SQL
                string sql = DefaultSelect + @"
WHERE
dc_ci_psc_inspection.delete_flag = false
AND
dc_ci_psc_inspection.comment_id = :comment_id
";

                //パラメータ
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "comment_id", Value = comment_id });

                //取得
                anslist = GetRecordsList<DcCiPscInspection>(cone, sql, plist);


            }
            catch (Exception e)
            {
                throw new Exception("DcCiPscInspection GetRecordsByCommentID", e);
            }

            return anslist;
        }








        /// <summary>
        /// コメントの検索
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public static List<DcCiPscInspection> GetRecordsByPSCSearchData(NpgsqlConnection cone, PscInspectionSearchData sdata)
        {
            List<DcCiPscInspection> anslist = new List<DcCiPscInspection>();

            try
            {
                //SQL
                string sql = DefaultSelect + @"
WHERE
dc_ci_psc_inspection.delete_flag = false
";

                //検索条件付加
                List<SqlParamData> paramlist = new List<SqlParamData>();
                sql += sdata.CreateSQLWhere(out paramlist);

                sql += @"
ORDER BY
dc_ci_psc_inspection.update_date DESC
";

                //SQL取得
                anslist = GetRecordsList<DcCiPscInspection>(cone, sql, paramlist);

            }
            catch (Exception e)
            {
                throw new Exception("DcCiPscInspection GetRecordsByPSCSearchData", e);
            }

            return anslist;
        }


        /// <summary>
        /// レコードの挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public override int InsertRecord(Npgsql.NpgsqlConnection cone, MsUser requser)
        {
            int ans = EVal;

            try
            {
                #region SQL
                string sql = @"
INSERT INTO dc_ci_psc_inspection(
comment_id,
ms_vessel_id,
ms_crew_matrix_type_id,
item_kind_id,
date,
search_keyword,

delete_flag,
create_ms_user_id,
update_ms_user_id,

deficinecy_count,
ms_basho_id,
ms_regional_code,
comment_remarks,

share_to_our_fleet,
share_to_our_fleet_date,

deficinecy_no,
status_id,
ms_user_id,
action_taken_by_vessel,
class_involved_nk_department,
class_involved_nk_name,
class_involved,
deficiency_code_id,
deficiency,
cause_of_deficiency,
action_taken_by_company,
corrective_action,
item_remarks

) VALUES (
:comment_id,
:ms_vessel_id,
:ms_crew_matrix_type_id,
:item_kind_id,
:date,
:search_keyword,

:delete_flag,
:create_ms_user_id,
:update_ms_user_id,

:deficinecy_count,
:ms_basho_id,
:ms_regional_code,
:comment_remarks,

:share_to_our_fleet,
:share_to_our_fleet_date,

:deficinecy_no,
:status_id,
:ms_user_id,
:action_taken_by_vessel,
:class_involved_nk_department,
:class_involved_nk_name,
:class_involved,
:deficiency_code_id,
:deficiency,
:cause_of_deficiency,
:action_taken_by_company,
:corrective_action,
:item_remarks

)
RETURNING comment_item_id;

";
                #endregion

                List<SqlParamData> plist = new List<SqlParamData>();

                this.delete_flag = false;
                this.create_ms_user_id = requser.ms_user_id;
                this.update_ms_user_id = requser.ms_user_id;

                #region パラメータ挿入
                
                plist.Add(new SqlParamData() { Name = "comment_id", Value = this.comment_id });

                plist.Add(new SqlParamData() { Name = "ms_vessel_id", Value = this.ms_vessel_id });
                plist.Add(new SqlParamData() { Name = "ms_crew_matrix_type_id", Value = this.ms_crew_matrix_type_id });
                plist.Add(new SqlParamData() { Name = "item_kind_id", Value = this.item_kind_id });
                plist.Add(new SqlParamData() { Name = "date", Value = this.date });
                plist.Add(new SqlParamData() { Name = "search_keyword", Value = this.search_keyword });
                
                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "create_ms_user_id", Value = this.create_ms_user_id });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });
                

                plist.Add(new SqlParamData() { Name = "deficinecy_count", Value = this.deficinecy_count });

                plist.Add(new SqlParamData() { Name = "ms_basho_id", Value = this.ms_basho_id });
                plist.Add(new SqlParamData() { Name = "ms_regional_code", Value = this.ms_regional_code });
                plist.Add(new SqlParamData() { Name = "comment_remarks", Value = this.comment_remarks });

                plist.Add(new SqlParamData() { Name = "share_to_our_fleet", Value = this.share_to_our_fleet });
                plist.Add(new SqlParamData() { Name = "share_to_our_fleet_date", Value = this.share_to_our_fleet_date });

                plist.Add(new SqlParamData() { Name = "deficinecy_no", Value = this.deficinecy_no });
                plist.Add(new SqlParamData() { Name = "status_id", Value = this.status_id });
                plist.Add(new SqlParamData() { Name = "ms_user_id", Value = this.ms_user_id });

                plist.Add(new SqlParamData() { Name = "action_taken_by_vessel", Value = this.action_taken_by_vessel });
                plist.Add(new SqlParamData() { Name = "class_involved_nk_department", Value = this.class_involved_nk_department });
                plist.Add(new SqlParamData() { Name = "class_involved_nk_name", Value = this.class_involved_nk_name });
                plist.Add(new SqlParamData() { Name = "class_involved", Value = this.class_involved });
                plist.Add(new SqlParamData() { Name = "deficiency_code_id", Value = this.deficiency_code_id });
                plist.Add(new SqlParamData() { Name = "deficiency", Value = this.deficiency });
                plist.Add(new SqlParamData() { Name = "cause_of_deficiency", Value = this.cause_of_deficiency });
                plist.Add(new SqlParamData() { Name = "action_taken_by_company", Value = this.action_taken_by_company });
                plist.Add(new SqlParamData() { Name = "corrective_action", Value = this.corrective_action });
                plist.Add(new SqlParamData() { Name = "item_remarks", Value = this.item_remarks });
                

                #endregion


                ans = (int)ExecuteScalar(cone, sql, plist);
            }
            catch (Exception e)
            {
                throw new Exception("DcCiPscInspection InsertRecord", e);
            }

            return ans;
        }

        /// <summary>
        /// レコードの更新
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public override bool UpdateRecord(NpgsqlConnection cone, MsUser requser)
        {
            bool ret = false;

            try
            {
                #region SQL
                string sql = @"
UPDATE
dc_ci_psc_inspection

SET
comment_id = :comment_id,
ms_vessel_id = :ms_vessel_id,
ms_crew_matrix_type_id = :ms_crew_matrix_type_id,
item_kind_id = :item_kind_id,
date = :date,
search_keyword = :search_keyword,

delete_flag = :delete_flag,
update_ms_user_id = :update_ms_user_id,

deficinecy_count = :deficinecy_count,
ms_basho_id = :ms_basho_id,
ms_regional_code = :ms_regional_code,
comment_remarks = :comment_remarks,

share_to_our_fleet = :share_to_our_fleet,
share_to_our_fleet_date = :share_to_our_fleet_date,

deficinecy_no = :deficinecy_no,
status_id = :status_id,
ms_user_id = :ms_user_id,

action_taken_by_vessel = :action_taken_by_vessel,

class_involved_nk_department = :class_involved_nk_department,
class_involved_nk_name = :class_involved_nk_name,
class_involved = :class_involved,

deficiency_code_id = :deficiency_code_id,
deficiency = :deficiency,

cause_of_deficiency = :cause_of_deficiency,
action_taken_by_company = :action_taken_by_company,
corrective_action = :corrective_action,
item_remarks = :item_remarks

WHERE
comment_item_id = :comment_item_id;
";
                #endregion

                List<SqlParamData> plist = new List<SqlParamData>();

                this.delete_flag = false;
                this.update_ms_user_id = requser.ms_user_id;

                #region パラメータ挿入

                plist.Add(new SqlParamData() { Name = "comment_id", Value = this.comment_id });

                plist.Add(new SqlParamData() { Name = "ms_vessel_id", Value = this.ms_vessel_id });
                plist.Add(new SqlParamData() { Name = "ms_crew_matrix_type_id", Value = this.ms_crew_matrix_type_id });
                plist.Add(new SqlParamData() { Name = "item_kind_id", Value = this.item_kind_id });
                plist.Add(new SqlParamData() { Name = "date", Value = this.date });                
                plist.Add(new SqlParamData() { Name = "search_keyword", Value = this.search_keyword });


                plist.Add(new SqlParamData() { Name = "delete_flag", Value = this.delete_flag });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = this.update_ms_user_id });
                

                plist.Add(new SqlParamData() { Name = "deficinecy_count", Value = this.deficinecy_count });

                plist.Add(new SqlParamData() { Name = "ms_basho_id", Value = this.ms_basho_id });
                plist.Add(new SqlParamData() { Name = "ms_regional_code", Value = this.ms_regional_code });
                plist.Add(new SqlParamData() { Name = "comment_remarks", Value = this.comment_remarks });

                plist.Add(new SqlParamData() { Name = "share_to_our_fleet", Value = this.share_to_our_fleet });
                plist.Add(new SqlParamData() { Name = "share_to_our_fleet_date", Value = this.share_to_our_fleet_date });

                plist.Add(new SqlParamData() { Name = "deficinecy_no", Value = this.deficinecy_no });
                plist.Add(new SqlParamData() { Name = "status_id", Value = this.status_id });
                plist.Add(new SqlParamData() { Name = "ms_user_id", Value = this.ms_user_id });

                plist.Add(new SqlParamData() { Name = "action_taken_by_vessel", Value = this.action_taken_by_vessel });
                plist.Add(new SqlParamData() { Name = "class_involved_nk_department", Value = this.class_involved_nk_department });
                plist.Add(new SqlParamData() { Name = "class_involved_nk_name", Value = this.class_involved_nk_name });
                plist.Add(new SqlParamData() { Name = "class_involved", Value = this.class_involved });
                plist.Add(new SqlParamData() { Name = "deficiency_code_id", Value = this.deficiency_code_id });
                plist.Add(new SqlParamData() { Name = "deficiency", Value = this.deficiency });
                plist.Add(new SqlParamData() { Name = "cause_of_deficiency", Value = this.cause_of_deficiency });
                plist.Add(new SqlParamData() { Name = "action_taken_by_company", Value = this.action_taken_by_company });
                plist.Add(new SqlParamData() { Name = "corrective_action", Value = this.corrective_action });
                plist.Add(new SqlParamData() { Name = "item_remarks", Value = this.item_remarks });
                
                
                plist.Add(new SqlParamData() { Name = "comment_item_id", Value = this.comment_item_id });

                #endregion


                //実行！
                ret = ExecuteNonQuery(cone, sql, plist);
            }
            catch (Exception e)
            {
                throw new Exception("DcCiPscInspection UpdateRecord", e);
            }

            return ret;
        }

        /// <summary>
        /// レコードの削除
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public override bool DeleteRecord(NpgsqlConnection cone, MsUser requser)
        {
            bool ret = false;

            try
            {
                //削除実行
                ret = ExecuteDelete(cone, requser, "dc_ci_psc_inspection", "comment_item_id", this.comment_item_id);
            }
            catch(Exception e)
            {
                throw new Exception("DcCiPscInspection DeleteRecord", e);
            }

            return ret;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
dc_ci_psc_inspection

SET
status_id = :status_id

WHERE
comment_item_id = :comment_item_id;
";
                #endregion

                List<SqlParamData> plist = new List<SqlParamData>();

                this.delete_flag = false;
                this.update_ms_user_id = requser.ms_user_id;

                #region パラメータ挿入

                plist.Add(new SqlParamData() { Name = "status_id", Value = this.status_id });
                

                plist.Add(new SqlParamData() { Name = "comment_item_id", Value = this.comment_item_id });

                #endregion


                //実行！
                ret = ExecuteNonQuery(cone, sql, plist);
            }
            catch (Exception e)
            {
                throw new Exception("DcCiPscInspection UpdateStatus", e);
            }

            return ret;
        }
        
    }
}
