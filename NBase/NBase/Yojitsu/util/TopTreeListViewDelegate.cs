using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseUtil;
using System.Drawing;
using LidorSystems.IntegralUI.Lists;
using NBaseData.DAC;
using LidorSystems.IntegralUI.Lists.Collections;
using System.Windows.Forms;
using Yojitsu.DA;

namespace Yojitsu.util
{
    class TopTreeListViewDelegate : YojitsuTreeListViewDelegate
    {
        internal TopTreeListViewDelegate(TreeListView treeListView)
            : base(treeListView)
        {
        }


        internal void CreateYojitsuTable(BgYosanHead yosanHead, List<BgKadouVessel> kadouVessels, List<BgYosanItem> yosanItems, List<BgJiseki> jisekis)
        {
            this.yosanHead = yosanHead;
            
            treeListView.SuspendUpdate();

            treeListView.Columns.Clear();
            treeListView.Nodes.Clear();

            SetColumns(CreateColumns(yosanHead, kadouVessels));

            Dictionary<Yojitsu.util.CellItem.CellItemKey, List<CellItem>> itemsDic = BuildCellItemDictionary(yosanItems, jisekis);
            CreateRows(null, HimokuTreeReader.GetHimokuTree(), itemsDic);
            //NotifyParents(itemsDic);

            treeListView.Columns[0].Fixed = ColumnFixedType.Left;
            treeListView.Columns[1].Fixed = ColumnFixedType.Left;
            treeListView.Columns[2].Fixed = ColumnFixedType.Left;
            treeListView.Columns[3].Fixed = ColumnFixedType.Left;
            treeListView.Columns[4].Fixed = ColumnFixedType.Left;

            Expand();
            treeListView.ResumeUpdate();
        }


        private List<TreeListViewDelegate.Column> CreateColumns(BgYosanHead yosanHead, List<BgKadouVessel> kadouVessels)
        {
            List<TreeListViewDelegate.Column> columns = new List<TreeListViewDelegate.Column>();

            // 費目
            TreeListViewDelegate.Column h = new TreeListViewDelegate.Column();
            h.headerContent = "<div><p>費目</p><p>稼働期間</p><p>検査種別 / 不稼働月 / 前 / 入渠 / 後</p></div>";
            h.width = 250;
            h.fixedWidth = true;
            columns.Add(h);

            // 担当
            h = new TreeListViewDelegate.Column();
            h.headerContent = "担当";
            h.width = 50;
            h.fixedWidth = true;
            columns.Add(h);

            string[] kinds = { "予算", "実績", "差異" };
            Color[] colors = { COLOR_予算, COLOR_実績, COLOR_差異 };
            
            for(int i = 0; i < kinds.Length; i++)
            {
                h = new TreeListViewDelegate.Column();

                if (i == 0 && kadouVessels != null)
                {
                    h.headerContent = "<div><p>" + yosanHead.Year + " 年度" + kinds[i] + "</p>" + Create船稼働String(kadouVessels[0]) + "</div>";
                }
                else
                {
                    h.headerContent = "<div><p>" + yosanHead.Year + " 年度" + kinds[i] + "</p><p>　</p><p>　</p></div>";
                }

                h.width = 100;
                h.fixedWidth = true;
                h.alignment = HorizontalAlignment.Right;
                h.headerColor = colors[i];
                columns.Add(h);
            }

            for (int i = 1; i < NBaseData.BLC.予実.GetYearRange(yosanHead.YosanSbtID); i++)
            {
                h = new TreeListViewDelegate.Column();

                if (kadouVessels != null)
                {
                    h.headerContent = "<div><p>" + (yosanHead.Year + i) + " 年度予算</p>" + Create船稼働String(kadouVessels[i]) + "</div>";
                }
                else
                {
                    h.headerContent = "<div><p>" + (yosanHead.Year + i) + " 年度予算</p>" + "<p>　</p><p>　</p>" + "</div>";
                }
                
                h.width = 100;
                h.fixedWidth = true;
                h.alignment = HorizontalAlignment.Right;
                h.headerColor = COLOR_予算;
                columns.Add(h);
            }

            return columns;
        }
        
        
        private static Dictionary<CellItem.CellItemKey, List<CellItem>>
            BuildCellItemDictionary(List<BgYosanItem> yosanItems, List<BgJiseki> jisekis)
        {
            var itemsDic = new Dictionary<CellItem.CellItemKey, List<CellItem>>();
            var jisekiDic = JisekiBuilder.Build_年(jisekis);

            foreach (BgYosanItem item in yosanItems)
            {
                CreateItems(itemsDic, jisekiDic, item, CurrencyFactory.金額種別.円);
                CreateItems(itemsDic, jisekiDic, item, CurrencyFactory.金額種別.ドル);
                CreateItems(itemsDic, jisekiDic, item, CurrencyFactory.金額種別.合計);
            }
            
            return itemsDic;
        }

        private static void CreateItems(Dictionary<CellItem.CellItemKey, List<CellItem>> itemsDic, 
                                          Dictionary<int, BgJiseki> jisekiDic, 
                                          BgYosanItem item, 
                                          CurrencyFactory.金額種別 amountKind)
        {
            Yojitsu.util.CellItem.CellItemKey key = new Yojitsu.util.CellItem.CellItemKey(item.MsHimokuID, amountKind);
            CurrencyFactory currency = CurrencyFactory.Create(amountKind);

            if (!itemsDic.ContainsKey(key))
            {
                var items = new List<CellItem>();
                itemsDic.Add(key, items);
            }

            // 予算
            var yosanCellItem = new CellItem(item, currency);
            yosanCellItem.ReadOnly = true;
            itemsDic[key].Add(yosanCellItem);

            if (itemsDic[key].Count == 1)
            {
                // 実績
                CellItem jisekiCellItem;

                if (jisekiDic.ContainsKey(item.MsHimokuID))
                {
                    BgJiseki jiseki = jisekiDic[item.MsHimokuID];
                    jisekiCellItem = new CellItem(jiseki, currency);
                }
                else
                {
                    jisekiCellItem = new CellItem(currency);
                }

                jisekiCellItem.ReadOnly = true;
                itemsDic[key].Add(jisekiCellItem);

                // 差異
                CellItem saiCellItem = new Yojitsu.util.CellItem.SaiCellItem(yosanCellItem, jisekiCellItem, currency);
                saiCellItem.ReadOnly = true;
                itemsDic[key].Add(saiCellItem);
            }
        }
    }
}
