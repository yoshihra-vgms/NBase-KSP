using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DcCommon.Output;
using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;
using AdvanceSoftware.ExcelCreator.Xlsx;
using CIsl.DB;
using CIsl.DB.WingDAC;
using System.IO;
using DeficiencyControl.Util;
using DeficiencyControl.Schedule;
using DeficiencyControl.Schedule.ListGrid;
using DcCommon;
using System.Drawing;

namespace DeficiencyControl.Files
{
    /// <summary>
    /// スケジュール一覧Excelファイル、一覧グリッドの書き込み
    /// </summary>
    class ScheduleListExcelFileGrid : BaseScheduleListExcelFile
    {
        #region Excel変数名定義


        public const string CreateVessel_Age = "**CreateVessel_Age";

        public const string Vessel_Category = "**Vessel_Category";

        public const string VesselAgeAve = "**VesselAgeAve";


        public const string MonthS = "**MonthS";
        public const string MonthE = "**MonthE";
        public const string VesselMonthData = "**Vessel_{0}_Month_{1}";
        public const string VesselDateData = "**Vessel_{0}_Date_{1}";

        public const string Other_Month = "**Other_Month";
        public const string Other_Date = "**Other_Date";

        /// <summary>
        /// 最大月数
        /// </summary>
        public const int MaxMonth = 81;

        #endregion

        /// <summary>
        /// ヘッダーの書き込み
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="wdata"></param>
        private void WriteHeader(XlsxCreator crea, ScheduleListWriteData wdata)
        {
            string tag = "";

            DateTime basedate = wdata.Date;
            DBDataCache db = DcGlobal.Global.DBCache;

            //全船のヘッダー
            foreach (MsVessel ves in wdata.AllVesselList)
            {
                //書き込み位置取得
                int vwpos = wdata.GetVesselWritePos(ves.ms_vessel_id);


                //船名の書き込み
                tag = this.CreateTemplateNo(VesselName, vwpos);
                crea.Cell(tag).Value = ves.ToString();

                //船齢の計算と書き込み
                tag = this.CreateTemplateNo(CreateVessel_Age, vwpos);
                decimal age = CommonLogic.CalcuVesselAge(basedate, ves.completion_date);
                if (age < 0)
                {
                    //エラーなら船齢平均計算に含めない
                    crea.Cell(tag).Value = string.Format("{0}", ves.completion_date.ToString("yyyy/MM/dd"));
                }
                else
                {
                    //追加
                    crea.Cell(tag).Value = string.Format("{0} {1}", ves.completion_date.ToString("yyyy/MM/dd"), age.ToString("F1"));
                }


                //カテゴリ
                tag = this.CreateTemplateNo(Vessel_Category, vwpos);
                MsVesselCategory vca = db.GetMsVesselCategory(ves.ms_vessel_category_id);
                if (vca != null)
                {
                    crea.Cell(tag).Value = vca.ToString();
                }

            }

            //船齢平均の書き込み
            decimal aveage = DcCommon.CommonLogic.CalcuAverageVesselAge(basedate, wdata.AllVesselList);
            tag = VesselAgeAve;
            crea.Cell(tag).Value = aveage.ToString("F1");

        }


        /// <summary>
        /// 対象のPlanを書き込み
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="vwpos">船書き込み位置</param>
        /// <param name="ywpos">Y位置</param>
        /// <param name="plan">書き込みデータ</param>
        private void WritePlanMonthVessel(XlsxCreator crea, int vwpos, int ywpos, DcSchedulePlan plan)
        {
            string tag = "";

            DBDataCache db =  DcGlobal.Global.DBCache;


            //種別
            tag = string.Format(VesselMonthData, vwpos, ywpos);
            crea.Cell(tag).Value = db.GetMsScheduleKindDetail(plan.schedule_kind_detail_id);

            //色
            crea.Cell(tag).Attr.BackColor = plan.DataColor;


            //実施日の作成
            tag = string.Format(VesselDateData, vwpos, ywpos);

            string s = "";
            //日付と実績メモを入れる
            if (plan.inspection_date != DcSchedule.EDate)
            {
                s = plan.inspection_date.ToString("MM/dd");
            }

            //追加
            s += " " + plan.record_memo;

            crea.Cell(tag).Value = s;
            

        }


        /// <summary>
        /// 対象その他データの書き込み
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="ywpos"></param>
        /// <param name="otherdata"></param>
        private void WriteOtherMonth(XlsxCreator crea, int ywpos, DcScheduleOther otherdata)
        {
            string tag = "";

            DBDataCache db = DcGlobal.Global.DBCache;


            //種別
            tag = this.CreateTemplateNo(Other_Month, ywpos);
            crea.Cell(tag).Value = otherdata.event_memo;

            //色
            crea.Cell(tag).Attr.BackColor = otherdata.DataColor;


            //実施日の作成
            tag = this.CreateTemplateNo(Other_Date, ywpos);

            string s = "";
            //日付と実績メモを入れる
            if (otherdata.inspection_date != DcSchedule.EDate)
            {
                s = otherdata.inspection_date.ToString("MM/dd");
            }

            //追加
            s += " " + otherdata.record_memo;

            crea.Cell(tag).Value = s;


        }


        

        /// <summary>
        /// 対象月の書き込み
        /// </summary>
        /// <param name="month">書き込み月</param>
        /// <param name="wdata">データ本体</param>
        /// <param name="vdata">書き込みデータ</param>
        /// <param name="ywpos">書き込み位置(共有)</param>
        private void WriteMonth(XlsxCreator crea, int month, ScheduleListWriteData wdata, MonthVesselData vdata, ref int ywpos)
        {
            //この月で一番の場所を算出する
            int maxdatacount = vdata.CalcuMaxDataCount();

            string tag = "";


            //月
            {
                string starttag = this.CreateTemplateNo(MonthS, ywpos);
                string endtag = this.CreateTemplateNo(MonthE, ywpos + maxdatacount - 1);

                Cell aa = crea.Cell(starttag);

                aa.Attr.MergeCells = true;


                var stn = from f in crea.GetVarNames where f.VarName == starttag select f.CellString;
                var edn = from f in crea.GetVarNames where f.VarName == endtag select f.CellString;

                if (stn.Count() > 0 && edn.Count() > 0)
                {
                    string s = stn.First();
                    string e = edn.First();

                    crea.Cell(s + ":" + e).Attr.MergeCells = false;
                    crea.Cell(s + ":" + e).Attr.MergeCells = true;
                    crea.Cell(s + ":" + e).Value = string.Format("{0}月", month);
                    
                }

                //crea.Cell(starttag + ":" + endtag).Value = string.Format("{0}月", month);
                //crea.Cell(starttag + ":" + endtag).Attr.MergeCells = true;
                
            }


            //最大データ量で回す
            for (int i = 0; i < maxdatacount; i++)
            {

                //対象船の書き込み
                foreach (MsVessel ves in wdata.AllVesselList)
                {
                    //書き込みデータの取得
                    List<DcSchedulePlan> planlist = vdata.VesselDic[ves.ms_vessel_id];
                    if (planlist.Count <= i)
                    {
                        continue;
                    }
                    DcSchedulePlan plan = planlist[i];


                    //書き込み場所取得し書き込み
                    int vwpos = wdata.GetVesselWritePos(ves.ms_vessel_id);
                    this.WritePlanMonthVessel(crea, vwpos, ywpos, plan);


                }

                //その他の書き込み
                {
                    if (vdata.OtherList.Count > i)
                    {
                        DcScheduleOther oth = vdata.OtherList[i];

                        this.WriteOtherMonth(crea, ywpos, oth);
                    }
                }


                //
                ywpos++;

            }


        }

        /// <summary>
        /// スケジュールの一覧部分を書き込む
        /// </summary>
        /// <param name="crea">書き込み場所</param>
        /// <param name="wdata">書き込みデータまとめ</param>
        public void WriteSchedule(XlsxCreator crea, ScheduleListWriteData wdata)
        {
            //ヘッダーの書き込み
            this.WriteHeader(crea, wdata);

            List<int> monthvec = CommonLogic.CreateMonthOrder();

            int ywpos = 1;
            foreach (int month in monthvec)
            {
                //対象月のデータ書き込み
                MonthVesselData vdata = wdata.MonthDic[month];

                //対象月の書き込み
                this.WriteMonth(crea, month, wdata, vdata, ref ywpos);

            }

            //スケジュールの残りを削除する
            string starttag = string.Format(VesselMonthData, 1, ywpos);
            string endtag = string.Format(VesselDateData, 1, MaxMonth);
            this.DeleteRows(crea, starttag, endtag);



        }
    }
}
