using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using ExcelCreator = AdvanceSoftware.ExcelCreator;

namespace NBaseCommon.Senin.Excel
{
    public class 送金通知出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;
        
        
        public 送金通知出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }
        
        
        public void CreateFile(MsUser loginUser, string siSoukinId)
        {
            SiSoukin soukin = SiSoukin.GetRecord(loginUser, siSoukinId);
            MsCustomer customer = MsCustomer.GetRecord(loginUser, soukin.MsCustomerID);
            
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

                xls.Cell("**TODAY").Value = DateTime.Now.ToShortDateString();
                xls.Cell("**CUSTOMER").Value = customer.CustomerName;
                xls.Cell("**FAX").Value = customer.Fax;
                xls.Cell("**SOUKIN_USER_GROUP").Value = MsUser.GetRecordsByUserID(loginUser, soukin.SoukinUserID).BumonName;
                xls.Cell("**SOUKIN_USER").Value = soukin.SoukinUserName;
                xls.Cell("**VESSEL").Value = MsVessel.GetRecordByMsVesselID(loginUser, soukin.MsVesselID).VesselName;
                xls.Cell("**SOUKIN_DATE").Value = soukin.SoukinDate.ToShortDateString();
                xls.Cell("**KINGAKU").Value = soukin.Kingaku + "円";
                xls.Cell("**BANK").Value = customer.BankName + " " + customer.BranchName + " " + customer.AccountNo;

                // 10,000 円
                int f = 金額セル設定(xls, "AC29", soukin.Kingaku, 10000);
                //  5,000 円
                f = 金額セル設定(xls, "AC30", f, 5000);
                //  1,000 円
                f = 金額セル設定(xls, "AC31", f, 1000);
                //    500 円
                f = 金額セル設定(xls, "AC32", f, 500);
                //    100 円
                f = 金額セル設定(xls, "AC33", f, 100);
                //     50 円
                f = 金額セル設定(xls, "AC34", f, 50);
                //     10 円
                f = 金額セル設定(xls, "AC35", f, 10);
                //      5 円
                f = 金額セル設定(xls, "AC36", f, 5);
                //      1 円
                f = 金額セル設定(xls, "AC37", f, 1);
                // 合計
                金額セル設定(xls, "AC38", soukin.Kingaku, 1);

                xls.CloseBook(true);
            }
        }


        private int 金額セル設定(ExcelCreator.Xlsx.XlsxCreator xls, string baseCell, int kingaku, int d)
        {
            string k = (kingaku / d * d).ToString();
            for (int i = k.Length - 1; i >= 0; i--)
            {
                xls.Cell(baseCell, (i + 1) - k.Length, 0).Value = k[i].ToString();
            }

            return kingaku % d;
        }
    }
}
