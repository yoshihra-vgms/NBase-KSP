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
    public class 乗下船カード出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;
        
        
        public 乗下船カード出力(string templateFilePath, string outputFilePath)
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

                Dictionary<int, 休日付与簿出力.休暇Object> cardDic = 休日付与簿出力.CreateCardDic(loginUser, seninTableCache, date);

                List<MsSenin> senins = 船員.BLC_船員検索_帳票(loginUser, date);

                DateTime yearStart = DateTimeUtils.年度開始日(date);
                DateTime yearEnd = DateTimeUtils.年度終了日(date);

                int i = 0;
                int rows = 72;
                foreach (MsSenin s in senins)
                {
                    if (i + 1 < senins.Count)
                    {
                        xls.Cell(ExcelUtils.ToCellName(0, i * rows) + ":" + ExcelUtils.ToCellName(9, (i + 1) * rows - 1)).Copy(ExcelUtils.ToCellName(0, (i + 1) * rows));
                    }

                    // 出力日
                    xls.Cell(ExcelUtils.ToCellName(7, i * rows + 0)).Value = "出力日: " + DateTime.Now.ToShortDateString();
                    // No
                    xls.Cell(ExcelUtils.ToCellName(8, i * rows + 1)).Value = i + 1;
                    // 氏名コード
                    xls.Cell(ExcelUtils.ToCellName(2, i * rows + 4)).Value = s.ShimeiCode;
                    // 職名
                    xls.Cell(ExcelUtils.ToCellName(2, i * rows + 5)).Value = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, s.MsSiShokumeiID);
                    // 氏名
                    xls.Cell(ExcelUtils.ToCellName(2, i * rows + 6)).Value = s.Sei + " " + s.Mei;


                    // 至
                    DateTime start;

                    if (s.NyuushaDate < yearStart || yearEnd < s.NyuushaDate)
                    {
                        start = yearStart;
                    }
                    else
                    {
                        start = s.NyuushaDate;
                    }

                    xls.Cell(ExcelUtils.ToCellName(8, i * rows + 3)).Value = start.ToShortDateString();

                    // 自
                    DateTime end = yearEnd;

                    if (s.RetireDate < yearStart || yearEnd < s.RetireDate)
                    {
                        end = yearEnd;
                    }
                    else
                    {
                        end = s.RetireDate;
                    }

                    xls.Cell(ExcelUtils.ToCellName(8, i * rows + 4)).Value = end.ToShortDateString();

                    if (cardDic.ContainsKey(s.MsSeninID))
                    {
                        Output(loginUser, seninTableCache, xls, i, rows, cardDic[s.MsSeninID], yearStart, yearEnd);
                    }

                    // 印刷指定
                    xls.Cell(ExcelUtils.ToCellName(11, i + 3)).Value = "=" + ExcelUtils.ToCellName(8, i * rows + 1);
                    xls.Cell(ExcelUtils.ToCellName(12, i + 3)).Value = "=" + ExcelUtils.ToCellName(2, i * rows + 4);
                    xls.Cell(ExcelUtils.ToCellName(13, i + 3)).Value = "=" + ExcelUtils.ToCellName(2, i * rows + 5);
                    xls.Cell(ExcelUtils.ToCellName(14, i + 3)).Value = "=" + ExcelUtils.ToCellName(2, i * rows + 6);

                    // 縦罫線
                    xls.Cell(ExcelUtils.ToCellName(11, i + 3)).Attr.LineLeft (ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
                    xls.Cell(ExcelUtils.ToCellName(11, i + 3)).Attr.LineRight ( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell(ExcelUtils.ToCellName(12, i + 3)).Attr.LineRight ( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell(ExcelUtils.ToCellName(13, i + 3)).Attr.LineRight ( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell(ExcelUtils.ToCellName(14, i + 3)).Attr.LineRight  (ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
                    // 横罫線
                    xls.Cell(ExcelUtils.ToCellName(11, i + 3) + ":" + ExcelUtils.ToCellName(14, i + 3)).Attr.LineBottom ( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                    i++;

                    xls.Cell(ExcelUtils.ToCellName(0, i * rows)).Break = true;
                }

                // 横罫線
                xls.Cell(ExcelUtils.ToCellName(11, i + 2) + ":" + ExcelUtils.ToCellName(14, i + 2)).Attr.LineBottom (ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

                xls.PrintArea(0, 0, 8, i * rows - 1);
                xls.CloseBook(true);
            }
        }


        private void Output(MsUser loginUser, SeninTableCache seninTableCache, ExcelCreator.Xlsx.XlsxCreator xls, int i, int rows, 休日付与簿出力.休暇Object obj, DateTime yearStart, DateTime yearEnd)
        {
            int[] p_乗船 = { 1, i * rows + 11 };

            int y_有給休暇 = i * rows + 33;
            int[] p_有給休暇 = { 1, y_有給休暇 };
            int maxRow_有給休暇 = 8;

            int y_傷病 = i * rows + 42;
            int[] p_傷病 = { 1, y_傷病 };
            int maxRow_傷病 = 2;

            int y_陸上勤務 = i * rows + 45;
            int[] p_陸上勤務 = { 1, y_陸上勤務 };
            int maxRow_陸上勤務 = 2;
           
            int y_旅行日 = i * rows + 48;
            int[] p_旅行日 = { 1, y_旅行日 };
            int maxRow_旅行日 = 8;
            
            int y_その他 = i * rows + 57;
            int[] p_その他 = { 1, y_その他 };
            int maxRow_その他 = 10;

            int[] p_休暇買上 = { 1, i * rows + 68 };
            
            foreach (SiCard c in obj.cards)
            {
                // 2010.06.01 年度の帳票なので、年度外を除外するため置き換えをする
                if (c.StartDate < yearStart)
                {
                    c.StartDate = yearStart;
                }
                if (c.EndDate > yearEnd)
                {
                    c.EndDate = yearEnd;
                }

                // 乗船
                if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船) || c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.艤装員))
                {
                    Output_日数(xls, p_乗船, c);

                    //if (c.MsVesselID != short.MinValue)
                    //{
                    //    xls.Cell(ExcelUtils.ToCellName(p_乗船[0] + 3, p_乗船[1])).Value = seninTableCache.GetMsVesselName(loginUser, c.MsVesselID);
                    //}
                    xls.Cell(ExcelUtils.ToCellName(p_乗船[0] + 3, p_乗船[1])).Value = c.VesselName;

                    p_乗船[1]++;
                }
                // 有給休暇
                else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.有給休暇))
                {
                    Output_日数(xls, p_有給休暇, c);

                    if (p_有給休暇[1] - y_有給休暇 == maxRow_有給休暇 - 1)
                    {
                        p_有給休暇[0] += 4;
                        p_有給休暇[1] = y_有給休暇;
                    }
                    else
                    {
                        p_有給休暇[1]++;
                    }
                }
                // 傷病
                else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.傷病))
                {
                    Output_日数(xls, p_傷病, c);

                    if (p_傷病[1] - y_傷病 == maxRow_傷病 - 1)
                    {
                        p_傷病[0] += 4;
                        p_傷病[1] = y_傷病;
                    }
                    else
                    {
                        p_傷病[1]++;
                    }
                }
                // 陸上勤務
                else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.陸上勤務))
                {
                    Output_日数(xls, p_陸上勤務, c);

                    if (p_陸上勤務[1] - y_陸上勤務 == maxRow_陸上勤務 - 1)
                    {
                        p_陸上勤務[0] += 4;
                        p_陸上勤務[1] = y_陸上勤務;
                    }
                    else
                    {
                        p_陸上勤務[1]++;
                    }
                }
                // 旅行日
                //else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.移動日))
                //else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.移動日) || c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.転船))
                else if (seninTableCache.Is_旅行日(loginUser, c.MsSiShubetsuID)) // 2014.08.06：201408月度改造
                {
                    // 同日転船の場合、レコードを無視する
                    if (c.Days < 0)
                        continue;

                    Output_日数(xls, p_旅行日, c);

                    if (p_旅行日[1] - y_旅行日 == maxRow_旅行日 - 1)
                    {
                        p_旅行日[0] += 4;
                        p_旅行日[1] = y_旅行日;
                    }
                    else
                    {
                        p_旅行日[1]++;
                    }
                }
                // その他
                else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.研修) ||
                    c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.待機) ||
                    c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.休職) ||
                    c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.請暇))
                {
                    Output_日数(xls, p_その他, c);
                    xls.Cell(ExcelUtils.ToCellName(p_その他[0] + 3, p_その他[1])).Value = seninTableCache.GetMsSiShubetsuName(loginUser, c.MsSiShubetsuID);

                    if (p_その他[1] - y_その他 == maxRow_その他 - 1)
                    {
                        p_その他[0] += 4;
                        p_その他[1] = y_その他;
                    }
                    else
                    {
                        p_その他[1]++;
                    }
                }
                // 休暇買上
                else if (c.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.休暇買上))
                {
                    xls.Cell(ExcelUtils.ToCellName(p_休暇買上[0], p_休暇買上[1])).Value = "-";
                    xls.Cell(ExcelUtils.ToCellName(p_休暇買上[0] + 1, p_休暇買上[1])).Value = "-";
                    xls.Cell(ExcelUtils.ToCellName(p_休暇買上[0] + 2, p_休暇買上[1])).Value = c.Days;
                }
            }
        }


        private static void Output_日数(ExcelCreator.Xlsx.XlsxCreator xls, int[] p, SiCard c)
        {
            if (c.EndDate == DateTime.MinValue || (DateTimeUtils.ToFrom(c.StartDate) <= DateTime.Now && DateTime.Now < DateTimeUtils.ToTo(c.EndDate)))
            {
                xls.Cell(ExcelUtils.ToCellName(p[0], p[1])).Value = c.StartDate.ToShortDateString();
                xls.Cell(ExcelUtils.ToCellName(p[0] + 1, p[1])).Value = DateTime.Now.ToShortDateString();
                xls.Cell(ExcelUtils.ToCellName(p[0] + 2, p[1])).Value = int.Parse(StringUtils.ToStr(c.StartDate, DateTime.Now));
            }
            else
            {
                xls.Cell(ExcelUtils.ToCellName(p[0], p[1])).Value = c.StartDate.ToShortDateString();
                xls.Cell(ExcelUtils.ToCellName(p[0] + 1, p[1])).Value = c.EndDate.ToShortDateString();
                xls.Cell(ExcelUtils.ToCellName(p[0] + 2, p[1])).Value = int.Parse(StringUtils.ToStr(c.StartDate, c.EndDate));
            }
        }
    }
}
