using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using System.IO;
using ExcelCreator = AdvanceSoftware.ExcelCreator;

namespace NBaseData.BLC.Doc
{
    public class 概算計上一覧出力
    {
        public static readonly string TemplateName = "概算計上一覧";
        private int StartRow = 5;
        private int RowNo = 0;


        public byte[] 概算計上一覧取得(MsUser loginUser, int year, int month,  List<概算計上一覧Row> 概算計上一覧Rows)
        {

            #region 元になるファイルの確認と出力ファイル生成
            string BaseFileName = TemplateName;
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateName = path + "Template_" + BaseFileName + ".xlsx";
            string outPutFileName = path + "outPut_[" + loginUser.FullName + "]_" + BaseFileName + ".xlsx";

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

                RowNo = StartRow;
                decimal 合計金額 = 0;
                var rows = from p in 概算計上一覧Rows
                           orderby p.MsVesselId, p.種別, p.詳細種別, p.StatusOrder, p.受領番号
                           select p;
                foreach (概算計上一覧Row data in rows)
                {
                    if (data.金額 <= 0)
                    {
                        continue;
                    }
                    合計金額 += data.金額;

                    xls.Cell(CellStr(NoCell)).Value = (RowNo - StartRow + 1);
                    xls.Cell(CellStr(船名Cell)).Value = data.船;
                    xls.Cell(CellStr(種別Cell)).Value = data.種別;
                    xls.Cell(CellStr(詳細種別Cell)).Value = data.詳細種別;
                    xls.Cell(CellStr(件名Cell)).Value = data.件名;
                    xls.Cell(CellStr(金額Cell)).Value = data.金額;
                    xls.Cell(CellStr(受領日Cell)).Value = data.受領日;
                    xls.Cell(CellStr(受領番号Cell)).Value = data.受領番号;
                    xls.Cell(CellStr(StatusCell)).Value = data.Status;
                    xls.Cell(CellStr(取引先Cell)).Value = data.取引先;
                    xls.Cell(CellStr(事務担当Cell)).Value = data.事務担当者;

                    // 下線
                    //xls.Cell("A" + (RowNo - 1).ToString() + ":K" + (RowNo - 1).ToString()).Attr.LineBottom (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("A" + (RowNo).ToString() + ":K" + (RowNo).ToString()).Attr.LineBottom (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                    RowNo++;
                }

                // 実線
                xls.Cell("A" + StartRow.ToString() + ":A" + (RowNo - 1).ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("A" + StartRow.ToString() + ":A" + (RowNo - 1).ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("B" + StartRow.ToString() + ":B" + (RowNo - 1).ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("C" + StartRow.ToString() + ":C" + (RowNo - 1).ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("D" + StartRow.ToString() + ":D" + (RowNo - 1).ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("E" + StartRow.ToString() + ":E" + (RowNo - 1).ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("F" + StartRow.ToString() + ":F" + (RowNo - 1).ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("G" + StartRow.ToString() + ":G" + (RowNo - 1).ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("H" + StartRow.ToString() + ":H" + (RowNo - 1).ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("I" + StartRow.ToString() + ":I" + (RowNo - 1).ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("J" + StartRow.ToString() + ":J" + (RowNo - 1).ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("K" + StartRow.ToString() + ":K" + (RowNo - 1).ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                //
                xls.Cell(年月Cell).Value = "対象年月：" + year.ToString() + "/" + month.ToString("00");
                string kigou = @"\";
                string value = kigou + 合計金額.ToString("###,###,###,###,###");
                xls.Cell(合計金額Cell).Value = "概算金額　 " + value;


                xls.CloseBook(true);
            }


            byte[] bytBuffer = null;
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

        #region プロパティ

        private string CellStr( string Prefix )
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(Prefix, RowNo);
            return sb.ToString();
        }
        private string 年月Cell = "A2";
        private string 合計金額Cell = "J2";

        private string NoCell = "A{0}";
        private string 船名Cell   = "B{0}";
        private string 種別Cell = "C{0}";
        private string 詳細種別Cell = "D{0}";
        private string 件名Cell = "E{0}";
        private string 金額Cell = "F{0}";
        private string 受領日Cell   = "G{0}";
        private string 受領番号Cell = "H{0}";
        private string StatusCell = "I{0}";
        private string 取引先Cell = "J{0}";
        private string 事務担当Cell = "K{0}";
        #endregion
    }
}
