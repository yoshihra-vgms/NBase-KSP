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
    public partial class 船用品船詳細Form : Form
    {
        private NBaseData.DAC.MsVesselItemVessel MsVesselItemVessel;
        private List<NBaseData.DAC.MsVesselItemCategory> MsVesselItemCategoryList;

        public 船用品船詳細Form(NBaseData.DAC.MsVesselItemVessel target)
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "船用品船詳細", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            MsVesselItemVessel = target;
            MakeDropDownList();
            SetItems(MsVesselItemVessel);
        }

        /// <summary>
        /// 削除ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void Delete_Btn_Click(object sender, EventArgs e)
        private void Delete_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                //-------------------------------------------------------
                // 確認
                //-------------------------------------------------------
                if (Message.Show問合せ("この船用品船を削除します。よろしいですか？") == false)
                {
                    return;
                }

                //--------------------------------------------------------
                // Delete処理
                //--------------------------------------------------------
                NBaseData.DAC.MsVesselItemVessel msVesselItemVessel = new NBaseData.DAC.MsVesselItemVessel();
                msVesselItemVessel.MsVesselItemVesselID = MsVesselItemVessel.MsVesselItemVesselID;
                msVesselItemVessel.Ts = MsVesselItemVessel.Ts;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    serviceClient.MsVesselItemVessel_DeleteRecord(NBaseCommon.Common.LoginUser, msVesselItemVessel);

                    Message.Show確認("削除しました。");
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Message.Showエラー(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 戻るボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void Cancel_Btn_Click(object sender, EventArgs e)
        private void Cancel_Btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


        /// <summary>
        /// 更新アイテムをGUIへセットする
        /// </summary>
        /// <param name="customer"></param>
        private void SetItems(NBaseData.DAC.MsVesselItemVessel msVesselItemVessel)
        {
            //-----------------------------------------------------
            // GUIの表示設定
            //-----------------------------------------------------
            MsVessel_comboBox.Enabled = false;
            Category_comboBox.Enabled = false;
            MsVesselItem_comboBox.Enabled = false;

            //-----------------------------------------------------
            // GUIへ値を設定する
            //-----------------------------------------------------
            MsVessel_comboBox.Text = MsVesselItemVessel.VesselName;
            foreach (NBaseData.DAC.MsVesselItemCategory item in MsVesselItemCategoryList)
            {
                if (MsVesselItemVessel.CategoryNumber == item.CategoryNumber)
                {
                    Category_comboBox.Text = item.CategoryName;
                }
            }
            MsVesselItem_comboBox.Text = MsVesselItemVessel.VesselItemName;

            textBox_備考.Text = MsVesselItemVessel.Bikou;
        }

        /// <summary>
        /// ドロップダウンリストに値を設定する
        /// </summary>
        private void MakeDropDownList()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                // カテゴリー
                MsVesselItemCategoryList = serviceClient.MsVesselItemCategory_GetRecords(NBaseCommon.Common.LoginUser);
                foreach (NBaseData.DAC.MsVesselItemCategory item in MsVesselItemCategoryList)
                {
                    Category_comboBox.Items.Add(new ListItem(item.CategoryName, item.CategoryNumber.ToString()));
                }
                Category_comboBox.SelectedIndex = 0;
            }
        }

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
            // Update
            //----------------------------------------------
            try
            {
                MsVesselItemVessel.Bikou = textBox_備考.Text;

                MsVesselItemVessel.RenewDate = DateTime.Today;
                MsVesselItemVessel.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    serviceClient.BLC_船用品登録(NBaseCommon.Common.LoginUser, MsVesselItemVessel);

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
        /// 入力値のチェック
        /// </summary>
        /// <returns></returns>
        private bool 入力値チェック()
        {
            //---------------------------------------------------
            // 必須入力確認
            //---------------------------------------------------

            return true;
        }


    }
}
