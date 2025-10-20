using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;

using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;
using CIsl.DB;
using CIsl.DB.WingDAC;

namespace DcCommon.DB
{
    /// <summary>
    /// RegistCountData
    /// </summary>
    public class RegistCountData
    {
        #region メンバ変数
        /// <summary>
        /// タイトル
        /// </summary>
        public string Title = "";

        /// <summary>
        /// Kind
        /// </summary>
        public int Kind = BaseDac.EVal;

        /// <summary>
        /// データ数
        /// </summary>
        public int RegistCount = 0;
        #endregion


        /// <summary>
        /// レコードカウントの取得
        /// </summary>
        /// <param name="cone"></param>
        /// <returns></returns>
        public static List<RegistCountData> GetRegistCount(NpgsqlConnection cone)
        {
            List<RegistCountData> anslist = new List<RegistCountData>();

            //CommentItem
            {
                List<MsItemKind> kindlist = MsItemKind.GetRecords(cone);
                foreach (MsItemKind kind in kindlist)
                {
                    RegistCountData comdata = new RegistCountData();
                    comdata.Title = "CommentList";
                    comdata.Kind = kind.item_kind_id;

                    //取得
                    comdata.RegistCount = DcCommentItem.GetCountByItemKindID(cone, kind.item_kind_id);

                    //追加
                    anslist.Add(comdata);
                }
            }

          
            return anslist;
        }
    }
}
