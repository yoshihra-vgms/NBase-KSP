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
    public class 免許免状一覧出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;

        public 免許免状一覧出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }

        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache, SiMenjouFilter filter, List<SiMenjou> menjouList)
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

                if (menjouList != null)
                {

                    List<MsSenin> seninList = MsSenin.GetRecords(loginUser);
                    List<RowData> rowDataList = RowData.BuildRowData(loginUser, seninTableCache, menjouList, seninList);

                    int row = 8;
                    SetRows(loginUser, xls, seninTableCache, filter, rowDataList, ref row);
                    
                    // 印刷範囲の設定
                    int printRangeRow = (8 + ((row - 9) / 25) * 25);
                    if (((row - 9) % 25) > 0)
                    {
                        printRangeRow += 25;
                    }
                    xls.PrintArea(0, 0, 8, printRangeRow);
                }

                xls.CloseBook(true);
            }
        }

        private void SetRows(MsUser loginUser,
            ExcelCreator.Xlsx.XlsxCreator xls,
            SeninTableCache seninTableCache,
            SiMenjouFilter filter,
            List<RowData> rowDataList,
            ref int row)
        {
            int col = 0;

            string names = "";
            foreach (int id in filter.MsSiMenjouIDs)
            {
                names += "," + seninTableCache.GetMsSiMenjouName(loginUser, id);
            }
            foreach (int id in filter.MsSiMenjouKindIDs)
            {
                names += "," + seninTableCache.GetMsSiMenjouKindName(loginUser, id);
            }
            xls.Cell("A2").Value = "免許・免状名/種別：" + (names.Length > 0 ? names.Substring(1) : "");
            names = "";
            foreach (int id in filter.MsSiShokumeiIDs)
            {
                names += "," + seninTableCache.GetMsSiShokumeiName(loginUser, id);
            }
            xls.Cell("A3").Value = "職名：" + (names.Length > 0 ? names.Substring(1) : "");
            xls.Cell("A4").Value = "氏名コード：" + filter.ShimeiCode;
            xls.Cell("A5").Value = "氏名：" + filter.Name;
            xls.Cell("A6").Value = "有効期限：" + ( filter.Yukokigen > 0 ? filter.Yukokigen.ToString() + "ヶ月" : "");
            xls.Cell("A7").Value = "対象：" + (filter.is取得済 ? "取得済" : "未取得");


            row++;

            int rowNo = 0;
            foreach (RowData obj in rowDataList)
            {
                col = 0;

                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = (++rowNo);
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = obj.msSenin.ShimeiCode;
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, obj.msSenin.MsSiShokumeiID);
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = obj.msSenin.Sei + " " + obj.msSenin.Mei;
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = obj.msSiMenjou.Name;
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = obj.msSiMenjouKind == null ? "" : obj.msSiMenjouKind.Name;
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = obj.menjou.No;
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = (obj.menjou.Kigen == DateTime.MinValue ? "" : obj.menjou.Kigen.ToShortDateString());
                xls.Cell(ExcelUtils.ToCellName(col++, row)).Value = (obj.menjou.ShutokuDate == DateTime.MinValue ? "" : obj.menjou.ShutokuDate.ToShortDateString());

                row++;
            }
       }

        private class RowData
        {
            public SiMenjou menjou = null;

            public MsSenin msSenin = null;
            public MsSiMenjou msSiMenjou = null;
            public MsSiMenjouKind msSiMenjouKind = null;

            public static List<RowData> BuildRowData(MsUser loginUser,
                                                    SeninTableCache seninTableCache,
                                                    List<SiMenjou> menjouList, 
                                                    List<MsSenin> seninList)
            {
                List<RowData> ret = new List<RowData>();

                Dictionary<int, MsSenin> seninDic = new Dictionary<int, MsSenin>();
                foreach (MsSenin senin in seninList)
                {
                    seninDic.Add(senin.MsSeninID, senin);
                }

                foreach (SiMenjou menjou in menjouList)
                {
                    if (seninDic.ContainsKey(menjou.MsSeninID) == false)
                        continue;

                    RowData row = new RowData();

                    row.menjou = menjou;
                    row.msSenin = seninDic[menjou.MsSeninID];
                    row.msSiMenjou = seninTableCache.GetMsSiMenjou(loginUser, menjou.MsSiMenjouID);
                    row.msSiMenjouKind = seninTableCache.GetMsSiMenjouKind(loginUser, menjou.MsSiMenjouKindID);
                    if (row.msSiMenjouKind == null)
                        row.msSiMenjouKind = new MsSiMenjouKind();

                    ret.Add(row);
                }

                return ret.OrderBy(obj => obj.msSenin.ShimeiCode).ThenBy(obj => obj.msSiMenjou.ShowOrder).ThenBy(obj => obj.msSiMenjouKind.ShowOrder).ThenBy(obj => obj.menjou.ShutokuDate).ToList();
            }
        }
    }
}
