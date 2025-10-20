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
    public partial class ViqVersion管理詳細Form : Form
    {
        private MsViqVersion viqVersion;
		
		//データを変更したかどうか？
		private bool ChangeFlag = false;


        public ViqVersion管理詳細Form() : this(new MsViqVersion())
        {
        }


        public ViqVersion管理詳細Form(MsViqVersion viqVersion)
        {
            this.viqVersion = viqVersion;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            if (!viqVersion.IsNew())
            {
                textBoxVersion.Text = viqVersion.ViqVersion;
                if (viqVersion.StartDate == DateTime.MinValue)
                {
                    nullableDateTimePickerStartDate.Value = null;
                }
                else
                {
                    nullableDateTimePickerStartDate.Value = viqVersion.StartDate;
                }

                if (viqVersion.EndDate == DateTime.MaxValue)
                {
                    nullableDateTimePickerEndDate.Value = null;
                }
                else
                {
                    nullableDateTimePickerEndDate.Value = viqVersion.EndDate;
                }
            }
            else
            {
                nullableDateTimePickerStartDate.Value = DateTime.Today;
                nullableDateTimePickerEndDate.Value = null;
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
            if (textBoxVersion.Text.Length == 0)
            {
                textBoxVersion.BackColor = Color.Pink;
                MessageBox.Show("名前を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxVersion.BackColor = Color.White;
                return false;
            }

            return true;
        }


        private void FillInstance()
        {
            viqVersion.ViqVersion = textBoxVersion.Text;
            viqVersion.StartDate = nullableDateTimePickerStartDate.Value != null ? (DateTime)nullableDateTimePickerStartDate.Value : DateTime.MinValue;
            viqVersion.EndDate = nullableDateTimePickerEndDate.Value != null ? (DateTime)nullableDateTimePickerEndDate.Value : DateTime.MaxValue;
        }


        private bool InsertOrUpdate()
        {
            bool result = false;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.MsViqVersion_InsertOrUpdate(NBaseCommon.Common.LoginUser, viqVersion);
            }

            return result;
        }


        private void button削除_Click(object sender, EventArgs e)
        {
            //このデータは利用しているため削除できません

            //削除前に使用しているかのチェックをする
            bool ret = this.CheckDeleteUsing(this.viqVersion);

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
                viqVersion.DeleteFlag = true;
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
        /// 対象のMsViqVersionデータが使用されているかを調べる
        /// 引数：チェックするデータ
        /// 返り値：削除可能→true、使用されている→false
        /// </summary>
        /// <returns></returns>
        private bool CheckDeleteUsing(MsViqVersion data)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
            }

            return true;
        }
    }
}
