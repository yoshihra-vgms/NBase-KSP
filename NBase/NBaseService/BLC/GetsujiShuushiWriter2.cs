using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseData.BLC;

using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseData.DS;
using NBaseUtil;

namespace NBaseService
{
    class GetsujiShuushiWriter2
    {
        private readonly string fileName;
        MsUser loginUser = null;

        Dictionary<int, int> 費目変換 = new Dictionary<int, int>();
        Dictionary<int, decimal> himokuDic = new Dictionary<int, decimal>();
        Dictionary<string, int> himokuKikanHimokuDic = new Dictionary<string, int>();
        Dictionary<string, Dictionary<int, decimal>> yosanDic = new Dictionary<string, Dictionary<int, decimal>>();
        Dictionary<string, Dictionary<int, decimal>> jisekiDic = new Dictionary<string, Dictionary<int, decimal>>();
        Dictionary<string, Dictionary<int, decimal>> yosanDicUSD = new Dictionary<string, Dictionary<int, decimal>>();
        Dictionary<string, Dictionary<int, decimal>> jisekiDicUSD = new Dictionary<string, Dictionary<int, decimal>>();
        Dictionary<string, Dictionary<int, decimal>> kansanDic = new Dictionary<string, Dictionary<int, decimal>>();
        Dictionary<string, Dictionary<int, decimal>> eikyoDic = new Dictionary<string, Dictionary<int, decimal>>();

MsVesselType debugVesselType = null;

        public GetsujiShuushiWriter2(string fileName, MsUser user)
        {
            this.fileName = fileName;
            this.loginUser = user;

            費目変換.Add(58, 61); // ID:58(固定資産税)は、ID:61(その他船費)に合算する
            費目変換.Add(60, 61); // ID:60(P.I保険料)は、ID:61(その他船費)に合算する
        }


        public bool Write(BgYosanHead selectedYosanHead, decimal unit, string month, bool is累計)
        {
            Constants.LoginUser = this.loginUser;

            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFileName = path + "Template_月次収支資料.xlsx";

            bool result = false;

            ExcelCreator.Xlsx.XlsxCreator  xls = new ExcelCreator.Xlsx.XlsxCreator ();

            decimal jisekiUnit = unit / 1000; // 実績取得SQLでは既に値が1000で割られているため
            List<MsHimoku> himokuList = MsHimoku.GetRecords(loginUser);
            BuildHimokuDic(himokuList);
            BuildHimokuKikanHimokuDic(himokuList);

            try
            {

                if (xls.OpenBook(fileName, templateFileName) == 0)
                {
                    List<MsVesselType> types = MsVesselType.GetRecords(this.loginUser);
                    List<MsVessel> vessels = MsVessel.GetRecordsByYojitsuEnabled(this.loginUser);
                    decimal rate = DetectRate(selectedYosanHead, selectedYosanHead.Year, int.Parse(month));


                    //================================================
                    // 「グループ」(VesselType)毎のシート
                    //================================================
                    int orgSheetNo = 2;
                    int sheetIdx = 0;
                    foreach (MsVesselType type in types)
                    {

                        //==================================================================================
                        // test用 実績を取得するものを制限するため
                        //==================================================================================
                        debugVesselType = type;

                        var byTypeOfVessel = from p in vessels
                                             where p.MsVesselTypeID == type.MsVesselTypeID
                                             orderby p.ShowOrder, p.VesselNo
                                             select p;

                        if (byTypeOfVessel.Count<MsVessel>() == 0)
                            continue;

                        // MsVesselType毎の出力用
                        yosanDic.Add(type.MsVesselTypeID, new Dictionary<int, decimal>());
                        jisekiDic.Add(type.MsVesselTypeID, new Dictionary<int, decimal>());
                        yosanDicUSD.Add(type.MsVesselTypeID, new Dictionary<int, decimal>());
                        jisekiDicUSD.Add(type.MsVesselTypeID, new Dictionary<int, decimal>());
                        kansanDic.Add(type.MsVesselTypeID, new Dictionary<int, decimal>());
                        eikyoDic.Add(type.MsVesselTypeID, new Dictionary<int, decimal>());

  　                    // シートをコピー
                        sheetIdx++;
                        xls.CopySheet(orgSheetNo, orgSheetNo + sheetIdx, "月次収支(" + type.VesselTypeName + ")");

                        xls.SheetNo = orgSheetNo + sheetIdx;
                        string nengetsu = "";
                        if (int.Parse(month) < NBaseCommon.Common.FiscalYearStartMonth)
                        {
                            nengetsu = (selectedYosanHead.Year + 1).ToString();
                        }
                        else
                        {
                            nengetsu = selectedYosanHead.Year.ToString();
                        }
                        nengetsu += month;
                        Write_タイトル(xls, type.VesselTypeName, nengetsu, is累計, rate);

                        // 各船の予実を書き込む
                        int vesselIdx = 0;
                        foreach (MsVessel vessel in byTypeOfVessel)
                        {
                            if (vesselIdx == 0)
                            {
                                // 一番目の船は、シート内の列をコピーしないで利用したいので、最後に書き込む 
                                vesselIdx++;
                                continue;
                            }

                            result = Write_船(xls, vesselIdx, vessel, nengetsu, selectedYosanHead, month, unit, jisekiUnit, is累計);
                            if (!result)
                            {
                                return result;
                            }

                            vesselIdx++;
                        }
                        result = Write_船(xls, 0, byTypeOfVessel.First<MsVessel>(), nengetsu, selectedYosanHead, month, unit, jisekiUnit, is累計);

                        result = Write_為替(xls, vesselIdx, type, unit, jisekiUnit);
                    }

                    // 各Group用テンプレートシートの削除
                    xls.DeleteSheet(2, 1);


                    //================================================
                    // 「ＩＧＴ」シート
                    //================================================
                    xls.SheetNo = 1;
                    Write_タイトル(xls, "IGT", selectedYosanHead.Year.ToString() + month, is累計, rate);
                    int typeIdx = 0;
                    foreach (MsVesselType type in types)
                    {
                        if (typeIdx == 0)
                        {
                            typeIdx++;
                            continue;
                        }
                        Write_グループ(xls, typeIdx, type, unit, jisekiUnit);
                        typeIdx++;
                    }
                    Write_グループ(xls, 0, types[0], unit, jisekiUnit);

                    Write_為替(xls, typeIdx, types, unit, jisekiUnit);


                    //================================================
                    // 「全社」シート
                    //================================================
                    xls.SheetNo = 0;
                    Write_タイトル(xls, "全社", selectedYosanHead.Year.ToString() + month, is累計, rate);
                    Write_全社(xls, types, unit, jisekiUnit);
                    Write_為替(xls, 2, types, unit, jisekiUnit);

                    // 「全社」シートをアクティブに
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


        /// <summary>
        /// シートのタイトル行の出力
        /// </summary>
        /// <param name="xls"></param>
        /// <param name="name"></param>
        /// <param name="ym"></param>
        /// <param name="is累計"></param>
        /// <param name="rate"></param>
        #region private void Write_タイトル(ExcelCreator.Xlsx.XlsxCreator  xls, string name, string ym, bool is累計, decimal rate)
        private void Write_タイトル(ExcelCreator.Xlsx.XlsxCreator  xls, string name, string ym, bool is累計, decimal rate)
        {
            // タイトル行
            string sheetTitle = name + "(" + ym + ")";
            if (is累計)
            {
                sheetTitle += "累計";
            }
            else
            {
                sheetTitle += "単月";
            }
            xls.Cell("A2").Value = sheetTitle;

            // レート
            if (name == "全社")
            {
                xls.Cell("M2").Value = rate;
            }
            else
            {
                xls.Cell("N2").Value = rate;
                xls.Cell("**計").Value = name + " 計";
            }

        }
        #endregion

        /// <summary>
        /// 「各船」のデータをＤＢから取得し出力する
        /// </summary>
        /// <param name="xls"></param>
        /// <param name="vesselIndex"></param>
        /// <param name="vessel"></param>
        /// <param name="nengetsu"></param>
        /// <param name="yosanHead"></param>
        /// <param name="month"></param>
        /// <param name="unit"></param>
        /// <param name="jisekiUnit"></param>
        /// <param name="is累計"></param>
        /// <returns></returns>
        #region private bool Write_船(ExcelCreator.Xlsx.XlsxCreator  xls, int vesselIndex, MsVessel vessel, string nengetsu, BgYosanHead yosanHead, string month, decimal unit, decimal jisekiUnit, bool is累計)
        private bool Write_船(ExcelCreator.Xlsx.XlsxCreator  xls, int vesselIndex, MsVessel vessel, string nengetsu, BgYosanHead yosanHead, string month, decimal unit, decimal jisekiUnit, bool is累計)
        {
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
            #region BG_JISSEKIを使用する場合のコード
            //List<BgJiseki> jisekis今年度 =
            //  BgJiseki.GetRecords_月単位_船(this.loginUser,
            //                                vessel.MsVesselID,
            //                                yosanHead.Year + NBaseData.DS.Constants.PADDING_MONTHS[0],
            //                                (yosanHead.Year + 1) + NBaseData.DS.Constants.PADDING_MONTHS[11]
            //                               );
            #endregion

            List<月次収支データ> getsujiDatas = null;

            //if (vessel.KaikeiBumonCode != null && vessel.KaikeiBumonCode.Length > 0)
            //{
            //    // 2015.10 基幹連携サーバで取得したものを利用する
            //    //getsujiDatas = 月次収支データ.GetRecords(this.loginUser, nengetsu, yosanHead.Year, vessel.KaikeiBumonCode, is累計, himokuKikanHimokuDic);
            //    //using (WingCoreServer.Service1Client serviceClient = new WingCoreServer.Service1Client())
            //    //{
            //    //    getsujiDatas = serviceClient.BLC_基幹システム連携読み込み処理_月次収支データ(loginUser, nengetsu, yosanHead.Year, vessel.KaikeiBumonCode, is累計, himokuKikanHimokuDic);
            //    //}
            //}
            //else
            //{
            //    getsujiDatas = new List<月次収支データ>();
            //}
            getsujiDatas = new List<月次収支データ>();

            //xls出力
            #region BG_JISSEKIを使用する場合のコード
            //return WriteFile(xls, vesselIndex, vessel, nengetsu, yosanHead, month, yosanItems今年度, jisekis今年度, getsujiDatas, unit, is累計);
            #endregion
            return WriteFile(xls, vesselIndex, vessel, nengetsu, yosanHead, month, yosanItems今年度, getsujiDatas, unit, jisekiUnit, is累計);
        }
        #endregion

        /// <summary>
        /// 「各船」のデータを出力する（実処理）
        /// </summary>
        /// <param name="xls"></param>
        /// <param name="vesselIndex"></param>
        /// <param name="vessel"></param>
        /// <param name="nengetsu"></param>
        /// <param name="yosanHead"></param>
        /// <param name="month"></param>
        /// <param name="yosanItems今年度"></param>
        /// <param name="getsujiShuushi"></param>
        /// <param name="unit"></param>
        /// <param name="jisekiUnit"></param>
        /// <param name="is累計"></param>
        /// <returns></returns>
        #region private bool WriteFile(ExcelCreator.Xlsx.XlsxCreator  xls, int vesselIndex, MsVessel vessel, string nengetsu, BgYosanHead yosanHead, string month, List<BgYosanItem> yosanItems今年度, List<月次収支データ> getsujiShuushi, decimal unit, bool is累計)
        private bool WriteFile(ExcelCreator.Xlsx.XlsxCreator  xls, int vesselIndex, MsVessel vessel, string nengetsu, BgYosanHead yosanHead, string month,
                               List<BgYosanItem> yosanItems今年度, 
                               List<月次収支データ> getsujiShuushi,
                               decimal unit,
                               decimal jisekiUnit,
                               bool is累計)
        #region BG_JISSEKIを使用する場合のコード
        //private bool WriteFile(ExcelCreator.Xlsx.XlsxCreator  xls, int vesselIndex, MsVessel vessel, string nengetsu, BgYosanHead yosanHead, string month,
        //                       List<BgYosanItem> yosanItems今年度, 
        //                       List<BgJiseki> jisekis今年度,
        //                       List<月次収支データ> getsujiShuushi,
        //                       decimal unit, bool is累計)
        #endregion
        {
            int tmpColStart = 4;

            int colStart = tmpColStart + (3 * vesselIndex);

            if (vesselIndex != 0)
            {
                // テンプレート列をコピー
                xls.ColumnInsert(colStart, 3);
                xls.ColumnCopy(tmpColStart, colStart);
                xls.ColumnCopy(tmpColStart + 1, colStart + 1);
                xls.ColumnCopy(tmpColStart + 2, colStart + 2);
            }

            // 船名
            string cellName = ExcelUtils.ToCellName(colStart, 1);
            xls.Cell(cellName).Value = vessel.VesselName;

            #region 予算
            Dictionary<int, decimal> yosanByTypeOfVessel = yosanDic[vessel.MsVesselTypeID];
            Dictionary<int, decimal> yosanUSDByTypeOfVessel = yosanDicUSD[vessel.MsVesselTypeID];
            Dictionary<int, decimal> yosan = new Dictionary<int, decimal>();
            foreach (BgYosanItem yosanItem in yosanItems今年度)
            {
                if (yosanItem.Nengetsu.Trim().Length == 4)
                {
                    continue;
                }
                if (is累計 == false)
                {
                    if (yosanItem.Nengetsu.Trim() != nengetsu)
                    {
                        continue;
                    }
                }
                else
                {
                    if (年月が範囲内(yosanItem.Nengetsu, nengetsu) == false)
                    {
                        continue;
                    }
                }
                decimal amount = GetAmount(yosanItem, yosanItem.MsHimokuID);

                int himokuId = yosanItem.MsHimokuID;
                if (費目変換.ContainsKey(yosanItem.MsHimokuID))
                {
                    himokuId = 費目変換[yosanItem.MsHimokuID];
                }

                if (yosan.ContainsKey(himokuId))
                {
                    yosan[himokuId] += amount;
                }
                else
                {
                    yosan.Add(himokuId, amount);
                }

                if (yosanByTypeOfVessel.ContainsKey(himokuId))
                {
                    yosanByTypeOfVessel[himokuId] += amount;
                }
                else
                {
                    yosanByTypeOfVessel.Add(himokuId, amount);
                }

                decimal dollerAmount = GetDollarAmount(yosanItem, yosanItem.MsHimokuID);
                if (dollerAmount > 0)
                {
                    // 船タイプ毎（シート毎）の出力用に保持
                    if (yosanUSDByTypeOfVessel.ContainsKey(himokuId))
                    {
                        yosanUSDByTypeOfVessel[himokuId] += dollerAmount;
                    }
                    else
                    {
                        yosanUSDByTypeOfVessel.Add(himokuId, dollerAmount);
                    }
                }
            }

            foreach (int himokuId in yosan.Keys)
            {
                decimal amount = yosan[himokuId];
                if (amount > 0)
                {
                    xls.Cell(DetectBaseCell(himokuId), (colStart -tmpColStart + 1), 0).Value = amount / unit;
                }
            }
            #endregion

            #region 実績
            Dictionary<int, decimal> jisekiByTypeOfVessel = jisekiDic[vessel.MsVesselTypeID];
            Dictionary<int, decimal> jisekiUSDByTypeOfVessel = jisekiDicUSD[vessel.MsVesselTypeID];
            Dictionary<int, decimal> kansanByTypeOfVessel = kansanDic[vessel.MsVesselTypeID];
            Dictionary<int, decimal> eikyoByTypeOfVessel = eikyoDic[vessel.MsVesselTypeID];
            #region BG_JISSEKIを使用する場合のコード
            //var jiseki今年度Dic = JisekiBuilder.Build_月(jisekis今年度);
            //Dictionary<int, decimal> jiseki = new Dictionary<int, decimal>();
            //foreach (KeyValuePair<int, Dictionary<string, BgJiseki>> pair in jiseki今年度Dic)
            //{
            //    foreach (KeyValuePair<string, BgJiseki> pair2 in pair.Value)
            //    {
            //        if (is累計 == false)
            //        {
            //            if (pair2.Key != nengetsu)
            //            {
            //                continue;
            //            }
            //        }
            //        else
            //        {
            //            if (年月が範囲内(pair2.Key, nengetsu) == false)
            //            {
            //                continue;
            //            }
            //        }
            //        decimal amount = GetJisekiAmount(pair2.Value, pair.Key);

            //        int himokuId = pair.Key;
            //        if (費目変換.ContainsKey(pair.Key))
            //        {
            //            himokuId = 費目変換[pair.Key];
            //        }

            //        if (jiseki.ContainsKey(himokuId))
            //        {
            //            jiseki[himokuId] += amount;
            //        }
            //        else
            //        {
            //            jiseki.Add(himokuId, amount);
            //        }

            //        if (jisekiByTypeOfVessel.ContainsKey(himokuId))
            //        {
            //            jisekiByTypeOfVessel[himokuId] += amount;
            //        }
            //        else
            //        {
            //            jisekiByTypeOfVessel.Add(himokuId, amount);
            //        }
            //    }
            //}
            //foreach (int himokuId in jiseki.Keys)
            //{
            //    decimal amount = jiseki[himokuId];
            //    if (amount > 0)
            //    {
            //        xls.Cell(DetectBaseCell(himokuId), (colStart - tmpColStart), 0).Value = amount / unit;
            //    }
            //}
            #endregion

            月次収支データ check_g = null;
            try
            {
                foreach (月次収支データ g in getsujiShuushi)
                {
                    check_g = g;

                    xls.Cell(DetectBaseCell(g.MsHimokuID), (colStart - tmpColStart), 0).Value = g.実績 / jisekiUnit;

                    // 船タイプ毎（シート毎）の出力用に保持
                    if (jisekiByTypeOfVessel.ContainsKey(g.MsHimokuID))
                    {
                        jisekiByTypeOfVessel[g.MsHimokuID] += g.実績;
                    }
                    else
                    {
                        jisekiByTypeOfVessel.Add(g.MsHimokuID, g.実績);
                    }
                    // 船タイプ毎（シート毎）の出力用に保持
                    if (jisekiUSDByTypeOfVessel.ContainsKey(g.MsHimokuID))
                    {
                        jisekiUSDByTypeOfVessel[g.MsHimokuID] += g.実績_USD;
                    }
                    else
                    {
                        jisekiUSDByTypeOfVessel.Add(g.MsHimokuID, g.実績_USD);
                    }
                    // 船タイプ毎（シート毎）の出力用に保持
                    if (kansanByTypeOfVessel.ContainsKey(g.MsHimokuID))
                    {
                        kansanByTypeOfVessel[g.MsHimokuID] += g.換算後;
                    }
                    else
                    {
                        kansanByTypeOfVessel.Add(g.MsHimokuID, g.換算後);
                    }
                    // 船タイプ毎（シート毎）の出力用に保持
                    if (eikyoByTypeOfVessel.ContainsKey(g.MsHimokuID))
                    {
                        eikyoByTypeOfVessel[g.MsHimokuID] += g.影響額;
                    }
                    else
                    {
                        eikyoByTypeOfVessel.Add(g.MsHimokuID, g.影響額);
                    }
                }
            }
            catch(Exception e)
            {
                string m = e.Message;
            }
            #endregion

            return true;
        }
        #endregion

        /// <summary>
        /// 「ＩＧＴ」シート(２枚目のシート)の作成
        /// </summary>
        /// <param name="xls"></param>
        /// <param name="typeIdx"></param>
        /// <param name="type"></param>
        /// <param name="unit"></param>
        /// <param name="jisekiUnit"></param>
        /// <returns></returns>
        #region private bool Write_グループ(ExcelCreator.Xlsx.XlsxCreator  xls, int typeIdx, MsVesselType type, decimal unit, decimal jisekiUnit)
        private bool Write_グループ(ExcelCreator.Xlsx.XlsxCreator  xls, int typeIdx, MsVesselType type, decimal unit, decimal jisekiUnit)
        {
            int tmpColStart = 4;

            int colStart = tmpColStart + (3 * typeIdx);

            if (typeIdx != 0)
            {
                // テンプレート列をコピー
                xls.ColumnInsert(colStart, 3);
                xls.ColumnCopy(tmpColStart, colStart);
                xls.ColumnCopy(tmpColStart + 1, colStart + 1);
                xls.ColumnCopy(tmpColStart + 2, colStart + 2);
            }

            // グループ（船タイプ）名
            string cellName = ExcelUtils.ToCellName(colStart, 1);
            xls.Cell(cellName).Value = type.VesselTypeName;

            Dictionary<int, decimal> yosan = yosanDic[type.MsVesselTypeID];
            foreach (int himokuId in yosan.Keys)
            {
                decimal amount = yosan[himokuId];
                if (amount > 0)
                {
                    xls.Cell(DetectBaseCell(himokuId), (colStart - tmpColStart + 1), 0).Value = amount / unit;
                }
            }

            Dictionary<int, decimal> jiseki = jisekiDic[type.MsVesselTypeID];
            foreach (int himokuId in jiseki.Keys)
            {
                decimal amount = jiseki[himokuId];
                xls.Cell(DetectBaseCell(himokuId), (colStart - tmpColStart), 0).Value = amount / jisekiUnit;
            }

            return true;
        }
        #endregion

        /// <summary>
        /// 「全社」シート(１枚目のシート)の作成
        /// </summary>
        /// <param name="xls"></param>
        /// <param name="types"></param>
        /// <param name="unit"></param>
        /// <param name="jisekiUnit"></param>
        /// <returns></returns>
        #region private bool Write_全社(ExcelCreator.Xlsx.XlsxCreator  xls, List<MsVesselType> types, decimal unit, decimal jisekiUnit)
        private bool Write_全社(ExcelCreator.Xlsx.XlsxCreator  xls, List<MsVesselType> types, decimal unit, decimal jisekiUnit)
        {

            Dictionary<int, decimal> yosan = new Dictionary<int, decimal>();
            Dictionary<int, decimal> jiseki = new Dictionary<int, decimal>();

            foreach(MsVesselType type in types)
            {
                Dictionary<int, decimal> y = yosanDic[type.MsVesselTypeID];
                foreach (int key in y.Keys)
                {
                    if (yosan.ContainsKey(key))
                    {
                        yosan[key] += y[key];
                    }
                    else
                    {
                        yosan.Add(key, y[key]);
                    }
                }

                Dictionary<int, decimal> j = jisekiDic[type.MsVesselTypeID];
                foreach (int key in j.Keys)
                {
                    if (jiseki.ContainsKey(key))
                    {
                        jiseki[key] += j[key];
                    }
                    else
                    {
                        jiseki.Add(key, j[key]);
                    }
                }
            }

            foreach (int himokuId in yosan.Keys)
            {
                decimal amount = yosan[himokuId];
                if (amount > 0)
                {
                    xls.Cell(DetectBaseCell(himokuId), 1, 0).Value = amount / unit;
                }
            }

            foreach (int himokuId in jiseki.Keys)
            {
                decimal amount = jiseki[himokuId];
                xls.Cell(DetectBaseCell(himokuId), 0, 0).Value = amount / jisekiUnit;
            }

            return true;
        }
        #endregion

        /// <summary>
        /// 各グループのシートの「計画為替」「為替の影響」を出力する
        /// </summary>
        /// <param name="xls"></param>
        /// <param name="index"></param>
        /// <param name="type"></param>
        /// <param name="unit"></param>
        /// <param name="jisekiUnit"></param>
        /// <returns></returns>
        #region private bool Write_為替(ExcelCreator.Xlsx.XlsxCreator  xls, int index, MsVesselType type, decimal unit, decimal jisekiUnit)
        private bool Write_為替(ExcelCreator.Xlsx.XlsxCreator  xls, int index, MsVesselType type, decimal unit, decimal jisekiUnit)
        {
            Dictionary<int, decimal> jisekiUSD = new Dictionary<int, decimal>();
            Dictionary<int, decimal> yosanUSD = new Dictionary<int, decimal>();
            Dictionary<int, decimal> kansan = new Dictionary<int, decimal>();
            Dictionary<int, decimal> eikyo = new Dictionary<int, decimal>();

            Dictionary<int, decimal> jUSD = jisekiDicUSD[type.MsVesselTypeID];
            foreach (int key in jUSD.Keys)
            {
                if (jisekiUSD.ContainsKey(key))
                {
                    jisekiUSD[key] += jUSD[key];
                }
                else
                {
                    jisekiUSD.Add(key, jUSD[key]);
                }
            }
            Dictionary<int, decimal> yUSD = yosanDicUSD[type.MsVesselTypeID];
            foreach (int key in yUSD.Keys)
            {
                if (yosanUSD.ContainsKey(key))
                {
                    yosanUSD[key] += yUSD[key];
                }
                else
                {
                    yosanUSD.Add(key, yUSD[key]);
                }
            }
            Dictionary<int, decimal> k = kansanDic[type.MsVesselTypeID];
            foreach (int key in k.Keys)
            {
                if (kansan.ContainsKey(key))
                {
                    kansan[key] += k[key];
                }
                else
                {
                    kansan.Add(key, k[key]);
                }
            }
            Dictionary<int, decimal> e = eikyoDic[type.MsVesselTypeID];
            foreach (int key in e.Keys)
            {
                if (eikyo.ContainsKey(key))
                {
                    eikyo[key] += e[key];
                }
                else
                {
                    eikyo.Add(key, e[key]);
                }
            }

            foreach (int himokuId in jisekiUSD.Keys)
            {
                decimal amount = jisekiUSD[himokuId];
                xls.Cell(DetectBaseCell(himokuId) + "_USD", 0, 0).Value = amount / jisekiUnit;
            }

            foreach (int himokuId in yosanUSD.Keys)
            {
                decimal amount = yosanUSD[himokuId];
                xls.Cell(DetectBaseCell(himokuId) + "_USD", 1, 0).Value = amount / unit;
            }

            foreach (int himokuId in kansan.Keys)
            {
                decimal amount = kansan[himokuId];
                xls.Cell(DetectBaseCell(himokuId) + "_USD", 2, 0).Value = amount / jisekiUnit;
            }


            foreach (int himokuId in eikyo.Keys)
            {
                decimal amount = eikyo[himokuId];
                xls.Cell(DetectBaseCell(himokuId) + "_USD", 3, 0).Value = amount / jisekiUnit;
            }


            return true;
        }
        #endregion

        /// <summary>
        /// 「全社」シート(１枚目のシート)、「ＩＧＴ」シート(２枚目のシート)の「計画為替」「為替の影響」を出力する
        /// </summary>
        /// <param name="xls"></param>
        /// <param name="index"></param>
        /// <param name="types"></param>
        /// <param name="unit"></param>
        /// <param name="jisekiUnit"></param>
        /// <returns></returns>
        #region private bool Write_為替(ExcelCreator.Xlsx.XlsxCreator  xls, int index, List<MsVesselType> types, decimal unit, decimal jisekiUnit)
        private bool Write_為替(ExcelCreator.Xlsx.XlsxCreator  xls, int index, List<MsVesselType> types, decimal unit, decimal jisekiUnit)
        {
            Dictionary<int, decimal> jisekiUSD = new Dictionary<int, decimal>();
            Dictionary<int, decimal> yosanUSD = new Dictionary<int, decimal>();
            Dictionary<int, decimal> kansan = new Dictionary<int, decimal>();
            Dictionary<int, decimal> eikyo = new Dictionary<int, decimal>();

            foreach (MsVesselType type in types)
            {
                Dictionary<int, decimal> jUSD = jisekiDicUSD[type.MsVesselTypeID];
                foreach (int key in jUSD.Keys)
                {
                    if (jisekiUSD.ContainsKey(key))
                    {
                        jisekiUSD[key] += jUSD[key];
                    }
                    else
                    {
                        jisekiUSD.Add(key, jUSD[key]);
                    }
                }

                Dictionary<int, decimal> yUSD = yosanDicUSD[type.MsVesselTypeID];
                foreach (int key in yUSD.Keys)
                {
                    if (yosanUSD.ContainsKey(key))
                    {
                        yosanUSD[key] += yUSD[key];
                    }
                    else
                    {
                        yosanUSD.Add(key, yUSD[key]);
                    }
                }

                Dictionary<int, decimal> k = kansanDic[type.MsVesselTypeID];
                foreach (int key in k.Keys)
                {
                    if (kansan.ContainsKey(key))
                    {
                        kansan[key] += k[key];
                    }
                    else
                    {
                        kansan.Add(key, k[key]);
                    }
                }

                Dictionary<int, decimal> e = eikyoDic[type.MsVesselTypeID];
                foreach (int key in e.Keys)
                {
                    if (eikyo.ContainsKey(key))
                    {
                        eikyo[key] += e[key];
                    }
                    else
                    {
                        eikyo.Add(key, e[key]);
                    }
                }
            }

            foreach (int himokuId in jisekiUSD.Keys)
            {
                decimal amount = jisekiUSD[himokuId];
                xls.Cell(DetectBaseCell(himokuId) + "_USD", 0, 0).Value = amount / jisekiUnit;
            }

            foreach (int himokuId in yosanUSD.Keys)
            {
                decimal amount = yosanUSD[himokuId];
                xls.Cell(DetectBaseCell(himokuId) + "_USD", 1, 0).Value = amount / unit;
            }

            foreach (int himokuId in kansan.Keys)
            {
                decimal amount = kansan[himokuId];
                xls.Cell(DetectBaseCell(himokuId) + "_USD", 2, 0).Value = amount / jisekiUnit;
            }

            foreach (int himokuId in eikyo.Keys)
            {
                decimal amount = eikyo[himokuId];
                xls.Cell(DetectBaseCell(himokuId) + "_USD", 3, 0).Value = amount / jisekiUnit;
            }

            return true;
        }
        #endregion


        private static decimal GetAmount(IYojitsu yojitsu, int himokuId)
        {
            decimal amount;

            if (HimokuTreeReader.GetHimokuTreeNode(himokuId) == null)
            {
                amount = 0;
            }
            //else if (HimokuTreeReader.GetHimokuTreeNode(himokuId).Dollar)
            else if (yojitsu.YenAmount > 0)
            {
                amount = yojitsu.YenAmount;
            }
            else
            {
                amount = yojitsu.Amount;
            }

            return amount;
        }
        private static decimal GetDollarAmount(IYojitsu yojitsu, int himokuId)
        {
            decimal amount = 0;

            if (HimokuTreeReader.GetHimokuTreeNode(himokuId) != null)
            {
                amount = yojitsu.YenAmount;
            }

            return amount;
        }
        //private static decimal GetJisekiAmount(IYojitsu yojitsu, int himokuId)
        //{
        //    decimal amount = 0;

        //    //if (HimokuTreeReader.GetHimokuTreeNode(himokuId).Dollar)
        //    //{
        //    //    amount = yojitsu.YenAmount;
        //    //}
        //    //else
        //    //{
        //    //    amount = yojitsu.Amount;
        //    //}
        //    amount += yojitsu.YenAmount;

        //    return amount;
        //}

        private string DetectBaseCell(int msHimokuID)
        {
            return "**" + msHimokuID;
        }

        private bool 年月が範囲内(string nengetsu1, string nengetsu2)
        {
            if (int.Parse(nengetsu1.Substring(0, 4)) > int.Parse(nengetsu2.Substring(0, 4)))
            {
                return false;
            }
            if (int.Parse(nengetsu1.Substring(4, 2)) > int.Parse(nengetsu2.Substring(4, 2)))
            {
                return false;
            }
            return true;
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

        private void BuildHimokuDic(List<MsHimoku> himokuList)
        {
            foreach (MsHimoku himoku in himokuList)
            {
                if (!himokuDic.ContainsKey(himoku.MsHimokuID))
                {
                    himokuDic.Add(himoku.MsHimokuID, 0);
                }
            }

        }
        private void BuildHimokuKikanHimokuDic(List<MsHimoku> himokuList)
        {
            foreach (MsHimoku himoku in himokuList)
            {
                if (!himokuKikanHimokuDic.ContainsKey(himoku.KikanHimokuID))
                {
                    himokuKikanHimokuDic.Add(himoku.KikanHimokuID, himoku.MsHimokuID);
                }
            }

        }
    }
}
