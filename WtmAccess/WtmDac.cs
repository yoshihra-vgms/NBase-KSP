using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WtmModelBase;
using WtmModels;

namespace WtmData
{
    public class WtmDac
    {
        public static string SITE_ID { set; get; }
        public static string MODULE_ID { set; get; } = null;
        public static int SEND_FLAG { set; get; } = 0;


        public static List<Work> GetWorks(DateTime date1, DateTime date2, int seninId = 0, int vesselId = 0)
        {
            List<Work> ret = null;

            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                {
                    var list1 = Work.GetRecordsByDate(dbConnect, AccessorCommon.ConnectionKey, (DateTime)date1, (DateTime)date2, seninId, vesselId);


                    list1 = list1.Where(o => o.IsDelete == false).ToList();    // 先に削除されているものを除く


                    //=======================================================================
                    var crewNos = list1.Select(o => o.CrewNo).Distinct();
                    var deleteList = new List<string>();
                    foreach (var crewNo in crewNos)
                    {
                        var seninWorks = list1.Where(o => o.CrewNo == crewNo).OrderBy(o => o.StartWork).ThenBy(o => o.FinishWork);
                        foreach (var item in seninWorks)
                        {
                            var start = item.StartWork;
                            var end = item.FinishWork;

                            var query = seninWorks.Where(o => o.StartWork < end && o.FinishWork > start);
                            foreach (var item2 in query)
                            {
                                if (item2 == item)
                                { }
                                else if (item.SquenceDate < item2.SquenceDate)
                                {
                                    deleteList.Add(item.WorkID);
                                }
                                else
                                {
                                    deleteList.Add(item2.WorkID);
                                }
                            }
                        }

                    }
                    foreach (var item in deleteList)
                    {
                        list1.RemoveAll(o => o.WorkID.Equals(item));
                    }
                    
                    //=======================================================================
                    list1.ForEach(o => { o.ConvertWorkContentDetail(); });

                    list1.ForEach(o => { o.ConvertDeviation(1, o.Deviation); });
                    list1.ForEach(o => { o.ConvertDeviation(2, o.Deviation1Week); });
                    list1.ForEach(o => { o.ConvertDeviation(3, o.Deviation4Week); });
                    list1.ForEach(o => { o.ConvertDeviation(4, o.DeviationResttime); });

                    ret = list1;
                }
            }

            return ret;
        }

        public static Work GetBeInWork(int seninId)
        {
            Work ret = null;
            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                //var list1 = WorkTek.GetBeInWorkRecords(dbConnect, seninId);
                var list1 = Work.GetBeInWorkRecords(dbConnect, AccessorCommon.ConnectionKey, seninId);
                list1 = list1.Where(o => o.IsDelete == false).ToList();
                if (list1.Any(o => o.FinishWork == DateTime.MaxValue))
                {
                    ret = list1.Where(o => o.FinishWork == DateTime.MaxValue).First();
                }
            }
            return ret;
        }
        public static List<Work> GetBeInWorks(int vesselId)
        {
            List<Work> ret = (List<Work>)null;
            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                var list1 = Work.GetBeInWorkRecords(dbConnect, AccessorCommon.ConnectionKey, vesselId: vesselId);
                list1 = list1.Where(o => o.IsDelete == false).ToList();
                if (list1.Any(o => o.FinishWork == DateTime.MaxValue))
                {
                    ret = list1.Where(o => o.FinishWork == DateTime.MaxValue).ToList();
                }
            }
            return ret;
        }



        public static bool StartWork(int vesselId, int seninId, DateTime d)
        {
            bool ret = false;
            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                Work w = new Work();

                w.SiteID = SITE_ID;
                w.WorkID = System.Guid.NewGuid().ToString();
                w.SquenceDate = DateTime.Now;
                w.CrewNo = seninId.ToString();
                //w.CrewID
                w.VesselID = vesselId.ToString();
                w.StartWork = DateTime.Parse(d.ToString("yyyy/MM/dd HH:mm"));
                w.StartWorkAcutual = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                w.FinishWork = DateTime.MaxValue;
                w.FinishWorkAcutual = DateTime.MaxValue;

                w.Alive = true;

                SetSyncInfo(w);

                ret = w.InsertRecord(dbConnect, AccessorCommon.ConnectionKey);

            }
            return ret;
        }


        public static bool InsertWork(Work w)
        {
            bool ret = false;
            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                w.SiteID = SITE_ID;
                w.WorkID = System.Guid.NewGuid().ToString();
                w.SquenceDate = DateTime.Now;
                w.StartWorkAcutual = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                w.FinishWorkAcutual = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));

                w.Alive = true;

                SetSyncInfo(w);

                ret = w.InsertRecord(dbConnect, AccessorCommon.ConnectionKey);

            }
            return ret;
        }


        public static bool FinshWork(Work w)
        {
            bool ret = false;

            w.ConvertWorkContentDetail();


            List<WorkContentDetail> wcds = new List<WorkContentDetail>();
            List<List<WorkContentDetail>> wcdLists = new List<List<WorkContentDetail>>();
            List<DateTime> finishWorks = new List<DateTime>();

            // 休息を除く
            var workContentDetails = w.WorkContentDetails.Where(o => string.IsNullOrEmpty(o.WorkContentID) == false).OrderBy(o => o.WorkDate);

            //DateTime dt = workContentDetails.ElementAt(0).WorkDate;
            //wcds.Add(workContentDetails.ElementAt(0));

            //for (int i = 1; i < workContentDetails.Count(); i++)
            //{
            //    if (dt.AddMinutes(WtmCommon.WorkRange) != workContentDetails.ElementAt(i).WorkDate)
            //    {
            //        // 作業の時間がつながっていない => 休息
            //        wcdLists.Add(wcds);
            //        finishWorks.Add(dt.AddMinutes(WtmCommon.WorkRange));

            //        wcds = new List<WorkContentDetail>();
            //    }
            //    wcds.Add(workContentDetails.ElementAt(i));
            //    dt = workContentDetails.ElementAt(i).WorkDate;
            //}
            //wcdLists.Add(wcds);
            //finishWorks.Add(w.FinishWork);


            DateTime dt = DateTime.MinValue;
            for (int i = 0; i < workContentDetails.Count(); i++)
            {
                if (dt != DateTime.MinValue)
                {
                    if (string.IsNullOrEmpty(workContentDetails.ElementAt(i).WorkContentID) || dt.AddMinutes(WtmCommon.WorkRange) != workContentDetails.ElementAt(i).WorkDate)
                    {
                        // 作業がない 又は　作業の時間がつながっていない => 休息
                        wcdLists.Add(wcds);
                        finishWorks.Add(dt.AddMinutes(WtmCommon.WorkRange));

                        wcds = new List<WorkContentDetail>();
                        dt = DateTime.MinValue;
                    }
                }

                if (string.IsNullOrEmpty(workContentDetails.ElementAt(i).WorkContentID))
                    continue;

                wcds.Add(workContentDetails.ElementAt(i));
                dt = workContentDetails.ElementAt(i).WorkDate;
            }
            wcdLists.Add(wcds);
            finishWorks.Add(w.FinishWork);



            var start = w.StartWork;
            var finish = w.FinishWork;

            //if (wcdLists.Count == 1)
            //{
            //    // 作業詳細に「休息」を含まない場合

            //    w.StartWork = w.WorkContentDetails.First().WorkDate;
            //    w.FinishWork = w.WorkContentDetails.Last().WorkDate.AddMinutes(WtmCommon.WorkRange);

            //    if (IsNewWork(w))
            //    {
            //        ret = InsertWork(w);
            //        ret = EditWork(null, w);
            //    }
            //    else
            //    {
            //        ret = EditWork(null, w);
            //    }
            //}
            //else
            //{
            // 作業詳細に「休息」が含まれる場合

            int vesselId = int.Parse(w.VesselID);
                int crewId = int.Parse(w.CrewNo);

                int finishWorkCount = 0;

                foreach (List<WorkContentDetail> wcdList in wcdLists)
                {
                    string wcdStr = "";
                    DateTime work_date = wcdList.First().WorkDate;
                    string wcdId = wcdList.First().WorkContentID;
                    int count = 0;
                    foreach (var wcd in wcdList)
                    {
                        if (work_date == wcd.WorkDate)
                        {
                            wcdStr += $"{work_date.ToString("yyyy/MM/dd HH:mm:ss")},{wcdId},";
                            count++;
                        }
                        else if (wcdId == wcd.WorkContentID)
                        {
                            count++;
                        }
                        else
                        {
                            wcdStr += $"{count};";

                            work_date = wcd.WorkDate;
                            wcdId = wcd.WorkContentID;
                            wcdStr += $"{work_date.ToString("yyyy/MM/dd HH:mm:ss")},{wcdId},";
                            count = 1;
                        }
                    }
                    wcdStr += $"{count};";

                    var nightTimeList = wcdList.Where(o => o.NightTime == true).OrderBy(o => o.WorkDate);
                    string ntStr = "";
                    if (nightTimeList != null && nightTimeList.Count() > 0)
                    {
                        work_date = nightTimeList.First().WorkDate;
                        count = 0;
                        foreach (var wcd in nightTimeList)
                        {
                            if (work_date == wcd.WorkDate)
                            {
                                ntStr += $"{work_date.ToString("yyyy/MM/dd HH:mm:ss")},";
                                count++;
                            }
                            else if (work_date.AddMinutes(WtmCommon.WorkRange) == wcd.WorkDate)
                            {
                                count++;
                                work_date = wcd.WorkDate;
                            }
                            else
                            {
                                ntStr += $"{count};";

                                work_date = wcd.WorkDate;
                                ntStr += $"{work_date.ToString("yyyy/MM/dd HH:mm:ss")},";
                                count = 1;
                            }
                        }
                        if (string.IsNullOrEmpty(ntStr) == false)
                        {
                            ntStr += $"{count};";
                        }

                    }


                    w.WorkContentDetail = wcdStr;
                    w.NightTime = ntStr;

                    w.ConvertWorkContentDetail();


                    if (wcdList == wcdLists.First())
                    {
                        w.StartWork = start;
                        if (start < wcdList.First().WorkDate)
                        {
                            w.StartWork = wcdList.First().WorkDate;
                        }
                        if (wcdList == wcdLists.Last())
                        {
                            w.FinishWork = finish;
                            if (wcdList.Last().WorkDate.AddMinutes(WtmCommon.WorkRange) < finish)
                            {
                                w.FinishWork = wcdList.Last().WorkDate.AddMinutes(WtmCommon.WorkRange);
                            }
                        }
                        else
                        {
                            w.FinishWork = wcdList.Last().WorkDate.AddMinutes(WtmCommon.WorkRange);
                        }

                        if (IsNewWork(w))
                        {
                            ret = InsertWork(w);
                            ret = EditWork(null, w);
                        }
                        else
                        {
                            SetSyncInfo(w);
                            ret = EditWork(null, w);
                        }
                    }
                    else  if (wcdList == wcdLists.Last())
                    {
                        //w.StartWork = w.WorkContentDetails.First().WorkDate;
                        //w.FinishWork = finishWorks.ElementAt(finishWorkCount);
                        w.StartWork = wcdList.First().WorkDate;
                        w.FinishWork = finish;
                        if (wcdList.Last().WorkDate.AddMinutes(WtmCommon.WorkRange) < finish)
                        {
                            w.FinishWork = wcdList.Last().WorkDate.AddMinutes(WtmCommon.WorkRange);
                        }

                        ret = InsertWork(w);
                        ret = EditWork(null, w);
                    }
                    else
                    {
                        //w.StartWork = w.WorkContentDetails.First().WorkDate;
                        //w.FinishWork = finishWorks.ElementAt(finishWorkCount);
                        w.StartWork = wcdList.First().WorkDate;
                        w.FinishWork = wcdList.Last().WorkDate.AddMinutes(WtmCommon.WorkRange);

                        ret = InsertWork(w);
                        ret = EditWork(null, w);
                    }

                    finishWorkCount++;
                }
            //}

            return ret;
        }

        public static bool DeleteWork(Work w)
        {
            return EditWork(w, null);
        }

        public static bool EditWork(Work delWork, Work editWork)
        {
            bool ret = false;

            var crewNo = delWork != null ? delWork.CrewNo : editWork.CrewNo;


            Work beforeEditWork = null;


            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                if (delWork == null || delWork.FinishWork != DateTime.MaxValue)
                {
                    // 編集前データ
                    if (editWork != null)
                    {
                        beforeEditWork = Work.GetRecord(dbConnect, AccessorCommon.ConnectionKey, editWork.WorkID);
                    }

                    // 見直すデータ(削除データ以降または編集後データ以降）
                    DateTime fromDate = DateTime.MaxValue;
                    DateTime toDate = DateTime.MaxValue;
                    if (delWork != null)
                    {
                        fromDate = delWork.FinishWork;
                        toDate = delWork.FinishWork.AddMonths(1);
                    }
                    if (editWork != null)
                    {
                        fromDate = DateTime.MaxValue;
                        if (beforeEditWork != null && beforeEditWork.StartWork < fromDate)
                            fromDate = beforeEditWork.StartWork;
                        if (editWork.StartWork < fromDate)
                            fromDate = editWork.StartWork;

                        toDate = editWork.FinishWork;
                        if (beforeEditWork != null && beforeEditWork.FinishWork != DateTime.MaxValue && beforeEditWork.FinishWork > toDate)
                            toDate = beforeEditWork.FinishWork;

                        toDate = toDate.AddMonths(1);
                    }
                    var assesslist = GetWorks(fromDate, toDate, int.Parse(crewNo));

                    var tmpAssessList = assesslist.AsEnumerable();
                    if (editWork != null)
                    {
                        if (tmpAssessList.Any(o => o.WorkID == editWork.WorkID))
                        {
                            tmpAssessList = tmpAssessList.Where(o => o.WorkID != editWork.WorkID);
                        }
                    }
                    if (delWork != null)
                    {
                        if (tmpAssessList.Any(o => o.WorkID == delWork.WorkID))
                        {
                            tmpAssessList = tmpAssessList.Where(o => o.WorkID != delWork.WorkID).OrderBy(o => o.StartWork);
                        }
                    }

                    assesslist = tmpAssessList.Where(o => (o.StartWork >= fromDate && o.StartWork < toDate) || (o.FinishWork > fromDate && o.FinishWork < toDate)).ToList();
                    if (editWork != null)
                    {
                        editWork.ConvertWorkContentDetail();

                        assesslist.Add(editWork);
                        assesslist = assesslist.OrderBy(o => o.StartWork).ToList();
                    }
                    assesslist = assesslist.OrderBy(o => o.StartWork).ToList();



                    // 見直すために過去データの準備
                    toDate = fromDate;
                    fromDate = fromDate.AddMonths(-1); // 最大４週間前から
                    var pastlist = GetWorks(fromDate, toDate, int.Parse(crewNo));

                    var tmpPastList = pastlist.AsEnumerable();
                    if (editWork != null)
                    {
                        if (tmpPastList.Any(o => o.WorkID == editWork.WorkID))
                        {
                            tmpPastList = tmpPastList.Where(o => o.WorkID != editWork.WorkID);
                        }
                    }
                    if (delWork != null)
                    {
                        if (tmpPastList.Any(o => o.WorkID == delWork.WorkID))
                        {
                            tmpPastList = tmpPastList.Where(o => o.WorkID != delWork.WorkID);
                        }
                    }
                    pastlist = tmpPastList.Where(o => (o.StartWork >= fromDate && o.StartWork < toDate) || (o.FinishWork > fromDate && o.FinishWork < toDate)).OrderBy(o => o.StartWork).ToList();


                    if (assesslist != null && assesslist.Count > 0)
                    {
                        // Deviation見直し
                        var reassessedList = deviationProc(pastlist, assesslist);

                        foreach (Work w in reassessedList)
                        {
                            w.ConvertDeviation();

                            SetSyncInfo(w);
                            ret = w.UpdateRecord(dbConnect, AccessorCommon.ConnectionKey);
                        }
                    }
                }



                // 
                if (delWork != null)
                {
                    delWork.IsDelete = true;
                    delWork.UpdateDate = DateTime.Now;

                    SetSyncInfo(delWork);
                    ret = delWork.UpdateRecord(dbConnect, AccessorCommon.ConnectionKey);
                }
            }


            return ret;
        }



        public static bool CanCopyWork(Work w, List<int> seninIds, DateTime fromDay, DateTime toDay, out List<int> errorSeninIds)
        {
            bool ret = true;

            errorSeninIds = new List<int>();

            var sp = w.FinishWork.Date - w.StartWork.Date;
            foreach (int seninId in seninIds)
            {
                var inWork = GetBeInWork(seninId);
                var works = GetWorks(fromDay.Date, toDay.Date.AddDays(sp.TotalDays), seninId);
                for (DateTime wdate = fromDay.Date; wdate <= toDay.Date; wdate = wdate.AddDays(1))
                {
                    var sw = wdate.Date.AddHours(w.StartWork.Hour).AddMinutes(w.StartWork.Minute);
                    var fw = wdate.Date.AddHours(w.FinishWork.Hour).AddMinutes(w.FinishWork.Minute).AddDays(sp.TotalDays);

                    if (inWork != null)
                    {
                        if (inWork.StartWork <= sw)
                        {
                            // コピー先：出勤中あり
                            ret = false;

                            errorSeninIds.Add(seninId);
                        }
                    }
                    if (works.Any(o => (o.StartWork <= sw && sw < o.FinishWork) || (o.StartWork < fw && fw <= o.FinishWork) ||
                                       (sw <= o.StartWork && o.StartWork < fw) || (sw < o.FinishWork && o.FinishWork <= fw)))
                    {
                        // コピー先：勤務あり
                        ret = false;

                        errorSeninIds.Add(seninId);
                    }
                }
            }

            return ret;
        }



        public static bool CopyWork(Work w, List<int> seninIds, DateTime fromDay, DateTime toDay)
        {
            bool ret = true;

            var sp = w.FinishWork.Date - w.StartWork.Date;
            for (DateTime wdate = fromDay.Date; wdate <= toDay.Date; wdate = wdate.AddDays(1))
            {
                var sw = wdate.Date.AddHours(w.StartWork.Hour).AddMinutes(w.StartWork.Minute);
                var fw = wdate.Date.AddHours(w.FinishWork.Hour).AddMinutes(w.FinishWork.Minute).AddDays(sp.TotalDays);

                foreach (int seninId in seninIds)
                {
                    Work dst = new Work();

                    //dst.SiteID = w.SiteID;
                    //dst.WorkID = w.WorkID;
                    //dst.SquenceDate = w.SquenceDate;
                    dst.CrewNo = seninId.ToString();
                    //dst.CrewID = w.CrewID;
                    dst.VesselID = w.VesselID;
                    dst.StartWork = sw;
                    //dst.StartWorkAcutual
                    dst.FinishWork = fw;
                    //dst.FinishWorkAcutual
                    //dst.IsDeviation
                    //dst.IsDeviation1Week
                    //dst.IsDeviation4Week
                    //dst.IsDeviationResttime


                    //dst.WorkContentDetail = w.WorkContentDetail;

                    //dst.WorkContentDetails = new List<WorkContentDetail>();

                    //foreach (WorkContentDetailTek wcd in w.WorkContentDetails)
                    //{
                    //    TimeSpan span = wdate - wcd.WorkDate.Date;

                    //    WorkContentDetailTek dstWcd = new WorkContentDetailTek();

                    //    dstWcd.WorkDate = wcd.WorkDate.Add(span);
                    //    dstWcd.WorkContentID = wcd.WorkContentID;
                    //    dstWcd.NightTime = wcd.NightTime;
                    //    dst.WorkContentDetails.Add(dstWcd);
                    //}


                    string wcdStr = "";
                    DateTime work_date = w.WorkContentDetails.First().WorkDate;
                    string wcdId = w.WorkContentDetails.First().WorkContentID;
                    int count = 0;
                    foreach (var wcd in w.WorkContentDetails)
                    {
                        TimeSpan span = wdate - wcd.WorkDate.Date;

                        if (work_date == wcd.WorkDate)
                        {
                            wcdStr += $"{work_date.Add(span).ToString("yyyy/MM/dd HH:mm:ss")},{wcdId},";
                            count++;
                        }
                        else if (wcdId == wcd.WorkContentID)
                        {
                            count++;
                        }
                        else
                        {
                            wcdStr += $"{count};";

                            work_date = wcd.WorkDate;
                            wcdId = wcd.WorkContentID;
                            wcdStr += $"{work_date.Add(span).ToString("yyyy/MM/dd HH:mm:ss")},{wcdId},";
                            count = 1;
                        }
                    }
                    wcdStr += $"{count};";
                    dst.WorkContentDetail = wcdStr;


                    //dst.Deviation
                    //dst.Deviation1Week
                    //dst.Deviation4Week
                    //dst.DeviationResttime
                    dst.NightTime = w.NightTime;

                    dst.UpdateDate = DateTime.Now;

                    InsertWork(dst);
                    EditWork(null, dst);
                }
            }

            return ret;
        }



        private static List<Work> deviationProc(List<Work> pastlist, List<Work> assesslist)
        {
            foreach (var work in assesslist)
            {
                pastlist.Add(work);
                work.Deviations = new List<Deviation>();

                foreach (var wcd in work.WorkContentDetails)
                {
                    System.Diagnostics.Debug.WriteLine($"{wcd.WorkDate}");


                    //
                    // あらゆる２４時間の労働時間：def=１４時間
                    //
                    //
                    //
                    var devCheckStart = wcd.WorkDate.AddDays(-1);

                    //System.Diagnostics.Debug.Write($"CheckStart:{devCheckStart}");

                    var checkWorks = pastlist.Where(o => o.FinishWork >= devCheckStart && o.StartWork <= wcd.WorkDate);
                    var tmpStart = devCheckStart;
                    checkWorks = checkWorks.OrderBy(o => o.StartWork).ToList();
                    var totalWork = 0;
                    foreach (Work wk in checkWorks)
                    {
                        if (wk.StartWork < devCheckStart)
                        {
                            totalWork += ((int)(wk.FinishWork - devCheckStart).TotalMinutes);
                        }
                        else if (wk.FinishWork > wcd.WorkDate)
                        {
                            totalWork += ((int)(wcd.WorkDate - wk.StartWork).TotalMinutes);
                        }
                        else
                        {
                            totalWork += ((int)(wk.FinishWork - wk.StartWork).TotalMinutes);
                        }
                    }
                    //System.Diagnostics.Debug.Write($" => {totalWork}");
                    if (totalWork >= (60 * WtmCommon.Deviation)) // １４時間
                    {
                        //System.Diagnostics.Debug.Write($" => Deviation");
                        Deviation dev = new Deviation();
                        dev.WorkID = work.WorkID;
                        dev.Kind = 1; // あらゆる２４時間の労働時間
                        dev.WorkDate = wcd.WorkDate;

                        work.Deviations.Add(dev);
                    }
                    //System.Diagnostics.Debug.WriteLine("");





                    //
                    // あらゆる１週間の労働時間：def=７２時間
                    //
                    //
                    //
                    var dev1CheckStart = wcd.WorkDate.AddDays(-7);
                    checkWorks = pastlist.Where(o => o.FinishWork >= dev1CheckStart && o.StartWork <= wcd.WorkDate);
                    tmpStart = dev1CheckStart;
                    checkWorks = checkWorks.OrderBy(o => o.StartWork).ToList();
                    totalWork = 0;
                    foreach (Work wk in checkWorks)
                    {
                        if (wk.StartWork < dev1CheckStart)
                        {
                            totalWork += ((int)(wk.FinishWork - dev1CheckStart).TotalMinutes);
                        }
                        else if (wk.FinishWork > wcd.WorkDate)
                        {
                            totalWork += ((int)(wcd.WorkDate - wk.StartWork).TotalMinutes);
                        }
                        else
                        {
                            totalWork += ((int)(wk.FinishWork - wk.StartWork).TotalMinutes);
                        }
                    }

                    if (totalWork >= (60 * WtmCommon.Deviation1Week))　// ７２時間
                    {
                        Deviation dev = new Deviation();
                        dev.WorkID = work.WorkID;
                        dev.Kind = 2; // あらゆる１週間の労働時間
                        dev.WorkDate = wcd.WorkDate;

                        work.Deviations.Add(dev);
                    }





                    //
                    // あらゆる４週間の時間外労働時間：def=５６時間
                    //
                    //
                    //
                    var dev4CheckStart = ToFrom(wcd.WorkDate).AddMonths(-1);
                    int overTimes = 0;
                    checkWorks = pastlist.Where(o => o.FinishWork >= dev4CheckStart && o.StartWork <= wcd.WorkDate);
                    tmpStart = dev4CheckStart;
                    var dayStart = tmpStart;
                    var dayEnd = dayStart.AddDays(1);
                    checkWorks = checkWorks.OrderBy(o => o.StartWork).ToList();
                    totalWork = 0;
                    //foreach (Work wk in checkWorks)
                    //{
                    //    if (wk.StartWork > tmpStart)
                    //    {
                    //        tmpStart = wk.StartWork;
                    //    }
                    //    if (wk.FinishWork > dayEnd)
                    //    {
                    //        totalWork += ((int)(dayEnd - tmpStart).TotalMinutes);

                    //        System.Diagnostics.Debug.WriteLine($"      => {tmpStart} の作業 :{totalWork} = ({totalWork / 60}:{totalWork % 60})");

                    //        if (totalWork > (60 * (int)WtmCommon.WorkingHours)) // １日の作業時間が８時間を超えているか
                    //        {
                    //            overTimes += (totalWork - (60 * (int)WtmCommon.WorkingHours));
                    //        }

                    //        dayStart = dayEnd;
                    //        dayEnd = dayStart.AddDays(1);
                    //        if (dayEnd > wcd.WorkDate)
                    //        {
                    //            dayEnd = wcd.WorkDate;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        totalWork += ((int)(wk.FinishWork - tmpStart).TotalMinutes);
                    //        tmpStart = wk.FinishWork;
                    //    }
                    //}

                    var days = checkWorks.Select(o => o.StartWork.Date).Concat(checkWorks.Select(o => o.FinishWork.Date)).Distinct();
                    foreach(var day in days)
                    {
                        totalWork = 0;

                        var worksByDay = checkWorks.Where(o => o.StartWork.Date == day || o.FinishWork.Date == day).OrderBy(o => o.StartWork);
                        foreach(var wk in worksByDay)
                        {
                            DateTime st = wk.StartWork;
                            DateTime ed = wk.FinishWork;
                            if (wk.StartWork.Date < day)
                                st = day;
                            if (ed.Date != day)
                                ed = day.AddDays(1);

                            totalWork += ((int)(ed - st).TotalMinutes);
                        }

                        //System.Diagnostics.Debug.WriteLine($"      => {day} の作業 :{totalWork} = ({totalWork / 60}:{totalWork % 60})");

                        if (totalWork > (60 * (int)WtmCommon.WorkingHours)) // １日の作業時間が８時間を超えているか
                        {
                            overTimes += (totalWork - (60 * (int)WtmCommon.WorkingHours));
                        }

                        //System.Diagnostics.Debug.WriteLine($"            => overTimes:{overTimes}");
                    }
                    System.Diagnostics.Debug.WriteLine($"=> overTimes:{overTimes}");


                    if (overTimes > (60 * WtmCommon.Deviation4Week))　// ５６時間
                    {
                        Deviation dev = new Deviation();
                        dev.WorkID = work.WorkID;
                        dev.Kind = 3; // あらゆる４週間の時間外労働時間
                        dev.WorkDate = wcd.WorkDate;

                        work.Deviations.Add(dev);
                    }

                    System.Diagnostics.Debug.WriteLine("");





                    //
                    // ２４時間の休息時間
                    //
                    //
                    //
                    var restCheckStart = wcd.WorkDate.AddDays(-1).AddMinutes(WtmCommon.WorkRange);
                    checkWorks = pastlist.Where(o => o.FinishWork >= restCheckStart && o.StartWork < wcd.WorkDate.AddMinutes(WtmCommon.WorkRange));
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

                        for(int i = 1; i < WtmCommon.DeviationRestTimeCount; i++)
                        {
                            if (restMinutes.Count() > i)
                                restTimes += restMinutes[i];
                        }


                        if (restTimes < (60 * WtmCommon.DeviationRestTime)) // 60分×10時間
                        {
                            Deviation dev = new Deviation();
                            dev.WorkID = work.WorkID;
                            dev.Kind = 4; // ２４時間の休息時間
                            dev.WorkDate = wcd.WorkDate;

                            work.Deviations.Add(dev);
                        }
                        else if (restMinutes.First() < (60 * WtmCommon.DeviationLongRestTime)) // 60分×6時間
                        {
                            Deviation dev = new Deviation();
                            dev.WorkID = work.WorkID;
                            dev.Kind = 4; // ２４時間の休息時間
                            dev.WorkDate = wcd.WorkDate;

                            work.Deviations.Add(dev);
                        }
                    }
                }

            }
            var start = assesslist.Min(o => o.StartWork);
            return pastlist.Where(o => (o.StartWork >= start) || (o.FinishWork > start)).OrderBy(o => o.StartWork).ToList();
        }


        public static DateTime ToFrom(DateTime date)
        {
            DateTime result = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

            return result;
        }

        public static bool IsNewWork(Work w)
        {
            return string.IsNullOrEmpty(w.WorkID);
        }


        #region Setting
        public static Setting GetSetting()
        {
            Setting ret = null;

            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                ret = WtmModelBase.Setting.GetRecord(dbConnect, AccessorCommon.ConnectionKey.ToLower());
            }

            return ret;
        }
        #endregion


        #region WorkContent
        public static List<WorkContent> GetWorkContents()
        {
            List<WorkContent> ret = null;

            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                ret = WtmModelBase.WorkContent.GetRecords(dbConnect, AccessorCommon.ConnectionKey.ToLower());
            }

            return ret;
        }
        #endregion


        #region RankCategory
        public static List<RankCategory> GetRankCategories()
        {
            List<RankCategory> ret = null;

            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                ret = WtmModelBase.RankCategory.GetRecords(dbConnect, AccessorCommon.ConnectionKey.ToLower());
                ret.ForEach(o => { o.ConvertRanks(); });
            }

            return ret;
        }
        #endregion

        #region Role
        public static List<Role> GetRoles()
        {
            List<Role> ret = null;

            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                ret = WtmModelBase.Role.GetRecords(dbConnect, AccessorCommon.ConnectionKey.ToLower());
                ret.ForEach(o => { o.ConvertRanks(); });
            }

            return ret;
        }
        #endregion

        #region SyncTables
        public static List<SyncTables> GetSyncTables()
        {
            List<SyncTables> ret = null;

            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                ret = WtmModelBase.SyncTables.GetRecords(dbConnect, $"sync_tables_{AccessorCommon.ConnectionKey.ToLower()}");
            }

            return ret;
        }
        #endregion






        #region VesselMovement
        public static List<VesselMovement> GetVesselMovementDispRecord(DateTime date1, DateTime date2, int vesselid = 0)
        {
            List<VesselMovement> ret = new List<VesselMovement>();

            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                if (vesselid > 0)
                    ret = VesselMovement.GetRecordsByDate(dbConnect, AccessorCommon.ConnectionKey.ToLower(), date1.ToString("yyyyMMdd"), date2.ToString("yyyyMMdd"), vesselid.ToString());
                else
                    ret = VesselMovement.GetRecordsByDate(dbConnect, AccessorCommon.ConnectionKey.ToLower(), date1.ToString("yyyyMMdd"), date2.ToString("yyyyMMdd"));
            }

            if (ret.Count > 0)
            {
                ret = ret.OrderBy(obj => obj.DateInfo).ToList();
            }

            return ret;
        }
        public static bool InsertUpdateVesselMovement(VesselMovement vm)
        {
            bool ret = false;
            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                vm.SiteID = SITE_ID;
                vm.SquenceDate = DateTime.Now;
                //vm.GeneratorID

                SetSyncInfo(vm);
                ret = VesselMovement.InsertOrUpdate(dbConnect, AccessorCommon.ConnectionKey.ToLower(), vm);

            }
            return ret;
        }

        #endregion

        #region  MovementInfo
        //public static List<MovementInfoTek> GetMovementInfoRecord(DateTime date1, DateTime date2, int vessel_id)
        //{
        //    List<MovementInfoTek> ret = new List<MovementInfoTek>();

        //    using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
        //    {
        //        ret = MovementInfoTek.GetRecordsByDate(dbConnect, date1.ToString("yyyyMMdd"), date2.ToString("yyyyMMdd"), vessel_id.ToString());
        //    }

        //    if (ret.Count > 0)
        //    {
        //        ret = ret.OrderBy(obj => obj.DateInfo).ToList();
        //    }

        //    return ret;
        //}

        //public static bool InsertUpdateMovementInfo(string vesselid, string dateinfo, List<MovementInfoTek> mvis)
        //{
        //    bool ret = false;
        //    using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
        //    {
        //        ret = MovementInfoTek.ReverseEntries2(dbConnect, SITE_ID, vesselid, dateinfo, mvis);

        //    }
        //    return ret;
        //}
        #endregion


        #region Anchorage 

        //public static List<AnchorageTek> GetAnchorageRecords(DateTime date1, DateTime date2, int vessel_id)
        //{
        //    List<AnchorageTek> ret = new List<AnchorageTek>();


        //    using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
        //    {
        //        ret = AnchorageTek.GetRecordsByDate(dbConnect, date1.ToString("yyyyMMdd"), date2.ToString("yyyyMMdd"), vessel_id.ToString());
        //    }

        //    if (ret.Count > 0)
        //    {
        //        ret = ret.OrderBy(obj => obj.Start).ToList();

        //    }

        //    return ret;
        //}

        //public static bool InsertUpdateAnchorage(string vesselId, string dateinfo, List<AnchorageTek> ancs)
        //{
        //    bool ret = false;
        //    using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
        //    {
        //        ret = AnchorageTek.ReverseEntries2(dbConnect, SITE_ID, vesselId, dateinfo, ancs);
        //    }
        //    return ret;
        //}


        #endregion





        #region VesselApprovalMonth
        public static VesselApprovalMonth GetVesselApprovalMonth(int vesselId, DateTime approvalMonth)
        {
            VesselApprovalMonth vesselApprovalMonth = null;
            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                var vamList = VesselApprovalMonth.GetRecordsByDate(dbConnect, AccessorCommon.ConnectionKey.ToLower(), approvalMonth, vesselId.ToString());
                vesselApprovalMonth = vamList.FirstOrDefault();
            }
            return vesselApprovalMonth;
        }

        public static bool InsertOrUpdateApprovalMonth(int vesselId, int crewId, DateTime approvalMonth)
        {
            bool ret = false;
            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                VesselApprovalMonth vesselApprovalMonth = null;

                var vamList = VesselApprovalMonth.GetRecordsByDate(dbConnect, AccessorCommon.ConnectionKey.ToLower(), approvalMonth, vesselId.ToString());
                if (vamList == null || vamList.Count == 0)
                {
                    vesselApprovalMonth = new VesselApprovalMonth();
                    vesselApprovalMonth.SiteID = SITE_ID;
                    vesselApprovalMonth.VesselApprovalMonthID = System.Guid.NewGuid().ToString();
                    vesselApprovalMonth.VesselID = vesselId.ToString();
                    vesselApprovalMonth.ApprovalMonth = approvalMonth;
                    vesselApprovalMonth.ApprovalCrewNo = crewId.ToString();
                    vesselApprovalMonth.IsDelete = false;
                    vesselApprovalMonth.SquenceDate = DateTime.Now;
                    vesselApprovalMonth.Alive = true;
                }
                else
                {
                    vesselApprovalMonth = vamList.First();
                    vesselApprovalMonth.SquenceDate = DateTime.Now;
                }

                SetSyncInfo(vesselApprovalMonth);
                ret = VesselApprovalMonth.InsertOrUpdate(dbConnect, AccessorCommon.ConnectionKey.ToLower(), vesselApprovalMonth);
            }
            return ret;
        }
        #endregion

        #region VesselApprovalDay
        public static List<VesselApprovalDay> GetVesselApprovalDay(int vesselId, DateTime approvalDay)
        {
            List<VesselApprovalDay> vesselApprovalDays = null;
            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                vesselApprovalDays = VesselApprovalDay.GetRecordsByDate(dbConnect, AccessorCommon.ConnectionKey.ToLower(), approvalDay, vesselId.ToString());
            }
            return vesselApprovalDays.Where(o => o.Alive == true && o.IsDelete == false).ToList();
        }

        public static int InsertOrUpdateApprovalDay(int vesselId, DateTime approvalDay, int crewId, List<int>approvedCrewIds)
        {
            int retCount = 0;

            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                List<VesselApprovalDay> vesselApprovalDays = VesselApprovalDay.GetRecordsByDate(dbConnect, AccessorCommon.ConnectionKey.ToLower(), approvalDay, vesselId.ToString());

                foreach(int approverdCrewId in approvedCrewIds)
                {
                    var vad = vesselApprovalDays.Where(o => o.ApprovedCrewNo == approverdCrewId.ToString()).FirstOrDefault();

                    if (vad != null && vad.Alive == true && vad.IsDelete == false)
                        continue;

                    if (vad == null)
                    {
                        vad = new VesselApprovalDay();
                        vad.SiteID = SITE_ID;
                        vad.VesselApprovalDayID = System.Guid.NewGuid().ToString();
                        vad.VesselID = vesselId.ToString();
                        vad.ApprovalDay = approvalDay;
                        vad.ApproverCrewNo = crewId.ToString();
                        vad.ApprovedCrewNo = approverdCrewId.ToString();
                        vad.IsDelete = false;
                        vad.SquenceDate = DateTime.Now;
                        vad.Alive = true;
                    }
                    else
                    {
                        vad.IsDelete = false;
                        vad.SquenceDate = DateTime.Now;
                        vad.Alive = true;
                    }

                    SetSyncInfo(vad);
                    var ret = VesselApprovalDay.InsertOrUpdate(dbConnect, AccessorCommon.ConnectionKey.ToLower(), vad);

                    if (ret)
                        retCount++;
                }

            }
            return retCount;
        }
        public static bool DeleteApprovalDay(int vesselId, DateTime approvalDay, int removeCrewId)
        {
            bool ret = false;
            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                List<VesselApprovalDay> vesselApprovalDays = VesselApprovalDay.GetRecordsByDate(dbConnect, AccessorCommon.ConnectionKey.ToLower(), approvalDay, vesselId.ToString());
                var vad = vesselApprovalDays.Where(o => o.ApprovedCrewNo == removeCrewId.ToString()).FirstOrDefault();

                if (vad != null)
                {
                    vad.IsDelete = true;
                    vad.SquenceDate = DateTime.Now;
                    vad.Alive = false;

                    SetSyncInfo(vad);
                    ret = VesselApprovalDay.InsertOrUpdate(dbConnect, SITE_ID, vad);
                }
            }
            return ret;
        }
        #endregion


        #region SummaryTimes
        public static List<SummaryTimes> GetSummaryTimes(int vesselId, DateTime summaryDate)
        {
            List<SummaryTimes> summaryTimesTek = null;
            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                summaryTimesTek = SummaryTimes.GetRecordsByDate(dbConnect, AccessorCommon.ConnectionKey.ToLower(), summaryDate, vesselId.ToString());
            }
            return summaryTimesTek.Where(o => o.Alive == true).ToList();
        }

        public static bool InsertOrUpdateSummaryTimes(int crewId, int vesselId, DateTime summaryDate, string allowanceTime)
        {
            bool ret = true;

            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                List<SummaryTimes> summaryTimesTek = SummaryTimes.GetRecordsByDate(dbConnect, AccessorCommon.ConnectionKey.ToLower(), summaryDate, vesselId.ToString(), crewId.ToString());

                SummaryTimes sts = null;
                if (summaryTimesTek != null)
                {
                    sts = summaryTimesTek.Where(o => o.Alive == true).FirstOrDefault();
                }
                if (sts == null)
                {
                    sts = new SummaryTimes();
                    sts.SiteID = SITE_ID;
                    sts.SummaryTimesID = System.Guid.NewGuid().ToString();
                    sts.VesselID = vesselId.ToString();
                    sts.CrewNo = crewId.ToString();
                    sts.SummaryDate = summaryDate;
                    sts.AllowanceTime = allowanceTime;
                    sts.UpdateDate = DateTime.Now;
                    sts.Alive = true;
                }
                else
                {
                    sts.AllowanceTime = allowanceTime;
                    sts.UpdateDate = DateTime.Now;
                    sts.Alive = true;
                }

                SetSyncInfo(sts);
                ret = SummaryTimes.InsertOrUpdate(dbConnect, AccessorCommon.ConnectionKey.ToLower(), sts);
            }
            return ret;
        }

        #endregion






        #region MaxDataNo
        public static int GetMaxDateNo(string tabelName)
        {
            int ret = 0;

            using (ORMapping.DBConnect dbConnect = AccessorCommon.GetConnection())
            {
                {
                    ret = WtmModelBase.BaseCommon.GetMaxNo(dbConnect, tabelName, AccessorCommon.ConnectionKey.ToLower());
                }
            }

            return ret;
        }
        #endregion



        private static void SetSyncInfo(Work obj)
        {
            obj.SendFlag = SEND_FLAG;
            obj.UserKey = MODULE_ID;
        }
        private static void SetSyncInfo(VesselMovement obj)
        {
            obj.SendFlag = SEND_FLAG;
            obj.UserKey = MODULE_ID;
        }
        //private static void SetSyncInfo(Anchorage obj)
        //{
        //    obj.SendFlag = SEND_FLAG;
        //    obj.UserKey = MODULE_ID;
        //}

        //private static void SetSyncInfo(MovementInfo obj)
        //{
        //    obj.SendFlag = SEND_FLAG;
        //    obj.UserKey = MODULE_ID;
        //}
        private static void SetSyncInfo(VesselApprovalMonth obj)
        {
            obj.SendFlag = SEND_FLAG;
            obj.UserKey = MODULE_ID;
        }
        private static void SetSyncInfo(VesselApprovalDay obj)
        {
            obj.SendFlag = SEND_FLAG;
            obj.UserKey = MODULE_ID;
        }
        private static void SetSyncInfo(SummaryTimes obj)
        {
            obj.SendFlag = SEND_FLAG;
            obj.UserKey = MODULE_ID;
        }


        public static List<WorkSummary> GetWorkSummaries(DateTime date1, DateTime date2, int seninId = 0, int vesselId = 0)
        {
            var ret = (List<WorkSummary>)null;

            if (vesselId > 0)
            {
                ret = _GetWorkSummaries1(date1, date2, vesselId);
            }
            else if (seninId > 0)
            {
                ret = _GetWorkSummaries2(date1, date2, seninId);
            }
            else
            {
                ret = new List<WorkSummary>();
            }

            return ret;
        }


        public static List<WorkSummary> _GetWorkSummaries1(DateTime date1, DateTime date2, int vesselId)
        {
            List<WorkSummary> ret = new List<WorkSummary>();

            var setting = GetSetting();
            var workingMinutes = 60 * setting.WorkingHours;
            var workRange = setting.WorkRange;

            var workContentList = GetWorkContents();

            var works = GetWorks(date1, date2, vesselId: vesselId);
            var crewNos = works.Select(o => o.CrewNo).Distinct().OrderBy(o => o);

            foreach (var crewNo in crewNos)
            {
                var wks = works.Where(o => o.CrewNo == crewNo.ToString()).OrderBy(o => o.StartWork);

                double workMinutes = 0;
                double workMinute1Weeks = 0;

                Dictionary<DateTime, double> dailyWorking = new Dictionary<DateTime, double>();

                for (DateTime dt = date1; dt <= date2; dt = dt.AddDays(1))
                {
                    var tmp = wks.Where(o => o.StartWork.Date == dt.Date || (o.FinishWork > dt.Date && o.FinishWork.Date == dt.Date)).OrderBy(o => o.StartWork);

                    workMinutes = 0;

                    foreach (Work w in tmp)
                    {
                        if (w.FinishWork == DateTime.MaxValue)
                            continue;

                        foreach (WorkContentDetail wd in w.WorkContentDetails)
                        {
                            var wc = workContentList.Where(o => o.WorkContentID == wd.WorkContentID).FirstOrDefault();
                            if (wc == null || wc.IsIncludeWorkTime == false)
                                continue;

                            DateTime st = wd.WorkDate;
                            DateTime fin = st.AddMinutes(workRange);
                            if (st < w.StartWork)
                                st = w.StartWork;
                            if (fin > w.FinishWork)
                                fin = w.FinishWork;

                            var m = (fin - st).TotalMinutes;

                            if (st.Date == dt.Date)
                            {
                                workMinutes += m;
                            }
                        }
                    }
                    if (dailyWorking.ContainsKey(dt.Date))
                    {
                        dailyWorking[dt.Date] += workMinutes;
                    }
                    else
                    {
                        dailyWorking.Add(dt.Date, workMinutes);
                    }
                }

                // １週間
                workMinute1Weeks = 0;
                for (DateTime wdt = date2.AddDays(-6); wdt <= date2; wdt = wdt.AddDays(1))
                {
                    if (dailyWorking.ContainsKey(wdt))
                    {
                        workMinute1Weeks += dailyWorking[wdt];
                    }
                }

                // ４週間
                double overTimes = 0;
                for (DateTime wdt = date2.AddDays(-27); wdt <= date2; wdt = wdt.AddDays(1))
                {
                    if (dailyWorking.ContainsKey(wdt))
                    {
                        if (dailyWorking[wdt] > workingMinutes)
                        {
                            overTimes += (dailyWorking[wdt] - workingMinutes);
                        }
                    }
                }

                // 休息
                var wksRest = wks.Where(o => o.StartWork.Date == date2.Date || (o.FinishWork > date2.Date && o.FinishWork.Date == date2.Date));
                double restMinutes = LongRestTime(date2, wksRest);



                var workSummary = new WorkSummary();
                workSummary.CrewNo = crewNo;
                workSummary.WorkMinutes = workMinutes;
                workSummary.WorkMinutes1Week = workMinute1Weeks;
                workSummary.OverTimes = overTimes;
                workSummary.RestMinutes = restMinutes;

                ret.Add(workSummary);
            }

            return ret;
        }

        public static List<WorkSummary> _GetWorkSummaries2(DateTime date1, DateTime date2, int seninId)
        {
            List<WorkSummary> ret = new List<WorkSummary>();

            var setting = GetSetting();
            var workingMinutes = 60 * setting.WorkingHours;
            var workRange = setting.WorkRange;

            var workContentList = GetWorkContents();

            var works = GetWorks(date1, date2, seninId: seninId);


            var startDate = date2.AddMonths(-1);

            for (DateTime dt = date1; dt <= date2; dt = dt.AddDays(1))
            {
                var tmp = works.Where(o => o.StartWork.Date == dt.Date || (o.FinishWork > dt.Date && o.FinishWork.Date == dt.Date)).OrderBy(o => o.StartWork);

                double workMinutes = 0;

                foreach (Work w in tmp)
                {
                    if (w.FinishWork == DateTime.MaxValue)
                        continue;

                    foreach (WorkContentDetail wd in w.WorkContentDetails)
                    {
                        var wc = workContentList.Where(o => o.WorkContentID == wd.WorkContentID).FirstOrDefault();
                        if (wc == null || wc.IsIncludeWorkTime == false)
                            continue;

                        DateTime st = wd.WorkDate;
                        DateTime fin = st.AddMinutes(workRange);
                        if (st < w.StartWork)
                            st = w.StartWork;
                        if (fin > w.FinishWork)
                            fin = w.FinishWork;

                        var m = (fin - st).TotalMinutes;

                        if (st.Date == dt.Date)
                        {
                            workMinutes += m;
                        }
                    }
                }

                var workSummary = new WorkSummary();
                workSummary.Date = dt;
                workSummary.WorkMinutes = workMinutes;

                if (dt >= startDate)
                {
                    workSummary.RestMinutes = LongRestTime(dt, tmp);
                }
                ret.Add(workSummary);
            }

            return ret;
        }


        #region private double LongRestTime(DateTime day, IEnumerable<Work> Works)
        private static double LongRestTime(DateTime day, IEnumerable<Work> Works)
        {
            TimeSpan longRestTime = new TimeSpan(0, 0, 0);
            if (Works.Count() != 0)
            {
                for (int i = 0; i <= Works.Count(); i++)
                {
                    if (i == 0)
                    {
                        if (Works.ElementAt(i).StartWork.Date == day.Date)
                        {
                            longRestTime = TimeSpan.Parse(Works.ElementAt(i).StartWork.ToString("HH:mm"));
                        }
                        else
                        {
                            longRestTime = TimeSpan.Parse(day.ToString("HH:mm"));
                        }
                    }
                    else
                    {
                        DateTime workTo = Works.ElementAt(i - 1).FinishWork;
                        if (i == Works.Count())
                        {
                            if (workTo.Day == Works.ElementAt(i - 1).StartWork.Day)
                            {
                                if ((new TimeSpan(24, 0, 0) - TimeSpan.Parse(workTo.ToString("HH:mm"))) > longRestTime)
                                {
                                    longRestTime = new TimeSpan(24, 0, 0) - TimeSpan.Parse(workTo.ToString("HH:mm"));
                                }
                            }
                        }
                        else
                        {
                            DateTime workFrom = Works.ElementAt(i).StartWork;
                            if ((workFrom - workTo) > longRestTime)
                            {
                                longRestTime = workFrom - workTo;
                            }
                        }
                    }
                }
            }

            return longRestTime.TotalMinutes;
        }
        #endregion

    }
}
