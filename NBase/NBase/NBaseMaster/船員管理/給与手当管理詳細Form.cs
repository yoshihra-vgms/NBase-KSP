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
    public partial class 給与手当管理詳細Form : Form
    {
        public List<MsSiKyuyoTeate> kyuyoTeateList = null;
        public List<MsSiShokumei> shokumeiList = null;
        public MsSiKyuyoTeateSet kyuyoTeateSet = null;


        //編集をしたかどうか？
		private bool ChangeFlag = false;


        public 給与手当管理詳細Form(List<MsSiKyuyoTeate> kyuyoTeateList, List<MsSiShokumei> shokumeiList)
            : this(kyuyoTeateList, shokumeiList, new MsSiKyuyoTeateSet())
        {
        }


        public 給与手当管理詳細Form(List<MsSiKyuyoTeate> kyuyoTeateList, List<MsSiShokumei> shokumeiList, MsSiKyuyoTeateSet kyuyoTeateSet)
        {
            this.kyuyoTeateList = kyuyoTeateList;
            this.shokumeiList = shokumeiList;
            this.kyuyoTeateSet = kyuyoTeateSet;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox給与手当();
            InitComboBox職名();

            if (!kyuyoTeateSet.IsNew())
            {
                comboBox給与手当.SelectedItem = kyuyoTeateList.Where(obj => obj.MsSiKyuyoTeateID == kyuyoTeateSet.MsSiKyuyoTeateID).First();
                comboBox職名.SelectedItem = shokumeiList.Where(obj => obj.MsSiShokumeiID == kyuyoTeateSet.MsSiShokumeiID).First();
                textBox単価.Text = kyuyoTeateSet.Tanka.ToString();

                comboBox給与手当.Enabled = false;
                comboBox職名.Enabled = false;
            }
            else
            {
                button削除.Enabled = false;
            }

			this.ChangeFlag = false;
        }


        private void InitComboBox給与手当()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                kyuyoTeateList = serviceClient.MsSiKyuyoTeate_GetRecords(NBaseCommon.Common.LoginUser);
            }

            foreach (MsSiKyuyoTeate m in kyuyoTeateList)
            {
                comboBox給与手当.Items.Add(m);
            }
            if (comboBox給与手当.Items.Count > 0)
                comboBox給与手当.SelectedIndex = 0;
        }

        private void InitComboBox職名()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                shokumeiList = serviceClient.MsSiShokumei_GetRecords(NBaseCommon.Common.LoginUser);
            }

            foreach (MsSiShokumei s in shokumeiList)
            {
                comboBox職名.Items.Add(s);
            }

            comboBox職名.SelectedIndex = 0;
        }


        private void button更新_Click(object sender, EventArgs e)
        {
            Save();
        }


        private void Save()
        {
            string msg = "登録";
            if (kyuyoTeateSet.DeleteFlag == 1)
            {
                msg = "削除";
            }
            if (ValidateFields())
            {
                FillInstance();

                if (InsertOrUpdate())
                {
                    MessageBox.Show(this, msg +"しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Dispose();
                }
                else
                {
                    MessageBox.Show(this, msg + "に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private bool ValidateFields()
        {
            if (kyuyoTeateSet.DeleteFlag == 1) // 削除処理時、ヴァリデーションはいらない
                return true;


            if (textBox単価.Text.Length == 0 || NumberUtils.Validate(textBox単価.Text) == false)
            {
                textBox単価.BackColor = Color.Pink;
                MessageBox.Show("単価を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox単価.BackColor = Color.White;
                return false;
            }

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                int kyuyoTeateId = (comboBox給与手当.SelectedItem as MsSiKyuyoTeate).MsSiKyuyoTeateID;
                int shokumeiId = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;

                List<MsSiKyuyoTeateSet> check = serviceClient.MsSiKyuyoTeateSet_SearchRecords(NBaseCommon.Common.LoginUser, kyuyoTeateId, shokumeiId);

                if (check.Count() > 0)
                {
                    if (kyuyoTeateSet.IsNew() || check.Any(obj => obj.MsSiKyuyoTeateSetID != kyuyoTeateSet.MsSiKyuyoTeateSetID))
                    {
                        MessageBox.Show("既に、給与手当、職名の組み合わせは登録されています。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            return true;
        }


        private void FillInstance()
        {
            kyuyoTeateSet.MsSiKyuyoTeateID = (comboBox給与手当.SelectedItem as MsSiKyuyoTeate).MsSiKyuyoTeateID;
            kyuyoTeateSet.MsSiShokumeiID = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            kyuyoTeateSet.Tanka = int.Parse(textBox単価.Text);
        }


        private bool InsertOrUpdate()
        {
            bool result = false;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.MsSiKyuyoTeateSet_InsertOrUpdate(NBaseCommon.Common.LoginUser, kyuyoTeateSet);
            }

            return result;
        }


        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                kyuyoTeateSet.DeleteFlag = 1;
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

		//comboが変更されたとき
		private void Edited(object sender, EventArgs e)
		{
			this.ChangeFlag = true;
		}
    }
}
