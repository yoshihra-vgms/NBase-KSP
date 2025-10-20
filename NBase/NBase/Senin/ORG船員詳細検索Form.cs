using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Senin.util;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;

namespace Senin
{
    public partial class ORG船員詳細検索Form : Form
    {
        private List<AdvancedConditionItem> AndConditionList;
        private List<AdvancedConditionItem> OrConditionList;



        private List<SiAdvancedSearchConditionHead> AdvancedSearchConditionHeadList;

        private SiAdvancedSearchConditionHead AdvancedSearchConditionHead;
        private List<SiAdvancedSearchConditionItem> AdvancedSearchConditionItemList;
        private List<SiAdvancedSearchConditionValue> AdvancedSearchConditionValueList;

        private List<int> PresentationItemSetNoList;
        private List<SiPresentaionItem> PresentationItemList;

        private 船員Form MainForm;
        public void SetMainForm(船員Form mainForm)
        {
            MainForm = mainForm;
        }

        private static ORG船員詳細検索Form instance;

        public static ORG船員詳細検索Form Instance()
        {
            if (instance == null)
            {
                instance = new ORG船員詳細検索Form();
            }
            return instance;
        }

        public static new void Dispose()
        {
            if (instance != null)
            {
                instance.Hide();
                instance = null;
            }
        }

        private ORG船員詳細検索Form()
        {
            InitializeComponent();
        }

        private void ORG船員詳細検索Form_Load(object sender, EventArgs e)
        {
            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    AdvancedSearchConditionHeadList = serviceClient.BLC_Get船員検索条件(NBaseCommon.Common.LoginUser);

                    InitConditionName();
                }
            }, "データ取得中です...");

            progressDialog.ShowDialog();


            AndConditionList = new List<AdvancedConditionItem>();
            AddConditionUI(panel_AddCondition, AndConditionList);
            AddConditionUI(panel_AddCondition, AndConditionList);
            AddConditionUI(panel_AddCondition, AndConditionList);
            AddConditionUI(panel_AddCondition, AndConditionList);
            AddConditionUI(panel_AddCondition, AndConditionList);

            OrConditionList = new List<AdvancedConditionItem>();
            AddConditionUI(panel_OrCondition, OrConditionList);
            AddConditionUI(panel_OrCondition, OrConditionList);


            PresentationItemSetNoList = new List<int>();
            PresentationItemSetNoList.Add(1);

            PresentationItemList = new List<SiPresentaionItem>();

            SetPresentationItemGrid();
        }

        private void ORG船員詳細検索Form_Activated(object sender, EventArgs e)
        {
            int a = 0;
        }

        private void ORG船員詳細検索Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
               e.Cancel = true;
               this.Hide();
            }
        }

        private void InitConditionName()
        {
            singleLineCombo_ConditionName.Text = "";
            singleLineCombo_ConditionName.Items.Clear();
            singleLineCombo_ConditionName.AutoCompleteCustomSource.Clear();

            foreach (SiAdvancedSearchConditionHead head in AdvancedSearchConditionHeadList)
            {
                singleLineCombo_ConditionName.Items.Add(head);
                singleLineCombo_ConditionName.AutoCompleteCustomSource.Add(head.Name);
            }
        }

        private void button_Open_Click(object sender, EventArgs e)
        {
            string name = "";
            if (singleLineCombo_ConditionName.Text != null && singleLineCombo_ConditionName.Text.Length > 0)
            {
                name = singleLineCombo_ConditionName.Text;
            }

            if (AdvancedSearchConditionHeadList.Any(obj => obj.Name == name))
            {
                AdvancedSearchConditionHead = AdvancedSearchConditionHeadList.Where(obj => obj.Name == name).First();

                AdvancedSearchConditionItemList = AdvancedSearchConditionHead.ConditionItemList;
                AdvancedSearchConditionValueList = AdvancedSearchConditionHead.ConditionValueList;


                // GUI をリセット
                ResetConditionUI();
                foreach (AdvancedConditionItem conditionItem in AndConditionList)
                {
                    conditionItem.Clear();
                }
                foreach (AdvancedConditionItem conditionItem in OrConditionList)
                {
                    conditionItem.Clear();
                }

                // GUI に検索条件をセットする
                if (AdvancedSearchConditionItemList.Any(obj => obj.AndOrFlag == SiAdvancedSearchConditionItem.AND_OR_FLAG_AND))
                {
                    var conditonList = AdvancedSearchConditionItemList.Where(obj => obj.AndOrFlag == SiAdvancedSearchConditionItem.AND_OR_FLAG_AND);

                    AdvancedConditionItem conditionItem = null;
                    int idx = 0;
                    var orderNolList = conditonList.Select(obj => obj.ShowOrder).Distinct().OrderBy(obj => obj);
                    foreach(int orderNo in orderNolList)
                    {
                        var conditons = conditonList.Where(obj => obj.ShowOrder == orderNo);
                        var values = AdvancedSearchConditionValueList.Where(obj => obj.ShowOrder == orderNo);

                        if (idx == AndConditionList.Count)
                        {
                            AddConditionUI(panel_AddCondition, AndConditionList);
                        }
                        conditionItem = AndConditionList[idx];

                        conditionItem.SetCondition(conditons.ToList(), values.ToList());

                        idx++;
                    }
                }
                if (AdvancedSearchConditionItemList.Any(obj => obj.AndOrFlag == SiAdvancedSearchConditionItem.AND_OR_FLAG_OR))
                {
                    var conditonList = AdvancedSearchConditionItemList.Where(obj => obj.AndOrFlag == SiAdvancedSearchConditionItem.AND_OR_FLAG_OR);

                    AdvancedConditionItem conditionItem = null;
                    int idx = 0;
                    var orderNolList = conditonList.Select(obj => obj.ShowOrder).Distinct().OrderBy(obj => obj);
                    foreach (int orderNo in orderNolList)
                    {
                        var conditons = conditonList.Where(obj => obj.ShowOrder == orderNo);
                        var values = AdvancedSearchConditionValueList.Where(obj => obj.ShowOrder == orderNo);

                        if (idx == OrConditionList.Count)
                        {
                            AddConditionUI(panel_OrCondition, OrConditionList);
                        }
                        conditionItem = OrConditionList[idx];

                        conditionItem.SetCondition(conditons.ToList(), values.ToList());
                    }
                }
            }
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            CreateCondtions();
            if (AdvancedSearchConditionHead.Name == null || AdvancedSearchConditionHead.Name.Length == 0)
            {
                singleLineCombo_ConditionName.BackColor = Color.Pink;
                MessageBox.Show("条件セット名を入力してください.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                singleLineCombo_ConditionName.BackColor = Color.White;
                return;
            }

            if (AdvancedSearchConditionItemList.Count() == 0 || AdvancedSearchConditionValueList.Count() == 0 || AdvancedSearchConditionValueList.Any(obj => obj.ItemValueFlag == SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE) == false)
            {
                MessageBox.Show("検索条件がありません", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            AdvancedSearchConditionHead.ConditionItemList = AdvancedSearchConditionItemList;
            AdvancedSearchConditionHead.ConditionValueList = AdvancedSearchConditionValueList;


            bool ret = true;
            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    if (AdvancedSearchConditionHead.IsNew())
                    {
                        AdvancedSearchConditionHead.SiAdvancedSearchConditionHeadID = NBaseUtil.Common.NewID();
                    }

                    ret = serviceClient.BLC_船員検索条件登録(NBaseCommon.Common.LoginUser, AdvancedSearchConditionHead);

                    if (ret)
                    {
                        AdvancedSearchConditionHeadList = serviceClient.BLC_Get船員検索条件(NBaseCommon.Common.LoginUser);
                    }
                }
            }, "検索条件セットを登録しています...");

            progressDialog.ShowDialog();

            if (ret)
            {
                MessageBox.Show(this, "保存しました.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                InitConditionName();
                singleLineCombo_ConditionName.Text = AdvancedSearchConditionHead.Name;
            }
            else
            {
                MessageBox.Show(this, "保存に失敗しました.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        /// <summary>
        /// 「Delete」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_Delete_Click(object sender, EventArgs e)
        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (AdvancedSearchConditionHead == null)
            {
                return;
            }

            if (AdvancedSearchConditionHead.IsNew())
            {
                return;
            }

            if (MessageBox.Show(this, "検索条件セットを削除します." + System.Environment.NewLine + "よろしいですか?",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                AdvancedSearchConditionHead.DeleteFlag = 1;

                bool ret = true;
                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        ret = serviceClient.BLC_船員検索条件登録(NBaseCommon.Common.LoginUser, AdvancedSearchConditionHead);
                    }
                }, "検索条件セットを削除しています...");

                progressDialog.ShowDialog();

                if (ret == true)
                {
                    MessageBox.Show("削除しました.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    AdvancedSearchConditionHeadList.Remove(AdvancedSearchConditionHead);
                    InitConditionName();
                    Clear();
                }
                else
                {
                    MessageBox.Show("削除に失敗しました.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion

        /// <summary>
        /// 「Search」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_Search_Click(object sender, EventArgs e)
        private void button_Search_Click(object sender, EventArgs e)
        {
            CreateCondtions();
            if (ParentForm != null && AdvancedSearchConditionItemList != null && AdvancedSearchConditionValueList != null)
            {
                if (AdvancedSearchConditionItemList.Count() == 0 || AdvancedSearchConditionValueList.Count() == 0 || AdvancedSearchConditionValueList.Any(obj => obj.ItemValueFlag == SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE) == false)
                {
                    MessageBox.Show("検索条件がありません", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MainForm.SetAdvancedSearchConditions(AdvancedSearchConditionItemList, AdvancedSearchConditionValueList, PresentationItemList);
                MainForm.Search船員();

                this.Hide();
            }
        }
        #endregion

        /// <summary>
        /// 「Clear」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_Clear_Click(object sender, EventArgs e)
        private void button_Clear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        #endregion


        /// <summary>
        ///（内部処理） 画面クリア
        /// </summary>
        #region private void Clear()
        private void Clear()
        {
            ResetConditionUI();

            foreach (AdvancedConditionItem conditionItem in AndConditionList)
            {
                conditionItem.Clear();
            }
            foreach (AdvancedConditionItem conditionItem in OrConditionList)
            {
                conditionItem.Clear();
            }

            AdvancedSearchConditionHead = null;
            AdvancedSearchConditionItemList = null;
            AdvancedSearchConditionValueList = null;

            singleLineCombo_ConditionName.Text = "";
        }
        #endregion

        /// <summary>
        /// 「Cancel」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_Cancel_Click(object sender, EventArgs e)
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Hide();
        }
        #endregion

        /// <summary>
        /// AndConditionの「Add」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_AndAdd_Click(object sender, EventArgs e)
        private void button_AndAdd_Click(object sender, EventArgs e)
        {
            AddConditionUI(panel_AddCondition, AndConditionList);
        }
        #endregion

        /// <summary>
        /// OrConditionの「Add」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_OrAdd_Click(object sender, EventArgs e)
        private void button_OrAdd_Click(object sender, EventArgs e)
        {
            AddConditionUI(panel_OrCondition, OrConditionList);
        }
        #endregion


        /// <summary>
        /// （内部処理）検索条件を１行追加する
        /// 　　　　　　AndConditionの「Add」ボタンクリック
        /// 　　　　　　OrConditionの「Add」ボタンクリック　から呼び出される
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="conditionList"></param>
        #region private void AddConditionUI(Panel panel, List<AdvancedConditionItem> conditionList)
        private void AddConditionUI(Panel panel, List<AdvancedConditionItem> conditionList)
        {
            AdvancedConditionItem conditionItem = new AdvancedConditionItem();
            conditionItem.selected += new AdvancedConditionItem.SelectedEventHandler(AdvancedConditionItem_Selected);
            conditionList.Add(conditionItem);

            conditionItem.Location = new Point(3, 3 + ((conditionList.Count()-1) * (conditionItem.Height + 3)));
            panel.Controls.Add(conditionItem);
        }
        #endregion

        /// <summary>
        /// （内部処理）
        /// </summary>
        #region private void ResetConditionUI()
        private void ResetConditionUI()
        {
            if (panel_AddCondition.Controls.Count > 5)
            {
                int count = panel_AddCondition.Controls.Count;
                for(int i = count; i > 5; i -- )
                {
                    panel_AddCondition.Controls.RemoveAt(i-1);
                    AndConditionList.RemoveAt(i-1);
                }
            }
            if (panel_OrCondition.Controls.Count > 2)
            {
                int count = panel_OrCondition.Controls.Count;
                for (int i = count; i > 2; i--)
                {
                    panel_OrCondition.Controls.RemoveAt(i-1);
                    OrConditionList.RemoveAt(i-1);
                }
            }
        }
        #endregion

        /// <summary>
        /// AdvancedConditionItem の選択時コールバック
        /// 該当するPresentaionItemをセットする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region public void AdvancedConditionItem_Selected(object sender, EventArgs e)
        public void AdvancedConditionItem_Selected(object sender, EventArgs e)
        {
            PresentationItemSetNoList.Clear();
            PresentationItemSetNoList.Add(1);
            List<int> tmp = new List<int>();


            foreach(AdvancedConditionItem conditionItem in AndConditionList)
            {
                int setNo = conditionItem.GetPresentationItemSetNo();
                if (setNo > 0 && tmp.Contains(setNo) == false)
                {
                    tmp.Add(setNo);
                }
            }
            foreach (AdvancedConditionItem conditionItem in OrConditionList)
            {
                int setNo = conditionItem.GetPresentationItemSetNo();
                if (setNo > 0 && tmp.Contains(setNo) == false)
                {
                    tmp.Add(setNo);
                }
            }

            PresentationItemSetNoList.AddRange(tmp.OrderBy(obj => obj));

            SetPresentationItemGrid();
        }
        #endregion


        /// <summary>
        /// 「PresentaionItem」の表示
        /// </summary>
        #region private void SetPresentationItemGrid()
        private void SetPresentationItemGrid()
        {
            List<SiPresentaionItem> src = new List<SiPresentaionItem>();
            List<SiPresentaionItem> dst = new List<SiPresentaionItem>();
            src.AddRange(PresentationItemList);

            PresentationItemList.Clear();
            foreach(int setNo in PresentationItemSetNoList)
            {
                var presentationItemSet = SeninTableCache.instance().GetMsSiPresentationItemList(NBaseCommon.Common.LoginUser).Where(obj => obj.SetNo == setNo);
                if (presentationItemSet.Count() == 0)
                    continue;

                foreach(MsSiPresentationItem item in presentationItemSet)
                {
                    var tmp = src.Where(obj => obj.MsSiPresentaionItemID == item.MsSiPresentationItemID);
                    if (tmp.Count() > 0)
                    {
                        dst.Add(tmp.First());
                    }
                    else
                    {
                        SiPresentaionItem newItem = new SiPresentaionItem();

                        newItem.MsSiPresentaionItemID = item.MsSiPresentationItemID;
                        newItem.SetNo = item.SetNo;
                        newItem.SN = item.SN;
                        newItem.Name = item.Name;
                        newItem.OnOffFlag = SiPresentaionItem.ON_OFF_FLAG_ON;

                        dst.Add(newItem);
                    }
                }
            }

            PresentationItemList.AddRange(dst.OrderBy(obj => obj.SetNo).ThenBy(obj => obj.SN));


            dataGridView1.Columns.Clear();
            dataGridView1.ColumnHeadersHeight = 30;
            int columnIndex = 0;
            foreach (SiPresentaionItem item in PresentationItemList)
            {
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = item.Name;
                dataGridView1.Columns.Add(textColumn);

                dataGridView1.Columns[columnIndex].Frozen = false;
                dataGridView1.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[columnIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (item.OnOffFlag == SiPresentaionItem.ON_OFF_FLAG_ON)
                {
                    // ON
                    dataGridView1.Columns[columnIndex].HeaderCell.Style.BackColor = SystemColors.Control;
                }
                else
                {
                    // OFF
                    dataGridView1.Columns[columnIndex].HeaderCell.Style.BackColor = SystemColors.ControlDark;
                }
                columnIndex++;
            }
        }
        #endregion

        /// <summary>
        /// 「PresentaionItem」のクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int columnIndex = e.ColumnIndex;

            if (PresentationItemList[columnIndex].SetNo == 1) // 基本セットは非表示にできない
                return;

            if (PresentationItemList[columnIndex].OnOffFlag == SiPresentaionItem.ON_OFF_FLAG_ON)
            {
                // ON -> OFF
                dataGridView1.Columns[columnIndex].HeaderCell.Style.BackColor = SystemColors.ControlDark;
                PresentationItemList[columnIndex].OnOffFlag = SiPresentaionItem.ON_OFF_FLAG_OFF;
            }
            else
            {
                // OFF -> ON
                dataGridView1.Columns[columnIndex].HeaderCell.Style.BackColor = SystemColors.Control;
                PresentationItemList[columnIndex].OnOffFlag = SiPresentaionItem.ON_OFF_FLAG_ON;
            }
        }
        #endregion




        private void CreateCondtions()
        {
            if (AdvancedSearchConditionHead == null)
            {
                AdvancedSearchConditionHead = new SiAdvancedSearchConditionHead();
                AdvancedSearchConditionHead.MsSeninCompanyID = null;
            }
            if (singleLineCombo_ConditionName.Text != null && singleLineCombo_ConditionName.Text.Length > 0)
            {
                AdvancedSearchConditionHead.Name = singleLineCombo_ConditionName.Text;
            }


            AdvancedSearchConditionItemList = new List<SiAdvancedSearchConditionItem>();
            AdvancedSearchConditionValueList = new List<SiAdvancedSearchConditionValue>();

            int showOrder = 0;

            // And条件
            foreach (AdvancedConditionItem conditionItem in AndConditionList)
            {
                MsSeninAdvancedFilter filter = conditionItem.GetConditions(SiAdvancedSearchConditionItem.AND_OR_FLAG_AND, showOrder);

                if (filter != null)
                {
                    AdvancedSearchConditionItemList.AddRange(filter.ItemList);
                    AdvancedSearchConditionValueList.AddRange(filter.ValueList);
                    showOrder++;
                }
            }
            // Or条件
            foreach (AdvancedConditionItem conditionItem in OrConditionList)
            {
                MsSeninAdvancedFilter filter = conditionItem.GetConditions(SiAdvancedSearchConditionItem.AND_OR_FLAG_OR, showOrder);

                if (filter != null)
                {
                    AdvancedSearchConditionItemList.AddRange(filter.ItemList);
                    AdvancedSearchConditionValueList.AddRange(filter.ValueList);
                    showOrder++;
                }
            }
        }

    }
}
