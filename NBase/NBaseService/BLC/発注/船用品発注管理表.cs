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
using NBaseCommon;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_Excel船用品発注管理表_取得(NBaseData.DAC.MsUser loginUser, int FromYear, int FromMonth, int ToYear, int ToMonth);
    }

    public partial class Service
    {
        public byte[] BLC_Excel船用品発注管理表_取得(NBaseData.DAC.MsUser loginUser, int FromYear, int FromMonth, int ToYear, int ToMonth)
        {
            try
            {
                #region 元になるファイルの確認と出力ファイル生成
                string BaseFileName = "船用品発注管理表";
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


                // 最大１０回、１０秒のインターバルで処理を繰り返す
                int maxRetry = 10;
                int mmSecond = 1000;
                int retryInterval = 1 * mmSecond;
                bool isComplated = false;

                for (int i = 0; i < maxRetry; i++)
                {
                    try
                    {
                        if (Make船用品発注管理表(loginUser, FromYear, FromMonth, ToYear, ToMonth, templateName, outPutFileName))
                        {
                            isComplated = true;
                            break;
                        }
                    }
                    catch (ExcelCreatorException)
                    {
                        // ExcelCreatorがインスタンス化できない場合、ビジーとみなしスル―する
                    }

                    System.Threading.Thread.Sleep(retryInterval);
                }

                byte[] bytBuffer = null;

                if (isComplated)
                {
                    #region バイナリーへ変換
                    using (FileStream objFileStream = new FileStream(outPutFileName, FileMode.Open))
                    {
                        long lngFileSize = objFileStream.Length;

                        bytBuffer = new byte[(int)lngFileSize];
                        objFileStream.Read(bytBuffer, 0, (int)lngFileSize);
                        objFileStream.Close();
                    }
                    #endregion
                }
                else
                {
                    throw new ExcelCreatorException();
                }


                return bytBuffer;
            }
            catch (Exception Ex)
            {
                string message = "";
                if (Ex is ExcelCreatorException)
                {
                    message = "";// "帳票出力が込み合っています。時間をおいて再度出力して下さい。";
                }
                else
                {
                    message = Ex.Message;
                }
                throw new FaultException(message);
            }
        }


        private bool Make船用品発注管理表(NBaseData.DAC.MsUser loginUser, int FromYear, int FromMonth, int ToYear, int ToMonth, string templateName, string outPutFileName)
        {
            bool ret = true;
            bool execFlag = false;

            // 対象の予算（費目のID）
            const int MS_HIMOKU_船用品費_ID = 40;

            try
            {
                using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
                {
                    try
                    {
                        using (DBConnect dbConnect = new DBConnect())
                        {
                            execFlag = true;

                            xls.OpenBook(outPutFileName, templateName);
                            if (xls.ErrorNo != ExcelCreator.ErrorNo.NoError)
                            {
                                return false;
                            }

                            // 2010.03: パフォーマンスの改善案（未実装）
                            //DateTime hachuDateFrom = new DateTime(FromYear, FromMonth, 1, 0, 0, 0);
                            //DateTime hachuDateTo = new DateTime(ToYear, ToMonth, GetMonthLastDay(ToYear, ToMonth), 23, 59, 59);
                            //List<NBaseData.BLC.管理表Info> 船用品管理表Infos = NBaseData.BLC.管理表Info.GetRecordsFor船用品管理表(loginUser, hachuDateFrom, hachuDateTo);

                            Dictionary<int, 船員月次> 月次range = 船員月次.Get船員月次(dbConnect, loginUser, new DateTime(FromYear, FromMonth, 1, 0, 0, 0));

                            List<MsVessel> msVesselList = MsVessel.GetRecordsByHachuEnabled(dbConnect, loginUser);
                            foreach (MsVessel msVessel in msVesselList)
                            {
                                // 現在の総シート数を確認します
                                int nSheetCount = xls.SheetCount;

                                // 1 番左のシート(シート番号 0)を、最終シート以降にコピー
                                xls.CopySheet(0, nSheetCount, msVessel.VesselName);
                                xls.SheetNo = nSheetCount;

                                #region Excelファイルの編集

                                // 船名
                                xls.Cell("B2").Value = msVessel.VesselName;
                                // 年度
                                xls.Cell("A3").Value = FromYear.ToString() + "年度予算";
                                // 予算
                                decimal 予算 = 0;
                                BgYosanHead bgYosanHead = BgYosanHead.GetRecordByYear(dbConnect, loginUser, FromYear.ToString());
                                if (bgYosanHead != null)
                                {
                                    BgYosanItem bgYosanItem = BgYosanItem.GetRecordByYearHimokuIDMsVesselID(dbConnect, loginUser, bgYosanHead.YosanHeadID, FromYear, MS_HIMOKU_船用品費_ID, msVessel.MsVesselID);
                                    if (bgYosanItem != null)
                                    {
                                        予算 = bgYosanItem.Amount;
                                    }
                                }
                                xls.Cell("B3").Value = 予算;

                                // [OD_THI]手配依頼
                                OdThiFilter odThiFilter = new OdThiFilter();
                                odThiFilter.MsVesselID = msVessel.MsVesselID;

                                // 2018.01 年度で検索
                                //// 2010.04.21:aki 手配依頼日ではなく発注日で検索する
                                ////odThiFilter.ThiIraiDateFrom = new DateTime(FromYear, FromMonth, 1, 0, 0, 0);
                                ////odThiFilter.ThiIraiDateTo = new DateTime(ToYear, ToMonth, GetMonthLastDay(ToYear, ToMonth), 23, 59, 59);
                                //odThiFilter.HachuDateFrom = new DateTime(FromYear, FromMonth, 1, 0, 0, 0);
                                //odThiFilter.HachuDateTo = new DateTime(ToYear, ToMonth, GetMonthLastDay(ToYear, ToMonth), 23, 59, 59);
                                odThiFilter.Nendo = FromYear.ToString();

                                DateTime HachuDateTo = new DateTime(ToYear, ToMonth, GetMonthLastDay(ToYear, ToMonth), 23, 59, 59);

                                List<OdThi> list_odThi = OdThi.GetRecordsByFilter(dbConnect, loginUser, odThiFilter);

                                List<船用品管理表Row> list_船用品管理表Row = new List<船用品管理表Row>();

                                #region 出力データの準備
                                foreach (OdThi odThi in list_odThi)
                                {
                                    // 出力対象のレコードか判定する
                                    if (!船用品発注管理表出力対象判定(dbConnect, odThi))
                                    {
                                        continue;
                                    }

                                    // 船用品管理表Rowクラス
                                    船用品管理表Row row = new 船用品管理表Row();
                                    row.手配内容 = odThi.Naiyou;
                                    row.発生日 = odThi.ThiIraiDate;
                                    row.備考 = odThi.Bikou;

                                    // [OD_MM]見積依頼
                                    List<OdMm> list_odMm = OdMm.GetRecordsByOdThiID(dbConnect, loginUser, odThi.OdThiID);
                                    foreach (OdMm odMm in list_odMm)
                                    {
                                        // [OD_MK]見積回答
                                        #region
                                        List<OdMk> list_odMk = OdMk.GetRecordsByOdMmID(dbConnect, loginUser, odMm.OdMmID);
                                        foreach (OdMk odMk in list_odMk)
                                        {
                                            if (odMk.Nendo != FromYear.ToString()) // 20190514 年度が違うものは出力対象外とする
                                                continue;

                                            if (odMk.Status != 4)
                                            {
                                                continue;
                                            }

                                            MsCustomer msCustomer = MsCustomer.GetRecord(dbConnect, loginUser, odMk.MsCustomerID);
                                            row.業者 = msCustomer.CustomerName;
                                            row.発注価格 = odMk.Amount;
                                            if (odMk.Carriage > 0)
                                            {
                                                row.発注価格 += odMk.Carriage;
                                            }
                                            if (odMk.MkAmount > 0)
                                            {
                                                row.発注価格 -= odMk.MkAmount;
                                            }
                                            row.発注日 = odMk.HachuDate;
                                            //row.発注番号 = odMk.MkNo.Replace("Enabled","");
                                            row.発注番号 = odMk.HachuNo; // 2009.11.10:aki (W090207)

                                            row.完了_納品日 = DateTime.MinValue;
                                            row.概算計上額 = decimal.MinValue;

                                            // [OD_JRY]受領
                                            List<OdJry> list_odJry = OdJry.GetRecordsByOdMkId(dbConnect, loginUser, odMk.OdMkID);
                                            #region
                                            if (list_odJry.Count == 0)
                                            {
                                                list_船用品管理表Row.Add(row);
                                            }
                                            else
                                            {
                                                bool sameJry = false;
                                                foreach (OdJry odJry in list_odJry)
                                                {
                                                    if (odJry.OdJryID == "9cfad237-4ba6-4f52-815a-1cea86fb6128")
                                                    {
                                                        int a = 0;
                                                    }
                                                    // 2010.07.15:aki 分納等の場合、ここで初期化が必要でした
                                                    row.完了_納品日 = DateTime.MinValue;
                                                    row.概算計上額 = decimal.MinValue;

                                                    OdJryGaisan jryGaisan = OdJryGaisan.GetRecordByOdJryID(dbConnect, new MsUser(), odJry.OdJryID);
                                                    if (odJry.Status == odJry.OdStatusValue.Values[(int)OdJry.STATUS.受領承認済み].Value)
                                                    {
                                                        row.完了_納品日 = odJry.JryDate;
                                                        //row.概算計上額 = odJry.Amount;
                                                        //if (odJry.NebikiAmount > 0)
                                                        //{
                                                        //    row.概算計上額 -= odJry.NebikiAmount;
                                                        //}
                                                        if (jryGaisan != null)
                                                        {
                                                            row.概算計上額 = jryGaisan.Amount;
                                                        }
                                                        else
                                                        {
                                                            row.概算計上額 = odJry.Amount;
                                                            if (odJry.Carriage > 0)
                                                            {
                                                                row.概算計上額 += odJry.Carriage;
                                                            }
                                                            if (odJry.NebikiAmount > 0)
                                                            {
                                                                row.概算計上額 -= odJry.NebikiAmount;
                                                            }
                                                        }
                                                    }

                                                    // [OD_SHR]支払
                                                    List<OdShr> list_odShr = OdShr.GetRecordByOdJryID(dbConnect, loginUser, odJry.OdJryID);
                                                    // 2010.03:aki 支払合算対応のため、以下の４行追加
                                                    if (list_odShr.Count == 0)
                                                    {
                                                        list_odShr = OdShr.GetRecordByGassanItem(dbConnect, loginUser, odJry.OdJryID);
                                                    }
                                                    #region
                                                    int shrCount = 0;
                                                    foreach (OdShr odShr in list_odShr)
                                                    {
                                                        船用品管理表Row 支払row = row.Clone();

                                                        if (odShr.Sbt == (int)OdShr.SBT.支払)
                                                        {
                                                            if (odShr.Status == odShr.OdStatusValue.Values[(int)OdShr.STATUS.支払済].Value &&
                                                                (odShr.SyoriStatus != ((int)支払実績連携IF.STATUS.支払済み).ToString() &&
                                                                 odShr.SyoriStatus != ((int)支払実績連携IF.STATUS.実績).ToString()))
                                                            {
                                                                // 支払い済みなんだけど、基幹で支払い済み,実績でないものは、読み飛ばす
                                                                continue;
                                                            }
                                                            if (odShr.Status == odShr.OdStatusValue.Values[(int)OdShr.STATUS.支払依頼済み].Value)
                                                            {
                                                                // 支払依頼済み、読み飛ばす(基幹取込エラー)
                                                                continue;
                                                            }
                                                            shrCount++;

                                                            支払row.請求額 = odShr.Amount;
                                                            if (odShr.Carriage > 0)
                                                            {
                                                                支払row.請求額 += odShr.Carriage;
                                                            }
                                                            if (odShr.NebikiAmount > 0)
                                                            {
                                                                支払row.請求額 -= odShr.NebikiAmount;
                                                            }
                                                            支払row.請求書日 = odShr.ShrIraiDate;
                                                            支払row.概算計上額 = 支払row.請求額;
                                                            if (odShr.Status != odShr.OdStatusValue.Values[(int)OdShr.STATUS.支払作成済み].Value)
                                                            {
                                                                // 2010.03:aki [W090251]起票日=計上日
                                                                //支払row.起票日 = odShr.Kihyoubi;
                                                                支払row.起票日 = odShr.KeijyoDate;
                                                            }
                                                            if (odShr.Status == odShr.OdStatusValue.Values[(int)OdShr.STATUS.支払済].Value)
                                                            {
                                                                // 2010.03:aki [W090251]計上月=概算計上月（完了_納品日の月）
                                                                //支払row.計上月 = odShr.KeijyoDate.Month.ToString();
                                                                支払row.計上月 = row.完了_納品日.Month.ToString();

                                                                // 支払い済みの場合は、概算計上額をクリア
                                                                支払row.概算計上額 = decimal.MinValue;
                                                            }

                                                            if (sameJry)
                                                            {
                                                                支払row.発注価格 = decimal.MaxValue;
                                                            }
                                                            else
                                                            {
                                                                sameJry = true;
                                                            }

                                                            // 2010.03:aki 支払合算対応のため、以下の２５行追加
                                                            if (odShr.OdShrGassanHeadID != null && odShr.OdShrGassanHeadID.Length > 0)
                                                            {
                                                                if (odShr.NebikiAmount > 0)
                                                                {
                                                                    // 備考(元の備考に追加する場合)
                                                                    if (支払row.備考.Length > 0)
                                                                        支払row.備考 += " ";
                                                                    支払row.備考 += "合算値引：" + odShr.NebikiAmount.ToString("C");
                                                                    // 備考(元の備考を置き換える場合)
                                                                    //支払row.備考 = "合算値引：" + odShr.NebikiAmount.ToString("C");
                                                                }
                                                            }
                                                            else if (odShr.HachuNo != null && odShr.HachuNo.Length > 0)
                                                            {
                                                                // 備考(元の備考に追加する場合)
                                                                if (支払row.備考.Length > 0)
                                                                    支払row.備考 += " ";
                                                                支払row.備考 += "発注番号：" + odShr.HachuNo + " と統合";
                                                                // 備考(元の備考を置き換える場合)
                                                                //row.備考 = "発注番号：" + odShr.HachuNo + " と統合";

                                                                // 合算した場合、以下項目は表示しない
                                                                支払row.請求額 = decimal.MaxValue;
                                                                支払row.概算計上額 = decimal.MinValue;
                                                            }

                                                            list_船用品管理表Row.Add(支払row);
                                                        }
                                                    }
                                                    if (shrCount == 0)
                                                    {
                                                        船用品管理表Row 受領row = row.Clone();
                                                        if (sameJry)
                                                        {
                                                            受領row.発注価格 = decimal.MaxValue;
                                                        }
                                                        else
                                                        {
                                                            sameJry = true;
                                                        }
                                                        if (jryGaisan != null)
                                                        {
                                                            if (jryGaisan.Bikou != null && jryGaisan.Bikou.Length > 0)
                                                            {
                                                                if (受領row.備考.Length > 0)
                                                                    受領row.備考 += " ";
                                                                受領row.備考 += jryGaisan.Bikou;
                                                            }
                                                        }
                                                        list_船用品管理表Row.Add(受領row);
                                                    }
                                                    #endregion
                                                }
                                            }
                                            #endregion
                                        }
                                        #endregion
                                    }
                                }
                                #endregion

                                // ここから振替取立のデータ
                                #region
                                OdFurikaeToritateFilter odFkTtFilter = new OdFurikaeToritateFilter();
                                odFkTtFilter.MsVesselID = msVessel.MsVesselID;
                                odFkTtFilter.HachuDateFrom = new DateTime(FromYear, FromMonth, 1, 0, 0, 0);
                                odFkTtFilter.HachuDateTo = new DateTime(ToYear, ToMonth, GetMonthLastDay(ToYear, ToMonth), 23, 59, 59);
                                odFkTtFilter.MsThiIraiSbtID = NBaseCommon.Common.MsThiIraiSbt_船用品ID;
                                List<OdFurikaeToritate> OdFurikaeToritates = null;
                                OdFurikaeToritates = OdFurikaeToritate.GetRecordsByFilter(dbConnect, loginUser, odFkTtFilter);

                                foreach (OdFurikaeToritate furikaeToritate in OdFurikaeToritates)
                                {
                                    船用品管理表Row row = new 船用品管理表Row();
                                    row.発生日 = furikaeToritate.HachuDate;
                                    row.手配内容 = furikaeToritate.Koumoku;
                                    row.発注日 = furikaeToritate.HachuDate;
                                    row.業者 = furikaeToritate.MsCustomerName;
                                    row.発注価格 = decimal.MinValue;
                                    row.完了_納品日 = furikaeToritate.Kanryobi;
                                    row.請求書日 = furikaeToritate.Seikyushobi;
                                    row.請求額 = furikaeToritate.Amount;
                                    row.備考 = furikaeToritate.Bikou;
                                    row.起票日 = furikaeToritate.Kihyobi;
                                    row.概算計上額 = decimal.MinValue;
                                    row.計上月 = furikaeToritate.Kihyobi.Month.ToString();
                                    //row.発注番号

                                    list_船用品管理表Row.Add(row);
                                }
                                #endregion

                                // ここから準備金のデータ
                                #region

                                List<SiGetsujiShime> SiGetsujiShimes = null;
                                ////SiGetsujiShimes = SiGetsujiShime.GetRecords(loginUser, DateTime.Today);
                                //SiGetsujiShimes = SiGetsujiShime.GetRecords(dbConnect, loginUser, odThiFilter.HachuDateFrom);
                                DateTime fromDate = new DateTime(FromYear, FromMonth, 1);
                                SiGetsujiShimes = SiGetsujiShime.GetRecords(dbConnect, loginUser, fromDate);

                                List<SiJunbikin> SiJunbikins = null;
                                ////SiJunbikins = SiJunbikin.Get_振替_船用品費(loginUser, DateTime.Today, msVessel.MsVesselID);
                                //SiJunbikins = SiJunbikin.Get_振替_船用品費(loginUser, odThiFilter.HachuDateFrom, msVessel.MsVesselID);
                                SiJunbikins = SiJunbikin.Get_振替_船用品費(loginUser, fromDate, msVessel.MsVesselID);
                                decimal[] 船内収支報告 = new decimal[12];
                                for (int i = 0; i < 12; i++)
                                {
                                    船内収支報告[i] = 0;
                                }
                                foreach (SiJunbikin jk in SiJunbikins)
                                {
                                    //int m = jk.JunbikinDate.Month;
                                    //船内収支報告[m] += jk.KingakuOut;
                                    for (int i = 1; i <= 12; i++)
                                    {
                                        if (jk.JunbikinDate >= 月次range[i].from && jk.JunbikinDate < 月次range[i].to)
                                        {
                                            船内収支報告[i] += jk.KingakuOut;
                                        }
                                    }
                                }
                                for (int i = 0; i < 12; i++)
                                {
                                    if (船内収支報告[i] > 0)
                                    {
                                        SiGetsujiShime shime = null;
                                        foreach (SiGetsujiShime gs in SiGetsujiShimes)
                                        {
                                            if (gs.NenGetsu.Substring(4) == i.ToString("00"))
                                            {
                                                shime = gs;
                                                break;
                                            }
                                        }
                                        if (shime != null)
                                        {
                                            船用品管理表Row row = new 船用品管理表Row();
                                            row.発生日 = shime.RenewDate;
                                            row.手配内容 = (int.Parse(shime.NenGetsu.Substring(4))).ToString() + "月船内収支報告";
                                            //row.発注番号
                                            row.発注日 = DateTime.MinValue;
                                            row.業者 = "現金";
                                            row.発注価格 = 船内収支報告[i];
                                            row.完了_納品日 = shime.RenewDate;
                                            row.請求書日 = DateTime.MinValue;
                                            row.請求額 = 船内収支報告[i];
                                            //row.備考;
                                            row.起票日 = DateTime.MinValue;
                                            //row.概算計上額 = 船内収支報告[i];
                                            row.概算計上額 = decimal.MinValue;
                                            row.計上月 = (int.Parse(shime.NenGetsu.Substring(4))).ToString();

                                            list_船用品管理表Row.Add(row);
                                        }
                                    }
                                }
                                #endregion

                                int offset_y = 0;
                                decimal temp予算 = 予算;
                                offset_y = 0;
                                string[] months = { "4", "5", "6", "7", "8", "9", "10", "11", "12", "1", "2", "3" };
                                bool outputFin = false;
                                foreach (string month in months)
                                {
                                    if (outputFin)
                                    {
                                        break;
                                    }
                                    if (month == ToMonth.ToString())
                                    {
                                        outputFin = true;
                                    }
                                    ////var 船用品管理表Rows = from p in list_船用品管理表Row
                                    ////                 where p.計上月 == month
                                    ////                 where p.発注日 <= odThiFilter.ThiIraiDateTo
                                    ////                 orderby p.発生日
                                    ////                 select p;
                                    //var 船用品管理表Rows = from p in list_船用品管理表Row
                                    //                 where p.計上月 == month
                                    //                    && ((p.発注日 <= odThiFilter.HachuDateTo && p.発注日 >= odThiFilter.HachuDateFrom) || p.発注日 == DateTime.MinValue)
                                    //                 orderby p.発生日
                                    //                 select p;
                                    var 船用品管理表Rows = from p in list_船用品管理表Row
                                                     where p.計上月 == month
                                                        && (p.発注日 <= HachuDateTo || p.発注日 == DateTime.MinValue)
                                                     orderby p.発生日
                                                     select p;

                                    int num = 0;
                                    decimal 月請求額合計 = 0;
                                    decimal 月請概算計上額合計 = 0;
                                    foreach (var row in 船用品管理表Rows)
                                    {
                                        // データを出力
                                        writeRow(xls, offset_y, row);

                                        // 罫線を出力
                                        xls.Cell("A6:O6", 0, offset_y).Attr.LineBottom (ExcelCreator.BorderStyle.Dotted, ExcelCreator.xlColor.Black);

                                        //月請概算計上額合計
                                        if (row.請求額 != decimal.MinValue && row.請求額 != decimal.MaxValue)
                                            月請求額合計 = 月請求額合計 + row.請求額;

                                        offset_y++;
                                        num++;
                                    }

                                    if (num > 0)
                                    {
                                        // 2009.12.14:aki 12.07川崎さん指示によりコメントアウト
                                        // 収支報告
                                        //xls.Cell("B6", 0, offset_y).Value = "収支報告";
                                        // 月末合計
                                        xls.Cell("M6", 0, offset_y).Value = 月請求額合計 + 月請概算計上額合計;
                                        // 予算残高
                                        temp予算 = temp予算 - 月請求額合計 + 月請概算計上額合計;
                                        xls.Cell("N6", 0, offset_y).Value = temp予算;

                                        // 罫線を出力
                                        xls.Cell("A6:O6", 0, offset_y).Attr.LineBottom (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                        // 太線の場合、こちら
                                        //xls.Cell("A6:O6", 0, offset_y).Attr.LineBottom = ExcelCreator.xlLineStyle.lsThick2;

                                        offset_y++;
                                    }

                                }
                                // 2018.01 年度で検索

                                ////var 未計上船用品管理表Rows = from p in list_船用品管理表Row
                                ////                    where p.計上月 == ""
                                ////                    where p.発注日 <= odThiFilter.ThiIraiDateTo
                                ////                    orderby p.発生日
                                ////                    select p;
                                //var 未計上船用品管理表Rows = from p in list_船用品管理表Row
                                //                    where p.計上月 == ""
                                //                       && p.発注日 <= odThiFilter.HachuDateTo
                                //                       && p.発注日 >= odThiFilter.HachuDateFrom
                                //                    orderby p.発生日
                                //                    select p;
                                var 未計上船用品管理表Rows = from p in list_船用品管理表Row
                                                    where p.計上月 == ""
                                                       && p.発注日 <= HachuDateTo
                                                    orderby p.発生日
                                                    select p;

                                foreach (var row in 未計上船用品管理表Rows)
                                {
                                    // データを出力
                                    writeRow(xls, offset_y, row);
                                    // 罫線を出力
                                    xls.Cell("A6:O6", 0, offset_y).Attr.LineBottom (ExcelCreator.BorderStyle.Dotted, ExcelCreator.xlColor.Black);
                                    offset_y++;
                                }

                                #region  合計を出力
                                int val = 6 + offset_y;
                                if (val > 6)
                                {
                                    xls.Cell("F" + val.ToString()).Value = "=SUM(F6:F" + (val - 1).ToString() + ")";
                                    xls.Cell("H" + val.ToString()).Value = "合計";
                                    xls.Cell("I" + val.ToString()).Value = "=SUM(I6:I" + (val - 1).ToString() + ")";
                                    xls.Cell("L" + val.ToString()).Value = "=SUM(L6:L" + (val - 1).ToString() + ")";
                                }
                                else
                                {
                                    xls.Cell("F" + val.ToString()).Value = 0;
                                    xls.Cell("H" + val.ToString()).Value = "合計";
                                    xls.Cell("I" + val.ToString()).Value = 0;
                                    xls.Cell("L" + val.ToString()).Value = 0;

                                }
                                xls.Cell("N" + val.ToString()).Value = temp予算;
                                #endregion

                                #region 罫線を出力
                                xls.Cell("A6:A" + val.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell("A6:A" + val.ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell("B6:B" + val.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                                xls.Cell("C6:C" + val.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                                xls.Cell("D6:D" + val.ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell("E6:E" + val.ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell("F6:F" + val.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                                xls.Cell("G6:G" + val.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                                xls.Cell("H6:H" + val.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                                xls.Cell("I6:I" + val.ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell("J6:J" + val.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                                xls.Cell("K6:K" + val.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                                xls.Cell("L6:L" + val.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                                xls.Cell("M6:M" + val.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                                xls.Cell("N6:N" + val.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                                xls.Cell("O6:O" + val.ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell("A" + val.ToString() + ":O" + val.ToString()).Attr.LineBottom (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                #endregion

                                #endregion
                            }
                            xls.DeleteSheet(0, 1);
                            xls.CloseBook(true);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return ret;
            }
            catch(Exception ex)
            {
                NBaseCommon.LogFile.Write(loginUser.FullName, "船用品管理表：" + ex.Message);
                if (execFlag)
                {
                    throw ex;
                }
                else
                {
                    throw new ExcelCreatorException();
                }
            }
        }

        private void writeRow(ExcelCreator.Xlsx.XlsxCreator xls, int offset_y, 船用品管理表Row row)
        {
            // 発生日
            if (row.発生日 != DateTime.MinValue)
            {
                xls.Cell("A6", 0, offset_y).Value = row.発生日.ToString("yyyy/MM/dd");
            }
            // 手配内容
            xls.Cell("B6", 0, offset_y).Value = row.手配内容;
            // 発注番号
            xls.Cell("C6", 0, offset_y).Value = row.発注番号;
            // 発注日
            if (row.発注日 != DateTime.MinValue)
            {
                xls.Cell("D6", 0, offset_y).Value = row.発注日.ToString("yyyy/MM/dd");
            }
            // 業者
            xls.Cell("E6", 0, offset_y).Value = row.業者;
            // 発注価格
            if (row.発注価格 == decimal.MaxValue)
            {
                xls.Cell("F6", 0, offset_y).Value = "'*****";
            }
            else if (row.発注価格 != decimal.MinValue)
            {
                xls.Cell("F6", 0, offset_y).Value = row.発注価格;
            }
            // 完了_納品日
            if (row.完了_納品日 != DateTime.MinValue)
            {
                xls.Cell("G6", 0, offset_y).Value = row.完了_納品日.ToString("yyyy/MM/dd");
            }
            // 請求書日
            if (row.請求書日 != DateTime.MinValue)
            {
                xls.Cell("H6", 0, offset_y).Value = row.請求書日.ToString("yyyy/MM/dd");
            }
            // 請求額
            if (row.請求額 == decimal.MaxValue)
            {
                xls.Cell("I6", 0, offset_y).Value = "'*****";
            }
            else if (row.請求額 != decimal.MinValue)
            {
                xls.Cell("I6", 0, offset_y).Value = row.請求額;
            }
            // 備考
            xls.Cell("J6", 0, offset_y).Value = row.備考;
            xls.Cell("J6", 0, offset_y).Attr.WrapText = false;

            // 起票日
            if (row.起票日 != DateTime.MinValue)
            {
                xls.Cell("K6", 0, offset_y).Value = row.起票日.ToString("yyyy/MM/dd");
            }
            // 概算計上額
            if (row.概算計上額 != decimal.MinValue)
            {
                xls.Cell("L6", 0, offset_y).Value = row.概算計上額;
            }
            // 計上月
            if (row.計上月 != "")
            {
                xls.Cell("O6", 0, offset_y).Value = row.計上月 + "月";
            }
        }

        /// <summary>
        /// 帳票の出力対象判定を行う
        /// </summary>
        /// <param name="odThi"></param>
        /// <returns>
        /// 出力対象の場合、Trueを返す
        /// </returns>
        private bool 船用品発注管理表出力対象判定(DBConnect dbConnect, OdThi odThi)
        {
            bool ret = true;

            #region 手配依頼種別マスタが"船用品"のモノが出力対象
            // 手配依頼種別マスタ
            //MsThiIraiSbt msThiIraiSbt = MsThiIraiSbt.GetRecord(new MsUser(), odThi.MsThiIraiSbtID);
            // 手配依頼詳細種別マスタ
            //MsThiIraiShousai msThiIraiShousai = MsThiIraiShousai.GetRecord(new MsUser(), odThi.MsThiIraiShousaiID);

            // 手配依頼種別マスタが"船用品"のモノが出力対象
            //if (msThiIraiSbt.ThiIraiSbtName == "船用品")
            if (odThi.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
            {
            }
            else
            {
                return false;
            }
            #endregion

            #region 発注済みのモノが出力対象
            // [OD_MM]見積依頼
            List<OdMm> list_odMm = OdMm.GetRecordsByOdThiID(dbConnect, new MsUser(), odThi.OdThiID);
            if (list_odMm.Count == 0)
            {
                // 「見積依頼」がない。
                return false;
            }
            int odMkStatus = -1;
            foreach (OdMm odMm in list_odMm)
            {
                // [OD_MK]見積回答
                // 1つでも発注済みのものがあるか？
                List<OdMk> list_odMk = OdMk.GetRecordsByOdMmID(dbConnect, new MsUser(), odMm.OdMmID);
                foreach (OdMk odMk in list_odMk)
                {
                    // 発注済みのモノが出力対象
                    // 発注済み = 4
                    if (odMk.Status == 4)
                    {
                        odMkStatus = 4;
                    }
                }
            }
            if (odMkStatus != 4)
            {
                // 「発注済み」がない。
                return false;
            }
            #endregion

            return ret;
        }

    }

    public class 船用品管理表Row
    {
        public 船用品管理表Row()
        {
            計上月 = "";
        }

        public DateTime 発生日 { get; set; }
        public string 手配内容 { get; set; }
        public string 発注番号 { get; set; }
        public DateTime 発注日 { get; set; }
        public string 業者 { get; set; }
        public decimal 発注価格 { get; set; }
        public DateTime 完了_納品日 { get; set; }
        public DateTime 請求書日 { get; set; }
        public decimal 請求額 { get; set; }
        public string 備考 { get; set; }
        public DateTime 起票日 { get; set; }
        public decimal 概算計上額 { get; set; }
        public string 計上月 { get; set; }


        public 船用品管理表Row Clone()
        {
            船用品管理表Row clone = new 船用品管理表Row();

            clone.発生日       = 発生日;
            clone.手配内容     = 手配内容;
            clone.発注番号     = 発注番号;
            clone.発注日       = 発注日;
            clone.業者         = 業者;
            clone.発注価格     = 発注価格;
            clone.完了_納品日 = 完了_納品日;
            clone.請求書日     = 請求書日;
            clone.請求額       = 請求額;
            clone.備考         = 備考;
            clone.起票日       = 起票日;
            clone.概算計上額   = 概算計上額;
            clone.計上月       = 計上月;

            return clone;
        }
    }

    public class 船員月次
    {
        public int month;
        public DateTime from;
        public DateTime to;

        public static Dictionary<int, 船員月次> Get船員月次(MsUser loginUser, DateTime date)
        {
            return Get船員月次(null, loginUser, date);
        }
        public static Dictionary<int, 船員月次> Get船員月次(DBConnect dbConnect,MsUser loginUser, DateTime date)
        {
            List<MsSiGetsujiShimeBi> msgsbs = MsSiGetsujiShimeBi.GetRecords(dbConnect, loginUser);

            Dictionary<int, 船員月次> ret = new Dictionary<int, 船員月次>();
            for (int i = 0; i < 12; i++)
            {
                船員月次 sg = new 船員月次();
                sg.month = i + 1;
                ret.Add(sg.month, sg);
            }
            foreach (MsSiGetsujiShimeBi msgsb in msgsbs)
            {
                int y = date.Year;
                int m = int.Parse(msgsb.Month);
                if (m < NBaseCommon.Common.FiscalYearStartMonth)
                {
                    y++;
                }
                船員月次 sg = ret[m];
                sg.to = new DateTime(y, m, msgsb.ShimeBi);

                if ((m + 1) == 13)
                {
                    sg = ret[1];
                }
                else if ((m + 1) == NBaseCommon.Common.FiscalYearStartMonth)
                {
                    y--;
                    sg = ret[m + 1];
                }
                else
                {
                    sg = ret[m + 1];
                }
                sg.from = new DateTime(y, m, msgsb.ShimeBi);
            }

            return ret;
        }

    }
}
