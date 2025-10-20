using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using System.IO;
using System.Collections;
using System.Runtime.Serialization;
using ORMapping.Attrs;
using ORMapping;
using NBaseData.DS;
using ExcelCreator = AdvanceSoftware.ExcelCreator;

namespace NBaseData.BLC.Doc
{

    [DataContract()]
    public class IssueData
    {

        #region データメンバ

        /// <summary>
        /// 公開先フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("KOUKAI_SAKI")]
        public int KoukaiSaki { get; set; }

        /// <summary>
        /// 船名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_NAME")]
        public string MsVesselName { get; set; }

        /// <summary>
        /// 船表示順
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ORDER")]
        public int MsVesselOrder { get; set; }

        /// <summary>
        /// 部門ＩＤ
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BUMON_ID")]
        public string MsBumonID { get; set; }

        /// <summary>
        /// 部門名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BUMON_NAME")]
        public string MsBumonName { get; set; }

        /// <summary>
        /// 時期（年）
        /// </summary>
        [DataMember]
        [ColumnAttribute("JIKI_NEN")]
        public int JikiNen { get; set; }

        /// <summary>
        /// 時期（月）
        /// </summary>
        [DataMember]
        [ColumnAttribute("JIKI_TUKI")]
        public int JikiTuki { get; set; }

        /// <summary>
        /// 発行日
        /// </summary>
        [DataMember]
        [ColumnAttribute("ISSUE_DATE")]
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// アラーム発生日
        /// </summary>
        [DataMember]
        [ColumnAttribute("HAASEI_DATE")]
        public DateTime HasseiDate { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IssueData()
        {
        }

        public static List<IssueData> GetRecords(NBaseData.DAC.MsUser loginUser, string msDmHoukokushoId, int businessYear)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(未提出確認一覧出力), "GetRecords");
            List<IssueData> ret = new List<IssueData>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", msDmHoukokushoId));
            Params.Add(new DBParameter("JIKI_NEN", businessYear));
            Params.Add(new DBParameter("JIKI_YOKU_NEN", businessYear));
            Params.Add(new DBParameter("TODAY", DateTime.Today.ToShortDateString()));
            Params.Add(new DBParameter("ALARM_SHOW_FLAG", (int)PtAlarmInfo.AlarmShowFlagEnum.アラームＯＮ));

            MappingBase<IssueData> mapping = new MappingBase<IssueData>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
    }


    public class 未提出確認一覧出力
    {

        public static readonly string TemplateName = "未提出確認一覧";
        public Hashtable hash_cellStr = new Hashtable();
        private int StartRow = 4;
        private int BaseRow = 0;
        private int Row = 0;


        public byte[] 未提出確認一覧取得(MsUser loginUser, int businessYear)
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

            List<HoukokushoKanriKiroku> houkokushos = HoukokushoKanriKiroku.GetRecords未提出一覧(loginUser, businessYear);
            hash_cellStr.Add(0, 時期4月Cell);
            hash_cellStr.Add(1, 時期5月Cell);
            hash_cellStr.Add(2, 時期6月Cell);
            hash_cellStr.Add(3, 時期7月Cell);
            hash_cellStr.Add(4, 時期8月Cell);
            hash_cellStr.Add(5, 時期9月Cell);
            hash_cellStr.Add(6, 時期10月Cell);
            hash_cellStr.Add(7, 時期11月Cell);
            hash_cellStr.Add(8, 時期12月Cell);
            hash_cellStr.Add(9, 時期1月Cell);
            hash_cellStr.Add(10, 時期2月Cell);
            hash_cellStr.Add(11, 時期3月Cell);

            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                if (BaseFileName != null && BaseFileName.Length != 0)
                {
                    // 指定されたテンプレートを元にファイルを作成
                    xls.OpenBook(outPutFileName, templateName);
                }

                Row = StartRow - 1;
                writeMonth(xls);

                Row = StartRow;
                foreach (HoukokushoKanriKiroku houkokusho in houkokushos)
                {
                    List<IssueData> datas = IssueData.GetRecords(loginUser, houkokusho.MsDmHoukokushoID, businessYear);
                    
                    var fune = from p in datas
                               where p.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船
                               orderby p.MsVesselOrder, p.JikiNen, p.JikiTuki, p.IssueDate
                               select p;
                    write船(xls, houkokusho, fune);

                    var userRoleList = DocConstants.ClassItemList().Where(o => o.enumClass == DocConstants.ClassEnum.USER);
                    foreach(DocConstants.ClassItem item in userRoleList)
                    {
                        var userDatas = from p in datas
                                         where p.KoukaiSaki == (int)item.enumRole
                                         orderby p.JikiNen, p.JikiTuki, p.IssueDate
                                         select p;
                        writeグループ(xls, houkokusho, item.menuName, userDatas);
                    }

                    var bumonRoleList = DocConstants.ClassItemList().Where(o => o.enumClass == DocConstants.ClassEnum.BUMON);
                    foreach (DocConstants.ClassItem item in userRoleList)
                    {
                        var bumonDatas = from p in datas
                                        where p.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.部門
                                        && p.MsBumonID == item.bumonId
                                        orderby p.JikiNen, p.JikiTuki, p.IssueDate
                                        select p;
                        writeグループ(xls, houkokusho, item.menuName, bumonDatas);
                    }
                }

                // 罫線
                xls.Cell("A" + (StartRow).ToString() + ":T" + (Row - 1).ToString()).Attr.LineLeft ( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("A" + (StartRow).ToString() + ":T" + (Row - 1).ToString()).Attr.LineRight ( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("A" + (StartRow).ToString() + ":T" + (Row - 1).ToString()).Attr.LineBottom ( ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                xls.CloseBook(true);
            }


            byte[] bytBuffer = null;
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
        /// 年度開始月に対応したヘッダ文字列を出力する
        /// </summary>
        /// <param name="xls"></param>
        private void writeMonth(ExcelCreator.Xlsx.XlsxCreator xls)
        {
            int i = 0;
            // セル文字列　"時期4月Cell"…は、NBaseUtil.DateTimeUtils.instance().INT_MONTHの月で置き換えられる
            xls.Cell(CellStr(時期4月Cell)).Value = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            xls.Cell(CellStr(時期5月Cell)).Value = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            xls.Cell(CellStr(時期6月Cell)).Value = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            xls.Cell(CellStr(時期7月Cell)).Value = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            xls.Cell(CellStr(時期8月Cell)).Value = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            xls.Cell(CellStr(時期9月Cell)).Value = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            xls.Cell(CellStr(時期10月Cell)).Value = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            xls.Cell(CellStr(時期11月Cell)).Value = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            xls.Cell(CellStr(時期12月Cell)).Value = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            xls.Cell(CellStr(時期1月Cell)).Value = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            xls.Cell(CellStr(時期2月Cell)).Value = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            xls.Cell(CellStr(時期3月Cell)).Value = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
        }

        private void write船(ExcelCreator.Xlsx.XlsxCreator xls, HoukokushoKanriKiroku houkokusho, IEnumerable<IssueData> issueDatas)
        {
            string vesselName = "";

            foreach (IssueData d in issueDatas)
            {
                if (vesselName != d.MsVesselName)
                {
                    if (vesselName != "")
                    {
                        Row++;
                    }
                    xls.Cell(CellStr(行番号Cell)).Value = (Row - StartRow + 1).ToString();
                    xls.Cell(CellStr(分類名Cell)).Value = houkokusho.BunruiName;
                    xls.Cell(CellStr(小分類名Cell)).Value = houkokusho.ShoubunruiName;
                    xls.Cell(CellStr(文書番号Cell)).Value = houkokusho.BunshoNo;
                    xls.Cell(CellStr(文書名Cell)).Value = houkokusho.BunshoName;
                    xls.Cell(CellStr(船名_Ｇ名Cell)).Value = d.MsVesselName;
                    xls.Cell(CellStr(提出周期Cell)).Value = houkokusho.Shuki;
                    if (d.HasseiDate != DateTime.MinValue)
                    {
                        xls.Cell(CellStr(アラーム状況Cell)).Value = "○";
                    }

                    for (int i = 0; i < 12; i++)
                    {
                        int index = NBaseUtil.DateTimeUtils.GetMonthIndex(i + 1);
                        if (houkokusho.Jiki[i] == '1')
                        {
                            xls.Cell(CellStr((hash_cellStr[index] as string))).Value = "※";
                        }
                        System.Diagnostics.Debug.WriteLine($"   [{i}] = _INT_MONTH:{NBaseUtil.DateTimeUtils._INT_MONTH[i]}, index:{index}, houkokusho.Jiki[i]:{houkokusho.Jiki[i]}, hash_cellStr:{hash_cellStr[i]}");
                    }
                    vesselName = d.MsVesselName;
                }

                if (d.JikiNen > 0 && d.JikiTuki > 0)
                {
                    string cellName = "";

                    for (int i = 0; i < 12; i++)
                    {
                        if (d.JikiTuki == NBaseUtil.DateTimeUtils.instance().INT_MONTH[i])
                        {
                            cellName = CellStr((hash_cellStr[i] as string));
                        }
                    }
                    xls.Cell(cellName).Value = d.IssueDate.ToShortDateString();
                }
            }
            if (vesselName != "")
            {
                Row++;
            }
        }
        //private void writeグループ(ExcelCreator.Xlsx.XlsxCreator xls, HoukokushoKanriKiroku houkokusho, IEnumerable<IssueData> issueDatas)
        //{
        //    if (issueDatas == null || issueDatas.Count<IssueData>() == 0)
        //    {
        //        return;
        //    }

        //    xls.Cell(CellStr(行番号Cell)).Value = (Row - StartRow + 1).ToString();
        //    xls.Cell(CellStr(分類名Cell)).Value = houkokusho.BunruiName;
        //    xls.Cell(CellStr(小分類名Cell)).Value = houkokusho.ShoubunruiName;
        //    xls.Cell(CellStr(文書番号Cell)).Value = houkokusho.BunshoNo;
        //    xls.Cell(CellStr(文書名Cell)).Value = houkokusho.BunshoName;
        //    xls.Cell(CellStr(船名_Ｇ名Cell)).Value = (issueDatas.First<IssueData>()).MsBumonName;
        //    xls.Cell(CellStr(提出周期Cell)).Value = houkokusho.Shuki;
        //    if ((issueDatas.First<IssueData>()).HasseiDate != DateTime.MinValue)
        //    {
        //        xls.Cell(CellStr(アラーム状況Cell)).Value = "○";
        //    }
        //    for (int i = 0; i < 12; i++)
        //    {
        //        if (houkokusho.Jiki[i] == '1')
        //        {
        //            xls.Cell(CellStr((hash_jiki[i] as string))).Value = "※";
        //        }
        //    }

        //    foreach (IssueData d in issueDatas)
        //    {
        //        if (d.JikiNen > 0 && d.JikiTuki > 0)
        //        {
        //            string cellName = "";
        //            if (d.JikiTuki < 4)
        //            {
        //                cellName = CellStr((hash_jiki[d.JikiTuki + 8] as string));
        //            }
        //            else
        //            {
        //                cellName = CellStr((hash_jiki[d.JikiTuki - 4] as string));
        //            }
        //            xls.Cell(cellName).Value = d.IssueDate.ToShortDateString();
        //        }
        //    }
        //    Row++;
        //}
        private void writeグループ(ExcelCreator.Xlsx.XlsxCreator xls, HoukokushoKanriKiroku houkokusho, string name, IEnumerable<IssueData> issueDatas)
        {
            if (issueDatas == null || issueDatas.Count<IssueData>() == 0)
            {
                return;
            }
            xls.Cell(CellStr(行番号Cell)).Value = (Row - StartRow + 1).ToString();
            xls.Cell(CellStr(分類名Cell)).Value = houkokusho.BunruiName;
            xls.Cell(CellStr(小分類名Cell)).Value = houkokusho.ShoubunruiName;
            xls.Cell(CellStr(文書番号Cell)).Value = houkokusho.BunshoNo;
            xls.Cell(CellStr(文書名Cell)).Value = houkokusho.BunshoName;
            xls.Cell(CellStr(船名_Ｇ名Cell)).Value = name;
            xls.Cell(CellStr(提出周期Cell)).Value = houkokusho.Shuki;
            if ((issueDatas.First<IssueData>()).HasseiDate != DateTime.MinValue)
            {
                xls.Cell(CellStr(アラーム状況Cell)).Value = "○";
            }
            for (int i = 0; i < 12; i++)
            {
                int index = NBaseUtil.DateTimeUtils.GetMonthIndex(i + 1);
                if (houkokusho.Jiki[i] == '1')
                {
                    xls.Cell(CellStr((hash_cellStr[index] as string))).Value = "※";
                }
            }

            foreach (IssueData d in issueDatas)
            {
                if (d.JikiNen > 0 && d.JikiTuki > 0)
                {
                    string cellName = "";
                    for (int i = 0; i < 12; i++)
                    {
                        if (d.JikiTuki == NBaseUtil.DateTimeUtils.instance().INT_MONTH[i])
                        {
                            cellName = CellStr((hash_cellStr[i] as string));
                        }
                    }
                    xls.Cell(cellName).Value = d.IssueDate.ToShortDateString();
                }
            }
            Row++;
        }

        #region プロパティ

        private string CellStr( string Prefix )
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(Prefix, Row);
            return sb.ToString();
        }


        private string 行番号Cell = "A{0}";
        private string 分類名Cell = "B{0}";
        private string 小分類名Cell = "C{0}";
        private string 文書番号Cell = "D{0}";
        private string 文書名Cell   = "E{0}";
        private string 船名_Ｇ名Cell = "F{0}";
        private string 提出周期Cell = "G{0}";
        private string アラーム状況Cell = "H{0}";

        private string 時期4月Cell = "I{0}";
        private string 時期5月Cell = "J{0}";
        private string 時期6月Cell = "K{0}";
        private string 時期7月Cell = "L{0}";
        private string 時期8月Cell = "M{0}";
        private string 時期9月Cell = "N{0}";
        private string 時期10月Cell = "O{0}";
        private string 時期11月Cell = "P{0}";
        private string 時期12月Cell = "Q{0}";
        private string 時期1月Cell = "R{0}";
        private string 時期2月Cell = "S{0}";
        private string 時期3月Cell = "T{0}";
        #endregion
    }
}
