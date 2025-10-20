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

namespace NBaseMaster.審査管理
{
    public partial class 審査管理詳細Form : Form
    {
        public MsShinsa msShinsa = null;

        //データを編集したかどうか？
        private bool ChangeFlag = false;


        public 審査管理詳細Form()
        {
            InitializeComponent();
        }

        private void 審査管理詳細Form_Load(object sender, EventArgs e)
        {
            if (msShinsa == null)
            {
                Delete_button.Enabled = false;
            }
            else
            {
                Name_textBox.Text = msShinsa.MsShinsaName;
                Kankaku_textBox.Text = msShinsa.Kankaku.ToString();
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
            if (msShinsa == null)
            {
                msShinsa = new MsShinsa();
                msShinsa.MsshinsaID = Guid.NewGuid().ToString();
                msShinsa.RenewDate = DateTime.Now;
                msShinsa.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                msShinsa.UserKey = "1";
                is新規作成 = true;
            }
            else
            {
                is新規作成 = false;
            }

            msShinsa.MsShinsaName = Name_textBox.Text;
            msShinsa.Kankaku = Convert.ToInt32(Kankaku_textBox.Text);

            if (is新規作成 == true)
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsShinsa_InsertRecord(NBaseCommon.Common.LoginUser, msShinsa);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", "審査管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            else
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsShinsa_UpdateRecord(NBaseCommon.Common.LoginUser, msShinsa);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", "審査管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }

        private bool Validation()
        {
            if (Name_textBox.Text == "")
            {
                MessageBox.Show("審査名を入力して下さい", "審査管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                Convert.ToInt32(Kankaku_textBox.Text);
            }
            catch
            {
                MessageBox.Show("間隔を正しく入力して下さい", "審査管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        /// <summary>
        /// 削除時の使用チェック
        /// 引数：探索データ
        /// 返り値：true→削除可能 false→使用中削除不可
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckDeleteUsing(MsShinsa data)
        {
            //リンク先は
            //KS_SHINSA
            //KS_SHOUSHO_SHINSA_LINK

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region KsShinsa
                List<KsShinsa> klist = serviceClient.KsShinsa_GetRecordsByMsShinsaID(NBaseCommon.Common.LoginUser, data.MsshinsaID);

                //関連するものがある
                if (klist.Count > 0)
                {
                    return false;
                }
                #endregion


                #region KsShoushoShinsaLink
                
                List<KsShoushoShinsaLink> kslist = serviceClient.KsShoushoShinsaLink_GetRecordsByMsShinsaID(NBaseCommon.Common.LoginUser, data.MsshinsaID);

                if (kslist.Count > 0)
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
            bool result = this.CheckDeleteUsing(this.msShinsa);
            if (result == false)
            {
                MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }



            if (MessageBox.Show("削除しますか？", "審査管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ret;
                msShinsa.DeleteFlag = 1;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsShinsa_UpdateRecord(NBaseCommon.Common.LoginUser, msShinsa);
                }

                 if (ret == true)
                {
                    MessageBox.Show("削除しました", "審査管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
