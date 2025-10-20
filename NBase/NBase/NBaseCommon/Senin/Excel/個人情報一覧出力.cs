using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using NBaseData.DS;
using ExcelCreator = AdvanceSoftware.ExcelCreator;

namespace NBaseCommon.Senin.Excel
{
    public class 個人情報一覧出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;


        public 個人情報一覧出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }


        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache, DateTime date, int msVesselId)
        {
            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                //-----------------------
                //2013/12/17 コメントアウト
                //xls.OpenBook(outputFilePath, templateFilePath);
                //-----------------------
                //2013/12/17 変更
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
                //-----------------------

                // 全船
                if (msVesselId == int.MinValue)
                {
                    int i = 0;
                    foreach (MsVessel v in MsVessel.GetRecordsBySeninEnabled(loginUser))
                    {
                        i++;
                        xls.CopySheet(0, i, "");
                        xls.SheetNo = i;
                        xls.SheetName = v.VesselName;

                        _CreateFile(loginUser, xls, seninTableCache, date, v.MsVesselID, v.VesselName);
                    }
                    xls.DeleteSheet(0, 1);
                }
                // 各船
                else
                {
                    string vesselName = MsVessel.GetRecordByMsVesselID(loginUser, msVesselId).VesselName;
                    xls.SheetName = vesselName;

                    _CreateFile(loginUser, xls, seninTableCache, date, msVesselId, vesselName);
                }

                xls.CloseBook(true);
            }
        }


        private void _CreateFile(MsUser loginUser, ExcelCreator.Xlsx.XlsxCreator xls, SeninTableCache seninTableCache, DateTime date, int msVesselId, string vesselName)
        {
            SiCardFilter filter = new SiCardFilter();
            filter.MsVesselIDs.Add(msVesselId);
            filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船));
            filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船休暇));
            filter.Start = date;
            filter.End = date;
            filter.OrderByStr = "OrderByMsSiShokumeiId";
            filter.RetireFlag = 0;

            List<SiCard> cards = SiCard.GetRecordsByFilter(loginUser, filter);

            xls.Cell("**TODAY").Value = date.ToShortDateString();

            int startRow = 8;
            int i = 0;

            List<TreeListViewUtils.SiCardRow> rows = TreeListViewUtils.CreateRowData(cards, loginUser, seninTableCache);

            foreach (TreeListViewUtils.SiCardRow r in rows)
            {
                // 職名
                xls.Cell("A" + (startRow + i)).Value = seninTableCache.ToTopShokumeiAbbrStr(loginUser, r.card.SiLinkShokumeiCards);

                // 氏名
                xls.Cell("B" + (startRow + i)).Value = r.card.SeninName;

                // 乗船日
                xls.Cell("C" + (startRow + i)).Value = r.乗船日;

                // 海技免状有効期限・番号
                // 帳票出力フラグがチェックされていて、有効期限があるものを２つまで
                #region
                List<SiMenjou> menjous_海技免状 = SiMenjou.GetRecordsByMsSeninIDAndMsSiMenjouID(loginUser, r.card.MsSeninID, seninTableCache.MsSiMenjou_海技免状ID(loginUser));

                if (menjous_海技免状.Count == 1)
                {
                    MsSiMenjouKind k = seninTableCache.GetMsSiMenjouKind(loginUser, menjous_海技免状[0].MsSiMenjouKindID);

                    xls.Cell("D" + (startRow + i)).Value = k.NameAbbr;

                    if (menjous_海技免状[0].Kigen != DateTime.MinValue)
                    {
                        xls.Cell("D" + (startRow + i + 1)).Value = menjous_海技免状[0].Kigen.ToShortDateString();
                    }
                }
                else if (menjous_海技免状.Count > 1)
                {
                    int n = 0;
                    for (int m = 0; m < menjous_海技免状.Count; m++)
                    {
                        if (menjous_海技免状[m].ChouhyouFlag == 1)
                        {
                            MsSiMenjouKind k = seninTableCache.GetMsSiMenjouKind(loginUser, menjous_海技免状[m].MsSiMenjouKindID);
                            string name = k.NameAbbr;

                            if (name.Length == 0)
                            {
                                name = k.Name;
                            }

                            if (menjous_海技免状[m].Kigen != DateTime.MinValue)
                            {
                                xls.Cell("D" + (startRow + i + n)).Value = name + " " + menjous_海技免状[m].Kigen.ToShortDateString();
                                
                                // 2012.07.11:Add 1Lines
                                n++;
                            }

                            // 2012.07.11:Mod 1Lines
                            //n++;

                            if (n == 2) break;
                        }
                    }
                }
                #endregion

                // 船員手帳有効期限・番号
                // 帳票出力フラグがチェックされている１番目のもの
                #region
                List<SiMenjou> menjous_船員手帳 = SiMenjou.GetRecordsByMsSeninIDAndMsSiMenjouID(loginUser, r.card.MsSeninID, seninTableCache.MsSiMenjou_船員手帳ID(loginUser));

                if (menjous_船員手帳.Count > 0)
                {
                    //if (menjous_船員手帳[0].Kigen != DateTime.MinValue)
                    //{
                    //    xls.Cell("E" + (startRow + i)).Value = menjous_船員手帳[0].Kigen.ToShortDateString();
                    //}

                    //xls.Cell("F" + (startRow + i)).Value = menjous_船員手帳[0].No;

                    // 2012.07.11:Add 13Lines
                    for (int m = 0; m < menjous_船員手帳.Count; m++)
                    {
                        if (menjous_船員手帳[m].ChouhyouFlag == 0)
                        {
                            continue;
                        }
                        if (menjous_船員手帳[m].Kigen != DateTime.MinValue)
                        {
                            xls.Cell("E" + (startRow + i)).Value = menjous_船員手帳[m].Kigen.ToShortDateString();
                        }
                        xls.Cell("F" + (startRow + i)).Value = menjous_船員手帳[m].No;
                        break;
                    }

                }
                #endregion

                // 健康診断有効期限
                // 帳票出力フラグがチェックされている１番目のもの
                #region
                List<SiMenjou> menjous_健康診断 = SiMenjou.GetRecordsByMsSeninIDAndMsSiMenjouID(loginUser, r.card.MsSeninID, seninTableCache.MsSiMenjou_健康診断ID(loginUser));

                if (menjous_健康診断.Count > 0)
                {
                    //if (menjous_健康診断[0].Kigen != DateTime.MinValue)
                    //{
                    //    xls.Cell("G" + (startRow + i)).Value = menjous_健康診断[0].Kigen.ToShortDateString();
                    //}

                    // 2012.07.11:Add 12Lines
                    for (int m = 0; m < menjous_健康診断.Count; m++)
                    {
                        if (menjous_健康診断[m].ChouhyouFlag == 0)
                        {
                            continue;
                        }
                        if (menjous_健康診断[m].Kigen != DateTime.MinValue)
                        {
                            xls.Cell("G" + (startRow + i)).Value = menjous_健康診断[m].Kigen.ToShortDateString();
                        }
                        break;
                    }
                }
                #endregion

                // 当直部員
                // 帳票出力フラグがチェックされているものを２つまで
                #region
                List<SiMenjou> menjous_当直部員 = SiMenjou.GetRecordsByMsSeninIDAndMsSiMenjouID(loginUser, r.card.MsSeninID, seninTableCache.MsSiMenjou_当直部員ID(loginUser));

                if (menjous_当直部員.Count > 0)
                {
                    int n = 0;

                    for (int m = 0; m < menjous_当直部員.Count; m++)
                    {
                        // 2012.07.11:Add 3Lines
                        if (menjous_当直部員[m].ChouhyouFlag == 0)
                        {
                            continue;
                        }

                        MsSiMenjouKind k = seninTableCache.GetMsSiMenjouKind(loginUser, menjous_当直部員[m].MsSiMenjouKindID);
                        string name = k.NameAbbr;

                        if (name.Length == 0)
                        {
                            name = k.Name;
                        }

                        //xls.Cell("H" + (startRow + i + m)).Value = name;
                        xls.Cell("H" + (startRow + i + n)).Value = name;

                        // 2012.07.11:Add 2Lines
                        n++;
                        if (n == 2) break;
                    }
                }
                #endregion

                // 危険物取扱者
                // 帳票出力フラグがチェックされているものを２つまで、有効期限は１番目のもの
                #region
                List<SiMenjou> menjous_危険物取扱者 = SiMenjou.GetRecordsByMsSeninIDAndMsSiMenjouID(loginUser, r.card.MsSeninID, seninTableCache.MsSiMenjou_危険物取扱者ID(loginUser));

                if (menjous_危険物取扱者.Count > 0)
                {
                    int n = 0;

                    for (int m = 0; m < menjous_危険物取扱者.Count; m++)
                    {
                        // 2012.07.11:Add 3Lines
                        if (menjous_危険物取扱者[m].ChouhyouFlag == 0)
                        {
                            continue;
                        }

                        MsSiMenjouKind k = seninTableCache.GetMsSiMenjouKind(loginUser, menjous_危険物取扱者[m].MsSiMenjouKindID);
                        string name = k.NameAbbr;

                        if (name.Length == 0)
                        {
                            name = k.Name;
                        }

                        //if (m == 0)
                        if (n == 0)
                        {
                            if (menjous_危険物取扱者[m].Kigen != DateTime.MinValue)
                            {
                                //xls.Cell("I" + (startRow + i + m)).Value = menjous_危険物取扱者[m].Kigen.ToShortDateString();
                                xls.Cell("I" + (startRow + i + n)).Value = menjous_危険物取扱者[m].Kigen.ToShortDateString();
                            }
                        }
                        
                        //xls.Cell("J" + (startRow + i + m)).Value = name;
                        xls.Cell("J" + (startRow + i + n)).Value = name;
                        
                        // 2012.07.11:Add 2Lines
                        n++;
                        if (n == 2) break;
                   }
                }
                #endregion

                i += 2;
            }

            //xls.PrintArea(0, 0, 10, startRow - 2 + (((cards.Count - 1) / 10 + 1) * 20));
            int pageParData = 12;
            xls.PrintArea(0, 0, 9, startRow - 2 + (((cards.Count - 1) / pageParData + 1) * (pageParData*2)));
        }
    }
}
