using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseUtil;

namespace NBaseCommon.Senin.Excel
{
    public class 船内収支報告書出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;


        public 船内収支報告書出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }


        public void CreateFile(MsUser loginUser, DateTime date, int msVesselId)
        {
            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                //-----------------------------------------
                //2013/12/17 コメントアウト m.y
                //xls.OpenBook(outputFilePath, templateFilePath);
                //-----------------------------------------
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
                //-----------------------------------------

                // 全船
                if (msVesselId == int.MinValue)
                {
                    int i = 0;
                    foreach (MsVessel v in MsVessel.GetRecordsBySeninEnabled(loginUser))
                    {
                        i++;
                        xls.CopySheet(0, i, "");
                        xls.SheetNo = i;
                        xls.SheetName = v.VesselName;
                        
                        _CreateFile(loginUser, xls, date, v.MsVesselID, v.VesselName);
                    }
                    xls.DeleteSheet(0, 1);
                }
                // 各船
                else
                {
                    string vesselName = MsVessel.GetRecordByMsVesselID(loginUser, msVesselId).VesselName;
                    xls.SheetName = vesselName;

                    _CreateFile(loginUser, xls, date, msVesselId, vesselName);
                }

                xls.CloseBook(true);
            }
        }


        private void _CreateFile(MsUser loginUser, ExcelCreator.Xlsx.XlsxCreator xls, DateTime date, int msVesselId, string vesselName)
        {
            decimal 先月末残高 = SiJunbikin.Get_先月末残高(loginUser, date, msVesselId);
            List<SiJunbikin> junbikins = SiJunbikin.GetRecordsByDateAndMsVesselID(loginUser, date, msVesselId);

            xls.Cell("**TODAY").Value = DateTime.Now.ToShortDateString();
            xls.Cell("**VESSEL").Value = vesselName;
            xls.Cell("**NENGETSU").Value = date.Year + "年" + date.Month + "月";

            int startRow = 6;
            int i = 0;

            CreateRow_先月末残高(loginUser, xls, date, 先月末残高, startRow);
            i++;
            
            foreach (SiJunbikin j in junbikins)
            {
                // 番号
                xls.Cell("A" + (startRow + i)).Value = (i + 1).ToString();

                // 日付
                xls.Cell("B" + (startRow + i)).Value = j.JunbikinDate.Month + "月" + j.JunbikinDate.Day + "日";

                // 支払額
                //xls.Cell("C" + (startRow + i)).Value = j.KingakuOut + j.TaxOut;
                xls.Cell("C" + (startRow + i)).Value = j.KingakuOut;

                // 消費税
                if (j.TaxOut > 0)
                {
                    xls.Cell("D" + (startRow + i)).Value = j.TaxOut;
                }

                // 受入額
                xls.Cell("E" + (startRow + i)).Value = j.KingakuIn + j.TaxIn;

                // 明細
                if (j.MsSiMeisaiID != int.MinValue)
                {
                    xls.Cell("F" + (startRow + i)).Value = MsSiMeisai.GetRecord(loginUser, j.MsSiMeisaiID);
                }

                // 備考
                xls.Cell("G" + (startRow + i)).Value = j.Bikou;

                // 勘定科目
                if (j.MsSiKamokuId != int.MinValue)
                {
                    MsSiKamoku sk = MsSiKamoku.GetRecordByMsSiKamokuID(loginUser, j.MsSiKamokuId);

                    xls.Cell("H" + (startRow + i)).Value = sk.KamokuName;

                    // 税金
                    xls.Cell("I" + (startRow + i)).Value = sk.TaxFlag == 0 ? "非課税" : string.Empty;
                }

                // 横罫線
                xls.Cell("A" + (startRow + i) + ":I" + (startRow + i)).Attr.LineBottom ( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                i++;
            }

            CreateRow_合計(xls, startRow, i);
            i++;
            
            CreateRow_次月繰越金額(xls, startRow, i);
            i++;

            // 縦罫線
            xls.Cell("A" + (startRow) + ":A" + (startRow + i - 1)).Attr.LineLeft ( ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
            xls.Cell("B" + (startRow) + ":B" + (startRow + i - 1)).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("C" + (startRow) + ":C" + (startRow + i - 1)).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("D" + (startRow) + ":D" + (startRow + i - 1)).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("E" + (startRow) + ":E" + (startRow + i - 1)).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("F" + (startRow) + ":F" + (startRow + i - 1)).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("G" + (startRow) + ":G" + (startRow + i - 1)).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("H" + (startRow) + ":H" + (startRow + i - 1)).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("I" + (startRow) + ":I" + (startRow + i - 1)).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("I" + (startRow) + ":I" + (startRow + i - 1)).Attr.LineRight(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

            // 横罫線
            xls.Cell("A" + (startRow + i - 1) + ":I" + (startRow + i - 1)).Attr.LineBottom(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

            // 印刷範囲
            //xls.PrintArea(0, 0, 6, startRow + i);
        }


        private void CreateRow_先月末残高(MsUser loginUser, ExcelCreator.Xlsx.XlsxCreator xls, DateTime date, decimal 先月末残高, int startRow)
        {
            // 番号
            xls.Cell("A" + startRow).Value = "1";

            // 日付
            DateTime d = MsSiGetsujiShimeBi.ToFrom_船員_月次締日(loginUser, date);
            xls.Cell("B" + startRow).Value = d.Month + "月" + (d.Day - 1) + "日";

            // 受入額
            xls.Cell("E" + startRow).Value = 先月末残高;

            // 明細
            xls.Cell("F" + startRow).Value = "先月末残高";

            // 横罫線
            xls.Cell("A" + startRow + ":I" + startRow).Attr.LineBottom(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
        }


        private void CreateRow_合計(ExcelCreator.Xlsx.XlsxCreator xls, int startRow, int i)
        {
            // 日付
            xls.Cell("B" + (startRow + i)).Value = "合計";

            // 支払額
            xls.Cell("C" + (startRow + i)).Value = "=SUM(C" + startRow + ":C" + (startRow + i - 1) + ")";

            // 消費税
            xls.Cell("D" + (startRow + i)).Value = "=SUM(D" + startRow + ":D" + (startRow + i - 1) + ")";
      
            // 受入額
            xls.Cell("E" + (startRow + i)).Value = "=SUM(E" + startRow + ":E" + (startRow + i - 1) + ")";
            
            // 横罫線
            xls.Cell("A" + (startRow + i) + ":I" + (startRow + i)).Attr.LineBottom(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
        }

        private void CreateRow_次月繰越金額(ExcelCreator.Xlsx.XlsxCreator xls, int startRow, int i)
        {
            xls.Cell("B" + (startRow + i)).Value = "次月繰越金額";

            // 受入額 - (支払額 + 消費税)
            xls.Cell("E" + (startRow + i)).Value = "=E" + (startRow + i - 1) + "-C" + (startRow + i - 1) + "-D" + (startRow + i - 1);
        }
    }
}
