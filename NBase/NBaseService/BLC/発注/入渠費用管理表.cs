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
using System.Collections;
using ExcelCreator= AdvanceSoftware.ExcelCreator;
using NBaseCommon;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_Excel入渠費用管理表_取得(NBaseData.DAC.MsUser loginUser, int FromYear, int FromMonth, int ToYear, int ToMonth);
    }

    public partial class Service
    {
        public byte[] BLC_Excel入渠費用管理表_取得(NBaseData.DAC.MsUser loginUser, int FromYear, int FromMonth, int ToYear, int ToMonth)
        {
            try
            {
                #region 元になるファイルの確認と出力ファイル生成
                string BaseFileName = "入渠費用管理表";
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
                        if (Make入渠費用管理表(loginUser, FromYear, FromMonth, ToYear, ToMonth, templateName, outPutFileName))
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


        private bool Make入渠費用管理表(NBaseData.DAC.MsUser loginUser, int FromYear, int FromMonth, int ToYear, int ToMonth, string templateName, string outPutFileName)
        {
            bool ret = true;
            bool execFlag = false;

            // 対象の予算（費目のID）
            //const int MS_HIMOKU_船舶修繕費_ID = 42;
            const int MS_HIMOKU_ランニング費用_ID = 54;

            Dictionary<int, int> dic_himoku_kamoku = new Dictionary<int, int>();
            dic_himoku_kamoku.Add(1, 44);
            dic_himoku_kamoku.Add(2, 46);
            dic_himoku_kamoku.Add(3, 47);
            dic_himoku_kamoku.Add(4, 48);
            dic_himoku_kamoku.Add(5, 49);
            dic_himoku_kamoku.Add(6, 50);
            dic_himoku_kamoku.Add(7, 51);
            dic_himoku_kamoku.Add(8, 52);
            dic_himoku_kamoku.Add(9, 53);
            dic_himoku_kamoku.Add(10, 79);

            DateTime HachuDateTo = new DateTime(ToYear, ToMonth, GetMonthLastDay(ToYear, ToMonth), 23, 59, 59);

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
                            //List<NBaseData.BLC.管理表Info> 入渠管理表Infos = NBaseData.BLC.管理表Info.GetRecordsFor入渠管理表(loginUser, hachuDateFrom, hachuDateTo);

NBaseCommon.LogFile.Write(loginUser.FullName, "入渠管理表：予算確認");
                            List<MsHimoku> msHimokuList = MsHimoku.GetRecordsWithMsKamoku(dbConnect, loginUser);
                            BgYosanHead bgYosanHead = BgYosanHead.GetRecordByYear(dbConnect, loginUser, FromYear.ToString());
                            List<BgKadouVessel> bgKadouVesselList = null;
                            if (bgYosanHead != null)
                            {
                                bgKadouVesselList = BgKadouVessel.GetRecordsByYosanHeadID(dbConnect, loginUser, bgYosanHead.YosanHeadID);
                            }
                            else
                            {
                                NBaseCommon.LogFile.Write(loginUser.FullName, "入渠管理表：予算が設定されていません:[" + FromYear.ToString() + "]");
                            }


                            // 2012.12 : ランニング管理表と合わせるために以下の行を追加
                            Dictionary<int, 船員月次> 月次range = 船員月次.Get船員月次(dbConnect, loginUser, new DateTime(FromYear, FromMonth, 1, 0, 0, 0));

                            // [MS_VESSEL]船名
                            List<MsVessel> msVesselList = MsVessel.GetRecordsByHachuEnabled(dbConnect, loginUser);
                            foreach (MsVessel msVessel in msVesselList)
                            {
                                // 現在の総シート数を確認します
                                int nSheetCount = xls.SheetCount;

                                // 1 番左のシート(シート番号 0)を、最終シート以降にコピー
                                xls.CopySheet(0, nSheetCount, msVessel.VesselName);
                                xls.SheetNo = nSheetCount;

                                #region Excelファイルの編集

                                #region セル割り当て

                                string cell_タイトル = "A1";
                                string cell_船名 = "B3";
                                string cell_検査名 = "D3";
                                string cell_実績 = "J4";
                                string cell_月計上額 = "Q3";
                                string cell_概算計上額 = "Q4";
                                string cell_修繕費予算 = "J3";
                                string cell_総合予算合計 = "M3";
                                string cell_総合実績合計 = "M4";
                                #endregion

                                #region カラム割り当て

                                int startRow = 8;

                                string column_内容 = "A";
                                string column_業者名 = "B";
                                string column_利益計画 = "C";
                                string column_発注日 = "D";
                                string column_発注者 = "E";
                                string column_発注番号 = "F";
                                string column_見積額 = "G";
                                string column_完了日 = "H";
                                string column_落成入手日 = "I";
                                string column_落成見積額 = "J";
                                string column_請求書日 = "K";
                                string column_請求額 = "L";
                                string column_備考 = "M";
                                string column_起票日 = "N";
                                string column_計上月 = "O";
                                string column_支払日 = "P";
                                string column_概算計上 = "Q";

                                string col_内容 = "A8";
                                string col_業者名 = "B8";
                                string col_利益計画 = "C8";
                                string col_発注日 = "D8";
                                string col_発注者 = "E8";
                                string col_発注番号 = "F8";
                                string col_見積額 = "G8";
                                string col_完了日 = "H8";
                                string col_落成入手日 = "I8";
                                string col_落成見積額 = "J8";
                                string col_請求書日 = "K8";
                                string col_請求額 = "L8";
                                string col_備考 = "M8";
                                string col_起票日 = "N8";
                                string col_計上月 = "O8";
                                string col_支払日 = "P8";
                                string col_概算計上 = "Q8";

                                #endregion

                                // タイトル
                                xls.Cell(cell_タイトル).Value = FromYear.ToString() + "年度　　入　渠　費　用　管　理　表";
                                // 船名
                                xls.Cell(cell_船名).Value = msVessel.VesselName;

                                OdThi odThi = new OdThi();
                                
                                int offset_y = 0;

                                decimal shoukei = 0;
                                decimal goukei = 0;
                                decimal keijoGaku = 0;
                                decimal 実績合計 = 0;
                                decimal ランニング実績合計 = 0;
                                //OdThi lastOdThi = null;
                                string KensaName = "";//検査種類名 追加20150907


                                #region [OD_MM]見積依頼 データ取得(list_odMm)
                                OdThiFilter odThiFilter = new OdThiFilter();
                                odThiFilter.MsVesselID = msVessel.MsVesselID;
                                
                                // 2018.01 年度で検索
                                //// 2010.04.21:aki 手配依頼日ではなく発注日で検索する
                                ////odThiFilter.ThiIraiDateFrom = new DateTime(FromYear, FromMonth, 1, 0, 0, 0);
                                ////odThiFilter.ThiIraiDateTo = new DateTime(ToYear, ToMonth, GetMonthLastDay(ToYear, ToMonth), 23, 59, 59);
                                //odThiFilter.HachuDateFrom = new DateTime(FromYear, FromMonth, 1, 0, 0, 0);
                                //odThiFilter.HachuDateTo = new DateTime(ToYear, ToMonth, GetMonthLastDay(ToYear, ToMonth), 23, 59, 59);
                                odThiFilter.Nendo = FromYear.ToString();

                                List<OdMm> list_odMm = OdMm.GetRecordsByFilter(dbConnect, loginUser, odThiFilter);
                                #endregion

                                List<MsNyukyoKamoku> nyukyoKmks = MsNyukyoKamoku.GetRecords(dbConnect, loginUser);

                                foreach (MsNyukyoKamoku msnKmk in nyukyoKmks)
                                {
                                    List<入渠管理表Row> list_入渠管理表Row = new List<入渠管理表Row>();
                                    #region 出力データの準備
                                    ArrayList mmids = new ArrayList();
                                    foreach (OdMm odMm in list_odMm)
                                    {
                                        // 重複データは除く
                                        if (mmids.Contains(odMm.OdMmID))
                                            continue;
                                        mmids.Add(odMm.OdMmID);


                                        // 入渠科目を確認する
                                        if (odMm.MsNyukyoKamokuID != msnKmk.MsNyukyoKamokuID)
                                        {
                                            continue;
                                        }

                                        #region 手配依頼確認
                                        if (odThi.OdThiID != odMm.OdThiID)
                                        {
                                            #region 手配が違っている為、対応する手配の取得
                                            odThi = OdThi.GetRecord(dbConnect, loginUser, odMm.OdThiID);

                                            if (odThi == null)
                                            {
                                                // OD_THIは必ずある(無い時はデータ不良)ので、ここはこない。
                                                continue;
                                            }
                                            #endregion
                                        }

                                        #region 手配依頼種別・手配依頼詳細種別の確認

                                        // 手配依頼種別マスタが"修繕"、手配依頼詳細種別マスタが"小修理"のモノが出力対象
                                        if ((odThi.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID && 
                                             odThi.MsThiIraiShousaiID == NBaseCommon.Common.MsThiIraiShousai_入渠ID) == false)
                                        {
                                            continue;
                                        }
                                        #endregion

                                        #endregion

                                        #region [OD_MK]見積回答
                                        List<OdMk> list_odMk = OdMk.GetRecordsByOdMmID(dbConnect, loginUser, odMm.OdMmID);
                                        foreach (OdMk odMk in list_odMk)
                                        {
                                            if (odMk.Nendo != FromYear.ToString()) // 20190514 年度が違うものは出力対象外とする
                                                continue;

                                            // ステータス
                                            if (odMk.Status != 4)
                                            {
                                                continue;
                                            }

                                            // 入渠管理表Rowクラス
                                            入渠管理表Row row = new 入渠管理表Row();

                                            // 手配内容
                                            row.手配内容 = odThi.Naiyou;
                                            // 業者
                                            MsCustomer msCustomer = MsCustomer.GetRecord(dbConnect, loginUser, odMk.MsCustomerID);
                                            row.業者 = msCustomer.CustomerName;
                                            // 発注日
                                            row.発注日 = odMk.HachuDate;
                                            // 2009.10.16:aki 発注者は「事務担当者」を出力してください（ by SUZUKI )
                                            //row.発注者 = odThi.ThiUserName;
                                            row.発注者 = odThi.JimTantouName;
                                            // 発注番号
                                            //row.発注番号 = odMk.MkNo.Replace("Enabled", "");
                                            row.発注番号 = odMk.HachuNo; // 2009.11.10:aki (W090207)
                                            // 見積り額
                                            row.見積り額 = odMk.Amount;
                                            if (odMk.Carriage > 0)
                                            {
                                                row.見積り額 += odMk.Carriage;
                                            }
                                            if (odMk.MkAmount > 0)
                                            {
                                                row.見積り額 -= odMk.MkAmount;
                                            }
                                            // 備考
                                            row.備考 = odThi.Bikou;

                                            #region [OD_JRY]受領
                                            List<OdJry> list_odJry = OdJry.GetRecordsByOdMkId(dbConnect, loginUser, odMk.OdMkID);
                                            if (list_odJry.Count == 0)
                                            {
                                                入渠管理表Row 回答row = row.Clone();
                                                list_入渠管理表Row.Add(回答row);
                                            }
                                            else
                                            {
                                                bool sameJry = false;
                                                foreach (OdJry odJry in list_odJry)
                                                {
                                                    // 一旦クリアする
                                                    row.完了日 = DateTime.MinValue;
                                                    row.落成入手日 = DateTime.MinValue;
                                                    row.落成見積り額 = decimal.MinValue;
                                                    row.概算計上額 = decimal.MinValue;

                                                    // 完了日
                                                    if (odJry.Status == odJry.OdStatusValue.Values[(int)OdJry.STATUS.受領承認済み].Value)
                                                    {
                                                        row.完了日 = odJry.JryDate;
                                                    }

                                                    // [OD_SHR]支払
                                                    #region
                                                    List<OdShr> list_odShr = OdShr.GetRecordByOdJryID(dbConnect, loginUser, odJry.OdJryID);

                                                    // 2010.03:aki 支払合算対応のため、以下の４行追加
                                                    if (list_odShr.Count == 0)
                                                    {
                                                        list_odShr = OdShr.GetRecordByGassanItem(dbConnect, loginUser, odJry.OdJryID);
                                                    }
                                                    if (list_odShr.Count == 0)
                                                    {
                                                        // 支払いデータがない場合、概算計上額は、受領額
                                                        入渠管理表Row 受領row = row.Clone();
                                                        if (odJry.Status == odJry.OdStatusValue.Values[(int)OdJry.STATUS.受領承認済み].Value)
                                                        {
                                                            //受領row.概算計上額 = odJry.Amount; // 2010.03.11:aki
                                                            //if (odJry.NebikiAmount > 0)
                                                            //{
                                                            //    //受領row.見積り額 -= odJry.NebikiAmount; // 2010.03.11:aki
                                                            //    受領row.概算計上額 -= odJry.NebikiAmount; // 2010.03.11:aki
                                                            //}
                                                            OdJryGaisan jryGaisan = OdJryGaisan.GetRecordByOdJryID(dbConnect, new MsUser(), odJry.OdJryID);
                                                            if (jryGaisan != null)
                                                            {
                                                                受領row.概算計上額 = jryGaisan.Amount;
                                                                if (jryGaisan.Bikou != null && jryGaisan.Bikou.Length > 0)
                                                                {
                                                                    if (受領row.備考.Length > 0)
                                                                        受領row.備考 += " ";
                                                                    受領row.備考 += jryGaisan.Bikou;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                受領row.概算計上額 = odJry.Amount;
                                                                if (odJry.Carriage > 0)
                                                                {
                                                                    受領row.概算計上額 += odJry.Carriage;
                                                                }
                                                                if (odJry.NebikiAmount > 0)
                                                                {
                                                                    受領row.概算計上額 -= odJry.NebikiAmount;
                                                                }
                                                            }
                                                        }
                                                        if (sameJry)
                                                        {
                                                            受領row.見積り額 = decimal.MaxValue;
                                                        }
                                                        else
                                                        {
                                                            sameJry = true;
                                                        }

                                                        list_入渠管理表Row.Add(受領row);
                                                    }
                                                    else
                                                    {
                                                        #region OD_SHR分割(落成と支払に分ける)
                                                        List<OdShr> list_odShr_落成 = new List<OdShr>();
                                                        List<OdShr> list_odShr_支払 = new List<OdShr>();

                                                        foreach (OdShr odshr in list_odShr)
                                                        {
                                                            if (odshr.Sbt == (int)OdShr.SBT.落成)
                                                            {
                                                                list_odShr_落成.Add(odshr);
                                                            }
                                                            else
                                                            {
                                                                if (odshr.Status == odshr.OdStatusValue.Values[(int)OdShr.STATUS.支払済].Value &&
                                                                    (odshr.SyoriStatus != ((int)支払実績連携IF.STATUS.支払済み).ToString() &&
                                                                     odshr.SyoriStatus != ((int)支払実績連携IF.STATUS.実績).ToString()))
                                                                {
                                                                    // 支払い済みなんだけど、基幹で支払い済み,実績でないものは、読み飛ばす
                                                                    continue;
                                                                }
                                                                if (odshr.Status == odshr.OdStatusValue.Values[(int)OdShr.STATUS.支払依頼済み].Value)
                                                                {
                                                                    // 支払依頼済み、読み飛ばす(基幹取込エラー)
                                                                    continue;
                                                                }

                                                                list_odShr_支払.Add(odshr);
                                                            }
                                                        }
                                                        #endregion

                                                        if (list_odShr_落成.Count > 0)
                                                        {
                                                            OdShr odShr_落成 = list_odShr_落成[0];

                                                            // 落成入手日
                                                            if (odShr_落成.Status == odShr_落成.OdStatusValue.Values[(int)OdShr.STATUS.落成済み].Value)
                                                            {
                                                                row.落成入手日 = odShr_落成.ShrDate;
                                                            }

                                                            // 落成見積り額
                                                            row.落成見積り額 = odShr_落成.Amount;
                                                            if (odShr_落成.Carriage > 0)
                                                            {
                                                                row.落成見積り額 += odShr_落成.Carriage;
                                                            }
                                                            if (odShr_落成.NebikiAmount > 0)
                                                            {
                                                                row.落成見積り額 -= odShr_落成.NebikiAmount;
                                                            }

                                                            // 概算計上を出力するときのために、落成額を保持
                                                            row.概算計上額 = row.落成見積り額;
                                                        }
                                                        if (list_odShr_支払.Count == 0)
                                                        {
                                                            #region 支払いデータがない場合
                                                            入渠管理表Row 落成row = row.Clone();
                                                            if (sameJry)
                                                            {
                                                                落成row.見積り額 = decimal.MaxValue;
                                                            }
                                                            else
                                                            {
                                                                sameJry = true;
                                                            }
                                                            list_入渠管理表Row.Add(落成row);
                                                            #endregion
                                                        }
                                                        else
                                                        {
                                                            #region 支払いデータの処理
                                                            int odShrCount = 0;
                                                            foreach (OdShr odShr_支払 in list_odShr_支払)
                                                            {
                                                                odShrCount++;
                                                                入渠管理表Row 支払row = row.Clone();

                                                                // 請求額
                                                                支払row.請求額 = odShr_支払.Amount;
                                                                if (odShr_支払.Carriage > 0)
                                                                {
                                                                    支払row.請求額 += odShr_支払.Carriage;
                                                                }
                                                                if (odShr_支払.NebikiAmount > 0)
                                                                {
                                                                    支払row.請求額 -= odShr_支払.NebikiAmount;
                                                                }
                                                                // 請求書日
                                                                支払row.請求書日 = odShr_支払.ShrIraiDate;
                                                                // 支払日
                                                                支払row.支払日 = odShr_支払.ShrDate;
                                                                // 起票日
                                                                支払row.起票日 = DateTime.MinValue;
                                                                if (odShr_支払.Status != odShr_支払.OdStatusValue.Values[(int)OdShr.STATUS.支払作成済み].Value)
                                                                {
                                                                    // 2010.03:aki [W090251]起票日=計上日
                                                                    //支払row.起票日 = odShr_支払.Kihyoubi;
                                                                    支払row.起票日 = odShr_支払.KeijyoDate;
                                                                }
                                                                // 計上月
                                                                if (odShr_支払.Status == odShr_支払.OdStatusValue.Values[(int)OdShr.STATUS.支払済].Value)
                                                                {
                                                                    if (odShr_支払.KeijyoDate != DateTime.MinValue)
                                                                    {
                                                                        // 2010.03:aki [W090251]計上月=概算計上月（完了_納品日の月）
                                                                        //支払row.計上月 = odShr_支払.KeijyoDate.Month.ToString() + "月";
                                                                        支払row.計上月 = row.完了日.Month.ToString() + "月";

                                                                        // 計上されていて当月のモノなら、計上額として集計する
                                                                        if (odShr_支払.KeijyoDate.Month == ToMonth)
                                                                        {
                                                                            if (odShr_支払.HachuNo != null && odShr_支払.HachuNo.Length > 0)
                                                                            {
                                                                                // 合算した場合、計上額として集計しない
                                                                            }
                                                                            else
                                                                            {
                                                                                keijoGaku = keijoGaku + 支払row.請求額;
                                                                            }
                                                                        }
                                                                    }

                                                                    // 支払い済みの場合は、概算計上額をクリア
                                                                    支払row.概算計上額 = decimal.MinValue;
                                                                }
                                                                else
                                                                {
                                                                    // 支払済でない場合、支払い金額を概算計上として出力
                                                                    支払row.概算計上額 = 支払row.請求額;
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
                                                                if (odShr_支払.OdShrGassanHeadID != null && odShr_支払.OdShrGassanHeadID.Length > 0)
                                                                {
                                                                    if (odShr_支払.NebikiAmount > 0)
                                                                    {
                                                                        // 備考(元の備考に追加する場合)
                                                                        if (支払row.備考.Length > 0)
                                                                            支払row.備考 += " ";
                                                                        支払row.備考 += "合算値引：" + odShr_支払.NebikiAmount.ToString("C");
                                                                        // 備考(元の備考を置き換える場合)
                                                                        //支払row.備考 = "合算値引：" + odShr_支払.NebikiAmount.ToString("C");
                                                                    }
                                                                }
                                                                else if (odShr_支払.HachuNo != null && odShr_支払.HachuNo.Length > 0)
                                                                {
                                                                    // 備考(元の備考に追加する場合)
                                                                    if (支払row.備考.Length > 0)
                                                                        支払row.備考 += " ";
                                                                    支払row.備考 += "発注番号：" + odShr_支払.HachuNo + " と統合";
                                                                    // 備考(元の備考を置き換える場合)
                                                                    //row.備考 = "発注番号：" + odShr_支払.HachuNo + " と統合";

                                                                    // 合算した場合、以下項目は表示しない
                                                                    支払row.請求額 = decimal.MaxValue;
                                                                    支払row.概算計上額 = decimal.MinValue;
                                                                }

                                                                list_入渠管理表Row.Add(支払row);
                                                            }
                                                            #endregion
                                                        }
                                                    }
                                                    #endregion
                                                }
                                            }
                                            #endregion
                                        }
                                        #endregion
                                    }

                                    // ここから振替取立のデータ
                                    #region
                                    OdFurikaeToritateFilter odFkTtFilter = new OdFurikaeToritateFilter();
                                    odFkTtFilter.MsVesselID = msVessel.MsVesselID;
                                    odFkTtFilter.HachuDateFrom = new DateTime(FromYear, FromMonth, 1, 0, 0, 0);
                                    odFkTtFilter.HachuDateTo = new DateTime(ToYear, ToMonth, GetMonthLastDay(ToYear, ToMonth), 23, 59, 59);
                                    odFkTtFilter.MsThiIraiSbtID = NBaseCommon.Common.MsThiIraiSbt_修繕ID;
                                    List<OdFurikaeToritate> OdFurikaeToritates = null;
                                    OdFurikaeToritates = OdFurikaeToritate.GetRecordsByFilter(dbConnect, loginUser, odFkTtFilter);

                                    foreach (OdFurikaeToritate furikaeToritate in OdFurikaeToritates)
                                    {
                                        if (furikaeToritate.MsThiIraiShousaiID == NBaseCommon.Common.MsThiIraiShousai_小修理ID)
                                        {
                                            continue;
                                        }
                                        if (furikaeToritate.MsNyukyoKamokuID != msnKmk.MsNyukyoKamokuID)
                                        {
                                            continue;
                                        }
                                        入渠管理表Row row = new 入渠管理表Row();
                                        row.手配内容 = furikaeToritate.Koumoku;
                                        row.業者 = furikaeToritate.MsCustomerName;
                                        row.発注日 = furikaeToritate.HachuDate;
                                        row.発注者 = furikaeToritate.CreateUserName;
                                        //row.発注番号
                                        row.見積り額 = decimal.MinValue;
                                        row.完了日 = furikaeToritate.Kanryobi;
                                        row.落成入手日 = DateTime.MinValue;
                                        row.落成見積り額 = decimal.MinValue;
                                        row.請求書日 = furikaeToritate.Seikyushobi;
                                        row.請求額 = furikaeToritate.Amount;
                                        row.備考 = furikaeToritate.Bikou;
                                        row.起票日 = furikaeToritate.Kihyobi;
                                        row.計上月 = "";
                                        row.支払日 = DateTime.MinValue;
                                        row.概算計上額 = decimal.MinValue;

                                        list_入渠管理表Row.Add(row);

                                        //実績合計 += furikaeToritate.Amount; // 実績合計
                                        //shoukei += furikaeToritate.Amount;
                                    }
                                    #endregion

                                    #endregion

                                    #region 科目タイトル行出力
                                    {
                                        // 科目タイトル
                                        xls.Cell(col_内容, 0, offset_y).Value = msnKmk.NyukyoKamokuName;

                                        // 利益計画
                                        if (bgYosanHead != null)
                                        {
                                            // 2010.02.03 ORG
                                            //foreach (MsHimoku msHimoku in msHimokuList)
                                            //{
                                            //    if (msHimoku.MsKamokuID == msnKmk.MsKamokuID)
                                            //    {
                                            //        BgYosanItem bgYosanItem = BgYosanItem.GetRecordByYearHimokuIDMsVesselID(new MsUser(), bgYosanHead.YosanHeadID, FromYear, msHimoku.MsHimokuID, msVessel.MsVesselID);
                                            //        if (bgYosanItem != null)
                                            //        {
                                            //            xls.Cell(col_利益計画, 0, offset_y).Value = bgYosanItem.Amount;
                                            //            break;
                                            //        }
                                            //    }
                                            //}

                                            // 2010.02.03 川口君案
                                            //foreach (MsHimoku msHimoku in msHimokuList)
                                            //{
                                            //　　if (msHimoku.MsKamokus.Count == 1 && msHimoku.MsKamokus[0].MsKamokuId == msnKmk.MsKamokuID)
                                            //    {
                                            //        BgYosanItem bgYosanItem = BgYosanItem.GetRecordByYearHimokuIDMsVesselID(new MsUser(), bgYosanHead.YosanHeadID, FromYear, msHimoku.MsHimokuID, msVessel.MsVesselID);
                                            //        if (bgYosanItem != null)
                                            //        {
                                            //            xls.Cell(col_利益計画, 0, offset_y).Value = bgYosanItem.Amount;
                                            //            break;
                                            //        }
                                            //    }
                                            //}

                                            // 2010.02.03 暫定的にハードコーディングのコードで！
                                            int himokuID = -1;
                                            if (dic_himoku_kamoku.ContainsKey(msnKmk.MsKamokuID))
                                            {
                                                himokuID = dic_himoku_kamoku[msnKmk.MsKamokuID];
                                                BgYosanItem bgYosanItem = BgYosanItem.GetRecordByYearHimokuIDMsVesselID(dbConnect, new MsUser(), bgYosanHead.YosanHeadID, FromYear, himokuID, msVessel.MsVesselID);
                                                if (bgYosanItem != null)
                                                {
                                                    xls.Cell(col_利益計画, 0, offset_y).Value = bgYosanItem.Amount;
                                                }
                                            }
                                        }


                                        罫線_点線(xls, col_内容, col_概算計上, offset_y);
                                        offset_y++;
                                    }
                                    #endregion

                                    #region データ行出力
                                    //foreach ( 入渠管理表Row row in list_入渠管理表Row )

                                    // 2018.01 年度で検索
                                    ////var 入渠管理表Rows = from p in list_入渠管理表Row
                                    ////                where p.発注日 <= odThiFilter.ThiIraiDateTo
                                    ////                orderby p.発注日
                                    ////                select p;
                                    //var 入渠管理表Rows = from p in list_入渠管理表Row
                                    //                where p.発注日 <= odThiFilter.HachuDateTo
                                    //                   && p.発注日 >= odThiFilter.HachuDateFrom
                                    //                orderby p.発注日
                                    //                select p;
                                    var 入渠管理表Rows = from p in list_入渠管理表Row
                                                    where p.発注日 <= HachuDateTo
                                                    orderby p.発注日
                                                    select p;
                                    foreach (var row in 入渠管理表Rows)
                                    {
                                        // 手配内容
                                        xls.Cell(col_内容, 0, offset_y).Value = row.手配内容;
                                        // 業者
                                        xls.Cell(col_業者名, 0, offset_y).Value = row.業者;
                                        // 発注日
                                        xls.Cell(col_発注日, 0, offset_y).Value = row.発注日.ToString("yyyy/MM/dd");
                                        // 発注者
                                        xls.Cell(col_発注者, 0, offset_y).Value = row.発注者;
                                        // 発注番号
                                        xls.Cell(col_発注番号, 0, offset_y).Value = row.発注番号;
                                        // 見積り額
                                        if (row.見積り額 == decimal.MaxValue)
                                        {
                                            xls.Cell(col_見積額, 0, offset_y).Value = "'*****";
                                        }
                                        else if (row.見積り額 != decimal.MinValue)
                                        {
                                            xls.Cell(col_見積額, 0, offset_y).Value = row.見積り額;
                                        }
                                        // 完了日
                                        if (row.完了日 != DateTime.MinValue)
                                        {
                                            xls.Cell(col_完了日, 0, offset_y).Value = row.完了日.ToString("yyyy/MM/dd");
                                        }
                                        // 落成入手日
                                        if (row.落成入手日 != DateTime.MinValue)
                                        {
                                            xls.Cell(col_落成入手日, 0, offset_y).Value = row.落成入手日.ToString("yyyy/MM/dd");
                                        }
                                        // 落成見積り額
                                        if (row.落成見積り額 != decimal.MinValue)
                                        {
                                            xls.Cell(col_落成見積額, 0, offset_y).Value = row.落成見積り額;
                                        }
                                        // 請求書日
                                        if (row.請求書日 != DateTime.MinValue)
                                        {
                                            xls.Cell(col_請求書日, 0, offset_y).Value = row.請求書日.ToString("yyyy/MM/dd");
                                        }
                                        // 請求額
                                        if (row.請求額 == decimal.MaxValue)
                                        {
                                            xls.Cell(col_請求額, 0, offset_y).Value = "'*****";
                                        }
                                        else if (row.請求額 != decimal.MinValue)
                                        {
                                            xls.Cell(col_請求額, 0, offset_y).Value = row.請求額;

                                            実績合計 += row.請求額; // 実績合計
                                            shoukei += row.請求額;
                                        }
                                        // 備考
                                        xls.Cell(col_備考, 0, offset_y).Value = row.備考;
                                        // 起票日
                                        if (row.起票日 != DateTime.MinValue)
                                        {
                                            xls.Cell(col_起票日, 0, offset_y).Value = row.起票日.ToString("yyyy/MM/dd");
                                        }
                                        // 計上月
                                        if (row.計上月 != "")
                                        {
                                            xls.Cell(col_計上月, 0, offset_y).Value = row.計上月;
                                        }
                                        // 支払日
                                        if (row.支払日 != DateTime.MinValue)
                                        {
                                            xls.Cell(col_支払日, 0, offset_y).Value = row.支払日.ToString("yyyy/MM/dd");
                                        }
                                        // 概算計上額
                                        if (row.概算計上額 != decimal.MinValue)
                                        {
                                            xls.Cell(col_概算計上, 0, offset_y).Value = row.概算計上額;
                                        }

                                        罫線_点線(xls, col_内容, col_概算計上, offset_y);
                                        offset_y++;
                                    }
                                    #endregion

                                    #region 小計行出力
                                    {
                                        xls.Cell(col_請求額, 0, offset_y).Value = "小計";
                                        xls.Cell(col_備考, 0, offset_y).Value = shoukei;
                                        goukei += shoukei;
                                        shoukei = 0;

                                        #region 罫線
                                        xls.Cell(col_内容 + ":" + col_概算計上, 0, offset_y).Attr.LineBottom(ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
                                        #endregion

                                        offset_y++;
                                    }
                                    #endregion
                                }

                                #region 合計行出力
                                {
                                    offset_y++;

                                    xls.Cell(col_内容, 0, offset_y).Value = "合計";
                                    xls.Cell(col_備考, 0, offset_y).Value = goukei;

                                    xls.Cell(col_利益計画, 0, offset_y).Value = "=SUM(" + col_利益計画 + ":" + "C" + (6 + offset_y).ToString() + ")";

                                    #region 罫線
                                    xls.Cell(col_内容 + ":" + col_概算計上, 0, offset_y).Attr.LineBottom(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
                                    #endregion

                                    offset_y++;
                                }
                                #endregion

                                #region ヘッダー
                                {
                                    // 計上月
                                    xls.Cell("P3").Value = ToMonth.ToString() + "月計上額";
                                    // 計上月
                                    xls.Cell("P4").Value = ToMonth.ToString() + "月概算計上";

                                    // 実績
                                    xls.Cell(cell_実績).Value = goukei;

                                    // 計上額
                                    xls.Cell(cell_月計上額).Value = keijoGaku;

                                    // 概算計上額
                                    xls.Cell(cell_概算計上額).Value = "=SUM(" + col_概算計上 + ":" + column_概算計上 + (6 + offset_y).ToString() + ")";

                                    // 修繕費予算
                                    xls.Cell(cell_修繕費予算).Value = "=SUM(" + col_利益計画 + ":" + column_利益計画 + (6 + offset_y).ToString() + ")";

                                    // 総合予算合計
                                    if (bgYosanHead != null)
                                    {
                                        // 2010.03.11:aki [W090268] 
                                        //BgYosanItem bgYosanItem = BgYosanItem.GetRecordByYearHimokuIDMsVesselID(new MsUser(), bgYosanHead.YosanHeadID, FromYear, MS_HIMOKU_船舶修繕費_ID, msVessel.MsVesselID);
                                        //if (bgYosanItem != null)
                                        //{
                                        //    xls.Cell(cell_総合予算合計).Value = bgYosanItem.Amount;
                                        //}
                                        BgYosanItem bgYosanItem = BgYosanItem.GetRecordByYearHimokuIDMsVesselID(dbConnect, new MsUser(), bgYosanHead.YosanHeadID, FromYear, MS_HIMOKU_ランニング費用_ID, msVessel.MsVesselID);
                                        if (bgYosanItem != null)
                                        {
                                            xls.Cell(cell_総合予算合計).Value = "=SUM(" + col_利益計画 + ":" + column_利益計画 + (6 + offset_y).ToString() + ") + " + bgYosanItem.Amount.ToString();
                                        }
                                    }

                                    // 総合実績合計
                                    ////ランニング実績合計 = ランニング実績(loginUser, odThiFilter);
                                    //ランニング実績合計 = ランニング実績(dbConnect, loginUser, odThiFilter, 月次range);
                                    ランニング実績合計 = ランニング実績(dbConnect, loginUser, odThiFilter, HachuDateTo, 月次range);
                                    xls.Cell(cell_総合実績合計).Value = 実績合計 + ランニング実績合計;


                                    // 検査名
                                    //---------------------------------------------------------
                                    //変更20150907
                                    #region コメントアウト
                                    //
                                    //if (lastOdThi == null)
                                    //{
                                    //    xls.Cell(cell_検査名).Value = "";
                                    //}
                                    //else
                                    //{
                                    //    if (lastOdThi.MsThiIraiShousaiID == NBaseCommon.Common.MsThiIraiShousai_入渠_年次ID)
                                    //    {
                                    //        xls.Cell(cell_検査名).Value = "年次検査";
                                    //    }
                                    //    else if (lastOdThi.MsThiIraiShousaiID == NBaseCommon.Common.MsThiIraiShousai_入渠_中間ID)
                                    //    {
                                    //        xls.Cell(cell_検査名).Value = "中間検査";
                                    //    }
                                    //    else if (lastOdThi.MsThiIraiShousaiID == NBaseCommon.Common.MsThiIraiShousai_入渠_定期ID)
                                    //    {
                                    //        xls.Cell(cell_検査名).Value = "定期検査";
                                    //    }
                                    //    else
                                    //    {
                                    //        xls.Cell(cell_検査名).Value = "";
                                    //    }
                                    //}
                                    #endregion

                                    if (bgKadouVesselList != null)
                                    {
                                        var tmp = bgKadouVesselList.Where(obj => (obj.MsVesselID == msVessel.MsVesselID));
                                        if (tmp.Count() > 0)
                                        {
                                            BgKadouVessel bgKV = tmp.First();
                                            if (bgKV.NyukyoKind == "AS" || bgKV.NyukyoKind == "AS/AF" || bgKV.NyukyoKind == "AS/DS")
                                            {
                                                KensaName = "年次検査";
                                            }
                                            else if (bgKV.NyukyoKind == "SS")
                                            {
                                                KensaName = "定期検査";
                                            }
                                            else if (bgKV.NyukyoKind == "IS")
                                            {
                                                KensaName = "中間検査";
                                            }
                                        }
                                    }

                                    xls.Cell(cell_検査名).Value = KensaName;
                                    //---------------------------------------------------------
                                }
                                #endregion

                                #region セルの色設定/縦の罫線

                                int endRow = startRow + offset_y - 1;

                                #region 縦線
                                xls.Cell(column_内容 + startRow.ToString() + ":" + column_内容 + endRow.ToString()).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell(column_内容 + startRow.ToString() + ":" + column_内容 + endRow.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell(column_業者名 + startRow.ToString() + ":" + column_業者名 + endRow.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell(column_利益計画 + startRow.ToString() + ":" + column_利益計画 + endRow.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell(column_発注日 + startRow.ToString() + ":" + column_発注日 + endRow.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell(column_発注者 + startRow.ToString() + ":" + column_発注者 + endRow.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell(column_発注番号 + startRow.ToString() + ":" + column_発注番号 + endRow.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell(column_見積額 + startRow.ToString() + ":" + column_見積額 + endRow.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell(column_完了日 + startRow.ToString() + ":" + column_完了日 + endRow.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell(column_落成入手日 + startRow.ToString() + ":" + column_落成入手日 + endRow.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell(column_落成見積額 + startRow.ToString() + ":" + column_落成見積額 + endRow.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell(column_請求書日 + startRow.ToString() + ":" + column_請求書日 + endRow.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell(column_請求額 + startRow.ToString() + ":" + column_請求額 + endRow.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell(column_備考 + startRow.ToString() + ":" + column_備考 + endRow.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell(column_起票日 + startRow.ToString() + ":" + column_起票日 + endRow.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell(column_計上月 + startRow.ToString() + ":" + column_計上月 + endRow.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell(column_支払日 + startRow.ToString() + ":" + column_支払日 + endRow.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                                xls.Cell(column_概算計上 + startRow.ToString() + ":" + column_概算計上 + endRow.ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black); ;
                                #endregion

                                #region 色

                                // 2021/09/03 m.yoshihara 薄黄色? xlColor yellow = (xlColor)43;
                                System.Drawing.Color yellow = System.Drawing.Color.FromArgb(255, 255, 153);

                                xls.Cell(column_業者名 + startRow.ToString() + ":" + column_業者名 + endRow.ToString()).Attr.BackColor
                                    = yellow;
                                xls.Cell(column_発注日 + startRow.ToString() + ":" + column_発注日 + endRow.ToString()).Attr.BackColor
                                    = yellow;
                                xls.Cell(column_発注者 + startRow.ToString() + ":" + column_発注者 + endRow.ToString()).Attr.BackColor
                                    = yellow;
                                xls.Cell(column_発注番号 + startRow.ToString() + ":" + column_発注番号 + endRow.ToString()).Attr.BackColor
                                    = yellow;
                                xls.Cell(column_見積額 + startRow.ToString() + ":" + column_見積額 + endRow.ToString()).Attr.BackColor
                                    = yellow;

                                // 2021/09/03 m.yoshihara コーラル？　xlColor gray = (xlColor)22;
                                System.Drawing.Color gray = System.Drawing.Color.FromArgb(255,128,128);
                                xls.Cell(column_完了日 + startRow.ToString() + ":" + column_完了日 + endRow.ToString()).Attr.BackColor
                                    = gray;

                                // 2021/09/03 m.yoshihara ミントブルー？  xlColor green = (xlColor)42;
                                System.Drawing.Color green = System.Drawing.Color.FromArgb(51, 204, 204);

                                xls.Cell(column_落成入手日 + startRow.ToString() + ":" + column_落成入手日 + endRow.ToString()).Attr.BackColor
                                    = green;
                                xls.Cell(column_落成見積額 + startRow.ToString() + ":" + column_落成見積額 + endRow.ToString()).Attr.BackColor
                                    = green;

                                //2021/09/03 m.yoshihara ブルーグレー？ xlColor orange = (xlColor)47;

                                System.Drawing.Color orange = System.Drawing.Color.FromArgb(102, 102, 153);
                                xls.Cell(column_請求書日 + startRow.ToString() + ":" + column_請求書日 + endRow.ToString()).Attr.BackColor
                                    = orange;
                                xls.Cell(column_請求額 + startRow.ToString() + ":" + column_請求額 + endRow.ToString()).Attr.BackColor
                                    = orange;

                                //2021/09/03 m.yoshihara オレンジに近い黄色？  xlColor blue = (xlColor)44;
                                System.Drawing.Color blue = System.Drawing.Color.FromArgb(255, 204, 0);
                                xls.Cell(column_起票日 + startRow.ToString() + ":" + column_起票日 + endRow.ToString()).Attr.BackColor
                                    = blue;
                                xls.Cell(column_計上月 + startRow.ToString() + ":" + column_計上月 + endRow.ToString()).Attr.BackColor
                                    = blue;
                                xls.Cell(column_支払日 + startRow.ToString() + ":" + column_支払日 + endRow.ToString()).Attr.BackColor
                                    = blue;

                                #endregion

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
                NBaseCommon.LogFile.Write(loginUser.FullName, "入渠管理表：" + ex.Message);
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

        private void 罫線_点線(ExcelCreator.Xlsx.XlsxCreator xls, string col_A, string col_B, int offset_y)
        {
            xls.Cell(col_A + ":" + col_B, 0, offset_y).Attr.LineBottom(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
        }



        ////private decimal ランニング実績(NBaseData.DAC.MsUser loginUser, OdThiFilter odThiFilter)
        //private decimal ランニング実績(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, OdThiFilter odThiFilter, Dictionary<int, 船員月次> 月次range)
        private decimal ランニング実績(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, OdThiFilter odThiFilter, DateTime HachuDateTo, Dictionary<int, 船員月次> 月次range)
        {
            decimal ランニング実績合計 = 0;

            List<OdThi> list_odThi = OdThi.GetRecordsByFilter(dbConnect, loginUser, odThiFilter);
            foreach (OdThi odThi in list_odThi)
            {
                // 出力対象のレコードか判定する
                if (!ランニング管理表出力対象判定(odThi))
                {
                    continue;
                }
                // [OD_MM]見積依頼
                List<OdMm> list_odMm = OdMm.GetRecordsByOdThiID(dbConnect, loginUser, odThi.OdThiID);
                if (list_odMm.Count == 0)
                {
                    continue;
                }
                foreach (OdMm odMm in list_odMm)
                {
                    // [OD_MK]見積回答
                    List<OdMk> list_odMk = OdMk.GetRecordsByOdMmID(dbConnect, loginUser, odMm.OdMmID);
                    if (list_odMk.Count == 0)
                    {
                        continue;
                    }
                    foreach (OdMk odMk in list_odMk)
                    {
                        if (odMk.Status != 4)
                        {
                            continue;
                        }

                        // 2012.12 : ランニング管理表と合わせるために以下４行を追加
                        //if (odMk.HachuDate < odThiFilter.HachuDateFrom || odMk.HachuDate > odThiFilter.HachuDateTo)
                        if (odMk.HachuDate > HachuDateTo)
                        {
                            continue;
                        }

                        // [OD_JRY]受領
                        List<OdJry> list_odJry = OdJry.GetRecordsByOdMkId(dbConnect, loginUser, odMk.OdMkID);
                        if (list_odJry.Count == 0)
                        {
                            continue;
                        }
                        foreach (OdJry odJry in list_odJry)
                        {
                            // [OD_SHR]支払
                            List<OdShr> list_odShr = OdShr.GetRecordByOdJryID(dbConnect, loginUser, odJry.OdJryID);
                            if (list_odShr.Count == 0)
                            {
                                continue;
                            }
                            // 決定価格を取得
                            foreach (OdShr odShr in list_odShr)
                            {
                                if (odShr.Sbt == (int)OdShr.SBT.支払)
                                {
                                    // 標準化モジュールは基幹連携がないので、以下コメント
                                    //// 2012.12 : ランニング管理表と合わせるために以下１２行を追加
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
                                    //<-- ここまで追加

                                    ランニング実績合計 += odShr.Amount;
                                    if (odShr.NebikiAmount > 0)
                                    {
                                        ランニング実績合計 -= odShr.NebikiAmount;
                                    }
                                    // 2012.09.23 Add
                                    if (odShr.Carriage > 0)
                                    {
                                        ランニング実績合計 += odShr.Carriage;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            // 2012.12 : ランニング管理表と合わせるために以下 "振替取立のデータ"、"準備金のデータ"を追加
            //return ランニング実績合計;

            // ここから振替取立のデータ
            #region
            OdFurikaeToritateFilter odFkTtFilter = new OdFurikaeToritateFilter();
            odFkTtFilter.MsVesselID = odThiFilter.MsVesselID;
            odFkTtFilter.HachuDateFrom = odThiFilter.HachuDateFrom;
            odFkTtFilter.HachuDateTo = odThiFilter.HachuDateTo;
            odFkTtFilter.MsThiIraiSbtID = NBaseCommon.Common.MsThiIraiSbt_修繕ID;
            odFkTtFilter.MsThiIraiShousaiID = NBaseCommon.Common.MsThiIraiShousai_小修理ID;
            List<OdFurikaeToritate> OdFurikaeToritates = null;
            OdFurikaeToritates = OdFurikaeToritate.GetRecordsByFilter(dbConnect, loginUser, odFkTtFilter);

            foreach (OdFurikaeToritate furikaeToritate in OdFurikaeToritates)
            {
                ランニング実績合計 += furikaeToritate.Amount;
            }
            #endregion

            // ここから準備金のデータ
            #region

            DateTime fromDate = new DateTime(int.Parse(odThiFilter.Nendo), NBaseCommon.Common.FiscalYearStartMonth, 1);

            List<SiGetsujiShime> SiGetsujiShimes = null;
            //SiGetsujiShimes = SiGetsujiShime.GetRecords(dbConnect, loginUser, odThiFilter.HachuDateFrom);
            SiGetsujiShimes = SiGetsujiShime.GetRecords(dbConnect, loginUser, fromDate);
            List<SiJunbikin> SiJunbikins = null;
            //SiJunbikins = SiJunbikin.Get_振替_修繕費(dbConnect, loginUser, odThiFilter.HachuDateFrom, odThiFilter.MsVesselID);
            SiJunbikins = SiJunbikin.Get_振替_修繕費(dbConnect, loginUser, fromDate, odThiFilter.MsVesselID);
            decimal[] 船用金修繕費 = new decimal[12];
            for (int i = 0; i < 12; i++)
            {
                船用金修繕費[i] = 0;
            }
            foreach (SiJunbikin jk in SiJunbikins)
            {
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
                        ランニング実績合計 += 船用金修繕費[i];
                    }
                }
            }
            #endregion


            return ランニング実績合計;
        }
    }

    public class 入渠管理表Row
    {
        public 入渠管理表Row()
        {
            完了日 = DateTime.MinValue;
            請求書日 = DateTime.MinValue;
            起票日 = DateTime.MinValue;
            支払日 = DateTime.MinValue;
            落成入手日 = DateTime.MinValue;
            落成見積り額 = decimal.MinValue;
            請求額 = decimal.MinValue;
            概算計上額 = decimal.MinValue;
            計上月 = "";
        }

        public string 手配内容 { get; set; }
        public string 業者 { get; set; }
        public DateTime 発注日 { get; set; }
        public string 発注者 { get; set; }
        public string 発注番号 { get; set; }
        public decimal 見積り額 { get; set; }
        public DateTime 完了日 { get; set; }
        public DateTime 落成入手日 { get; set; }
        public decimal 落成見積り額 { get; set; }
        public DateTime 請求書日 { get; set; }
        public decimal 請求額 { get; set; }
        public string 備考 { get; set; }
        public DateTime 起票日 { get; set; }
        public string 計上月 { get; set; }
        public decimal 概算計上額 { get; set; }
        public DateTime 支払日 { get; set; }


        public 入渠管理表Row Clone()
        {
            入渠管理表Row clone = new 入渠管理表Row();

            clone.手配内容 　　= 手配内容;
            clone.業者 　　　　= 業者;
            clone.発注日 　　　= 発注日;
            clone.発注者 　　　= 発注者;
            clone.発注番号 　　= 発注番号;
            clone.見積り額 　　= 見積り額;
            clone.完了日 　　　= 完了日;
            clone.落成入手日 　= 落成入手日;
            clone.落成見積り額 = 落成見積り額;
            clone.請求書日 　　= 請求書日;
            clone.請求額 　　　= 請求額;
            clone.備考 　　　　= 備考;
            clone.起票日 　　　= 起票日;
            clone.計上月 　　　= 計上月;
            clone.概算計上額 　= 概算計上額;
            clone.支払日 　　　= 支払日;

            return clone;
        }
    }

}
