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
    public class 休暇消化状況出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;
        
        
        public 休暇消化状況出力(string templateFilePath, string outputFilePath)
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

                string hCenter = "&\"ＭＳ Ｐゴシック\"&14休暇消化状況表（" + date.ToString("yyyy") + "年度）";
                string hRight = "&\"ＭＳ Ｐゴシック\"&11日付：" + DateTime.Now.ToShortDateString();
                xls.Header("", hCenter, hRight);

                Dictionary<int, 休暇Object> cardDic = CreateCardDic(loginUser, seninTableCache, date);

                List<MsSenin> senins = 船員.BLC_船員検索_帳票(loginUser, date);
  
                int startRow = 6;
                xls.RowInsert(startRow - 1, senins.Count);

                Output_有給休暇(loginUser, seninTableCache, xls, cardDic, senins, startRow);
            }
        }


        internal static void Output_有給休暇(MsUser loginUser,
            SeninTableCache seninTableCache, 
            ExcelCreator.Xlsx.XlsxCreator xls, 
            Dictionary<int, 休暇Object> cardDic, 
            List<MsSenin> senins, 
            int startRow)
        {
            int i = 0;
            foreach (MsSenin s in senins)
            {
                // 行ヘッダ.
                // No, 職名, 船員名.
                xls.Cell("A" + (startRow + i)).Value = (i + 1).ToString();
                xls.Cell("B" + (startRow + i)).Value = seninTableCache.GetMsSiShokumeiName(loginUser, s.MsSiShokumeiID);
                xls.Cell("C" + (startRow + i)).Value = s.Sei + " " + s.Mei;

                // 縦罫線
                xls.Cell("A" + (startRow + i)).Attr.LineLeft (ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
                xls.Cell("A" + (startRow + i)).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("B" + (startRow + i)).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("C" + (startRow + i)).Attr.LineRight (ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

                if (!cardDic.ContainsKey(s.MsSeninID))
                {
                    continue;
                }

                // 休暇情報.
                for (int k = 0, col = 3; k < 15; k++, col += 3)
                {
                    if (k < cardDic[s.MsSeninID].cards_有給休暇.Count)
                    {
                        SiCard c = cardDic[s.MsSeninID].cards_有給休暇[k];

                        xls.Cell(ExcelUtils.ToCellName(col, startRow + i - 1)).Value = c.StartDate.ToShortDateString();

                        if (c.EndDate == DateTime.MinValue || (DateTimeUtils.ToFrom(c.StartDate) <= DateTime.Now && DateTime.Now < DateTimeUtils.ToTo(c.EndDate)))
                        {
                            xls.Cell(ExcelUtils.ToCellName(col + 2, startRow + i - 1)).Value = StringUtils.ToStr(c.StartDate, DateTime.Now);
                        }
                        else
                        {
                            xls.Cell(ExcelUtils.ToCellName(col + 1, startRow + i - 1)).Value = c.EndDate.ToShortDateString();
                            xls.Cell(ExcelUtils.ToCellName(col + 2, startRow + i - 1)).Value = StringUtils.ToStr(c.StartDate, c.EndDate);
                        }
                    }

                    // 縦罫線
                    xls.Cell(ExcelUtils.ToCellName(col + 1, startRow + i - 1)).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell(ExcelUtils.ToCellName(col + 2, startRow + i - 1)).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                }

                // 買上日数.
                if (cardDic[s.MsSeninID].card_休暇買上 != null)
                    xls.Cell("AW" + (startRow + i)).Value = cardDic[s.MsSeninID].card_休暇買上.Days;
                // 繰越日数.
                if (cardDic[s.MsSeninID].card_前年度休暇繰越日数 != null)
                    xls.Cell("AX" + (startRow + i)).Value = cardDic[s.MsSeninID].card_前年度休暇繰越日数.Days;
                // 本年給付日数.
                if (cardDic[s.MsSeninID].card_本年度休暇日数 != null)
                    xls.Cell("AY" + (startRow + i)).Value = cardDic[s.MsSeninID].card_本年度休暇日数.Days;
                // 本年度日数.
                xls.Cell("AZ" + (startRow + i)).Value = "=AX" + (startRow + i) + "+AY" + (startRow + i);
                // 消化日数.
                xls.Cell("BA" + (startRow + i)).Value = CreateCellValue_有休日数(startRow + i - 1) + "+AW" + (startRow + i);
                // 残休日数.
                xls.Cell("BB" + (startRow + i)).Value = "=AZ" + (startRow + i) + "-BA" + (startRow + i);

                // 縦罫線
                xls.Cell("AW" + (startRow + i)).Attr.LineLeft (ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
                xls.Cell("AW" + (startRow + i)).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("AY" + (startRow + i)).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("AZ" + (startRow + i)).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("BA" + (startRow + i)).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("BB" + (startRow + i)).Attr.LineRight (ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

                // 横罫線
                xls.Cell("A" + (startRow + i) + ":BB" + (startRow + i)).Attr.LineBottom (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                i++;
            }

            // 合計.
            // 買上日数.
            xls.Cell("**買上日数").Value = "=SUM(AW" + startRow + ":AW" + (startRow + i - 1) + ")";
            xls.Cell("**SUB_買上日数").Value = "=SUBTOTAL(109, AW" + startRow + ":AW" + (startRow + i - 1) + ")";
            // 繰越日数.
            xls.Cell("**繰越日数").Value = "=SUM(AX" + startRow + ":AX" + (startRow + i - 1) + ")";
            xls.Cell("**SUB_繰越日数").Value = "=SUBTOTAL(109, AX" + startRow + ":AX" + (startRow + i - 1) + ")";
            // 本年給付日数.
            xls.Cell("**本年給付日数").Value = "=SUM(AY" + startRow + ":AY" + (startRow + i - 1) + ")";
            xls.Cell("**SUB_本年給付日数").Value = "=SUBTOTAL(109, AY" + startRow + ":AY" + (startRow + i - 1) + ")";
            // 本年度日数.
            xls.Cell("**本年度合計").Value = "=SUM(AZ" + startRow + ":AZ" + (startRow + i - 1) + ")";
            xls.Cell("**SUB_本年度合計").Value = "=SUBTOTAL(109, AZ" + startRow + ":AZ" + (startRow + i - 1) + ")";
            // 消化日数.
            xls.Cell("**消化日数").Value = "=SUM(BA" + startRow + ":BA" + (startRow + i - 1) + ")";
            xls.Cell("**SUB_消化日数").Value = "=SUBTOTAL(109, BA" + startRow + ":BA" + (startRow + i - 1) + ")";
            // 残休日数.
            xls.Cell("**残休日数").Value = "=SUM(BB" + startRow + ":BB" + (startRow + i - 1) + ")";
            xls.Cell("**SUB_残休日数").Value = "=SUBTOTAL(109, BB" + startRow + ":BB" + (startRow + i - 1) + ")";

            xls.CloseBook(true);
        }


        private static string CreateCellValue_有休日数(int row)
        {
            string str = "=";

            for (int i = 0, col = 5; i < 15; i++, col += 3)
            {
                if (i > 0)
                {
                    str += "+";
                }

                str += ExcelUtils.ToCellName(col, row);
            }

            return str;
        }


        private Dictionary<int, 休暇Object> CreateCardDic(MsUser loginUser, SeninTableCache seninTableCache, DateTime date)
        {
            // Dictionary<MsSeninId, 休暇Object>
            Dictionary<int, 休暇Object> cardDic = new Dictionary<int, 休暇Object>();

            SiCardFilter filter = new SiCardFilter();

            filter.Start = DateTimeUtils.年度開始日(date);
            filter.End = DateTimeUtils.年度終了日(date);

            filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.有給休暇));
            filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.休暇買上));
            filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.本年度休暇日数));
            filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.前年度休暇繰越日数));
            
            filter.OrderByStr = "OrderByStartDate";

            List<SiCard> cards = SiCard.GetRecordsByFilter(loginUser, filter);
            foreach (SiCard c in cards)
            {
                if (!cardDic.ContainsKey(c.MsSeninID))
                {
                    cardDic[c.MsSeninID] = new 休暇Object();
                }

                if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.休暇買上))
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
                else
                {
                    cardDic[c.MsSeninID].cards_有給休暇.Add(c);
                }
            }

            return cardDic;
        }


        internal class 休暇Object
        {
            public List<SiCard> cards_乗船 = new List<SiCard>();
            public List<SiCard> cards_傷病 = new List<SiCard>();
            public List<SiCard> cards_陸上勤務 = new List<SiCard>();
            public List<SiCard> cards_旅行日 = new List<SiCard>();
            public List<SiCard> cards_その他 = new List<SiCard>();
            public List<SiCard> cards_有給休暇 = new List<SiCard>();

            public SiCard card_休暇買上;
            public SiCard card_本年度休暇日数;
            public SiCard card_前年度休暇繰越日数;
        }
    }
}
