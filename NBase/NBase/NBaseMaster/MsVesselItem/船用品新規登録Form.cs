using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;

namespace NBaseMaster.MsVesselItem
{
    public partial class 船用品新規登録Form : Form
    {
        private List<NBaseData.DAC.MsTani> MsTaniList;
        private List<NBaseData.DAC.MsVesselItemCategory> MsVesselItemCategoryList;

        public 船用品新規登録Form()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "船用品新規登録", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
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
                NBaseData.DAC.MsVesselItem msVesselItem = new NBaseData.DAC.MsVesselItem();
                msVesselItem.MsVesselItemID = MsVesselItem_textBox.Text;
                msVesselItem.VesselItemName = VesselName_textBox.Text;
                msVesselItem.CategoryNumber = Convert.ToInt32(((ListItem)MsVesselItemCategory_comboBox.SelectedItem).Value);
                msVesselItem.MsTaniID = ((ListItem)tani_comboBox.SelectedItem).Value;

                // 2015.03.31:規定在庫数は利用しなくなった
                //if (textBox_在庫数.Text.Length > 0)
                //{
                //    msVesselItem.ZaikoCount = int.Parse(textBox_在庫数.Text);
                //}
                msVesselItem.Bikou = textBox_備考.Text;

                msVesselItem.RenewDate = DateTime.Today;
                msVesselItem.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                msVesselItem.SendFlag = 0;
                msVesselItem.VesselID = 0;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    serviceClient.MsVesselItem_Insert(NBaseCommon.Common.LoginUser, msVesselItem);

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

            // 船用品ID
            if (MsVesselItem_textBox.Text.Length < 1)
            {
                Message.Showエラー("船用品IDを入力してください。");
                return false;
            }

            // 船用品名
            if (VesselName_textBox.Text.Length < 1)
            {
                Message.Showエラー("船用品名を入力してください。");
                return false;
            }

            // 2015.03.31:規定在庫数は利用しなくなった
            //if (textBox_在庫数.Text.Length > 0)
            //{
            //    int count = 0;
            //    if (int.TryParse(textBox_在庫数.Text, out count) == false)
            //    {
            //        Message.Showエラー("在庫数は数字を入力してください。");
            //        return false;
            //    }
            //}

            //--------------------------------------------------
            // 重複確認
            //--------------------------------------------------

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                NBaseData.DAC.MsVesselItem msVesselItem = null;

                // 船用品ID
                msVesselItem = serviceClient.MsVesselItem_GetRecordsByMsVesselItemID(NBaseCommon.Common.LoginUser, MsVesselItem_textBox.Text);
                if (msVesselItem != null)
                {
                    Message.Showエラー("入力された船用品IDは登録されています。");
                    return false;
                }
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
                MsTaniList = serviceClient.MsTani_GetRecords(NBaseCommon.Common.LoginUser);

                foreach (NBaseData.DAC.MsTani item in MsTaniList)
                {
                    tani_comboBox.Items.Add(new ListItem(item.TaniName, item.MsTaniID));
                }
                tani_comboBox.SelectedIndex = 0;

                // カテゴリー
                MsVesselItemCategoryList = serviceClient.MsVesselItemCategory_GetRecords(NBaseCommon.Common.LoginUser);
                foreach (NBaseData.DAC.MsVesselItemCategory item in MsVesselItemCategoryList)
                {
                    MsVesselItemCategory_comboBox.Items.Add(new ListItem(item.CategoryName, item.CategoryNumber.ToString()));
                }
                MsVesselItemCategory_comboBox.SelectedIndex = 0;
            }
        }
    }
}
