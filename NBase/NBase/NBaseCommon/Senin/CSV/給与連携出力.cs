using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseUtil;
using NBaseData.DS;
using NBaseCommon.Senin.Excel.util;
using System.IO;
using System.Text;

namespace NBaseCommon.Senin.Excel
{
    public class 給与連携出力
    {
        public static string 報告区分161 = "161.csv";
        public static string 報告区分171 = "171.csv";


        private readonly string basePath;
        private readonly string outputPath;


        public 給与連携出力(string basePath, string outputPath)
        {
            this.basePath = basePath;
            this.outputPath = outputPath;
        }


        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache, DateTime fromDate, DateTime toDate)
        {
            MsSeninFilter filter = new MsSeninFilter();
            filter.Kubuns.Add(0); // 社員のみ
            filter.OrderByStr = "OrderByMsSiShokumeiIdSeiMei";
            List<MsSenin> seninList = MsSenin.GetRecordsByFilter(loginUser, filter);


            SiCardFilter cardFilter = new SiCardFilter();
            cardFilter.Start = fromDate.AddMonths(-1); // 指定月の１ヵ月前からを取得
            cardFilter.End = toDate;
            foreach(MsSiShubetsu shubetsu in seninTableCache.GetMsSiShubetsuList(loginUser))
            {
                if (seninTableCache.Is_休暇管理(loginUser,shubetsu.MsSiShubetsuID) == false)
                {
                    cardFilter.MsSiShubetsuIDs.Add(shubetsu.MsSiShubetsuID);
                }
            }
            List<SiCard> cardList = SiCard.GetRecordsByFilter(loginUser, cardFilter);

            string ym = fromDate.ToString("yyyyMM");
            List<SiKyuyoTeate> kyuyoTeateList = SiKyuyoTeate.GetRecordsByYearMonth(loginUser, ym);

            if (Directory.Exists(basePath))
            {
                Directory.Delete(basePath, true);
            }
            Directory.CreateDirectory(basePath);

            Make161(loginUser, seninTableCache, fromDate, toDate, seninList, cardList);
            Make171(loginUser, seninTableCache, fromDate, toDate, seninList, kyuyoTeateList);

            CompressFile(basePath);
        }

        public void Make161(MsUser loginUser, SeninTableCache seninTableCache, DateTime fromDate, DateTime toDate, List<MsSenin> seninList, List<SiCard> cardList)
        {
            string filePath = basePath + "\\" + 報告区分161;
            StreamWriter fileSw = new StreamWriter(new FileStream(filePath, FileMode.Create), System.Text.Encoding.GetEncoding("shift_jis"));

            fileSw.WriteLine("氏名,氏名ｺｰﾄﾞ,601, 602, 603, 604,605, 606, 607,608,609,610,611,612,613,614,615,616,617,618,619,620,621");

            foreach (MsSiShokumei s in seninTableCache.GetMsSiShokumeiList(loginUser))
            {
                var sortedList = seninList.Where(obj => obj.RetireFlag == 0 && obj.MsSiShokumeiID == s.MsSiShokumeiID).OrderBy(obj => obj.IntShomeiCode);

                foreach (MsSenin senin in sortedList)
                {
                    StringBuilder strLine = new StringBuilder();

                    // 氏名
                    strLine.Append(senin.Sei + " " + senin.Mei);
                    strLine.Append(",");
                    System.Diagnostics.Debug.WriteLine(senin.Sei + " " + senin.Mei);

                    // 氏名コード
                    strLine.Append(senin.ShimeiCode.Trim());
                    strLine.Append(",");

                    StringBuilder strLine1 = new StringBuilder();
                    StringBuilder strLine2 = new StringBuilder();
                    StringBuilder strLine3 = new StringBuilder();

                    if (cardList.Any(obj => obj.MsSeninID == senin.MsSeninID && seninTableCache.Is_乗船(loginUser, obj.MsSiShubetsuID)))
                    {
                        var boardingCards = cardList.Where(obj => obj.MsSeninID == senin.MsSeninID && seninTableCache.Is_乗船(loginUser, obj.MsSiShubetsuID)).OrderByDescending(obj => obj.StartDate);

                        #region １つ目の船
                        System.Diagnostics.Debug.WriteLine("  １つ目の船");

                        var latestCard = boardingCards.ElementAt(0); // 直近の乗船情報

                        MsVessel vessel = seninTableCache.GetMsVessel(loginUser, latestCard.MsVesselID);

                        // 船No
                        strLine1.Append(vessel.KyuyoRenkeiNo);
                        strLine1.Append(",");

                        // 本給日数
                        #region
                        System.Diagnostics.Debug.WriteLine("  本給日数");
                        System.Diagnostics.Debug.WriteLine("    カード:" + latestCard.StartDate.ToShortDateString() + "-" + latestCard.EndDate.ToShortDateString());
                        int days = 0;
                        DateTime from = fromDate;
                        DateTime to = toDate;
                        if (latestCard.StartDate > from)
                        {
                            from = latestCard.StartDate;
                        }
                        if (latestCard.EndDate != DateTime.MinValue && latestCard.EndDate < to)
                        {
                            to = latestCard.EndDate;
                        }
                        if (from < to)
                        {
                            System.Diagnostics.Debug.WriteLine("      ==> " + from.ToShortDateString() + "-" + to.ToShortDateString());
                            days = to.Day - from.Day + 1;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("      ==> 対象外");
                        }
                        strLine1.Append(days.ToString());
                        strLine1.Append(",");
                        #endregion

                        // 手当日数、乗船日数
                        #region
                        System.Diagnostics.Debug.WriteLine("  手当日数");
                        System.Diagnostics.Debug.WriteLine("    カード:" + latestCard.StartDate.ToShortDateString() + "-" + latestCard.EndDate.ToShortDateString());
                        days = 0;
                        from = fromDate.AddMonths(-1);
                        to = fromDate.AddDays(-1);

                        if (latestCard.StartDate < to)
                        {
                            if (latestCard.StartDate > from)
                            {
                                from = latestCard.StartDate;
                            }
                            if (latestCard.EndDate != DateTime.MinValue && latestCard.EndDate < to)
                            {
                                to = latestCard.EndDate;
                            }
                            if (from < to)
                            {
                                System.Diagnostics.Debug.WriteLine("      ==> " + from.ToShortDateString() + "-" + to.ToShortDateString());
                                days = to.Day - from.Day;
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("      ==> 対象外");
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("      ==> 対象外");
                        }

                        if (to != fromDate.AddDays(-1) && days != 0)
                        {
                            days += 1;
                        }
                        strLine1.Append(days.ToString());
                        strLine1.Append(",");

                        strLine1.Append(days.ToString());
                        strLine1.Append(",");
                        #endregion

                        // 航海日当
                        #region
                        if (to == fromDate.AddDays(-1))
                        {
                            days += 1;
                        }
                        strLine1.Append(days.ToString());
                        strLine1.Append(",");
                        #endregion

                        #endregion

                        #region ２つ目の船

                        if (boardingCards.Count() > 1)
                        {
                            var prevCard = boardingCards.ElementAt(1); // 一つ前の乗船情報

                            vessel = seninTableCache.GetMsVessel(loginUser, prevCard.MsVesselID);
                            System.Diagnostics.Debug.WriteLine("  ２つ目の船");

                            // 船No
                            if (vessel.KyuyoRenkeiNo != null && vessel.KyuyoRenkeiNo.Trim().Length == 3)
                            {
                                strLine3.Append(vessel.KyuyoRenkeiNo.Substring(0, 1));
                                strLine3.Append(",");
                                strLine3.Append(vessel.KyuyoRenkeiNo.Substring(1, 2));
                                strLine3.Append(",");
                            }
                            else
                            {
                                strLine3.Append(",");
                                strLine3.Append(",");
                            }

                            // 本給日数
                            #region
                            System.Diagnostics.Debug.WriteLine("  本給日数");
                            System.Diagnostics.Debug.WriteLine("    カード:" + prevCard.StartDate.ToShortDateString() + "-" + prevCard.EndDate.ToShortDateString());
                            days = 0;
                            from = fromDate;
                            to = toDate;
                            if (prevCard.StartDate > from)
                            {
                                from = prevCard.StartDate;
                            }
                            if (prevCard.EndDate != DateTime.MinValue && prevCard.EndDate < to)
                            {
                                to = prevCard.EndDate;
                            }
                            if (from < to)
                            {
                                System.Diagnostics.Debug.WriteLine("      ==> " + from.ToShortDateString() + "-" + to.ToShortDateString());
                                days = to.Day - from.Day + 1;
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("      ==> 対象外");
                            }
                            strLine3.Append(days.ToString());
                            strLine3.Append(",");
                            #endregion

                            // 手当日数、乗船日数
                            #region
                            System.Diagnostics.Debug.WriteLine("  手当日数");
                            System.Diagnostics.Debug.WriteLine("    カード:" + prevCard.StartDate.ToShortDateString() + "-" + prevCard.EndDate.ToShortDateString());
                            days = 0;
                            from = fromDate.AddMonths(-1);
                            to = fromDate.AddDays(-1);


                            if (prevCard.StartDate < to)
                            {
                                if (prevCard.StartDate > from)
                                {
                                    from = latestCard.StartDate;
                                }
                                if (prevCard.EndDate != DateTime.MinValue && prevCard.EndDate < to)
                                {
                                    to = prevCard.EndDate;
                                }
                                if (from < to)
                                {
                                    System.Diagnostics.Debug.WriteLine("      ==> " + from.ToShortDateString() + "-" + to.ToShortDateString());
                                    days = to.Day - from.Day;
                                }
                                else
                                {
                                    System.Diagnostics.Debug.WriteLine("      ==> 対象外");
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("      ==> 対象外");
                            }
                            if (to != fromDate.AddDays(-1) && days != 0)
                            {
                                days += 1;
                            }
                            strLine3.Append(days.ToString());
                            strLine3.Append(",");

                            strLine3.Append(days.ToString());
                            strLine3.Append(",");
                            #endregion

                            // 航海日当
                            #region
                            if (to == fromDate.AddDays(-1))
                            {
                                days += 1;
                            }
                            strLine3.Append(days.ToString());
                            strLine3.Append(",");
                            #endregion
                        }
                        else
                        {
                            strLine3.Append(",,,,,");
                        }

                        #endregion
                    }
                    else
                    {
                        strLine1.Append(",,,,,");
                        strLine3.Append(",,,,,");
                    }

                    #region 休暇
                    if (cardList.Any(obj => obj.MsSeninID == senin.MsSeninID && seninTableCache.Is_乗船(loginUser, obj.MsSiShubetsuID) == false))
                    {
                        var notBoardingCards = cardList.Where(obj => obj.MsSeninID == senin.MsSeninID && seninTableCache.Is_乗船(loginUser, obj.MsSiShubetsuID) == false);


                        // 船No
                        strLine2.Append("435");
                        strLine2.Append(",");

                        // 本給日数
                        #region
                        System.Diagnostics.Debug.WriteLine("  本給日数");
                        int days = 0;
                        DateTime from = fromDate;
                        DateTime to = toDate;
                        foreach (SiCard c in notBoardingCards)
                        {
                            System.Diagnostics.Debug.WriteLine("    カード:" + c.StartDate.ToShortDateString() + "-" + c.EndDate.ToShortDateString());
                            from = fromDate;
                            to = toDate;

                            if (c.StartDate > from)
                            {
                                from = c.StartDate;
                            }
                            if (c.EndDate != DateTime.MinValue && c.EndDate < to)
                            {
                                to = c.EndDate;
                            }
                            if (from < to)
                            {
                                System.Diagnostics.Debug.WriteLine("      ==> " + from.ToShortDateString() + "-" + to.ToShortDateString());
                                days += (to.Day - from.Day + 1);
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("      ==> 対象外" );
                            }

                        }

                        strLine2.Append(days.ToString());
                        strLine2.Append(",");
                        #endregion

                        // 手当日数、乗船日数
                        #region
                        System.Diagnostics.Debug.WriteLine("  手当日数");
                        days = 0;
                        bool existLastDay = false;
                        foreach (SiCard c in notBoardingCards)
                        {
                            System.Diagnostics.Debug.WriteLine("    カード:" + c.StartDate.ToShortDateString() + "-" + c.EndDate.ToShortDateString());
                            from = fromDate.AddMonths(-1);
                            to = fromDate.AddDays(-1);

                            if (c.StartDate > from)
                            {
                                from = c.StartDate;
                            }
                            if (c.EndDate != DateTime.MinValue && c.EndDate < to)
                            {
                                to = c.EndDate;
                            }
                            if (from < to)
                            {
                                System.Diagnostics.Debug.WriteLine("      ==> " + from.ToShortDateString() + "-" + to.ToShortDateString());
                                days += (to.Day - from.Day + 1);

                                if (to == fromDate.AddDays(-1))
                                {
                                    existLastDay = true;
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("      ==> 対象外");
                            }
                        }
                        if (existLastDay)
                        {
                            days -= 1;
                        }
                        strLine2.Append(days.ToString());
                        strLine2.Append(",");

                        strLine2.Append(days.ToString());
                        strLine2.Append(",");
                        #endregion

                        // 航海日当
                        #region
                        days = 0;
                        strLine2.Append(days.ToString());
                        strLine2.Append(",");
                        #endregion
                    }
                    else
                    {
                        strLine2.Append(",,,,,");
                    }
                    #endregion

                    strLine.Append(strLine1);
                    strLine.Append(",");
                    strLine.Append(strLine2);
                    strLine.Append(",");
                    strLine.Append(strLine3);

                    fileSw.WriteLine(strLine);
                }
            }


            fileSw.Close();
        }
        public void Make171(MsUser loginUser, SeninTableCache seninTableCache, DateTime fromDate, DateTime toDate, List<MsSenin> seninList, List<SiKyuyoTeate> kyuyoTeateList)
        {
            string filePath = basePath + "\\" + 報告区分171;
            StreamWriter fileSw = new StreamWriter(new FileStream(filePath, FileMode.Create), System.Text.Encoding.GetEncoding("shift_jis"));

            var sortedList = seninList.OrderBy(obj => obj.ShimeiCode);

            foreach (MsSenin senin in sortedList)
            {

                StringBuilder strLine = new StringBuilder();

                // 氏名
                strLine.Append(senin.Sei + " " + senin.Mei);
                strLine.Append(",");

                // 氏名コード
                strLine.Append(senin.ShimeiCode.Trim());
                strLine.Append(",");

                if (kyuyoTeateList.Any(obj => obj.MsSeninID == senin.MsSeninID))
                {
                    var kyuyoTeateBySenin = kyuyoTeateList.Where(obj => obj.MsSeninID == senin.MsSeninID);
                    var vesselIds = kyuyoTeateBySenin.Select(obj => obj.MsVesselID).Distinct();

                    MsVessel vessel = seninTableCache.GetMsVessel(loginUser, vesselIds.ElementAt(0));

                    // 701:船No
                    strLine.Append(vessel.KyuyoRenkeiNo);
                    strLine.Append(",");

                    // 702:執職手当
                    strLine.Append(",");

                    // 703:欠員手当Ａ
                    strLine.Append(",");

                    // 704:休暇手当 
                    strLine.Append(",");

                    // 705:その他１（支）
                    int amount = kyuyoTeateBySenin.Where(obj => obj.MsVesselID == vesselIds.ElementAt(0)).Sum(obj => obj.Kingaku);
                    strLine.Append(amount.ToString());
                    strLine.Append(",");

                    // 706:その他１（控）
                    strLine.Append(",");

                    // 711:船No
                    strLine.Append(",");

                    // 712:その他２（支）
                    strLine.Append(",");

                    // 713:その他２（控）
                    strLine.Append(",");

                    if (vesselIds.Count() > 1)
                    {
                        vessel = seninTableCache.GetMsVessel(loginUser, vesselIds.ElementAt(1));

                        // 714:船No
                        strLine.Append(vessel.KyuyoRenkeiNo);
                        strLine.Append(",");

                        // 715:その他３（支）
                        amount = kyuyoTeateBySenin.Where(obj => obj.MsVesselID == vesselIds.ElementAt(1)).Sum(obj => obj.Kingaku);
                        strLine.Append(amount.ToString());
                        strLine.Append(",");

                        // 716:その他３（控）
                        strLine.Append(",");
                    }
                    else
                    {
                        // 714:船No
                        strLine.Append(",");

                        // 715:その他３（支）
                        strLine.Append(",");

                        // 716:その他３（控）
                        strLine.Append(",");
                    }
                }
                else
                {
                    // 701:船No
                    strLine.Append(",");

                    // 702:執職手当
                    strLine.Append(",");

                    // 703:欠員手当Ａ
                    strLine.Append(",");

                    // 704:休暇手当 
                    strLine.Append(",");

                    // 705:その他１（支）
                    strLine.Append(",");

                    // 706:その他１（控）
                    strLine.Append(",");

                    // 711:船No
                    strLine.Append(",");

                    // 712:その他２（支）
                    strLine.Append(",");

                    // 713:その他２（控）
                    strLine.Append(",");
                    // 714:船No
                    strLine.Append(",");

                    // 715:その他３（支）
                    strLine.Append(",");

                    // 716:その他３（控）
                    strLine.Append(",");
                }

                fileSw.WriteLine(strLine);
            }

            fileSw.Close();
        }         
        
        public void CompressFile(string folder)
        {
            try
            {
                // 圧縮する
                using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile(System.Text.Encoding.GetEncoding("shift_jis")))
                {
                    //フォルダを追加する
                    zip.AddDirectory(folder);

                    //ZIP書庫を作成する
                    zip.Save(outputPath);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("ex=" + ex.Message);
            }
        }
    }
}
