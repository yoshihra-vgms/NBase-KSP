using NBaseData.DAC;
using NBaseUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hachu
{
    public partial class 発注修正Form : Form
    {
        public 発注修正Form()
        {
            InitializeComponent();
        }

        private void 発注修正Form_Load(object sender, EventArgs e)
        {
            List<MsItemSbt> ItemSbtList = null;
            List<MsVesselItemCategory> VesselItemCategoryList = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ItemSbtList = serviceClient.MsItemSbt_GetRecords(NBaseCommon.Common.LoginUser);
                VesselItemCategoryList = serviceClient.MsVesselItemCategory_GetRecords(NBaseCommon.Common.LoginUser);
            }

            ItemSbtList.ForEach(o => comboBox_区分.Items.Add(o));
            VesselItemCategoryList.ForEach(o => comboBox_仕様型式.Items.Add(o));
        }

        private void comboBox_区分_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_区分.SelectedIndex == 0)
                comboBox_仕様型式.SelectedIndex = 0;
            else if (comboBox_区分.SelectedIndex == 1)
                comboBox_仕様型式.SelectedIndex = 1;
            else
                comboBox_仕様型式.SelectedIndex = 2;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (StringUtils.Empty(textBox_手配依頼番号.Text))
            {
                MessageBox.Show("手配依頼番号を入力してください");
                return;
            }

            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                string thiSBtId = (comboBox_区分.SelectedItem as MsItemSbt).MsItemSbtID;
                int categoryNumber = (comboBox_仕様型式.SelectedItem as MsVesselItemCategory).CategoryNumber;

                ret = serviceClient.BLC_区分_仕様型式編集(NBaseCommon.Common.LoginUser, textBox_手配依頼番号.Text, thiSBtId, categoryNumber);
            }
            if (ret)
            {
                MessageBox.Show("変更しました。");
            }
            else
            {
                MessageBox.Show("変更に失敗しました。");
            }
        }
    }
}
