//#define 船用品


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using LidorSystems.IntegralUI.Lists.Collections;
using NBaseData.DAC;
using SyncClient;
using NBaseUtil;
using NBaseData.DS;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseCommon.Nyukyo;
using NBaseHonsen.util;
using ORMapping;
using NBaseHonsen.Senin.util;
using NBaseCommon.Senyouhin;
using NBaseCommon.Hachu.Excel;

namespace NBaseHonsen
{
    public partial class 手配依頼Form : Form
    {
        public enum Status { 未保存_0, 船未手配_1, 手配依頼済かつ未同期_2, 手配依頼済かつ同期済_3 }

        private TreeListViewDelegate treeListViewDelegate;

        private readonly 手配依頼一覧Form tehaiIraiIchiranForm;
        private string msThiIraiSbtId;
        private string msThiIraiShousaiId;
        private int  vesselItemCategoryNumber = -1;
        private readonly OdThi odThi;

        private Dictionary<TreeListViewNode, OdThiItem> odThiItemNodes = new Dictionary<TreeListViewNode, OdThiItem>();

        private readonly IFormDelegate formDelegate;

        private bool saved;

        public List<OdAttachFile> 添付Files = null;


        public 手配依頼Form(手配依頼一覧Form tehaiIraiIchiranForm,
                            string msThiIraiSbtId,
                            string msThiIraiShousaiId,
                            int categoryNumber)
            : this(tehaiIraiIchiranForm, msThiIraiSbtId, msThiIraiShousaiId, categoryNumber, new OdThi())
        {
        }

        public 手配依頼Form(手配依頼一覧Form tehaiIraiIchiranForm, string msThiIraiSbtId, string msThiIraiShousaiId, int categoryNumber = -1, OdThi odThi = null)
        {
            同期Client.SYNC_SUSPEND = true;

            this.tehaiIraiIchiranForm = tehaiIraiIchiranForm;
            this.msThiIraiSbtId = msThiIraiSbtId;
            this.msThiIraiShousaiId = msThiIraiShousaiId;
            this.vesselItemCategoryNumber = categoryNumber;
            this.odThi = odThi.Clone();
            this.添付Files = new List<OdAttachFile>();

            InitializeComponent();
            Text = NBaseCommon.Common.WindowTitle("HN020102/HN020103", "手配依頼", WcfServiceWrapper.ConnectedServerID);

            if (is燃料_潤滑油())
            {
                formDelegate = new FormDelegate_燃料_潤滑油(this);
            }
            else
            {
                formDelegate = new FormDelegate_燃料_潤滑油以外(this);
            }
        }

        private void 手配依頼Form_Load(object sender, EventArgs e)
        {
            textBox手配内容.Text = odThi.Naiyou;
            textBox備考.Text = odThi.Bikou;
            GetAttachFiles();

            InitBikou();

            InitializeComponentsEnabled();
            InitializeTable();

            textBox手配内容.Focus();
        }

        private void InitBikou()
        {
            textBox備考.MaxLength = 500;
            textBox備考.Width = 673;
            textBox備考.Height = 141;

            OdThiFilter filter = new OdThiFilter();
            filter.MsVesselID = 同期Client.LOGIN_VESSEL.MsVesselID;
            List<OdThi> tmp = OdThi.GetRecordsByFilter(同期Client.LOGIN_USER, filter);

            foreach (OdThi t in tmp)
            {
                if (t.Bikou != null && t.Bikou.Length > 0)
                    textBox備考.AutoCompleteCustomSource.Add(t.Bikou);
            }

            textBox備考.ReadOnly = true;
        }

        private void InitializeComponentsEnabled()
        {
            // 修繕-入渠
            if (is修繕入渠())
            {
                button読込.Visible = true;
                button出力.Visible = true;
                // 2012.02 -->
                button読込.Text = "ﾄﾞｯｸｵｰﾀﾞｰ読込";
                button出力.Text = "ﾄﾞｯｸｵｰﾀﾞｰ出力";
                button読込.Width = 153;
                button出力.Width = 153;
                button出力.Location = new Point(330, 226);
                // <-- 2012.02
            }
#if 船用品
            // 2012.02 -->
            else if (is船用品())
            {
                button読込.Visible = true;
                button出力.Visible = true;
                button読込.Text = "船用品注文書読込";
                button出力.Text = "船用品注文書出力";
                button読込.Width = 185;
                button出力.Width = 185;
                button出力.Location = new Point(362, 226);

                // 2016.03
                if (vesselItemCategoryNumber != MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    buttonリスト入力.Location = button品目追加.Location;
                    buttonリスト入力.Visible = true;
                    button品目追加.Visible = false;
                }
            }
            // <-- 2012.02
#endif
            switch (GetStatus())
            {
                case Status.未保存_0:

                    textBox手配内容.ReadOnly = false;
                    textBox備考.ReadOnly = false;
                    button品目追加.Enabled = true;
                    button読込.Enabled = true;
                    if (is船用品())
                    {
                        button出力.Enabled = false;
                    }
                    else
                    {
                        button出力.Enabled = true;
                    }
                    dateTimePicker希望納期.Enabled = true;
                    textBox希望港.ReadOnly = false;

                    button手配依頼.Enabled = true;
                    button保存.Enabled = true;

                    // 2016.03
                    buttonリスト入力.Enabled = true;

                    break;

                case Status.船未手配_1:

                    textBox手配内容.ReadOnly = false;
                    textBox備考.ReadOnly = false;
                    button品目追加.Enabled = true;
                    button読込.Enabled = true;
                    button出力.Enabled = true;
                    dateTimePicker希望納期.Enabled = true;
                    textBox希望港.ReadOnly = false;

                    button手配依頼.Enabled = true;
                    button保存.Enabled = true;
                    button削除.Enabled = true;

                    // 2016.03
                    buttonリスト入力.Enabled = true;
                    break;

                case Status.手配依頼済かつ未同期_2:

                    textBox手配内容.ReadOnly = false;
                    textBox備考.ReadOnly = false;
                    button品目追加.Enabled = true;
                    button読込.Enabled = true;
                    button出力.Enabled = true;
                    dateTimePicker希望納期.Enabled = true;
                    textBox希望港.ReadOnly = false;

                    button手配依頼.Enabled = true;
                    button削除.Enabled = true;

                    // 2016.03
                    buttonリスト入力.Enabled = true;
                    break;

                case Status.手配依頼済かつ同期済_3:
                    if (is船用品())
                    {
                        button出力.Enabled = true;
                    }
                    break;
            }

            // 2012.02 [4.2.10]承認機能を設ける
            if (button手配依頼.Enabled == true)
            {
                MsUser loginUser = NBaseCommon.Common.LoginUser;
                if (loginUser.UserKbn == (int)NBaseData.DAC.MsUser.USER_KBN.船員)
                {
                    bool enabled = false;
                    SiCard siCard = NBaseCommon.Common.siCard;
                    foreach (SiLinkShokumeiCard s in siCard.SiLinkShokumeiCards)
                    {
                        string shokumei = SeninTableCache.instance().GetMsSiShokumeiName(loginUser, s.MsSiShokumeiID);
                        if (shokumei == "船長" || shokumei == "機関長")
                        {
                            enabled = true;
                        }
                    }
                    button手配依頼.Enabled = enabled;
                }
            }

            formDelegate.InitializeComponentsEnabled();
        }

        private void InitializeTable()
        {
            treeListViewDelegate = new TreeListViewDelegate(treeListView1);

            treeListViewDelegate.SetColumnFont(HonsenUIConstants.DEFAULT_FONT);

            formDelegate.InitializeTable();

            UpdateTable();
        }

        internal void UpdateTable()
        {
            treeListView1.SuspendUpdate();

            treeListView1.Nodes.Clear();
            formDelegate.UpdateTable();

            treeListView1.ResumeUpdate();
        }

        private TreeListViewNode CreateOdThiShousaiItemNode(TreeListViewNode thiItemNode, OdThiShousaiItem si, int i)
        {
            return formDelegate.CreateOdThiShousaiItemNode(thiItemNode, si, i);
        }


        private TreeListViewNode CreateOdThiItemNode(TreeListViewNode parentNode, OdThiItem t)
        {
            TreeListViewNode node = treeListViewDelegate.CreateNode();
            treeListViewDelegate.AddSubItem(node, "", true);
            //string itemName = t.MsItemSbtName != null && t.MsItemSbtName.Length > 0 ?
            //    t.MsItemSbtName + " : " + t.ItemName : t.ItemName;
            //treeListViewDelegate.AddSubItem(node, itemName, true);
            treeListViewDelegate.AddSubItem(node, t.ItemName, true);
            treeListViewDelegate.AddSubItem(node, "", true);

            //if (is船用品())
            //    treeListViewDelegate.AddSubItem(node, "", true);

            treeListViewDelegate.AddSubItem(node, "", true);
            treeListViewDelegate.AddSubItem(node, "", true);
            // 添付
            if (t.OdAttachFileID != null && t.OdAttachFileID.Length > 0)
            {
                treeListViewDelegate.AddLinkItem(node, t.OdAttachFileID);
            }
            else
            {
                treeListViewDelegate.AddSubItem(node, "", true);
            }
            treeListViewDelegate.AddSubItem(node, "", true);

            if (parentNode == null)
            {
                treeListView1.Nodes.Add(node);
            }
            else
            {
                parentNode.Nodes.Add(node);
            }
            
            return node;
        }


        private TreeListViewNode CreateThiItemHeaderNode(TreeListViewNode parentNode, ItemHeader<OdThiItem> h)
        {
            Color backColor = Color.LightBlue;

            TreeListViewNode node = treeListViewDelegate.CreateNode(backColor);
            treeListViewDelegate.AddSubItem(node, "", true);
            treeListViewDelegate.AddSubItem(node, h.Header, true);
            treeListViewDelegate.AddSubItem(node, "", true);

            //if (is船用品())
            //    treeListViewDelegate.AddSubItem(node, "", true);

            treeListViewDelegate.AddSubItem(node, "", true);
            treeListViewDelegate.AddSubItem(node, "", true);
            treeListViewDelegate.AddSubItem(node, "", true);
            treeListViewDelegate.AddSubItem(node, "", true);

            if (parentNode == null)
            {
                treeListView1.Nodes.Add(node);
            }
            else
            {
                parentNode.Nodes.Add(node);
            }

            return node;
        }


        internal Status GetStatus()
        {
            if (odThi.OdThiID == null)
            {
                return Status.未保存_0;
            }
            else if (odThi.Status == (int)OdThi.STATUS.船未手配)
            {
                return Status.船未手配_1;
            }
            else if (odThi.SendFlag == 0)
            {
                return Status.手配依頼済かつ未同期_2;
            }
            else
            {
                return Status.手配依頼済かつ同期済_3;
            }
        }

        private void addButt_Click(object sender, EventArgs e)
        {
            手配品目Form form = new 手配品目Form(this, msThiIraiSbtId, ref 添付Files);
            form.ShowDialog();
        }

        private void button手配依頼_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("手配依頼を送信します。よろしいですか？", "手配依頼の確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (ValidateFields())
                {
                    InsertRecords(OdThi.STATUS.手配依頼済);
                    tehaiIraiIchiranForm.BuildModel();
                    tehaiIraiIchiranForm.UpdateTable();

                    saved = true;
                    Close();
                }
            }
        }

        private void button保存_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                InsertRecords(OdThi.STATUS.船未手配);
                tehaiIraiIchiranForm.BuildModel();
                tehaiIraiIchiranForm.UpdateTable();

                saved = true;
                Close();
            }
        }

        private void InsertRecords(OdThi.STATUS status)
        {
            formDelegate.DetectInsertRecords();

            bool Is_新規 = (GetStatus() == Status.未保存_0 || GetStatus() == Status.船未手配_1);

            BuildOdThi(status);

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    SyncTableSaver.InsertOrUpdate(odThi, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);

                    int i = 1;
                    foreach (OdThiItem item in odThi.OdThiItems)
                    {
                        if (item.OdThiItemID == null)
                        {
                            item.OdThiItemID = System.Guid.NewGuid().ToString();
                        }

                        item.OdThiID = odThi.OdThiID;
                        item.ShowOrder = i++;
                        item.RenewDate = DateTime.Now;
                        item.RenewUserID = 同期Client.LOGIN_USER.MsUserID;

                        SyncTableSaver.InsertOrUpdate(item, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);
                        MsShoushuriItem shoushuriItem = InsertMsShoushuriItem(item.ItemName);
                        if (shoushuriItem != null)
                            SyncTableSaver.InsertOrUpdate(shoushuriItem, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);

                        int k = 1;
                        foreach (OdThiShousaiItem shousaiItem in item.OdThiShousaiItems)
                        {
                            if (shousaiItem.OdThiShousaiItemID == null)
                            {
                                shousaiItem.OdThiShousaiItemID = System.Guid.NewGuid().ToString();
                            }

                            if (shousaiItem.ZaikoCount == int.MinValue)
                            {
                                shousaiItem.ZaikoCount = 0;
                            }

                            if (shousaiItem.Count == int.MinValue)
                            {
                                shousaiItem.Count = shousaiItem.Sateisu = 0;
                            }

                            shousaiItem.OdThiItemID = item.OdThiItemID;
                            shousaiItem.ShowOrder = k++;
                            shousaiItem.RenewDate = DateTime.Now;
                            shousaiItem.RenewUserID = 同期Client.LOGIN_USER.MsUserID;

                            SyncTableSaver.InsertOrUpdate(shousaiItem, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);
                            //InsertMsSsShousaiItem(shousaiItem.ShousaiItemName);
                        }
                    }

                    foreach (OdAttachFile attach in 添付Files)
                    {
                        attach.VesselID = 同期Client.LOGIN_VESSEL.MsVesselID;
                        attach.DataNo = 0;
                        attach.RenewDate = DateTime.Now;
                        attach.RenewUserID = 同期Client.LOGIN_USER.MsUserID;
                        SyncTableSaver.InsertOrUpdate(attach, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);
                    }

                    if (Is_新規 && status == OdThi.STATUS.手配依頼済)
                    {
                        // 本船更新情報
                        PtHonsenkoushinInfo honsenkoushinInfo = 本船更新情報.CreateInstance(odThi, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.手配依頼);
                        SyncTableSaver.InsertOrUpdate(honsenkoushinInfo, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);
                    }

                    dbConnect.Commit();
                }
                catch (Exception e)
                {
                    dbConnect.RollBack();
                }
            }
        }

        private MsShoushuriItem InsertMsShoushuriItem(string itemName)
        {
            MsShoushuriItem shoushuriItem = null;

            List<MsShoushuriItem> identicals = MsShoushuriItem.GetRecordsByItemName(NBaseCommon.Common.LoginUser, itemName);

            if (identicals.Count == 0)
            {
                shoushuriItem = new MsShoushuriItem();

                shoushuriItem.MsSsItemID = System.Guid.NewGuid().ToString();
                shoushuriItem.MsVesselID = 同期Client.LOGIN_VESSEL.MsVesselID;
                shoushuriItem.ItemName = itemName;
                shoushuriItem.RenewDate = DateTime.Now;
                shoushuriItem.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                shoushuriItem.VesselID = 同期Client.LOGIN_VESSEL.MsVesselID;

                //shoushuriItem.InsertRecord(NBaseCommon.Common.LoginUser);
            }
            return shoushuriItem;
        }

        private void InsertMsSsShousaiItem(string shousaiItemName)
        {
            List<MsSsShousaiItem> identicals = MsSsShousaiItem.GetRecordsByShousaiItemName(NBaseCommon.Common.LoginUser, shousaiItemName);

            if (identicals.Count == 0)
            {
                MsSsShousaiItem shousaiItem = new MsSsShousaiItem();

                shousaiItem.MsSsShousaiItemID = System.Guid.NewGuid().ToString();
                shousaiItem.MsVesselID = 同期Client.LOGIN_VESSEL.MsVesselID;
                shousaiItem.ShousaiItemName = shousaiItemName;
                shousaiItem.RenewDate = DateTime.Now;
                shousaiItem.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                shousaiItem.VesselID = 同期Client.LOGIN_VESSEL.MsVesselID;

                shousaiItem.InsertRecord(NBaseCommon.Common.LoginUser);
            }
        }

        private OdThi BuildOdThi(OdThi.STATUS status)
        {
            odThi.Status = (int)status;

            if (odThi.OdThiID == null)
            {
                odThi.OdThiID = System.Guid.NewGuid().ToString();
                odThi.MsThiIraiStatusID = "0";
            }

            odThi.MsVesselID = 同期Client.LOGIN_VESSEL.MsVesselID;
            odThi.ThiIraiDate = DateTime.Now;
            odThi.MsThiIraiSbtID = this.msThiIraiSbtId;
            odThi.MsThiIraiShousaiID = this.msThiIraiShousaiId;
            //odThi.Naiyou = textBox手配内容.Text;
            //odThi.Bikou = textBox備考.Text;
            odThi.Naiyou = StringUtils.Escape(textBox手配内容.Text);
            odThi.Bikou = StringUtils.Escape(textBox備考.Text);
            odThi.Kiboubi = dateTimePicker希望納期.Value;
            odThi.Kiboukou = textBox希望港.Text;
            odThi.ThiUserID = 同期Client.LOGIN_USER.MsUserID;
            odThi.VesselID = 同期Client.LOGIN_VESSEL.MsVesselID;
            odThi.DataNo = 0;
            odThi.RenewDate = DateTime.Now;
            odThi.RenewUserID = 同期Client.LOGIN_USER.MsUserID;

            return odThi;
        }


        private void button削除_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("この手配依頼を削除してよろしいですか？", "削除の確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DeleteRecords();

                tehaiIraiIchiranForm.BuildModel();
                tehaiIraiIchiranForm.UpdateTable();
                saved = true;
                Close();
            }
        }


        private void DeleteRecords()
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    odThi.CancelFlag = 1;
                    SyncTableSaver.InsertOrUpdate(odThi, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);
                    
                    foreach (OdThiItem ti in odThi.OdThiItems)
                    {
                        if (ti.OdThiItemID == null)
                        {
                            continue;
                        }

                        ti.CancelFlag = 1;
                        SyncTableSaver.InsertOrUpdate(ti, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);

                        foreach (OdThiShousaiItem si in ti.OdThiShousaiItems)
                        {
                            if (si.OdThiShousaiItemID == null)
                            {
                                continue;
                            }

                            si.CancelFlag = 1;
                            SyncTableSaver.InsertOrUpdate(si, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);
                        }
                    }

                    List<PtHonsenkoushinInfo> his = PtHonsenkoushinInfo.GetRecordsBySanshoumotoId(dbConnect, 同期Client.LOGIN_USER, odThi.OdThiID);

                    foreach (PtHonsenkoushinInfo h in his)
                    {
                        h.DeleteFlag = 1;
                        SyncTableSaver.InsertOrUpdate(h, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);
                    }

                    dbConnect.Commit();
                }
                catch (Exception e)
                {
                    dbConnect.RollBack();
                }
            }
        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            Close();
        }


        private bool ValidateFields()
        {
            if (textBox手配内容.Text.Length == 0)
            {
                MessageBox.Show("手配内容を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (textBox手配内容.Text.Length > 50)
            {
                MessageBox.Show("手配内容は50文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (textBox備考.Text.Length > 500)
            {
                MessageBox.Show("備考（品名、規格等）は500文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (textBox希望港.Text.Length > 30)
            {
                MessageBox.Show("希望港は30文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (is燃料_潤滑油() && !Validate_燃料_潤滑油_依頼数())
            {
                return false;
            }

            return true;
        }


        private bool Validate_燃料_潤滑油_依頼数()
        {
            foreach (OdThiItem ti in odThi.OdThiItems)
            {
                foreach (OdThiShousaiItem si in ti.OdThiShousaiItems)
                {
                    if (si.Count != 0)
                    {
                        return true;
                    }
                }
            }

            if (MessageBox.Show("全ての詳細品目の依頼数がゼロですがよろしいですか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                return true;
            }

            return false;
        }


        internal void AddOdThiItem(OdThiItem odThiItem)
        {
            if (!ReplaceCloneOdThiItem(odThiItem))
            {
                if (!AddCloneOdThiShousaiItem(odThiItem))
                {
                    odThi.OdThiItems.Add(odThiItem);
                }
            }
            UpdateTable();
        }

        internal bool ReplaceCloneOdThiItem(OdThiItem odThiItem)
        {
            int sameObjIndex = -1;

            for (int i = 0; i < odThi.OdThiItems.Count; i++)
            {
                if (odThi.OdThiItems[i].OdThiItemID == odThiItem.OdThiItemID)
                {
                    sameObjIndex = i;
                    break;
                }
            }

            if (sameObjIndex >= 0)
            {
                odThi.OdThiItems.RemoveAt(sameObjIndex);
                odThi.OdThiItems.Insert(sameObjIndex, odThiItem);
                return true;
            }

            return false;
        }

        internal bool AddCloneOdThiShousaiItem(OdThiItem odThiItem)
        {
            int sameObjIndex = -1;

            for (int i = 0; i < odThi.OdThiItems.Count; i++)
            {
                if (odThi.OdThiItems[i].Header == odThiItem.Header && odThi.OdThiItems[i].ItemName == odThiItem.ItemName)
                {
                    sameObjIndex = i;
                    break;
                }
            }

            if (sameObjIndex >= 0)
            {
                odThi.OdThiItems[sameObjIndex].OdThiShousaiItems.AddRange(odThiItem.OdThiShousaiItems);
                return true;
            }

            return false;
        }


        private bool is修繕小修理()
        {
            return (msThiIraiSbtId.Equals("1") && msThiIraiShousaiId.Equals("1"));
        }

        private bool is修繕入渠()
        {
            return (msThiIraiSbtId.Equals("1") && !msThiIraiShousaiId.Equals("1"));
        }

        private bool is燃料_潤滑油()
        {
            return msThiIraiSbtId.Equals("2");
        }

        public bool is船用品()
        {
            return msThiIraiSbtId.Equals("3");
        }

        private void textBox手配内容_Enter(object sender, EventArgs e)
        {
            textBox手配内容.BackColor = HonsenUIConstants.FOCUS_BACK_COLOR;
        }

        private void textBox手配内容_Leave(object sender, EventArgs e)
        {
            textBox手配内容.BackColor = Color.White;
        }

        private void textBox備考_Enter(object sender, EventArgs e)
        {
            textBox備考.BackColor = HonsenUIConstants.FOCUS_BACK_COLOR;
        }

        private void textBox備考_Leave(object sender, EventArgs e)
        {
            textBox備考.BackColor = Color.White;
        }

        private void textBox希望港_Enter(object sender, EventArgs e)
        {
            textBox希望港.BackColor = HonsenUIConstants.FOCUS_BACK_COLOR;
        }

        private void textBox希望港_Leave(object sender, EventArgs e)
        {
            textBox希望港.BackColor = Color.White;
        }

        private void 手配依頼Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            同期Client.SYNC_SUSPEND = false;
        }

        /// <summary>
        /// 「ドッグオーダー読込／船用品注文書読込」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button読込_Click(object sender, EventArgs e)
        private void button読込_Click(object sender, EventArgs e)
        {
            if (is修繕入渠())
            {
                if (openFileDialogドックオーダー読込.ShowDialog(this) == DialogResult.OK)
                {
                    DockOrderReader reader = new DockOrderReader(openFileDialogドックオーダー読込.FileName,
                                                                 new DirectDockOrderDacProxy());

                    try
                    {
                        List<OdThiItem> odThiItems = reader.Read();

                        odThi.OdThiItems.AddRange(odThiItems);

                        UpdateTable();
                    }
                    catch (InvalidFormatException ex)
                    {
                        MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (is船用品())
            {
                openFileDialogドックオーダー読込.Title = "";
                if (openFileDialogドックオーダー読込.ShowDialog(this) == DialogResult.OK)
                {
                    VesselItemReader reader = new VesselItemReader(openFileDialogドックオーダー読込.FileName,
                                                                 new DirectVesselItemDacProxy(),
                                                                 同期Client.LOGIN_VESSEL.MsVesselID);

                    try
                    {
                        List<OdThiItem> odThiItems = reader.Read();

                        odThi.OdThiItems.AddRange(odThiItems);

                        UpdateTable();
                    }
                    catch (InvalidFormatException ex)
                    {
                        MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        #endregion


        /// <summary>
        /// 「ドッグオーダー出力／船用品注文書出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button出力_Click(object sender, EventArgs e)
        private void button出力_Click(object sender, EventArgs e)
        {
            if (is修繕入渠())
            {
                //入渠種別Form form = new 入渠種別Form();

                //if (form.ShowDialog(this) == DialogResult.OK)
                //{
                //    if (saveFileDialogドックオーダー出力.ShowDialog(this) == DialogResult.OK)
                //    {
                //        DockOrderWriter writer = new DockOrderWriter(saveFileDialogドックオーダー出力.FileName,
                //                                                     new DirectDockOrderDacProxy());
                //        try
                //        {
                //            writer.Write(同期Client.LOGIN_VESSEL.MsVesselID, form.SelectedThiIraiShousai.MsThiIraiShousaiID);
                //        }
                //        catch (NoDataException ex)
                //        {
                //            MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        }
                //    }
                //}
                if (saveFileDialogドックオーダー出力.ShowDialog(this) == DialogResult.OK)
                {
                    DockOrderWriter writer = new DockOrderWriter(saveFileDialogドックオーダー出力.FileName,
                                                                 new DirectDockOrderDacProxy());
                    try
                    {
                        List<MsThiIraiShousai> shousaiList = MsThiIraiShousai.GetRecords(同期Client.LOGIN_USER);
                        MsThiIraiShousai 検査種類 = shousaiList.Where(o => o.MsThiIraiShousaiID == MsThiIraiShousai.ToId(MsThiIraiShousai.ThiIraiShousaiEnum.入渠)).FirstOrDefault();
                        writer.Write(同期Client.LOGIN_VESSEL.MsVesselID, 検査種類.MsThiIraiShousaiID);
                    }
                    catch (NoDataException ex)
                    {
                        MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (is船用品())
            {
                string exeDir = System.IO.Directory.GetCurrentDirectory();
                string templateFilePath = exeDir + "\\Template\\Template_船用品注文書.xlsx";

                saveFileDialog1.CreatePrompt = true;       //新規作成確認
                saveFileDialog1.OverwritePrompt = true;    //上書き確認
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.Filter = "船用品注文書ファイル(*.xlsx)|*.xlsx";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.FileName = "船用品注文書.xlsx";

                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }

                VesselItemWriter writer = new VesselItemWriter(templateFilePath, saveFileDialog1.FileName);
                try
                {
                    writer.CreateFile(同期Client.LOGIN_USER, odThi.OdThiID);
                    MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("船用品注文書の出力に失敗しました。\n (Err:" + ex.Message + ")", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
                    formDelegate.treeListView1_DoubleClick(sender, e);
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
        /***********************************************************************/
        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
        //Excel出力関数
        #region Excel出力関数
        /// <summary>
        /// 保存ファイル選択
        /// 引数：選択ファイル名
        /// 返り値：選ばれたか？
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private bool SelectSaveFile(発注帳票Common.kubun発注帳票 kubun, ref string filename)
        {
            string BaseFileName = "査定表";
            if (kubun == 発注帳票Common.kubun発注帳票.査定表)
            {
                BaseFileName = "査定表";
            }
            else
            {
                BaseFileName = "請求書";
            }
            this.saveFileDialog1.Filter = BaseFileName + " (*.xlsx)|*.xlsx";
            this.saveFileDialog1.FileName = BaseFileName + ".xlsx";
            this.saveFileDialog1.OverwritePrompt = true;    //上書き確認

            this.saveFileDialog1.Filter =
                "xlsxファイル(*.xlsx)|*.xlsx" +
                "|" +
                "全てのファイル(*.*)|*.*";


            DialogResult ret = this.saveFileDialog1.ShowDialog();

            if (ret == DialogResult.Cancel)
            {
                return false;
            }

            filename = this.saveFileDialog1.FileName;

            return true;
        }


        //private bool 手配依頼出力総括()
        //{
        //    //保存ファイルの選択
        //    string filename = "";
        //    bool ret = this.SelectSaveFile(ref filename);

        //    if (ret == false)
        //    {
        //        return false;
        //    }

        //    ret = this.手配依頼一覧出力(filename);

        //    if (ret == false)
        //    {
        //        MessageBox.Show("ファイルの出力に失敗しました。", "手配依頼", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }

        //    string smes = "「" + filename + "」に出力しました。";
        //    MessageBox.Show(smes, "手配依頼", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //    return true;
        //}

        //private bool 手配依頼一覧出力(string filename)
        //{
        //    using (ExcelCreator.Xlsx.XlsxCreator crea = new ExcelCreator.Xlsx.XlsxCreator())
        //    {
        //        int ret = crea.CreateBook(filename, 1, ExcelCreator.xlsxVersion.ver2013);

        //        if (ret < 0)
        //        {
        //            return false;
        //        }

        //        this.Write先頭項目(crea);
        //        this.Write手配依頼Main(crea);

        //        crea.CloseBook(true);
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// 項目名の出力
        ///// </summary>
        ///// <param name="crea"></param>
        //private void Write先頭項目(ExcelCreator.Xlsx.XlsxCreator crea)
        //{
        //    int pos = 1;
        //    string colpos = "";
        //    int i = 0;
        //    //---------------------------------------
        //    while (true)
        //    {
        //        //終了文字列だった
        //        if (this.HeadName[i, 0] == "-1")
        //        {
        //            break;
        //        }

        //        //書き込み位置を作る
        //        colpos = this.HeadName[i, 1];
        //        colpos += pos.ToString();


        //        crea.Cell(colpos).Value = this.HeadName[i, 0];
        //        crea.Cell(colpos).Attr.FontStyle = ExcelCreator.FontStyle.Bold;

        //        i++;
        //    }


        //}

        ///// <summary>
        ///// 手配依頼出力Main
        ///// </summary>
        ///// <param name="crea"></param>
        //private void Write手配依頼Main(ExcelCreator.Xlsx.XlsxCreator crea)
        //{
        //    //書き込み開始位置
        //    int pos = 2;
        //    this.Write手配(crea, this.odThi, pos);
        //}

        ///// <summary>
        ///// 手配書き込み
        ///// 引数：書き込み場所、書き込み手配、書き込み開始位置Y
        ///// </summary>
        ///// <param name="crea"></param>
        ///// <param name="thi"></param>
        ///// <param name="pos"></param>        
        //private void Write手配(ExcelCreator.Xlsx.XlsxCreator crea, OdThi thi, int pos)
        //{
        //    if (this.is燃料_潤滑油() == true)
        //    {
        //        this.Write手配潤滑油(crea, thi, pos);
        //    }
        //    else
        //    {
        //        this.Write手配潤滑油以外(crea, thi, pos);
        //    }
        //}

        //private void Write手配潤滑油(ExcelCreator.Xlsx.XlsxCreator crea, OdThi thi, int pos)
        //{
        //    string cell = "";

        //    foreach (OdThiItem item in thi.OdThiItems)
        //    {
        //        // 削除されているものは出力しない
        //        if (item.CancelFlag == 1) 
        //            continue;

        //        //区分を書く
        //        cell = this.HeadName[0, 1] + pos.ToString();
        //        crea.Cell(cell).Value = item.MsItemSbtName;

        //        //仕様・型式を書く
        //        cell = this.HeadName[1, 1] + pos.ToString();
        //        crea.Cell(cell).Value = item.ItemName;

        //        //中身を描く
        //        this.Write手配詳細Item(crea, item, ref pos);
        //    }
        //}

        //private void Write手配潤滑油以外(ExcelCreator.Xlsx.XlsxCreator crea, OdThi thi, int pos)
        //{
        //    List<ItemHeader<OdThiItem>> itemheaderlist = OdThiTreeListViewHelper.GroupByThiItemHeader(thi.OdThiItems);

        //    string cell = "";

        //    foreach (ItemHeader<OdThiItem> head in itemheaderlist)
        //    {
        //        foreach (OdThiItem item in head.Items)
        //        {
        //            // 削除されているものは出力しない
        //            if (item.CancelFlag == 1)
        //                continue;

        //            //区分を書く
        //            cell = this.HeadName[0, 1] + pos.ToString();
        //            crea.Cell(cell).Value = item.MsItemSbtName;

        //            //仕様・型式を書く
        //            cell = this.HeadName[1, 1] + pos.ToString();
        //            crea.Cell(cell).Value = item.ItemName;

        //            //中身を描く
        //            this.Write手配詳細Item(crea, item, ref pos);
        //        }
        //    }
        //}

        //private void Write手配詳細Item(ExcelCreator.Xlsx.XlsxCreator crea, OdThiItem thi_item, ref int pos)
        //{
        //    string cell = "";

        //    foreach (OdThiShousaiItem item in thi_item.OdThiShousaiItems)
        //    {
        //        // 削除されているものは出力しない
        //        if (item.CancelFlag == 1)
        //            continue;

        //        //詳細品目名を書く
        //        cell = this.HeadName[2, 1] + pos.ToString();
        //        crea.Cell(cell).Value = item.ShousaiItemName;

        //        //単位を書く
        //        cell = this.HeadName[3, 1] + pos.ToString();
        //        crea.Cell(cell).Value = item.MsTaniName;

        //        //在庫数を書く
        //        cell = this.HeadName[4, 1] + pos.ToString();
        //        crea.Cell(cell).Value = item.ZaikoCount;

        //        //依頼数を書く
        //        cell = this.HeadName[5, 1] + pos.ToString();
        //        crea.Cell(cell).Value = item.Count;

        //        //備考を書く
        //        cell = this.HeadName[6, 1] + pos.ToString();
        //        crea.Cell(cell).Value = item.Bikou;

        //        //次の位置へ
        //        pos++;
        //    }
        //}

        //=====================================================================
        private string[,] HeadName = {      //名前          位置
                                            {"区分",         "A"},
                                            {"仕様・型式",   "B"},
                                            {"詳細品目",     "C"},
                                            {"単位",         "D"},
                                            {"在庫数",       "E"},
                                            {"依頼数",       "F"},
                                            {"備考",         "G"},                                   


                                            {"-1",         "-1"},                                       
                                     };
        #endregion

        //private void Buttonファイル出力_Click(object sender, EventArgs e)
        //{
        //    this.手配依頼出力総括();
        //}

        private void Button査定表出力_Click(object sender, EventArgs e)
        {
            請求書出力(発注帳票Common.kubun発注帳票.査定表);
        }

        private void Button請求書出力_Click(object sender, EventArgs e)
        {
            請求書出力(発注帳票Common.kubun発注帳票.請求書);
        }

        private void 請求書出力(発注帳票Common.kubun発注帳票 kubun)
        {
            if (odThi.OdThiID == null)
                return;

            string outputFilePath = "";
            bool ret = this.SelectSaveFile(kubun, ref outputFilePath);
            if (ret == false)
            {
                return;
            }

            string exeDir = System.IO.Directory.GetCurrentDirectory();
            string templateFilePath = "";

            if (msThiIraiSbtId == MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.修繕))
            {
                string baseFileName = null;
                if (odThi.MsThiIraiShousaiID == MsThiIraiShousai.ToId(MsThiIraiShousai.ThiIraiShousaiEnum.入渠))
                {
                    baseFileName = "KK修繕申込書";
                }
                else
                {
                    baseFileName = "KK修繕注文書";
                }
                if (kubun == 発注帳票Common.kubun発注帳票.査定表)
                {
                    baseFileName += "(査定)";
                }

                templateFilePath = exeDir + "\\Template\\Template_" + baseFileName + ".xlsx";
                //templateFilePath = exeDir + "\\Template\\Template_KK修繕注文書.xlsx";

                new KK修繕注文書出力(templateFilePath, outputFilePath, null).CreateFile(NBaseCommon.Common.LoginUser, kubun, odThi.OdThiID);
            }
            else
            {
                templateFilePath = exeDir + "\\Template\\Template_KK船用品請求書.xlsx";
                new KK船用品請求書出力(templateFilePath, outputFilePath, null).CreateFile(NBaseCommon.Common.LoginUser, kubun, odThi.OdThiID);
            }
            string smes = "「" + outputFilePath + "」に出力しました。";
            MessageBox.Show(smes, "手配依頼", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return;
        }




        private void 手配依頼Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!saved)
            {
                DialogResult result = MessageBox.Show("このウィンドウを閉じてよろしいですか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void GetAttachFiles()
        {
            if (odThi.OdThiID != null)
            {
                List<OdAttachFile> OdAttachFiles = OdAttachFile.GetRecordsByOdThiId(同期Client.LOGIN_USER, odThi.OdThiID);
                OdAttachFile tmp = null;
                foreach (OdThiItem item in odThi.OdThiItems)
                {
                    tmp = GetAttachFile(OdAttachFiles, item.OdAttachFileID);
                    if (tmp != null)
                    {
                        item.OdAttachFileName = tmp.FileName;
                    }

                    foreach (OdThiShousaiItem thiShousaiItem in item.OdThiShousaiItems)
                    {
                        tmp = GetAttachFile(OdAttachFiles, thiShousaiItem.OdAttachFileID);
                        if (tmp != null)
                        {
                            thiShousaiItem.OdAttachFileName = tmp.FileName;
                        }
                    }
                }
            }
        }


        private OdAttachFile GetAttachFile(List<OdAttachFile> attachFiles, string id)
        {
            if (attachFiles.Count == 0)
                return null;
            if (id == null || id.Length == 0)
                return null;
            var trgs = from af in attachFiles
                       where af.OdAttachFileID == id
                       select af;
            if (trgs.Count<OdAttachFile>() > 0)
                return trgs.First<OdAttachFile>();
            else
                return null;

        }


        public void View添付ファイル(string odAttachFileId)
        {
            string id = "";
            string fileName = "";
            byte[] fileData = null;

            try
            {
                Cursor = Cursors.WaitCursor;

                OdAttachFile odAttachFile = null;
                foreach (OdAttachFile file in 添付Files)
                {
                    if (file.OdAttachFileID == odAttachFileId)
                    {
                        odAttachFile = file;
                        break;
                    }
                }
                if (odAttachFile == null)
                {
                    // サーバから添付データを取得する
                    odAttachFile = OdAttachFile.GetRecord(同期Client.LOGIN_USER, odAttachFileId);
                }
                if (odAttachFile == null)
                {
                    MessageBox.Show("対象ファイルを開けません：添付ファイルがみつかりません", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                fileName = odAttachFile.FileName;
                fileData = odAttachFile.Data;
                id = odAttachFile.OdAttachFileID;

                // ファイルの表示
                NBaseCommon.FileView.View(id, fileName, fileData);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("対象ファイルを開けません：" + Ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }



        private void buttonリスト入力_Click(object sender, EventArgs e)
        {
#if 船用品
            //手配品目Form form = new 手配品目Form(this, msThiIraiSbtId, ref 添付Files);
            //form.ShowDialog();
            船用品一括入力Form form = new 船用品一括入力Form(this);
            form.ShowDialog();
#endif
        }
    }
}
