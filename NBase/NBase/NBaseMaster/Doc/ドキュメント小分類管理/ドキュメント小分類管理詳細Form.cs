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

namespace NBaseMaster.Doc.ドキュメント小分類管理
{
    public partial class ドキュメント小分類管理詳細Form : Form
    {
        private string DIALOG_TITLE = "ドキュメント小分類管理";
        public MsDmShoubunrui msDmShoubunrui = null;

        //データを編集したかどうか？
        private bool ChangeFlag = false;

        public ドキュメント小分類管理詳細Form()
        {
            InitializeComponent();
        }

        private void バース管理詳細Form_Load(object sender, EventArgs e)
        {

            #region ドキュメント分類
            List<MsDmBunrui> msDmBunruis = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msDmBunruis = serviceClient.MsDmBunrui_GetRecords(NBaseCommon.Common.LoginUser);
            }
            comboBox_Bunrui.Items.Clear();
            MsDmBunrui dmy = new MsDmBunrui();
            dmy.MsDmBunruiID = "";
            dmy.Name = "";
            comboBox_Bunrui.Items.Add(dmy);
            foreach (MsDmBunrui msDmBunrui in msDmBunruis)
            {
                comboBox_Bunrui.Items.Add(msDmBunrui);
            }
            #endregion

            if (msDmShoubunrui == null)
            {
                Delete_button.Enabled = false;
            }
            else
            {
                textBox_Code.Text = msDmShoubunrui.Code;
                textBox_Name.Text = msDmShoubunrui.Name;

                for (int i = 0; i < comboBox_Bunrui.Items.Count; i++)
                {
                    MsDmBunrui msDmBunrui = comboBox_Bunrui.Items[i] as MsDmBunrui;
                    if (msDmBunrui.MsDmBunruiID == msDmShoubunrui.MsDmBunruiID)
                    {
                        comboBox_Bunrui.SelectedIndex = i;
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
            if (msDmShoubunrui == null)
            {
                msDmShoubunrui = new MsDmShoubunrui();

                msDmShoubunrui.MsDmShoubunruiID = Guid.NewGuid().ToString();
                msDmShoubunrui.RenewDate = DateTime.Now;
                msDmShoubunrui.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                msDmShoubunrui.UserKey = "1";

                is新規作成 = true;
            }
            else
            {
                is新規作成 = false;
            }

            msDmShoubunrui.Code = textBox_Code.Text;
            msDmShoubunrui.Name = textBox_Name.Text;
            MsDmBunrui msDmBunrui = comboBox_Bunrui.SelectedItem as MsDmBunrui;
            if (msDmBunrui != null)
            {
                msDmShoubunrui.MsDmBunruiID = msDmBunrui.MsDmBunruiID;
            }
            else
            {
                msDmShoubunrui.MsDmBunruiID = "";
            }

            if (is新規作成 == true)
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsDmShoubunrui_InsertRecord(NBaseCommon.Common.LoginUser, msDmShoubunrui);
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
                    ret = serviceClient.MsDmShoubunrui_UpdateRecord(NBaseCommon.Common.LoginUser, msDmShoubunrui);
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
            this.ChangeFlag = false;
        }

        private bool Validation()
        {
            if (textBox_Code.Text.Length != 4)
            {
                MessageBox.Show("ドキュメント小分類ｺｰﾄﾞは４ケタの数字で入力して下さい", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                int code = int.Parse(textBox_Code.Text);
            }
            catch
            {
                MessageBox.Show("ドキュメント小分類ｺｰﾄﾞは４ケタの数字で入力して下さい", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                if (NBaseUtil.StringUtils.isHankaku(textBox_Code.Text) == false)
                {
                    MessageBox.Show("ドキュメント小分類ｺｰﾄﾞは４ケタの数字で入力して下さい", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("ドキュメント小分類ｺｰﾄﾞは４ケタの数字で入力して下さい", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox_Name.Text == "")
            {
                MessageBox.Show("ドキュメント小分類名を入力して下さい", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                MsDmBunrui msDmBunrui = comboBox_Bunrui.SelectedItem as MsDmBunrui;
                if (msDmBunrui.MsDmBunruiID == "")
                {
                    MessageBox.Show("ドキュメント分類名を選択して下さい", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("ドキュメント分類名を選択して下さい", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
            }

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                string msDmShoubunruiId = "";
                if (msDmShoubunrui != null)
                {
                    msDmShoubunruiId = msDmShoubunrui.MsDmShoubunruiID;
                }
                
                MsDmBunrui msDmBunrui = comboBox_Bunrui.SelectedItem as MsDmBunrui;
                List<MsDmShoubunrui> check = serviceClient.MsDmShoubunrui_GetRecordsByBunruiID(NBaseCommon.Common.LoginUser, msDmBunrui.MsDmBunruiID);
                if (check != null || check.Count > 0)
                {
                    var sameCode = from p in check
                                   where p.Code == textBox_Code.Text && p.MsDmShoubunruiID != msDmShoubunruiId
                                   select p;
                    if (sameCode.Count<MsDmShoubunrui>() > 0)
                    {
                        MessageBox.Show("同一なドキュメント小分類ｺｰﾄﾞが存在します", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    var sameName = from p in check
                                   where p.Name == textBox_Name.Text && p.MsDmShoubunruiID != msDmShoubunruiId
                                   select p;
                    if (sameName.Count<MsDmShoubunrui>() > 0)
                    {
                        MessageBox.Show("同一なドキュメント小分類名が存在します", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            bool result = this.CheckDeleteUsing(this.msDmShoubunrui);

            if (result == false)
            {
                MessageBox.Show(this, "このデータは利用しているため削除できません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("削除しますか？", DIALOG_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                msDmShoubunrui.DeleteFlag = 1;
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsDmShoubunrui_UpdateRecord(NBaseCommon.Common.LoginUser, msDmShoubunrui);
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
        private bool CheckDeleteUsing(MsDmShoubunrui data)
        {
            // MsDmShoubunruiは
            // MS_DM_HOUKOKUSHO
            // DM_KOUBUNSHO_KISOKU
            // の２つのテーブルにリンクしている。(ER図参照)
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region MS_DM_HOUKOKUSHO

                List<MsDmHoukokusho> check1 = serviceClient.MsDmHoukokusho_SearchRecords(NBaseCommon.Common.LoginUser, "", data.MsDmShoubunruiID, "", "");
                if (check1.Count > 0)
                {
                    return false;
                }

                #endregion

                #region DM_KOUBUNSHO_KISOKU

                List<DmKoubunshoKisoku> check2 = serviceClient.DmKoubunshoKisoku_GetRecordsByShoubunruiID(NBaseCommon.Common.LoginUser, data.MsDmShoubunruiID);
                if (check2.Count > 0)
                {
                    return false;
                }

                #endregion
            }
            return true;
        }
    }
}
