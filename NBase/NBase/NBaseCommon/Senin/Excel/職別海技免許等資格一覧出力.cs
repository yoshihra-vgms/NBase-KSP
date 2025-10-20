using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseUtil;
using NBaseData.DS;
using NBaseCommon.Senin.Excel.util;

namespace NBaseCommon.Senin.Excel
{
    public class 職別海技免許等資格一覧出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;


        private Dictionary<string, int> rowIndexDic;



        public 職別海技免許等資格一覧出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }


        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache, int msSiShokumeiId, int msSeninId)
        {
            rowIndexDic = new Dictionary<string, int>();
            
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

                Dictionary<int, string> shokumeiGroupDic = ShokumeiGroup.GetShokumeiGroupDic職別海技免許等資格一覧用(loginUser, seninTableCache);

                MsSeninFilter filter = new MsSeninFilter();
                filter.Kubuns.Add(0); // 社員のみ
                filter.職別海技免許等資格一覧対象 = true;
                filter.joinSiCard = MsSeninFilter.JoinSiCard.LEFT_JOIN;
                List<MsSenin> seninList = MsSenin.GetRecordsByFilter(loginUser, filter);
                if (msSeninId > 0)
                {
                    seninList = seninList.Where(obj => obj.MsSeninID == msSeninId).ToList();
                    msSiShokumeiId = seninList.First().MsSiShokumeiID;
                }

                List<SiRireki> rirekiList = SiRireki.GetRecords(loginUser);
                List<SiMenjou> menjouList = SiMenjou.GetRecords(loginUser);
                List<SiExperienceCargo> experienceCargoList = SiExperienceCargo.GetRecords(loginUser);
                List<SiExperienceForeign> experienceForeinList = SiExperienceForeign.GetRecords(loginUser);

                SiCardFilter cardFilter = new SiCardFilter();
                cardFilter.KenmTushincyo = true;
                List<SiCard> cardList = SiCard.GetRecordsByFilter(loginUser, cardFilter);

                SiKoushuFilter koushuFilter = new SiKoushuFilter();
                koushuFilter.Is受講済み = true;
                koushuFilter.Is退職者を除く = true;
                List<SiKoushu> koushuList = SiKoushu.GetRecordsByFilter(loginUser, koushuFilter);

                // 各シートの行インデックスを初期化
                foreach(string shokumeiGroup in shokumeiGroupDic.Values)
                {
                    rowIndexDic[shokumeiGroup] = 0;
                }


                List<int> sheetNos = new List<int>();
                foreach (MsSiShokumei s in seninTableCache.GetMsSiShokumeiList(loginUser))
                {
                    if (msSiShokumeiId > 0 && s.MsSiShokumeiID != msSiShokumeiId)
                    {
                        continue;
                    }

                    int sheetNo = GetSheetNo(xls, shokumeiGroupDic[s.MsSiShokumeiID]);
                    if (sheetNos.Contains(sheetNo) == false)
                    {
                        sheetNos.Add(sheetNo);
                    }

                    xls.SheetNo = sheetNo;
                    xls.Cell("**DATE").Value = DateTime.Today;


                    var seninListByShokumei = seninList.Where(obj => obj.MsSiShokumeiID == s.MsSiShokumeiID && obj.RetireFlag != 1).OrderBy(obj => obj.Birthday);

                    _CreateFile(loginUser, xls, seninTableCache, shokumeiGroupDic[s.MsSiShokumeiID], seninListByShokumei.ToList(), 
                                rirekiList, menjouList, experienceCargoList, experienceForeinList, cardList, koushuList);
                }

                if (msSiShokumeiId > 0)
                {
                    // 対象職以外のシートを削除する
                    int maxNo = xls.SheetCount;
                    for (int i = maxNo; i >= 0; i --)
                    {
                        if (sheetNos.Contains(i) == false)
                            xls.DeleteSheet(i, 1);
                    }
                }

                xls.CloseBook(true);
            }
        }
        private int GetSheetNo(ExcelCreator.Xlsx.XlsxCreator xls, string sheetName)
        {
            int sheetNo = 0;

            for(int i = 0; i < xls.SheetCount; i ++)
            {
                if (xls.SheetName == sheetName)
                {
                    sheetNo = i;
                }

                xls.SheetNo = i + 1;
            }

            return sheetNo;
        }

        private void _CreateFile(MsUser loginUser, ExcelCreator.Xlsx.XlsxCreator xls, SeninTableCache seninTableCache, string groupName, 
                                 List<MsSenin> seninList, 
                                 List<SiRireki> rirekiList, 
                                 List<SiMenjou> menjouList, 
                                 List<SiExperienceCargo> experienceCargoList, 
                                 List<SiExperienceForeign> experienceForeinList, 
                                 List<SiCard> cardList,
                                 List<SiKoushu> koushuList)
        {
            int startRow = 6;
            int rowOffset = rowIndexDic[groupName];

            foreach(MsSenin senin in seninList)
            {
                // 職名
                xls.Cell("B" + (startRow + rowOffset)).Value = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, senin.MsSiShokumeiID);

                // 氏名
                xls.Cell("C" + (startRow + rowOffset)).Value = senin.Sei + " " + senin.Mei;

                // 年齢
                TimeSpan ts = DateTime.Now - senin.Birthday;
                double age = ts.TotalDays / 365;
                xls.Cell("D" + (startRow + rowOffset)).Value = NumberUtils.ToRoundDown(age, 1);

                // 在籍
                #region
                if (senin.NyuushaDate != DateTime.MinValue)
                {
                    ts = DateTime.Now - senin.NyuushaDate;
                    double zaiseki = ts.TotalDays / 365;
                    xls.Cell("E" + (startRow + rowOffset)).Value = NumberUtils.ToRoundDown(zaiseki, 1);
                }
                #endregion

                // 現職歴
                #region
                var rirekiBySenin = rirekiList.Where(obj => obj.MsSeninID == senin.MsSeninID && obj.MsSiShokumeiID == senin.MsSiShokumeiID).OrderBy(obj => obj.RirekiDate);
                if (rirekiBySenin.Count() != 0)
                {
                    ts = DateTime.Now - rirekiBySenin.First().RirekiDate;
                    double genshokureki = ts.TotalDays / 365;
                    xls.Cell("F" + (startRow + rowOffset)).Value = NumberUtils.ToRoundDown(genshokureki, 1);
                }
                #endregion


                var menjouBySenin = menjouList.Where(obj => obj.MsSeninID == senin.MsSeninID && obj.WrittenTest != 1);

                // 海技免状
                #region
                if (groupName == "船長" || groupName == "一航士" || groupName == "二航士" || groupName == "通信長")
                {
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 1)) // 一級海技士（航海）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 1).First();
                        xls.Cell("G" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 2)) // 二級海技士（航海）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 2).First();
                        xls.Cell("H" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 3)) // 三級海技士（航海）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 3).First();
                        xls.Cell("I" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 4)) // 四級海技士（航海）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 4).First();
                        xls.Cell("J" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 5)) // 五級海技士（航海）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 5).First();
                        xls.Cell("K" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 6)) // 六級海技士（航海）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 6).First();
                        xls.Cell("L" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                    }
                }
                if (groupName == "機関長" || groupName == "一機士" || groupName == "二機士")
                {
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 7)) // 一級海技士（機関）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 7).First();
                        xls.Cell("G" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 8)) // 二級海技士（機関）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 8).First();
                        xls.Cell("H" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 9)) // 三級海技士（機関）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 9).First();
                        xls.Cell("I" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 10)) // 四級海技士（機関）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 10).First();
                        xls.Cell("J" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 11)) // 五級海技士（機関）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 11).First();
                        xls.Cell("K" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 12)) // 六級海技士（機関）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 12).First();
                        xls.Cell("L" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                    }
                }

                if (groupName == "司厨長・手・員")
                {
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouID == 10)) // 船舶料理士
                    {
                        xls.Cell("G" + (startRow + rowOffset)).Value = "○";
                    }

                }

                if (groupName == "甲板長・手・員")
                {
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 1)) // 一級海技士（航海）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 1).First();
                        xls.Cell("G" + (startRow + rowOffset)).Value = "1級";
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 2)) // 二級海技士（航海）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 2).First();
                        xls.Cell("G" + (startRow + rowOffset)).Value = "2級";
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 3)) // 三級海技士（航海）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 3).First();
                        xls.Cell("G" + (startRow + rowOffset)).Value = "3級";
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 4)) // 四級海技士（航海）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 4).First();
                        xls.Cell("G" + (startRow + rowOffset)).Value = "4級";
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 5)) // 五級海技士（航海）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 5).First();
                        xls.Cell("G" + (startRow + rowOffset)).Value = "5級";
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 6)) // 六級海技士（航海）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 6).First();
                        xls.Cell("G" + (startRow + rowOffset)).Value = "6級";
                    }
                }
                if (groupName == "操機長・手・員")
                {
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 7)) // 一級海技士（機関）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 7).First();
                        xls.Cell("G" + (startRow + rowOffset)).Value = "1級";
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 8)) // 二級海技士（機関）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 8).First();
                        xls.Cell("G" + (startRow + rowOffset)).Value = "2級";
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 9)) // 三級海技士（機関）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 9).First();
                        xls.Cell("G" + (startRow + rowOffset)).Value = "3級";
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 10)) // 四級海技士（機関）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 10).First();
                        xls.Cell("G" + (startRow + rowOffset)).Value = "4級";
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 11)) // 五級海技士（機関）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 11).First();
                        xls.Cell("G" + (startRow + rowOffset)).Value = "5級";
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 12)) // 六級海技士（機関）
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 12).First();
                        xls.Cell("G" + (startRow + rowOffset)).Value = "6級";
                    }
                }
                #endregion

                // 無線
                #region
                if (groupName == "船長" || groupName == "一航士" || groupName == "二航士" || groupName == "通信長")
                {
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 17)) // 一級海上無線通信士
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 17).First();
                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("N" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("N" + (startRow + rowOffset)).Value = "○";
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 18)) // 二級海上無線通信士
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 18).First();
                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("O" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("O" + (startRow + rowOffset)).Value = "○";
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 19)) // 三級海上無線通信士
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 19).First();
                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("P" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("P" + (startRow + rowOffset)).Value = "○";
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 20)) // 四級海上無線通信士
                    {
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 21)) // 一級海上特殊無線技士
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 21).First();
                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("Q" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("Q" + (startRow + rowOffset)).Value = "○";
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 22)) // 二級海上特殊無線技士
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 22).First();
                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("R" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("R" + (startRow + rowOffset)).Value = "○";
                    }
                }
                #endregion

                // 危険物（甲）
                #region
                if (groupName == "船長" || groupName == "一航士" || groupName == "二航士" || groupName == "通信長")
                {
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 29)) // ガス
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 29).First();
                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("T" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("T" + (startRow + rowOffset)).Value = "○";
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 30)) // ケミ
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 30).First();
                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("U" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("U" + (startRow + rowOffset)).Value = "○";
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 28)) // オイ
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 28).First();
                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("V" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("V" + (startRow + rowOffset)).Value = "○";
                    }


                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 32)) // 石油・ガス
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 32).First();
                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("V" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("V" + (startRow + rowOffset)).Value = "○";

                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("T" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("T" + (startRow + rowOffset)).Value = "○";
                    }

                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 33)) // 石油・科学
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 33).First();
                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("V" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("V" + (startRow + rowOffset)).Value = "○";

                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("U" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("U" + (startRow + rowOffset)).Value = "○";
                    }

                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 34)) // ガス・科学
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 34).First();
                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("T" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("T" + (startRow + rowOffset)).Value = "○";

                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("U" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("U" + (startRow + rowOffset)).Value = "○";
                    }

                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 35)) // 石油・科学・ガス
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 35).First();
                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("T" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("T" + (startRow + rowOffset)).Value = "○";

                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("U" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("U" + (startRow + rowOffset)).Value = "○";

                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("V" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("V" + (startRow + rowOffset)).Value = "○";
                    }
                }

                if (groupName == "機関長" || groupName == "一機士" || groupName == "二機士")
                {
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 29)) // ガス
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 29).First();
                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("N" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("N" + (startRow + rowOffset)).Value = "○";
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 30)) // ケミ
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 30).First();
                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("O" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("O" + (startRow + rowOffset)).Value = "○";
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 28)) // オイ
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 28).First();
                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("P" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("P" + (startRow + rowOffset)).Value = "○";
                    }


                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 32)) // 石油・ガス
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 32).First();
                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("P" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("P" + (startRow + rowOffset)).Value = "○";

                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("N" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("N" + (startRow + rowOffset)).Value = "○";
                    }

                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 33)) // 石油・科学
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 33).First();
                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("P" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("P" + (startRow + rowOffset)).Value = "○";

                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("O" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("O" + (startRow + rowOffset)).Value = "○";
                    }

                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 34)) // ガス・科学
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 34).First();
                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("N" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("N" + (startRow + rowOffset)).Value = "○";

                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("O" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("O" + (startRow + rowOffset)).Value = "○";
                    }

                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 35)) // 石油・科学・ガス
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 35).First();
                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("N" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("N" + (startRow + rowOffset)).Value = "○";

                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("O" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("O" + (startRow + rowOffset)).Value = "○";

                        if (menjou.Kigen != DateTime.MinValue)
                            xls.Cell("P" + (startRow + rowOffset)).Value = menjou.Kigen.ToShortDateString();
                        else
                            xls.Cell("P" + (startRow + rowOffset)).Value = "○";
                    }
                }
                #endregion

                // SSO
                #region
                if (groupName == "船長" || groupName == "一航士" || groupName == "二航士")
                {
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouID == 11)) // 船舶保安管理者（SSO)
                    {
                        xls.Cell("W" + (startRow + rowOffset)).Value = "○";
                    }
                }
                #endregion

                // 川崎航海実歴
                #region
                if (groupName == "船長")
                {
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouID == 7)) // 川崎航海実歴
                    {
                        xls.Cell("X" + (startRow + rowOffset)).Value = "○";
                    }

                }
                #endregion



                var cargoBySenin = experienceCargoList.Where(obj => obj.MsSeninID == senin.MsSeninID);
                var foreinBySenin = experienceForeinList.Where(obj => obj.MsSeninID == senin.MsSeninID);

                // 積荷経験
                #region
                if (groupName == "船長")
                {
                    if (cargoBySenin.Any(obj => obj.MsCargoGroupID == 3)) // LEG
                    {
                        xls.Cell("Y" + (startRow + rowOffset)).Value = "○";
                    }
                    if (cargoBySenin.Any(obj => obj.MsCargoGroupID == 5)) // 硫黄
                    {
                        xls.Cell("Z" + (startRow + rowOffset)).Value = "○";
                    }
                    if (cargoBySenin.Any(obj => obj.MsCargoGroupID == 2)) // LNG
                    {
                        xls.Cell("AC" + (startRow + rowOffset)).Value = "○";
                    }
                    if (cargoBySenin.Any(obj => obj.MsCargoGroupID == 1)) // LPG
                    {
                        xls.Cell("AD" + (startRow + rowOffset)).Value = "○";
                    }
                    if (cargoBySenin.Any(obj => obj.MsCargoGroupID == 4)) // C5
                    {
                        xls.Cell("AE" + (startRow + rowOffset)).Value = "○";
                    }
                }
                if (groupName == "一航士" || groupName == "二航士")
                {
                    if (cargoBySenin.Any(obj => obj.MsCargoGroupID == 3)) // LEG
                    {
                        xls.Cell("X" + (startRow + rowOffset)).Value = "○";
                    }
                    if (cargoBySenin.Any(obj => obj.MsCargoGroupID == 5)) // 硫黄
                    {
                        xls.Cell("Y" + (startRow + rowOffset)).Value = "○";
                    }
                    if (cargoBySenin.Any(obj => obj.MsCargoGroupID == 2)) // LNG
                    {
                        xls.Cell("AA" + (startRow + rowOffset)).Value = "○";
                    }
                    if (cargoBySenin.Any(obj => obj.MsCargoGroupID == 1)) // LPG
                    {
                        xls.Cell("AB" + (startRow + rowOffset)).Value = "○";
                    }
                    if (cargoBySenin.Any(obj => obj.MsCargoGroupID == 4)) // C5
                    {
                        xls.Cell("AC" + (startRow + rowOffset)).Value = "○";
                    }
                }
                if (groupName == "機関長" || groupName == "一機士" || groupName == "二機士")
                {
                    if (cargoBySenin.Any(obj => obj.MsCargoGroupID == 3)) // LEG
                    {
                        xls.Cell("Q" + (startRow + rowOffset)).Value = "○";
                    }
                    if (cargoBySenin.Any(obj => obj.MsCargoGroupID == 5)) // 硫黄
                    {
                        xls.Cell("R" + (startRow + rowOffset)).Value = "○";
                    }
                    if (cargoBySenin.Any(obj => obj.MsCargoGroupID == 2)) // LNG
                    {
                        xls.Cell("T" + (startRow + rowOffset)).Value = "○";
                    }
                    if (cargoBySenin.Any(obj => obj.MsCargoGroupID == 1)) // LPG
                    {
                        xls.Cell("U" + (startRow + rowOffset)).Value = "○";
                    }
                    if (cargoBySenin.Any(obj => obj.MsCargoGroupID == 4)) // C5
                    {
                        xls.Cell("V" + (startRow + rowOffset)).Value = "○";
                    }
                }
                #endregion

                // 内外
                #region
                if (groupName == "一航士" || groupName == "二航士")
                {
                    if (foreinBySenin.Any(obj => obj.C5_Flag == 0))  // C5以外で外航経験あり
                    {
                        xls.Cell("Z" + (startRow + rowOffset)).Value = "○";
                    }
                    if (foreinBySenin.Any(obj => obj.C5_Flag == 1)) // C5で外航経験あり
                    {
                        xls.Cell("AD" + (startRow + rowOffset)).Value = "○";
                    }
                }
                if (groupName == "機関長" || groupName == "一機士" || groupName == "二機士")
                {
                    if (foreinBySenin.Any(obj => obj.C5_Flag == 0))  // C5以外で外航経験あり
                    {
                        xls.Cell("S" + (startRow + rowOffset)).Value = "○";
                    }
                    if (foreinBySenin.Any(obj => obj.C5_Flag == 1)) // C5で外航経験あり
                    {
                        xls.Cell("W" + (startRow + rowOffset)).Value = "○";
                    }
                }
                #endregion


                var cardBySenin = cardList.Where(obj => obj.MsSeninID == senin.MsSeninID);

                // CR要・不要
                #region
                if (groupName == "船長")
                {
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 19)) // 三級海上無線通信士 を持っていることが条件
                    {
                        if (cardBySenin.Count() > 0)
                            xls.Cell("AB" + (startRow + rowOffset)).Value = "○";
                        else
                            xls.Cell("AA" + (startRow + rowOffset)).Value = "○";
                    }
                }
                #endregion

                var koushuBySenin = koushuList.Where(obj => obj.MsSeninID == senin.MsSeninID);

                // BRM
                #region
                if (groupName == "船長")
                {
                    if (koushuBySenin.Any(obj => obj.MsSiKoushuID == 125))  // ＢＲＭ訓練（3日間コース）
                    {
                        xls.Cell("AF" + (startRow + rowOffset)).Value = "○";
                    }
                    if (koushuBySenin.Any(obj => obj.MsSiKoushuID == 123))  // ＢＲＭ訓練（2日間コース）
                    {
                        xls.Cell("AG" + (startRow + rowOffset)).Value = "○";
                    }
                } 
                if (groupName == "一航士" || groupName == "二航士")
                {
                    if (koushuBySenin.Any(obj => obj.MsSiKoushuID == 125))  // ＢＲＭ訓練（3日間コース）
                    {
                        xls.Cell("AE" + (startRow + rowOffset)).Value = "○";
                    }
                    if (koushuBySenin.Any(obj => obj.MsSiKoushuID == 123))  // ＢＲＭ訓練（2日間コース）
                    {
                        xls.Cell("AF" + (startRow + rowOffset)).Value = "○";
                    }
                }
                #endregion


                var writtenTestBySenin = menjouList.Where(obj => obj.MsSeninID == senin.MsSeninID && obj.WrittenTest == 1);

                // 筆記
                #region
                //if (groupName == "船長")
                //{
                //    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 6)) // 六級海技士（航海）
                //    {
                //        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 6).First();
                //        xls.Cell("AH" + (startRow + rowOffset)).Value = "6級";
                //        xls.Cell("AI" + (startRow + rowOffset)).Value = menjou.No;
                //    }
                //    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 5)) // 五級海技士（航海）
                //    {
                //        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 5).First();
                //        xls.Cell("AH" + (startRow + rowOffset)).Value = "5級";
                //        xls.Cell("AI" + (startRow + rowOffset)).Value = menjou.No;
                //    }
                //    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 4)) // 四級海技士（航海）
                //    {
                //        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 4).First();
                //        xls.Cell("AH" + (startRow + rowOffset)).Value = "4級";
                //        xls.Cell("AI" + (startRow + rowOffset)).Value = menjou.No;
                //    }
                //    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 3)) // 三級海技士（航海）
                //    {
                //        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 3).First();
                //        xls.Cell("AH" + (startRow + rowOffset)).Value = "3級";
                //        xls.Cell("AI" + (startRow + rowOffset)).Value = menjou.No;
                //    }
                //    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 2)) // 二級海技士（航海）
                //    {
                //        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 2).First();
                //        xls.Cell("AH" + (startRow + rowOffset)).Value = "2級";
                //        xls.Cell("AI" + (startRow + rowOffset)).Value = menjou.No;
                //    }
                //    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 1)) // 一級海技士（航海）
                //    {
                //        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 1).First();
                //        xls.Cell("AH" + (startRow + rowOffset)).Value = "1級";
                //        xls.Cell("AI" + (startRow + rowOffset)).Value = menjou.No;
                //    }
                //}
                if (groupName == "一航士" || groupName == "二航士")
                {
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 6)) // 六級海技士（航海）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 6).First();
                        xls.Cell("AG" + (startRow + rowOffset)).Value = "6級";
                        xls.Cell("AH" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 5)) // 五級海技士（航海）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 5).First();
                        xls.Cell("AG" + (startRow + rowOffset)).Value = "5級";
                        xls.Cell("AH" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 4)) // 四級海技士（航海）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 4).First();
                        xls.Cell("AG" + (startRow + rowOffset)).Value = "4級";
                        xls.Cell("AH" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 3)) // 三級海技士（航海）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 3).First();
                        xls.Cell("AG" + (startRow + rowOffset)).Value = "3級";
                        xls.Cell("AH" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 2)) // 二級海技士（航海）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 2).First();
                        xls.Cell("AG" + (startRow + rowOffset)).Value = "2級";
                        xls.Cell("AH" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 1)) // 一級海技士（航海）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 1).First();
                        xls.Cell("AG" + (startRow + rowOffset)).Value = "1級";
                        xls.Cell("AH" + (startRow + rowOffset)).Value = menjou.No;
                    }
                }
                if (groupName == "機関長" || groupName == "一機士" || groupName == "二機士")
                {
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 12)) // 六級海技士（機関）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 12).First();
                        xls.Cell("X" + (startRow + rowOffset)).Value = "6級";
                        xls.Cell("Y" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 11)) // 五級海技士（機関）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 11).First();
                        xls.Cell("X" + (startRow + rowOffset)).Value = "5級";
                        xls.Cell("Y" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 10)) // 四級海技士（機関）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 10).First();
                        xls.Cell("X" + (startRow + rowOffset)).Value = "4級";
                        xls.Cell("Y" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 9)) // 三級海技士（機関）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 9).First();
                        xls.Cell("X" + (startRow + rowOffset)).Value = "3級";
                        xls.Cell("Y" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 8)) // 二級海技士（機関）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 8).First();
                        xls.Cell("X" + (startRow + rowOffset)).Value = "2級";
                        xls.Cell("Y" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 7)) // 一級海技士（機関）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 7).First();
                        xls.Cell("X" + (startRow + rowOffset)).Value = "1級";
                        xls.Cell("Y" + (startRow + rowOffset)).Value = menjou.No;
                    }

                }

                if (groupName == "甲板長・手・員")
                {
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 6)) // 六級海技士（航海）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 6).First();
                        xls.Cell("K" + (startRow + rowOffset)).Value = "6級";
                        xls.Cell("L" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 5)) // 五級海技士（航海）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 5).First();
                        xls.Cell("K" + (startRow + rowOffset)).Value = "5級";
                        xls.Cell("L" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 4)) // 四級海技士（航海）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 4).First();
                        xls.Cell("K" + (startRow + rowOffset)).Value = "4級";
                        xls.Cell("L" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 3)) // 三級海技士（航海）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 3).First();
                        xls.Cell("K" + (startRow + rowOffset)).Value = "3級";
                        xls.Cell("L" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 2)) // 二級海技士（航海）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 2).First();
                        xls.Cell("K" + (startRow + rowOffset)).Value = "2級";
                        xls.Cell("L" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 1)) // 一級海技士（航海）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 1).First();
                        xls.Cell("I" + (startRow + rowOffset)).Value = "1級";
                        xls.Cell("L" + (startRow + rowOffset)).Value = menjou.No;
                    }
                }
                if (groupName == "操機長・手・員")
                {
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 12)) // 六級海技士（機関）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 12).First();
                        xls.Cell("K" + (startRow + rowOffset)).Value = "6級";
                        xls.Cell("L" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 11)) // 五級海技士（機関）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 11).First();
                        xls.Cell("K" + (startRow + rowOffset)).Value = "5級";
                        xls.Cell("L" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 10)) // 四級海技士（機関）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 10).First();
                        xls.Cell("K" + (startRow + rowOffset)).Value = "4級";
                        xls.Cell("L" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 9)) // 三級海技士（機関）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 9).First();
                        xls.Cell("K" + (startRow + rowOffset)).Value = "3級";
                        xls.Cell("L" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 8)) // 二級海技士（機関）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 8).First();
                        xls.Cell("K" + (startRow + rowOffset)).Value = "2級";
                        xls.Cell("L" + (startRow + rowOffset)).Value = menjou.No;
                    }
                    if (writtenTestBySenin.Any(obj => obj.MsSiMenjouKindID == 7)) // 一級海技士（機関）
                    {
                        var menjou = writtenTestBySenin.Where(obj => obj.MsSiMenjouKindID == 7).First();
                        xls.Cell("K" + (startRow + rowOffset)).Value = "1級";
                        xls.Cell("L" + (startRow + rowOffset)).Value = menjou.No;
                    }
                }
                #endregion

                // 海特
                #region
                if (groupName == "甲板長・手・員" || groupName == "操機長・手・員")
                {
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 22)) // 第二級海上特殊無線技士
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 22).First();
                        xls.Cell("I" + (startRow + rowOffset)).Value = "2海特";
                    }
                    if (menjouBySenin.Any(obj => obj.MsSiMenjouKindID == 21)) // 第一級海上特殊無線技士
                    {
                        var menjou = menjouBySenin.Where(obj => obj.MsSiMenjouKindID == 21).First();
                        xls.Cell("I" + (startRow + rowOffset)).Value = "1海特";
                    }
                }
                #endregion

                // 最終学歴・前職
                #region
                if (groupName == "甲板長・手・員" || groupName == "操機長・手・員")
                {
                    xls.Cell("M" + (startRow + rowOffset)).Value = senin.Gakureki;
                    xls.Cell("R" + (startRow + rowOffset)).Value = senin.Zenreki;
                }
                #endregion

                // 背景色
                #region
                if (senin.Sex == 1 || senin.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.陸上勤務))
                {
                    string cell = "C" + (startRow + rowOffset) + ":";
                    if (groupName == "船長")
                    {
                        cell += "AG" + (startRow + rowOffset);
                    }
                    if (groupName == "一航士" || groupName == "二航士")
                    {
                        cell += "AH" + (startRow + rowOffset);
                    }
                    if (groupName == "機関長" || groupName == "一機士" || groupName == "二機士")
                    {
                        cell += "Y" + (startRow + rowOffset);
                    }
                    if (groupName == "通信長")
                    {
                        cell += "V" + (startRow + rowOffset);
                    }
                    if (groupName == "甲板長・手・員" || groupName == "操機長・手・員")
                    {
                        cell += "V" + (startRow + rowOffset);
                    }
                    if (groupName == "司厨長・手・員")
                    {
                        cell += "H" + (startRow + rowOffset);
                    }

                    // 陸上勤務優先で背景色を付ける
                    if (senin.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.陸上勤務))
                    {
                        xls.Cell(cell).Attr.BackColor = System.Drawing.Color.FromArgb(204,204,255);  // アイスブルー
                    }
                    else if (senin.Sex == 1)
                    {
                        xls.Cell(cell).Attr.BackColor = System.Drawing.Color.FromArgb(255, 204, 153);// ベージュ
                    }
                }
                #endregion

                rowOffset++;
            }

            rowIndexDic[groupName] = rowOffset;
        }
    }
}
