using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;

using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;
using CIsl.DB.WingDAC;
using CIsl.DB;

namespace DcCommon.DB
{
    /// <summary>
    /// コメント親データまとめ
    /// DcComment    
    /// </summary>
    public class CommentData
    {
        /// <summary>
        /// コメント
        /// </summary>
        public DcComment Comment = null;


        /// <summary>
        /// コメント添付ファイルまとめ
        /// </summary>
        public List<DcAttachment> AttachmentList = new List<DcAttachment>();



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// コメントデータ取得(子供コメントアイテムなし)
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="comment_id"></param>
        /// <returns></returns>
        public static CommentData GetRecordByCommetIDWithoutCommentItem(NpgsqlConnection cone, int comment_id)
        {
            CommentData ans = null;

            try
            {
                //対象の取得
                DcComment com = DcComment.GetRecordByCommentID(cone, comment_id);
                if (com == null)
                {
                    return null;
                }

                ans = new CommentData();
                ans.Comment = com;

              

                //添付
                ans.AttachmentList = DcAttachment.GetRecrodsByCommentID(cone, com.comment_id);


            }
            catch (Exception e)
            {
                throw new Exception("CommentData GetRecordByCommetIDWithoutCommentItem", e);
            }

            return ans;
        }


        /// <summary>
        /// 挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public int Insert(NpgsqlConnection cone, MsUser requser)
        {
            int comment_id = BaseDac.EVal;

            try
            {

                //本体挿入
                comment_id = this.Comment.InsertRecord(cone, requser);


                //添付があるなら挿入
                if (this.AttachmentList != null)
                {
                    this.InsertAttachment(cone, requser, comment_id);
                }
                                


            }
            catch (Exception e)
            {
                throw new Exception("CommentData Insert", e);
            }

            return comment_id;
        }



        /// <summary>
        /// 更新 上でトランザクションを
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="requser">更新ユーザー</param>
        /// <returns></returns>
        public bool Update(NpgsqlConnection cone, MsUser requser)
        {
            try
            {

                //必要なら本体更新
                this.Comment.UpdateRecord(cone, requser);
                

                //添付があるなら削除or挿入
                if (this.AttachmentList != null)
                {
                    this.UpdateAttachment(cone, requser, this.Comment.comment_id);
                }
                
             

            }
            catch (Exception e)
            {
                throw new Exception("CommentData Update", e);
            }

            return true;
        }



        /// <summary>
        /// 親の削除
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool Delete(NpgsqlConnection cone, MsUser requser)
        {
            try
            {

                //必要なら本体更新
                this.Comment.DeleteRecord(cone, requser);


                //添付があるなら削除
                if (this.AttachmentList != null)
                {
                    foreach (DcAttachment at in this.AttachmentList)
                    {
                        at.DeleteRecord(cone, requser);
                    }
                }


              

            }
            catch (Exception e)
            {
                throw new Exception("CommentData Delete", e);
            }


            return true;

        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 添付ファイルの挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <param name="comment_id"></param>
        protected void InsertAttachment(NpgsqlConnection cone, MsUser requser, int comment_id)
        {
            foreach (DcAttachment attach in this.AttachmentList)
            {
                attach.InsertRecordComment(cone, requser, comment_id);
            }
        }

        /// <summary>
        /// 添付ﾌｧｲﾙの更新
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <param name="comment_id"></param>
        protected void UpdateAttachment(NpgsqlConnection cone, MsUser requser, int comment_id)
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
                    int id = attach.InsertRecordComment(cone, requser, comment_id);
                }
            }
        }


    }
}
