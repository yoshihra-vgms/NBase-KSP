using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NBaseData.DAC;
using NBaseData.DS;
using LidorSystems.IntegralUI.Lists;


namespace Senin.util
{
    public partial class SeninListControl : UserControl
    {

        private bool beforeSelectFlag = false;

        public delegate void ClickEventHandler(int seninId);
        public event ClickEventHandler ClickEvent;


        private TreeListViewDelegate船員 treeListViewDelegate;

        public SeninListControl()
        {
            InitializeComponent();

            treeListViewDelegate = new TreeListViewDelegate船員(treeListView1);
        }


        public void DrawList(List<MsSenin> datas)
        {
            treeListViewDelegate.SetColumns();
            treeListViewDelegate.SetRows(datas);
        }

        public void DrawList(List<SiPresentaionItem> headres, List<MsSeninPlus> datas)
        {
            treeListViewDelegate.SetColumns(headres);
            treeListViewDelegate.SetRows(headres, datas);
        }

        private void treeListView1_BeforeSelect(object sender, LidorSystems.IntegralUI.ObjectCancelEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("treeListView1_BeforeSelect");

            beforeSelectFlag = true;
        }

        private void treeListView1_Click(object sender, EventArgs e)
        {
            if (ClickEvent == null)
                return;

            System.Diagnostics.Debug.WriteLine("treeListView1_Click");

            beforeSelectFlag = false;

            if (e is MouseEventArgs)
            {
                MouseEventArgs me = (MouseEventArgs)e;

                if (me.Y <= 16)
                {
                    return;
                }

                TreeListViewNode node = treeListView1.GetNodeAt(me.X, me.Y);

                if (node != null)
                {
                    MsSenin senin = node.Tag as MsSenin;
                    ClickEvent(senin.MsSeninID);
                }
            }
        }

        private void treeListView1_AfterSelect(object sender, LidorSystems.IntegralUI.ObjectEventArgs e)
        {
            if (beforeSelectFlag)
                return;

            if (ClickEvent == null)
                return;

            System.Diagnostics.Debug.WriteLine("treeListView1_AfterSelect");

            if (e.Object is LidorSystems.IntegralUI.Lists.TreeListViewNode)
            {
                LidorSystems.IntegralUI.Lists.TreeListViewNode node = (LidorSystems.IntegralUI.Lists.TreeListViewNode)e.Object;
                if (node != null)
                {
                    System.Diagnostics.Debug.WriteLine(node.Index.ToString());

                    MsSenin senin = node.Tag as MsSenin;
                    ClickEvent(senin.MsSeninID);
                }
            }
        }

    }
}
