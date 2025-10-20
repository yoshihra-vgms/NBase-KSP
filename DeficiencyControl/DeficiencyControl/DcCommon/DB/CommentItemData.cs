using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CIsl.DB.WingDAC;
using Npgsql;

using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;

namespace DcCommon.DB
{
    //コメントアイテム関連のまとめデータ
    public class BaseCommentItemData
    {
        /// <summary>
        /// 親データまとめ これは参照だけとすること。
        /// </summary>
        public CommentData ParentData = null;   

        /// <summary>
        /// コメントアイテム添付ファイルまとめ
        /// </summary>
        public List<DcAttachment> AttachmentList = null;


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 添付ファイルの挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <param name="comment_item_id"></param>
        protected void InsertAttachment(NpgsqlConnection cone, MsUser requser, int comment_item_id)
        {
            foreach (DcAttachment attach in this.AttachmentList)
            {
                attach.InsertRecordCommentItem(cone, requser, comment_item_id);
            }
        }

        /// <summary>
        /// 添付ﾌｧｲﾙの更新
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <param name="comment_item_id"></param>
        protected void UpdateAttachment(NpgsqlConnection cone, MsUser requser, int comment_item_id)
        {
            foreach (DcAttachment attach in this.AttachmentList)
            {
                //削除フラグが立っているなら削除
                if (attach.delete_flag == true)
                {
                    bool ret = attach.DeleteRecord(cone, requser);
                }
                else
                {
                    //それ以外は挿入
                    int id = attach.InsertRecordCommentItem(cone, requser, comment_item_id);
                }
            }
        }
        

                
        public virtual int Insert(NpgsqlConnection cone, MsUser requser)
        {
            throw new NotImplementedException();
        }

        public virtual bool Update(NpgsqlConnection cone, MsUser requser)
        {
            throw new NotImplementedException();
        }
        public virtual bool Delete(NpgsqlConnection cone, MsUser requser)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 対象の添付ファイル一覧を取得
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<DcAttachment> GetSelectAttachmentTypeList(EAttachmentType type)
        {
            var sel = from at in this.AttachmentList where at.attachment_type_id == (int)type select at;

            List<DcAttachment> anslist = sel.ToList();
            return anslist;
        }
    }





    


    

    
}
