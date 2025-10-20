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
    public class 休日付与簿出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;
        
        
        public 休日付与簿出力(string templateFilePath, string outputFilePath)
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

                Dictionary<int, 休暇Object> cardDic = CreateCardDic(loginUser, seninTableCache, date);

                List<MsSenin> senins = 船員.BLC_船員検索_帳票(loginUser, date);

                int i = 0;
                int rows = 72;
                foreach (MsSenin s in senins)
                {
                    if (i + 1 < senins.Count)
                    {
                        xls.Cell(ExcelUtils.ToCellName(16, (i + 1) * rows)).Break = true;
                        xls.Cell(ExcelUtils.ToCellName(0, i * rows) + ":" + ExcelUtils.ToCellName(15, (i + 1) * rows - 1)).Copy(ExcelUtils.ToCellName(0, (i + 1) * rows));
                    }

                    // No
                    xls.Cell(ExcelUtils.ToCellName(15, i * rows + 4)).Value = i + 1;
                    // 氏名コード
                    xls.Cell(ExcelUtils.ToCellName(1, i * rows + 5)).Value = s.ShimeiCode;
                    // 職名
                    xls.Cell(ExcelUtils.ToCellName(1, i * rows + 6)).Value = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, s.MsSiShokumeiID);
                    // 氏名
                    xls.Cell(ExcelUtils.ToCellName(2, i * rows + 6)).Value = s.Sei + " " + s.Mei;

                    DateTime yearStart = DateTimeUtils.年度開始日(date);
                    DateTime yearEnd = DateTimeUtils.年度終了日(date);

                    // 自
                    DateTime start;

                    if (s.NyuushaDate < yearStart || yearEnd < s.NyuushaDate)
                    {
                        start = yearStart;
                    }
                    else
                    {
                        start = s.NyuushaDate;
                    }

                    xls.Cell(ExcelUtils.ToCellName(0, i * rows + 15)).Value = start.ToShortDateString();
                    
                    // 至
                    DateTime end = yearEnd;

                    if (s.RetireDate < yearStart || yearEnd < s.RetireDate)
                    {
                        end = yearEnd;
                    }
                    else
                    {
                        end = s.RetireDate;
                    }

                    xls.Cell(ExcelUtils.ToCellName(0, i * rows + 18)).Value = end.ToShortDateString();

                    // 基準労働期間
                    
                    xls.Cell(ExcelUtils.ToCellName(6, i * rows + 6)).Value = ((end - start).Days / 30) + "カ月";

                    if (cardDic.ContainsKey(s.MsSeninID))
                    {
                        Output(loginUser, seninTableCache, xls, i, rows, cardDic[s.MsSeninID]);
                    }

                    // 印刷指定
                    xls.Cell(ExcelUtils.ToCellName(18, i + 3)).Value = "=" + ExcelUtils.ToCellName(15, i * rows + 4);
                    xls.Cell(ExcelUtils.ToCellName(19, i + 3)).Value = "=" + ExcelUtils.ToCellName(1, i * rows + 5);
                    xls.Cell(ExcelUtils.ToCellName(20, i + 3)).Value = "=" + ExcelUtils.ToCellName(1, i * rows + 6);
                    xls.Cell(ExcelUtils.ToCellName(21, i + 3)).Value = "=" + ExcelUtils.ToCellName(2, i * rows + 6);

                    // 縦罫線
                    xls.Cell(ExcelUtils.ToCellName(18, i + 3)).Attr.LineLeft (ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
                    xls.Cell(ExcelUtils.ToCellName(18, i + 3)).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell(ExcelUtils.ToCellName(19, i + 3)).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell(ExcelUtils.ToCellName(20, i + 3)).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell(ExcelUtils.ToCellName(21, i + 3)).Attr.LineRight ( ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
                    // 横罫線
                    xls.Cell(ExcelUtils.ToCellName(18, i + 3) + ":" + ExcelUtils.ToCellName(21, i + 3)).Attr.LineBottom(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                    i++;
                }

                // 横罫線
                xls.Cell(ExcelUtils.ToCellName(18, i + 2) + ":" + ExcelUtils.ToCellName(21, i + 2)).Attr.LineBottom ( ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

                xls.PrintArea(0, 0, 15, i * rows - 1);
                xls.CloseBook(true);
            }
        }


        internal static Dictionary<int, 休暇Object> CreateCardDic(MsUser loginUser, SeninTableCache seninTableCache, DateTime date)
        {
            // Dictionary<MsSeninId, 休暇Object>
            Dictionary<int, 休暇Object> cardDic = new Dictionary<int, 休暇Object>();

            SiCardFilter filter = new SiCardFilter();
            filter.Start = DateTimeUtils.年度開始日(date);
            filter.End = DateTimeUtils.年度終了日(date);
            filter.OrderByStr = "OrderByStartDate";

            List<SiCard> cards = SiCard.GetRecordsByFilter(loginUser, filter);


            List<SiKyuka> kyukaList = SiKyuka.GetRecordsByNendo(loginUser, filter.Start.Year.ToString());

            foreach (SiCard c in cards)
            {
                if (!cardDic.ContainsKey(c.MsSeninID))
                {
                    cardDic[c.MsSeninID] = new 休暇Object();
                }

                if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船休暇))
                {
                    cardDic[c.MsSeninID].cards_乗船休暇.Add(c);
                }
                else
                {
                    cardDic[c.MsSeninID].cards.Add(c);
                }
            }


            foreach (SiKyuka kyuka in kyukaList)
            {
                if (!cardDic.ContainsKey(kyuka.MsSeninID))
                {
                    cardDic[kyuka.MsSeninID] = new 休暇Object();
                }

                cardDic[kyuka.MsSeninID].kyuka = kyuka;
            }

            return cardDic;
        }


        private void Output(MsUser loginUser, SeninTableCache seninTableCache, ExcelCreator.Xlsx.XlsxCreator xls, int i, int rows, 休暇Object obj)
        {
            int[] p_乗船 = { 1, i * rows + 14 };
            int[] p_傷病 = { 1, i * rows + 35 };
            int[] p_陸上勤務 = { 1, i * rows + 38 };
            int[] p_乗船休暇 = { 4, i * rows + 14 };
            int[] p_有給休暇 = { 7, i * rows + 14 };
            int[] p_旅行日 = { 7, i * rows + 30 };
            int[] p_その他 = { 7, i * rows + 46 };

            foreach (SiCard c in obj.cards)
            {
                // 乗船
                if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船) || c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.艤装員))
                {
                    if (c.MsVesselID != short.MinValue)
                    {
                        xls.Cell(ExcelUtils.ToCellName(p_乗船[0], p_乗船[1])).Value = seninTableCache.GetMsVesselName(loginUser, c.MsVesselID);
                    }
                    else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.艤装員))
                    {
                        xls.Cell(ExcelUtils.ToCellName(p_乗船[0], p_乗船[1])).Value = "艤装員";
                    }

                    Output_日数(xls, p_乗船[0] + 1, p_乗船[1], c);

                    xls.Cell(ExcelUtils.ToCellName(p_乗船[0] + 2, p_乗船[1])).Value = seninTableCache.ToShokumeiAbbrStr(loginUser, c.SiLinkShokumeiCards);
                    p_乗船[1]++;

                    // 乗船休暇
                    int days_乗船休暇 = int.MinValue;
                    DateTime first_乗船休暇_Date = DateTime.MinValue;

                    foreach (SiCard c1 in obj.cards_乗船休暇)
                    {
                        if (c1.StartDate <= c.EndDate && c.StartDate < c1.EndDate || c.EndDate == DateTime.MinValue && c.StartDate < c1.EndDate)
                        {
                            if (days_乗船休暇 == int.MinValue)
                            {
                                first_乗船休暇_Date = c1.StartDate;
                                days_乗船休暇 = 0;
                            }

                            days_乗船休暇 += int.Parse(StringUtils.ToStr(c1.StartDate, c1.EndDate));
                        }
                    }

                    if (days_乗船休暇 != int.MinValue)
                    {
                        xls.Cell(ExcelUtils.ToCellName(p_乗船休暇[0], p_乗船休暇[1])).Value = days_乗船休暇;
                        xls.Cell(ExcelUtils.ToCellName(p_乗船休暇[0] + 1, p_乗船休暇[1])).Value = first_乗船休暇_Date.ToShortDateString();
                    }

                    p_乗船休暇[1]++;
                }
                // 傷病
                else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.傷病))
                {
                    xls.Cell(ExcelUtils.ToCellName(p_傷病[0], p_傷病[1])).Value = "傷病待機";

                    Output_日数(xls, p_傷病[0] + 1, p_傷病[1], c);
                    p_傷病[1]++;
                }
                // 陸上勤務
                else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.陸上勤務))
                {
                    xls.Cell(ExcelUtils.ToCellName(p_陸上勤務[0], p_陸上勤務[1])).Value = "陸上勤務";

                    Output_日数(xls, p_陸上勤務[0] + 1, p_陸上勤務[1], c);
                    p_陸上勤務[1]++;
                }
                // 有給休暇
                else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.有給休暇))
                {
                    //Output_日数(xls, p_有給休暇[0], p_有給休暇[1], c);

                    //xls.Cell(ExcelUtils.ToCellName(p_有給休暇[0] + 1, p_有給休暇[1])).Value = c.StartDate.ToShortDateString();
                    //xls.Cell(ExcelUtils.ToCellName(p_有給休暇[0] + 2, p_有給休暇[1])).Value = "有給休暇";
                    //p_有給休暇[1]++;

                }
                // 旅行日
                //else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.移動日))
                //else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.移動日) || c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.転船))
                else if (seninTableCache.Is_旅行日(loginUser, c.MsSiShubetsuID)) // 2014.08.06：201408月度改造
                {
                    // 同日転船の場合、レコードを無視する
                    if (c.Days < 0)
                        continue;

                    Output_日数(xls, p_旅行日[0], p_旅行日[1], c);

                    xls.Cell(ExcelUtils.ToCellName(p_旅行日[0] + 1, p_旅行日[1])).Value = c.StartDate.ToShortDateString();
                    xls.Cell(ExcelUtils.ToCellName(p_旅行日[0] + 2, p_旅行日[1])).Value = "旅行日";
                    p_旅行日[1]++;
                }
                // その他
                else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.研修) ||
                    c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.待機) ||
                    c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.休職) ||
                    c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.請暇))
                {
                    Output_日数(xls, p_その他[0], p_その他[1], c);

                    xls.Cell(ExcelUtils.ToCellName(p_その他[0] + 1, p_その他[1])).Value = c.StartDate.ToShortDateString();

                    if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.研修))
                    {
                        xls.Cell(ExcelUtils.ToCellName(p_その他[0] + 2, p_その他[1])).Value = "その他";
                    }
                    else
                    {
                        xls.Cell(ExcelUtils.ToCellName(p_その他[0] + 2, p_その他[1])).Value = seninTableCache.GetMsSiShubetsuName(loginUser, c.MsSiShubetsuID);
                    }

                    p_その他[1]++;
                }
            }

            xls.Cell(ExcelUtils.ToCellName(p_有給休暇[0] + 2, p_有給休暇[1])).Value = "有給休暇";
            if (obj.kyuka != null)
            {
                xls.Cell(ExcelUtils.ToCellName(p_有給休暇[0], p_有給休暇[1])).Value = obj.kyuka.YukyuKyuka;
            }
            p_有給休暇[1]++;
            xls.Cell(ExcelUtils.ToCellName(p_有給休暇[0] + 2, p_有給休暇[1])).Value = "陸上休暇";
            if (obj.kyuka != null)
            {
                xls.Cell(ExcelUtils.ToCellName(p_有給休暇[0], p_有給休暇[1])).Value = obj.kyuka.RikujyoKyuka;
            }
            p_有給休暇[1]++;
            xls.Cell(ExcelUtils.ToCellName(p_有給休暇[0] + 2, p_有給休暇[1])).Value = "船内休日";
            if (obj.kyuka != null)
            {
                xls.Cell(ExcelUtils.ToCellName(p_有給休暇[0], p_有給休暇[1])).Value = obj.kyuka.SennaiKyujitsu;
            }
            p_有給休暇[1]++;
            xls.Cell(ExcelUtils.ToCellName(p_有給休暇[0] + 2, p_有給休暇[1])).Value = "船内休暇";
            if (obj.kyuka != null)
            {
                xls.Cell(ExcelUtils.ToCellName(p_有給休暇[0], p_有給休暇[1])).Value = obj.kyuka.SennaiKyuka;
            }
        }


        private static void Output_日数(ExcelCreator.Xlsx.XlsxCreator xls, int x, int y, SiCard c)
        {
            if (c.EndDate == DateTime.MinValue || (DateTimeUtils.ToFrom(c.StartDate) <= DateTime.Now && DateTime.Now < DateTimeUtils.ToTo(c.EndDate)))
            {
                xls.Cell(ExcelUtils.ToCellName(x, y)).Value = int.Parse(StringUtils.ToStr(c.StartDate, DateTime.Now));
            }
            else
            {
                xls.Cell(ExcelUtils.ToCellName(x, y)).Value = int.Parse(StringUtils.ToStr(c.StartDate, c.EndDate));
            }
        }


        internal class 休暇Object
        {
            public List<SiCard> cards = new List<SiCard>();
            public List<SiCard> cards_乗船休暇 = new List<SiCard>();
            public SiKyuka kyuka = null;
        }
    }
}
