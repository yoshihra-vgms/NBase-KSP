using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseData.DAC;

namespace NBaseData.BLC
{
    public class 簡易動静表
    {
        public static void 動静表_取得(NBaseData.DAC.MsUser loginUser, DateTime today, string path, string templateName, string outPutFileName)
        {
            string BaseFileName = "動静表";

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
                    return;
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
                //-----------------------------------

                int offset_y = 0;

                // ヘッダー出力
                //DateTime headColDateTime = today.AddDays(-1);
                DateTime headColDateTime = today;

                DateTime fromDatetime = new DateTime(headColDateTime.Year, headColDateTime.Month, headColDateTime.Day);
                DateTime toDatetime = fromDatetime.AddDays(11);
                toDatetime = toDatetime.AddSeconds(-1);

                xls.Cell("A2").Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                xls.Cell("B2").Value = "船名";
                for (int n = 0; n < 11; n++)
                {
                    xls.Cell("C2", n, offset_y).Value = headColDateTime.ToString("MM/dd(ddd)");
                    headColDateTime = headColDateTime.AddDays(1);
                }
                xls.Cell("**date").Value = today.ToString("yyyy年MM月dd日");

                // 簡易動静情報の対象となる船を取得
                //List<MsVessel> msVesselList = MsVessel.GetRecordsByKanidouseiEnabled(new MsUser());
                List<MsVessel> msVesselList = MsVessel.GetRecordsByKanidouseiEnabled(new MsUser()).Where(obj => obj.KanidouseiEnabled == 1).ToList();
                List<PtKanidouseiInfo> ptKanidouseiInfo_list =
                    PtKanidouseiInfo.GetRecordByEventDate(loginUser, fromDatetime, toDatetime);


                foreach (MsVessel msVessel in msVesselList)
                {
                    xls.Cell("A3", 0, offset_y).Value = (offset_y + 1).ToString();

                    string strVesselInfo = "";
                    strVesselInfo = msVessel.VesselName;
                    strVesselInfo += "\n";
                    strVesselInfo += msVessel.CaptainName;
                    strVesselInfo += "\n";
                    strVesselInfo += msVessel.Tel;
                    strVesselInfo += "\n";
                    strVesselInfo += msVessel.HpTel;
                    xls.Cell("B3", 0, offset_y).Value = strVesselInfo;
                    xls.Cell("A3", 0, offset_y).Attr.LineLeft (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                    // 日付ごとの予定を出す
                    //DateTime colDateTime = today.AddDays(-1);
                    DateTime colDateTime = today;
                    for (int n = 0; n < 11; n++)
                    {
                        PtKanidouseiInfo[] PtKanidouseiInfos = GetPtKanidouseiInfo(colDateTime, msVessel, ptKanidouseiInfo_list);
                        xls.Cell("C3", n, offset_y).Attr.VerticalAlignment = ExcelCreator.VerticalAlignment.Top;
                        xls.Cell("C3", n, offset_y).Value = MakeKoma(PtKanidouseiInfos);

                        colDateTime = colDateTime.AddDays(1);
                    }

                    offset_y++;
                }

                #region 罫線を出力
                for (int i = 0; i < (offset_y + 2); i++)
                {
                    xls.Cell("A1:M1", 0, i).Attr.LineBottom (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                }

                string toA = "A" + (offset_y + 2).ToString();
                for (int i = 0; i < 13; i++)
                {
                    xls.Cell("A2:" + toA, i, 0).Attr.LineRight (ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                }

                #endregion

                xls.CloseBook(true);

                if (xls.ErrorNo == ExcelCreator.ErrorNo.NoError)
                {
                    //res = true;
                }

            }
        }

        private static string MakeKoma(PtKanidouseiInfo[] PtKanidouseiInfos)
        {
            StringBuilder sb = new StringBuilder();

            //sb.Append(MakeKomaDetail(PtKanidouseiInfos[0]));
            //if (PtKanidouseiInfos[0] != null)
            //{
            //    sb.Append("\n");
            //}
            //else
            //{
            //    sb.Append("\n\n");
            //}
            //sb.Append(MakeKomaDetail(PtKanidouseiInfos[1]));
            for(int i = 0; i < PtKanidouseiInfos.Length; i ++)
            {
                sb.Append(MakeKomaDetail(PtKanidouseiInfos[i]));
                if (i + 1 < PtKanidouseiInfos.Length)
                {
                    sb.Append("\n");
                }
            }

            return sb.ToString();
        }
        private static string MakeKomaDetail(PtKanidouseiInfo PtKanidouseiInfo)
        {
            StringBuilder sb = new StringBuilder();

            // 予定無し
            if (PtKanidouseiInfo != null)
            {
                sb.Append(PtKanidouseiInfo.BashoName);
                //簡易動静表では基地名は表示しない2010.2.24(kohji@chi.co.jp)
                //if (PtKanidouseiInfo.KitiName != "")
                //{
                //    sb.AppendFormat("：{0}", PtKanidouseiInfo.KitiName);
                //}
                sb.Append("\n");
                if (PtKanidouseiInfo.MsKanidouseiInfoShubetuId != MsKanidouseiInfoShubetu.不明ID)
                {
                    sb.Append(PtKanidouseiInfo.KanidouseiInfoShubetuName);
                }
            }
            return sb.ToString();
        }

        private static PtKanidouseiInfo[] GetPtKanidouseiInfo(DateTime colDateTime, MsVessel msVessel, List<PtKanidouseiInfo> ptKanidouseiInfo_list)
        {
            //PtKanidouseiInfo[] ret = new PtKanidouseiInfo[2];

            //var Vessel毎ptKanidouseiInfo_list = from list in ptKanidouseiInfo_list
            //                                   where list.MsVesselID == msVessel.MsVesselID
            //                                   orderby list.EventDate, list.Koma
            //                                   select list;

            ////コマ０を検索
            //foreach (PtKanidouseiInfo kanidousei in Vessel毎ptKanidouseiInfo_list)
            //{
            //    if (kanidousei.EventDate.ToString("yyyyMMdd") == colDateTime.ToString("yyyyMMdd") && kanidousei.Koma == 0)
            //    {
            //        ret[0] = kanidousei;
            //        break;
            //    }
            //}

            ////コマ１を検索
            //foreach (PtKanidouseiInfo kanidousei in Vessel毎ptKanidouseiInfo_list)
            //{
            //    if (kanidousei.EventDate.ToString("yyyyMMdd") == colDateTime.ToString("yyyyMMdd") && kanidousei.Koma == 1)
            //    {
            //        ret[1] = kanidousei;
            //        break;
            //    }
            //}

            var Vessel毎ptKanidouseiInfo_list = from list in ptKanidouseiInfo_list
                                               where list.MsVesselID == msVessel.MsVesselID
                                               && list.EventDate.ToShortDateString() == colDateTime.ToShortDateString()
                                               orderby list.EventDate
                                               select list;
            return Vessel毎ptKanidouseiInfo_list.ToArray<PtKanidouseiInfo>();
        }
    }
}
