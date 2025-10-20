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
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseCommon;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_Excelランニング管理表_取得(NBaseData.DAC.MsUser loginUser, int FromYear, int FromMonth, int ToYear, int ToMonth);
    }

    public partial class Service
    {

        public byte[] BLC_Excelランニング管理表_取得(NBaseData.DAC.MsUser loginUser, int FromYear, int FromMonth, int ToYear, int ToMonth)
        {
            try
            {
                #region 元になるファイルの確認と出力ファイル生成
                string BaseFileName = "ランニング管理表";
                string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
                string templateName = path + "Template_" + BaseFileName + ".xlsx";

                string outpath = NBaseUtil.FileUtils.CheckOutPath(path)+"\\";
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
                        if (Makeランニング管理表(loginUser, FromYear, FromMonth, ToYear, ToMonth, templateName, outPutFileName))
                        {
                            isComplated = true;
                            break;
                        }
                    }
                    catch(ExcelCreatorException)
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
            catch( Exception Ex )
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

        private bool Makeランニング管理表(NBaseData.DAC.MsUser loginUser, int FromYear, int FromMonth, int ToYear, int ToMonth, string templateName, string outPutFileName)
        {
            bool ret = true;
            bool execFlag = false;

            // 対象の予算（費目のID）
            const int MS_HIMOKU_ランニング費用_ID = 54;

            try
            {
                using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
                {
                    try
                    {
                        using (DBConnect dbConnect = new DBConnect())
                        {
                            execFlag = true;

                            // 指定されたテンプレートを元にファイルを作成
                            #region 以前のｺｰﾄﾞ
                            //if (BaseFileName != null && BaseFileName.Length != 0)
                            //{
                            //    xls.OpenBook(outPutFileName, templateName);
                            //}
                            #endregion

                            xls.OpenBook(outPutFileName, templateName);
                            if (xls.ErrorNo != ExcelCreator.ErrorNo.NoError)
                            {
                                return false;
                            }

                            // 2010.03: パフォーマンスの改善案（未実装）
                            //DateTime hachuDateFrom = new DateTime(FromYear, FromMonth, 1, 0, 0, 0);
                            //DateTime hachuDateTo = new DateTime(ToYear, ToMonth, GetMonthLastDay(ToYear, ToMonth), 23, 59, 59);
                            //List<NBaseData.BLC.管理表Info> ランニング管理表Infos = NBaseData.BLC.管理表Info.GetRecordsForランニング管理表(loginUser, hachuDateFrom, hachuDateTo);

                            Dictionary<int, 船員月次> 月次range = 船員月次.Get船員月次(dbConnect, loginUser, new DateTime(FromYear, FromMonth, 1, 0, 0, 0));

                            // [MS_VESSEL]船名
                            List<MsVessel> msVesselList = MsVessel.GetRecordsByHachuEnabled(dbConnect ,new MsUser());
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
                                xls.Cell("E2").Value = FromYear.ToString() + "年度予算";
                                // 年度予算
                                BgYosanHead bgYosanHead = BgYosanHead.GetRecordByYear(dbConnect, new MsUser(), FromYear.ToString());
                                if (bgYosanHead != null)
                                {
                                    BgYosanItem bgYosanItem = BgYosanItem.GetRecordByYearHimokuIDMsVesselID(dbConnect, new MsUser(), bgYosanHead.YosanHeadID, FromYear, MS_HIMOKU_ランニング費用_ID, msVessel.MsVesselID);
                                    if (bgYosanItem != null)
                                    {
                                        xls.Cell("F2").Value = bgYosanItem.Amount;
                                    }
                                }
                                // 月計上額
                                xls.Cell("L2").Value = ToMonth.ToString() + "月計上額";

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


                                List<OdThi> list_odThi = OdThi.GetRecordsByFilter( dbConnect, new MsUser(), odThiFilter);

                                List<ランニング管理表Row> list_ランニング管理表Row = new List<ランニング管理表Row>();
                                #region 出力データの準備
                                foreach (OdThi odThi in list_odThi)
                                {
                                    // 出力対象のレコードか判定する
                                    if (!ランニング管理表出力対象判定(dbConnect, odThi))
                                    {
                                        continue;
                                    }

                                    // ランニング管理表Rowクラス
                                    ランニング管理表Row row = new ランニング管理表Row();

                                    // 2009.10.16:aki 発注者は「事務担当者」を出力してください（ by SUZUKI )
                                    //row.発注者 = odThi.ThiUserName;
                                    row.発注者 = odThi.JimTantouName;
                                    row.手配内容 = odThi.Naiyou;
                                    // 2009.10.16:aki 発注日は「発注」ボタンをクリックしたときの日時（ by SUZUKI )
                                    //row.発注日 = odThi.ThiIraiDate;
                                    row.備考 = odThi.Bikou;

                                    // [OD_MM]見積依頼
                                    List<OdMm> list_odMm = OdMm.GetRecordsByOdThiID(dbConnect, new MsUser(), odThi.OdThiID);
                                    if (list_odMm.Count == 0)
                                    {
                                        continue;
                                    }
                                    foreach (OdMm odMm in list_odMm)
                                    {
                                        // [OD_MK]見積回答
                                        List<OdMk> list_odMk = OdMk.GetRecordsByOdMmID(dbConnect, new MsUser(), odMm.OdMmID);
                                        if (list_odMk.Count == 0)
                                        {
                                            continue;
                                        }
                                        foreach (OdMk odMk in list_odMk)
                                        {
                                            if (odMk.Nendo != FromYear.ToString()) // 20190514 年度が違うものは出力対象外とする
                                                continue;

                                            if (odMk.Status != 4)
                                            {
                                                continue;
                                            }

                                            MsCustomer msCustomer = MsCustomer.GetRecord(dbConnect, new MsUser(), odMk.MsCustomerID);

                                            row.発注日 = odMk.HachuDate;
                                            //row.発注番号 = odMk.MkNo.Replace("Enabled", "");
                                            row.発注番号 = odMk.HachuNo; // 2009.11.10:aki (W090207)
                                            row.業者 = msCustomer.CustomerName;
                                            row.見積り額 = odMk.Amount;
                                            if (odMk.Carriage > 0)
                                            {
                                                row.見積り額 += odMk.Carriage;
                                            }
                                            if (odMk.MkAmount > 0)
                                            {
                                                row.見積り額 -= odMk.MkAmount;
                                            }
                                            row.概算計上額 = decimal.MinValue;
                                            row.落成見積り額 = decimal.MinValue;
                                            row.内訳科目 = "";

                                            // [OD_JRY]受領
                                            List<OdJry> list_odJry = OdJry.GetRecordsByOdMkId(dbConnect, new MsUser(), odMk.OdMkID);
                                            if (list_odJry.Count == 0)
                                            {
                                                ランニング管理表Row 回答row = row.Clone();
                                                list_ランニング管理表Row.Add(回答row);
                                                continue;
                                            }
                                            bool sameJry = false;
                                            foreach (OdJry odJry in list_odJry)
                                            {
                                                // 2010.07.15:aki 分納等の場合、ここで初期化が必要でした
                                                row.完了_納品日 = DateTime.MinValue;
                                                row.概算計上額 = decimal.MinValue;
                                                row.内訳科目 = "";

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

                                                // [OD_SHR]支払------------------------------
                                                List<OdShr> list_odShr = OdShr.GetRecordByOdJryID(dbConnect, new MsUser(), odJry.OdJryID);
                                                // 2010.03:aki 支払合算対応のため、以下の４行追加
                                                if (list_odShr.Count == 0)
                                                {
                                                    list_odShr = OdShr.GetRecordByGassanItem(dbConnect, loginUser, odJry.OdJryID);
                                                }
                                                if (list_odShr.Count == 0)
                                                {
                                                    ランニング管理表Row 受領row = row.Clone();
                                                    if (sameJry)
                                                    {
                                                        受領row.見積り額 = decimal.MaxValue;
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
                                                    list_ランニング管理表Row.Add(受領row);
                                                    continue;
                                                }

                                                // 落成見積り額を取得
                                                decimal 落成見積り額 = decimal.MinValue;
                                                foreach (OdShr odShr in list_odShr)
                                                {
                                                    if (odShr.Sbt == (int)OdShr.SBT.落成)
                                                    {
                                                        // 落成見積り額
                                                        落成見積り額 = odShr.Amount;
                                                        if (odShr.Carriage > 0)
                                                        {
                                                            落成見積り額 += odShr.Carriage;
                                                        }
                                                        if (odShr.NebikiAmount > 0)
                                                        {
                                                            落成見積り額 -= odShr.NebikiAmount;
                                                        }
                                                        row.概算計上額 = 落成見積り額;

                                                        // 内訳科目
                                                        MsKamoku msKamoku = MsKamoku.GetRecord(dbConnect, new MsUser(), odShr.KamokuNo, odShr.UtiwakeKamokuNo);
                                                        if (msKamoku != null)
                                                        {
                                                            row.内訳科目 = msKamoku.KamokuName;
                                                        }
                                                    }
                                                }
                                                // 落成見積り額
                                                if (落成見積り額 != decimal.MinValue)
                                                {
                                                    row.落成見積り額 = 落成見積り額;
                                                }

                                                int shrCount = 0;

                                                // 決定価格を取得
                                                foreach (OdShr odShr in list_odShr)
                                                {
                                                    if (odShr.Sbt == (int)OdShr.SBT.支払)
                                                    {
                                                        // 標準化モジュールは基幹連携がないので、以下コメント
                                                        //if (odShr.Status == odShr.OdStatusValue.Values[(int)OdShr.STATUS.支払済].Value &&
                                                        //    (odShr.SyoriStatus != ((int)支払実績連携IF.STATUS.支払済み).ToString() &&
                                                        //     odShr.SyoriStatus != ((int)支払実績連携IF.STATUS.実績).ToString()))
                                                        //{
                                                        //    // 支払い済みなんだけど、基幹で支払い済み,実績でないものは、読み飛ばす
                                                        //    continue;
                                                        //}
                                                        if (odShr.Status == odShr.OdStatusValue.Values[(int)OdShr.STATUS.支払依頼済み].Value)
                                                        {
                                                            // 支払依頼済み、読み飛ばす(基幹取込エラー)
                                                            continue;
                                                        }

                                                        shrCount++;
                                                        ランニング管理表Row 支払row = row.Clone();

                                                        // 決定価格
                                                        支払row.決定価格 = odShr.Amount;
                                                        if (odShr.Carriage > 0)
                                                        {
                                                            支払row.決定価格 += odShr.Carriage;
                                                        }
                                                        if (odShr.NebikiAmount > 0)
                                                        {
                                                            支払row.決定価格 -= odShr.NebikiAmount;
                                                        }
                                                        支払row.概算計上額 = 支払row.決定価格;

                                                        // 内訳科目
                                                        MsKamoku msKamoku = MsKamoku.GetRecord(dbConnect, new MsUser(), odShr.KamokuNo, odShr.UtiwakeKamokuNo);
                                                        if (msKamoku != null)
                                                        {
                                                            支払row.内訳科目 = msKamoku.KamokuName;
                                                        }

                                                        支払row.請求書日 = odShr.ShrIraiDate;
                                                        if (odShr.Status != odShr.OdStatusValue.Values[(int)OdShr.STATUS.支払作成済み].Value)
                                                        {
                                                            // 2010.03:aki [W090251]起票日=計上日
                                                            //支払row.起票日 = odShr.Kihyoubi;
                                                            支払row.起票日 = odShr.KeijyoDate;
                                                        }
                                                        if (odShr.Status == odShr.OdStatusValue.Values[(int)OdShr.STATUS.支払済].Value)
                                                        {
                                                            if (DateTime.MinValue != odShr.KeijyoDate)
                                                            {
                                                                // 2010.03:aki [W090251]計上月=概算計上月（完了_納品日の月）
                                                                //支払row.計上月 = odShr.KeijyoDate.Month.ToString();
                                                                支払row.計上月 = row.完了_納品日.Month.ToString();
                                                            }
                                                            else
                                                            {
                                                                支払row.計上月 = null;
                                                            }

                                                            // 支払い済みの場合は、概算計上額をクリア
                                                            支払row.概算計上額 = decimal.MinValue;
                                                        }
                                                        else
                                                        {
                                                            支払row.計上月 = null;
                                                        }

                                                        if (sameJry)
                                                        {
                                                            支払row.見積り額 = decimal.MaxValue;
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
                                                            支払row.決定価格 = decimal.MaxValue;
                                                            支払row.概算計上額 = decimal.MinValue;
                                                        }

                                                        list_ランニング管理表Row.Add(支払row);
                                                    }
                                                }
                                                if (shrCount == 0)
                                                {
                                                    list_ランニング管理表Row.Add(row);
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion

                                // ここから振替取立のデータ
                                #region
                                OdFurikaeToritateFilter odFkTtFilter = new OdFurikaeToritateFilter();
                                odFkTtFilter.MsVesselID = msVessel.MsVesselID;
                                odFkTtFilter.HachuDateFrom = new DateTime(FromYear, FromMonth, 1, 0, 0, 0);
                                odFkTtFilter.HachuDateTo = new DateTime(ToYear, ToMonth, GetMonthLastDay(ToYear, ToMonth), 23, 59, 59);
                                odFkTtFilter.MsThiIraiSbtID = NBaseCommon.Common.MsThiIraiSbt_修繕ID;
                                odFkTtFilter.MsThiIraiShousaiID = NBaseCommon.Common.MsThiIraiShousai_小修理ID;
                                List<OdFurikaeToritate> OdFurikaeToritates = null;
                                OdFurikaeToritates = OdFurikaeToritate.GetRecordsByFilter(dbConnect, loginUser, odFkTtFilter);

                                foreach (OdFurikaeToritate furikaeToritate in OdFurikaeToritates)
                                {
                                    ランニング管理表Row row = new ランニング管理表Row();
                                    row.発注者 = furikaeToritate.CreateUserName;
                                    row.手配内容 = furikaeToritate.Koumoku;
                                    row.発注日 = furikaeToritate.HachuDate;
                                    //row.発注番号
                                    row.業者 = furikaeToritate.MsCustomerName;
                                    row.見積り額 = decimal.MinValue;
                                    row.完了_納品日 = furikaeToritate.Kanryobi;
                                    row.落成見積り額 = decimal.MinValue;
                                    row.決定価格 = furikaeToritate.Amount;
                                    row.備考 = furikaeToritate.Bikou;
                                    row.請求書日 = furikaeToritate.Seikyushobi;
                                    row.起票日 = furikaeToritate.Kihyobi;
                                    //row.計上月
                                    row.概算計上額 = decimal.MinValue;
                                    //row.内訳科目

                                    list_ランニング管理表Row.Add(row);
                                }
                                #endregion

                                // ここから準備金のデータ
                                #region
                                List<SiGetsujiShime> SiGetsujiShimes = null;
                                //SiGetsujiShimes = SiGetsujiShime.GetRecords(loginUser, DateTime.Today);
                                //SiGetsujiShimes = SiGetsujiShime.GetRecords(dbConnect, loginUser, odThiFilter.HachuDateFrom);
                                DateTime fromDate = new DateTime(FromYear, FromMonth, 1);
                                SiGetsujiShimes = SiGetsujiShime.GetRecords(dbConnect, loginUser, fromDate);
                                List<SiJunbikin> SiJunbikins = null;
                                //SiJunbikins = SiJunbikin.Get_振替_修繕費(loginUser, DateTime.Today, msVessel.MsVesselID);
                                //SiJunbikins = SiJunbikin.Get_振替_修繕費(dbConnect, loginUser, odThiFilter.HachuDateFrom, msVessel.MsVesselID);
                                SiJunbikins = SiJunbikin.Get_振替_修繕費(dbConnect, loginUser, fromDate, msVessel.MsVesselID);
                                decimal[] 船用金修繕費 = new decimal[12];
                                for (int i = 0; i < 12; i++)
                                {
                                    船用金修繕費[i] = 0;
                                }
                                foreach (SiJunbikin jk in SiJunbikins)
                                {
                                    //int m = jk.JunbikinDate.Month;
                                    //船用金修繕費[m] += jk.KingakuOut;
                                    for (int i = 1; i <= 12; i++)
                                    {
                                        if (jk.JunbikinDate >= 月次range[i].from && jk.JunbikinDate < 月次range[i].to)
                                        {
                                            船用金修繕費[i] += jk.KingakuOut;
                                        }
                                    }
                                }
                                for (int i = 0; i < 12; i++)
                                {
                                    if (船用金修繕費[i] > 0)
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
                                            ランニング管理表Row row = new ランニング管理表Row();
                                            row.発注者 = "船員Ｇ";
                                            row.手配内容 = "船用金修繕費";
                                            row.発注日 = shime.RenewDate;
                                            //row.発注番号
                                            //row.業者
                                            row.見積り額 = decimal.MinValue;
                                            //row.完了_納品日
                                            row.落成見積り額 = decimal.MinValue;
                                            row.決定価格 = 船用金修繕費[i];
                                            //row.備考
                                            //row.請求書日
                                            //row.起票日
                                            row.計上月 = (int.Parse(shime.NenGetsu.Substring(4))).ToString();
                                            //row.概算計上額
                                            //row.内訳科目

                                            list_ランニング管理表Row.Add(row);
                                        }
                                    }
                                }
                                #endregion

                                #region 削除したｺｰﾄﾞ
                                // 2010.03.07:aki [W090251]計上額は、指定された月の計上額とする
                                //                表示対象内の合計とするため、ここはコメントアウト　データ行出力処理内で算出
                                //string month = DateTime.Now.Month.ToString();
                                //string month = ToMonth.ToString();
                                //var ランニング管理表Rows = from p in list_ランニング管理表Row
                                //                   where p.計上月 == month
                                //                   select p;
                                //foreach (var row in ランニング管理表Rows)
                                //{
                                //    計上額 = 計上額 + row.決定価格;
                                //}
                                //xls.Cell("N2").Value = 計上額;
                                #endregion

                                #region データ行出力
                                // 2018.01 年度で検索
                                ////var ランニング管理表Rows = from p in list_ランニング管理表Row
                                ////                   where p.発注日 <= odThiFilter.ThiIraiDateTo
                                ////                   orderby p.発注日
                                ////                   select p;
                                //var ランニング管理表Rows = from p in list_ランニング管理表Row
                                //                   where p.発注日 <= odThiFilter.HachuDateTo
                                //                      && p.発注日 >= odThiFilter.HachuDateFrom
                                //                   orderby p.発注日
                                //                   select p;
                                var ランニング管理表Rows = from p in list_ランニング管理表Row
                                                   where p.発注日 <= HachuDateTo
                                                   orderby p.発注日
                                                   select p;
                                int offset_y = 0;
                                decimal 計上額 = 0;
                                foreach (var row in ランニング管理表Rows)
                                {
                                    // 発注者
                                    xls.Cell("A7", 0, offset_y).Value = row.発注者;
                                    // 手配内容
                                    xls.Cell("B7", 0, offset_y).Value = row.手配内容;
                                    // 発注日
                                    if (DateTime.MinValue != row.発注日)
                                    {
                                        xls.Cell("C7", 0, offset_y).Value = row.発注日.ToString("yyyy/MM/dd");
                                    }
                                    // 発注番号
                                    xls.Cell("D7", 0, offset_y).Value = row.発注番号;
                                    // 業者
                                    xls.Cell("E7", 0, offset_y).Value = row.業者;
                                    // 見積り額
                                    if (row.見積り額 == decimal.MaxValue)
                                    {
                                        xls.Cell("F7", 0, offset_y).Value = "'*****";
                                    }
                                    else if (row.見積り額 > decimal.MinValue)
                                    {
                                        xls.Cell("F7", 0, offset_y).Value = row.見積り額;
                                    }
                                    // 完了_納品日
                                    if (DateTime.MinValue != row.完了_納品日)
                                    {
                                        xls.Cell("G7", 0, offset_y).Value = row.完了_納品日.ToString("yyyy/MM/dd");
                                    }
                                    // 落成見積り額
                                    if (row.落成見積り額 != decimal.MinValue)
                                    {
                                        xls.Cell("H7", 0, offset_y).Value = row.落成見積り額;
                                    }
                                    // 決定価格
                                    if (row.決定価格 == decimal.MaxValue)
                                    {
                                        xls.Cell("I7", 0, offset_y).Value = "'*****";
                                    }
                                    else
                                    {
                                        xls.Cell("I7", 0, offset_y).Value = row.決定価格;
                                    }
                                    // 備考
                                    xls.Cell("J7", 0, offset_y).Value = row.備考;
                                    // 請求書日
                                    if (DateTime.MinValue != row.請求書日)
                                    {
                                        xls.Cell("K7", 0, offset_y).Value = row.請求書日.ToString("yyyy/MM/dd");
                                    }
                                    // 起票日
                                    if (DateTime.MinValue != row.起票日)
                                    {
                                        xls.Cell("L7", 0, offset_y).Value = row.起票日.ToString("yyyy/MM/dd");
                                    }
                                    // 計上月
                                    if (null != row.計上月)
                                    {
                                        xls.Cell("M7", 0, offset_y).Value = row.計上月 + "月";

                                        // 2010.09.03: 合算データは無視する
                                        //if (row.計上月 == ToMonth.ToString())
                                        if (row.計上月 == ToMonth.ToString() && row.決定価格 != decimal.MaxValue)
                                        {
                                            計上額 = 計上額 + row.決定価格;
                                        }
                                    }
                                    // 概算計上額
                                    if (row.概算計上額 != decimal.MinValue)
                                    {
                                        xls.Cell("N7", 0, offset_y).Value = row.概算計上額;
                                    }
                                    // 内訳科目
                                    xls.Cell("O7", 0, offset_y).Value = row.内訳科目;


                                    offset_y++;
                                }
                                xls.Cell("N2").Value = 計上額;
                                #endregion


                                #region 罫線を出力
                                for (int i = 0; i < offset_y; i++)
                                {
                                    xls.Cell("A7:O7", 0, i).Attr.LineBottom ( ExcelCreator.BorderStyle.Dotted, ExcelCreator.xlColor.Black);
                                }
                                int val = 6 + offset_y;
                                xls.Cell("A7:A" + val.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell("A7:A" + val.ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell("B7:B" + val.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                                xls.Cell("C7:C" + val.ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell("D7:D" + val.ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell("E7:E" + val.ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell("F7:F" + val.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                                xls.Cell("G7:G" + val.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                                xls.Cell("H7:H" + val.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                                xls.Cell("I7:I" + val.ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell("J7:J" + val.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                                xls.Cell("K7:K" + val.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                                xls.Cell("L7:L" + val.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                                xls.Cell("M7:M" + val.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                                xls.Cell("N7:N" + val.ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell("O7:O" + val.ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell("A" + val.ToString() + ":O" + val.ToString()).Attr.LineBottom (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                #endregion

                                #region  合計を出力
                                xls.Cell("H" + (val + 1).ToString()).Value = "予算";
                                xls.Cell("H" + (val + 2).ToString()).Value = "実績合計";
                                xls.Cell("H" + (val + 3).ToString()).Value = "比較";

                                xls.Cell("I" + (val + 1).ToString()).Value = "=SUM(F2)";
                                xls.Cell("I" + (val + 2).ToString()).Value = "=SUM(I7:I" + val.ToString() + ")";
                                xls.Cell("I" + (val + 3).ToString()).Value = "=SUM(I" + (val + 1).ToString() + "-I" + (val + 2).ToString() + ")";

                                // 実績
                                xls.Cell("F3").Value = "=I" + (val + 2).ToString(); //　"実績合計"を参照
                                // 比較
                                xls.Cell("F4").Value = "=I" + (val + 3).ToString(); //　"比較"を参照

                                // 概算計上額
                                xls.Cell("N3").Value = "=SUM(N7:N" + val.ToString() + ")";

                                #endregion

                                #endregion

                            }
                            xls.DeleteSheet(0, 1);
                            xls.CloseBook(true);
                        }
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
                return ret;
            }
            catch(Exception ex)
            {
                NBaseCommon.LogFile.Write(loginUser.FullName, "ﾗﾝﾆﾝｸﾞ管理表：" + ex.Message);
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

        public bool ランニング管理表出力対象判定(OdThi odThi)
        {
            return ランニング管理表出力対象判定(null, odThi);
        }
        /// <summary>
        /// 帳票の出力対象判定を行う
        /// </summary>
        /// <param name="odThi"></param>
        /// <returns>
        /// 出力対象の場合、Trueを返す
        /// </returns>
        public bool ランニング管理表出力対象判定(DBConnect dbConnect, OdThi odThi)
        {
            bool ret = true;

            #region 手配依頼種別マスタが"修繕"、手配依頼詳細種別マスタが"小修理"のモノが出力対象
            // 手配依頼種別マスタ
            //MsThiIraiSbt msThiIraiSbt = MsThiIraiSbt.GetRecord(new MsUser(), odThi.MsThiIraiSbtID);
            // 手配依頼詳細種別マスタ
            //MsThiIraiShousai msThiIraiShousai = MsThiIraiShousai.GetRecord(new MsUser(), odThi.MsThiIraiShousaiID);

            // 手配依頼種別マスタが"修繕"、手配依頼詳細種別マスタが"小修理"のモノが出力対象
            //if (msThiIraiSbt.ThiIraiSbtName == "修繕" && msThiIraiShousai.ThiIraiShousaiName == "小修理")
            if (odThi.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID && odThi.MsThiIraiShousaiID == NBaseCommon.Common.MsThiIraiShousai_小修理ID)
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

        public int GetMonthLastDay(int year, int month)
        {
            DateTime dt = new DateTime(year, month, 1);
            dt = dt.AddMonths(1);
            dt = dt.AddDays(-1);

            return dt.Day;
        }
    }

    public class ランニング管理表Row
    {
        public ランニング管理表Row()
        {

        }

        public string 発注者 { get; set; }
        public string 手配内容 { get; set; }
        public DateTime 発注日 { get; set; }
        public string 発注番号 { get; set; }
        public string 業者 { get; set; }
        public decimal 見積り額 { get; set; }
        public DateTime 完了_納品日 { get; set; }
        public decimal 落成見積り額 { get; set; }
        public decimal 決定価格 { get; set; }
        public string 備考 { get; set; }
        public DateTime 請求書日 { get; set; }
        public DateTime 起票日 { get; set; }
        public string 計上月 { get; set; }
        public decimal 概算計上額 { get; set; }
        public string 内訳科目 { get; set; }


        public ランニング管理表Row Clone()
        {
            ランニング管理表Row clone = new ランニング管理表Row();

            clone.発注者 　　　= 発注者;
            clone.手配内容     = 手配内容;
            clone.発注日       = 発注日;
            clone.発注番号     = 発注番号;
            clone.業者         = 業者;
            clone.見積り額     = 見積り額;
            clone.完了_納品日 = 完了_納品日;
            clone.落成見積り額 = 落成見積り額;
            clone.決定価格 　　= 決定価格;
            clone.備考 　　　　= 備考;
            clone.請求書日 　　= 請求書日;
            clone.起票日       = 起票日;
            clone.計上月       = 計上月;
            clone.概算計上額   = 概算計上額;
            clone.内訳科目     = 内訳科目;

            return clone;
        }
    }
}
