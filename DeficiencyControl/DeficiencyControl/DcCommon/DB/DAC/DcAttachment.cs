using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;
using CIsl.DB;
using CIsl.DB.WingDAC;

namespace DcCommon.DB.DAC
{
    


    /// <summary>
    /// 添付ファイルテーブル
    /// dc_attachment
    /// <remarks>このクラスをまとめて取得する時は、file_dataを取得することを避けること。file_dataは巨大な可能性があるため、個別で取得すること</remarks>
    /// </summary>
    public class DcAttachment : BaseDac
    {
        public override int ID
        {
            get
            {
                return DBTableID.dc_attachment;
            }
        }

        #region メンバ変数
        /// <summary>
        /// 添付ファイルID
        /// </summary>
        public int attachment_id = EVal;

        /// <summary>
        /// ファイル名
        /// </summary>
        public string filename = "";


        /// <summary>
        /// アイコンデータ
        /// </summary>
        public byte[] icon_data = null;

        /// <summary>
        /// ファイルデータ
        /// </summary>
        public byte[] file_data = null;

        /// <summary>
        /// ファイル種別
        /// </summary>
        public int attachment_type_id = EVal;
        
        #endregion


        /// <summary>
        /// 添付ファイル種別名
        /// </summary>
        public string AttachmentTypeName { get; set; }

        
        /// <summary>
        /// 添付ファイル種別取得と設定        
        /// </summary>             
        public EAttachmentType AttachmentType
        {
            //なぜかSet句を書くとWCFがエラーを履くようになる。あきらめたのでtype設定はattachment_type_idをしようすること。
            get
            {
                EAttachmentType ans = EAttachmentType.MAX;

                ans = (EAttachmentType)this.attachment_type_id;

                return ans;
            }
            
        }


        /// <summary>
        /// 基本SELECT
        /// </summary>
        private static string DefaultSelect = @"
SELECT
dc_attachment.attachment_id,
dc_attachment.filename,
dc_attachment.icon_data,
dc_attachment.attachment_type_id,

dc_attachment.delete_flag,
dc_attachment.create_ms_user_id,
dc_attachment.update_ms_user_id,
dc_attachment.create_date,
dc_attachment.update_date,

ms_attachment_type.attachment_type_name

FROM
dc_attachment

LEFT OUTER JOIN ms_attachment_type ON ms_attachment_type.attachment_type_id = dc_attachment.attachment_type_id

";
        /// <summary>
        /// データの読み込み
        /// </summary>
        /// <param name="dr"></param>
        protected override void ReadAll(System.Data.Common.DbDataReader dr)
        {
            //既存メンバの読み込み
            base.ReadAll(dr);

            //添付ファイル種別名の取得
            this.AttachmentTypeName = (string)GetSafe(dr["attachment_type_name"], "");
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        /// <summary>
        /// コメントアイテムに関するファイルを取得
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="comitem_id">取得コメントアイテムID</param>
        /// <returns></returns>
        public static List<DcAttachment> GetRecrodsByCommentItemID(NpgsqlConnection cone, int comitem_id)
        {
            List<DcAttachment> anslist = new List<DcAttachment>();

            try
            {
                string sql = DefaultSelect + @"
INNER JOIN dc_comment_item_attachment ON dc_comment_item_attachment.attachment_id = dc_attachment.attachment_id

WHERE
dc_attachment.delete_flag = false
AND
dc_comment_item_attachment.comment_item_id = :comitem_id
";
                //パラメータ作成
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "comitem_id", Value = comitem_id });

                //取得
                anslist = GetRecordsList<DcAttachment>(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcAttachment GetRecrodsByCommentItemID", e);
            }

            return anslist;
        }

        /// <summary>
        /// コメント親アイテムに関するファイルを取得
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="comment_id">取得コメント親ID</param>
        /// <returns></returns>
        public static List<DcAttachment> GetRecrodsByCommentID(NpgsqlConnection cone, int comment_id)
        {
            List<DcAttachment> anslist = new List<DcAttachment>();

            try
            {
                string sql = DefaultSelect + @"
INNER JOIN dc_comment_attachment ON dc_comment_attachment.attachment_id = dc_attachment.attachment_id

WHERE
dc_attachment.delete_flag = false
AND
dc_comment_attachment.comment_id = :comment_id
";
                //パラメータ作成
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "comment_id", Value = comment_id });

                //取得
                anslist = GetRecordsList<DcAttachment>(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcAttachment GetRecrodsByCommentID", e);
            }

            return anslist;
        }
        


        /// <summary>
        /// 対象コメントに関するファイルをすべて取得する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="comment_item_id"></param>
        /// <returns></returns>
        public static List<DcAttachment> GetARecrodsAllByCommentItemID(NpgsqlConnection cone, int comment_item_id)
        {
            List<DcAttachment> anslist = new List<DcAttachment>();

            try
            {
                string sql = DefaultSelect + @"
LEFT JOIN dc_comment_item_attachment ON dc_comment_item_attachment.attachment_id = dc_attachment.attachment_id
LEFT JOIN dc_comment_attachment ON dc_comment_attachment.attachment_id = dc_attachment.attachment_id
LEFT JOIN dc_comment_item ON dc_comment_item.comment_id = dc_comment_attachment.comment_id

WHERE
dc_attachment.delete_flag = false
AND
(
dc_comment_item_attachment.comment_item_id = :comment_item_id 
OR
dc_comment_item.comment_item_id = :comment_item_id
)
";
                //パラメータ作成
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "comment_item_id", Value = comment_item_id });

                //取得
                anslist = GetRecordsList<DcAttachment>(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcAttachment GetARecrodsAllByCommentItemID", e);
            }

            return anslist;
        }





        /// <summary>
        /// Accidentに関するファイルを一覧取得
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="accident_id"></param>
        /// <returns></returns>
        public static List<DcAttachment> GetRecrodsByAccidentID(NpgsqlConnection cone, int accident_id)
        {
            List<DcAttachment> anslist = new List<DcAttachment>();


            try
            {
                string sql = DefaultSelect + @"
INNER JOIN dc_accident_attachment ON dc_accident_attachment.attachment_id = dc_attachment.attachment_id

WHERE
dc_attachment.delete_flag = false
AND
dc_accident_attachment.accident_id = :accident_id
";
                //パラメータ作成
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "accident_id", Value = accident_id });

                //取得
                anslist = GetRecordsList<DcAttachment>(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcAttachment GetRecrodsByAccidentID", e);
            }

            return anslist;
        }


        /// <summary>
        /// Accident進捗添付ファイルの一覧取得
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="accident_progress_id"></param>
        /// <returns></returns>
        public static List<DcAttachment> GetRecrodsByAccidentProgressID(NpgsqlConnection cone, int accident_progress_id)
        {
            List<DcAttachment> anslist = new List<DcAttachment>();
            

            try
            {
                string sql = DefaultSelect + @"
INNER JOIN dc_accident_progress_attachment ON dc_accident_progress_attachment.attachment_id = dc_attachment.attachment_id

WHERE
dc_attachment.delete_flag = false
AND
dc_accident_progress_attachment.accident_progress_id = :accident_progress_id
";
                //パラメータ作成
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "accident_progress_id", Value = accident_progress_id });

                //取得
                anslist = GetRecordsList<DcAttachment>(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcAttachment GetRecrodsByAccidentProgressID", e);
            }

            return anslist;
        }




        /// <summary>
        /// Accident報告書添付ファイルの一覧取得
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="accident_reports_id"></param>
        /// <returns></returns>
        public static List<DcAttachment> GetRecrodsByAccidentReportsID(NpgsqlConnection cone, int accident_reports_id)
        {
            List<DcAttachment> anslist = new List<DcAttachment>();


            try
            {
                string sql = DefaultSelect + @"
INNER JOIN dc_accident_reports_attachment ON dc_accident_reports_attachment.attachment_id = dc_attachment.attachment_id

WHERE
dc_attachment.delete_flag = false
AND
dc_accident_reports_attachment.accident_reports_id = :accident_reports_id
";
                //パラメータ作成
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "accident_reports_id", Value = accident_reports_id });

                //取得
                anslist = GetRecordsList<DcAttachment>(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcAttachment GetRecrodsByAccidentReportsID", e);
            }

            return anslist;
        }


        /// <summary>
        /// Accidentにかかわるすべてのファイルを取得
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="comment_item_id"></param>
        /// <returns></returns>
        public static List<DcAttachment> GetARecrodsAllByAccidentID(NpgsqlConnection cone, int accident_id)
        {
            List<DcAttachment> anslist = new List<DcAttachment>();

            try
            {
                string sql = DefaultSelect + @"
LEFT JOIN dc_accident_attachment ON dc_accident_attachment.attachment_id = dc_attachment.attachment_id
LEFT JOIN dc_accident ON dc_accident.accident_id = dc_accident_attachment.accident_id

LEFT JOIN dc_accident_progress_attachment ON dc_accident_progress_attachment.attachment_id = dc_attachment.attachment_id
LEFT JOIN dc_accident_progress ON dc_accident_progress.accident_progress_id = dc_accident_progress_attachment.accident_progress_id


LEFT JOIN dc_accident_reports_attachment ON dc_accident_reports_attachment.attachment_id = dc_attachment.attachment_id
LEFT JOIN dc_accident_reports ON dc_accident_reports.accident_reports_id = dc_accident_reports_attachment.accident_reports_id

WHERE
dc_attachment.delete_flag = false
AND
(
dc_accident.accident_id = :accident_id
OR
dc_accident_progress.accident_id = :accident_id
OR
dc_accident_reports.accident_id = :accident_id
)

ORDER BY
dc_attachment.update_date

";
                //パラメータ作成
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "accident_id", Value = accident_id });

                //取得
                anslist = GetRecordsList<DcAttachment>(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcAttachment GetARecrodsAllByAccidentID", e);
            }

            return anslist;
        }



        /// <summary>
        /// 検船添付ファイルの一覧取得
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="accident_reports_id"></param>
        /// <returns></returns>
        public static List<DcAttachment> GetRecrodsByMoiID(NpgsqlConnection cone, int moi_id)
        {
            List<DcAttachment> anslist = new List<DcAttachment>();


            try
            {
                string sql = DefaultSelect + @"
INNER JOIN dc_moi_attachment ON dc_moi_attachment.attachment_id = dc_attachment.attachment_id

WHERE
dc_attachment.delete_flag = false
AND
dc_moi_attachment.moi_id = :moi_id
";
                //パラメータ作成
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "moi_id", Value = moi_id });

                //取得
                anslist = GetRecordsList<DcAttachment>(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcAttachment GetRecrodsByMoiID", e);
            }

            return anslist;
        }


        /// <summary>
        /// 検船指摘事項添付ファイルの一覧取得
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="moi_observation_id"></param>
        /// <returns></returns>
        public static List<DcAttachment> GetRecrodsByMoiObservationID(NpgsqlConnection cone, int moi_observation_id)
        {
            List<DcAttachment> anslist = new List<DcAttachment>();


            try
            {
                string sql = DefaultSelect + @"
INNER JOIN dc_moi_observation_attachment ON dc_moi_observation_attachment.attachment_id = dc_attachment.attachment_id

WHERE
dc_moi_observation_attachment.delete_flag = false
AND
dc_moi_observation_attachment.moi_observation_id = :moi_observation_id
";
                //パラメータ作成
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "moi_observation_id", Value = moi_observation_id });

                //取得
                anslist = GetRecordsList<DcAttachment>(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcAttachment GetRecrodsByMoiObservationID", e);
            }

            return anslist;
        }

        /// <summary>
        /// 検船指摘事項に係るファイルを全て取得する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="moi_observation_id"></param>
        /// <returns></returns>
        public static List<DcAttachment> GetARecrodsAllByMoi(NpgsqlConnection cone, int moi_observation_id)
        {
            List<DcAttachment> anslist = new List<DcAttachment>();

            try
            {
                /*
                string sql = DefaultSelect + @"
LEFT JOIN dc_moi_observation_attachment ON dc_moi_observation_attachment.attachment_id = dc_attachment.attachment_id
LEFT JOIN dc_moi_attachment ON dc_moi_attachment.attachment_id = dc_attachment.attachment_id

WHERE
dc_attachment.delete_flag = false
AND
(
dc_moi_observation_attachment.moi_observation_id = :moi_observation_id
OR
dc_moi_attachment.moi_id = :moi_id

)

ORDER BY
dc_attachment.update_date

";*/

                string sql = DefaultSelect + @"
LEFT JOIN dc_moi_observation_attachment ON dc_moi_observation_attachment.attachment_id = dc_attachment.attachment_id


LEFT JOIN dc_moi_attachment ON dc_moi_attachment.attachment_id = dc_attachment.attachment_id
LEFT JOIN dc_moi_observation ON dc_moi_observation.moi_id = dc_moi_attachment.moi_id


WHERE
dc_attachment.delete_flag = false
AND
(
dc_moi_observation_attachment.moi_observation_id = :moi_observation_id
OR
dc_moi_observation.moi_observation_id = :moi_observation_id

)

ORDER BY
dc_attachment.update_date
";

                //パラメータ作成
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "moi_observation_id", Value = moi_observation_id });

                //取得
                anslist = GetRecordsList<DcAttachment>(cone, sql, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcAttachment GetARecrodsAllByMoi", e);
            }

            return anslist;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 対象をデータ付で取得する。
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="attachment_id">取得対象</param>
        /// <returns></returns>
        public static DcAttachment DownloadAttachment(NpgsqlConnection cone, int attachment_id)
        {
            DcAttachment ans = null;
            try
            {
                #region SQL
                string sql = @"
SELECT
dc_attachment.attachment_id,
dc_attachment.filename,
dc_attachment.icon_data,
dc_attachment.file_data,
dc_attachment.attachment_type_id,

dc_attachment.delete_flag,
dc_attachment.create_ms_user_id,
dc_attachment.update_ms_user_id,
dc_attachment.create_date,
dc_attachment.update_date,

ms_attachment_type.attachment_type_name

FROM
dc_attachment

LEFT OUTER JOIN ms_attachment_type ON ms_attachment_type.attachment_type_id = dc_attachment.attachment_type_id

WHERE
dc_attachment.attachment_id = :attachment_id
AND
dc_attachment.delete_flag = false
";
                #endregion

                //パラメータ
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "attachment_id", Value = attachment_id });

                //実行
                ans = GetRecordData<DcAttachment>(cone, sql, plist);

            }
            catch(Exception e)
            {
                throw new Exception("DcAttachment GetRecordByAttachmentIDWithFileData", e);
            }

            return ans;
        }


        /// <summary>
        /// レコードの挿入 これ単体では使わないようにすること
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public override int InsertRecord(NpgsqlConnection cone, MsUser requser)
        {
            int ans = EVal;

            try
            {

                string sql = @"
INSERT INTO dc_attachment
(
filename,
icon_data,
file_data,
attachment_type_id,

delete_flag,
create_ms_user_id,
update_ms_user_id
)
VALUES
(
:filename,
:icon_data,
:file_data,
:attachment_type_id,
:delete_flag,
:create_ms_user_id,
:update_ms_user_id
)
RETURNING attachment_id;
";

                this.delete_flag = false;
                this.create_ms_user_id = requser.ms_user_id;
                this.update_ms_user_id = requser.ms_user_id;

                
                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "filename", Value = filename });
                plist.Add(new SqlParamData() { Name = "icon_data", Value = icon_data });
                plist.Add(new SqlParamData() { Name = "file_data", Value = file_data });
                plist.Add(new SqlParamData() { Name = "attachment_type_id", Value = attachment_type_id });

                plist.Add(new SqlParamData() { Name = "delete_flag", Value = delete_flag });
                plist.Add(new SqlParamData() { Name = "create_ms_user_id", Value = create_ms_user_id });
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = update_ms_user_id });

                //実行
                ans = (int)this.ExecuteScalar(cone, sql, plist);


            }
            catch (Exception e)
            {
                throw e;
            }

            return ans;
        }

                
        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <param name="requser">ユーザー</param>
        /// <returns></returns>
        public override bool DeleteRecord(NpgsqlConnection cone, MsUser requser)
        {
            bool ret = true;
            try
            {
                ret = this.ExecuteDelete(cone, requser, "dc_attachment", "attachment_id", this.attachment_id);

                //接続テーブルの削除                
                this.ExecuteDelete(cone, requser, "dc_comment_attachment", "attachment_id", this.attachment_id);
                this.ExecuteDelete(cone, requser, "dc_comment_item_attachment", "attachment_id", this.attachment_id);

                this.ExecuteDelete(cone, requser, "dc_accident_attachment", "attachment_id", this.attachment_id);
                this.ExecuteDelete(cone, requser, "dc_accident_progress_attachment", "attachment_id", this.attachment_id);
                this.ExecuteDelete(cone, requser, "dc_accident_reports_attachment", "attachment_id", this.attachment_id);

                this.ExecuteDelete(cone, requser, "dc_moi_attachment", "attachment_id", this.attachment_id);
                this.ExecuteDelete(cone, requser, "dc_moi_observation_attachment", "attachment_id", this.attachment_id);

                
            }
            catch (Exception e)
            {
                throw e;
            }

            return ret;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// コメントアイテム添付ファイルの挿入 トランザクションすること 挿入したファイルIDの返却
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="requser">要求ユーザー</param>
        /// <param name="comment_item_id">関連付けるコメントアイテムID</param>        
        /// <returns>進捗ID</returns>
        public int InsertRecordCommentItem(NpgsqlConnection cone, MsUser requser, int comment_item_id)
        {
            //ファイル挿入
            int atid = this.InsertRecord(cone, requser);


            //中間テーブルの挿入
            DcCommentItemAttachment cat = new DcCommentItemAttachment();
            cat.comment_item_id = comment_item_id;
            cat.attachment_id = atid;


            atid = cat.InsertRecord(cone, requser);

            return atid;
        }


        /// <summary>
        /// コメント親添付ファイルの挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <param name="comment_id"></param>
        /// <returns></returns>
        public int InsertRecordComment(NpgsqlConnection cone, MsUser requser, int comment_id)
        {
            //ファイル挿入
            int atid = this.InsertRecord(cone, requser);


            //中間テーブルの挿入
            DcCommentAttachment cat = new DcCommentAttachment();
            cat.comment_id = comment_id;
            cat.attachment_id = atid;


            atid = cat.InsertRecord(cone, requser);

            return atid;
        }


        /// <summary>
        /// 事故トラブル添付ファイルの挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <param name="accident_id"></param>
        /// <returns></returns>
        public int InsertRecordAccident(NpgsqlConnection cone, MsUser requser, int accident_id)
        {
            //ファイル挿入
            int atid = this.InsertRecord(cone, requser);


            //中間テーブルの挿入
            DcAccidentAttachment aa = new DcAccidentAttachment();
            aa.accident_id = accident_id;
            aa.attachment_id = atid;


            atid = aa.InsertRecord(cone, requser);

            return atid;
        }

        /// <summary>
        /// 事故トラブル進捗添付ファイルの挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <param name="accident_progress_id"></param>
        /// <returns></returns>
        public int InsertRecordAccidentProgress(NpgsqlConnection cone, MsUser requser, int accident_progress_id)
        {
            //ファイル挿入
            int atid = this.InsertRecord(cone, requser);


            //中間テーブルの挿入
            DcAccidentProgressAttachment ap = new DcAccidentProgressAttachment();
            ap.accident_progress_id = accident_progress_id;
            ap.attachment_id = atid;


            atid = ap.InsertRecord(cone, requser);

            return atid;
        }

        /// <summary>
        /// 事故トラブル報告書提出先 添付ファイルの挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <param name="accident_reports_id"></param>
        /// <returns></returns>
        public int InsertRecordAccidentReports(NpgsqlConnection cone, MsUser requser, int accident_reports_id)
        {
            //ファイル挿入
            int atid = this.InsertRecord(cone, requser);


            //中間テーブルの挿入
            DcAccidentReportsAttachment ar = new DcAccidentReportsAttachment();
            ar.accident_reports_id = accident_reports_id;
            ar.attachment_id = atid;




            atid = ar.InsertRecord(cone, requser);

            return atid;
        }



        /// <summary>
        /// 検船添付ファイルの挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <param name="accident_progress_id"></param>
        /// <returns></returns>
        public int InsertRecordMoi(NpgsqlConnection cone, MsUser requser, int moi_id)
        {
            //ファイル挿入
            int atid = this.InsertRecord(cone, requser);


            //中間テーブルの挿入
            DcMoiAttachment moi = new DcMoiAttachment();
            moi.moi_id = moi_id;
            moi.attachment_id = atid;


            atid = moi.InsertRecord(cone, requser);

            return atid;
        }


        /// <summary>
        /// 検船指摘事項 添付ファイルの挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <param name="moi_observation_id"></param>
        /// <returns></returns>
        public int InsertRecordMoiObservation(NpgsqlConnection cone, MsUser requser, int moi_observation_id)
        {
            //ファイル挿入
            int atid = this.InsertRecord(cone, requser);


            //中間テーブルの挿入
            DcMoiObservationAttachment ob = new DcMoiObservationAttachment();
            ob.moi_observation_id = moi_observation_id;
            ob.attachment_id = atid;


            atid = ob.InsertRecord(cone, requser);

            return atid;
        }

    }
}
