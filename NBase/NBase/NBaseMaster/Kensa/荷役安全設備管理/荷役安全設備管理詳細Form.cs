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

namespace NBaseMaster.荷役安全設備管理
{
    public partial class 荷役安全設備管理詳細Form : Form
    {
        public MsNiyaku msNiyaku = null;

        //データを編集したかどうか？
        private bool ChangeFlag = false;


        public 荷役安全設備管理詳細Form()
        {
            InitializeComponent();
        }

        private void 荷役安全設備管理詳細Form_Load(object sender, EventArgs e)
        {
            if (msNiyaku == null)
            {
                Delete_button.Enabled = false;
            }
            else
            {
                Name_textBox.Text = msNiyaku.MsNiyakusetsubiName;
                Kankaku_textBox.Text = msNiyaku.Kankaku.ToString();
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
            if (msNiyaku == null)
            {
                msNiyaku = new MsNiyaku();
                msNiyaku.MsNiyakuID = Guid.NewGuid().ToString();
                msNiyaku.RenewDate = DateTime.Now;
                msNiyaku.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                msNiyaku.UserKey = "1";
                is新規作成 = true;
            }
            else
            {
                is新規作成 = false;
            }

            msNiyaku.MsNiyakusetsubiName = Name_textBox.Text;
            msNiyaku.Kankaku = Convert.ToInt32(Kankaku_textBox.Text);

            if (is新規作成 == true)
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsNiyaku_InsertRecord(NBaseCommon.Common.LoginUser, msNiyaku);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", "荷役安全設備管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            else
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsNiyaku_UpdateRecord(NBaseCommon.Common.LoginUser, msNiyaku);
                }


                if (ret == true)
                {
                    MessageBox.Show("更新しました", "荷役安全設備管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }

        private bool Validation()
        {
            if (Name_textBox.Text == "")
            {
                MessageBox.Show("荷役安全設備名を入力して下さい", "荷役安全設備管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                Convert.ToInt32(Kankaku_textBox.Text);
            }
            catch
            {
                MessageBox.Show("間隔を正しく入力して下さい", "荷役安全設備管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 削除時の使用チェック
        /// 引数：対象
        /// 返り値：可能→true 不可能→false
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckDeleteUsing(MsNiyaku data)
        {
            //KS_NIYAKU

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region KsNiyaku

                //使用しているかどうかをID検索をかけてチェックする。
                List<KsNiyaku> nilist = serviceClient.KsNiyaku_GetRecordsByMsNiyakuID(NBaseCommon.Common.LoginUser, data.MsNiyakuID);

                if (nilist.Count > 0)
                {
                    return false;
                }

                #endregion

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
            bool result = this.CheckDeleteUsing(this.msNiyaku);
            if (result == false)
            {
                MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (MessageBox.Show("削除しますか？", "荷役安全設備管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ret;
                msNiyaku.DeleteFlag = 1;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsNiyaku_UpdateRecord(NBaseCommon.Common.LoginUser, msNiyaku);
                }

                 if (ret == true)
                {
                    MessageBox.Show("削除しました", "荷役安全設備管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
