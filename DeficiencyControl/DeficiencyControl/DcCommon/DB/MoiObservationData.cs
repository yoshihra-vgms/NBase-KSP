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
    /// 検船指摘事項データまとめ
    /// </summary>
    /// <remarks>指摘事項一件一件のデータを管理する。つまり親と子の関係は一対一。親子関係を扱いたいならMoiDataのほうを使用すること</remarks>
    public class MoiObservationData
    {

        public MoiObservationData()
        {

        }

        /// <summary>
        /// 検船指摘事項
        /// </summary>
        public DcMoiObservation Observation = null;

        /// <summary>
        /// 検船指摘事項添付ファイル
        /// </summary>
        public List<DcAttachment> AttachmentList = new List<DcAttachment>();

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        

        /// <summary>
        /// 対象のデータの一式を作成する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="obs"></param>
        /// <returns></returns>
        private static MoiObservationData CreateSelectData(NpgsqlConnection cone, DcMoiObservation obs)
        {
            MoiObservationData ans = new MoiObservationData();

            if (obs == null)
            {
                return null;
            }

            ans.Observation = obs;
            ans.AttachmentList = DcAttachment.GetRecrodsByMoiObservationID(cone, obs.moi_observation_id);

            return ans;

        }


        /// <summary>
        /// 対象のデータを取得する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="moi_observation_id"></param>
        /// <returns></returns>
        public static MoiObservationData GetDataByMoiObservationID(NpgsqlConnection cone, int moi_observation_id)
        {
            MoiObservationData ans = new MoiObservationData();


            try
            {
                //対象の取得
                DcMoiObservation ob = DcMoiObservation.GetRecordByMoiObservationID(cone, moi_observation_id);
                ans = MoiObservationData.CreateSelectData(cone, ob);
                
            }
            catch (Exception e)
            {
                throw new Exception("MoiObservationData GetDataByMoiObservationID", e);
            }



            return ans;
        }


        /// <summary>
        /// データの検索
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public static List<MoiObservationData> GetDataListBySearchData(NpgsqlConnection cone, MoiSearchData sdata)
        {
            List<MoiObservationData> anslist = new List<MoiObservationData>();


            try
            {

                //データの検索
                List<DcMoiObservation> oblist = DcMoiObservation.GetRecordsBySearchData(cone, sdata);

                //データの作成
                foreach (DcMoiObservation ob in oblist)
                {
                    MoiObservationData ans = MoiObservationData.CreateSelectData(cone, ob);

                    anslist.Add(ans);
                }


            }
            catch (Exception e)
            {
                throw new Exception("MoiObservationData GetDataListBySearchData", e);
            }



            return anslist;
        }


        /// <summary>
        /// 親に関連するものを一覧で取得する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="moi_id"></param>
        /// <returns></returns>
        public static List<MoiObservationData> GetDataListByMoiID(NpgsqlConnection cone, int moi_id)
        {
            List<MoiObservationData> anslist = new List<MoiObservationData>();


            try
            {
                //取得
                List<DcMoiObservation> oblist = DcMoiObservation.GetRecordsByMoiID(cone, moi_id);

                //データの作成
                foreach (DcMoiObservation ob in oblist)
                {
                    MoiObservationData ans = MoiObservationData.CreateSelectData(cone, ob);

                    anslist.Add(ans);
                }


            }
            catch (Exception e)
            {
                throw new Exception("MoiObservationData GetDataListByMoiID", e);
            }



            return anslist;
        }


        /// <summary>
        /// データの挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <param name="moi_id"></param>
        /// <returns></returns>
        public int InsertData(NpgsqlConnection cone, MsUser requser, int moi_id)
        {
            int ansid = DcMoiObservation.EVal;

            try
            {
                //本体挿入
                this.Observation.moi_id = moi_id;
                ansid = this.Observation.InsertRecord(cone, requser);

                //ファイル挿入
                foreach (DcAttachment att in this.AttachmentList)
                {
                    att.InsertRecordMoiObservation(cone, requser, ansid);
                }


            }
            catch (Exception e)
            {
                throw new Exception("MoiObservationData InsertData", e);
            }


            return ansid;
        }


        /// <summary>
        /// 検船指摘事項の更新
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <param name="moi_id"></param>
        /// <returns></returns>
        public bool UpdateData(NpgsqlConnection cone, MsUser requser, int moi_id)
        {
            bool ret = true;

            try
            {
                //本体
                this.Observation.moi_id = moi_id;
                ret = this.Observation.UpdateRecord(cone, requser);

                //ファイル挿入、削除
                foreach (DcAttachment att in this.AttachmentList)
                {
                    //DeleteFlagで削除する もしくは親の削除
                    if (att.delete_flag == true || this.Observation.delete_flag == true)
                    {
                        att.DeleteRecord(cone, requser);
                    }
                    else
                    {
                        att.InsertRecordMoiObservation(cone, requser, this.Observation.moi_observation_id);
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception("MoiObservationData UpdateData", e);
            }


            return ret;
        }


        /// <summary>
        /// MoiObservationDataの削除
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
                ret = this.Observation.DeleteRecord(cone, requser);

                //ファイル削除
                foreach (DcAttachment att in this.AttachmentList)
                {
                    att.DeleteRecord(cone, requser);
                }

            }
            catch (Exception e)
            {
                throw new Exception("MoiObservationData DeleteData", e);
            }


            return ret;
        }

    }


    
}
