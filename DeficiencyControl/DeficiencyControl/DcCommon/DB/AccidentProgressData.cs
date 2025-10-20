using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;

using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;
using CIsl.DB.WingDAC;

namespace DcCommon.DB
{

    /// <summary>
    /// Accident進捗データまとめ
    /// </summary>
    public class AccidentProgressData
    {
        public AccidentProgressData()
        {
        }


        /// <summary>
        /// 事故トラブル進捗
        /// </summary>
        public DcAccidentProgress Progress = null;

        /// <summary>
        /// 添付ファイル
        /// </summary>
        public List<DcAttachment> AttachmentList = new List<DcAttachment>();

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// 親に関連するものを一覧で取得する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="accident_id"></param>
        /// <returns></returns>
        public static List<AccidentProgressData> GetDataListByAccidentID(NpgsqlConnection cone, int accident_id)
        {
            List<AccidentProgressData> anslist = new List<AccidentProgressData>();


            try
            {
                //進捗を取得
                List<DcAccidentProgress> proglist = DcAccidentProgress.GetRecordsByAccidentID(cone, accident_id);

                foreach (DcAccidentProgress prog in proglist)
                {
                    AccidentProgressData ans = new AccidentProgressData();
                    ans.Progress = prog;

                    //Accident進捗に関連するファイルを取得
                    ans.AttachmentList = DcAttachment.GetRecrodsByAccidentProgressID(cone, prog.accident_progress_id);

                    anslist.Add(ans);
                }
   
            }
            catch (Exception e)
            {
                throw new Exception("AccidentProgressData GetDataListByAccidentID", e);
            }
            
            

            return anslist;
        }



        /// <summary>
        /// データの挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <param name="accident_id">親ID</param>
        /// <returns></returns>
        public int InsertData(NpgsqlConnection cone, MsUser requser, int accident_id)
        {
            int ansid = DcAccidentProgress.EVal;
                        
            try
            {
                //本体挿入
                this.Progress.accident_id = accident_id;
                ansid = this.Progress.InsertRecord(cone, requser);

                //ファイル挿入
                foreach (DcAttachment att in this.AttachmentList)
                {
                    att.InsertRecordAccidentProgress(cone, requser, ansid);
                }
                

            }
            catch (Exception e)
            {
                throw new Exception("AccidentProgressData InsertData", e);
            }


            return ansid;
        }


        /// <summary>
        /// データの更新
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <param name="accident_id">親Id</param>
        /// <returns></returns>
        public bool UpdateData(NpgsqlConnection cone, MsUser requser, int accident_id)
        {
            bool ret = true;

            try
            {
                //本体
                this.Progress.accident_id = accident_id;
                ret = this.Progress.UpdateRecord(cone, requser);

                //ファイル挿入、削除
                foreach (DcAttachment att in this.AttachmentList)
                {
                    //DeleteFlagで削除する もしくは親の削除
                    if (att.delete_flag == true || this.Progress.delete_flag == true)
                    {
                        att.DeleteRecord(cone, requser);                        
                    }
                    else
                    {
                        att.InsertRecordAccidentProgress(cone, requser, this.Progress.accident_progress_id);
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception("AccidentProgressData UpdateData", e);
            }


            return ret;
        }


        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool DeleteData(NpgsqlConnection cone, MsUser requser)
        {
            bool ret = true;

            try
            {
                //本体削除
                ret = this.Progress.DeleteRecord(cone, requser);

                //ファイル削除
                foreach (DcAttachment att in this.AttachmentList)
                {

                    att.DeleteRecord(cone, requser);

                }

            }
            catch (Exception e)
            {
                throw new Exception("AccidentProgressData DeleteData", e);
            }


            return ret;
        }
    }
}
