//using ExcelCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;
using ExcelCreator = AdvanceSoftware.ExcelCreator;

namespace NBaseCommon.Master.Excel
{
    public class 乗り合わせ一覧出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;

        public 乗り合わせ一覧出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }

        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache, List<SiFellowPassengers> fellowPassengersList)
        {
            //using (ExcelCreator.XlsCreator xls = new ExcelCreator.XlsCreator())
            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                xls.OpenBook(outputFilePath, templateFilePath);

                _CreateFile(loginUser, xls, seninTableCache, fellowPassengersList);

                xls.CloseBook(true);
            }
        }

        //private void _CreateFile(MsUser loginUser, XlsCreator xls, SeninTableCache seninTableCache, List<SiFellowPassengers> fellowPassengersList)
        private void _CreateFile(MsUser loginUser, ExcelCreator.Xlsx.XlsxCreator xls, SeninTableCache seninTableCache, List<SiFellowPassengers> fellowPassengersList)
        {
            xls.SheetNo = 0;

            int rowNo = 1;
            int startRowIndex = 4;
            int rowIndex = startRowIndex + 1;
            foreach (SiFellowPassengers info in fellowPassengersList)
            {
                xls.RowCopy(startRowIndex, rowIndex);


                int colIndex = 0;

                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = rowNo;
                colIndex++;

                // 職名
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, info.MsSiShokumeiID1);
                colIndex++;

                // 氏名
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.Name1;
                colIndex++;

                // 職名
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, info.MsSiShokumeiID2);
                colIndex++;

                // 氏名
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.Name2;
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
