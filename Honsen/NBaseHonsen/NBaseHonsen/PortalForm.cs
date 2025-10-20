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
using SyncClient;
using NBaseUtil;
using NBaseHonsen.util;
using System.Net.NetworkInformation;
using NBaseHonsen.Senin;
using System.Threading;
using NBaseData.DS;
using System.Diagnostics;
using NBaseCommon;
using NBaseHonsen.Dousei;
using WTM;
using WtmData;

namespace NBaseHonsen
{
    public partial class PortalForm : ExForm, IDataSyncObserver
    {
        private TreeListViewDelegate treeListViewDelegate;

        private List<OdJry> odJrys = new List<OdJry>();
        private Dictionary<TreeListViewNode, object> nodeModelDic = new Dictionary<TreeListViewNode, object>();

        private HashSet<string> expandNodes = new HashSet<string>();

        public List<MsKanidouseiInfoShubetu> MsKanidouseiInfoShubetu_List;
        private int KanidouseiDataHash = 0;
        List<PtKanidouseiInfo> ptKanidouseiInfo_list;

        private bool 簡易動静タブ選択;

        private DateTime ColStartDate = DateTime.Today;


        public PortalForm()
        {
            InitializeComponent();
            Text = NBaseCommon.Common.WindowTitle("HN010201", "ポータル", WcfServiceWrapper.ConnectedServerID);

            kaniDouseiControl1.AfterEdit += new NBaseHonsen.Controls.KaniDouseiControl2.AfterEditHandler(簡易動静_AfterEdit);

            // 簡易動静情報の初期表示は、昨日
            ColStartDate = DateTime.Today.AddDays(-1);
            dateTimePicker_動静表.Value = ColStartDate;
        }

        private void PortalForm_Load(object sender, EventArgs e)
        {
            label_Vessel.Text = "- " + 同期Client.LOGIN_VESSEL.VesselName + " -";
            label_Vessel.Left = Width / 2 - label_Vessel.Width / 2;

            if (同期Client.OFFLINE)
            {
                toolStripStatusLabel2.Text = "オフライン";
            }

            labelPC名.Text = "ｺﾝﾋﾟｭｰﾀ名：" + 同期Client.GetHostName();

            EnableComponents();

            dateTimePicker1.Value = DateTime.Now.AddDays(-14); // ２週間前から
            dateTimePicker2.Value = DateTime.Now.AddDays(0);
        }


        private void PortalForm_Shown(object sender, EventArgs e)
        {
            this.Refresh();

            InitTabPages();

        }

        #region private async void InitTabPages()
        private async void InitTabPages()
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate ()
                {

                    if (button手配依頼.Visible)
                    {
                        treeListView1.Visible = false;

                        label_納品一覧メッセージ.Visible = true;
                        label_納品一覧メッセージ.Refresh();

                        InitializeTable();

                        label_納品一覧メッセージ.Visible = false;
                        treeListView1.Visible = true;

                        treeListView1.UpdateCurrentView();
                    }
                    InitializeAlarmInfoTable();
                    InitializeJimushoKoushinInfoTable();

                });
            });
        }
        #endregion

        /// <summary>
        /// 契約、権限による画面部品の表示/非表示
        /// </summary>
        #region private void EnableComponents()
        private void EnableComponents()
        {
            if (同期Client.LOGIN_VESSEL.HachuEnabled == 0)
            {
                button手配依頼.Enabled = false;
                button貯蔵品編集.Enabled = false;

                button手配依頼.BackColor = SystemColors.Control;
                button貯蔵品編集.BackColor = SystemColors.Control;
            }
            else
            {
                button手配依頼.Enabled = true;
                button貯蔵品編集.Enabled = true;
            }

            if (同期Client.LOGIN_VESSEL.SeninEnabled == 0)
            {
                button船員管理.Enabled = false;
                button船員手当.Enabled = false;
                button船内準備金.Enabled = false;

                button船員管理.BackColor = SystemColors.Control;
                button船員手当.BackColor = SystemColors.Control;
                button船内準備金.BackColor = SystemColors.Control;
            }
            else
            {
                button船員管理.Enabled = true;
                button船員手当.Enabled = true;
                button船内準備金.Enabled = true;
            }
            //------------------------------------------------------------

            if (同期Client.LOGIN_VESSEL.DocumentEnabled == 0)
            {
                button文書管理.Enabled = false;
                button文書管理.BackColor = SystemColors.Control;
            }
            else
            {
                button文書管理.Enabled = true;
            }

            if (同期Client.LOGIN_VESSEL.KanidouseiEnabled == 0)
            {
                button動静報告.Enabled = false;
                button動静報告.BackColor = SystemColors.Control;
            }
            else
            {
                button動静報告.Enabled = true;
            }



            ////==============================
            //// 契約によるボタンの制御
            ////==============================

            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.船員) == false)
            {
                button船員管理.Visible = false;
                button船員手当.Visible = false;
                button船内準備金.Visible = false;

                checkBox_Senin.Visible = false;
                checkBox_Jimu_Senin.Visible = false;
            }
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "表示", "給与手当申請"))
            {
                button船員手当.Enabled = false;
            }
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "表示", "船用金管理"))
            {
                button船内準備金.Enabled = false;
            }

            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.文書) == false)
            {
                button文書管理.Visible = false;

                checkBox_Document.Visible = false;
                checkBox_Jimu_Bunsho.Visible = false;
            }

            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.動静) == false)
            {
                button動静報告.Visible = false;

                tabControl1.TabPages.RemoveByKey("tabPage2");
            }

            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.発注) == false)
            {
                button手配依頼.Visible = false;
                button貯蔵品編集.Visible = false;

                checkBox_Hachu.Visible = false;
                checkBox_Jimu_Hachu.Visible = false;

                tabControl1.TabPages.RemoveByKey("tabPage1");
            }




            bool loginByCrew = false;
            if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["VesselLoginByCrew"]) == false)
            {
                var val = System.Configuration.ConfigurationManager.AppSettings["VesselLoginByCrew"];
                loginByCrew = (val.ToUpper() == "TRUE") ? true : false;
            }

            if (loginByCrew && NBaseCommon.Common.siCard == null)
            {
                button勤怠管理.Enabled = false;
                button勤怠管理.BackColor = SystemColors.ControlDark;

                button出勤登録.Enabled = false;
                button出勤登録.BackColor = SystemColors.ControlDark;
            }






            // 給与手当ボタンは非表示
            button船員手当.Visible = false;

            // 出勤登録ボタンは非表示
            button出勤登録.Visible = false;

        }
        #endregion


        #region private void InitializeTable()
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
            BuildModel();
            UpdateTable();
        }
        #endregion

        #region internal void UpdateTable()
        internal void UpdateTable()
        {
            this.treeListView1.SuspendUpdate();

            this.treeListView1.AfterCollapse -= new LidorSystems.IntegralUI.ObjectEventHandler(this.treeListView1_AfterCollapse);
            this.treeListView1.AfterExpand -= new LidorSystems.IntegralUI.ObjectEventHandler(this.treeListView1_AfterExpand);

            treeListView1.Nodes.Clear();

            foreach (OdJry j in odJrys)
            {
                TreeListViewNode jryNode = OdJryTreeListViewHelper.CreateOdJryNode(treeListView1, treeListViewDelegate, j);
                nodeModelDic.Add(jryNode, j);

                UpdateTree(j, jryNode);

                if (expandNodes.Contains(j.OdJryID))
                {
                    jryNode.Expand();
                }
            }

            toolStripStatusLabel1.Text = "";

            if (同期Client.SYNC_DATE == DateTime.MinValue)
                label同期時刻.Text = "最新同期：----/--/-- --:--";
            else
                label同期時刻.Text = "最新同期：" + 同期Client.SYNC_DATE.ToString("yyyy/MM/dd HH:mm");

            this.treeListView1.AfterCollapse += new LidorSystems.IntegralUI.ObjectEventHandler(this.treeListView1_AfterCollapse);
            this.treeListView1.AfterExpand += new LidorSystems.IntegralUI.ObjectEventHandler(this.treeListView1_AfterExpand);

            //ノードの変更をしたらViewを更新 2021/09/08 m.yoshihara
            this.treeListView1.UpdateCurrentView();

            this.treeListView1.ResumeUpdate();

        }
        #endregion

        #region internal void UpdateTree(OdJry j, TreeListViewNode jryNode)
        internal void UpdateTree(OdJry j, TreeListViewNode jryNode)
        {
            if (is燃料_潤滑油(j))
            {
                UpdateTree_燃料_潤滑油(j, jryNode);
            }
            else
            {
                UpdateTree_燃料_潤滑油以外(j, jryNode);
            }
        }
        #endregion

        #region private void UpdateTree_燃料_潤滑油(OdJry j, TreeListViewNode thiNode)
        private void UpdateTree_燃料_潤滑油(OdJry j, TreeListViewNode thiNode)
        {
            int i = 0;
            foreach (OdJryItem ji in j.OdJryItems)
            {
                if (ji.CancelFlag == 1)
                {
                    continue;
                }

                TreeListViewNode jryItemNode = OdJryTreeListViewHelper.CreateOdJryItemNode(thiNode,
                                                                                  treeListViewDelegate,
                                                                                  ji);
                // 2013.02 2013年度改造
                //nodeModelDic.Add(jryItemNode, ji);

                int siCount = 0;
                foreach (OdJryShousaiItem si in ji.OdJryShousaiItems)
                {
                    // 2013.02 2013年度改造
                    //if (si.CancelFlag == 1)
                    if (si.CancelFlag == 1 || si.Count < 1)
                    {
                        continue;
                    }

                    TreeListViewNode jryShousaiItemNode = OdJryTreeListViewHelper.CreateOdJryShousaiItemNode(jryItemNode,
                                                                  treeListViewDelegate,
                                                                  si, ++i);
                    nodeModelDic.Add(jryShousaiItemNode, si);

                    // 2013.02 2013年度改造
                    siCount++;
                }


                // 2013.02 2013年度改造
                //if (expandNodes.Contains(ji.OdJryItemID))
                //{
                //    jryItemNode.Expand();
                //}
                if (siCount > 0)
                {
                    nodeModelDic.Add(jryItemNode, ji);

                    if (expandNodes.Contains(ji.OdJryItemID))
                    {
                        jryItemNode.Expand();
                    }
                }
            }
        }
        #endregion

        #region private void UpdateTree_燃料_潤滑油以外(OdJry j, TreeListViewNode jryNode)
        private void UpdateTree_燃料_潤滑油以外(OdJry j, TreeListViewNode jryNode)
        {
            List<ItemHeader<OdJryItem>> jryItemHeaders = OdJryTreeListViewHelper.GroupByJryItemHeader(j.OdJryItems);

            int i = 0;
            foreach (ItemHeader<OdJryItem> h in jryItemHeaders)
            {
                TreeListViewNode jryItemHeaderNode = OdJryTreeListViewHelper.CreateJryItemHeaderNode(jryNode,
                                                                                  treeListViewDelegate,
                                                                                  h);
                nodeModelDic.Add(jryItemHeaderNode, h);

                foreach (OdJryItem ji in h.Items)
                {
                    TreeListViewNode jryItemNode = OdJryTreeListViewHelper.CreateOdJryItemNode(jryItemHeaderNode, treeListViewDelegate, ji);
                    nodeModelDic.Add(jryItemNode, ji);

                    foreach (OdJryShousaiItem si in ji.OdJryShousaiItems)
                    {
                        TreeListViewNode jryShousaiItemNode = OdJryTreeListViewHelper.CreateOdJryShousaiItemNode(jryItemNode, treeListViewDelegate, si, ++i);
                        nodeModelDic.Add(jryShousaiItemNode, si);
                    }

                    if (expandNodes.Contains(ji.OdJryItemID))
                    {
                        jryItemNode.Expand();
                    }
                }

                if (expandNodes.Contains(h.Id.ToString()))
                {
                    jryItemHeaderNode.Expand();
                }
            }
        }
        #endregion

        #region private bool is燃料_潤滑油(OdJry odJry)
        private bool is燃料_潤滑油(OdJry odJry)
        {
            return odJry.MsThiIraiSbtID == MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.燃料_潤滑油);
        }
        #endregion

        #region internal void BuildModel()
        internal void BuildModel()
        {
            List<OdJry> tmp = OdJry.GetRecordsByVesselId(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);
            odJrys.Clear();

            List<string> jriIds =
                (from r in tmp
                 select r.OdJryID).ToList<string>();

            Dictionary<string, List<OdJryItem>> jryItemDic = CreateOdJryItemDic(jriIds);
            Dictionary<string, List<OdJryShousaiItem>> jryShousaiItemDic = CreateOdJryShousaiItemDic(jriIds);

            foreach (OdJry t in tmp)
            {
                if (jryItemDic.ContainsKey(t.OdJryID) == false || jryShousaiItemDic.ContainsKey(t.OdJryID) == false)
                    continue;

                List<OdJryItem> jryItems = jryItemDic[t.OdJryID];
                List<OdJryShousaiItem> jryShousaiItems = jryShousaiItemDic[t.OdJryID];

                foreach (OdJryItem ti in jryItems)
                {
                    foreach (OdJryShousaiItem si in jryShousaiItems)
                    {
                        if (si.OdJryItemID == ti.OdJryItemID)
                        {
                            ti.OdJryShousaiItems.Add(si);
                        }
                    }

                    int overTimeCount = 0;
                    int nullCount = 0;
                    foreach (OdJryShousaiItem si in ti.OdJryShousaiItems)
                    {
                        jryShousaiItems.Remove(si);

                        if (si.Nouhinbi == DateTime.MinValue)
                        {
                            nullCount++;
                        }
                        // 2012年度改造：24時間の猶予はなくし、受領済みなら非表示にするカウントをとる
                        //if (si.Nouhinbi != DateTime.MinValue && (DateTime.Now - si.Nouhinbi).TotalHours > 24)
                        if (si.Nouhinbi != DateTime.MinValue)
                        {
                            overTimeCount++;
                        }
                    }

                    //全ての詳細品目の納品日が 24 時間以前のときは非表示のためリストに追加しない。
                    if (ti.OdJryShousaiItems.Count == overTimeCount)
                    {
                        ti.OdJryShousaiItems.Clear();
                    }
                    else if (t.Status != (int)OdJry.STATUS.未受領 && ti.OdJryShousaiItems.Count == (overTimeCount + nullCount))
                    {
                        ti.OdJryShousaiItems.Clear();
                    }

                    if (ti.OdJryShousaiItems.Count > 0)
                    {
                        t.OdJryItems.Add(ti);
                    }
                }

                if (t.OdJryItems.Count > 0)
                {
                    odJrys.Add(t);
                }
            }
        }
        #endregion

        private Dictionary<string, List<OdJryItem>> CreateOdJryItemDic(List<string> jriIds)
        {
            Dictionary<string, List<OdJryItem>> result = new Dictionary<string, List<OdJryItem>>();

            List<OdJryItem> jryItems = OdJryItem.GetRecordsByOdJryIDs(同期Client.LOGIN_USER, jriIds.ToList<string>());

            foreach (OdJryItem item in jryItems)
            {
                if (!result.ContainsKey(item.OdJryID))
                {
                    result[item.OdJryID] = new List<OdJryItem>();
                }

                result[item.OdJryID].Add(item);
            }

            return result;
        }


        private Dictionary<string, List<OdJryShousaiItem>> CreateOdJryShousaiItemDic(List<string> jriIds)
        {
            Dictionary<string, List<OdJryShousaiItem>> result = new Dictionary<string, List<OdJryShousaiItem>>();

            List<OdJryShousaiItem> jryItems = OdJryShousaiItem.GetRecordsByOdJryIDs(同期Client.LOGIN_USER, jriIds.ToList<string>());

            foreach (OdJryShousaiItem item in jryItems)
            {
                if (!result.ContainsKey(item.OdJryID))
                {
                    result[item.OdJryID] = new List<OdJryShousaiItem>();
                }

                result[item.OdJryID].Add(item);
            }

            return result;
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
                if (簡易動静情報の更新() == true)
                {
                    this.Invoke((MethodInvoker)delegate()
                    {
                        kaniDouseiControl1.Visible = false;
                        KanidouseiRefresh();
                    });
                }
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

        private void treeListView1_AfterExpand(object sender, LidorSystems.IntegralUI.ObjectEventArgs e)
        {
            TreeListViewNode node = e.Object as TreeListViewNode;

            if (node != null && nodeModelDic.ContainsKey(node))
            {
                if (nodeModelDic[node] is OdJry)
                {
                    expandNodes.Add(((OdJry)nodeModelDic[node]).OdJryID);
                }
                else if (nodeModelDic[node] is ItemHeader<OdJryItem>)
                {
                    expandNodes.Add(((ItemHeader<OdJryItem>)nodeModelDic[node]).Id.ToString());
                }
                else if (nodeModelDic[node] is OdJryItem)
                {
                    expandNodes.Add(((OdJryItem)nodeModelDic[node]).OdJryItemID);
                }
            }
        }

        private void treeListView1_AfterCollapse(object sender, LidorSystems.IntegralUI.ObjectEventArgs e)
        {
            TreeListViewNode node = e.Object as TreeListViewNode;

            if (node != null && nodeModelDic.ContainsKey(node))
            {
                if (nodeModelDic[node] is OdJry)
                {
                    expandNodes.Remove(((OdJry)nodeModelDic[node]).OdJryID);
                }
                else if (nodeModelDic[node] is ItemHeader<OdJryItem>)
                {
                    expandNodes.Remove(((ItemHeader<OdJryItem>)nodeModelDic[node]).Id.ToString());
                }
                else if (nodeModelDic[node] is OdJryItem)
                {
                    expandNodes.Remove(((OdJryItem)nodeModelDic[node]).OdJryItemID);
                }
            }
        }

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
                        if (nodeModelDic[node] is OdJry)
                        {
                            OdJry jry = (OdJry)nodeModelDic[node];

                            一括受領Form form = new 一括受領Form(this, jry);
                            form.ShowDialog();
                        }
                        else if (nodeModelDic[node] is OdJryShousaiItem)
                        {
                            OdJryShousaiItem jryShousaiItem = (OdJryShousaiItem)nodeModelDic[node];
                            OdJry jry;

                            // 燃料_潤滑油
                            if (nodeModelDic[node.Parent.Parent] is OdJry)
                            {
                                jry = (OdJry)nodeModelDic[node.Parent.Parent];
                            }
                            // 燃料_潤滑油以外
                            else
                            {
                                jry = (OdJry)nodeModelDic[node.Parent.Parent.Parent];
                            }

                            納品Form form = new 納品Form(this, jry, jryShousaiItem);
                            form.ShowDialog();
                        }
                    }
                }
            }
        }



        private void tehaiButt_Click(object sender, EventArgs e)
        {
            手配依頼一覧Form tehaiForm = new 手配依頼一覧Form();
            tehaiForm.ShowDialog();
        }

        private void exitButt_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        private void butt貯蔵品_Click(object sender, EventArgs e)
        {
            Chozo.貯蔵品Form form = new NBaseHonsen.Chozo.貯蔵品Form();
            form.ShowDialog();
        }
        private void button船員管理_Click(object sender, EventArgs e)
        {
            船員メニュー form = new 船員メニュー();
            form.ShowDialog();
        }

        private void button船内準備金_Click(object sender, EventArgs e)
        {
            船内収支Form form = new 船内収支Form(this);
            form.ShowDialog();
        }
        private void button手当一覧_Click(object sender, EventArgs e)
        {
            tek手当一覧Form form = tek手当一覧Form.Instance();
            form.ShowDialog();
        }

        private void button給与手当_Click(object sender, EventArgs e)
        {
            給与手当申請Form form = 給与手当申請Form.Instance();
            form.ShowDialog();
        }

        private void button同期詳細_Click(object sender, EventArgs e)
        {
            同期詳細情報Form form = new 同期詳細情報Form();
            form.ShowDialog();
        }

        private void button文書管理_Click(object sender, EventArgs e)
        {
            Document.文書メニュー form = new NBaseHonsen.Document.文書メニュー();
            form.parentForm = this; // 2012.02 : Add
            form.ShowDialog();
            //this.WindowState = FormWindowState.Normal; // 2012.02 : Add
        }

        private void button動静報告_Click(object sender, EventArgs e)
        {
            動静報告一覧Form form = new 動静報告一覧Form();
            form.ShowDialog();
        }

        #region 簡易動静

        public class PortName
        {
            public PtKanidouseiInfo PtKanidouseiInfo;
            public MsVessel msVessel;
            public bool IsJiseki { get; set; }
            public DateTime colDate;

            public PortName(PtKanidouseiInfo ptKanidouseiInfo, bool isjiseki, DateTime date, MsVessel vessel)
            {
                PtKanidouseiInfo = ptKanidouseiInfo;
                IsJiseki = isjiseki;
                colDate = date;
                msVessel = vessel;
            }
        }

        public class VesselData
        {
            public string 船名 { get; set; }
            public string 船長 { get; set; }
            public string 電話 { get; set; }
            public string 携帯 { get; set; }
        }



        private void KanidouseiRefresh()
        {
            this.Invoke((MethodInvoker)delegate()
            {
                描画中メッセージ_label.Refresh();
            });
            #region 動静情報の作成

            //kaniDouseiControl1.msVessel_list = MsVessel.GetRecordsByKanidouseiEnabled(NBaseCommon.Common.LoginUser);
            if (kaniDouseiControl1.msVessel_list == null)
            {
                //kaniDouseiControl1.msVessel_list = MsVessel.GetRecordsByKanidouseiEnabled(同期Client.LOGIN_USER);
                kaniDouseiControl1.msVessel_list = MsVessel.GetRecordsByKanidouseiEnabled(同期Client.LOGIN_USER).Where(obj => obj.KanidouseiEnabled == 1).ToList();
            }
            kaniDouseiControl1.Init(ColStartDate);
            簡易動静情報の更新();
            kaniDouseiControl1.KandidouseiRefresh(ptKanidouseiInfo_list);

            #endregion

            Thread PaintThread = new Thread(
            delegate()
            {
                this.Invoke((MethodInvoker)delegate()
                {
                    kaniDouseiControl1.Visible = true;

                    label_表示時刻.Text = "  " + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + " 時点のデータを表示";
                });
            });

            PaintThread.Start();
        }


        private bool 簡易動静情報の更新()
        {
            // 船の表示期間のレコードを一括で取得する
            DateTime fromDatetime = new DateTime(ColStartDate.Year, ColStartDate.Month, ColStartDate.Day);
            DateTime toDatetime = fromDatetime.AddDays(11);
            toDatetime = toDatetime.AddSeconds(-1);

            ptKanidouseiInfo_list = PtKanidouseiInfo.GetRecordByEventDate(同期Client.LOGIN_USER, fromDatetime, toDatetime);

            int hash = GetKanidouseiDataHashCode(ptKanidouseiInfo_list);
            if (hash != KanidouseiDataHash)
            {
                KanidouseiDataHash = hash;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 簡易動静情報のハッシュコードを取得
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        private int GetKanidouseiDataHashCode(List<PtKanidouseiInfo> datas)
        {
            StringBuilder sb = new StringBuilder();

            foreach (PtKanidouseiInfo pk in datas)
            {
                int hash = pk.GetHashCode();
                sb.Append(hash.ToString());
            }

            return sb.ToString().GetHashCode();
        }

        /// <summary>
        /// 簡易動静編集時のイベント
        /// </summary>
        /// <param name="PtKanidouseiInfo"></param>
        #region private void 簡易動静_AfterEdit()
        private void 簡易動静_AfterEdit()
        {
            // 船の表示期間のレコードを一括で取得する
            DateTime fromDatetime = new DateTime(ColStartDate.Year, ColStartDate.Month, ColStartDate.Day);
            DateTime toDatetime = fromDatetime.AddDays(11);
            toDatetime = toDatetime.AddSeconds(-1);

            //ptKanidouseiInfo_list = PtKanidouseiInfo.GetRecordsByEventDateVessel(NBaseCommon.Common.LoginUser, 同期Client.LOGIN_VESSEL.MsVesselID, fromDatetime.AddSeconds(-1), toDatetime);
            ptKanidouseiInfo_list = PtKanidouseiInfo.GetRecordByEventDate(NBaseCommon.Common.LoginUser, fromDatetime.AddSeconds(-1), toDatetime, 同期Client.LOGIN_VESSEL.MsVesselID);

            kaniDouseiControl1.KandidouseiRefresh(同期Client.LOGIN_VESSEL.MsVesselID, ptKanidouseiInfo_list);

            Thread PaintThread = new Thread(
            delegate()
            {
                this.Invoke((MethodInvoker)delegate()
                {
                    kaniDouseiControl1.Visible = true;
                });
            });

            PaintThread.Start();
        }
        #endregion

        /// <summary>
        /// 【<<】ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Prev_Click(object sender, EventArgs e)
        {
            if (kaniDouseiControl1.Visible == false)
            {
                return;
            }

            // －１日する
            ColStartDate = ColStartDate.AddDays(-1);
            dateTimePicker_動静表.Value = ColStartDate;

            kaniDouseiControl1.Visible = false;
            KanidouseiRefresh();

        }

        /// <summary>
        /// 【>>】ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Next_Click(object sender, EventArgs e)
        {
            if (kaniDouseiControl1.Visible == false)
            {
                return;
            }

            // ＋１日する
            ColStartDate = ColStartDate.AddDays(1);
            dateTimePicker_動静表.Value = ColStartDate;

            kaniDouseiControl1.Visible = false;
            KanidouseiRefresh();
        }


        #region 簡易動静表出力
        string BaseFileName = "動静表";
        private void OutputFile(DateTime today)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = BaseFileName + " (*.xlsx)|*.xlsx";
            fd.FileName = BaseFileName + ".xlsx";

            if (fd.ShowDialog() == DialogResult.OK)
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                path = path + "/Template/";
                string templateName = path + "Template_" + BaseFileName + ".xlsx";
                string outPutFileName = fd.FileName;

                try
                {
                    Cursor = Cursors.WaitCursor;

                    NBaseData.BLC.簡易動静表.動静表_取得(NBaseCommon.Common.LoginUser, DateTime.Today, path, templateName, outPutFileName);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }

                MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #endregion

        #region アラーム情報

        private void InitializeAlarmInfoTable()
        {
            MakeAlarmView();
        }

        internal void MakeAlarmView()
        {
            //------------------------------------------------------------------
            // 検索条件作成
            //------------------------------------------------------------------
            PtAlarmInfoCondition condition = new PtAlarmInfoCondition();

            // 発生日
            condition.HasseiDate = DateTime.Today;
            condition.HasseiDate = condition.HasseiDate.AddDays(1);
            condition.HasseiDate = condition.HasseiDate.AddSeconds(-1);

            // 種別
            if (checkBox_Senin.Checked == true)
            {
                condition.Shubetu_list.Add(MsPortalInfoShubetu.船員);
            }
            if (checkBox_Hachu.Checked == true)
            {
                condition.Shubetu_list.Add(MsPortalInfoShubetu.発注);
            }
            if (checkBox_Document.Checked == true)
            {
                condition.Shubetu_list.Add(MsPortalInfoShubetu.文書);
            }

            // 船
            condition.Vessel_list.Add(同期Client.LOGIN_VESSEL.MsVesselID);
            condition.Vessel_list.Add(0);

            //----------------------------------------------------------------------
            // 検索
            //----------------------------------------------------------------------
            List<PtAlarmInfo> PtAlarmInfo_List = new List<PtAlarmInfo>();
            if (condition.Shubetu_list.Count > 0)
            {
                PtAlarmInfo_List = PtAlarmInfo.GetRecordByCondition(NBaseCommon.Common.LoginUser, condition);
            }

            List<MsSenin> senins_乗船中 = null;

            if (checkBox_Senin.Checked)
            {
                senins_乗船中 = MsSenin.GetRecordsByFilter(同期Client.LOGIN_USER, CreateMsSeninFilter());
            }

            //-----------------------------------------------------------------------
            // アラーム情報表示
            //-----------------------------------------------------------------------
            treeListView3.SuspendUpdate();
            treeListView3.Nodes.Clear();
            foreach (PtAlarmInfo ptAlarmInfo in PtAlarmInfo_List)
            {
                if (int.Parse(ptAlarmInfo.MsPortalInfoShubetuId) == (int)MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.船員 &&
                    int.Parse(ptAlarmInfo.MsPortalInfoKoumokuId) == (int)MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.免許免状)
                {
                    if (!Is_乗船中の船員の免状のアラーム(ptAlarmInfo, senins_乗船中))
                    {
                        continue;
                    }
                }
                if (int.Parse(ptAlarmInfo.MsPortalInfoShubetuId) == (int)MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.文書)
                {
                    if (ptAlarmInfo.MsVesselId != 同期Client.LOGIN_VESSEL.MsVesselID)
                    {
                        continue;
                    }
                }

                //------------------------------------------------------
                // ノードを作成
                //------------------------------------------------------
                TreeListViewNode node = new TreeListViewNode();

                // 発生日
                if (ptAlarmInfo.HasseiDate == DateTime.MinValue)
                {
                    node.SubItems.Add(AddSubItem(""));
                }
                else
                {
                    node.SubItems.Add(AddSubItem(ptAlarmInfo.HasseiDate.ToString("yyyy/MM/dd")));
                }
                // 有効期限
                if (ptAlarmInfo.Yuukoukigen == DateTime.MinValue)
                {
                    node.SubItems.Add(AddSubItem(""));
                }
                else
                {
                    node.SubItems.Add(AddSubItem(ptAlarmInfo.Yuukoukigen.ToString("yyyy/MM/dd")));
                }
                // 種別
                node.SubItems.Add(AddSubItem(ptAlarmInfo.PortalInfoSyubetuName));
                // 内容
                node.SubItems.Add(AddSubItem(ptAlarmInfo.Naiyou));
                // 詳細
                node.SubItems.Add(AddSubItem(ptAlarmInfo.Shousai));

                //------------------------------------------------------
                // ノードを追加
                //------------------------------------------------------
                treeListView3.Nodes.Add(node);


            }
            treeListView3.ResumeUpdate();
        }


        private bool Is_乗船中の船員の免状のアラーム(PtAlarmInfo ptAlarmInfo, List<MsSenin> senins_乗船中)
        {
            if (ptAlarmInfo.SanshoumotoId != null)
            {
                SiMenjou m = SiMenjou.GetRecord(NBaseCommon.Common.LoginUser, ptAlarmInfo.SanshoumotoId);

                if (m != null)
                {
                    foreach (MsSenin s in senins_乗船中)
                    {
                        if (s.MsSeninID == m.MsSeninID)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }


        private MsSeninFilter CreateMsSeninFilter()
        {
            MsSeninFilter filter = new MsSeninFilter();

            filter.MsVesselID = 同期Client.LOGIN_VESSEL.MsVesselID;
            filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船));
            filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船休暇));
            filter.joinSiCard = MsSeninFilter.JoinSiCard.INNER_JOIN;

            return filter;
        }


        /// <summary>
        /// SubItemを追加する
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private TreeListViewSubItem AddSubItem(string text)
        {
            TreeListViewSubItem subItem = new TreeListViewSubItem();
            subItem.Text = text;

            return subItem;
        }

        private void checkBox_KensaShousho_CheckedChanged(object sender, EventArgs e)
        {
            //MakeAlarmView();
        }

        private void checkBox_Senin_CheckedChanged(object sender, EventArgs e)
        {
            MakeAlarmView();
        }

        private void checkBox_Hachu_CheckedChanged(object sender, EventArgs e)
        {
            MakeAlarmView();
        }

        // 2011.04.13:aki
        private void checkBox_Document_CheckedChanged(object sender, EventArgs e)
        {
            MakeAlarmView();
        }

        #endregion

        #region 事務所更新情報

        private void button検索_Click(object sender, EventArgs e)
        {
            MakeJimushoKoushinView();
        }

        private void InitializeJimushoKoushinInfoTable()
        {
            MakeJimushoKoushinView();
        }

        internal void MakeJimushoKoushinView()
        {
            //------------------------------------------------------------------
            // 検索条件作成
            //------------------------------------------------------------------
            PtJimushokoushinInfoCondition condition = new PtJimushokoushinInfoCondition();

            // 日付From
            condition.EventDate_From = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, 00, 00, 00);

            // 日付To
            condition.EventDate_To = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day, 23, 59, 59);

            // 種別
            if (checkBox_Jimu_Senin.Checked == true)
            {
                condition.Shubetu_list.Add(MsPortalInfoShubetu.船員);
            }
            if (checkBox_Jimu_Hachu.Checked == true)
            {
                condition.Shubetu_list.Add(MsPortalInfoShubetu.発注);
            }
            if (checkBox_Jimu_Bunsho.Checked == true)
            {
                condition.Shubetu_list.Add(MsPortalInfoShubetu.文書);
            }

            // 船
            condition.Vessel_list.Add(同期Client.LOGIN_VESSEL.MsVesselID);
            condition.Vessel_list.Add(0);

            //----------------------------------------------------------------------
            // 検索
            //----------------------------------------------------------------------
            List<PtJimushokoushinInfo> PtJimuKoushinInfo_List = new List<PtJimushokoushinInfo>();
            if (condition.Shubetu_list.Count > 0)
            {
                PtJimuKoushinInfo_List = PtJimushokoushinInfo.GetRecordsByCondition(同期Client.LOGIN_USER, condition);
            }

            //-----------------------------------------------------------------------
            // 事務所更新情報表示
            //-----------------------------------------------------------------------
            treeListView2.SuspendUpdate();
            treeListView2.Nodes.Clear();
            foreach (PtJimushokoushinInfo ptJimuKoushinInfo in PtJimuKoushinInfo_List)
            {
                //------------------------------------------------------
                // ノードを作成
                //------------------------------------------------------
                TreeListViewNode node = new TreeListViewNode();

                // 日付
                if (ptJimuKoushinInfo.EventDate == DateTime.MinValue)
                {
                    node.SubItems.Add(AddSubItem(""));
                }
                else
                {
                    node.SubItems.Add(AddSubItem(ptJimuKoushinInfo.EventDate.ToString("yyyy/MM/dd")));
                }
                // 種別
                node.SubItems.Add(AddSubItem(ptJimuKoushinInfo.PortalInfoSyubetuName));
                // 内容
                node.SubItems.Add(AddSubItem(ptJimuKoushinInfo.Naiyou));
                // 更新内容
                node.SubItems.Add(AddSubItem(ptJimuKoushinInfo.KoushinNaiyou));
                // 更新者
                node.SubItems.Add(AddSubItem(ptJimuKoushinInfo.HonsenkoushinInfoUserName));

                //------------------------------------------------------
                // ノードを追加
                //------------------------------------------------------
                treeListView2.Nodes.Add(node);


            }
            treeListView2.ResumeUpdate();
        }
        #endregion



        private void button1_Click(object sender, EventArgs e)
        {
            if (kaniDouseiControl1.Visible == false)
            {
                return;
            }

            OutputFile(ColStartDate);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text =="動静情報" && !簡易動静タブ選択)
            {
                Thread PaintThread = new Thread(
                delegate()
                {
                    簡易動静情報の更新();
                    Thread.Sleep(1000);
                    this.Invoke((MethodInvoker)delegate()
                    {
                        KanidouseiRefresh();
                    });
                });

                PaintThread.Start();

                簡易動静タブ選択 = true;
            }
        }

        private void Refresh_button_Click(object sender, EventArgs e)
        {
            ColStartDate = dateTimePicker_動静表.Value;

            if (kaniDouseiControl1.Visible == false)
            {
                return;
            }

            kaniDouseiControl1.Visible = false;
            KanidouseiRefresh();
        }


        private void button出勤登録_Click(object sender, EventArgs e)
        {
            //if (NBaseCommon.Common.siCard == null)
            //    return;

            if (WtmSyncClient.SYNC_STATUS != WtmSyncClient.SyncStatus.AFTER_FIRST_SYNC)
            {
                MessageBox.Show("勤怠データ同期中です（初回データ取得）");
                return;
            }

            //Vessel
            NBaseCommon.Common.VesselList = MsVessel.GetRecordsBySeninEnabled(WTM.Common.LoginUser);

            // Shokumei
            NBaseCommon.Common.ShokumeiList = MsSiShokumei.GetRecords(WTM.Common.LoginUser).OrderBy(o => o.ShowOrder).ToList();

            // Senin
            NBaseCommon.Common.SeninList = MsSenin.GetRecords(WTM.Common.LoginUser);

            //ランクカテゴリ
            WtmCommon.RankCategoryList = WtmAccessor.Instance().GetRankCategories();

            //権限
            WtmCommon.RoleList = WtmAccessor.Instance().GetRoles();

            //ワークコンテンツ
            WtmCommon.WorkContentList = WtmAccessor.Instance().GetWorkContents();

            // 設定
            WtmCommon.SetSetting(WtmAccessor.Instance().GetSetting());



            出退勤登録Form frm = new 出退勤登録Form();
            frm.Vessel = WTM.Common.Vessel;
            frm.ShowDialog();
        }

        private void button勤怠管理_Click(object sender, EventArgs e)
        {
            //if (NBaseCommon.Common.siCard == null)
            //    return;

            if (WtmSyncClient.SYNC_STATUS != WtmSyncClient.SyncStatus.AFTER_FIRST_SYNC)
            {
                MessageBox.Show("勤怠データ同期中です（初回データ取得）");
                return;
            }

            //Vessel
            NBaseCommon.Common.VesselList = MsVessel.GetRecordsBySeninEnabled(WTM.Common.LoginUser);

            // Shokumei
            NBaseCommon.Common.ShokumeiList = MsSiShokumei.GetRecords(WTM.Common.LoginUser).OrderBy(o => o.ShowOrder).ToList();

            // Senin
            NBaseCommon.Common.SeninList = MsSenin.GetRecords(WTM.Common.LoginUser);

            //ランクカテゴリ
            WtmCommon.RankCategoryList = WtmAccessor.Instance().GetRankCategories();

            //権限
            WtmCommon.RoleList = WtmAccessor.Instance().GetRoles();

            //ワークコンテンツ
            WtmCommon.WorkContentList = WtmAccessor.Instance().GetWorkContents();

            // 設定
            WtmCommon.SetSetting(WtmAccessor.Instance().GetSetting());


            //日表示Form frm = new 日表示Form();
            //frm.ShowDialog(); 
            WtmFormController.Show_日表示Form();
        }



        public void AbortForm()
        {
            try
            {
                Invoke(new MethodInvoker(
                   delegate ()
                   {
                       this.Close();
                   }
               ));
            }
            catch (InvalidOperationException e)
            {
            }
        }


    }
}
