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

namespace NBaseMaster.検船管理
{
    public partial class 検船管理詳細Form : Form
    {
        public MsKensen msKensen = null;

        //データを編集したかどうか？
        private bool ChangeFlag = false;


        public 検船管理詳細Form()
        {
            InitializeComponent();
        }

        private void 検船管理詳細Form_Load(object sender, EventArgs e)
        {
            if (msKensen == null)
            {
                Delete_button.Enabled = false;
            }
            else
            {
                Name_textBox.Text = msKensen.MsKensenName;
                Kankaku_textBox.Text = msKensen.Kankaku.ToString();
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
            if (msKensen == null)
            {
                msKensen = new MsKensen();
                msKensen.MsKensenID = Guid.NewGuid().ToString();
                msKensen.RenewDate = DateTime.Now;
                msKensen.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                msKensen.UserKey = "1";
                is新規作成 = true;
            }
            else
            {
                is新規作成 = false;
            }

            msKensen.MsKensenName = Name_textBox.Text;
            msKensen.Kankaku = Convert.ToInt32(Kankaku_textBox.Text);

            if (is新規作成 == true)
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsKensen_InsertRecord(NBaseCommon.Common.LoginUser, msKensen);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", "検船管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            else
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsKensen_UpdateRecord(NBaseCommon.Common.LoginUser, msKensen);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", "検船管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }

        private bool Validation()
        {
            if (Name_textBox.Text == "")
            {
                MessageBox.Show("証書名を入力して下さい", "検船管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                Convert.ToInt32(Kankaku_textBox.Text);
            }
            catch
            {
                MessageBox.Show("間隔を正しく入力して下さい", "検船管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        /// <summary>
        /// 削除するときの使用チェック
        /// 引数：チェック対象
        /// 返り値：true→削除可能　false→削除不可
        /// </summary>
        /// <param name="mskensen"></param>
        /// <returns></returns>
        private bool CheckDeletUsing(MsKensen data)
        {
            //MsKensenは
            //KS_SHOUSHO_KENSEN_LINK
            //KS_KENSEN
            //にリンクしている
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region KsShoushoKensenLink
                //一致するIDがあるかを検索
                List<KsShoushoKensenLink> llist = serviceClient.KsShoushoKensenLink_GetRecordsByMsKensenID(NBaseCommon.Common.LoginUser, data.MsKensenID);

                if (llist.Count > 0)
                {
                    return false;
                }
                


                #endregion

                #region KsKensen

                //IDで引っかけたデータ取得
                List<KsKensen> ksenlist = serviceClient.KsKensen_GetRecordsByMsKensenID(NBaseCommon.Common.LoginUser, data.MsKensenID);

                if (ksenlist.Count > 0)
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
            //使用状況尾をチェックする・
            bool deletecheck = this.CheckDeletUsing(this.msKensen);            
            if (deletecheck == false)
            {
                MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("削除しますか？", "検船管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ret;
                msKensen.DeleteFlag = 1;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsKensen_UpdateRecord(NBaseCommon.Common.LoginUser, msKensen);
                }

                 if (ret == true)
                {
                    MessageBox.Show("削除しました", "検船管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
