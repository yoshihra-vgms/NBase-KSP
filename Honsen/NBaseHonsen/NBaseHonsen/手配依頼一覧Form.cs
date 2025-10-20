using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using NBaseUtil;
using NBaseData.DAC;
using SyncClient;
using NBaseHonsen.util;
using NBaseData.DS;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace NBaseHonsen
{
    public partial class 手配依頼一覧Form : Form, IDataSyncObserver
    {
        private TreeListViewDelegate treeListViewDelegate;

        private List<OdThi> odThis = new List<OdThi>();
        private Dictionary<TreeListViewNode, object> nodeModelDic = new Dictionary<TreeListViewNode, object>();

        private HashSet<string> expandNodes = new HashSet<string>();

        public enum SHOW_WINDOW
        {
            手配依頼,
            見積依頼,
            見積回答,
            受領,
            落成,
            支払
        };


        public 手配依頼一覧Form()
        {
            InitializeComponent();
            Text = NBaseCommon.Common.WindowTitle("HN020101", "手配依頼一覧", WcfServiceWrapper.ConnectedServerID);

            if (同期Client.OFFLINE)
            {
                toolStripStatusLabel2.Text = "オフライン";
            }

            InitializeTable();
        }

        private void InitializeTable()
        {
            treeListViewDelegate = new TreeListViewDelegate(treeListView1);
            treeListViewDelegate.SetColumnFont(HonsenUIConstants.DEFAULT_FONT);

            //object[,] columns = new object[,] {
            //                                   {"手配依頼日", 140, null, null},
            //                                   {"手配依頼者", 120, null, null},
            //                                   {"状況", 90, null, null},
            //                                   {"業者 / No", 150, null, null},
            //                                   {"手配内容 / 区分 / 仕様・型式 / 詳細品目", 200, null, null},
            //                                   {"種別", 60, null, null},
            //                                   {"単位", 60, null, null},
            //                                   {"在庫数", 60, null, HorizontalAlignment.Right},
            //                                   {"依頼数", 60, null, HorizontalAlignment.Right},
            //                                   {"発注数", 60, null, HorizontalAlignment.Right},
            //                                   {"備考（品名、規格等）", 250, null, null},
            //                                   {"通信状況", 90, null, null},
            //                                 };
            object[,] columns = new object[,] {
                                               {"手配依頼日", 140, null, null},
                                               {"手配依頼者", 120, null, null},
                                               {"状況", 90, null, null},
                                               {"業者 / No", 150, null, null},
                                               {"手配内容 / 区分 / 仕様・型式 / 詳細品目", 250, null, null},
                                               {"単位", 60, null, null},
                                               {"在庫数", 60, null, HorizontalAlignment.Right},
                                               {"依頼数", 60, null, HorizontalAlignment.Right},
                                               {"発注数", 60, null, HorizontalAlignment.Right},
                                               {"備考（品名、規格等）", 300, null, null},
                                               {"通信状況", 90, null, null},
                                             };
            treeListViewDelegate.SetColumns(columns);
            BuildModel();
            UpdateTable();
        }

        internal void UpdateTable()
        {
            treeListView1.SuspendUpdate();
            
            this.treeListView1.AfterCollapse -= new LidorSystems.IntegralUI.ObjectEventHandler(this.treeListView1_AfterCollapse);
            this.treeListView1.AfterExpand -= new LidorSystems.IntegralUI.ObjectEventHandler(this.treeListView1_AfterExpand);

            treeListView1.Nodes.Clear();

            foreach (OdThi t in odThis)
            {
                if (t.CancelFlag == 1 || t.Status == (int)OdThi.STATUS.事務所未手配)
                {
                    continue;
                }

                TreeListViewNode thiNode = OdThiTreeListViewHelper.CreateOdThiNode(treeListView1,
                                                                            treeListViewDelegate,
                                                                            t);
                nodeModelDic.Add(thiNode, t);

                if (is燃料_潤滑油(t))
                {
                    UpdateTree_燃料_潤滑油(t, thiNode);
                }
                else
                {
                    UpdateTree_燃料_潤滑油以外(t, thiNode);
                }


                if (expandNodes.Contains(t.OdThiID))
                {
                    thiNode.Expand();
                }
            }

            toolStripStatusLabel1.Text = "";

            this.treeListView1.AfterCollapse += new LidorSystems.IntegralUI.ObjectEventHandler(this.treeListView1_AfterCollapse);
            this.treeListView1.AfterExpand += new LidorSystems.IntegralUI.ObjectEventHandler(this.treeListView1_AfterExpand);

            treeListView1.ResumeUpdate();
        }


        private void UpdateTree_燃料_潤滑油(OdThi t, TreeListViewNode thiNode)
        {
            int i = 0;
            foreach (OdThiItem ti in t.OdThiItems)
            {
                if (ti.CancelFlag == 1)
                {
                    continue;
                }

                TreeListViewNode thiItemNode = OdThiTreeListViewHelper.CreateOdThiItemNode(thiNode,
                                                                                  treeListViewDelegate,
                                                                                  ti);
                nodeModelDic.Add(thiItemNode, ti);

                foreach (OdThiShousaiItem si in ti.OdThiShousaiItems)
                {
                    if (si.CancelFlag == 1)
                    {
                        continue;
                    }

                    OdThiTreeListViewHelper.CreateOdThiShousaiItemNode(thiItemNode,
                                                                  treeListViewDelegate,
                                                                  si, 
                                                                  t.MsThiIraiSbtID,
                                                                  ++i);
                }

                if (expandNodes.Contains(ti.OdThiItemID))
                {
                    thiItemNode.Expand();
                }
            }
        }

        
        private void UpdateTree_燃料_潤滑油以外(OdThi t, TreeListViewNode thiNode)
        {
            List<ItemHeader<OdThiItem>> thiItemHeaders = OdThiTreeListViewHelper.GroupByThiItemHeader(t.OdThiItems);

            int i = 0;
            foreach (ItemHeader<OdThiItem> h in thiItemHeaders)
            {
                TreeListViewNode thiItemHeaderNode = OdThiTreeListViewHelper.CreateThiItemHeaderNode(thiNode,
                                                                                  treeListViewDelegate,
                                                                                  h);
                nodeModelDic.Add(thiItemHeaderNode, h);

                foreach (OdThiItem ti in h.Items)
                {
                    if (ti.CancelFlag == 1)
                    {
                        continue;
                    }

                    TreeListViewNode thiItemNode = OdThiTreeListViewHelper.CreateOdThiItemNode(thiItemHeaderNode,
                                                                                      treeListViewDelegate,
                                                                                      ti);
                    nodeModelDic.Add(thiItemNode, ti);

                    foreach (OdThiShousaiItem si in ti.OdThiShousaiItems)
                    {
                        if (si.CancelFlag == 1)
                        {
                            continue;
                        }

                        OdThiTreeListViewHelper.CreateOdThiShousaiItemNode(thiItemNode,
                                                                      treeListViewDelegate,
                                                                      si, 
                                                                      t.MsThiIraiSbtID,
                                                                      ++i);
                    }

                    if (expandNodes.Contains(ti.OdThiItemID))
                    {
                        thiItemNode.Expand();
                    }
                }

                if (expandNodes.Contains(h.Id.ToString()))
                {
                    thiItemHeaderNode.Expand();
                }
            }
        }


        private bool is燃料_潤滑油(OdThi odThi)
        {
            return odThi.MsThiIraiSbtID == MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.燃料_潤滑油);
        }


        internal void BuildModel()
        {
            OdThiFilter filter = CreateSearchFilter();

            if (filter != null)
            {
                List<OdThi> tmp = OdThi.GetRecordsByFilter(同期Client.LOGIN_USER, filter);
                odThis.Clear();

                List<string> thiIds =
                  (from r in tmp
                   select r.OdThiID).ToList<string>();

                Dictionary<string, List<OdThiItem>> thiItemDic = CreateOdThiItemDic(thiIds);
                Dictionary<string, List<OdThiShousaiItem>> thiShousaiItemDic = CreateOdThiShousaiItemDic(thiIds);


                // 2014.02 2013年度改造
                //List<OdJry> OdJrys = OdJry.GetRecordsByFilter(同期Client.LOGIN_USER, (int)OdJry.STATUS.船受領, filter);
                List<OdJry> OdJrys = OdJry.GetRecordsByFilter(同期Client.LOGIN_USER, int.MinValue, filter);
                List<OdMk> OdMks = OdMk.GetRecordsByFilter(同期Client.LOGIN_USER, int.MinValue, filter);

                foreach (OdThi t in tmp)
                {
                    // 2014.02 2013年度改造 ==>
                    var 船受領Jry = from obj in OdJrys
                                 //where obj.OdThiID == t.OdThiID
                                 where obj.OdThiID == t.OdThiID && obj.Status == (int)OdJry.STATUS.船受領
                                 select obj;
                    if (船受領Jry.Count<OdJry>() > 0)
                    {
                        var mks = OdMks.Where(obj => obj.OdThiID == t.OdThiID);
                        int mkCount = mks.Count();

                        var jrys = OdJrys.Where(obj => obj.OdThiID == t.OdThiID);
                        int jryCount = jrys.Count();

                        if (filter.JryStatus != (int)OdJry.STATUS.船受領 && mkCount == 1 && jryCount == 1)
                        {
                            // 条件に船受領のﾁｪｯｸがなく、船受領のﾃﾞｰﾀしかない場合、表示対象からはずす
                            continue;
                        }
                        if (mkCount == 1)
                        {
                            t.OrderThiIraiStatus = "船受領";
                        }

                    }
                    // <==

                    List<OdThiItem> tehaiItems;
                    if (thiItemDic.ContainsKey(t.OdThiID))
                    {
                        tehaiItems = thiItemDic[t.OdThiID];
                    }
                    else
                    {
                        tehaiItems = new List<OdThiItem>();
                    }

                    List<OdThiShousaiItem> tehaiShousaiItems;
                    if (thiShousaiItemDic.ContainsKey(t.OdThiID))
                    {
                        tehaiShousaiItems = thiShousaiItemDic[t.OdThiID];
                    }
                    else
                    {
                        tehaiShousaiItems = new List<OdThiShousaiItem>();
                    }

                    foreach (OdThiItem ti in tehaiItems)
                    {
                        foreach (OdThiShousaiItem si in tehaiShousaiItems)
                        {
                            if (si.OdThiItemID == ti.OdThiItemID)
                            {
                                ti.OdThiShousaiItems.Add(si);
                            }
                        }
                        foreach (OdThiShousaiItem si in ti.OdThiShousaiItems)
                        {
                            tehaiShousaiItems.Remove(si);
                        }

                        t.OdThiItems.Add(ti);
                    }

                    odThis.Add(t);
                }
            }
        }


        private Dictionary<string, List<OdThiItem>> CreateOdThiItemDic(List<string> thiIds)
        {
            Dictionary<string, List<OdThiItem>> result = new Dictionary<string, List<OdThiItem>>();

            List<OdThiItem> thiItems = OdThiItem.GetRecordsByOdThiIDs(同期Client.LOGIN_USER, thiIds.ToList<string>());

            foreach (OdThiItem item in thiItems)
            {
                if (!result.ContainsKey(item.OdThiID))
                {
                    result[item.OdThiID] = new List<OdThiItem>();
                }

                result[item.OdThiID].Add(item);
            }

            return result;
        }


        private Dictionary<string, List<OdThiShousaiItem>> CreateOdThiShousaiItemDic(List<string> thiIds)
        {
            Dictionary<string, List<OdThiShousaiItem>> result = new Dictionary<string, List<OdThiShousaiItem>>();

            List<OdThiShousaiItem> thiItems = OdThiShousaiItem.GetRecordsByOdThiIDs(同期Client.LOGIN_USER, thiIds.ToList<string>());

            foreach (OdThiShousaiItem item in thiItems)
            {
                if (!result.ContainsKey(item.OdThiID))
                {
                    result[item.OdThiID] = new List<OdThiShousaiItem>();
                }

                result[item.OdThiID].Add(item);
            }

            return result;
        }


        private OdThiFilter CreateSearchFilter()
        {
            OdThiFilter filter = new OdThiFilter();
            filter.MsVesselID = 同期Client.LOGIN_VESSEL.MsVesselID;

            DateTime date;
            bool result = DateTime.TryParse(maskedTextBox手配依頼日開始.Text, out date);

            if (result)
            {
                filter.ThiIraiDateFrom = date;
            }

            result = DateTime.TryParse(maskedTextBox手配依頼日終了.Text, out date);

            if (result)
            {
                filter.ThiIraiDateTo = date;
            }

            if (filter.ThiIraiDateFrom > filter.ThiIraiDateTo)
            {
                MessageBox.Show("検索条件の手配依頼日の終了日が開始日以前です。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            
            result = DateTime.TryParse(maskedTextBox受領日開始.Text, out date);

            if (result)
            {
                filter.JryDateFrom = date;
            }

            result = DateTime.TryParse(maskedTextBox受領日終了.Text, out date);

            if (result)
            {
                filter.JryDateTo = date;
            }

            if (filter.JryDateFrom > filter.JryDateTo)
            {
                MessageBox.Show("検索条件の受領日の終了日が開始日以前です。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (checkBox未対応.Checked)
            {
                filter.MsThiIraiStatusIDs.Add(MsThiIraiStatus.ToId(MsThiIraiStatus.THI_IRAI_STATUS.未対応));
            }
            if (checkBox見積中.Checked)
            {
                filter.MsThiIraiStatusIDs.Add(MsThiIraiStatus.ToId(MsThiIraiStatus.THI_IRAI_STATUS.見積中));
            }
            if (checkBox発注済.Checked)
            {
                filter.MsThiIraiStatusIDs.Add(MsThiIraiStatus.ToId(MsThiIraiStatus.THI_IRAI_STATUS.発注済));
            }
            if (checkBox受領済.Checked)
            {
                filter.MsThiIraiStatusIDs.Add(MsThiIraiStatus.ToId(MsThiIraiStatus.THI_IRAI_STATUS.受領済));
            }
            if (checkBox完了.Checked)
            {
                filter.MsThiIraiStatusIDs.Add(MsThiIraiStatus.ToId(MsThiIraiStatus.THI_IRAI_STATUS.完了));
            }

            // 2014.02 2013年度改造
            if (checkBox船受領.Checked)
            {
                filter.JryStatus = (int)OdJry.STATUS.船受領;
            }

            
            return filter;
        }


        private void newButt_Click(object sender, EventArgs e)
        {
            // まず新規に手配依頼する種別を選択する
            依頼種別Form form = new 依頼種別Form(SHOW_WINDOW.手配依頼);
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string shubetsuId = form.SelectedSyubetsu.MsThiIraiSbtID;
            string shousaiShubetsuId = form.SelectedSyousaiSyubetsu != null ?
                                    form.SelectedSyousaiSyubetsu.MsThiIraiShousaiID : null;
            int categoryNumber = form.SelectedCategoryNumber;

            手配依頼Form tehaiHeaderForm = new 手配依頼Form(this, shubetsuId, shousaiShubetsuId, categoryNumber);
            tehaiHeaderForm.ShowDialog();
        }

        private void closeButt_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        private void treeListView1_AfterExpand(object sender, LidorSystems.IntegralUI.ObjectEventArgs e)
        {
            TreeListViewNode node = e.Object as TreeListViewNode;

            if (node != null && nodeModelDic.ContainsKey(node))
            {
                if (nodeModelDic[node] is OdThi)
                {
                    expandNodes.Add(((OdThi)nodeModelDic[node]).OdThiID);
                }
                else if (nodeModelDic[node] is ItemHeader<OdThiItem>)
                {
                    expandNodes.Add(((ItemHeader<OdThiItem>)nodeModelDic[node]).Id.ToString());
                }
                else if (nodeModelDic[node] is OdThiItem)
                {
                    expandNodes.Add(((OdThiItem)nodeModelDic[node]).OdThiItemID);
                }
            }
        }

        private void treeListView1_AfterCollapse(object sender, LidorSystems.IntegralUI.ObjectEventArgs e)
        {
            TreeListViewNode node = e.Object as TreeListViewNode;

            if (node != null && nodeModelDic.ContainsKey(node))
            {
                if (nodeModelDic[node] is OdThi)
                {
                    expandNodes.Remove(((OdThi)nodeModelDic[node]).OdThiID);
                }
                else if (nodeModelDic[node] is ItemHeader<OdThiItem>)
                {
                    expandNodes.Remove(((ItemHeader<OdThiItem>)nodeModelDic[node]).Id.ToString());
                }
                else if (nodeModelDic[node] is OdThiItem)
                {
                    expandNodes.Remove(((OdThiItem)nodeModelDic[node]).OdThiItemID);
                }
            }
        }

        private void button検索_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {

                BuildModel();
                UpdateTable();
            }
            catch
            {
            }
            this.Cursor = Cursors.Default;
        }

        private void button条件クリア_Click(object sender, EventArgs e)
        {
            maskedTextBox手配依頼日開始.Text = null;
            maskedTextBox手配依頼日終了.Text = null;
            maskedTextBox受領日開始.Text = null;
            maskedTextBox受領日終了.Text = null;
            checkBox未対応.Checked = true;
            checkBox見積中.Checked = true;
            checkBox発注済.Checked = true;
            checkBox完了.Checked = false;

            BuildModel();
            UpdateTable();
        }

        #region IDataSyncObserver メンバ

        public void SyncStart()
        {
            try
            {
                Invoke(new MethodInvoker(
                   delegate()
                   {
                       toolStripStatusLabel1.Text = "データ同期中です...";
                   }
               ));
            }
            catch (InvalidOperationException e)
            {
            }
        }

        public void SyncFinish()
        {
            try
            {
                BuildModel();
                Invoke(new MethodInvoker(UpdateTable));
            }
            catch (InvalidOperationException e)
            {
            }
        }


        public void Online()
        {
            try
            {
                Invoke(new MethodInvoker(
                   delegate()
                   {
                       toolStripStatusLabel2.Text = "";
                   }
               ));
            }
            catch (InvalidOperationException e)
            {
            }
        }


        public void Offline()
        {
            try
            {
                Invoke(new MethodInvoker(
                   delegate()
                   {
                       toolStripStatusLabel2.Text = "オフライン";
                   }
               ));
            }
            catch (InvalidOperationException e)
            {
            }
        }


        public void Message(string message)
        {
        }

        public void Message2(string message)
        {
        }

        public void Message3(string message)
        {
        }

        #endregion

        private void treeListView1_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                TreeListViewNode selected = treeListView1.GetNodeAt(me.X, me.Y);

                if (selected != null && !selected.ExpandButtonRect.Contains(me.X, me.Y))
                {
                    TreeListViewNode node = treeListView1.SelectedNode;

                    if (node != null && nodeModelDic.ContainsKey(node))
                    {
                        if (nodeModelDic[node] is OdThi)
                        {
                            OdThi thi = (OdThi)nodeModelDic[node];

                            int categoryNumber = is船用品ペイント(thi) ? MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント) : -1;

                            手配依頼Form tehaiHeaderForm = new 手配依頼Form(this, thi.MsThiIraiSbtID, thi.MsThiIraiShousaiID, categoryNumber, thi);
                            tehaiHeaderForm.ShowDialog();
                        }
                    }
                }
            }
        }

        private bool is船用品ペイント(OdThi thi)
        {
            List<MsVesselItem> msVesselItemList = MsVesselItem.GetRecords(同期Client.LOGIN_USER);

            List<OdThiShousaiItem> shousaiItems = new List<OdThiShousaiItem>();
            foreach(OdThiItem item in thi.OdThiItems)
            {
                shousaiItems.AddRange(item.OdThiShousaiItems);
            }
            var vesselItemIdList = shousaiItems.Where(obj => obj.MsVesselItemID != null && obj.MsVesselItemID != string.Empty).Select(obj => obj.MsVesselItemID).Distinct();
            var categoryNumberList = msVesselItemList.Where(obj => vesselItemIdList.Contains(obj.MsVesselItemID)).Select(obj => obj.CategoryNumber).Distinct();

            if (categoryNumberList.Contains(MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント)))
                return true;
            else
                return false;
        }
    }
}
