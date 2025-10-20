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
    public partial class 明細管理詳細Form : Form
    {
        private MsSiMeisai meisai;
        private List<MsSiHiyouKamoku> hiyouKamokus;
        private List<MsSiDaikoumoku> daikoumokus;
        private List<MsSiKamoku> siKamokus;

		//編集をしたかどうか？
		private bool ChangeFlag = false;


        public 明細管理詳細Form(List<MsSiHiyouKamoku> hiyouKamokus, List<MsSiDaikoumoku> daikoumokus, List<MsSiKamoku> siKamokus)
            : this(new MsSiMeisai(), hiyouKamokus, daikoumokus, siKamokus)
        {
        }


        public 明細管理詳細Form(MsSiMeisai meisai, List<MsSiHiyouKamoku> hiyouKamokus, List<MsSiDaikoumoku> daikoumokus, List<MsSiKamoku> siKamokus)
        {
            this.meisai = meisai;
            this.hiyouKamokus = hiyouKamokus;
            this.daikoumokus = daikoumokus;
            this.siKamokus = siKamokus;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox_費用科目名();
            InitComboBox_明細科目名();

            if (!meisai.IsNew())
            {
                foreach (MsSiHiyouKamoku m in hiyouKamokus)
                {
                    if (m.MsSiHiyouKamokuID == meisai.MsSiHiyouKamokuID)
                    {
                        comboBox費用科目名.SelectedItem = m;
                        break;
                    }
                }

                foreach (MsSiDaikoumoku m in daikoumokus)
                {
                    if (m.MsSiDaikoumokuID == meisai.MsSiDaikoumokuID)
                    {
                        comboBox大項目名.SelectedItem = m;
                        break;
                    }
                }

                foreach (MsSiKamoku m in siKamokus)
                {
                    if (m.MsSiKamokuId == meisai.MsSiKamokuId)
                    {
                        comboBox明細科目名.SelectedItem = m;
                        break;
                    }
                }

                textBox明細名.Text = meisai.Name;
            }
            else
            {
                button削除.Enabled = false;
            }

			this.ChangeFlag = false;
        }


        private void InitComboBox_費用科目名()
        {
            comboBox費用科目名.Items.Add(string.Empty);

            foreach (MsSiHiyouKamoku m in hiyouKamokus)
            {
                comboBox費用科目名.Items.Add(m);
            }

            comboBox費用科目名.SelectedIndex = 0;
        }


        private void InitComboBox_明細科目名()
        {
            comboBox明細科目名.Items.Add(string.Empty);

            foreach (MsSiKamoku m in siKamokus)
            {
                comboBox明細科目名.Items.Add(m);
            }

            comboBox明細科目名.SelectedIndex = 0;
        }


        private void button更新_Click(object sender, EventArgs e)
        {
            Save();
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
            if (comboBox費用科目名.SelectedIndex == 0)
            {
                comboBox費用科目名.BackColor = Color.Pink;
                MessageBox.Show("費用科目名を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox費用科目名.BackColor = Color.White;
                return false;
            }
            else if (comboBox大項目名.SelectedIndex == 0)
            {
                comboBox大項目名.BackColor = Color.Pink;
                MessageBox.Show("大項目名を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox大項目名.BackColor = Color.White;
                return false;
            }
            else if (textBox明細名.Text.Length == 0)
            {
                textBox明細名.BackColor = Color.Pink;
                MessageBox.Show("明細名を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox明細名.BackColor = Color.White;
                return false;
            }
            else if (textBox明細名.Text.Length > 20)
            {
                textBox明細名.BackColor = Color.Pink;
                MessageBox.Show("明細名は20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox明細名.BackColor = Color.White;
                return false;
            }

            return true;
        }


        private void FillInstance()
        {
            meisai.MsSiDaikoumokuID = (comboBox大項目名.SelectedItem as MsSiDaikoumoku).MsSiDaikoumokuID;
            meisai.Name = textBox明細名.Text;

            if (comboBox明細科目名.SelectedIndex > 0)
            {
                meisai.MsSiKamokuId = (comboBox明細科目名.SelectedItem as MsSiKamoku).MsSiKamokuId;
            }
            else
            {
                meisai.MsSiKamokuId = int.MinValue;
            }
        }


        private bool InsertOrUpdate()
        {
            bool result = false;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.MsSiMeisai_InsertOrUpdate(NBaseCommon.Common.LoginUser, meisai);
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
                meisai.DeleteFlag = 1;
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


        private void comboBox費用科目名_SelectedIndexChanged(object sender, EventArgs e)
        {
			this.ChangeFlag = true;

            MsSiHiyouKamoku m = comboBox費用科目名.SelectedItem as MsSiHiyouKamoku;

            if (m != null)
            {
                comboBox大項目名.Items.Clear();
                comboBox大項目名.Items.Add(string.Empty);

                foreach (MsSiDaikoumoku d in daikoumokus)
                {
                    if (d.MsSiHiyouKamokuID == m.MsSiHiyouKamokuID)
                    {
                        comboBox大項目名.Items.Add(d);
                    }
                }

                comboBox大項目名.SelectedIndex = 0;
            }
        }

		//comboが変更されたとき
		private void ComboChange(object sender, EventArgs e)
		{
			this.ChangeFlag = true;
		}
    }
}
