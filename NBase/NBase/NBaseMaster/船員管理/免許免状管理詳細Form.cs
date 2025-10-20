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
    public partial class 免許免状管理詳細Form : Form
    {
        private MsSiMenjou menjou;

		//データを編集したかどうか？
		private bool ChangeFlag = false;

        public 免許免状管理詳細Form() : this(new MsSiMenjou())
        {
			this.ChangeFlag = false;
        }


        public 免許免状管理詳細Form(MsSiMenjou menjou)
        {
            this.menjou = menjou;
			this.ChangeFlag = false;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            if (!menjou.IsNew())
            {
                textBox名.Text = menjou.Name;
                textBox略称.Text = menjou.NameAbbr;
                textBox表示順序.Text = menjou.ShowOrder.ToString();
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
                MessageBox.Show("免状／免許名を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox名.BackColor = Color.White;
                return false;
            }
            else if (textBox名.Text.Length > 20)
            {
                textBox名.BackColor = Color.Pink;
                MessageBox.Show("免状／免許名は20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox名.BackColor = Color.White;
                return false;
            }
            else if (textBox略称.Text.Length > 20)
            {
                textBox略称.BackColor = Color.Pink;
                MessageBox.Show("略称は20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox略称.BackColor = Color.White;
                return false;
            }
            else if (!NumberUtils.Validate(textBox表示順序.Text) || textBox表示順序.Text.Length > 3)
            {
                textBox表示順序.BackColor = Color.Pink;
                MessageBox.Show("表示順序は半角数字3文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox表示順序.BackColor = Color.White;
                return false;
            }

            return true;
        }


        private void FillInstance()
        {
            menjou.Name = textBox名.Text;
            menjou.NameAbbr = textBox略称.Text;
            menjou.ShowOrder = int.Parse(textBox表示順序.Text);
        }


        private bool InsertOrUpdate()
        {
            bool result = false;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.MsSiMenjou_InsertOrUpdate(NBaseCommon.Common.LoginUser, menjou);
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
        private bool CheckDeleteUsing(MsSiMenjou data)
        {
            //MS_SI_MENJOU_KINDと
            //SI_MENJOU

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region MS_SI_MENJOU_KIND
                List<MsSiMenjouKind> menlist =
                    serviceClient.MsSiMenjouKind_GetRecordsByMsSiMenjouID(NBaseCommon.Common.LoginUser, data.MsSiMenjouID);

                if (menlist.Count > 0)
                {
                    return false;
                }
                #endregion

                #region SI_MENJOU
                List<SiMenjou> silist =
                    serviceClient.SiMenjou_GetRecordsByMsSiMenjouID(NBaseCommon.Common.LoginUser, data.MsSiMenjouID);

                if (silist.Count > 0)
                {
                    return false;
                }


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
                bool result = this.CheckDeleteUsing(this.menjou);
                if (result == false)
                {
                    MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                menjou.DeleteFlag = 1;
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
