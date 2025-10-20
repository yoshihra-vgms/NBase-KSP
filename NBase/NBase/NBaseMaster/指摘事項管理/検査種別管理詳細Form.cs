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

namespace NBaseMaster.指摘事項管理
{
    public partial class 検査種別管理詳細Form : Form
    {
        private MsItemKind itemKind;
		
		//データを変更したかどうか？
		private bool ChangeFlag = false;


        public 検査種別管理詳細Form() : this(new MsItemKind())
        {
        }


        public 検査種別管理詳細Form(MsItemKind itemKind)
        {
            this.itemKind = itemKind;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            if (!itemKind.IsNew())
            {
                textBox名.Text = itemKind.ItemKindName;
            }
            else
            {
                button削除.Enabled = false;
            }

			//編集可否を初期化
			this.ChangeFlag = false;
        }


        private void button更新_Click(object sender, EventArgs e)
        {
            Save(false);
        }

		//引数：削除かどうか？
        private void Save(bool dele)
        {
            if (ValidateFields())
            {
                FillInstance();

                if (InsertOrUpdate())
                {
					if (dele == false)
					{
						MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else
					{
						MessageBox.Show(this, "削除しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					
					DialogResult = DialogResult.OK;
                    Dispose();
                }
                else
                {
					if (dele == false)
					{
						MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						MessageBox.Show(this, "削除に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
                }
            }
        }


        private bool ValidateFields()
        {
            if (textBox名.Text.Length == 0)
            {
                textBox名.BackColor = Color.Pink;
                MessageBox.Show("検査種別名を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox名.BackColor = Color.White;
                return false;
            }
            else if (textBox名.Text.Length > 20)
            {
                textBox名.BackColor = Color.Pink;
                MessageBox.Show("検査種別名は20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox名.BackColor = Color.White;
                return false;
            }

            return true;
        }


        private void FillInstance()
        {
            itemKind.ItemKindName = textBox名.Text;
        }


        private bool InsertOrUpdate()
        {
            bool result = false;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.MsItemKind_InsertOrUpdate(NBaseCommon.Common.LoginUser, itemKind);
            }

            return result;
        }


        private void button削除_Click(object sender, EventArgs e)
        {
            //このデータは利用しているため削除できません

            //削除前に使用しているかのチェックをする
            bool ret = this.CheckDeleteUsing(this.itemKind);

            if (ret == false)
            {
                MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                itemKind.DeleteFlag = true;
                Save(true);
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

		//データが変更されたとき
		private void ChangeDataText(object sender, EventArgs e)
		{
			this.ChangeFlag = true;
		}


        /// <summary>
        /// 対象のMsItemKindデータが使用されているかを調べる
        /// 引数：チェックするデータ
        /// 返り値：削除可能→true、使用されている→false
        /// </summary>
        /// <returns></returns>
        private bool CheckDeleteUsing(MsItemKind data)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
            }

            return true;
        }
    }
}
