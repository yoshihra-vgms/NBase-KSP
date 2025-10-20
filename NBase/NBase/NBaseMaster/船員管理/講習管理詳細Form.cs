using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseUtil;

namespace NBaseMaster.船員管理
{
    public partial class 講習管理詳細Form : Form
    {
        private MsSiKoushu koushu;

		//データを編集したかどうか？
		private bool ChangeFlag = false;

        public 講習管理詳細Form() : this(new MsSiKoushu())
        {
			this.ChangeFlag = false;
        }


        public 講習管理詳細Form(MsSiKoushu koushu)
        {
            this.koushu = koushu;
			this.ChangeFlag = false;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            if (!koushu.IsNew())
            {
                textBox名.Text = koushu.Name;
                textBox有効期限Str.Text = koushu.YukokigenStr;
                textBox有効期限Days.Text = koushu.YukokigenDays.ToString();
            }
            else
            {
                button削除.Enabled = false;
            }

			this.ChangeFlag = false;
        }


        private void button更新_Click(object sender, EventArgs e)
        {
            Save();
			this.ChangeFlag = false;
        }


        private void Save()
        {
            if (ValidateFields())
            {
                FillInstance();

                if (InsertOrUpdate())
                {
                    MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Dispose();
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private bool ValidateFields()
        {
            if (textBox名.Text.Length == 0)
            {
                textBox名.BackColor = Color.Pink;
                MessageBox.Show("講習名を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox名.BackColor = Color.White;
                return false;
            }
            if (textBox名.Text.Length > 20)
            {
                textBox名.BackColor = Color.Pink;
                MessageBox.Show("講習名は20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox名.BackColor = Color.White;
                return false;
            }
            if (textBox有効期限Str.Text.Length > 50)
            {
                textBox有効期限Str.BackColor = Color.Pink;
                MessageBox.Show("有効期限（表示用）は50文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox有効期限Str.BackColor = Color.White;
                return false;
            }
            if (!NumberUtils.Validate(textBox有効期限Days.Text) || textBox有効期限Days.Text.Length > 4)
            {
                textBox有効期限Days.BackColor = Color.Pink;
                MessageBox.Show("有効期限（日数）は半角数字4文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox有効期限Days.BackColor = Color.White;
                return false;
            }

            return true;
        }


        private void FillInstance()
        {
            koushu.Name = textBox名.Text;
            koushu.YukokigenStr = textBox有効期限Str.Text;
            koushu.YukokigenDays = int.Parse(textBox有効期限Days.Text);
        }


        private bool InsertOrUpdate()
        {
            bool result = false;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.MsSiKoushu_InsertOrUpdate(NBaseCommon.Common.LoginUser, koushu);
            }
            return result;
        }


        /// <summary>
        /// 削除チェック
        /// 引数：確認データ
        /// 返り値：true→削除可能ですよ
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckDeleteUsing(MsSiKoushu data)
        {
            //SI_KOUSHU

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region SI_KOUSHU

                //List<SiMenjou> silist = serviceClient.SiMenjou_GetRecordsByMsSiKoushuID(NBaseCommon.Common.LoginUser, data.MsSiKoushuID);

                //if (silist.Count > 0)
                //{
                //    return false;
                //}


                #endregion
            }

            return true;
        }

        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {

                //削除チェック
                bool result = this.CheckDeleteUsing(this.koushu);
                if (result == false)
                {
                    MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                koushu.DeleteFlag = 1;
                Save();
            }
        }


        private void button閉じる_Click(object sender, EventArgs e)
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

            DialogResult = DialogResult.Cancel;
            Dispose();
        }

		//データを編集した時
		private void DataTextChange(object sender, EventArgs e)
		{
			this.ChangeFlag = true;
		}
    }
}
