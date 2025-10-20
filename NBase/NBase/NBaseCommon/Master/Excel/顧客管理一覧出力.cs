using ExcelCreator = AdvanceSoftware.ExcelCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;

namespace NBaseCommon.Master.Excel
{
    public class 顧客管理一覧出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;


        public 顧客管理一覧出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }

        public void CreateFile(MsUser loginUser, List<MsCustomer> customerList)
        {
            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                xls.OpenBook(outputFilePath, templateFilePath);

                _CreateFile(loginUser, xls, customerList);

                xls.CloseBook(true);
            }
        }

        private void _CreateFile(MsUser loginUser, ExcelCreator.Xlsx.XlsxCreator xls, List<MsCustomer> customerList)
        {
            xls.SheetNo = 0;

            int rowNo = 1;
            int startRowIndex = 2;
            int rowIndex = startRowIndex + 1;
            foreach (MsCustomer info in customerList)
            {
                xls.RowCopy(startRowIndex, rowIndex);


                int colIndex = 0;

                // 顧客No
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.MsCustomerID;
                colIndex++;

                // 顧客名
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.CustomerName;
                colIndex++;

                // 電話番号
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.Tel;
                colIndex++;

                // FAX
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.Fax;
                colIndex++;

                // 郵便番号
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.ZipCode;
                colIndex++;

                // 住所１
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.Address1;
                colIndex++;

                // 住所２
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.Address2;
                colIndex++;

                // 建物名
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.BuildingName;
                colIndex++;

                // 取引先
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.Is取引先() ? "○" : "";
                colIndex++;

                // 代理店
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.Is代理店() ? "○" : "";
                colIndex++;

                // 荷主
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.Is荷主() ? "○" : "";
                colIndex++;

                // 企業
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.Is企業() ? "○" : "";
                colIndex++;

                // 学校
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.Is学校() ? "○" : "";
                colIndex++;

                // 申請先
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.AppointedFlag == 1 ? "○" : "";
                colIndex++;

                // 検船実施会社
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.InspectionFlag == 1 ? "○" : "";
                colIndex++;

                // ログインID
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.LoginID;
                colIndex++;

                // パスワード
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.Password;
                colIndex++;

                // 銀行名
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.BankName;
                colIndex++;

                // 支店名
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.BranchName;
                colIndex++;

                // 口座名
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.AccountNo;
                colIndex++;

                // 口座名義
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.AccountId;
                colIndex++;

                // 校長先生名
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.Teacher1;
                colIndex++;

                // 進路指導部先生名
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.Teacher2;
                colIndex++;

                // 備考
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.Bikou;
                colIndex++;

                rowNo++;
                rowIndex++;
            }
            xls.RowDelete(startRowIndex, 1);
        }
    }



}
