using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using SyncClient;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using NBaseHonsen.util;
using System.Drawing;

namespace NBaseHonsen
{
    partial class 手配依頼Form
    {
        private interface IFormDelegate
        {
            void InitializeTable();

            void UpdateTable();

            TreeListViewNode CreateOdThiShousaiItemNode(TreeListViewNode thiItemNode, OdThiShousaiItem si, int i);

            void treeListView1_DoubleClick(object sender, EventArgs e);

            void InitializeComponentsEnabled();

            void DetectInsertRecords();


            
        }
    }
}
