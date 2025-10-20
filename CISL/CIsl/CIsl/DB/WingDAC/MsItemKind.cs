using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;

namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// 検査種別Item番号 DBのMsItemTypeのIDと番号をそろえること
    /// </summary>
    public enum ECommentItemKind
    {

        /// <summary>
        /// PSC Inspection
        /// </summary>
        PSC_Inspection = 1,

 
        //------------------------------
        Max
    }
   

    /// <summary>
    /// 検査種別マスタ
    /// ms_item_type
    /// </summary>
    public class MsItemKind : BaseDac
    {
        #region メンバ変数

        /// <summary>
        /// 検査種別ID
        /// </summary>
        public int item_kind_id = EVal;

        /// <summary>
        /// 検査種別名
        /// </summary>
        public string item_kind_name = "";

        #endregion

        //追加
        /// <summary>
        /// このクラスのEItemTypeを取得
        /// </summary>
        public ECommentItemKind ItemKindNo
        {
            get
            {
                //無効値
                if (this.item_kind_id == EVal)
                {
                    return ECommentItemKind.Max;
                }

                //取得
                ECommentItemKind ans = (ECommentItemKind)this.item_kind_id;
                return ans;
            }
        }
        



        public override string ToString()
        {
            return this.item_kind_name;
        }

        /// <summary>
        /// 基本SELECT
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_item_kind.item_kind_id,
ms_item_kind.item_kind_name,
ms_item_kind.delete_flag,
ms_item_kind.create_ms_user_id,
ms_item_kind.update_ms_user_id,
ms_item_kind.create_date,
ms_item_kind.update_date
FROM
ms_item_kind
";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <returns></returns>
        public static List<MsItemKind> GetRecords(NpgsqlConnection cone)
        {
            List<MsItemKind> anslist = new List<MsItemKind>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_item_kind.delete_flag = false
";
                anslist = GetRecordsList<MsItemKind>(cone, sql);

                
            }
            catch
            {
                return null;
            }

            return anslist;

        }


    }
}
