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
    public class tek手当支給依頼書出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;


        public tek手当支給依頼書出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }

        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache, SiAllowance allowance, List<SiAllowanceDetail> details)
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

                _CreateFile(loginUser, xls, seninTableCache, allowance, details);

                xls.FullCalcOnLoad = true;
                xls.CloseBook(true);
            }
        }


        private void _CreateFile(MsUser loginUser, ExcelCreator.Xlsx.XlsxCreator xls, SeninTableCache seninTableCache, SiAllowance allowance, List<SiAllowanceDetail> details)
        {
            xls.Cell("**手当名").Value = seninTableCache.GetMsSiAllowanceName(loginUser, allowance.MsSiAllowanceID);
            xls.Cell("**船名").Value = seninTableCache.GetMsVesselName(loginUser, allowance.MsVesselID);

            var captain = MsSenin.GetRecord(loginUser, allowance.CaptainSeninID);
            xls.Cell("**船長名").Value = captain != null ? captain.FullName : "";

            int year = int.Parse(allowance.YearMonth.Substring(0, 4));
            int month = int.Parse(allowance.YearMonth.Substring(4, 2));
            DateTime from = new DateTime(year, month, 1);
            DateTime to = from.AddMonths(1).AddDays(-1);

            xls.Cell("**開始年").Value = from;
            xls.Cell("**開始月").Value = from.Month;
            xls.Cell("**開始日").Value = from.Day;

            xls.Cell("**終了年").Value = to;
            xls.Cell("**終了月").Value = to.Month;
            xls.Cell("**終了日").Value = to.Day;

            xls.Cell("**内容").Value = allowance.Contents;

            xls.Cell("**数量").Value = allowance.Quantity;
            xls.Cell("**金額").Value = allowance.Allowance;

            xls.Cell("**責任者").Value = allowance.PersonInCharge;

            int index = 1;
            decimal sum = 0;
            List<MsSenin> senins = new List<MsSenin>();
            foreach(SiAllowanceDetail detail in details)
            {
                senins.Add(MsSenin.GetRecord(loginUser, detail.MsSeninID));
            }
            foreach (MsSiShokumei s in seninTableCache.GetMsSiShokumeiList(loginUser))
            {
                var work = details.Where(o => o.MsSiShokumeiID == s.MsSiShokumeiID && o.IsTarget == 1);
                if (work != null)
                {
                    foreach(SiAllowanceDetail d in work)
                    {
                        var senin = senins.Where(o => o.MsSeninID == d.MsSeninID).First();

                        xls.Cell("**職名" + index.ToString()).Value = s.NameAbbr;
                        xls.Cell("**氏名" + index.ToString()).Value = senin.FullName;
                        xls.Cell("**金額" + index.ToString()).Value = d.Allowance;

                        sum += d.Allowance;

                        index++;
                    }
                }
            }
            xls.Cell("**支給額").Value = sum;

        }
    }
}
