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
using NBaseData.BLC;
using NBaseData.DAC;
using System.Collections.Generic;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using System.Text;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_合見積比較表_取得(NBaseData.DAC.MsUser loginUser,string odMmId);
    }

    public partial class Service
    {
        public byte[] BLC_合見積比較表_取得(NBaseData.DAC.MsUser loginUser, string odMmId)
        {
            string BaseFileName = "合見積比較表";

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


            //=========================================
            // ＤＢから必要な情報を取得する
            //=========================================
            List<OdMmItem> OdMmItems = null;
            List<OdMmShousaiItem> OdMmShousaiItems = null;
            List<OdMk> OdMks = null;
            List<List<OdMkItem>> OdMkItemsList = new List<List<OdMkItem>>();
            OdMmItems = OdMmItem.GetRecordsByOdMmID(loginUser, odMmId);
            OdMmShousaiItems = OdMmShousaiItem.GetRecordsByOdMmID(loginUser, odMmId);
            OdMks = OdMk.GetRecordsByOdMmID(loginUser, odMmId);
            foreach (OdMk odMk in OdMks)
            {
                List<OdMkItem> OdMkItems = OdMkItem.GetRecordsByOdMkID(loginUser, odMk.OdMkID);
                List<OdMkShousaiItem> OdMkShousaiItems = OdMkShousaiItem.GetRecordsByOdMkID(loginUser, odMk.OdMkID);
                foreach (OdMkItem mkItem in OdMkItems)
                {
                    foreach (OdMkShousaiItem shousaiItem in OdMkShousaiItems)
                    {
                        if (shousaiItem.OdMkItemID == mkItem.OdMkItemID)
                        {
                            mkItem.OdMkShousaiItems.Add(shousaiItem);
                        }
                    }
                }
                OdMkItemsList.Add(OdMkItems);
            }
            foreach (OdMmItem mmItem in OdMmItems)
            {
                foreach (OdMmShousaiItem shousaiItem in OdMmShousaiItems)
                {
                    if (shousaiItem.OdMmItemID == mmItem.OdMmItemID)
                    {
                        mmItem.OdMmShousaiItems.Add(shousaiItem);
                    }
                }
            }


            //=========================================
            // 表示用に加工する
            //=========================================
            合見積データ 合見積データ生成 = new 合見積データ();
            List<合見積データ.TreeNode_Header> treeNodeHeaders = 合見積データ生成.MakeTreeNodeHeaders(OdMmItems, OdMkItemsList);

            // 
            string cell_船 = "B1";
            string cell_手配内容 = "B2";
            string range_業者 = "C3:D10";
            string cell_業者名 = "C3";
            string cell_抜合計 = "D4";
            string cell_消費税 = "D5";
            string cell_送運料 = "D6";
            string cell_値引き = "D7"; // = "D6";
            string cell_込合計 = "D8"; // = "D7";

            string cell_品目名 = "A10"; // ="A9";

            int offset_x = 0;
            int offset_y = 0;

            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                xls.OpenBook(outPutFileName, templateName);

                #region Excelファイルの編集

                // ヘッダ部
                #region
                xls.Cell(cell_船).Value = OdMks[0].MsVesselName;
                xls.Cell(cell_手配内容).Value = OdMks[0].OdThiNaiyou;
                #endregion

                // 業者情報部
                #region
                offset_x = 0;
                foreach (OdMk odMk in OdMks)
                {
                    // C3 = (3,3) は、業者枠の左上のセル
                    string cellStr = GetCell(3 + offset_x, 3);

                    xls.Cell(range_業者).Copy(cellStr);
                    xls.Cell(cellStr).ColWidth = 20.75; // 見積回答数

                    cellStr = GetCell(3 + offset_x + 1, 3);
                    xls.Cell(cellStr).ColWidth = 11.88; // 金額
                    offset_x += 2;
                }

                offset_x = 0;
                foreach (OdMk odMk in OdMks)
                {
                    xls.Cell(cell_業者名, offset_x, 0).Value = odMk.MsCustomerName;
                    xls.Cell(cell_抜合計, offset_x, 0).Value = NBaseCommon.Common.金額出力(odMk.Amount);
                    xls.Cell(cell_消費税, offset_x, 0).Value = NBaseCommon.Common.金額出力(odMk.Tax);
                    xls.Cell(cell_送運料, offset_x, 0).Value = NBaseCommon.Common.金額出力(odMk.Carriage);
                    xls.Cell(cell_値引き, offset_x, 0).Value = NBaseCommon.Common.金額出力(odMk.MkAmount);
                    //xls.Cell(cell_込合計, offset_x, 0).Value = NBaseCommon.Common.金額出力(odMk.Amount + odMk.Tax - odMk.MkAmount);
                    xls.Cell(cell_込合計, offset_x, 0).Value = NBaseCommon.Common.金額出力(odMk.Amount + odMk.Tax + odMk.Carriage - odMk.MkAmount);
                    
                    offset_x += 2;
                }
                #endregion

                // データ部
                #region
                offset_y = 0;
                foreach (合見積データ.TreeNode_Header header in treeNodeHeaders)
                {
                    // ヘッダ
                    xls.Cell(cell_品目名, 0, offset_y).Value = header.Header;
                    //xls.Cell(cell_品目名, 0, offset_y).Attr.OverReturn = false;
                               
                    // (―)ヘッダ（見積回答列）
                    offset_x = 2;
                    for (int i = 0; i < OdMkItemsList.Count; i++) // 横方向に
                    {
                        xls.Cell(cell_品目名, offset_x, offset_y).Value = "'-----";
                        offset_x += 2;
                    }
                    offset_y++;

                    foreach (合見積データ.TreeNode_Item item in header.Items)
                    {
                        // 品目名
                        xls.Cell(cell_品目名, 0, offset_y).Value = "　"+item.ItemName;
                        //xls.Cell(cell_品目名, 0, offset_y).Attr.OverReturn = false;
                        
                        // (―)品目名（見積回答列）
                        offset_x = 2;
                        for (int i = 0; i < OdMkItemsList.Count; i++) // 横方向に
                        {
                            xls.Cell(cell_品目名, offset_x, offset_y).Value = "'-----";
                            offset_x += 2;
                        }
                        offset_y++;


                        foreach (合見積データ.TreeNode_ShousaiItem shousaiItem in item.ShousaiItems)
                        {
                            // 詳細品目名
                            xls.Cell(cell_品目名, 0, offset_y).Value = "　　" + shousaiItem.ShousaiItemName;
                            //xls.Cell(cell_品目名, 0, offset_y).Attr.OverReturn = false;
                            try
                            {
                                int c = int.Parse(shousaiItem.Count);
                                xls.Cell(cell_品目名, 1, offset_y).Value = shousaiItem.Count + " " + shousaiItem.TaniName;
                            }
                            catch
                            {
                                xls.Cell(cell_品目名, 1, offset_y).Value = shousaiItem.TaniName;
                            }

                            // （数量、金額）詳細品目名（見積回答列）   // 横方向に
                            offset_x = 2; // C列から
                            for (int i = 0; i < OdMkItemsList.Count; i++)
                            {
                                OdMkShousaiItem mkShousaiItem = 合見積データ生成.GetOdMkShousaiItem(OdMkItemsList[i], item, shousaiItem);
                                if (mkShousaiItem != null)
                                {
                                    xls.Cell(cell_品目名, offset_x, offset_y).Value = mkShousaiItem.Count;
                                    offset_x++;
                                    xls.Cell(cell_品目名, offset_x, offset_y).Value = (mkShousaiItem.Count * mkShousaiItem.Tanka);
                                    xls.Cell(cell_品目名, offset_x, offset_y).Attr.Format = "\\\\#,##0;[赤]\"▲\"\\\\#,##0";

                                    offset_x++;
                                }
                                else
                                {
                                    xls.Cell(cell_品目名, offset_x, offset_y).Value = "'-----";
                                    offset_x++;
                                    offset_x++;
                                }
                            }
                            offset_y++;
                        }
                    }
                }

                xls.Cell(cell_品目名 + ":" + GetCell(offset_x, offset_y + 9)).Attr.LineTop (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell(cell_品目名 + ":" + GetCell(offset_x, offset_y + 9)).Attr.LineBottom (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell(cell_品目名 + ":" + GetCell(offset_x, offset_y + 9)).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell(cell_品目名 + ":" + GetCell(offset_x, offset_y + 9)).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                #endregion

                // 印刷範囲の設定
                xls.PrintArea(0, 0, offset_x, offset_y + 8);

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

        protected string GetCell(int Col, int Row)
        {
            StringBuilder ret = new StringBuilder();
            string BaseChar = "";
            if (Col > 442)
            {
                BaseChar = "Q";
                Col -= 442;
            }
            else if (Col > 416)
            {
                BaseChar = "P";
                Col -= 416;
            }
            else if (Col > 390)
            {
                BaseChar = "O";
                Col -= 390;
            }
            else if (Col > 364)
            {
                BaseChar = "N";
                Col -= 364;
            }
            else if (Col > 338)
            {
                BaseChar = "M";
                Col -= 338;
            }
            else if (Col > 312)
            {
                BaseChar = "L";
                Col -= 312;
            }
            else if (Col > 286)
            {
                BaseChar = "K";
                Col -= 286;
            }
            else if (Col > 260)
            {
                BaseChar = "J";
                Col -= 260;
            }
            else if (Col > 234)
            {
                BaseChar = "I";
                Col -= 234;
            }
            else if (Col > 208)
            {
                BaseChar = "H";
                Col -= 208;
            }
            else if (Col > 182)
            {
                BaseChar = "G";
                Col -= 182;
            }
            else if (Col > 156)
            {
                BaseChar = "F";
                Col -= 156;
            }
            else if (Col > 130)
            {
                BaseChar = "E";
                Col -= 130;
            }
            else if (Col > 104)
            {
                BaseChar = "D";
                Col -= 104;
            }
            else if (Col > 78)
            {
                BaseChar = "C";
                Col -= 78;
            }
            else if (Col > 52)
            {
                BaseChar = "B";
                Col -= 52;
            }
            else if (Col > 26)
            {
                BaseChar = "A";
                Col -= 26;
            }


            char ColChar = 'A';
            ColChar += (char)(Col - 1);

            ret.Append(BaseChar);
            ret.Append(ColChar);
            ret.Append(Row);

            return ret.ToString();
        }
    
    }
}
