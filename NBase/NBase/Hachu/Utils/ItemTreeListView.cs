using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using LidorSystems.IntegralUI.Lists.Collections;

namespace Hachu.Utils
{
    public class ItemTreeListView
    {
        public delegate void LinkClickEventHandler(string id);
        public event LinkClickEventHandler LinkClickEvent;

        // 2016.07 特定品振替
        public delegate void LinkClickEventHandler2(string id);
        public event LinkClickEventHandler2 LinkClickEvent2;

        public bool SpecificItemLink = false;


        //
        // カラム情報は、　カラム名、幅、色、TextAlign　
        //

        /// <summary>
        /// TreeListView
        /// </summary>
        protected TreeListView treeListView;

        /// <summary>
        /// Noのカラム位置
        /// </summary>
        protected int noColumnIndex = -1;

        /// <summary>
        /// ヘッダ行の表示/非表示
        /// </summary>
        protected bool viewHeader = false;

        /// <summary>
        /// フォント
        /// </summary>
        //protected Font defaultFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
        protected Font defaultFont = new Font("MS UI Gothic", 10, FontStyle.Regular);

        /// <summary>
        /// ReadOnlyの行の背景色
        /// </summary>
        /// <param name="treeListView"></param>
        protected Color readOnlyBgColor = Color.FromArgb(240, 240, 240); // f0f0f0

        protected bool StyleFromParent = false;

        public bool Enabled { set; get; }

        public bool CheckBoxEvent = false;
        public bool ExpandCollapseEvent = false;

        public ItemTreeListView(TreeListView treeListView)
        {
            this.treeListView = treeListView;
            Size s = new Size();
            s.Height = Hachu.Common.CommonDefine.ExpandBoxSize;
            s.Width = Hachu.Common.CommonDefine.ExpandBoxSize;
            treeListView.ExpandBoxStyle.ImageSize = s;


            treeListView.AfterExpand += new LidorSystems.IntegralUI.ObjectEventHandler(treeListView_AfterExpand);
            treeListView.AfterCollapse += new LidorSystems.IntegralUI.ObjectEventHandler(treeListView_AfterCollapse);
        }

        private void treeListView_AfterExpand(object sender, LidorSystems.IntegralUI.ObjectEventArgs e)
        {
            ExpandCollapseEvent = true;
        }

        private void treeListView_AfterCollapse(object sender, LidorSystems.IntegralUI.ObjectEventArgs e)
        {
            ExpandCollapseEvent = true;
        }

        public void SuspendUpdate()
        {
            treeListView.SuspendUpdate();
        }
        public void ResumeUpdate()
        {
            treeListView.ResumeUpdate();
        }

        public virtual void Clear()
        {
            treeListView.Columns.Clear();
            treeListView.Nodes.Clear();
        }
        public virtual void NodesClear()
        {
            treeListView.Nodes.Clear();
        }
        public void SetColumns(object[,] headers)
        {
            SetColumns(-1, headers);
        }
        public void SetColumns(int noColumnIndex, object[,] headers)
        {

            this.noColumnIndex = noColumnIndex;
            for (int i = 0; i < headers.GetLength(0); i++)
            {
                TreeListViewColumn column = new TreeListViewColumn();

                column.StyleFromParent = false;
                column.HeaderContent = (string)headers[i, 0];
                column.Width = (int)headers[i, 1];

                //column.FormatStyle.HeaderFont = cFont;
                column.FormatStyle.HeaderFont = defaultFont;
                column.FormatStyle.HeaderPadding = new Padding(2, 5, 3, 2);

                column.FormatStyle.HeaderBorderCornerShape.SetCornerShape(LidorSystems.IntegralUI.Style.CornerShape.Squared);

                if (i == noColumnIndex + 1)
                {
                    column.ContentType = LidorSystems.IntegralUI.Lists.ColumnContentType.Custom;
                }
                else
                {
                    column.ContentType = LidorSystems.IntegralUI.Lists.ColumnContentType.Control;
                }

                if (headers.GetLength(1) == 3 && headers[i, 2] != null)
                {
                    column.NormalStyle.HeaderColor = (Color)headers[i, 2];
                    column.NormalStyle.BackColor = (Color)headers[i, 2];
                }

                if (headers.GetLength(1) == 4 && headers[i, 3] != null)
                {
                    column.FormatStyle.ContentAlign = (HorizontalAlignment)headers[i, 3];
                }


                // ヘッダが「添付」の場合、リンクを常に表示
                // ヘッダが「添付対象」の場合、チェックボックスを常に表示
                if ((string)headers[i, 0] == "添付")
                {
                    //column.ContentType = LidorSystems.IntegralUI.Lists.ColumnContentType.Custom;
                    column.ContentControlVisibility = LidorSystems.IntegralUI.Lists.ContentControlVisibility.AlwaysVisible;
                }
                else if ((string)headers[i, 0] == "添付対象")
                {
                    //column.ContentType = LidorSystems.IntegralUI.Lists.ColumnContentType.Control;
                    column.ContentControlVisibility = LidorSystems.IntegralUI.Lists.ContentControlVisibility.AlwaysVisible;
                }
                else if (SpecificItemLink && (string)headers[i,0] == "種別")
                {
                    column.ContentControlVisibility = LidorSystems.IntegralUI.Lists.ContentControlVisibility.AlwaysVisible;
                }
                else
                {
                    column.ContentControlVisibility = LidorSystems.IntegralUI.Lists.ContentControlVisibility.OnClick;
                }

                treeListView.Columns.Add(column);
            }

        }

        public TreeListViewSubItem AddSubItem(TreeListViewNode node, string value)
        {
            return AddSubItem(node, value, false);
        }

        public TreeListViewSubItem AddSubItem(TreeListViewNode node, string value, bool readOnly)
        {
            return AddSubItem(node, value, value, 5, readOnly);
        }
        public TreeListViewSubItem AddSubItem(TreeListViewNode node, string value, string textValue, int maxlength, bool readOnly)
        {
            Color color;

            if (readOnly)
            {
                color = readOnlyBgColor;
            }
            else
            {
                color = Color.White;
            }

            return AddSubItem(node, color, value, textValue, maxlength, readOnly);
        }

        public TreeListViewSubItem AddSubItem(TreeListViewNode node, Color color, string value)
        {
            return AddSubItem(node, color, value, null, 0, false);
        }

        public TreeListViewSubItem AddSubItem(TreeListViewNode node, Color color, string value, string textValue, int maxlength, bool readOnly)
        {
            TreeListViewSubItem subItem = new TreeListViewSubItem();
            subItem.StyleFromParent = StyleFromParent;
            subItem.NormalStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.Flat;
            subItem.NormalStyle.BackColor = color;
            subItem.HoverStyle = subItem.NormalStyle;
            subItem.FormatStyle.Font = defaultFont;

            subItem.Text = (string)value;

            if (!readOnly)
            {
                TextBox textBox = new TextBox();
                textBox.ImeMode = ImeMode.Disable;
                textBox.TextAlign = HorizontalAlignment.Right;
                textBox.MaxLength = maxlength;
                textBox.Tag = subItem;
                textBox.Text = textValue;
                textBox.TextChanged -= new EventHandler(OnTextChanged);
                textBox.GotFocus -= new EventHandler(OnVisibleChanged);
                textBox.KeyPress -= new KeyPressEventHandler(OnKeyPress);
                textBox.TextChanged += new EventHandler(OnTextChanged);
                textBox.GotFocus += new EventHandler(OnVisibleChanged);
                textBox.KeyPress += new KeyPressEventHandler(OnKeyPress);
                subItem.Control = textBox;
                subItem.Key = node.SubItems.Count.ToString();

                subItem.Control.LostFocus -= new EventHandler(OnLostFocus);
                subItem.Control.LostFocus += new EventHandler(OnLostFocus);
                //textBox.LostFocus -= new EventHandler(OnLostFocus);     // .AfterLabelEdit -= new LidorSystems.IntegralUI.ObjectEditEventHandler(AAA);
                //textBox.LostFocus += new EventHandler(OnLostFocus);
            }

            node.SubItems.Add(subItem);
          
            return subItem;
        }
        public void OnLostFocus(object sendor, EventArgs e)
        {
            if (sendor is TextBox)
            {
                TextBox t = sendor as TextBox;
                //object ob = t.Parent;
                //LidorSystems.IntegralUI.Containers.InnerPanel p = ob as LidorSystems.IntegralUI.Containers.InnerPanel;
                //p.Refresh();
                //treeListView.Update();
                //treeListView.UpdateControlVisibility;
                treeListView.UpdateCurrentView();
            }        
        }
        public TreeListViewSubItem AddSubItemAsHeader(TreeListViewNode node, string value, bool readOnly)
        {
            TreeListViewSubItem subItem = new TreeListViewSubItem();
            subItem.StyleFromParent = StyleFromParent;
            subItem.NormalStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.Flat;
            subItem.NormalStyle.BackColor = readOnlyBgColor;
            subItem.HoverStyle = subItem.NormalStyle;
            subItem.FormatStyle.Font = defaultFont;

            StringBuilder subItemStr = new StringBuilder();
            subItemStr.Append("<div>");
            subItemStr.Append("<table><tr>");
            subItemStr.Append("<td width=\"1\"> </td>");
            subItemStr.Append("<td><font size=\"11\"><b>" + value + "</b></font></td>");
            subItemStr.Append("</tr></table>");
            subItemStr.Append("</div>");
            subItem.Content = subItemStr.ToString();
            node.SubItems.Add(subItem);

            return subItem;
        }
        public TreeListViewSubItem AddSubItemAsItem(TreeListViewNode node, string value, bool readOnly)
        {
            TreeListViewSubItem subItem = new TreeListViewSubItem();
            subItem.StyleFromParent = StyleFromParent;
            subItem.NormalStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.Flat;
            subItem.NormalStyle.BackColor = readOnlyBgColor;
            subItem.HoverStyle = subItem.NormalStyle;
            subItem.FormatStyle.Font = defaultFont;
            
            StringBuilder subItemStr = new StringBuilder();
            subItemStr.Append("<div>");
            subItemStr.Append("<table><tr>");
            subItemStr.Append("<td width=\"15\"> </td>");
            subItemStr.Append("<td>" + value + "</td>");
            subItemStr.Append("</tr></table>");
            subItemStr.Append("</div>");
            subItem.Content = subItemStr.ToString();
            node.SubItems.Add(subItem);

            return subItem;
        }
        public TreeListViewSubItem AddSubItemAsShousai(TreeListViewNode node, string value, bool readOnly)
        {
            return AddSubItemAsShousai(node, 25, value, readOnly);
        }
        public TreeListViewSubItem AddSubItemAsShousai(TreeListViewNode node, int padding, string value, bool readOnly)
        {
            TreeListViewSubItem subItem = new TreeListViewSubItem();
            subItem.StyleFromParent = StyleFromParent;
            subItem.NormalStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.Flat;
            subItem.NormalStyle.BackColor = readOnlyBgColor;
            subItem.HoverStyle = subItem.NormalStyle;
            subItem.FormatStyle.Font = defaultFont;

            StringBuilder subItemStr = new StringBuilder();
            subItemStr.Append("<div>");
            subItemStr.Append("<table><tr>");
            if (padding > 0)
            {
                subItemStr.Append("<td width=\"" + padding.ToString() + "\"> </td>");
            }
            subItemStr.Append("<td>" + System.Web.HttpUtility.HtmlEncode(value) + "</td>");
            subItemStr.Append("</tr></table>");
            subItemStr.Append("</div>");
            subItem.Content = subItemStr.ToString();
            node.SubItems.Add(subItem);

            return subItem;
        }

        public TreeListViewSubItem AddLinkItem(TreeListViewNode node, string value)
        {
            TreeListViewSubItem subItem = new TreeListViewSubItem();
            subItem.StyleFromParent = StyleFromParent;
            subItem.NormalStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.Flat;
            subItem.NormalStyle.BackColor = readOnlyBgColor;
            subItem.HoverStyle = subItem.NormalStyle;
            subItem.FormatStyle.Font = defaultFont;

            // リンク
            LinkLabel link = new LinkLabel();
            link.Padding = new Padding(2, 4, 0, 0);
            link.Text = "〇";
            link.Tag = subItem;
            link.Click += new EventHandler(OnClick);　// イベントハンドラを紐付ける
            subItem.Key = value;
            subItem.Controls.Add(link);

            //subItem.Text = (string)value;
            node.SubItems.Add(subItem);

            return subItem;
        }
        // 2016.07 特定品振替
        public TreeListViewSubItem AddLinkItem2(TreeListViewNode node, string text, string value)
        {
            TreeListViewSubItem subItem = new TreeListViewSubItem();
            subItem.StyleFromParent = StyleFromParent;
            subItem.NormalStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.Flat;
            subItem.NormalStyle.BackColor = readOnlyBgColor;
            subItem.HoverStyle = subItem.NormalStyle;
            subItem.FormatStyle.Font = defaultFont;

            subItem.Key = value;

            // リンク
            if (text == "追加")
            {
                LinkLabel link = new LinkLabel();
                link.Padding = new Padding(2, 4, 0, 0);
                link.Text = text;
                link.Tag = subItem;

                link.Click += new EventHandler(OnClick);　// イベントハンドラを紐付ける

                subItem.Controls.Add(link); 
            }
            else
            {
                Label label = new Label();
                label.Padding = new Padding(2, 6, 0, 0);
                label.Text = text;
                label.Tag = subItem;

                subItem.Controls.Add(label); 
            }
 
            subItem.Text = text;
            node.SubItems.Add(subItem);

            return subItem;
        }
        public TreeListViewSubItem AddCheckBoxItem(TreeListViewNode node, bool isChecked)
        {
            TreeListViewSubItem subItem = new TreeListViewSubItem();
            subItem.StyleFromParent = StyleFromParent;
            subItem.NormalStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.Flat;
            subItem.NormalStyle.BackColor = readOnlyBgColor;
            subItem.HoverStyle = subItem.NormalStyle;
            subItem.FormatStyle.Font = defaultFont;

            // チェックボックス
            CheckBox cb = new CheckBox();
            cb.Tag = subItem;
            cb.Size = new Size(13, 13);
            cb.Checked = isChecked;
            cb.CheckedChanged += new EventHandler(OnCheckedChanged);　// イベントハンドラを紐付ける
            subItem.Controls.Add(cb);

            //subItem.Text = (string)value;
            node.SubItems.Add(subItem);

            return subItem;
        }

        public void OnClick(object sender, EventArgs e)
        {
            if (sender is LinkLabel)
            {
                LinkLabel link = (LinkLabel)sender;
                LidorSystems.IntegralUI.Lists.TreeListViewSubItem subItem = (LidorSystems.IntegralUI.Lists.TreeListViewSubItem)link.Tag;

                treeListView.SelectedNode = subItem.Parent;

                // 2016.07 特定品振替
                //// ここに、リンクをクリックした時の処理を実装する
                //// （添付ファイルを開く）
                //if (LinkClickEvent != null)
                //    LinkClickEvent(subItem.Key);

                if (link.Text == "〇")//"○")
                {
                    if (LinkClickEvent != null)
                        LinkClickEvent(subItem.Key); // （添付ファイルを開く）
                }
                else
                {
                    if (LinkClickEvent2 != null)
                        LinkClickEvent2(subItem.Key); // （特定品振替画面を開く）
                }

            }
        }
        public virtual void OnCheckedChanged(object sender, EventArgs e)
        {
        }

        #region TreeListViewの編集TextBoxの制御
        public void OnVisibleChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox textBox = (TextBox)sender;
                textBox.SelectAll();

                //System.Diagnostics.Debug.WriteLine("OnVisibleChanged:" + treeListView.SelectedSubItem.Text);
            }
        }
        public virtual void OnTextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox textBox = (TextBox)sender;
                LidorSystems.IntegralUI.Lists.TreeListViewSubItem subItem = (LidorSystems.IntegralUI.Lists.TreeListViewSubItem)textBox.Tag;
                subItem.Text = textBox.Text;
            }
        }
        private void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
        #endregion

        #region TreeListViewのチェックボックスの制御
        public void OnAfterCheck(object sender, LidorSystems.IntegralUI.ObjectEventArgs e)
        {
            if (e.Object is TreeListViewNode)
            {
                CheckBoxEvent = true;

                TreeListViewNode node = e.Object as TreeListViewNode;

                // Use this method to update the state for all child nodes of this node
                UpdateChild(node);

                // Use this method to update the state for all parent nodes of this node
                UpdateParent(node);
            }

        }
        private void UpdateChild(TreeListViewNode node)
        {
            // If the parent node is not Indeterminate
            // change the state of all child nodes
            if (node.CheckState != CheckState.Indeterminate)
            {
                CheckState state = node.CheckState;
                foreach (TreeListViewNode childNode in node.Nodes)
                {
                    if (childNode.Visible)
                    {
                        // Use this method to change the CheckState of the childNode,
                        // without triggerring the BeforeCheck and AfterCheck events
                        this.treeListView.ChangeCheckState(childNode, state);

                        // Repeat the whole cycle for other child nodes
                        UpdateChild(childNode);
                    }
                }
            }
        }
        private void UpdateParent(TreeListViewNode node)
        {
            // Hold te number of unchecked and visible nodes
            int numUnchecked = 0;
            int numVisible = 0;

            // Let presume that by default that node is checked
            CheckState state = CheckState.Checked;

            // Get the parent node
            TreeListViewNode parentNode = node.Parent;
            while (parentNode != null)
            {
                numUnchecked = 0;
                numVisible = 0;

                state = CheckState.Checked;
                foreach (TreeListViewNode childNode in parentNode.Nodes)
                {
                    if (childNode.Visible)
                    {
                        numVisible++;
                        // If there is at least one Indeterminate node, exit the loop.
                        // The state for the parent node will be also Indeterminate
                        if (childNode.CheckState == CheckState.Indeterminate)
                        {
                            state = CheckState.Indeterminate;
                            break;
                        }
                        // Count the unchecked nodes
                        else if (childNode.CheckState == CheckState.Unchecked)
                            numUnchecked++;
                    }
                }

                if (numVisible > 0)
                {
                    // If there are no unchecked nodes and there is at least one indeterminate node,
                    // that means that all child nodes are checked. So, the parent node will also be Checked
                    if (numUnchecked == 0 && state != CheckState.Indeterminate)
                        state = CheckState.Checked;
                    // If number of visible and unchecked nodes are equal, then parent node will be Unchecked
                    else if (numUnchecked == numVisible)
                        state = CheckState.Unchecked;
                    // In any other case, then parent node will be Indeterminate
                    else
                        state = CheckState.Indeterminate;
                }
                // If there are no visible nodes, the parent node will be unchecked
                else
                    state = CheckState.Unchecked;

                // Use this method to change the CheckState of the node,
                // without triggerring the BeforeCheck and AfterCheck events
                this.treeListView.ChangeCheckState(parentNode, state);


                // Repeat the whole cycle for other parent nodes
                parentNode = parentNode.Parent;
            }
        }
        public void AllCheck()
        {
            CheckStateChange(this.treeListView.Nodes, CheckState.Checked);
        }
        public void AllUncheck()
        {
            CheckStateChange(this.treeListView.Nodes, CheckState.Unchecked);
        }
        public void CheckStateChange(TreeListViewNodeCollection nodes, CheckState state)
        {
            foreach (TreeListViewNode childNode in nodes)
            {
                if (childNode.Visible)
                {
                    // Use this method to change the CheckState of the childNode,
                    // without triggerring the BeforeCheck and AfterCheck events
                    this.treeListView.ChangeCheckState(childNode, state);

                    // Repeat the whole cycle for other child nodes
                    CheckStateChange(childNode.Nodes, state);
                }
            }
        }
        #endregion


        public TreeListViewNode selectedNode()
        {
            return treeListView.SelectedNode;
        }
        public void SetSelectedNode(int index)
        {
            treeListView.SelectedNode = treeListView.Nodes[index];
        }
        public void SetSelectedNode(TreeListViewNode node)
        {
            treeListView.SelectedNode = node;
        }
        public void SelectLastNode()
        {
            int index = treeListView.Nodes.Count - 1;
            if ( index >= 0 )
                SetSelectedNode(index);
        }
        public int GetNodeIndex()
        {
            int index = -1;
            TreeListViewNode selectedNode = treeListView.SelectedNode;
            if (selectedNode == null)
            {
                return index;
            }
            foreach (TreeListViewNode node in treeListView.Nodes)
            {
                index++;
                if (node == selectedNode)
                {
                    break;
                }
            }
            return index;
        }

        public void RemoveAt(int index)
        {
            RemoveAt(treeListView.Nodes, index);
        }
        public void RemoveAt(TreeListViewNodeCollection nodes, int index)
        {
            // 番号カラムがある場合、番号を１つずらす
            if (noColumnIndex > 0)
            {
                int max = nodes.Count;
                for (int i = index + 1; i < max; i++)
                {
                    nodes[i].SubItems[noColumnIndex].Text = i.ToString();
                }
            }
            // ノードを削除する
            nodes.RemoveAt(index);
        }

        public void InsertAt(int index, TreeListViewNode node)
        {
            InsertAt(treeListView.Nodes, index, node);
        }
        public void InsertAt(TreeListViewNodeCollection nodes, int index, TreeListViewNode node)
        {
            // ノードをインサートする
            nodes.Insert(index, node);

            // 番号カラムがある場合、番号を１つずらす
            if (noColumnIndex > 0)
            {
                int max = nodes.Count;
                for (int i = index + 1; i < max; i++)
                {
                    nodes[i].SubItems[noColumnIndex].Text = (i + 1).ToString();
                }
            }
        }
    }
}
