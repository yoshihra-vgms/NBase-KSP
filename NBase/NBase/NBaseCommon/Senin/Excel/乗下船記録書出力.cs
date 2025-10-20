using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseUtil;
using NBaseData.DS;
using NBaseData.BLC;

namespace NBaseCommon.Senin.Excel
{
    public class 乗下船記録書出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;


        public 乗下船記録書出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }


        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache, DateTime date)
        {
            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                //-----------------------
                //2013/12/18 コメントアウト m.y
                //xls.OpenBook(outputFilePath, templateFilePath);              
                //-----------------------
                //2013/12/18 変更:OpenBookエラーをなげる m.y
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
                //-----------------------

                Dictionary<int, 休暇消化状況出力.休暇Object> cardDic = CreateCardDic(loginUser, seninTableCache, date);

                List<MsSenin> senins = 船員.BLC_船員検索_帳票(loginUser, date);

                // 2010.06.01：　年度の帳票なので、年度外を除外するための判定変数を準備
                DateTime 年度開始日 = DateTimeUtils.年度開始日(date);
                DateTime 年度終了日 = DateTimeUtils.年度終了日(date);

                int startRow = 7;

                for (int sheetNo = 0; sheetNo < 5; sheetNo++)
                {
                    xls.SheetNo = sheetNo;

                    xls.RowInsert(startRow - 1, senins.Count);

                    string hCenter = "&\"ＭＳ Ｐゴシック\"&14" + xls.SheetName + "表（" + date.ToString("yyyy") + "年度）";
                    string hRight = "&\"ＭＳ Ｐゴシック\"&11日付：" + DateTime.Now.ToShortDateString();
                    xls.Header("", hCenter, hRight);

                    #region 1～5シート目.
                    // 最大件数.
                    int maxCount = 0;
                    switch (sheetNo)
                    {
                        case 0:
                        case 4:
                            maxCount = 20;
                            break;
                        case 1:
                        case 2:
                            maxCount = 3;
                            break;
                        case 3:
                            maxCount = 15;
                            break;
                    }

                    int i = 0;
                    foreach (MsSenin s in senins)
                    {
                        // 行ヘッダ.
                        // No, 職名, 船員名.
                        xls.Cell("A" + (startRow + i)).Value = (i + 1).ToString();
                        xls.Cell("B" + (startRow + i)).Value = seninTableCache.GetMsSiShokumeiName(loginUser, s.MsSiShokumeiID);
                        xls.Cell("C" + (startRow + i)).Value = s.Sei + " " + s.Mei;

                        // 縦罫線
                        xls.Cell("A" + (startRow + i)).Attr.LineLeft ( ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
                        xls.Cell("A" + (startRow + i)).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                        xls.Cell("B" + (startRow + i)).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                        xls.Cell("C" + (startRow + i)).Attr.LineRight ( ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

                        if (!cardDic.ContainsKey(s.MsSeninID))
                        {
                            continue;
                        }

                        List<SiCard> cards = null;

                        switch (sheetNo)
                        {
                            case 0:
                                cards = cardDic[s.MsSeninID].cards_乗船;
                                break;
                            case 1:
                                cards = cardDic[s.MsSeninID].cards_傷病;
                                break;
                            case 2:
                                cards = cardDic[s.MsSeninID].cards_陸上勤務;
                                break;
                            case 3:
                                cards = cardDic[s.MsSeninID].cards_旅行日;
                                break;
                            case 4:
                                cards = cardDic[s.MsSeninID].cards_その他;
                                break;
                        }

                        for (int k = 0, col = 3; k < maxCount; k++, col += 4)
                        {
                            if (k < cards.Count)
                            {
                                SiCard c = cards[k];

                                switch (sheetNo)
                                {
                                    case 0:
                                        xls.Cell(ExcelUtils.ToCellName(col, startRow + i - 1)).Value = seninTableCache.GetMsVesselName(loginUser, c.MsVesselID);
                                        break;
                                    case 1:
                                    case 2:
                                    case 3:
                                    case 4:
                                        xls.Cell(ExcelUtils.ToCellName(col, startRow + i - 1)).Value = seninTableCache.GetMsSiShubetsuName(loginUser, c.MsSiShubetsuID);
                                        break;
                                }
                                
                                // 2010.06.01 年度の帳票なので、年度外を除外するため置き換えをする
                                if (c.StartDate < 年度開始日)
                                {
                                    c.StartDate = 年度開始日;
                                }
                                if (c.EndDate > 年度終了日)
                                {
                                    c.EndDate = 年度終了日;
                                }

                                xls.Cell(ExcelUtils.ToCellName(col + 1, startRow + i - 1)).Value = c.StartDate.ToString("yy/MM/dd"); // 開始日

                                if (c.EndDate == DateTime.MinValue || (DateTimeUtils.ToFrom(c.StartDate) <= DateTime.Now && DateTime.Now < DateTimeUtils.ToTo(c.EndDate)))
                                {
                                    xls.Cell(ExcelUtils.ToCellName(col + 3, startRow + i - 1)).Value = StringUtils.ToStr(c.StartDate, DateTime.Now); // 日数
                                }
                                else
                                {
                                    xls.Cell(ExcelUtils.ToCellName(col + 2, startRow + i - 1)).Value = c.EndDate.ToString("yy/MM/dd"); // 終了日
                                    xls.Cell(ExcelUtils.ToCellName(col + 3, startRow + i - 1)).Value = StringUtils.ToStr(c.StartDate, c.EndDate); // 日数
                                }
                            }

                            // 縦罫線
                            xls.Cell(ExcelUtils.ToCellName(col, startRow + i - 1)).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                            xls.Cell(ExcelUtils.ToCellName(col + 2, startRow + i - 1)).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                            xls.Cell(ExcelUtils.ToCellName(col + 3, startRow + i - 1)).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                        }

                        // 合計
                        xls.Cell(ExcelUtils.ToCellName(3 + 4 * maxCount, startRow + i - 1)).Value = CreateCellValue_合計(startRow + i - 1, maxCount);

                        // 横罫線
                        xls.Cell("A" + (startRow + i) + ":" + ExcelUtils.ToCellName(3 + 4 * maxCount, startRow + i - 1)).Attr.LineBottom (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                        i++;
                    }

                    // 総合計.
                    xls.Cell("**TOTAL").Value = "=SUM(" + ExcelUtils.ToCellName(3 + 4 * maxCount, startRow - 1) +
                        ":" + ExcelUtils.ToCellName(3 + 4 * maxCount, startRow + i - 2) + ")";
                    xls.Cell("**SUB_TOTAL").Value = "=SUBTOTAL(109," + ExcelUtils.ToCellName(3 + 4 * maxCount, startRow - 1) +
                        ":" + ExcelUtils.ToCellName(3 + 4 * maxCount, startRow + i - 2) + ")";

                    // 縦罫線
                    xls.Cell(ExcelUtils.ToCellName(3 + 4 * maxCount, startRow - 1) + ":" + ExcelUtils.ToCellName(3 + 4 * maxCount, startRow + i)).Attr.LineRight ( ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
                    #endregion
                }

                xls.SheetNo = 0;
                xls.CloseBook(true);
            }
        }


        private string CreateCellValue_合計(int row, int maxCount)
        {
            string str = "=";

            for (int i = 0, col = 6; i < maxCount; i++, col += 4)
            {
                if (i > 0)
                {
                    str += "+";
                }

                str += ExcelUtils.ToCellName(col, row);
            }

            return str;
        }


        private Dictionary<int, 休暇消化状況出力.休暇Object> CreateCardDic(MsUser loginUser, SeninTableCache seninTableCache, DateTime date)
        {
            // Dictionary<MsSeninId, 休暇Object>
            Dictionary<int, 休暇消化状況出力.休暇Object> cardDic = new Dictionary<int, 休暇消化状況出力.休暇Object>();

            SiCardFilter filter = new SiCardFilter();

            filter.Start = DateTimeUtils.年度開始日(date);
            filter.End = DateTimeUtils.年度終了日(date);

            filter.OrderByStr = "OrderByStartDate";

            List<SiCard> cards = SiCard.GetRecordsByFilter(loginUser, filter);
            foreach (SiCard c in cards)
            {
                if (!cardDic.ContainsKey(c.MsSeninID))
                {
                    cardDic[c.MsSeninID] = new 休暇消化状況出力.休暇Object();
                }

                if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船) ||
                    c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.艤装員))
                {
                    cardDic[c.MsSeninID].cards_乗船.Add(c);
                }
                else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.傷病))
                {
                    cardDic[c.MsSeninID].cards_傷病.Add(c);
                }
                else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.陸上勤務))
                {
                    cardDic[c.MsSeninID].cards_陸上勤務.Add(c);
                }
                else if (seninTableCache.Is_旅行日(loginUser, c.MsSiShubetsuID))
                {
                    // 同日転船の場合、レコードを無視する
                    if (c.Days < 0)
                        continue;

                    cardDic[c.MsSeninID].cards_旅行日.Add(c);
                }
                else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.研修) ||
                    c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.待機) ||
                    c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.休職) ||
                    c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.請暇))
                {
                    cardDic[c.MsSeninID].cards_その他.Add(c);
                }
                else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.有給休暇))
                {
                    cardDic[c.MsSeninID].cards_有給休暇.Add(c);
                }
                else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.休暇買上))
                {
                    cardDic[c.MsSeninID].card_休暇買上 = c;
                }
                else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.本年度休暇日数))
                {
                    cardDic[c.MsSeninID].card_本年度休暇日数 = c;
                }
                else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.前年度休暇繰越日数))
                {
                    cardDic[c.MsSeninID].card_前年度休暇繰越日数 = c;
                }
            }

            return cardDic;
        }
    }
}
