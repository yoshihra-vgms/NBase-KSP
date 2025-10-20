using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DS;
using NBaseData.DAC;
using LidorSystems.IntegralUI.Lists;
using System.Drawing;

namespace Yojitsu.util
{
    public class CellItem_入渠
    {
        public BgUchiwakeYosanItem YosanItem { get; private set; }

        public bool ReadOnly { get; set; }
        public bool Edited { get; private set; }

        private TreeListViewSubItem subItemAmount;
        public TreeListViewSubItem SubItemAmount
        {
            get
            {
                return subItemAmount;
            }

            set
            {
                subItemAmount = value;
            }
        }

        private TreeListViewSubItem subItemBikou;
        public TreeListViewSubItem SubItemBikou
        {
            get
            {
                return subItemBikou;
            }

            set
            {
                subItemBikou = value;
            }
        }

        private List<CellItem_入渠> parents;
        public List<CellItem_入渠> Parents
        {
            get
            {
                if (parents == null)
                {
                    parents = new List<CellItem_入渠>();
                }

                return parents;
            }
        }

        private List<CellItem_入渠> vChildrenPlus;
        public List<CellItem_入渠> VChildrenPlus
        {
            get
            {
                if (vChildrenPlus == null)
                {
                    vChildrenPlus = new List<CellItem_入渠>();
                }

                return vChildrenPlus;
            }
        }


        public CellItem_入渠(BgUchiwakeYosanItem yosanItem)
        {
            this.YosanItem = yosanItem;
        }


        internal void SetAmount(decimal newAmount)
        {
            SetAmount(newAmount, true);
        }


        internal void SetAmount(decimal newAmount, bool edited)
        {
            if (YosanItem.Amount != newAmount)
            {
                YosanItem.Amount = newAmount;
                Edited = edited;
            }
        }

        
        internal void SetBikou(string newBikou)
        {
            if (YosanItem.Bikou != newBikou)
            {
                YosanItem.Bikou = newBikou;
                Edited = true;
            }
        }


        internal void NotifyParents()
        {
            NotifyParents(true);
        }


        internal void NotifyParents(bool edited)
        {
            foreach (CellItem_入渠 p in Parents)
            {
                p.CalcTotal(edited);
                p.NotifyParents(edited);
            }
        }


        private void CalcTotal()
        {
            CalcTotal(true);
        }


        private void CalcTotal(bool edited)
        {
            CalcAmountTotal(edited);
        }


        private void CalcAmountTotal(bool edited)
        {
            decimal total = 0;

            foreach (CellItem_入渠 c in VChildrenPlus)
            {
                total += c.YosanItem.Amount;
            }

            SubItemAmount.Text = NBaseCommon.Common.金額出力(total / 1000);

            SetAmount(total, edited);
        }


        public class Collection
        {
            private readonly Dictionary<int, Dictionary<string, List<CellItem_入渠>>> itemsDic =
                               new Dictionary<int, Dictionary<string, List<CellItem_入渠>>>();

            private static string 備考 = "備考";
            public static int MS_HIMOKU_ID_備考 = int.MinValue;
            

            public void Add(BgUchiwakeYosanItem yosanItem)
            {
                int himokuId = yosanItem.MsHimokuID;
                string nengetsu = yosanItem.Nengetsu.Trim();

                InitCollection(himokuId, nengetsu);

                CellItem_入渠 cellItem = new CellItem_入渠(yosanItem);

                if (yosanItem.MsHimokuID == MS_HIMOKU_ID_備考)
                {
                    itemsDic[himokuId][nengetsu][0] = cellItem;
                }
                else
                {
                    itemsDic[himokuId][nengetsu].Insert(itemsDic[himokuId][nengetsu].Count - 1, cellItem);
                }
            }


            internal CellItem_入渠 Get(HimokuTreeNode htNode, string year, int index)
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


            internal bool IsLast(CellItem_入渠 cellItem)
            {
                if (cellItem.YosanItem.MsHimokuID == MS_HIMOKU_ID_備考)
                {
                    return false;
                }
                else
                {
                    int himokuId = cellItem.YosanItem.MsHimokuID;
                    string nengetsu = cellItem.YosanItem.Nengetsu.Trim();

                    return itemsDic[himokuId][nengetsu].IndexOf(cellItem) == itemsDic[himokuId][nengetsu].Count - 1;
                }
            }


            public IEnumerator<CellItem_入渠> GetEnumerator()
            {
                foreach (Dictionary<string, List<CellItem_入渠>> dic in itemsDic.Values)
                {
                    foreach (List<CellItem_入渠> list in dic.Values)
                    {
                        int i = 0;
                        foreach (CellItem_入渠 item in list)
                        {
                            item.YosanItem.ShowOrder = ++i;
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
                else if(htNode.Name == 備考)
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
                    var items = new Dictionary<string, List<CellItem_入渠>>();
                    itemsDic.Add(himokuId, items);
                }

                if (!itemsDic[himokuId].ContainsKey(nengetsu))
                {
                    var items = new List<CellItem_入渠>();

                    BgUchiwakeYosanItem yosanItem = new BgUchiwakeYosanItem();
                    yosanItem.MsHimokuID = himokuId;
                    yosanItem.Nengetsu = nengetsu;

                    items.Add(new CellItem_入渠(yosanItem));
                    
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

                foreach (List<CellItem_入渠> items in itemsDic[id].Values)
                {
                    if (maxRow < items.Count)
                    {
                        maxRow = items.Count;
                    }
                }

                return maxRow;
            }


            internal Dictionary<string, List<CellItem_入渠>> GetParentCellItems(HimokuTreeNode htNode)
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


            internal void NotifyParents()
            {
                foreach (Dictionary<string, List<CellItem_入渠>> dic in itemsDic.Values)
                {
                    foreach (List<CellItem_入渠> items in dic.Values)
                    {
                        foreach (CellItem_入渠 i in items)
                        {
                            i.NotifyParents(false);
                        }
                    }
                }
            }


            internal void AddNew(CellItem_入渠 cellItem_入渠)
            {
                BgUchiwakeYosanItem yosanItem = new BgUchiwakeYosanItem();
                yosanItem.MsHimokuID = cellItem_入渠.YosanItem.MsHimokuID;
                yosanItem.Nengetsu = cellItem_入渠.YosanItem.Nengetsu;

                itemsDic[yosanItem.MsHimokuID][yosanItem.Nengetsu].Add(new CellItem_入渠(yosanItem));
            }
        }
    }
}
