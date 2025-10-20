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
using NBaseCommon;

namespace Senin
{
    public partial class 船員詳細検索Form : Form
    {
        private List<AdvancedConditionItem> AndConditionList;
        private List<AdvancedConditionItem> OrConditionList;

        private List<SiAdvancedSearchConditionHead> AdvancedSearchConditionHeadList;

        private SiAdvancedSearchConditionHead AdvancedSearchConditionHead;
        private List<SiAdvancedSearchConditionItem> AdvancedSearchConditionItemList;
        private List<SiAdvancedSearchConditionValue> AdvancedSearchConditionValueList;



        private 船員Form mainForm;
        private ListSettingForm listSettingForm = null;


        public void SetMainForm(船員Form form)
        {
            mainForm = form;
        }
        public void SetListSettingForm(ListSettingForm form)
        {
            listSettingForm = form;
        }
        private static 船員詳細検索Form instance;

        public static 船員詳細検索Form Instance()
        {
            if (instance == null)
            {
                instance = new 船員詳細検索Form();
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

        private 船員詳細検索Form()
        {
            InitializeComponent();
            Init();
        }

        private void 船員詳細検索Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }





        private void Init()
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


            settingListControl1.RowHeaderVisible = false;
            settingListControl1.AllowUserToResizeColumns = false;
            settingListControl1.SortMode = DataGridViewColumnSortMode.NotSortable;



            SetSettingList();
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
            if (mainForm != null && AdvancedSearchConditionItemList != null && AdvancedSearchConditionValueList != null)
            {
                if (AdvancedSearchConditionItemList.Count() == 0 || AdvancedSearchConditionValueList.Count() == 0 || AdvancedSearchConditionValueList.Any(obj => obj.ItemValueFlag == SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE) == false)
                {
                    MessageBox.Show("検索条件がありません", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                mainForm.SetAdvancedSearchConditions(AdvancedSearchConditionItemList, AdvancedSearchConditionValueList);
                mainForm.Search船員();

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








        private void button項目設定_Click(object sender, EventArgs e)
        {
            if (listSettingForm != null)
            {
                // リスト項目設定画面に、リスト項目（マスタ）、ユーザのリスト項目をセットして、画面を表示
                listSettingForm.Init(NBaseCommon.Common.SeninListTitle, NBaseCommon.Common.SeninListItemList, NBaseCommon.Common.SeninListItemUserList);
                listSettingForm.ShowDialog();
            }
        }

        public void SetSettingList()
        {
            // リスト項目を一覧にセットする
            settingListControl1.SelectedUserListItemsList = NBaseCommon.Common.SeninListItemUserList.Where(o => o.Title == NBaseCommon.Common.SeninListTitle).ToList();
            settingListControl1.DrawList();
        }
    }
}
