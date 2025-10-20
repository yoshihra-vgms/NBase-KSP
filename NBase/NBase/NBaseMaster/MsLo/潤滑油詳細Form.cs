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

namespace NBaseMaster.MsLo
{
    public partial class 潤滑油詳細Form : Form
    {
        private NBaseData.DAC.MsLo MsLo;
        private List<NBaseData.DAC.MsTani> MsTaniList;

        public 潤滑油詳細Form(NBaseData.DAC.MsLo target)
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "潤滑油詳細", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            MsLo = target;
            MakeDropDownList();
            SetItems(MsLo);
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
                    NBaseData.DAC.MsLo msLo = new NBaseData.DAC.MsLo();

                    msLo.MsLoID = MsLo.MsLoID;
                    msLo.LoName = LoName_textBox.Text;
                    msLo.MsTaniID = ((ListItem)tani_comboBox.SelectedItem).Value;
                    msLo.RenewDate = DateTime.Now;
                    msLo.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                    msLo.Ts = MsLo.Ts;

                    serviceClient.MsLo_Update(NBaseCommon.Common.LoginUser, msLo);

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


        //削除チェック
        //引数：調べるデータ
        //返り値：true→削除できます。 false→削除はできません
        private bool CheckDeleteUsing(NBaseData.DAC.MsLo data)
        {
            //MS_LO_VESSEL-
            //OD_THI_SHOUSAI_ITEM-
            //OD_MM_SHOUSAI_ITEM-
            //OD_MK_SHOUSAI_ITEM-

            //OD_JRY_SHOUSAI_ITEM-
            //OD_SHR_SHOUSAI_ITEM-
            //OD_CHOZO_SHOUSAI-
            //OD_HACHU_TANKA-


            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region MsLoVessel
                List<NBaseData.DAC.MsLoVessel> lolist =
                    serviceClient.MsLoVessel_GetRecordsByMsLoID(NBaseCommon.Common.LoginUser, data.MsLoID);

                //一個以上ある
                if (lolist.Count > 0)
                {
                    return false;
                }
                    
                    
                #endregion

                #region OdThiShousaiItem
                List<OdThiShousaiItem> thilist =
                    serviceClient.OdThiShousaiItem_GetRecordsByMsLoID(NBaseCommon.Common.LoginUser, data.MsLoID);
                if (thilist.Count > 0)
                {
                    return false;
                }

                #endregion

                #region OdMmShousaiItem
                List<OdMmShousaiItem> mmlist =
                    serviceClient.OdMmShousaiItem_GetRecordsByMsLoID(NBaseCommon.Common.LoginUser, data.MsLoID);

                if (mmlist.Count > 0)
                {
                    return false;
                }
                #endregion

                #region OdMkShousaiItem
                List<OdMkShousaiItem> mklist =
                    serviceClient.OdMkShousaiItem_GetRecordsByMsLoID(NBaseCommon.Common.LoginUser, data.MsLoID);

                if (mklist.Count > 0)
                {
                    return false;
                }
                #endregion
                //-----

                #region OdJryShousaiItem
                List<OdJryShousaiItem> jrylist =
                    serviceClient.OdJryShousaiItem_GetRecordsByMsLoID(NBaseCommon.Common.LoginUser, data.MsLoID);

                if (jrylist.Count > 0)
                {
                    return false;
                }
                #endregion

                #region OdShrShousaiItem
                List<OdShrShousaiItem> shrlist =
                    serviceClient.OdShrShousaiItem_GetRecordsByMsLoID(NBaseCommon.Common.LoginUser, data.MsLoID);

                if (shrlist.Count > 0)
                {
                    return false;
                }
                #endregion

                #region OdChozoShousai
                List<OdChozoShousai> cholist =
                    serviceClient.OdChozoShousai_GetRecordsByMsLoID(NBaseCommon.Common.LoginUser, data.MsLoID);

                if (cholist.Count > 0)
                {
                    return false;
                }
                #endregion 

                #region  OdHachuTanka
                List<OdHachuTanka> tanlist =
                    serviceClient.OdHachuTanka_GetRecordsByMaLoID(NBaseCommon.Common.LoginUser, data.MsLoID);

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
                if (Message.Show問合せ("この潤滑油を削除します。よろしいですか？") == false)
                {
                    return;
                }


                //使用チェック
                bool result = this.CheckDeleteUsing(this.MsLo);

                if (result == false)
                {
                    MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //--------------------------------------------------------
                // Delete処理
                //--------------------------------------------------------
                NBaseData.DAC.MsLo msLo = new NBaseData.DAC.MsLo();
                msLo.MsLoID = MsLo.MsLoID;
                msLo.Ts = MsLo.Ts;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    serviceClient.MsLo_DeleteRecord(NBaseCommon.Common.LoginUser, msLo);

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
        private void SetItems(NBaseData.DAC.MsLo msLo)
        {
            //-----------------------------------------------------
            // GUIの表示設定
            //-----------------------------------------------------
            MsLoId_textBox.Enabled = false;
            kubun_comboBox.Enabled = false;

            //-----------------------------------------------------
            // GUIへ値を設定する
            //-----------------------------------------------------
            if (-1 < msLo.MsLoID.IndexOf("LO"))
            {
                kubun_comboBox.Text = "LO";
            }
            if (-1 < msLo.MsLoID.IndexOf("ETC"))
            {
                kubun_comboBox.Text = "その他";
            }
            MsLoId_textBox.Text = msLo.MsLoID;
            LoName_textBox.Text = msLo.LoName;
            tani_comboBox.Text = msLo.MsTaniName;

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
            }

            // 区分
            kubun_comboBox.Items.Add(new ListItem("", ""));
            kubun_comboBox.Items.Add(new ListItem("LO", "LO-"));
            kubun_comboBox.Items.Add(new ListItem("その他", "ETC-"));
            kubun_comboBox.SelectedIndex = 0;
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

            // 潤滑油ID
            if (MsLoId_textBox.Text.Length < 1)
            {
                Message.Showエラー("潤滑油IDを入力してください。");
                return false;
            }

            // 潤滑油名
            if (LoName_textBox.Text.Length < 1)
            {
                Message.Showエラー("滑油名を入力してください。");
                return false;
            }

            //--------------------------------------------------
            // 重複確認
            //--------------------------------------------------

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                NBaseData.DAC.MsLo msLo = null;

                // 潤滑油ID
                msLo = serviceClient.MsLo_GetRecordsByMsLoID(NBaseCommon.Common.LoginUser, MsLoId_textBox.Text);
                if (msLo != null)
                {
                    // 更新対象のレコード自身なら。。
                    if (msLo.MsLoID == MsLo.MsLoID)
                    {
                        // 自身なら、更新対象。
                    }
                    else
                    {
                        Message.Showエラー("入力された潤滑油IDは登録されています。");
                        return false;
                    }   
                }
            }

            return true;
        }
    }
}
