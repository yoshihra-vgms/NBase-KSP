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
    public class 未受講者一覧出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;

        public 未受講者一覧出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }


        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache, SiKoushuFilter filter, List<SiKoushu> koushuList)
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

                if (koushuList != null)
                {
                    int row = 10;
                    SetRows(loginUser, xls, seninTableCache, filter, koushuList, ref row);
                    
                    
                    // 印刷範囲の設定
                    int printRangeRow = (9 + ((row - 10) / 25) * 25);
                    if (((row - 10) % 25) > 0)
                    {
                        printRangeRow += 25;
                    }
                    xls.PrintArea(0, 0, 9, printRangeRow);
                }

                xls.CloseBook(true);
            }
        }

        private void SetRows(MsUser loginUser,
            ExcelCreator.Xlsx.XlsxCreator xls,
            SeninTableCache seninTableCache, 
            SiKoushuFilter filter,
            List<SiKoushu> koushuList,
            ref int row)
        {
            int col = 0;

            xls.Cell("A2").Value = "講習名：" + (filter.KoushuName == null ? "" : filter.KoushuName);
            xls.Cell("A3").Value = "職名：" + seninTableCache.GetMsSiShokumeiName(loginUser, filter.MsSiShokumeiID);
            xls.Cell("A4").Value = "氏名コード：" + filter.ShimeiCode;
            xls.Cell("A5").Value = "氏名：" + filter.Name;
            xls.Cell("A6").Value = "氏名（カナ）：" + filter.NameKana;
            string dateStr = "";
            if (filter.JisekiFrom != DateTime.MinValue)
            {
                dateStr = filter.JisekiFrom.ToShortDateString() + "～";
            }
            if (filter.JisekiTo != DateTime.MinValue)
            {
                if (filter.JisekiFrom == DateTime.MinValue)
                {
                    dateStr += "～";
                }
                dateStr += filter.JisekiTo.ToShortDateString() + "～";
            }
            xls.Cell("A7").Value = "受講日：" + dateStr;
            xls.Cell("A8").Value = "対象：" + ( filter.Is受講済み ? "受講済" : "未受講" );

            int rowNo = 0;
            foreach (SiKoushu obj in koushuList)
            {
                if (obj.DeleteFlag == 1)
                    continue;

                col = 0;

                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = ++rowNo;
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = obj.SeninShimeiCode;
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = obj.SeninShokumei;
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = obj.SeninName;
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = obj.KoushuName;
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = obj.Basho;
                if (obj.JisekiFrom != DateTime.MinValue)
                {
                    xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = obj.JisekiFrom.ToShortDateString();
                }
                else
                {
                    col++;
                }
                if (obj.JisekiTo != DateTime.MinValue)
                {
                    xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = obj.JisekiTo.ToShortDateString();
                }
                else
                {
                    col++;
                }
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = obj.KoushuYukokigenStr;
                int attachCount = 0;
                foreach (SiKoushuAttachFile a in obj.AttachFiles)
                {
                    if (a.DeleteFlag == 0)
                        attachCount++;
                }
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = attachCount;

                row++;
            }
       }
    }
}
