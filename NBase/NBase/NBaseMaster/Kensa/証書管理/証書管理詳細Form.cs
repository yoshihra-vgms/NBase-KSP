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

namespace NBaseMaster.証書管理
{
    public partial class 証書管理詳細Form : Form
    {
        public MsShousho msShousho = null;

        //データを編集したかどうか？
        private bool ChangeFlag = false;


        public 証書管理詳細Form()
        {
            InitializeComponent();
        }

        private void 証書管理詳細Form_Load(object sender, EventArgs e)
        {
            if (msShousho == null)
            {
                Delete_button.Enabled = false;
            }
            else
            {
                Name_textBox.Text = msShousho.MsShoushoName;
                Kankaku_textBox.Text = msShousho.Kanakaku.ToString();
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
            if (msShousho == null)
            {
                msShousho = new MsShousho();
                msShousho.MsShoushoID = Guid.NewGuid().ToString();
                msShousho.RenewDate = DateTime.Now;
                msShousho.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                msShousho.UserKey = "1";
                is新規作成 = true;
            }
            else
            {
                is新規作成 = false;
            }

            msShousho.MsShoushoName = Name_textBox.Text;
            msShousho.Kanakaku = Convert.ToInt32(Kankaku_textBox.Text);

            if (is新規作成 == true)
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsShousho_InsertRecord(NBaseCommon.Common.LoginUser, msShousho);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", "証書管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            else
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsShousho_UpdateRecord(NBaseCommon.Common.LoginUser, msShousho);
                }


                if (ret == true)
                {
                    MessageBox.Show("更新しました", "証書管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }

        private bool Validation()
        {
            if (Name_textBox.Text == "")
            {
                MessageBox.Show("証書名を入力して下さい", "証書管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                Convert.ToInt32(Kankaku_textBox.Text);
            }
            catch
            {
                MessageBox.Show("間隔を正しく入力して下さい", "証書管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        //削除時の使用チェック
        //引数：調査対象
        //返り値：true→削除できる false→できない
        private bool CheckDeleteUsing(MsShousho data)
        {
            //関連品は
            //KsShoushoのみ


            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region KsShousho
                List<KsShousho> slist = serviceClient.KsShousho_GetRecordsByMsShoushoID(NBaseCommon.Common.LoginUser, data.MsShoushoID);

                if (slist.Count > 0)
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
            bool result = this.CheckDeleteUsing(this.msShousho);
            if (result == false)
            {
                MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (MessageBox.Show("削除しますか？", "証書管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ret;
                msShousho.DeleteFlag = 1;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsShousho_UpdateRecord(NBaseCommon.Common.LoginUser, msShousho);
                }

                 if (ret == true)
                {
                    MessageBox.Show("削除しました", "証書管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
