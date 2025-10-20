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

namespace NBaseMaster.救命設備管理
{
    public partial class 救命設備管理詳細Form : Form
    {
        public MsKyumeisetsubi msKyumeisetsubi = null;

        //データを編集したかどうか？
        private bool ChangeFlag = false;


        public 救命設備管理詳細Form()
        {
            InitializeComponent();
        }

        private void 救命設備管理詳細Form_Load(object sender, EventArgs e)
        {
            if (msKyumeisetsubi == null)
            {
                Delete_button.Enabled = false;
            }
            else
            {
                Name_textBox.Text = msKyumeisetsubi.MsKyumeisetsubiName;
                Kankaku_textBox.Text = msKyumeisetsubi.Kankaku.ToString();

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
            if (msKyumeisetsubi == null)
            {
                msKyumeisetsubi = new MsKyumeisetsubi();
                msKyumeisetsubi.MsKyumeisetsubiID = Guid.NewGuid().ToString();
                msKyumeisetsubi.RenewDate = DateTime.Now;
                msKyumeisetsubi.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                msKyumeisetsubi.UserKey = "1";
                is新規作成 = true;
            }
            else
            {
                is新規作成 = false;
            }

            msKyumeisetsubi.MsKyumeisetsubiName = Name_textBox.Text;
            msKyumeisetsubi.Kankaku = Convert.ToInt32(Kankaku_textBox.Text);


            if (is新規作成 == true)
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsKyumeisetsubi_InsertRecord(NBaseCommon.Common.LoginUser, msKyumeisetsubi);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", "救命設備管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            else
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsKyumeisetsubi_UpdateRecord(NBaseCommon.Common.LoginUser, msKyumeisetsubi);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", "救命設備管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }

        private bool Validation()
        {
            if (Name_textBox.Text == "")
            {
                MessageBox.Show("救命設備名を入力して下さい", "救命設備管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                Convert.ToInt32(Kankaku_textBox.Text);
            }
            catch
            {
                MessageBox.Show("間隔を正しく入力して下さい", "救命設備管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }                
            return true;
        }

        //削除時の使用チェック
        //引数：調査対象
        //返り値：true　削除できる falseできない
        private bool CheckDeleteUsing(MsKyumeisetsubi data)
        {
            //リンク先
            //KS_KYUMEISETUBI

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region KsKyumeisetubi

                //関連IDによる検索
                List<KsKyumeisetsubi> klist= serviceClient.KsKyumeisetsubi_GetRecordsByMsKyumeisetsubiID(NBaseCommon.Common.LoginUser, data.MsKyumeisetsubiID);

                if (klist.Count > 0)
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
            bool result = this.CheckDeleteUsing(this.msKyumeisetsubi);
            if (result == false)
            {
                MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (MessageBox.Show("削除しますか？", "救命設備管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ret;
                msKyumeisetsubi.DeleteFlag = 1;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsKyumeisetsubi_UpdateRecord(NBaseCommon.Common.LoginUser, msKyumeisetsubi);
                }

                 if (ret == true)
                {
                    MessageBox.Show("削除しました", "救命設備管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
