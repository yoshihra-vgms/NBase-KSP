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
    /// 検船帳票 章別指摘数データ取得
    /// </summary>
    public class MoiExcelCategoryCollector : BaseOutputCollector
    {
        public MoiExcelCategoryCollector(NpgsqlConnection wing, NpgsqlConnection defcon)
            : base(wing, defcon)
        {
        }


        
        /// <summary>
        /// VIQCode名ごとのデータを作成する
        /// </summary>
        /// <param name="datalist"></param>
        /// <returns></returns>
        private Dictionary<int, List<MoiData>> CreateData(List<MoiData> datalist)
        {
            //元コード取得
            List<MsViqCode> codelist = MsViqCode.GetRecords(this.WingCone);

            Dictionary<int, List<MoiData>> ansdic = new Dictionary<int, List<MoiData>>();


            foreach (MsViqCode code in codelist)
            {
                //対象のコード取得
                var n = from f in datalist where f.Observation.Observation.viq_code_id == code.viq_code_id select f;



                //含まれていないならADD
                bool cret = ansdic.ContainsKey(code.viq_code_name_id);
                if (cret == false)
                {
                    ansdic.Add(code.viq_code_name_id, new List<MoiData>());
                }

                //ADD
                List<MoiData> moilist = ansdic[code.viq_code_name_id];
                moilist.AddRange(n);                

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
        public MoiExcelOutputDataCategory Collect(MoiExcelOutputParameter idata)
        {
            //検索条件の作成
            MoiSearchData sdata = new MoiSearchData();
            sdata.date_start = CommonLogic.CalcuYearStart(idata.StartYear);
            sdata.date_end = CommonLogic.CalcuYearEnd(idata.EndYear);


            //対象期間の全データ取得
            List<MoiData> datalist = MoiData.GetDataListBySearchData(this.DefCone, sdata);


            //データ作成
            MoiExcelOutputDataCategory ans = new MoiExcelOutputDataCategory();
            ans.StartYear = idata.StartYear;
            ans.EndYear = idata.EndYear;

            //総指摘数
            ans.MoiCount = datalist.Count;

            //船数            
            //全ﾃﾞｰﾀの船IDを一意に取得
            var vesn = from f in datalist select f.Header.Moi.ms_vessel_id;
            List<decimal> vesidlist = vesn.Distinct().ToList();
            ans.VesselCount = vesidlist.Count;

            //データの作成
            ans.DataDic = this.CreateData(datalist);
            

            return ans;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 帳票出力データの収集本体
        /// </summary>
        /// <param name="idata"></param>
        /// <returns></returns>
        public MoiExcelOutputDataCategory Collect2(MoiExcelOutputParameter idata, MsViqVersion version)
        {
            bool deleteSheet = false;

            //検索条件の作成
            MoiSearchData sdata = new MoiSearchData();
            sdata.date_start = CommonLogic.CalcuYearStart(idata.StartYear);
            sdata.date_end = CommonLogic.CalcuYearEnd(idata.EndYear);

            // 今回検索対象のVIQ Verionの期間に、開始年度と終了年度が入っているかチェックする。
            if (CommonLogic.IsRangeDateTime((DateTime)sdata.date_start, version.start_date, version.end_date) ||
                CommonLogic.IsRangeDateTime((DateTime)sdata.date_end, version.start_date, version.end_date))
            {
                // 検索期間をVersion範囲に変更する。
                if (sdata.date_start < version.start_date)
                {
                    sdata.date_start = version.start_date;
                }
                if (sdata.date_end > version.end_date)
                {
                    sdata.date_end = version.end_date;
                }
            }
            else
            {
                // 範囲外の場合は、件数ゼロのデータを作りたいため、ありえない検索条件に変更する。
                sdata.date_start = DateTime.MinValue;
                sdata.date_end = DateTime.MinValue;
                deleteSheet = true; // このシートは削除させる。
            }

            //対象期間の全データ取得
            List<MoiData> datalist = MoiData.GetDataListBySearchData(this.DefCone, sdata);

            //データ作成
            MoiExcelOutputDataCategory ans = new MoiExcelOutputDataCategory();
            ans.StartYear = idata.StartYear;
            ans.EndYear = idata.EndYear;

            //総指摘数
            ans.MoiCount = datalist.Count;

            //シート削除フラグ
            ans.DeleteSheet = deleteSheet;

            //船数            
            //全ﾃﾞｰﾀの船IDを一意に取得
            var vesn = from f in datalist select f.Header.Moi.ms_vessel_id;
            List<decimal> vesidlist = vesn.Distinct().ToList();
            ans.VesselCount = vesidlist.Count;

            //データの作成
            ans.DataDic = this.CreateData(datalist);


            return ans;
        }
    }
}