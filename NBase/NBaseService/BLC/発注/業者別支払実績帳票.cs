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
        byte[] BLC_業者別支払実績帳票_取得(NBaseData.DAC.MsUser loginUser, NBaseData.BLC.発注RowData検索条件 検索条件);
    }

    public partial class Service
    {
        public byte[] BLC_業者別支払実績帳票_取得(NBaseData.DAC.MsUser loginUser, NBaseData.BLC.発注RowData検索条件 検索条件)
        {
            #region 元になるファイルの確認と出力ファイル生成
            string BaseFileName = "業者別支払実績";
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

                // データ取得
                List<NBaseData.BLC.発注RowData> 発注RowDatas = NBaseData.BLC.発注RowData.GetRecordsヘッダー(loginUser, 検索条件);

                var customers = 発注RowDatas.Select(obj => obj.見積依頼先).Distinct();
                
                
                // データ行出力
                string kikan = "";
                if (検索条件.HachuDateFrom != null || 検索条件.HachuDateTo != null)
                {
                    kikan = "期間 : （発注日）";
                    if (検索条件.HachuDateFrom != null)
                        kikan += DateFormat(検索条件.HachuDateFrom);
                    kikan += " ～ ";
                    if (検索条件.HachuDateTo != null)
                        kikan += DateFormat(検索条件.HachuDateTo);
                }
                else if (検索条件.JryDateFrom != null || 検索条件.JryDateTo != null)
                {
                    kikan = "期間 : （受領日）";
                    if (検索条件.JryDateFrom != null)
                        kikan += DateFormat(検索条件.JryDateFrom);
                    kikan += " ～ ";
                    if (検索条件.JryDateTo != null)
                        kikan += DateFormat(検索条件.JryDateTo);
                }
                else if (検索条件.ShrDateFrom != null || 検索条件.ShrDateTo != null)
                {
                    kikan = "期間 : （支払日）";
                    if (検索条件.ShrDateFrom != null)
                        kikan += DateFormat(検索条件.ShrDateFrom);
                    kikan += " ～ ";
                    if (検索条件.ShrDateTo != null)
                        kikan += DateFormat(検索条件.ShrDateTo);
                }

                xls.Cell("A1").Value = kikan;


                int offset_x = 0;
                int offset_y = 3;

                int no = 0;
                foreach (string customer in customers)
                {
                    var list = 発注RowDatas.Where(obj => obj.見積依頼先 == customer).OrderBy(obj => obj.発注日);

                    offset_x = 0;

                    xls.Cell("A1", offset_x++, offset_y).Value = (++no);
                    xls.Cell("A1", offset_x++, offset_y).Value = customer;
                    offset_x++;
                    offset_x++;
                    offset_x++;
                    offset_x++;
                    offset_x++;
                    offset_x++;
                    offset_x++;
                    offset_x++;
                    offset_x++;
                    xls.Cell("A1", offset_x++, offset_y).Value = 発注RowDatas.Where(obj => obj.見積依頼先 == customer && obj.受領番号.Length > 0 && obj.支払番号.Length > 0).Sum(obj => obj.支払額);

                    offset_y++;

                    foreach (NBaseData.BLC.発注RowData row in list)
                    {
                        offset_x = 0;
                        offset_x++;
                        offset_x++;
                        xls.Cell("A1", offset_x++, offset_y).Value = row.手配依頼種別;
                        xls.Cell("A1", offset_x++, offset_y).Value = row.手配依頼詳細種別;
                        xls.Cell("A1", offset_x++, offset_y).Value = row.船;
                        xls.Cell("A1", offset_x++, offset_y).Value = row.手配内容;
                        xls.Cell("A1", offset_x++, offset_y).Value = row.発注日;

                        if (row.受領日 != DateTime.MinValue)
                            xls.Cell("A1", offset_x++, offset_y).Value = row.受領日;
                        else
                            offset_x++;

                        if (row.支払日 != DateTime.MinValue)
                            xls.Cell("A1", offset_x++, offset_y).Value = row.支払日;
                        else
                            offset_x++; 

                        if (row.計上日 != DateTime.MinValue)
                            xls.Cell("A1", offset_x++, offset_y).Value = row.計上日;
                        else
                            offset_x++;

                        if (row.支払番号.Length > 0)
                            xls.Cell("A1", offset_x++, offset_y).Value = row.支払額;
                        else
                            offset_x++;


                        offset_y++;
                    }

                }

                // 罫線を出力
                for (int i = 4; i <= offset_y; i++)
                {
                    xls.Cell("A" + i.ToString() + ":L" + i.ToString()).Attr.LineBottom (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                }
                xls.Cell("A3:A" + offset_y.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("B3:B" + offset_y.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("C3:C" + offset_y.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("D3:D" + offset_y.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("E3:E" + offset_y.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("F3:F" + offset_y.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("G3:G" + offset_y.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("H3:H" + offset_y.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("I3:I" + offset_y.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("J3:J" + offset_y.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("K3:K" + offset_y.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("L3:L" + offset_y.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("L3:L" + offset_y.ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

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

        private string DateFormat(DateTime? date)
        {
            return DateTime.Parse(date.ToString()).ToShortDateString();
        }
    }
}
