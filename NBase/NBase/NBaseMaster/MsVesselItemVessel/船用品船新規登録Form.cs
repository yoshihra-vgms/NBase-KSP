using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;

namespace NBaseMaster.MsVesselItemVessel
{
    public partial class 船用品船新規登録Form : Form
    {
        private int msVesselId = -1;
        private List<NBaseData.DAC.MsVessel> MsVesselList;
        private List<NBaseData.DAC.MsVesselItemCategory> MsVesselItemCategoryList;
        private List<NBaseData.DAC.MsVesselItem> MsVesselItemList;

        public 船用品船新規登録Form(int msVesselId)
        {
            this.msVesselId = msVesselId;
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "船用品船新規登録", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsVesselItemList = serviceClient.MsVesselItem_GetRecords(NBaseCommon.Common.LoginUser);   // 船用品
            }
            this.MakeDropDownList();
        }

        /// <summary>
        /// 登録ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Btn_Click(object sender, EventArgs e)
        {
            //----------------------------------------------
            // 入力値チェック
            //----------------------------------------------
            if (!入力値チェック())
            {
                return;
            }

            //----------------------------------------------
            // Insert
            //----------------------------------------------
            try
            {
                NBaseData.DAC.MsVesselItemVessel msVesselItemVessel = new NBaseData.DAC.MsVesselItemVessel();
                msVesselItemVessel.MsVesselItemVesselID = Guid.NewGuid().ToString();
                msVesselItemVessel.MsVesselID = this.msVesselId;
                msVesselItemVessel.MsVesselItemID = ((ListItem)MsVesselItem_comboBox.SelectedItem).Value;

                msVesselItemVessel.Bikou = textBox_備考.Text;

                msVesselItemVessel.RenewDate = DateTime.Today;
                msVesselItemVessel.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                msVesselItemVessel.SendFlag = 0;
                msVesselItemVessel.VesselID = 0;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    serviceClient.BLC_船用品登録(NBaseCommon.Common.LoginUser, msVesselItemVessel);

                    Message.Show確認("更新しました。");
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Message.Showエラー(ex.Message);
            }
        }

        /// <summary>
        /// 戻るボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 入力値のチェック
        /// </summary>
        /// <returns></returns>
        private bool 入力値チェック()
        {
            //---------------------------------------------------
            // 必須入力確認
            //---------------------------------------------------

            // 船用品名
            if (MsVesselItem_comboBox.Text.Length < 1)
            {
                Message.Showエラー("船用品名を選択してください。");
                return false;
            }

            return true;
        }

        /// <summary>
        /// ドロップダウンリストに値を設定する
        /// </summary>
        private void MakeDropDownList()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                // 船
                MsVesselList = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);

                int selIdx = 0;
                int idx = 0;
                foreach (NBaseData.DAC.MsVessel item in MsVesselList)
                {
                    MsVessel_comboBox.Items.Add(new ListItem(item.VesselName, item.MsVesselID.ToString()));
                    if (item.MsVesselID == this.msVesselId)
                    {
                        selIdx = idx;
                    }
                    idx++;
                }
                MsVessel_comboBox.SelectedIndex = selIdx;
                MsVessel_comboBox.Enabled = false; // 選択は無効とする

                // カテゴリー
                MsVesselItemCategoryList = serviceClient.MsVesselItemCategory_GetRecords(NBaseCommon.Common.LoginUser);
                foreach (NBaseData.DAC.MsVesselItemCategory item in MsVesselItemCategoryList)
                {
                    Category_comboBox.Items.Add(new ListItem(item.CategoryName, item.CategoryNumber.ToString()));
                }
                Category_comboBox.SelectedIndex = 0;

                MakeVesselItemDropDownList();
            }
        }

        /// <summary>
        /// 船用品ドロップダウンリストを作成する
        /// </summary>
        private void MakeVesselItemDropDownList()
        {
            List<NBaseData.DAC.MsVesselItemVessel> tempMsVesselItemVessel;
            List<NBaseData.DAC.MsVesselItem> tempMsVesselItem = new List<NBaseData.DAC.MsVesselItem>();
            int MsVesselID = Convert.ToInt32(((ListItem)MsVessel_comboBox.SelectedItem).Value);
            int CategoryNumber = int.MinValue;
            if (Category_comboBox.Text != "")
            {
                CategoryNumber = Convert.ToInt32(((ListItem)Category_comboBox.SelectedItem).Value);
            }

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                tempMsVesselItemVessel = serviceClient.MsVesselItemVessel_GetRecordByMsVesselIDVesselItemName2(NBaseCommon.Common.LoginUser, MsVesselID, CategoryNumber, "");
            }

            foreach (NBaseData.DAC.MsVesselItem item in MsVesselItemList)
            {
                if (CategoryNumber != int.MinValue)
                {
                    if (item.CategoryNumber != CategoryNumber)
                    {
                        continue;
                    }
                }

                bool flag = true;
                foreach (NBaseData.DAC.MsVesselItemVessel 登録済船用品 in tempMsVesselItemVessel)
                {
                    if (登録済船用品.MsVesselItemID == item.MsVesselItemID)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    tempMsVesselItem.Add(item);
                }
            }

            MsVesselItem_comboBox.Items.Clear();
            foreach (NBaseData.DAC.MsVesselItem item in tempMsVesselItem)
            {
                MsVesselItem_comboBox.Items.Add(new ListItem(item.VesselItemName, item.MsVesselItemID));
            }
            if (MsVesselItem_comboBox.Items.Count > 0)
            {
                MsVesselItem_comboBox.SelectedIndex = 0;

                NBaseData.DAC.MsVesselItem mvi = tempMsVesselItem[0];

                textBox_備考.Text = mvi.Bikou;
            }
        }

        private void MsVessel_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MakeVesselItemDropDownList();
        }

        private void Category_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_備考.Text = "";
            

            MakeVesselItemDropDownList();
        }


        private void MsVesselItem_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MsVesselItem_comboBox.SelectedItem is ListItem)
            {
                ListItem item = MsVesselItem_comboBox.SelectedItem as ListItem;

                var vesselItem = MsVesselItemList.Where(obj => obj.MsVesselItemID == item.Value);
                if (vesselItem.Count() > 0)
                {
                    NBaseData.DAC.MsVesselItem mvi = vesselItem.First();

                    textBox_備考.Text = mvi.Bikou;
                }
            }
        }

    }
}
