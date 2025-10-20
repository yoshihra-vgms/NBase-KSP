using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;
using System.IO;
using NBaseData.DAC;
using System.Collections.Generic;
using ExcelCreator= AdvanceSoftware.ExcelCreator;
using NBaseCommon;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_貯蔵品集計表_取得(NBaseData.DAC.MsUser loginUser, int year, List<bool> targets, NBaseData.BLC.貯蔵品集計表.Enum対象 outputKind);
    }

    public partial class Service
    {
        public byte[] BLC_貯蔵品集計表_取得(NBaseData.DAC.MsUser loginUser, int year, List<bool> targets, NBaseData.BLC.貯蔵品集計表.Enum対象 outputKind)
        {
            #region 元になるファイルの確認と出力ファイル生成
            string BaseFileName = "";
            if (outputKind == NBaseData.BLC.貯蔵品集計表.Enum対象.潤滑油)
            {
                BaseFileName = "潤滑油集計表";
            }
            else
            {
                BaseFileName = "船用品集計表";
            }
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateName = path + "Template_" + BaseFileName + ".xlsx";

            string outpath = NBaseUtil.FileUtils.CheckOutPath(path) + "\\";
            string outBaseFileName = BaseFileName + "_" + DateTime.Now.ToString("HHmmss");

            string outPutFileName = outpath + "outPut_[" + loginUser.FullName + "]_" + outBaseFileName + ".xlsx";

            bool exists = System.IO.File.Exists(templateName);
            if (exists == false)
            {
                return null;
            }
            #endregion

            try
            {

            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                if (BaseFileName != null && BaseFileName.Length != 0)
                {
                    // 指定されたテンプレートを元にファイルを作成
                    xls.OpenBook(outPutFileName, templateName);
                }

                // 出力対象となる船
                List<MsVessel> msVesselList = MsVessel.GetRecordsByHachuEnabled(loginUser);
                
                int offset_y = 0;
                int start_y = 0;

                //========================================
                // １シート目「潤滑油費月末集計表」
                //========================================
LogFile.Write(loginUser.FullName, "１シート目");
                xls.SheetNo = 0;
                xls.Header("", year.ToString() + "年　" + "&A", "&D");

                // 月末データ
                Dictionary<int, NBaseData.BLC.月末集計表データ> 月末Datas = null;
                if (outputKind == NBaseData.BLC.貯蔵品集計表.Enum対象.潤滑油)
                {
                    //月末Datas = NBaseData.BLC.貯蔵品集計表.GetRecords潤滑油_受入(loginUser, year);
                    月末Datas = NBaseData.BLC.貯蔵品集計表.GetRecords潤滑油_月末(loginUser, year, targets);
                }
                else
                {
                    //月末Datas = NBaseData.BLC.貯蔵品集計表.GetRecords船用品_受入(loginUser, year);
                    月末Datas = NBaseData.BLC.貯蔵品集計表.GetRecords船用品_月末(loginUser, year, targets);
                }
                #region
                offset_y = 0;
                start_y = 7;
                xls.RowInsert(start_y, msVesselList.Count-2);
                for (int i = 0; i < msVesselList.Count-2; i++ )
                {
                    xls.RowCopy(start_y-1, start_y+i);
                }
                foreach (MsVessel msVessel in msVesselList)
                {
                    // 船名
                    #region
                    xls.Cell("B7", 0, offset_y).Value = msVessel.VesselName;
                    #endregion

                    // 各月のデータ（横方向）
                    #region
                    if (月末Datas.ContainsKey(msVessel.MsVesselID))
                    {
                        NBaseData.BLC.月末集計表データ data = 月末Datas[msVessel.MsVesselID];

                        if (targets[0])
                        {
                            xls.Cell(GetCell("C",(start_y + offset_y))).Value = data.Amounts[4];
                            xls.Cell(GetCell("D",(start_y + offset_y))).Value = data.Amounts[5];
                            xls.Cell(GetCell("E",(start_y + offset_y))).Value = data.Amounts[6];
                        }
                        if (targets[1])
                        {
                            xls.Cell(GetCell("G", (start_y + offset_y))).Value = data.Amounts[7];
                            xls.Cell(GetCell("H", (start_y + offset_y))).Value = data.Amounts[8];
                            xls.Cell(GetCell("I", (start_y + offset_y))).Value = data.Amounts[9];
                        }
                        if (targets[2])
                        {
                            xls.Cell(GetCell("L", (start_y + offset_y))).Value = data.Amounts[10];
                            xls.Cell(GetCell("M", (start_y + offset_y))).Value = data.Amounts[11];
                            xls.Cell(GetCell("N", (start_y + offset_y))).Value = data.Amounts[12];
                        }
                        if (targets[3])
                        {
                            xls.Cell(GetCell("P", (start_y + offset_y))).Value = data.Amounts[1];
                            xls.Cell(GetCell("Q", (start_y + offset_y))).Value = data.Amounts[2];
                            xls.Cell(GetCell("R", (start_y + offset_y))).Value = data.Amounts[3];
                        }
                    }
                    #endregion

                    offset_y++;
                }
                #endregion


LogFile.Write(loginUser.FullName, "２シート目");
                //========================================
                // ２シート目「潤滑油費期末集計表」
                //========================================
                xls.SheetNo = 1;
                xls.Header("", year.ToString() + "年　" + "&A", "&D");
                
                // 期末データ
                Dictionary<int, NBaseData.BLC.期末集計表データ> 期末Datas = null;
                if (outputKind == NBaseData.BLC.貯蔵品集計表.Enum対象.潤滑油)
                {
                    月末Datas = NBaseData.BLC.貯蔵品集計表.GetRecords潤滑油_受入(loginUser, year);
                    期末Datas = NBaseData.BLC.貯蔵品集計表.GetRecords潤滑油_期末(loginUser, year, targets);
                }
                else
                {
                    月末Datas = NBaseData.BLC.貯蔵品集計表.GetRecords船用品_受入(loginUser, year);
                    期末Datas = NBaseData.BLC.貯蔵品集計表.GetRecords船用品_期末(loginUser, year, targets);
                }
                
                #region
                offset_y = 0;
                start_y = 8;
                xls.RowInsert(start_y, msVesselList.Count - 2);
                for (int i = 0; i < msVesselList.Count - 1; i++)
                {
                    xls.RowCopy(start_y - 1, start_y + i);
                }

                foreach (MsVessel msVessel in msVesselList)
                {
                    // 期間内受入
                    if (月末Datas.ContainsKey(msVessel.MsVesselID))
                    {
                        NBaseData.BLC.月末集計表データ data = 月末Datas[msVessel.MsVesselID];
                        string cells = "";
                        xls.Cell(GetCell("E", (start_y + offset_y))).Value = (data.Amounts[4] + data.Amounts[5] + data.Amounts[6]);
                        xls.Cell(GetCell("F", (start_y + offset_y))).Value = (data.Amounts[7] + data.Amounts[8] + data.Amounts[9]);
                        xls.Cell(GetCell("G", (start_y + offset_y))).Value = (data.Amounts[10] + data.Amounts[11] + data.Amounts[12]);
                        xls.Cell(GetCell("H", (start_y + offset_y))).Value = (data.Amounts[1] + data.Amounts[2] + data.Amounts[3]);
                        if (targets[0])
                        {
                            cells += GetCell("E", (start_y + offset_y));
                        }
                        if (targets[1])
                        {
                            if (cells.Length > 0)
                            {
                                cells += ",";
                            }
                            cells += GetCell("F", (start_y + offset_y));
                        }
                        if (targets[2])
                        {
                            if (cells.Length > 0)
                            {
                                cells += ",";
                            }
                            cells += GetCell("G", (start_y + offset_y));
                        }
                        if (targets[3])
                        {
                            if (cells.Length > 0)
                            {
                                cells += ",";
                            }
                            cells += GetCell("H", (start_y + offset_y));
                        }
                        xls.Cell(GetCell("I", (start_y + offset_y))).Value = "=SUM(" + cells + ")";
                    }


                    if (期末Datas.ContainsKey(msVessel.MsVesselID))
                    {
                        NBaseData.BLC.期末集計表データ data = 期末Datas[msVessel.MsVesselID];

                        // 前回繰越
                        NBaseData.BLC.期末集計表データ.期末Enum 前回繰越 = NBaseData.BLC.期末集計表データ.期末Enum.繰越;
                        if (targets[0])
                        {
                            xls.Cell("C6").Value = "期首(3/31)残";
                            前回繰越 = NBaseData.BLC.期末集計表データ.期末Enum.繰越;
                        }
                        else if (targets[1])
                        {
                            xls.Cell("C6").Value = "第一(6/30)残";
                            前回繰越 = NBaseData.BLC.期末集計表データ.期末Enum.第一;
                        }
                        else if (targets[2])
                        {
                            xls.Cell("C6").Value = "第二(9/30)残";
                            前回繰越 = NBaseData.BLC.期末集計表データ.期末Enum.第二;
                        }
                        else if (targets[3])
                        {
                            xls.Cell("C6").Value = "第三(12/31)残";
                            前回繰越 = NBaseData.BLC.期末集計表データ.期末Enum.第三;
                        }
                        //xls.Cell(GetCell("C", (start_y + offset_y))).Value = data.Counts[前回繰越];
                        xls.Cell(GetCell("D", (start_y + offset_y))).Value = data.Amounts[前回繰越];


                        // 期間後繰越
                        xls.Cell(GetCell("J", (start_y + offset_y))).Value = data.Amounts[NBaseData.BLC.期末集計表データ.期末Enum.第一];
                        xls.Cell(GetCell("K", (start_y + offset_y))).Value = data.Amounts[NBaseData.BLC.期末集計表データ.期末Enum.第二];
                        xls.Cell(GetCell("L", (start_y + offset_y))).Value = data.Amounts[NBaseData.BLC.期末集計表データ.期末Enum.第三];
                        xls.Cell(GetCell("M", (start_y + offset_y))).Value = data.Amounts[NBaseData.BLC.期末集計表データ.期末Enum.第四];
                        decimal 期間後繰越合計 = 0;
                        if (targets[0])
                        {
                            期間後繰越合計 += data.Amounts[NBaseData.BLC.期末集計表データ.期末Enum.第一];
                        }
                        if (targets[1])
                        {
                            期間後繰越合計 += data.Amounts[NBaseData.BLC.期末集計表データ.期末Enum.第二];
                        }
                        if (targets[2])
                        {
                            期間後繰越合計 += data.Amounts[NBaseData.BLC.期末集計表データ.期末Enum.第三];
                        }
                        if (targets[3])
                        {
                            期間後繰越合計 += data.Amounts[NBaseData.BLC.期末集計表データ.期末Enum.第四];
                        }
                        xls.Cell(GetCell("N", (start_y + offset_y))).Value = 期間後繰越合計;

                        // 期間内消費
                        decimal 期間内消費 = 0;
                        decimal 期間内消費合計 = 0;
                        if (targets[0])
                        {
                            期間内消費 = 0;
                            期間内消費 = GetCellValue(xls, GetCell("D", (start_y + offset_y)));
                            期間内消費 += GetCellValue(xls, GetCell("E", (start_y + offset_y)));
                            期間内消費 -= GetCellValue(xls, GetCell("J", (start_y + offset_y)));
                            期間内消費合計 += 期間内消費;
                            xls.Cell(GetCell("O", (start_y + offset_y))).Value = 期間内消費;
                        }
                        if (targets[1])
                        {
                            期間内消費 = 0;
                            期間内消費 = GetCellValue(xls, GetCell("J", (start_y + offset_y)));
                            期間内消費 += GetCellValue(xls, GetCell("F", (start_y + offset_y)));
                            期間内消費 -= GetCellValue(xls, GetCell("K", (start_y + offset_y)));
                            期間内消費合計 += 期間内消費;
                            xls.Cell(GetCell("P", (start_y + offset_y))).Value = 期間内消費;
                        }
                        if (targets[2])
                        {
                            期間内消費 = 0;
                            期間内消費 = GetCellValue(xls, GetCell("K", (start_y + offset_y)));
                            期間内消費 += GetCellValue(xls, GetCell("G", (start_y + offset_y)));
                            期間内消費 -= GetCellValue(xls, GetCell("L", (start_y + offset_y)));
                            期間内消費合計 += 期間内消費;
                            xls.Cell(GetCell("Q", (start_y + offset_y))).Value = 期間内消費;
                        }
                        if (targets[3])
                        {
                            期間内消費 = 0;
                            期間内消費 = GetCellValue(xls, GetCell("L", (start_y + offset_y)));
                            期間内消費 += GetCellValue(xls, GetCell("H", (start_y + offset_y)));
                            期間内消費 -= GetCellValue(xls, GetCell("M", (start_y + offset_y)));
                            期間内消費合計 += 期間内消費;
                            xls.Cell(GetCell("R", (start_y + offset_y))).Value = 期間内消費;
                        }
                        xls.Cell(GetCell("S", (start_y + offset_y))).Value = 期間内消費合計;
                        
                        // 本年度予算
                        xls.Cell(GetCell("T", (start_y + offset_y))).Value = data.Yosan;
                        
                        // 消費/予算
                        if (data.Yosan > 0 && 期間内消費合計 > 0)
                        {
                            xls.Cell(GetCell("U", (start_y + offset_y))).Value = (期間内消費合計 / data.Yosan);
                        }
                        else
                        {
                            xls.Cell(GetCell("U", (start_y + offset_y))).Value = 0;
                        }
                    }
                    offset_y++;
                }
                
                // 期間内消費量の非表示列を削除する
                int col = 17;
                if (!targets[3])
                {
                    xls.ColumnDelete(col, 1);
                }
                col--;
                if (!targets[2])
                {
                    xls.ColumnDelete(col, 1);
                }
                col--;
                if (!targets[1])
                {
                    xls.ColumnDelete(col, 1);
                }
                col--;
                if (!targets[0])
                {
                    xls.ColumnDelete(col, 1);
                }

                // 期間後繰越の非表示列を削除する
                col = 12;
                if (!targets[3])
                {
                    xls.ColumnDelete(col, 1);
                }
                col--;
                if (!targets[2])
                {
                    xls.ColumnDelete(col, 1);
                }
                col--;
                if (!targets[1])
                {
                    xls.ColumnDelete(col, 1);
                }
                col--;
                if (!targets[0])
                {
                    xls.ColumnDelete(col, 1);
                }

                // 期間内受入の非表示列を削除する
                col = 7;
                if (!targets[3])
                {
                    xls.ColumnDelete(col, 1);
                }
                col--;
                if (!targets[2])
                {
                    xls.ColumnDelete(col, 1);
                }
                col--;
                if (!targets[1])
                {
                    xls.ColumnDelete(col, 1);
                }
                col--;
                if (!targets[0])
                {
                    xls.ColumnDelete(col, 1);
                }

                //
                string min_期間内受入 = "E5";
                string min_期間後繰越 = "G5";
                string min_期間内消費量 = "I5";
                int offset_期間後繰越 = -1;
                int offset_期間内消費量 = 0;

                if (targets[0])
                {
                    offset_期間後繰越++;
                }
                if (targets[1])
                {
                    offset_期間後繰越++;
                }
                if (targets[2])
                {
                    offset_期間後繰越++;
                }
                if (targets[3])
                {
                    offset_期間後繰越++;
                }
                offset_期間内消費量 = offset_期間後繰越 * 2;
                xls.Cell(min_期間内受入).Value = "期間内受入（Ｂ）";
                xls.Cell(min_期間後繰越, offset_期間後繰越, 0).Value = "期間後繰越（Ｃ）";
                xls.Cell(min_期間内消費量, offset_期間内消費量, 0).Value = "期間内消費量［（A)+（B)-（C)］";
                #endregion


LogFile.Write(loginUser.FullName, "３シート目");
                //========================================
                // ３シート目「潤滑油費繰越集計表」
                //========================================
                xls.SheetNo = 2;
                xls.Header("", year.ToString() + "年　" + "&A", "&D");

                #region
                offset_y = 0;
                start_y = 4;
                xls.RowInsert(start_y, msVesselList.Count - 2);
                for (int i = 0; i < msVesselList.Count - 1; i++)
                {
                    xls.RowCopy(start_y - 1, start_y + i);
                }
                foreach (MsVessel msVessel in msVesselList)
                {
                    if (期末Datas.ContainsKey(msVessel.MsVesselID))
                    {
                        NBaseData.BLC.期末集計表データ data = 期末Datas[msVessel.MsVesselID];

                        xls.Cell(GetCell("C", (start_y + offset_y))).Value = data.Amounts[NBaseData.BLC.期末集計表データ.期末Enum.繰越];
                        if (targets[0])
                        {
                            xls.Cell(GetCell("D", (start_y + offset_y))).Value = data.Amounts[NBaseData.BLC.期末集計表データ.期末Enum.第一];
                        }
                        if (targets[1])
                        {
                            xls.Cell(GetCell("E", (start_y + offset_y))).Value = data.Amounts[NBaseData.BLC.期末集計表データ.期末Enum.第二];
                        }
                        if (targets[2])
                        {
                            xls.Cell(GetCell("F", (start_y + offset_y))).Value = data.Amounts[NBaseData.BLC.期末集計表データ.期末Enum.第三];
                        }
                        if (targets[3])
                        {
                            xls.Cell(GetCell("G", (start_y + offset_y))).Value = data.Amounts[NBaseData.BLC.期末集計表データ.期末Enum.第四];
                        }
                    }
                    offset_y++;
                }
                #endregion

                // ファイルクローズ
                xls.CloseBook(true);
            }
            }
            catch(Exception e)
            {
                LogFile.Write(loginUser.FullName, "Error:" + e.Message);
                if (e.InnerException != null)
                    LogFile.Write(loginUser.FullName, "InnerException:" + e.InnerException.Message);
            }

            byte[] bytBuffer;
            #region バイナリーへ変換
            using (FileStream objFileStream = new FileStream(outPutFileName, FileMode.Open))
            {
                long lngFileSize = objFileStream.Length;

                bytBuffer = new byte[(int)lngFileSize];
                objFileStream.Read(bytBuffer, 0, (int)lngFileSize);
                objFileStream.Close();
            }
            #endregion

            return bytBuffer;
        }

        private string GetCell(string prefix, int no)
        {
            return prefix + no.ToString();
        }

        private decimal GetCellValue(ExcelCreator.Xlsx.XlsxCreator xls, string cellStr)
        {
            decimal retDec = 0;
            try
            {
                retDec = decimal.Parse(xls.Cell(cellStr).Str);
            }
            catch
            {
            }
            return retDec;
        }
    }
}
