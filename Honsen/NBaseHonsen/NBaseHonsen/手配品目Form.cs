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
using NBaseData.DAC;
using SyncClient;
using NBaseUtil;
using System.IO;

namespace NBaseHonsen
{
    public partial class 手配品目Form : Form
    {
        private TreeListViewDelegate treeListViewDelegate;

        private readonly 手配依頼Form tehaiIraiForm;
        private readonly OdThiItem odThiItem;
        private readonly IFormDelegate formDelegate;

        private Dictionary<TreeListViewNode, OdThiShousaiItem> odThiShousaiItemNodes =
                                             new Dictionary<TreeListViewNode, OdThiShousaiItem>();

        private OdThiShousaiItem newShousaiItem;

        private List<string> shousaiHinmokuAutoCompletes = new List<string>();

        private List<MsVesselItemCategory> MsVesselItemCategorys = null;
        private int msVesselItemCategoryNumber = -1;

        private bool saved;

        public List<OdAttachFile> 添付Files = null;
        public string msThiIraiSbtId;

        public 手配品目Form(手配依頼Form tehaiIraiForm, string msThiIraiSbtId, ref List<OdAttachFile> 添付Files)
            : this(tehaiIraiForm, new OdThiItem(), msThiIraiSbtId, ref 添付Files)
        {
        }

        public 手配品目Form(手配依頼Form tehaiIraiForm, OdThiItem odThiItem, string msThiIraiSbtId, ref List<OdAttachFile> 添付Files)
        {
            this.tehaiIraiForm = tehaiIraiForm;
            this.odThiItem = odThiItem.Clone();
            this.添付Files = 添付Files;
            this.msThiIraiSbtId = msThiIraiSbtId;

            if (msThiIraiSbtId == MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品))
            {
                MsVesselItemCategorys = MsVesselItemCategory.GetRecords();
                formDelegate = new FormDelegate_船用品(this);
            }
            else
            {
                formDelegate = new FormDelegate_船用品以外(this);
            }

            InitializeComponent();
            Text = NBaseCommon.Common.WindowTitle("HN020104", "手配品目", WcfServiceWrapper.ConnectedServerID);
        }

        private void 手配品目Form_Load(object sender, EventArgs e)
        {
            List<MsItemSbt> itemSbts = MsItemSbt.GetRecords(NBaseCommon.Common.LoginUser);

            // 区分コンボボックス初期化
            //if (this.msThiIraiSbtId == MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品))
            //{
            //    comboBox区分.Items.Clear();
            //    comboBox区分.Items.Add("機関部");
            //    comboBox区分.Items.Add("甲板部");
            //    comboBox区分.SelectedItem = null;
            //}
            //else
            //{
            //    foreach (MsItemSbt itemSbt in itemSbts)
            //    {
            //        comboBox区分.Items.Add(itemSbt);

            //        if (itemSbt.MsItemSbtID == odThiItem.MsItemSbtID)
            //        {
            //            comboBox区分.SelectedItem = itemSbt;
            //        }
            //    }
            //}
            foreach (MsItemSbt itemSbt in itemSbts)
            {
                comboBox区分.Items.Add(itemSbt);

                if (itemSbt.MsItemSbtID == odThiItem.MsItemSbtID)
                {
                    comboBox区分.SelectedItem = itemSbt;
                }
            }
            if (comboBox区分.SelectedItem == null)
            {
                comboBox区分.SelectedItem = comboBox区分.Items[0];
            }

            // 単位ComboBox初期化
            comboBox単位.Items.Clear();
            foreach (MsTani t in MasterTable.instance().MsTaniList)
            {
                comboBox単位.Items.Add(t);
            }

            // 品目詳細初期化
            formDelegate.手配品目Form_Load(sender, e);

            // 添付ファイル
            textBox仕様型式添付.Text = odThiItem.OdAttachFileName;

            InitializeComponentsEnabled();

#if 船用品
            if (MsVesselItemCategorys != null)
            {
                bool isペイント = false;
                if (odThiItem.OdThiShousaiItems.Count() == 0)
                {
                    isペイント = true;
                }
                else if (odThiItem.OdThiShousaiItems.Any(obj => obj.CategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント)))
                {
                    isペイント = true;
                }
                if (isペイント)
                {
                    ペイント選択();
                    textBox仕様型式添付.ReadOnly = true;
                }
            }
            if (msVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
            {
                buttonカテゴリ選択.Enabled = false;
            }

            //// 次期改造
            //if (msVesselItemCategoryNumber != -1 && msVesselItemCategoryNumber != MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
            //{
            //    button特定品選択.Enabled = true;
            //}
#endif
            InitializeTable();
        }

        private void InitializeComponentsEnabled()
        {
            if (tehaiIraiForm.GetStatus() != 手配依頼Form.Status.手配依頼済かつ同期済_3)
            {
                comboBox区分.Enabled = true;
                multiLineCombo品目.Enabled = true;

                button登録.Enabled = true;

                if (tehaiIraiForm.GetStatus() != 手配依頼Form.Status.未保存_0)
                {
                    button品目削除.Enabled = true;
                }

                textBox仕様型式添付.ReadOnly = true;
                button仕様型式添付選択.Enabled = true;
                button仕様型式添付削除.Enabled = true;
            }
        }

        private void InitializeTable()
        {
            treeListViewDelegate = new TreeListViewDelegate(treeListView1);
            treeListViewDelegate.SetColumnFont(HonsenUIConstants.DEFAULT_FONT);

            object[,] columns = null;
            //if (MsVesselItemCategorys == null)
            //{
                columns = new object[,] {
                                        {"詳細品目", 400, null},
                                        {"単位", 60, null},
                                        {"在庫数", 60, null},
                                        {"依頼数", 60, null},
                                        {"添付", 40, null},
                                        {"備考（品名、規格等）", 350, null},
                                        };
            //}
            //else
            //{
            //    columns = new object[,] {
            //                            {"詳細品目", 400, null},
            //                            {"種別", 60, null},
            //                            {"単位", 50, null},
            //                            {"在庫数", 60, null},
            //                            {"依頼数", 60, null},
            //                            {"添付", 40, null},
            //                            {"備考（品名、規格等）", 350, null},
            //                            };
            //}
            treeListViewDelegate.SetColumns(columns);
            UpdateTable();
        }

        private void UpdateTable()
        {
            treeListView1.SuspendUpdate();

            treeListView1.Nodes.Clear();

            odThiShousaiItemNodes.Clear(); // 2015/03

            foreach (OdThiShousaiItem si in odThiItem.OdThiShousaiItems)
            {
                if (si.CancelFlag == 1)
                {
                    continue;
                }
                
                Color backColor = Color.White;

                TreeListViewNode node1 = treeListViewDelegate.CreateNode(backColor);
                treeListViewDelegate.AddSubItem(node1, si.ShousaiItemName, true);
                //if (MsVesselItemCategorys != null)
                //{
                //    treeListViewDelegate.AddSubItem(node1, NBaseHonsen.util.OdThiTreeListViewHelper.GetShousaiInputKind(si, MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品)), true);
                //}
                treeListViewDelegate.AddSubItem(node1, MasterTable.instance().GetMsTani(si.MsTaniID).TaniName, true);
                treeListViewDelegate.AddSubItem(node1, si.ZaikoCount != int.MinValue ? si.ZaikoCount.ToString() : string.Empty, true);
                treeListViewDelegate.AddSubItem(node1, si.Count != int.MinValue ? si.Count.ToString() : string.Empty, true);
                if (si.OdAttachFileID != null && si.OdAttachFileID.Length > 0)
                {
                    //treeListViewDelegate.AddLinkItem(node1, si.OdAttachFileID);
                    treeListViewDelegate.AddSubItem(node1, "〇", true);
                }
                else
                {
                    treeListViewDelegate.AddSubItem(node1, "", true);
                }
                treeListViewDelegate.AddSubItem(node1, si.Bikou, true);
                treeListView1.Nodes.Add(node1);

                odThiShousaiItemNodes.Add(node1, si);
            }
            
            CreateNewNode();

            treeListView1.ExpandAll();
            treeListView1.ResumeUpdate();
        }

        private void CreateNewNode()
        {
            Color backColor = Color.White;

            TreeListViewNode node1 = treeListViewDelegate.CreateNode();
            treeListViewDelegate.AddSubItem(node1, "（新規詳細品目）", true);
            //if (MsVesselItemCategorys != null)　treeListViewDelegate.AddSubItem(node1, "", true);
            treeListViewDelegate.AddSubItem(node1, "", true);
            treeListViewDelegate.AddSubItem(node1, "", true);
            treeListViewDelegate.AddSubItem(node1, "", true);
            treeListViewDelegate.AddSubItem(node1, "", true);
            treeListViewDelegate.AddSubItem(node1, "", true);

            newShousaiItem = new OdThiShousaiItem();
            newShousaiItem.ZaikoCount = int.MinValue;
            newShousaiItem.Count = int.MinValue;
            newShousaiItem.MsTaniID = string.Empty;

            treeListView1.Nodes.Add(node1);

            odThiShousaiItemNodes.Add(node1, newShousaiItem);
        }


        private void button登録_Click(object sender, EventArgs e)
        {
            if (formDelegate.ValidateFields())
            {
                BuildOdThiItem();
                tehaiIraiForm.AddOdThiItem(odThiItem);

                saved = true;
                Close();
            }
        }

        private void button閉じる_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BuildOdThiItem()
        {
            if (odThiItem.OdThiItemID == null)
            {
                odThiItem.OdThiItemID = System.Guid.NewGuid().ToString();
            }

            ////odThiItem.Header = textBoxヘッダ.Text;
            //odThiItem.Header = StringUtils.Escape(textBoxヘッダ.Text);

            ////区分　2021/09/09 m.yoshihara
            ////if (comboBox区分.Visible)
            ////{
            ////    odThiItem.Header = comboBox区分.Text;
            ////    odThiItem.MsItemSbtID = ((MsItemSbt)comboBox区分.SelectedItem).MsItemSbtID;
            ////}
            //odThiItem.Header = comboBox区分.Text;
            //if (this.msThiIraiSbtId != MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品))
            //{
            //    MsItemSbt itemSbt = null;
            //    if (comboBox区分.SelectedItem is MsItemSbt)
            //    {
            //        itemSbt = comboBox区分.SelectedItem as MsItemSbt;
            //    }
            //    if (itemSbt != null && itemSbt.MsItemSbtID.Length > 0)
            //    {
            //        odThiItem.MsItemSbtID = itemSbt.MsItemSbtID;
            //        odThiItem.MsItemSbtName = itemSbt.ItemSbtName;
            //    }
            //}

            odThiItem.Header = comboBox区分.Text;

            MsItemSbt itemSbt = null;
            if (comboBox区分.SelectedItem is MsItemSbt)
            {
                itemSbt = comboBox区分.SelectedItem as MsItemSbt;
            }
            if (itemSbt != null && itemSbt.MsItemSbtID.Length > 0)
            {
                odThiItem.MsItemSbtID = itemSbt.MsItemSbtID;
                odThiItem.MsItemSbtName = itemSbt.ItemSbtName;
            }  
            
            odThiItem.Bikou = "";

            formDelegate.Set品目ToModel(odThiItem);
           
            odThiItem.VesselID = 同期Client.LOGIN_VESSEL.MsVesselID;
            odThiItem.DataNo = 0;

            BuildOdThiShousaiItems();
        }

        private void BuildOdThiShousaiItems()
        {
            foreach (OdThiShousaiItem odThiShousaiItem in odThiItem.OdThiShousaiItems)
            {
                if (odThiShousaiItem.OdThiShousaiItemID == null)
                {
                    odThiShousaiItem.OdThiShousaiItemID = System.Guid.NewGuid().ToString();
                }

                odThiShousaiItem.VesselID = 同期Client.LOGIN_VESSEL.MsVesselID;
                odThiShousaiItem.DataNo = 0;
                odThiShousaiItem.Sateisu = odThiShousaiItem.Count;
            }
        }

        public void AddOdThiShousaiItem(OdThiShousaiItem odThiShousaiItem)
        {
            if (!odThiItem.OdThiShousaiItems.Contains(odThiShousaiItem))
            {
                odThiItem.OdThiShousaiItems.Add(odThiShousaiItem);
            }
            
            UpdateTable();
        }


        private void treeListView1_BeforeSelect(object sender, LidorSystems.IntegralUI.ObjectCancelEventArgs e)
        {
            TreeListViewNode node = treeListView1.SelectedNode;

            if (node != null && odThiShousaiItemNodes.ContainsKey(node))
            {
                OdThiShousaiItem item = odThiShousaiItemNodes[node];

                formDelegate.Set詳細品目ToModel(item);
                
                MsTani tani = comboBox単位.SelectedItem as MsTani;
                item.MsTaniID = tani.MsTaniID;
                item.ZaikoCount = textBox在庫数.Text != string.Empty ? StringUtils.ToNumber(textBox在庫数.Text) : int.MinValue;
                item.Count = textBox依頼数.Text != string.Empty ? StringUtils.ToNumber(textBox依頼数.Text) : int.MinValue;
                item.Sateisu = item.Count;
                //item.Bikou = textBox備考.Text;
                item.Bikou = StringUtils.Escape(textBox備考.Text);
                item.OdAttachFileName = textBox詳細添付.Text;

            }
        }

        private void treeListView1_AfterSelect(object sender, LidorSystems.IntegralUI.ObjectEventArgs e)
        {
            TreeListViewNode node = treeListView1.SelectedNode;

            if (node != null && odThiShousaiItemNodes.ContainsKey(node))
            {
                OdThiShousaiItem item = odThiShousaiItemNodes[node];

                formDelegate.Set詳細品目ToControl(item);

                textBox在庫数.Text = item.ZaikoCount != int.MinValue ? item.ZaikoCount.ToString() : string.Empty;
                //textBox依頼数.Text = item.Count != int.MinValue ? item.Count.ToString() : string.Empty;
                textBox依頼数.Text = item.Count > -1 ? item.Count.ToString() : string.Empty;
                textBox備考.Text = item.Bikou;
                
                foreach (object obj in comboBox単位.Items)
                {
                    MsTani tani = (MsTani)obj;
                    
                    if (tani.MsTaniID == item.MsTaniID)
                    {
                        comboBox単位.SelectedItem = tani;
                    }
                }
                if (item.OdAttachFileName != null && item.OdAttachFileName.Length > 0)
                {
                    textBox詳細添付.Text = item.OdAttachFileName;
                }
                else
                {
                    textBox詳細添付.Text = "";
                }

                InitializeShousaiItemComponentsEnabled(item);
            }
        }

        private void InitializeShousaiItemComponentsEnabled(OdThiShousaiItem item)
        {
            if (tehaiIraiForm.GetStatus() != 手配依頼Form.Status.手配依頼済かつ同期済_3)
            {
                multiLineCombo詳細品目.Enabled = true;
                
                comboBox単位.Enabled = true;
                textBox在庫数.ReadOnly = false;
                textBox依頼数.ReadOnly = false;
                textBox備考.ReadOnly = false;
                
                button詳細添付選択.Enabled = true;
                button詳細添付削除.Enabled = true;

                button船用品選択.Enabled = true;
                button登録.Enabled = true;

                if (odThiItem.OdThiShousaiItems.Contains(item))
                {
                    button詳細品目削除.Enabled = true;
                }
            }
        }

        private void treeListView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                //Tabキーが押されてもフォーカスが移動しないようにする
                case Keys.Tab:
                    e.IsInputKey = true;

                    TreeListViewNode node = treeListView1.SelectedNode;

                    if (node != null)
                    {
                        treeListView1.SelectedNode = node.NextNode;
                        formDelegate.Focus詳細品目();
                    }
                  break;
            }
        }

        private void multiLineCombo詳細品目_Leave(object sender, EventArgs e)
        {
            詳細品目入力コントロール_Leave(delegate(OdThiShousaiItem item)
            {
                formDelegate.Set詳細品目ToModel(item);

                if (item == newShousaiItem && !odThiItem.OdThiShousaiItems.Contains(item) && multiLineCombo詳細品目.Text.Length > 0)
                {
                    odThiItem.OdThiShousaiItems.Add(item);
                }
            });

            Control_Leave(sender, e);
        }

        private void comboBox単位_Leave(object sender, EventArgs e)
        {
            詳細品目入力コントロール_Leave(delegate(OdThiShousaiItem item)
            {
                MsTani tani = comboBox単位.SelectedItem as MsTani;
                item.MsTaniID = tani.MsTaniID;
            });

            Control_Leave(sender, e);
        }

        private void textBox在庫数_Leave(object sender, EventArgs e)
        {
            詳細品目入力コントロール_Leave(delegate(OdThiShousaiItem item)
            {
                if (textBox在庫数.Text != string.Empty)
                {
                    int i = StringUtils.ToNumber(textBox在庫数.Text);
                    textBox在庫数.Text = i.ToString();
                    item.ZaikoCount = i;
                }
                else
                {
                    textBox在庫数.Text = string.Empty;
                    item.ZaikoCount = int.MinValue;
                }
            });

            Control_Leave(sender, e);
        }

        private void textBox依頼数_Leave(object sender, EventArgs e)
        {
            詳細品目入力コントロール_Leave(delegate(OdThiShousaiItem item)
            {
                if (textBox依頼数.Text != string.Empty)
                {
                    int i = StringUtils.ToNumber(textBox依頼数.Text);
                    textBox依頼数.Text = i.ToString();
                    item.Count = item.Sateisu = i;
                }
                else
                {
                    textBox依頼数.Text = string.Empty;
                    item.Count = item.Sateisu = int.MinValue;
                }
            });

            Control_Leave(sender, e);
        }
        
        private void textBox備考_Leave(object sender, EventArgs e)
        {
            詳細品目入力コントロール_Leave(delegate(OdThiShousaiItem item)
            {
                //item.Bikou = textBox備考.Text;
                item.Bikou = StringUtils.Escape(textBox備考.Text);
            });

            Control_Leave(sender, e);
        }

        internal void 詳細品目選択(object sender, EventArgs e)
        {
            詳細品目入力コントロール_Leave(delegate(OdThiShousaiItem item)
            {
                formDelegate.Set詳細品目ToModel(item);

                if (item == newShousaiItem && !odThiItem.OdThiShousaiItems.Contains(item) && multiLineCombo詳細品目.Text.Length > 0)
                {
                    odThiItem.OdThiShousaiItems.Add(item);
                }
            });
        }

        private void 詳細品目入力コントロール_Leave(Action<OdThiShousaiItem> action)
        {
            this.treeListView1.AfterSelect -= new LidorSystems.IntegralUI.ObjectEventHandler(this.treeListView1_AfterSelect);

            TreeListViewNode node = treeListView1.SelectedNode;

            if (node != null && odThiShousaiItemNodes.ContainsKey(node))
            {
                OdThiShousaiItem item = odThiShousaiItemNodes[node];

                action(item);

                int i = node.Index;
                UpdateTable();
                treeListView1.SelectedNode = treeListView1.Nodes[i];
            }

            this.treeListView1.AfterSelect += new LidorSystems.IntegralUI.ObjectEventHandler(this.treeListView1_AfterSelect);
        }
        
        private void textBox備考_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                //Tabキーが押されてもフォーカスが移動しないようにする
                case Keys.Tab:
                    e.IsInputKey = true;

                    TreeListViewNode node = treeListView1.SelectedNode;

                    if (node != null)
                    {
                        if (node.NextNode != null)
                        {
                            treeListView1.SelectedNode = node.NextNode;
                        }
                        else
                        {
                            treeListView1.SelectedNode = treeListView1.Nodes[0];
                        }

                        formDelegate.Focus詳細品目();
                    }

                    break;
            }
        }

        private void textBox備考_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = textBox.Text.Replace("\t", "");
        }

        private void multiLineCombo品目_Leave(object sender, EventArgs e)
        {
            treeListView1.SelectedNode = treeListView1.Nodes[0];
            formDelegate.Focus詳細品目();

            Control_Leave(sender, e);
        }

        private void button詳細品目削除_Click(object sender, EventArgs e)
        {
             TreeListViewNode node = treeListView1.SelectedNode;

             if (node != null && odThiShousaiItemNodes.ContainsKey(node))
             {
                 OdThiShousaiItem item = odThiShousaiItemNodes[node];
                 item.CancelFlag = 1;
             }

             UpdateTable();
       }

        private void button品目削除_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("この仕様・型式を削除してよろしいですか？", "削除の確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                odThiItem.CancelFlag = 1;

                foreach (OdThiShousaiItem si in odThiItem.OdThiShousaiItems)
                {
                    si.CancelFlag = 1;
                }

                tehaiIraiForm.ReplaceCloneOdThiItem(odThiItem);
                tehaiIraiForm.UpdateTable();

                saved = true;
                Close();
            }
        }

        private void Control_Enter(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = HonsenUIConstants.FOCUS_BACK_COLOR;
        }

        private void Control_Leave(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.White;
        }

        private void button船用品選択_Click(object sender, EventArgs e)
        {
            if (msVesselItemCategoryNumber == -1)
            {
                MessageBox.Show("仕様・型式が選択されていません。先に、仕様・型式を選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            詳細品目選択Form form = new 詳細品目選択Form(msVesselItemCategoryNumber);
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            MsVesselItemVessel vesselItemVessel = form.SelectedVesselItemVessel;
            textBox船用品詳細品目.Text = vesselItemVessel.VesselItemName;
            labelMsVesselItemId.Text = vesselItemVessel.MsVesselItemID;

            詳細品目入力コントロール_Leave(delegate(OdThiShousaiItem item)
            {
                formDelegate.Set詳細品目ToModel(item);

                if (item == newShousaiItem && !odThiItem.OdThiShousaiItems.Contains(item) && textBox船用品詳細品目.Text.Length > 0)
                {
                    odThiItem.OdThiShousaiItems.Add(item);
                }
            });

            comboBox単位.Focus();
        }

        private void buttonカテゴリ選択_Click(object sender, EventArgs e)
        {
            int cnt = treeListView1.Nodes.Count;
            船用品カテゴリ選択Form form = new 船用品カテゴリ選択Form(this, cnt);

            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            MsVesselItemCategory vesselItemCategory = form.SelectedVesselItemCategory;
            msVesselItemCategoryNumber = vesselItemCategory.CategoryNumber;
            textBox品目.Text = vesselItemCategory.CategoryName;

            if (vesselItemCategory.CategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
            {
                //button特定品選択.Enabled = false;

                formDelegate.ChangeForm詳細Delegate(new FormDelegate_ペイント(this));
            }
            else
            {
                //button特定品選択.Enabled = true;

                // 2016.08.08 次期改造
                // 特定船用品選択Formは「特定品」ボタンからのみとするように改造
                //formDelegate.ChangeForm詳細Delegate(new FormDelegate_ペイント以外(this));
                formDelegate.ChangeForm詳細Delegate(new FormDelegate_ペイント以外(this));

                //List<string> selectedItemIdList = GetVesselItemIdList();

                //特定船用品選択Form form2 = new 特定船用品選択Form(SyncClient.同期Client.LOGIN_VESSEL.MsVesselID, vesselItemCategory, selectedItemIdList);
                //if (form2.ShowDialog() == DialogResult.OK)
                //{
                //    formDelegate.CreateShousai(form2.SelectedRows);
                //}
            }

            if (cnt > 1)
            {
                odThiItem.OdThiShousaiItems.Clear();
                UpdateTable();
                formDelegate.Clear詳細品目();
            }
            treeListView1.SelectedNode = treeListView1.Nodes[0];
            formDelegate.Focus詳細品目();
        }

        private void ペイント選択()
        {
            int cnt = treeListView1.Nodes.Count;

            MsVesselItemCategory vesselItemCategory = MsVesselItemCategorys.Where(obj => obj.CategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント)).First();
            msVesselItemCategoryNumber = vesselItemCategory.CategoryNumber;
            textBox品目.Text = vesselItemCategory.CategoryName;

            //button特定品選択.Enabled = false;

            formDelegate.ChangeForm詳細Delegate(new FormDelegate_ペイント(this));

            if (cnt > 1)
            {
                odThiItem.OdThiShousaiItems.Clear();
                UpdateTable();
                formDelegate.Clear詳細品目();
            }
            treeListView1.SelectedNode = treeListView1.Nodes[0];
            formDelegate.Focus詳細品目();
        }

        private void 手配品目Form_FormClosing(object sender, FormClosingEventArgs e)
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

        private void button仕様型式添付選択_Click(object sender, EventArgs e)
        {
            OdAttachFile attachFile = GetAttachFile();

            if (attachFile == null)
            {
                return;
            }
            AddAttachFile(attachFile);

            odThiItem.OdAttachFileID = attachFile.OdAttachFileID;
            odThiItem.OdAttachFileName = attachFile.FileName;

            textBox仕様型式添付.Text = attachFile.FileName;
        }

        private void button仕様型式添付削除_Click(object sender, EventArgs e)
        {
            RemoveAttachFile(odThiItem.OdAttachFileID);

            odThiItem.OdAttachFileID = null;
            odThiItem.OdAttachFileName = null;
            textBox仕様型式添付.Text = "";
        }

        private void button詳細添付選択_Click(object sender, EventArgs e)
        {
            TreeListViewNode node = treeListView1.SelectedNode;

            if (node != null && odThiShousaiItemNodes.ContainsKey(node))
            {
                OdAttachFile attachFile = GetAttachFile();

                if (attachFile == null)
                {
                    return;
                }
                AddAttachFile(attachFile);

                OdThiShousaiItem item = odThiShousaiItemNodes[node];
                item.OdAttachFileID = attachFile.OdAttachFileID;
                item.OdAttachFileName = attachFile.FileName;

                textBox詳細添付.Text = attachFile.FileName;

                int idx = node.FlatIndex; // 選択されたノードの位置
                UpdateTable();
                treeListView1.SelectedNode = treeListView1.Nodes[idx];
            }
        }

        private void button詳細添付削除_Click(object sender, EventArgs e)
        {
            TreeListViewNode node = treeListView1.SelectedNode;

            if (node != null && odThiShousaiItemNodes.ContainsKey(node))
            {
                OdThiShousaiItem item = odThiShousaiItemNodes[node];
                RemoveAttachFile(item.OdAttachFileID);
                item.OdAttachFileID = null;
                item.OdAttachFileName = null;

                textBox詳細添付.Text = "";
                UpdateTable();

                treeListView1.SelectedNode = node;
            }
        }

        public OdAttachFile GetAttachFile()
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Filter = "";// "ファイル(*.xls)|*.xls";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return null;
            }

            string filePath = openFileDialog1.FileName;
            byte[] bs = File.ReadAllBytes(filePath);


            if (NBaseCommon.FileView.CheckFileNameLength(filePath) == false)
            {
                MessageBox.Show("指定されたファイルのファイル名が長すぎます", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            int MaxSize = 1;
            SnParameter snParameter = SnParameter.GetRecord(同期Client.LOGIN_USER);
            MaxSize = int.Parse(snParameter.Prm3);
            try
            {
                if (bs.Length > (MaxSize * 1024 * 1024))
                {
                    MessageBox.Show("指定されたファイルのサイズが制限値 " + MaxSize + " MByteを超えています", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                if (bs.Length == 0)
                {
                    MessageBox.Show("指定されたファイルのサイズが 0 Byteです", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("対象ファイルを開けません：" + Ex.Message, "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            OdAttachFile attachFile = new OdAttachFile();
            attachFile.OdAttachFileID = System.Guid.NewGuid().ToString();
            attachFile.FileName = System.IO.Path.GetFileName(filePath);
            attachFile.Data = bs;

            return attachFile;
        }

        public void AddAttachFile(OdAttachFile addAttachFile)
        {
            bool isExists = false;
            foreach (OdAttachFile attachFile in 添付Files)
            {
                if (attachFile.OdAttachFileID == addAttachFile.OdAttachFileID)
                {
                    isExists = true;
                    break;
                }
            }
            if (isExists == false)
            {
                添付Files.Add(addAttachFile);
            }
        }
        public void RemoveAttachFile(string attachFileId)
        {
            // 2015.2.23 : DeleteFlagを立ててしまうとHonsen同期で文書ファイルが同期できなくなってしまうのでコメント
            //foreach (OdAttachFile attachFile in 添付Files)
            //{
            //    if (attachFile.OdAttachFileID == attachFileId)
            //    {
            //        attachFile.DeleteFlag = 1;
            //        break;
            //    }
            //}
        }

        //private void button特定品選択_Click(object sender, EventArgs e)
        //{
        //    if (msVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
        //        return;

        //    MsVesselItemCategory vesselItemCategory = MsVesselItemCategorys.Where(obj => obj.CategoryNumber == msVesselItemCategoryNumber).First();
            
        //    List<string> selectedItemIdList = GetVesselItemIdList();

        //    特定船用品選択Form form2 = new 特定船用品選択Form(SyncClient.同期Client.LOGIN_VESSEL.MsVesselID, vesselItemCategory, selectedItemIdList);
        //    if (form2.ShowDialog() == DialogResult.OK)
        //    {
        //        formDelegate.CreateShousai(form2.SelectedRows);
        //        UpdateTable();
        //    }
        //}

        public List<string> GetVesselItemIdList()
        {
            List<string> ret = new List<string>();

            foreach (OdThiShousaiItem shousaiItem in odThiItem.OdThiShousaiItems)
            {
                if (shousaiItem.MsVesselItemID != null && shousaiItem.MsVesselItemID != "" && shousaiItem.CancelFlag == 0)
                {
                    ret.Add(shousaiItem.MsVesselItemID);
                }
            }

            return ret;
        }
    }
}
