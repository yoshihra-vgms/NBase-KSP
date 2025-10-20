using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using NBaseData.DAC;

using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseData.DS;
using NBaseUtil;

namespace NBaseService
{
    class YosanhyoWriter
    {
        private readonly string fileName;
        MsUser loginUser = null;

        private int YearColOffset = 14;

        private Dictionary<int, Dictionary<int, List<BlobUnkouhi.Line2>>> Dic燃料費_初年度 = new Dictionary<int,Dictionary<int,List<BlobUnkouhi.Line2>>>();

        public YosanhyoWriter(string fileName, MsUser user)
        {
            this.fileName = fileName;
            this.loginUser = user;
        }


        public bool Write(BgYosanHead selectedYosanHead, int years, decimal unit)
        {
            Constants.LoginUser = this.loginUser;

            //anahara
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFileName = path + "Template_予算表.xlsx";

            bool result = false;

            ExcelCreator.Xlsx.XlsxCreator  xls = new ExcelCreator.Xlsx.XlsxCreator ();

            try
            {
                List<MsVessel> vessels = MsVessel.GetRecordsByYojitsuEnabled(this.loginUser);

                if (xls.OpenBook(fileName, templateFileName) == 0)
                {
                    // 各船シート
                    for (int i = 0; i < vessels.Count; i++)
                    {
                        if (i < vessels.Count)
                        {
                            xls.CopySheet(1, i + 2, vessels[i].VesselName);
                        }

                        result = Write_船(xls, i + 2, vessels[i], selectedYosanHead, years, unit);

                        if (!result)
                        {
                            return result;
                        }
                    }

                    // 燃料シート
                    for (int i = 0; i < vessels.Count; i++)
                    {
                        Write_燃料_初年度(xls, i, vessels[i]);
                    }
                    Write_燃料_初年度合計(xls, vessels.Count);


                    // 集計表シート
                    xls.SheetNo = 0;
                    //xls.Cell("J4").Value = selectedYosanHead.Year + "年度 [" + BgYosanSbt.ToName(selectedYosanHead.YosanSbtID) + "予算" + " Rev." + CreateRevisitionStr(selectedYosanHead) + "] ";
                    xls.Cell("I4").Value = selectedYosanHead.Year + "年度 [" + BgYosanSbt.ToName(selectedYosanHead.YosanSbtID) + "予算" + " Rev." + CreateRevisitionStr(selectedYosanHead) + "] ";
                    // ヘッダー行
                    //xls.Cell("E5").Value = selectedYosanHead.Year + "年度　　初年度";
                    xls.Cell("D5").Value = selectedYosanHead.Year + "年度　　初年度";
                    for (int i = 0; i < years; i++)
                    {
                        //xls.Cell("T5", i, 0).Value = (selectedYosanHead.Year + i + 1) + "年度";
                        xls.Cell("S5", i, 0).Value = (selectedYosanHead.Year + i + 1) + "年度";
                    }
                    // ExcelCreatorでは、シート参照の式が有効にならないので
                    // App側で集計表シートの式を処理する
                    //result = Write_集計表(xls, vessels);

                    // 印刷範囲
                    //xls.PrintArea(1, 0, 18 + years, 47);
                    xls.PrintArea(0, 0, 17 + years, 47);

                    // 各船用テンプレートシートの削除
                    xls.DeleteSheet(1, 1);

                    // 集計表をアクティブに
                    xls.ActiveSheet = 0;

                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                xls.CloseBook(true);
            }

            return result;
        }

        private bool Write_船(ExcelCreator.Xlsx.XlsxCreator  xls, int sheetIndex, MsVessel vessel, BgYosanHead yosanHead, int years, decimal unit)
        {
            // 船稼働
            List<BgKadouVessel> kadouVessels = BgKadouVessel.GetRecordsByYosanHeadIDAndMsVesselID(this.loginUser,
                                                                              yosanHead.YosanHeadID,
                                                                              vessel.MsVesselID);

            if (kadouVessels == null || kadouVessels.Count == 0)
                return true;


            // 今年度予算
            List<BgYosanItem> yosanItems今年度 =
              BgYosanItem.GetRecords_月単位(this.loginUser,
                                            yosanHead.YosanHeadID,
                                            vessel.MsVesselID,
                                            (yosanHead.Year).ToString(),
                                            (yosanHead.Year + 19).ToString()
                                           );

            // 次年度〜指定範囲年
            List<BgYosanItem> yosanItems次年度以降 =
              BgYosanItem.GetRecords_年単位_船(this.loginUser,
                                            yosanHead.YosanHeadID,
                                            vessel.MsVesselID,
                                            (yosanHead.Year + 1).ToString(),
                                            (yosanHead.Year + years - 1).ToString()
                                           );

            //xls出力
            return WriteFile(xls, sheetIndex, vessel, yosanHead, years, yosanItems今年度, yosanItems次年度以降, kadouVessels, unit);
        }


        private bool WriteFile(ExcelCreator.Xlsx.XlsxCreator  xls, int sheetIndex, MsVessel vessel, BgYosanHead yosanHead, int years,
                               List<BgYosanItem> yosanItems今年度,
                               List<BgYosanItem> yosanItems次年度以降,
                               List<BgKadouVessel> kadouVessels, decimal unit)
        {
            xls.SheetNo = sheetIndex;
            xls.SheetName = vessel.VesselName;

            // 印刷範囲
            //xls.PrintArea(3, 0, 20 + years, 49);
            xls.PrintArea(0, 0, 17 + years, 49);

            // 印刷ヘッダ
            xls.Header("", "", "日付：&D\n  ページ &P / &N");

            // ヘッダー行
            xls.Cell("**船名").Value = vessel.VesselName;
            //xls.Cell("G5").Value =  yosanHead.Year + "年度　　初年度";
            xls.Cell("D5").Value = yosanHead.Year + "年度　　初年度";
            
            Write_船稼働_初年度(kadouVessels[0], xls);

            for (int i = 1; i <= years; i++)
            {
                //xls.Cell("G5", YearColOffset + i, 0).Value = (yosanHead.Year + i) + "年度";
                xls.Cell("D5", YearColOffset + i, 0).Value = (yosanHead.Year + i) + "年度";

                if (kadouVessels != null && i < kadouVessels.Count)
                {
                    Write_船稼働_次年度以降(kadouVessels[i], xls, YearColOffset + i);
                }
            }

            foreach (BgYosanItem yosanItem in yosanItems今年度)
            {
                if (yosanItem.Nengetsu.Trim().Length == 4)
                {
                    continue;
                }

                decimal amount = GetAmount(yosanItem, yosanItem.MsHimokuID);

                xls.Cell(DetectBaseCell(yosanItem.MsHimokuID), DetectColumn今年度(yosanItem.Nengetsu), 0).Value = amount / unit;
            }

            foreach (BgYosanItem yosanItem in yosanItems次年度以降)
            {
                decimal amount = GetAmount(yosanItem, yosanItem.MsHimokuID);

                xls.Cell(DetectBaseCell(yosanItem.MsHimokuID), DetectColumn次年度以降(yosanHead, yosanItem.Nengetsu), 0).Value = amount / unit;
            }

            Write_運航費_初年度(xls, vessel, yosanHead, unit);
            Write_運航費_次年度以降(xls, vessel, yosanHead, unit, years);

            return true;
        }


        private void Write_運航費_初年度(ExcelCreator.Xlsx.XlsxCreator  xls, MsVessel vessel, BgYosanHead yosanHead, decimal unit)
        {
            if (Dic燃料費_初年度.ContainsKey(vessel.MsVesselID) == false)
            {
                Dic燃料費_初年度.Add(vessel.MsVesselID, new Dictionary<int, List<BlobUnkouhi.Line2>>()); 
            }
            Dictionary<int, List<BlobUnkouhi.Line2>> dic燃料費_vessel = Dic燃料費_初年度[vessel.MsVesselID];

            int 運賃ID = 2;
            int 燃料費ID = 18;
            int 港費ID = 19;

            BgUnkouhi unkouhi = BgUnkouhi.GetRecordByYosanHeadIdAndMsVesselIdAndYear(this.loginUser, yosanHead.YosanHeadID, vessel.MsVesselID, yosanHead.Year);
            BlobUnkouhiList unkouhiData = BlobUnkouhiList.ToObject(unkouhi.ObjectData);
            int idx = 0;
            for (int col = 0; col < 13; col++)
            {
                if (col == 6)
                    continue;

                BlobUnkouhi u = unkouhiData.List[idx];
                decimal rate = DetectRate(yosanHead, yosanHead.Year, NBaseData.DS.Constants.INT_MONTHS[idx]);

                List<BlobUnkouhi.Line2> 燃料費 = new List<BlobUnkouhi.Line2>();
                燃料費.AddRange(u.円データ.運賃2_燃料費);
                燃料費.AddRange(u.ドルデータ.運賃2_燃料費);
                dic燃料費_vessel.Add(NBaseData.DS.Constants.INT_MONTHS[idx], 燃料費);

                string 運賃式 = "";
                string 燃料費式 = "";
                string 港費式 = "";


                // 運賃(1)
                if (BlobUnkouhi.Store.Calc_計(u.円データ.運賃1) != 0)
                {
                    運賃式 += BlobUnkouhi.Store.Calc式(u.円データ.運賃1);
                }
                if (BlobUnkouhi.Store.Calc_計(u.ドルデータ.運賃1) != 0)
                {
                    if (運賃式.Length > 0)
                        運賃式 += " + ";
                    運賃式 += BlobUnkouhi.Store.Calc式(u.ドルデータ.運賃1, rate);
                }

                // 運賃(2)
                if (u.円データ.運賃2_固定費 != 0)
                {
                    if (運賃式.Length > 0)
                        運賃式 += " + ";
                    運賃式 += u.円データ.運賃2_固定費.ToString();
                }
                if (u.ドルデータ.運賃2_固定費 != 0)
                {
                    if (運賃式.Length > 0)
                        運賃式 += " + ";
                    運賃式 += "( " + u.ドルデータ.運賃2_固定費.ToString() + "*" + rate.ToString() + " )";
                }
                if (BlobUnkouhi.Store.Calc_計(u.円データ.運賃2_燃料費) != 0)
                {
                    if (運賃式.Length > 0)
                        運賃式 += " + ";
                    運賃式 += BlobUnkouhi.Store.Calc式(u.円データ.運賃2_燃料費);
                }
                if (BlobUnkouhi.Store.Calc_計(u.ドルデータ.運賃2_燃料費) != 0)
                {
                    if (運賃式.Length > 0)
                        運賃式 += " + ";
                    運賃式 += BlobUnkouhi.Store.Calc式(u.ドルデータ.運賃2_燃料費, rate);
                }

                if (BlobUnkouhi.Store.Calc_計(u.円データ.運賃2_港費) != 0)
                {
                    if (運賃式.Length > 0)
                        運賃式 += " + ";
                    運賃式 += BlobUnkouhi.Store.Calc式(u.円データ.運賃2_港費);
                }
                if (BlobUnkouhi.Store.Calc_計(u.ドルデータ.運賃2_港費) != 0)
                {
                    if (運賃式.Length > 0)
                        運賃式 += " + ";
                    運賃式 += BlobUnkouhi.Store.Calc式(u.ドルデータ.運賃2_港費, rate);
                }

                if (BlobUnkouhi.Store.Calc_計(u.円データ.運賃2_追加費) != 0)
                {
                    if (運賃式.Length > 0)
                        運賃式 += " + ";
                    運賃式 += BlobUnkouhi.Store.Calc式(u.円データ.運賃2_追加費);
                }
                if (BlobUnkouhi.Store.Calc_計(u.ドルデータ.運賃2_追加費) != 0)
                {
                    if (運賃式.Length > 0)
                        運賃式 += " + ";
                    運賃式 += BlobUnkouhi.Store.Calc式(u.ドルデータ.運賃2_追加費, rate);
                }

                // 燃料費
                if (BlobUnkouhi.Store.Calc_計(u.円データ.燃料費) != 0)
                {
                    if (燃料費式.Length > 0)
                        燃料費式 += " + ";
                    燃料費式 += BlobUnkouhi.Store.Calc式(u.円データ.燃料費);
                }
                if (BlobUnkouhi.Store.Calc_計(u.ドルデータ.燃料費) != 0)
                {
                    if (燃料費式.Length > 0)
                        燃料費式 += " + ";
                    燃料費式 += BlobUnkouhi.Store.Calc式(u.ドルデータ.燃料費, rate);
                }

                // 港費
                if (BlobUnkouhi.Store.Calc_計(u.円データ.港費) != 0)
                {
                    if (港費式.Length > 0)
                        港費式 += " + ";
                    港費式 += BlobUnkouhi.Store.Calc式(u.円データ.港費);
                }
                if (BlobUnkouhi.Store.Calc_計(u.ドルデータ.港費) != 0)
                {
                    if (港費式.Length > 0)
                        港費式 += " + ";
                    港費式 += BlobUnkouhi.Store.Calc式(u.ドルデータ.港費, rate);
                }

                // 貨物費
                //u.円データ.貨物費;

                // その他運航費
                //u.円データ.その他運航費;


                // 売上高-運賃
                if (運賃式.Length > 0)
                {
                    string val = (string)xls.Cell(DetectBaseCell(運賃ID), col, 0).Value;
                    int intVal = 0;
                    if (int.TryParse(val, out intVal))
                        //xls.Cell(DetectBaseCell(運賃ID), col, 0).Value = "=(" + 運賃式 + ") / " + unit.ToString() + " * " + ExcelUtils.ToCellName(6 + col, 12);
                        xls.Cell(DetectBaseCell(運賃ID), col, 0).Value = "=(" + 運賃式 + ") / " + unit.ToString() + " * " + ExcelUtils.ToCellName(3 + col, 12);
                }
                
                // 売上原価-運航費-燃料費
                if (燃料費式.Length > 0)
                {
                    string val = (string)xls.Cell(DetectBaseCell(燃料費ID), col, 0).Value;
                    int intVal = 0;
                    if (int.TryParse(val, out intVal))
                        //xls.Cell(DetectBaseCell(燃料費ID), col, 0).Value = "=(" + 燃料費式 + ") / " + unit.ToString() + " * " + ExcelUtils.ToCellName(6 + col, 12);
                        xls.Cell(DetectBaseCell(燃料費ID), col, 0).Value = "=(" + 燃料費式 + ") / " + unit.ToString() + " * " + ExcelUtils.ToCellName(3 + col, 12);
                }

                // 売上原価-運航費-港費
                if (港費式.Length > 0)
                {
                    string val = (string)xls.Cell(DetectBaseCell(港費ID), col, 0).Value;
                    int intVal = 0;
                    if (int.TryParse(val, out intVal))
                        //xls.Cell(DetectBaseCell(港費ID), col, 0).Value = "=(" + 港費式 + ") / " + unit.ToString() + " * " + ExcelUtils.ToCellName(6 + col, 12);
                        xls.Cell(DetectBaseCell(港費ID), col, 0).Value = "=(" + 港費式 + ") / " + unit.ToString() + " * " + ExcelUtils.ToCellName(3 + col, 12);
                }

                idx++;
            }

        }

        private void Write_運航費_次年度以降(ExcelCreator.Xlsx.XlsxCreator  xls, MsVessel vessel, BgYosanHead yosanHead, decimal unit, int years)
        {
            int 運賃ID = 2;
            int 燃料費ID = 18;
            int 港費ID = 19;

            for (int addYears = 0; addYears < years; addYears++)
            {
                BgUnkouhi unkouhi = BgUnkouhi.GetRecordByYosanHeadIdAndMsVesselIdAndYear(this.loginUser, yosanHead.YosanHeadID, vessel.MsVesselID, yosanHead.Year + (addYears + 1));
                BlobUnkouhiList unkouhiData = BlobUnkouhiList.ToObject(unkouhi.ObjectData);
                
                string 運賃式 = "";
                string 燃料費式 = "";
                string 港費式 = "";

                for (int i = 0; i < 12; i++)
                {
                    BlobUnkouhi u = unkouhiData.List[i];
                    decimal rate = DetectRate(yosanHead, yosanHead.Year, NBaseData.DS.Constants.INT_MONTHS[i]);


                    // 運賃(1)
                    if (BlobUnkouhi.Store.Calc_計(u.円データ.運賃1) != 0)
                    {
                        if (運賃式.Length > 0)
                            運賃式 += " + ";
                        運賃式 += BlobUnkouhi.Store.Calc式(u.円データ.運賃1);
                    }
                    if (BlobUnkouhi.Store.Calc_計(u.ドルデータ.運賃1) != 0)
                    {
                        if (運賃式.Length > 0)
                            運賃式 += " + ";
                        運賃式 += BlobUnkouhi.Store.Calc式(u.ドルデータ.運賃1, rate);
                    }

                    // 運賃(2)
                    if (u.円データ.運賃2_固定費 != 0)
                    {
                        if (運賃式.Length > 0)
                            運賃式 += " + ";
                        運賃式 += u.円データ.運賃2_固定費.ToString();
                    }
                    if (u.ドルデータ.運賃2_固定費 != 0)
                    {
                        if (運賃式.Length > 0)
                            運賃式 += " + ";
                        運賃式 += "( " + u.ドルデータ.運賃2_固定費.ToString() + "*" + rate.ToString() + " )";
                    }
                    if (BlobUnkouhi.Store.Calc_計(u.円データ.運賃2_燃料費) != 0)
                    {
                        if (運賃式.Length > 0)
                            運賃式 += " + ";
                        運賃式 += BlobUnkouhi.Store.Calc式(u.円データ.運賃2_燃料費);
                    }
                    if (BlobUnkouhi.Store.Calc_計(u.ドルデータ.運賃2_燃料費) != 0)
                    {
                        if (運賃式.Length > 0)
                            運賃式 += " + ";
                        運賃式 += BlobUnkouhi.Store.Calc式(u.ドルデータ.運賃2_燃料費, rate);
                    }

                    if (BlobUnkouhi.Store.Calc_計(u.円データ.運賃2_港費) != 0)
                    {
                        if (運賃式.Length > 0)
                            運賃式 += " + ";
                        運賃式 += BlobUnkouhi.Store.Calc式(u.円データ.運賃2_港費);
                    }
                    if (BlobUnkouhi.Store.Calc_計(u.ドルデータ.運賃2_港費) != 0)
                    {
                        if (運賃式.Length > 0)
                            運賃式 += " + ";
                        運賃式 += BlobUnkouhi.Store.Calc式(u.ドルデータ.運賃2_港費, rate);
                    }

                    if (BlobUnkouhi.Store.Calc_計(u.円データ.運賃2_追加費) != 0)
                    {
                        if (運賃式.Length > 0)
                            運賃式 += " + ";
                        運賃式 += BlobUnkouhi.Store.Calc式(u.円データ.運賃2_追加費);
                    }
                    if (BlobUnkouhi.Store.Calc_計(u.ドルデータ.運賃2_追加費) != 0)
                    {
                        if (運賃式.Length > 0)
                            運賃式 += " + ";
                        運賃式 += BlobUnkouhi.Store.Calc式(u.ドルデータ.運賃2_追加費, rate);
                    }

                    // 燃料費
                    if (BlobUnkouhi.Store.Calc_計(u.円データ.燃料費) != 0)
                    {
                        if (燃料費式.Length > 0)
                            燃料費式 += " + ";
                        燃料費式 += BlobUnkouhi.Store.Calc式(u.円データ.燃料費);
                    }
                    if (BlobUnkouhi.Store.Calc_計(u.ドルデータ.燃料費) != 0)
                    {
                        if (燃料費式.Length > 0)
                            燃料費式 += " + ";
                        燃料費式 += BlobUnkouhi.Store.Calc式(u.ドルデータ.燃料費, rate);
                    }

                    // 港費
                    if (BlobUnkouhi.Store.Calc_計(u.円データ.港費) != 0)
                    {
                        if (港費式.Length > 0)
                            港費式 += " + ";
                        港費式 += BlobUnkouhi.Store.Calc式(u.円データ.港費);
                    }
                    if (BlobUnkouhi.Store.Calc_計(u.ドルデータ.港費) != 0)
                    {
                        if (港費式.Length > 0)
                            港費式 += " + ";
                        港費式 += BlobUnkouhi.Store.Calc式(u.ドルデータ.港費, rate);
                    }
            
                    // 貨物費
                    //u.円データ.貨物費;

                    // その他運航費
                    //u.円データ.その他運航費;
                }

                // 売上高-運賃
                if (運賃式.Length > 0)
                {
                    string val = (string)xls.Cell(DetectBaseCell(運賃ID), YearColOffset + (addYears + 1), 0).Value;
                    int intVal = 0;
                    if (int.TryParse(val, out intVal))
                        //xls.Cell(DetectBaseCell(運賃ID), YearColOffset + addYears, 0).Value = "=(" + 運賃式 + ") / " + unit.ToString() + " * " + ExcelUtils.ToCellName(6 + YearColOffset + addYears, 12);
                        xls.Cell(DetectBaseCell(運賃ID), YearColOffset + (addYears + 1), 0).Value = "=(" + 運賃式 + ") / " + unit.ToString() + " * " + ExcelUtils.ToCellName(3 + YearColOffset + addYears, 12);
                }

                // 売上原価-運航費-燃料費
                if (燃料費式.Length > 0)
                {
                    string val = (string)xls.Cell(DetectBaseCell(燃料費ID), YearColOffset + (addYears + 1), 0).Value;
                    int intVal = 0;
                    if (int.TryParse(val, out intVal))
                        //xls.Cell(DetectBaseCell(燃料費ID), YearColOffset + addYears, 0).Value = "=(" + 燃料費式 + ") / " + unit.ToString() + " * " + ExcelUtils.ToCellName(6 + YearColOffset + addYears, 12);
                        xls.Cell(DetectBaseCell(燃料費ID), YearColOffset + (addYears + 1), 0).Value = "=(" + 燃料費式 + ") / " + unit.ToString() + " * " + ExcelUtils.ToCellName(3 + YearColOffset + addYears, 12);
                }

                // 売上原価-運航費-港費
                if (港費式.Length > 0)
                {
                    string val = (string)xls.Cell(DetectBaseCell(港費ID), YearColOffset + (addYears + 1), 0).Value;
                    int intVal = 0;
                    if (int.TryParse(val, out intVal))
                        //xls.Cell(DetectBaseCell(港費ID), YearColOffset + addYears, 0).Value = "=(" + 港費式 + ") / " + unit.ToString() + " * " + ExcelUtils.ToCellName(6 + YearColOffset + addYears, 12);
                        xls.Cell(DetectBaseCell(港費ID), YearColOffset + (addYears + 1), 0).Value = "=(" + 港費式 + ") / " + unit.ToString() + " * " + ExcelUtils.ToCellName(3 + YearColOffset + addYears, 12);
                }
            }

        }



        private void Write_燃料_初年度(ExcelCreator.Xlsx.XlsxCreator  xls, int idx, MsVessel vessel)
        {
            int sheetNo = xls.SheetNo2("燃料消費量");
            xls.SheetNo = sheetNo;
            int rowStart = 4 + ((idx+1)*5);　// 5行目+船×3行

            xls.RowInsert(rowStart,5);
            xls.RowCopy(4, rowStart);
            xls.RowCopy(4 + 1, rowStart + 1);
            xls.RowCopy(4 + 2, rowStart + 2);
            xls.RowCopy(4 + 3, rowStart + 3);
            xls.RowCopy(4 + 4, rowStart + 4);

            xls.Cell("A" + (rowStart + 1).ToString()).Value = vessel.VesselName;

            if (Dic燃料費_初年度.ContainsKey(vessel.MsVesselID) == false)
                return;

            Dictionary<int, List<BlobUnkouhi.Line2>> dic燃料費_vessel = Dic燃料費_初年度[vessel.MsVesselID];

            int col = 0;
            for(int i = 0; i < 12; i ++)
            {
                if (i == 6)
                    col++;

                int month = NBaseData.DS.Constants.INT_MONTHS[i];
                if (dic燃料費_vessel.ContainsKey(month) == false)
                    continue;

                List<BlobUnkouhi.Line2> dic燃料費_vessel_月 = dic燃料費_vessel[month];

                // Ａ重油
                xls.Cell("C" + (rowStart + 1).ToString(), (col * 2), 0).Value = dic燃料費_vessel_月[0].数量;
                xls.Cell("D" + (rowStart + 1).ToString(), (col * 2), 0).Value = dic燃料費_vessel_月[0].単価;
                xls.Cell("C" + (rowStart + 1).ToString(), (col * 2), 1).Value = dic燃料費_vessel_月[1].数量;
                xls.Cell("D" + (rowStart + 1).ToString(), (col * 2), 1).Value = dic燃料費_vessel_月[1].単価;
               
                // Ｃ重油
                xls.Cell("C" + (rowStart + 1).ToString(), (col * 2), 2).Value = dic燃料費_vessel_月[2].数量;
                xls.Cell("D" + (rowStart + 1).ToString(), (col * 2), 2).Value = dic燃料費_vessel_月[2].単価;
                xls.Cell("C" + (rowStart + 1).ToString(), (col * 2), 3).Value = dic燃料費_vessel_月[3].数量;
                xls.Cell("D" + (rowStart + 1).ToString(), (col * 2), 3).Value = dic燃料費_vessel_月[3].単価;

                col++;
            }

        }
        private void Write_燃料_初年度合計(ExcelCreator.Xlsx.XlsxCreator  xls, int vesselCount)
        {
            // テンプレート行を削除
            xls.RowDelete(4, 5);

            // 船行の最終行番号
            int rowEnd = 4 + (vesselCount * 5);

            int col = 0;
            for(int i = 0; i < 12; i ++)
            {
                if (i == 6)
                    col++;

                // 合計行(Ａ重油)
                xls.Cell("C" + (rowEnd + 1).ToString(), (col * 2), 0).Value = "=SUMIF(" + ExcelUtils.ToCellName(1, 4) + ":" + ExcelUtils.ToCellName(1, rowEnd - 1) + ",\"=A1重油(KL)\", " + ExcelUtils.ToCellName(2 + (col * 2), 4) + ":" + ExcelUtils.ToCellName(2 + (col * 2), rowEnd - 1) + ")";
                xls.Cell("D" + (rowEnd + 1).ToString(), (col * 2), 0).Value = "=SUMIF(" + ExcelUtils.ToCellName(1, 4) + ":" + ExcelUtils.ToCellName(1, rowEnd - 1) + ",\"=A1重油(KL)\", " + ExcelUtils.ToCellName(3 + (col * 2), 4) + ":" + ExcelUtils.ToCellName(3 + (col * 2), rowEnd - 1) + ")";
                xls.Cell("C" + (rowEnd + 1).ToString(), (col * 2), 1).Value = "=SUMIF(" + ExcelUtils.ToCellName(1, 4) + ":" + ExcelUtils.ToCellName(1, rowEnd - 1) + ",\"=A2重油(KL)\", " + ExcelUtils.ToCellName(2 + (col * 2), 4) + ":" + ExcelUtils.ToCellName(2 + (col * 2), rowEnd - 1) + ")";
                xls.Cell("D" + (rowEnd + 1).ToString(), (col * 2), 1).Value = "=SUMIF(" + ExcelUtils.ToCellName(1, 4) + ":" + ExcelUtils.ToCellName(1, rowEnd - 1) + ",\"=A2重油(KL)\", " + ExcelUtils.ToCellName(3 + (col * 2), 4) + ":" + ExcelUtils.ToCellName(3 + (col * 2), rowEnd - 1) + ")";

                // 合計行(Ｃ重油)
                xls.Cell("C" + (rowEnd + 1).ToString(), (col * 2), 2).Value = "=SUMIF(" + ExcelUtils.ToCellName(1, 4) + ":" + ExcelUtils.ToCellName(1, rowEnd - 1) + ",\"=C1重油(KL)\", " + ExcelUtils.ToCellName(2 + (col * 2), 4) + ":" + ExcelUtils.ToCellName(2 + (col * 2), rowEnd - 1) + ")";
                xls.Cell("D" + (rowEnd + 1).ToString(), (col * 2), 2).Value = "=SUMIF(" + ExcelUtils.ToCellName(1, 4) + ":" + ExcelUtils.ToCellName(1, rowEnd - 1) + ",\"=C1重油(KL)\", " + ExcelUtils.ToCellName(3 + (col * 2), 4) + ":" + ExcelUtils.ToCellName(3 + (col * 2), rowEnd - 1) + ")";
                xls.Cell("C" + (rowEnd + 1).ToString(), (col * 2), 3).Value = "=SUMIF(" + ExcelUtils.ToCellName(1, 4) + ":" + ExcelUtils.ToCellName(1, rowEnd - 1) + ",\"=C2重油(KL)\", " + ExcelUtils.ToCellName(2 + (col * 2), 4) + ":" + ExcelUtils.ToCellName(2 + (col * 2), rowEnd - 1) + ")";
                xls.Cell("D" + (rowEnd + 1).ToString(), (col * 2), 3).Value = "=SUMIF(" + ExcelUtils.ToCellName(1, 4) + ":" + ExcelUtils.ToCellName(1, rowEnd - 1) + ",\"=C2重油(KL)\", " + ExcelUtils.ToCellName(3 + (col * 2), 4) + ":" + ExcelUtils.ToCellName(3 + (col * 2), rowEnd - 1) + ")";
                
                col++;
            }
        }

        private static decimal GetAmount(IYojitsu yojitsu, int himokuId)
        {
            decimal amount;
            if (HimokuTreeReader.GetHimokuTreeNode(himokuId) == null)
            {
                amount = 0;
            }
            else if (HimokuTreeReader.GetHimokuTreeNode(himokuId).Dollar)
            {
                amount = yojitsu.YenAmount;
            }
            else
            {
                amount = yojitsu.Amount;
            }

            return amount;
        }

        private void Write_船稼働_初年度(BgKadouVessel bgKadouVessel, ExcelCreator.Xlsx.XlsxCreator  xls)
        {
            decimal fukadoubiTotal = bgKadouVessel.Fukadoubi1 +
                                           bgKadouVessel.Fukadoubi2 +
                                           bgKadouVessel.Fukadoubi3;
            int idx = 0;
            for (int col = 0; col < 13; col++)
            {
                if (col == 6)
                    continue;


                int year = col < 9 ? bgKadouVessel.Year : bgKadouVessel.Year + 1;
                int daysOfMonth;

                if (NBaseData.DS.Constants.INT_MONTHS[idx] != 12)
                {
                    daysOfMonth = (new DateTime(year, NBaseData.DS.Constants.INT_MONTHS[idx] + 1, 1) -
                                           new DateTime(year, NBaseData.DS.Constants.INT_MONTHS[idx], 1)).Days;
                }
                else
                {
                    daysOfMonth = (new DateTime(year + 1, 1, 1) -
                                           new DateTime(year, NBaseData.DS.Constants.INT_MONTHS[idx], 1)).Days;
                }

                // 暦日
                //xls.Cell("G7", col, 0).Value = daysOfMonth;
                xls.Cell("D7", col, 0).Value = daysOfMonth;

                if (bgKadouVessel.NyukyoMonth == NBaseData.DS.Constants.INT_MONTHS[idx])
                {
                    decimal fukadouMonth = Math.Round(((decimal)fukadoubiTotal / (decimal)daysOfMonth), 2, MidpointRounding.AwayFromZero);
                    decimal kadouMonth = 1 - fukadouMonth;

                    // 不稼動事由
                    //xls.Cell("G8", col, 0).Value = bgKadouVessel.NyukyoKind;
                    xls.Cell("D8", col, 0).Value = bgKadouVessel.NyukyoKind;
                    // 発生月
                    // ;

                    // 同日数
                    //xls.Cell("G10", col, 0).Value = fukadoubiTotal;
                    xls.Cell("D10", col, 0).Value = fukadoubiTotal;
                    // 同月数
                    // 計算式を埋め込んである

                    // 稼働日数
                    // 計算式を埋め込んである
                    // 稼働月数 
                    // 計算式を埋め込んである

                }

                idx++;
            }
        }

        private void Write_船稼働_次年度以降(BgKadouVessel bgKadouVessel, ExcelCreator.Xlsx.XlsxCreator  xls, int col)
        {
            int daysOfYear = (new DateTime(bgKadouVessel.Year, 12, 31) - new DateTime(bgKadouVessel.Year, 1, 1)).Days + 1;
            decimal fukadoubiTotal = bgKadouVessel.Fukadoubi1 +
                                           bgKadouVessel.Fukadoubi2 +
                                           bgKadouVessel.Fukadoubi3;
            decimal fukadouMonth = Math.Round(((decimal)fukadoubiTotal / (decimal)daysOfYear) * 12, 2, MidpointRounding.AwayFromZero);
            decimal kadouMonth = 12 - fukadouMonth;

            string nyukyoKind = bgKadouVessel.NyukyoKind != string.Empty ? bgKadouVessel.NyukyoKind : "--";

            // 暦日
            //xls.Cell("G7", col, 0).Value = daysOfYear;
            xls.Cell("D7", col, 0).Value = daysOfYear;
            // 不稼動事由
            //xls.Cell("G8", col, 0).Value = nyukyoKind;
            xls.Cell("D8", col, 0).Value = nyukyoKind;

            // 発生月
            if (nyukyoKind != "--")
                //xls.Cell("G9", col, 0).Value = bgKadouVessel.NyukyoMonth;
                xls.Cell("D9", col, 0).Value = bgKadouVessel.NyukyoMonth;

            // 同日数
            //xls.Cell("G10", col, 0).Value = fukadoubiTotal;
            xls.Cell("D10", col, 0).Value = fukadoubiTotal;
            // 同月数
            // 計算式を埋め込んである

            // 稼働日数 
            // 計算式を埋め込んである
            // 稼働月数
            // 計算式を埋め込んである
        }


        //private bool Write_集計表(ExcelCreator.Xlsx.XlsxCreator  xls, List<MsVessel> vessels)
        //{
        //    int 集計SheetX_Base = 4;// E列：0オリジン
        //    int 集計SheetY_Base = 6;// 7行：0オリジン
        //    int 船SheetX_Base = 6;// G列：0オリジン
        //    int 船SheetY_Base = 13;// 14行：0オリジン

        //    for (int colIdx = 0; colIdx < 13; colIdx++)
        //    {
        //        if (colIdx == 6
        //            || colIdx == 13
        //            || colIdx == 14)
        //            continue;

        //        for (int rowIdx = 0; rowIdx < 39; rowIdx++)
        //        {
        //            if (rowIdx == 4
        //                || rowIdx == 9
        //                || rowIdx == 22
        //                || rowIdx == 25
        //                || rowIdx == 26
        //                || rowIdx == 30
        //                || rowIdx == 34
        //                || rowIdx == 35
        //                || rowIdx == 38)
        //                continue;

        //            StringBuilder sb = new StringBuilder();
        //            foreach (MsVessel vessel in vessels)
        //            {
        //                if (sb.Length > 0)
        //                {
        //                    sb.Append("+");
        //                }
        //                else if (sb.Length == 0)
        //                {
        //                    sb.Append("'=");
        //                }
        //                sb.Append("'" + vessel.VesselName + "'!" + ExcelUtils.ToCellName(船SheetX_Base + colIdx, 船SheetY_Base + rowIdx));
        //            }
        //            xls.Cell(ExcelUtils.ToCellName(集計SheetX_Base + colIdx, 集計SheetY_Base + rowIdx)).Value = sb.ToString();
        //        }
        //    }

        //    return true;
        //}


        private string DetectBaseCell(int msHimokuID)
        {
            return "**" + msHimokuID;
        }


        private int DetectColumn今年度(string nengetsu)
        {
            string month = nengetsu.Substring(4, 2);

            for (int i = 0; i < NBaseData.DS.Constants.PADDING_MONTHS.Length; i++)
            {
                if (month == NBaseData.DS.Constants.PADDING_MONTHS[i])
                {
                    if (i > 5)
                    {
                        return (i + 1);
                    }
                    else
                    {
                        return i;
                    }
                }
            }

            return 0;
        }


        private int DetectColumn次年度以降(BgYosanHead yosanHead, string nengetsu)
        {
            int year = Int32.Parse(nengetsu.Substring(0, 4).Trim());

            int col = YearColOffset + (year - yosanHead.Year);
            return col;
        }

        private Dictionary<int, List<BgRate>> bgRateDic;
        private decimal DetectRate(BgYosanHead yosanHead, int year, int month)
        {
            List<BgRate> rates = GetBgRateList(yosanHead);

            BgRate rate = null;
            foreach (BgRate r in rates)
            {
                if (r.Year == year)
                {
                    rate = r;
                    break;
                }
            }

            if (IsKamiki(month))
            {
                return rate.KamikiRate;
            }
            else
            {
                return rate.ShimokiRate;
            }
        }

        private List<BgRate> GetBgRateList(BgYosanHead yosanHead)
        {
            if (bgRateDic == null)
            {
                bgRateDic = new Dictionary<int, List<BgRate>>();
            }

            int yosanHeadId = yosanHead.YosanHeadID;

            if (!bgRateDic.ContainsKey(yosanHeadId))
            {
                bgRateDic[yosanHeadId] = new List<BgRate>();
                
                // 前年度予算
                BgYosanHead preYosanHead = BgYosanHead.GetRecordByYear(this.loginUser, (yosanHead.Year - 1).ToString());

                if (preYosanHead != null)
                {
                    List<BgRate> preRates = BgRate.GetRecordsByYosanHeadID(this.loginUser, preYosanHead.YosanHeadID);
                    bgRateDic[yosanHeadId].Add(preRates[0]);
                }

                bgRateDic[yosanHeadId].AddRange(BgRate.GetRecordsByYosanHeadID(this.loginUser, yosanHeadId));
            }
            
            return bgRateDic[yosanHeadId];
        }

        internal static bool IsKamiki(int month)
        {
            for (int i = 0; i < NBaseData.DS.Constants.INT_MONTHS.Length; i++)
            {
                if (month == NBaseData.DS.Constants.INT_MONTHS[i])
                {
                    return i < 6 ? true : false;
                }
            }

            return true;
        }



        internal static string CreateRevisitionStr(BgYosanHead yosanHead)
        {
            string revStr = yosanHead.Revision.ToString();

            if (!yosanHead.IsFixed())
            {
                revStr += " (未 Fix)";
            }
            else
            {
                revStr += " (" + yosanHead.FixDate.ToString("yyyy/MM/dd") + " Fix)";
            }

            return revStr;
        }
    }
}
