using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WingUtil;
using System.Drawing;
using LidorSystems.IntegralUI.Lists;
using WingData.DAC;
using System.Windows.Forms;
using Yojitsu.DA;

namespace Yojitsu
{
    partial class 販管割掛入力Form
    {
        partial void InitializeMockData()
        {
            CreateTable();
        }

        private void CreateTable()
        {
            TreeListViewDelegate treeListViewDelegate = new TreeListViewDelegate(treeListView1);

            object[,] columns = new object[,] {
                                               {"船名", 200, null, null},
                                               {"DWT(LT)", 100, null, HorizontalAlignment.Right},
                                               {"営業基礎割掛", 100, null, HorizontalAlignment.Right},
                                               {"管理基礎割掛", 100, null, HorizontalAlignment.Right},
                                               {"差額DWT割掛", 100, null, HorizontalAlignment.Right},
                                               {"計", 100, null, HorizontalAlignment.Right},
                                               {"総額に対する割合", 100, null, HorizontalAlignment.Right},
                                              };
            treeListViewDelegate.SetColumns(columns);

            foreach (MsVessel v in DbAccessorFactory.FACTORY.MsVessel_GetRecords(WingCommon.Common.LoginUser))
            {
                TreeListViewNode node = treeListViewDelegate.CreateNode();
                treeListViewDelegate.AddSubItem(node, v.VesselName, true);
                treeListViewDelegate.AddSubItem(node, v.DWT.ToString("#,##0"), true);
                treeListViewDelegate.AddSubItem(node, "", true);
                treeListViewDelegate.AddSubItem(node, "", true);
                treeListViewDelegate.AddSubItem(node, "", true);
                treeListViewDelegate.AddSubItem(node, "", true);
                treeListViewDelegate.AddSubItem(node, "", true);

                treeListView1.Nodes.Add(node);
            }

            TreeListViewNode node1 = treeListViewDelegate.CreateNode();
            treeListViewDelegate.AddSubItem(node1, Color.Yellow, "総計", true);
            treeListViewDelegate.AddSubItem(node1, Color.Yellow, "", true);
            treeListViewDelegate.AddSubItem(node1, Color.Yellow, "", true);
            treeListViewDelegate.AddSubItem(node1, Color.Yellow, "", true);
            treeListViewDelegate.AddSubItem(node1, Color.Yellow, "", true);
            treeListViewDelegate.AddSubItem(node1, Color.Yellow, "", true);
            treeListViewDelegate.AddSubItem(node1, Color.Yellow, "", true);

            treeListView1.Nodes.Add(node1);
        }
    }
}
