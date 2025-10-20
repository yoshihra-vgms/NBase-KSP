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

namespace NBaseMaster.場所区分管理
{
    public partial class 場所区分管理詳細Form : Form
    {
        public MsBashoKubun msBashoKubun = null;

        //データを編集したかどうか？
        private bool ChangeFlag = false;


        public 場所区分管理詳細Form()
        {
            InitializeComponent();
        }

        private void 場所区分管理詳細Form_Load(object sender, EventArgs e)
        {
            if (msBashoKubun == null)
            {
                Delete_button.Enabled = false;
            }
            else
            {
                BashoKubunName_textBox.Text = msBashoKubun.BashoKubunName;
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
            if (msBashoKubun == null)
            {
                msBashoKubun = new MsBashoKubun();
                msBashoKubun.MsBashoKubunId = Guid.NewGuid().ToString();
                msBashoKubun.RenewDate = DateTime.Now;
                msBashoKubun.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                msBashoKubun.UserKey = "1";

                is新規作成 = true;
            }
            else
            {
                is新規作成 = false;
            }

            msBashoKubun.BashoKubunName = BashoKubunName_textBox.Text;

            if (is新規作成 == true)
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsBashoKubun_InsertRecord(NBaseCommon.Common.LoginUser, msBashoKubun);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", "場所区分管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("更新に失敗しました", "場所区分管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsBashoKubun_UpdateRecord(NBaseCommon.Common.LoginUser, msBashoKubun);
                }
                if (ret == true)
                {
                    MessageBox.Show("更新しました", "場所区分管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("更新に失敗しました", "場所区分管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool Validation()
        {
            if (BashoKubunName_textBox.Text == "")
            {
                MessageBox.Show("場所区分名を入力して下さい", "場所区分管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //使用しているかをチェックする
            bool result = this.CheckDeleteUsing(this.msBashoKubun);

            if (result == false)
            {
                MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (MessageBox.Show("削除しますか？", "場所区分管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ret;
                msBashoKubun.DeleteFlag = 1;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsBashoKubun_UpdateRecord(NBaseCommon.Common.LoginUser, msBashoKubun);
                }
                if (ret == true)
                {
                    MessageBox.Show("削除しました", "場所区分管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        /// <summary>
        /// データが削除できるかどうかをチェックする
        /// 引数：チェックするデータ
        /// 返り値：true→未使用削除可能　false→使用している削除不可
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckDeleteUsing(MsBashoKubun data)
        {
            //MsBashoKubunはMsBashoのみに関連している。

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region MsBasho
                List<MsBasho> basholist = new List<MsBasho>();

                //関連するものがあるかをチェック
                basholist = serviceClient.MsBasho_GetRecordsByBashoKubun(NBaseCommon.Common.LoginUser, data.MsBashoKubunId);

                //取得できたなら使用している。
                if (basholist.Count > 0)
                {
                    return false;
                }

                #endregion
            }


            return true;
        }

    }
}
