using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;

namespace Hachu.HachuManage
{
    public partial class 特定品振替Form : Form
    {
        // 船ID
        private int MsVesselID; 
        private string SelectedItem = null;
        private List<MsVesselItemVessel> MsVesselItemVesselList;

        // 船用品カテゴリ
        public MsVesselItemCategory SelectedVesselItemCategory;
        // 船用品
        public MsVesselItemVessel SelectedVesselItemVessel;

        public 特定品振替Form(int msVesselID, string selectedItem)
        {
            this.MsVesselID = msVesselID;
            this.SelectedItem = selectedItem;

            InitializeComponent();
        }

        private void 特定品振替Form_Load(object sender, EventArgs e)
        {
            textBox_selectedItem.Text = SelectedItem;

            List<MsVesselItemCategory> msVesselItemCategorys = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msVesselItemCategorys = serviceClient.MsVesselItemCategory_GetRecords(NBaseCommon.Common.LoginUser);
            }
            foreach (MsVesselItemCategory category in msVesselItemCategorys)
            {
                comboBox_category.Items.Add(category);
            }
            comboBox_category.SelectedIndex = 1;
        }

        private void comboBox_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedVesselItemCategory = (comboBox_category.SelectedItem as MsVesselItemCategory);

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsVesselItemVesselList = serviceClient.MsVesselItemVessel_GetRecordByMsVesselIDVesselItemName2(NBaseCommon.Common.LoginUser, MsVesselID, SelectedVesselItemCategory.CategoryNumber, "");
            }

            singleLineCombo1.Items.Clear();
            singleLineCombo1.AutoCompleteCustomSource.Clear();
            foreach (MsVesselItemVessel obj in MsVesselItemVesselList)
            {
                if (obj.SpecificFlag != 1)
                    continue;

                singleLineCombo1.Items.Add(obj);
                singleLineCombo1.AutoCompleteCustomSource.Add(obj.VesselItemName);
            }
        }

        private void button変更_Click(object sender, EventArgs e)
        {
            if (!(singleLineCombo1.SelectedItem is MsVesselItemVessel))
            {
                MessageBox.Show("詳細品目を選択してください");
                return;
            }

            SelectedVesselItemVessel = (singleLineCombo1.SelectedItem as MsVesselItemVessel);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button閉じる_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


    }
}
