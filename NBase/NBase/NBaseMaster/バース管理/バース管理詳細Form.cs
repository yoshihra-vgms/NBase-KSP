using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using ServiceReferences.NBaseService;

namespace NBaseMaster.バース管理
{
    public partial class バース管理詳細Form : Form
    {
        public MsBerth msBerth = null;

        //データを編集したかどうか？
        private bool ChangeFlag = false;

        public バース管理詳細Form()
        {
            InitializeComponent();
        }

        private void バース管理詳細Form_Load(object sender, EventArgs e)
        {

            #region 基地
            List<MsKichi> msKichis;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msKichis = serviceClient.MsKichi_GetRecords(NBaseCommon.Common.LoginUser);
            }

            Kichi_comboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            Kichi_comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;

            MsKichi 空 = new MsKichi();
            Kichi_comboBox.Items.Add(空);
            foreach (MsKichi kichi in msKichis)
            {
                Kichi_comboBox.Items.Add(kichi);
                Kichi_comboBox.AutoCompleteCustomSource.Add(kichi.KichiName);
            }
            #endregion

            if (msBerth == null)
            {
                Delete_button.Enabled = false;
            }
            else
            {
                BerthCode_textBox.Text = msBerth.BerthCode;
                BerthName_textBox.Text = msBerth.BerthName;
                BerthCode_textBox.ReadOnly = true;

                for (int i = 0; i < Kichi_comboBox.Items.Count; i++)
                {
                    MsKichi kichi = Kichi_comboBox.Items[i] as MsKichi;
                    if (kichi.MsKichiId == msBerth.MsKichiId)
                    {
                        Kichi_comboBox.SelectedIndex = i;
                    }
                }
            }
            this.ChangeFlag = false;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_button_Click(object sender, EventArgs e)
        {
            if (Validation() == false)
            {
                return;
            }

            bool is新規作成;
            if (msBerth == null)
            {
                msBerth = new MsBerth();

                bool is重複;
                MsBerth b;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    b = serviceClient.MsBerth_GetRecordByBerthCode(NBaseCommon.Common.LoginUser, BerthCode_textBox.Text);
                }
                if (b != null)
                {
                    is重複 = true;
                }
                else
                {
                    is重複 = false;
                }
                if (is重複 == true)
                {
                    MessageBox.Show("バースコードが重複しています", "バース管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                msBerth.MsBerthId = Guid.NewGuid().ToString();
                msBerth.RenewDate = DateTime.Now;
                msBerth.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                msBerth.UserKey = "1";

                is新規作成 = true;
            }
            else
            {
                is新規作成 = false;
            }

            msBerth.BerthCode = BerthCode_textBox.Text;
            msBerth.BerthName = BerthName_textBox.Text;
            MsKichi kichi = Kichi_comboBox.SelectedItem as MsKichi;
            if (kichi != null)
            {
                msBerth.MsKichiId = kichi.MsKichiId;
            }
            else
            {
                msBerth.MsKichiId = "";
            }

            if (is新規作成 == true)
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsBerth_InsertRecord(NBaseCommon.Common.LoginUser, msBerth);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", "バース管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("更新に失敗しました", "バース管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsBerth_UpdateRecord(NBaseCommon.Common.LoginUser, msBerth);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", "バース管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("更新に失敗しました", "バース管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.ChangeFlag = false;
        }

        private bool Validation()
        {
            if (BerthCode_textBox.Text.Length != 4)
            {
                MessageBox.Show("バースコードは4桁を入力して下さい", "バース管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (BerthName_textBox.Text == "")
            {
                MessageBox.Show("バース名を入力して下さい", "バース管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_button_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("削除しますか？", "バース管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                msBerth.DeleteFlag = 1;
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsBerth_UpdateRecord(NBaseCommon.Common.LoginUser, msBerth);
                }
                if (ret == true)
                {
                    MessageBox.Show("削除しました", "バース管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            //編集中に閉じようとした。
            if (this.ChangeFlag == true)
            {
                DialogResult ret = MessageBox.Show(this, "データが編集されていますが、閉じますか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question);

                if (ret == DialogResult.Cancel)
                {
                    return;
                }
            }

            Close();
        }

        //データを編集した時
        private void DataChange(object sender, EventArgs e)
        {
            this.ChangeFlag = true;
        }

    }
}
