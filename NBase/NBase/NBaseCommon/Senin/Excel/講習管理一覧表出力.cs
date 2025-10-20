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
    public class 講習管理一覧表出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;

        private int startRow = 0;

        public 講習管理一覧表出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }


        public void CreateFile(MsUser loginUser, List<SiKoushu> koushuList)
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

                if (koushuList != null)
                {
                    int row = 0;
                    SetRows(loginUser, xls, koushuList, ref row);
                    Draw_縦罫線(xls, row);
                }

                xls.CloseBook(true);
            }
        }

        private void SetRows(MsUser loginUser,
            ExcelCreator.Xlsx.XlsxCreator xls,
            List<SiKoushu> koushuList,
            ref int row)
        {
            int col = 0;

            xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = "講習名";
            xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = "講習場所";
            xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = "開始予定日";
            xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = "終了予定日";
            xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = "コード";
            xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = "職名";
            xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = "氏名";
            xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = "受講開始日";
            xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = "受講終了日";
            xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = "有効期間";
            xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = "備考";
            xls.Cell(ExcelUtils.ToCellName(0, row) + ":" + ExcelUtils.ToCellName(col - 1, row)).Attr.LineTop ( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell(ExcelUtils.ToCellName(0, row + 1) + ":" + ExcelUtils.ToCellName(col - 1, row + 1)).Attr.LineBottom ( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

            row++;

            foreach (SiKoushu k in koushuList)
            {
                if (k.DeleteFlag == 1)
                    continue;


                col = 0;

                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = k.KoushuName;
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = k.Basho;
                if (k.YoteiFrom != DateTime.MinValue)
                {
                    xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = k.YoteiFrom.ToShortDateString();
                }
                else
                {
                    col++;
                }
                if (k.YoteiTo != DateTime.MinValue)
                {
                    xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = k.YoteiTo.ToShortDateString();
                }
                else
                {
                    col++;
                }
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = k.SeninShimeiCode;
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = k.SeninShokumei;
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = k.SeninName;
                if (k.JisekiFrom != DateTime.MinValue)
                {
                    xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = k.JisekiFrom.ToShortDateString();
                }
                else
                {
                    col++;
                }
                if (k.JisekiTo != DateTime.MinValue)
                {
                    xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = k.JisekiTo.ToShortDateString();
                }
                else
                {
                    col++;
                }
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = k.KoushuYukokigenStr;
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = k.Bikou;

                // 横罫線
                xls.Cell(ExcelUtils.ToCellName(0, row) + ":" + ExcelUtils.ToCellName(col - 1, row)).Attr.LineTop (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell(ExcelUtils.ToCellName(0, row + 1) + ":" + ExcelUtils.ToCellName(col - 1, row + 1)).Attr.LineBottom ( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                row++;
            }
       }

        private void Draw_縦罫線(ExcelCreator.Xlsx.XlsxCreator xls, int row)
        {
            xls.Cell(ExcelUtils.ToCellName(0, 0) + ":" + ExcelUtils.ToCellName(0, row)).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            for (int i = 0; i <= 10; i++)
            {
                xls.Cell(ExcelUtils.ToCellName(i, 0) + ":" + ExcelUtils.ToCellName(i, row)).Attr.LineRight( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            }
        }
    }
}
