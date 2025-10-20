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
using NBaseData.DS;
//using Oracle.DataAccess.Client;
using System.IO;
using ExcelCreator = AdvanceSoftware.ExcelCreator;

namespace NBaseData.BLC.動静帳票
{
    public class 内航船舶輸送実績調査票
    {
        public static readonly string TemplateName = "内航船舶輸送実績調査票";
        private int LineCount = 1;
        private List<MsVessel> msVessels;

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

        //    msVessels = MsVessel.GetRecords(loginUser);
        //    List<TKJSHIP> Ships = TKJSHIP.GetRecords(loginUser);
        //    List<MsCargo> Cargos = MsCargo.GetRecords(loginUser);
        //    List<MsCargoYusoItem> YusoItems = MsCargoYusoItem.GetRecordsByJikoNo(loginUser, Date.ToString("yyMM"));

        //    using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
        //    {
        //        if (BaseFileName != null && BaseFileName.Length != 0)
        //        {
        //            // 指定されたテンプレートを元にファイルを作成
        //            xls.OpenBook(outPutFileName, templateName);
        //        }
        //        foreach (TKJSHIP ship in Ships)
        //        {
        //            if (内航船舶輸送実績調査票船(ship.FuneNo) == true)
        //            {
        //                LineCount = 1;
        //                // 現在の総シート数を確認します
        //                int nSheetCount = xls.SheetCount;

        //                // 1 番左のシート(シート番号 0)を、最終シート以降にコピー
        //                xls.CopySheet(0, nSheetCount, ship.VesselName);
        //                xls.SheetNo = nSheetCount;

        //                List<TKJNAIPLAN> LdDsSetNos = TKJNAIPLAN.GetRecordsByVesselVoyageNo(loginUser, ship.FuneNo, Date.ToString("yyMM"));
        //                string 前月港NO = "";

        //                xls.Cell("**作成者").Str = loginUser.FullName;

        //                #region 日付
        //                // 2010.06.16 川崎さんのメール指示により変更
        //                //xls.Cell("**YEAR").Str = DateTime.Today.ToString("yyyy");
        //                //xls.Cell("**MONTH").Str = DateTime.Today.ToString("MM");
        //                xls.Cell("**YEAR").Str = Date.ToString("yyyy");
        //                xls.Cell("**MONTH").Str = Date.ToString("MM");
        //                #endregion

        //                #region 船情報
        //                xls.Cell("**VESSEL_NAME").Str = ship.VesselName;
        //                {
        //                    int StartColumn = 8 - ship.OfficialNumber.Length;
        //                    int cnt = 0;
        //                    for (int i = StartColumn; i <= 7; i++)
        //                    {
        //                        xls.Cell("**VESSEL_NO" + i.ToString()).Str = ship.OfficialNumber[cnt++].ToString();
        //                    }
        //                }
        //                // 201410月度改造：総トン数にはGRTを出力する
        //                //if (ship.DWT > int.MinValue)
        //                if (ship.GRT > int.MinValue)
        //                {
        //                    //xls.Cell("**DWT").Value = ship.DWT;
        //                    xls.Cell("**DWT").Value = ship.GRT;
        //                }
        //                if (ship.CargoWeight > decimal.MinValue)
        //                {
        //                    xls.Cell("**CARGO_WEIGHT").Value = ship.CargoWeight;
        //                }
        //                #endregion

        //                #region 前月最後の港を取得
        //                {
        //                    string 前月VoyageNo = Date.AddMonths(-1).ToString("yyMM");
        //                    TKJNAIPLAN 前月港TKJNAIPLAN = TKJNAIPLAN.GetRecordBy前月港(loginUser, ship.FuneNo, 前月VoyageNo);
        //                    if (前月港TKJNAIPLAN != null)
        //                    {
        //                        List<TKJNAIPLAN> 前月LineDatas = TKJNAIPLAN.GetRecordsByLdDsSetNo(loginUser, ship.FuneNo, 前月港TKJNAIPLAN.LdDsSetNo);

        //                        TKJNAIPLAN Age1 = null;

        //                        #region 揚げ1を取得
        //                        var AgeDatas = from l in 前月LineDatas
        //                                       where l.SgKbn == "2"

        //                                       orderby l.ScYmd, l.KomaNo
        //                                       select l;
        //                        if (AgeDatas.Count<TKJNAIPLAN>() > 0)
        //                        {
        //                            Age1 = AgeDatas.First<TKJNAIPLAN>();
        //                        }
        //                        #endregion
        //                        if (Age1 != null)
        //                        {
        //                            前月港NO = Age1.PortNo;
        //                        }
        //                    }
        //                }

        //                #endregion

        //                foreach (TKJNAIPLAN LdDsSetNo in LdDsSetNos)
        //                {
        //                    List<TKJNAIPLAN> LineDatas = TKJNAIPLAN.GetRecordsByLdDsSetNo(loginUser, LdDsSetNo.FuneNo, LdDsSetNo.LdDsSetNo);

        //                    if (Is外港(LineDatas) == false)
        //                    {
        //                        //内地の場合
        //                        TKJNAIPLAN Tsumi1 = null;
        //                        TKJNAIPLAN Age1 = null;

        //                        #region 積み1を取得
        //                        var TsumiDatas = from l in LineDatas
        //                                         where l.SgKbn == "1"

        //                                         orderby l.ScYmd, l.KomaNo
        //                                         select l;
        //                        if (TsumiDatas.Count<TKJNAIPLAN>() > 0)
        //                        {
        //                            Tsumi1 = TsumiDatas.First<TKJNAIPLAN>();
        //                        }
        //                        #endregion

        //                        #region 揚げ1を取得
        //                        var AgeDatas = from l in LineDatas
        //                                       where l.SgKbn == "2"

        //                                       orderby l.ScYmd, l.KomaNo
        //                                       select l;
        //                        if (AgeDatas.Count<TKJNAIPLAN>() > 0)
        //                        {
        //                            Age1 = AgeDatas.First<TKJNAIPLAN>();
        //                        }
        //                        #endregion

        //                        #region 積み
        //                        MsBasho TsumiBasho = MsBasho.GetRecord(loginUser, Tsumi1.PortNo);
        //                        xls.Cell(船に積んだ日Cell).Str = Tsumi1.ScYmd.ToString("MM月dd日");
        //                        if (TsumiBasho != null)
        //                        {
        //                            xls.Cell(積み港Cell).Str = TsumiBasho.BashoName;
        //                        }
        //                        else
        //                        {
        //                            xls.Cell(積み港Cell).Str = Tsumi1.PortNo;
        //                        }
        //                        #endregion

        //                        #region 揚げ
        //                        MsBasho AgeBasho = MsBasho.GetRecord(loginUser, Age1.PortNo);
        //                        xls.Cell(船から揚げた日Cell).Str = Age1.ScYmd.ToString("MM月dd日");
        //                        if (AgeBasho != null)
        //                        {
        //                            xls.Cell(揚げ港Cell).Str = AgeBasho.BashoName;
        //                        }
        //                        else
        //                        {
        //                            xls.Cell(揚げ港Cell).Str = Age1.PortNo;
        //                        }
        //                        #endregion

        //                        #region 貨物
        //                        List<TKJNAIPLAN_AMT_BILL> TKJNAIPLAN_AMT_BILLs = TKJNAIPLAN_AMT_BILL.GetRecords(loginUser, Tsumi1);
        //                        if (TKJNAIPLAN_AMT_BILLs.Count > 0)
        //                        {
        //                            //MsCargo cargo = MsCargo.GetRecord(loginUser, TKJNAIPLAN_AMT_BILLs[0].CargoNo);
        //                            //if (cargo != null)
        //                            //{
        //                            //    xls.Cell(貨物名Cell).Str = cargo.CargoName;
        //                            //}
        //                            //else
        //                            //{
        //                            //    xls.Cell(貨物名Cell).Str = TKJNAIPLAN_AMT_BILLs[0].CargoNo;
        //                            //}
        //                            string item = 輸送品目(YusoItems, TKJNAIPLAN_AMT_BILLs[0].CargoNo);
        //                            if (item != null)
        //                            {
        //                                xls.Cell(貨物名Cell).Str = item;
        //                            }
        //                            else
        //                            {
        //                                xls.Cell(貨物名Cell).Str = TKJNAIPLAN_AMT_BILLs[0].CargoNo;
        //                            }
        //                            xls.Cell(貨物数量Cell).Value = 貨物数量(loginUser, LineDatas);
        //                        }
        //                        #endregion

        //                        #region 距離を算出
        //                        MsBashoKyori kyori = MsBashoKyori.GetRecord(loginUser, Tsumi1.PortNo, Age1.PortNo);
        //                        if (kyori != null)
        //                        {
        //                            xls.Cell(距離Cell).Value = kyori.Kyori;
        //                        }
        //                        else
        //                        {
        //                            xls.Cell(距離Cell).Value = 0;
        //                        }
        //                        #endregion

        //                        MsBashoKyori SecondKyori = MsBashoKyori.GetRecord(loginUser, 前月港NO, Tsumi1.PortNo);
        //                        if (SecondKyori != null)
        //                        {
        //                            xls.Cell(前距離Cell).Value = SecondKyori.Kyori;
        //                        }
        //                        else
        //                        {
        //                            xls.Cell(前距離Cell).Value = 0;
        //                        }

        //                        前月港NO = Age1.PortNo;

        //                        LineCount++;
        //                    }
        //                }
        //                // 2010.06.14: 印刷範囲を設定する
        //                if (LineCount < 12)
        //                {
        //                    xls.PrintArea(0, 0, 59, 30);
        //                }
        //                else if (LineCount < 22)
        //                {
        //                    xls.PrintArea(0, 0, 59, 44);
        //                }
        //            }
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

        public byte[] 内航船舶輸送実績調査票取得(MsUser loginUser, DateTime Date, List<NBaseData.DAC.DJ.内航船舶輸送実績調査票Data> datas)
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

            msVessels = MsVessel.GetRecords(loginUser);
            //List<TKJSHIP> Ships = TKJSHIP.GetRecords(loginUser);
            List<MsCargo> Cargos = MsCargo.GetRecords(loginUser);
            List<MsCargoYusoItem> YusoItems = MsCargoYusoItem.GetRecordsByJikoNo(loginUser, Date.ToString("yyMM"));

            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                if (BaseFileName != null && BaseFileName.Length != 0)
                {
                    // 指定されたテンプレートを元にファイルを作成
                    xls.OpenBook(outPutFileName, templateName);
                }

                foreach (NBaseData.DAC.DJ.内航船舶輸送実績調査票Data data in datas)
                {
                    LineCount = 1;
                    // 現在の総シート数を確認します
                    int nSheetCount = xls.SheetCount;

                    // 1 番左のシート(シート番号 0)を、最終シート以降にコピー
                    xls.CopySheet(0, nSheetCount, data.TKJSHIP.VesselName);
                    xls.SheetNo = nSheetCount;

                    string 前月港NO = "";

                    xls.Cell("**作成者").Str = loginUser.FullName;

                    #region 日付
                    xls.Cell("**YEAR").Str = Date.ToString("yyyy");
                    xls.Cell("**MONTH").Str = Date.ToString("MM");
                    #endregion

                    #region 船情報
                    xls.Cell("**VESSEL_NAME").Str = data.TKJSHIP.VesselName;
                    {
                        int StartColumn = 8 - data.TKJSHIP.OfficialNumber.Length;
                        int cnt = 0;
                        for (int i = StartColumn; i <= 7; i++)
                        {
                            xls.Cell("**VESSEL_NO" + i.ToString()).Str = data.TKJSHIP.OfficialNumber[cnt++].ToString();
                        }
                    }
                    if (data.TKJSHIP.GRT > int.MinValue)
                    {
                        xls.Cell("**DWT").Value = data.TKJSHIP.GRT;
                    }
                    if (data.TKJSHIP.CargoWeight > decimal.MinValue)
                    {
                        xls.Cell("**CARGO_WEIGHT").Value = data.TKJSHIP.CargoWeight;
                    }
                    #endregion

                    #region 前月最後の港を取得
                    if (data.Details.Count() > 0)
                        前月港NO = data.Details[0].LastPortNo;
                    #endregion

                    foreach (NBaseData.DAC.DJ.内航船舶輸送実績調査票DetailData detail in data.Details)
                    {
                        #region 積み
                        MsBasho TsumiBasho = MsBasho.GetRecord(loginUser, detail.Tumi.PortNo);
                        xls.Cell(船に積んだ日Cell).Str = detail.Tumi.ScYmd.ToString("MM月dd日");
                        if (TsumiBasho != null)
                        {
                            xls.Cell(積み港Cell).Str = TsumiBasho.BashoName;
                        }
                        else
                        {
                            xls.Cell(積み港Cell).Str = detail.Tumi.PortNo;
                        }
                        #endregion

                        #region 揚げ
                        MsBasho AgeBasho = MsBasho.GetRecord(loginUser, detail.Age.PortNo);
                        xls.Cell(船から揚げた日Cell).Str = detail.Age.ScYmd.ToString("MM月dd日");
                        if (AgeBasho != null)
                        {
                            xls.Cell(揚げ港Cell).Str = AgeBasho.BashoName;
                        }
                        else
                        {
                            xls.Cell(揚げ港Cell).Str = detail.Age.PortNo;
                        }
                        #endregion

                        #region 貨物
                        if (detail.Tumini != null)
                        {
                            string item = 輸送品目(YusoItems, detail.Tumini.CargoNo);
                            if (item != null)
                            {
                                xls.Cell(貨物名Cell).Str = item;
                            }
                            else
                            {
                                xls.Cell(貨物名Cell).Str = detail.Tumini.CargoNo;
                            }
                            xls.Cell(貨物数量Cell).Value = detail.Qtty;
                        }
                        #endregion

                        #region 距離を算出
                        MsBashoKyori kyori = MsBashoKyori.GetRecord(loginUser, detail.Tumi.PortNo, detail.Age.PortNo);
                        if (kyori != null)
                        {
                            xls.Cell(距離Cell).Value = kyori.Kyori;
                        }
                        else
                        {
                            xls.Cell(距離Cell).Value = 0;
                        }
                        #endregion

                        MsBashoKyori SecondKyori = MsBashoKyori.GetRecord(loginUser, 前月港NO, detail.Tumi.PortNo);
                        if (SecondKyori != null)
                        {
                            xls.Cell(前距離Cell).Value = SecondKyori.Kyori;
                        }
                        else
                        {
                            xls.Cell(前距離Cell).Value = 0;
                        }

                        前月港NO = detail.Age.PortNo;

                        LineCount++;
                    }

                    // 2010.06.14: 印刷範囲を設定する
                    if (LineCount < 12)
                    {
                        xls.PrintArea(0, 0, 59, 30);
                    }
                    else if (LineCount < 22)
                    {
                        xls.PrintArea(0, 0, 59, 44);
                    }

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
        //private bool 内航船舶輸送実績調査票船(string FuneNo)
        //{
        //    var ret = from v in msVessels
        //              where v.VesselNo == FuneNo
        //              select v;

        //    if (ret.Count<MsVessel>() != 0)
        //    {
        //        MsVessel msVessel = ret.First<MsVessel>();
        //        if (msVessel.DouseiReport1 == 1)
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        //private double 貨物数量(MsUser loginUser,List<TKJNAIPLAN> datas)
        //{
        //    double Qtty = 0;
        //    foreach (TKJNAIPLAN data in datas)
        //    {
        //        if (data.SgKbn == "1")
        //        {
        //            List<TKJNAIPLAN_AMT_BILL> details = TKJNAIPLAN_AMT_BILL.GetRecords(loginUser, data);

        //            foreach (TKJNAIPLAN_AMT_BILL detail in details)
        //            {
        //                Qtty += detail.Qtty;
        //            }
        //        }
        //    }
        //    return Qtty;
        //}

        //private bool Is外港(List<TKJNAIPLAN> LineDatas)
        //{
        //    var ret = from p in LineDatas
        //              where p.GaichiFlag == 1
        //              select p;
        //    if (ret.Count<TKJNAIPLAN>() > 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        private string 輸送品目(List<MsCargoYusoItem> LineDatas, string cargoNo)
        {
            var ret = from p in LineDatas
                      where p.CargoNo == cargoNo
                      select p;
            if (ret.Count<MsCargoYusoItem>() > 0)
            {
                return (ret.First<MsCargoYusoItem>()).YusoItemName;
            }
            return null;
        }

        #region Cellプロパティ
        private string 船に積んだ日Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("**L_DATE{0}", LineCount);
                return sb.ToString();
            }
        }
        private string 積み港Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("**L_PORT{0}", LineCount);
                return sb.ToString();
            }
        }
        private string 船から揚げた日Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("**D_DATE{0}", LineCount);
                return sb.ToString();
            }
        }
        private string 揚げ港Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("**D_PORT{0}", LineCount);
                return sb.ToString();
            }
        }

        private string 貨物名Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("**CARGO_NAME{0}", LineCount);
                return sb.ToString();
            }
        }
        private string 貨物数量Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("**QTTY{0}", LineCount);
                return sb.ToString();
            }
        }
        private string 距離Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("**DISTANCE{0}", LineCount);
                return sb.ToString();
            }
        }

        private string 前距離Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("**2DISTANCE{0}", LineCount);
                return sb.ToString();
            }
        }
        #endregion
    }
}
