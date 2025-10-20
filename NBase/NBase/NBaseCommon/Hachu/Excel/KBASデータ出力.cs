using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBaseData.DAC;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using System.Globalization;

namespace NBaseCommon.Hachu.Excel
{
    public class KBASデータ出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;

        private List<出力Data> OutputDatas = null;
        private List<仕分G> 仕分List = new List<仕分G>();

        public KBASデータ出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }


        public void CreateFile(MsUser loginUser, DateTime date, MsVessel vessel)
        {

            // 出力データ構築
            _BuildData(loginUser, date, vessel);


            // EXCELに吐き出す
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

                
                _CreateFile(loginUser, xls, date);


                // オープン時に再計算を実施するように設定
                xls.FullCalcOnLoad = true;
                xls.CloseBook(true);
            }
        }


        private void _CreateFile(MsUser loginUser, ExcelCreator.Xlsx.XlsxCreator xls, DateTime date)
        {

            xls.Cell("**NENGETSU").Value = date.Year + "年" + date.Month + "月";

            int startRow = 4;
            int i = 0;

            var sortedList = OutputDatas.OrderBy(o => o.決算日.ToShortDateString()).ThenBy(o => o.支払先).ThenBy(o => o.支払先).ThenBy(o => o.船表示順).ThenBy(o => o.船名);
            foreach (出力Data dt in sortedList)
            {
                //同一仕訳グループ
                xls.Cell("A" + (startRow + i)).Value = dt.同一仕訳グループ;

                //支払先
                //xls.Cell("B" + (startRow + i)).Value = dt.支払先;
                var AttrNo = xls.Cell("B" + (startRow + i)).AttrNo;
                xls.Cell("B" + (startRow + i)).Value2(dt.支払先, AttrNo);


                // 下記は、Excel内で設定する
                #region
                ////支払先コード
                //xls.Cell("C" + (startRow + i)).Value = dt.支払先コード;

                //// 支店コード
                //xls.Cell("D" + (startRow + i)).Value = dt.支店コード;

                //// 支払方法コード
                //xls.Cell("E" + (startRow + i)).Value = dt.支払方法コード;

                //// 銀行タイプ
                //xls.Cell("F" + (startRow + i)).Value = dt.銀行タイプ;

                //// 支払口座
                //xls.Cell("G" + (startRow + i)).Value = dt.支払口座;
                #endregion

                // 決算日
                xls.Cell("H" + (startRow + i)).Value = dt.決算日;

                // 船コード
                xls.Cell("I" + (startRow + i)).Value = dt.船コード;

                // 船名
                xls.Cell("J" + (startRow + i)).Value = dt.船名;

                // 管轄部門コード
                xls.Cell("K" + (startRow + i)).Value = dt.管轄部門コード;

                // 管轄部門
                xls.Cell("L" + (startRow + i)).Value = dt.管轄部門;

                // 下記は、Excel内で設定する
                #region
                //// 修繕項目コード
                //xls.Cell("M" + (startRow + i)).Value = dt.修繕項目コード;

                //// 修繕種別
                //xls.Cell("N" + (startRow + i)).Value = dt.修繕種別;

                //// 修繕項目
                //xls.Cell("O" + (startRow + i)).Value = dt.修繕項目;
                #endregion

                // 完工日
                xls.Cell("P" + (startRow + i)).Value = dt.完工日;

                // 下記は、Excel内で設定する
                #region
                //// 完工地コード
                //xls.Cell("Q" + (startRow + i)).Value = dt.完工地コード;

                //// 完工地
                //xls.Cell("R" + (startRow + i)).Value = dt.完工地;
                #endregion

                // 金額
                xls.Cell("S" + (startRow + i)).Value =dt.金額;

                // 消費税
                xls.Cell("T" + (startRow + i)).Value = dt.消費税;

                // 横罫線
                //xls.Cell("A" + (startRow + i) + ":I" + (startRow + i)).Attr.LineBottom(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                i++;
            }

        }



        protected void _BuildData(MsUser loginUser, DateTime date, MsVessel vessel)
        {
            List<MsVessel> vesselList = MsVessel.GetRecordsByHachuEnabled(loginUser);
            List<MsVesselSection> vesselSectionList = MsVesselSection.GetRecords(loginUser);


            //検索する月の範囲　日を取得
            DateTime fromDate = date;
            DateTime toDate = date.AddMonths(1);

            //支払いデータ取得、船、年月でフィルター
            NBaseData.DS.OdThiFilter filter = new NBaseData.DS.OdThiFilter();

            if (vessel != null)
                filter.MsVesselID = vessel.MsVesselID;

            List<OdShr> wkList = OdShr.GetRecordsByFilter(loginUser, int.MinValue, filter);
            List<OdShr> shrList = null;

            if (wkList != null)
            {
                var list1 = wkList.Where(obj => (obj.ShrIraiDate >= fromDate && obj.ShrIraiDate < toDate));

                if (vessel != null)
                {
                    shrList = list1.ToList();
                }
                else
                {
                    var vesselIds = vesselList.Select(o => o.MsVesselID);

                    shrList = list1.Where(o => vesselIds.Contains(o.OdThi_MsVesselID)).ToList();
                }
            }
            if (shrList == null)
            {
                shrList = new List<OdShr>();
            }



            OutputDatas = new List<出力Data>();



            foreach (OdShr shrobj in shrList)
            {
                OdJry 対象受領 = null;
                OdMk 対象見積回答 = null;

                対象受領 = OdJry.GetRecord(loginUser, shrobj.OdJryID);
                対象見積回答 = OdMk.GetRecord(loginUser, 対象受領.OdMkID);
                
                出力Data dt = new 出力Data();

                #region 顧客情報

                if (対象見積回答 != null && 対象見積回答.MsCustomerID!=null)
                {
                    MsCustomer customer = MsCustomer.GetRecord(loginUser, 対象見積回答.MsCustomerID);

                    dt.支払先 = customer.CustomerName;
                    dt.支払先コード = "";
                    dt.支店コード="";
                    dt.支払方法コード="";
                    dt.銀行タイプ="";
                    dt.支払口座 = "";// customer.bank_name ,customer.BranchName;
                }
                #endregion


                dt.決算日 = shrobj.ShrIraiDate;// 「支払画面：請求書日」

                #region 船
                if (対象見積回答 != null)
                {
                    var v = vesselList.Where(o => o.MsVesselID == 対象見積回答.MsVesselID).FirstOrDefault();
                    dt.船コード = v != null ? v.KbasVesselCode : "";
                    dt.船名 = 対象見積回答.MsVesselName;
                    dt.管轄部門 = "";
                    dt.管轄部門コード = "";
                    if (v != null)
                    {
                        if (vesselSectionList.Any(o => o.MsVesselSectionID == v.MsVesselSectionID))
                        {
                            dt.管轄部門 = vesselSectionList.Where(o => o.MsVesselSectionID == v.MsVesselSectionID).First().VesselSectionName;
                            dt.管轄部門コード = vesselSectionList.Where(o => o.MsVesselSectionID == v.MsVesselSectionID).First().MsVesselSectionID;
                        }
                    }
                    dt.船表示順 = v != null ? v.ShowOrder : 0;
                }
                #endregion

                #region 修繕情報
                
                dt.修繕項目コード="";
                dt.修繕種別="";
                dt.修繕項目 = "";

                #endregion

                dt.完工日 = 対象受領.JryDate;// 「受領画面：受領日」
                dt.完工地コード="";
                dt.完工地="";
                dt.金額 = shrobj.Amount;
                dt.消費税 = shrobj.Tax;

                OutputDatas.Add(dt);
            }


            #region 仕訳グループセットするための処理
            if (OutputDatas.Count > 0)
            {
                var sortedList = OutputDatas.OrderBy(o => o.決算日.ToShortDateString()).ThenBy(o => o.支払先);

                foreach (出力Data d in sortedList)
                {
                    if (仕分List.Any(o => o.支払先 == d.支払先 && o.決算日.ToString() == d.決算日.ToString()) == false)
                    {
                        仕分List.Add(new 仕分G(d.支払先, d.決算日));
                    }
                }


                int num = 1;
                foreach (仕分G s in 仕分List)
                {
                    var list = OutputDatas.Where(o => (o.支払先 == s.支払先 && o.決算日.ToString() == s.決算日.ToString())).ToList();

                    if (list.Count() <= 1) continue;

                    foreach (出力Data dt in list)
                    {
                        dt.同一仕訳グループ = num.ToString();
                    }
                    num++;
                }

            }


            #endregion
            
        }
    }

    public class 仕分G
    {
        public string 支払先 = "";
        public DateTime 決算日=DateTime.MinValue;

        public 仕分G(string 支払先, DateTime 決算日)
        {
            this.支払先 = 支払先;
            this.決算日 = 決算日;
        }
    }

    public class 出力Data
    {
        public string 同一仕訳グループ="";
        public string 支払先="";
        public string 支払先コード="";
        public string 支店コード="";
        public string 支払方法コード="";
        public string 銀行タイプ="";
        public string 支払口座="";
        public DateTime 決算日=DateTime.MinValue;
        public string 船コード="";
        public string 船名="";
        public int 船表示順 = 0;
        public string 管轄部門="";
        public string 管轄部門コード="";
        public string 修繕項目コード = "";
        public string 修繕種別 = "";
        public string 修繕項目 = "";
        public DateTime 完工日 = DateTime.MinValue;
        public string 完工地コード = "";
        public string 完工地 = "";
        public decimal 金額 = 0;
        public decimal 消費税 = 0;
    }
}
