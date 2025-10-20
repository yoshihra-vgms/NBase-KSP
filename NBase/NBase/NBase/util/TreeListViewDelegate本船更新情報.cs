using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseUtil;
using NBaseData.DAC;
using LidorSystems.IntegralUI.Lists;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NBase.util
{
    internal class TreeListViewDelegate本船更新情報 : TreeListViewDelegate
    {
        internal TreeListViewDelegate本船更新情報(TreeListView treeListView)
            : base(treeListView)
        {
            treeListView.SuspendUpdate();

            //SetColumns(CreateColumns());

            treeListView.ResumeUpdate();
        }


        internal void SetRows(List<PtHonsenkoushinInfo> ptHonsenkoushinInfo_list, Hashtable KoushinInfoHash)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            if (ptHonsenkoushinInfo_list != null)
            {
                for (int i = 0; i < ptHonsenkoushinInfo_list.Count; i++)
                {
                    PtHonsenkoushinInfo ptHonsenkoushinInfo = ptHonsenkoushinInfo_list[i];

                    TreeListViewNode node = CreateNode();

                    AddSubItem(node, ptHonsenkoushinInfo.EventDate.ToString("yyyy/MM/dd"), true);
                    AddSubItem(node, ptHonsenkoushinInfo.VesselName, true);
                    AddSubItem(node, ptHonsenkoushinInfo.Shubetsu, true);
                    AddSubItem(node, ptHonsenkoushinInfo.Naiyou, true);
                    AddSubItem(node, ptHonsenkoushinInfo.KoushinNaiyou.Replace('\\', (char)165), true);
                    AddSubItem(node, ptHonsenkoushinInfo.HonsenkoushinInfoUserName, true);

                    node.Tag = ptHonsenkoushinInfo;

                    treeListView.Nodes.Add(node);

                    // ハッシュに追加
                    KoushinInfoHash.Add(node, ptHonsenkoushinInfo);
                }
            }

            treeListView.ResumeUpdate();
        }
    }
}
