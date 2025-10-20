using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using DcCommon.Files;
using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;
using CIsl.DB.WingDAC;
using DcCommon.Output;
using DcCommon;
using Npgsql;

namespace WcfServiceDeficiencyControl.Output
{
    /// <summary>
    /// 事故トラブル帳票データ収集
    /// </summary>
    public class AccidentOutputDataCollector : BaseOutputCollector
    {
        public AccidentOutputDataCollector(NpgsqlConnection wing, NpgsqlConnection defcon)
            : base(wing, defcon)
        {
        }
        



        //--------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 今回のパラメータに該当する船を一覧で取得する
        /// </summary>
        /// <param name="idata">データ</param>
        /// <returns></returns>
        private List<MsVessel> CreateKindVesselList(AccidentOutputParameter idata)
        {
            List<MsVessel> anslist = new List<MsVessel>();

            

            switch (idata.OutputKind)
            {
                    //船ごとに場合
                case EAccidentOutputKind.船毎:
                    {
                        //対象船を取得
                        List<MsVessel> veslist = MsVessel.GetRecords(this.WingCone);

                        //対象の船だけ
                        if (idata.ms_vessel_id != null)
                        {
                            var n = from f in veslist where f.ms_vessel_id == idata.ms_vessel_id select f;
                            if (n.Count() <= 0)
                            {
                                throw new Exception("Vessel Not Match");
                            }
                            MsVessel v = n.First();
                            anslist.Add(v);
                        }
                        else
                        {
                            //全船が対象
                            anslist.AddRange(veslist);
                        }
                    }
                    break;

                case EAccidentOutputKind.IGT船:
                    {
                        string igtid = WebConfigManager.VesselKindID_IGT;
                        anslist = MsVessel.GetRecordsByMsVesselKindID(this.WingCone, igtid);

                    }
                    break;

                case EAccidentOutputKind.外航船:
                    {
                        string ogsid = WebConfigManager.VesselKindID_OGS;
                        anslist = MsVessel.GetRecordsByMsVesselKindID(this.WingCone, ogsid);
                    }
                    break;

                case EAccidentOutputKind.傭船:
                    {
                        string chid = WebConfigManager.VesselKindID_Chartering;
                        anslist = MsVessel.GetRecordsByMsVesselKindID(this.WingCone, chid);
                    }
                    break;
            }


            return anslist;
        }


        /// <summary>
        /// 対象日付範囲の対象船データを取得する
        /// </summary>
        /// <param name="sdate">開始日</param>
        /// <param name="edate">終了日</param>
        /// <param name="vidlist">対象の船IDリスト</param>
        /// <returns></returns>
        /// <remarks>とりあえず日付で括った全データを取得し、船ごとの判定はロジック上で行う。</remarks>
        private List<DcAccident> GetDateAccidentData(DateTime sdate, DateTime edate, List<decimal> vidlist)
        {
            List<DcAccident> anslist = new List<DcAccident>();

            //対象データ検索
            AccidentSearchData sdata = new AccidentSearchData();
            //日付で括る・・・船は複数ある可能性があるため、プログラムで抽出する。その他も同上
            sdata.date_start = sdate;
            sdata.date_end = edate;

            //データの検索
            List<DcAccident> aclist = DcAccident.GetRecordsBySearchData(this.DefCone, sdata);

            //対象船の抜出
            var n = from f in aclist where vidlist.Contains(f.ms_vessel_id) == true select f;
            anslist = n.ToList();

            return anslist;
        }


        /// <summary>
        /// 対象年度のデータ収集
        /// </summary>
        /// <param name="year">対象年度</param>
        /// <param name="vidlist">対象船ID一覧</param>
        /// <param name="idata">パラメータ</param>
        /// <returns></returns>
        private AccidentOutputData CollectSelectYearData(int year,  List<decimal> vidlist, AccidentOutputParameter idata)
        {
            AccidentOutputData ans = new AccidentOutputData();
            //対象年度設定
            ans.Year = year;

            //開始日付と終了日付の割り出し
            ans.StartDate = CommonLogic.CalcuYearStart(year);
            ans.EndDate = CommonLogic.CalcuYearEnd(year);


            //対象のデータを取得
            List<DcAccident> aclist = this.GetDateAccidentData(ans.StartDate, ans.EndDate, vidlist);

            #region KindOfAccident
            {
                ans.KindDic = new Dictionary<int, List<DcAccident>>();

                //全データを作成
                List<MsKindOfAccident> kindlist = MsKindOfAccident.GetRecords(this.WingCone);
                foreach (MsKindOfAccident kind in kindlist)
                {
                    //対象のデータ一覧を取得
                    var n = from f in aclist where f.kind_of_accident_id == kind.kind_of_accident_id select f;

                    //ADD
                    ans.KindDic.Add(kind.kind_of_accident_id, n.ToList());
                }
            }
            #endregion

            #region Situation
            {
                ans.SituationDic = new Dictionary<int, List<DcAccident>>();

                //対象の取得
                List<MsAccidentSituation> situlist = MsAccidentSituation.GetRecords(this.WingCone);
                foreach (MsAccidentSituation situ in situlist)
                {
                    //一致する状況データの取得
                    var n = from f in aclist where f.accident_situation_id == situ.accident_situation_id select f;

                    ans.SituationDic.Add(situ.accident_situation_id, n.ToList());
                }
            }
            #endregion



            return ans;
        }


        /// <summary>
        /// 帳票データの収集
        /// </summary>
        /// <param name="idata"></param>
        /// <returns></returns>
        public List<AccidentOutputData> Collect(AccidentOutputParameter idata)
        {            
            

            //計算対象となる船を割り出す
            List<MsVessel> veslist = this.CreateKindVesselList(idata);
            var vnid = from f in veslist select f.ms_vessel_id;
            List<decimal> vesidlist = vnid.ToList();

            
            List<AccidentOutputData> anslist = new List<AccidentOutputData>();

            //対象年度のデータを取得する
            for (int year = idata.StartYear; year <= idata.EndYear; year++)
            {
                //対象年度のデータを取得
                AccidentOutputData ans = this.CollectSelectYearData(year, vesidlist, idata);
                anslist.Add(ans);
            }

            return anslist;
        }

        
    }
}