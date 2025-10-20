using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using NBaseData.DAC;
using NBaseUtil;
using SyncClient;
using NBaseHonsen.util;

namespace NBaseHonsen
{
    public partial class 一括受領Form : Form
    {
        private readonly PortalForm portalForm;
        private readonly OdJry odJry;

        private TreeListViewDelegate treeListViewDelegate;


        public 一括受領Form(PortalForm portalForm, OdJry odJry)
        {
            同期Client.SYNC_SUSPEND = true;

            this.portalForm = portalForm;
            this.odJry = odJry;

            InitializeComponent();
            Text = NBaseCommon.Common.WindowTitle("HN010202", "一括受領", WcfServiceWrapper.ConnectedServerID);

            if (odJry.Status != (int)OdJry.STATUS.未受領)
            {
                butt一括受領.Enabled = false;
            }
        }

        private void 一括受領Form_Load(object sender, EventArgs e)
        {
            InitializeTable();
        }

        private void InitializeTable()
        {
            treeListViewDelegate = new TreeListViewDelegate(treeListView1);
            treeListViewDelegate.SetColumnFont(HonsenUIConstants.DEFAULT_FONT);

            object[,] columns = new object[,] {
                                               {"手配依頼日", 140, null, null},
                                               {"出荷予定日", 130, null, null},
                                               {"手配内容", 200, null, null},
                                               {"業者 / No", 150, null, null},
                                               {"区分 / 仕様・型式 / 詳細品目", 200, null, null},
                                               {"納品数", 60, null, HorizontalAlignment.Right},
                                               {"受領数", 60, null, HorizontalAlignment.Right},
                                               {"手配依頼者", 120, null, null},
                                               {"通信状況", 90, null, null},
                                               {"備考", 200, null, null},
                                             };
            treeListViewDelegate.SetColumns(columns);
            UpdateTable();
        }

        private void UpdateTable()
        {
            treeListView1.SuspendUpdate();

            treeListView1.Nodes.Clear();

            TreeListViewNode jryNode = OdJryTreeListViewHelper.CreateOdJryNode(treeListView1,
                                                                               treeListViewDelegate,
                                                                               odJry);

            portalForm.UpdateTree(odJry, jryNode);
            
            //treeListView1.ExpandAll();
            treeListView1.EnsureVisible(jryNode);
            foreach (TreeListViewNode node1 in jryNode.Nodes)
            {
                treeListView1.EnsureVisible(node1);
                foreach (TreeListViewNode node2 in node1.Nodes)
                {
                    treeListView1.EnsureVisible(node2);
                    foreach (TreeListViewNode node3 in node2.Nodes)
                    {
                        treeListView1.EnsureVisible(node3);
                    }
                }
            }

            treeListView1.ResumeUpdate();
        }

        private void butt一括受領_Click(object sender, EventArgs e)
        {
            一括受領確認Form form = new 一括受領確認Form(portalForm, odJry);

            if (form.ShowDialog() == DialogResult.Yes)
            {
                Dispose();
            }
        }

        private void cancelButt_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void 一括受領Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            同期Client.SYNC_SUSPEND = false;
        }
    }
}
