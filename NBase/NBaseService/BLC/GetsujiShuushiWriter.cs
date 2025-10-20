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

namespace NBaseService
{
    class GetsujiShuushiWriter
    {
        private readonly string fileName;
        MsUser loginUser = null;

        public GetsujiShuushiWriter(string fileName, MsUser user)
        {
            this.fileName = fileName;
            this.loginUser = user;
        }


        public bool Write(BgYosanHead selectedYosanHead, decimal unit, int selectindex, object selectitem, bool is前年度実績出力)
        {
            Constants.LoginUser = this.loginUser;

            //anahara
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFileName = path + "Template_月次収支報告書.xlsx";

            bool result = false;

            ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator();

            try
            {
                if (xls.OpenBook(fileName, templateFileName) == 0)
                {
                    // 全社
                    if (selectindex == 0)
                    {
                        List<MsVessel> vessels = MsVessel.GetRecordsByYojitsuEnabled(this.loginUser);

                        for(int i = 0; i < vessels.Count + 1; i++)
                        {
                            if (i < vessels.Count)
                            {
                                xls.CopySheet(i, i + 1, "");
                            }

                            if(i == 0)
                            {
                                result = Write_全社(xls, 0, selectedYosanHead, unit, is前年度実績出力);
                            }
                            else
                            {
                                result = Write_船(xls, i, vessels[i - 1], selectedYosanHead, unit, is前年度実績出力);
                            }

                            if (!result)
                            {
                                return result;
                            }
                       }
                    }
                    // グループ
                    else if (selectitem is MsVesselType)
                    {
                        result = Write_グループ(xls, 0, (selectitem as MsVesselType), selectedYosanHead, unit, is前年度実績出力);
                    }
                    // 船
                    else if (selectitem is MsVessel)
                    {
                        result = Write_船(xls, 0, (selectitem as MsVessel), selectedYosanHead, unit, is前年度実績出力);
                    }
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


        private bool Write_全社(ExcelCreator.Xlsx.XlsxCreator xls, int sheetIndex, BgYosanHead yosanHead, decimal unit, bool is前年度実績出力)
        {
            // 前年度実績
            List<BgJiseki> jisekis前年度 =
              BgJiseki.GetRecords_年単位_全社(this.loginUser,
                                            (yosanHead.Year - 1).ToString(),
                                            yosanHead.Year.ToString()
                                           );

            // 予算
            List<BgYosanItem> yosanItems今年度 =
              BgYosanItem.GetRecords_月単位_全社
              (this.loginUser,
                                            yosanHead.YosanHeadID,
                                            (yosanHead.Year).ToString(),
                                            (yosanHead.Year + 19).ToString()
                                           );
            // 今年度実績
            List<BgJiseki> jisekis今年度 =
              BgJiseki.GetRecords_月単位_全社(this.loginUser,
                                            yosanHead.Year + NBaseData.DS.Constants.PADDING_MONTHS[0],
                                            (yosanHead.Year + 1) + NBaseData.DS.Constants.PADDING_MONTHS[11]
                                           );


            // 20年分（年）
            List<BgYosanItem> yosanItems20年分 =
              BgYosanItem.GetRecords_年単位_全社(this.loginUser,
                                            yosanHead.YosanHeadID,
                                            yosanHead.Year.ToString(),
                                            (yosanHead.Year + Constants.GetYearRange(yosanHead.YosanSbtID) - 1).ToString()
                                           );

            //xls出力
            return WriteFile(xls, sheetIndex, "全社", yosanHead, jisekis前年度, yosanItems今年度, jisekis今年度, yosanItems20年分, null, unit, is前年度実績出力);
        }


        private bool Write_グループ(ExcelCreator.Xlsx.XlsxCreator xls, int sheetIndex, MsVesselType selectedVessel, BgYosanHead yosanHead, decimal unit, bool is前年度実績出力)
        {
            // 前年度実績
            List<BgJiseki> jisekis前年度 =
              BgJiseki.GetRecords_年単位_グループ(this.loginUser,
                                            selectedVessel.MsVesselTypeID,
                                            (yosanHead.Year - 1).ToString(),
                                            yosanHead.Year.ToString()
                                           );

            // 予算
            List<BgYosanItem> yosanItems今年度 =
              BgYosanItem.GetRecords_月単位_グループ
              (this.loginUser,
                                            yosanHead.YosanHeadID,
                                            selectedVessel.MsVesselTypeID,
                                            (yosanHead.Year).ToString(),
                                            (yosanHead.Year + 19).ToString()
                                           );
            // 今年度実績
            List<BgJiseki> jisekis今年度 =
              BgJiseki.GetRecords_月単位_グループ(this.loginUser,
                                            selectedVessel.MsVesselTypeID,
                                            yosanHead.Year + NBaseData.DS.Constants.PADDING_MONTHS[0],
                                            (yosanHead.Year + 1) + NBaseData.DS.Constants.PADDING_MONTHS[11]
                                           );


            // 20年分（年）
            List<BgYosanItem> yosanItems20年分 =
              BgYosanItem.GetRecords_年単位_グループ(this.loginUser,
                                            yosanHead.YosanHeadID,
                                            selectedVessel.MsVesselTypeID,
                                            yosanHead.Year.ToString(),
                                            (yosanHead.Year + Constants.GetYearRange(yosanHead.YosanSbtID) - 1).ToString()
                                           );

            //xls出力
            return WriteFile(xls, sheetIndex, selectedVessel.VesselTypeName, yosanHead, jisekis前年度, yosanItems今年度, jisekis今年度, yosanItems20年分, null, unit, is前年度実績出力);
        }


        private bool Write_船(ExcelCreator.Xlsx.XlsxCreator xls, int sheetIndex, MsVessel vessel, BgYosanHead yosanHead, decimal unit, bool is前年度実績出力)
        {
            // 前年度実績
            List<BgJiseki> jisekis前年度 =
              BgJiseki.GetRecords_年単位_船(this.loginUser,
                                            vessel.MsVesselID,
                                            (yosanHead.Year - 1).ToString(),
                                            yosanHead.Year.ToString()
                                           );

            // 予算
            List<BgYosanItem> yosanItems今年度 =
              BgYosanItem.GetRecords_月単位
              (this.loginUser,
                                            yosanHead.YosanHeadID,
                                            vessel.MsVesselID,
                                            (yosanHead.Year).ToString(),
                                            (yosanHead.Year + 19).ToString()
                                           );
            // 今年度実績
            List<BgJiseki> jisekis今年度 =
              BgJiseki.GetRecords_月単位_船(this.loginUser,
                                            vessel.MsVesselID,
                                            yosanHead.Year + NBaseData.DS.Constants.PADDING_MONTHS[0],
                                            (yosanHead.Year + 1) + NBaseData.DS.Constants.PADDING_MONTHS[11]
                                           );


            // 20年分（年）
            List<BgYosanItem> yosanItems20年分 =
              BgYosanItem.GetRecords_年単位_船(this.loginUser,
                                            yosanHead.YosanHeadID,
                                            vessel.MsVesselID,
                                            yosanHead.Year.ToString(),
                                            (yosanHead.Year + Constants.GetYearRange(yosanHead.YosanSbtID) - 1).ToString()
                                           );


            // 船稼働
            List<BgKadouVessel> kadouVessels = BgKadouVessel.GetRecordsByYosanHeadIDAndMsVesselID(this.loginUser,
                                                                              yosanHead.YosanHeadID,
                                                                              vessel.MsVesselID);

            //xls出力
            return WriteFile(xls, sheetIndex, vessel.VesselName, yosanHead, jisekis前年度, yosanItems今年度, jisekis今年度, yosanItems20年分, kadouVessels, unit, is前年度実績出力);
        }


        private bool WriteFile(ExcelCreator.Xlsx.XlsxCreator xls, int sheetIndex, string name, BgYosanHead yosanHead, List<BgJiseki> jisekis前年度, List<BgYosanItem> yosanItems今年度, List<BgJiseki> jisekis今年度,
                               List<BgYosanItem> yosanItems20年分,
                               List<BgKadouVessel> kadouVessels, decimal unit, bool is前年度実績出力)
        {
            xls.Header("", yosanHead.Year.ToString() + "年度 予算総括表", "日付：&D\n  ページ &P / &N");

            var jiseki前年度Dic = JisekiBuilder.Build_年(jisekis前年度);
            var jiseki今年度Dic = JisekiBuilder.Build_月(jisekis今年度);

            xls.SheetNo = sheetIndex;
            xls.SheetName = name;

            xls.Cell("**船名").Value = name;
            xls.Cell("H4").Value = "前年度";
            xls.Cell("H5").Value = "（" + (yosanHead.Year - 1) + "年度）";
            xls.Cell("I4").Value = "初年度（" + yosanHead.Year + "年度）";

            int start = 44;
            for (int i = 0; i < Constants.GetYearRange(yosanHead.YosanSbtID); i++)
            {
                xls.Cell("I4", start + i, 0).Value = (yosanHead.Year + i) + "年度";

                if (kadouVessels != null && i < kadouVessels.Count)
                {
                    if (i == 0)
                    {
                        Write_船稼働_初年度(kadouVessels[i], xls);
                    }
                    else
                    {
                        Write_船稼働_20年分(kadouVessels[i], xls, start + i);
                    }
                }
            }

            foreach (HimokuTreeNode htNode in HimokuTreeReader.GetHimokuTree())
            {
                if (jiseki前年度Dic.ContainsKey(htNode.MsHimoku.MsHimokuID))
                {
                    decimal amount = GetAmount(jiseki前年度Dic[htNode.MsHimoku.MsHimokuID], htNode.MsHimoku.MsHimokuID);

                    xls.Cell(DetectBaseCell(htNode.MsHimoku.MsHimokuID), -1, 0).Value =
                        amount / unit;
                }
                else
                {
                    xls.Cell(DetectBaseCell(htNode.MsHimoku.MsHimokuID), -1, 0).Value = 0;
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

                if (HimokuTreeReader.GetHimokuTreeNode(yosanItem.MsHimokuID).Children.Count == 0)
                {
                    xls.Cell(DetectBaseCell(yosanItem.MsHimokuID), DetectColumn今年度(yosanItem.Nengetsu) + 1, 0).Value = 0;
                }
            }

            foreach (KeyValuePair<int, Dictionary<string, BgJiseki>> pair in jiseki今年度Dic)
            {
                foreach (KeyValuePair<string, BgJiseki> pair2 in pair.Value)
                {
                    decimal amount = GetAmount(pair2.Value, pair.Key);

                    xls.Cell(DetectBaseCell(pair.Key), DetectColumn今年度(pair2.Key) + 1, 0).Value = amount / unit;
                }
            }

            foreach (BgYosanItem yosanItem in yosanItems20年分)
            {
                decimal amount = GetAmount(yosanItem, yosanItem.MsHimokuID);

                xls.Cell(DetectBaseCell(yosanItem.MsHimokuID), DetectColumn20年分(yosanHead, yosanItem.Nengetsu), 0).Value = amount / unit;
            }

            if (!is前年度実績出力)
            {
                xls.Cell("H4").ColWidth = 0;
            }
            return true;
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

        #region オリジナルコード Write_船稼働_初年度(BgKadouVessel bgKadouVessel, ExcelCreator.Xlsx.XlsxCreator xls) 
        //private void Write_船稼働_初年度(BgKadouVessel bgKadouVessel, ExcelCreator.Xlsx.XlsxCreator xls)
        //{
        //    decimal fukadoubiTotal = bgKadouVessel.Fukadoubi1 +
        //                                   bgKadouVessel.Fukadoubi2 +
        //                                   bgKadouVessel.Fukadoubi3;
        //    decimal kamikiFukadoubi = 0;
        //    int kamikiDays = 0;
        //    decimal kamikiFukadouMonth = 0;
        //    decimal kamikiKadouMonth = 0;

        //    decimal shimokiFukadoubi = 0;
        //    int shimokiDays = 0;
        //    decimal shimokiFukadouMonth = 0;
        //    decimal shimokiKadouMonth = 0;

        //    int col = 0;
        //    for (int i = 0; i < 12; i++)
        //    {
        //        int year = i < 9 ? bgKadouVessel.Year : bgKadouVessel.Year + 1;
        //        int daysOfMonth;

        //        if (NBaseData.DS.Constants.INT_MONTHS[i] != 12)
        //        {
        //            daysOfMonth = (new DateTime(year, NBaseData.DS.Constants.INT_MONTHS[i] + 1, 1) -
        //                                   new DateTime(year, NBaseData.DS.Constants.INT_MONTHS[i], 1)).Days;
        //        }
        //        else
        //        {
        //            daysOfMonth = (new DateTime(year + 1, 1, 1) -
        //                                   new DateTime(year, NBaseData.DS.Constants.INT_MONTHS[i], 1)).Days;
        //        }

        //        decimal fukadouMonth = Math.Round(((decimal)fukadoubiTotal / (decimal)daysOfMonth), 2, MidpointRounding.AwayFromZero);
        //        decimal kadouMonth = 1 - fukadouMonth;

        //        if (bgKadouVessel.NyukyoMonth == NBaseData.DS.Constants.INT_MONTHS[i])
        //        {
        //            // 不稼動事由（発生月）/暦日
        //            xls.Cell("I7", col, 0).Value = bgKadouVessel.NyukyoKind +
        //                                          "(" +
        //                                          bgKadouVessel.NyukyoMonth +
        //                                          ")" +
        //                                          "／" +
        //                                          daysOfMonth;

        //            // 不稼動発生日数 ［同月数］
        //            xls.Cell("I8", col, 0).Value = fukadoubiTotal +
        //                                           "[" +
        //                                           fukadouMonth +
        //                                           "]";

        //            // 稼働日数 ［同月数］
        //            xls.Cell("I9", col, 0).Value = (daysOfMonth - fukadoubiTotal) +
        //                                          "[" +
        //                                           kadouMonth +
        //                                          "]";

        //            if (i < 6)
        //            {
        //                kamikiFukadoubi = fukadoubiTotal;
        //            }
        //            else
        //            {
        //                shimokiFukadoubi = fukadoubiTotal;
        //            }
        //        }
        //        else
        //        {
        //            // 不稼動事由（発生月）/暦日
        //            xls.Cell("I7", col, 0).Value = "--" +
        //                                         "(--)" +
        //                                         "／" +
        //                                         daysOfMonth;

        //            // 不稼動発生日数 ［同月数］
        //            xls.Cell("I8", col, 0).Value = "-" +
        //                                           "[0.00]";

        //            // 稼働日数 ［同月数］
        //            xls.Cell("I9", col, 0).Value = daysOfMonth +
        //                                          "[" +
        //                                           kadouMonth +
        //                                          "]";
        //        }

        //        if (i < 6)
        //        {
        //            kamikiDays += daysOfMonth;
        //            kamikiFukadouMonth += fukadouMonth;
        //            kamikiKadouMonth += kadouMonth;
        //        }
        //        else
        //        {
        //            shimokiDays += daysOfMonth;
        //            shimokiFukadouMonth += fukadouMonth;
        //            shimokiKadouMonth += kadouMonth;
        //        }

        //        col += 3;

        //        if (i == 5)
        //        {
        //            // 不稼動事由（発生月）/暦日
        //            xls.Cell("I7", col, 0).Value = "--" +
        //                                         "(--)" +
        //                                         "／" +
        //                                         kamikiDays;

        //            // 不稼動発生日数 ［同月数］
        //            xls.Cell("I8", col, 0).Value = kamikiFukadoubi +
        //                                           "[" +
        //                                           kamikiFukadouMonth +
        //                                           "]";

        //            // 稼働日数 ［同月数］
        //            xls.Cell("I9", col, 0).Value = (kamikiDays - kamikiFukadoubi) +
        //                                          "[" +
        //                                           kamikiKadouMonth +
        //                                          "]";
        //            col += 3;
        //        }
        //        else if (i == 11)
        //        {
        //            // 不稼動事由（発生月）/暦日
        //            xls.Cell("I7", col, 0).Value = "--" +
        //                                         "(--)" +
        //                                         "／" +
        //                                         shimokiDays;

        //            // 不稼動発生日数 ［同月数］
        //            xls.Cell("I8", col, 0).Value = shimokiFukadoubi +
        //                                           "[" +
        //                                           shimokiFukadouMonth +
        //                                           "]";

        //            // 稼働日数 ［同月数］
        //            xls.Cell("I9", col, 0).Value = (shimokiDays - shimokiFukadoubi) +
        //                                          "[" +
        //                                           shimokiKadouMonth +
        //                                          "]";
        //            col += 3;

        //            // 不稼動事由（発生月）/暦日
        //            xls.Cell("I7", col, 0).Value = "--" +
        //                                         "(--)" +
        //                                         "／" +
        //                                         (kamikiDays + shimokiDays);

        //            // 不稼動発生日数 ［同月数］
        //            xls.Cell("I8", col, 0).Value = (kamikiFukadoubi + shimokiFukadoubi) +
        //                                           "[" +
        //                                           (kamikiFukadouMonth + shimokiFukadouMonth) +
        //                                           "]";

        //            // 稼働日数 ［同月数］
        //            xls.Cell("I9", col, 0).Value = ((kamikiDays + shimokiDays) - (kamikiFukadoubi + shimokiFukadoubi)) +
        //                                          "[" +
        //                                           (kamikiKadouMonth + shimokiKadouMonth) +
        //                                          "]";
        //        }
        //    }
        //}
        #endregion
        // 2010.09.13 不具合対応
        private void Write_船稼働_初年度(BgKadouVessel bgKadouVessel, ExcelCreator.Xlsx.XlsxCreator xls)
        {
            decimal fukadoubiTotal = bgKadouVessel.Fukadoubi1 +
                                           bgKadouVessel.Fukadoubi2 +
                                           bgKadouVessel.Fukadoubi3;
            int kamikiDays = 0;
            decimal kamikiFukadoubi = 0;
            decimal kamikiFukadouMonth = 0;
            decimal kamikiKadouMonth = 0;
            decimal shimokiFukadoubi = 0;
            int shimokiDays = 0;
            decimal shimokiFukadouMonth = 0;
            decimal shimokiKadouMonth = 0;

            int col = 0;
            for (int i = 0; i < 12; i++)
            {
                int year = i < 9 ? bgKadouVessel.Year : bgKadouVessel.Year + 1;
                int daysOfMonth;

                if (NBaseData.DS.Constants.INT_MONTHS[i] != 12)
                {
                    daysOfMonth = (new DateTime(year, NBaseData.DS.Constants.INT_MONTHS[i] + 1, 1) -
                                           new DateTime(year, NBaseData.DS.Constants.INT_MONTHS[i], 1)).Days;
                }
                else
                {
                    daysOfMonth = (new DateTime(year + 1, 1, 1) -
                                           new DateTime(year, NBaseData.DS.Constants.INT_MONTHS[i], 1)).Days;
                }


                if (bgKadouVessel.NyukyoMonth == NBaseData.DS.Constants.INT_MONTHS[i])
                {
                    decimal fukadouMonth = Math.Round(((decimal)fukadoubiTotal / (decimal)daysOfMonth), 2, MidpointRounding.AwayFromZero);
                    decimal kadouMonth = 1 - fukadouMonth;

                    // 不稼動事由（発生月）/暦日
                    xls.Cell("I7", col, 0).Value = bgKadouVessel.NyukyoKind +
                                                  "(" +
                                                  bgKadouVessel.NyukyoMonth +
                                                  ")" +
                                                  "／" +
                                                  daysOfMonth;

                    // 不稼動発生日数 ［同月数］
                    xls.Cell("I8", col, 0).Value = fukadoubiTotal +
                                                   "[" +
                                                   fukadouMonth +
                                                   "]";

                    // 稼働日数 ［同月数］
                    xls.Cell("I9", col, 0).Value = (daysOfMonth - fukadoubiTotal) +
                                                  "[" +
                                                   kadouMonth +
                                                  "]";

                    if (i < 6)
                    {
                        kamikiFukadoubi = fukadoubiTotal;
                    }
                    else
                    {
                        shimokiFukadoubi = fukadoubiTotal;
                    }
                }
                else
                {
                    // 不稼動事由（発生月）/暦日
                    xls.Cell("I7", col, 0).Value = "--" +
                                                 "(--)" +
                                                 "／" +
                                                 daysOfMonth;

                    // 不稼動発生日数 ［同月数］
                    xls.Cell("I8", col, 0).Value = "-" +
                                                   "[0.00]";

                    // 稼働日数 ［同月数］
                    xls.Cell("I9", col, 0).Value = daysOfMonth +
                                                  "[1.00]";
                }

                if (i < 6)
                {
                    kamikiDays += daysOfMonth;
                }
                else
                {
                    shimokiDays += daysOfMonth;
                }

                col += 3;

                if (i == 5)
                {
                    kamikiFukadouMonth = Math.Round(((decimal)kamikiFukadoubi / (decimal)kamikiDays), 2, MidpointRounding.AwayFromZero);
                    kamikiKadouMonth = 1 - kamikiFukadouMonth;

                    // 不稼動事由（発生月）/暦日
                    xls.Cell("I7", col, 0).Value = "--" +
                                                 "(--)" +
                                                 "／" +
                                                 kamikiDays;

                    // 不稼動発生日数 ［同月数］
                    xls.Cell("I8", col, 0).Value = kamikiFukadoubi +
                                                   "[" +
                                                   kamikiFukadouMonth +
                                                   "]";

                    // 稼働日数 ［同月数］
                    xls.Cell("I9", col, 0).Value = (kamikiDays - kamikiFukadoubi) +
                                                  "[" +
                                                   kamikiKadouMonth +
                                                  "]";
                    col += 3;
                }
                else if (i == 11)
                {
                    shimokiFukadouMonth = Math.Round(((decimal)shimokiFukadoubi / (decimal)shimokiDays), 2, MidpointRounding.AwayFromZero);
                    shimokiKadouMonth = 1 - shimokiFukadouMonth;

                    // 不稼動事由（発生月）/暦日
                    xls.Cell("I7", col, 0).Value = "--" +
                                                 "(--)" +
                                                 "／" +
                                                 shimokiDays;

                    // 不稼動発生日数 ［同月数］
                    xls.Cell("I8", col, 0).Value = shimokiFukadoubi +
                                                   "[" +
                                                   shimokiFukadouMonth +
                                                   "]";

                    // 稼働日数 ［同月数］
                    xls.Cell("I9", col, 0).Value = (shimokiDays - shimokiFukadoubi) +
                                                  "[" +
                                                   shimokiKadouMonth +
                                                  "]";
                    
                    col += 3;
                    decimal nenFukadouMonth = Math.Round(((decimal)(kamikiFukadoubi + shimokiFukadoubi) / (decimal)(kamikiDays + shimokiDays)), 2, MidpointRounding.AwayFromZero);
                    decimal nenKadouMonth = 1 - nenFukadouMonth;



                    // 不稼動事由（発生月）/暦日
                    xls.Cell("I7", col, 0).Value = "--" +
                                                 "(--)" +
                                                 "／" +
                                                 (kamikiDays + shimokiDays);

                    // 不稼動発生日数 ［同月数］
                    xls.Cell("I8", col, 0).Value = (kamikiFukadoubi + shimokiFukadoubi) +
                                                   "[" +
                                                   nenFukadouMonth +
                                                   "]";

                    // 稼働日数 ［同月数］
                    xls.Cell("I9", col, 0).Value = ((kamikiDays + shimokiDays) - (kamikiFukadoubi + shimokiFukadoubi)) +
                                                  "[" +
                                                   nenKadouMonth +
                                                  "]";
                }
            }
        }


        private void Write_船稼働_20年分(BgKadouVessel bgKadouVessel, ExcelCreator.Xlsx.XlsxCreator xls, int col)
        {
            int daysOfYear = (new DateTime(bgKadouVessel.Year, 12, 31) - new DateTime(bgKadouVessel.Year, 1, 1)).Days + 1;
            decimal fukadoubiTotal = bgKadouVessel.Fukadoubi1 +
                                           bgKadouVessel.Fukadoubi2 +
                                           bgKadouVessel.Fukadoubi3;
            decimal fukadouMonth = Math.Round(((decimal)fukadoubiTotal / (decimal)daysOfYear) * 12, 2, MidpointRounding.AwayFromZero);
            decimal kadouMonth = 12 - fukadouMonth;

            string nyukyoKind = bgKadouVessel.NyukyoKind != string.Empty ? bgKadouVessel.NyukyoKind : "--";

            // 不稼動事由（発生月）/暦日
            xls.Cell("I7", col, 0).Value = nyukyoKind +
                                           "(" +
                                           bgKadouVessel.NyukyoMonth +
                                           ")" +
                                           "／" +
                                           daysOfYear;

            // 不稼動発生日数 ［同月数］
            xls.Cell("I8", col, 0).Value = fukadoubiTotal +
                                           "[" +
                                           fukadouMonth +
                                           "]";

            // 稼働日数 ［同月数］
            xls.Cell("I9", col, 0).Value = (daysOfYear - fukadoubiTotal) +
                                          "[" +
                                           kadouMonth +
                                          "]";
        }


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
                        return (i + 1) * 3;
                    }
                    else
                    {
                        return i * 3;
                    }
                }
            }

            return 0;
        }


        private int DetectColumn20年分(BgYosanHead yosanHead, string nengetsu)
        {
            int start = 44;
            int year = Int32.Parse(nengetsu.Substring(0, 4).Trim());

            int col = start + year - yosanHead.Year;

            if (year == yosanHead.Year)
            {
                col -= 2;
            }

            return col;
        }
    }
}
