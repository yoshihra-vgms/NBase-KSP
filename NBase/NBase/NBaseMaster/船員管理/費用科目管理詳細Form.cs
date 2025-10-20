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
    public partial class 費用科目管理詳細Form : Form
    {
        private MsSiHiyouKamoku hiyouKamoku;
		
		//データを変更したかどうか？
		private bool ChangeFlag = false;


        public 費用科目管理詳細Form() : this(new MsSiHiyouKamoku())
        {
        }


        public 費用科目管理詳細Form(MsSiHiyouKamoku menjou)
        {
            this.hiyouKamoku = menjou;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            if (!hiyouKamoku.IsNew())
            {
                textBox名.Text = hiyouKamoku.Name;
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
                MessageBox.Show("費用科目名を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox名.BackColor = Color.White;
                return false;
            }
            else if (textBox名.Text.Length > 20)
            {
                textBox名.BackColor = Color.Pink;
                MessageBox.Show("費用科目名は20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox名.BackColor = Color.White;
                return false;
            }

            return true;
        }


        private void FillInstance()
        {
            hiyouKamoku.Name = textBox名.Text;
        }


        private bool InsertOrUpdate()
        {
            bool result = false;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.MsSiHiyouKamoku_InsertOrUpdate(NBaseCommon.Common.LoginUser, hiyouKamoku);
            }

            return result;
        }


        private void button削除_Click(object sender, EventArgs e)
        {
            //このデータは利用しているため削除できません

            //削除前に使用しているかのチェックをする
            bool ret = this.CheckDeleteUsing(this.hiyouKamoku);

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
                hiyouKamoku.DeleteFlag = 1;
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
        /// 対象のMsSiHiyouKamokuデータが使用されているかを調べる
        /// 引数：チェックするデータ
        /// 返り値：削除可能→true、使用されている→false
        /// </summary>
        /// <returns></returns>
        private bool CheckDeleteUsing(MsSiHiyouKamoku data)
        {
            //MsSiHiyouKamokuがFKでつながっているテーブルは
            //ER図によると
            //SI_JUNBIKINとMS_SI_DAIKOUMOKU
            //この二つのテーブルをチェックする。

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {

                #region SI_JUNBIKIN
                List<SiJunbikin> junlist = new List<SiJunbikin>();

                //使用されているかをチェックするため、関連するデータを引っ張ってくる
                junlist = serviceClient.SiJunbikin_GetRecordsByMsSiHiyouKamokuID(NBaseCommon.Common.LoginUser, data.MsSiHiyouKamokuID);

                //一つ以上取得できるなら使用されている。
                if (junlist.Count > 0)
                {
                    return false;
                }

                #endregion


                #region MS_SI_DAIKOUMOKU

                List<MsSiDaikoumoku> dailist = new List<MsSiDaikoumoku>();

                dailist = serviceClient.MsSiDaikoumoku_GetRecordsByMsSiHiyouKamokuID(NBaseCommon.Common.LoginUser, data.MsSiHiyouKamokuID);

                //使用されいる？
                if (dailist.Count > 0)
                {
                    return false;
                }
                
                #endregion
            }

            return true;
        }
    }
}
