using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseUtil;
using NBaseData.DAC;
using LidorSystems.IntegralUI.Lists;
using System.Drawing;
using LidorSystems.IntegralUI.Lists.Collections;
using System.Windows.Forms;
using Yojitsu.DA;
using NBaseData.DS;

namespace Yojitsu.util
{
    public abstract class YojitsuTreeListViewDelegate : TreeListViewDelegate
    {
        protected readonly Dictionary<TreeListViewSubItem, CellItem> hasYosanItemCellItems =
            new Dictionary<TreeListViewSubItem, CellItem>();

        public static readonly Color COLOR_予算 = Color.PaleGreen;
        public static readonly Color COLOR_実績 = Color.PowderBlue;
        public static readonly Color COLOR_差異 = Color.Khaki;

        protected bool Editable { get; set; }

        public delegate void EditEventHandler();
        public event EditEventHandler Edited;

        protected BgYosanHead yosanHead;


        internal YojitsuTreeListViewDelegate(TreeListView treeListView)
            : base(treeListView)
        {
            treeListView.SelectionMode = SelectionMode.MultiExtended;

            treeListView.HoverNodeStyle.BackColor = Color.DarkTurquoise;
            treeListView.FocusedNodeStyle.BackColor = Color.Lime;
            treeListView.SelectedNodeStyle.BackColor = Color.Lime;

            treeListView.Click += new EventHandler(treeListView_Click);
        }


        internal void CreateRows(TreeListViewNode parentTlvNode, HimokuTreeNode parentHtNode,
                                     Dictionary<Yojitsu.util.CellItem.CellItemKey, List<CellItem>> itemsDic)
        {
            int parentHimokuId = int.MinValue;
            if (parentTlvNode != null)
            {
                parentHimokuId = parentHtNode.MsHimoku.MsHimokuID;
            }

            foreach (HimokuTreeNode childHtNode in parentHtNode.Children)
            {
                TreeListViewNode childTlvNode = CreateNode(Color.White, childHtNode);

                // 費目名
                AddSubItem(childTlvNode, childHtNode.BgColor, childHtNode.Name, true);
                // 部門名
                AddSubItem(childTlvNode, Yojitsu.DA.Constants.GetMsBumonColor(childHtNode.MsHimoku.MsBumonID), DbTableCache.instance().GetMsBumon(childHtNode.MsHimoku.MsBumonID).BumonName, true);

                CreateRowData(childTlvNode, childHtNode, itemsDic, parentHimokuId, CurrencyFactory.金額種別.合計);

                CreateRow_内訳(childTlvNode, childHtNode, itemsDic, parentHimokuId, CurrencyFactory.金額種別.円);
                CreateRow_内訳(childTlvNode, childHtNode, itemsDic, parentHimokuId, CurrencyFactory.金額種別.ドル);

                Link合計(childHtNode, itemsDic);

                // 子費目について再帰呼び出し
                if (childHtNode.Children != null)
                {
                    CreateRows(childTlvNode, childHtNode, itemsDic);
                }

                SetNodeVisible(childHtNode, childTlvNode);
                AddNode(parentTlvNode, childTlvNode);
            }
        }


        protected void Expand()
        {
            foreach (TreeListViewNode n in treeListView.Nodes)
            {
                Expand(n);
            }
        }


        private void Expand(TreeListViewNode node)
        {
            if (node.Tag is HimokuTreeNode)
            {
                HimokuTreeNode htNode = node.Tag as HimokuTreeNode;
                
                //if (htNode.Children.Count > 0 || htNode.Dollar)
                if (htNode.Dollar)
                {
                    node.Expand();
                }
            }

            foreach (TreeListViewNode n in node.Nodes)
            {
                Expand(n);
            }
        }


        private static bool Is_合計表示(HimokuTreeNode childHtNode)
        {
            return childHtNode.Dollar || childHtNode.Children.Count > 0 ||
                     childHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_売上総利益 ||
                     childHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_営業損益 ||
                     childHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_経常損益 ||
                     childHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_税引前当期損益 ||
                     childHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_当期損益;
        }


        private static void SetNodeVisible(HimokuTreeNode childHtNode, TreeListViewNode childTlvNode)
        {
            if (Yojitsu.DA.Constants.BUMON_VISIBILITY.ContainsKey(childHtNode.MsHimoku.MsBumonID))
            {
                childTlvNode.Visible = Yojitsu.DA.Constants.BUMON_VISIBILITY[childHtNode.MsHimoku.MsBumonID];
            }
        }


        private void AddNode(TreeListViewNode parentTlvNode, TreeListViewNode childTlvNode)
        {
            if (parentTlvNode == null)
            {
                treeListView.Nodes.Add(childTlvNode);
            }
            else
            {
                parentTlvNode.Nodes.Add(childTlvNode);
            }
        }


        public TreeListViewSubItem AddSubItem(TreeListViewNode node, string value)
        {
            return AddSubItem(node, value, false);
        }

        public TreeListViewSubItem AddSubItem(TreeListViewNode node, string value, bool readOnly)
        {
            Color color;

            if (readOnly)
            {
                color = Color.Gainsboro;
            }
            else
            {
                color = Color.White;
            }

            return AddSubItem(node, color, value, readOnly);
        }

        public TreeListViewSubItem AddSubItem(TreeListViewNode node, Color color, string value)
        {
            return AddSubItem(node, color, value, false);
        }

        public TreeListViewSubItem AddSubItem(TreeListViewNode node, Color color, string value, bool readOnly)
        {
            return AddSubItem(node, color, value, readOnly, null);
        }


        public TreeListViewSubItem AddSubItem(TreeListViewNode node, Color color, string value, bool readOnly, EventHandler leaveEventHandler)
        {
            return AddSubItem(node, color, value, value, readOnly, leaveEventHandler);
        }


        public TreeListViewSubItem AddSubItem(TreeListViewNode node, Color color, string subItemValue, string textBoxValue, bool readOnly, EventHandler leaveEventHandler)
        {
            return AddSubItem(node, color, subItemValue, textBoxValue, readOnly, false, int.MinValue, ImeMode.Disable, leaveEventHandler);
        }


        public TreeListViewSubItem AddSubItem(TreeListViewNode node, Color color, string subItemValue, string textBoxValue, bool readOnly, bool multiLine, int maxLength, ImeMode imeMode, EventHandler leaveEventHandler)
        {
            TreeListViewSubItem subItem = new TreeListViewSubItem();
            subItem.StyleFromParent = false;
            subItem.NormalStyle.BackColor = color;

            subItem.Text = subItemValue;
            subItem.FormatStyle.Font = SubItemFont;

            if (!readOnly)
            {
                CreateSubItemTextBox(node, textBoxValue, multiLine, maxLength, imeMode, leaveEventHandler, subItem);
            }

            node.SubItems.Add(subItem);

            return subItem;
        }


        private void SetReadOnly(HimokuTreeNode htNode, CellItem item, out bool isReadOnly, out Color color)
        {
            // 読み書き可
            if (IsEditable(htNode, item))
            {
                isReadOnly = false;
                color = Color.White;
            }
            // 読み取り専用
            else
            {
                isReadOnly = true;
                color = Color.Gainsboro;
            }
        }


        protected static void NotifyParents(Dictionary<CellItem.CellItemKey, List<CellItem>> itemsDic)
        {
            foreach (List<CellItem> items in itemsDic.Values)
            {
                foreach (CellItem i in items)
                {
                    i.NotifyParents(false);
                }
            }
        }


        /// <summary>
        /// 売上総利益 = 売上高 - 売上原価
        /// </summary>
        /// <param name="parentHtNode"></param>
        /// <param name="itemsDic"></param>
        /// <param name="i"></param>
        /// <param name="item"></param>
        /// <param name="amountKind"></param>
        private static void Build売上総利益Relations(HimokuTreeNode parentHtNode, Dictionary<CellItem.CellItemKey, List<CellItem>> itemsDic, int i, CellItem item,
                                                        CurrencyFactory.金額種別 amountKind)
        {
            CellItem.CellItemKey key = new CellItem.CellItemKey(Yojitsu.DA.Constants.MS_HIMOKU_ID_売上総利益, amountKind);

            if (parentHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_売上高)
            {
                item.Parents.Add(itemsDic[key][i]);
                itemsDic[key][i].VChildrenPlus.Add(item);
            }
            else if (parentHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_売上原価)
            {
                item.Parents.Add(itemsDic[key][i]);
                itemsDic[key][i].VChildrenMinus.Add(item);
            }
        }


        /// <summary>
        /// 営業損益 = 売上総利益 - 販管費
        /// </summary>
        /// <param name="parentHtNode"></param>
        /// <param name="itemsDic"></param>
        /// <param name="i"></param>
        /// <param name="item"></param>
        /// <param name="amountKind"></param>
        private static void Build営業損益Relations(HimokuTreeNode parentHtNode, Dictionary<CellItem.CellItemKey, List<CellItem>> itemsDic, int i, CellItem item,
                                                        CurrencyFactory.金額種別 amountKind)
        {
            CellItem.CellItemKey key = new CellItem.CellItemKey(Yojitsu.DA.Constants.MS_HIMOKU_ID_営業損益, amountKind);

            if (parentHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_売上総利益)
            {
                item.Parents.Add(itemsDic[key][i]);
                itemsDic[key][i].VChildrenPlus.Add(item);
            }
            else if (parentHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_販管費)
            {
                item.Parents.Add(itemsDic[key][i]);
                itemsDic[key][i].VChildrenMinus.Add(item);
            }
        }


        /// <summary>
        /// 経常損益 = 営業損益 - 営業外費用
        /// </summary>
        /// <param name="parentHtNode"></param>
        /// <param name="itemsDic"></param>
        /// <param name="i"></param>
        /// <param name="item"></param>
        /// <param name="amountKind"></param>
        private static void Build経常損益Relations(HimokuTreeNode parentHtNode, Dictionary<CellItem.CellItemKey, List<CellItem>> itemsDic, int i, CellItem item,
                                                        CurrencyFactory.金額種別 amountKind)
        {
            CellItem.CellItemKey key = new CellItem.CellItemKey(Yojitsu.DA.Constants.MS_HIMOKU_ID_経常損益, amountKind);

            if (parentHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_営業損益)
            {
                item.Parents.Add(itemsDic[key][i]);
                itemsDic[key][i].VChildrenPlus.Add(item);
            }
            else if (parentHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_営業外費用)
            {
                item.Parents.Add(itemsDic[key][i]);
                itemsDic[key][i].VChildrenMinus.Add(item);
            }
        }


        /// <summary>
        /// 税引前当期損益 = 経常損益 + 特別利益 + 特別損失
        /// </summary>
        /// <param name="parentHtNode"></param>
        /// <param name="itemsDic"></param>
        /// <param name="i"></param>
        /// <param name="item"></param>
        /// <param name="amountKind"></param>
        private static void Build税引前当期損益Relations(HimokuTreeNode parentHtNode, Dictionary<CellItem.CellItemKey, List<CellItem>> itemsDic, int i, CellItem item,
                                                        CurrencyFactory.金額種別 amountKind)
        {
            CellItem.CellItemKey key = new CellItem.CellItemKey(Yojitsu.DA.Constants.MS_HIMOKU_ID_税引前当期損益, amountKind);

            if (parentHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_経常損益)
            {
                item.Parents.Add(itemsDic[key][i]);
                itemsDic[key][i].VChildrenPlus.Add(item);
            }
            else if (parentHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_特別利益)
            {
                item.Parents.Add(itemsDic[key][i]);
                itemsDic[key][i].VChildrenPlus.Add(item);
            }
            else if (parentHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_特別損失)
            {
                item.Parents.Add(itemsDic[key][i]);
                itemsDic[key][i].VChildrenPlus.Add(item);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentHtNode"></param>
        /// <param name="itemsDic"></param>
        /// <param name="i"></param>
        /// <param name="item"></param>
        /// <param name="amountKind"></param>
        private static void Build当期損益Relations(HimokuTreeNode parentHtNode, Dictionary<CellItem.CellItemKey, List<CellItem>> itemsDic, int i, CellItem item,
                                                       CurrencyFactory.金額種別 amountKind)
        {
            CellItem.CellItemKey key = new CellItem.CellItemKey(Yojitsu.DA.Constants.MS_HIMOKU_ID_当期損益, amountKind);

            if (parentHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_税引前当期損益)
            {
                item.Parents.Add(itemsDic[key][i]);
                itemsDic[key][i].VChildrenPlus.Add(item);
            }
            else if (parentHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_法人税等)
            {
                item.Parents.Add(itemsDic[key][i]);
                itemsDic[key][i].VChildrenMinus.Add(item);
            }
        }


        private bool IsEditable(HimokuTreeNode parentHtNode, CellItem item)
        {
            if (parentHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_貨物運賃 ||
                parentHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_貨物費 ||
                parentHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_燃料費 ||
                parentHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_港費 ||
                parentHtNode.MsHimoku.MsHimokuID == Yojitsu.DA.Constants.MS_HIMOKU_ID_その他運航費)
            {
                // 2013.02:　上記、費目は、入力補助画面でのみ変更可とする。
                return false;
            }
            else
            {
                return Editable && Yojitsu.DA.Constants.BelongMsBumon(parentHtNode.MsHimoku.MsBumonID) && !item.ReadOnly &&
                         item.Currency.Type() != CurrencyFactory.金額種別.合計 && parentHtNode.Children.Count == 0;
            }
        }


        private void Link合計(HimokuTreeNode childHtNode, Dictionary<CellItem.CellItemKey, List<CellItem>> itemsDic)
        {
            List<CellItem> cellItems円 = itemsDic[new CellItem.CellItemKey(childHtNode.MsHimoku.MsHimokuID, CurrencyFactory.金額種別.円)];
            List<CellItem> cellItemsドル = itemsDic[new CellItem.CellItemKey(childHtNode.MsHimoku.MsHimokuID, CurrencyFactory.金額種別.ドル)];
            List<CellItem> cellItems合計 = itemsDic[new CellItem.CellItemKey(childHtNode.MsHimoku.MsHimokuID, CurrencyFactory.金額種別.合計)];

            for (int i = 0; i < cellItems合計.Count; i++)
            {
                cellItems円[i].CellItem合計 = cellItems合計[i];
                cellItemsドル[i].CellItem合計 = cellItems合計[i];
            }
        }


        private void CreateRow_内訳(TreeListViewNode parentTlvNode, HimokuTreeNode parentHtNode,
                                          Dictionary<Yojitsu.util.CellItem.CellItemKey, List<CellItem>> itemsDic,
                                          int parentMsHimokuId,
                                          CurrencyFactory.金額種別 amountKind)
        {
            TreeListViewNode n = CreateNode(Color.White, parentHtNode);

            if (Yojitsu.DA.Constants.BUMON_VISIBILITY.ContainsKey(parentHtNode.MsHimoku.MsBumonID))
            {
                n.Visible = Yojitsu.DA.Constants.BUMON_VISIBILITY[parentHtNode.MsHimoku.MsBumonID];
            }

            if (amountKind == CurrencyFactory.金額種別.ドル)
            {
                AddSubItem(n, parentHtNode.BgColor, "$", true);
            }
            else
            {
                AddSubItem(n, parentHtNode.BgColor, "円", true);
            }

            AddSubItem(n, Yojitsu.DA.Constants.GetMsBumonColor(parentHtNode.MsHimoku.MsBumonID), DbTableCache.instance().GetMsBumon(parentHtNode.MsHimoku.MsBumonID).BumonName, true);

            CreateRowData(n, parentHtNode, itemsDic, parentMsHimokuId, amountKind);

            parentTlvNode.Nodes.Add(n);
        }


        private void CreateRowData(TreeListViewNode tlvNode, HimokuTreeNode htNode,
                                           Dictionary<CellItem.CellItemKey, List<CellItem>> itemsDic,
                                           int parentMsHimokuId,
                                           CurrencyFactory.金額種別 amountKind)
        {
            List<CellItem> parentCellItems = null;
            if (parentMsHimokuId != int.MinValue && itemsDic.ContainsKey(new CellItem.CellItemKey(parentMsHimokuId, amountKind)))
            {
                parentCellItems = itemsDic[new CellItem.CellItemKey(parentMsHimokuId, amountKind)];
            }

            List<CellItem> cellItems = itemsDic[new CellItem.CellItemKey(htNode.MsHimoku.MsHimokuID, amountKind)];

            for (int i = 0; i < cellItems.Count; i++)
            {
                CellItem item = cellItems[i];

                CurrencyFactory currency = CurrencyFactory.Create(amountKind);

                string subItemValue = currency.金額出力(item);
                string textBoxValue = currency.Amount(item).ToString();

                bool isReadOnly;
                Color color;

                SetReadOnly(htNode, item, out isReadOnly, out color);

                TreeListViewSubItem subItem =
                    AddSubItem(tlvNode, color, subItemValue, textBoxValue, isReadOnly,
                               delegate(object sender, EventArgs e)
                               {
                                   TextBox textBox = (TextBox)sender;
                                   TreeListViewSubItem si = textBox.Tag as TreeListViewSubItem;

                                   if (!hasYosanItemCellItems.ContainsKey(si))
                                   {
                                       return;
                                   }

                                   decimal amount;
                                   Decimal.TryParse(textBox.Text, out amount);
                                   si.Text = hasYosanItemCellItems[si].Currency.金額出力(amount);
                                   textBox.Text = amount.ToString();

                                   if (hasYosanItemCellItems[si].Currency.Amount(hasYosanItemCellItems[si]).ToString() == textBox.Text)
                                   {
                                       return;
                                   }

                                   hasYosanItemCellItems[si].SetNewAmount(amount);
                                   hasYosanItemCellItems[si].NotifyParents();

                                   if (hasYosanItemCellItems[si].Edited)
                                   {
                                       Edited();
                                   }
                               });

                item.SubItem = subItem;

                if (parentCellItems != null)
                {
                    item.Parents.Add(parentCellItems[i]);
                    parentCellItems[i].VChildrenPlus.Add(item);
                }

                Build売上総利益Relations(htNode, itemsDic, i, item, amountKind);
                Build営業損益Relations(htNode, itemsDic, i, item, amountKind);
                Build経常損益Relations(htNode, itemsDic, i, item, amountKind);
                Build税引前当期損益Relations(htNode, itemsDic, i, item, amountKind);
                Build当期損益Relations(htNode, itemsDic, i, item, amountKind);

                if (item.HasBgYosanItem())
                {
                    hasYosanItemCellItems.Add(subItem, item);
                }
            }
        }


        public List<BgYosanItem> GetEditedBgYosanItems()
        {
            List<BgYosanItem> result = new List<BgYosanItem>();

            foreach (CellItem ci in hasYosanItemCellItems.Values)
            {
                if (ci.Edited)
                {
                    ci.BuildYosanItem(result);
                }
            }

            return result;
        }


        internal static string Create船稼働String(BgKadouVessel kadouVessel)
        {
            if (kadouVessel == null)
            {
                return "<p>　</p><p>　</p>";
            }
            else if (kadouVessel.KadouStartDate == DateTime.MinValue || kadouVessel.KadouEndDate == DateTime.MinValue)
            {
                return "<p>不稼働</p><p>　</p>";
            }
            else
            {
                return "<p>" + Create稼働期間Str(kadouVessel) + "</p><p>" + Create定期点検Str(kadouVessel) + "</p>";
            }
        }


        private static string Create稼働期間Str(BgKadouVessel bgKadouVessel)
        {
            if (bgKadouVessel.KadouStartDate.Month == 4 && bgKadouVessel.KadouStartDate.Day == 1 &&
                    船稼働設定Form.AmOrPm(bgKadouVessel.KadouStartDate) == "AM" &&
                    bgKadouVessel.KadouEndDate.Month == 3 && bgKadouVessel.KadouEndDate.Day == 31 &&
                    船稼働設定Form.AmOrPm(bgKadouVessel.KadouEndDate) == "PM")
            {
                return "　";
            }

            StringBuilder buff = new StringBuilder();

            if (bgKadouVessel.KadouStartDate.Month != 4 || bgKadouVessel.KadouStartDate.Day != 1 ||
                    船稼働設定Form.AmOrPm(bgKadouVessel.KadouStartDate) != "AM")
            {
                buff.Append(bgKadouVessel.KadouStartDate.ToString("MM/dd"));
                buff.Append(船稼働設定Form.AmOrPm(bgKadouVessel.KadouStartDate));
            }

            buff.Append(" ～ ");

            if (bgKadouVessel.KadouEndDate.Month != 3 || bgKadouVessel.KadouEndDate.Day != 31 ||
                    船稼働設定Form.AmOrPm(bgKadouVessel.KadouEndDate) != "PM")
            {
                buff.Append(bgKadouVessel.KadouEndDate.ToString("MM/dd"));
                buff.Append(船稼働設定Form.AmOrPm(bgKadouVessel.KadouEndDate));
            }

            return buff.ToString();
        }


        private static string Create定期点検Str(BgKadouVessel kadouVessel)
        {
            if (kadouVessel.NyukyoKind == null || kadouVessel.NyukyoKind == string.Empty)
            {
                return "　";
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


        internal void NodesVisible(string msBumonId, bool visible)
        {
            treeListView.SuspendUpdate();

            foreach (TreeListViewNode node in treeListView.FlatNodes)
            {
                HimokuTreeNode himokuTreeNode = node.Tag as HimokuTreeNode;

                if (himokuTreeNode.MsHimoku.MsBumonID == msBumonId)
                {
                    node.Visible = visible;
                }
            }

            treeListView.ResumeUpdate();
        }


        private void treeListView_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;

            if (me.Button == MouseButtons.Right)
            {
                TreeListViewNode node = treeListView.GetNodeAt(me.Location);

                if (node != null)
                {
                    TreeListViewSubItem subItem = treeListView.GetSubItem(node, me.Location);

                    if (subItem != null && hasYosanItemCellItems.ContainsKey(subItem) && subItem.Control != null)
                    {
                        予算コピーForm form = new 予算コピーForm(subItem, hasYosanItemCellItems, yosanHead);
                        form.ShowDialog();
                    }
                }
            }
        }


        internal void SetExcelData(Dictionary<int, Dictionary<string, Excelファイル読込Form.YosanObject>> amountDic)
        {
            foreach (KeyValuePair<int, Dictionary<string, Excelファイル読込Form.YosanObject>> pair in amountDic)
            {
                foreach (KeyValuePair<string, Excelファイル読込Form.YosanObject> pair2 in pair.Value)
                {
                    foreach (CellItem ci in hasYosanItemCellItems.Values)
                    {
                        int msHimokuId = ci.GetMsHimokuId();
                        string nengetsu = ci.GetNengetsu().Trim();

                        if (msHimokuId == pair.Key && nengetsu == pair2.Key.Trim())
                        {
                            if (ci.Currency.Type() == CurrencyFactory.金額種別.円)
                            {
                                decimal am = pair2.Value.Amount / 1000;

                                ci.SetNewAmount(am);
                                ci.SubItem.Text = NBaseCommon.Common.金額出力(am);
                                ci.NotifyParents();
                            }
                            else if (ci.Currency.Type() == CurrencyFactory.金額種別.ドル)
                            {
                                decimal am = pair2.Value.DollerAmount;

                                ci.SetNewAmount(am);
                                ci.SubItem.Text = NBaseCommon.Common.ドル金額出力(am);
                                ci.NotifyParents();
                            }
                        }
                    }
                }
            }
        }


        public class EmptyYojitsu : IYojitsu
        {
            #region IYojitsu メンバ

            public decimal YenAmount { get; set; }
            public decimal DollerAmount { get; set; }
            public decimal Amount { get; set; }

            public decimal PreYenAmount { get; set; }
            public decimal PreDollerAmount { get; set; }
            public decimal PreAmount { get; set; }

            public string Nengetsu { get; set; }

            #endregion
        }
    }
}
