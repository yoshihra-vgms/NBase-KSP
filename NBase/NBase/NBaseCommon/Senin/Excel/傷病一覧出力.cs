using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseUtil;
using NBaseData.DS;
using NBaseCommon.Senin.Excel.util;

namespace NBaseCommon.Senin.Excel
{
    public class 傷病一覧出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;


        private Dictionary<string, int> rowIndexDic;



        public 傷病一覧出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }


        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache, DateTime fromDate, DateTime toDate, int msSiShokumeiId, int msSeninId)
        {
            rowIndexDic = new Dictionary<string, int>();
            
            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
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


                _CreateFile(loginUser, xls, seninTableCache, fromDate, toDate, msSiShokumeiId, msSeninId);
   

                xls.CloseBook(true);
            }
        }


        private void _CreateFile(MsUser loginUser, ExcelCreator.Xlsx.XlsxCreator xls, SeninTableCache seninTableCache, DateTime fromDate, DateTime toDate, int msSiShokumeiId, int msSeninId)
        {
            int lineNo = 0;
            int rowOffset = 3;

            List<MsSenin> seninList = MsSenin.GetRecords(loginUser);
            List<SiShobyo> shobyoList = SiShobyo.SearchRecords(loginUser, fromDate, toDate, msSiShokumeiId, msSeninId);

            foreach (SiShobyo shobyo in shobyoList)
            {
                if (seninList.Any(obj => obj.MsSeninID == shobyo.MsSeninID) == false)
                    continue;

                xls.RowCopy(1, rowOffset-1);

                var senin = seninList.Where(obj => obj.MsSeninID == shobyo.MsSeninID).First();

                // No
                xls.Cell("A" + rowOffset).Value = ++lineNo;

                // 氏名コード
                xls.Cell("B" + rowOffset).Value = senin.ShimeiCode;

                // 氏名
                xls.Cell("C" + rowOffset).Value = senin.Sei + " " + senin.Mei;

                // 保険番号
                int hokenNo = 0;
                if (int.TryParse(senin.HokenNo, out hokenNo))
                {
                    xls.Cell("D" + rowOffset).Value = hokenNo;
                }
                else
                {
                    xls.Cell("D" + rowOffset).Value = senin.HokenNo;
                }

                // 等級
                xls.Cell("E" + rowOffset).Value = shobyo.Tokyu;

                // 日額
                xls.Cell("F" + rowOffset).Value = shobyo.Nitigaku;

                // ステータス
                xls.Cell("G" + rowOffset).Value = SiShobyo.STATUS[shobyo.Status];

                // 対象期間
                if (shobyo.FromDate != DateTime.MinValue)
                    xls.Cell("H" + rowOffset).Value = DateTime.Parse(shobyo.FromDate.ToShortDateString());

                if (shobyo.ToDate != DateTime.MinValue)
                    xls.Cell("I" + rowOffset).Value = DateTime.Parse(shobyo.ToDate.ToShortDateString());

                // 船名
                xls.Cell("J" + rowOffset).Value = seninTableCache.GetMsVesselName(loginUser, shobyo.MsVesselID);

                // 現職名
                xls.Cell("K" + rowOffset).Value = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, senin.MsSiShokumeiID);
                
                // 傷病名
                xls.Cell("L" + rowOffset).Value = shobyo.ShobyoName;

                // 口座
                xls.Cell("M" + rowOffset).Value = shobyo.Kouza;

                // 書類送付日
                if (shobyo.SendDocument != DateTime.MinValue)
                    xls.Cell("N" + rowOffset).Value = DateTime.Parse(shobyo.SendDocument.ToShortDateString());

                // 書類返送
                if (shobyo.DocumentReturn != DateTime.MinValue)
                    xls.Cell("O" + rowOffset).Value = DateTime.Parse(shobyo.DocumentReturn.ToShortDateString());

                // 提出日
                if (shobyo.FilingDate != DateTime.MinValue)
                    xls.Cell("P" + rowOffset).Value = DateTime.Parse(shobyo.FilingDate.ToShortDateString());
                
                // 通知
                if (shobyo.Notification != DateTime.MinValue)
                    xls.Cell("Q" + rowOffset).Value = DateTime.Parse(shobyo.Notification.ToShortDateString());
                
                // 立替金伝票
                if (shobyo.AdvanceVoucher != DateTime.MinValue)
                    xls.Cell("R" + rowOffset).Value = DateTime.Parse(shobyo.AdvanceVoucher.ToShortDateString());
                
                // 入金伝票
                if (shobyo.DepositSlip != DateTime.MinValue)
                    xls.Cell("S" + rowOffset).Value = DateTime.Parse(shobyo.DepositSlip.ToShortDateString());
                
                // 送金
                if (shobyo.MoneyTransfer != DateTime.MinValue)
                    xls.Cell("T" + rowOffset).Value = DateTime.Parse(shobyo.MoneyTransfer.ToShortDateString());
                
                // 本人郵送
                if (shobyo.MailToPrincipal != DateTime.MinValue)
                    xls.Cell("U" + rowOffset).Value = DateTime.Parse(shobyo.MailToPrincipal.ToShortDateString()); ;
    

                rowOffset++;
            }

            xls.RowDelete(1, 1);

        }
    }
}
