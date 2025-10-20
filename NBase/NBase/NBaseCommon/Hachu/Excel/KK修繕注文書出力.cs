using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseUtil;
using NBaseData.DS;
using NBaseCommon.Senin.Excel.util;
using System.Globalization;

namespace NBaseCommon.Hachu.Excel
{
    public class KK修繕注文書出力 : 発注帳票Common
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;
        private readonly string outputPdfFilePath;

        public KK修繕注文書出力(string templateFilePath, string outputFilePath, string outputPdfFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
            this.outputPdfFilePath = outputPdfFilePath;
        }

        public void CreateFile(MsUser loginUser, 発注帳票Common.kubun発注帳票 kubun, string Id)
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
                //-----------------------

                _BuildData(loginUser, kubun, Id);
                _CreateFile(loginUser, xls, kubun);

                xls.DeleteSheet(0, 2); // Template ページを削除する


                if (outputPdfFilePath != null)
                {
                    xls.CloseBook(true, outputPdfFilePath, true);
                }
                else
                {
                    xls.CloseBook(true);
                }
            }
        }


        private void _CreateFile(MsUser loginUser, ExcelCreator.Xlsx.XlsxCreator xls, 発注帳票Common.kubun発注帳票 kubun)
        {
            int tempCoverPageNo = 0;
            int tempPageNo = 1;

            int rowsPerCoverPageMax = 23; // １ページの行数
            int rowsPerPageMax = 28;

            int pages = 1;

            int itemSbtCount = 1;
            string coverSheetName = "";

            JapaneseCalendar jc = new JapaneseCalendar();

            var itemSbtIds = Items.Select(o => o.ItemSbtID).Distinct().OrderBy(o => o);
            foreach (string itemSbtId in itemSbtIds)
            {
                var shousaiItemList = dic_ShousaiItems[itemSbtId];

                int keyCount = 0;
                foreach (string key in shousaiItemList.Keys)
                {
                    keyCount++;

                    xls.CopySheet(tempCoverPageNo, pages + 1, "");
                    xls.SheetNo = pages + 1;

                    coverSheetName = dic_Items[itemSbtId][0].Header;
                    if (keyCount > 1)
                    {
                        coverSheetName += "(" + keyCount.ToString() + ")";
                    }
                    if (kubun == kubun発注帳票.見積依頼書)
                        coverSheetName += "見積依頼書";
                    else
                        coverSheetName += "注文書";

                    xls.SheetName = coverSheetName;



                    int necessaryPages = 1;

                    string[] keys = key.Split('\n');

                    if (keys.Count() + 1 + shousaiItemList[key].Count() > rowsPerCoverPageMax)
                    {
                        int rows = (keys.Count() + 1 + shousaiItemList[key].Count()) - rowsPerCoverPageMax;
                        necessaryPages = (rows / rowsPerPageMax) + 1;

                        for (int i = 0; i < necessaryPages; i++)
                            xls.CopySheet(tempPageNo, pages + 1 + (i + 1), coverSheetName + "(" + (i + 1).ToString() + ")");

                        necessaryPages += 1;
                    }


                    int maxRows = rowsPerCoverPageMax;
                    int itemPageCount = 0;
                    int shousaiNo = 0;
                    do
                    {
                        int rowNo = 0;

                        xls.SheetNo = pages + 1;

                        xls.Cell("**年").Value = jc.GetYear(HachuDay);
                        xls.Cell("**月").Value = HachuDay.Month;
                        xls.Cell("**日").Value = HachuDay.Day;

                        xls.Cell("**発注先").Value = CustomerName;
                        xls.Cell("**宛先").Value = Tanto;
                        xls.Cell("**敬称").Value = Honorific;



                        xls.Cell("**船名").Value = VesselName;

                        if (dic_Items[itemSbtId][0].ItemSbtID == "1")
                        {
                            xls.Cell("**甲板").Value = "○";
                        }
                        else if (dic_Items[itemSbtId][0].ItemSbtID == "2")
                        {
                            xls.Cell("**機関").Value = "○";
                        }
                        else if (dic_Items[itemSbtId][0].ItemSbtID == "3")
                        {
                            xls.Cell("**事務").Value = "○";
                        }



                        if (itemPageCount > 0)
                        {
                            maxRows = rowsPerPageMax;
                        }
                        else
                        {
                            foreach (string k in keys)
                            {
                                ++rowNo;
                                xls.Cell("**品名" + (rowNo).ToString()).Attr.HorizontalAlignment = ExcelCreator.HorizontalAlignment.Center;
                                xls.Cell("**品名" + (rowNo).ToString()).Value = k;
                            }

                            ++rowNo;


                            maxRows -= rowNo;
                        }


                        for (int i = 0; i < maxRows; i++)
                        {
                            if (shousaiItemList[key].Count <= shousaiNo)
                                break;

                            ShousaiItem shousaiItem = shousaiItemList[key][shousaiNo];

                            ++rowNo;

                            // 番号
                            xls.Cell("**番号" + (rowNo).ToString()).Value = (++shousaiNo);

                            // 品名
                            xls.Cell("**品名" + (rowNo).ToString()).Value = shousaiItem.ShousaiItemName;

                            // 数量
                            xls.Cell("**数量" + (rowNo).ToString()).Value = shousaiItem.Count.ToString() + shousaiItem.TaniName;

                            // 在庫
                            xls.Cell("**在庫" + (rowNo).ToString()).Value = shousaiItem.Zaiko.ToString();

                            // 査定数
                            if (kubun == 発注帳票Common.kubun発注帳票.査定表)
                            {
                                xls.Cell("**査定" + (rowNo).ToString()).Value = shousaiItem.Satei.ToString();
                            }

                            // 備考
                            xls.Cell("**備考" + (rowNo).ToString()).Value = shousaiItem.Bikou;

                        }

                        pages++;
                        itemPageCount++;

                    } while (itemPageCount < necessaryPages);
                }

                itemSbtCount++;
            }
       }
    }
}
