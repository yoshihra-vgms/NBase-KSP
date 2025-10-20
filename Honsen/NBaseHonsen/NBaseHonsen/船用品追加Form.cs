using SyncClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseUtil;

namespace NBaseHonsen
{
    public partial class 船用品追加Form : Form
    {
        public OdThiShousaiItem ShousaiItem = null;

        public 船用品追加Form()
        {
            InitializeComponent();

            Text = NBaseCommon.Common.WindowTitle("", "船用品追加", WcfServiceWrapper.ConnectedServerID);
        }

        private void 船用品追加Form_Load(object sender, EventArgs e)
        {
            List<MsVesselItemCategory> vesselItemCategorys = MsVesselItemCategory.GetRecords(NBaseCommon.Common.LoginUser);
            foreach (MsVesselItemCategory category in vesselItemCategorys)
            {
                // 対象となるのは「ペイント」以外
                if (category.CategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                    continue;

                comboBoxカテゴリ.Items.Add(category);
            }

            //List<MsSsShousaiItem> shousaiItems = MsSsShousaiItem.GetRecords(NBaseCommon.Common.LoginUser);
            //foreach (MsSsShousaiItem si in shousaiItems)
            //{
            //    multiLineCombo詳細品目.AutoCompleteCustomSource.Add(si.ShousaiItemName);
            //}
            List<OdThiShousaiItem> odThiShousaiItems = OdThiShousaiItem.GetRecordByThiIraiSbtID(NBaseCommon.Common.LoginUser, MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品), SyncClient.同期Client.LOGIN_VESSEL.MsVesselID);
            var sortedList = odThiShousaiItems.OrderBy(obj => obj.ShousaiItemName);

            foreach (OdThiShousaiItem si in sortedList)
            {
                if (multiLineCombo詳細品目.AutoCompleteCustomSource.Contains(si.ShousaiItemName) == false)
                    multiLineCombo詳細品目.AutoCompleteCustomSource.Add(si.ShousaiItemName);
            }

            comboBox単位.Items.Clear();
            foreach (MsTani t in MasterTable.instance().MsTaniList)
            {
                comboBox単位.Items.Add(t);
            }
        }

        private void button追加_Click(object sender, EventArgs e)
        {
            if (ValidateFields() == false)
            {
                return;
            }
            FillInstance();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void button閉じる_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool ValidateFields()
        {
            if (!(comboBoxカテゴリ.SelectedItem is MsVesselItemCategory))
            {
                MessageBox.Show("仕様・型式を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (multiLineCombo詳細品目.Text == null || multiLineCombo詳細品目.Text.Length == 0)
            {
                MessageBox.Show("詳細品目を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (multiLineCombo詳細品目.Text.Length > 500)
            {
                MessageBox.Show("詳細品目は500文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox備考.Text != null && textBox備考.Text.Length > 500)
            {
                MessageBox.Show("備考（品名、規格等）は500文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void FillInstance()
        {
            if (ShousaiItem == null)
            {
                ShousaiItem = new OdThiShousaiItem();
                ShousaiItem.OdThiShousaiItemID = "";
                ShousaiItem.ShousaiItemName = "";
                ShousaiItem.MsTaniID = "";
                ShousaiItem.MsTaniName = "";
                ShousaiItem.ZaikoCount = int.MinValue;
                ShousaiItem.Count = int.MinValue;
                ShousaiItem.Sateisu = int.MinValue;
                ShousaiItem.Bikou = "";
                ShousaiItem.OdAttachFileID = null;
                ShousaiItem.OdAttachFileName = null;

                ShousaiItem.OdThiShousaiItemID = System.Guid.NewGuid().ToString();
            }

            MsVesselItemCategory category = comboBoxカテゴリ.SelectedItem as MsVesselItemCategory;
            ShousaiItem.CategoryNumber = category.CategoryNumber;
            ShousaiItem.CategoryName = category.CategoryName;
            //ShousaiItem.MsVesselItemID = 
            //ShousaiItem.MsVesselItemName 
            ShousaiItem.ShousaiItemName = multiLineCombo詳細品目.Text;

            if (comboBox単位.SelectedItem is MsTani)
            {
                MsTani tani = comboBox単位.SelectedItem as MsTani;
                ShousaiItem.MsTaniID = tani.MsTaniID;
                ShousaiItem.MsTaniName = tani.TaniName;
            }
            ShousaiItem.Bikou = textBox備考.Text;

            int intVal = 0;
            int.TryParse(textBox在庫数.Text, out intVal);
            ShousaiItem.ZaikoCount = intVal;

            int.TryParse(textBox依頼数.Text, out intVal);
            ShousaiItem.Count = intVal;
        }
    }
}
