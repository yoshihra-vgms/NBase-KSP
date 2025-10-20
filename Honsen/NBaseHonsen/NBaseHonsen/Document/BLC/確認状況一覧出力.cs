using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using System.IO;
using NBaseData.BLC;
using NBaseData.DS;
using NBaseUtil;
using ExcelCreator = AdvanceSoftware.ExcelCreator;

namespace NBaseHonsen.Document.BLC
{
    public class 確認状況一覧出力
    {
        public static readonly string TemplateName = "確認状況一覧";
        private int StartRow = 5;
        private int BaseRow = 0;
        private int Row = 0;
       
        
        public byte[] 確認状況一覧取得(MsUser loginUser, List<状況確認一覧Row> 状況確認一覧Rows)
        {

            #region 元になるファイルの確認と出力ファイル生成
            string exeDir = System.IO.Directory.GetCurrentDirectory();

            string BaseFileName = TemplateName;
            //string templateName = exeDir + "\\Template\\Template_" + BaseFileName + ".xls";
            //string outPutFileName = exeDir + "\\Template\\outPut_[" + loginUser.FullName + "]_" + BaseFileName + ".xls";
            string templateName = ".\\Template\\Template_" + BaseFileName + ".xlsx";
            string outPutFileName = ".\\Template\\outPut_[" + loginUser.FullName + "]_" + BaseFileName + ".xlsx";

            bool exists = System.IO.File.Exists(templateName);
            if (exists == false)
            {
                return null;
            }
            #endregion


            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                if (BaseFileName != null && BaseFileName.Length != 0)
                {
                    // 指定されたテンプレートを元にファイルを作成
                    xls.OpenBook(outPutFileName, templateName);
                }

                BaseRow = StartRow;
                foreach (状況確認一覧Row data in 状況確認一覧Rows)
                {
                    Row = BaseRow;

                    int linkSaki = -1;
                    string linkSakiId = "";
                    string linkSakiId2 = "";
                    if (data.DmKanriKirokuId != null && data.DmKanriKirokuId.Length > 0)
                    {
                        linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                        linkSakiId = data.DmKanriKirokuId;
                        linkSakiId2 = data.MsDmHoukokushoId;
                    }
                    else
                    {
                        linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則;
                        linkSakiId = data.DmKoubunshoKisokuId;
                        linkSakiId2 = data.DmKoubunshoKisokuId;
                    }

                    List<DmPublisher> dmPublishers = DmPublisher.GetRecordsByLinkSakiID(loginUser, linkSakiId);
                    List<DmKakuninJokyo> dmKakuninJokyos = DmKakuninJokyo.GetRecordsByLinkSaki(loginUser, linkSaki, linkSakiId);
                    List<DmKoukaiSaki> dmKoukaisakis = DmKoukaiSaki.GetRecordsByLinkSakiID(loginUser, linkSakiId2);
                    List<DmDocComment> dmComments = DmDocComment.GetRecordsByLinkSaki(loginUser, linkSaki, linkSakiId);

                    xls.Cell(CellStr(発行元Cell)).Value = data.発行元;
                    xls.Cell(CellStr(船名Cell)).Value = data.船名;
                    xls.Cell(CellStr(発行日Cell)).Value = data.発行日;
                    xls.Cell(CellStr(分類名Cell)).Value = data.分類名;
                    xls.Cell(CellStr(小分類名Cell)).Value = data.小分類名;
                    xls.Cell(CellStr(文書番号Cell)).Value = data.文書番号;
                    xls.Cell(CellStr(文書名Cell)).Value = data.文書名;
                    if (data.Status == (int)NBaseData.DS.DocConstants.StatusEnum.完了)
                    {
                        xls.Cell(CellStr(完了日付Cell)).Value = data.完了日;
                        xls.Cell(CellStr(完了氏名Cell)).Value = data.完了者;
                    }


                    bool 要確認 = false;
                    int MaxRow = 0;

                    write確認状況(xls, dmPublishers);
                    if (MaxRow < Row)
                    {
                        MaxRow = Row;
                    }

                    #region 20210824 下記に変更
                    //要確認 = 要確認チェック(dmKoukaisakis, (int)NBaseData.DS.DocConstants.RoleEnum.会長社長);
                    //var kaicyo_shacyo = from p in dmKakuninJokyos
                    //                    where (p.KaichoFlag == (int)NBaseData.DS.DocConstants.FlagEnum.ON || p.ShachoFlag == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                    //                    && p.KoukaiSaki != (int)NBaseData.DS.DocConstants.RoleEnum.船
                    //                    orderby p.KaichoFlag descending, p.ShachoFlag descending, p.KakuninDate ascending
                    //                    select p;
                    //write確認状況(xls, 会長社長日付Cell, 会長社長氏名Cell, 要確認, kaicyo_shacyo, dmComments);
                    //if (MaxRow < Row)
                    //{
                    //    MaxRow = Row;
                    //}

                    //要確認 = 要確認チェック_部門(dmKoukaisakis, ((int)MsBumon.MsBumonIdEnum.安全運航海技Ｔ).ToString());
                    //var 安全管理_海務Ｇs = from p in dmKakuninJokyos
                    //                where p.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.部門
                    //                && p.MsBumonID == ((int)MsBumon.MsBumonIdEnum.安全運航海技Ｔ).ToString()
                    //                orderby p.GLFlag descending, p.TLFlag descending, p.KakuninDate ascending
                    //                select p;
                    //write確認状況(xls, 安全運航海技Ｔ日付Cell, 安全運航海技Ｔ氏名Cell, 要確認, 安全管理_海務Ｇs, dmComments);
                    //if (MaxRow < Row)
                    //{
                    //    MaxRow = Row;
                    //}

                    //要確認 = 要確認チェック(dmKoukaisakis, (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者);
                    //var sekininsha = from p in dmKakuninJokyos
                    //                 where p.SekininshaFlag == (int)NBaseData.DS.DocConstants.FlagEnum.ON
                    //                 && p.KoukaiSaki != (int)NBaseData.DS.DocConstants.RoleEnum.船
                    //                 orderby p.KakuninDate ascending
                    //                 select p;
                    //write確認状況(xls, 管理責任者日付Cell, 管理責任者氏名Cell, 要確認, sekininsha, dmComments);
                    //if (MaxRow < Row)
                    //{
                    //    MaxRow = Row;
                    //}

                    //要確認 = 要確認チェック_部門(dmKoukaisakis, ((int)MsBumon.MsBumonIdEnum.船舶管理技術Ｔ).ToString());
                    //var 工務Ｇs = from p in dmKakuninJokyos
                    //           where p.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.部門
                    //           && p.MsBumonID == ((int)MsBumon.MsBumonIdEnum.船舶管理技術Ｔ).ToString()
                    //           orderby p.GLFlag descending, p.TLFlag descending, p.KakuninDate ascending
                    //           select p;
                    //write確認状況(xls, 船員Ｇ日付Cell, 船員Ｇ氏名Cell, 要確認, 工務Ｇs, dmComments);
                    //if (MaxRow < Row)
                    //{
                    //    MaxRow = Row;
                    //}

                    //要確認 = 要確認チェック_部門(dmKoukaisakis, ((int)MsBumon.MsBumonIdEnum.船員Ｔ).ToString());
                    //var 船員Ｇs = from p in dmKakuninJokyos
                    //           where p.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.部門
                    //           && p.MsBumonID == ((int)MsBumon.MsBumonIdEnum.船員Ｔ).ToString()
                    //           orderby p.GLFlag descending, p.TLFlag descending, p.KakuninDate ascending
                    //           select p;
                    //write確認状況(xls, 工務Ｇ日付Cell, 工務Ｇ氏名Cell, 要確認, 船員Ｇs, dmComments);
                    //if (MaxRow < Row)
                    //{
                    //    MaxRow = Row;
                    //}

                    //要確認 = 要確認チェック_部門(dmKoukaisakis, ((int)MsBumon.MsBumonIdEnum.ITDX推進室).ToString());
                    //var ITDX推進室s = from p in dmKakuninJokyos
                    //               where p.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.部門
                    //               && p.MsBumonID == ((int)MsBumon.MsBumonIdEnum.ITDX推進室).ToString()
                    //               orderby p.GLFlag descending, p.TLFlag descending, p.KakuninDate ascending
                    //               select p;
                    //write確認状況(xls, ITDX推進室日付Cell, ITDX推進室氏名Cell, 要確認, ITDX推進室s, dmComments);
                    //if (MaxRow < Row)
                    //{
                    //    MaxRow = Row;
                    //}
                    #endregion

                    int index = 0;
                    var userRoleList = DocConstants.ClassItemList().Where(o => o.enumClass == DocConstants.ClassEnum.USER);
                    foreach (DocConstants.ClassItem item in userRoleList)
                    {
                        string 日付Cell = get日付Cell(index);
                        string 氏名Cell = get氏名Cell(index);

                        StringBuilder titleCell = new StringBuilder();
                        titleCell.AppendFormat(日付Cell, 4);
                        StringBuilder title = new StringBuilder(item.viewName1);
                        if (StringUtils.Empty(item.viewName2) == false)
                        {
                            title.Append(System.Environment.NewLine);
                            title.Append(item.viewName2);
                        }
                        xls.Cell(titleCell.ToString()).Value = title.ToString();

                        要確認 = 要確認チェック(dmKoukaisakis, (int)item.enumRole);
                        var kakuninJokyos = from p in dmKakuninJokyos
                                            where p.KoukaiSaki == (int)item.enumRole
                                            orderby p.DocFlag_CEO descending,
                                                    p.DocFlag_Admin descending,
                                                    p.DocFlag_MsiFerry descending,
                                                    p.DocFlag_CrewFerry descending,
                                                    p.DocFlag_TsiFerry descending,
                                                    p.DocFlag_MsiCargo descending,
                                                    p.DocFlag_CrewCargo descending,
                                                    p.DocFlag_TsiCargo descending,
                                                    p.DocFlag_Officer descending,
                                                    p.DocFlag_SdManager descending,
                                                    p.DocFlag_GL descending,
                                                    p.DocFlag_TL descending,
                                                    p.KakuninDate ascending
                                            select p;
                        write確認状況(xls, 日付Cell, 氏名Cell, 要確認, kakuninJokyos, dmComments);
                        if (MaxRow < Row)
                        {
                            MaxRow = Row;
                        }
                        index++;
                    }

                    var bumonRoleList = DocConstants.ClassItemList().Where(o => o.enumClass == DocConstants.ClassEnum.BUMON);
                    foreach (DocConstants.ClassItem item in bumonRoleList)
                    {
                        string 日付Cell = get日付Cell(index);
                        string 氏名Cell = get氏名Cell(index);

                        StringBuilder titleCell = new StringBuilder();
                        titleCell.AppendFormat(日付Cell, 4);
                        StringBuilder title = new StringBuilder(item.viewName1);
                        if (StringUtils.Empty(item.viewName2) == false)
                        {
                            title.Append(System.Environment.NewLine);
                            title.Append(item.viewName2);
                        }
                        xls.Cell(titleCell.ToString()).Value = title.ToString();

                        要確認 = 要確認チェック(dmKoukaisakis, (int)item.enumRole);
                        var kakuninJokyos = from p in dmKakuninJokyos
                                            where p.KoukaiSaki == (int)DocConstants.RoleEnum.部門
                                            && p.MsBumonID == item.bumonId
                                            orderby p.DocFlag_CEO descending,
                                                    p.DocFlag_Admin descending,
                                                    p.DocFlag_MsiFerry descending,
                                                    p.DocFlag_CrewFerry descending,
                                                    p.DocFlag_TsiFerry descending,
                                                    p.DocFlag_MsiCargo descending,
                                                    p.DocFlag_CrewCargo descending,
                                                    p.DocFlag_TsiCargo descending,
                                                    p.DocFlag_Officer descending,
                                                    p.DocFlag_SdManager descending,
                                                    p.DocFlag_GL descending,
                                                    p.DocFlag_TL descending,
                                                    p.KakuninDate ascending
                                            select p;
                        write確認状況(xls, 日付Cell, 氏名Cell, 要確認, kakuninJokyos, dmComments);
                        if (MaxRow < Row)
                        {
                            MaxRow = Row;
                        }
                        index++;
                    }



                    要確認 = 要確認チェック_船(dmKoukaisakis, data.KoukaiSakiVesselId);
                    var 船s = from p in dmKakuninJokyos
                             where p.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船
                             && p.MsVesselID == data.KoukaiSakiVesselId
                             orderby p.DocFlag_GL descending, p.DocFlag_TL descending, p.KakuninDate ascending, p.ShowOrder ascending
                             select p;
                    write確認状況(xls, 本船日付Cell, 本船氏名Cell, 要確認, 船s, dmComments);
                    if (MaxRow < Row)
                    {
                        MaxRow = Row;
                    }
                    BaseRow = MaxRow;

                    // 下線
                    xls.Cell("A" + (BaseRow - 1).ToString() + ":AM" + (BaseRow - 1).ToString()).Attr.LineBottom(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                }

                // 実線
                xls.Cell("A" + StartRow.ToString() + ":A" + (BaseRow - 1).ToString()).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("A" + StartRow.ToString() + ":AM" + (BaseRow - 1).ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                // 破線
                xls.Cell("H" + StartRow.ToString() + ":H" + (BaseRow - 1).ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Dashed, ExcelCreator.xlColor.Black);
                xls.Cell("J" + StartRow.ToString() + ":J" + (BaseRow - 1).ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Dashed, ExcelCreator.xlColor.Black);
                xls.Cell("L" + StartRow.ToString() + ":L" + (BaseRow - 1).ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Dashed, ExcelCreator.xlColor.Black);
                xls.Cell("N" + StartRow.ToString() + ":N" + (BaseRow - 1).ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Dashed, ExcelCreator.xlColor.Black);
                xls.Cell("P" + StartRow.ToString() + ":P" + (BaseRow - 1).ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Dashed, ExcelCreator.xlColor.Black);
                xls.Cell("R" + StartRow.ToString() + ":R" + (BaseRow - 1).ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Dashed, ExcelCreator.xlColor.Black);
                xls.Cell("T" + StartRow.ToString() + ":T" + (BaseRow - 1).ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Dashed, ExcelCreator.xlColor.Black);
                xls.Cell("V" + StartRow.ToString() + ":V" + (BaseRow - 1).ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Dashed, ExcelCreator.xlColor.Black);
                xls.Cell("X" + StartRow.ToString() + ":X" + (BaseRow - 1).ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Dashed, ExcelCreator.xlColor.Black);
                xls.Cell("Z" + StartRow.ToString() + ":Z" + (BaseRow - 1).ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Dashed, ExcelCreator.xlColor.Black);
                xls.Cell("AB" + StartRow.ToString() + ":AB" + (BaseRow - 1).ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Dashed, ExcelCreator.xlColor.Black);
                xls.Cell("AD" + StartRow.ToString() + ":AD" + (BaseRow - 1).ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Dashed, ExcelCreator.xlColor.Black);
                xls.Cell("AF" + StartRow.ToString() + ":AF" + (BaseRow - 1).ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Dashed, ExcelCreator.xlColor.Black);
                xls.Cell("AH" + StartRow.ToString() + ":AH" + (BaseRow - 1).ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Dashed, ExcelCreator.xlColor.Black);
                xls.Cell("AJ" + StartRow.ToString() + ":AJ" + (BaseRow - 1).ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Dashed, ExcelCreator.xlColor.Black);
                xls.Cell("AL" + StartRow.ToString() + ":AL" + (BaseRow - 1).ToString()).Attr.LineRight(ExcelCreator.BorderStyle.Dashed, ExcelCreator.xlColor.Black);


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

        private bool 要確認チェック(List<DmKoukaiSaki> koukaisakis, int koukaisaki)
        {
            bool ret = false;

            var tmp = from p in koukaisakis
                      where p.KoukaiSaki == koukaisaki
                      select p;

            if (tmp.Count<DmKoukaiSaki>() > 0)
            {
                ret = true;
            }

            return ret;
        }
        private bool 要確認チェック_部門(List<DmKoukaiSaki> koukaisakis, string bumonId)
        {
            bool ret = false;

            var tmp = from p in koukaisakis
                      where p.MsBumonID == bumonId
                      select p;

            if (tmp.Count<DmKoukaiSaki>() > 0)
            {
                ret = true;
            }

            return ret;
        }
        private bool 要確認チェック_船(List<DmKoukaiSaki> koukaisakis, int vesselId)
        {
            bool ret = false;

            ret = 要確認チェック(koukaisakis, (int)NBaseData.DS.DocConstants.RoleEnum.船);
            if (ret)
            {
                var tmp = from p in koukaisakis
                          where p.MsVesselID == vesselId
                          select p;

                if (tmp.Count<DmKoukaiSaki>() > 0)
                {
                    ret = true;
                }
            }
            return ret;
        }


        private void write確認状況(ExcelCreator.Xlsx.XlsxCreator xls, List<DmPublisher> publishers)
        {
            Row = BaseRow;
            foreach (DmPublisher pub in publishers)
            {
                xls.Cell(CellStr(登録者日付Cell)).Value = pub.RenewDate.ToShortDateString();
                xls.Cell(CellStr(登録者氏名Cell)).Value = pub.FullName;

                Row++;
            }
        }
        private void write確認状況(ExcelCreator.Xlsx.XlsxCreator xls, string pre1, string pre2, bool isConfirm, IEnumerable<DmKakuninJokyo> kakuninJokyos, List<DmDocComment> comments)
        {
            Row = BaseRow;
            if (isConfirm)
            {
                foreach (DmKakuninJokyo kakuninJokyo in kakuninJokyos)
                {
                    DmDocComment ddc = IsExistsComment(comments, kakuninJokyo.MsUserID);

                    if (kakuninJokyo.KakuninDate != null && kakuninJokyo.KakuninDate > DateTime.MinValue)
                    {
                        DateTime kakuninDate = kakuninJokyo.KakuninDate;
                        string fullName = kakuninJokyo.FullName;
                        if (ddc != null)
                        {
                            kakuninDate = ddc.RegDate;
                            fullName += "[C]";
                        }
                        xls.Cell(CellStr(pre1)).Value = kakuninDate.ToShortDateString();
                        xls.Cell(CellStr(pre2)).Value = fullName;
                        
                        Row++;
                    }
                }
            }
            else
            {
                xls.Cell(CellStr(pre1)).Value = "(確認不要)";
                Row++;
            }
        }
        private DmDocComment IsExistsComment(List<DmDocComment> comments, string userId)
        {
            DmDocComment ret = null;

            var c = from p in comments
                    where p.MsUserID == userId
                    orderby p.RegDate descending
                    select p;
            if (c.Count<DmDocComment>() > 0)
            {
                foreach (DmDocComment ddc in c)
                {
                    ret = ddc;
                    break;
                }
            }

            return ret;
        }

        #region プロパティ

        private string CellStr( string Prefix )
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(Prefix, Row);
            return sb.ToString();
        }



        private string 発行元Cell = "A{0}";
        private string 船名Cell = "B{0}";
        private string 発行日Cell = "C{0}";
        private string 分類名Cell = "D{0}";
        private string 小分類名Cell = "E{0}";
        private string 文書番号Cell = "F{0}";
        private string 文書名Cell = "G{0}";
        private string 登録者日付Cell = "H{0}";
        private string 登録者氏名Cell = "I{0}";

        private string 要員1_日付Cell = "J{0}";
        private string 要員1_氏名Cell = "K{0}";
        private string 要員2_日付Cell = "L{0}";
        private string 要員2_氏名Cell = "M{0}";
        private string 要員3_日付Cell = "N{0}";
        private string 要員3_氏名Cell = "O{0}";
        private string 要員4_日付Cell = "P{0}";
        private string 要員4_氏名Cell = "Q{0}";
        private string 要員5_日付Cell = "R{0}";
        private string 要員5_氏名Cell = "S{0}";
        private string 要員6_日付Cell = "T{0}";
        private string 要員6_氏名Cell = "U{0}";
        private string 要員7_日付Cell = "V{0}";
        private string 要員7_氏名Cell = "W{0}";
        private string 要員8_日付Cell = "X{0}";
        private string 要員8_氏名Cell = "Y{0}";
        private string 要員9_日付Cell = "Z{0}";
        private string 要員9_氏名Cell = "AA{0}";

        private string 部門1_日付Cell = "AB{0}";
        private string 部門1_氏名Cell = "AC{0}";
        private string 部門2_日付Cell = "AD{0}";
        private string 部門2_氏名Cell = "AE{0}";
        private string 部門3_日付Cell = "AF{0}";
        private string 部門3_氏名Cell = "AG{0}";
        private string 部門4_日付Cell = "AH{0}";
        private string 部門4_氏名Cell = "AI{0}";

        private string 本船日付Cell = "AJ{0}";
        private string 本船氏名Cell = "AK{0}";
        private string 完了日付Cell = "AL{0}";
        private string 完了氏名Cell = "AM{0}";




        private string get日付Cell(int index)
        {
            string ret = null;
            switch (index)
            {
                case 0: ret = 要員1_日付Cell; break;
                case 1: ret = 要員2_日付Cell; break;
                case 2: ret = 要員3_日付Cell; break;
                case 3: ret = 要員4_日付Cell; break;
                case 4: ret = 要員5_日付Cell; break;
                case 5: ret = 要員6_日付Cell; break;
                case 6: ret = 要員7_日付Cell; break;
                case 7: ret = 要員8_日付Cell; break;
                case 8: ret = 要員9_日付Cell; break;
                case 9: ret = 部門1_日付Cell; break;
                case 10: ret = 部門2_日付Cell; break;
                case 11: ret = 部門3_日付Cell; break;
                case 12: ret = 部門4_日付Cell; break;
            }
            return ret;
        }
        private string get氏名Cell(int index)
        {
            string ret = null;
            switch (index)
            {
                case 0: ret = 要員1_氏名Cell; break;
                case 1: ret = 要員2_氏名Cell; break;
                case 2: ret = 要員3_氏名Cell; break;
                case 3: ret = 要員4_氏名Cell; break;
                case 4: ret = 要員5_氏名Cell; break;
                case 5: ret = 要員6_氏名Cell; break;
                case 6: ret = 要員7_氏名Cell; break;
                case 7: ret = 要員8_氏名Cell; break;
                case 8: ret = 要員9_氏名Cell; break;
                case 9: ret = 部門1_氏名Cell; break;
                case 10: ret = 部門2_氏名Cell; break;
                case 11: ret = 部門3_氏名Cell; break;
                case 12: ret = 部門4_氏名Cell; break;
            }
            return ret;
        }


        #endregion
    }
}
