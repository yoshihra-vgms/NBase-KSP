using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using System.IO;
using System.Runtime.Serialization;
using ORMapping.Atts;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using ExcelCreator = AdvanceSoftware.ExcelCreator;

namespace NBaseData.BLC.動静帳票
{
    [DataContract()]
    [TableAttribute("V_ENEHOU_DATA")]
    public class V_ENEHOU_DATA
    {
        #region データメンバ
        /// <summary>
        /// 船舶NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("船NO")]
        public string FuneNo { get; set; }

        /// <summary>
        /// 船名
        /// </summary>
        [DataMember]
        [ColumnAttribute("船名")]
        public string VesselName { get; set; }

        /// <summary>
        /// 次航NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("次航")]
        public string JikoNo { get; set; }

        /// <summary>
        /// 積揚セットNO
        /// </summary>
        [DataMember]
        [ColumnAttribute("積揚セット")]
        public string LdDsSetNo { get; set; }

        /// <summary>
        /// 積み港NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("積地コード")]
        public string TumiPortNo { get; set; }

        /// <summary>
        /// 積み港名
        /// </summary>
        [DataMember]
        [ColumnAttribute("積地名")]
        public string TumiPortName { get; set; }

        /// <summary>
        /// 積み数量
        /// </summary>
        [DataMember]
        [ColumnAttribute("積数量")]
        public decimal TumiQtty { get; set; }

        /// <summary>
        /// 揚げ港NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("揚地コード")]
        public string AgePortNo { get; set; }

        /// <summary>
        /// 揚げ港名
        /// </summary>
        [DataMember]
        [ColumnAttribute("揚地名")]
        public string AgePortName { get; set; }

        /// <summary>
        /// 揚げ数量
        /// </summary>
        [DataMember]
        [ColumnAttribute("揚数量")]
        public decimal AgeQtty { get; set; }

        /// <summary>
        /// 貨物NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("積荷コード")]
        public string CargoNo { get; set; }

        /// <summary>
        /// 貨物名
        /// </summary>
        [DataMember]
        [ColumnAttribute("積荷名")]
        public string CargoName { get; set; }

        /// <summary>
        /// 積み日付
        /// </summary>
        [DataMember]
        [ColumnAttribute("積日付")]
        public DateTime TumiYmd { get; set; }

        /// <summary>
        /// 揚げ日付
        /// </summary>
        [DataMember]
        [ColumnAttribute("揚日付")]
        public DateTime AgeYmd { get; set; }

        /// <summary>
        /// 荷主
        /// </summary>
        [DataMember]
        [ColumnAttribute("荷主")]
        public string Charterer { get; set; }
        #endregion

        public static List<V_ENEHOU_DATA> GetRecords(MsUser loginUser, string JikoNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(V_ENEHOU_DATA), MethodBase.GetCurrentMethod());
            SQL += SqlMapper.SqlMapper.GetSql(typeof(V_ENEHOU_DATA), "OrderBy");

            List<V_ENEHOU_DATA> ret = new List<V_ENEHOU_DATA>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<V_ENEHOU_DATA> mapping = new MappingBase<V_ENEHOU_DATA>();

            Params.Add(new DBParameter("JIKO_NO", JikoNo));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
    }


    public class 改正省エネ法エネルギー報告書
    {
        public static readonly string TemplateName = "改正省エネ法エネルギー報告書";
        // 2015.10 基幹連携サーバで取得したものを利用する
        //private List<データ> Datas = new List<データ>();
        //private List<MsVessel> msVessels;
        private List<MsBasho> msBashos;
        private List<MsCargo> msCargos;
        private int Row = 6;

        //public byte[] 改正省エネ法エネルギー報告書取得(MsUser loginUser, DateTime SelectedDate)
　      public byte[] 改正省エネ法エネルギー報告書取得(MsUser loginUser, DateTime SelectedDate, List<データ> Datas)
        {
            msBashos = MsBasho.GetRecords(loginUser);
            msCargos = MsCargo.GetRecords(loginUser);
            //msVessels = MsVessel.GetRecords(loginUser);

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

            // 2015.10 基幹連携サーバで取得したものを利用する
            //データ集計(loginUser,SelectedDate);

            System.Globalization.Calendar calendar = new System.Globalization.JapaneseCalendar();
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("ja-JP");
            culture.DateTimeFormat.Calendar = calendar;

            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                if (BaseFileName != null && BaseFileName.Length != 0)
                {
                    // 指定されたテンプレートを元にファイルを作成
                    xls.OpenBook(outPutFileName, templateName);
                }

                // 現在の総シート数を確認します
                int nSheetCount = xls.SheetCount;

                // 1 番左のシート(シート番号 0)を、最終シート以降にコピー
                xls.CopySheet(0, nSheetCount, SelectedDate.ToString("gyy年MM月分", culture));
                xls.SheetNo = nSheetCount;


                xls.Cell(TitleCell).Str = SelectedDate.ToString("gyy年MM月分", culture);
                xls.Cell(出力日Cell).Str = DateTime.Today.ToString("yyyy/MM/dd");

                // データ数分、行をコピーする
                int rowCount = Datas.Count;
                for (int i = 0; i < rowCount-1; i ++ ) // templateに元々１行あるので－１分コピー
                {
                    xls.RowCopy(Row - 1, Row + i);
                }

                // データを出力する
                int startRow = Row;
                string LdDsSetNo = "";
                string AgePortNo = "";
                foreach (データ data in Datas)
                {
                    if (data.Type == データ.TypeEnum.詳細)
                    {
                        xls.Cell(船名Cell).Value = data.EnehouData.VesselName;
                        xls.Cell(荷主Cell).Value = data.EnehouData.Charterer;
                        xls.Cell(積荷Cell).Value = CargoName(data.EnehouData.CargoNo); // data.EnehouData.CargoName;
                        xls.Cell(積地Cell).Value = PortName(data.EnehouData.TumiPortNo); // data.EnehouData.TumiPortName;
                        xls.Cell(積数量Cell).Value = data.EnehouData.TumiQtty;
                        xls.Cell(積日付Cell).Value = data.EnehouData.TumiYmd;
                        xls.Cell(揚地Cell).Value = PortName(data.EnehouData.AgePortNo); // data.EnehouData.AgePortName;
                        xls.Cell(揚数量Cell).Value = data.EnehouData.AgeQtty;
                        xls.Cell(揚日付Cell).Value = data.EnehouData.AgeYmd;

                        double yusoKyori = Kyori(loginUser, data.EnehouData.TumiPortNo, data.EnehouData.AgePortNo);
                        if (yusoKyori != -1)
                        {
                            xls.Cell(輸送距離Cell).Value = yusoKyori;
                        }
                        double kaikoKyori = Kyori(loginUser, AgePortNo, data.EnehouData.TumiPortNo); // １つ前の揚げから今回の積みまでの距離
                        if (kaikoKyori != -1)
                        {
                            xls.Cell(回航距離Cell).Value = kaikoKyori;
                        }

                        //if (yusoKyori == -1 && kaikoKyori == -1)
                        //{
                        //    xls.Cell(輸送距離Cell + ":" + 原油換算Cell).Value = "";
                        //}

                        if (LdDsSetNo != data.EnehouData.LdDsSetNo)
                        {
                            LdDsSetNo = data.EnehouData.LdDsSetNo;
                            AgePortNo = data.EnehouData.AgePortNo;
                        }
                    }
                    else if (data.Type == データ.TypeEnum.船合計)
                    {
                        xls.Cell(船名Cell + ":" + 原油換算Cell).Attr.BackColor = System.Drawing.Color.Yellow;// ExcelCreator.xlColor.xcYellow;

                        xls.Cell(船名Cell).Value = data.VesselName;
                        xls.Cell(小計Cell).Value = "小計";
                        xls.Cell(積数量Cell).Value = "=SUM(" + 積数量Col + startRow.ToString() + ":" + 積数量Col + (Row-1).ToString() + ")";
                        xls.Cell(揚数量Cell).Value = "=SUM(" + 揚数量Col + startRow.ToString() + ":" + 揚数量Col + (Row - 1).ToString() + ")";
                        xls.Cell(輸送距離Cell).Value = "=SUM(" + 輸送距離Col + startRow.ToString() + ":" + 輸送距離Col + (Row - 1).ToString() + ")";
                        
                        xls.Cell(A重油使用Cell).Value = "";
                        xls.Cell(C重油使用Cell).Value = "";

                        startRow = Row + 1;
                        LdDsSetNo = "";
                        AgePortNo = "";
                    }
                    Row++;
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

       private string PortName(string PortNo)
       {
           var port = from p in msBashos
                      where p.MsBashoNo == PortNo
                      select p;

           return port.First<MsBasho>().BashoName;
       }

       private string CargoName(string CargoNo)
       {
           var cargo = from p in msCargos
                       where p.CargoNo == CargoNo
                       select p;

           return cargo.First<MsCargo>().CargoName;
       }


        // 2015.10 基幹連携サーバで取得したものを利用する
        #region
        //private string VesselName(string FuneNo)
        //{
        //    var ret = from v in msVessels
        //              where v.VesselNo == FuneNo
        //              select v;

        //    if (ret.Count<MsVessel>() != 0)
        //    {
        //        MsVessel msVessel = ret.First<MsVessel>();
        //        return msVessel.VesselName;
        //    }

        //    return "";
        //}
        //private void データ集計(MsUser loginUser, DateTime SelectedDate)
        //{
        //    List<V_ENEHOU_DATA> EnehouDatas = V_ENEHOU_DATA.GetRecords(loginUser, SelectedDate.ToString("yyMM"));

        //    string vesselName = "";
        //    foreach (V_ENEHOU_DATA EnehouData in EnehouDatas)
        //    {
        //        if (改正省エネ法エネルギー報告書船(EnehouData.FuneNo) == true)
        //        {
        //            if (vesselName != VesselName(EnehouData.FuneNo))
        //            {
        //                if (vesselName != "")
        //                {
        //                    データ 船合計データ = new データ();

        //                    船合計データ.Type = データ.TypeEnum.船合計;
        //                    船合計データ.VesselName = vesselName;

        //                    Datas.Add(船合計データ);
        //                }
        //                vesselName = VesselName(EnehouData.FuneNo);
        //            }
        //            データ d = new データ();

        //            d.Type = データ.TypeEnum.詳細;
        //            d.EnehouData = EnehouData;

        //            Datas.Add(d);
        //        }
        //    }
        //    if (vesselName != "")
        //    {
        //        データ 船合計データ = new データ();

        //        船合計データ.Type = データ.TypeEnum.船合計;
        //        船合計データ.VesselName = vesselName;

        //        Datas.Add(船合計データ);
        //    }
        //}

        //private bool 改正省エネ法エネルギー報告書船(string FuneNo)
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
        #endregion


        //private TKJNAIPLAN 揚地(List<TKJNAIPLAN> tKJNAIPLANIFs, TKJNAIPLAN tKJNAIPLAN)
        //{
        //    var ret = from p in tKJNAIPLANIFs
        //              where p.SgKbn == "2" && p.LdDsSetNo == tKJNAIPLAN.LdDsSetNo
        //              orderby p.ScYmd,p.KomaNo
        //              select p;

        //    if (ret.Count<TKJNAIPLAN>() == 0)
        //    {
        //        return null;
        //    }
        //    return ret.First<TKJNAIPLAN>();
        //}

        //private TKJNAIPLAN 積地1(List<TKJNAIPLAN> tKJNAIPLANIFs)
        //{
        //    var ret = from p in tKJNAIPLANIFs
        //              where p.SgKbn == "1"
        //              orderby p.ScYmd, p.KomaNo
        //              select p;

        //    if (ret.Count<TKJNAIPLAN>() == 0)
        //    {
        //        return null;
        //    }
        //    return ret.First<TKJNAIPLAN>();
        //}


        //private string CargoNo(MsUser loginUser, TKJNAIPLAN TKJNAIPLANIF)
        //{
        //    List<TKJNAIPLAN_AMT_BILL> tKJNAIPLAN_AMTIFs = TKJNAIPLAN_AMT_BILL.GetRecords(loginUser, TKJNAIPLANIF);

        //    return tKJNAIPLAN_AMTIFs[0].CargoNo;
        //}

        //private string CargoNo(MsUser loginUser, TKJNAIPLAN_AMT_BILL TKJNAIPLANAMTBILL)
        //{
        //    return TKJNAIPLANAMTBILL.CargoNo;
        //}

        //private string Ninushi(MsUser loginUser, TKJNAIPLAN_AMT_BILL TKJNAIPLANAMTBILL)
        //{
        //    return TKJNAIPLANAMTBILL.ChartererNo;
        //}

        //private double Qtty(MsUser loginUser, List<TKJNAIPLAN> tKJNAIPLANIFs, string SgKbn)
        //{
        //    double ret = 0;
        //    foreach (TKJNAIPLAN tKJNAIPLANIF in tKJNAIPLANIFs)
        //    {
        //        if (tKJNAIPLANIF.SgKbn == SgKbn)
        //        {
        //            List<TKJNAIPLAN_AMT_BILL> tKJNAIPLAN_AMTIFs = TKJNAIPLAN_AMT_BILL.GetRecords(loginUser, tKJNAIPLANIF);
        //            foreach (TKJNAIPLAN_AMT_BILL tKJNAIPLAN_AMTIF in tKJNAIPLAN_AMTIFs)
        //            {
        //                ret += tKJNAIPLAN_AMTIF.Qtty;
        //            }
        //        }
        //    }

        //    return ret;
        //}
        //private double Kyori(MsUser loginUser, TKJNAIPLAN tsumi, TKJNAIPLAN age)
        //{
        //    double PortDistance = 0;

        //    MsBashoKyori PortKyori = MsBashoKyori.GetRecord(loginUser, tsumi.PortNo, age.PortNo);
        //    if (PortKyori != null)
        //    {
        //        PortDistance += PortKyori.Kyori;
        //    }

        //    return PortDistance;
        //}
        private double Kyori(MsUser loginUser, string tsumiPortNo, string agePortNo)
        {
            double PortDistance = 0;

            MsBashoKyori PortKyori = MsBashoKyori.GetRecord(loginUser, tsumiPortNo, agePortNo);
            if (PortKyori != null)
            {
                PortDistance += PortKyori.Kyori;
            }

            return PortDistance;
        }

        #region データ
        public class データ
        {
            public TypeEnum Type;
            public enum TypeEnum { 詳細, 船合計 };
            //public string OpeSetKey;
            //public string TsumiPortNo;
            //public string AgePortNo;
            //public string CargoNo;
            //public double Qtty;
            //public double Kyori;
            //public string VesselName;

            //public string ChartaitNo;
            //public string Charterer;
            //public decimal TsumiQtty;
            //public decimal AgeQtty;
            //public DateTime TsumiDate;
            //public DateTime AgeDate;
            
            public string VesselName;
            public V_ENEHOU_DATA EnehouData;
        }
        #endregion
        #region プロパティ
        private string TitleCell
        {
            get
            {
                return "**TITLE";
            }
        }
        private string 出力日Cell
        {
            get
            {
                return "**DATE";
            }
        }

        private string 船名Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("A{0}", Row);
                return sb.ToString();
            }
        }

        private string 小計Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("B{0}", Row);
                return sb.ToString();
            }
        }

        private string 荷主Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("C{0}", Row);
                return sb.ToString();
            }
        }

        private string 積荷Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("D{0}", Row);
                return sb.ToString();
            }
        }

        private string 積地Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("E{0}", Row);
                return sb.ToString() ;
            }
        }

        private string 積数量Col = "F";
        private string 積数量Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}{1}", 積数量Col,Row);
                return sb.ToString();
            }
        }

        private string 積日付Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("G{0}", Row);
                return sb.ToString();
            }
        }

        private string 揚地Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("H{0}", Row);
                return sb.ToString();
            }
        }

        private string 揚数量Col = "I";
        private string 揚数量Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}{1}", 揚数量Col, Row);
                return sb.ToString();
            }
        }

        private string 揚日付Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("J{0}", Row);
                return sb.ToString();
            }
        }

        private string 輸送距離Col = "K";
        private string 輸送距離Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}{1}", 輸送距離Col, Row);
                return sb.ToString();
            }
        }

        private string 回航距離Col = "L";
        private string 回航距離Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}{1}", 回航距離Col, Row);
                return sb.ToString();
            }
        }

        private string A重油使用Col = "P";
        private string A重油使用Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}{1}", A重油使用Col, Row);
                return sb.ToString();
            }
        }

        private string C重油使用Col = "P";
        private string C重油使用Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}{1}", C重油使用Col, Row);
                return sb.ToString();
            }
        }

        private string 原油換算Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("X{0}", Row);
                return sb.ToString();
            }
        }

        #endregion
    }
}
