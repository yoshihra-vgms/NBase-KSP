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
using NBaseData.BLC;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<貯蔵品編集RowData> BLC_Get貯蔵品(MsUser loginUser, MsVessel msVessel, 貯蔵品リスト.対象Enum kind, int FromYear, int FromMonth, int ToYear, int ToMonth);
        
        [OperationContract]
        byte[] BLC_Excel貯蔵品管理表_出力(MsUser loginUser, MsVessel msVessel, 貯蔵品リスト.対象Enum kind, int FromYear, int FromMonth, int ToYear, int ToMonth);

        [OperationContract]
        byte[] BLC_Excel貯蔵品年間管理表_出力(MsUser loginUser, MsVessel msVessel, 貯蔵品リスト.対象Enum kind, int year);
    }

    public partial class Service
    {
        public List<貯蔵品編集RowData> BLC_Get貯蔵品(MsUser loginUser, MsVessel msVessel, 貯蔵品リスト.対象Enum kind, int FromYear, int FromMonth, int ToYear, int ToMonth)
        {
            貯蔵品管理票 data = 貯蔵品管理票.Get貯蔵品管理票(loginUser, msVessel, kind, FromYear, FromMonth, ToYear, ToMonth);
            return data.datas;
        }

        public byte[] BLC_Excel貯蔵品管理表_出力(MsUser loginUser, MsVessel msVessel, 貯蔵品リスト.対象Enum kind, int FromYear, int FromMonth, int ToYear, int ToMonth)
        {
            string BaseFileName = "貯蔵品管理表";

            #region 元になるファイルの確認と出力ファイル生成
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateName = path + "Template_" + BaseFileName + ".xlsx";

            string outpath = NBaseUtil.FileUtils.CheckOutPath(path) + "\\";
            string outBaseFileName = BaseFileName + "_" + DateTime.Now.ToString("HHmmss");

            string outPutFileName = outpath + "outPut_[" + loginUser.FullName + "]_" + outBaseFileName + ".xlsx";
            
            // templateNameの存在有無の確認
            bool exists = System.IO.File.Exists(templateName);
            if (exists == false)
            {
                return null;
            }
            //----------------------------------
            //2013/12/24 コメントアウト
            //ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator();
            //xls.OpenBook(outPutFileName, templateName);
            //----------------------------------
            #endregion

            //----------------------------------
            //2013/12/24 変更：エラー処理を追加
            //ExcelCreator.Xlsx.XlsxCreator xls = null; 
            //using (ExcelCreator.Xlsx.XlsxCreator xls1 = new ExcelCreator.Xlsx.XlsxCreator())
            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                // 指定されたテンプレートを元にファイルを作成 OpenBookエラーをなげる
                //int xlsRet = xls1.OpenBook(outPutFileName, templateName);
                int xlsRet = xls.OpenBook(outPutFileName, templateName);
                if (xlsRet < 0)
                {
                    Exception xlsEx = null;
                    //if (xls1.ErrorNo == ExcelCreator.xlErrorNo.errTempCreate)
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
                //xls = xls1;
                //----------------------------------
 
                #region Excelファイルの編集

                //貯蔵品管理票 data = 貯蔵品管理票.Get貯蔵品管理票(loginUser, msVessel, kind, FromYear, FromMonth, ToYear, ToMonth);

                //bool ret = 貯蔵品管理票.Excel書込(ref xls, msVessel, kind, FromYear, FromMonth, ToYear, ToMonth, data.datas, data.yosan);

NBaseCommon.LogFile.Write("", "Kind=" + kind);
                if (msVessel != null)
                {
                    貯蔵品管理票 data = 貯蔵品管理票.Get貯蔵品管理票(loginUser, msVessel, kind, FromYear, FromMonth, ToYear, ToMonth);

                    //bool ret = 貯蔵品管理票.Excel書込(ref xls, msVessel, kind, FromYear, FromMonth, ToYear, ToMonth, data.datas, data.yosan);
                    bool ret = 貯蔵品管理票.Excel書込(xls, msVessel, kind, FromYear, FromMonth, ToYear, ToMonth, data.datas, data.yosan);
                }
                else
                {
                    List<MsVessel> msVesselList = MsVessel.GetRecordsByHachuEnabled(new MsUser());
                    foreach (MsVessel vessel in msVesselList)
                    {
// DEBUG
//if (vessel.MsVesselID != 47) // 桃邦丸のみ
//    continue;
                        // 現在の総シート数を確認します
                        int nSheetCount = xls.SheetCount;

                        // 1 番左のシート(シート番号 0)を、最終シート以降にコピー
                        xls.CopySheet(0, nSheetCount, vessel.VesselName);
                        xls.SheetNo = nSheetCount;

                        貯蔵品管理票 data = 貯蔵品管理票.Get貯蔵品管理票(loginUser, vessel, kind, FromYear, FromMonth, ToYear, ToMonth);

                        //bool ret = 貯蔵品管理票.Excel書込(ref xls, vessel, kind, FromYear, FromMonth, ToYear, ToMonth, data.datas, data.yosan);
                        bool ret = 貯蔵品管理票.Excel書込(xls, vessel, kind, FromYear, FromMonth, ToYear, ToMonth, data.datas, data.yosan);
                    }
                    xls.DeleteSheet(0, 1);
                }

                #endregion

                #region Excel出力
                // Excel出力
                xls.CloseBook(true);

                if (xls.ErrorNo == ExcelCreator.ErrorNo.NoError)
                {
                    //res = true;
                }
                #endregion
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

        public byte[] BLC_Excel貯蔵品年間管理表_出力(MsUser loginUser, MsVessel msVessel, 貯蔵品リスト.対象Enum kind, int year)
        {
            string BaseFileName = "貯蔵品年間管理表";

            #region 元になるファイルの確認と出力ファイル生成
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateName = path + "Template_" + BaseFileName + ".xlsx";

            string outpath = NBaseUtil.FileUtils.CheckOutPath(path) + "\\";
            string outBaseFileName = BaseFileName + "_" + DateTime.Now.ToString("HHmmss");

            string outPutFileName = outpath + "outPut_[" + loginUser.FullName + "]_" + outBaseFileName + ".xlsx";
            
            // templateNameの存在有無の確認
            bool exists = System.IO.File.Exists(templateName);
            if (exists == false)
            {
                return null;
            }

            //----------------------------------
            //2013/12/24 コメントアウト
            //ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator();
            //xls.OpenBook(outPutFileName, templateName);
            //----------------------------------
            #endregion

            //----------------------------------
            //2013/12/24 変更：エラー処理を追加、Excelファイルの編集・Excel出力をusingの中に移動
            //ExcelCreator.Xlsx.XlsxCreator xls = null; 
            //using (ExcelCreator.Xlsx.XlsxCreator xls1 = new ExcelCreator.Xlsx.XlsxCreator())
            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                // 指定されたテンプレートを元にファイルを作成 OpenBookエラーをなげる
                //int xlsRet = xls1.OpenBook(outPutFileName, templateName);
                int xlsRet = xls.OpenBook(outPutFileName, templateName);
                if (xlsRet < 0)
                {
                    Exception xlsEx = null;
                    //if (xls1.ErrorNo == ExcelCreator.xlErrorNo.errTempCreate)
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
                //xls = xls1;
                //----------------------------------

                #region Excelファイルの編集

                if (msVessel != null)
                {
                    貯蔵品年間管理表 data = 貯蔵品年間管理表.Get貯蔵品年間管理表(loginUser, msVessel, kind, year);

                    貯蔵品年間管理表.Excel書込(xls, msVessel, kind, data.datas);
                }
                else
                {

                    List<MsVessel> msVesselList = MsVessel.GetRecordsByHachuEnabled(new MsUser());
                    foreach (MsVessel vessel in msVesselList)
                    {
//// DEBUG
//if (vessel.MsVesselID != 47) // 桃邦丸のみ
//    continue;
                        // 現在の総シート数を確認します
                        int nSheetCount = xls.SheetCount;
                        // 1 番左のシート(シート番号 0)を、最終シート以降にコピー
                        xls.CopySheet(0, nSheetCount, vessel.VesselName);
                        xls.SheetNo = nSheetCount;

                        貯蔵品年間管理表 data = 貯蔵品年間管理表.Get貯蔵品年間管理表(loginUser, vessel, kind, year);

                        貯蔵品年間管理表.Excel書込(xls, vessel, kind, data.datas);
                    }
                    xls.DeleteSheet(0, 1);
                }

                #endregion

                #region Excel出力
                // Excel出力
                xls.CloseBook(true);

                if (xls.ErrorNo == ExcelCreator.ErrorNo.NoError)
                {
                    //res = true;
                }
                #endregion
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
