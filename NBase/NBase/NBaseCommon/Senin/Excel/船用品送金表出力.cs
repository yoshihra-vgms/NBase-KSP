using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using ExcelCreator = AdvanceSoftware.ExcelCreator;

namespace NBaseCommon.Senin.Excel
{
    public class 船用品送金表出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;


        public 船用品送金表出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }
        
        
        public void CreateFile(MsUser loginUser, DateTime date, int msVesselId)
        {
            List<SoukinHolder> holders = CreateSoukinHolders(loginUser, date, msVesselId);

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

                xls.Cell("**NENGETSU").Value = date.Year + "年" + date.Month + "月";

                int startRow = 9;
                int no = 0;

                foreach (SoukinHolder h in holders)
                {
                    // 船名出力
                    xls.Cell("B" + (startRow + no)).Value = h.vessel.VesselName;
                    xls.Cell("B" + (startRow + no) + ":B" + (startRow + no + h.soukins.Count - 1)).Attr.MergeCells = true;

                    for (int i = 0; i < h.soukins.Count; i++)
                    {
                        SiSoukin s = h.soukins[i];

                        if (i == 0)
                        {
                            decimal 先月末残高 = SiJunbikin.Get_先月末残高(loginUser, date, h.vessel.MsVesselID);

                            // 前月残船用金
                            xls.Cell("C" + (startRow + no)).Value = 先月末残高;
                        }

                        // 番号
                        xls.Cell("A" + (startRow + no)).Value = (no + 1).ToString();

                        // 決定送金額
                        xls.Cell("D" + (startRow + no)).Value = s.Kingaku;

                        // 送金日
                        xls.Cell("E" + (startRow + no)).Value = s.SoukinDate.Month + "/" + s.SoukinDate.Day;

                        // 代理店
                        xls.Cell("F" + (startRow + no)).Value = s.CustomerName;

                        // 送金者
                        xls.Cell("G" + (startRow + no)).Value = s.SoukinUserName;

                        // 備考
                        xls.Cell("H" + (startRow + no)).Value = s.Bikou;

                        // 横罫線
                        xls.Cell("A" + (startRow + no) + ":H" + (startRow + no)).Attr.LineBottom(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                        no++;
                    }
                }

                if (holders.Count > 0)
                {
                    // 合計
                    xls.Cell("A" + (startRow + no)).Value = "合　　　　　　計";
                    xls.Cell("A" + (startRow + no)).Attr.HorizontalAlignment = ExcelCreator.HorizontalAlignment.Left;
                    xls.Cell("A" + (startRow + no) + ":B" + (startRow + no)).Attr.MergeCells = true;
                    // 前月残船用金
                    xls.Cell("C" + (startRow + no)).Value = "=SUM(C" + startRow + ":C" + (startRow + no - 1) + ")";
                    // 決定送金額
                    xls.Cell("D" + (startRow + no)).Value = "=SUM(D" + startRow + ":D" + (startRow + no - 1) + ")";
                    // 横罫線
                    xls.Cell("A" + (startRow + no) + ":H" + (startRow + no)).Attr.LineTop(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                    xls.Cell("A" + (startRow + no) + ":H" + (startRow + no)).Attr.LineBottom(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

                    // 縦罫線
                    xls.Cell("A" + (startRow) + ":A" + (startRow + no)).Attr.LineLeft(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
                    xls.Cell("B" + (startRow) + ":B" + (startRow + no)).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("C" + (startRow) + ":C" + (startRow + no)).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("D" + (startRow) + ":D" + (startRow + no)).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("E" + (startRow) + ":E" + (startRow + no)).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("F" + (startRow) + ":F" + (startRow + no)).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("G" + (startRow) + ":G" + (startRow + no)).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("H" + (startRow) + ":H" + (startRow + no)).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("H" + (startRow) + ":H" + (startRow + no)).Attr.LineRight ( ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
                }

                xls.CloseBook(true);
            }
        }


        private List<SoukinHolder> CreateSoukinHolders(MsUser loginUser, DateTime date, int msVesselId)
        {
            // <MsVesselID, List<SiSoukin>
            Dictionary<int, List<SiSoukin>> soukinDic = new Dictionary<int, List<SiSoukin>>();

            List<MsVessel> vessels = MsVessel.GetRecordsBySeninEnabled(loginUser);
            List<SiSoukin> soukins = SiSoukin.GetRecordsByDateAndMsVesselID(loginUser, date, date, msVesselId, "ORDER BY SOUKIN_DATE");

            foreach (SiSoukin s in soukins)
            {
                if (!soukinDic.ContainsKey(s.MsVesselID))
                {
                    soukinDic[s.MsVesselID] = new List<SiSoukin>();
                }

                soukinDic[s.MsVesselID].Add(s);
            }

            List<SoukinHolder> result = new List<SoukinHolder>();

            foreach (MsVessel v in vessels)
            {
                if (soukinDic.ContainsKey(v.MsVesselID))
                {
                    SoukinHolder h = new SoukinHolder();
                    h.vessel = v;
                    h.soukins = soukinDic[v.MsVesselID];

                    result.Add(h);
                }
            }

            return result;
        }


        private struct SoukinHolder
        {
            public MsVessel vessel;
            public List<SiSoukin> soukins;
        }
    }
}
