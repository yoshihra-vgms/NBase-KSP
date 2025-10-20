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

namespace Senin
{
    public partial class 家族情報Form : Form
    {
        private 船員詳細Panel parentForm;
        private SiKazoku kazoku;
        private bool isNew;


        public 家族情報Form(船員詳細Panel parentPanel)
            : this(parentPanel, new SiKazoku(), true)
        {
        }


        public 家族情報Form(船員詳細Panel parentPanel, SiKazoku kazoku, bool isNew)
        {
            this.parentForm = parentPanel;
            this.kazoku = kazoku;
            this.isNew = isNew;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitFields();
        }


        private void InitFields()
        {
            if (!isNew)
            {
                textBox姓.Text = kazoku.Sei;
                textBox名.Text = kazoku.Mei;
                textBox姓カナ.Text = kazoku.SeiKana;
                textBox名カナ.Text = kazoku.MeiKana;
                if (kazoku.Sex == 1) radioButton女.Checked = true;
                textBox続柄.Text = kazoku.Zokugara;

                if (kazoku.Birthday != DateTime.MinValue)
                {
                    maskedTextBox生年月日.Text = kazoku.Birthday.ToShortDateString();
                }

                if (kazoku.Fuyou == 1) radioButton扶養無.Checked = true;
                maskedTextBoxTEL.Text = kazoku.Tel;
                textBox備考.Text = kazoku.Bikou;
            }
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

                //2021/07/29 m.yoshihar DB更新処理追加
                if (parentForm.InsertOrUpdate_家族情報(kazoku))
                {
                    MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //2021/07/29 引数無しに変更
                //parentForm.Refresh家族情報(kazoku); 
                parentForm.Refresh家族情報();

                Dispose();
            }
        }


        private bool ValidateFields()
        {
            if (textBox姓.Text.Length > 20)
            {
                textBox姓.BackColor = Color.Pink;
                MessageBox.Show("姓は20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox姓.BackColor = Color.White;
                return false;
            }
            else if (textBox名.Text.Length > 20)
            {
                textBox名.BackColor = Color.Pink;
                MessageBox.Show("名は20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox名.BackColor = Color.White;
                return false;
            }
            else if (textBox姓カナ.Text.Length > 20)
            {
                textBox姓カナ.BackColor = Color.Pink;
                MessageBox.Show("姓カナは20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox姓カナ.BackColor = Color.White;
                return false;
            }
            else if (textBox名カナ.Text.Length > 20)
            {
                textBox名カナ.BackColor = Color.Pink;
                MessageBox.Show("名カナは20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox名カナ.BackColor = Color.White;
                return false;
            }
            else if (textBox続柄.Text.Length > 20)
            {
                textBox続柄.BackColor = Color.Pink;
                MessageBox.Show("続柄は20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox続柄.BackColor = Color.White;
                return false;
            }
            else if (!DateTimeUtils.Empty(maskedTextBox生年月日.Text) && !DateTimeUtils.Validate(maskedTextBox生年月日.Text))
            {
                maskedTextBox生年月日.BackColor = Color.Pink;
                MessageBox.Show("生年月日を正しく入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBox生年月日.BackColor = Color.White;
                return false;
            }
            else if (textBox備考.Text.Length > 500)
            {
                textBox備考.BackColor = Color.Pink;
                MessageBox.Show("備考は500文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox備考.BackColor = Color.White;
                return false;
            }

            return true;
        }


        private void FillInstance()
        {
            kazoku.Sei = textBox姓.Text;
            kazoku.Mei = textBox名.Text;
            kazoku.SeiKana = textBox姓カナ.Text;
            kazoku.MeiKana = textBox名カナ.Text;
            kazoku.Sex = radioButton男.Checked ? 0 : 1;
            kazoku.Zokugara = textBox続柄.Text;

            if (DateTimeUtils.Empty(maskedTextBox生年月日.Text))
            {
                kazoku.Birthday = DateTime.MinValue;
            }
            else
            {
                kazoku.Birthday = DateTime.Parse(maskedTextBox生年月日.Text);
            }

            kazoku.Fuyou = radioButton扶養有.Checked ? 0 : 1;
            kazoku.Tel = maskedTextBoxTEL.Text;
            //kazoku.Bikou = textBox備考.Text;
            kazoku.Bikou = StringUtils.Escape(textBox備考.Text);
        }

        
        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                kazoku.DeleteFlag = 1;
                Save();
            }
        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
