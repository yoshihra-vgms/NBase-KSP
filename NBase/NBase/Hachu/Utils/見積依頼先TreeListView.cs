using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using LidorSystems.IntegralUI.Lists.Collections;
using NBaseData.DAC;
using Hachu.Models;
using Hachu.Controllers;
using Hachu.HachuManage;

namespace Hachu.Utils
{
    public class 見積依頼先TreeListView : ItemTreeListView
    {
        private NodeController NodeControllers = null;

        public 見積依頼先TreeListView(TreeListView treeListView)
            : base(treeListView)
        {
            NodeControllers = new NodeController();
        }
        public override void Clear()
        {
            base.Clear();
            NodeControllers.Clear();
        }
        public int Count
        {
            get
            {
                return NodeControllers.Count;
            }
        }
        public void AddNode(見積依頼先 依頼先)
        {
            // ノードの追加
            TreeListViewNode node = MakeNode(依頼先);
            treeListView.Nodes.Add(node);

            NodeControllers.SetNode(依頼先.OdMkID, 依頼先, node);

            // 全部、開く
            treeListView.ExpandAll();
        }
        public void AddNodes(List<見積依頼先> 依頼先s)
        {
            treeListView.SuspendUpdate();

            // ノードの追加
            foreach (見積依頼先 依頼先 in 依頼先s)
            {
                TreeListViewNode node = MakeNode(依頼先);
                treeListView.Nodes.Add(node);

                NodeControllers.SetNode(依頼先.OdMkID, 依頼先, node);
            }

            // 全部、開く
            treeListView.ExpandAll();
            treeListView.ResumeUpdate();
        }

        private TreeListViewNode MakeNode(見積依頼先 依頼先)
        {
            TreeListViewNode node = new TreeListViewNode();

            string mailSendMsg = "未送信";
            string maiSendDate = "";
            if (依頼先.TantouMailAddress.Length > 0)
            {
                mailSendMsg = "送信済";
                maiSendDate = 依頼先.CreateDate.ToString("yy/MM/dd HH:mm");
            }

            // 依頼先名
            AddSubItemAsShousai(node, 0, 依頼先.CustomerName, true);
            // メール送信
            AddSubItem(node, mailSendMsg, true);
            // 送信日時
            AddSubItem(node, maiSendDate, true);

            return node;
        }
        public 見積依頼先 GetSelectedInfo()
        {
            string key = null;
            TreeListViewNode selectedNode = treeListView.SelectedNode;
            if (selectedNode == null)
            {
                return null;
            }
            key = NodeControllers.GetTopKey(selectedNode);
            return NodeControllers.GetTopInfo(key) as 見積依頼先;
        }
    }
}
