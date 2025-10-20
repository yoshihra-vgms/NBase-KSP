using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using NBaseData.DS;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using System.Drawing;
using NBaseUtil;
using System.Globalization;
using NBaseData.BLC;

namespace NBaseCommon.Senin.Excel
{
    public class tek手当一覧出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;


        public tek手当一覧出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }

        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache, string from, string to, int vesselId, string allowanceName)
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

                _CreateFile(loginUser, xls, seninTableCache, from, to, vesselId, allowanceName);

                xls.FullCalcOnLoad = true;
                xls.CloseBook(true);
            }
        }


        private void _CreateFile(MsUser loginUser, ExcelCreator.Xlsx.XlsxCreator xls, SeninTableCache seninTableCache, string from, string to, int vesselId, string allowanceName)
        {
            List<SiAllowance> allowances = SiAllowance.GetRecords(loginUser, from, to, vesselId, allowanceName);

            for(int i = 2; i <= allowances.Count(); i ++)
            {
                xls.RowCopy(1, i);
            }

            int index = 2;
            foreach (SiAllowance row in allowances)
            {
                xls.Cell("A" + index.ToString()).Value = $"{row.YearMonth.Substring(0, 4)}年{row.YearMonth.Substring(4, 2)}月";
                xls.Cell("B" + index.ToString()).Value = seninTableCache.GetMsVesselName(loginUser, row.MsVesselID);
                xls.Cell("C" + index.ToString()).Value = seninTableCache.GetMsSiAllowanceName(loginUser, row.MsSiAllowanceID);
                xls.Cell("D" + index.ToString()).Value = row.PersonInCharge;
                xls.Cell("E" + index.ToString()).Value = NBaseCommon.Common.金額出力(row.Allowance);
                index++;
            }
        }
    }
}
