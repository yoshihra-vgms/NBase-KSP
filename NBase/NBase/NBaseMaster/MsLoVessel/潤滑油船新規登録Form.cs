using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using ServiceReferences;

namespace NBaseMaster.MsLoVessel
{
    public partial class 潤滑油船新規登録Form : Form
    {
        private List<NBaseData.DAC.MsVessel> MsVesselList;
        private List<NBaseData.DAC.MsLo> MsLoList;

        public 潤滑油船新規登録Form()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "潤滑油船新規登録", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsLoList = serviceClient.MsLo_GetRecords(NBaseCommon.Common.LoginUser);   // 潤滑油
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
                NBaseData.DAC.MsLoVessel msLoVessel = new NBaseData.DAC.MsLoVessel();
                msLoVessel.MsLoVesselID = Guid.NewGuid().ToString();
                msLoVessel.MsVesselID = Convert.ToInt32(((ListItem)MsVessel_comboBox.SelectedItem).Value);
                msLoVessel.MsLoID = ((ListItem)MsLo_comboBox.SelectedItem).Value;

                msLoVessel.RenewDate = DateTime.Today;
                msLoVessel.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                msLoVessel.SendFlag = 0;
                msLoVessel.VesselID = 0;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    //serviceClient.MsLoVessel_Insert(NBaseCommon.Common.LoginUser, msLoVessel);
                    serviceClient.BLC_潤滑油登録(NBaseCommon.Common.LoginUser, msLoVessel);

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

            // 潤滑油
            if (MsLo_comboBox.Text.Length < 1)
            {
                Message.Showエラー("潤滑油を選択してください。");
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
                MsVesselList = serviceClient.MsVessel_GetRecords(NBaseCommon.Common.LoginUser);
                foreach (NBaseData.DAC.MsVessel item in MsVesselList)
                {
                    MsVessel_comboBox.Items.Add(new ListItem(item.VesselName, item.MsVesselID.ToString()));
                }
                if (MsVessel_comboBox.Items.Count > 0)
                    MsVessel_comboBox.SelectedIndex = 0;
            }

            // 区分
            kubun_comboBox.Items.Add(new ListItem("LO", "LO"));
            kubun_comboBox.Items.Add(new ListItem("その他", "ETC"));
            kubun_comboBox.SelectedIndex = 0;

            MakeLoDropDownList();
        }

        /// <summary>
        /// 潤滑油
        /// </summary>
        private void MakeLoDropDownList()
        {
            if (MsVessel_comboBox.SelectedItem == null)
                return;

            List<NBaseData.DAC.MsLoVessel> tempMsLoVessel;
            List<NBaseData.DAC.MsLo> tempMsLo = new List<NBaseData.DAC.MsLo>();
            int MsVesselID = Convert.ToInt32(((ListItem)MsVessel_comboBox.SelectedItem).Value);

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                tempMsLoVessel = serviceClient.MsLoVessel_GetRecordsByMsVesselIDAndLoName(NBaseCommon.Common.LoginUser, MsVesselID, "");
            }

            foreach (NBaseData.DAC.MsLo item in MsLoList)
            {
                bool flag = true;
                foreach (NBaseData.DAC.MsLoVessel 登録済潤滑油 in tempMsLoVessel)
                {
                    if (登録済潤滑油.MsLoID == item.MsLoID)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    tempMsLo.Add(item);
                }
            }

            MsLo_comboBox.Items.Clear();
            foreach (NBaseData.DAC.MsLo item in tempMsLo)
            {
                // 区分
                if (kubun_comboBox.Text == "その他")
                {
                    if (-1 < item.MsLoID.IndexOf("LO"))
                    {
                        continue;
                    }
                }
                else if (kubun_comboBox.Text == "LO")
                {
                    if (-1 < item.MsLoID.IndexOf("ETC"))
                    {
                        continue;
                    }
                }

                MsLo_comboBox.Items.Add(new ListItem(item.LoName, item.MsLoID));
            }
            if (MsLo_comboBox.Items.Count > 0)
            {
                MsLo_comboBox.SelectedIndex = 0;
            }
        }

        private void MsVessel_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MakeLoDropDownList();
        }

        private void kubun_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MakeLoDropDownList();
        }
    }
}
