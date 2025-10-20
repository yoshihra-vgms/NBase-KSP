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
using ExcelCreator=AdvanceSoftware.ExcelCreator;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_発注RowDataヘッダー_取得(NBaseData.DAC.MsUser loginUser, NBaseData.BLC.発注RowData検索条件 検索条件);
        
        [OperationContract]
        byte[] BLC_発注RowData詳細品目_取得(NBaseData.DAC.MsUser loginUser, NBaseData.BLC.発注RowData検索条件 検索条件h);
    }

    public partial class Service
    {
        public byte[] BLC_発注RowDataヘッダー_取得(NBaseData.DAC.MsUser loginUser, NBaseData.BLC.発注RowData検索条件 検索条件)
        {
            #region 元になるファイルの確認と出力ファイル生成
            string BaseFileName = "発注RowData";
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

            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                if (BaseFileName != null && BaseFileName.Length != 0)
                {
                    // 指定されたテンプレートを元にファイルを作成
                    xls.OpenBook(outPutFileName, templateName);
                }

                int offset_y = 0;
                int offset_x = 0;

                // ヘッダー行出力
                #region
                xls.Cell("A1", offset_x++, offset_y).Value = "No";
                xls.Cell("A1", offset_x++, offset_y).Value = "状況";
                xls.Cell("A1", offset_x++, offset_y).Value = "手配依頼種別";
                xls.Cell("A1", offset_x++, offset_y).Value = "手配依頼詳細種別";
                xls.Cell("A1", offset_x++, offset_y).Value = "手配依頼番号";
                xls.Cell("A1", offset_x++, offset_y).Value = "船";
                xls.Cell("A1", offset_x++, offset_y).Value = "場所";
                xls.Cell("A1", offset_x++, offset_y).Value = "手配依頼者";
                xls.Cell("A1", offset_x++, offset_y).Value = "事務担当者";
                xls.Cell("A1", offset_x++, offset_y).Value = "手配内容";
                xls.Cell("A1", offset_x++, offset_y).Value = "備考";
                xls.Cell("A1", offset_x++, offset_y).Value = "見積番号";
                xls.Cell("A1", offset_x++, offset_y).Value = "見積依頼先";
                xls.Cell("A1", offset_x++, offset_y).Value = "見積依頼先_担当者";
                xls.Cell("A1", offset_x++, offset_y).Value = "メール送信状況";
                xls.Cell("A1", offset_x++, offset_y).Value = "送信日時";
                xls.Cell("A1", offset_x++, offset_y).Value = "見積回答期限";
                xls.Cell("A1", offset_x++, offset_y).Value = "支払条件";
                xls.Cell("A1", offset_x++, offset_y).Value = "入渠科目";
                xls.Cell("A1", offset_x++, offset_y).Value = "送り先";
                xls.Cell("A1", offset_x++, offset_y).Value = "内容";
                xls.Cell("A1", offset_x++, offset_y).Value = "見積回答日";
                xls.Cell("A1", offset_x++, offset_y).Value = "見積有効期限";
                xls.Cell("A1", offset_x++, offset_y).Value = "納期";
                xls.Cell("A1", offset_x++, offset_y).Value = "工期";
                xls.Cell("A1", offset_x++, offset_y).Value = "見積_金額";
                xls.Cell("A1", offset_x++, offset_y).Value = "見積_消費税";
                xls.Cell("A1", offset_x++, offset_y).Value = "見積_送料・運搬料";
                xls.Cell("A1", offset_x++, offset_y).Value = "見積_値引";
                xls.Cell("A1", offset_x++, offset_y).Value = "見積_合計金額（税抜）";
                xls.Cell("A1", offset_x++, offset_y).Value = "見積_合計金額（税込）";
                xls.Cell("A1", offset_x++, offset_y).Value = "発注番号";
                xls.Cell("A1", offset_x++, offset_y).Value = "受領番号";
                xls.Cell("A1", offset_x++, offset_y).Value = "受領日";
                xls.Cell("A1", offset_x++, offset_y).Value = "納品額";
                xls.Cell("A1", offset_x++, offset_y).Value = "納品_消費税";
                xls.Cell("A1", offset_x++, offset_y).Value = "納品_送料・運搬料";
                xls.Cell("A1", offset_x++, offset_y).Value = "納品_値引";
                xls.Cell("A1", offset_x++, offset_y).Value = "納品_合計金額（税抜）";
                xls.Cell("A1", offset_x++, offset_y).Value = "納品_合計金額（税込）";
                xls.Cell("A1", offset_x++, offset_y).Value = "支払番号";
                xls.Cell("A1", offset_x++, offset_y).Value = "摘要";
                xls.Cell("A1", offset_x++, offset_y).Value = "申請担当者";
                xls.Cell("A1", offset_x++, offset_y).Value = "請求書日";
                xls.Cell("A1", offset_x++, offset_y).Value = "起票日";
                xls.Cell("A1", offset_x++, offset_y).Value = "計上日";
                xls.Cell("A1", offset_x++, offset_y).Value = "支払日";
                xls.Cell("A1", offset_x++, offset_y).Value = "基幹システム支払番号";
                xls.Cell("A1", offset_x++, offset_y).Value = "支払額";
                xls.Cell("A1", offset_x++, offset_y).Value = "支払_消費税";
                xls.Cell("A1", offset_x++, offset_y).Value = "支払_送料・運搬料";
                xls.Cell("A1", offset_x++, offset_y).Value = "請求値引";
                xls.Cell("A1", offset_x++, offset_y).Value = "支払_合計金額（税抜）";
                xls.Cell("A1", offset_x++, offset_y).Value = "支払_合計金額（税込）";
                xls.Cell("A1", offset_x++, offset_y).Value = "計上額月";
                xls.Cell("A1", offset_x++, offset_y).Value = "概算計上額";
                #endregion
                offset_y++;


                // データ行出力
                List<NBaseData.BLC.発注RowData> 発注RowDatas = NBaseData.BLC.発注RowData.GetRecordsヘッダー(loginUser, 検索条件);
                foreach (NBaseData.BLC.発注RowData rowData in 発注RowDatas)
                {
                    int 残りカラム数 = 0;
                    string 計上月 = "";
                    decimal 概算計上額 = decimal.MinValue;
                    offset_x = 0;

                    xls.Cell("A1", offset_x++, offset_y).Value = offset_y.ToString();
                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.状況;
                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.手配依頼種別;
                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.手配依頼詳細種別;
                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.手配依頼番号;
                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.船;
                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.場所;
                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.手配依頼者;
                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.事務担当者;
                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.手配内容;
                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.備考;
                    if (rowData.見積番号 != null && rowData.見積番号.Length > 0)
                    {
                        if (rowData.見積番号.Substring(0, 7) != "Enabled")
                        {
                            xls.Cell("A1", offset_x++, offset_y).Value = rowData.見積番号;
                        }
                        else
                        {
                            offset_x++;
                        }
                        if (rowData.見積回答番号 != null && rowData.見積回答番号.Length > 0)
                        {
                            xls.Cell("A1", offset_x++, offset_y).Value = rowData.見積依頼先;
                            xls.Cell("A1", offset_x++, offset_y).Value = rowData.見積依頼先_担当者;
                            xls.Cell("A1", offset_x++, offset_y).Value = rowData.メール送信状況;
                            xls.Cell("A1", offset_x++, offset_y).Value = Convert日付(rowData.送信日時);
                            xls.Cell("A1", offset_x++, offset_y).Value = rowData.見積回答期限;
                            xls.Cell("A1", offset_x++, offset_y).Value = rowData.支払条件;
                            xls.Cell("A1", offset_x++, offset_y).Value = rowData.入渠科目;
                            xls.Cell("A1", offset_x++, offset_y).Value = rowData.送り先;
                            xls.Cell("A1", offset_x++, offset_y).Value = rowData.内容;
                            xls.Cell("A1", offset_x++, offset_y).Value = Convert日付(rowData.見積回答日);
                            xls.Cell("A1", offset_x++, offset_y).Value = rowData.見積有効期限;
                            xls.Cell("A1", offset_x++, offset_y).Value = Convert日付(rowData.納期);
                            xls.Cell("A1", offset_x++, offset_y).Value = rowData.工期;
                            xls.Cell("A1", offset_x++, offset_y).Value = rowData.見積_金額;
                            xls.Cell("A1", offset_x++, offset_y).Value = rowData.見積_消費税;
                            xls.Cell("A1", offset_x++, offset_y).Value = rowData.見積_送料運搬料;
                            xls.Cell("A1", offset_x++, offset_y).Value = rowData.見積_値引;
                            xls.Cell("A1", offset_x++, offset_y).Value = rowData.見積_合計金額;
                            xls.Cell("A1", offset_x++, offset_y).Value = rowData.見積_合計金額 + rowData.見積_消費税;
                            if (rowData.発注番号.Length > 1) // 発注していなくても"0"が入っているので。。。
                            {
                                xls.Cell("A1", offset_x++, offset_y).Value = rowData.発注番号;
                            }
                            else
                            {
                                offset_x++;
                            }
                            if (rowData.受領番号 != null && rowData.受領番号.Length > 0)
                            {
                                xls.Cell("A1", offset_x++, offset_y).Value = rowData.受領番号;
                                xls.Cell("A1", offset_x++, offset_y).Value = Convert日付(rowData.受領日);
                                xls.Cell("A1", offset_x++, offset_y).Value = rowData.納品額;
                                xls.Cell("A1", offset_x++, offset_y).Value = rowData.納品_消費税;
                                xls.Cell("A1", offset_x++, offset_y).Value = rowData.納品_送料運搬料;
                                xls.Cell("A1", offset_x++, offset_y).Value = rowData.納品_値引;
                                xls.Cell("A1", offset_x++, offset_y).Value = rowData.納品_合計金額;
                                xls.Cell("A1", offset_x++, offset_y).Value = rowData.納品_合計金額 + rowData.納品_消費税;
                                if (rowData.支払番号 != null && rowData.支払番号.Length > 0)
                                {
                                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.支払番号;
                                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.摘要;
                                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.申請担当者;
                                    xls.Cell("A1", offset_x++, offset_y).Value = Convert日付(rowData.請求書日);
                                    xls.Cell("A1", offset_x++, offset_y).Value = Convert日付(rowData.起票日);
                                    xls.Cell("A1", offset_x++, offset_y).Value = Convert日付(rowData.計上日);
                                    xls.Cell("A1", offset_x++, offset_y).Value = Convert日付(rowData.支払日);
                                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.基幹システム支払番号;
                                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.支払額;
                                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.支払_消費税;
                                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.支払_送料運搬料;
                                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.請求値引;
                                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.支払_合計金額;
                                    xls.Cell("A1", offset_x++, offset_y).Value = rowData.支払_合計金額 + rowData.支払_消費税;
                                }
                                else
                                {
                                    残りカラム数 = 14; // 12;
                                }
                                if (rowData.受領ステータス == (int)OdJry.STATUS.受領承認済み)
                                {
                                    概算計上額 = rowData.納品_合計金額;
                                }
                                if (rowData.落成_合計金額 > 0)
                                {
                                    概算計上額 = rowData.落成_合計金額;
                                }
                                if (rowData.支払_合計金額 > 0)
                                {
                                    概算計上額 = rowData.支払_合計金額;
                                }
                                if (rowData.支払ステータス == (int)OdShr.STATUS.支払済)
                                {
                                    if (rowData.計上日 != DateTime.MinValue)
                                    {
                                        計上月 = rowData.計上日.Month.ToString() + "月";
                                    }
                                    概算計上額 = decimal.MinValue;
                                }
                            }
                            else
                            {
                                残りカラム数 = 24; // 20;
                            }
                        }
                        else
                        {
                            残りカラム数 = 44; // 38;
                        }
                    }
                    else
                    {
                        残りカラム数 = 45; // 39;
                    }
                    if (残りカラム数 > 0)
                    {
                        for (int i = 0; i < 残りカラム数; i++)
                        {
                            xls.Cell("A1", offset_x++, offset_y).Value = "";
                        }
                    }
                    if (rowData.受領ステータス == (int)OdJry.STATUS.受領承認済み)
                    {
                        xls.Cell("A1", offset_x++, offset_y).Value = 計上月;
                        if (概算計上額 > 0)
                        {
                            xls.Cell("A1", offset_x++, offset_y).Value = 概算計上額;
                        }
                        else
                        {
                            xls.Cell("A1", offset_x++, offset_y).Value = "";
                        }
                    }

                    offset_y++;
                }

                // 見積金額フォーマット
                //xls.Cell("Z2:AC" + offset_x.ToString()).Attr.Format = "\\\\#,##0;[赤]\"▲\"\\\\#,##0";
                xls.Cell("Z2:AE" + offset_x.ToString()).Attr.Format = "\\\\#,##0;[赤]\"▲\"\\\\#,##0";
                // 納品金額フォーマット
                //xls.Cell("AG2:AJ" + offset_x.ToString()).Attr.Format = "\\\\#,##0;[赤]\"▲\"\\\\#,##0";
                xls.Cell("AI2:AN" + offset_x.ToString()).Attr.Format = "\\\\#,##0;[赤]\"▲\"\\\\#,##0";
                // 支払金額フォーマット
                //xls.Cell("AS2:AV" + offset_x.ToString()).Attr.Format = "\\\\#,##0;[赤]\"▲\"\\\\#,##0";
                xls.Cell("AW2:BB" + offset_x.ToString()).Attr.Format = "\\\\#,##0;[赤]\"▲\"\\\\#,##0";
                //概算金額
                xls.Cell("BD2:BD" + offset_x.ToString()).Attr.Format = "\\\\#,##0;[赤]\"▲\"\\\\#,##0";


                xls.CloseBook(true);
                if (xls.ErrorNo == ExcelCreator.ErrorNo.NoError)
                {
                    //res = true;
                }
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

        public byte[] BLC_発注RowData詳細品目_取得(NBaseData.DAC.MsUser loginUser, NBaseData.BLC.発注RowData検索条件 検索条件)
        {
            #region 元になるファイルの確認と出力ファイル生成
            string BaseFileName = "発注RowData";
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

            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                if (BaseFileName != null && BaseFileName.Length != 0)
                {
                    // 指定されたテンプレートを元にファイルを作成
                    xls.OpenBook(outPutFileName, templateName);
                }

                int offset_y = 0;
                int offset_x = 0;

                // ヘッダー行出力
                #region
                xls.Cell("A1", offset_x++, offset_y).Value = "No";
                xls.Cell("A1", offset_x++, offset_y).Value = "状況";
                xls.Cell("A1", offset_x++, offset_y).Value = "手配依頼種別";
                xls.Cell("A1", offset_x++, offset_y).Value = "手配依頼詳細種別";
                xls.Cell("A1", offset_x++, offset_y).Value = "手配依頼番号";
                xls.Cell("A1", offset_x++, offset_y).Value = "船";
                xls.Cell("A1", offset_x++, offset_y).Value = "場所";
                xls.Cell("A1", offset_x++, offset_y).Value = "手配依頼者";
                xls.Cell("A1", offset_x++, offset_y).Value = "事務担当者";
                xls.Cell("A1", offset_x++, offset_y).Value = "手配内容";
                xls.Cell("A1", offset_x++, offset_y).Value = "備考";
                xls.Cell("A1", offset_x++, offset_y).Value = "見積番号";
                xls.Cell("A1", offset_x++, offset_y).Value = "見積依頼先";
                xls.Cell("A1", offset_x++, offset_y).Value = "見積依頼先_担当者";
                xls.Cell("A1", offset_x++, offset_y).Value = "メール送信状況";
                xls.Cell("A1", offset_x++, offset_y).Value = "送信日時";

                xls.Cell("A1", offset_x++, offset_y).Value = "部署名";
                xls.Cell("A1", offset_x++, offset_y).Value = "区分";
                xls.Cell("A1", offset_x++, offset_y).Value = "仕様・型式";
                xls.Cell("A1", offset_x++, offset_y).Value = "詳細品目";
                xls.Cell("A1", offset_x++, offset_y).Value = "備考（品目、規格等）";
                xls.Cell("A1", offset_x++, offset_y).Value = "単位";

                xls.Cell("A1", offset_x++, offset_y).Value = "在庫数";
                xls.Cell("A1", offset_x++, offset_y).Value = "依頼数";

                xls.Cell("A1", offset_x++, offset_y).Value = "見積数";

                xls.Cell("A1", offset_x++, offset_y).Value = "回答数";
                xls.Cell("A1", offset_x++, offset_y).Value = "単価";
                xls.Cell("A1", offset_x++, offset_y).Value = "見積金額";
                xls.Cell("A1", offset_x++, offset_y).Value = "発注日";

                xls.Cell("A1", offset_x++, offset_y).Value = "受領日";
                xls.Cell("A1", offset_x++, offset_y).Value = "受領数";
                xls.Cell("A1", offset_x++, offset_y).Value = "受領金額";

                xls.Cell("A1", offset_x++, offset_y).Value = "支払数量";
                xls.Cell("A1", offset_x++, offset_y).Value = "支払金額";
                #endregion
                offset_y++;

                // データ行出力
                List<NBaseData.BLC.発注RowData> 発注RowDatas = NBaseData.BLC.発注RowData.GetRecords詳細品目(loginUser, 検索条件);
                foreach (NBaseData.BLC.発注RowData rowData in 発注RowDatas)
                {
                    // 出力する詳細データ
                    Dictionary<NBaseData.BLC.発注RowData詳細Base, NBaseData.BLC.発注RowData詳細> 発注RowData詳細s = new Dictionary<NBaseData.BLC.発注RowData詳細Base, NBaseData.BLC.発注RowData詳細>();

                    if (rowData.OD_MK_ID.Length > 0)
                    {
                        //========================================================
                        // 見積回答がある場合、見積回答から詳細品目をリストアップする
                        //========================================================

                        // 回答詳細を出力する詳細データにセットする
                        #region
                        List<NBaseData.BLC.発注RowData回答詳細> 回答詳細s = NBaseData.BLC.発注RowData回答詳細.GetRecords回答詳細(loginUser, rowData.OD_MK_ID);
                        foreach (NBaseData.BLC.発注RowData回答詳細 回答詳細 in 回答詳細s)
                        {
                            NBaseData.BLC.発注RowData詳細 詳細 = new NBaseData.BLC.発注RowData詳細();
                            詳細.Set回答詳細(回答詳細);

                            if (発注RowData詳細s.ContainsKey(詳細))
                            {
                                発注RowData詳細s[詳細].MkCount = 詳細.MkCount;
                                発注RowData詳細s[詳細].MkTanka = 詳細.MkTanka;
                                発注RowData詳細s[詳細].MkAmount = 詳細.MkAmount;
                            }
                            else
                            {
                                発注RowData詳細s.Add(詳細, 詳細);
                            }
                        }
                        #endregion
                        // 手配詳細を出力する詳細データにセットする
                        #region
                        List<NBaseData.BLC.発注RowData手配詳細> 手配詳細s = NBaseData.BLC.発注RowData手配詳細.GetRecords手配詳細(loginUser, rowData.OD_THI_ID);
                        foreach (NBaseData.BLC.発注RowData手配詳細 手配詳細 in 手配詳細s)
                        {
                            NBaseData.BLC.発注RowData詳細 詳細 = new NBaseData.BLC.発注RowData詳細();
                            詳細.Set手配詳細(手配詳細);

                            if (発注RowData詳細s.ContainsKey(詳細))
                            {
                                発注RowData詳細s[詳細].ZaikoCount = 詳細.ZaikoCount;
                                発注RowData詳細s[詳細].ThiCount = 詳細.ThiCount;
                            }
                        }
                        #endregion
                        // 見積詳細を出力する詳細データにセットする
                        #region
                        List<NBaseData.BLC.発注RowData見積詳細> 見積詳細s = NBaseData.BLC.発注RowData見積詳細.GetRecords見積詳細(loginUser, rowData.OD_MM_ID);
                        foreach (NBaseData.BLC.発注RowData見積詳細 見積詳細 in 見積詳細s)
                        {
                            NBaseData.BLC.発注RowData詳細 詳細 = new NBaseData.BLC.発注RowData詳細();
                            詳細.Set見積詳細(見積詳細);

                            if (発注RowData詳細s.ContainsKey(詳細))
                            {
                                発注RowData詳細s[詳細].MmCount = 詳細.MmCount;
                            }
                        }
                        #endregion
                        // 受領詳細を出力する詳細データにセットする
                        #region
                        List<NBaseData.BLC.発注RowData受領詳細> 受領詳細s = NBaseData.BLC.発注RowData受領詳細.GetRecords受領詳細(loginUser, rowData.OD_JRY_ID);
                        foreach (NBaseData.BLC.発注RowData受領詳細 受領詳細 in 受領詳細s)
                        {
                            NBaseData.BLC.発注RowData詳細 詳細 = new NBaseData.BLC.発注RowData詳細();
                            詳細.Set受領詳細(受領詳細);

                            if (発注RowData詳細s.ContainsKey(詳細))
                            {
                                発注RowData詳細s[詳細].JryCount = 詳細.JryCount;
                                発注RowData詳細s[詳細].JryAmount = 詳細.JryAmount;
                            }
                            else
                            {
                                発注RowData詳細s.Add(詳細, 詳細);
                            }
                        }
                        #endregion
                        // 支払詳細を出力する詳細データにセットする
                        #region
                        List<NBaseData.BLC.発注RowData支払詳細> 支払詳細s = NBaseData.BLC.発注RowData支払詳細.GetRecords支払詳細(loginUser, rowData.OD_SHR_ID);
                        foreach (NBaseData.BLC.発注RowData支払詳細 支払詳細 in 支払詳細s)
                        {
                            NBaseData.BLC.発注RowData詳細 詳細 = new NBaseData.BLC.発注RowData詳細();
                            詳細.Set支払詳細(支払詳細);

                            if (発注RowData詳細s.ContainsKey(詳細))
                            {
                                発注RowData詳細s[詳細].ShrCount = 詳細.ShrCount;
                                発注RowData詳細s[詳細].ShrAmount = 詳細.ShrAmount;
                            }
                            else
                            {
                                発注RowData詳細s.Add(詳細, 詳細);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        //========================================================
                        // 見積回答の無い場合、手配から詳細品目をリストアップする
                        //========================================================

                        // 手配詳細を出力する詳細データにセットする
                        #region
                        List<NBaseData.BLC.発注RowData手配詳細> 手配詳細s = NBaseData.BLC.発注RowData手配詳細.GetRecords手配詳細(loginUser, rowData.OD_THI_ID);
                        foreach (NBaseData.BLC.発注RowData手配詳細 手配詳細 in 手配詳細s)
                        {
                            NBaseData.BLC.発注RowData詳細 詳細 = new NBaseData.BLC.発注RowData詳細();
                            詳細.Set手配詳細(手配詳細);

                            if (発注RowData詳細s.ContainsKey(詳細))
                            {
                                発注RowData詳細s[詳細].ZaikoCount = 詳細.ZaikoCount;
                                発注RowData詳細s[詳細].ThiCount = 詳細.ThiCount;
                            }
                            else
                            {
                                発注RowData詳細s.Add(詳細, 詳細);
                            }
                        }
                        #endregion
                        // 見積詳細を出力する詳細データにセットする
                        #region
                        List<NBaseData.BLC.発注RowData見積詳細> 見積詳細s = NBaseData.BLC.発注RowData見積詳細.GetRecords見積詳細(loginUser, rowData.OD_MM_ID);
                        foreach (NBaseData.BLC.発注RowData見積詳細 見積詳細 in 見積詳細s)
                        {
                            NBaseData.BLC.発注RowData詳細 詳細 = new NBaseData.BLC.発注RowData詳細();
                            詳細.Set見積詳細(見積詳細);

                            if (発注RowData詳細s.ContainsKey(詳細))
                            {
                                発注RowData詳細s[詳細].MmCount = 詳細.MmCount;
                            }
                            else
                            {
                                発注RowData詳細s.Add(詳細, 詳細);
                            }
                        }
                        #endregion
                    }

                    foreach (NBaseData.BLC.発注RowData詳細 key in 発注RowData詳細s.Keys)
                    {
                        NBaseData.BLC.発注RowData詳細 詳細 = 発注RowData詳細s[key];
                        
                        offset_x = 0;

                        #region ヘッダー情報

                        xls.Cell("A1", offset_x++, offset_y).Value = offset_y.ToString();
                        xls.Cell("A1", offset_x++, offset_y).Value = rowData.状況;
                        xls.Cell("A1", offset_x++, offset_y).Value = rowData.手配依頼種別;
                        xls.Cell("A1", offset_x++, offset_y).Value = rowData.手配依頼詳細種別;
                        xls.Cell("A1", offset_x++, offset_y).Value = rowData.手配依頼番号;
                        xls.Cell("A1", offset_x++, offset_y).Value = rowData.船;
                        xls.Cell("A1", offset_x++, offset_y).Value = rowData.場所;
                        xls.Cell("A1", offset_x++, offset_y).Value = rowData.手配依頼者;
                        xls.Cell("A1", offset_x++, offset_y).Value = rowData.事務担当者;
                        xls.Cell("A1", offset_x++, offset_y).Value = rowData.手配内容;
                        xls.Cell("A1", offset_x++, offset_y).Value = rowData.備考;
                        if (rowData.見積番号 != null && rowData.見積番号.Length > 0)
                        {
                            if (rowData.見積番号.Substring(0, 7) != "Enabled")
                            {
                                xls.Cell("A1", offset_x++, offset_y).Value = rowData.見積番号;
                            }
                            else
                            {
                                offset_x++; // 見積番号
                            }
                            if (rowData.見積回答番号 != null && rowData.見積回答番号.Length > 0)
                            {
                                xls.Cell("A1", offset_x++, offset_y).Value = rowData.見積依頼先;
                                xls.Cell("A1", offset_x++, offset_y).Value = rowData.見積依頼先_担当者;
                                xls.Cell("A1", offset_x++, offset_y).Value = rowData.メール送信状況;
                                xls.Cell("A1", offset_x++, offset_y).Value = Convert日付(rowData.送信日時);
                            }
                            else
                            {
                                offset_x++; // 見積依頼先
                                offset_x++; // 見積依頼先_担当者
                                offset_x++; // メール送信状況
                                offset_x++; // 送信日時
                            }
                        }
                        else
                        {
                            offset_x++; // 見積番号
                            offset_x++; // 見積依頼先
                            offset_x++; // 見積依頼先_担当者
                            offset_x++; // メール送信状況
                            offset_x++; // 送信日時
                        }

                        #endregion

                        xls.Cell("A1", offset_x++, offset_y).Value = 詳細.Header;
                        xls.Cell("A1", offset_x++, offset_y).Value = 詳細.MsItemSbtName;
                        xls.Cell("A1", offset_x++, offset_y).Value = 詳細.ItemName;
                        xls.Cell("A1", offset_x++, offset_y).Value = 詳細.ShousaiItemName;
                        xls.Cell("A1", offset_x++, offset_y).Value = 詳細.ShousaiBikou;

                        xls.Cell("A1", offset_x++, offset_y).Value = 詳細.MsTaniName;
                        xls.Cell("A1", offset_x++, offset_y).Value = 詳細.ZaikoCount;
                        xls.Cell("A1", offset_x++, offset_y).Value = 詳細.ThiCount;

                        xls.Cell("A1", offset_x++, offset_y).Value = 詳細.MmCount;

                        xls.Cell("A1", offset_x++, offset_y).Value = 詳細.MkCount;
                        xls.Cell("A1", offset_x++, offset_y).Value = 詳細.MkTanka;
                        xls.Cell("A1", offset_x++, offset_y).Value = 詳細.MkAmount;
                        if (rowData.OD_MK_ID.Length > 0 && rowData.発注日 != DateTime.MinValue)
                        {
                            xls.Cell("A1", offset_x++, offset_y).Value = Convert日付(rowData.発注日);
                            if (rowData.OD_JRY_ID.Length > 0 && rowData.受領日 != DateTime.MinValue)
                            {
                                xls.Cell("A1", offset_x++, offset_y).Value = Convert日付(rowData.受領日);
                                xls.Cell("A1", offset_x++, offset_y).Value = 詳細.JryCount;
                                xls.Cell("A1", offset_x++, offset_y).Value = 詳細.JryAmount;

                                if (rowData.OD_SHR_ID.Length > 0)
                                {
                                    xls.Cell("A1", offset_x++, offset_y).Value = 詳細.ShrCount;
                                    xls.Cell("A1", offset_x++, offset_y).Value = 詳細.ShrAmount;
                                }
                            }
                        }

                        offset_y++;
                    }
                }


                xls.CloseBook(true);
                if (xls.ErrorNo == ExcelCreator.ErrorNo.NoError)
                {
                    //res = true;
                }
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

        public string Convert日付(DateTime 日付)
        {
            string str日付 = "";
            if (日付 != DateTime.MinValue)
            {
                str日付 = 日付.ToShortDateString();
            }
            return str日付;
        }
    }
}
