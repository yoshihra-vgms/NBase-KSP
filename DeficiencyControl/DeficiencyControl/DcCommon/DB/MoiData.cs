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
    /// 検船のデータ 指摘事項と一対一
    /// </summary>
    /// <remarks>指摘事項と一対一、親子関係は保持しない</remarks>
    public class MoiData
    {
        /// <summary>
        /// 検船データ一式
        /// </summary>
        public MoiHeaderData Header = null;


        /// <summary>
        /// 指摘事項データ一式
        /// </summary>
        public MoiObservationData Observation = null;


        //-----------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 検索条件での検索
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public static List<MoiData> GetDataListBySearchData(NpgsqlConnection cone, MoiSearchData sdata)
        {
            List<MoiData> anslist = new List<MoiData>();


            try
            {
                //検索
                List<MoiObservationData> obslist = MoiObservationData.GetDataListBySearchData(cone, sdata);


                //高速化のためheaderをキャッシュ化する。
                //<moi_id, MoiHeaderData>
                Dictionary<int, MoiHeaderData> hdatadic = new Dictionary<int, MoiHeaderData>();

                //親の取得
                foreach (MoiObservationData obs in obslist)
                {
                    MoiData ans = new MoiData();
                    ans.Observation = obs;

                    //自分の親を取得
                    int moiid = obs.Observation.moi_id;
                    bool ret = hdatadic.ContainsKey(moiid);
                    if (ret == false)
                    {
                        //無いなら取得してキャッシュ化
                        MoiHeaderData hdata = MoiHeaderData.GetDataByMoiID(cone, moiid);
                        hdatadic.Add(moiid, hdata);
                    }

                    //取得
                    ans.Header = hdatadic[moiid];
                    anslist.Add(ans);

                }


                //最後に更新順に並べ替え
                anslist.Sort((x, y) =>
                {
                    //TimeSpan sp = y.Observation.Observation.update_date - x.Observation.Observation.update_date;                    
                    TimeSpan sp = y.Header.Moi.update_date - x.Header.Moi.update_date;                    
                    if (sp.Ticks > 0)
                    {
                        return 1;
                    }
                    if (sp.Ticks < 0)
                    {
                        return -1;
                    }
                    return 0;
                }
                );

            }
            catch (Exception e)
            {
                throw new Exception("MoiData GetDataListBySearchData", e);
            }



            return anslist;
        }


        /// <summary>
        /// 対象のデータを取得する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="moi_observation_id"></param>
        /// <returns></returns>
        public static MoiData GetDataByMoiObservationID(NpgsqlConnection cone, int moi_observation_id)
        {
            MoiData ans = new MoiData();


            try
            {
                //取得
                ans.Observation = MoiObservationData.GetDataByMoiObservationID(cone, moi_observation_id);

                //親の取得
                ans.Header = MoiHeaderData.GetDataByMoiID(cone, ans.Observation.Observation.moi_id);

                


            }
            catch (Exception e)
            {
                throw new Exception("MoiData GetDataByMoiObservationID", e);
            }



            return ans;
        }


        /// <summary>
        /// データの挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>        
        /// <returns></returns>
        public int InsertData(NpgsqlConnection cone, MsUser requser)
        {
            int ansid = DcMoi.EVal;

            try
            {
                //ヘッダー
                ansid = this.Header.InsertData(cone, requser);

                //子供
                this.Observation.InsertData(cone, requser, ansid);


            }
            catch (Exception e)
            {
                throw new Exception("MoiData InsertData", e);
            }


            return ansid;
        }


        /// <summary>
        /// 検船の更新
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>        
        /// <returns></returns>
        public bool UpdateData(NpgsqlConnection cone, MsUser requser)
        {
            bool ret = true;

            try
            {
                //ヘッダー
                ret = this.Header.UpdateData(cone, requser);

                //子供
                ret = this.Observation.UpdateData(cone, requser, this.Header.Moi.moi_id);

            }
            catch (Exception e)
            {
                throw new Exception("MoiData UpdateData", e);
            }


            return ret;
        }


        /// <summary>
        /// MoiDataの削除
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool DeleteData(NpgsqlConnection cone, MsUser requser)
        {
            bool ret = true;

            try
            {
                //姉妹を取得する
                List<MoiObservationData> restlist = MoiObservationData.GetDataListByMoiID(cone, this.Header.Moi.moi_id);


                //ヘッダー
                if (restlist.Count <= 1)
                {
                    //自分の姉妹が全て消えているなら削除 1=自分なので削除で問題なし！
                    this.Header.DeleteData(cone, requser);
                }


                //指摘事項の削除
                this.Observation.DeleteData(cone, requser);

            }
            catch (Exception e)
            {
                throw new Exception("MoiData DeleteData", e);
            }


            return ret;
        }

    }
}
