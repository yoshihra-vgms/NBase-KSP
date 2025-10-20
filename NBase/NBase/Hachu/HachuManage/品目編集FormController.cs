using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using NBaseData.DAC;
using Hachu.Models;
using Hachu.Utils;
using NBaseUtil;
using System.Drawing;
using System.IO;
using ServiceReferences.NBaseService;

namespace Hachu.HachuManage
{
    partial class 品目編集Form
    {
        abstract private class FormController
        {
            // Form
            protected 品目編集Form form;
            // 船ID
            protected int MsVesselID;
            // 詳細品目をエラー表示する際の最大文字数
            protected int Err文字表示数 = 20;
            //
            protected Point shousaiPanelLocation = new Point(22, 387); //new Point(22, 350);
            protected Size shousaiPanelSize = new Size(598, 202);

           
            // 小修理詳細品目マスタ（フリーメンテナンス）
            protected Dictionary<string, string> MsSsShousaiItemDic = null;

            abstract public void InitializeForm();
            abstract public void UpdateTreeListView();
            abstract public void BeforeSelect();
            abstract public void AfterSelect();
            abstract public void multiLineCombo詳細品目_Leave();
            abstract public void comboBox単位_Leave();
            abstract public void textBox備考_Leave();
            abstract public void button品目削除_Click();
            abstract public void button詳細品目削除_Click();
            abstract public void button登録_Click();
            abstract public void button選択_Click(MsVesselItemVessel vesselItemVessel);
            abstract public void SetFocus();
            abstract public void ValidateFields(ref string errMessage);
            abstract public bool ValidateShousai();
            abstract public void CreateShousai(List<DataGridViewRow> rows);
            abstract public List<string> GetVesselItemIdList();


            virtual public void textBox在庫数_Leave() { }
            virtual public void textBox依頼数_Leave() { }
            virtual public void textBox回答数_Leave() { }
            virtual public void textBox受領数_Leave() { }
            virtual public void textBox数量_Leave() { }
            virtual public void textBox発注数_Leave() { }
            virtual public void textBox単価_Leave() { }

            virtual public void button仕様型式添付選択_Click() { }
            virtual public void button仕様型式添付削除_Click() { }
            virtual public void button詳細添付選択_Click() { }
            virtual public void button詳細添付削除_Click() { }

            #region public void Initialize詳細品目(MultiLineCombo multiLineCombo詳細品目)
            //public void Initialize詳細品目(MultiLineCombo multiLineCombo詳細品目)
            //{
            //    List<MsSsShousaiItem> MsSsShousaiItems = null;
            //    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            //    {
            //        MsSsShousaiItems = serviceClient.MsSsShousaiItem_GetRecordByMsVesselID(NBaseCommon.Common.LoginUser, MsVesselID);
            //    }
            //    MsSsShousaiItemDic = new Dictionary<string, string>();
            //    foreach (MsSsShousaiItem item in MsSsShousaiItems)
            //    {
            //        multiLineCombo詳細品目.AutoCompleteCustomSource.Add(item.ShousaiItemName);
            //        if (MsSsShousaiItemDic.ContainsKey(item.ShousaiItemName) == false)
            //        {
            //            MsSsShousaiItemDic.Add(item.ShousaiItemName, item.ShousaiItemName);
            //        }
            //    }
            //}
            public void Initialize詳細品目(string ThiIraiSbtID, MultiLineCombo multiLineCombo詳細品目)
            {
                List<OdThiShousaiItem> odThiShousaiItems = null;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    odThiShousaiItems = serviceClient.OdThiShousaiItem_GetRecordByThiIraiSbtID(NBaseCommon.Common.LoginUser, ThiIraiSbtID, MsVesselID);
                }
                var sortedList = odThiShousaiItems.OrderBy(obj => obj.ShousaiItemName);

                MsSsShousaiItemDic = new Dictionary<string, string>();
                foreach (OdThiShousaiItem item in sortedList)
                {
                    if (MsSsShousaiItemDic.ContainsKey(item.ShousaiItemName) == false)
                    {
                        MsSsShousaiItemDic.Add(item.ShousaiItemName, item.ShousaiItemName);
                        multiLineCombo詳細品目.AutoCompleteCustomSource.Add(item.ShousaiItemName);
                    }
                }
            }
            #endregion
            #region public void Initialize単位(ComboBox comboBox単位)
            public void Initialize単位(ComboBox comboBox単位)
            {
                List<MsTani> MsTanis = null;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    MsTanis = serviceClient.MsTani_GetRecords(NBaseCommon.Common.LoginUser);
                }
                MsTani dmy = new MsTani();
                dmy.MsTaniID = "";
                dmy.TaniName = "";
                comboBox単位.Items.Add(dmy);
                foreach (MsTani t in MsTanis)
                {
                    comboBox単位.Items.Add(t);
                }
            }
            #endregion
            #region public void Init詳細()
            public void Init詳細()
            {
                form.treeList詳細品目.SetSelectedNode(0);
                SetFocus();
            }
            #endregion
            #region public void Set詳細()
            public void Set詳細()
            {
                TreeListViewNode node = form.treeList詳細品目.selectedNode();

                if (node != null)
                {
                    if (node.NextNode != null)
                    {
                        form.treeList詳細品目.SetSelectedNode(node.NextNode);
                    }
                    else
                    {
                        form.treeList詳細品目.SetSelectedNode(0);
                    }
                    SetFocus();
                }
            }
            #endregion
            #region protected void 詳細品目入力コントロール_Leave(Action<TreeListViewNode> action)
            protected void 詳細品目入力コントロール_Leave(Action<TreeListViewNode> action)
            {
                // 処理前の位置を保持する
                Point orgPoint = form.treeListView.GetScrollPos();

                form.treeListView.AfterSelect -= new LidorSystems.IntegralUI.ObjectEventHandler(form.treeListView_AfterSelect);

                TreeListViewNode node = form.treeList詳細品目.selectedNode();

                if (node != null)
                {
                    action(node);

                    int i = node.Index;
                    form.UpdateTreeListView();
                    form.treeListView.SelectedNode = form.treeListView.Nodes[i];
                }

                form.treeListView.AfterSelect += new LidorSystems.IntegralUI.ObjectEventHandler(form.treeListView_AfterSelect);
                               
                // 処理前の位置をセットする
                form.treeListView.SetScrollPos(orgPoint);
            }
            #endregion

            protected string GetShousaiName(string src)
            {
                // 前後のスペース、改行を除いた詳細品目名を返す
                string name = src.Trim();             
                name = name.Replace(Environment.NewLine, "");
                return name;
            }
            public string GetBikou(string src)
            {
                string bikou = src.Trim();
                bikou = bikou.Replace(Environment.NewLine, "");
                bikou = bikou.Replace("\t", "");
                return bikou;
            }

            // 2011.05.19: Add
            virtual public int GetSbt() { return -1; }
        }

        /// <summary>
        /// 手配依頼の品目追加時のコントローラ
        /// </summary>
        #region private class FormController手配依頼 : FormController
        private class FormController手配依頼 : FormController
        {
            // 手配依頼種別ID
            private string ThiIraiSbtID;
            // 手配依頼品目
            private OdThiItem thiItem;
            // 手配依頼詳細品目
            private Dictionary<TreeListViewNode, OdThiShousaiItem> odThiShousaiItemNodes =
                                                 new Dictionary<TreeListViewNode, OdThiShousaiItem>();

            private OdThiShousaiItem newShousaiItem;

            public FormController手配依頼(品目編集Form form, int vesselID,  string thiIraiSbtID, ref OdThiItem thiItem)
            {
                this.form = form;
                this.ThiIraiSbtID = thiIraiSbtID;
                this.MsVesselID = vesselID;
                this.thiItem = thiItem;
            }
            public override void InitializeForm()
            {
                form.Text = NBaseCommon.Common.WindowTitle("JM040202", "仕様・型式編集", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
                
                // 詳細品目TreeListView
                int noColumIndex = -1;
                object[,] columns = new object[,] {
                                            {"詳細品目", 250, null, null},
                                            {"単位", 50, null, null},
                                            {"在庫数", 52, null, HorizontalAlignment.Right},
                                            {"依頼数", 52, null, HorizontalAlignment.Right},
                                            {"査定数", 52, null, HorizontalAlignment.Right},
                                            {" 添付 ", 45, null, HorizontalAlignment.Center},
                                            {"備考（品名、規格等）", 150, null, null}
                                            };

                form.treeList詳細品目 = new ItemTreeListView品目編集(form.treeListView);
                form.treeList詳細品目.ThiIraiSbtID = ThiIraiSbtID;
                form.treeList詳細品目.SetColumns(noColumIndex, columns);
                form.treeList詳細品目.AddNodes(thiItem.OdThiShousaiItems, ref odThiShousaiItemNodes);
                CreateNewShousaiItem();
                form.treeList詳細品目.AddNode(newShousaiItem, ref odThiShousaiItemNodes);
                
                // 区分
                foreach (object obj in form.comboBox区分.Items)
                {
                    //if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                    //{
                    //    string itemStr = obj as string;
                    //    if (itemStr == thiItem.Header)
                    //    {
                    //        form.comboBox区分.SelectedItem = itemStr;
                    //        break;
                    //    }
                    //}
                    //else
                    //{
                    //    MsItemSbt itemSbt = obj as MsItemSbt;
                    //    if (itemSbt.MsItemSbtID == thiItem.MsItemSbtID)
                    //    {
                    //        form.comboBox区分.SelectedItem = itemSbt;
                    //        break;
                    //    }
                    //}
                    MsItemSbt itemSbt = obj as MsItemSbt;
                    if (itemSbt.MsItemSbtID == thiItem.MsItemSbtID)
                    {
                        form.comboBox区分.SelectedItem = itemSbt;
                        break;
                    }
                }

                // 仕様・型式
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                {
                    foreach (MsVesselItemCategory vic in form.MsVesselItemCategorys)
                    {
                        if (vic.CategoryName == thiItem.ItemName)
                        {
                            form.MsVesselItemCategoryNumber = vic.CategoryNumber;
                            form.buttonカテゴリ選択.Enabled = false;
                            break;
                        }
                    }
                }
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                {
                    form.textBox品目.Text = thiItem.ItemName;
                }
                else
                {
                    form.multiLineCombo品目.Text = thiItem.ItemName;
                }
                form.textBox仕様型式添付.Text = thiItem.OdAttachFileName;

                // 仕様・型式への添付ファイル
                form.label仕様型式添付.Visible = true;
                form.textBox仕様型式添付.Visible = true;
                form.button仕様型式添付選択.Visible = true;
                form.button仕様型式添付削除.Visible = true;

                // 手配依頼用の詳細品目パネルをVisibleに
                form.shousaiPanel手配依頼.Location = shousaiPanelLocation;
                form.shousaiPanel手配依頼.Size = shousaiPanelSize;
                form.shousaiPanel手配依頼.Visible = true;

                // 詳細品目パネルの初期化
                InitShousaiPanel();
                base.Initialize単位(form.comboBox単位_手配依頼);
            }
            public void InitShousaiPanel()
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    form.multiLineCombo詳細品目_手配依頼.Visible = false;

                    form.textBox詳細品目_手配依頼.Visible = true;
                    form.button選択_手配依頼.Visible = true;
                    form.button選択_手配依頼.Location = new Point(416, 6);
                    form.button詳細品目削除_手配依頼.Location = new Point(416, 35);
                }
                else
                {
                    form.multiLineCombo詳細品目_手配依頼.Visible = true;
                    form.multiLineCombo詳細品目_手配依頼.MaxLength = 500;
                    //base.Initialize詳細品目(form.multiLineCombo詳細品目_手配依頼);
                    base.Initialize詳細品目(ThiIraiSbtID, form.multiLineCombo詳細品目_手配依頼);

                    form.textBox詳細品目_手配依頼.Visible = false;
                    form.button選択_手配依頼.Visible = false;
                }
            }
            public override void UpdateTreeListView()
            {
                form.treeList詳細品目.NodesClear();
                odThiShousaiItemNodes.Clear();

                form.treeList詳細品目.AddNodes(thiItem.OdThiShousaiItems, ref odThiShousaiItemNodes);
                CreateNewShousaiItem();
                form.treeList詳細品目.AddNode(newShousaiItem, ref odThiShousaiItemNodes);
            }
            private void CreateNewShousaiItem()
            {
                newShousaiItem = new OdThiShousaiItem();
                newShousaiItem.OdThiShousaiItemID = "";
                newShousaiItem.ShousaiItemName = "";
                newShousaiItem.MsTaniID = "";
                newShousaiItem.MsTaniName = "";
                newShousaiItem.ZaikoCount = -1;
                newShousaiItem.Count = -1;
                newShousaiItem.Sateisu = -1;
                newShousaiItem.Bikou = "";
                newShousaiItem.OdAttachFileID = null;
                newShousaiItem.OdAttachFileName = null;
            }

            public override void BeforeSelect()
            {
                TreeListViewNode node = form.treeList詳細品目.selectedNode();

                if (node != null && odThiShousaiItemNodes.ContainsKey(node))
                {
                    OdThiShousaiItem item = odThiShousaiItemNodes[node];

                    Set詳細品目ToModel(item);
                    MsTani tani = form.comboBox単位_手配依頼.SelectedItem as MsTani;
                    item.MsTaniID = tani.MsTaniID;
                    item.MsTaniName = tani.TaniName;
                    try
                    {
                        item.ZaikoCount = Int32.Parse(form.textBox在庫数_手配依頼.Text);
                    }
                    catch
                    {
                    }
                    try
                    {
                        item.Count = Int32.Parse(form.textBox依頼数_手配依頼.Text);
                        item.Sateisu = item.Count;
                    }
                    catch
                    {
                    }
                    string bikou = GetBikou(form.textBox備考_手配依頼.Text);
                    if (bikou.Length > 0)
                        //item.Bikou = form.textBox備考_手配依頼.Text;
                        item.Bikou = StringUtils.Escape(form.textBox備考_手配依頼.Text);
                    else
                        item.Bikou = "";
                    item.OdAttachFileName = form.textBox詳細添付.Text;
                }
            }
            public override void AfterSelect()
            {
                TreeListViewNode node = form.treeList詳細品目.selectedNode();

                if (node != null && odThiShousaiItemNodes.ContainsKey(node))
                {
                    OdThiShousaiItem item = odThiShousaiItemNodes[node];

                    Set詳細品目ToControl(item);
                    foreach (object obj in form.comboBox単位_手配依頼.Items)
                    {
                        MsTani tani = (MsTani)obj;

                        if (tani.MsTaniID == item.MsTaniID)
                        {
                            form.comboBox単位_手配依頼.SelectedItem = tani;
                        }
                    }
                    if (item.ZaikoCount >= 0)
                    {
                        form.textBox在庫数_手配依頼.Text = item.ZaikoCount.ToString();
                    }
                    else
                    {
                        form.textBox在庫数_手配依頼.Text = "";
                    }
                    if (item.Count >= 0)
                    {
                        form.textBox依頼数_手配依頼.Text = item.Count.ToString();
                    }
                    else
                    {
                        form.textBox依頼数_手配依頼.Text = "";
                    }
                    string bikou = GetBikou(item.Bikou);
                    if (bikou.Length > 0)
                    {
                        form.textBox備考_手配依頼.Text = item.Bikou;
                    }
                    else
                    {
                        form.textBox備考_手配依頼.Text = "";
                    }
                    if (item.OdAttachFileName != null && item.OdAttachFileName.Length > 0)
                    {
                        form.textBox詳細添付.Text = item.OdAttachFileName;
                    }
                    else
                    {
                        form.textBox詳細添付.Text = "";
                    }

                    InitializeShousaiItemComponentsEnabled(item);
                }
            }
            private void InitializeShousaiItemComponentsEnabled(OdThiShousaiItem item)
            {
                if (thiItem.OdThiShousaiItems.Contains(item))
                {
                    form.button詳細品目削除_手配依頼.Enabled = true;
                }
                else
                {
                    form.button詳細品目削除_手配依頼.Enabled = false;
                }
            }
            public override void multiLineCombo詳細品目_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (odThiShousaiItemNodes.ContainsKey(node))
                    {
                        OdThiShousaiItem shousaiItem = odThiShousaiItemNodes[node];
                        Set詳細品目ToModel(shousaiItem);
                        string name = GetShousaiName(shousaiItem.ShousaiItemName);
                        if (shousaiItem == newShousaiItem && !thiItem.OdThiShousaiItems.Contains(shousaiItem) && name.Length > 0)
                        {
                            shousaiItem.OdThiShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                            thiItem.OdThiShousaiItems.Add(shousaiItem);
                        }
                    }
                });
            }
            public override void comboBox単位_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (odThiShousaiItemNodes.ContainsKey(node))
                    {
                        OdThiShousaiItem shousaiItem = odThiShousaiItemNodes[node];
                        MsTani tani = form.comboBox単位_手配依頼.SelectedItem as MsTani;
                        shousaiItem.MsTaniID = tani.MsTaniID;
                        shousaiItem.MsTaniName = tani.TaniName;
                    }
                });
            }
            public override void textBox在庫数_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (odThiShousaiItemNodes.ContainsKey(node))
                    {
                        OdThiShousaiItem shousaiItem = odThiShousaiItemNodes[node];
                        try
                        {
                            shousaiItem.ZaikoCount = Int32.Parse(form.textBox在庫数_手配依頼.Text);
                        }
                        catch
                        {
                        }
                    }
                });
            }
            public override void textBox依頼数_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (odThiShousaiItemNodes.ContainsKey(node))
                    {
                        OdThiShousaiItem shousaiItem = odThiShousaiItemNodes[node];
                        try
                        {
                            shousaiItem.Count = Int32.Parse(form.textBox依頼数_手配依頼.Text);
                            shousaiItem.Sateisu = shousaiItem.Count;
                        }
                        catch
                        {
                        }
                    }
                });
            }
            public override void textBox備考_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (odThiShousaiItemNodes.ContainsKey(node))
                    {
                        OdThiShousaiItem shousaiItem = odThiShousaiItemNodes[node];
                        string bikou = GetBikou(form.textBox備考_手配依頼.Text);
                        if (bikou.Length > 0)
                            //shousaiItem.Bikou = form.textBox備考_手配依頼.Text;
                            shousaiItem.Bikou = StringUtils.Escape(form.textBox備考_手配依頼.Text);
                        else
                            shousaiItem.Bikou = "";
                    }
                });
            }
            public override void button品目削除_Click()
            {
                thiItem.CancelFlag = 1;
                foreach (OdThiShousaiItem shousaiItem in thiItem.OdThiShousaiItems)
                {
                    shousaiItem.CancelFlag = 1;
                }
            }
            public override void button詳細品目削除_Click()
            {
                TreeListViewNode node = form.treeList詳細品目.selectedNode();

                if (node != null)
                {
                    OdThiShousaiItem item = odThiShousaiItemNodes[node];
                    item.CancelFlag = 1;
                }
                int idx = node.FlatIndex; // 選択されたノードの位置

                UpdateTreeListView();

                // 選択された行の次の行（削除されているので、先ほど選択した位置）を選択する
                form.treeList詳細品目.SetSelectedNode(idx);
            }
            public override void button登録_Click()
            {
                BuildOdThiItem();
            }
            public override void button選択_Click(MsVesselItemVessel vesselItemVessel)
            {
                form.textBox詳細品目_手配依頼.Text = vesselItemVessel.VesselItemName;
                form.labelMsVesselItemId_手配依頼.Text = vesselItemVessel.MsVesselItemID;
                foreach (object obj in form.comboBox単位_手配依頼.Items)
                {
                    MsTani tani = (MsTani)obj;

                    if (tani.MsTaniID == vesselItemVessel.MsTaniID)
                    {
                        form.comboBox単位_手配依頼.SelectedItem = tani;
                    }
                }

                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (odThiShousaiItemNodes.ContainsKey(node))
                    {
                        OdThiShousaiItem shousaiItem = odThiShousaiItemNodes[node];
                        Set詳細品目ToModel(shousaiItem);
                        if (shousaiItem == newShousaiItem && !thiItem.OdThiShousaiItems.Contains(shousaiItem) && form.textBox詳細品目_手配依頼.Text.Length > 0)
                        {
                            shousaiItem.OdThiShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                            thiItem.OdThiShousaiItems.Add(shousaiItem);
                        }
                    }
                });

                form.comboBox単位_手配依頼.Focus();
            }

            private void BuildOdThiItem()
            {
                if (form.EditMode == (int)EDIT_MODE.新規)
                {
                    thiItem.OdThiItemID = Hachu.Common.CommonDefine.新規ID();
                }

                thiItem.Header = form.comboBox区分.Text;

                //if (ThiIraiSbtID != NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                //{
                //    MsItemSbt itemSbt = null;
                //    if (form.comboBox区分.SelectedItem is MsItemSbt)
                //    {
                //        itemSbt = form.comboBox区分.SelectedItem as MsItemSbt;
                //    }
                //    if (itemSbt != null && itemSbt.MsItemSbtID.Length > 0)
                //    {
                //        thiItem.MsItemSbtID = itemSbt.MsItemSbtID;
                //        thiItem.MsItemSbtName = itemSbt.ItemSbtName;
                //    }
                //}
                MsItemSbt itemSbt = null;
                if (form.comboBox区分.SelectedItem is MsItemSbt)
                {
                    itemSbt = form.comboBox区分.SelectedItem as MsItemSbt;
                }
                if (itemSbt != null && itemSbt.MsItemSbtID.Length > 0)
                {
                    thiItem.MsItemSbtID = itemSbt.MsItemSbtID;
                    thiItem.MsItemSbtName = itemSbt.ItemSbtName;
                }

                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                {
                    thiItem.ItemName = form.textBox品目.Text;
                }
                else
                {
                    thiItem.ItemName = StringUtils.Escape(form.multiLineCombo品目.Text);

                    if (form.MsShoushuriItemDic.ContainsKey(thiItem.ItemName) == false)
                    {
                        thiItem.SaveDB = true;
                    }
                }

                BuildOdThiShousaiItems();
            }
            private void BuildOdThiShousaiItems()
            {
                foreach (OdThiShousaiItem shousaiItem in thiItem.OdThiShousaiItems)
                {
                    if (shousaiItem.OdThiShousaiItemID.Length == 0)
                    {
                        shousaiItem.OdThiShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                    }
                    if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID)
                    {
                        if (MsSsShousaiItemDic.ContainsKey(shousaiItem.ShousaiItemName) == false)
                        {
                            shousaiItem.SaveDB = true;
                        }
                    }
                }
            }
            public override void SetFocus()
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    if (form.textBox詳細品目_手配依頼.Visible == false)
                    {
                        InitShousaiPanel();
                    }
                    form.textBox詳細品目_手配依頼.Focus();
                }
                else
                {
                    if (form.multiLineCombo詳細品目_手配依頼.Visible == false)
                    {
                        InitShousaiPanel();
                    }
                    form.multiLineCombo詳細品目_手配依頼.Focus();
                }
            }
            private void Set詳細品目ToControl(OdThiShousaiItem item)
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    form.textBox詳細品目_手配依頼.Text = item.ShousaiItemName;
                    form.labelMsVesselItemId_手配依頼.Text = item.MsVesselItemID;
                }
                //else
                //{
                //    form.multiLineCombo詳細品目_手配依頼.Text = item.ShousaiItemName;
                //}
                else if (item.MsVesselItemID != null && item.MsVesselItemID.Length > 0)
                {
                    form.multiLineCombo詳細品目_手配依頼.Text = item.ShousaiItemName;
                    form.labelMsVesselItemId_手配依頼.Text = item.MsVesselItemID;

                    form.multiLineCombo詳細品目_手配依頼.ReadOnly = true;
                }
                else
                {
                    form.multiLineCombo詳細品目_手配依頼.Text = item.ShousaiItemName;
                    form.labelMsVesselItemId_手配依頼.Text = "";

                    form.multiLineCombo詳細品目_手配依頼.ReadOnly = false;
                }
            }
            public void Set詳細品目ToModel(OdThiShousaiItem item)
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    item.ShousaiItemName = form.textBox詳細品目_手配依頼.Text;
                    item.MsVesselItemID = form.labelMsVesselItemId_手配依頼.Text;
                }
                //else
                //{
                //    //item.ShousaiItemName = form.multiLineCombo詳細品目_手配依頼.Text;
                //    item.ShousaiItemName = StringUtils.Escape(form.multiLineCombo詳細品目_手配依頼.Text);
                //}
                else if (form.labelMsVesselItemId_手配依頼.Text.Length > 0)
                {
                    item.ShousaiItemName = StringUtils.Escape(form.multiLineCombo詳細品目_手配依頼.Text);
                    item.MsVesselItemID = form.labelMsVesselItemId_手配依頼.Text;
                }
                else
                {
                    item.ShousaiItemName = StringUtils.Escape(form.multiLineCombo詳細品目_手配依頼.Text);
                }
            }

            public override bool ValidateShousai()
            {
                bool ret = true;

                TreeListViewNode node = form.treeList詳細品目.selectedNode();
                if (node != null)
                {
                    OdThiShousaiItem item = odThiShousaiItemNodes[node];
                    string name = item.ShousaiItemName;
                    name = name.Trim();
                    name = name.Replace(Environment.NewLine, "");
                    if (name.Length == 0)
                    {
                        ret = false;
                    }
                }
                return ret;
            }

            public override void ValidateFields(ref string errMessage)
            {
                // 詳細品目
                for (int i = 0; i < thiItem.OdThiShousaiItems.Count; i++)
                {
                    OdThiShousaiItem si = thiItem.OdThiShousaiItems[i];

                    if (si.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                    {
                        continue;
                    }
                    // (2009.09.21: aki <入力時にブロックしているのでここでは判定の必要なし>)
                    //if (si.ShousaiItemName.Length == 0)
                    //{
                    //    errMessage += "・No. " + (i + 1) + " の詳細品目を入力して下さい\n";
                    //}
                    //else if (si.ShousaiItemName.Length > 500)
                    //{
                    //    errMessage += "・No. " + (i + 1) + " の詳細品目は500文字以下で入力して下さい\n";
                    //}
                    string name = si.ShousaiItemName.Replace(Environment.NewLine, " ").Trim();
                    if (name.Length > Err文字表示数)
                    {
                        name = name.Substring(0, Err文字表示数);
                        name = name + "…";
                    }
                    if (si.ZaikoCount < 0)
                    {
                        errMessage += "・「 " + name + " 」の在庫数を入力して下さい\n";
                    }
                    if (si.Count < 0)
                    {
                        errMessage += "・「 " + name + " 」の依頼数を入力して下さい\n";
                    }
                    if (si.Bikou.Length > 500)
                    {
                        errMessage += "・「 " + name + " 」の備考（品名、規格等）は500文字以下で入力して下さい\n";
                    }
                }

            }


            public override void button仕様型式添付選択_Click()
            {
                OdAttachFile attachFile = GetAttachFile();

                if (attachFile == null)
                {
                    return;
                }
                AddAttachFile(attachFile);

                thiItem.OdAttachFileID = attachFile.OdAttachFileID;
                thiItem.OdAttachFileName = attachFile.FileName;

                form.textBox仕様型式添付.Text = attachFile.FileName;
            }

            public override void button仕様型式添付削除_Click()
            {
                RemoveAttachFile(thiItem.OdAttachFileID);

                thiItem.OdAttachFileID = null;
                thiItem.OdAttachFileName = null;
                form.textBox仕様型式添付.Text = "";
            }
 
            public override void button詳細添付選択_Click()
            {
                TreeListViewNode node = form.treeList詳細品目.selectedNode();
                if (node != null)
                {
                    OdAttachFile attachFile = GetAttachFile();

                    if (attachFile == null)
                    {
                        return;
                    }
                    AddAttachFile(attachFile);

                    OdThiShousaiItem item = odThiShousaiItemNodes[node];

                    // 2012.05.09 : Add 4Lines
                    if (item.OdAttachFileID != null && item.OdAttachFileID.Length > 0)
                    {
                        RemoveAttachFile(item.OdAttachFileID);
                    }
                    //<--

                    item.OdAttachFileID = attachFile.OdAttachFileID;
                    item.OdAttachFileName = attachFile.FileName;

                    form.textBox詳細添付.Text = attachFile.FileName;

                    int idx = node.FlatIndex; // 選択されたノードの位置
                    UpdateTreeListView();
                    form.treeList詳細品目.SetSelectedNode(idx);
                }

            }

            public override void button詳細添付削除_Click()
            {
                TreeListViewNode node = form.treeList詳細品目.selectedNode();

                if (node != null)
                {
                    OdThiShousaiItem item = odThiShousaiItemNodes[node];

                    RemoveAttachFile(item.OdAttachFileID);

                    item.OdAttachFileID = null;
                    item.OdAttachFileName = null;
                    form.textBox詳細添付.Text = "";

                    int idx = node.FlatIndex; // 選択されたノードの位置
                    UpdateTreeListView();
                    form.treeList詳細品目.SetSelectedNode(idx);
                }
            }

            public OdAttachFile GetAttachFile()
            {
                form.openFileDialog1.CheckFileExists = true;
                form.openFileDialog1.RestoreDirectory = true;
                form.openFileDialog1.Filter = "";// "ファイル(*.xls)|*.xlsx";
                form.openFileDialog1.FilterIndex = 1;
                form.openFileDialog1.FileName = "";

                if (form.openFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    return null;
                }

                string filePath = form.openFileDialog1.FileName;
                byte[] bs = File.ReadAllBytes(filePath);

                if (NBaseCommon.FileView.CheckFileNameLength(filePath) == false)
                {
                    MessageBox.Show("指定されたファイルのファイル名が長すぎます", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                int MaxSize = 1;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    SnParameter snParameter = serviceClient.SnParameter_GetRecord(NBaseCommon.Common.LoginUser);
                    MaxSize = int.Parse(snParameter.Prm3);
                }
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
                attachFile.OdAttachFileID = Hachu.Common.CommonDefine.新規ID(true);
                attachFile.FileName = System.IO.Path.GetFileName(filePath);
                attachFile.Data = bs;

                return attachFile;
            }

            public void AddAttachFile(OdAttachFile addAttachFile)
            {
                bool isExists = false;
                foreach (OdAttachFile attachFile in form.添付Files)
                {
                    if (attachFile.OdAttachFileID == addAttachFile.OdAttachFileID)
                    {
                        isExists = true;
                        break;
                    }
                }
                if (isExists == false)
                {
                    form.添付Files.Add(addAttachFile);
                }
            }
            public void RemoveAttachFile(string attachFileId)
            {
                foreach (OdAttachFile attachFile in form.添付Files)
                {
                    if (attachFile.OdAttachFileID == attachFileId)
                    {
                        if (Hachu.Common.CommonDefine.Is新規(attachFileId))
                        {
                            form.添付Files.Remove(attachFile);
                        }
                        // 2015.2.23 : DeleteFlagを立ててしまうとHonsen同期で文書ファイルが同期できなくなってしまうのでコメント
                        //else
                        //{
                        //    attachFile.DeleteFlag = 1;
                        //}
                        break;
                    }
                }
            }

            public override void CreateShousai(List<DataGridViewRow> rows)
            {
                foreach (DataGridViewRow row in rows)
                {
                    MsVesselItemVessel vesselItem = row.Cells["obj"].Value as MsVesselItemVessel;
                    OdThiShousaiItem shousaiItem = new OdThiShousaiItem();
                    shousaiItem.OdThiShousaiItemID = "";
                    shousaiItem.ShousaiItemName = "";
                    shousaiItem.MsTaniID = "";
                    shousaiItem.MsTaniName = "";
                    shousaiItem.ZaikoCount = -1;
                    shousaiItem.Count = -1;
                    shousaiItem.Sateisu = -1;
                    shousaiItem.Bikou = "";
                    shousaiItem.OdAttachFileID = null;
                    shousaiItem.OdAttachFileName = null;

                    // 特定船用品をセット
                    shousaiItem.OdThiShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                    shousaiItem.MsVesselItemID = vesselItem.MsVesselItemID;
                    shousaiItem.ShousaiItemName = vesselItem.VesselItemName;
                    shousaiItem.MsTaniID = vesselItem.MsTaniID;
                    shousaiItem.MsTaniName = vesselItem.MsTaniName;

                    int zaikoCount = 0;
                    if (row.Cells["在庫数"] != null)
                    {
                        if (int.TryParse((row.Cells["在庫数"].Value as string), out zaikoCount))
                        {
                            shousaiItem.ZaikoCount = zaikoCount;
                        }
                    }
                    int count = 0;
                    if (row.Cells["数量"].Value != null)
                    {
                        if (int.TryParse((row.Cells["数量"].Value as string), out count))
                        {
                            shousaiItem.Count = count;
                            shousaiItem.Sateisu = count;
                        }
                    }

                    shousaiItem.Bikou = vesselItem.Bikou;

                    thiItem.OdThiShousaiItems.Add(shousaiItem);
                }
            }

            public override List<string> GetVesselItemIdList()
            {
                List<string> ret = new List<string>();

                foreach (OdThiShousaiItem shousaiItem in thiItem.OdThiShousaiItems)
                {
                    if (shousaiItem.MsVesselItemID != null && shousaiItem.MsVesselItemID != "" && shousaiItem.CancelFlag == 0)
                    {
                        ret.Add(shousaiItem.MsVesselItemID);
                    }
                }

                return ret;
            }
        }
        #endregion

        /// <summary>
        /// 見積回答の品目追加時のコントローラ
        /// </summary>
        #region private class FormController見積回答 : FormController
        private class FormController見積回答 : FormController
        {
            // 手配依頼種別ID
            private string ThiIraiSbtID;
            // 見積回答品目
            private OdMkItem mkItem;
            // 見積回答詳細品目
            private Dictionary<TreeListViewNode, OdMkShousaiItem> OdMkShousaiItemNodes =
                                                 new Dictionary<TreeListViewNode, OdMkShousaiItem>();

            private OdMkShousaiItem newShousaiItem;

            public FormController見積回答(品目編集Form form, int vesselID, string thiIraiSbtID, ref OdMkItem mkItem)
            {
                this.form = form;
                this.ThiIraiSbtID = thiIraiSbtID;
                this.MsVesselID = vesselID;
                this.mkItem = mkItem;
            }
            public override void InitializeForm()
            {
                form.Text = NBaseCommon.Common.WindowTitle("番号不明", "仕様・型式編集", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

                // 詳細品目TreeListView
                int noColumIndex = -1;
                object[,] columns = new object[,] {
                                                   {"詳細品目", 275, null, null},
                                                   {"単位", 45, null, null},
                                                   {"見積数", 52, null, HorizontalAlignment.Right},
                                                   {"回答数", 52, null, HorizontalAlignment.Right},
                                                   {"単価", 65, null, HorizontalAlignment.Right},
                                                   {"金額", 80, null, HorizontalAlignment.Right},
                                                   {"備考（品名、規格等）", 200, null, null}
                                                 };

                form.treeList詳細品目 = new ItemTreeListView品目編集(form.treeListView);
                form.treeList詳細品目.SetColumns(noColumIndex, columns);
                form.treeList詳細品目.AddNodes(mkItem.OdMkShousaiItems, ref OdMkShousaiItemNodes);
                CreateNewShousaiItem();
                form.treeList詳細品目.AddNode(newShousaiItem, ref OdMkShousaiItemNodes);

                // 区分
                foreach (object obj in form.comboBox区分.Items)
                {
                    //if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                    //{
                    //    string itemStr = obj as string;
                    //    if (itemStr == mkItem.Header)
                    //    {
                    //        form.comboBox区分.SelectedItem = itemStr;
                    //        break;
                    //    }
                    //}
                    //else
                    //{
                    //    MsItemSbt itemSbt = obj as MsItemSbt;
                    //    if (itemSbt.MsItemSbtID == mkItem.MsItemSbtID)
                    //    {
                    //        form.comboBox区分.SelectedItem = itemSbt;
                    //        break;
                    //    }
                    //}
                    MsItemSbt itemSbt = obj as MsItemSbt;
                    if (itemSbt.MsItemSbtID == mkItem.MsItemSbtID)
                    {
                        form.comboBox区分.SelectedItem = itemSbt;
                        break;
                    }
                }

                // 仕様・型式
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                {
                    foreach (MsVesselItemCategory vic in form.MsVesselItemCategorys)
                    {
                        if (vic.CategoryName == mkItem.ItemName)
                        {
                            form.MsVesselItemCategoryNumber = vic.CategoryNumber;
                            form.buttonカテゴリ選択.Enabled = false;
                            break;
                        }
                    }
                }
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                {
                    form.textBox品目.Text = mkItem.ItemName;
                }
                else
                {
                    form.multiLineCombo品目.Text = mkItem.ItemName;
                }
                // 見積回答用の詳細品目パネルをVisibleに
                form.shousaiPanel見積回答.Location = shousaiPanelLocation;
                form.shousaiPanel見積回答.Size = shousaiPanelSize;
                form.shousaiPanel見積回答.Visible = true;

                // 詳細品目パネルの初期化
                InitShousaiPanel();
                base.Initialize単位(form.comboBox単位_見積回答);
            }
            public void InitShousaiPanel()
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    form.multiLineCombo詳細品目_見積回答.Visible = false;

                    form.textBox詳細品目_見積回答.Visible = true;
                    form.button選択_見積回答.Visible = true;
                    form.button選択_見積回答.Location = new Point(416, 6);
                    form.button詳細品目削除_見積回答.Location = new Point(416, 35);
                }
                else
                {
                    form.multiLineCombo詳細品目_見積回答.Visible = true;
                    form.multiLineCombo詳細品目_見積回答.MaxLength = 500;
                    //base.Initialize詳細品目(form.multiLineCombo詳細品目_見積回答);
                    base.Initialize詳細品目(ThiIraiSbtID, form.multiLineCombo詳細品目_見積回答);

                    form.textBox詳細品目_見積回答.Visible = false;
                    form.button選択_見積回答.Visible = false;
                }
            }
            public override void UpdateTreeListView()
            {
                form.treeList詳細品目.NodesClear();
                OdMkShousaiItemNodes.Clear();

                form.treeList詳細品目.AddNodes(mkItem.OdMkShousaiItems, ref OdMkShousaiItemNodes);
                CreateNewShousaiItem();
                form.treeList詳細品目.AddNode(newShousaiItem, ref OdMkShousaiItemNodes);
            }
            private void CreateNewShousaiItem()
            {
                newShousaiItem = new OdMkShousaiItem();
                newShousaiItem.OdMkShousaiItemID = "";
                newShousaiItem.ShousaiItemName = "";
                newShousaiItem.MsTaniID = "";
                newShousaiItem.MsTaniName = "";
                newShousaiItem.OdMmShousaiItemCount = -1;
                newShousaiItem.Count = -1;
                newShousaiItem.Tanka = -1;
                newShousaiItem.Bikou = "";
            }

            public override void BeforeSelect()
            {
                TreeListViewNode node = form.treeList詳細品目.selectedNode();

                if (node != null && OdMkShousaiItemNodes.ContainsKey(node))
                {
                    OdMkShousaiItem item = OdMkShousaiItemNodes[node];

                    Set詳細品目ToModel(item);
                    MsTani tani = form.comboBox単位_見積回答.SelectedItem as MsTani;
                    item.MsTaniID = tani.MsTaniID;
                    item.MsTaniName = tani.TaniName;
                    try
                    {
                        item.Count = Int32.Parse(form.textBox回答数_見積回答.Text);
                    }
                    catch
                    {
                    }
                    try
                    {
                        item.Tanka = decimal.Parse(form.textBox単価_見積回答.Text);
                    }
                    catch
                    {
                    }
                    //item.Bikou = form.textBox備考_見積回答.Text;
                    item.Bikou = StringUtils.Escape(form.textBox備考_見積回答.Text);
                }
            }
            public override void AfterSelect()
            {
                TreeListViewNode node = form.treeList詳細品目.selectedNode();

                if (node != null && OdMkShousaiItemNodes.ContainsKey(node))
                {
                    OdMkShousaiItem item = OdMkShousaiItemNodes[node];

                    Set詳細品目ToControl(item);
                    foreach (object obj in form.comboBox単位_見積回答.Items)
                    {
                        MsTani tani = (MsTani)obj;

                        if (tani.MsTaniID == item.MsTaniID)
                        {
                            form.comboBox単位_見積回答.SelectedItem = tani;
                        }
                    }
                    if (item.Count >= 0)
                    {
                        form.textBox回答数_見積回答.Text = item.Count.ToString();
                    }
                    else
                    {
                        form.textBox回答数_見積回答.Text = "";
                    }
                    if (item.Tanka >= 0)
                    {
                        form.textBox単価_見積回答.Text = item.Tanka.ToString();
                    }
                    else
                    {
                        form.textBox単価_見積回答.Text = "";
                    }
                    form.textBox備考_見積回答.Text = item.Bikou.ToString();

                    InitializeShousaiItemComponentsEnabled(item);
                }
            }
            private void InitializeShousaiItemComponentsEnabled(OdMkShousaiItem item)
            {
                if (mkItem.OdMkShousaiItems.Contains(item))
                {
                    form.button詳細品目削除_見積回答.Enabled = true;
                }
                else
                {
                    form.button詳細品目削除_見積回答.Enabled = false;
                }
            }
            public override void multiLineCombo詳細品目_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdMkShousaiItemNodes.ContainsKey(node))
                    {
                        OdMkShousaiItem shousaiItem = OdMkShousaiItemNodes[node];
                        Set詳細品目ToModel(shousaiItem);
                        string name = GetShousaiName(shousaiItem.ShousaiItemName);
                        //if (shousaiItem == newShousaiItem && !mkItem.OdMkShousaiItems.Contains(shousaiItem) && form.multiLineCombo詳細品目_見積回答.Text.Length > 0)
                        if (shousaiItem == newShousaiItem && !mkItem.OdMkShousaiItems.Contains(shousaiItem) && name.Length > 0)
                        {
                            shousaiItem.OdMkShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                            mkItem.OdMkShousaiItems.Add(shousaiItem);
                        }
                    }
                });
            }
            public override void comboBox単位_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdMkShousaiItemNodes.ContainsKey(node))
                    {
                        OdMkShousaiItem shousaiItem = OdMkShousaiItemNodes[node];
                        MsTani tani = form.comboBox単位_見積回答.SelectedItem as MsTani;
                        shousaiItem.MsTaniID = tani.MsTaniID;
                        shousaiItem.MsTaniName = tani.TaniName;
                    }
                });
            }
            public override void textBox回答数_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdMkShousaiItemNodes.ContainsKey(node))
                    {
                        OdMkShousaiItem shousaiItem = OdMkShousaiItemNodes[node];
                        try
                        {
                            shousaiItem.Count = Int32.Parse(form.textBox回答数_見積回答.Text);
                        }
                        catch
                        {
                        }
                    }
                });
            }
            public override void textBox単価_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdMkShousaiItemNodes.ContainsKey(node))
                    {
                        OdMkShousaiItem shousaiItem = OdMkShousaiItemNodes[node];
                        try
                        {
                            shousaiItem.Tanka = Int32.Parse(form.textBox単価_見積回答.Text);
                        }
                        catch
                        {
                        }
                    }
                });
            }
            public override void textBox備考_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdMkShousaiItemNodes.ContainsKey(node))
                    {
                        OdMkShousaiItem shousaiItem = OdMkShousaiItemNodes[node];
                        //shousaiItem.Bikou = form.textBox備考_見積回答.Text;
                        shousaiItem.Bikou = StringUtils.Escape(form.textBox備考_見積回答.Text);
                    }
                });
            }
            public override void button品目削除_Click()
            {
                mkItem.CancelFlag = 1;
                foreach (OdMkShousaiItem shousaiItem in mkItem.OdMkShousaiItems)
                {
                    shousaiItem.CancelFlag = 1;
                }
            }
            public override void button詳細品目削除_Click()
            {
                TreeListViewNode node = form.treeList詳細品目.selectedNode();

                if (node != null)
                {
                    OdMkShousaiItem item = OdMkShousaiItemNodes[node];
                    item.CancelFlag = 1;
                }
                int idx = node.FlatIndex; // 選択されたノードの位置

                UpdateTreeListView();

                // 選択された行の次の行（削除されているので、先ほど選択した位置）を選択する
                form.treeList詳細品目.SetSelectedNode(idx); 
            }
            public override void button登録_Click()
            {
                BuildOdMkItem();
            }
            public override void button選択_Click(MsVesselItemVessel vesselItemVessel)
            {
                form.textBox詳細品目_見積回答.Text = vesselItemVessel.VesselItemName;
                form.labelMsVesselItemId_見積回答.Text = vesselItemVessel.MsVesselItemID;
                foreach (object obj in form.comboBox単位_見積回答.Items)
                {
                    MsTani tani = (MsTani)obj;

                    if (tani.MsTaniID == vesselItemVessel.MsTaniID)
                    {
                        form.comboBox単位_見積回答.SelectedItem = tani;
                    }
                }

                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdMkShousaiItemNodes.ContainsKey(node))
                    {
                        OdMkShousaiItem shousaiItem = OdMkShousaiItemNodes[node];
                        Set詳細品目ToModel(shousaiItem);
                        if (shousaiItem == newShousaiItem && !mkItem.OdMkShousaiItems.Contains(shousaiItem) && form.textBox詳細品目_見積回答.Text.Length > 0)
                        {
                            shousaiItem.OdMkShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                            mkItem.OdMkShousaiItems.Add(shousaiItem);
                        }
                    }
                });

                form.comboBox単位_見積回答.Focus();
            }

            private void BuildOdMkItem()
            {
                if (form.EditMode == (int)EDIT_MODE.新規)
                {
                    mkItem.OdMkItemID = Hachu.Common.CommonDefine.新規ID();
                }

                mkItem.Header = form.comboBox区分.Text;

                //if (ThiIraiSbtID != NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                //{
                //    MsItemSbt itemSbt = null;
                //    if (form.comboBox区分.SelectedItem is MsItemSbt)
                //    {
                //        itemSbt = form.comboBox区分.SelectedItem as MsItemSbt;
                //    }
                //    if (itemSbt != null && itemSbt.MsItemSbtID.Length > 0)
                //    {
                //        mkItem.MsItemSbtID = itemSbt.MsItemSbtID;
                //        mkItem.MsItemSbtName = itemSbt.ItemSbtName;
                //    }
                //}
                MsItemSbt itemSbt = null;
                if (form.comboBox区分.SelectedItem is MsItemSbt)
                {
                    itemSbt = form.comboBox区分.SelectedItem as MsItemSbt;
                }
                if (itemSbt != null && itemSbt.MsItemSbtID.Length > 0)
                {
                    mkItem.MsItemSbtID = itemSbt.MsItemSbtID;
                    mkItem.MsItemSbtName = itemSbt.ItemSbtName;
                }

                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                {
                    mkItem.ItemName = form.textBox品目.Text;
                }
                else
                {
                    //mkItem.ItemName = form.multiLineCombo品目.Text;
                    mkItem.ItemName = StringUtils.Escape(form.multiLineCombo品目.Text);

                    if (form.MsShoushuriItemDic.ContainsKey(mkItem.ItemName) == false)
                    {
                        mkItem.SaveDB = true;
                    }
                }

                BuildOdMkShousaiItems();
            }
            private void BuildOdMkShousaiItems()
            {
                foreach (OdMkShousaiItem shousaiItem in mkItem.OdMkShousaiItems)
                {
                    if (shousaiItem.OdMkShousaiItemID.Length == 0)
                    {
                        shousaiItem.OdMkShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                    }
                    if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID)
                    {
                        if (MsSsShousaiItemDic.ContainsKey(shousaiItem.ShousaiItemName) == false)
                        {
                            shousaiItem.SaveDB = true;
                        }
                    }
                }
            }
            public override void SetFocus()
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    if (form.textBox詳細品目_見積回答.Visible == false)
                    {
                        InitShousaiPanel();
                    }
                    form.textBox詳細品目_見積回答.Focus();
                }
                else
                {
                    if (form.multiLineCombo詳細品目_見積回答.Visible == false)
                    {
                        InitShousaiPanel();
                    }
                    form.multiLineCombo詳細品目_見積回答.Focus();
                }
            }
            private void Set詳細品目ToControl(OdMkShousaiItem item)
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    form.textBox詳細品目_見積回答.Text = item.ShousaiItemName;
                    form.labelMsVesselItemId_見積回答.Text = item.MsVesselItemID;
                }
                //else
                //{
                //    form.multiLineCombo詳細品目_見積回答.Text = item.ShousaiItemName;
                //}
                else if (item.MsVesselItemID != null && item.MsVesselItemID.Length > 0)
                {
                    form.multiLineCombo詳細品目_見積回答.Text = item.ShousaiItemName;
                    form.labelMsVesselItemId_見積回答.Text = item.MsVesselItemID;

                    form.multiLineCombo詳細品目_見積回答.ReadOnly = true;
                }
                else
                {
                    form.multiLineCombo詳細品目_見積回答.Text = item.ShousaiItemName;
                    form.labelMsVesselItemId_見積回答.Text = "";

                    form.multiLineCombo詳細品目_見積回答.ReadOnly = false;
                }
            }
            public void Set詳細品目ToModel(OdMkShousaiItem item)
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    item.ShousaiItemName = form.textBox詳細品目_見積回答.Text;
                    item.MsVesselItemID = form.labelMsVesselItemId_見積回答.Text;
                }
                //else
                //{
                //    //item.ShousaiItemName = form.multiLineCombo詳細品目_見積回答.Text;
                //    item.ShousaiItemName = StringUtils.Escape(form.multiLineCombo詳細品目_見積回答.Text);
                //}
                else if (form.labelMsVesselItemId_見積回答.Text.Length > 0)
                {
                    item.ShousaiItemName = StringUtils.Escape(form.multiLineCombo詳細品目_見積回答.Text);
                    item.MsVesselItemID = form.labelMsVesselItemId_見積回答.Text;
                }
                else
                {
                    item.ShousaiItemName = StringUtils.Escape(form.multiLineCombo詳細品目_見積回答.Text);
                }
            }

            public override bool ValidateShousai()
            {
                bool ret = true;

                TreeListViewNode node = form.treeList詳細品目.selectedNode();
                if (node != null)
                {
                    OdMkShousaiItem item = OdMkShousaiItemNodes[node];
                    string name = item.ShousaiItemName;
                    name = name.Trim();
                    name = name.Replace(Environment.NewLine, "");
                    if (name.Length == 0)
                    {
                        ret = false;
                    }
                }
                return ret;
            }

            public override void ValidateFields(ref string errMessage)
            {
                // 詳細品目
                for (int i = 0; i < mkItem.OdMkShousaiItems.Count; i++)
                {
                    OdMkShousaiItem si = mkItem.OdMkShousaiItems[i];

                    if (si.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                    {
                        continue;
                    }
                    // (2009.09.21: aki <入力時にブロックしているのでここでは判定の必要なし>)
                    //if (si.ShousaiItemName.Length == 0)
                    //{
                    //    errMessage += "・No. " + (i + 1) + " の詳細品目を入力して下さい\n";
                    //}
                    //else if (si.ShousaiItemName.Length > 500)
                    //{
                    //    errMessage += "・No. " + (i + 1) + " の詳細品目は500文字以下で入力して下さい\n";
                    //}
                    string name = si.ShousaiItemName.Trim().Replace(Environment.NewLine, " ");
                    if (name.Length > Err文字表示数)
                    {
                        name = name.Substring(0, Err文字表示数);
                        name = name + "…";
                    }
                    if (si.Count < 0)
                    {
                        errMessage += "・「 " + name + " 」の回答数を入力して下さい\n";
                    }
                    if (si.Tanka < 0)
                    {
                        errMessage += "・「 " + name + " 」の単価を入力して下さい\n";
                    }
                    if (si.Bikou.Length > 500)
                    {
                        errMessage += "・「 " + name + " 」の備考（品名、規格等）は500文字以下で入力して下さい\n";
                    }
                }
            }

            public override void CreateShousai(List<DataGridViewRow> rows)
            {
                foreach (DataGridViewRow row in rows)
                {
                    MsVesselItemVessel vesselItem = row.Cells["obj"].Value as MsVesselItemVessel;
                    OdMkShousaiItem shousaiItem = new OdMkShousaiItem();
                    shousaiItem.OdMkShousaiItemID = "";
                    shousaiItem.ShousaiItemName = "";
                    shousaiItem.MsTaniID = "";
                    shousaiItem.MsTaniName = "";
                    shousaiItem.OdMmShousaiItemCount = -1;
                    shousaiItem.Count = -1;
                    shousaiItem.Bikou = "";
                    shousaiItem.OdAttachFileID = null;

                    // 特定船用品をセット
                    shousaiItem.OdMkShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                    shousaiItem.MsVesselItemID = vesselItem.MsVesselItemID;
                    shousaiItem.ShousaiItemName = vesselItem.VesselItemName;
                    shousaiItem.MsTaniID = vesselItem.MsTaniID;
                    shousaiItem.MsTaniName = vesselItem.MsTaniName;

                    int count = 0;
                    if (row.Cells["数量"].Value != null)
                    {
                        if (int.TryParse((row.Cells["数量"].Value as string), out count))
                        {
                            shousaiItem.Count = count;
                        }
                    }

                    shousaiItem.Bikou = vesselItem.Bikou;

                    mkItem.OdMkShousaiItems.Add(shousaiItem);
                }
            }
            public override List<string> GetVesselItemIdList()
            {
                List<string> ret = new List<string>();

                foreach (OdMkShousaiItem shousaiItem in mkItem.OdMkShousaiItems)
                {
                    if (shousaiItem.MsVesselItemID != null && shousaiItem.MsVesselItemID != "" && shousaiItem.CancelFlag == 0)
                    {
                        ret.Add(shousaiItem.MsVesselItemID);
                    }
                }

                return ret;
            }
        }
        #endregion

        /// <summary>
        /// 受領の品目追加時のコントローラ
        /// </summary>
        #region private class FormController受領 : FormController
        private class FormController受領 : FormController
        {
            // 手配依頼種別ID
            private string ThiIraiSbtID;
            // 受領品目
            private OdJryItem jryItem;
            // 受領詳細品目
            private Dictionary<TreeListViewNode, OdJryShousaiItem> OdJryShousaiItemNodes =
                                                 new Dictionary<TreeListViewNode, OdJryShousaiItem>();

            private OdJryShousaiItem newShousaiItem;

            public FormController受領(品目編集Form form, int vesselID, string thiIraiSbtID, ref OdJryItem jryItem)
            {
                this.form = form;
                this.ThiIraiSbtID = thiIraiSbtID;
                this.MsVesselID = vesselID;
                this.jryItem = jryItem;
            }
            public override void InitializeForm()
            {
                form.Text = NBaseCommon.Common.WindowTitle("番号不明", "仕様・型式編集", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

                // 詳細品目TreeListView
                int noColumIndex = -1;
                object[,] columns = new object[,] {
                                                   {"詳細品目", 275, null, null},
                                                   {"単位", 45, null, null},
                                                   {"発注数", 52, null, HorizontalAlignment.Right},
                                                   {"受領数", 52, null, HorizontalAlignment.Right},
                                                   {"単価", 65, null, HorizontalAlignment.Right},
                                                   {"金額", 80, null, HorizontalAlignment.Right},
                                                   {"備考（品名、規格等）", 200, null, null}
                                                 };

                form.treeList詳細品目 = new ItemTreeListView品目編集(form.treeListView);
                form.treeList詳細品目.SetColumns(noColumIndex, columns);
                form.treeList詳細品目.AddNodes(jryItem.OdJryShousaiItems, ref OdJryShousaiItemNodes);

                // 区分
                foreach (object obj in form.comboBox区分.Items)
                {
                    //if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                    //{
                    //    string itemStr = obj as string;
                    //    if (itemStr == jryItem.Header)
                    //    {
                    //        form.comboBox区分.SelectedItem = itemStr;
                    //        break;
                    //    }
                    //}
                    //else
                    //{
                    //    MsItemSbt itemSbt = obj as MsItemSbt;
                    //    if (itemSbt.MsItemSbtID == jryItem.MsItemSbtID)
                    //    {
                    //        form.comboBox区分.SelectedItem = itemSbt;
                    //        break;
                    //    }
                    //}
                    MsItemSbt itemSbt = obj as MsItemSbt;
                    if (itemSbt.MsItemSbtID == jryItem.MsItemSbtID)
                    {
                        form.comboBox区分.SelectedItem = itemSbt;
                        break;
                    }
                }

                // 仕様・型式
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                {
                    foreach (MsVesselItemCategory vic in form.MsVesselItemCategorys)
                    {
                        if (vic.CategoryName == jryItem.ItemName)
                        {
                            form.MsVesselItemCategoryNumber = vic.CategoryNumber;
                            form.buttonカテゴリ選択.Enabled = false;
                            break;
                        }
                    }
                }
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                {
                    form.textBox品目.Text = jryItem.ItemName;
                }
                else
                {
                    form.multiLineCombo品目.Text = jryItem.ItemName;
                }

                // 受領用の詳細品目パネルをVisibleに
                form.shousaiPanel受領.Location = shousaiPanelLocation;
                form.shousaiPanel受領.Size = shousaiPanelSize;
                form.shousaiPanel受領.Visible = true;

                // 詳細品目パネルの初期化
                InitShousaiPanel();
                base.Initialize単位(form.comboBox単位_受領);

                // 2015.04: コメントあり
                form.button削除.Enabled = false;
                form.button詳細品目削除_受領.Enabled = false;
            }
            public void InitShousaiPanel()
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    form.multiLineCombo詳細品目_受領.Visible = false;

                    form.textBox詳細品目_受領.Visible = true;
                    form.button選択_受領.Visible = true;
                    form.button選択_受領.Location = new Point(416, 6);
                    form.button詳細品目削除_受領.Location = new Point(416, 35);
                }
                else
                {
                    form.multiLineCombo詳細品目_受領.Visible = true;
                    form.multiLineCombo詳細品目_受領.MaxLength = 500;
                    //base.Initialize詳細品目(form.multiLineCombo詳細品目_受領);
                    base.Initialize詳細品目(ThiIraiSbtID, form.multiLineCombo詳細品目_受領);

                    form.textBox詳細品目_受領.Visible = false;
                    form.button選択_受領.Visible = false;
                }
            }
            public override void UpdateTreeListView()
            {
                form.treeList詳細品目.NodesClear();
                OdJryShousaiItemNodes.Clear();

                form.treeList詳細品目.AddNodes(jryItem.OdJryShousaiItems, ref OdJryShousaiItemNodes);

                //CreateNewShousaiItem();
                //form.treeList詳細品目.AddNode(newShousaiItem, ref OdJryShousaiItemNodes);
            }
            private void CreateNewShousaiItem()
            {
                newShousaiItem = new OdJryShousaiItem();
                newShousaiItem.OdJryShousaiItemID = "";
                newShousaiItem.ShousaiItemName = "";
                newShousaiItem.MsTaniID = "";
                newShousaiItem.MsTaniName = "";
                newShousaiItem.Count = -1;
                newShousaiItem.JryCount = -1;
                newShousaiItem.Tanka = -1;
                newShousaiItem.Bikou = "";
            }

            public override void BeforeSelect()
            {
                TreeListViewNode node = form.treeList詳細品目.selectedNode();

                if (node != null && OdJryShousaiItemNodes.ContainsKey(node))
                {
                    OdJryShousaiItem item = OdJryShousaiItemNodes[node];

                    Set詳細品目ToModel(item);
                    MsTani tani = form.comboBox単位_受領.SelectedItem as MsTani;
                    item.MsTaniID = tani.MsTaniID;
                    item.MsTaniName = tani.TaniName;
                    try
                    {
                        item.JryCount = Int32.Parse(form.textBox受領数_受領.Text);
                    }
                    catch
                    {
                    }
                    try
                    {
                        item.Tanka = decimal.Parse(form.textBox単価_受領.Text);
                    }
                    catch
                    {
                    }
                    //item.Bikou = form.textBox備考_受領.Text;
                    item.Bikou = StringUtils.Escape(form.textBox備考_受領.Text);
                }
            }
            public override void AfterSelect()
            {
                TreeListViewNode node = form.treeList詳細品目.selectedNode();

                if (node != null && OdJryShousaiItemNodes.ContainsKey(node))
                {
                    OdJryShousaiItem item = OdJryShousaiItemNodes[node];

                    Set詳細品目ToControl(item);
                    foreach (object obj in form.comboBox単位_受領.Items)
                    {
                        MsTani tani = (MsTani)obj;

                        if (tani.MsTaniID == item.MsTaniID)
                        {
                            form.comboBox単位_受領.SelectedItem = tani;
                        }
                    }
                    if (item.JryCount >= 0)
                    {
                        form.textBox受領数_受領.Text = item.JryCount.ToString();
                    }
                    else
                    {
                        form.textBox受領数_受領.Text = "";
                    }
                    if (item.Tanka >= 0)
                    {
                        form.textBox単価_受領.Text = item.Tanka.ToString();
                    }
                    else
                    {
                        form.textBox単価_受領.Text = "";
                    }
                    form.textBox備考_受領.Text = item.Bikou.ToString();

                    InitializeShousaiItemComponentsEnabled(item);
                }
            }
            private void InitializeShousaiItemComponentsEnabled(OdJryShousaiItem item)
            {
                // 2015.04: コメントあり
                //if (jryItem.OdJryShousaiItems.Contains(item))
                //{
                //    form.button詳細品目削除_受領.Enabled = true;
                //}
                //else
                //{
                //    form.button詳細品目削除_受領.Enabled = false;
                //}
            }
            public override void multiLineCombo詳細品目_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdJryShousaiItemNodes.ContainsKey(node))
                    {
                        OdJryShousaiItem shousaiItem = OdJryShousaiItemNodes[node];
                        Set詳細品目ToModel(shousaiItem);
                        string name = GetShousaiName(shousaiItem.ShousaiItemName);
                        //if (shousaiItem == newShousaiItem && !jryItem.OdJryShousaiItems.Contains(shousaiItem) && form.multiLineCombo詳細品目_受領.Text.Length > 0)
                        if (shousaiItem == newShousaiItem && !jryItem.OdJryShousaiItems.Contains(shousaiItem) && name.Length > 0)
                        {
                            shousaiItem.OdJryShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                            jryItem.OdJryShousaiItems.Add(shousaiItem);
                        }
                    }
                });
            }
            public override void comboBox単位_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdJryShousaiItemNodes.ContainsKey(node))
                    {
                        OdJryShousaiItem shousaiItem = OdJryShousaiItemNodes[node];
                        MsTani tani = form.comboBox単位_受領.SelectedItem as MsTani;
                        shousaiItem.MsTaniID = tani.MsTaniID;
                        shousaiItem.MsTaniName = tani.TaniName;
                    }
                });
            }
            public override void textBox受領数_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdJryShousaiItemNodes.ContainsKey(node))
                    {
                        OdJryShousaiItem shousaiItem = OdJryShousaiItemNodes[node];
                        try
                        {
                            shousaiItem.JryCount = Int32.Parse(form.textBox受領数_受領.Text);
                        }
                        catch
                        {
                        }
                    }
                });
            }
            public override void textBox単価_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdJryShousaiItemNodes.ContainsKey(node))
                    {
                        OdJryShousaiItem shousaiItem = OdJryShousaiItemNodes[node];
                        try
                        {
                            shousaiItem.Tanka = Int32.Parse(form.textBox単価_受領.Text);
                        }
                        catch
                        {
                        }
                    }
                });
            }
            public override void textBox備考_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdJryShousaiItemNodes.ContainsKey(node))
                    {
                        OdJryShousaiItem shousaiItem = OdJryShousaiItemNodes[node];
                        //shousaiItem.Bikou = form.textBox備考_受領.Text;
                        shousaiItem.Bikou = StringUtils.Escape(form.textBox備考_受領.Text);
                    }
                });
            }
            public override void button品目削除_Click()
            {
                jryItem.CancelFlag = 1;
                foreach (OdJryShousaiItem shousaiItem in jryItem.OdJryShousaiItems)
                {
                    shousaiItem.CancelFlag = 1;
                }
            }
            public override void button詳細品目削除_Click()
            {
                TreeListViewNode node = form.treeList詳細品目.selectedNode();

                if (node != null)
                {
                    OdJryShousaiItem item = OdJryShousaiItemNodes[node];
                    item.CancelFlag = 1;
                }
                int idx = node.FlatIndex; // 選択されたノードの位置

                UpdateTreeListView();

                // 選択された行の次の行（削除されているので、先ほど選択した位置）を選択する
                form.treeList詳細品目.SetSelectedNode(idx);
            }
            public override void button登録_Click()
            {
                BuildOdJryItem();
            }
            public override void button選択_Click(MsVesselItemVessel vesselItemVessel)
            {
                form.textBox詳細品目_受領.Text = vesselItemVessel.VesselItemName;
                form.labelMsVesselItemId_受領.Text = vesselItemVessel.MsVesselItemID;
                foreach (object obj in form.comboBox単位_受領.Items)
                {
                    MsTani tani = (MsTani)obj;

                    if (tani.MsTaniID == vesselItemVessel.MsTaniID)
                    {
                        form.comboBox単位_受領.SelectedItem = tani;
                    }
                }

                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdJryShousaiItemNodes.ContainsKey(node))
                    {
                        OdJryShousaiItem shousaiItem = OdJryShousaiItemNodes[node];
                        Set詳細品目ToModel(shousaiItem);
                        if (shousaiItem == newShousaiItem && !jryItem.OdJryShousaiItems.Contains(shousaiItem) && form.textBox詳細品目_受領.Text.Length > 0)
                        {
                            shousaiItem.OdJryShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                            jryItem.OdJryShousaiItems.Add(shousaiItem);
                        }
                    }
                });

                form.comboBox単位_受領.Focus();
            }

            private void BuildOdJryItem()
            {
                if (form.EditMode == (int)EDIT_MODE.新規)
                {
                    jryItem.OdJryItemID = Hachu.Common.CommonDefine.新規ID();
                }

                jryItem.Header = form.comboBox区分.Text;

                //if (ThiIraiSbtID != NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                //{
                //    MsItemSbt itemSbt = null;
                //    if (form.comboBox区分.SelectedItem is MsItemSbt)
                //    {
                //        itemSbt = form.comboBox区分.SelectedItem as MsItemSbt;
                //    }
                //    if (itemSbt != null && itemSbt.MsItemSbtID.Length > 0)
                //    {
                //        jryItem.MsItemSbtID = itemSbt.MsItemSbtID;
                //        jryItem.MsItemSbtName = itemSbt.ItemSbtName;
                //    }
                //}
                MsItemSbt itemSbt = null;
                if (form.comboBox区分.SelectedItem is MsItemSbt)
                {
                    itemSbt = form.comboBox区分.SelectedItem as MsItemSbt;
                }
                if (itemSbt != null && itemSbt.MsItemSbtID.Length > 0)
                {
                    jryItem.MsItemSbtID = itemSbt.MsItemSbtID;
                    jryItem.MsItemSbtName = itemSbt.ItemSbtName;
                }

                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                {
                    jryItem.ItemName = form.textBox品目.Text;
                }
                else
                {
                    //jryItem.ItemName = form.multiLineCombo品目.Text;
                    jryItem.ItemName = StringUtils.Escape(form.multiLineCombo品目.Text);

                    if (form.MsShoushuriItemDic.ContainsKey(jryItem.ItemName) == false)
                    {
                        jryItem.SaveDB = true;
                    }
                }

                BuildOdJryShousaiItems();
            }
            private void BuildOdJryShousaiItems()
            {
                foreach (OdJryShousaiItem shousaiItem in jryItem.OdJryShousaiItems)
                {
                    if (shousaiItem.OdJryShousaiItemID.Length == 0)
                    {
                        shousaiItem.OdJryShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                    }
                    if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID)
                    {
                        if (MsSsShousaiItemDic.ContainsKey(shousaiItem.ShousaiItemName) == false)
                        {
                            shousaiItem.SaveDB = true;
                        }
                    }
                }
            }
            public override void SetFocus()
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    if (form.textBox詳細品目_受領.Visible == false)
                    {
                        InitShousaiPanel();
                    }
                    form.textBox詳細品目_受領.Focus();
                }
                else
                {
                    if (form.multiLineCombo詳細品目_受領.Visible == false)
                    {
                        InitShousaiPanel();
                    }
                    form.multiLineCombo詳細品目_受領.Focus();
                }
            }
            private void Set詳細品目ToControl(OdJryShousaiItem item)
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    form.textBox詳細品目_受領.Text = item.ShousaiItemName;
                    form.labelMsVesselItemId_受領.Text = item.MsVesselItemID;
                }
                //else
                //{
                //    form.multiLineCombo詳細品目_受領.Text = item.ShousaiItemName;
                //}
                else if (item.MsVesselItemID != null && item.MsVesselItemID.Length > 0)
                {
                    form.multiLineCombo詳細品目_受領.Text = item.ShousaiItemName;
                    form.labelMsVesselItemId_受領.Text = item.MsVesselItemID;

                    form.multiLineCombo詳細品目_受領.ReadOnly = true;
                }
                else
                {
                    form.multiLineCombo詳細品目_受領.Text = item.ShousaiItemName;
                    form.labelMsVesselItemId_受領.Text = "";

                    form.multiLineCombo詳細品目_受領.ReadOnly = false;
                }
            }
            public void Set詳細品目ToModel(OdJryShousaiItem item)
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    item.ShousaiItemName = form.textBox詳細品目_受領.Text;
                    item.MsVesselItemID = form.labelMsVesselItemId_受領.Text;
                }
                //else
                //{
                //    //item.ShousaiItemName = form.multiLineCombo詳細品目_受領.Text;
                //    item.ShousaiItemName = StringUtils.Escape(form.multiLineCombo詳細品目_受領.Text);
                //}
                else if (form.labelMsVesselItemId_受領.Text.Length > 0)
                {
                    item.ShousaiItemName = StringUtils.Escape(form.multiLineCombo詳細品目_受領.Text);
                    item.MsVesselItemID = form.labelMsVesselItemId_受領.Text;
                }
                else
                {
                    item.ShousaiItemName = StringUtils.Escape(form.multiLineCombo詳細品目_受領.Text);
                }
            }

            public override bool ValidateShousai()
            {
                bool ret = true;

                TreeListViewNode node = form.treeList詳細品目.selectedNode();
                if (node != null)
                {
                    OdJryShousaiItem item = OdJryShousaiItemNodes[node];
                    string name = item.ShousaiItemName;
                    name = name.Trim();
                    name = name.Replace(Environment.NewLine, "");
                    if (name.Length == 0)
                    {
                        ret = false;
                    }
                }
                return ret;
            }

            public override void ValidateFields(ref string errMessage)
            {
                // 詳細品目
                for (int i = 0; i < jryItem.OdJryShousaiItems.Count; i++)
                {
                    OdJryShousaiItem si = jryItem.OdJryShousaiItems[i];

                    if (si.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                    {
                        continue;
                    }
                    // (2009.09.21: aki <入力時にブロックしているのでここでは判定の必要なし>)
                    //if (si.ShousaiItemName.Length == 0)
                    //{
                    //    errMessage += "・No. " + (i + 1) + " の詳細品目を入力して下さい\n";
                    //}
                    //else if (si.ShousaiItemName.Length > 500)
                    //{
                    //    errMessage += "・No. " + (i + 1) + " の詳細品目は500文字以下で入力して下さい\n";
                    //}
                    string name = si.ShousaiItemName.Trim().Replace(Environment.NewLine, " ");
                    if (name.Length > Err文字表示数)
                    {
                        name = name.Substring(0, Err文字表示数);
                        name = name + "…";
                    }
                    if (si.JryCount < 0)
                    {
                        errMessage += "・「 " + name + " 」の受領数を入力して下さい\n";
                    }
                    if (si.Tanka < 0)
                    {
                        errMessage += "・「 " + name + " 」の単価を入力して下さい\n";
                    }
                    if (si.Bikou.Length > 500)
                    {
                        errMessage += "・「 " + name + " 」の備考（品名、規格等）は500文字以下で入力して下さい\n";
                    }
                }
            }

            public override void CreateShousai(List<DataGridViewRow> rows)
            {
                foreach (DataGridViewRow row in rows)
                {
                    MsVesselItemVessel vesselItem = row.Cells["obj"].Value as MsVesselItemVessel;
                    OdJryShousaiItem shousaiItem = new OdJryShousaiItem();
                    shousaiItem.OdJryShousaiItemID = "";
                    shousaiItem.ShousaiItemName = "";
                    shousaiItem.MsTaniID = "";
                    shousaiItem.MsTaniName = "";
                    newShousaiItem.Count = -1;
                    newShousaiItem.JryCount = -1;
                    shousaiItem.Bikou = "";

                    // 特定船用品をセット
                    shousaiItem.OdJryShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                    shousaiItem.MsVesselItemID = vesselItem.MsVesselItemID;
                    shousaiItem.ShousaiItemName = vesselItem.VesselItemName;
                    shousaiItem.MsTaniID = vesselItem.MsTaniID;
                    shousaiItem.MsTaniName = vesselItem.MsTaniName;

                    int count = 0;
                    if (row.Cells["数量"].Value != null)
                    {
                        if (int.TryParse((row.Cells["数量"].Value as string), out count))
                        {
                            shousaiItem.JryCount = count;
                        }
                    }

                    shousaiItem.Bikou = vesselItem.Bikou;


                    jryItem.OdJryShousaiItems.Add(shousaiItem);
                }
            }
            public override List<string> GetVesselItemIdList()
            {
                List<string> ret = new List<string>();

                foreach (OdJryShousaiItem shousaiItem in jryItem.OdJryShousaiItems)
                {
                    if (shousaiItem.MsVesselItemID != null && shousaiItem.MsVesselItemID != "" && shousaiItem.CancelFlag == 0)
                    {
                        ret.Add(shousaiItem.MsVesselItemID);
                    }
                }

                return ret;
            }
        }
        #endregion

        /// <summary>
        /// 支払の品目追加時のコントローラ
        /// </summary>
        #region private class FormController支払 : FormController
        private class FormController支払 : FormController
        {
            // 種別
            private int sbt;
            // 手配依頼種別ID
            private string ThiIraiSbtID;
            // 支払品目
            private OdShrItem shrItem;
            // 支払詳細品目
            private Dictionary<TreeListViewNode, OdShrShousaiItem> OdShrShousaiItemNodes =
                                                 new Dictionary<TreeListViewNode, OdShrShousaiItem>();

            private OdShrShousaiItem newShousaiItem;

            public FormController支払(品目編集Form form, int sbt, int vesselID, string thiIraiSbtID, ref OdShrItem shrItem)
            {
                this.form = form;
                this.sbt = sbt;
                this.ThiIraiSbtID = thiIraiSbtID;
                this.MsVesselID = vesselID;
                this.shrItem = shrItem;
            }
            public override void InitializeForm()
            {
                if (sbt == (int)OdShr.SBT.支払)
                {
                    form.Text = NBaseCommon.Common.WindowTitle("番号不明", "仕様・型式編集", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
                }
                else
                {
                    form.Text = NBaseCommon.Common.WindowTitle("番号不明", "仕様・型式編集", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
                }

                // 詳細品目TreeListView
                int noColumIndex = -1;
                object[,] columns = new object[,] {
                                                   {"詳細品目", 275, null, null},
                                                   {"単位", 45, null, null},
                                                   {"数量", 50, null, HorizontalAlignment.Right},
                                                   {"単価", 65, null, HorizontalAlignment.Right},
                                                   {"金額", 80, null, HorizontalAlignment.Right},
                                                   {"備考（品名、規格等）", 200, null, null}
                                                 };

                form.treeList詳細品目 = new ItemTreeListView品目編集(form.treeListView);
                form.treeList詳細品目.SetColumns(noColumIndex, columns);
                form.treeList詳細品目.AddNodes(shrItem.OdShrShousaiItems, ref OdShrShousaiItemNodes);

                // 2011.05.19: Update 支払時は、追加できないように変更
                //CreateNewShousaiItem();
                //form.treeList詳細品目.AddNode(newShousaiItem, ref OdShrShousaiItemNodes);
                if (sbt != (int)OdShr.SBT.支払)
                {
                    CreateNewShousaiItem();
                    form.treeList詳細品目.AddNode(newShousaiItem, ref OdShrShousaiItemNodes);
                }
                // 2011.05.19: Update End

                
                // 区分
                foreach (object obj in form.comboBox区分.Items)
                {
                    //if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                    //{
                    //    string itemStr = obj as string;
                    //    if (itemStr == shrItem.Header)
                    //    {
                    //        form.comboBox区分.SelectedItem = itemStr;
                    //        break;
                    //    }
                    //}
                    //else
                    //{
                    //    MsItemSbt itemSbt = obj as MsItemSbt;
                    //    if (itemSbt.MsItemSbtID == shrItem.MsItemSbtID)
                    //    {
                    //        form.comboBox区分.SelectedItem = itemSbt;
                    //        break;
                    //    }
                    //}
                    MsItemSbt itemSbt = obj as MsItemSbt;
                    if (itemSbt.MsItemSbtID == shrItem.MsItemSbtID)
                    {
                        form.comboBox区分.SelectedItem = itemSbt;
                        break;
                    }
                }

                // 仕様・型式
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                {
                    foreach (MsVesselItemCategory vic in form.MsVesselItemCategorys)
                    {
                        if (vic.CategoryName == shrItem.ItemName)
                        {
                            form.MsVesselItemCategoryNumber = vic.CategoryNumber;
                            form.buttonカテゴリ選択.Enabled = false;
                            break;
                        }
                    }
                }
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                {
                    form.textBox品目.Text = shrItem.ItemName;
                }
                else
                {
                    form.multiLineCombo品目.Text = shrItem.ItemName;           
                }

                // 支払用の詳細品目パネルをVisibleに
                form.shousaiPanel支払.Location = shousaiPanelLocation;
                form.shousaiPanel支払.Size = shousaiPanelSize;
                form.shousaiPanel支払.Visible = true;

                // 詳細品目パネルの初期化
                InitShousaiPanel();
                base.Initialize単位(form.comboBox単位_支払);

                // 2011.05.19: Add
                if (sbt == (int)OdShr.SBT.支払)
                {
                    // Tab移動の制御を変更する
                    form.button削除.TabStop = false;
                    form.comboBox区分.TabStop = false;
                    form.buttonカテゴリ選択.TabStop = false;
                    form.textBox品目.TabStop = false;
                    form.multiLineCombo品目.TabStop = false;

                    form.button削除.Enabled = false;
                    form.comboBox区分.Enabled = false;
                    form.buttonカテゴリ選択.Enabled = false;
                    if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                    {
                        form.textBox品目.Enabled = false;
                    }
                    else
                    {
                        form.multiLineCombo品目.Enabled = false;
                    }

                    form.treeListView.Focus();
                }
                // 2011.05.19: Add End
            }
            public void InitShousaiPanel()
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    form.multiLineCombo詳細品目_支払.Visible = false;

                    form.textBox詳細品目_支払.Visible = true;
                    form.button選択_支払.Visible = true;
                    form.button選択_支払.Location = new Point(416, 6);
                    form.button詳細品目削除_支払.Location = new Point(416, 35);
                }
                else
                {
                    form.multiLineCombo詳細品目_支払.Visible = true;
                    form.multiLineCombo詳細品目_支払.MaxLength = 500;
                    //base.Initialize詳細品目(form.multiLineCombo詳細品目_支払);
                    base.Initialize詳細品目(ThiIraiSbtID, form.multiLineCombo詳細品目_支払);

                    form.textBox詳細品目_支払.Visible = false;
                    form.button選択_支払.Visible = false;
                }

                // 2011.05.19: Add
                if (sbt == (int)OdShr.SBT.支払)
                {
                    if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                        && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                    {
                        form.textBox詳細品目_支払.Enabled = false;
                        form.button選択_支払.Enabled = false;
                    }
                    else
                    {
                        form.multiLineCombo詳細品目_支払.Enabled = false;
                    }
                    form.button詳細品目削除_支払.Enabled = false;
                    form.comboBox単位_支払.Enabled = false;
                    form.textBox数量_支払.Enabled = false;
                    form.textBox備考_支払.Enabled = false;

                    // Tab移動の制御を変更する
                    form.multiLineCombo詳細品目_支払.TabStop = false;
                    form.textBox詳細品目_支払.TabStop = false;
                    form.button詳細品目削除_支払.TabStop = false;
                    form.comboBox単位_支払.TabStop = false;
                    form.textBox数量_支払.TabStop = false;
                    form.textBox備考_支払.TabStop = false;

                    form.textBox単価_支払.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(textBox単価_PreviewKeyDown);
                }
                // 2011.05.19: Add End
            }
            public override void UpdateTreeListView()
            {
                form.treeList詳細品目.NodesClear();
                OdShrShousaiItemNodes.Clear();

                form.treeList詳細品目.AddNodes(shrItem.OdShrShousaiItems, ref OdShrShousaiItemNodes);
                
                // 2011.05.19: Update 支払時は、追加できないように変更
                //CreateNewShousaiItem();
                //form.treeList詳細品目.AddNode(newShousaiItem, ref OdShrShousaiItemNodes);
                if (sbt != (int)OdShr.SBT.支払)
                {
                    CreateNewShousaiItem();
                    form.treeList詳細品目.AddNode(newShousaiItem, ref OdShrShousaiItemNodes);
                }
                // 2011.05.19: Update End
            }
            private void CreateNewShousaiItem()
            {
                newShousaiItem = new OdShrShousaiItem();
                newShousaiItem.OdShrShousaiItemID = "";
                newShousaiItem.ShousaiItemName = "";
                newShousaiItem.MsTaniID = "";
                newShousaiItem.MsTaniName = "";
                newShousaiItem.Count = -1;
                newShousaiItem.Tanka = -1;
                newShousaiItem.Bikou = "";
            }

            public override void BeforeSelect()
            {
                TreeListViewNode node = form.treeList詳細品目.selectedNode();

                if (node != null && OdShrShousaiItemNodes.ContainsKey(node))
                {
                    OdShrShousaiItem item = OdShrShousaiItemNodes[node];

                    Set詳細品目ToModel(item);
                    MsTani tani = form.comboBox単位_支払.SelectedItem as MsTani;
                    item.MsTaniID = tani.MsTaniID;
                    item.MsTaniName = tani.TaniName;
                    try
                    {
                        item.Count = Int32.Parse(form.textBox数量_支払.Text);
                    }
                    catch
                    {
                    }
                    try
                    {
                        item.Tanka = decimal.Parse(form.textBox単価_支払.Text);
                    }
                    catch
                    {
                    }
                    //item.Bikou = form.textBox備考_支払.Text;
                    item.Bikou = StringUtils.Escape(form.textBox備考_支払.Text);
                }
            }
            public override void AfterSelect()
            {
                TreeListViewNode node = form.treeList詳細品目.selectedNode();

                if (node != null && OdShrShousaiItemNodes.ContainsKey(node))
                {
                    OdShrShousaiItem item = OdShrShousaiItemNodes[node];

                    Set詳細品目ToControl(item);
                    foreach (object obj in form.comboBox単位_支払.Items)
                    {
                        MsTani tani = (MsTani)obj;

                        if (tani.MsTaniID == item.MsTaniID)
                        {
                            form.comboBox単位_支払.SelectedItem = tani;
                        }
                    }
                    if (item.Count >= 0)
                    {
                        form.textBox数量_支払.Text = item.Count.ToString();
                    }
                    else
                    {
                        form.textBox数量_支払.Text = "";
                    }
                    if (item.Tanka >= 0)
                    {
                        form.textBox単価_支払.Text = item.Tanka.ToString();
                    }
                    else
                    {
                        form.textBox単価_支払.Text = "";
                    }
                    form.textBox備考_支払.Text = item.Bikou.ToString();

                    InitializeShousaiItemComponentsEnabled(item);
                }
            }
            private void InitializeShousaiItemComponentsEnabled(OdShrShousaiItem item)
            {
                // 2011.05.19: Update
                //if (shrItem.OdShrShousaiItems.Contains(item))
                //{
                //    form.button詳細品目削除_支払.Enabled = true;
                //}
                //else
                //{
                //    form.button詳細品目削除_支払.Enabled = false;
                //}
                if (sbt != (int)OdShr.SBT.支払)
                {
                    if (shrItem.OdShrShousaiItems.Contains(item))
                    {
                        form.button詳細品目削除_支払.Enabled = true;
                    }
                    else
                    {
                        form.button詳細品目削除_支払.Enabled = false;
                    }
                }
                // 2011.05.19: Update End
            }
            public override void multiLineCombo詳細品目_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdShrShousaiItemNodes.ContainsKey(node))
                    {
                        OdShrShousaiItem shousaiItem = OdShrShousaiItemNodes[node];
                        Set詳細品目ToModel(shousaiItem);
                        string name = GetShousaiName(shousaiItem.ShousaiItemName);
                        //if (shousaiItem == newShousaiItem && !shrItem.OdShrShousaiItems.Contains(shousaiItem) && form.multiLineCombo詳細品目_支払.Text.Length > 0)
                        if (shousaiItem == newShousaiItem && !shrItem.OdShrShousaiItems.Contains(shousaiItem) && name.Length > 0)
                        {
                            shousaiItem.OdShrShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                            shrItem.OdShrShousaiItems.Add(shousaiItem);
                        }
                    }
                });
            }
            public override void comboBox単位_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdShrShousaiItemNodes.ContainsKey(node))
                    {
                        OdShrShousaiItem shousaiItem = OdShrShousaiItemNodes[node];
                        MsTani tani = form.comboBox単位_支払.SelectedItem as MsTani;
                        shousaiItem.MsTaniID = tani.MsTaniID;
                        shousaiItem.MsTaniName = tani.TaniName;
                    }
                });
            }
            public override void textBox数量_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdShrShousaiItemNodes.ContainsKey(node))
                    {
                        OdShrShousaiItem shousaiItem = OdShrShousaiItemNodes[node];
                        try
                        {
                            shousaiItem.Count = Int32.Parse(form.textBox数量_支払.Text);
                        }
                        catch
                        {
                        }
                    }
                });
            }
            public override void textBox単価_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdShrShousaiItemNodes.ContainsKey(node))
                    {
                        OdShrShousaiItem shousaiItem = OdShrShousaiItemNodes[node];
                        try
                        {
                            shousaiItem.Tanka = Int32.Parse(form.textBox単価_支払.Text);
                        }
                        catch
                        {
                        }
                    }
                });
            }
            public override void textBox備考_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdShrShousaiItemNodes.ContainsKey(node))
                    {
                        OdShrShousaiItem shousaiItem = OdShrShousaiItemNodes[node];
                        //shousaiItem.Bikou = form.textBox備考_支払.Text;
                        shousaiItem.Bikou = StringUtils.Escape(form.textBox備考_支払.Text);
                    }
                });
            }
            public override void button品目削除_Click()
            {
                shrItem.CancelFlag = 1;
                foreach (OdShrShousaiItem shousaiItem in shrItem.OdShrShousaiItems)
                {
                    shousaiItem.CancelFlag = 1;
                }
            }
            public override void button詳細品目削除_Click()
            {
                TreeListViewNode node = form.treeList詳細品目.selectedNode();

                if (node != null)
                {
                    OdShrShousaiItem item = OdShrShousaiItemNodes[node];
                    item.CancelFlag = 1;
                }
                int idx = node.FlatIndex; // 選択されたノードの位置

                UpdateTreeListView();

                // 選択された行の次の行（削除されているので、先ほど選択した位置）を選択する
                form.treeList詳細品目.SetSelectedNode(idx);
            }
            public override void button登録_Click()
            {
                BuildOdShrItem();
            }
            public override void button選択_Click(MsVesselItemVessel vesselItemVessel)
            {
                form.textBox詳細品目_支払.Text = vesselItemVessel.VesselItemName;
                form.labelMsVesselItemId_支払.Text = vesselItemVessel.MsVesselItemID;
                foreach (object obj in form.comboBox単位_支払.Items)
                {
                    MsTani tani = (MsTani)obj;

                    if (tani.MsTaniID == vesselItemVessel.MsTaniID)
                    {
                        form.comboBox単位_支払.SelectedItem = tani;
                    }
                }

                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdShrShousaiItemNodes.ContainsKey(node))
                    {
                        OdShrShousaiItem shousaiItem = OdShrShousaiItemNodes[node];
                        Set詳細品目ToModel(shousaiItem);
                        if (shousaiItem == newShousaiItem && !shrItem.OdShrShousaiItems.Contains(shousaiItem) && form.textBox詳細品目_支払.Text.Length > 0)
                        {
                            shousaiItem.OdShrShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                            shrItem.OdShrShousaiItems.Add(shousaiItem);
                        }
                    }
                });

                form.comboBox単位_支払.Focus();
            }

            private void BuildOdShrItem()
            {
                if (form.EditMode == (int)EDIT_MODE.新規)
                {
                    shrItem.OdShrItemID = Hachu.Common.CommonDefine.新規ID();
                }

                shrItem.Header = form.comboBox区分.Text;

                //if (ThiIraiSbtID != NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                //{
                //    MsItemSbt itemSbt = null;
                //    if (form.comboBox区分.SelectedItem is MsItemSbt)
                //    {
                //        itemSbt = form.comboBox区分.SelectedItem as MsItemSbt;
                //    }
                //    if (itemSbt != null && itemSbt.MsItemSbtID.Length > 0)
                //    {
                //        shrItem.MsItemSbtID = itemSbt.MsItemSbtID;
                //        shrItem.MsItemSbtName = itemSbt.ItemSbtName;
                //    }
                //}
                MsItemSbt itemSbt = null;
                if (form.comboBox区分.SelectedItem is MsItemSbt)
                {
                    itemSbt = form.comboBox区分.SelectedItem as MsItemSbt;
                }
                if (itemSbt != null && itemSbt.MsItemSbtID.Length > 0)
                {
                    shrItem.MsItemSbtID = itemSbt.MsItemSbtID;
                    shrItem.MsItemSbtName = itemSbt.ItemSbtName;
                }
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                {
                    shrItem.ItemName = form.textBox品目.Text;
                }
                else
                {
                    //shrItem.ItemName = form.multiLineCombo品目.Text;
                    shrItem.ItemName = StringUtils.Escape(form.multiLineCombo品目.Text);

                    if (form.MsShoushuriItemDic.ContainsKey(shrItem.ItemName) == false)
                    {
                        shrItem.SaveDB = true;
                    }
                }

                BuildOdShrShousaiItems();
            }
            private void BuildOdShrShousaiItems()
            {
                foreach (OdShrShousaiItem shousaiItem in shrItem.OdShrShousaiItems)
                {
                    if (shousaiItem.OdShrShousaiItemID.Length == 0)
                    {
                        shousaiItem.OdShrShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                    }
                    if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID)
                    {
                        if (MsSsShousaiItemDic.ContainsKey(shousaiItem.ShousaiItemName) == false)
                        {
                            shousaiItem.SaveDB = true;
                        }
                    }
                }
            }
            public override void SetFocus()
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    if (form.textBox詳細品目_支払.Visible == false)
                    {
                        InitShousaiPanel();
                    }
                    form.textBox詳細品目_支払.Focus();
                }
                else
                {
                    if (form.multiLineCombo詳細品目_支払.Visible == false)
                    {
                        InitShousaiPanel();
                    }
                    form.multiLineCombo詳細品目_支払.Focus();
                }
                // 2011.05.19: Add
                if (sbt == (int)OdShr.SBT.支払)
                {
                    form.textBox単価_支払.Focus();
                }
            }
            private void Set詳細品目ToControl(OdShrShousaiItem item)
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    form.textBox詳細品目_支払.Text = item.ShousaiItemName;
                    form.labelMsVesselItemId_支払.Text = item.MsVesselItemID;
                }
                //else
                //{
                //    form.multiLineCombo詳細品目_支払.Text = item.ShousaiItemName;
                //}
                else if (item.MsVesselItemID != null && item.MsVesselItemID.Length > 0)
                {
                    form.multiLineCombo詳細品目_支払.Text = item.ShousaiItemName;
                    form.labelMsVesselItemId_支払.Text = item.MsVesselItemID;

                    form.multiLineCombo詳細品目_支払.ReadOnly = true;
                }
                else
                {
                    form.multiLineCombo詳細品目_支払.Text = item.ShousaiItemName;
                    form.labelMsVesselItemId_支払.Text = "";

                    form.multiLineCombo詳細品目_支払.ReadOnly = false;
                }
            }
            public void Set詳細品目ToModel(OdShrShousaiItem item)
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    item.ShousaiItemName = form.textBox詳細品目_支払.Text;
                    item.MsVesselItemID = form.labelMsVesselItemId_支払.Text;
                }
                //else
                //{
                //    //item.ShousaiItemName = form.multiLineCombo詳細品目_支払.Text;
                //    item.ShousaiItemName = StringUtils.Escape(form.multiLineCombo詳細品目_支払.Text);
                //}
                else if (form.labelMsVesselItemId_支払.Text.Length > 0)
                {
                    item.ShousaiItemName = StringUtils.Escape(form.multiLineCombo詳細品目_支払.Text);
                    item.MsVesselItemID = form.labelMsVesselItemId_支払.Text;
                }
                else
                {
                    item.ShousaiItemName = StringUtils.Escape(form.multiLineCombo詳細品目_支払.Text);
                }
            }

            public override bool ValidateShousai()
            {
                bool ret = true;

                TreeListViewNode node = form.treeList詳細品目.selectedNode();
                if (node != null)
                {
                    OdShrShousaiItem item = OdShrShousaiItemNodes[node];
                    string name = item.ShousaiItemName;
                    name = name.Trim();
                    name = name.Replace(Environment.NewLine, "");
                    if (name.Length == 0)
                    {
                        ret = false;
                    }
                }
                return ret;
            }

            public override void ValidateFields(ref string errMessage)
            {
                // 詳細品目
                for (int i = 0; i < shrItem.OdShrShousaiItems.Count; i++)
                {
                    OdShrShousaiItem si = shrItem.OdShrShousaiItems[i];

                    if (si.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                    {
                        continue;
                    }
                    // (2009.09.21: aki <入力時にブロックしているのでここでは判定の必要なし>)
                    //if (si.ShousaiItemName.Length == 0)
                    //{
                    //    errMessage += "・No. " + (i + 1) + " の詳細品目を入力して下さい\n";
                    //}
                    //else if (si.ShousaiItemName.Length > 500)
                    //{
                    //    errMessage += "・No. " + (i + 1) + " の詳細品目は500文字以下で入力して下さい\n";
                    //}
                    string name = si.ShousaiItemName.Trim().Replace(Environment.NewLine, " ");
                    if (name.Length > Err文字表示数)
                    {
                        name = name.Substring(0, Err文字表示数);
                        name = name + "…";
                    }
                    if (si.Count < 0)
                    {
                        errMessage += "・「 " + name + " 」の数量を入力して下さい\n";
                    }
                    if (si.Tanka < 0)
                    {
                        errMessage += "・「 " + name + " 」の単価を入力して下さい\n";
                    }
                    if (si.Bikou.Length > 500)
                    {
                        errMessage += "・「 " + name + " 」の備考（品名、規格等）は500文字以下で入力して下さい\n";
                    }
                }
            }

            /// <summary>
            /// 2011.05.19: Add
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void textBox単価_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
            {
                switch (e.KeyCode)
                {
                    //Tabキーが押されてもフォーカスが移動しないようにする
                    case Keys.Tab:
                        textBox単価_Leave();
                        e.IsInputKey = true;
                        Set詳細();
                        break;
                }
            }

            /// <summary>
            /// 2011.05.19: Add
            /// </summary>
            /// <returns></returns>
            public override int GetSbt()
            {
                return sbt;
            }


            public override void CreateShousai(List<DataGridViewRow> rows)
            {
                foreach (DataGridViewRow row in rows)
                {
                    MsVesselItemVessel vesselItem = row.Cells["obj"].Value as MsVesselItemVessel;
                    OdShrShousaiItem shousaiItem = new OdShrShousaiItem();
                    shousaiItem.OdShrShousaiItemID = "";
                    shousaiItem.ShousaiItemName = "";
                    shousaiItem.MsTaniID = "";
                    shousaiItem.MsTaniName = "";
                    shousaiItem.Count = -1;
                    shousaiItem.Bikou = "";

                    // 特定船用品をセット
                    shousaiItem.OdShrShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                    shousaiItem.MsVesselItemID = vesselItem.MsVesselItemID;
                    shousaiItem.ShousaiItemName = vesselItem.VesselItemName;
                    shousaiItem.MsTaniID = vesselItem.MsTaniID;
                    shousaiItem.MsTaniName = vesselItem.MsTaniName;

                    int count = 0;
                    if (row.Cells["数量"].Value != null)
                    {
                        if (int.TryParse((row.Cells["数量"].Value as string), out count))
                        {
                            shousaiItem.Count = count;
                        }
                    }

                    shousaiItem.Bikou = vesselItem.Bikou;

                    shrItem.OdShrShousaiItems.Add(shousaiItem);
                }
            }
            public override List<string> GetVesselItemIdList()
            {
                List<string> ret = new List<string>();

                foreach (OdShrShousaiItem shousaiItem in shrItem.OdShrShousaiItems)
                {
                    if (shousaiItem.MsVesselItemID != null && shousaiItem.MsVesselItemID != "" && shousaiItem.CancelFlag == 0)
                    {
                        ret.Add(shousaiItem.MsVesselItemID);
                    }
                }

                return ret;
            }
        }
        #endregion
        
        /// <summary>
        /// 新規発注の品目追加時のコントローラ
        /// </summary>
        #region private class FormController新規発注 : FormController
        private class FormController新規発注 : FormController
        {
            // 手配依頼種別ID
            private string ThiIraiSbtID;
            // 見積回答品目
            private OdMkItem mkItem;
            // 見積回答詳細品目
            private Dictionary<TreeListViewNode, OdMkShousaiItem> OdMkShousaiItemNodes =
                                                 new Dictionary<TreeListViewNode, OdMkShousaiItem>();

            private OdMkShousaiItem newShousaiItem;

            public FormController新規発注(品目編集Form form, int vesselID, string thiIraiSbtID, ref OdMkItem mkItem)
            {
                this.form = form;
                this.ThiIraiSbtID = thiIraiSbtID;
                this.MsVesselID = vesselID;
                this.mkItem = mkItem;
            }
            public override void InitializeForm()
            {
                form.Text = NBaseCommon.Common.WindowTitle("番号不明", "仕様・型式編集", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

                // 詳細品目TreeListView
                int noColumIndex = -1;
                object[,] columns = new object[,] {
                                                   {"詳細品目", 275, null, null},
                                                   {"単位", 45, null, null},
                                                   {"発注数", 52, null, HorizontalAlignment.Right},
                                                   {"単価", 65, null, HorizontalAlignment.Right},
                                                   {"金額", 80, null, HorizontalAlignment.Right},
                                                   {"備考（品名、規格等）", 200, null, null}
                                                 };

                form.treeList詳細品目 = new ItemTreeListView品目編集(form.treeListView);
                form.treeList詳細品目.SetColumns(noColumIndex, columns);
                form.treeList詳細品目.AddNodes(mkItem.OdMkShousaiItems, ref OdMkShousaiItemNodes);
                CreateNewShousaiItem();
                form.treeList詳細品目.AddNode(newShousaiItem, ref OdMkShousaiItemNodes);

                // 区分
                foreach (object obj in form.comboBox区分.Items)
                {
                    //if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                    //{
                    //    string itemStr = obj as string;
                    //    if (itemStr == mkItem.Header)
                    //    {
                    //        form.comboBox区分.SelectedItem = itemStr;
                    //        break;
                    //    }
                    //}
                    //else
                    //{
                    //    MsItemSbt itemSbt = obj as MsItemSbt;
                    //    if (itemSbt.MsItemSbtID == mkItem.MsItemSbtID)
                    //    {
                    //        form.comboBox区分.SelectedItem = itemSbt;
                    //        break;
                    //    }
                    //}
                    MsItemSbt itemSbt = obj as MsItemSbt;
                    if (itemSbt.MsItemSbtID == mkItem.MsItemSbtID)
                    {
                        form.comboBox区分.SelectedItem = itemSbt;
                        break;
                    }
                }                

                // 仕様・型式
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                {
                    foreach (MsVesselItemCategory vic in form.MsVesselItemCategorys)
                    {
                        if (vic.CategoryName == mkItem.ItemName)
                        {
                            form.MsVesselItemCategoryNumber = vic.CategoryNumber;
                            form.buttonカテゴリ選択.Enabled = false;
                            break;
                        }
                    }
                }
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                {
                    form.textBox品目.Text = mkItem.ItemName;
                }
                else
                {
                    form.multiLineCombo品目.Text = mkItem.ItemName;
                }

                // 見積回答用の詳細品目パネルをVisibleに
                form.shousaiPanel新規発注.Location = shousaiPanelLocation;
                form.shousaiPanel新規発注.Size = shousaiPanelSize;
                form.shousaiPanel新規発注.Visible = true;

                // 詳細品目パネルの初期化
                InitShousaiPanel();
                base.Initialize単位(form.comboBox単位_新規発注);
            }
            public void InitShousaiPanel()
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    form.multiLineCombo詳細品目_新規発注.Visible = false;

                    form.textBox詳細品目_新規発注.Visible = true;
                    form.button選択_新規発注.Visible = true;
                    form.button選択_新規発注.Location = new Point(416, 6);
                    form.button詳細品目削除_新規発注.Location = new Point(416, 35);
                }
                else
                {
                    form.multiLineCombo詳細品目_新規発注.Visible = true;
                    form.multiLineCombo詳細品目_新規発注.MaxLength = 500;
                    //base.Initialize詳細品目(form.multiLineCombo詳細品目_新規発注);
                    base.Initialize詳細品目(ThiIraiSbtID, form.multiLineCombo詳細品目_新規発注);

                    form.textBox詳細品目_新規発注.Visible = false;
                    form.button選択_新規発注.Visible = false;
                }
            }
            public override void UpdateTreeListView()
            {
                form.treeList詳細品目.NodesClear();
                OdMkShousaiItemNodes.Clear();

                form.treeList詳細品目.AddNodes(mkItem.OdMkShousaiItems, ref OdMkShousaiItemNodes);
                CreateNewShousaiItem();
                form.treeList詳細品目.AddNode(newShousaiItem, ref OdMkShousaiItemNodes);
            }
            private void CreateNewShousaiItem()
            {
                newShousaiItem = new OdMkShousaiItem();
                newShousaiItem.OdMkShousaiItemID = "";
                newShousaiItem.OdMmShousaiItemID = "新規発注";
                newShousaiItem.ShousaiItemName = "";
                newShousaiItem.MsTaniID = "";
                newShousaiItem.MsTaniName = "";
                newShousaiItem.OdMmShousaiItemCount = -1;
                newShousaiItem.Count = -1;
                newShousaiItem.Tanka = -1;
                newShousaiItem.Bikou = "";
            }

            public override void BeforeSelect()
            {
                TreeListViewNode node = form.treeList詳細品目.selectedNode();

                if (node != null && OdMkShousaiItemNodes.ContainsKey(node))
                {
                    OdMkShousaiItem item = OdMkShousaiItemNodes[node];

                    Set詳細品目ToModel(item);
                    MsTani tani = form.comboBox単位_新規発注.SelectedItem as MsTani;
                    item.MsTaniID = tani.MsTaniID;
                    item.MsTaniName = tani.TaniName;
                    try
                    {
                        item.Count = Int32.Parse(form.textBox発注数_新規発注.Text);
                    }
                    catch
                    {
                    }
                    try
                    {
                        item.Tanka = decimal.Parse(form.textBox単価_新規発注.Text);
                    }
                    catch
                    {
                    }
                    //item.Bikou = form.textBox備考_新規発注.Text;
                    item.Bikou = StringUtils.Escape(form.textBox備考_新規発注.Text);
                }
            }
            public override void AfterSelect()
            {
                TreeListViewNode node = form.treeList詳細品目.selectedNode();

                if (node != null && OdMkShousaiItemNodes.ContainsKey(node))
                {
                    OdMkShousaiItem item = OdMkShousaiItemNodes[node];

                    Set詳細品目ToControl(item);
                    foreach (object obj in form.comboBox単位_新規発注.Items)
                    {
                        MsTani tani = (MsTani)obj;

                        if (tani.MsTaniID == item.MsTaniID)
                        {
                            form.comboBox単位_新規発注.SelectedItem = tani;
                        }
                    }
                    if (item.Count >= 0)
                    {
                        form.textBox発注数_新規発注.Text = item.Count.ToString();
                    }
                    else
                    {
                        form.textBox発注数_新規発注.Text = "";
                    }
                    if (item.Tanka >= 0)
                    {
                        form.textBox単価_新規発注.Text = item.Tanka.ToString();
                    }
                    else
                    {
                        form.textBox単価_新規発注.Text = "";
                    }
                    form.textBox備考_新規発注.Text = item.Bikou.ToString();

                    InitializeShousaiItemComponentsEnabled(item);
                }
            }
            private void InitializeShousaiItemComponentsEnabled(OdMkShousaiItem item)
            {
                if (mkItem.OdMkShousaiItems.Contains(item))
                {
                    form.button詳細品目削除_新規発注.Enabled = true;
                }
                else
                {
                    form.button詳細品目削除_新規発注.Enabled = false;
                }
            }
            public override void multiLineCombo詳細品目_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdMkShousaiItemNodes.ContainsKey(node))
                    {
                        OdMkShousaiItem shousaiItem = OdMkShousaiItemNodes[node];
                        Set詳細品目ToModel(shousaiItem);
                        string name = GetShousaiName(shousaiItem.ShousaiItemName);
                        //if (shousaiItem == newShousaiItem && !mkItem.OdMkShousaiItems.Contains(shousaiItem) && form.multiLineCombo詳細品目_新規発注.Text.Length > 0)
                        if (shousaiItem == newShousaiItem && !mkItem.OdMkShousaiItems.Contains(shousaiItem) && name.Length > 0)
                        {
                            shousaiItem.OdMkShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                            mkItem.OdMkShousaiItems.Add(shousaiItem);
                        }
                    }
                });
            }
            public override void comboBox単位_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdMkShousaiItemNodes.ContainsKey(node))
                    {
                        OdMkShousaiItem shousaiItem = OdMkShousaiItemNodes[node];
                        MsTani tani = form.comboBox単位_新規発注.SelectedItem as MsTani;
                        shousaiItem.MsTaniID = tani.MsTaniID;
                        shousaiItem.MsTaniName = tani.TaniName;
                    }
                });
            }
            public override void textBox発注数_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdMkShousaiItemNodes.ContainsKey(node))
                    {
                        OdMkShousaiItem shousaiItem = OdMkShousaiItemNodes[node];
                        try
                        {
                            shousaiItem.Count = Int32.Parse(form.textBox発注数_新規発注.Text);
                        }
                        catch
                        {
                        }
                    }
                });
            }
            public override void textBox単価_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdMkShousaiItemNodes.ContainsKey(node))
                    {
                        OdMkShousaiItem shousaiItem = OdMkShousaiItemNodes[node];
                        try
                        {
                            shousaiItem.Tanka = Int32.Parse(form.textBox単価_新規発注.Text);
                        }
                        catch
                        {
                        }
                    }
                });
            }
            public override void textBox備考_Leave()
            {
                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdMkShousaiItemNodes.ContainsKey(node))
                    {
                        OdMkShousaiItem shousaiItem = OdMkShousaiItemNodes[node];
                        //shousaiItem.Bikou = form.textBox備考_新規発注.Text;
                        shousaiItem.Bikou = StringUtils.Escape(form.textBox備考_新規発注.Text);
                    }
                });
            }
            public override void button品目削除_Click()
            {
                mkItem.CancelFlag = 1;
                foreach (OdMkShousaiItem shousaiItem in mkItem.OdMkShousaiItems)
                {
                    shousaiItem.CancelFlag = 1;
                }
            }
            public override void button詳細品目削除_Click()
            {
                TreeListViewNode node = form.treeList詳細品目.selectedNode();

                if (node != null)
                {
                    OdMkShousaiItem item = OdMkShousaiItemNodes[node];
                    item.CancelFlag = 1;
                }
                int idx = node.FlatIndex; // 選択されたノードの位置

                UpdateTreeListView();

                // 選択された行の次の行（削除されているので、先ほど選択した位置）を選択する
                form.treeList詳細品目.SetSelectedNode(idx);
            }
            public override void button登録_Click()
            {
                BuildOdMkItem();
            }
            public override void button選択_Click(MsVesselItemVessel vesselItemVessel)
            {
                form.textBox詳細品目_新規発注.Text = vesselItemVessel.VesselItemName;
                form.labelMsVesselItemId_新規発注.Text = vesselItemVessel.MsVesselItemID;
                foreach (object obj in form.comboBox単位_新規発注.Items)
                {
                    MsTani tani = (MsTani)obj;

                    if (tani.MsTaniID == vesselItemVessel.MsTaniID)
                    {
                        form.comboBox単位_新規発注.SelectedItem = tani;
                    }
                }

                詳細品目入力コントロール_Leave(delegate(TreeListViewNode node)
                {
                    if (OdMkShousaiItemNodes.ContainsKey(node))
                    {
                        OdMkShousaiItem shousaiItem = OdMkShousaiItemNodes[node];
                        Set詳細品目ToModel(shousaiItem);
                        if (shousaiItem == newShousaiItem && !mkItem.OdMkShousaiItems.Contains(shousaiItem) && form.textBox詳細品目_新規発注.Text.Length > 0)
                        {
                            shousaiItem.OdMkShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                            mkItem.OdMkShousaiItems.Add(shousaiItem);
                        }
                    }
                });

                form.comboBox単位_新規発注.Focus();
            }

            private void BuildOdMkItem()
            {
                if (form.EditMode == (int)EDIT_MODE.新規)
                {
                    mkItem.OdMkItemID = Hachu.Common.CommonDefine.新規ID();
                }

                mkItem.Header = form.comboBox区分.Text;

                //if (ThiIraiSbtID != NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                //{
                //    MsItemSbt itemSbt = null;
                //    if (form.comboBox区分.SelectedItem is MsItemSbt)
                //    {
                //        itemSbt = form.comboBox区分.SelectedItem as MsItemSbt;
                //    }
                //    if (itemSbt != null && itemSbt.MsItemSbtID.Length > 0)
                //    {
                //        mkItem.MsItemSbtID = itemSbt.MsItemSbtID;
                //        mkItem.MsItemSbtName = itemSbt.ItemSbtName;
                //    }
                //}
                MsItemSbt itemSbt = null;
                if (form.comboBox区分.SelectedItem is MsItemSbt)
                {
                    itemSbt = form.comboBox区分.SelectedItem as MsItemSbt;
                }
                if (itemSbt != null && itemSbt.MsItemSbtID.Length > 0)
                {
                    mkItem.MsItemSbtID = itemSbt.MsItemSbtID;
                    mkItem.MsItemSbtName = itemSbt.ItemSbtName;
                }
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                {
                    mkItem.ItemName = form.textBox品目.Text;
                }
                else
                {
                    //mkItem.ItemName = form.multiLineCombo品目.Text;
                    mkItem.ItemName = StringUtils.Escape(form.multiLineCombo品目.Text);

                    if (form.MsShoushuriItemDic.ContainsKey(mkItem.ItemName) == false)
                    {
                        mkItem.SaveDB = true;
                    }
                }

                BuildOdMkShousaiItems();
            }
            private void BuildOdMkShousaiItems()
            {
                foreach (OdMkShousaiItem shousaiItem in mkItem.OdMkShousaiItems)
                {
                    if (shousaiItem.OdMkShousaiItemID.Length == 0)
                    {
                        shousaiItem.OdMkShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                    }
                    if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID)
                    {
                        if (MsSsShousaiItemDic.ContainsKey(shousaiItem.ShousaiItemName) == false)
                        {
                            shousaiItem.SaveDB = true;
                        }
                    }
                }
            }
            public override void SetFocus()
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    if (form.textBox詳細品目_新規発注.Visible == false)
                    {
                        InitShousaiPanel();
                    }
                    form.textBox詳細品目_新規発注.Focus();
                }
                else
                {
                    if (form.multiLineCombo詳細品目_新規発注.Visible == false)
                    {
                        InitShousaiPanel();
                    }
                    form.multiLineCombo詳細品目_新規発注.Focus();
                }
            }
            private void Set詳細品目ToControl(OdMkShousaiItem item)
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    form.textBox詳細品目_新規発注.Text = item.ShousaiItemName;
                    form.labelMsVesselItemId_新規発注.Text = item.MsVesselItemID;
                }
                //else
                //{
                //    form.multiLineCombo詳細品目_新規発注.Text = item.ShousaiItemName;
                //}
                else if (item.MsVesselItemID != null && item.MsVesselItemID.Length > 0)
                {
                    form.multiLineCombo詳細品目_新規発注.Text = item.ShousaiItemName;
                    form.labelMsVesselItemId_新規発注.Text = item.MsVesselItemID;

                    form.multiLineCombo詳細品目_新規発注.ReadOnly = true;
                }
                else
                {
                    form.multiLineCombo詳細品目_新規発注.Text = item.ShousaiItemName;
                    form.labelMsVesselItemId_新規発注.Text = "";

                    form.multiLineCombo詳細品目_新規発注.ReadOnly = false;
                }
            }
            public void Set詳細品目ToModel(OdMkShousaiItem item)
            {
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID
                    && form.MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    item.ShousaiItemName = form.textBox詳細品目_新規発注.Text;
                    item.MsVesselItemID = form.labelMsVesselItemId_新規発注.Text;
                }
                //else
                //{
                //    //item.ShousaiItemName = form.multiLineCombo詳細品目_新規発注.Text;
                //    item.ShousaiItemName = StringUtils.Escape(form.multiLineCombo詳細品目_新規発注.Text);
                //}
                else if (form.labelMsVesselItemId_新規発注.Text.Length > 0)
                {
                    item.ShousaiItemName = StringUtils.Escape(form.multiLineCombo詳細品目_新規発注.Text);
                    item.MsVesselItemID = form.labelMsVesselItemId_新規発注.Text;
                }
                else
                {
                    item.ShousaiItemName = StringUtils.Escape(form.multiLineCombo詳細品目_新規発注.Text);
                }
            }

            public override bool ValidateShousai()
            {
                bool ret = true;

                TreeListViewNode node = form.treeList詳細品目.selectedNode();
                if (node != null)
                {
                    OdMkShousaiItem item = OdMkShousaiItemNodes[node];
                    string name = item.ShousaiItemName;
                    name = name.Trim();
                    name = name.Replace(Environment.NewLine, "");
                    if (name.Length == 0)
                    {
                        ret = false;
                    }
                }
                return ret;
            }

            public override void ValidateFields(ref string errMessage)
            {
                // 詳細品目
                for (int i = 0; i < mkItem.OdMkShousaiItems.Count; i++)
                {
                    OdMkShousaiItem si = mkItem.OdMkShousaiItems[i];

                    if (si.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                    {
                        continue;
                    }
                    // (2009.09.21: aki <入力時にブロックしているのでここでは判定の必要なし>)
                    //if (si.ShousaiItemName.Length == 0)
                    //{
                    //    errMessage += "・No. " + (i + 1) + " の詳細品目を入力して下さい\n";
                    //}
                    //else if (si.ShousaiItemName.Length > 500)
                    //{
                    //    errMessage += "・No. " + (i + 1) + " の詳細品目は500文字以下で入力して下さい\n";
                    //}
                    string name = si.ShousaiItemName.Trim().Replace(Environment.NewLine, " ");
                    if (name.Length > Err文字表示数)
                    {
                        name = name.Substring(0, Err文字表示数);
                        name = name + "…";
                    }
                    if (si.Count < 0)
                    {
                        errMessage += "・「 " + name + " 」の発注数を入力して下さい\n";
                    }
                    if (si.Tanka < 0)
                    {
                        errMessage += "・「 " + name + " 」の単価を入力して下さい\n";
                    }
                    if (si.Bikou.Length > 500)
                    {
                        errMessage += "・「 " + name + " 」の備考（品名、規格等）は500文字以下で入力して下さい\n";
                    }
                }
            }

            public override void CreateShousai(List<DataGridViewRow> rows)
            {
                foreach (DataGridViewRow row in rows)
                {
                    MsVesselItemVessel vesselItem = row.Cells["obj"].Value as MsVesselItemVessel;
                    OdMkShousaiItem shousaiItem = new OdMkShousaiItem();
                    shousaiItem.OdMkShousaiItemID = "";
                    shousaiItem.OdMmShousaiItemID = "新規発注";
                    shousaiItem.ShousaiItemName = "";
                    shousaiItem.MsTaniID = "";
                    shousaiItem.MsTaniName = "";
                    shousaiItem.OdMmShousaiItemCount = -1;
                    shousaiItem.Count = -1;
                    shousaiItem.Bikou = "";

                    // 特定船用品をセット
                    shousaiItem.OdMkShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                    shousaiItem.MsVesselItemID = vesselItem.MsVesselItemID;
                    shousaiItem.ShousaiItemName = vesselItem.VesselItemName;
                    shousaiItem.MsTaniID = vesselItem.MsTaniID;
                    shousaiItem.MsTaniName = vesselItem.MsTaniName;

                    int count = 0;
                    if (row.Cells["数量"].Value != null)
                    {
                        if (int.TryParse((row.Cells["数量"].Value as string), out count))
                        {
                            shousaiItem.Count = count;
                        }
                    }

                    shousaiItem.Bikou = vesselItem.Bikou;

                    mkItem.OdMkShousaiItems.Add(shousaiItem);
                }
            }
            public override List<string> GetVesselItemIdList()
            {
                List<string> ret = new List<string>();

                foreach (OdMkShousaiItem shousaiItem in mkItem.OdMkShousaiItems)
                {
                    if (shousaiItem.MsVesselItemID != null && shousaiItem.MsVesselItemID != "" && shousaiItem.CancelFlag == 0)
                    {
                        ret.Add(shousaiItem.MsVesselItemID);
                    }
                }

                return ret;
            }
        }
        #endregion
    }
}