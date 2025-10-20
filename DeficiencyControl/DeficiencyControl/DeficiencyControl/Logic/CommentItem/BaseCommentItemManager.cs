using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DcCommon;
using DcCommon.DB;
using DcCommon.DB.DAC;

using DeficiencyControl.Util;
using DeficiencyControl.Controls;
using DeficiencyControl.Controls.CommentItem;
using CIsl.DB.WingDAC;


namespace DeficiencyControl.Logic.CommentItem
{

    

    /// <summary>
    /// コメントアイテム管理基底
    /// </summary>
    public abstract class BaseCommentItemManager
    {
        public BaseCommentItemManager(ECommentItemKind kind)
        {
            this.ItemKind = kind;
        }

        /// <summary>
        /// 自分の種別
        /// </summary>
        public ECommentItemKind ItemKind = ECommentItemKind.Max;

        /// <summary>
        /// このクラスの初期化
        /// </summary>
        /// <param name="comment_item_id">管理するCommentItemID DcCommentItem.EVal = 新規</param>
        public abstract void Init(int comment_item_id);


        /// <summary>
        /// 管理データのｽﾃｰﾀｽがCompleteかをチェックする true=Complete
        /// </summary>
        /// <returns></returns>
        public virtual bool CheckComplete()
        {
            return false;
        }

        /// <summary>
        /// 指摘事項数
        /// </summary>
        public virtual int DeficiencyCount
        {
            get
            {
                return 0;
            }            
        }


        /// <summary>
        /// データエラーチェック
        /// </summary>
        /// <param name="headcon"></param>
        /// <param name="dcllist"></param>
        /// <returns></returns>
        protected bool CheckDataError(BaseCommentItemHeaderControl headcon, List<BaseCommentItemDetailControl> dcllist)
        {
            bool eret = false;

            //全エラーチェック
            eret |= !headcon.CheckError(true);
            foreach (BaseCommentItemDetailControl dc in dcllist)
            {
                eret |= !dc.CheckError(true);
            }

            //エラーある？
            if (eret == false)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 入力チェック単品版
        /// </summary>
        /// <param name="headcon"></param>
        /// <param name="dc"></param>
        /// <returns></returns>
        protected bool CheckDataError(BaseCommentItemHeaderControl headcon, BaseCommentItemDetailControl dc)
        {
            List<BaseCommentItemDetailControl> dclist = new List<BaseCommentItemDetailControl>();
            dclist.Add(dc);

            return this.CheckDataError(headcon, dclist);
        }



        /// <summary>
        /// エラーリセット処理
        /// </summary>
        /// <param name="headcon"></param>
        /// <param name="dcllist"></param>
        public virtual void ResetError(BaseCommentItemHeaderControl headcon, List<BaseCommentItemDetailControl> dcllist)
        {
            headcon.ResetError();
            foreach (BaseCommentItemDetailControl dc in dcllist)
            {
                dc.ResetError();
            }

        }

        /// <summary>
        /// エラーリセット単品版
        /// </summary>
        /// <param name="headcon"></param>
        /// <param name="dc"></param>
        public virtual void ResetError(BaseCommentItemHeaderControl headcon, BaseCommentItemDetailControl dc)
        {
            List<BaseCommentItemDetailControl> dclist = new List<BaseCommentItemDetailControl>();
            dclist.Add(dc);

            this.ResetError(headcon, dclist);

        }

        /// <summary>
        /// 挿入処理
        /// </summary>
        /// <param name="headcon">ヘッダーコントロール</param>
        /// <param name="dcllist">詳細コントロール一式</param>
        public abstract void InsertData(BaseCommentItemHeaderControl headcon, List<BaseCommentItemDetailControl> dcllist);


        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="headcon"></param>
        /// <param name="detailcon"></param>
        public abstract void UpdateData(BaseCommentItemHeaderControl headcon, BaseCommentItemDetailControl detailcon);


        /// <summary>
        /// 削除処理
        /// </summary>
        public abstract void DeleteData();
    }
}
