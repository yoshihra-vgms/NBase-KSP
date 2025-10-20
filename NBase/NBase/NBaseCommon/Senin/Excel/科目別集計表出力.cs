using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseUtil;

namespace NBaseCommon.Senin.Excel
{
    public class 科目別集計表出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;
        
        
        public 科目別集計表出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }


        public void CreateFile(MsUser loginUser, DateTime date, int msVesselId)
        {
            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                //-----------------------
                //2013/12/17 コメントアウト m.y
                //xls.OpenBook(outputFilePath, templateFilePath);
                //-----------------------
                //2013/12/17 変更:OpenBookエラーをなげる m.y
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

                // 2014.03: 2013年度改造
                #region 下のコードに書き換え
                //// 全船
                //if (msVesselId == int.MinValue)
                //{
                //    int i = 0;
                //    foreach (MsVessel v in MsVessel.GetRecordsByHachuEnabled(loginUser))
                //    {
                //        i++;
                //        xls.CopySheet(0, i, "");
                //        xls.SheetNo = i;
                //        xls.SheetName = v.VesselName;

                //        _CreateFile(loginUser, xls, date, v.MsVesselID, v.VesselName);
                //    }
                //    xls.DelSheet(0, 1);
                //}
                //// 各船
                //else
                //{
                //    string vesselName = MsVessel.GetRecordByMsVesselID(loginUser, msVesselId).VesselName;
                //    xls.SheetName = vesselName;

                //    _CreateFile(loginUser, xls, date, msVesselId, vesselName);
                //}
                #endregion

                List<MsTax> taxList = MsTax.GetRecords(loginUser);

                // 全船
                if (msVesselId == int.MinValue)
                {
                    int i = 0;
                    foreach (MsVessel v in MsVessel.GetRecordsBySeninEnabled(loginUser))
                    {
                        //Dictionary<decimal, List<SiJunbikin>> junbikins = SiJunbikin.Get科目別集計表データ(loginUser, date, v.MsVesselID);

                        SortedDictionary<int, List<SiJunbikin>> junbikins = new SortedDictionary<int, List<SiJunbikin>>();
                        try
                        {
                            junbikins = SiJunbikin.Get科目別集計表データ(loginUser, date, v.MsVesselID);
                        }
                        catch (Exception e)
                        {
                            LogFile.Write("", e.Message);
                        }

                        int idxOfVessel = 0;
                        int sheetMaxNo = junbikins.Keys.Count();
                        //foreach (decimal taxRate in junbikins.Keys)
                        foreach (int taxId in junbikins.Keys)
                        {
                            var tax = taxList.Where(obj => obj.MsTaxID == taxId).First();

                            i++;
                            xls.CopySheet(0, i, "");
                            xls.SheetNo = i;
                            //xls.SheetName = v.VesselName + "(" + taxRate.ToString("#") + "%)";
                            if (tax.MsTaxID != 2)
                            {
                                xls.SheetName = v.VesselName + "(" + tax.TaxRate.ToString("#") + "%)";
                            }
                            else
                            {
                                xls.SheetName = v.VesselName + "(軽減" + tax.TaxRate.ToString("#") + "%)";
                            }

                            //_CreateFile(loginUser, xls, date, v.MsVesselID, v.VesselName, idxOfVessel, taxRate, junbikins[taxRate]);
                            _CreateFile(loginUser, xls, date, v.MsVesselID, v.VesselName, idxOfVessel, sheetMaxNo, tax.TaxRate, junbikins[taxId]);

                            idxOfVessel++;
                        }
                    }
                    xls.DeleteSheet(0, 1);
                }
                // 各船
                else
                {
                    int i = 0;
                    string vesselName = MsVessel.GetRecordByMsVesselID(loginUser, msVesselId).VesselName;

                    //Dictionary<decimal, List<SiJunbikin>> junbikins = SiJunbikin.Get科目別集計表データ(loginUser, date, msVesselId);
                    SortedDictionary<int, List<SiJunbikin>> junbikins = SiJunbikin.Get科目別集計表データ(loginUser, date, msVesselId);

                    int idxOfVessel = 0;
                    int sheetMaxNo = junbikins.Keys.Count();
                    //foreach (decimal taxRate in junbikins.Keys)
                    foreach (int taxId in junbikins.Keys)
                    {
                        var tax = taxList.Where(obj => obj.MsTaxID == taxId).First();

                        i++;
                        xls.CopySheet(0, i, "");
                        xls.SheetNo = i;
                        //xls.SheetName = vesselName + "(" + taxRate.ToString("#") + "%)";
                        if (tax.MsTaxID != 2)
                        {
                            xls.SheetName = vesselName + "(" + tax.TaxRate.ToString("#") + "%)";
                        }
                        else
                        {
                            xls.SheetName = vesselName + "(軽減" + tax.TaxRate.ToString("#") + "%)";
                        }

                        //_CreateFile(loginUser, xls, date, msVesselId, vesselName, idxOfVessel, taxRate, junbikins[taxRate]);
                        _CreateFile(loginUser, xls, date, msVesselId, vesselName, idxOfVessel, sheetMaxNo, tax.TaxRate, junbikins[taxId]);

                        idxOfVessel++;
                    }
                    xls.DeleteSheet(0, 1);
                }

                xls.CloseBook(true);
            }
        }

        private void _CreateFile(MsUser loginUser, ExcelCreator.Xlsx.XlsxCreator xls, DateTime date, int msVesselId, string vesselName, int idxOfVessel, int sheetMaxNo, decimal taxRate, List<SiJunbikin> junbikins)
        {
            decimal 先月末残高 = 0;
            //if (idxOfVessel == 0)
            if (sheetMaxNo == idxOfVessel +1) // 最終ページに出力
            {
                先月末残高 = SiJunbikin.Get_先月末残高(loginUser, date, msVesselId);
            }
            SortedDictionary<int, SortedDictionary<int, KamokuRow>> kamokuRows = GroupByKamoku(先月末残高, junbikins);

            xls.Cell("**TODAY").Value = DateTime.Now.ToShortDateString();
            xls.Cell("**VESSEL").Value = vesselName;
            xls.Cell("**NENGETSU").Value = date.Year + "年" + date.Month + "月";

            xls.Cell("D6").Value = "消費税(" + taxRate.ToString("#") + "%)";
            xls.Cell("F6").Value = "消費税(" + taxRate.ToString("#") + "%)";

            int i = 7;
            int rowNo_船員費用 = 65535;
            int rowNo_全社費用 = 65535;

            foreach (KeyValuePair<int, SortedDictionary<int, KamokuRow>> pair in kamokuRows)
            {
                if (pair.Key == (int)MsSiKamoku.費用種別.船員費用)
                {
                    int si = i;
                    OutputRows(xls, ref i, pair.Value);
                    rowNo_船員費用 = i - 1;
                    OutputRow_小計(xls, si, ref i, "船員費用小計");
                }
                else if (pair.Key == (int)MsSiKamoku.費用種別.全社費用)
                {
                    int si = i;
                    OutputRows(xls, ref i, pair.Value);
                    rowNo_全社費用 = i - 1;
                    OutputRow_小計(xls, si, ref i, "全社費用小計");
                }
            }

            i++;

            int rowNo_船内準備金繰越 = i; // 2015.02.20 Add

            OutputRow_船内準備金繰越(xls, rowNo_船員費用, rowNo_全社費用, ref i);

            OutputRow_合計(xls, rowNo_船員費用, rowNo_全社費用, ref i);

            OutputRow_実船内準備金繰越額(xls, rowNo_船内準備金繰越, i);// 2015.02.20 Add


            // 横罫線
            xls.Cell("A5:A" + i).Attr.LineLeft(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
            xls.Cell("B5:B" + i).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("C5:C" + i).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("D5:D" + i).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("E5:E" + i).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("F5:F" + i).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("G5:G" + i).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("G5:G" + i).Attr.LineRight(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
        }


        private void OutputRows(ExcelCreator.Xlsx.XlsxCreator xls, ref int rowNo, SortedDictionary<int, KamokuRow> kamokuRows)
        {
            foreach (KamokuRow row in kamokuRows.Values)
            {
                // 科目
                xls.Cell("A" + rowNo).Value = row.KamokuName;

                // 税金
                xls.Cell("B" + rowNo).Value = row.TaxFlag == (int)MsSiKamoku.税金フラグ.非課税 ? "非課税" : string.Empty;

                // 借方 支払額
                xls.Cell("C" + rowNo).Value = row.KingakuOut;

                // 借方 税金
                xls.Cell("D" + rowNo).Value = row.TaxOut;

                // 貸方 受入額
                xls.Cell("E" + rowNo).Value = row.KingakuIn;

                // 貸方 税金
                xls.Cell("F" + rowNo).Value = row.TaxIn;

                // 合計
                xls.Cell("G" + rowNo).Value = "=SUM(C" + rowNo + ":F" + rowNo + ")";

                // 横罫線
                xls.Cell("A" + rowNo + ":G" + rowNo).Attr.LineTop(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("A" + rowNo + ":G" + rowNo).Attr.LineBottom(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                rowNo++;
            }
        }

        private void OutputRow_小計(ExcelCreator.Xlsx.XlsxCreator xls, int startRow, ref int endRow, string name)
        {
            // 科目
            xls.Cell("A" + endRow).Value = name;
            xls.Cell("A" + endRow).Attr.FontStyle = ExcelCreator.FontStyle.Bold;
            xls.Cell("A" + endRow).RowHeight = 18.75;

            // 借方 支払額
            xls.Cell("C" + endRow).Value = "=SUM(" + ExcelUtils.ToCellName(2, startRow - 1) + ":" + ExcelUtils.ToCellName(2, endRow - 2) + ")";

            // 借方 税金
            xls.Cell("D" + endRow).Value = "=SUM(" + ExcelUtils.ToCellName(3, startRow - 1) + ":" + ExcelUtils.ToCellName(3, endRow - 2) + ")";

            // 貸方 受入額
            xls.Cell("E" + endRow).Value = "=SUM(" + ExcelUtils.ToCellName(4, startRow - 1) + ":" + ExcelUtils.ToCellName(4, endRow - 2) + ")";

            // 貸方 税金
            xls.Cell("F" + endRow).Value = "=SUM(" + ExcelUtils.ToCellName(5, startRow - 1) + ":" + ExcelUtils.ToCellName(5, endRow - 2) + ")";

            // 横罫線
            xls.Cell("A" + endRow + ":G" + endRow).Attr.LineTop( ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
            xls.Cell("A" + endRow + ":G" + endRow).Attr.LineBottom(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

            endRow++;
        }

        private void OutputRow_船内準備金繰越(ExcelCreator.Xlsx.XlsxCreator xls, int rowNo_船員費用, int rowNo_全社費用, ref int i)
        {
            // 科目
            xls.Cell("A" + i).Value = "船内準備金繰越";
            xls.Cell("A" + i).Attr.FontStyle = ExcelCreator.FontStyle.Bold;
            xls.Cell("A" + i).RowHeight = 18.75;

            // 繰越額
            //xls.Cell("C" + i).Value = "=" + ExcelUtils.ToCellName(4, rowNo_船員費用) + "+" + ExcelUtils.ToCellName(4, rowNo_全社費用) + "-" +
            //                                  "(" + ExcelUtils.ToCellName(2, rowNo_船員費用) + "+" + ExcelUtils.ToCellName(3, rowNo_船員費用) + "+" +
            //                                  ExcelUtils.ToCellName(2, rowNo_全社費用) + "+" + ExcelUtils.ToCellName(3, rowNo_全社費用) + ")";
            if (rowNo_全社費用 == 65535)
            {
                xls.Cell("C" + i).Value = "=" + ExcelUtils.ToCellName(4, rowNo_船員費用) + "+" + ExcelUtils.ToCellName(5, rowNo_船員費用) + "-" +
                                                  "(" + ExcelUtils.ToCellName(2, rowNo_船員費用) + "+" + ExcelUtils.ToCellName(3, rowNo_船員費用) + ")";
            }
            else
            {
                xls.Cell("C" + i).Value = "=" + ExcelUtils.ToCellName(4, rowNo_船員費用) + "+" + ExcelUtils.ToCellName(5, rowNo_船員費用) + "+" +
                                                ExcelUtils.ToCellName(4, rowNo_全社費用) + "+" + ExcelUtils.ToCellName(5, rowNo_全社費用) + "-" +
                                                  "(" + ExcelUtils.ToCellName(2, rowNo_船員費用) + "+" + ExcelUtils.ToCellName(3, rowNo_船員費用) + "+" +
                                                  ExcelUtils.ToCellName(2, rowNo_全社費用) + "+" + ExcelUtils.ToCellName(3, rowNo_全社費用) + ")";
            }

            // 横罫線
            xls.Cell("A" + i + ":G" + i).Attr.LineTop ( ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
            xls.Cell("A" + i + ":G" + i).Attr.LineBottom(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

            i++;
        }

        private void OutputRow_合計(ExcelCreator.Xlsx.XlsxCreator xls, int rowNo_船員費用, int rowNo_全社費用, ref int i)
        {
            // 科目
            xls.Cell("A" + i).Value = "合計（消費税含む）";
            xls.Cell("A" + i).Attr.FontStyle = ExcelCreator.FontStyle.Bold;
            xls.Cell("A" + i).RowHeight = 18.75;
            xls.Cell("A" + i + "B" + i).Attr.MergeCells = true;

            // 借方 合計
            xls.Cell("C" + i).Value = "=" + ExcelUtils.ToCellName(2, rowNo_船員費用) + "+" + ExcelUtils.ToCellName(3, rowNo_船員費用) + "+" +
                                              ExcelUtils.ToCellName(2, rowNo_全社費用) + "+" + ExcelUtils.ToCellName(3, rowNo_全社費用) + "+" +
                                              ExcelUtils.ToCellName(2, i - 2);

            // 貸方 合計
            xls.Cell("E" + i).Value = "=" + ExcelUtils.ToCellName(4, rowNo_船員費用) + "+" + ExcelUtils.ToCellName(5, rowNo_船員費用) + "+" +
                                              ExcelUtils.ToCellName(4, rowNo_全社費用) + "+" + ExcelUtils.ToCellName(5, rowNo_全社費用) + "+" +
                                              ExcelUtils.ToCellName(4, i - 2);

            // 横罫線
            xls.Cell("A" + i + ":G" + i).Attr.LineTop (ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
            xls.Cell("A" + i + ":G" + i).Attr.LineBottom( ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
        }




        private void OutputRow_実船内準備金繰越額(ExcelCreator.Xlsx.XlsxCreator xls, int rowNo_船内準備金繰越, int i)
        {
            int rowNo = i + 2;

            // 食糧調整金
            xls.Cell("A" + rowNo).Value = "食糧調整金";
            xls.Cell("A" + rowNo).Attr.FontStyle = ExcelCreator.FontStyle.Bold;

            // 金額欄（空白で出力）
            //xls.Cell("B" + rowNo).Value =

            // 横罫線
            xls.Cell("A" + rowNo + ":B" + rowNo).Attr.LineTop ( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("A" + rowNo + ":B" + rowNo).Attr.LineBottom(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

            rowNo++;

            // 実船内準備金繰越額
            xls.Cell("A" + rowNo).Value = "実船内準備金繰越額";
            xls.Cell("A" + rowNo).Attr.FontStyle = ExcelCreator.FontStyle.Bold; 

            // 金額欄（=船内準備金繰越+食糧調整金）
            xls.Cell("B" + rowNo).Value = "=" + ExcelUtils.ToCellName(1, rowNo - 2) + "+" + ExcelUtils.ToCellName(2, rowNo_船内準備金繰越-1);

            // 横罫線
            xls.Cell("A" + rowNo + ":B" + rowNo).Attr.LineTop( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("A" + rowNo + ":B" + rowNo).Attr.LineBottom ( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

            // 縦罫線
            xls.Cell("A" + (i + 2) + ":A" + rowNo).Attr.LineLeft ( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("B" + (i + 2) + ":B" + rowNo).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("B" + (i + 2) + ":B" + rowNo).Attr.LineRight ( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

        }


        private SortedDictionary<int, SortedDictionary<int, KamokuRow>> GroupByKamoku(decimal 先月末残高, List<SiJunbikin> junbikins)
        {
            SortedDictionary<int, SortedDictionary<int, KamokuRow>> kamokuRows = new SortedDictionary<int, SortedDictionary<int, KamokuRow>>();
            
            // 船内準備金繰越
            kamokuRows[(int)MsSiKamoku.費用種別.船員費用] = new SortedDictionary<int, KamokuRow>();
            kamokuRows[(int)MsSiKamoku.費用種別.船員費用][int.MinValue] = new KamokuRow();
            kamokuRows[(int)MsSiKamoku.費用種別.船員費用][int.MinValue].KamokuName = "船用金繰越";
            kamokuRows[(int)MsSiKamoku.費用種別.船員費用][int.MinValue].KingakuIn = 先月末残高;
         
            foreach (SiJunbikin j in junbikins)
            {
                if (!kamokuRows.ContainsKey(j.HiyouKind))
                {
                    kamokuRows[j.HiyouKind] = new SortedDictionary<int, KamokuRow>();
                }

                if (!kamokuRows[j.HiyouKind].ContainsKey(j.MsSiKamokuId))
                {
                    kamokuRows[j.HiyouKind][j.MsSiKamokuId] = new KamokuRow();
                    kamokuRows[j.HiyouKind][j.MsSiKamokuId].KamokuName = j.KamokuName;
                    kamokuRows[j.HiyouKind][j.MsSiKamokuId].TaxFlag = j.TaxFlag;
                }

                kamokuRows[j.HiyouKind][j.MsSiKamokuId].KingakuOut += j.KingakuOut;
                kamokuRows[j.HiyouKind][j.MsSiKamokuId].TaxOut += j.TaxOut;
                kamokuRows[j.HiyouKind][j.MsSiKamokuId].KingakuIn += j.KingakuIn;
                kamokuRows[j.HiyouKind][j.MsSiKamokuId].TaxIn += j.TaxIn;
            }

            return kamokuRows;
        }


        private class KamokuRow
        {
            public string KamokuName { get; set; }
            public int TaxFlag { get; set; }

            public decimal KingakuOut { get; set; }
            public decimal TaxOut { get; set; }
            public decimal KingakuIn { get; set; }
            public decimal TaxIn { get; set; }
        }
   }
}
