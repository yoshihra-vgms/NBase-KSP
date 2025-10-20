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
    public class 健康診断一覧出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;


        private Dictionary<string, int> rowIndexDic;



        public 健康診断一覧出力(string templateFilePath, string outputFilePath)
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
            int rowOffset = 6;

            //List<MsSenin> seninList = MsSenin.GetRecords(loginUser);
            //List<SiKenshin> kenshinList = SiKenshin.SearchRecords(loginUser, fromDate, toDate, msSiShokumeiId, msSeninId);

            //foreach (SiKenshin kenshin in kenshinList)
            //{
            //    if (seninList.Any(obj => obj.MsSeninID == kenshin.MsSeninID) == false)
            //        continue;

            //    xls.RowCopy(4, rowOffset-1);

            //    var senin = seninList.Where(obj => obj.MsSeninID == kenshin.MsSeninID).First();
            //    if (senin.RetireFlag == 1)
            //    {
            //        xls.Cell("A" + rowOffset + ":K" + rowOffset).Attr.BackColor = System.Drawing.Color.FromArgb(192, 192,192);  // 25%灰色
            //    }  

            //    // 保険番号
            //    int hokenNo = 0;
            //    if (int.TryParse(senin.HokenNo, out hokenNo))
            //    {
            //        xls.Cell("A" + rowOffset).Value = hokenNo;
            //    }
            //    else
            //    {
            //        xls.Cell("A" + rowOffset).Value = senin.HokenNo;
            //    }

            //    // 氏名
            //    xls.Cell("B" + rowOffset).Value = senin.Sei + " " + senin.Mei;

            //    // 現職名
            //    xls.Cell("C" + rowOffset).Value = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, senin.MsSiShokumeiID);

            //    // 種別
            //    xls.Cell("D" + rowOffset).Value = SiKenshin.KIND[kenshin.Kind];

            //    // 受信日
            //    xls.Cell("E" + rowOffset).Value = kenshin.ConsultationDate != DateTime.MinValue ? kenshin.ConsultationDate.ToShortDateString() : "";

            //    // 有効期限
            //    xls.Cell("F" + rowOffset).Value = kenshin.ExpirationDate != DateTime.MinValue ? kenshin.ExpirationDate.ToShortDateString() : "";
            //    if (kenshin.ExpirationDate != DateTime.MinValue && kenshin.ExpirationDate < DateTime.Today)
            //    {
            //        xls.Cell("F" + rowOffset).Attr.BackColor = System.Drawing.Color.FromArgb(255, 153, 0);  // 薄いオレンジ
            //    }    
         

            //    // 結果
            //    xls.Cell("G" + rowOffset).Value = SiKenshin.RESULT[kenshin.Result];

            //    // kenshin.Result == 0 "異常なし"

            //    if (kenshin.Result == 1) // "経過観察"
            //    {
            //        xls.Cell("G" + rowOffset).Attr.BackColor = System.Drawing.Color.FromArgb(255, 255, 153);  // 薄い黄色
            //    }
            //    else if (kenshin.Result == 2) // "要精密検査"
            //    {
            //        //2021/09/03 m.yoshihara もともと　(xlColor)29;コラールとあった。本当にコーラルなのか。29は濃い紫では？
            //        xls.Cell("G" + rowOffset).Attr.BackColor = System.Drawing.Color.FromArgb(255, 128, 128);  // コラール
            //    }
            //    else if (kenshin.Result == 3) // "要治療"
            //    {
            //        xls.Cell("G" + rowOffset).Attr.BackColor = System.Drawing.Color.FromArgb(255, 255, 153);  // 薄い黄色
            //    }
            //    else if (kenshin.Result == 4) // "治療中"
            //    {
            //        xls.Cell("G" + rowOffset).Attr.BackColor = System.Drawing.Color.FromArgb(204, 255, 204);  // 薄い緑
            //    }

            //    xls.Cell("H" + rowOffset).Value = kenshin.ResultDatail;

            //    // 生年月日
            //    xls.Cell("I" + rowOffset).Value = senin.Birthday != DateTime.MinValue ? senin.Birthday.ToShortDateString() : "";

            //    // 年齢
            //    xls.Cell("J" + rowOffset).Value = DateTimeUtils.GetElapsedYears(senin.Birthday, DateTime.Today);
            //    xls.Cell("K" + rowOffset).Value = DateTimeUtils.GetElapsedMonths(senin.Birthday, DateTime.Today) % 12;

            //    rowOffset++;
            //}

            //xls.RowDelete(4, 1);

        }
    }
}
