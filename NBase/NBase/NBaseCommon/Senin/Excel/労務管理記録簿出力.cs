#define ORG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseUtil;
using NBaseData.DS;
using NBaseData.BLC;
using System.Drawing;
using WtmData;
using WtmModelBase;

namespace NBaseCommon.Senin.Excel
{
    public class 労務管理記録簿出力
    {
        public static int KIND_管理者用 = 0;
        public static int KIND_船員用 = 1;


        private readonly string templateFilePath;
        private readonly string outputFilePath;


        private int minuteRange = 15;

        private SiLaborManagementRecordBook laborManagementRecordBook;
        private List<SiRequiredNumberOfDays> requiredNumberOfDaysList;
        //private List<SiNightSetting> nightSettingList;

        private List<WorkContent> WorkContents = null;
        private Dictionary<string, string> dspNoDic;
        private Dictionary<string, Color> bgColorDic;
        private Dictionary<string, Color> fgColorDic;

        private TimeSpan OverWorkBaseTime = new TimeSpan(8, 0, 0); //時間外労働の基準時間、時間は8時間固定とする


        private int WorkContentRow = 16; // 作業の種類を出力する行
        private int DetailStartRow = 24; // 勤務詳細の開始行
        private int RowsPerDay = 5; // 1日のデータを設定するのに使用する行の数(作業内容表示用、夜間時間表示用、労働時間設定用、安全臨時労働時間設定用、労働時間が連続したときに入力する時分用

        private int WorkDetailStartColNo = 6; // 作業詳細の開始カラム
        private int LongRestColNo = 132; // 分割時の長いほうの休憩時間のカラム


        private int Boarding_ID = 0;
        //private int HolidayA_ID = 0;
        //private int HolidayB_ID = 0;


        public 労務管理記録簿出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }


        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache, WtmAccessor wtmAccessor, int vesselId, int msSeninID, DateTime fromDate, DateTime toDate)
        {
            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                int xlsRet = xls.OpenBook(outputFilePath, templateFilePath);
                if (xlsRet < 0)
                {
                    Exception xlsEx = null;
                    if (xls.ErrorNo == ExcelCreator.ErrorNo.TempCreate)
                    {
                        xlsEx = new Exception("ディスクに十分な空き領域がありません。");
                    }
                    else
                    {
                        xlsEx = new Exception("サーバでエラーが発生しました。");
                    }
                    throw xlsEx;
                }
                xls.FullCalcOnLoad = true;

                _CreateFile(loginUser, seninTableCache, wtmAccessor, xls, vesselId, msSeninID, fromDate, toDate);

                xls.CloseBook(true);
            }
        }

        private void _CreateFile(MsUser loginUser, SeninTableCache seninTableCache, WtmAccessor wtmAccessor, ExcelCreator.Xlsx.XlsxCreator xlsx, int vesselID, int msSeninID, DateTime fromDate, DateTime toDate)
        {
            dspNoDic = new Dictionary<string, string>();
            bgColorDic = new Dictionary<string, Color>();
            fgColorDic = new Dictionary<string, Color>();

            WorkContents = wtmAccessor.GetWorkContents();
            WorkContents.ForEach(o => {
                dspNoDic.Add(o.WorkContentID, o.DspName);
                bgColorDic.Add(o.WorkContentID, ColorTranslator.FromHtml(o.BgColor));
                fgColorDic.Add(o.WorkContentID, ColorTranslator.FromHtml(o.FgColor));
            });

            // 作業種類の入力
            int colStart = 12;
            int colNo = colStart;
            foreach (WorkContent wc in WorkContents)
            {
                xlsx.Cell(ExcelUtils.ToCellName(colNo, WorkContentRow) + ":" + ExcelUtils.ToCellName(colNo + 3, WorkContentRow)).Attr.MergeCells = true;
                xlsx.Cell(ExcelUtils.ToCellName(colNo, WorkContentRow)).Value = wc.DspName;
                xlsx.Cell(ExcelUtils.ToCellName(colNo, WorkContentRow)).Attr.HorizontalAlignment = ExcelCreator.HorizontalAlignment.Center;
                xlsx.Cell(ExcelUtils.ToCellName(colNo, WorkContentRow)).Attr.VerticalAlignment = ExcelCreator.VerticalAlignment.Center;
                xlsx.Cell(ExcelUtils.ToCellName(colNo, WorkContentRow)).Attr.BackColor = bgColorDic[wc.WorkContentID];
                xlsx.Cell(ExcelUtils.ToCellName(colNo, WorkContentRow)).Attr.FontColor = fgColorDic[wc.WorkContentID];

                colNo += 4;

                xlsx.Cell(ExcelUtils.ToCellName(colNo, WorkContentRow) + ":" + ExcelUtils.ToCellName(colNo + 4, WorkContentRow)).Attr.MergeCells = true;
                xlsx.Cell(ExcelUtils.ToCellName(colNo, WorkContentRow)).Value = wc.Name;
                xlsx.Cell(ExcelUtils.ToCellName(colNo, WorkContentRow)).Attr.HorizontalAlignment = ExcelCreator.HorizontalAlignment.Left;
                xlsx.Cell(ExcelUtils.ToCellName(colNo, WorkContentRow)).Attr.VerticalAlignment = ExcelCreator.VerticalAlignment.Center;

                colNo += 5;
            }


            // 不要な行を削除
            int days = (toDate - fromDate).Days + 1;

            int ry = (DetailStartRow + (days * RowsPerDay));
            xlsx.RowDelete(ry, (366 - days) * RowsPerDay);


            // 時間外労働の基準時間を設定
            xlsx.Cell("A27").Value = OverWorkBaseTime.TotalDays;


            // 勤怠管理情報
            laborManagementRecordBook = SiLaborManagementRecordBook.GetRecord(loginUser);
            requiredNumberOfDaysList = SiRequiredNumberOfDays.GetRecords(loginUser);
            //nightSettingList = SiNightSetting.GetRecords(loginUser);

            if (laborManagementRecordBook != null)
            {
                xlsx.Cell("**時間外労働協定の有無").Value = laborManagementRecordBook.OvertimeWorkAgreement == 1 ? "有" : "無";
                xlsx.Cell("**補償休日労働協定の有無").Value = laborManagementRecordBook.CompensationHolidayLaborAgreement == 1 ? "有" : "無";
                xlsx.Cell("**休息時間分割協定の有無").Value = laborManagementRecordBook.BreakTimeDivisionAgreement == 1 ? "有" : "無";
                xlsx.Cell("**基準労働期間").Value = laborManagementRecordBook.StandardWorkingPeriod;

                if (laborManagementRecordBook.StartStandardWorkingPeriod != DateTime.MinValue)
                    xlsx.Cell("**起算日").Value = laborManagementRecordBook.StartStandardWorkingPeriod.ToString("yyyy年MM月dd日");
                if (laborManagementRecordBook.LastStandardWorkingPeriod != DateTime.MinValue)
                    xlsx.Cell("**末日").Value = laborManagementRecordBook.LastStandardWorkingPeriod.ToString("yyyy年MM月dd日");
            }

            int rowNo = 0;
            int margeStartRow = DetailStartRow;

            for (int i = 0; i < days; i++)
            {
                DateTime day = DateTimeUtils.ToFrom(fromDate.AddDays(i));

                // 月日を入力
                xlsx.Cell(ExcelUtils.ToCellName(4, (DetailStartRow + (i * RowsPerDay)))).Value = day.ToString("M/d");
            }

            List<MsSenin> senins = new List<MsSenin>();
            List<SiCard> cards = new List<SiCard>();
            Boarding_ID = seninTableCache.GetMsSiShubetsu(loginUser, (int)MsSiShubetsu.SiShubetsu.乗船).MsSiShubetsuID;
            //HolidayA_ID = seninTableCache.GetMsSiShubetsu(loginUser, (int)MsSiShubetsu.SiShubetsu.陸上休暇A).MsSiShubetsuID;
            //HolidayB_ID = seninTableCache.GetMsSiShubetsu(loginUser, (int)MsSiShubetsu.SiShubetsu.陸上休暇B).MsSiShubetsuID;

            if (msSeninID != 0)
            {
                var senin = MsSenin.GetRecord(loginUser, msSeninID);
                senins.Add(senin);


                var cardFilter = new SiCardFilter();
                cardFilter.MsSeninID = msSeninID;
                cardFilter.Start = fromDate;
                cardFilter.End = toDate;
                cardFilter.MsSiShubetsuIDs.Add(Boarding_ID);
                //cardFilter.MsSiShubetsuIDs.Add(HolidayA_ID);
                //cardFilter.MsSiShubetsuIDs.Add(HolidayB_ID);

                cards = SiCard.GetRecordsByFilter(loginUser, cardFilter);
            }
            else if (vesselID != 0)
            {

                var cardFilter = new SiCardFilter();
                cardFilter.MsSeninIDs.AddRange(senins.Select(o => o.MsSeninID));
                cardFilter.Start = fromDate;
                cardFilter.End = toDate;
                cardFilter.MsSiShubetsuIDs.Add(Boarding_ID);
                //cardFilter.MsSiShubetsuIDs.Add(HolidayA_ID);
                //cardFilter.MsSiShubetsuIDs.Add(HolidayB_ID);

                cards = SiCard.GetRecordsByFilter(loginUser, cardFilter);

                var seninFilter = new MsSeninFilter();
                seninFilter.OrderByStr = "OrderByMsSiShokumeiIdSeiMei";

                var allSenins = MsSenin.GetRecordsByFilter(loginUser, seninFilter);

                //var sortedShokumeiList = SeninTableCache.instance().GetMsSiShokumeiList(loginUser).OrderBy(o => o.ShowOrder);
                var sortedShokumeiList = seninTableCache.GetMsSiShokumeiList(loginUser).OrderBy(o => o.ShowOrder);
                foreach (MsSiShokumei shokumei in sortedShokumeiList)
                {
                    var targetCards = cards.Where(o => o.MsVesselID == vesselID && o.SiLinkShokumeiCards[0].MsSiShokumeiID == shokumei.MsSiShokumeiID);
                    var onCrewId = targetCards.Select(o => o.MsSeninID).Distinct();

                    var onCrews = allSenins.Where(o => onCrewId.Contains(o.MsSeninID));
                    if (onCrews != null)
                    {
                        foreach (MsSenin senin in onCrews)
                        {
                            if (senins.Any(o => o.MsSeninID == senin.MsSeninID) == false)
                            {
                                senins.Add(senin);
                            }
                        }
                    }
                }

            }
            else
            {
                var seninFilter = new MsSeninFilter();
                seninFilter.OrderByStr = "OrderByMsSiShokumeiIdSeiMei";


                senins = MsSenin.GetRecordsByFilter(loginUser, seninFilter);

                var cardFilter = new SiCardFilter();
                cardFilter.MsSeninIDs.AddRange(senins.Select(o => o.MsSeninID));
                cardFilter.Start = fromDate;
                cardFilter.End = toDate;
                cardFilter.MsSiShubetsuIDs.Add(Boarding_ID);
                //cardFilter.MsSiShubetsuIDs.Add(HolidayA_ID);
                //cardFilter.MsSiShubetsuIDs.Add(HolidayB_ID);

                cards = SiCard.GetRecordsByFilter(loginUser, cardFilter);
            }

            // 船の動静は、開始日～終了日
            var vesselMovements = wtmAccessor.GetVesselMovementDispRecord(fromDate, toDate, 0);

            // シートのコピー
            {
                int i = 1;
                foreach (MsSenin senin in senins)
                {
                    xlsx.CopySheet(0, i, senin.FullName);
                    i++;
                }
                xlsx.DeleteSheet(0, 1);
            }

            foreach (MsSenin senin in senins)
            {
                var seninCards = (List<SiCard>)null;
                if (cards.Any(o => o.MsSeninID == senin.MsSeninID))
                    seninCards = cards.Where(o => o.MsSeninID == senin.MsSeninID).ToList();


                // 作業詳細取得
                var seninWorks = wtmAccessor.GetWorks(fromDate.AddDays(-2), toDate, seninId: senin.MsSeninID);
                {

                    //=======================================================================
                    var deleteList = new List<string>();
                    seninWorks = seninWorks.OrderBy(o => o.StartWork).ThenBy(o => o.FinishWork).ToList();
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
                    foreach (var item in deleteList)
                    {
                        seninWorks.RemoveAll(o => o.WorkID.Equals(item));
                    }
                    //=======================================================================
                }


                try
                {
                    _WriteSheet(loginUser, seninTableCache, xlsx, fromDate, toDate, senin, seninCards, seninWorks, vesselMovements);
                }
                catch (Exception ex)
                {
                    var mes = ex.Message;
                }
            }
        }





        private void _WriteSheet(MsUser loginUser, SeninTableCache seninTableCache, ExcelCreator.Xlsx.XlsxCreator xlsx, DateTime fromDate, DateTime toDate, MsSenin senin, List<SiCard> cards, List<Work> works, List<VesselMovement> vesselMovements)
        {
            int rowNo = 0;
            var boardings = new List<SiCard>();
            var holidays = new List<SiCard>();
            if (cards != null)
            {
                boardings = cards.Where(o => o.MsSiShubetsuID == Boarding_ID).OrderBy(o => o.StartDate).ToList();
                //holidays = cards.Where(o => o.MsSiShubetsuID == HolidayA_ID || o.MsSiShubetsuID == HolidayB_ID).ToList();
            }



            //シートの選択
            int sheetno = xlsx.SheetNo2(senin.FullName);
            xlsx.SheetNo = sheetno;

            // 船員の名前を入力
            xlsx.Cell("**氏名").Value = senin.FullName;

            // 配乗情報、船名、役職名を入力
            rowNo = 7;
            foreach (SiCard c in boardings)
            {
                string SignOnOff = c.StartDate.ToString("yyyy/MM/dd(ddd)") + "-";
                if (c.EndDate != DateTime.MinValue)
                {
                    SignOnOff += c.EndDate.ToString("yyyy/MM/dd(ddd)");
                }
                xlsx.Cell(ExcelUtils.ToCellName(4, rowNo)).Value = SignOnOff;
                xlsx.Cell(ExcelUtils.ToCellName(25, rowNo)).Value = c.VesselName;
                xlsx.Cell(ExcelUtils.ToCellName(45, rowNo)).Value = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, c.SiLinkShokumeiCards[0].MsSiShokumeiID);

                rowNo++;
            }

            //// 休日データを設定
            //if (kind == KIND_管理者用 && requiredNumberOfDaysList != null)
            //{
            //    var requiredNumberOfDays = requiredNumberOfDaysList.Where(o => o.MsSeninCompanyID == senin.MsSeninCompanyID && o.Kind == 0).FirstOrDefault();
            //    if (requiredNumberOfDays != null)
            //    {
            //        xlsx.Cell("**陸上休暇必要日数").Value = requiredNumberOfDays.Days;
            //    }

            //    requiredNumberOfDays = requiredNumberOfDaysList.Where(o => o.MsSeninCompanyID == senin.MsSeninCompanyID && o.Kind == 1).FirstOrDefault();
            //    if (requiredNumberOfDays != null)
            //    {
            //        xlsx.Cell("**陸上休暇A必要日数").Value = requiredNumberOfDays.Days;
            //    }
            //}

            int days = (toDate - fromDate).Days + 1;
            var reportInfo = CreateReportData(fromDate, days, works);


            rowNo = DetailStartRow;
            for (int i = 0; i < days; i ++)
            {
                DateTime day = DateTimeUtils.ToFrom(fromDate.AddDays(i));



                var card = boardings.Where(o => DateTimeUtils.ToFrom(o.StartDate) <= day && (o.EndDate == DateTime.MinValue || DateTimeUtils.ToTo(o.EndDate) >= day)).FirstOrDefault();
                if (card != null)
                {
                    var vm = vesselMovements.Where(o => o.VesselID == card.MsVesselID.ToString() && o.DateInfo == day.ToString("yyyyMMdd")).FirstOrDefault();
                    //if (vm != null && vm.Anchorages != null)
                    //{
                    //    int max = vm.Anchorages.Count;
                    //    if (max > 2)
                    //        max = 2;
                    //    for (int j = 0; j < max; j++)
                    //    {
                    //        xlsx.Cell(ExcelUtils.ToCellName(2, rowNo + j)).Value = vm.Anchorages[j].Start;
                    //        xlsx.Cell(ExcelUtils.ToCellName(3, rowNo + j)).Value = vm.Anchorages[j].Finish;
                    //    }
                    //}
                    if (vm != null)
                    {
                        xlsx.Cell(ExcelUtils.ToCellName(2, rowNo + 0)).Value = vm.AnchorageS1;
                        xlsx.Cell(ExcelUtils.ToCellName(3, rowNo + 0)).Value = vm.AnchorageF1;
                        xlsx.Cell(ExcelUtils.ToCellName(2, rowNo + 1)).Value = vm.AnchorageS2;
                        xlsx.Cell(ExcelUtils.ToCellName(3, rowNo + 1)).Value = vm.AnchorageF2;

                    }
                }


                // 作業内容と作業時間を入力
                if (reportInfo.Works.Any(o => DateTimeUtils.ToFrom(o.WorkDate) == day))
                {
                    var dw = reportInfo.Works.Where(o => DateTimeUtils.ToFrom(o.WorkDate) == day).FirstOrDefault();


                    foreach (WorkInfo info in dw.WorkInfos)
                    {
                        DateTime workFrom = info.Details.First().WorkDate;
                        DateTime workTo = info.Details.Last().WorkDate;

                        var colNo = WorkDetailStartColNo + (workFrom.Hour * 4) + (workFrom.Minute / minuteRange);
                        var colEndNo = WorkDetailStartColNo + (workTo.Hour * 4) + (workTo.Minute / minuteRange);


                        if (workFrom != info.WorkFrom)
                            workFrom = info.WorkFrom;


                        if (workTo != info.WorkTo)
                            workTo = info.WorkTo;
                        else
                            workTo = workTo.AddMinutes(minuteRange);

                        int hourFrom = int.Parse(workFrom.ToString("HH"));
                        int minuteFrom = int.Parse(workFrom.ToString("mm"));
                        int hourTo = int.Parse(workTo.ToString("HH"));
                        int minuteTo = int.Parse(workTo.ToString("mm"));
                        int diffHour = hourTo - hourFrom;
                        int diffMinute = minuteTo - minuteFrom;
                        int minuteOffset;
                        TimeSpan workTime;
                        workTime = workTo - workFrom;


                        if (diffMinute == -45)
                        {
                            diffHour--;
                            minuteOffset = 1;
                        }
                        else if (diffMinute == -30)
                        {
                            diffHour--;
                            minuteOffset = 2;
                        }
                        else if (diffMinute == -15)
                        {
                            diffHour--;
                            minuteOffset = 3;
                        }
                        else if (diffMinute == 14)
                        {
                            //workTime = workTime.Add(new TimeSpan(0, 1, 0));
                            minuteOffset = 1;
                        }
                        else if (diffMinute == 15)
                        {
                            minuteOffset = 1;
                        }
                        else if (diffMinute == 29)
                        {
                            //workTime = workTime.Add(new TimeSpan(0, 1, 0));
                            minuteOffset = 2;
                        }
                        else if (diffMinute == 30)
                        {
                            minuteOffset = 2;
                        }
                        else if (diffMinute == 44)
                        {
                            //workTime = workTime.Add(new TimeSpan(0, 1, 0));
                            minuteOffset = 3;
                        }
                        else if (diffMinute == 45)
                        {
                            minuteOffset = 3;
                        }
                        else if (diffMinute == 59)
                        {
                            diffHour++;
                            //workTime = workTime.Add(new TimeSpan(0, 1, 0));
                            minuteOffset = 0;
                        }
                        else
                        {
                            minuteOffset = 0;
                        }

                        xlsx.Cell(ExcelUtils.ToCellName(colNo, rowNo + 4)).Value = minuteFrom.ToString("00");




                        // 作業内容の色を塗る
                        xlsx.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = dspNoDic[info.WorkContentID];
                        xlsx.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Attr.BackColor = bgColorDic[info.WorkContentID];
                        xlsx.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Attr.FontColor = fgColorDic[info.WorkContentID];
                        xlsx.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Attr.HorizontalAlignment = ExcelCreator.HorizontalAlignment.Center;
                        xlsx.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Attr.VerticalAlignment = ExcelCreator.VerticalAlignment.Center;

                        var workContent = WorkContents.Where(o => o.WorkContentID == info.WorkContentID).FirstOrDefault();
                        if (workContent.IsIncludeWorkTime)
                        {
                            // 労働時間をシリアル値で入力
                            xlsx.Cell(ExcelUtils.ToCellName(colNo, rowNo + 2)).Value = workTime.TotalDays;
                        }
                        else if (workContent.IsSafetyTemporaryLabor)
                        {
                            TimeSpan timeSpan = workTime;

                            // 安全臨時労働時間をシリアル値で入力
                            xlsx.Cell(ExcelUtils.ToCellName(colNo, rowNo + 3)).Value = timeSpan.TotalDays;
                        }
                        else
                        {
                            xlsx.Cell(ExcelUtils.ToCellName(colNo, rowNo + 3)).Value = workTime.TotalDays;
                        }

                        if (xlsx.Cell(ExcelUtils.ToCellName(colNo - 1, rowNo + 4)).Value != null)
                        {
                            xlsx.Cell(ExcelUtils.ToCellName(colNo - 1, rowNo + 4)).Clear();
                        }

                        //if (hourTo == 23 && minuteTo == 59)
                        //{
                        //    xlsx.Cell(ExcelUtils.ToCellName(colEndNo, rowNo + 4)).Value = "00";
                        //}
                        //else
                        //{
                            xlsx.Cell(ExcelUtils.ToCellName(colEndNo, rowNo + 4)).Value = minuteTo.ToString("00");
                        //}
                        xlsx.Cell(ExcelUtils.ToCellName(colEndNo, rowNo + 4)).Attr.HorizontalAlignment = ExcelCreator.HorizontalAlignment.Right;

                        // セルの結合
                        xlsx.Cell(ExcelUtils.ToCellName(colNo, rowNo) + ":" + ExcelUtils.ToCellName(colEndNo, rowNo)).Attr.MergeCells = true;
                    }


                    foreach (WorkInfo info in dw.NightTimeInfos)
                    {
                        DateTime workFrom = info.Details.First().WorkDate;
                        DateTime workTo = info.Details.Last().WorkDate;

                        var colNo = WorkDetailStartColNo + (workFrom.Hour * 4) + (workFrom.Minute / minuteRange);
                        var colEndNo = WorkDetailStartColNo + (workTo.Hour * 4) + (workTo.Minute / minuteRange);

                        // 作業内容の色を塗る
                        xlsx.Cell(ExcelUtils.ToCellName(colNo, rowNo + 1)).Value = dspNoDic[info.WorkContentID];
                        xlsx.Cell(ExcelUtils.ToCellName(colNo, rowNo + 1)).Attr.BackColor = bgColorDic[info.WorkContentID];
                        xlsx.Cell(ExcelUtils.ToCellName(colNo, rowNo + 1)).Attr.FontColor = fgColorDic[info.WorkContentID];
                        xlsx.Cell(ExcelUtils.ToCellName(colNo, rowNo + 1)).Attr.HorizontalAlignment = ExcelCreator.HorizontalAlignment.Center;
                        xlsx.Cell(ExcelUtils.ToCellName(colNo, rowNo + 1)).Attr.VerticalAlignment = ExcelCreator.VerticalAlignment.Center;

                        // セルの結合
                        xlsx.Cell(ExcelUtils.ToCellName(colNo, rowNo + 1) + ":" + ExcelUtils.ToCellName(colEndNo, rowNo + 1)).Attr.MergeCells = true;
                    }

                    // 分割時の長いほうの時間数                 
                    xlsx.Cell(ExcelUtils.ToCellName(LongRestColNo, rowNo)).Value = LongRestTime(dw.WorkInfos);

                }

                // 労働時間設定用、安全臨時労働時間設定用の行は非表示にする
                xlsx.Cell(ExcelUtils.ToCellName(0, rowNo + 2)).RowHeight = 0;
                xlsx.Cell(ExcelUtils.ToCellName(0, rowNo + 3)).RowHeight = 0;

                rowNo += RowsPerDay;
            }

        }

        private double LongRestTime(List<WorkInfo> workTimeList)
        {
            TimeSpan longRestTime = new TimeSpan(0, 0, 0);
            for (int i = 0; i <= workTimeList.Count; i++)
            {
                if (i == 0)
                {
                    longRestTime = TimeSpan.Parse(workTimeList[i].Details.First().WorkDate.ToString("HH:mm"));
                }
                else
                {
                    DateTime workTo = workTimeList[i - 1].Details.Last().WorkDate;
                    if (workTo == workTimeList[i - 1].WorkTo)
                        workTo = workTo.AddMinutes(minuteRange);
                    else
                        workTo = workTimeList[i - 1].WorkTo;

                    if (workTo.Day != workTimeList[i - 1].Details.First().WorkDate.Day)
                        workTo = workTo.AddMinutes(-1);

                    if (i == workTimeList.Count)
                    {
                        if ((new TimeSpan(24, 0, 0) - TimeSpan.Parse(workTo.ToString("HH:mm"))) > longRestTime)
                        {
                            longRestTime = new TimeSpan(24, 0, 0) - TimeSpan.Parse(workTo.ToString("HH:mm"));
                        }
                    }
                    else 
                    {
                        DateTime workFrom = workTimeList[i].Details.First().WorkDate;
                        if ((workFrom - workTo) > longRestTime)
                        {
                            longRestTime = workFrom - workTo;
                        }
                    }
                }
            }
            return longRestTime.TotalDays;
        }




        private ReportInfo CreateReportData(DateTime fromDate, int days, List<Work> works)
        {
            ReportInfo repInfo = new ReportInfo
            {
                Works = new List<DaysWork>()
                //WorkTimeOfDay = setting.Deviation.ToString() + ":00"
            };

            if (works == null)
                return repInfo;

            //Crew毎のWorkデータ
            List<DaysWork> repworks = new List<DaysWork>();

            for (int i = 0; i <= days; i++)
            {
                //日毎のWorkデータ
                DaysWork daysWork = new DaysWork
                {
                    WorkDate = DateTimeUtils.ToFrom(fromDate.AddDays(i)),
                    WorkInfos = new List<WorkInfo>(),
                    NightTimeInfos = new List<WorkInfo>()
                };


                //repWorkに追加するworkInfo
                var addWorkInfo = new WorkInfo();
                // repWorkに追加するNightTimeInfo
                var addNightTimeInfo = new WorkInfo();

                var sorted = works.Where(o => o.IsDelete == false && (DateTimeUtils.ToFrom(o.StartWork) == daysWork.WorkDate || DateTimeUtils.ToFrom(o.FinishWork) == daysWork.WorkDate)).OrderBy(o => o.StartWork);
                foreach (var targetWork in sorted)
                {
                    daysWork.VesselID = targetWork.VesselID;

                    var sortedList = targetWork.WorkContentDetails.OrderBy(o => o.WorkDate);

                    foreach (var workContentDetail in sortedList)
                    {
                        if (daysWork.WorkDate <= workContentDetail.WorkDate && workContentDetail.WorkDate <= daysWork.WorkDate.AddDays(1).AddSeconds(-1))
                        {
                            if (addWorkInfo.WorkContentID == null)
                            {
                                //addWorkInfo.WorkFrom = targetWork.StartWork;
                                
                                if (targetWork.StartWork.Day == workContentDetail.WorkDate.Day)
                                {
                                    addWorkInfo.WorkFrom = targetWork.StartWork;
                                }
                                else
                                {
                                    addWorkInfo.WorkFrom = workContentDetail.WorkDate;
                                }
                                addWorkInfo.WorkContentID = workContentDetail.WorkContentID;
                                addWorkInfo.Details = new List<WorkContentDetail>();
                                addWorkInfo.Details.Add(workContentDetail);
                            }
                            else if (addWorkInfo.WorkContentID != workContentDetail.WorkContentID || addWorkInfo.Details.Last().WorkDate.AddMinutes(minuteRange) != workContentDetail.WorkDate)
                            {
                                addWorkInfo.WorkTo = addWorkInfo.Details.Last().WorkDate;
                                daysWork.WorkInfos.Add(addWorkInfo);


                                //WorkContentが変わった為WorkInfoをリセット
                                addWorkInfo = new WorkInfo();
                                addWorkInfo.WorkContentID = workContentDetail.WorkContentID;
                                addWorkInfo.Details = new List<WorkContentDetail>();
                                addWorkInfo.Details.Add(workContentDetail);

                                addWorkInfo.WorkFrom = workContentDetail.WorkDate;
                            }
                            else
                            {
                                addWorkInfo.Details.Add(workContentDetail);
                            }


                            // 夜間設定に関するデータの作成
                            if (workContentDetail.NightTime == true)
                            {
                                // 夜間設定かつ、WorkContentが未設定(データがない)なら、新規登録
                                if (addNightTimeInfo.WorkContentID == null)
                                {
                                    addNightTimeInfo.WorkContentID = workContentDetail.WorkContentID;
                                    addNightTimeInfo.Details = new List<WorkContentDetail>();
                                    addNightTimeInfo.Details.Add(workContentDetail);
                                }
                                else if (addNightTimeInfo.WorkContentID != workContentDetail.WorkContentID || addNightTimeInfo.Details.Last().WorkDate.AddMinutes(minuteRange) != workContentDetail.WorkDate)
                                {
                                    addNightTimeInfo.WorkTo = addNightTimeInfo.Details.Last().WorkDate;
                                    daysWork.NightTimeInfos.Add(addNightTimeInfo);

                                    //WorkContentが変わった為WorkInfoをリセット
                                    addNightTimeInfo = new WorkInfo();
                                    addNightTimeInfo.WorkContentID = workContentDetail.WorkContentID;
                                    addNightTimeInfo.Details = new List<WorkContentDetail>();
                                    addNightTimeInfo.Details.Add(workContentDetail);
                                }
                                else
                                {
                                    addNightTimeInfo.Details.Add(workContentDetail);
                                }
                            }
                            else
                            {
                                // 夜間設定でないかつ、WorkContentが存在するなら、NightTimeInfoをリセット
                                if (addNightTimeInfo.WorkContentID != null)
                                {
                                    addNightTimeInfo.WorkTo = addNightTimeInfo.Details.Last().WorkDate;
                                    daysWork.NightTimeInfos.Add(addNightTimeInfo);

                                    addNightTimeInfo = new WorkInfo();
                                }
                            }

                        }
                    }

                    if (addWorkInfo.WorkContentID != null)
                    {
                        //最後のタスクの終了時間は退勤時間を入力
                        if (daysWork.WorkDate.AddDays(1) <= targetWork.FinishWork)
                        {
                            //addWorkInfo.WorkTo = daysWork.WorkDate.AddDays(1).AddMinutes(-1);
                            addWorkInfo.WorkTo = daysWork.WorkDate.AddDays(1);
                        }
                        else
                        {
                            addWorkInfo.WorkTo = targetWork.FinishWork;
                        }

                        daysWork.WorkInfos.Add(addWorkInfo);
                    }

                    addWorkInfo = new WorkInfo();


                    // 夜間設定はデータが新規かどうかを確認してから終了時間を設定する
                    if (addNightTimeInfo.WorkContentID != null)
                    {
                        //最後のタスクの終了時間は退勤時間を入力
                        if (daysWork.WorkDate.AddDays(1) <= targetWork.FinishWork)
                        {
                            //addNightTimeInfo.WorkTo = daysWork.WorkDate.AddDays(1).AddMinutes(-1);
                            addNightTimeInfo.WorkTo = daysWork.WorkDate.AddDays(1);
                        }
                        else
                        {
                            addNightTimeInfo.WorkTo = targetWork.FinishWork;
                        }

                        daysWork.NightTimeInfos.Add(addNightTimeInfo);
                    }

                    addNightTimeInfo = new WorkInfo();
                }

                if (daysWork.WorkInfos.Count != 0)
                {
                    repInfo.Works.Add(daysWork);
                }
            }

            return repInfo;
        }







        public class ReportInfo
        {
            /// <summary>
            /// 表示月
            /// </summary>
            public DateTime DisplayMonth { get; set; }

            /// <summary>
            /// 船員
            /// </summary>
            public string CrewName { get; set; }

            /// <summary>
            /// 作業内容1か月
            /// </summary>
            public List<DaysWork> Works { get; set; }

            /// <summary>
            /// 1日の労働時間
            /// </summary>
            public string WorkTimeOfDay { get; set; }

            /// <summary>
            /// 労働期間、休日データ
            /// </summary>
            //public LaborManagementInfo LaborManagement { get; set; }
        }

        public class DaysWork
        {
            public DateTime WorkDate { get; set; }

            public string VesselID { get; set; }

            /// <summary>
            /// 作業内容(1日)
            /// </summary>
            public List<WorkInfo> WorkInfos { get; set; }

            /// <summary>
            /// 夜間時間内容(1日)
            /// </summary>
            public List<WorkInfo> NightTimeInfos { get; set; }
        }

        public class WorkInfo
        {
            public string WorkContentID { get; set; }

            public List<WorkContentDetail> Details { get; set; }

            public DateTime WorkFrom { get; set; }

            public DateTime WorkTo { get; set; }
        }
    }
}
