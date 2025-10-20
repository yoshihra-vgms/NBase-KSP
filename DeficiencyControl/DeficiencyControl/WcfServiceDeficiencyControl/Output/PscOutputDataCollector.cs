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
    /// PSC帳票データの収集
    /// </summary>
    public class PscOutputDataCollector : BaseOutputCollector
    {
        public PscOutputDataCollector(NpgsqlConnection wing, NpgsqlConnection defcon)
            : base(wing, defcon)
        {
        }


        /// <summary>
        /// 対象アクションコードの集計結果を作成する
        /// </summary>
        /// <param name="sdata"></param>
        /// <returns></returns>
        private Dictionary<int, List<DcActionCodeHistory>> SearchActionCodeAggregate(PscInspectionSearchData sdata)
        {
            Dictionary<int, List<DcActionCodeHistory>> ansdic = new Dictionary<int, List<DcActionCodeHistory>>();
            
            //期間内対象のActionCode一覧を取得する
            List<DcActionCodeHistory> datalist = DcActionCodeHistory.GetRecordsByPscSearchData(this.DefCone, sdata);

            //全アクションコードとの対応リストを作成する
            List<MsActionCode> codelist = MsActionCode.GetRecords(this.WingCone);
            foreach (MsActionCode code in codelist)
            {
                //対象のコードを抽出
                var n = from f in datalist where f.action_code_id == code.action_code_id select f;
                List<DcActionCodeHistory> aclist = n.ToList();

                //ADD
                ansdic.Add(code.action_code_id, aclist);
            }
            

            return ansdic;

        }


        /// <summary>
        /// DeficiencyCode集計結果の検索
        /// </summary>
        /// <param name="sdata"></param>
        /// <param name="psccout"></param>
        /// <param name="deficiencycount"></param>
        /// <returns></returns>
        private Dictionary<int, PscOutputDeficiencyAggregateData> SearchDeficiencyCodeAggregate(PscInspectionSearchData sdata, out int psccout, out int deficiencycount)
        {
            Dictionary<int, PscOutputDeficiencyAggregateData> ansdic = new Dictionary<int, PscOutputDeficiencyAggregateData>();

            //元データの検索・・・対象のデータを全て取得
            List<DcCiPscInspection> datalist = DcCiPscInspection.GetRecordsByPSCSearchData(this.DefCone, sdata);

            //回数を計算
            {

                //親の数を数える
                //一意な親IDを取得すればよい
                List<int> paridlist = new List<int>();
                {
                    var cn = from f in datalist select f.comment_id;
                    paridlist = cn.Distinct().ToList();
                    
                }

                psccout = paridlist.Count;
                deficiencycount = datalist.Count;


            }

            //DeficiencyCode
            List<MsDeficiencyCode> codelist = MsDeficiencyCode.GetRecords(this.WingCone);
            foreach (MsDeficiencyCode code in codelist)
            {
                //対象コードの抽出
                var n = from f in datalist where f.deficiency_code_id == code.deficiency_code_id select f;
                List<DcCiPscInspection> plist = n.ToList();

                //ないものはADDしない
                if (plist.Count <= 0)
                {
                    continue;
                }

                //データの作成
                PscOutputDeficiencyAggregateData ans = new PscOutputDeficiencyAggregateData();
                ans.Code = code;
                ans.PscLsit = plist;

                //ADD
                ansdic.Add(code.deficiency_code_id, ans);
            }



            return ansdic;
        }

        /// <summary>
        /// 対象年度のデータを取得する
        /// </summary>
        /// <param name="year"></param>
        /// <param name="idata"></param>
        /// <returns></returns>
        private PscOutputAggregateData CollectAggregateYear(int year, PscOutputParameter idata)
        {
            PscOutputAggregateData ans = new PscOutputAggregateData();
            ans.Year = year;
            ans.StartDate = CommonLogic.CalcuYearStart(year);
            ans.EndDate = CommonLogic.CalcuYearEnd(year);            

            //検索条件の作成
            PscInspectionSearchData sdata = new PscInspectionSearchData();
            sdata.ms_vessel_id = idata.ms_vessel_id;
            sdata.date_start = ans.StartDate;
            sdata.date_end = ans.EndDate;
            sdata.StatusPending = true;
            sdata.StatusComplete = true;
            sdata.DeficiencyCountZeroEnabledFlag = true;


            //対象ActionCodeの検索
            {
                ans.ActionCodeDic = this.SearchActionCodeAggregate(sdata);
            }


            //DeficiencyCodeのための検索
            {
                ans.DeficiencyCodeDic = this.SearchDeficiencyCodeAggregate(sdata, out ans.PscCount, out ans.DeficiencyCount);
            }

            return ans;
        }

        /// <summary>
        /// 集計データの取得
        /// </summary>
        /// <param name="idata">条件</param>
        /// <returns></returns>
        private List<PscOutputAggregateData> CollectAggregateData(PscOutputParameter idata)
        {
            List<PscOutputAggregateData> anslist = new List<PscOutputAggregateData>();

            //対象年度のデータを取得する
            for (int year = idata.StartYear; year <= idata.EndYear; year++)
            {
                PscOutputAggregateData ans = this.CollectAggregateYear(year, idata);
                anslist.Add(ans);

            }

            return anslist;
        }

        /// <summary>
        /// 船ごとにまとめたPSCデータの検索
        /// </summary>
        /// <param name="idata"></param>
        /// <returns></returns>
        private Dictionary<decimal, List<PSCInspectionData>> CollectVesselPSC(PscOutputParameter idata)
        {
            Dictionary<decimal, List<PSCInspectionData>> ansdic = new Dictionary<decimal,List<PSCInspectionData>>();

            //検索条件の作成
            //期間内の全部
            PscInspectionSearchData sdata = new PscInspectionSearchData();
            sdata.ms_vessel_id = idata.ms_vessel_id;
            sdata.date_start = CommonLogic.CalcuYearStart(idata.StartYear);
            sdata.date_end = CommonLogic.CalcuYearEnd(idata.EndYear);

            //対象データの検索
            List<PSCInspectionData> datalist = PSCInspectionData.GetDataListBySearchData(this.DefCone, sdata);

            //有効な船ID
            List<decimal> vesidlist = new List<decimal>();
            {
                //取得できな船の一意なIDを取得する(船ごとにまとめるため)
                //var vn = from f in datalist select f.PscInspection.ms_vessel_id;

                //順番を担保するため船マスタ依存とする
                List<MsVessel> veslist = MsVessel.GetRecords(this.WingCone);
                var vn = from f in veslist select f.ms_vessel_id;
                vesidlist = vn.Distinct().ToList();
                
            }

            //全船のデータを取得
            foreach (decimal vesid in vesidlist)
            {
                //対象船のデータを抽出
                var n = from f in datalist where f.PscInspection.ms_vessel_id == vesid select f;
                List<PSCInspectionData> plist = n.ToList();
                if (plist.Count <= 0)
                {
                    //データが無いなら登録しない
                    continue;
                }

                //並べ替え？
                //日付順、同じなら番号順
                plist.Sort((x, y) =>
                {
                    TimeSpan span = y.PscInspection.date.Date - x.PscInspection.date.Date;
                    if (span.Ticks < 0)
                    {
                        return 1;
                    }
                    if (span.Ticks > 0)
                    {
                        return -1;
                    }

                    //同じなら番号で比べる
                    int sa = y.PscInspection.deficinecy_no - x.PscInspection.deficinecy_no;
                    
                    return -sa;
                });

                //ADD
                ansdic.Add(vesid, plist);
            }


            return ansdic;

        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 帳票出力データの収集本体
        /// </summary>
        /// <param name="idata"></param>
        /// <returns></returns>
        public PscOutputData Collect(PscOutputParameter idata)
        {
            PscOutputData ans = new PscOutputData();

            //集計データの取得
            ans.AggregateList = this.CollectAggregateData(idata);

            

            //船ごとのデータの取得
            ans.PSCVesselDic = this.CollectVesselPSC(idata);

            return ans;
        }
    }
}