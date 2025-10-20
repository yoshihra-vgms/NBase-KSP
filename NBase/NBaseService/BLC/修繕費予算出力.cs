using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;
using System.IO;
using NBaseData.DAC;
using NBaseData.DS;
using System.Collections.Generic;
using ExcelCreator=AdvanceSoftware.ExcelCreator;
using NBaseData.BLC;
using NBaseUtil;
using System.Text;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_Excel修繕費予算出力(MsUser loginUser, BgYosanHead yosanHead, int msVesselId);
    }


    public partial class Service
    {
        public byte[] BLC_Excel修繕費予算出力(MsUser loginUser, BgYosanHead yosanHead, int msVesselId)
        {
            Constants.LoginUser = loginUser;

            #region 元になるファイルの確認と出力ファイル生成
            string BaseFileName = "修繕費予算出力";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFileName = path + "Template_" + BaseFileName + ".xlsx";
            string outPutFileName = path + "outPut_[" + loginUser.FullName + "]_" + BaseFileName + ".xlsx";

            #endregion

            ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator();

            try
            {
                // 各船
                if (msVesselId != int.MinValue)
                {
                    if (xls.OpenBook(outPutFileName, templateFileName) == 0)
                    {
                        xls.SheetName = MsVessel.GetRecordByMsVesselID(loginUser, msVesselId).VesselName;

                        OutputSheet(loginUser, yosanHead, msVesselId, xls);
                    }
                }
                // 全船
                else
                {
                    List<MsVessel> vessels = MsVessel.GetRecordsByYojitsuEnabled(loginUser);

                    if (xls.OpenBook(outPutFileName, templateFileName) == 0)
                    {
                        for (int i = 0; i < vessels.Count; i++)
                        {
                            xls.CopySheet(0, i + 1, "");
                            xls.SheetNo = i + 1;
                            xls.SheetName = vessels[i].VesselName;

                            OutputSheet(loginUser, yosanHead, vessels[i].MsVesselID, xls);
                        }
                        xls.DeleteSheet(0, 1);
                    }
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
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


        private void OutputSheet(MsUser loginUser, BgYosanHead yosanHead, int msVesselId, ExcelCreator.Xlsx.XlsxCreator xls)
        {
            // 予算
            List<BgUchiwakeYosanItem> yosanItems =
                      BgUchiwakeYosanItem.GetRecords_入渠(loginUser,
                                                    yosanHead.YosanHeadID,
                                                    msVesselId,
                                                    (yosanHead.Year - 1).ToString(),
                                                    (yosanHead.Year + NBaseData.BLC.予実.GetYearRange(yosanHead.YosanSbtID)).ToString()
                                                   );

            // 船稼働
            List<BgKadouVessel> kadouVessels = BgKadouVessel.GetRecordsByYosanHeadIDAndMsVesselID(loginUser,
                                                                                                        yosanHead.YosanHeadID,
                                                                                                        msVesselId);

            // 備考
            BgYosanBikou yosanBikou = BgYosanBikou.GetRecordByYosanHeadIDAndMsVesselID(loginUser,
                                                                                                        yosanHead.YosanHeadID,
                                                                                                        msVesselId);

            CreateHeaderRow(xls, yosanHead, kadouVessels);

            int row = 1;
            int rowNo_入渠費計 = 1;
            HimokuTreeNode root = HimokuTreeReader_入渠.GetHimokuTree();
            Collection collection = BuildColection(yosanItems);
            
            foreach (HimokuTreeNode node in root.Children)
            {
                CreateRows(xls, node, ref row, yosanHead, collection, ref rowNo_入渠費計);
            }

            int x = (NBaseData.BLC.予実.GetYearRange(yosanHead.YosanSbtID) - 1 * 2) + 1;
            string lineCellStr = ExcelUtils.ToCellName(0, row) +
                                          ":" + ExcelUtils.ToCellName(x, row);
            xls.Cell(lineCellStr).Attr.LineTop (ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

            if (yosanBikou != null)
            {
                xls.Pos(0, row + 1).Value = yosanBikou.Bikou;
            }
        }


        private void CreateHeaderRow(ExcelCreator.Xlsx.XlsxCreator xls, BgYosanHead yosanHead, List<BgKadouVessel> kadouVessels)
        {
            xls.Pos(0, 0).Value = "費目\n稼働期間\n検査種別 / 不稼働月 / 前 / 入渠 / 後";
            xls.Pos(0, 0).ColWidth = 32;

            int x = 0;
            for (int i = 0; i < NBaseData.BLC.予実.GetYearRange(yosanHead.YosanSbtID); i++)
            {
                x = (i * 2) + 1;

                if (i < kadouVessels.Count)
                {
                    xls.Pos(x, 0).Value = (yosanHead.Year + i) + " 年度予算\n" + Create船稼働String(kadouVessels[i]);
                }
                else
                {
                    xls.Pos(x, 0).Value = (yosanHead.Year + i) + " 年度予算\n";
                }

                xls.Pos(x, 0).ColWidth = 10;
                xls.Pos(x + 1, 0).ColWidth = 20;
                xls.Pos(x, 0, x + 1, 0).Attr.MergeCells = true;

                string lineCellStr = ExcelUtils.ToCellName(x, 0);
                xls.Cell(lineCellStr).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

                lineCellStr = ExcelUtils.ToCellName(x + 1, 0);
                xls.Cell(lineCellStr).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black) ;
            }

            string lineCellStr2 = ExcelUtils.ToCellName(0, 0) +
                                          ":" + ExcelUtils.ToCellName(x + 1, 0);
            xls.Cell(lineCellStr2).Attr.LineTop (ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
            xls.Cell(lineCellStr2).Attr.LineBottom (ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);

            lineCellStr2 = ExcelUtils.ToCellName(0, 0);
            xls.Cell(lineCellStr2).Attr.LineLeft (ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

            lineCellStr2 = ExcelUtils.ToCellName(x + 1, 0);
            xls.Cell(lineCellStr2).Attr.LineRight (ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

            lineCellStr2 = ExcelUtils.ToCellName(0, 0);
            xls.Cell(lineCellStr2).Attr.LineRight (ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
        }


        internal static string Create船稼働String(BgKadouVessel kadouVessel)
        {
            if (kadouVessel.KadouStartDate == DateTime.MinValue || kadouVessel.KadouEndDate == DateTime.MinValue)
            {
                return "不稼働";
            }
            else
            {
                return Create稼働期間Str(kadouVessel) + "\n" + Create定期点検Str(kadouVessel);
            }
        }


        private static string Create稼働期間Str(BgKadouVessel bgKadouVessel)
        {
            if (bgKadouVessel.KadouStartDate.Month == 4 && bgKadouVessel.KadouStartDate.Day == 1 &&
                    AmOrPm(bgKadouVessel.KadouStartDate) == "AM" &&
                    bgKadouVessel.KadouEndDate.Month == 3 && bgKadouVessel.KadouEndDate.Day == 31 &&
                    AmOrPm(bgKadouVessel.KadouEndDate) == "PM")
            {
                return string.Empty;
            }

            StringBuilder buff = new StringBuilder();

            if (bgKadouVessel.KadouStartDate.Month != 4 || bgKadouVessel.KadouStartDate.Day != 1 ||
                    AmOrPm(bgKadouVessel.KadouStartDate) != "AM")
            {
                buff.Append(bgKadouVessel.KadouStartDate.ToString("MM/dd"));
                buff.Append(AmOrPm(bgKadouVessel.KadouStartDate));
            }

            buff.Append(" ～ ");

            if (bgKadouVessel.KadouEndDate.Month != 3 || bgKadouVessel.KadouEndDate.Day != 31 ||
                    AmOrPm(bgKadouVessel.KadouEndDate) != "PM")
            {
                buff.Append(bgKadouVessel.KadouEndDate.ToString("MM/dd"));
                buff.Append(AmOrPm(bgKadouVessel.KadouEndDate));
            }

            return buff.ToString();
        }


        internal static string AmOrPm(DateTime dateTime)
        {
            if (dateTime.Hour == 0)
            {
                return "AM";
            }
            else
            {
                return "PM";
            }
        }


        private static string Create定期点検Str(BgKadouVessel kadouVessel)
        {
            if (kadouVessel.NyukyoKind == null || kadouVessel.NyukyoKind == string.Empty)
            {
                return string.Empty;
            }

            StringBuilder buff = new StringBuilder();

            buff.Append(kadouVessel.NyukyoKind);
            buff.Append("/");
            buff.Append(kadouVessel.NyukyoMonth);
            buff.Append("/");
            buff.Append(kadouVessel.Fukadoubi1);
            buff.Append("/");
            buff.Append(kadouVessel.Fukadoubi2);
            buff.Append("/");
            buff.Append(kadouVessel.Fukadoubi3);

            return buff.ToString();
        }


        private Collection BuildColection(List<BgUchiwakeYosanItem> yosanItems)
        {
            Collection col = new Collection();

            foreach (BgUchiwakeYosanItem item in yosanItems)
            {
                col.Add(item);
            }

            return col;
        }


        private void CreateRows(ExcelCreator.Xlsx.XlsxCreator xls, HimokuTreeNode himokuTreeNode, ref int row, BgYosanHead yosanHead, Collection collection, ref int rowNo_入渠費計)
        {
            foreach (HimokuTreeNode node in himokuTreeNode.Children)
            {
                CreateRows(xls, node, ref row, yosanHead, collection, ref rowNo_入渠費計);
            }

            xls.Pos(0, row).Value = himokuTreeNode.Name;

            bool Is入渠費計 = false;
            if (himokuTreeNode.Name == "入渠費計")
            {
                Is入渠費計 = true;
            }

            bool Is合計 = false;
            if (himokuTreeNode.Name == "合計")
            {
                Is合計 = true;
            }

            int rowCount = collection.MaxRow(himokuTreeNode);
            int x = 0;
            
            for (int i = 0; i < NBaseData.BLC.予実.GetYearRange(yosanHead.YosanSbtID); i++)
            {
                string year = (yosanHead.Year + i).ToString();

                for (int k = 0; k < rowCount; k++)
                {
                    BgUchiwakeYosanItem yosanItem = collection.Get(himokuTreeNode, year, k);

                    x = (i * 2) + 1;
                    
                    if (yosanItem != null)
                    {
                        if (Is入渠費計)
                        {
                            xls.Pos(x, row + k).Value = "=SUM(" + ExcelUtils.ToCellName(x, 1) + ":" + ExcelUtils.ToCellName(x, row + k - 1) + ")";
                            rowNo_入渠費計 = row + k;
                        }
                        else if (Is合計)
                        {
                            xls.Pos(x, row + k).Value = "=SUM(" + ExcelUtils.ToCellName(x, rowNo_入渠費計) + ":" + ExcelUtils.ToCellName(x, row + k - 1) + ")";
                        }
                        else
                        {
                            xls.Pos(x, row + k).Value = yosanItem.Amount;
                            xls.Pos(x + 1, row + k).Value = yosanItem.Bikou;
                        }
                    }
                    else
                    {
                        xls.Pos(x, row + k).Value = 0;
                    }

                    string lineCellStr = ExcelUtils.ToCellName(x + 1, row + k);
                    xls.Cell(lineCellStr).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
                }
            }
            
            row += rowCount;

            string lineCellStr2 = ExcelUtils.ToCellName(0, row) +
                                          ":" + ExcelUtils.ToCellName(x + 1, row);
            xls.Cell(lineCellStr2).Attr.LineTop(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

            lineCellStr2 = ExcelUtils.ToCellName(0, row - rowCount);

            if (rowCount > 1)
            {
                lineCellStr2 += ":" + ExcelUtils.ToCellName(0, row);
            }

            xls.Cell(lineCellStr2).Attr.LineLeft (ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

            lineCellStr2 = ExcelUtils.ToCellName(x + 1, row - rowCount);

            if (rowCount > 1)
            {
                lineCellStr2 += ":" + ExcelUtils.ToCellName(x + 1, row);
            }

            xls.Cell(lineCellStr2).Attr.LineRight (ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

            lineCellStr2 = ExcelUtils.ToCellName(0, row - rowCount);

            if (rowCount > 1)
            {
                lineCellStr2 += ":" + ExcelUtils.ToCellName(0, row);
            }

            xls.Cell(lineCellStr2).Attr.LineRight (ExcelCreator.BorderStyle.Double, ExcelCreator.xlColor.Black);
        }


        public class Collection
        {
            private readonly Dictionary<int, Dictionary<string, List<BgUchiwakeYosanItem>>> itemsDic =
                               new Dictionary<int, Dictionary<string, List<BgUchiwakeYosanItem>>>();

            private static string 備考 = "備考";
            public static int MS_HIMOKU_ID_備考 = int.MinValue;


            public void Add(BgUchiwakeYosanItem yosanItem)
            {
                int himokuId = yosanItem.MsHimokuID;
                string nengetsu = yosanItem.Nengetsu.Trim();

                InitCollection(himokuId, nengetsu);

                if (yosanItem.MsHimokuID == MS_HIMOKU_ID_備考)
                {
                    itemsDic[himokuId][nengetsu][0] = yosanItem;
                }
                else
                {
                    itemsDic[himokuId][nengetsu].Insert(itemsDic[himokuId][nengetsu].Count - 1, yosanItem);
                }
            }


            internal BgUchiwakeYosanItem Get(HimokuTreeNode htNode, string year, int index)
            {
                int id = GetKey(htNode);

                InitCollection(id, year);

                if (index < itemsDic[id][year].Count)
                {
                    return itemsDic[id][year][index];
                }
                else
                {
                    return null;
                }
            }


            internal bool IsLast(BgUchiwakeYosanItem yosanItem)
            {
                if (yosanItem.MsHimokuID == MS_HIMOKU_ID_備考)
                {
                    return false;
                }
                else
                {
                    int himokuId = yosanItem.MsHimokuID;
                    string nengetsu = yosanItem.Nengetsu.Trim();

                    return itemsDic[himokuId][nengetsu].IndexOf(yosanItem) == itemsDic[himokuId][nengetsu].Count - 1;
                }
            }


            public IEnumerator<BgUchiwakeYosanItem> GetEnumerator()
            {
                foreach (Dictionary<string, List<BgUchiwakeYosanItem>> dic in itemsDic.Values)
                {
                    foreach (List<BgUchiwakeYosanItem> list in dic.Values)
                    {
                        int i = 0;
                        foreach (BgUchiwakeYosanItem item in list)
                        {
                            item.ShowOrder = ++i;
                            yield return item;
                        }
                    }
                }
            }


            private static int GetKey(HimokuTreeNode htNode)
            {
                int id;

                if (htNode.MsHimoku != null)
                {
                    id = htNode.MsHimoku.MsHimokuID;
                }
                else if (htNode.Name == 備考)
                {
                    id = MS_HIMOKU_ID_備考;
                }
                else
                {
                    id = htNode.Name.GetHashCode();
                }

                return id;
            }


            private void InitCollection(int himokuId, string nengetsu)
            {
                if (!itemsDic.ContainsKey(himokuId))
                {
                    var items = new Dictionary<string, List<BgUchiwakeYosanItem>>();
                    itemsDic.Add(himokuId, items);
                }

                if (!itemsDic[himokuId].ContainsKey(nengetsu))
                {
                    var items = new List<BgUchiwakeYosanItem>();

                    BgUchiwakeYosanItem yosanItem = new BgUchiwakeYosanItem();
                    yosanItem.MsHimokuID = himokuId;
                    yosanItem.Nengetsu = nengetsu;

                    items.Add(yosanItem);

                    itemsDic[himokuId][nengetsu] = items;
                }
            }


            internal int MaxRow(HimokuTreeNode htNode)
            {
                int maxRow = 0;

                int id = GetKey(htNode);

                if (!itemsDic.ContainsKey(id))
                {
                    return 1;
                }

                foreach (List<BgUchiwakeYosanItem> items in itemsDic[id].Values)
                {
                    if (maxRow < items.Count)
                    {
                        maxRow = items.Count;
                    }
                }

                return maxRow;
            }


            internal Dictionary<string, List<BgUchiwakeYosanItem>> GetParentCellItems(HimokuTreeNode htNode)
            {
                if (htNode.MsHimoku == null && htNode.Name != null)
                {
                    if (htNode.Name == 備考)
                    {
                        return itemsDic[MS_HIMOKU_ID_備考];
                    }
                    else
                    {
                        return itemsDic[htNode.Name.GetHashCode()];
                    }
                }

                return null;
            }
        }
    }
}
