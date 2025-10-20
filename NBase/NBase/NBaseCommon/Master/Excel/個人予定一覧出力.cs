using ExcelCreator = AdvanceSoftware.ExcelCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;

namespace NBaseCommon.Master.Excel
{
    public class 個人予定一覧出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;


        public 個人予定一覧出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }

        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache, List<SiPersonalSchedule> personalScheduleList)
        {
            //using (ExcelCreator.Xlsx.Creator xls = new ExcelCreator.XlsCreator())
            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                xls.OpenBook(outputFilePath, templateFilePath);

                _CreateFile(loginUser, xls, seninTableCache, personalScheduleList);

                xls.CloseBook(true);
            }
        }

        private void _CreateFile(MsUser loginUser, ExcelCreator.Xlsx.XlsxCreator xls, SeninTableCache seninTableCache, List<SiPersonalSchedule> personalScheduleList)
        {
            xls.SheetNo = 0;

            int rowNo = 1;
            int startRowIndex = 4;
            int rowIndex = startRowIndex + 1;
            foreach (SiPersonalSchedule info in personalScheduleList)
            {
                xls.RowCopy(startRowIndex, rowIndex);

                int colIndex = 0;

                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = rowNo;
                colIndex++;

                // 職名
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, info.MsSiShokumeiID);
                colIndex++;

                // 氏名
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.Name;
                colIndex++;

                // 開始日
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.FromDate.ToShortDateString();
                colIndex++;

                // 終了日
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.ToDate.ToShortDateString();
                colIndex++;

                // 内容
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.Bikou;
                colIndex++;


                rowNo++;
                rowIndex++;
            }
            xls.RowDelete(startRowIndex, 1);
        }
    }



}
