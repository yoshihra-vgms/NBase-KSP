using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseUtil;
using LidorSystems.IntegralUI.Lists;
using System.Windows.Forms;
using NBaseData.DAC;
using System.Drawing;


namespace Dousei.util
{
    internal class TreeListViewDelegate動静 : TreeListViewDelegate
    {
        public Color 積みColor = Color.AliceBlue;
        public Color 揚げColor = Color.PeachPuff;
        
        internal TreeListViewDelegate動静(TreeListView treeListView)
            : base(treeListView)
        {
            treeListView.SuspendUpdate();

            SetColumns(CreateColumns());

            treeListView.ResumeUpdate();
        }

        private List<Column> CreateColumns()
        {
            List<TreeListViewDelegate.Column> columns = new List<TreeListViewDelegate.Column>();

            TreeListViewDelegate.Column h = new TreeListViewDelegate.Column();

            h.headerContent = "C";
            h.width = 60;
            h.ContentControlVisibility = ContentControlVisibility.AlwaysVisible;
            h.fixedWidth = true;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "日付";
            h.width = 70;
            h.fixedWidth = true;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "種別";
            h.width = 30;
            h.fixedWidth = true;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "区分";
            h.width = 40;
            h.fixedWidth = true;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "場所";
            h.width = 100;
            h.fixedWidth = true;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "基地";
            h.width = 100;
            h.fixedWidth = true;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "貨物";
            h.width = 70;
            h.fixedWidth = true;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "数量";
            h.width = 80;
            h.fixedWidth = true;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            //h.headerContent = "入港時間";
            h.headerContent = "入港時刻";
            h.width = 70;
            h.fixedWidth = true;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            //h.headerContent = "着桟時間";
            h.headerContent = "着桟時刻";
            h.width = 70;
            h.fixedWidth = true;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            //h.headerContent = "荷役開始";
            h.headerContent = "荷役時刻";
            h.width = 70;
            h.fixedWidth = true;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "荷役時刻";
            h.width = 70;
            h.fixedWidth = true;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            //h.headerContent = "離桟時間";
            h.headerContent = "離桟時刻";
            h.width = 70;
            h.fixedWidth = true;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            //h.headerContent = "出港時間";
            h.headerContent = "出港時刻";
            h.width = 70;
            h.fixedWidth = true;
            columns.Add(h);

            return columns;
        }

        internal void SetRows(List<DjDousei> douseis)
        {
            treeListView.SuspendUpdate();

            treeListView.Nodes.Clear();

            for (int i = 0; i < douseis.Count; i++)
            {
                DjDousei head = douseis[i];
                TreeListViewNode node = AddDouseiNode(head, head, true, 0);
                treeListView.Nodes.Add(node);

                ////子供を追加する
                //for (int j = 1; j < douseis[i].DjDouseis.Count; j++)
                //{
                //    TreeListViewNode cNode = AddDouseiNode(head,douseis[i].DjDouseis[j], false,j);
                //    node.Nodes.Add(cNode);
                //}
                //子供を追加する
                for (int j = 0; j < douseis[i].DjDouseis.Count; j++)
                {
                    if (douseis[i].DjDouseis[j].DjDouseiID == head.DjDouseiID)
                        continue;

                    TreeListViewNode cNode = AddDouseiNode(head, douseis[i].DjDouseis[j], false, j);
                    node.Nodes.Add(cNode);
                }
            }
            if (douseis.Count == 0)
            {
                TreeListViewNode node = AddClearNode();
                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        }

        private TreeListViewNode AddDouseiNode(DjDousei parent,DjDousei s, bool IsParent,int ChildIndex)
        {

            Color BackGroundColor;

            if (s.MsKanidouseiInfoShubetuID == NBaseCommon.Common.MsKanidouseiInfoShubetu_積み)
            {
                BackGroundColor = 積みColor;
            }
            else
            {
                BackGroundColor = 揚げColor;
            }

            TreeListViewNode node = CreateNode(BackGroundColor);

            if (IsParent == true && parent.KikanRenkeiFlag == 0) 
            {
                AddSubItemCheckBox(node, BackGroundColor, "", false);
            }
            else
            {
                AddSubItem(node, "", true);
            }

            node.Tag = new DouseiNode(parent, ChildIndex);

            AddSubItem(node, s.DouseiDate.ToString("yy/MM/dd"), true);
            if (s.予定 == false)
            {
                AddSubItem(node, "実", true);
            }
            else
            {
                AddSubItem(node, "予", true);
            }
            AddSubItem(node, s.KanidouseiInfoShubetuName, true);
            //AddSubItem(node, s.BashoName, true);
            AddSubItem(node, s.場所, true);
            //AddSubItem(node, s.KichiName, true);
            AddSubItem(node, s.基地, true);

            AddSubItem(node, s.CargoNames, true);
            AddSubItem(node, s.SumQtty.ToString(".000"), true);

            AddSubItem(node, s.入港時間, true);
            AddSubItem(node, s.着桟時間, true);
            AddSubItem(node, s.荷役開始, true);
            AddSubItem(node, s.荷役終了, true);
            AddSubItem(node, s.離桟時間, true);
            AddSubItem(node, s.出港時間, true);

            return node;
        }

        private TreeListViewNode AddClearNode()
        {

            Color BackGroundColor;

            BackGroundColor = 積みColor;
            
            TreeListViewNode node = CreateNode();

            //AddSubItem(node, "", true);

            //AddSubItem(node, "", true);
            //AddSubItem(node, "", true);
            //AddSubItem(node, "", true);
            //AddSubItem(node, "", true);
            //AddSubItem(node, "", true);

            //AddSubItem(node, "", true);
            //AddSubItem(node, "", true);

            //AddSubItem(node, "", true);
            //AddSubItem(node, "", true);
            //AddSubItem(node, "", true);
            //AddSubItem(node, "", true);
            //AddSubItem(node, "", true);
            //AddSubItem(node, "", true);

            return node;
        }

        public class DouseiNode
        {
            public DjDousei Dousei;
            public int ChildIndex;

            public DouseiNode(DjDousei dousei, int childIndex)
            {
                Dousei = dousei;
                ChildIndex = childIndex;
            }
        }
    }

}
