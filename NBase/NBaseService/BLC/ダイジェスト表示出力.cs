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
using NBaseData.DS;
using System.Collections.Generic;
using ExcelCreator=AdvanceSoftware.ExcelCreator;
using NBaseData.BLC;
using NBaseUtil;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_Excelダイジェスト表示(MsUser loginUser, BgYosanHead yosanHead, decimal unit, string syear, string smonth, string eyear, string emonth);
    }


    public partial class Service
    {
        public byte[] BLC_Excelダイジェスト表示(MsUser loginUser, BgYosanHead yosanHead, decimal unit, string syear, string smonth, string eyear, string emonth)
        {
            Constants.LoginUser = loginUser;

            #region 元になるファイルの確認と出力ファイル生成
            string BaseFileName = "ダイジェスト表示";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateName = path + "Template_" + BaseFileName + ".xlsx";
            string outPutFileName = path + "outPut_[" + loginUser.FullName + "]_" + BaseFileName + ".xlsx";

            bool exists = System.IO.File.Exists(templateName);
            if (exists == false)
            {
                return null;
            }
            #endregion

            ExcelCreator.Xlsx.XlsxCreator  xls = new ExcelCreator.Xlsx.XlsxCreator ();

            try
            {
                if (xls.OpenBook(outPutFileName, templateName) == 0)
                {
                    List<ダイジェスト出力> digests = null;
                    if (syear == eyear && smonth == emonth)
                    {
                        xls.Cell("**NENGETSU").Value = syear + "年" + smonth + "月　実績";

                        // 2015.10 基幹連携サーバで取得したものを利用する
                        //digests = ダイジェスト出力.GetRecords単月(loginUser, syear, syear + smonth);
                        //using (WingCoreServer.Service1Client serviceClient = new WingCoreServer.Service1Client())
                        //{
                        //    digests = serviceClient.BLC_基幹システム連携読み込み処理_ダイジェスト出力_単月(loginUser, syear, syear + smonth);
                        //}
                    }
                    else
                    {
                        xls.Cell("**NENGETSU").Value = eyear + "年" + emonth + "月　実績(累計)";

                        // 2015.10 基幹連携サーバで取得したものを利用する
                        //digests = ダイジェスト出力.GetRecords累計(loginUser, syear, eyear + emonth);
                        //using (WingCoreServer.Service1Client serviceClient = new WingCoreServer.Service1Client())
                        //{
                        //    digests = serviceClient.BLC_基幹システム連携読み込み処理_ダイジェスト出力_累計(loginUser, syear, eyear + emonth);
                        //}
                    }
                    if (unit == 1000)
                    {
                        xls.Cell("**TANI").Value = "単位：千円";
                    }
                    else
                    {
                        xls.Cell("**TANI").Value = "単位：百万円";
                    }


                    Dictionary<string, List<MsVessel>> vesselDic = CreateVesselDic(loginUser);

                    int x = 1, y = 4;
                    List<int> y_小計 = new List<int>(); ;
                    
                    foreach (List<MsVessel> vessels in vesselDic.Values)
                    {
                        foreach (MsVessel v in vessels)
                        {
                            Output_船(loginUser, yosanHead.YosanHeadID, unit, syear, smonth, eyear, emonth, xls, x, y, v, GetDigest(digests, v.KaikeiBumonCode));

                            y++;
                        }

                        Output_小計(xls, x, y, vessels.Count);
                        y_小計.Add(y);
                        y++;
                    }

                    Output_合計(xls, x, y, y_小計);

                    Draw_縦罫線(xls, x, 4, y);
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                xls.CloseBook(true);
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


        private void Output_船(MsUser loginUser, int yosanHeadId, decimal unit, string syear, string smonth, string eyear, string emonth, ExcelCreator.Xlsx.XlsxCreator  xls, int x, int y, MsVessel v, ダイジェスト出力 digest)
        {
            xls.Pos(x, y).Value = v.VesselTypeName;
            xls.Pos(x + 1, y).Value = v.VesselName;

            Dictionary<int, decimal> amountDic = BuildJisekiDic(loginUser, v.MsVesselID, syear + smonth, eyear + emonth);

            // 運賃
            xls.Pos(x + 2, y).Value = digest == null ? 0 : digest.運賃 / (unit / 1000); //GetAmount(amountDic, 2) / unit;
            // 貸船料
            xls.Pos(x + 3, y).Value = digest == null ? 0 : digest.貸船料 / (unit / 1000); //GetAmount(amountDic, 8) / unit;
            // 他船取扱手数料
            xls.Pos(x + 4, y).Value = digest == null ? 0 : digest.他船取扱手数料 / (unit / 1000); //GetAmount(amountDic, 13) / unit;
            // その他収益
            xls.Pos(x + 5, y).Value = digest == null ? 0 : digest.その他海運業収益 / (unit / 1000); //GetAmount(amountDic, 14) / unit;
            // 合計
            xls.Pos(x + 6, y).Value = digest == null ? 0 : digest.海運業収益 / (unit / 1000); //"=SUM(" + ExcelUtils.ToCellName(x + 2, y) +
                                                                                              //    ":" + ExcelUtils.ToCellName(x + 5, y) + ")";
            // ｳﾁ 過月度分
            xls.Pos(x + 7, y).Value = digest == null ? 0 : digest.ｳﾁ過月度分_収益 / (unit / 1000); //0;
            // ｳﾁ 為替分
            xls.Pos(x + 8, y).Value = digest == null ? 0 : digest.ｳﾁ為替分_収益 / (unit / 1000); //0;
            // 運航費
            xls.Pos(x + 9, y).Value = digest == null ? 0 : digest.運航費 / (unit / 1000); //GetAmount(amountDic, 16) / unit;
            // 運航損益
            xls.Pos(x + 10, y).Value = "=" + ExcelUtils.ToCellName(x + 2, y) +
                                           "+" + ExcelUtils.ToCellName(x + 3, y) +
                                           "-" + ExcelUtils.ToCellName(x + 9, y);
            // 船費
            xls.Pos(x + 11, y).Value = digest == null ? 0 : digest.船費 / (unit / 1000); //GetAmount(amountDic, 21) / unit;
            // ｳﾁ 減価償却費
            xls.Pos(x + 12, y).Value = GetAmount(amountDic, 57) / unit; //0;
            // 借船料
            xls.Pos(x + 13, y).Value = digest == null ? 0 : digest.借船料 / (unit / 1000); //GetAmount(amountDic, 62) / unit;
            // その他海運業費用
            xls.Pos(x + 14, y).Value = digest == null ? 0 : digest.その他海運業費用 / (unit / 1000); //GetAmount(amountDic, 63) / unit;
            // 合計
            xls.Pos(x + 15, y).Value = digest == null ? 0 : digest.海運業費用 / (unit / 1000); //"=" + ExcelUtils.ToCellName(x + 9, y) +
                                                                                               //"+" + ExcelUtils.ToCellName(x + 11, y) +
                                                                                               //"+" + ExcelUtils.ToCellName(x + 13, y) +
                                                                                               //"+" + ExcelUtils.ToCellName(x + 14, y);
            // ｳﾁ 過月度分
            xls.Pos(x + 16, y).Value = digest == null ? 0 : digest.ｳﾁ過月度分_費用 / (unit / 1000); //0;
            // ｳﾁ 為替分
            xls.Pos(x + 17, y).Value = digest == null ? 0 : digest.ｳﾁ為替分_費用 / (unit / 1000); //0;
            // 売上総利益
            xls.Pos(x + 18, y).Value = "=" + ExcelUtils.ToCellName(x + 6, y) +
                                           "-" + ExcelUtils.ToCellName(x + 15, y);
            // 販管費
            xls.Pos(x + 19, y).Value = digest == null ? 0 : digest.販管費 / (unit / 1000); //GetAmount(amountDic, 65) / unit;
            // 営業損益
            xls.Pos(x + 20, y).Value = "=" + ExcelUtils.ToCellName(x + 18, y) +
                                           "-" + ExcelUtils.ToCellName(x + 19, y);
            // 営業外収入
            xls.Pos(x + 21, y).Value = GetAmount(amountDic, 69) / unit;
            // 設備金利
            xls.Pos(x + 22, y).Value = GetAmount(amountDic, 71) / unit;
            // その他営業外費用
            xls.Pos(x + 23, y).Value = GetAmount(amountDic, 72) / unit;
            // 営業外費用
            xls.Pos(x + 24, y).Value = "=" + ExcelUtils.ToCellName(x + 22, y) +
                                           "+" + ExcelUtils.ToCellName(x + 23, y);
            // 経常損益
            xls.Pos(x + 25, y).Value = "=" + ExcelUtils.ToCellName(x + 20, y) +
                                           "+" + ExcelUtils.ToCellName(x + 21, y) +
                                           "-" + ExcelUtils.ToCellName(x + 24, y);

            // 予算
            xls.Pos(x + 26, y).Value = Get_予算_経常損益(loginUser, yosanHeadId, v.MsVesselID, int.Parse(syear), int.Parse(smonth), int.Parse(eyear), int.Parse(emonth)) / unit;
            // 計画比
            string cell1 = ExcelUtils.ToCellName(x + 25, y);
            string cell2 = ExcelUtils.ToCellName(x + 26, y);
            xls.Pos(x + 27, y).Value = "=IF(AND(" + cell1 + "<>\"\"," + cell1 + "<>0),IF(AND(" + cell2 + "<>\"\"," + cell2 + "<>0)," + cell1 + "/" + cell2 + ",\"\"),\"\")";

            string lineCellStr = ExcelUtils.ToCellName(x, y) +
                                          ":" + ExcelUtils.ToCellName(x + 27, y);

            xls.Cell(lineCellStr).Attr.LineBottom (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
        }


        private void Output_小計(ExcelCreator.Xlsx.XlsxCreator  xls, int x, int y, int vesselCount)
        {
            xls.Pos(x + 1, y).Value = "小計";
            xls.Pos(x + 1, y).Attr.HorizontalAlignment = ExcelCreator.HorizontalAlignment.Center;
            
            // 運賃
            xls.Pos(x + 2, y).Value = CreateSumStr(x + 2, y, vesselCount);
            // 貸船料
            xls.Pos(x + 3, y).Value = CreateSumStr(x + 3, y, vesselCount);
            // 他船取扱手数料
            xls.Pos(x + 4, y).Value = CreateSumStr(x + 4, y, vesselCount);
            // その他収益
            xls.Pos(x + 5, y).Value = CreateSumStr(x + 5, y, vesselCount);
            // 合計
            xls.Pos(x + 6, y).Value = CreateSumStr(x + 6, y, vesselCount);

            // ｳﾁ 過月度分
            xls.Pos(x + 7, y).Value = CreateSumStr(x + 7, y, vesselCount); 
            // ｳﾁ 為替分
            xls.Pos(x + 8, y).Value = CreateSumStr(x + 8, y, vesselCount); 
            // 運航費
            xls.Pos(x + 9, y).Value = CreateSumStr(x + 9, y, vesselCount); 
            // 運航損益
            xls.Pos(x + 10, y).Value = CreateSumStr(x + 10, y, vesselCount); 
            // 船費
            xls.Pos(x + 11, y).Value = CreateSumStr(x + 11, y, vesselCount); 
            // ｳﾁ 減価償却費
            xls.Pos(x + 12, y).Value = CreateSumStr(x + 12, y, vesselCount); 
            // 借船料
            xls.Pos(x + 13, y).Value = CreateSumStr(x + 13, y, vesselCount); 
            // その他海運業費用
            xls.Pos(x + 14, y).Value = CreateSumStr(x + 14, y, vesselCount); 
            // 合計
            xls.Pos(x + 15, y).Value = CreateSumStr(x + 15, y, vesselCount); 
            // ｳﾁ 過月度分
            xls.Pos(x + 16, y).Value = CreateSumStr(x + 16, y, vesselCount); 
            // ｳﾁ 為替分
            xls.Pos(x + 17, y).Value = CreateSumStr(x + 17, y, vesselCount); 
            // 売上総利益
            xls.Pos(x + 18, y).Value = CreateSumStr(x + 18, y, vesselCount); 
            // 販管費
            xls.Pos(x + 19, y).Value = CreateSumStr(x + 19, y, vesselCount); 
            // 営業損益
            xls.Pos(x + 20, y).Value = CreateSumStr(x + 20, y, vesselCount); 
            // 営業外収入
            xls.Pos(x + 21, y).Value = CreateSumStr(x + 21, y, vesselCount); 
            // 設備金利
            xls.Pos(x + 22, y).Value = CreateSumStr(x + 22, y, vesselCount); 
            // その他営業外費用
            xls.Pos(x + 23, y).Value = CreateSumStr(x + 23, y, vesselCount); 
            // 営業外費用
            xls.Pos(x + 24, y).Value = CreateSumStr(x + 24, y, vesselCount); 
            // 経常損益
            xls.Pos(x + 25, y).Value = CreateSumStr(x + 25, y, vesselCount);
            // 予算
            xls.Pos(x + 26, y).Value = CreateSumStr(x + 26, y, vesselCount);
            // 計画比
            xls.Pos(x + 27, y).Value = CreateSumStr(x + 27, y, vesselCount);

            string lineCellStr = ExcelUtils.ToCellName(x, y) +
                                          ":" + ExcelUtils.ToCellName(x + 27, y);

            xls.Cell(lineCellStr).Attr.LineBottom(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
        }

        
        private void Output_合計(ExcelCreator.Xlsx.XlsxCreator  xls, int x, int y, List<int> y_小計)
        {
            xls.Pos(x + 1, y).Value = "合計";
            xls.Pos(x + 1, y).Attr.HorizontalAlignment = ExcelCreator.HorizontalAlignment.Center;

            // 運賃
            xls.Pos(x + 2, y).Value = CreateSumStr(x + 2, y_小計);
            // 貸船料
            xls.Pos(x + 3, y).Value = CreateSumStr(x + 3, y_小計);
            // 他船取扱手数料
            xls.Pos(x + 4, y).Value = CreateSumStr(x + 4, y_小計);
            // その他収益
            xls.Pos(x + 5, y).Value = CreateSumStr(x + 5, y_小計);
            // 合計
            xls.Pos(x + 6, y).Value = CreateSumStr(x + 6, y_小計);

            // ｳﾁ 過月度分
            xls.Pos(x + 7, y).Value = CreateSumStr(x + 7, y_小計);
            // ｳﾁ 為替分
            xls.Pos(x + 8, y).Value = CreateSumStr(x + 8, y_小計);
            // 運航費
            xls.Pos(x + 9, y).Value = CreateSumStr(x + 9, y_小計);
            // 運航損益
            xls.Pos(x + 10, y).Value = CreateSumStr(x + 10, y_小計);
            // 船費
            xls.Pos(x + 11, y).Value = CreateSumStr(x + 11, y_小計);
            // ｳﾁ 減価償却費
            xls.Pos(x + 12, y).Value = CreateSumStr(x + 12, y_小計);
            // 借船料
            xls.Pos(x + 13, y).Value = CreateSumStr(x + 13, y_小計);
            // その他海運業費用
            xls.Pos(x + 14, y).Value = CreateSumStr(x + 14, y_小計);
            // 合計
            xls.Pos(x + 15, y).Value = CreateSumStr(x + 15, y_小計);
            // ｳﾁ 過月度分
            xls.Pos(x + 16, y).Value = CreateSumStr(x + 16, y_小計);
            // ｳﾁ 為替分
            xls.Pos(x + 17, y).Value = CreateSumStr(x + 17, y_小計);
            // 売上総利益
            xls.Pos(x + 18, y).Value = CreateSumStr(x + 18, y_小計);
            // 販管費
            xls.Pos(x + 19, y).Value = CreateSumStr(x + 19, y_小計);
            // 営業損益
            xls.Pos(x + 20, y).Value = CreateSumStr(x + 20, y_小計);
            // 営業外収入
            xls.Pos(x + 21, y).Value = CreateSumStr(x + 21, y_小計);
            // 設備金利
            xls.Pos(x + 22, y).Value = CreateSumStr(x + 22, y_小計);
            // その他営業外費用
            xls.Pos(x + 23, y).Value = CreateSumStr(x + 23, y_小計);
            // 営業外費用
            xls.Pos(x + 24, y).Value = CreateSumStr(x + 24, y_小計);
            // 経常損益
            xls.Pos(x + 25, y).Value = CreateSumStr(x + 25, y_小計);
            // 予算
            xls.Pos(x + 26, y).Value = CreateSumStr(x + 26, y_小計);
            // 計画比
            xls.Pos(x + 27, y).Value = CreateSumStr(x + 27, y_小計);

            string lineCellStr = ExcelUtils.ToCellName(x, y) +
                                          ":" + ExcelUtils.ToCellName(x + 27, y);

            xls.Cell(lineCellStr).Attr.LineBottom(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
        }

        
        private void Draw_縦罫線(ExcelCreator.Xlsx.XlsxCreator  xls, int x, int yStart, int yEnd)
        {
            for (int i = 0; i < 28; i++)
            {
                string lineCellStr = ExcelUtils.ToCellName(x + i, yStart) + ":" + ExcelUtils.ToCellName(x + i, yEnd);

                if (i == 0)
                {
                    xls.Cell(lineCellStr).Attr.LineLeft(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
                }
                else if (i == 25 || i == 27)
                {
                    xls.Cell(lineCellStr).Attr.LineRight(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
                }
                else if (i == 1 || i == 8 || i == 17 || i == 18 || i == 19 || i == 20 || i == 24)
                {
                    xls.Cell(lineCellStr).Attr.LineRight(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                }
                else
                {
                    xls.Cell(lineCellStr).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                }
            }
        }

        
        private object CreateSumStr(int x, List<int> y_小計)
        {
            string result = "=";

            for (int i = 0; i < y_小計.Count; i++)
            {
                if (i > 0)
                {
                    result += "+";
                }
                
                result += ExcelUtils.ToCellName(x, y_小計[i]);
            }

            return result;
        }


        private string CreateSumStr(int x, int y, int vesselCount)
        {
            return "=SUM(" + ExcelUtils.ToCellName(x, y - vesselCount) + ":" + ExcelUtils.ToCellName(x, y - 1) + ")";
        }


        private Dictionary<string, List<MsVessel>> CreateVesselDic(MsUser loginUser)
        {
            Dictionary<string, List<MsVessel>> vesselDic = new Dictionary<string, List<MsVessel>>();

            List<MsVessel> vessels = MsVessel.GetRecordsByYojitsuEnabled(loginUser);

            foreach (MsVessel v in vessels)
            {
                if (!vesselDic.ContainsKey(v.MsVesselTypeID))
                {
                    vesselDic[v.MsVesselTypeID] = new List<MsVessel>();
                }

                vesselDic[v.MsVesselTypeID].Add(v);
            }

            return vesselDic;
        }


        private static Dictionary<int, decimal> BuildJisekiDic(MsUser loginUser, int msVesselId, string snengetsu, string enengetsu)
        {
            // <MsHimokuId, YenAmount>
            Dictionary<int, decimal> amountDic = new Dictionary<int, decimal>();

            List<BgJiseki> jisekis = BgJiseki.GetRecords_月単位_船(loginUser, msVesselId, snengetsu, enengetsu);
            Dictionary<int, Dictionary<string, BgJiseki>> jisekiDic = JisekiBuilder.Build_月(jisekis);

            foreach (KeyValuePair<int, Dictionary<string, BgJiseki>> pair in jisekiDic)
            {
                amountDic[pair.Key] = pair.Value.Last().Value.YenAmount;
            }

            return amountDic;
        }


        private decimal Get_予算_経常損益(MsUser loginUser, int yosanHeadId, int msVesselId, int syear, int smonth, int eyear, int emonth)
        {
            int MS_HIMOKU_ID_経常損益 = 73;
            decimal amount = 0;
            
            BgYosanItem yosanItem = null;
            if (syear == eyear && smonth == emonth)
            {
                yosanItem = BgYosanItem.GetRecordByYearHimokuIDMsVesselID(loginUser, yosanHeadId, int.Parse(syear.ToString() + smonth.ToString("00")), MS_HIMOKU_ID_経常損益, msVesselId);
                amount += yosanItem != null ? yosanItem.YenAmount : 0;
            }
            else
            {
                if (smonth < emonth)
                {
                    for (int m = smonth; m <= emonth; m++)
                    {
                        yosanItem = BgYosanItem.GetRecordByYearHimokuIDMsVesselID(loginUser, yosanHeadId, int.Parse(syear.ToString() + m.ToString("00")), MS_HIMOKU_ID_経常損益, msVesselId);
                        amount += yosanItem != null ? yosanItem.YenAmount : 0;
                    }
                }
                else
                {
                    for (int m = smonth; m <= 12; m++)
                    {
                        yosanItem = BgYosanItem.GetRecordByYearHimokuIDMsVesselID(loginUser, yosanHeadId, int.Parse(syear.ToString() + m.ToString("00")), MS_HIMOKU_ID_経常損益, msVesselId);
                        amount += yosanItem != null ? yosanItem.YenAmount : 0;
                    }
                    for (int m = 1; m <= emonth; m++)
                    {
                        yosanItem = BgYosanItem.GetRecordByYearHimokuIDMsVesselID(loginUser, yosanHeadId, int.Parse(eyear.ToString() + m.ToString("00")), MS_HIMOKU_ID_経常損益, msVesselId);
                        amount += yosanItem != null ? yosanItem.YenAmount : 0;
                    }
                }
            }

            return amount;
        }


        private decimal GetAmount(Dictionary<int, decimal> amountDic, int msHimokuId)
        {
            if (!amountDic.ContainsKey(msHimokuId))
            {
                return 0;
            }
            else
            {
                return amountDic[msHimokuId];
            }
        }

        private ダイジェスト出力 GetDigest(List<ダイジェスト出力> digests, string code)
        {
            var tmp = from p in digests
                      where p.BUMON_CD == code
                      select p;
            if (tmp.Count<ダイジェスト出力>() > 0)
            {
                return tmp.First<ダイジェスト出力>();
            }
            else
            {
                return null;
            }
        }
    }
}
