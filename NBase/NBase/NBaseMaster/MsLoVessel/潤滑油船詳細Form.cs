using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NBaseMaster.MsLoVessel
{
    public partial class 潤滑油船詳細Form : Form
    {
        private NBaseData.DAC.MsLoVessel MsLoVessel;

        public 潤滑油船詳細Form(NBaseData.DAC.MsLoVessel target)
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "潤滑油船詳細", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            MsLoVessel = target;
            SetItems(MsLoVessel);
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
                if (Message.Show問合せ("この潤滑油船を削除します。よろしいですか？") == false)
                {
                    return;
                }

                //--------------------------------------------------------
                // Delete処理
                //--------------------------------------------------------
                NBaseData.DAC.MsLoVessel msLoVessel = new NBaseData.DAC.MsLoVessel();
                msLoVessel.MsLoVesselID = MsLoVessel.MsLoVesselID;
                msLoVessel.Ts = MsLoVessel.Ts;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    serviceClient.MsLoVessel_DeleteRecord(NBaseCommon.Common.LoginUser, msLoVessel);

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
        private void SetItems(NBaseData.DAC.MsLoVessel msLoVessel)
        {
            //-----------------------------------------------------
            // GUIの表示設定
            //-----------------------------------------------------
            MsVessel_comboBox.Enabled = false;
            kubun_comboBox.Enabled = false;
            MsLo_comboBox.Enabled = false;

            //-----------------------------------------------------
            // GUIへ値を設定する
            //-----------------------------------------------------
            MsVessel_comboBox.Text = msLoVessel.VesselName;
            if (-1 < msLoVessel.MsLoID.IndexOf("LO"))
            {
                kubun_comboBox.Text = "LO";
            }
            if (-1 < msLoVessel.MsLoID.IndexOf("ETC"))
            {
                kubun_comboBox.Text = "その他";
            }
            MsLo_comboBox.Text = msLoVessel.MsLoName;
        }
    }
}
