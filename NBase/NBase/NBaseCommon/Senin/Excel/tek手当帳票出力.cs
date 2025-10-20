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
    public class tek手当帳票出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;


        public tek手当帳票出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }

        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache, DateTime ym)
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

                _CreateFile(loginUser, xls, seninTableCache, ym);

                xls.FullCalcOnLoad = true;
                xls.CloseBook(true);
            }
        }


        private void _CreateFile(MsUser loginUser, ExcelCreator.Xlsx.XlsxCreator xlsx, SeninTableCache seninTableCache, DateTime ym)
        {
            List<MsVessel> vesselList = seninTableCache.GetMsVesselList(loginUser);
            List<MsSiAllowance> allowanceList = seninTableCache.GetMsSiAllowanceList(loginUser);
            List<MsSiShokumei> shokumeiList = seninTableCache.GetMsSiShokumeiList(loginUser);
            List<MsSenin> seninList = MsSenin.GetRecords(loginUser);

            var ymStr = ym.ToString("yyyyMM");
            List<SiAllowance> allowances = SiAllowance.GetRecords(loginUser, ymStr, ymStr, 0, null);

            var sortedVesselList = vesselList.OrderBy(o => o.ShowOrder);
            var sortedAllowanceList = allowanceList.OrderBy(o => o.ShowOrder).ThenBy(o => o.Name);

            int i = 1;
            foreach (MsVessel v in sortedVesselList)
            {
                // シートのコピー
                xlsx.CopySheet(0, i, v.VesselName);
                xlsx.SheetNo = i;
                i++;

                xlsx.Cell("**Nengetsu").Value = $"{ymStr.Substring(0, 4)}年{ymStr.Substring(4, 2)}月";
                xlsx.Cell("**VesselName").Value = v.VesselName;


                var allowancesInVessel = allowances.Where(o => o.MsVesselID == v.MsVesselID);

                List<SiAllowanceDetail> allowanceDetails = new List<SiAllowanceDetail>();
                foreach(SiAllowance a in allowancesInVessel)
                {
                    allowanceDetails.AddRange(SiAllowanceDetail.GetRecords(loginUser, a.SiAllowanceID));
                }

                var seninIds = allowanceDetails.Select(o => o.MsSeninID).Distinct();

                List<MsSenin> senins = new List<MsSenin>();
                foreach(int id in seninIds)
                {
                    var s = seninList.Where(o => o.MsSeninID == id).FirstOrDefault();
                    if (s != null)
                        senins.Add(s);
                }
                var sortedShokumeiList = shokumeiList.OrderBy(o => o.ShowOrder);
                List<MsSenin> sortedSeninList = new List<MsSenin>();
                foreach(MsSiShokumei s in sortedShokumeiList)
                {
                    if (senins.Any(o => o.MsSiShokumeiID == s.MsSiShokumeiID))
                    {
                        var seninsIsShokumei = senins.Where(o => o.MsSiShokumeiID == s.MsSiShokumeiID);
                        sortedSeninList.AddRange(seninsIsShokumei);
                    }
                }

                int rows = 4;
                foreach (MsSenin s in sortedSeninList)
                {
                    xlsx.Cell("A" + rows.ToString()).Value = $"{s.Sei} {s.Mei}";
                    rows++;
                }

                int cols = 2;
                foreach (MsSiAllowance a in sortedAllowanceList)
                {
                    rows = 2;

                    var targetAllowances = allowancesInVessel.Where(o => o.MsSiAllowanceID == a.MsSiAllowanceID);
                    if (targetAllowances == null || targetAllowances.Count() == 0)
                    {
                        continue;
                    }

                    xlsx.Cell(ExcelUtils.ToCellName(cols, rows)).Value = a.Name;


                    var allowanceIds = targetAllowances.Select(o => o.SiAllowanceID);
                    var details = allowanceDetails.Where(o => allowanceIds.Contains(o.SiAllowanceID));

                    rows ++;
                    foreach (MsSenin s in sortedSeninList)
                    {
                        var totalAllowace = 0;
                        if (details.Any(o => o.MsSeninID == s.MsSeninID))
                        {
                            totalAllowace = details.Where(o => o.MsSeninID == s.MsSeninID).Sum(o => o.Allowance);
                        }
                        if (totalAllowace != 0)
                        {
                            xlsx.Cell(ExcelUtils.ToCellName(cols, rows)).Value = totalAllowace;
                        }

                        rows++;
                    }

                    cols++;
                }
            }

            xlsx.DeleteSheet(0, 1);
        }
    }
}
