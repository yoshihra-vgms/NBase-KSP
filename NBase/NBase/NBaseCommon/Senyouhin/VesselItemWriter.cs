using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseData.BLC;

namespace NBaseCommon.Senyouhin
{
    public class VesselItemWriter
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;

        public VesselItemWriter(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }

        public void CreateFile(MsUser loginUser, string odThiId)
        {
           
            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                
                //-----------------------
                //2013/12/18 コメントアウト m.y
                //xls.OpenBook(outputFilePath, templateFilePath);      
                //-----------------------
                //2013/12/18 変更:OpenBookエラーをなげる m.y
                int xlsRet = xls.OpenBook(outputFilePath, templateFilePath);
                if (xlsRet < 0)
                {
                    Exception xlsEx = null;
                    if (xls.ErrorNo == ExcelCreator.ErrorNo.TempCreate)
                    {
                        xlsEx = new Exception("ディスクに十分な空き領域がありません。");
                    }
                    else
                    {
                        xlsEx = new Exception("サーバでエラーが発生しました。");
                    }
                    throw xlsEx;
                }
                //-----------------------

                WriteHeader(xls, loginUser, odThiId);
                WriteData(xls, loginUser, odThiId);

                xls.CloseBook(true);
            }
        }

        private void WriteHeader(ExcelCreator.Xlsx.XlsxCreator xls, MsUser loginUser, string odThiId)
        {
            OdThi odThi = OdThi.GetRecord(loginUser, odThiId);
            
            // 船名
            xls.Cell("A5").Value = odThi.VesselName;

            // 手配内容
            xls.Cell("A7").Value = odThi.Naiyou;
        }

        private void WriteData(ExcelCreator.Xlsx.XlsxCreator xls, MsUser loginUser, string odThiId)
        {
            List<OdThiItem> odThiItems = 船用品.BLC_船用品品目(loginUser, odThiId);
            List<MsTani> msTanis = MsTani.GetRecords(loginUser);

            int stratRowNo = 10;
            int rowNo = stratRowNo;
            int rowIdx = 0;
            string busho = "";
            string shiyouKatashiki = "";

            foreach (OdThiItem odThiItem in odThiItems)
            {
                if (odThiItem.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                    continue;

                if (busho != odThiItem.Header)
                {
                    if (rowNo != stratRowNo)
                    {
                        rowNo++;
                        //rowNo++;
                    }
                    busho = odThiItem.Header;
                    shiyouKatashiki = odThiItem.ItemName;

                    rowNo++;
                    xls.Cell("B" + rowNo.ToString()).Value = busho;
                    rowNo++;
                    xls.Cell("B" + rowNo.ToString()).Value = shiyouKatashiki;
                    rowNo++;
                } 
                else if (shiyouKatashiki != odThiItem.ItemName)
                {
                    rowNo++;
                    shiyouKatashiki = odThiItem.ItemName;

                    xls.Cell("B" + rowNo.ToString()).Value = shiyouKatashiki;
                    rowNo++;
                } 

                foreach (OdThiShousaiItem odThiShousaiItem in odThiItem.OdThiShousaiItems)
                {
                    if (odThiShousaiItem.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                        continue;

                    rowIdx++;
                    // 番号
                    xls.Cell("A" + rowNo.ToString()).Value = rowIdx.ToString();
                    // 詳細品目
                    xls.Cell("B" + rowNo.ToString()).Value = odThiShousaiItem.ShousaiItemName;
                    xls.Cell("B" + rowNo.ToString()).Attr.WrapText = true;
                    // 単位
                    xls.Cell("C" + rowNo.ToString()).Value = GetTaniName(msTanis, odThiShousaiItem.MsTaniID);
                    // 在庫数
                    xls.Cell("D" + rowNo.ToString()).Value = odThiShousaiItem.ZaikoCount.ToString();
                    // 依頼数
                    xls.Cell("E" + rowNo.ToString()).Value = odThiShousaiItem.Count.ToString();
                    // 査定数 // 2012.03.02:査定数の出力はしない
                    //xls.Cell("F" + rowNo.ToString()).Value = odThiShousaiItem.Sateisu.ToString();
                    // 備考
                    xls.Cell("G" + rowNo.ToString()).Value = odThiShousaiItem.Bikou;
                    xls.Cell("G" + rowNo.ToString()).Attr.WrapText = true;

                    rowNo++;
                }
            }

            // Box罫線
            xls.Cell("A" + stratRowNo.ToString() + ":G" + (rowNo - 1).ToString()).Attr.Box(ExcelCreator.BoxType.Box, ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

            // 横罫線
            xls.Cell("A" + (stratRowNo + 1).ToString() + ":G" + (rowNo - 1).ToString()).Attr.LineTop ( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("A" + (stratRowNo + 1).ToString() + ":G" + (rowNo - 1).ToString()).Attr.LineBottom ( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

            // 縦罫線
            xls.Cell("A" + stratRowNo.ToString() + ":G" + (rowNo - 1).ToString()).Attr.LineLeft ( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
        }

        private string GetTaniName(List<MsTani> taniList, string taniId)
        {
            string taniName = "";
            foreach (MsTani tani in taniList)
            {
                if (tani.MsTaniID == taniId)
                {
                    taniName = tani.TaniName;
                    break;
                }
            }
            return taniName;
        }
    }
}
