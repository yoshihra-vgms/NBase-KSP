using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LidorSystems.IntegralUI.Lists;
using LidorSystems.IntegralUI.Lists.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using LidorSystems.IntegralUI.Style;

namespace NBaseUtil
{
    public class TreeListViewDelegate
    {
        public delegate void LinkClickEventHandler(string id);
        public event LinkClickEventHandler LinkClickEvent;

        protected TreeListView treeListView;

        private static readonly int EXPAND_BOX_SIZE = 17;

        protected Font SubItemFont { get; set; }
        protected Font ColumnFont { get; set; }

        public bool CheckBoxEvent = false;

        public TreeListViewDelegate(TreeListView treeListView)
        {
            this.treeListView = treeListView;

            treeListView.Footer = false;

            treeListView.SelectedNodeStyle.BackColor = Color.Turquoise;

            treeListView.ExpandBoxStyle.ImageSize = new Size(EXPAND_BOX_SIZE, EXPAND_BOX_SIZE);
            //treeListView.ScrollPosChanged += new EventHandler(treeListView_ScrollPosChanged);
            SubItemFont = UIConstants.DEFAULT_FONT;
            ColumnFont = UIConstants.DEFAULT_FONT;

            treeListView.Font = UIConstants.DEFAULT_FONT;

        }
        private void treeListView_ScrollPosChanged(object sender, EventArgs e)
        {
            treeListView.ResumeUpdate();
        }

        public void SetColumnFont(Font font)
        {
            ColumnFont = font;
        }
        public void SetColumns(List<Column> columns)
        {
            foreach (Column c in columns)
            {
                TreeListViewColumn column = new TreeListViewColumn();

                column.StyleFromParent = false;
                column.HeaderContent = c.headerContent;
                column.Width = c.width;
                column.FixedWidth = c.fixedWidth;
                column.Tag = c.tag;

                column.FormatStyle.HeaderFont = ColumnFont;

                column.ContentType = ColumnContentType.Control;
                column.ContentControlVisibility = c.ContentControlVisibility;
                if (c.headerContent == "添付")
                {
                    column.ContentControlVisibility = LidorSystems.IntegralUI.Lists.ContentControlVisibility.AlwaysVisible;
                }

                if (c.headerColor != null)
                {
                    column.NormalStyle.HeaderColor = c.headerColor;
                }

                if (c.backColor != null)
                {
                    column.NormalStyle.BackColor = c.backColor;
                }

                if (c.headerContent != null)
                {
                    column.FormatStyle.ContentAlign = c.alignment;
                }

                column.FormatStyle.HeaderBorderCornerShape.TopLeft = c.leftShape;
                column.FormatStyle.HeaderBorderCornerShape.BottomLeft = c.leftShape;

                column.FormatStyle.HeaderBorderCornerShape.TopRight = c.rightShape;
                column.FormatStyle.HeaderBorderCornerShape.BottomRight = c.rightShape;

                treeListView.Columns.Add(column);
            }
        }


        public void SetColumns(object[,] headers)
        {
            List<TreeListViewDelegate.Column> columns = new List<TreeListViewDelegate.Column>();

            for (int i = 0; i < headers.GetLength(0); i++)
            {
                TreeListViewDelegate.Column h = new TreeListViewDelegate.Column();
                h.headerContent = (string)headers[i, 0];
                h.width = (int)headers[i, 1];

                if (headers.GetLength(1) > 2 && headers[i, 2] != null)
                {
                    h.headerColor = (Color)headers[i, 2];
                }

                if (headers.GetLength(1) > 3 && headers[i, 3] != null)
                {
                    h.alignment = (HorizontalAlignment)headers[i, 3];
                }
                columns.Add(h);
            }

            SetColumns(columns);
        }


        public TreeListViewNode CreateNode()
        {
            return CreateNode(Color.White, null);
        }


        public TreeListViewNode CreateNode(Color color)
        {
            return CreateNode(color, null);
        }


        public TreeListViewNode CreateNode(Color color, object tag)
        {
            TreeListViewNode node = new TreeListViewNode();

            node.StyleFromParent = false;
            node.NormalStyle.BackColor = color;
            node.Tag = tag;

            return node;
        }


        public TreeListViewSubItem AddSubItem(TreeListViewNode node, string value, bool readOnly)
        {
            return AddSubItem(node, value, value, readOnly, null);
        }


        public TreeListViewSubItem AddSubItem(TreeListViewNode node, string subItemValue, string textBoxValue, bool readOnly, EventHandler leaveEventHandler)
        {
            return AddSubItem(node, subItemValue, textBoxValue, readOnly, false, int.MinValue, ImeMode.Disable, leaveEventHandler);
        }


        public TreeListViewSubItem AddSubItem(TreeListViewNode node, string subItemValue, string textBoxValue, bool readOnly, bool multiLine, int maxLength, ImeMode imeMode, EventHandler leaveEventHandler)
        {
            TreeListViewSubItem subItem = new TreeListViewSubItem();
            subItem.StyleFromParent = false;

            subItem.Text = subItemValue;
            subItem.FormatStyle.Font = SubItemFont;

            if (!readOnly)
            {
                CreateSubItemTextBox(node, textBoxValue, multiLine, maxLength, imeMode, leaveEventHandler, subItem);
            }

            node.SubItems.Add(subItem);

            return subItem;
        }

        public TreeListViewSubItem AddSubItem2(TreeListViewNode node, string subItemValue)
        {
            TreeListViewSubItem subItem = new TreeListViewSubItem();
            subItem.StyleFromParent = false;
            subItem.NormalStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.Flat;
            subItem.HoverStyle = subItem.NormalStyle;
            subItem.FormatStyle.Font = new Font("MS UI Gothic", 8, FontStyle.Regular);

            subItem.Text = subItemValue;
            subItem.FormatStyle.Font = SubItemFont;

            node.SubItems.Add(subItem);

            return subItem;
        }

        public TreeListViewSubItem AddLinkItem(TreeListViewNode node, string value)
        {
            TreeListViewSubItem subItem = new TreeListViewSubItem();
            subItem.StyleFromParent = false;
            subItem.NormalStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.Flat;
            //subItem.NormalStyle.BackColor = readOnlyBgColor;
            subItem.HoverStyle = subItem.NormalStyle;
            subItem.FormatStyle.Font = SubItemFont;

            // リンク
            LinkLabel link = new LinkLabel();
            link.Text = "〇";
            link.Tag = subItem;
            link.Click += new EventHandler(OnClick);　// イベントハンドラを紐付ける
            subItem.Key = value;
            subItem.Controls.Add(link);

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

                // ここに、リンクをクリックした時の処理を実装する
                // （添付ファイルを開く）
                if (LinkClickEvent != null)
                    LinkClickEvent(subItem.Key);
            }
        }


        protected void CreateSubItemTextBox(TreeListViewNode node, string textBoxValue, bool multiLine, int maxLength, ImeMode imeMode, EventHandler leaveEventHandler, TreeListViewSubItem subItem)
        {
            TextBox textBox = new TextBox();
            textBox.Tag = subItem;
            textBox.Text = textBoxValue;
            textBox.AcceptsTab = true;
            textBox.TextChanged -= new EventHandler(OnTextChanged);

            textBox.Enter -= new EventHandler(Enter);
            textBox.TextChanged += new EventHandler(OnTextChanged);
            textBox.Enter += new EventHandler(Enter);
            textBox.PreviewKeyDown += new PreviewKeyDownEventHandler(textBox_PreviewKeyDown);
            textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);

            if (leaveEventHandler != null)
            {
                textBox.Leave -= leaveEventHandler;
                textBox.Leave += leaveEventHandler;
            }

            int height = textBox.Size.Height;

            if (multiLine)
            {
                textBox.Multiline = true;
                height = height * 5;
                textBox.Enter -= new EventHandler(Enter);
            }

            if (maxLength > 0)
            {
                textBox.MaxLength = maxLength;
            }

            textBox.ImeMode = imeMode;
            textBox.Size = new Size(treeListView.Columns[node.SubItems.Count].Width, height);

            subItem.Control = textBox;
        }


        public TreeListViewSubItem AddSubItemCheckBox(TreeListViewNode node, Color color, string value, bool readOnly)
        {
            TreeListViewSubItem subItem = new TreeListViewSubItem();
            subItem.StyleFromParent = false;
            subItem.NormalStyle.BackColor = color;

            subItem.Text = value;
            subItem.FormatStyle.Font = SubItemFont;

            if (readOnly == false)
            {
                CheckBox checkBox = new CheckBox();

                checkBox.Tag = subItem;
                checkBox.Text = value;
                subItem.Control = checkBox;

            }

            node.SubItems.Add(subItem);

            return subItem;
        }

        
        protected void textBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //Tabキーが押されてもフォーカスが移動しないようにする
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true;
            }
        }


        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // TAB key
            if (e.KeyChar == (char)9)
            {
                e.Handled = true;

                FieldInfo fieldInfo = typeof(TreeListView).GetField("activeSubItem", BindingFlags.Instance | BindingFlags.NonPublic);

                TreeListViewSubItem activeSubItem = (TreeListViewSubItem)fieldInfo.GetValue(treeListView);
                TreeListViewSubItem nextEditSubItem = GetNextEditSubItem(activeSubItem);

                if (nextEditSubItem != null)
                {
                    try
                    {
                        Scroll(nextEditSubItem);

                        treeListView.ExpandAll();

                        activeSubItem.Control.Hide();
                        nextEditSubItem.Control.Show();
                        nextEditSubItem.Control.Focus();

                        fieldInfo.SetValue(treeListView, nextEditSubItem);
                    }
                    catch(InvalidOperationException ex)
                    {
                        // ugly!!!
                    }
                }
            }
            // ENTER key
            else if (e.KeyChar == (char)13 && !((TextBox)sender).Multiline)
            {
                e.Handled = true;

                FieldInfo fieldInfo = typeof(TreeListView).GetField("activeSubItem", BindingFlags.Instance | BindingFlags.NonPublic);

                TreeListViewSubItem activeSubItem = (TreeListViewSubItem)fieldInfo.GetValue(treeListView);

                if (activeSubItem != null)
                {
                    activeSubItem.Control.Hide();
                }

                fieldInfo.SetValue(treeListView, null);
            }
        }


        private void Scroll(TreeListViewSubItem nextEditSubItem)
        {
            int fixedColumnsWidth = 0;

            foreach (TreeListViewColumn col in treeListView.Columns)
            {
                if (col.Fixed == ColumnFixedType.Left)
                {
                    fixedColumnsWidth += col.Width;
                }
            }

            Point p = new Point(nextEditSubItem.Bounds.X - fixedColumnsWidth -
                                ((treeListView.ContentPanel.ClientRectangle.Width - fixedColumnsWidth) / 2 +
                                (treeListView.ContentPanel.ClientRectangle.Width - fixedColumnsWidth) / 4),
                                nextEditSubItem.Bounds.Y - treeListView.ContentPanel.ClientRectangle.Height / 2);

            treeListView.SetScrollPos(p);
        }


        private TreeListViewSubItem GetNextEditSubItem(TreeListViewSubItem activeSubItem)
        {
            TreeListViewNode node = activeSubItem.Parent;

            if (activeSubItem.Index < node.SubItems.Count - 1)
            {
                for (int i = activeSubItem.Index + 1; i < node.SubItems.Count; i++)
                {
                    if (node.SubItems[i].Control != null)
                    {
                        return node.SubItems[i];
                    }
                }
            }

            TreeListViewNode nextNode = node;
            while ((nextNode = GetNextNode(nextNode)) != null)
            {
                foreach (TreeListViewSubItem subItem in nextNode.SubItems)
                {
                    if (subItem.Control != null)
                    {
                        return subItem;
                    }
                }
            }

            return null;
        }


        private TreeListViewNode GetNextNode(TreeListViewNode node)
        {
            foreach (TreeListViewNode n in node.Nodes)
            {
                return n;
            }

            if (node.NextNode != null)
            {
                return node.NextNode;
            }
            else if (node.Parent != null && node.Parent.NextNode != null)
            {
                return node.Parent.NextNode;
            }

            return treeListView.Nodes[0];
        }


        public void Enter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox textBox = (TextBox)sender;
                textBox.SelectAll();
            }
        }


        public void OnTextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox textBox = (TextBox)sender;
                LidorSystems.IntegralUI.Lists.TreeListViewSubItem subItem = (LidorSystems.IntegralUI.Lists.TreeListViewSubItem)textBox.Tag;
                subItem.Text = textBox.Text;
            }
        }


        public class Column
        {
            public enum ColumnKind { 予算, 実績, 差異 }

            public string headerContent;
            public int width;
            public Color headerColor;
            public Color backColor;
            public ContentAlignment headerAlignment = ContentAlignment.MiddleLeft;
            public HorizontalAlignment alignment;
            public bool fixedWidth;
            public ColumnKind tag;
            public ContentControlVisibility ContentControlVisibility = ContentControlVisibility.OnClick;

            public CornerShape leftShape = CornerShape.Rounded;
            public CornerShape rightShape = CornerShape.Rounded;
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


        public void UpdateCurrentView()
        {
            treeListView.UpdateCurrentView();
        }
    }
}
