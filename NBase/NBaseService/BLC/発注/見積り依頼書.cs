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

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_PDF見積り依頼書_取得(string odMkId);
    }

    public partial class Service
    {
        public byte[] BLC_PDF見積り依頼書_取得(string odMkId)
        {
            string BaseFileName = "見積り依頼書";
            string 等幅Font = "ＭＳ 明朝";


            #region 元になるファイルの確認と出力ファイル生成
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateName = path + "Template_" + BaseFileName + ".xlsx";
            string outPutFileName = path + "output_" + BaseFileName + ".xlsx";
            // templateNameの存在有無の確認
            bool exists = System.IO.File.Exists(templateName);
            if (exists == false)
            {
                return null;
            }
            #endregion

            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                xls.OpenBook(outPutFileName, templateName);

                #region Excelファイルの編集

                MsUser dmyUser = new MsUser();
                OdMk odMk = OdMk.GetRecord(dmyUser, odMkId);
                List<OdMkItem> list_odMkItem = OdMkItem.GetRecordsByOdMkID(dmyUser, odMkId);
                Dictionary<string, OdMkItem> dic_odMkItem = new Dictionary<string, OdMkItem>();
                foreach (OdMkItem odMkItem in list_odMkItem)
                {
                    // 見積り依頼された品目が対象
                    if (odMkItem.OdMmItemID == null || odMkItem.OdMmItemID == "")
                    {
                        continue;
                    }
                    dic_odMkItem.Add(odMkItem.OdMmItemID, odMkItem);
                }
                OdMm odMm = OdMm.GetRecord(dmyUser, odMk.OdMmID);
                List<OdMmItem> list_odMmItem = OdMmItem.GetRecordsByOdMmID(dmyUser, odMk.OdMmID);
                List<OdMmShousaiItem> list_odMmShousaiItemAll = OdMmShousaiItem.GetRecordsByOdMmID(dmyUser, odMk.OdMmID);
                Dictionary<string, List<OdMmShousaiItem>> dic_ListOdMmShousaiItem = new Dictionary<string, List<OdMmShousaiItem>>();
                foreach (OdMmShousaiItem odMmShousaiItem in list_odMmShousaiItemAll)
                {
                    List<OdMmShousaiItem> list = null;
                    if (dic_ListOdMmShousaiItem.ContainsKey(odMmShousaiItem.OdMmItemID))
                    {
                        list = dic_ListOdMmShousaiItem[odMmShousaiItem.OdMmItemID];
                        list.Add(odMmShousaiItem);

                        dic_ListOdMmShousaiItem[odMmShousaiItem.OdMmItemID] = list;
                    }
                    else
                    {
                        list = new List<OdMmShousaiItem>();
                        list.Add(odMmShousaiItem);

                        dic_ListOdMmShousaiItem.Add(odMmShousaiItem.OdMmItemID, list);
                    }
                }

                OdThi odThi = OdThi.GetRecord(dmyUser, odMm.OdThiID);
                MsShrJouken msShrJouken = MsShrJouken.GetRecord(dmyUser, odMm.MsShrJoukenID);
                MsCustomer msCustomer = MsCustomer.GetRecord(dmyUser, odMk.MsCustomerID);

                MsCustomerTantou customerTantou = null;
                List<MsCustomerTantou> customerTantous = MsCustomerTantou.GetRecordsByCustomerIDAndName(dmyUser, odMk.MsCustomerID, odMk.Tantousha);
                if (customerTantous.Count >= 0)
                {
                    customerTantou = customerTantous[0];
                }

                xls.Cell("**NO").Value = "NO." + odMk.MkNo;
                xls.Cell("**日付").Value = odMk.MmDate.ToString("yyyy年MM月dd日"); // 見積回答の見積依頼日
                if (msCustomer != null)
                {
                    xls.Cell("**相手先").Value = msCustomer.CustomerName + " " + odMk.Tantousha + " 様";
                }
                xls.Cell("**相手先").Attr.FontULine = ExcelCreator.FontULine.Normal;

                if (customerTantou != null)
                {
                    xls.Cell("**TEL_FAX").Value = "TEL:" + customerTantou.Tel + ",FAX:" + customerTantou.Fax;
                }
                else
                {
                    xls.Cell("**TEL_FAX").Value = "TEL:" + msCustomer.Tel + ",FAX:" + msCustomer.Fax;
                }
                xls.Cell("**作成者").Value = odMm.MmSakuseishaName;
                xls.Cell("**船舶名").Value = odThi.VesselName;
                if (odMk.Kiboubi != DateTime.MinValue)
                {
                    xls.Cell("**納期").Value = odMk.Kiboubi.ToString("yyyy年MM月dd日"); // 希望納期：「見積依頼先設定」の「希望納期」
                }
                xls.Cell("**場所").Value = odMm.Okurisaki;  　　　// 納品場所：「見積依頼」の「送り先」
                xls.Cell("**見積回答期限").Value = odMk.MkKigen;　// 見積回答期限：「見積依頼先設定」の「見積回答期限」
                if (msShrJouken != null)
                {
                    xls.Cell("**支払条件").Value = msShrJouken.ShiharaiJoukenName;  // 支払条件：「見積依頼」の「支払条件」
                }
                xls.Cell("**備考").RowHeight = getBikouHeight(odThi.Bikou);
                xls.Cell("**備考").Attr.WrapText = true;
                xls.Cell("**備考").Value = odThi.Bikou;
                xls.Cell("**備考").Attr.FontName = 等幅Font;

                int no = 1;
                int offset_y = 0;
                string header = null;
                int startRow = 24;
                foreach (OdMmItem odMmItem in list_odMmItem)
                {
                    // 見積り依頼された品目が対象
                    if (dic_odMkItem.ContainsKey(odMmItem.OdMmItemID) == false)
                    {
                        continue;
                    }

                    //=================================
                    // 主題
                    if (header == null || header != odMmItem.Header)
                    {
                        header = odMmItem.Header;
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).Value = header;
                        // 行の高さを設定する
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).RowHeight = getItemHeight(header);
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.WrapText = true;
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.FontName = 等幅Font;
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.VerticalAlignment = ExcelCreator.VerticalAlignment.Top;
                        offset_y++;
                    }


                    //=================================
                    // 仕様･型式
                    string itemName = "　";
                    //if (odMmItem.MsItemSbtID != null && odMmItem.MsItemSbtID.Length > 0)
                    //{
                    //    itemName += odMmItem.MsItemSbtName + "：";
                    //}
                    itemName += odMmItem.ItemName;
                    itemName = itemName.Replace("\n", "\n　");　 // 仕様･型式のインデント調整（スペース１個）
                    xls.Cell("C" + startRow.ToString(), 0, offset_y).Value = itemName;
                    // 行の高さを設定する
                    xls.Cell("C" + startRow.ToString(), 0, offset_y).RowHeight = getItemHeight(itemName);
                    xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.WrapText = true;
                    xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.FontName = 等幅Font;
                    xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.VerticalAlignment = ExcelCreator.VerticalAlignment.Top;
                    offset_y++;


                    //=================================
                    // 詳細品目
                    if (dic_ListOdMmShousaiItem.ContainsKey(odMmItem.OdMmItemID) == false)
                    {
                        continue;
                    }
                    OdMkItem odMkItem = dic_odMkItem[odMmItem.OdMmItemID];
                    List<OdMkShousaiItem> list_odMkShousaiItem = OdMkShousaiItem.GetRecordsByOdMkItemID(new MsUser(), odMkItem.OdMkItemID);
                    Dictionary<string, OdMkShousaiItem> dic_odMkShousaiItem = new Dictionary<string, OdMkShousaiItem>();
                    foreach (OdMkShousaiItem odMkShousaiItem in list_odMkShousaiItem)
                    {
                        // 見積り依頼された詳細品目が対象
                        if (odMkShousaiItem.OdMmShousaiItemID == null || odMkShousaiItem.OdMmShousaiItemID == "")
                        {
                            continue;
                        }
                        dic_odMkShousaiItem.Add(odMkShousaiItem.OdMmShousaiItemID, odMkShousaiItem);
                    }             


                    List<OdMmShousaiItem> list_odMmShousaiItem = dic_ListOdMmShousaiItem[odMmItem.OdMmItemID];
                    foreach (OdMmShousaiItem odMmShousaiItem in list_odMmShousaiItem)
                    {
                        // 見積り依頼された詳細品目が対象
                        if (dic_odMkShousaiItem.ContainsKey(odMmShousaiItem.OdMmShousaiItemID) == false)
                        {
                            continue;
                        }
                        OdMkShousaiItem odMkShousaiItem = dic_odMkShousaiItem[odMmShousaiItem.OdMmShousaiItemID];

                        // No
                        xls.Cell("B" + startRow.ToString(), 0, offset_y).Value = no.ToString();
                        // 詳細品目名
                        string shousaiName = "　　" + odMmShousaiItem.ShousaiItemName;
                        shousaiName = shousaiName.Replace("\n", "\n　　");　 // 詳細品目のインデント調整（スペース２個）
                        if (odMmShousaiItem.Bikou.Length > 0)
                        {
                            string bikou = "　　備考：" + odMmShousaiItem.Bikou;
                            bikou = bikou.Replace("\n", "\n　　　　　");

                            shousaiName += "\n" + bikou;
                        }
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).Value = shousaiName;
                        // 数量
                        xls.Cell("H" + startRow.ToString(), 0, offset_y).Value = odMkShousaiItem.OdMmShousaiItemCount.ToString();
                        xls.Cell("H" + startRow.ToString(), 0, offset_y).Attr.HorizontalAlignment = ExcelCreator.HorizontalAlignment.Center;
                        // 単位
                        xls.Cell("I" + startRow.ToString(), 0, offset_y).Value = odMmShousaiItem.MsTaniName;
                        xls.Cell("I" + startRow.ToString(), 0, offset_y).Attr.HorizontalAlignment = ExcelCreator.HorizontalAlignment.Center;

                        // 行の高さを設定する
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).RowHeight = getItemHeight(shousaiName);
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.WrapText = true;
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.FontName = 等幅Font;
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.VerticalAlignment = ExcelCreator.VerticalAlignment.Top;
                        no++;
                        offset_y++;
                    }
                }

                // 罫線を出力
                for (int i = 0; i < offset_y; i++)
                {
                    xls.Cell("B" + startRow.ToString() + ":I" + startRow.ToString(), 0, i).Attr.LineTop(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("B" + startRow.ToString() + ":I" + startRow.ToString(), 0, i).Attr.LineBottom(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("C" + startRow.ToString() + ":G" + startRow.ToString(), 0, i).Attr.MergeCells = true;
                    xls.Cell("C" + startRow.ToString() + ":G" + startRow.ToString(), 0, i).Attr.HorizontalAlignment = ExcelCreator.HorizontalAlignment.Left;		// 水平位置：左揃え
                }
                int val = startRow + offset_y - 1;
                xls.Cell("B" + startRow.ToString() + ":B" + val.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("C" + startRow.ToString() + ":C" + val.ToString()).Attr.LineLeft  (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("H" + startRow.ToString() + ":H" + val.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("I" + startRow.ToString() + ":I" + val.ToString()).Attr.LineLeft  (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("J" + startRow.ToString() + ":J" + val.ToString()).Attr.LineLeft  (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                // 印刷範囲の設定
                xls.PrintArea(0, 0, 9, (startRow + offset_y));

                #endregion

                // Excel出力
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
    }
}
