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

namespace NBaseMaster.輸送品目管理
{
    public partial class 輸送品目管理詳細Form : Form
    {
        public MsYusoItem msYusoItem = null;

        //データを編集したかどうか？
        private bool ChangeFlag = false;



        public 輸送品目管理詳細Form()
        {
            InitializeComponent();
        }

        private void 輸送品目管理詳細Form_Load(object sender, EventArgs e)
        {
            if (msYusoItem == null)
            {
                Delete_button.Enabled = false;
            }
            else
            {
                YusoItemCode_textBox.Text = msYusoItem.YusoItemCode;
                YusoItemName_textBox.Text = msYusoItem.YusoItemName;
                SenshuCode_textBox.Text = msYusoItem.SenshuCode;
                SenshuName_textBox.Text = msYusoItem.SenshuName;
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
            if (msYusoItem == null)
            {
                msYusoItem = new MsYusoItem();
                msYusoItem.RenewDate = DateTime.Now;
                msYusoItem.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                msYusoItem.UserKey = "1";
                is新規作成 = true;
            }
            else
            {
                is新規作成 = false;
            }

            msYusoItem.YusoItemCode = YusoItemCode_textBox.Text;
            msYusoItem.YusoItemName = YusoItemName_textBox.Text;
            msYusoItem.SenshuCode = SenshuCode_textBox.Text;
            msYusoItem.SenshuName = SenshuName_textBox.Text;


            if (is新規作成 == true)
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsYusoItem_InsertRecord(NBaseCommon.Common.LoginUser, msYusoItem);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", "輸送品目管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            else
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsYusoItem_UpdateRecord(NBaseCommon.Common.LoginUser, msYusoItem);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", "輸送品目管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }

        /// <summary>
        /// 入力確認
        /// </summary>
        /// <returns></returns>
        #region private bool Validation()
        private bool Validation()
        {
            if (YusoItemCode_textBox.Text == "")
            {
                MessageBox.Show("輸送品目コードを入力して下さい", "輸送品目管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (YusoItemName_textBox.Text == "")
            {
                MessageBox.Show("輸送品目名を入力して下さい", "輸送品目管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (SenshuCode_textBox.Text == "")
            {
                MessageBox.Show("船種コードを入力して下さい", "輸送品目管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (SenshuName_textBox.Text == "")
            {
                MessageBox.Show("船種名を入力して下さい", "輸送品目管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void Delete_button_Click(object sender, EventArgs e)
        private void Delete_button_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("削除しますか？", "輸送品目管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ret;
                msYusoItem.DeleteFlag = 1;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsYusoItem_DeleteRecord(NBaseCommon.Common.LoginUser, msYusoItem);
                }

                 if (ret == true)
                {
                    MessageBox.Show("削除しました", "輸送品目管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }
        #endregion

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void Close_button_Click(object sender, EventArgs e)
        private void Close_button_Click(object sender, EventArgs e)
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
        #endregion

        /// <summary>
        /// データを編集した時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void DataChange(object sender, EventArgs e)
        private void DataChange(object sender, EventArgs e)
        {
            this.ChangeFlag = true;
        }
        #endregion
    }
}
