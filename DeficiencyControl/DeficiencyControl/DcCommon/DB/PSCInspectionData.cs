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
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// PSC Inspectionデータまとめ
    /// </summary>
    public class PSCInspectionData : BaseCommentItemData
    {
        #region メンバ変数

        /// <summary>
        /// PSC Inspection
        /// </summary>
        public DcCiPscInspection PscInspection = null;


        /// <summary>
        /// ActionCodeリスト
        /// </summary>
        public List<DcActionCodeHistory> ActionCodeHistoryList = null;


        #endregion

        /// <summary>
        /// ひとつのPSCInspectionに関連するデータをすべて取得
        /// </summary>
        /// <param name="psc">対照データ</param>
        /// <returns></returns>
        private static PSCInspectionData CollectData(NpgsqlConnection cone, DcCiPscInspection psc)
        {
            PSCInspectionData ans = new PSCInspectionData();
            ans.PscInspection = psc;


            //関連ファイルの取得
            ans.AttachmentList = DcAttachment.GetRecrodsByCommentItemID(cone, psc.comment_item_id);

            //ActionCode履歴の取得
            ans.ActionCodeHistoryList = DcActionCodeHistory.GetRecordsByCommentItemID(cone, psc.comment_item_id);

            //親データを取得
            ans.ParentData = CommentData.GetRecordByCommetIDWithoutCommentItem(cone, psc.comment_id);

            return ans;
        }
        //-----------------------------------------------------------------------------------------------------------------


        



        /// <summary>
        /// 全データを取得する
        /// </summary>
        /// <param name="cone"></param>
        /// <returns></returns>
        public static List<PSCInspectionData> GetDataList(NpgsqlConnection cone)
        {

            List<PSCInspectionData> anslist = new List<PSCInspectionData>();


            try
            {
                //対象の取得
                List<DcCiPscInspection> psclist = DcCiPscInspection.GetRecords(cone);

                foreach (DcCiPscInspection psc in psclist)
                {
                    //データ検索
                    PSCInspectionData ans = CollectData(cone, psc);
                    anslist.Add(ans);
                }

            }
            catch (Exception e)
            {
                throw new Exception("PSCInspectionData GetDataList", e);
            }

            return anslist;
        }


        /// <summary>
        /// 対象のPSCを取得
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="comment_item_id"></param>
        /// <returns></returns>
        public static PSCInspectionData GetDataByCommentItemID(NpgsqlConnection cone, int comment_item_id)
        {
            PSCInspectionData ans = null;
            try
            {
                DcCiPscInspection psc = DcCiPscInspection.GetRecordByCommentItemID(cone, comment_item_id);
                ans = CollectData(cone, psc);

            }
            catch (Exception e)
            {
                throw new Exception("PSCInspectionData GetDataByCommentItemID", e);
            }
            return ans;
        }


        /// <summary>
        /// PSCデータの検索
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public static List<PSCInspectionData> GetDataListBySearchData(NpgsqlConnection cone, PscInspectionSearchData sdata)
        {

            List<PSCInspectionData> anslist = new List<PSCInspectionData>();


            try
            {
                //対象の取得
                List<DcCiPscInspection> psclist = DcCiPscInspection.GetRecordsByPSCSearchData(cone, sdata);

                foreach (DcCiPscInspection psc in psclist)
                {
                    //データ検索
                    PSCInspectionData ans = CollectData(cone, psc);
                    anslist.Add(ans);
                }

            }
            catch (Exception e)
            {
                throw new Exception("PSCInspectionData GetDataListBySearchData", e);
            }

            return anslist;
        }


        /// <summary>
        /// データの挿入 上でトランザクションを
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public override int Insert(NpgsqlConnection cone, MsUser requser)
        {
            int pscid = BaseDac.EVal;

            try
            {

                //本体挿入
                pscid = this.PscInspection.InsertRecord(cone, requser);


                //添付があるなら挿入
                if (this.AttachmentList != null)
                {
                    this.InsertAttachment(cone, requser, pscid);
                }

                //アクションコードの挿入
                if (this.ActionCodeHistoryList != null)
                {
                    foreach (DcActionCodeHistory ac in this.ActionCodeHistoryList)
                    {
                        ac.comment_item_id = pscid;
                        ac.InsertRecord(cone, requser);
                    }
                }



            }
            catch (Exception e)
            {
                throw new Exception("PSCInspectionData Insert", e);
            }

            return pscid;
        }


        /// <summary>
        /// 更新 上でトランザクションを
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="requser">更新ユーザー</param>
        /// <returns></returns>
        public override bool Update(NpgsqlConnection cone, MsUser requser)
        {
            try
            {

                //本体挿入
                bool ret = this.PscInspection.UpdateRecord(cone, requser);
                if (ret == false)
                {
                    throw new Exception("PscInspection.UpdateWithAlarm FALSE");
                }


                //添付があるなら削除or挿入
                if (this.AttachmentList != null)
                {
                    this.UpdateAttachment(cone, requser, this.PscInspection.comment_item_id);
                }

                //アクションコードの挿入、更新
                if (this.ActionCodeHistoryList != null)
                {
                    foreach (DcActionCodeHistory ac in this.ActionCodeHistoryList)
                    {
                        //IDがないなら新規
                        if (ac.action_code_history_id == DcActionCodeHistory.EVal)
                        {
                            ac.comment_item_id = this.PscInspection.comment_item_id;
                            ac.InsertRecord(cone, requser);
                        }
                        else
                        {
                            //更新・・・deleteフラグがたっているなら更新で消えます
                            ret = ac.UpdateRecord(cone, requser);
                        }


                    }
                }


            }
            catch (Exception e)
            {
                throw new Exception("PSCInspectionData Update", e);
            }

            return true;
        }


        /// <summary>
        /// データの削除 上でトランザクションを
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public override bool Delete(NpgsqlConnection cone, MsUser requser)
        {
            try
            {
                //本体
                bool ret = this.PscInspection.DeleteRecord(cone, requser);




                //添付ファイル
                foreach (DcAttachment file in this.AttachmentList)
                {
                    ret = file.DeleteRecord(cone, requser);
                }

                if (this.ActionCodeHistoryList != null)
                {
                    //ActionCode履歴
                    foreach (DcActionCodeHistory ac in this.ActionCodeHistoryList)
                    {
                        ret = ac.DeleteRecord(cone, requser);
                    }
                }



            }
            catch (Exception e)
            {
                throw new Exception("PSCInspectionData Delete", e);
            }


            return true;
        }





        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 自分を除いたともに更新する姉妹データのリストを作成する
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="comment_id">親ID</param>
        /// <param name="srcdata">自分　これを除外し、ヘッダー部をコピーしたデータを作成する</param>
        /// <returns></returns>
        private static List<DcCiPscInspection> CreateSisterList(NpgsqlConnection cone, int comment_id, DcCiPscInspection srcdata)
        {
            List<DcCiPscInspection> anslist = new List<DcCiPscInspection>();

            //姉妹データの取得
            List<DcCiPscInspection> sislist = DcCiPscInspection.GetRecordsByCommentID(cone, comment_id);            

            //ヘッダーデータをコピー
            foreach (DcCiPscInspection sis in sislist)
            {
                if (sis.comment_item_id == srcdata.comment_item_id)
                {
                    continue;
                }


                //ヘッダー情報のコピーを行う
                sis.date = srcdata.date;
                sis.item_kind_id = srcdata.item_kind_id;
                sis.ms_vessel_id = srcdata.ms_vessel_id;
                sis.ms_crew_matrix_type_id = srcdata.ms_crew_matrix_type_id;
                sis.ms_basho_id = srcdata.ms_basho_id;
                sis.ms_regional_code = srcdata.ms_regional_code;
                sis.comment_remarks = srcdata.comment_remarks;


                //ADD
                anslist.Add(sis);                
            }

            return anslist;
        }



        /// <summary>
        /// データの更新を姉妹込みで完璧に行う 上でトランザクションせよ
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="parentdata">今回更新する親</param>
        /// <param name="pscdata">今回更新する大本データ</param>
        /// <param name="requser">要求者</param>
        public static void UpdateDataWithSister(NpgsqlConnection cone, CommentData parentdata, PSCInspectionData pscdata, MsUser requser)
        {
            try
            {

                DcCiPscInspection cur = pscdata.PscInspection;

                //更新姉妹リストの作成
                //List<DcCiPscInspection> sislist = DcCiPscInspection.GetRecordsByCommentID(cone, parentdata.Comment.comment_id);
                List<DcCiPscInspection> sislist = CreateSisterList(cone, parentdata.Comment.comment_id, pscdata.PscInspection);

                //姉妹データ更新
                sislist.ForEach(x =>
                {
                    
                    x.UpdateRecord(cone, requser);
                });

                //親の更新
                parentdata.Update(cone, requser);


                //自分の更新
                pscdata.Update(cone, requser);
               

                
            }
            catch (Exception e)
            {
                throw new Exception("PSCInspectionData UpdateDataWithSister", e);
            }
        }





        /// <summary>
        /// 削除処理　安全にデータを削除する
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="comment_item_id">削除対象コメントアイテムID</param>
        /// <param name="requser">要求者</param>
        public static void DeleteSafe(NpgsqlConnection cone, int comment_item_id, MsUser requser)
        {

            try
            {
                //削除対象の取得
                PSCInspectionData deldata = PSCInspectionData.GetDataByCommentItemID(cone, comment_item_id);

                //自分以外の姉妹の取得
                List<DcCiPscInspection> sislist = DcCiPscInspection.GetRecordsByCommentID(cone, deldata.PscInspection.comment_id);
                var n = from f in sislist where f.comment_item_id != deldata.PscInspection.comment_item_id select f;

                //自分の削除
                deldata.Delete(cone, requser);

                //姉妹のデータがある？
                if (n.Count() > 0)
                {
                    return;
                }

                //ここまできたら姉妹データはすべて無い。よって親を削除する
                CommentData pdata = CommentData.GetRecordByCommetIDWithoutCommentItem(cone, deldata.PscInspection.comment_id);
                pdata.Delete(cone, requser);
            }
            catch (Exception e)
            {
                throw new Exception("PSCInspectionData DeleteSafe", e);
            }


        }



        /// <summary>
        /// Completeのか否かの確認
        /// </summary>
        /// <returns></returns>
        public bool CheckComplete()
        {
            if (this.PscInspection.status_id == (int)EStatus.Complete)
            {
                return true;
            }

            return false;
        }
    }
}
