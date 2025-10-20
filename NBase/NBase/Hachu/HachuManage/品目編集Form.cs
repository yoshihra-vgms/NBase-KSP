using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using NBaseData.DAC;
using Hachu.Models;
using Hachu.Utils;

namespace Hachu.HachuManage
{
    public partial class 品目編集Form : Form
    {
        //
        private readonly FormController formController;
        
        //
        private ItemTreeListView品目編集 treeList詳細品目;

        // 新規 or 変更
        public enum EDIT_MODE { 新規, 変更 }
        private int EditMode;

        // 手配依頼種別ID
        private string ThiIraiSbtID;
        
        // 船ID
        private int MsVesselID;

        // 小修理品目マスタ（フリーメンテナンス）
        public Dictionary<string, string> MsShoushuriItemDic = null;

        // 船用品カテゴリ
        List<MsVesselItemCategory> MsVesselItemCategorys = null;
        //private int MsVesselItemCategoryNumber = MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント);
        private int MsVesselItemCategoryNumber = -1;

        public List<OdAttachFile> 添付Files = null;


        // コンストラクタ

        public 品目編集Form(int editMode, string thiIraiSbtID, int vesselID, ref OdThiItem thiItem, ref List<OdAttachFile> 添付Files)
        {
            this.EditMode = editMode;
            this.ThiIraiSbtID = thiIraiSbtID;
            this.MsVesselID = vesselID;

            this.添付Files = 添付Files;

            formController = new FormController手配依頼(this, vesselID, thiIraiSbtID, ref thiItem);

            InitializeComponent();
        }

        public 品目編集Form(int editMode, string thiIraiSbtID, int vesselID, ref OdMkItem mkItem)
        {
            this.EditMode = editMode;
            this.ThiIraiSbtID = thiIraiSbtID;
            this.MsVesselID = vesselID;
            formController = new FormController見積回答(this, vesselID, thiIraiSbtID, ref mkItem);

            InitializeComponent();
        }

        public 品目編集Form(int editMode, string thiIraiSbtID, int vesselID, ref OdMkItem mkItem, int opt)
        {
            this.EditMode = editMode;
            this.ThiIraiSbtID = thiIraiSbtID;
            this.MsVesselID = vesselID;
            formController = new FormController新規発注(this, vesselID, thiIraiSbtID, ref mkItem);

            InitializeComponent();
        }

        public 品目編集Form(int editMode, string thiIraiSbtID, int vesselID, ref OdJryItem jryItem)
        {
            this.EditMode = editMode;
            this.ThiIraiSbtID = thiIraiSbtID;
            this.MsVesselID = vesselID;
            formController = new FormController受領(this, vesselID, thiIraiSbtID, ref jryItem);

            InitializeComponent();
        }

        public 品目編集Form(int editMode, int sbt, string thiIraiSbtID, int vesselID, ref OdShrItem shrItem)
        {
            this.EditMode = editMode;
            this.ThiIraiSbtID = thiIraiSbtID;
            this.MsVesselID = vesselID;
            formController = new FormController支払(this, sbt, vesselID, thiIraiSbtID, ref shrItem);

            InitializeComponent();
        }






        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 品目編集Form_Load(object sender, EventArgs e)
        private void 品目編集Form_Load(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            List<MsItemSbt> MsItemSbts = null;
            List<MsShoushuriItem> MsShoushuriItems = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsItemSbts = serviceClient.MsItemSbt_GetRecords(NBaseCommon.Common.LoginUser);
                MsShoushuriItems = serviceClient.MsShoushuriItem_GetRecordByMsVesselID(NBaseCommon.Common.LoginUser, MsVesselID);
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                {
                    MsVesselItemCategorys = serviceClient.MsVesselItemCategory_GetRecords(NBaseCommon.Common.LoginUser);
                }
            }

            // 区分
            #region
            //if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
            //{
            //    comboBox区分.Items.Clear();
            //    comboBox区分.Items.Add("機関部");
            //    comboBox区分.Items.Add("甲板部");
            //    comboBox区分.SelectedItem = null;
            //}
            //else
            //{
            //    comboBox区分.Items.Clear();
            //    foreach (MsItemSbt itemSbt in MsItemSbts)
            //    {
            //        comboBox区分.Items.Add(itemSbt);
            //    }
            //}
            comboBox区分.Items.Clear();
            foreach (MsItemSbt itemSbt in MsItemSbts)
            {
                comboBox区分.Items.Add(itemSbt);
            }
            #endregion

            // 仕様・型式
            #region
            if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
            {
                textBox品目.Visible = true;
                buttonカテゴリ選択.Visible = true;
                multiLineCombo品目.Visible = false;
            }
            else
            {
                textBox品目.Visible = false;
                buttonカテゴリ選択.Visible = false;
                multiLineCombo品目.Visible = true;
                multiLineCombo品目.MaxLength = 500;

                // 品目ComboBox初期化
                MsShoushuriItemDic = new Dictionary<string, string>();
                foreach (MsShoushuriItem item in MsShoushuriItems)
                {
                    multiLineCombo品目.AutoCompleteCustomSource.Add(item.ItemName);
                    if (MsShoushuriItemDic.ContainsKey(item.ItemName) == false)
                    {
                        MsShoushuriItemDic.Add(item.ItemName, item.ItemName);
                    }
                }
            }
            #endregion

            treeListView.Location = new Point(22, 148); //new Point(22, 118);
            treeListView.Anchor = AnchorStyles.None;

            if (EditMode == (int)EDIT_MODE.新規)
            {
                button削除.Enabled = false;
            }
            else
            {
                button削除.Enabled = true;
            }

            formController.InitializeForm();

            //// 2011.05.19: Update 
            ////textBoxヘッダ.Select();
            ////textBoxヘッダ.SelectionStart = textBoxヘッダ.Text.Length;
            //if (formController.GetSbt() != (int)OdShr.SBT.支払)
            //{
            //    textBoxヘッダ.Select();
            //    textBoxヘッダ.SelectionStart = textBoxヘッダ.Text.Length;
            //}
            //// 2011.05.19: Update End



            //if (MsVesselItemCategoryNumber != -1)
            //{
            //    if (formController is FormController受領)
            //    {
            //        button特定品選択.Enabled = false;
            //    }
            //    else
            //    {
            //        if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
            //        {

            //            if (MsVesselItemCategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
            //            {
            //                button特定品選択.Enabled = false;
            //            }
            //            else
            //            {
            //                button特定品選択.Enabled = true;
            //            }
            //        }
            //        else
            //        {
            //            button特定品選択.Enabled = false;
            //        }
            //    }
            //}


            this.Cursor = System.Windows.Forms.Cursors.Default;
        }
        #endregion

        /// <summary>
        /// 「登録」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button登録_Click(object sender, EventArgs e)
        private void button登録_Click(object sender, EventArgs e)
        {
            if (ValidateFields() == false)
            {
                return;
            }
            formController.button登録_Click();

            // Formを閉じる
            DialogResult = DialogResult.OK;
            Close();
        }
        #endregion

        /// <summary>
        /// 「削除」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button削除_Click(object sender, EventArgs e)
        private void button削除_Click(object sender, EventArgs e)
        {
            // 削除の確認
            if (MessageBox.Show("この仕様・型式/詳細品目を削除します。よろしいですか？", "削除確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            formController.button品目削除_Click();

            // Formを閉じる
            DialogResult = DialogResult.OK;
            Close();
        }
        #endregion

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button閉じる_Click(object sender, EventArgs e)
        private void button閉じる_Click(object sender, EventArgs e)
        {
            // Formを閉じる
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion


        public void UpdateTreeListView()
        {
            formController.UpdateTreeListView();
        }
        private bool ValidateFields()
        {
            string errMessage = "";

            // 区分
            MsItemSbt itemSbt = null;
            //if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
            //{
            //    if (comboBox区分.SelectedItem == null)
            //    {
            //        errMessage += "・区分を選択してください。\n";
            //    }
            //}
            //else
            //{
            //    if (comboBox区分.SelectedItem is MsItemSbt)
            //    {
            //        itemSbt = comboBox区分.SelectedItem as MsItemSbt;
            //    }
            //    if (itemSbt == null)
            //    {
            //        errMessage += "・区分を選択してください。\n";
            //    }
            //}
            if (comboBox区分.SelectedItem is MsItemSbt)
            {
                itemSbt = comboBox区分.SelectedItem as MsItemSbt;
            }
            if (itemSbt == null)
            {
                errMessage += "・区分を選択してください。\n";
            }

            // 仕様・型式
            if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
            {
                if (textBox品目.Text.Length == 0)
                {
                    errMessage += "・仕様・型式を選択してください。\n";
                }
            }
            else
            {
                if (multiLineCombo品目.Text.Length == 0)
                {
                    errMessage += "・仕様・型式を入力してください。\n";
                }
                else if (multiLineCombo品目.Text.Length > 500)
                {
                    errMessage += "・仕様・型式は500文字以下で入力して下さい\n";
                }
            }

            // 詳細品目
            formController.ValidateFields(ref errMessage);

            if (errMessage.Length > 0)
            {
                errMessage = "入力項目に不備があります。\n====================================\n" + errMessage;
                MessageBox.Show(errMessage, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }


        private void multiLineCombo品目_Leave(object sender, EventArgs e)
        {
            formController.Init詳細();
            Control_Leave(sender, e);
        }

        private void treeListView_BeforeSelect(object sender, LidorSystems.IntegralUI.ObjectCancelEventArgs e)
        {
            formController.BeforeSelect();
        }

        private void treeListView_AfterSelect(object sender, LidorSystems.IntegralUI.ObjectEventArgs e)
        {
            formController.AfterSelect();
        }

        private void multiLineCombo詳細品目_Enter(object sender, EventArgs e)
        {
            Control_Enter(sender, e);

            TreeListViewNode node = treeList詳細品目.selectedNode();
            if (node == null)
            {
                treeList詳細品目.SelectLastNode();
            }
        }

        private void multiLineCombo詳細品目_Leave(object sender, EventArgs e)
        {
            formController.multiLineCombo詳細品目_Leave();
            Control_Leave(sender, e);
        }

        private void comboBox単位_Leave(object sender, EventArgs e)
        {
            formController.comboBox単位_Leave();
            Control_Leave(sender, e);
        }

        private void textBox在庫数_Leave(object sender, EventArgs e)
        {
            formController.textBox在庫数_Leave();
            Control_Leave(sender, e);
        }

        private void textBox依頼数_Leave(object sender, EventArgs e)
        {
            formController.textBox依頼数_Leave();
            Control_Leave(sender, e);
        }

        private void textBox回答数_Leave(object sender, EventArgs e)
        {
            formController.textBox回答数_Leave();
            Control_Leave(sender, e);
        }

        private void textBox受領数_Leave(object sender, EventArgs e)
        {
            formController.textBox受領数_Leave();
            Control_Leave(sender, e);
        }

        private void textBox数量_Leave(object sender, EventArgs e)
        {
            formController.textBox数量_Leave();
            Control_Leave(sender, e);
        }

        private void textBox発注数_Leave(object sender, EventArgs e)
        {
            formController.textBox発注数_Leave();
            Control_Leave(sender, e);
        }

        private void textBox単価_Leave(object sender, EventArgs e)
        {
            formController.textBox単価_Leave();
            Control_Leave(sender, e);
        }

        private void textBox備考_Leave(object sender, EventArgs e)
        {
            formController.textBox備考_Leave();
            Control_Leave(sender, e);
        }

        private void textBox備考_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                //Tabキーが押されてもフォーカスが移動しないようにする
                case Keys.Tab:
                    e.IsInputKey = true;
                    formController.Set詳細();
                    break;
            }
        }

        private void treeListView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                //Tabキーが押されてもフォーカスが移動しないようにする
                case Keys.Tab:
                    e.IsInputKey = true;
                    formController.Set詳細();
                    break;
            }
        }

        private void Control_Enter(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Hachu.Common.CommonDefine.FOCUS_BACK_COLOR;
            if (sender is TextBox)
            {
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }
        }

        private void Control_Leave(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.White;
        }

        private void 詳細Control_Enter(object sender, EventArgs e)
        {
            if (formController.ValidateShousai())
            {
                Control_Enter(sender, e);
            }
            else
            {
                MessageBox.Show("詳細品目名を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // 2009.10.01
                //とばすと、次のイベントが１回うまく反応しない感じなので、とばさない
                //formController.SetFocus();
            }
        }

        private void textBox備考_Enter(object sender, EventArgs e)
        {
            // Tabキーが残っていることへの対応策
            // 備考文字がタブ、スペース、改行を除いて、長さが０なら
            // それらの文字はなしとする
            string tabRemovedStr = (((TextBox)sender).Text).Replace("\t", "");
            string t = formController.GetBikou(tabRemovedStr);
            if (t.Length == 0)
            {
                ((TextBox)sender).Text = "";
            }
            else
            {
                ((TextBox)sender).Text = tabRemovedStr;
            }
            詳細Control_Enter(sender, e);
        }

        private void button詳細品目削除_Click(object sender, EventArgs e)
        {
            formController.button詳細品目削除_Click();
        }

        private void button選択_Click(object sender, EventArgs e)
        {
            if (MsVesselItemCategoryNumber == -1)
            {
                MessageBox.Show("仕様・型式が選択されていません。先に、仕様・型式を選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            詳細品目選択Form form = new 詳細品目選択Form(MsVesselID,MsVesselItemCategoryNumber);
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            MsVesselItemVessel vesselItemVessel = form.SelectedVesselItemVessel;
            formController.button選択_Click(vesselItemVessel);
        }

        private void buttonカテゴリ選択_Click(object sender, EventArgs e)
        {
            int count = treeListView.Nodes.Count;
            船用品カテゴリ選択Form form = new 船用品カテゴリ選択Form();
            form.ShousaiCount = count;
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            MsVesselItemCategory vesselItemCategory = form.SelectedVesselItemCategory;
            MsVesselItemCategoryNumber = vesselItemCategory.CategoryNumber;
            textBox品目.Text = vesselItemCategory.CategoryName;

            if (count > 1)
            {
                formController.button品目削除_Click();
                formController.UpdateTreeListView();
            }

            //// 2015.03
            //if (vesselItemCategory.CategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
            //{
            //    button特定品選択.Enabled = false;
            //}
            //else
            //{
            //    button特定品選択.Enabled = true;

            //    // 2016.08.08 次期改造
            //    // 特定船用品選択Formは「特定品」ボタンからのみとするように改造
            //    {

            //    //List<string> selectedItemIdList = formController.GetVesselItemIdList();

            //    //this.Cursor = Cursors.WaitCursor;

            //    //特定船用品選択Form form2 = new 特定船用品選択Form(MsVesselID, vesselItemCategory, selectedItemIdList);
            //    //if (form2.ShowDialog() == DialogResult.OK)
            //    //{
            //    //    formController.CreateShousai(form2.SelectedRows);
            //    //    formController.UpdateTreeListView();
            //    //}

            //    //this.Cursor = Cursors.Default;

            //    }
            //}

            formController.Init詳細();
        }

        private void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void textBox備考_TextChanged(object sender, EventArgs e)
        {
            string tabRemovedStr = (((TextBox)sender).Text).Replace("\t", "");
            ((TextBox)sender).Text = tabRemovedStr;
        }





        private void button仕様型式添付選択_Click(object sender, EventArgs e)
        {
            formController.button仕様型式添付選択_Click();

        }

        private void button仕様型式添付削除_Click(object sender, EventArgs e)
        {
            textBox仕様型式添付.Text = "";
            formController.button仕様型式添付削除_Click();
        }

        private void button詳細添付選択_Click(object sender, EventArgs e)
        {
            formController.button詳細添付選択_Click();
        }

        private void button詳細添付削除_Click(object sender, EventArgs e)
        {
            formController.button詳細添付削除_Click();
        }
　   }
}
