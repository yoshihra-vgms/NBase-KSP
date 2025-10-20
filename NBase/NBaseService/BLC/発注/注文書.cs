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
using System.Text;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC_注文書_準備(NBaseData.DAC.MsUser loginUser, string odMkId, string telNo, string faxNo, DateTime nouki, string okurisaki);
        
        [OperationContract]
        byte[] BLC_PDF注文書_取得(string odMkId, string telNo, string faxNo, string headerBikou);
    }

    public partial class Service
    {
        public bool BLC_注文書_準備(NBaseData.DAC.MsUser loginUser, string odMkId, string telNo, string faxNo, DateTime nouki, string okurisaki)
        {
            bool ret = false;
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = loginUser.MsUserID;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    // 見積回答を更新
                    OdMk odMk = OdMk.GetRecord(dbConnect, loginUser, odMkId);
                    odMk.Nouki = nouki;
                    odMk.Kiboubi = nouki;
                    odMk.RenewDate = RenewDate;
                    odMk.RenewUserID = RenewUserID;
                    ret = odMk.UpdateRecord(dbConnect, loginUser);
                    if (ret)
                    {
                        // 見積依頼を更新
                        OdMm odMm = OdMm.GetRecord(dbConnect, loginUser, odMk.OdMmID);
                        odMm.Okurisaki = okurisaki;
                        odMm.RenewDate = RenewDate;
                        odMm.RenewUserID = RenewUserID;
                        ret = odMm.UpdateRecord(dbConnect, loginUser);
                        if (ret)
                        {
                            MsCustomerTantou customerTantou = null;
                            List<MsCustomerTantou> customerTantous = NBaseData.DAC.MsCustomerTantou.GetRecordsByCustomerIDAndName(dbConnect, loginUser, odMk.MsCustomerID, odMk.Tantousha);
                            if (customerTantous.Count == 0)
                            {
                                customerTantou = new MsCustomerTantou();
                                customerTantou.MsCustomerID = odMk.MsCustomerID;
                                customerTantou.Name = odMk.Tantousha;
                                customerTantou.MailAddress = odMk.TantouMailAddress;
                                customerTantou.RenewDate = RenewDate;
                                customerTantou.RenewUserID = RenewUserID;

                                customerTantou.Tel = telNo;
                                customerTantou.Fax = faxNo;

                                ret = customerTantou.InsertRecord(dbConnect, loginUser);
                            }
                            else
                            {
                                customerTantou = customerTantous[0];
                                customerTantou.MailAddress = odMk.TantouMailAddress;
                                customerTantou.RenewDate = RenewDate;
                                customerTantou.RenewUserID = RenewUserID;

                                customerTantou.Tel = telNo;
                                customerTantou.Fax = faxNo;

                                ret = customerTantou.UpdateRecord(dbConnect, loginUser);
                            }
                        }
                    }
                }
                catch
                {
                }
                if (ret)
                {
                    dbConnect.Commit();
                }
                else
                {
                    dbConnect.RollBack();
                }
            }
            return ret;
        }
        public byte[] BLC_PDF注文書_取得(string odMkId, string telNo, string faxNo, string headerBikou)
        {
            string BaseFileName = "注文書";
            string 等幅Font = "ＭＳ 明朝";

            #region 元になるファイルの確認と出力ファイル生成
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateName = path + "Template_" + BaseFileName + ".xlsx";
            string outPutFileName = path + "output_" + BaseFileName + ".xlsx";
            string pdfFileName = path + "pdf" + BaseFileName + ".pdf";
            // templateNameの存在有無の確認
            bool exists = System.IO.File.Exists(templateName);
            if (exists == false)
            {
                return null;
            }
            #endregion

            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                //xls.OpenBook(outPutFileName, templateName);
                int xlsRet = xls.OpenBook(outPutFileName, templateName);
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

                #region Excelファイルの編集

                OdMk odMk = OdMk.GetRecord(new MsUser(), odMkId);
                List<OdMkItem> list_odMkItem = OdMkItem.GetRecordsByOdMkID(new MsUser(), odMkId);
                OdMm odMm = OdMm.GetRecord(new MsUser(), odMk.OdMmID);
                OdThi odThi = OdThi.GetRecord(new MsUser(), odMm.OdThiID);
                MsShrJouken msShrJouken = MsShrJouken.GetRecord(new MsUser(), odMm.MsShrJoukenID);
                MsCustomer msCustomer = MsCustomer.GetRecord(new MsUser(), odMk.MsCustomerID);

                // 2009.12.14:aki 「注文書」に出力するのは、見積回答番号ではなく、発注番号
                //xls.Cell("**NO").Value = "NO." + odMk.MkNo;
                if (odMk.HachuNo != null)
                {
                    xls.Cell("**NO").Value = "NO." + odMk.HachuNo;
                }
                else
                {
                    xls.Cell("**NO").Value = "";
                }
                // 2009.10.25:aki 「注文書」に出力する日付は、「発注日」
                //xls.Cell("**日付").Value = odMm.MmDate.ToString("yyyy年MM月dd日");
                if (odMk.HachuDate != DateTime.MinValue)
                {
                    xls.Cell("**日付").Value = odMk.HachuDate.ToString("yyyy年MM月dd日");
                }
                else
                {
                    xls.Cell("**日付").Value = "";
                }
                if (msCustomer != null)
                {
                    xls.Cell("**相手先").Value = msCustomer.CustomerName + " " + odMk.Tantousha + " 様";
                }
                xls.Cell("**相手先").Attr.FontULine = ExcelCreator.FontULine.Normal;

                xls.Cell("**TEL_FAX").Value = "TEL:" + telNo + ",FAX:" + faxNo;
                xls.Cell("**作成者").Value = odMm.MmSakuseishaName;
                xls.Cell("**船舶名").Value = odThi.VesselName;
                if (odMk.Nouki != DateTime.MinValue)
                {
                    xls.Cell("**納期").Value = odMk.Nouki.ToString("yyyy年MM月dd日"); // 納期：「見積回答」の「納期」
                }
                if (msShrJouken != null)
                {
                    xls.Cell("**支払条件").Value = msShrJouken.ShiharaiJoukenName;  // 支払条件：「見積依頼」の「支払条件」
                }
                // 2009.10.02:aki 
                //xls.Cell("**場所").Value = odThi.Basho;
                //xls.Cell("**場所").Value = odMm.Okurisaki;  　　　                  // 場所：「見積依頼」の「送り先」

                // 2012.02
                double bashoHeight = getBikouHeight(odMm.Okurisaki);
                int bashoRows = getRowCount(bashoHeight);
                xls.Cell("**場所").RowHeight = bashoHeight;
                xls.Cell("**場所").Attr.WrapText = true;
                xls.Cell("**場所").Value = odMm.Okurisaki;  　　　                  // 場所：「見積依頼」の「送り先」
                xls.Cell("**場所").Attr.FontName = 等幅Font;
               
                // 2010.01.22:aki --- W090210
                //double bikouHeight = getBikouHeight(odThi.Bikou);
                // 2012.02
                double bikouHeight = getBikouHeight(headerBikou);
                int bikouRows = getRowCount(bikouHeight);
                xls.Cell("**備考").RowHeight = bikouHeight;
                xls.Cell("**備考").Attr.WrapText = true;
                //xls.Cell("**備考").Value = odThi.Bikou;
                // 2012.02
                xls.Cell("**備考").Value = headerBikou;
                xls.Cell("**備考").Attr.FontName = 等幅Font;

                int no = 1;
                int offset_y = 0;
                string header = null;
                //int startRow = 22;
                int startRow = 23;
                int writtenRows = 0;     // 書いた行数
                //int firstPageRows = 36 - bikouRows;  // １ページ目の行数
                int firstPageRows = 35 - bikouRows - bashoRows;  // １ページ目の行数
                int nextPageRows = 55;   // ２ページ目以降の行数
                int pageRows = firstPageRows;
                foreach (OdMkItem odMkItem in list_odMkItem)
                {

                    // 2016.03 燃料_潤滑油の場合、数量がないものは、注文書に出力しない
                    List<OdMkShousaiItem> list_odMkShousaiItem = OdMkShousaiItem.GetRecordsByOdMkItemID(new MsUser(), odMkItem.OdMkItemID);
                    if (odThi.MsThiIraiSbtID == MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.燃料_潤滑油) && list_odMkShousaiItem.Any(obj => obj.Count > 0) == false)
                    {
                        continue;
                    }


                    //=================================
                    // 主題
                    if (header == null || header != odMkItem.Header)
                    {
                        header = odMkItem.Header;
                        double headerHeight = getItemHeight(header);
                        int headerRows = getRowCount(headerHeight);

                        if (writtenRows + headerRows > pageRows)
                        {
                            for (int i = writtenRows; i < pageRows; i++)
                            {
                                offset_y++;
                            }
                            writeHeader(xls, startRow, offset_y);
                            offset_y++;
                            pageRows = nextPageRows;
                            writtenRows = 0;
                        }
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).Value = header;
                        setAttr(xls, startRow, offset_y);
                        //xls.Cell("C" + startRow.ToString(), 0, offset_y).RowHeight = getRowHeight(header);
                        //xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.OverReturn = false;
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).RowHeight = headerHeight;
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.WrapText = true;
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.FontName = 等幅Font;
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.VerticalAlignment = ExcelCreator.VerticalAlignment.Top;
                        offset_y++;
                        //writtenRows += getRows(odMkItem.Header);
                        writtenRows += headerRows;
                    }

                    //=================================
                    // 仕様･型式
                    string itemName = "　";
                    if (odMkItem.MsItemSbtID != null && odMkItem.MsItemSbtID.Length > 0)
                    {
                        itemName += odMkItem.MsItemSbtName + "：";
                    }
                    itemName += odMkItem.ItemName;
                    itemName = itemName.Replace("\n", "\n　");　 // 仕様･型式のインデント調整（スペース１個）

                    double itemHeight = getItemHeight(itemName);
                    int itemRows = getRowCount(itemHeight);
                    if (writtenRows + itemRows > pageRows)
                    {
                        for (int i = writtenRows; i < pageRows; i++)
                        {
                            offset_y++;
                        }
                        writeHeader(xls, startRow, offset_y);
                        offset_y++;
                        pageRows = nextPageRows;
                        writtenRows = 0;
                    }
                    xls.Cell("C" + startRow.ToString(), 0, offset_y).Value = itemName;
                    setAttr(xls, startRow, offset_y);
                    //xls.Cell("C" + startRow.ToString(), 0, offset_y).RowHeight = getRowHeight(odMkItem.ItemName);
                    //xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.OverReturn = false;
                    xls.Cell("C" + startRow.ToString(), 0, offset_y).RowHeight = itemHeight;
                    xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.WrapText = true;
                    xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.FontName = 等幅Font;
                    xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.VerticalAlignment = ExcelCreator.VerticalAlignment.Top;
                    offset_y++;
                    writtenRows += itemRows;


                    //=================================
                    // 詳細品目
                    foreach (OdMkShousaiItem odMkShousaiItem in list_odMkShousaiItem)
                    {
                        // 2016.03 燃料_潤滑油の場合、数量がないものは、注文書に出力しない
                        if (odMkShousaiItem.Count < 1)
                        {
                            continue;
                        }

                        string shousaiName = "　　" + odMkShousaiItem.ShousaiItemName;
                        shousaiName = shousaiName.Replace("\n", "\n　　");　 // 詳細品目のインデント調整（スペース２個）
                        if (odMkShousaiItem.Bikou.Length > 0)
                        {
                            string bikou = "　　備考：" + odMkShousaiItem.Bikou;
                            bikou = bikou.Replace("\n", "\n　　　　　");

                            shousaiName += "\n" + bikou;
                        }

                        double shousaiHeight = getItemHeight(shousaiName);
                        int shousaiRows = getRowCount(shousaiHeight);
                        if (writtenRows + shousaiRows > pageRows)
                        {
                            for (int i = writtenRows; i < pageRows; i++)
                            {
                                offset_y++;
                            }
                            writeHeader(xls, startRow, offset_y);
                            offset_y++;
                            pageRows = nextPageRows;
                            writtenRows = 0;
                        }
                        // No
                        xls.Cell("B" + startRow.ToString(), 0, offset_y).Value = no.ToString();
                        // 詳細品目名
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).Value = shousaiName;
                        // 数量
                        xls.Cell("H" + startRow.ToString(), 0, offset_y).Value = odMkShousaiItem.Count.ToString();
                        xls.Cell("H" + startRow.ToString(), 0, offset_y).Attr.HorizontalAlignment = ExcelCreator.HorizontalAlignment.Center;
                        // 単位
                        xls.Cell("I" + startRow.ToString(), 0, offset_y).Value = odMkShousaiItem.MsTaniName;
                        xls.Cell("I" + startRow.ToString(), 0, offset_y).Attr.HorizontalAlignment = ExcelCreator.HorizontalAlignment.Center;

                        //xls.Cell("B" + startRow.ToString(), 0, offset_y).RowHeight = getRowHeight(shousaiName);
                        //xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.OverReturn = false;
                        setAttr(xls, startRow, offset_y);
                        
                        // 行の高さを設定する
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).RowHeight = shousaiHeight;
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.WrapText = true;
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.FontName = 等幅Font;
                        xls.Cell("C" + startRow.ToString(), 0, offset_y).Attr.VerticalAlignment = ExcelCreator.VerticalAlignment.Top; 

                        no++;
                        offset_y++;
                        writtenRows += shousaiRows;
                    }
                }

                // 印刷範囲の設定
                xls.PrintArea(0, 0, 9, (startRow + offset_y));

                #endregion

                // PDF出力
                xls.CloseBook(true, pdfFileName, true);

                if (xls.ErrorNo == ExcelCreator.ErrorNo.NoError)
                {
                    //ret = true; 
                }
            }

            byte[] bytBuffer;
            #region バイナリーへ変換
            using (FileStream objFileStream = new FileStream(pdfFileName, FileMode.Open))
            {
                long lngFileSize = objFileStream.Length;

                bytBuffer = new byte[(int)lngFileSize];
                objFileStream.Read(bytBuffer, 0, (int)lngFileSize);
                objFileStream.Close();
            }
            #endregion

            return bytBuffer;
        }


        private void writeHeader(ExcelCreator.Xlsx.XlsxCreator xls, int startRow, int offset_y)
        {
            xls.Cell("B" + startRow.ToString(), 0, offset_y).Value = "No";
            xls.Cell("C" + startRow.ToString(), 0, offset_y).Value = "品目";
            xls.Cell("H" + startRow.ToString(), 0, offset_y).Value = "数量";
            xls.Cell("I" + startRow.ToString(), 0, offset_y).Value = "単位";

            setAttr(xls, startRow, offset_y);
        }

        private void setAttr(ExcelCreator.Xlsx.XlsxCreator xls, int startRow, int offset_y)
        {
            // 罫線
            xls.Cell("B" + startRow.ToString() + ":I" + startRow.ToString(), 0, offset_y).Attr.LineTop (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("B" + startRow.ToString() + ":I" + startRow.ToString(), 0, offset_y).Attr.LineBottom (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

            xls.Cell("B" + startRow.ToString() + ":B" + startRow.ToString(), 0, offset_y).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("C" + startRow.ToString() + ":C" + startRow.ToString(), 0, offset_y).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("H" + startRow.ToString() + ":H" + startRow.ToString(), 0, offset_y).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("I" + startRow.ToString() + ":I" + startRow.ToString(), 0, offset_y).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            xls.Cell("J" + startRow.ToString() + ":J" + startRow.ToString(), 0, offset_y).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
            
            // セルを結合
            xls.Cell("C" + startRow.ToString() + ":G" + startRow.ToString(), 0, offset_y).Attr.MergeCells = true;

            // 水平位置
            xls.Cell("C" + startRow.ToString() + ":G" + startRow.ToString(), 0, offset_y).Attr.HorizontalAlignment = ExcelCreator.HorizontalAlignment.Left;
            xls.Cell("H" + startRow.ToString() + ":H" + startRow.ToString(), 0, offset_y).Attr.HorizontalAlignment = ExcelCreator.HorizontalAlignment.Center;
            xls.Cell("I" + startRow.ToString() + ":I" + startRow.ToString(), 0, offset_y).Attr.HorizontalAlignment = ExcelCreator.HorizontalAlignment.Center;
        }

        /// <summary>
        /// 行数を取得する
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int getRows(string msg)
        {  
            int count = 1;
            #region 2009.10.01-修正(aki)
            //int idx = 0;
            //while (idx != -1)
            //{
            //    idx = msg.IndexOf("\n", idx);
            //    if (idx != -1)
            //    {
            //        idx = idx + 2;
            //        count++;
            //    }
            //}
            #endregion
            string[] msgs = msg.Split('\n');
            if (msgs.Length == 0)
            {
                count = 1;
            }
            else
            {
                count = msgs.Length;
            }
            return count;
        }

        /// <summary>
        /// 行の高さを取得する
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public double getRowHeight(string msg)
        {
            double height = 0;
            #region 2009.10.01-修正(aki)
            //int count = 1;
            //int idx = 0;
            //while (idx != -1)
            //{
            //    idx = msg.IndexOf("\n", idx);
            //    if (idx != -1)
            //    {
            //        idx = idx + 2;
            //        count++;
            //    }
            //}
            #endregion
            int count = getRows(msg);
            height = 13.5 * count;
            return height;
        }

        /// <summary>
        /// 行の高さを取得する
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public double getBikouHeight(string msg)
        {
            // １行 MS明朝 全角２７文字として行数を割り出す
            return getInnerRowHeight(msg, 27);
        }

        public double getItemHeight(string msg)
        {
            // １行 MS明朝 全角３３文字として行数を割り出す
            return getInnerRowHeight(msg, 33);
        }

        public double getInnerRowHeight(string msg, int maxCharLen)
        {
            // １行の高さ
            double height = 13.5;
            if (msg == null || msg.Length == 0)
            {
                return height;
            }

            // Excelの行の高さの制限
            double maxHeight = 409;

            // 改行コードで分割する
            string[] msgs = msg.Split('\n');

            // 各データが何行になるか
            int totalCount = 0;
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            for (int i = 0; i < msgs.Length; i++)
            {
                int count = 1;
                try
                {
                    // バイト数を数える
                    int num = sjisEnc.GetByteCount(msgs[i]);

                    // １行全角指定文字数として行数を割り出す
                    count = num / 2 / maxCharLen;
                    if (num != count * 2 * maxCharLen)
                    {
                        count += 1;
                    }
                }
                catch
                {
                }
                totalCount += count;
            }

            // 高さを割り出す
            height = 13.5 * totalCount;
            if (height > maxHeight)
            {
                height = maxHeight;
            }
            return height;
        }

        private int getRowCount(double height)
        {
            int count = 0;
            try
            {
                count = (int)(height / 13.5);
                if (count != (int)(height / 13.5))
                {
                    count += 1;
                }
            }
            catch
            {
            }
            return count;
        }
    }

}
