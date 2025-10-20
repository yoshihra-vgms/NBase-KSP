using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ORMapping.Atts;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using NBaseData.DAC;
using NBaseData.DAC.DJ;
using NBaseData.DS;
//using Oracle.DataAccess.Client;
using System.IO;
using ExcelCreator = AdvanceSoftware.ExcelCreator;

namespace NBaseData.BLC.動静帳票
{
    public class 内航海運輸送実績調査票
    {
        public static readonly string TemplateName = "内航海運輸送実績調査票";
        private int LineCount = 1;
        private string 船種 = "高圧液化ガス船";
        private string 船種コード = "810";

        #region 元のコード
        //public byte[] 内航船舶輸送実績調査票取得(MsUser loginUser, DateTime Date)
        //{
        //    #region 元になるファイルの確認と出力ファイル生成
        //    string BaseFileName = TemplateName;
        //    string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
        //    string templateName = path + "Template_" + BaseFileName + ".xlsx";
        //    string outPutFileName = path + "outPut_[" + loginUser.FullName + "]_" + BaseFileName + ".xlsx";

        //    bool exists = System.IO.File.Exists(templateName);
        //    if (exists == false)
        //    {
        //        return null;
        //    }
        //    #endregion

        //    string JikoNo = Date.ToString("yyMM");

        //    using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
        //    {
        //        if (BaseFileName != null && BaseFileName.Length != 0)
        //        {
        //            // 指定されたテンプレートを元にファイルを作成
        //            xls.OpenBook(outPutFileName, templateName);
        //        }
        //        LineCount = 20;
        //        // 現在の総シート数を確認します
        //        int nSheetCount = xls.SheetCount;

        //        // 1 番左のシート(シート番号 0)を、最終シート以降にコピー
        //        xls.CopySheet(0, nSheetCount, Date.ToString("yy年MM月") + "調査票");
        //        xls.SheetNo = nSheetCount;

        //        #region 日付
        //        xls.Cell("**DATE").Str = DateTime.Today.ToString("yyyy/MM/dd");

        //        #endregion

        //        xls.Cell("**E_MAIL").Str = loginUser.MailAddress;
        //        xls.Cell("**担当者名").Str = loginUser.FullName;

        //        List<内航海運輸送実績調査票Data> Datas = 内航海運輸送実績調査票Data.集計(loginUser,JikoNo);
        //        List<内航海運輸送実績調査票Data> 荷主s = 内航海運輸送実績調査票Data.荷主取得(loginUser, JikoNo);
        //        List<内航海運輸送実績調査票Data> 自社船s = Get自社船(Datas);

        //        foreach (内航海運輸送実績調査票Data 自社船 in 自社船s)
        //        {
        //            xls.Cell(調査年Cell).Str = Date.ToString("yyyy");
        //            xls.Cell(調査月Cell).Str = Date.ToString("MM");
        //            //xls.Cell(品目Cell).Str = 自社船.CargoName;
        //            //xls.Cell(品目コードCell).Str = 自社船.CargoNo;
        //            //xls.Cell(船種Cell).Str = 船種;
        //            //xls.Cell(船種コードCell).Str = 船種コード;
        //            xls.Cell(品目Cell).Str = 自社船.YusoItemName;
        //            xls.Cell(品目コードCell).Str = 自社船.YusoItemCode;
        //            xls.Cell(船種Cell).Str = 自社船.SenshuName;
        //            xls.Cell(船種コードCell).Str = 自社船.SenshuCode;
        //            xls.Cell(使用船舶月間輸送数量トンCell).Value = 自社船.Qtty;

        //            内航海運輸送実績調査票Data 他船 = Get他船(Datas, 自社船.CargoNo);
        //            if (他船 != null)
        //            {
        //                xls.Cell(ﾄﾘｯﾌﾟ船舶月間輸送数量トンCell).Value = 他船.Qtty;
        //            }
        //            //xls.Cell(主な荷主Cell).Value = 自社船.Ninushi;
        //            xls.Cell(主な荷主Cell).Value = Get荷主(荷主s, 自社船.YusoItemCode);

        //            LineCount++;
        //        }

        //        xls.DelSheet(0, 1);
        //        xls.CloseBook(true);

        //        if (xls.ErrorNo == ExcelCreator.xlErrorNo.errNoError)
        //        {
        //            //res = true;
        //        }
        //    }

        //    byte[] bytBuffer;
        //    #region バイナリーへ変換
        //    using (FileStream objFileStream = new FileStream(outPutFileName, FileMode.Open))
        //    {
        //        long lngFileSize = objFileStream.Length;

        //        bytBuffer = new byte[(int)lngFileSize];
        //        objFileStream.Read(bytBuffer, 0, (int)lngFileSize);
        //        objFileStream.Close();
        //    }
        //    #endregion

        //    return bytBuffer;
        //}
        #endregion

        public byte[] 内航船舶輸送実績調査票取得(MsUser loginUser, DateTime Date, List<内航海運輸送実績調査票Data> datas)
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

            string JikoNo = Date.ToString("yyMM");

            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                if (BaseFileName != null && BaseFileName.Length != 0)
                {
                    // 指定されたテンプレートを元にファイルを作成
                    xls.OpenBook(outPutFileName, templateName);
                }
                LineCount = 20;
                // 現在の総シート数を確認します
                int nSheetCount = xls.SheetCount;

                // 1 番左のシート(シート番号 0)を、最終シート以降にコピー
                xls.CopySheet(0, nSheetCount, Date.ToString("yy年MM月") + "調査票");
                xls.SheetNo = nSheetCount;

                #region 日付
                xls.Cell("**DATE").Str = DateTime.Today.ToString("yyyy/MM/dd");

                #endregion

                xls.Cell("**E_MAIL").Str = loginUser.MailAddress;
                xls.Cell("**担当者名").Str = loginUser.FullName;

                List<内航海運輸送実績調査票Data> Datas = 集計(datas);
                List<内航海運輸送実績調査票Data> 荷主s = 荷主取得(datas);
                List<内航海運輸送実績調査票Data> 自社船s = Get自社船(Datas);

                foreach (内航海運輸送実績調査票Data 自社船 in 自社船s)
                {
                    xls.Cell(調査年Cell).Str = Date.ToString("yyyy");
                    xls.Cell(調査月Cell).Str = Date.ToString("MM");
                    xls.Cell(品目Cell).Str = 自社船.YusoItemName;
                    xls.Cell(品目コードCell).Str = 自社船.YusoItemCode;
                    xls.Cell(船種Cell).Str = 自社船.SenshuName;
                    xls.Cell(船種コードCell).Str = 自社船.SenshuCode;
                    xls.Cell(使用船舶月間輸送数量トンCell).Value = 自社船.Qtty;

                    内航海運輸送実績調査票Data 他船 = Get他船(Datas, 自社船.CargoNo);
                    if (他船 != null)
                    {
                        xls.Cell(ﾄﾘｯﾌﾟ船舶月間輸送数量トンCell).Value = 他船.Qtty;
                    }
                    xls.Cell(主な荷主Cell).Value = Get荷主(荷主s, 自社船.YusoItemCode);

                    LineCount++;
                }

                xls.DeleteSheet(0, 1);
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

        private List<内航海運輸送実績調査票Data> 集計(List<内航海運輸送実績調査票Data> Datas)
        {
            List<内航海運輸送実績調査票Data>retDatas = new List<内航海運輸送実績調査票Data>();
            foreach(内航海運輸送実績調査票Data d in Datas)
            {
                var same = retDatas.Where(obj => obj.YusoItemCode == d.YusoItemCode && obj.YusoItemName == d.YusoItemName && obj.SenshuCode == d.SenshuCode && obj.SenshuName == d.SenshuName);
                if (same.Count() > 0)
                {
                    same.First().Qtty += d.Qtty;
                }
                else
                {
                    retDatas.Add(内航海運輸送実績調査票Data.Clone(d)); 
                }
            }

            return retDatas.ToList<内航海運輸送実績調査票Data>();
        }

        private List<内航海運輸送実績調査票Data> 荷主取得(List<内航海運輸送実績調査票Data> Datas)
        {
            List<内航海運輸送実績調査票Data> retDatas = new List<内航海運輸送実績調査票Data>();
            foreach (内航海運輸送実績調査票Data d in Datas)
            {
                var same = retDatas.Where(obj => obj.YusoItemCode == d.YusoItemCode && obj.CargoName == d.CargoName && obj.Ninushi == d.Ninushi && obj.ProfitKbn == d.ProfitKbn);
                if (same.Count() > 0)
                {
                    same.First().Qtty += d.Qtty;
                }
                else
                {
                    retDatas.Add(内航海運輸送実績調査票Data.Clone(d));
                }
            }

            return retDatas.ToList<内航海運輸送実績調査票Data>();
        }



        private List<内航海運輸送実績調査票Data> Get自社船(List<内航海運輸送実績調査票Data>Datas)
        {
            var data = from p in Datas
                       where p.ProfitKbn == "1"
                       orderby p.CargoNo
                       select p;

            return data.ToList<内航海運輸送実績調査票Data>();
        }

        private string Get荷主(List<内航海運輸送実績調査票Data> Datas, string YusoItemCode)
        {
            var data = from p in Datas
                       where p.ProfitKbn == "1" && p.YusoItemCode == YusoItemCode
                       orderby p.Qtty descending
                       select p;

            string ninushi = "";
            if (data.Count<内航海運輸送実績調査票Data>() > 0)
            {
                ninushi = (data.First<内航海運輸送実績調査票Data>()).Ninushi;
            }
            return ninushi;
        }

        private 内航海運輸送実績調査票Data Get他船(List<内航海運輸送実績調査票Data>Datas,string CargoNo)
        {
            var data = from p in Datas
                       where p.ProfitKbn == "4" && p.CargoNo == CargoNo
                       select p;

            if (data.Count<内航海運輸送実績調査票Data>() > 0)
            {
                return data.First<内航海運輸送実績調査票Data>();
            }
            else
            {
                return null;
            }
        }

        #region プロパティ
        private string 調査年Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("B{0}", LineCount);
                return sb.ToString();
            }
        }
        private string 調査月Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("C{0}", LineCount);
                return sb.ToString();
            }
        }
        private string 品目Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("D{0}", LineCount);
                return sb.ToString();
            }
        }
        private string 品目コードCell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("E{0}", LineCount);
                return sb.ToString();
            }
        }
        private string 船種Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("F{0}", LineCount);
                return sb.ToString();
            }
        }
        private string 船種コードCell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("G{0}", LineCount);
                return sb.ToString();
            }
        }
        private string 使用船舶月間輸送数量トンCell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("H{0}", LineCount);
                return sb.ToString();
            }
        }
        private string 使用船舶月間輸送数量キロリットルCell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("I{0}", LineCount);
                return sb.ToString();
            }
        }
        private string ﾄﾘｯﾌﾟ船舶月間輸送数量トンCell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("J{0}", LineCount);
                return sb.ToString();
            }
        }
        private string ﾄﾘｯﾌﾟ船舶月間輸送数量キロリットルCell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("K{0}", LineCount);
                return sb.ToString();
            }
        }
        private string 主な荷主Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("L{0}", LineCount);
                return sb.ToString();
            }
        }
        #endregion
    }
}
