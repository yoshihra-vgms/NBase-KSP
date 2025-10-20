using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using NBaseData.DAC;

using ExcelCreator=AdvanceSoftware.ExcelCreator;
using NBaseData.DS;
using NBaseUtil;

namespace NBaseService
{
    class YosanTaihihyoWriter
    {
        private readonly string fileName;
        MsUser loginUser = null;

        private static readonly string msVesselType外航 = "5";
        private static readonly int MS_HIMOKU_ID_売上高 = 1;
        private static readonly int MS_HIMOKU_ID_販管費 = 65;
        private static readonly int MS_HIMOKU_ID_経常損益 = 73;

        public YosanTaihihyoWriter(string fileName, MsUser user)
        {
            this.fileName = fileName;
            this.loginUser = user;
        }


        public bool Write(BgYosanHead yosanHead1, BgYosanHead yosanHead2, decimal unit)
        {
            Constants.LoginUser = this.loginUser;

            //anahara
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFileName = path + "Template_予算対比表.xlsx";

            bool result = false;

            // 比較元
            List<BgYosanItem> yosanItems1 =
              BgYosanItem.GetRecords_年単位_全社(this.loginUser,
                                            yosanHead1.YosanHeadID,
                                            (yosanHead1.Year).ToString(),
                                            (yosanHead1.Year + 19).ToString()
                                           );
            // 比較対象
            List<BgYosanItem> yosanItems2 =
              BgYosanItem.GetRecords_年単位_全社(this.loginUser,
                                            yosanHead2.YosanHeadID,
                                            (yosanHead2.Year).ToString(),
                                            (yosanHead2.Year + 19).ToString()
                                           );
            // 比較元
            List<BgYosanItem> g_yosanItems1 =
              BgYosanItem.GetRecords_年単位_グループ(this.loginUser,
                                            yosanHead1.YosanHeadID,
                                            msVesselType外航,
                                            (yosanHead1.Year).ToString(),
                                            (yosanHead1.Year + 19).ToString()
                                           );
            // 比較対象
            List<BgYosanItem> g_yosanItems2 =
              BgYosanItem.GetRecords_年単位_グループ(this.loginUser,
                                            yosanHead2.YosanHeadID,
                                            msVesselType外航,
                                            (yosanHead2.Year).ToString(),
                                            (yosanHead2.Year + 19).ToString()
                                           );


            ExcelCreator.Xlsx.XlsxCreator  xls = new ExcelCreator.Xlsx.XlsxCreator ();

            try
            {
                if (xls.OpenBook(fileName, templateFileName) == 0)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        // 対象年の予算を抜き出す
                        // 比較元
                        var items1 = from p in yosanItems1
                                     where p.Nengetsu.Trim() == (yosanHead1.Year + i).ToString()
                                     orderby p.MsHimokuID
                                     select p;
                        // 比較対象
                        var items2 = from p in yosanItems2
                                     where p.Nengetsu.Trim() == (yosanHead1.Year + i).ToString()
                                     orderby p.MsHimokuID
                                     select p;
                        // 比較元
                        var gitems1 = from p in g_yosanItems1
                                     where p.Nengetsu.Trim() == (yosanHead1.Year + i).ToString()
                                     orderby p.MsHimokuID
                                     select p;
                        // 比較対象
                        var gitems2 = from p in g_yosanItems2
                                     where p.Nengetsu.Trim() == (yosanHead1.Year + i).ToString()
                                     orderby p.MsHimokuID
                                     select p;


                        if (items1.Count<BgYosanItem>() == 0 || items2.Count<BgYosanItem>() == 0)
                            break;

                        // シートをコピー
                        xls.CopySheet(0, i + 1, (yosanHead1.Year + i).ToString() + "年");


                        // 書き込む
                        xls.SheetNo = i + 1;
                        xls.Cell("A4").Value = yosanHead1.Year + "年度 [" + BgYosanSbt.ToName(yosanHead1.YosanSbtID) + " Rev." + CreateRevisitionStr(yosanHead1) + "] ";
                        if (unit == 1000)
                        {
                            xls.Cell("F4").Value = "単位：千円";
                        }
                        else
                        {
                            xls.Cell("F4").Value = "単位：百万円";
                        }

                        result = Write(xls, 0, yosanHead1, items1.ToList<BgYosanItem>(), gitems1.ToList<BgYosanItem>(), unit);

                        result = Write(xls, 1, yosanHead2, items2.ToList<BgYosanItem>(), gitems2.ToList<BgYosanItem>(), unit);
                    }

                    // テンプレートシートの削除
                    xls.DeleteSheet(0, 1);

                    // １番始めのシートをアクティブに
                    xls.ActiveSheet = 0;
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                xls.CloseBook(true);
            }

            return result;
        }

        private bool Write(ExcelCreator.Xlsx.XlsxCreator  xls, int colIndex, BgYosanHead yosanHead, List<BgYosanItem> yosanItems, List<BgYosanItem> g_yosanItems, decimal unit)
        {
            if (colIndex == 0)
            {
                xls.Cell("D5", colIndex, 0).Value = xls.SheetName;
            }
            else
            {
                xls.Cell("D5", colIndex, 0).Value = yosanHead.Year + "年度 [" + BgYosanSbt.ToName(yosanHead.YosanSbtID) + " Rev." + yosanHead.Revision.ToString() +"] ";
            }

            if (g_yosanItems.Count > 0)
            {
                var item1 = from p in g_yosanItems
                            where p.MsHimokuID == MS_HIMOKU_ID_売上高
                            select p;
                if (item1.Count<BgYosanItem>() > 0)
                {
                    BgYosanItem yi = item1.First<BgYosanItem>();
                    decimal amount = GetAmount(yi, yi.MsHimokuID);

                    xls.Cell("D1", colIndex, 0).Value = amount / unit;
                }
                var item2 = from p in g_yosanItems
                            where p.MsHimokuID == MS_HIMOKU_ID_経常損益
                            select p;
                if (item2.Count<BgYosanItem>() > 0)
                {
                    BgYosanItem yi = item2.First<BgYosanItem>();
                    decimal amount = GetAmount(yi, yi.MsHimokuID);

                    xls.Cell("D2", colIndex, 0).Value = amount / unit;
                }
                var item3 = from p in g_yosanItems
                            where p.MsHimokuID == MS_HIMOKU_ID_販管費
                            select p;
                if (item3.Count<BgYosanItem>() > 0)
                {
                    BgYosanItem yi = item3.First<BgYosanItem>();
                    decimal amount = GetAmount(yi, yi.MsHimokuID);

                    xls.Cell("D3", colIndex, 0).Value = amount / unit;
                }
            }

            foreach (BgYosanItem yosanItem in yosanItems)
            {
                if (yosanItem.Nengetsu.Trim().Length == 6)
                {
                    continue;
                }

                decimal amount = GetAmount(yosanItem, yosanItem.MsHimokuID);

                xls.Cell(DetectBaseCell(yosanItem.MsHimokuID), colIndex, 0).Value = amount / unit;
            }

            return true;
        }

        private static decimal GetAmount(IYojitsu yojitsu, int himokuId)
        {
            decimal amount;

            if (HimokuTreeReader.GetHimokuTreeNode(himokuId) == null)
            {
                amount = 0;
            }
            else if (HimokuTreeReader.GetHimokuTreeNode(himokuId).Dollar)
            {
                amount = yojitsu.YenAmount;
            }
            else
            {
                amount = yojitsu.Amount;
            }

            return amount;
        }


        private string DetectBaseCell(int msHimokuID)
        {
            return "**" + msHimokuID;
        }

        internal static string CreateRevisitionStr(BgYosanHead yosanHead)
        {
            string revStr = yosanHead.Revision.ToString();

            if (!yosanHead.IsFixed())
            {
                revStr += " (未 Fix)";
            }
            else
            {
                revStr += " (" + yosanHead.FixDate.ToString("yyyy/MM/dd") + " Fix)";
            }

            return revStr;
        }
    }
}
