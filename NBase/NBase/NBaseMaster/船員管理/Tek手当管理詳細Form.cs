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
    public partial class Tek手当管理詳細Form : Form
    {
        public List<MsSiAllowance> allowanceList = null;
        public List<NBaseData.DAC.MsVessel> vesselList = null;
        public List<MsSiShokumei> shokumeiList = null;
        public MsSiAllowance allowance = null;


        //編集をしたかどうか？
        private bool ChangeFlag = false;


        public Tek手当管理詳細Form(List<MsSiAllowance> allowanceList, List<NBaseData.DAC.MsVessel> vesselList, List<MsSiShokumei> shokumeiList)
            : this(allowanceList, vesselList, shokumeiList, new MsSiAllowance())
        {
        }


        public Tek手当管理詳細Form(List<MsSiAllowance> allowanceList, List<NBaseData.DAC.MsVessel> vesselList, List<MsSiShokumei> shokumeiList, MsSiAllowance allowance)
        {
            this.allowanceList = allowanceList;
            this.vesselList = vesselList;
            this.shokumeiList = shokumeiList;
            this.allowance = allowance;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitListBox船();
            InitComboBox部署();
            InitComboBox職名();

            if (!allowance.IsNew())
            {

                textBox手当名.Text = allowance.Name;
                textBox作業内容.Text = allowance.Contents;

                var vesselIds = allowance.TargetVessel.Split(',');
                for (int idx = 0; idx < checkedListBox対象船.Items.Count; idx ++)
                {
                    var item = checkedListBox対象船.Items[idx];
                    foreach (var id in vesselIds)
                    {
                        if (id == (item as NBaseData.DAC.MsVessel).MsVesselID.ToString())
                        {
                            checkedListBox対象船.SetItemChecked(idx, true);
                        }
                    }
                }

                comboBox部署.SelectedIndex = allowance.Department;
                textBox金額.Text = allowance.Allowance.ToString();
                comboBox職名.SelectedItem = shokumeiList.Where(obj => obj.MsSiShokumeiID == allowance.MsSiShokumeiID).First();

                if (allowance.ShowOrder != 999999999)
                    textBox_表示順.Text = allowance.ShowOrder.ToString();

                checkBox_DestributionFlag.Checked = (allowance.DistributionFlag == 1);
            }
            else
            {
                button削除.Enabled = false;
            }

			this.ChangeFlag = false;
        }


        private void InitListBox船()
        {
            checkedListBox対象船.Items.Clear();
            foreach (NBaseData.DAC.MsVessel v in vesselList)
            {
                checkedListBox対象船.Items.Add(v);
            }
        }

        private void InitComboBox部署()
        {
            comboBox部署.Items.Clear();
            comboBox部署.Items.Add("全員");
            comboBox部署.Items.Add("甲板部");
            comboBox部署.Items.Add("機関部");

            comboBox部署.SelectedIndex = 0;
        }

        private void InitComboBox職名()
        {
            comboBox職名.Items.Clear();

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
            if (allowance.DeleteFlag == 1)
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
            if (allowance.DeleteFlag == 1) // 削除処理時、ヴァリデーションはいらない
                return true;

            if (textBox手当名.Text.Length == 0)
            {
                textBox手当名.BackColor = Color.Pink;
                MessageBox.Show("手当名を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox手当名.BackColor = Color.White;
                return false;
            }
            var cheked = checkedListBox対象船.CheckedItems;
            if (cheked == null || cheked.Count == 0)
            {
                checkedListBox対象船.BackColor = Color.Pink;
                MessageBox.Show("対象船を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkedListBox対象船.BackColor = Color.White;
                return false;
            }
            if (textBox金額.Text.Length == 0 || NumberUtils.Validate(textBox金額.Text) == false)
            {
                textBox金額.BackColor = Color.Pink;
                MessageBox.Show("金額を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox金額.BackColor = Color.White;
                return false;
            }

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                var name = textBox手当名.Text;

                List<MsSiAllowance> check = serviceClient.MsSiAllowance_SearchRecords(NBaseCommon.Common.LoginUser, name);

                if (check.Count() > 0)
                {

                    var sameName = check.Where(o => o.Name == name).FirstOrDefault();

                    if (sameName != null && sameName.MsSiAllowanceID != allowance.MsSiAllowanceID)
                    {
                        MessageBox.Show($"既に、「{name}」は登録されています。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            return true;
        }



        private void FillInstance()
        {
            allowance.Name = textBox手当名.Text;
            allowance.Contents = textBox作業内容.Text;

            allowance.TargetVessel = null;
            var cheked = checkedListBox対象船.CheckedItems;
            foreach (var item in cheked)
            {
                allowance.TargetVessel += "," + (item as NBaseData.DAC.MsVessel).MsVesselID.ToString();
            }
            if (allowance.TargetVessel != null)
            {
                allowance.TargetVessel = allowance.TargetVessel.Substring(1);
            }
            allowance.Department = comboBox部署.SelectedIndex;
            allowance.Allowance = int.Parse(textBox金額.Text);
            allowance.MsSiShokumeiID = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;

            try
            {
                allowance.ShowOrder = int.Parse(textBox_表示順.Text);
            }
            catch
            {
                allowance.ShowOrder = 999999999;
            }

            allowance.DistributionFlag = checkBox_DestributionFlag.Checked ? 1 : 0;
        }





        private bool InsertOrUpdate()
        {
            bool result = false;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.MsSiAllowance_InsertOrUpdate(NBaseCommon.Common.LoginUser, allowance);
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
                allowance.DeleteFlag = 1;
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
