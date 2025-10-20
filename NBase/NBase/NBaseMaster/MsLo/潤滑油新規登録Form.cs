using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;

namespace NBaseMaster.MsLo
{
    public partial class 潤滑油新規登録Form : Form
    {
        private List<NBaseData.DAC.MsTani> MsTaniList;

        public 潤滑油新規登録Form()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "潤滑油新規登録", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
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
                NBaseData.DAC.MsLo msLo = new NBaseData.DAC.MsLo();
                msLo.MsLoID = ((ListItem)kubun_comboBox.SelectedItem).Value + MsLoId_textBox.Text;
                msLo.LoName = LoName_textBox.Text;
                msLo.MsTaniID = ((ListItem)tani_comboBox.SelectedItem).Value;

                msLo.RenewDate = DateTime.Today;
                msLo.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                msLo.SendFlag = 0;
                msLo.VesselID = 0;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    serviceClient.MsLo_Insert(NBaseCommon.Common.LoginUser, msLo);

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
                msLo = serviceClient.MsLo_GetRecordsByMsLoID(NBaseCommon.Common.LoginUser, ((ListItem)kubun_comboBox.SelectedItem).Value + MsLoId_textBox.Text);
                if (msLo != null)
                {
                    Message.Showエラー("入力された潤滑油IDは登録されています。");
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
                // 単位
                MsTaniList = serviceClient.MsTani_GetRecords(NBaseCommon.Common.LoginUser);

                foreach (NBaseData.DAC.MsTani item in MsTaniList)
                {
                    tani_comboBox.Items.Add(new ListItem(item.TaniName, item.MsTaniID));
                }
                tani_comboBox.SelectedIndex = 0;

                // 区分
                kubun_comboBox.Items.Add(new ListItem("LO", "LO-"));
                kubun_comboBox.Items.Add(new ListItem("その他", "ETC-"));
                kubun_comboBox.SelectedIndex = 0;

                //kubun_comboBox.Text = ((ListItem)kubun_comboBox.SelectedItem).Value;
            }
        }

        private void kubun_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            kubun_textBox.Text = ((ListItem)kubun_comboBox.SelectedItem).Value;
        }

        private void Delete_Btn_Click(object sender, EventArgs e)
        {

        }
    }
}
