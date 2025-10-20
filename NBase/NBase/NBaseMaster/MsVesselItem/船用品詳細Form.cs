using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using NBaseData.DAC;

namespace NBaseMaster.MsVesselItem
{
    public partial class 船用品詳細Form : Form
    {
        private NBaseData.DAC.MsVesselItem MsVesselItem;
        private List<NBaseData.DAC.MsTani> MsTaniList;
        private List<NBaseData.DAC.MsVesselItemCategory> MsVesselItemCategoryList;

        public 船用品詳細Form(NBaseData.DAC.MsVesselItem target)
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "船用品詳細", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            MsVesselItem = target;
            MakeDropDownList();
            SetItems(MsVesselItem);
        }

        /// <summary>
        /// 更新ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                //-------------------------------------------------------
                // 入力値チェック
                //-------------------------------------------------------
                if (!入力値チェック())
                {
                    return;
                }

                //--------------------------------------------------------
                // UpDate処理
                //--------------------------------------------------------
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    NBaseData.DAC.MsVesselItem msVesselItem = new NBaseData.DAC.MsVesselItem();

                    msVesselItem.MsVesselItemID = MsVesselItem.MsVesselItemID;
                    msVesselItem.VesselItemName = VesselName_textBox.Text;
                    msVesselItem.CategoryNumber = Convert.ToInt32(((ListItem)MsVesselItemCategory_comboBox.SelectedItem).Value);
                    msVesselItem.MsTaniID = ((ListItem)tani_comboBox.SelectedItem).Value;
                    msVesselItem.RenewDate = DateTime.Now;
                    msVesselItem.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                    msVesselItem.Ts = MsVesselItem.Ts;

                    // 2015.03.31:規定在庫数は利用しなくなった
                    //if (textBox_在庫数.Text.Length > 0)
                    //{
                    //    msVesselItem.ZaikoCount = int.Parse(textBox_在庫数.Text);
                    //}
                    msVesselItem.Bikou = textBox_備考.Text;

                    serviceClient.MsVesselItem_Update(NBaseCommon.Common.LoginUser, msVesselItem);

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
        /// 削除チェック
        /// 引数：検索データ
        /// 返り値：true→削除可能
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckDeleteUsing(NBaseData.DAC.MsVesselItem data)
        {
            //MS_VESSEL_ITEM_VESSEL-
            //OD_MK_SHOUSAI_ITEM-

            //OD_THI_SHOUSAI_ITEM-
            //OD_MM_SHOUSAI_ITEM-
            
            //OD_JRY_SHOUSAI_ITEM-
            //OD_SHR_SHOUSAI_ITEM-
            
            //OD_CHOZO_SHOUSAI-
            //OD_HACHU_TANKA
            //の８がリンク・・・

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region MsVesselItemVessel
                List<NBaseData.DAC.MsVesselItemVessel> itemlist =
                    serviceClient.MsVesselItemVessel_GetRecordsByMsVesselItemID(NBaseCommon.Common.LoginUser, data.MsVesselItemID);

                if (itemlist.Count > 0)
                {
                    return false;
                }
                #endregion

                #region OdMkShousaiItem
                List<OdMkShousaiItem> mklist =
                    serviceClient.OdMkShousaiItem_GetRecordsByMsVesselItemID(NBaseCommon.Common.LoginUser, data.MsVesselItemID);

                if (mklist.Count > 0)
                {
                    return false;
                }
                #endregion

                #region OdThiShousaiItem
                List<OdThiShousaiItem> thilist =
                    serviceClient.OdThiShousaiItem_GetRecordsByMsVesselItemID(NBaseCommon.Common.LoginUser, data.MsVesselItemID);

                if (thilist.Count > 0)
                {
                    return false;
                }
                #endregion

                #region OdMmShousaiItem
                List<OdMmShousaiItem> mmlist =
                    serviceClient.OdMmShousaiItem_GetRecordsByMsVesselItemID(NBaseCommon.Common.LoginUser, data.MsVesselItemID);

                if (mmlist.Count > 0)
                {
                    return false;
                }
                #endregion

                #region OdJryShousaiItem
                List<OdJryShousaiItem> jrylist =
                    serviceClient.OdJryShousaiItem_GetRecordsByMsVesselItemID(NBaseCommon.Common.LoginUser, data.MsVesselItemID);
                if (jrylist.Count > 0)
                {
                    return false;
                }
                #endregion

                #region OdShrShousaiItem
                List<OdShrShousaiItem> shrlist =
                    serviceClient.OdShrShousaiItem_GetRecordsByMsVesselItemID(NBaseCommon.Common.LoginUser, data.MsVesselItemID);

                if (shrlist.Count > 0)
                {
                    return false;
                }
                #endregion

                #region OdChozoShousai
                List<OdChozoShousai> cholist =
                    serviceClient.OdChozoShousai_GetRecordsByMsVesselItemID(NBaseCommon.Common.LoginUser, data.MsVesselItemID);

                if (cholist.Count > 0)
                {
                    return false;
                }
                #endregion

                #region OdHachuTanka
                List<OdHachuTanka> tanlist =
                    serviceClient.OdHachuTanka_GetRecordsByMsVesselItemID(NBaseCommon.Common.LoginUser, data.MsVesselItemID);

                if (tanlist.Count > 0)
                {
                    return false;
                }
                #endregion
            }

            return true;
        }

        /// <summary>
        /// 削除ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                //-------------------------------------------------------
                // 確認
                //-------------------------------------------------------
                if (Message.Show問合せ("この船用品を削除します。よろしいですか？") == false)
                {
                    return;
                }

                //使用チェック
                bool result = this.CheckDeleteUsing(this.MsVesselItem);
                if (result == false)
                {
                    MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                //--------------------------------------------------------
                // Delete処理
                //--------------------------------------------------------
                NBaseData.DAC.MsVesselItem msVesselItem = new NBaseData.DAC.MsVesselItem();
                msVesselItem.MsVesselItemID = MsVesselItem.MsVesselItemID;
                msVesselItem.Ts = MsVesselItem.Ts;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    serviceClient.MsVesselItem_DeleteRecord(NBaseCommon.Common.LoginUser, msVesselItem);

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
        /// 更新アイテムをGUIへセットする
        /// </summary>
        /// <param name="customer"></param>
        private void SetItems(NBaseData.DAC.MsVesselItem msVesselItem)
        {
            //-----------------------------------------------------
            // GUIの表示設定
            //-----------------------------------------------------
            MsVesselItem_textBox.Enabled = false;

            //-----------------------------------------------------
            // GUIへ値を設定する
            //-----------------------------------------------------
            MsVesselItem_textBox.Text = msVesselItem.MsVesselItemID;
            VesselName_textBox.Text = msVesselItem.VesselItemName;
            foreach(NBaseData.DAC.MsVesselItemCategory item in MsVesselItemCategoryList)
            {
                if(msVesselItem.CategoryNumber == item.CategoryNumber)
                {
                    MsVesselItemCategory_comboBox.Text = item.CategoryName;
                }
            }
            tani_comboBox.Text = msVesselItem.TaniName;

            // 2015.03.31:規定在庫数は利用しなくなった
            //if (msVesselItem.ZaikoCount > 0)
            //{
            //    textBox_在庫数.Text = msVesselItem.ZaikoCount.ToString();
            //}
            textBox_備考.Text = msVesselItem.Bikou;

        }

        /// <summary>
        /// ドロップダウンリストに値を設定する
        /// </summary>
        private void MakeDropDownList()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                // 単位
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
                    // 更新対象のレコード自身なら。。
                    if (msVesselItem.MsVesselItemID == MsVesselItem.MsVesselItemID)
                    {
                        // 自身なら、更新対象。
                    }
                    else
                    {
                        Message.Showエラー("入力された船用品IDは登録されています。");
                        return false;
                    }
                    
                }
            }

            return true;
        }

        
    }
}
