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

namespace NBaseMaster.Doc.ドキュメント分類管理
{
    public partial class ドキュメント分類管理詳細Form : Form
    {
        private string DIALOG_TITLE = "ドキュメント分類管理";
        public MsDmBunrui msDmBunrui = null;

        //データを編集したかどうか？
        private bool ChangeFlag = false;


        public ドキュメント分類管理詳細Form()
        {
            InitializeComponent();
        }

        private void ドキュメント分類管理詳細Form_Load(object sender, EventArgs e)
        {
            if (msDmBunrui == null)
            {
                Delete_button.Enabled = false;
            }
            else
            {
                textBox_BunruiCode.Text = msDmBunrui.Code;
                textBox_BunruiName.Text = msDmBunrui.Name;
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
            if (msDmBunrui == null)
            {
                msDmBunrui = new MsDmBunrui();
                msDmBunrui.MsDmBunruiID = Guid.NewGuid().ToString();
                msDmBunrui.RenewDate = DateTime.Now;
                msDmBunrui.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                msDmBunrui.UserKey = "1";

                is新規作成 = true;
            }
            else
            {
                is新規作成 = false;
            }

            msDmBunrui.Code = textBox_BunruiCode.Text;
            msDmBunrui.Name = textBox_BunruiName.Text;

            if (is新規作成 == true)
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsDmBunrui_InsertRecord(NBaseCommon.Common.LoginUser, msDmBunrui);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("更新に失敗しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsDmBunrui_UpdateRecord(NBaseCommon.Common.LoginUser, msDmBunrui);
                }
                if (ret == true)
                {
                    MessageBox.Show("更新しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("更新に失敗しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private bool Validation()
        {
            if (textBox_BunruiCode.Text.Length != 4)
            {
                MessageBox.Show("ドキュメント分類ｺｰﾄﾞは４ケタの数字で入力して下さい", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                int code = int.Parse(textBox_BunruiCode.Text);
            }
            catch
            {
                MessageBox.Show("ドキュメント分類ｺｰﾄﾞは４ケタの数字で入力して下さい", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                if (NBaseUtil.StringUtils.isHankaku(textBox_BunruiCode.Text) == false)
                {
                    MessageBox.Show("ドキュメント分類ｺｰﾄﾞは４ケタの数字で入力して下さい", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("ドキュメント分類ｺｰﾄﾞは４ケタの数字で入力して下さい", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox_BunruiName.Text.Length == 0)
            {
                MessageBox.Show("ドキュメント分類名を入力して下さい", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                string msDmBunruiId = "";
                if (msDmBunrui != null)
                {
                    msDmBunruiId = msDmBunrui.MsDmBunruiID;
                }
                List<MsDmBunrui> check = serviceClient.MsDmBunrui_GetRecords(NBaseCommon.Common.LoginUser);
                if (check != null || check.Count > 0)
                {
                    var sameCode = from p in check
                                   where p.Code == textBox_BunruiCode.Text && p.MsDmBunruiID != msDmBunruiId
                                   select p;
                    if (sameCode.Count<MsDmBunrui>() > 0)
                    {
                        MessageBox.Show("同一なドキュメント分類ｺｰﾄﾞが存在します", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    var sameName = from p in check
                                   where p.Name == textBox_BunruiName.Text && p.MsDmBunruiID != msDmBunruiId
                                   select p;
                    if (sameName.Count<MsDmBunrui>() > 0)
                    {
                        MessageBox.Show("同一なドキュメント分類名が存在します", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
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
            //削除が可能かを調べる
            bool result = this.CheckDeleteUsing(this.msDmBunrui);

            if (result == false)
            {
                MessageBox.Show(this, "このデータは利用しているため削除できません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (MessageBox.Show("削除しますか？", DIALOG_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ret;
                msDmBunrui.DeleteFlag = 1;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsDmBunrui_UpdateRecord(NBaseCommon.Common.LoginUser, msDmBunrui);
                }
                if (ret == true)
                {
                    MessageBox.Show("削除しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_button_Click(object sender, EventArgs e)
        {
            //編集中に閉じようとした。
            if (this.ChangeFlag == true)
            {
                DialogResult ret = MessageBox.Show(this, "データが編集されていますが、閉じますか？",
                                            DIALOG_TITLE,
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
        /// 指定データが使用しているかを調べる
        /// 引数：確認するMsDmBunruiデータ
        /// 返り値：true→未使用削除可、false→使用削除不可
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckDeleteUsing(MsDmBunrui data)
        {
            // MsDmBunruiは
            // MS_DM_SHOUBUNRUI
            // MS_DM_HOUKOKUSHO
            // DM_KOUBUNSHO_KISOKU
            // の３つのテーブルにリンクしている。(ER図参照)
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region MS_DM_SHOUBUNRUI

                List<MsDmShoubunrui> check1 = serviceClient.MsDmShoubunrui_GetRecordsByBunruiID(NBaseCommon.Common.LoginUser, data.MsDmBunruiID);
                if (check1.Count > 0)
                {
                    return false;
                }

                #endregion


                #region MS_DM_HOUKOKUSHO

                List<MsDmHoukokusho>  check2 = serviceClient.MsDmHoukokusho_SearchRecords(NBaseCommon.Common.LoginUser, data.MsDmBunruiID, "", "", "");
                if (check2.Count > 0)
                {
                    return false;
                }


                #endregion


                #region DM_KOUBUNSHO_KISOKU

                List<DmKoubunshoKisoku> check3 = serviceClient.DmKoubunshoKisoku_GetRecordsByBunruiID(NBaseCommon.Common.LoginUser, data.MsDmBunruiID);
                if (check3.Count > 0)
                {
                    return false;
                }

                #endregion
            }
            return true;
        }
    }
}
