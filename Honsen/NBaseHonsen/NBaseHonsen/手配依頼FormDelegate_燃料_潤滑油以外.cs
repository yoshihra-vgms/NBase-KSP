using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using SyncClient;
using LidorSystems.IntegralUI.Lists;
using System.Windows.Forms;
using System.Drawing;
using NBaseHonsen.util;

namespace NBaseHonsen
{
    partial class 手配依頼Form
    {
        private class FormDelegate_燃料_潤滑油以外 : IFormDelegate
        {
            private 手配依頼Form form;

            #region IFormDelegate メンバ

            internal FormDelegate_燃料_潤滑油以外(手配依頼Form form)
            {
                this.form = form;
            }

            public void InitializeTable()
            {
                object[,] columns = null;

                //if (form.is船用品())
                //{
                //    columns = new object[,] {
                //                            {"No", 100, null, HorizontalAlignment.Right},
                //                            {"区分 / 仕様・型式 / 詳細品目", 400, null, null},
                //                            {"種別", 60, null, null},
                //                            {"単位", 60, null, null},
                //                            {"在庫数", 60, null, HorizontalAlignment.Right},
                //                            {"依頼数", 60, null, HorizontalAlignment.Right},
                //                            {"添付", 40, null, HorizontalAlignment.Center},
                //                            {"備考（品名、規格等）", 350, null, null},
                //                            };
                //}
                //else
                //{
                    columns = new object[,] {
                                            {"No", 100, null, HorizontalAlignment.Right},
                                            {"区分 / 仕様・型式 / 詳細品目", 400, null, null},
                                            {"単位", 60, null, null},
                                            {"在庫数", 60, null, HorizontalAlignment.Right},
                                            {"依頼数", 60, null, HorizontalAlignment.Right},
                                            {"添付", 40, null, HorizontalAlignment.Center},
                                            {"備考（品名、規格等）", 350, null, null},
                                            };
                //}

                form.treeListViewDelegate.SetColumns(columns);
                form.treeListViewDelegate.LinkClickEvent += new NBaseUtil.TreeListViewDelegate.LinkClickEventHandler(form.View添付ファイル);
            }


            public void UpdateTable()
            {
                List<ItemHeader<OdThiItem>> thiItemHeaders = OdThiTreeListViewHelper.GroupByThiItemHeader(form.odThi.OdThiItems);

                int i = 0;
                foreach (ItemHeader<OdThiItem> h in thiItemHeaders)
                {
                    TreeListViewNode thiItemHeaderNode = form.CreateThiItemHeaderNode(null, h);

                    foreach (OdThiItem t in h.Items)
                    {
                        if (t.CancelFlag == 1)
                        {
                            continue;
                        }

                        TreeListViewNode thiItemNode = form.CreateOdThiItemNode(thiItemHeaderNode, t);

                        form.odThiItemNodes.Add(thiItemNode, t);

                        //ノード開く
                        thiItemNode.EnsureVisible(LidorSystems.IntegralUI.VerticalAlignment.Top);

                        foreach (OdThiShousaiItem si in t.OdThiShousaiItems)
                        {
                            if (si.CancelFlag == 1)
                            {
                                continue;
                            }

                            //ノードを開くため　戻り値取得　2021/09/10 m.yoshihara
                            TreeListViewNode wknd = form.CreateOdThiShousaiItemNode(thiItemNode, si, ++i);

                            //ノード開く
                            wknd.EnsureVisible(LidorSystems.IntegralUI.VerticalAlignment.Top);

                        }
                    }
                }
            }


            public TreeListViewNode CreateOdThiShousaiItemNode(TreeListViewNode thiItemNode, OdThiShousaiItem si, int i)
            {
                TreeListViewNode node = form.treeListViewDelegate.CreateNode();

                form.treeListViewDelegate.AddSubItem(node, i.ToString(), true);
                form.treeListViewDelegate.AddSubItem(node, si.ShousaiItemName, true);
                //if (form.is船用品())
                //{
                //    form.treeListViewDelegate.AddSubItem(node, OdThiTreeListViewHelper.GetShousaiInputKind(si, MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品)), true);
                //}
                form.treeListViewDelegate.AddSubItem(node, MasterTable.instance().GetMsTani(si.MsTaniID).TaniName, true);
                form.treeListViewDelegate.AddSubItem(node, si.ZaikoCount != int.MinValue ? si.ZaikoCount.ToString() : string.Empty, true);
                form.treeListViewDelegate.AddSubItem(node, si.Count != int.MinValue ? si.Count.ToString() : string.Empty, true);
                if (si.OdAttachFileID != null && si.OdAttachFileID.Length > 0)
                {
                    form.treeListViewDelegate.AddLinkItem(node, si.OdAttachFileID);
                }
                else
                {
                    form.treeListViewDelegate.AddSubItem(node, "", true);
                }
                form.treeListViewDelegate.AddSubItem(node, si.Bikou, true);

                thiItemNode.Nodes.Add(node);

                return node;
            }


            public void treeListView1_DoubleClick(object sender, EventArgs e)
            {
                TreeListViewNode node = form.treeListView1.SelectedNode;

                if (node != null && form.odThiItemNodes.ContainsKey(node))
                {
                    OdThiItem item = form.odThiItemNodes[node];

                    List<OdAttachFile> 添付Files = form.添付Files;
                    手配品目Form tehaiHinmokuForm = new 手配品目Form(form, item, form.msThiIraiSbtId, ref 添付Files);
                    tehaiHinmokuForm.ShowDialog();

                }
            }


            public void InitializeComponentsEnabled()
            {
            }


            public void DetectInsertRecords()
            {
                // 2009.11.18:aki 
                // DetectInsertRecords()は、もとは何もしないメソッド
                // 表示順序を入れ替えるために以下のコードを追加しました
                List<OdThiItem> newTis = new List<OdThiItem>();
                List<ItemHeader<OdThiItem>> thiItemHeaders = OdThiTreeListViewHelper.GroupByThiItemHeader(form.odThi.OdThiItems, false);

                foreach (ItemHeader<OdThiItem> h in thiItemHeaders)
                {
                    //TreeListViewNode thiItemHeaderNode = form.CreateThiItemHeaderNode(null, h);

                    foreach (OdThiItem t in h.Items)
                    {
                        newTis.Add(t);
                    }
                }

                form.odThi.OdThiItems.Clear();
                foreach (OdThiItem nti in newTis)
                {
                    form.odThi.OdThiItems.Add(nti);
                }
            }
                        
            #endregion            
        }
    }
}
