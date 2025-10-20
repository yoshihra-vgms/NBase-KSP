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
    public class 配乗表出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;

        private int startRow = 0;

        public 配乗表出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }


        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache, SiHaijou haijou)
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
                    //if (xls.ErrorNo == ExcelCreator.xlErrorNo.errTempCreate)
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

                if (haijou != null)
                {
                    List<Dictionary<int, Dictionary<string, List<SiHaijouItem>>>> haijouDicList = CreateHaijouItemDic(haijou, loginUser, seninTableCache);
                    int row = startRow;
                    SetRows_Header(loginUser, seninTableCache, xls, ref row);
                    SetRows_船(haijouDicList[0], loginUser, seninTableCache, xls, ref row);
                    SetRows_乗船以外(haijouDicList[1], loginUser, seninTableCache, xls, ref row);
                    SetRows_合計(haijouDicList[1], loginUser, seninTableCache, xls, ref row);
                    Draw_縦罫線(loginUser, seninTableCache, xls, row);
                }

                xls.CloseBook(true);
            }
        }


        private List<Dictionary<int, Dictionary<string, List<SiHaijouItem>>>> CreateHaijouItemDic(SiHaijou haijou, MsUser loginUser, SeninTableCache seninTableCache)
        {
            List<Dictionary<int, Dictionary<string, List<SiHaijouItem>>>> result = new List<Dictionary<int, Dictionary<string, List<SiHaijouItem>>>>();

            Dictionary<int, Dictionary<string, List<SiHaijouItem>>> dic船 = new Dictionary<int, Dictionary<string, List<SiHaijouItem>>>();
            Dictionary<int, Dictionary<string, List<SiHaijouItem>>> dic乗船以外 = new Dictionary<int, Dictionary<string, List<SiHaijouItem>>>();

            Dictionary<int, string> shokumeiGroupDic = ShokumeiGroup.GetShokumeiGroupDic(loginUser, seninTableCache);

            foreach (SiHaijouItem item in haijou.SiHaijouItems)
            {
                if (!shokumeiGroupDic.ContainsKey(item.MsSiShokumeiID))
                {
                    continue;
                }

                string group = shokumeiGroupDic[item.MsSiShokumeiID];

                if (item.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船))
                {
                    if (!dic船.ContainsKey(item.MsVesselID))
                    {
                        dic船[item.MsVesselID] = new Dictionary<string, List<SiHaijouItem>>();
                    }

                    if (!dic船[item.MsVesselID].ContainsKey(group))
                    {
                        dic船[item.MsVesselID][group] = new List<SiHaijouItem>();
                    }

                    dic船[item.MsVesselID][group].Add(item);
                }
                else
                {
                    if (!dic乗船以外.ContainsKey(item.MsSiShubetsuID))
                    {
                        dic乗船以外[item.MsSiShubetsuID] = new Dictionary<string, List<SiHaijouItem>>();
                    }

                    if (!dic乗船以外[item.MsSiShubetsuID].ContainsKey(group))
                    {
                        dic乗船以外[item.MsSiShubetsuID][group] = new List<SiHaijouItem>();
                    }

                    dic乗船以外[item.MsSiShubetsuID][group].Add(item);
                }
            }

            result.Add(dic船);
            result.Add(dic乗船以外);

            return result;
        }


        private void SetRows_Header(MsUser loginUser,
            SeninTableCache seninTableCache,
            ExcelCreator.Xlsx.XlsxCreator xls,
            ref int row)
        {
            int col = 0;

            // 縦罫線
            //xls.Cell(ExcelUtils.ToCellName(col, row)).Attr.LineLeft = ExcelCreator.xlLineStyle.lsThick;
            xls.Cell(ExcelUtils.ToCellName(col, row)).Attr.LineLeft(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

            col++;
            xls.Cell(ExcelUtils.ToCellName(col, row)).Value = "乗船";
            xls.Cell(ExcelUtils.ToCellName(col, row + 1)).Value = "数";
            col++;

            foreach (string s in ShokumeiGroup.Group)
            {
                // 幅
                xls.Cell(ExcelUtils.ToCellName(col, row)).ColWidth = 16.13; //11.38;
                xls.Cell(ExcelUtils.ToCellName(col + 1, row)).ColWidth = 6.13; //4.38;
                xls.Cell(ExcelUtils.ToCellName(col + 2, row)).ColWidth = 6.13; //4.38;
                // フォント
                xls.Cell(ExcelUtils.ToCellName(col + 1, row) + ":" + ExcelUtils.ToCellName(col + 1, row + 1)).Attr.FontStyle = ExcelCreator.FontStyle.Bold;//xlFontStyle.xsBold;
                xls.Cell(ExcelUtils.ToCellName(col + 2, row) + ":" + ExcelUtils.ToCellName(col + 2, row + 1)).Attr.FontColor = System.Drawing.Color.Red;//xlColor.xcRed;
                // 縦罫線
                //xls.Cell(ExcelUtils.ToCellName(col, row) + ":" + ExcelUtils.ToCellName(col, row + 1)).Attr.LineLeft = ExcelCreator.xlLineStyle.lsThick;
                xls.Cell(ExcelUtils.ToCellName(col, row) + ":" + ExcelUtils.ToCellName(col, row + 1)).Attr.LineLeft(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

                //xls.Cell(ExcelUtils.ToCellName(col + 1, row) + ":" + ExcelUtils.ToCellName(col + 1, row + 1)).Attr.LineLeft = ExcelCreator.xlLineStyle.lsNormal;
                xls.Cell(ExcelUtils.ToCellName(col + 1, row) + ":" + ExcelUtils.ToCellName(col + 1, row + 1)).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                //xls.Cell(ExcelUtils.ToCellName(col + 2, row) + ":" + ExcelUtils.ToCellName(col + 2, row + 1)).Attr.LineLeft = ExcelCreator.xlLineStyle.lsNormal;
                xls.Cell(ExcelUtils.ToCellName(col + 2, row) + ":" + ExcelUtils.ToCellName(col + 2, row + 1)).Attr.LineLeft( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                //xls.Cell(ExcelUtils.ToCellName(col + 3, row) + ":" + ExcelUtils.ToCellName(col + 3, row + 1)).Attr.LineLeft = ExcelCreator.xlLineStyle.lsThick;
                xls.Cell(ExcelUtils.ToCellName(col + 3, row) + ":" + ExcelUtils.ToCellName(col + 3, row + 1)).Attr.LineLeft( ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

                xls.Cell(ExcelUtils.ToCellName(col, row + 1)).Value = s;
                col++;
                xls.Cell(ExcelUtils.ToCellName(col, row)).Value = "乗船";
                xls.Cell(ExcelUtils.ToCellName(col, row + 1)).Value = "日数";
                col++;
                xls.Cell(ExcelUtils.ToCellName(col, row)).Value = "休暇";
                xls.Cell(ExcelUtils.ToCellName(col, row + 1)).Value = "日数";
                col++;
            }

            // フォント
            xls.Cell(ExcelUtils.ToCellName(0, row) + ":" + ExcelUtils.ToCellName(col, row + 2)).Attr.FontPoint = 14; //10;
            // 横罫線
            //xls.Cell(ExcelUtils.ToCellName(0, row) + ":" + ExcelUtils.ToCellName(col - 1, row)).Attr.LineTop = ExcelCreator.xlLineStyle.lsThick;
            xls.Cell(ExcelUtils.ToCellName(0, row) + ":" + ExcelUtils.ToCellName(col - 1, row)).Attr.LineTop(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

            //xls.Cell(ExcelUtils.ToCellName(0, row + 1) + ":" + ExcelUtils.ToCellName(col - 1, row + 1)).Attr.LineBottom = ExcelCreator.xlLineStyle.lsThick;
            xls.Cell(ExcelUtils.ToCellName(0, row + 1) + ":" + ExcelUtils.ToCellName(col - 1, row + 1)).Attr.LineBottom(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

            row += 2;
        }


        private void SetRows_船(Dictionary<int, Dictionary<string, List<SiHaijouItem>>> haijouDic, 
            MsUser loginUser,
            SeninTableCache seninTableCache,
            ExcelCreator.Xlsx.XlsxCreator xls,
            ref int row)
        {

            xls.Cell(ExcelUtils.ToCellName(0, 0)).ColWidth = 28.38; //船名列幅
            xls.Cell(ExcelUtils.ToCellName(1, 0)).ColWidth = 5.58; //合計列幅
         
            foreach (MsVessel v in seninTableCache.GetMsVesselList(loginUser))
            {

                if (v.SeninEnabled == 0)
                    continue;


                int count = 0;
                int col = 0;

                xls.Cell(ExcelUtils.ToCellName(col, row)).Value = v.Tel + "          " + v.Capacity;
                xls.Cell(ExcelUtils.ToCellName(col, row + 2)).Value = v.VesselName;
                col++;
                col++;

                int maxRowCount = 0;
                foreach (string s in ShokumeiGroup.Group)
                {
                    if (!haijouDic.ContainsKey(v.MsVesselID) || !haijouDic[v.MsVesselID].ContainsKey(s))
                    {
                        col += 3;
                    }
                    else
                    {
                        List<SiHaijouItem> items = haijouDic[v.MsVesselID][s];

                        for (int i = 0; i < items.Count; i++)
                        {
                            SiHaijouItem it = items[i];

                            xls.Cell(ExcelUtils.ToCellName(col + 2, row + i)).Attr.FontColor = System.Drawing.Color.Red;//xlColor.xcRed;

                            xls.Cell(ExcelUtils.ToCellName(col, row + i)).Value = it.GetItemKindString() + it.SeninName;
                            xls.Cell(ExcelUtils.ToCellName(col + 1, row + i)).Value = it.WorkDays;
                            xls.Cell(ExcelUtils.ToCellName(col + 2, row + i)).Value = it.HoliDays;

                            count++;
                        }

                        col += 3;

                        if (items.Count > maxRowCount)
                        {
                            maxRowCount = items.Count;
                        }
                    }
                }

                xls.Cell(ExcelUtils.ToCellName(1, row)).Value = count;

                int pRow = row;

                if (maxRowCount > 3)
                {
                    row += maxRowCount;
                }
                else
                {
                    row += 3;
                }

                // フォント
                xls.Cell(ExcelUtils.ToCellName(0, pRow) + ":" + ExcelUtils.ToCellName(col, row + 1)).Attr.FontPoint = 14; //10;
                // 横罫線
                //xls.Cell(ExcelUtils.ToCellName(0, row) + ":" + ExcelUtils.ToCellName(col - 1, row)).Attr.LineTop = ExcelCreator.xlLineStyle.lsNormal;
                xls.Cell(ExcelUtils.ToCellName(0, row) + ":" + ExcelUtils.ToCellName(col - 1, row)).Attr.LineTop(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            }
        }


        private void SetRows_乗船以外(Dictionary<int, Dictionary<string, List<SiHaijouItem>>> haijouDic,
            MsUser loginUser,
            SeninTableCache seninTableCache,
            ExcelCreator.Xlsx.XlsxCreator xls,
            ref int row)
        {
            int col = 0;
            
            foreach (MsSiShubetsu shu in seninTableCache.GetMsSiShubetsuList(loginUser))
            {
                int count = 0;
                if (seninTableCache.Is_乗船中(loginUser, shu.MsSiShubetsuID) ||
                   seninTableCache.Is_休暇管理(loginUser, shu.MsSiShubetsuID))
                {
                    continue;
                }

                // 2014.09.24 旅行日（乗下船）、旅行日（転船）、旅行日（研修・講習）を、やっぱり、それぞれで扱いたい
                //// 2012.03
                //// 「旅行日(転船)」は、「旅行日」として扱うため、無視する
                //if (shu.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.転船))
                //{
                //    continue;
                //}
                //// 2014.08.06：201408月度改造
                //// 「旅行日(研修・講習)」は、「旅行日」として扱うため、無視する
                //if (shu.MsSiShubetsuID == seninTableCache.MsSiShubetsu_旅行日_研修講習ID(loginUser))
                //{
                //    continue;
                //}

                col = 0;

                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = shu.Name;
                col++;

                int maxRowCount = 0;
                foreach (string s in ShokumeiGroup.Group)
                {
                    if (!haijouDic.ContainsKey(shu.MsSiShubetsuID) || !haijouDic[shu.MsSiShubetsuID].ContainsKey(s))
                    {
                        col += 3;
                    }
                    else
                    {
                        List<SiHaijouItem> items = haijouDic[shu.MsSiShubetsuID][s];

                        for (int i = 0; i < items.Count; i++)
                        {
                            SiHaijouItem it = items[i];

                            xls.Cell(ExcelUtils.ToCellName(col + 2, row + i)).Attr.FontColor = System.Drawing.Color.Red;//xlColor.xcRed;

                            xls.Cell(ExcelUtils.ToCellName(col, row + i)).Value = it.SeninName;
                            xls.Cell(ExcelUtils.ToCellName(col + 1, row + i)).Value = it.WorkDays;
                            xls.Cell(ExcelUtils.ToCellName(col + 2, row + i)).Value = it.HoliDays;
                            
                            count++;
                        }

                        col += 3;

                        if (items.Count > maxRowCount)
                        {
                            maxRowCount = items.Count;
                        }
                    }
                }

                xls.Cell(ExcelUtils.ToCellName(1, row)).Value = count;

                int pRow = row;

                if (maxRowCount > 1)
                {
                    row += maxRowCount;
                }
                else
                {
                    row += 1;
                }

                // フォント
                xls.Cell(ExcelUtils.ToCellName(0, pRow) + ":" + ExcelUtils.ToCellName(col, row + 1)).Attr.FontPoint = 14; //10;
                // 横罫線
                //xls.Cell(ExcelUtils.ToCellName(0, row) + ":" + ExcelUtils.ToCellName(col - 1, row)).Attr.LineTop = ExcelCreator.xlLineStyle.lsNormal;
                xls.Cell(ExcelUtils.ToCellName(0, row) + ":" + ExcelUtils.ToCellName(col - 1, row)).Attr.LineTop( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            }

            // 横罫線
            //xls.Cell(ExcelUtils.ToCellName(0, row - 1) + ":" + ExcelUtils.ToCellName(col - 1, row - 1)).Attr.LineBottom = ExcelCreator.xlLineStyle.lsNormal;
            xls.Cell(ExcelUtils.ToCellName(0, row - 1) + ":" + ExcelUtils.ToCellName(col - 1, row - 1)).Attr.LineBottom(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
        }

        private void SetRows_合計(Dictionary<int, Dictionary<string, List<SiHaijouItem>>> haijouDic,
            MsUser loginUser,
            SeninTableCache seninTableCache,
            ExcelCreator.Xlsx.XlsxCreator xls,
            ref int row)
        {
            int col = 0;

            xls.Cell(ExcelUtils.ToCellName(col, row)).Value = "合計";
            col++;
            xls.Cell(ExcelUtils.ToCellName(col, row)).Value = "=SUM(" + ExcelUtils.ToCellName(col, startRow+2) + ":" + ExcelUtils.ToCellName(col, startRow + row - 3) + ")";
            col++;
            foreach (string s in ShokumeiGroup.Group)
            {
                xls.Cell(ExcelUtils.ToCellName(col, row)).Value = "=COUNTA(" + ExcelUtils.ToCellName(col, startRow + 2) + ":" + ExcelUtils.ToCellName(col, startRow + row - 3) + ")";
                //xls.Cell(ExcelUtils.ToCellName(col, row) + ":" + ExcelUtils.ToCellName(col + 2, row)).Attr.Joint = true;
                xls.Cell(ExcelUtils.ToCellName(col, row) + ":" + ExcelUtils.ToCellName(col + 2, row)).Attr.MergeCells = true;

                //xls.Cell(ExcelUtils.ToCellName(col, row)).Attr.PosHorz = ExcelCreator.xlPosHorz.phCenter;
                xls.Cell(ExcelUtils.ToCellName(col, row)).Attr.HorizontalAlignment = ExcelCreator.HorizontalAlignment.Center;

                col += 3;
            }

            // 横罫線
            //xls.Cell(ExcelUtils.ToCellName(0, row) + ":" + ExcelUtils.ToCellName(col - 1, row)).Attr.LineBottom = ExcelCreator.xlLineStyle.lsThick;
            xls.Cell(ExcelUtils.ToCellName(0, row) + ":" + ExcelUtils.ToCellName(col - 1, row)).Attr.LineBottom(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black); 
        }


        private void Draw_縦罫線(MsUser loginUser,
            SeninTableCache seninTableCache, ExcelCreator.Xlsx.XlsxCreator xls, int row)
        {
            int i = 0;

            //xls.Cell(ExcelUtils.ToCellName(i, startRow) + ":" + ExcelUtils.ToCellName(i, row)).Attr.LineLeft = ExcelCreator.xlLineStyle.lsThick;
            xls.Cell(ExcelUtils.ToCellName(i, startRow) + ":" + ExcelUtils.ToCellName(i, row)).Attr.LineLeft(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

            //xls.Cell(ExcelUtils.ToCellName(i, startRow) + ":" + ExcelUtils.ToCellName(i, row)).Attr.LineRight = ExcelCreator.xlLineStyle.lsThick;
            xls.Cell(ExcelUtils.ToCellName(i, startRow) + ":" + ExcelUtils.ToCellName(i, row)).Attr.LineRight(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
            i++;
            //xls.Cell(ExcelUtils.ToCellName(i, startRow) + ":" + ExcelUtils.ToCellName(i, row)).Attr.LineRight = ExcelCreator.xlLineStyle.lsThick;
            xls.Cell(ExcelUtils.ToCellName(i, startRow) + ":" + ExcelUtils.ToCellName(i, row)).Attr.LineRight(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
            i++;

            foreach (string s in ShokumeiGroup.Group)
            {
                //xls.Cell(ExcelUtils.ToCellName(i, startRow) + ":" + ExcelUtils.ToCellName(i, row)).Attr.LineRight = ExcelCreator.xlLineStyle.lsNormal;
                xls.Cell(ExcelUtils.ToCellName(i, startRow) + ":" + ExcelUtils.ToCellName(i, row)).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                //xls.Cell(ExcelUtils.ToCellName(i + 1, startRow) + ":" + ExcelUtils.ToCellName(i + 1, row)).Attr.LineRight = ExcelCreator.xlLineStyle.lsNormal;
                xls.Cell(ExcelUtils.ToCellName(i + 1, startRow) + ":" + ExcelUtils.ToCellName(i + 1, row)).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                //xls.Cell(ExcelUtils.ToCellName(i + 2, startRow) + ":" + ExcelUtils.ToCellName(i + 2, row)).Attr.LineRight = ExcelCreator.xlLineStyle.lsThick;
                xls.Cell(ExcelUtils.ToCellName(i + 2, startRow) + ":" + ExcelUtils.ToCellName(i + 2, row)).Attr.LineRight(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
                i += 3;
            }

            // 印刷範囲の設定
            xls.PrintArea(0, 0, i-1, row);
        }
    }
}
