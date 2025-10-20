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

namespace DeficiencyControl.Files
{
    /// <summary>
    /// スケジュール一覧Excelファイル、有効期限グリッドの書き込み
    /// </summary>
    class ScheduleListExcelFileExpiry : BaseScheduleListExcelFile
    {
        #region Excel変数名定義

        //検船
        public const string CreateVessel = "**CreateVessel";
        public const string InsDetail = "**InsDetail";
        public const string Vessel_InsDetail = "**Vessel_{0}_InsDetail_{1}";

        //SMS ISSC
        public const string SMSDetail = "**SMSDetail";
        public const string Vessel_SMSDetail = "**Vessel_{0}_SMSDetail_{1}";

        //内部監査
        public const string AuditDetail = "**AuditDetail";
        public const string Vessel_AuditDetail = "**Vessel_{0}_AuditDetail_{1}";

        //入渠
        public const string LastDockDate_Vessel = "**LastDockDate_Vessel";
        public const string LastDockDetail_Vessel = "**LastDockDetail_Vessel";

        public const string PrevDockDate_Vessel = "**PrevDockDate_Vessel";
        public const string PrevDockDetai_lVessel = "**PrevDockDetai_lVessel";

        public const string NextDockDate_Vessel = "**NextDockDate_Vessel";
        public const string NextDockDetail_Vessel = "**NextDockDetail_Vessel";



        /// <summary>
        /// 最大数
        /// </summary>
        public const int MaxDetailCount = 6;

        #endregion

        /// <summary>
        /// 書き込む有効期限に適したデータを取得する
        /// </summary>
        /// <param name="basedate">基準日</param>
        /// <param name="ms_vessel_id">対象の船</param>
        /// <param name="detail">取得する種別詳細</param>
        /// <param name="planlist">選択一覧</param>
        /// <returns></returns>
        /// <remarks>基準日後に一番早く来る有効期限のデータを取得する、ない場合はnull</remarks>
        private DcSchedulePlan GetWritePlan(DateTime basedate, decimal ms_vessel_id, MsScheduleKindDetail detail, List<DcSchedulePlan> planlist)
        {
            //対象の船、種別詳細のデータ、なおかつ基準日以降のデータ
            var n = from f in planlist where f.ms_vessel_id == ms_vessel_id && f.schedule_kind_detail_id == detail.schedule_kind_detail_id && f.expiry_date >= basedate select f;
            List<DcSchedulePlan> plist = n.ToList();
            if (plist.Count <= 0)
            {
                return null;
            }

            //有効期限の順番に並べる ・・・基準日以降のデータを抽出済みなのでこれで最新が分かるはず。
            plist.Sort((x, y) =>
            {
                long a = (x.expiry_date.Date - y.expiry_date.Date).Ticks;
                if (a < 0)
                {
                    return -1;

                } if (a > 0)
                {
                    return 1;
                }
                return 0;
            });

            //最初のデータを取得
            DcSchedulePlan ans = plist.First();

            return ans;

        }



        /// <summary>
        /// 対象のデータが有効化をチェックする true=有効 false=無効
        /// </summary>
        /// <param name="ms_vessel_id">対象の船</param>
        /// <param name="detail">チェックする詳細</param>
        /// <param name="wdata">書き込みデータ</param>
        /// <returns></returns>
        private bool CheckKindDetailEnabled(decimal ms_vessel_id, MsScheduleKindDetail detail, ScheduleListWriteData wdata)
        {
            bool ret = false;

            //対象の有効可否取得
            var n = from f in wdata.KindDetailEnabledList where f.ms_vessel_id == ms_vessel_id && f.schedule_kind_detail_id == detail.schedule_kind_detail_id select f;
            if (n.Count() <= 0)
            {
                return false;   //ないなら無効
            }

            //
            MsVesselScheduleKindDetailEnable enadata = n.First();
            ret = enadata.enabled;

            return ret;
        }


        /// <summary>
        /// グリッド書き込み
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="wdata"></param>
        /// <param name="headtag"></param>
        /// <param name="datatag"></param>
        /// <param name="kind"></param>
        private void WriteGrid(XlsxCreator crea, ScheduleListWriteData wdata, string headtag, string datatag, EScheduleKind kind)
        {
            DBDataCache db = DcGlobal.Global.DBCache;

            //詳細を取得する
            List<MsScheduleKindDetail> detaillist = db.GetMsScheduleKindDetailList(kind);

            string tag = "";
            int no = 1;
            foreach (MsScheduleKindDetail detail in detaillist)
            {

                //ヘッダー書き込み
                tag = this.CreateTemplateNo(headtag, no);
                crea.Cell(tag).Value = detail.ToString();


                //全船の書き込み
                foreach (MsVessel ves in wdata.AllVesselList)
                {
                    int vwpos = wdata.GetVesselWritePos(ves.ms_vessel_id);


                    //有効可否書き込みデータ取得
                    {
                        tag = string.Format(datatag, vwpos, no);
                        DcSchedulePlan pdata = this.GetWritePlan(wdata.Date, ves.ms_vessel_id, detail, wdata.ExpiryPlanList);
                        if (pdata != null)
                        {
                            crea.Cell(tag).Value = DcGlobal.DateTimeToString(pdata.expiry_date);
                        }

                        //無効なデータの場合を確認する
                        bool ena = this.CheckKindDetailEnabled(ves.ms_vessel_id, detail, wdata);
                        if (ena == false)
                        {
                            crea.Cell(tag).Attr.LineRightUp(AdvanceSoftware.ExcelCreator.BorderStyle.Thin, AdvanceSoftware.ExcelCreator.xlColor.Auto);
                        }
                    }

                }

                no++;
            }


            //残りを消す
            {
                string starttag = this.CreateTemplateNo(headtag, no);
                string endtag = this.CreateTemplateNo(headtag, MaxDetailCount);
                this.DeleteRows(crea, starttag, endtag);

            }
        }

        /// <summary>
        /// 検船の書き込み
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="wdata"></param>
        private void WriteInspection(XlsxCreator crea, ScheduleListWriteData wdata)
        {
            DBDataCache db = DcGlobal.Global.DBCache;
            

            string tag = "";

            //竣工日の書き込み
            foreach (MsVessel ves in wdata.AllVesselList)
            {
                int vwpos = wdata.GetVesselWritePos(ves.ms_vessel_id);

                //竣工年月日の書き込み・・・ここは何回も書き込まれてしまうが別に問題ないと思われる
                tag = this.CreateTemplateNo(CreateVessel, vwpos);
                crea.Cell(tag).Value = DcGlobal.DateTimeToString(ves.completion_date);
            }

            //Gridの書き込み
            this.WriteGrid(crea, wdata, InsDetail, Vessel_InsDetail, EScheduleKind.検船);

            //検船の詳細を取得する
            //List<MsScheduleKindDetail> detaillist = db.GetMsScheduleKindDetailList(EScheduleKind.検船);
            /*
            int no = 1;
            foreach (MsScheduleKindDetail detail in detaillist)
            {

                //ヘッダー書き込み
                tag = this.CreateTemplateNo(InsDetail, no);
                crea.Cell(tag).Value = detail.ToString();


                //全船の書き込み
                foreach (MsVessel ves in wdata.AllVesselList)
                {
                    int vwpos = wdata.GetVesselWritePos(ves.ms_vessel_id);

 
                    //有効可否書き込みデータ取得
                    {
                        tag = string.Format(Vessel_InsDetail, vwpos, no);
                        DcSchedulePlan pdata = this.GetWritePlan(wdata.Date, ves.ms_vessel_id, detail, wdata.PlanList);
                        if (pdata != null)
                        {
                            crea.Cell(tag).Value = DcGlobal.DateTimeToString(pdata.expiry_date);
                        }

                        //無効なデータの場合を確認する
                        bool ena = this.CheckKindDetailEnabled(ves.ms_vessel_id, detail, wdata);
                        if (ena == false)
                        {
                            crea.Cell(tag).Attr.LineRightUp(AdvanceSoftware.ExcelCreator.BorderStyle.Thin, AdvanceSoftware.ExcelCreator.xlColor.Auto);
                        }
                    }

                }

                no++;
            }


            //残りを消す
            {
                string starttag = this.CreateTemplateNo(InsDetail, no);
                string endtag = this.CreateTemplateNo(InsDetail, MaxDetailCount);
                this.DeleteRows(crea, starttag, endtag);

            }*/
        }



        /// <summary>
        /// SMS ISSC の書き込み
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="wdata"></param>
        private void WriteSMSISSC(XlsxCreator crea, ScheduleListWriteData wdata)
        {
            DBDataCache db = DcGlobal.Global.DBCache;

            //Gridの書き込み
            this.WriteGrid(crea, wdata, SMSDetail, Vessel_SMSDetail, EScheduleKind.SMS_ISSC);
          
        }

        /// <summary>
        /// 内部監査の書き込み
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="wdata"></param>
        private void WriteAudit(XlsxCreator crea, ScheduleListWriteData wdata)
        {
            DBDataCache db = DcGlobal.Global.DBCache;

            //Gridの書き込み
            this.WriteGrid(crea, wdata, AuditDetail, Vessel_AuditDetail, EScheduleKind.内部監査);

        }


        /// <summary>
        /// 対象の船の次の入渠を取得する nullがなし
        /// </summary>
        /// <param name="basedate">基準日　これ以降の近いデータ</param>
        /// <param name="ms_vessel_id">対象の船</param>
        /// <param name="planlist">対象物</param>
        /// <returns></returns>
        private DcSchedulePlan GetDockNext(DateTime basedate, decimal ms_vessel_id, List<DcSchedulePlan> planlist)
        {
            DcSchedulePlan ans = null;

            //対象の基準日より後の予定日
            var n = from f in planlist where f.ms_vessel_id == ms_vessel_id && f.schedule_kind_id == (int)EScheduleKind.入渠 && f.estimate_date > basedate select f;
            if (n.Count() <= 0)
            {
                return null;
            }

            

            //並べ替え
            n = n.OrderBy(x => x.estimate_date);            

            //最新を取得
            ans = n.First();
            
            return ans;
        }

        /// <summary>
        /// 過去実績の取得
        /// </summary>
        /// <param name="basedate">基準日　これより前の実績日</param>
        /// <param name="ms_vessel_id"></param>
        /// <param name="planlist"></param>
        /// <returns></returns>
        private List<DcSchedulePlan> GetDockPrev(DateTime basedate, decimal ms_vessel_id, List<DcSchedulePlan> planlist)
        {
            List<DcSchedulePlan> anslist = new List<DcSchedulePlan>();

            //対象の基準日より前の実績を取得　ただしEDateは除く
            var n = from f in planlist where f.ms_vessel_id == ms_vessel_id && f.schedule_kind_id == (int)EScheduleKind.入渠 && f.inspection_date <= basedate && f.inspection_date != BaseDac.EDate select f;
            if (n.Count() <= 0)
            {
                return anslist;
            }

            //並べ替え
            n = n.OrderByDescending(x => x.inspection_date);

            //最新を取得
            anslist = n.ToList();

            return anslist;
        }


        /// <summary>
        /// 入渠の書き込み
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="wdata"></param>
        private void WriteDock(XlsxCreator crea, ScheduleListWriteData wdata)
        {
            DBDataCache db = DcGlobal.Global.DBCache;

            string tag = "";
            
            //船ごとの入渠データ一覧を取得する
            foreach (MsVessel ves in wdata.AllVesselList)
            {

                //過去実績を取得
                List<DcSchedulePlan> prevlist = this.GetDockPrev(wdata.Date, ves.ms_vessel_id, wdata.ExpiryPlanList);

                //対象船の書き込み位置を取得
                int vwpos =wdata.GetVesselWritePos(ves.ms_vessel_id);

                //前々回・・・実績日
                if (prevlist.Count >= 2)
                {
                    DcSchedulePlan plp = prevlist[1];
                    //日付
                    tag = this.CreateTemplateNo(LastDockDate_Vessel, vwpos);                    
                    crea.Cell(tag).Value = DcGlobal.DateTimeToString(plp.inspection_date);

                    //詳細
                    tag = this.CreateTemplateNo(LastDockDetail_Vessel, vwpos);
                    MsScheduleKindDetail de = db.GetMsScheduleKindDetail(plp.schedule_kind_detail_id);
                    if (de != null)
                    {
                        crea.Cell(tag).Value = de.ToString();
                    }
                }

                //前回・・・実績日
                if (prevlist.Count >= 1)
                {
                    DcSchedulePlan plp = prevlist[0];
                    //日付
                    tag = this.CreateTemplateNo(PrevDockDate_Vessel, vwpos);
                    crea.Cell(tag).Value = DcGlobal.DateTimeToString(plp.inspection_date);

                    //詳細
                    tag = this.CreateTemplateNo(PrevDockDetai_lVessel, vwpos);
                    MsScheduleKindDetail de = db.GetMsScheduleKindDetail(plp.schedule_kind_detail_id);
                    if (de != null)
                    {
                        crea.Cell(tag).Value = de.ToString();
                    }

                }


                //次回・・・予定日
                DcSchedulePlan next = this.GetDockNext(wdata.Date, ves.ms_vessel_id, wdata.ExpiryPlanList);
                if (next != null)
                {
                    //日付
                    tag = this.CreateTemplateNo(NextDockDate_Vessel, vwpos);
                    crea.Cell(tag).Value = DcGlobal.DateTimeToString(next.estimate_date);

                    //詳細
                    tag = this.CreateTemplateNo(NextDockDetail_Vessel, vwpos);
                    MsScheduleKindDetail de = db.GetMsScheduleKindDetail(next.schedule_kind_detail_id);                    
                    if (de != null)
                    {
                        crea.Cell(tag).Value = de.ToString();
                    }
                }


            }

        }

        /// <summary>
        /// 有効期限表の書き込み
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="wdata">書き込みデータ</param>
        public void WriteExpiryGrid(XlsxCreator crea, ScheduleListWriteData wdata)
        {
            //検船
            this.WriteInspection(crea, wdata);

            //SMS・ISSC
            this.WriteSMSISSC(crea, wdata);

            //内部監査
            this.WriteAudit(crea, wdata);


            //入渠予定
            this.WriteDock(crea, wdata);
        }
    }
}
