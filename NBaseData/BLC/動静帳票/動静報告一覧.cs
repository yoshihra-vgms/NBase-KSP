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
    public class 動静報告一覧
    {
        public String ErrorMsg = "";

        public static readonly string TemplateName = "動静報告一覧";
        private List<MsVessel> vesselList = null;
        private List<MsBasho> bashoList = null;
        private List<MsDjTenkou> tenkouList = null;
        private List<MsDjKazamuki> kazamukiList = null;
        private List<MsDjSentaisetsubi> sentaisetsubiList = null;
        private List<MsDjKenkoujyoutai> kenkoujyoutaiList = null;
        private int Row = 6;

        /// <summary>
        /// Wing用
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="date"></param>
        /// <param name="houkokuList"></param>
        /// <returns></returns>
        public byte[] 動静報告一覧取得(MsUser loginUser, DateTime date, List<DjDouseiHoukoku> houkokuList)
        {
            vesselList = MsVessel.GetRecordsByKanidouseiEnabled(loginUser);
            bashoList = MsBasho.GetRecordsBy港(loginUser);
            tenkouList = MsDjTenkou.GetRecords(loginUser);
            kazamukiList = MsDjKazamuki.GetRecords(loginUser);
            sentaisetsubiList = MsDjSentaisetsubi.GetRecords(loginUser);
            kenkoujyoutaiList = MsDjKenkoujyoutai.GetRecords(loginUser);

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

            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                //----------------------------------
                //2013/12/24 コメントアウト
                //if (BaseFileName != null && BaseFileName.Length != 0)
                //{
                //    // 指定されたテンプレートを元にファイルを作成
                //    xls.OpenBook(outPutFileName, templateName);
                //}
                //----------------------------------
                //2013/12/24 変更:ファイル名が無い場合は何もせず抜ける　m.y
                if (BaseFileName == null || BaseFileName.Length == 0)
                {
                    return null;
                }
                // 指定されたテンプレートを元にファイルを作成 OpenBookエラーをなげる
                int xlsRet = xls.OpenBook(outPutFileName, templateName);
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
                //----------------------------------

                xls.Cell(TitleCell).Value = "動静報告一覧(" + date.ToString("yyyy年MM月dd日") + ")";

                // データ数分、行をコピーする
                int rowCount = vesselList.Count;
                for (int i = 0; i < rowCount-1; i ++ ) // templateに元々１行あるので－１分コピー
                {
                    xls.RowCopy(Row - 1, Row + i);
                }

                // データを出力する
                int startRow = Row;
                foreach (MsVessel vessel in vesselList)
                {
                    xls.Cell(船名Cell).Value = vessel.VesselName;

                    var tmp = from datas in houkokuList
                              where datas.VesselID == vessel.MsVesselID
                              select datas;

                    if (tmp.Count<DjDouseiHoukoku>() > 0)
                    {
                        DjDouseiHoukoku houkoku = tmp.First<DjDouseiHoukoku>();

                        xls.Cell(報告日時Cell).Value = GetDateStr(houkoku.HoukokuDate);
                        xls.Cell(現在地Cell).Value = houkoku.CurrentPlace;
                        xls.Cell(出港地Cell).Value = GetPort(houkoku.LeavePortID);
                        xls.Cell(出港日時Cell).Value = GetDateStr(houkoku.LeaveDate);
                        xls.Cell(仕向地Cell).Value = GetPort(houkoku.DestinationPortID);
                        xls.Cell(入港予定日時Cell).Value = GetDateStr(houkoku.ArrivalDate);
                        xls.Cell(天候Cell).Value = GetTenkou(houkoku.MsDjTenkouID);
                        xls.Cell(風向Cell).Value = GetKazamuki(houkoku.MsDjKazamukiID);
                        xls.Cell(風速Cell).Value = houkoku.Fusoku;
                        xls.Cell(波Cell).Value = houkoku.Nami;
                        xls.Cell(うねりCell).Value = houkoku.Uneri;
                        xls.Cell(視程Cell).Value = houkoku.Sitei;
                        xls.Cell(針路Cell).Value = houkoku.Sinro;
                        xls.Cell(速力Cell).Value = houkoku.Sokuryoku;
                        xls.Cell(設備状況Cell).Value = GetSentaisetsubi(houkoku.MsDjSentaisetsubiID);
                        xls.Cell(健康状態Cell).Value = GetKenkoujyoutai(houkoku.MsDjKenkoujyoutaiID);
                        xls.Cell(乗組員数Cell).Value = houkoku.Norikumiinsu;
                        xls.Cell(備考Cell).Value = houkoku.Bikou;
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

        /// <summary>
        /// NBaseHonsen用
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="date"></param>
        /// <param name="houkokuList"></param>
        /// <returns></returns>
        public bool 動静報告一覧取得(MsUser loginUser, String path, String outPutFileName, MsVessel vessel, DateTime from, DateTime to, List<DjDouseiHoukoku> houkokuList)
        {
            bashoList = MsBasho.GetRecordsBy港(loginUser);
            tenkouList = MsDjTenkou.GetRecords(loginUser);
            kazamukiList = MsDjKazamuki.GetRecords(loginUser);
            sentaisetsubiList = MsDjSentaisetsubi.GetRecords(loginUser);
            kenkoujyoutaiList = MsDjKenkoujyoutai.GetRecords(loginUser);

            #region 元になるファイルの確認と出力ファイル生成
            string BaseFileName = TemplateName;
            string templateName = path + "Template_" + BaseFileName + ".xlsx";

            bool exists = System.IO.File.Exists(templateName);
            if (exists == false)
            {
                ErrorMsg = "Temp[" + templateName + "]ファイルなし";
                return false;
            }
            #endregion

            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                //----------------------------------
                //2013/12/24 コメントアウト
                //if (BaseFileName != null && BaseFileName.Length != 0)
                //{
                //    // 指定されたテンプレートを元にファイルを作成
                //    xls.OpenBook(outPutFileName, templateName);
                //}
                //----------------------------------
                //2013/12/24 変更:ファイル名が無い場合は何もせず抜ける　m.y
                if (BaseFileName == null || BaseFileName.Length == 0)
                {
                    return false;
                }
                // 指定されたテンプレートを元にファイルを作成 OpenBookエラーをなげる
                int xlsRet = xls.OpenBook(outPutFileName, templateName);
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
                //----------------------------------

                xls.Cell(TitleCell).Value = "動静報告一覧(" + from.ToString("yyyy年MM月dd日") + "〜" + to.ToString("yyyy年MM月dd日") + ")";

                // データ数分、行をコピーする
                int rowCount = houkokuList.Count;
                for (int i = 0; i < rowCount - 1; i++) // templateに元々１行あるので－１分コピー
                {
                    xls.RowCopy(Row - 1, Row + i);
                }

                // データを出力する
                int startRow = Row;
                foreach (DjDouseiHoukoku houkoku in houkokuList)
                {
                    xls.Cell(船名Cell).Value = vessel.VesselName;


                    xls.Cell(報告日時Cell).Value = GetDateStr(houkoku.HoukokuDate);
                    xls.Cell(現在地Cell).Value = houkoku.CurrentPlace;
                    xls.Cell(出港地Cell).Value = GetPort(houkoku.LeavePortID);
                    xls.Cell(出港日時Cell).Value = GetDateStr(houkoku.LeaveDate);
                    xls.Cell(仕向地Cell).Value = GetPort(houkoku.DestinationPortID);
                    xls.Cell(入港予定日時Cell).Value = GetDateStr(houkoku.ArrivalDate);
                    xls.Cell(天候Cell).Value = GetTenkou(houkoku.MsDjTenkouID);
                    xls.Cell(風向Cell).Value = GetKazamuki(houkoku.MsDjKazamukiID);
                    xls.Cell(風速Cell).Value = houkoku.Fusoku;
                    xls.Cell(波Cell).Value = houkoku.Nami;
                    xls.Cell(うねりCell).Value = houkoku.Uneri;
                    xls.Cell(視程Cell).Value = houkoku.Sitei;
                    xls.Cell(針路Cell).Value = houkoku.Sinro;
                    xls.Cell(速力Cell).Value = houkoku.Sokuryoku;
                    xls.Cell(設備状況Cell).Value = GetSentaisetsubi(houkoku.MsDjSentaisetsubiID);
                    xls.Cell(健康状態Cell).Value = GetKenkoujyoutai(houkoku.MsDjKenkoujyoutaiID);
                    xls.Cell(乗組員数Cell).Value = houkoku.Norikumiinsu;
                    xls.Cell(備考Cell).Value = houkoku.Bikou;

                    Row++;
                }

                xls.DeleteSheet(0, 1);
                xls.CloseBook(true);
            }

            ErrorMsg = "完了";

            return true;
        }

        private string GetDateStr(DateTime date)
        {
            if (date == DateTime.MinValue)
                return "";

            return date.ToString("MM/dd HH:mm");
        }

        private string GetPort(string portId)
        {
            if (portId == null || portId.Length == 0)
                return "";

            foreach (MsBasho basho in bashoList)
            {
                if (basho.MsBashoId == portId)
                    return basho.BashoName;
            }
            return "";
        }

        private string GetTenkou(string tenkouId)
        {
            if (tenkouId == null || tenkouId.Length == 0)
                return "";

            foreach (MsDjTenkou tenkou in tenkouList)
            {
                if (tenkou.MsDjTenkouId == tenkouId)
                    return tenkou.Tenkou;
            }
            return "";
        }

        private string GetKazamuki(string kazamukiId)
        {
            if (kazamukiId == null || kazamukiId.Length == 0)
                return "";

            foreach (MsDjKazamuki kazamuki in kazamukiList)
            {
                if (kazamuki.MsDjKazamukiId == kazamukiId)
                    return kazamuki.Kazamuki;
            }
            return "";
        }

        private string GetSentaisetsubi(string sentaisetsubiId)
        {
            if (sentaisetsubiId == null || sentaisetsubiId.Length == 0)
                return "";

            foreach (MsDjSentaisetsubi sentaisetsubi in sentaisetsubiList)
            {
                if (sentaisetsubi.MsDjSentaisetsubiId == sentaisetsubiId)
                    return sentaisetsubi.Sentaisetsubi;
            }
            return "";
        }

        private string GetKenkoujyoutai(string kenkoujyoutaiId)
        {
            if (kenkoujyoutaiId == null || kenkoujyoutaiId.Length == 0)
                return "";

            foreach (MsDjKenkoujyoutai kenkoujyoutai in kenkoujyoutaiList)
            {
                if (kenkoujyoutai.MsDjKenkoujyoutaiId == kenkoujyoutaiId)
                    return kenkoujyoutai.Kenkoujyoutai;
            }
            return "";
        }

        #region プロパティ
        private string TitleCell
        {
            get
            {
                return "**TITLE";
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

        private string 報告日時Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("B{0}", Row);
                return sb.ToString();
            }
        }

        private string 現在地Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("C{0}", Row);
                return sb.ToString();
            }
        }

        private string 出港地Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("D{0}", Row);
                return sb.ToString();
            }
        }

        private string 出港日時Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("E{0}", Row);
                return sb.ToString() ;
            }
        }

        private string 仕向地Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("F{0}",Row);
                return sb.ToString();
            }
        }

        private string 入港予定日時Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("G{0}", Row);
                return sb.ToString();
            }
        }

        private string 天候Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("H{0}", Row);
                return sb.ToString();
            }
        }

        private string 風向Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("I{0}", Row);
                return sb.ToString();
            }
        }

        private string 風速Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("J{0}", Row);
                return sb.ToString();
            }
        }

        private string 波Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("K{0}", Row);
                return sb.ToString();
            }
        }

        private string うねりCell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("L{0}", Row);
                return sb.ToString();
            }
        }

        private string 視程Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("M{0}", Row);
                return sb.ToString();
            }
        }

        private string 針路Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("N{0}", Row);
                return sb.ToString();
            }
        }

        private string 速力Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("O{0}", Row);
                return sb.ToString();
            }
        }

        private string 設備状況Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("P{0}", Row);
                return sb.ToString();
            }
        }


        private string 健康状態Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Q{0}", Row);
                return sb.ToString();
            }
        }

        private string 乗組員数Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("R{0}", Row);
                return sb.ToString();
            }
        }

        private string 備考Cell
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("S{0}", Row);
                return sb.ToString();
            }
        }

        #endregion
    }
}
