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
using ExcelCreator = AdvanceSoftware.ExcelCreator;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_貯蔵品リスト_取得(NBaseData.DAC.MsUser loginUser, int year, int month, NBaseData.BLC.貯蔵品リスト.対象Enum outputKind);
        
        
        [OperationContract]
        List<NBaseData.BLC.貯蔵品リスト> BLC_貯蔵品編集_取得(NBaseData.DAC.MsUser loginUser, int sy, int sm, int ey, int em, int msVesselId, NBaseData.BLC.貯蔵品リスト.対象Enum outputKind);    
    }

    public partial class Service
    {
        public byte[] BLC_貯蔵品リスト_取得(NBaseData.DAC.MsUser loginUser, int year, int month, NBaseData.BLC.貯蔵品リスト.対象Enum outputKind)
        {
            #region 元になるファイルの確認と出力ファイル生成
            string BaseFileName = "貯蔵品リスト";
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

                // データ全部
                List<NBaseData.BLC.貯蔵品リスト> 潤滑油リストALL = NBaseData.BLC.貯蔵品リスト.GetRecords(loginUser, year, month, outputKind);

                // [MS_VESSEL]船名
                List<MsVessel> msVesselList = MsVessel.GetRecordsByHachuEnabled(new MsUser());
                foreach (MsVessel msVessel in msVesselList)
                {
                    // 1 番左のシート(シート番号 0)を、最終シート以降にコピー
                    int nSheetCount = xls.SheetCount;
                    xls.CopySheet(0, nSheetCount, msVessel.VesselName);
                    xls.SheetNo = nSheetCount;

                    // 船名
                    xls.Cell("B1").Value = msVessel.VesselName;
                    // 年月
                    xls.Cell("B2").Value = year.ToString() + "年" + month.ToString("00") + "月";

                    // 対象の船のデータを抜き出す
                    var 潤滑油リスト = from p in 潤滑油リストALL
                                 where p.MS_VESSEL_ID == msVessel.MsVesselID
                                 orderby p.ID, p.納品日, p.発注番号
                                 select p;

                    int start_y = 4;
                    int offset_y = 0;

                    string ID = "";
                    int 種別合計_offset_y = offset_y;
                    foreach (var row in 潤滑油リスト)
                    {
                        if (ID == "")
                        {
                            ID = row.ID;
                            種別合計_offset_y = offset_y;
                        }
                        else if (ID != row.ID)
                        {
                            xls.Cell("O4", 0, 種別合計_offset_y).Value = "=SUM(N" + (start_y + 種別合計_offset_y).ToString() + ":N" + (start_y + offset_y - 1).ToString() + ")";
                            ID = row.ID;
                            種別合計_offset_y = offset_y;
                        }
                        xls.Cell("A4", 0, offset_y).Value = row.品名;
                        xls.Cell("A" + (start_y + offset_y).ToString() + ":B" + (start_y + offset_y).ToString()).Attr.MergeCells = true;
                        xls.Cell("C4", 0, offset_y).Value = row.発注番号;
                        xls.Cell("D4", 0, offset_y).Value = row.業者名;
                        xls.Cell("E4", 0, offset_y).Value = row.納品日;
                        if (row.支払単価 > 0)
                        {
                            xls.Cell("F4", 0, offset_y).Value = row.支払数;
                            xls.Cell("H4", 0, offset_y).Value = row.支払単価;
                        }
                        else
                        {
                            xls.Cell("F4", 0, offset_y).Value = row.受領数;
                            xls.Cell("H4", 0, offset_y).Value = row.受領単価;
                        }
                        xls.Cell("I4", 0, offset_y).Value = "=F" + (start_y + offset_y).ToString() + "*H" + (start_y + offset_y).ToString();

                        xls.Cell("J4", 0, offset_y).Value = row.消費;
                        xls.Cell("L4", 0, offset_y).Value = "=F" + (start_y + offset_y).ToString() + "-J" + (start_y + offset_y).ToString();
                        xls.Cell("N4", 0, offset_y).Value = "=H" + (start_y + offset_y).ToString() + "*L" + (start_y + offset_y).ToString();

                       
                        xls.Cell("G4", 0, offset_y).Value = row.単位;
                        xls.Cell("K4", 0, offset_y).Value = row.単位;
                        xls.Cell("M4", 0, offset_y).Value = row.単位;

                        offset_y++;
                    }
                    xls.Cell("O4", 0, 種別合計_offset_y).Value = "=SUM(N" + (start_y + 種別合計_offset_y).ToString() + ":N" + (start_y + offset_y - 1).ToString() + ")";

                    #region 罫線を出力
                    for (int i = 0; i < offset_y; i++)
                    {
                        xls.Cell("A4:O4", 0, i).Attr.LineBottom (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    }
                    int val = start_y + offset_y - 1;
                    xls.Cell("A4:A" + val.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("C4:C" + val.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("D4:D" + val.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("E4:E" + val.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("F4:F" + val.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("G4:G" + val.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("H4:H" + val.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("I4:I" + val.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("J4:J" + val.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("K4:K" + val.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("L4:L" + val.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("M4:M" + val.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("N4:N" + val.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("O4:O" + val.ToString()).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    xls.Cell("O4:O" + val.ToString()).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                    #endregion

                    // 合計を出力
                    if (offset_y > 1)
                    {
                        xls.Cell("M4", 0, offset_y).Value = "合計";
                        xls.Cell("N4", 0, offset_y).Value = "=SUM(N4:N" + (start_y + offset_y - 1).ToString() + ")";
                    }
                }
                xls.DeleteSheet(0, 1);
                xls.CloseBook(true);
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

        public List<NBaseData.BLC.貯蔵品リスト> BLC_貯蔵品編集_取得(NBaseData.DAC.MsUser loginUser, int sy, int sm, int ey, int em, int msVesselId, NBaseData.BLC.貯蔵品リスト.対象Enum outputKind)
        {
            List<NBaseData.BLC.貯蔵品リスト> retList = new List<NBaseData.BLC.貯蔵品リスト>();
            DateTime st = new DateTime(sy, sm, 1);
            DateTime ed = new DateTime(ey, em, 1);

            for (DateTime tmp_ym = st; tmp_ym <= ed; tmp_ym = tmp_ym.AddMonths(1))
            {
                int tmp_y = tmp_ym.Year;
                int tmp_m = tmp_ym.Month;

                List<NBaseData.BLC.貯蔵品リスト> tmp_list = NBaseData.BLC.貯蔵品リスト.GetRecordsByMsVesselId(loginUser, tmp_y, tmp_m, outputKind, msVesselId);

                retList.AddRange(tmp_list);
            }
            return retList;
        }
     }
}
