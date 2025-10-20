using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WtmData;
using WtmModelBase;

namespace NBaseData.BLC
{
    public class SimulationProc
    {
        public class DeviationAlarmInfo
        {
            public int VesselId { set; get; }
            public DateTime Date { set; get; }
            public bool Alarm24H { set; get; }
            public bool Alarm1W { set; get; }
            public bool Alarm4W { set; get; }
            public bool AlarmRest { set; get; }
        }


        public class Appointment
        {
            public int MsSiShokumeiID { get; set; }

            public bool Visibled { get; set; }  // 表示するかしないか：予定の場合、表示しない
            public bool CheckTarget { get; set; }  // Deviationチェックの対象か

            /// <summary>
            /// 船員ID 
            /// </summary>
            public int MsSeninID { get; set; }

            /// <summary>
            /// 作業内容ID
            /// </summary>
            public string WorkContentID = null;

            /// <summary>
            /// Deviation区分
            /// </summary>
            public int DeviationKind = 0;

            /// <summary>
            /// 時間
            /// </summary>
            public DateTime WorkDate = DateTime.MinValue;

            public override string ToString()
            {
                return "";
            }

            public Appointment()
            {
                Visibled = true;
                CheckTarget = false;
            }

        }

        class DouseiInfo
        {
            public bool WaitingInfo { set; get; }
            public string BashoId { set; get; }
            public string BashoName { set; get; }

            public DateTime? InTime { set; get; }
            public DateTime? Docking { set; get; }
            public DateTime? OutTime { set; get; }


            public DouseiInfo()
            {
                WaitingInfo = false;
                BashoId = null;
                BashoName = null;
                InTime = null;
                Docking = null;
                OutTime = null;
            }
        }



        public static List<DeviationAlarmInfo> GetDeviationInfos(MsUser loginUser, SeninTableCache seninTableCache, string appName, int vesselId, DateTime date1, DateTime date2, List<DjDousei> douseis)
        {
            List<DeviationAlarmInfo> ret = new List<DeviationAlarmInfo>();

            DateTime TODAY = DateTime.Today;
            DateTime NOW = DateTime.Now;

            List<Appointment> AppointmentList = new List<Appointment>();


            System.Diagnostics.Debug.WriteLine($"[GetDeviationInfos]開始：{DateTime.Now.ToString("HH:mm:ss")}");


            // 実績分
            System.Diagnostics.Debug.WriteLine($"　実績分：{DateTime.Now.ToString("HH:mm:ss")}");

            //var resultsWorks = WTMGetProc.GetWorks(loginUser, appName, vesselId, date1, date2);
            var resultsWorks = WtmAccessor.Instance(false).GetWorks(date1, date2, vesselId: vesselId);
            for (DateTime d = date1; d <= TODAY; d = d.AddDays(1))
            {
                foreach (Work w in resultsWorks)
                {
                    // 船員の作業内訳
                    foreach (WorkContentDetail wd in w.WorkContentDetails)
                    {
                        // 現在時刻までの実績を有効データとする
                        if ((d == TODAY && wd.WorkDate < NOW) || (d < TODAY && DateTimeUtils.ToFrom(wd.WorkDate) == d))
                        {
                            Appointment apo = new Appointment();
                            apo.MsSeninID = int.Parse(w.CrewNo);
                            apo.WorkContentID = wd.WorkContentID;
                            apo.WorkDate = wd.WorkDate;

                            AppointmentList.Add(apo);
                        }
                    }

                    // 船員のDeviation
                    foreach (Deviation dev in w.Deviations)
                    {
                        // 現在時刻までの実績を有効データとする
                        if ((d == TODAY && dev.WorkDate < NOW) || (d < TODAY && DateTimeUtils.ToFrom(dev.WorkDate) == d))
                        {
                            Appointment apo = new Appointment();
                            apo.MsSeninID = int.Parse(w.CrewNo);
                            apo.DeviationKind = dev.Kind;
                            apo.WorkDate = dev.WorkDate;

                            AppointmentList.Add(apo);

                            //System.Diagnostics.Debug.WriteLine($"  Deviation実績：{apo.MsSeninID}:{apo.DeviationKind}{apo.WorkDate.ToShortDateString()}");
                        }
                    }
                }
            }

            // 今日～該当日までを構築
            System.Diagnostics.Debug.WriteLine($"　今日～該当日：{DateTime.Now.ToString("HH:mm:ss")}");
            List<Work> devationCheckWorkList = null;

            devationCheckWorkList = resultsWorks.Where(o => o.StartWork < NOW).ToList(); // 現在時刻までに開始しているもの
            devationCheckWorkList.ForEach(o => { if (o.FinishWork > NOW) o.FinishWork = NOW; });     // 終了が現在時刻以降の場合、現在時刻で置き換える




            System.Diagnostics.Debug.WriteLine($"　　　SiCard.GetRecordsByFilter：{DateTime.Now.ToString("HH:mm:ss")}");
            SiCardFilter filter = new SiCardFilter();
            filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser, MsSiShubetsu.SiShubetsu.乗船));
            filter.Start = DateTimeUtils.ToFrom(TODAY);
            filter.End = DateTimeUtils.ToTo(date2);
            filter.MsVesselIDs.Add(vesselId);
            List<SiCard> allcards = SiCard.GetRecordsByFilter(loginUser, filter);

            if (douseis == null)
            {
                douseis = new List<DjDousei>();
                System.Diagnostics.Debug.WriteLine($"　　　DjDousei.GetRecords：{DateTime.Now.ToString("HH:mm:ss")}");
                for (DateTime d = TODAY; d <= date2; d = d.AddDays(1))
                {
                    douseis.AddRange(DjDousei.GetRecords(loginUser, vesselId, d));
                }
            }


            for (DateTime d = TODAY; d <= date2; d = d.AddDays(1))
            {
                System.Diagnostics.Debug.WriteLine($" ================================================= ");
                System.Diagnostics.Debug.WriteLine($" {d.ToShortDateString()}");

                var cards = allcards.Where(o => DateTimeUtils.ToFrom(o.StartDate) <= d && (o.EndDate == DateTime.MinValue || DateTimeUtils.ToFrom(o.EndDate) >= d));

                // 該当日の動静情報
                List<DjDousei> djDousei = douseis.Where(o => o.MsVesselID == vesselId &&
                                                   (o.PlanNyuko.ToShortDateString() == d.ToShortDateString() ||
                                                    o.PlanShukou.ToShortDateString() == d.ToShortDateString() ||
                                                    o.ResultNyuko.ToShortDateString() == d.ToShortDateString() ||
                                                    o.ResultShukou.ToShortDateString() == d.ToShortDateString() ||
                                                    o.DouseiDate.ToShortDateString() == d.ToShortDateString()
                                                    )).OrderBy(o => o.DouseiDate).ToList();
 
                var dinfos = GetDouseiInfos(loginUser, d, djDousei);

                System.Diagnostics.Debug.WriteLine($"　　　makePatternList：{DateTime.Now.ToString("HH:mm:ss")}");
                var diffList = makePatternList(loginUser, vesselId, d, dinfos);
                diffList = diffList.Where(o => o.WorkDate.AddMinutes(15) >= NOW).ToList();

                // Deviation算出のため、パターンを、作業、作業内訳として設定
                foreach (SiCard card in cards)
                {
                    //System.Diagnostics.Debug.WriteLine($" {card.MsSeninID}:{card.SeninName}:{card.VesselName}");

                    int shokumeiId = card.SiLinkShokumeiCards[0].MsSiShokumeiID;

                    var lastApo = (Appointment)null;
                    if (AppointmentList.Any(o => o.MsSeninID == card.MsSeninID))
                        lastApo = AppointmentList.Where(o => o.MsSeninID == card.MsSeninID).OrderBy(o => o.WorkDate).Last();

                    var patternList = (IEnumerable<Appointment>)null;
                    if (lastApo != null)
                    {
                        patternList = diffList.Where(o => o.MsSiShokumeiID == shokumeiId && o.WorkDate > lastApo.WorkDate);
                    }
                    else
                    {
                        patternList = diffList.Where(o => o.MsSiShokumeiID == shokumeiId);
                    }

                    foreach (Appointment patternApo in patternList)
                    {
                        DateTime wkDate = new DateTime(d.Year, d.Month, d.Day, patternApo.WorkDate.Hour, patternApo.WorkDate.Minute, patternApo.WorkDate.Second);
                        Work wk = new Work();
                        wk.CrewNo = card.MsSeninID.ToString();
                        wk.StartWork = wkDate;
                        wk.FinishWork = wkDate.AddMinutes(15);

                        devationCheckWorkList.Add(wk);



                        Appointment apo = new Appointment();

                        //apo.Visibled = false; // Deviationを算出するためなので、表示しない
                        apo.CheckTarget = true;

                        apo.MsSeninID = card.MsSeninID;
                        apo.WorkContentID = patternApo.WorkContentID;
                        apo.WorkDate = wkDate;

                        AppointmentList.Add(apo);
                    }
                }

            }
            System.Diagnostics.Debug.WriteLine($"　　　deviationProc：{DateTime.Now.ToString("HH:mm:ss")}");
            deviationProc(AppointmentList, vesselId, devationCheckWorkList);

            var dates = AppointmentList.Where(o => o.DeviationKind != 0).Select(o => o.WorkDate.ToShortDateString()).Distinct().OrderBy(o => o);
            foreach(string date in dates)
            {
                var aps = AppointmentList.Where(o => o.DeviationKind != 0 && o.WorkDate.ToShortDateString() == date);

                DeviationAlarmInfo ainfo = new DeviationAlarmInfo();
                ainfo.VesselId = vesselId;
                ainfo.Date = DateTime.Parse(date);
                if (aps.Any(o => o.DeviationKind == 1))
                    ainfo.Alarm24H = true;
                if (aps.Any(o => o.DeviationKind == 2))
                    ainfo.Alarm1W = true;
                if (aps.Any(o => o.DeviationKind == 3))
                    ainfo.Alarm4W = true;
                if (aps.Any(o => o.DeviationKind == 4))
                    ainfo.AlarmRest = true;

                ret.Add(ainfo);
            }

            System.Diagnostics.Debug.WriteLine($"[GetDeviationInfos]終了：{DateTime.Now.ToString("HH:mm:ss")}");

            return ret;
        }

        //private static List<DouseiInfo> GetDouseiInfos(MsUser loginUser, int vesselId, DateTime date)
        private static List<DouseiInfo> GetDouseiInfos(MsUser loginUser, DateTime date, List<DjDousei> djDousei)
        {
            List<DouseiInfo> douseiInfos = new List<DouseiInfo>();

            //// 該当日の動静情報
            //var djDousei = DjDousei.GetRecords(loginUser, vesselId, date);
            //djDousei = djDousei.OrderBy(o => o.DouseiDate).ToList();


            bool isPast = date < DateTime.Today;
            string dateStr = date.ToShortDateString();

            foreach (DjDousei ds in djDousei)
            {
                if (ds.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.積みID || ds.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.揚げID)
                {
                    if (isPast && (ds.ResultNyuko.ToShortDateString() == dateStr || ds.ResultShukou.ToShortDateString() == dateStr))
                    {
                        DouseiInfo info = new DouseiInfo();

                        info.BashoId = ds.ResultMsBashoID;
                        info.BashoName = ds.ResultBashoName;

                        if (ds.ResultNyuko.ToShortDateString() == dateStr)
                            info.InTime = ds.ResultNyuko;

                        if (ds.ResultChakusan.ToShortDateString() == dateStr)
                            info.Docking = ds.ResultChakusan;

                        if (ds.ResultShukou.ToShortDateString() == dateStr)
                            info.OutTime = ds.ResultShukou;

                        douseiInfos.Add(info);

                    }
                    else if ((ds.PlanNyuko.ToShortDateString() == dateStr || ds.PlanShukou.ToShortDateString() == dateStr))
                    {
                        DouseiInfo info = new DouseiInfo();

                        info.BashoId = ds.MsBashoID;
                        info.BashoName = ds.BashoName;

                        if (ds.PlanNyuko.ToShortDateString() == dateStr)
                            info.InTime = ds.PlanNyuko;

                        if (ds.PlanChakusan.ToShortDateString() == dateStr)
                            info.Docking = ds.PlanChakusan;

                        if (ds.PlanShukou.ToShortDateString() == dateStr)
                            info.OutTime = ds.PlanShukou;

                        douseiInfos.Add(info);
                    }


                }
                else if (djDousei.Count == 1 && djDousei[0].MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.待機ID)
                {
                    DouseiInfo info = new DouseiInfo();
                    info.WaitingInfo = true;

                    info.BashoId = ds.MsBashoID;
                    info.BashoName = ds.BashoName;

                    douseiInfos.Add(info);
                }
            }

            return douseiInfos;
        }
        
        private static List<Appointment> makePatternList(MsUser loginUser, int vesselId, DateTime date, List<DouseiInfo> douseiInfos)
        {
            List<Appointment> retList = new List<Appointment>();

                bool ToDayOnTheVoyage = false;
                bool ToDayWaiting = false;
                if (douseiInfos.Count == 0)
                {
                    // 航海中
                    ToDayOnTheVoyage = true;
                }
                else if (douseiInfos.Count == 1 && douseiInfos[0].WaitingInfo)
                {
                    // 待機
                    ToDayWaiting = true;
                }

                // 前日の動静情報
                bool BeforeOnTheVoyage = true;
                var BeforeDousei = DjDousei.GetRecords(loginUser, vesselId, date.AddDays(-1));
                if (BeforeDousei.Count == 1 && BeforeDousei[0].MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.待機ID)
                {
                    // 待機
                    BeforeOnTheVoyage = false;
                }

                // 翌日の動静情報
                bool NextDayOnTheVoyage = true;
                var NextDousei = DjDousei.GetRecords(loginUser, vesselId, date.AddDays(1));
                if (NextDousei.Count == 1 && NextDousei[0].MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.待機ID)
                {
                    // 待機
                    NextDayOnTheVoyage = false;
                }

                //====================================================================
                //
                // パターン作成
                //
                //====================================================================
                var waitingPatternList = (List<WorkPattern>)null;
                var voyagePatternList = (List<WorkPattern>)null;
                var patternList1 = (List<WorkPattern>)null;
                if (ToDayWaiting || BeforeOnTheVoyage == false || NextDayOnTheVoyage == false)
                {
                    // 待機中
                    waitingPatternList = WorkPattern.GetRecords(loginUser, WorkPattern.WorkPatternEventKind.Kind停泊中, vesselId, null);
                }
                else
                {
                    // 航海中
                    voyagePatternList = WorkPattern.GetRecords(loginUser, WorkPattern.WorkPatternEventKind.Kind航海中, vesselId, null);
                }



                // 当日のパターン
                if (ToDayWaiting)
                {
                    // 待機中
                    patternList1 = waitingPatternList;
                }
                else //if (ToDayOnTheVoyage)
                {
                    // 航海中
                    patternList1 = voyagePatternList;
                }
                if (patternList1 != null)
                {
                    foreach (WorkPattern w in patternList1)
                    {
                        Appointment apo = new Appointment();
                        apo.MsSiShokumeiID = w.MsSiShokuemiID;
                        apo.WorkContentID = w.WorkContentID;
                        apo.WorkDate = new DateTime(date.Year, date.Month, date.Day, w.WorkDate.Hour, w.WorkDate.Minute, 0);

                        retList.Add(apo);
                    }
                }


                if (douseiInfos.Count > 0)
                {
                    if (BeforeOnTheVoyage == false && douseiInfos.First().InTime != null)
                    {
                        // 入港までを待機パターンとする
                        var inTime = douseiInfos.First().InTime;

                        foreach (WorkPattern w in waitingPatternList)
                        {
                            var workDate = new DateTime(date.Year, date.Month, date.Day, w.WorkDate.Hour, w.WorkDate.Minute, 0);

                            if (workDate < inTime)
                            {
                                var apo = retList.Where(o => o.MsSiShokumeiID == w.MsSiShokuemiID && o.WorkDate == workDate).FirstOrDefault();
                                if (apo != null)
                                {
                                    apo.WorkContentID = w.WorkContentID;
                                }
                                else
                                {
                                    apo = new Appointment();
                                    apo.MsSiShokumeiID = w.MsSiShokuemiID;
                                    apo.WorkContentID = w.WorkContentID;
                                    apo.WorkDate = new DateTime(date.Year, date.Month, date.Day, w.WorkDate.Hour, w.WorkDate.Minute, 0);

                                    retList.Add(apo);
                                }
                            }
                        }
                    }

                    if (NextDayOnTheVoyage == false && douseiInfos.Last().OutTime != null)
                    {
                        // 出港以降を待機パターンとする
                        var outTime = douseiInfos.Last().OutTime;

                        foreach (WorkPattern w in waitingPatternList)
                        {
                            var workDate = new DateTime(date.Year, date.Month, date.Day, w.WorkDate.Hour, w.WorkDate.Minute, 0);

                            if (workDate > outTime)
                            {
                                var apo = retList.Where(o => o.MsSiShokumeiID == w.MsSiShokuemiID && o.WorkDate == workDate).FirstOrDefault();
                                if (apo != null)
                                {
                                    apo.WorkContentID = w.WorkContentID;
                                }
                                else
                                {
                                    apo = new Appointment();
                                    apo.MsSiShokumeiID = w.MsSiShokuemiID;
                                    apo.WorkContentID = w.WorkContentID;
                                    apo.WorkDate = new DateTime(date.Year, date.Month, date.Day, w.WorkDate.Hour, w.WorkDate.Minute, 0);

                                    retList.Add(apo);
                                }
                            }
                        }
                    }
                }


                foreach (DouseiInfo dinfo in douseiInfos)
                {
                    //====================================================================
                    // 入港と出港の間のパターンをクリアする
                    DateTime inTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                    DateTime outTime = inTime.AddDays(1);
                    if (dinfo.InTime != null)
                    {
                        var timeStr = ((DateTime)dinfo.InTime).ToShortTimeString();
                        int h = int.Parse(timeStr.Split(':')[0]);
                        int m = int.Parse(timeStr.Split(':')[1]);
                        if (m > 45)
                            m = 45;
                        else if (m > 30)
                            m = 30;
                        else if (m > 15)
                            m = 15;
                        else
                            m = 0;
                        inTime = new DateTime(date.Year, date.Month, date.Day, h, m, 0);
                    }
                    if (dinfo.OutTime != null)
                    {
                        var timeStr = ((DateTime)dinfo.OutTime).ToShortTimeString();
                        int h = int.Parse(timeStr.Split(':')[0]);
                        int m = int.Parse(timeStr.Split(':')[1]);
                        outTime = new DateTime(date.Year, date.Month, date.Day, h, m, 0);
                    }

                    for (DateTime d = inTime; d < outTime; d = d.AddMinutes(15))
                    {
                        var delAppts = retList.Where(o => o.WorkDate == d).ToList();
                        if (delAppts != null)
                        {
                            foreach (Appointment ap in delAppts)
                                retList.Remove(ap);
                        }
                    }

                    DateTime docking = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                    if (dinfo.Docking != null)
                    {
                        var timeStr = ((DateTime)dinfo.Docking).ToShortTimeString();
                        int h = int.Parse(timeStr.Split(':')[0]);
                        int m = int.Parse(timeStr.Split(':')[1]);
                        docking = new DateTime(date.Year, date.Month, date.Day, h, m, 0);
                    }

                    //====================================================================
                    // 入港のパターンを重ねる
                    if (dinfo.InTime != null)
                    {
                        var patternList = WorkPattern.GetRecords(loginUser, WorkPattern.WorkPatternEventKind.Kind入港, vesselId, dinfo.BashoId);

                        foreach (WorkPattern w in patternList)
                        {
                            Appointment apo = new Appointment();
                            apo.MsSiShokumeiID = w.MsSiShokuemiID;
                            apo.WorkContentID = w.WorkContentID;

                            if (w.WorkDateDiff > 0)
                                apo.WorkDate = inTime.AddMinutes(15 * (w.WorkDateDiff - 1));
                            else
                                apo.WorkDate = inTime.AddMinutes(15 * (w.WorkDateDiff));

                            if (retList.Any(o => o.MsSiShokumeiID == apo.MsSiShokumeiID && o.WorkDate == apo.WorkDate))
                            {
                                retList.Where(o => o.MsSiShokumeiID == apo.MsSiShokumeiID && o.WorkDate == apo.WorkDate).First().WorkContentID = w.WorkContentID;
                            }
                            else
                            {
                                retList.Add(apo);
                            }
                        }
                    }
                    //====================================================================
                    // 着桟のパターンを重ねる
                    if (dinfo.Docking != null)
                    {
                        var patternList = WorkPattern.GetRecords(loginUser, WorkPattern.WorkPatternEventKind.Kind着桟, vesselId, dinfo.BashoId);

                        foreach (WorkPattern w in patternList)
                        {
                            Appointment apo = new Appointment();
                            apo.MsSiShokumeiID = w.MsSiShokuemiID;
                            apo.WorkContentID = w.WorkContentID;

                            if (w.WorkDateDiff > 0)
                                apo.WorkDate = docking.AddMinutes(15 * (w.WorkDateDiff - 1));
                            else
                                apo.WorkDate = docking.AddMinutes(15 * (w.WorkDateDiff));

                            if (retList.Any(o => o.MsSiShokumeiID == apo.MsSiShokumeiID && o.WorkDate == apo.WorkDate))
                            {
                                retList.Where(o => o.MsSiShokumeiID == apo.MsSiShokumeiID && o.WorkDate == apo.WorkDate).First().WorkContentID = w.WorkContentID;
                            }
                            else
                            {
                                retList.Add(apo);
                            }
                        }
                    }
                    //====================================================================
                    // 出港のパターンを重ねる
                    if (dinfo.OutTime != null)
                    {
                        var patternList = WorkPattern.GetRecords(loginUser, WorkPattern.WorkPatternEventKind.Kind出港, vesselId, dinfo.BashoId);

                        foreach (WorkPattern w in patternList)
                        {
                            Appointment apo = new Appointment();
                            apo.MsSiShokumeiID = w.MsSiShokuemiID;
                            apo.WorkContentID = w.WorkContentID;

                            if (w.WorkDateDiff > 0)
                                apo.WorkDate = outTime.AddMinutes(15 * (w.WorkDateDiff - 1));
                            else
                                apo.WorkDate = outTime.AddMinutes(15 * (w.WorkDateDiff));

                            if (retList.Any(o => o.MsSiShokumeiID == apo.MsSiShokumeiID && o.WorkDate == apo.WorkDate))
                            {
                                retList.Where(o => o.MsSiShokumeiID == apo.MsSiShokumeiID && o.WorkDate == apo.WorkDate).First().WorkContentID = w.WorkContentID;
                            }
                            else
                            {
                                retList.Add(apo);
                            }
                        }
                    }

            }

            return retList;
        }

        private static void deviationProc(List<Appointment> AppointmentList, int vesselId, List<Work> devationCheckWorkList)
        {
            var seninIds = AppointmentList.Select(o => o.MsSeninID).Distinct().ToArray();
            foreach (int seninId in seninIds)
            {
                //System.Diagnostics.Debug.WriteLine($"  船員のDeviation:開始：{DateTime.Now.ToString("HH:mm:ss")}");

                string crewNo = seninId.ToString();
                var seninList = AppointmentList.Where(o => o.MsSeninID == seninId);
                var checkList = seninList.Where(o => o.CheckTarget == true).ToList();

                System.Diagnostics.Debug.WriteLine($"　　　　　deviationProc：checkList.Count{checkList.Count()}");
                foreach (Appointment ap in checkList)
                {
                    //
                    // あらゆる２４時間の労働時間：１４時間
                    //
                    //
                    //
                    var devCheckStart = ap.WorkDate.AddDays(-1);
                    var checkWorks = devationCheckWorkList.Where(o => o.CrewNo == crewNo && o.FinishWork >= devCheckStart && o.StartWork <= ap.WorkDate);
                    var tmpStart = devCheckStart;
                    checkWorks = checkWorks.OrderBy(o => o.StartWork).ToList();
                    var totalWork = 0;
                    foreach (Work wk in checkWorks)
                    {
                        if (wk.StartWork < devCheckStart)
                        {
                            totalWork += ((int)(wk.FinishWork - devCheckStart).TotalMinutes);
                        }
                        else if (wk.FinishWork > ap.WorkDate)
                        {
                            totalWork += ((int)(ap.WorkDate - wk.StartWork).TotalMinutes);
                        }
                        else
                        {
                            totalWork += ((int)(wk.FinishWork - wk.StartWork).TotalMinutes);
                        }
                    }
                    if (totalWork >= (60 * 14))　// １４時間
                    {
                        Appointment apo = new Appointment();
                        apo.MsSeninID = seninId;
                        apo.DeviationKind = 1; // あらゆる２４時間の労働時間
                        apo.WorkDate = ap.WorkDate;

                        AppointmentList.Add(apo);

                        //System.Diagnostics.Debug.WriteLine($"  あらゆる２４時間の労働時間：{apo.MsSeninID}:{apo.WorkDate}");
                    }




                    //
                    // あらゆる１週間の労働時間：７２時間
                    //
                    //
                    //
                    var dev1CheckStart = ap.WorkDate.AddDays(-7);
                    checkWorks = devationCheckWorkList.Where(o => o.CrewNo == crewNo && o.FinishWork >= dev1CheckStart && o.StartWork <= ap.WorkDate);
                    tmpStart = dev1CheckStart;
                    checkWorks = checkWorks.OrderBy(o => o.StartWork).ToList();
                    totalWork = 0;
                    foreach (Work wk in checkWorks)
                    {
                        if (wk.StartWork < dev1CheckStart)
                        {
                            totalWork += ((int)(wk.FinishWork - dev1CheckStart).TotalMinutes);
                        }
                        else if (wk.FinishWork > ap.WorkDate)
                        {
                            totalWork += ((int)(ap.WorkDate - wk.StartWork).TotalMinutes);
                        }
                        else
                        {
                            totalWork += ((int)(wk.FinishWork - wk.StartWork).TotalMinutes);
                        }
                    }
                    if (totalWork >= (60 * 72))　// ７２時間
                    {
                        Appointment apo = new Appointment();
                        apo.MsSeninID = seninId;
                        apo.DeviationKind = 2; // あらゆる１週間の労働時間
                        apo.WorkDate = ap.WorkDate;

                        AppointmentList.Add(apo);
                        //System.Diagnostics.Debug.WriteLine($"  あらゆる１週間の労働時間：{apo.MsSeninID}:{apo.WorkDate}");
                    }


                    //
                    // あらゆる４週間の時間外労働時間間：５６時間
                    //
                    //
                    //
                    var dev4CheckStart = DateTimeUtils.ToFrom(ap.WorkDate).AddMonths(-1);
                    int overTimes = 0;
                    checkWorks = devationCheckWorkList.Where(o => o.CrewNo == crewNo && o.FinishWork >= dev4CheckStart && o.StartWork <= ap.WorkDate);
                    tmpStart = dev4CheckStart;
                    var dayStart = tmpStart;
                    var dayEnd = dayStart.AddDays(1);
                    checkWorks = checkWorks.OrderBy(o => o.StartWork).ToList();
                    totalWork = 0;
                    foreach (Work wk in checkWorks)
                    {
                        if (wk.StartWork > tmpStart)
                        {
                            tmpStart = wk.StartWork;
                        }
                        if (wk.FinishWork > dayEnd)
                        {
                            totalWork += ((int)(dayEnd - tmpStart).TotalMinutes);

                            if (totalWork > (60 * 8)) // １日の作業時間が８時間を超えているか
                            {
                                overTimes += (totalWork - (60 * 8));
                            }

                            dayStart = dayEnd;
                            dayEnd.AddDays(1);
                            if (dayEnd > ap.WorkDate)
                            {
                                dayEnd = ap.WorkDate;
                            }
                        }
                        else
                        {
                            totalWork += ((int)(wk.FinishWork - tmpStart).TotalMinutes);
                            tmpStart = wk.FinishWork;
                        }
                    }

                    if (overTimes > (60 * 56))　// ５６時間
                    {
                        Appointment apo = new Appointment();
                        apo.MsSeninID = seninId;
                        apo.DeviationKind = 3; // あらゆる４週間の時間外労働時間
                        apo.WorkDate = ap.WorkDate;

                        AppointmentList.Add(apo);

                        //System.Diagnostics.Debug.WriteLine($"  あらゆる４週間の時間外労働時間：{apo.MsSeninID}:{apo.WorkDate}");
                    }


                    //
                    // ２４時間の休息時間
                    //
                    //
                    //
                    var restCheckStart = ap.WorkDate.AddDays(-1).AddMinutes(15);
                    checkWorks = devationCheckWorkList.Where(o => o.CrewNo == crewNo && o.FinishWork >= restCheckStart && o.StartWork < ap.WorkDate.AddMinutes(15));
                    var restMinutes = new List<int>();
                    tmpStart = restCheckStart;
                    checkWorks = checkWorks.OrderBy(o => o.StartWork).ToList();
                    foreach (Work wk in checkWorks)
                    {
                        if (wk.StartWork > tmpStart)
                        {
                            restMinutes.Add((int)(wk.StartWork - tmpStart).TotalMinutes);
                            tmpStart = wk.FinishWork;
                        }
                        else if (checkWorks.First() == wk)
                        {
                            tmpStart = wk.FinishWork;
                        }
                        else if (wk.StartWork == tmpStart)
                        {
                            tmpStart = wk.FinishWork;
                        }
                    }
                    if (restMinutes.Count > 0)
                    {
                        restMinutes = restMinutes.OrderByDescending(o => o).ToList();

                        int restTimes = restMinutes.First();
                        if (restMinutes.Count > 1)
                        {
                            restTimes += restMinutes[1];
                        }
                        if (restMinutes.Count > 2)
                        {
                            restTimes += restMinutes[2];
                        }

                        if (restTimes < (60 * 10)) // 60分×10時間
                        {
                            Appointment apo = new Appointment();
                            apo.MsSeninID = seninId;
                            apo.DeviationKind = 4; // ２４時間の休息時間
                            apo.WorkDate = ap.WorkDate;

                            AppointmentList.Add(apo);
                            //System.Diagnostics.Debug.WriteLine($"  ２４時間の休息時間：{apo.MsSeninID}:{apo.WorkDate}");
                        }
                        else if (restMinutes.First() < (60 * 6)) // 60分×6時間
                        {
                            Appointment apo = new Appointment();
                            apo.MsSeninID = seninId;
                            apo.DeviationKind = 4; // ２４時間の休息時間
                            apo.WorkDate = ap.WorkDate;

                            AppointmentList.Add(apo);
                            //System.Diagnostics.Debug.WriteLine($"  ２４時間の休息時間：{apo.MsSeninID}:{apo.WorkDate}");
                        }
                    }
                }


                //System.Diagnostics.Debug.WriteLine($"  船員のDeviation:終了：{DateTime.Now.ToString("HH:mm:ss")}");
            }
        }
    }
}
