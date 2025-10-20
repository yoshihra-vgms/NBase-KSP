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
    /// コメントアイテムテーブル 検査コメントDBの基底クラス Insert文および、Update文は書くな！継承先に任せること
    /// dc_commnet_item
    /// </summary>
    public class DcCommentItem : BaseDac
    {
        public override int ID
        {
            get
            {
                return this.comment_item_id;
            }
        }

        #region メンバ変数

        /// <summary>
        /// コメントアイテムテーブルID
        /// </summary>
        public int comment_item_id = EVal;

        /// <summary>
        /// 親コメントテーブルID
        /// </summary>
        public int comment_id = EVal;

        /// <summary>
        /// 船ID
        /// </summary>
        public decimal ms_vessel_id = EVal;

        /// <summary>
        /// 船種ID
        /// </summary>
        public decimal ms_crew_matrix_type_id = EVal;

        /// <summary>
        /// コメント種別ID
        /// </summary>
        public int item_kind_id = EVal;

        /// <summary>
        /// 日付
        /// </summary>
        public DateTime date = EDate;

        /// <summary>
        /// 検索キーワード
        /// </summary>
        public string search_keyword = "";




        #endregion

        /// <summary>
        /// コメントTypeの取得
        /// </summary>
        public ECommentItemKind CommentItemKind
        {
            get
            {
                return (ECommentItemKind)this.item_kind_id;
            }
        }

        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
dc_comment_item.comment_item_id,

dc_comment_item.comment_id,
dc_comment_item.ms_vessel_id,
dc_comment_item.ms_crew_matrix_type_id,
dc_comment_item.item_kind_id,
dc_comment_item.date,
dc_comment_item.search_keyword,

dc_comment_item.delete_flag,
dc_comment_item.create_ms_user_id,
dc_comment_item.update_ms_user_id,
dc_comment_item.create_date,
dc_comment_item.update_date

FROM
dc_comment_item

";

        /// <summary>
        /// コメントの検索
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public static List<DcCommentItem> GetRecordsBySearchData(NpgsqlConnection cone, CommentItemSearchData sdata)
        {
            List<DcCommentItem> anslist = new List<DcCommentItem>();

            try
            {
                //SQL
                string sql = DefaultSelect + @"
WHERE
dc_comment_item.delete_flag = false
";

                //検索条件付加
                List<SqlParamData> paramlist = new List<SqlParamData>();
                sql += sdata.CreateSQLWhere(out paramlist);

                sql += @"
ORDER BY
dc_comment_item.update_date DESC
";

                //SQL取得
                anslist = GetRecordsList<DcCommentItem>(cone, sql, paramlist);

            }
            catch (Exception e)
            {
                throw new Exception("DcCommentItem GetRecordsSearchData", e);
            }

            return anslist;
        }



        /// <summary>
        /// 対象のIDでデータを取得
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="comment_item_id">コメントアイテムID</param>
        /// <returns></returns>
        public static DcCommentItem GetRecordByCommentItemID(NpgsqlConnection cone, int comment_item_id)
        {
            DcCommentItem ans = null;

            try
            {
                string sql = DefaultSelect + @"
WHERE
dc_comment_item.delete_flag = false
AND
dc_comment_item.comment_item_id = :comment_item_id
";

                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "comment_item_id", Value = comment_item_id });


                ans = GetRecordData<DcCommentItem>(cone, sql, plist);

            }
            catch(Exception e)
            {
                throw new Exception("DcCommentItem GetRecordByCommentItemID", e);
            }


            return ans;
        }


        /// <summary>
        /// 対象の種別のデータ数を取得   (GetRecordしてList.countだと重すぎるため、独自に作成する。)
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="item_kind_id"></param>
        /// <returns></returns>
        public static int GetCountByItemKindID(NpgsqlConnection cone, int item_kind_id)
        {
            int ans = 0;

            try
            {
                string sql = @"
SELECT count(*) as datacount
FROM
dc_comment_item
WHERE
delete_flag = false
AND
item_kind_id = :item_kind_id

";

                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "item_kind_id", Value = item_kind_id });

                ////////////////////////////////////////////////////////////////////////////////////////////////
                //読み込みの実行関数
                ReadDataDelegate del = (NpgsqlDataReader dr) =>
                {
                    int delans = 0;

                    while (dr.Read())
                    {
                        delans = Convert.ToInt32(GetSafe(dr["datacount"], 0));

                        //一つ読んだら終わり
                        break;
                    }

                    return delans;
                };
                ////////////////////////////////////////////////////////////////////////////////////////////////

                //実行！
                ans = (int)GetRecordObject(cone, sql, del, plist);

            }
            catch (Exception e)
            {
                throw new Exception("DcCommentItem GetCountByItemKindID", e);
            }


            return ans;
        }





    }
}
