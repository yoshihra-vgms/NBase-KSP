using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using SyncClient;
using LidorSystems.IntegralUI.Lists;
using System.Windows.Forms;
using System.Drawing;

namespace NBaseHonsen
{
    partial class 手配依頼Form
    {
        private class FormDelegate_燃料_潤滑油 : IFormDelegate
        {
            private 手配依頼Form form;
            private bool readOnly;


            #region IFormDelegate メンバ

            internal FormDelegate_燃料_潤滑油(手配依頼Form form)
            {
                this.form = form;
            }

            public void InitializeTable()
            {
                object[,] columns = new object[,] {
                                                   {"No", 100, null, HorizontalAlignment.Right},
                                                   {"品目 / 詳細品目", 400, null, null},
                                                   {"単位", 60, null, null},
                                                   {"依頼数", 60, Color.Turquoise, HorizontalAlignment.Right},
                                                 };

                if (form.GetStatus() != Status.手配依頼済かつ同期済_3)
                {
                    List<OdThiItem> odThiItems = CreateOdThiItems_燃料_潤滑油();
                    form.odThi.OdThiItems.Clear();
                    form.odThi.OdThiItems.AddRange(odThiItems);
                }

                form.treeListViewDelegate.SetColumns(columns);
            }


            public void UpdateTable()
            {
                int i = 0;
                foreach (OdThiItem t in form.odThi.OdThiItems)
                {
                    if (t.CancelFlag == 1)
                    {
                        continue;
                    }

                    TreeListViewNode thiItemNode = form.CreateOdThiItemNode(null, t);

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


            public TreeListViewNode CreateOdThiShousaiItemNode(TreeListViewNode thiItemNode, OdThiShousaiItem si, int i)
            {
                TreeListViewNode node = form.treeListViewDelegate.CreateNode();

                form.treeListViewDelegate.AddSubItem(node, i.ToString(), true);
                form.treeListViewDelegate.AddSubItem(node, si.ShousaiItemName, true);
                form.treeListViewDelegate.AddSubItem(node, MasterTable.instance().GetMsTani(si.MsTaniID).TaniName, true);

                if (!readOnly)
                {
                    form.treeListViewDelegate.AddSubItem(node, si.Count.ToString(), si.Count.ToString(), false,
                                                         delegate(object sender, EventArgs e)
                                                         {
                                                             node.UpdateLayout();
                                                             TextBox textBox = (TextBox)sender;
                                                             TreeListViewSubItem subItem = textBox.Tag as TreeListViewSubItem;

                                                             int iraiCount;
                                                             int.TryParse(textBox.Text, out iraiCount);
                                                             si.Count = iraiCount;
                                                             si.Sateisu = iraiCount;

                                                             form.treeListViewDelegate.UpdateCurrentView();
                                                         });
                }
                else
                {
                    form.treeListViewDelegate.AddSubItem(node, si.Count.ToString(), true);
                }

                thiItemNode.Nodes.Add(node);

                return node;
            }


            public void treeListView1_DoubleClick(object sender, EventArgs e)
            {
            }


            public void InitializeComponentsEnabled()
            {
                form.panel1.Visible = true;
                form.button品目追加.Enabled = false;

                if (form.odThi.Kiboubi != DateTime.MinValue)
                {
                    form.dateTimePicker希望納期.Value = form.odThi.Kiboubi;
                }
                form.textBox希望港.Text = form.odThi.Kiboukou;

                if (form.GetStatus() == Status.手配依頼済かつ同期済_3)
                {
                    readOnly = true;
                }
            }


            public void DetectInsertRecords()
            {
                // 依頼数が 0 のものは、INSERT しない。
                // ただし、すでに DB 保存済みのものは UPDATE の必要があるため除去しない.
                List<OdThiItem> newTis = new List<OdThiItem>();

                for (int i = 0; i < form.odThi.OdThiItems.Count; i++)
                {
                    OdThiItem ti = form.odThi.OdThiItems[i];

                    List<OdThiShousaiItem> newSis = new List<OdThiShousaiItem>();
                    
                    for (int k = 0; k < ti.OdThiShousaiItems.Count; k++)
                    {
                        OdThiShousaiItem si = ti.OdThiShousaiItems[k];
                        
                        if (si.Count > 0 || (si.RenewDate != null && si.RenewUserID != null))
                        {
                            if (si.Count == 0)
                            {
                                si.CancelFlag = 1;
                            }
                            
                            newSis.Add(si);
                        }
                    }

                    if (newSis.Count > 0)
                    {
                        ti.OdThiShousaiItems.Clear();
                        foreach (OdThiShousaiItem nsi in newSis)
                        {
                            ti.OdThiShousaiItems.Add(nsi);
                        }
                        newTis.Add(ti);
                    }
                }

                form.odThi.OdThiItems.Clear();
                foreach (OdThiItem nti in newTis)
                {
                    form.odThi.OdThiItems.Add(nti);
                }
            }

            #endregion

            private List<OdThiItem> CreateOdThiItems_燃料_潤滑油()
            {
                List<OdThiItem> result = new List<OdThiItem>();

                // 既保存の品目の Dictionary を生成する.
                var savedItemDic = new Dictionary<string, OdThiItem>();
                 
                foreach (OdThiItem ti in form.odThi.OdThiItems)
                {
                    savedItemDic[ti.ItemName] = ti;
                }
                
                result.Add(CreateFoItem(savedItemDic));
                result.Add(CreateLoItem(savedItemDic));
                result.Add(CreateEtcItem(savedItemDic));
                
                return result;
            }


            private OdThiItem CreateFoItem(Dictionary<string, OdThiItem> savedItemDic)
            {
                OdThiItem result;
                
                if (savedItemDic.ContainsKey(NBaseCommon.Common.MsLo_FO_String))
                {
                    result = savedItemDic[NBaseCommon.Common.MsLo_FO_String];
                }
                else
                {
                    result = CreateOdThiItem(NBaseCommon.Common.MsLo_FO_String);
                }

                // 既保存の詳細品目の Dictionary を生成する.
                var savedShousaiItemDic = new Dictionary<string, OdThiShousaiItem>();

                foreach (OdThiShousaiItem si in result.OdThiShousaiItems)
                {
                    savedShousaiItemDic[si.ShousaiItemName] = si;
                }

                result.OdThiShousaiItems.Clear();

                foreach (MsLo fo in NBaseCommon.Common.MsLo_Fos())
                {
                    if (savedShousaiItemDic.ContainsKey(fo.LoName))
                    {
                        result.OdThiShousaiItems.Add(savedShousaiItemDic[fo.LoName]);
                    }
                    else
                    {
                        result.OdThiShousaiItems.Add(CreateOdThiShousaiItem(fo.MsLoID,
                                                                            fo.LoName,
                                                                            fo.MsTaniID,
                                                                            fo.MsTaniName));
                    }
                }

                return result;
            }


            private OdThiItem CreateLoItem(Dictionary<string, OdThiItem> savedItemDic)
            {
                OdThiItem result;

                if (savedItemDic.ContainsKey(NBaseCommon.Common.MsLo_LO_String))
                {
                    result = savedItemDic[NBaseCommon.Common.MsLo_LO_String];
                }
                else
                {
                    result = CreateOdThiItem(NBaseCommon.Common.MsLo_LO_String);
                }

                // 既保存の詳細品目の Dictionary を生成する.
                var savedShousaiItemDic = new Dictionary<string, OdThiShousaiItem>();

                foreach (OdThiShousaiItem si in result.OdThiShousaiItems)
                {
                    savedShousaiItemDic[si.MsLoID] = si;
                }

                result.OdThiShousaiItems.Clear();

                List<MsLoVessel> loVessels = MsLoVessel.GetRecordsByMsVesselID(NBaseCommon.Common.LoginUser,
                                                                              同期Client.LOGIN_VESSEL.MsVesselID);

                foreach (MsLoVessel loVessel in loVessels)
                {
                    if (loVessel.MsLoID.StartsWith(NBaseCommon.Common.PrefixMsLoID_LO))
                    {
                        if (savedShousaiItemDic.ContainsKey(loVessel.MsLoID))
                        {
                            result.OdThiShousaiItems.Add(savedShousaiItemDic[loVessel.MsLoID]);
                        }
                        else
                        {
                            result.OdThiShousaiItems.Add(CreateOdThiShousaiItem(loVessel.MsLoID,
                                                                                  loVessel.MsLoName,
                                                                                  loVessel.MsTaniID,
                                                                                  loVessel.MsTaniName));
                        }
                    }
                }

                return result;
            }


            private OdThiItem CreateEtcItem(Dictionary<string, OdThiItem> savedItemDic)
            {
                OdThiItem result;

                if (savedItemDic.ContainsKey(NBaseCommon.Common.MsLo_ETC_String))
                {
                    result = savedItemDic[NBaseCommon.Common.MsLo_ETC_String];
                }
                else
                {
                    result = CreateOdThiItem(NBaseCommon.Common.MsLo_ETC_String);
                }

                // 既保存の詳細品目の Dictionary を生成する.
                var savedShousaiItemDic = new Dictionary<string, OdThiShousaiItem>();

                foreach (OdThiShousaiItem si in result.OdThiShousaiItems)
                {
                    savedShousaiItemDic[si.MsLoID] = si;
                }

                result.OdThiShousaiItems.Clear();

                List<MsLoVessel> loVessels = MsLoVessel.GetRecordsByMsVesselID(NBaseCommon.Common.LoginUser,
                                                                              同期Client.LOGIN_VESSEL.MsVesselID);

                foreach (MsLoVessel loVessel in loVessels)
                {
                    if (loVessel.MsLoID.StartsWith(NBaseCommon.Common.PrefixMsLoID_ETC))
                    {
                        if (savedShousaiItemDic.ContainsKey(loVessel.MsLoID))
                        {
                            result.OdThiShousaiItems.Add(savedShousaiItemDic[loVessel.MsLoID]);
                        }
                        else
                        {
                            result.OdThiShousaiItems.Add(CreateOdThiShousaiItem(loVessel.MsLoID,
                                                                                 loVessel.MsLoName,
                                                                                 loVessel.MsTaniID,
                                                                                 loVessel.MsTaniName));
                        }
                    }
                }

                return result;
            }


            private OdThiItem CreateOdThiItem(string itemName)
            {
                OdThiItem item = new OdThiItem();

                item.Header = string.Empty;
                item.ItemName = itemName;
                item.VesselID = 同期Client.LOGIN_VESSEL.MsVesselID;
                item.RenewDate = DateTime.Now;
                item.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

                return item;
            }


            private OdThiShousaiItem CreateOdThiShousaiItem(string msLoId, string loName, string msTaniId, string msTaniName)
            {
                OdThiShousaiItem shousaiItem = new OdThiShousaiItem();

                shousaiItem.MsLoID = msLoId;
                shousaiItem.ShousaiItemName = loName;
                shousaiItem.MsTaniID = msTaniId;
                shousaiItem.MsTaniName = msTaniName;
                shousaiItem.VesselID = 同期Client.LOGIN_VESSEL.MsVesselID;
                shousaiItem.RenewDate = DateTime.Now;
                shousaiItem.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

                return shousaiItem;
            }



            
        }
    }
}
