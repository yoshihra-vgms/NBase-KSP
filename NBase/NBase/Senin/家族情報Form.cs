using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;

namespace Senin
{
    public partial class 家族情報Form : Form
    {
        private 船員詳細Panel parentPanel;
        private SiKazoku kazoku;
        private bool isNew;


        public 家族情報Form(船員詳細Panel parentPanel)
            : this(parentPanel, new SiKazoku(), true)
        {
        }


        public 家族情報Form(船員詳細Panel parentPanel, SiKazoku kazoku, bool isNew)
        {
            this.parentPanel = parentPanel;
            this.kazoku = kazoku;
            this.isNew = isNew;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox緊急連絡先();
            InitComboBox続柄();
            InitComboBox都道府県();
            InitFields();
        }

        private void InitComboBox緊急連絡先()
        {
            foreach (MsSiOptions o in SeninTableCache.instance().GetMsSiOptionsList(NBaseCommon.Common.LoginUser, (int)MsSiOptions.EnumSelecterID.家族_緊急連絡先))
            {
                comboBox緊急連絡先.Items.Add(o);
            }
            comboBox緊急連絡先.SelectedIndex = 0;
        }

        private void InitComboBox続柄()
        {
            foreach (MsSiOptions o in SeninTableCache.instance().GetMsSiOptionsList(NBaseCommon.Common.LoginUser, (int)MsSiOptions.EnumSelecterID.家族_続柄))
            {
                comboBox続柄.Items.Add(o);
            }
        }
        private void InitComboBox都道府県()
        {
            foreach (MsSiOptions o in SeninTableCache.instance().GetMsSiOptionsList(NBaseCommon.Common.LoginUser, (int)MsSiOptions.EnumSelecterID.都道府県))
            {
                comboBox都道府県.Items.Add(o);
            }
        }

        private void InitFields()
        {

            // 初期状態は、「同居」選択状態にする
            maskedTextBox郵便番号.Enabled = false;
            comboBox都道府県.Enabled = false;
            textBox市区町村.Enabled = false;
            textBox番地.Enabled = false;


            if (!isNew)
            {
                foreach (MsSiOptions o in comboBox緊急連絡先.Items)
                {
                    if (o.MsSiOptionsID == kazoku.EmergencyKind)
                    {
                        comboBox緊急連絡先.SelectedItem = o;
                        break;
                    }
                }

                if (kazoku.Kind == 1) radioButton家族外.Checked = true;

                textBox姓.Text = kazoku.Sei;
                textBox名.Text = kazoku.Mei;
                textBox姓カナ.Text = kazoku.SeiKana;
                textBox名カナ.Text = kazoku.MeiKana;

                if (kazoku.Sex == 2) radioButton女.Checked = true;
                foreach (MsSiOptions o in comboBox続柄.Items)
                {
                    if (o.MsSiOptionsID == kazoku.Tuzukigara)
                    {
                        comboBox続柄.SelectedItem = o;
                        break;
                    }
                }
                if (kazoku.Birthday != DateTime.MinValue)
                {
                    maskedTextBox生年月日.Text = kazoku.Birthday.ToShortDateString();
                    textBox年齢.Text = DateTimeUtils.年齢計算(kazoku.Birthday).ToString();
                }
                textBoxTEL.Text = kazoku.Tel;
                textBox職業.Text = kazoku.Occupation;

                radioButton同居.Checked = kazoku.LivingTogether == 0 ? true : false;
                radioButton別居.Checked = kazoku.LivingTogether == 1 ? true : false;
                if (kazoku.LivingTogether == 1)
                {
                    maskedTextBox郵便番号.Text = kazoku.ZipCode;

                    foreach (MsSiOptions o in comboBox都道府県.Items)
                    {
                        if (o.MsSiOptionsID == kazoku.Prefectures)
                        {
                            comboBox都道府県.SelectedItem = o;
                            break;
                        }
                    }
                    textBox市区町村.Text = kazoku.CityTown;
                    textBox番地.Text = kazoku.Street;
                }

                radioButton扶養区分無.Checked = kazoku.SeamensInsuranceDependent == 0 ? true : false;
                radioButton扶養区分有.Checked = kazoku.SeamensInsuranceDependent == 1 ? true : false;

                radioButton扶養無.Checked = kazoku.Dependent == 0 ? true : false;
                radioButton扶養該当.Checked = kazoku.Dependent == 1 ? true : false;


                radioButton老年者無.Checked = kazoku.Elderly == 0 ? true : false;
                radioButton老年者該当.Checked = kazoku.Elderly == 1 ? true : false;


                radioButton障害者無.Checked = kazoku.Handicapped == 0 ? true : false;
                radioButton障害者一般.Checked = kazoku.Handicapped == 1 ? true : false;
                radioButton障害者特別.Checked = kazoku.Handicapped == 2 ? true : false;


                textBox備考.Text = kazoku.Remarks;
            }
            else
            {
                button削除.Enabled = false;
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

                if (parentPanel.InsertOrUpdate_家族情報(kazoku))
                {
                    MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                parentPanel.Refresh家族情報();
                Dispose();
            }
        }

        private bool ValidateFields()
        {
            if (textBox姓.Text.Length == 0)
            {
                textBox姓.BackColor = Color.Pink;
                MessageBox.Show("姓を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox姓.BackColor = Color.White;
                return false;
            }
            else if (textBox姓.Text.Length > 20)
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

            kazoku.EmergencyKind = (comboBox緊急連絡先.SelectedItem as MsSiOptions).MsSiOptionsID;

            kazoku.Kind = radioButton家族.Checked ? 0 : 1;

            kazoku.Sei = textBox姓.Text;
            kazoku.Mei = textBox名.Text;
            kazoku.SeiKana = textBox姓カナ.Text;
            kazoku.MeiKana = textBox名カナ.Text;
            kazoku.Sex = radioButton男.Checked ? 0 : 1;

            kazoku.Sex = radioButton男.Checked ? 1 : 2;

            if (comboBox続柄.SelectedItem is MsSiOptions)
            {
                kazoku.Tuzukigara = (comboBox続柄.SelectedItem as MsSiOptions).MsSiOptionsID;
            }

            if (DateTimeUtils.Empty(maskedTextBox生年月日.Text))
            {
                kazoku.Birthday = DateTime.MinValue;
            }
            else
            {
                kazoku.Birthday = DateTime.Parse(maskedTextBox生年月日.Text);
            }

            kazoku.Tel = textBoxTEL.Text;

            kazoku.Occupation = textBox職業.Text;


            kazoku.LivingTogether = radioButton同居.Checked ? 0 : 1;
            if (radioButton別居.Checked)
            {
                kazoku.ZipCode = maskedTextBox郵便番号.Text;
                if (comboBox都道府県.SelectedItem is MsSiOptions)
                    kazoku.Prefectures = (comboBox都道府県.SelectedItem as MsSiOptions).MsSiOptionsID;
                kazoku.CityTown = textBox市区町村.Text;
                kazoku.Street = textBox番地.Text;
            }
            else
            {
                kazoku.ZipCode = null;
                kazoku.Prefectures = null;
                kazoku.CityTown = null;
                kazoku.Street = null;
            }

            kazoku.SeamensInsuranceDependent = radioButton扶養区分無.Checked ? 0 : 1;
            kazoku.Dependent = radioButton扶養無.Checked ? 0 : 1;
            kazoku.Elderly = radioButton老年者無.Checked ? 0 : 1;
            kazoku.Handicapped = radioButton障害者無.Checked ? 0 : radioButton障害者一般.Checked ? 1 : 2;

            kazoku.Remarks = textBox備考.Text;

            kazoku.EditFlag = true;
        }

        
        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "削除してよろしいですか？",
            //if (MessageBox.Show(this, "It deletes." + System.Environment.NewLine + "Is it all right? ",
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


        private void maskedTextBox生年月日_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime 生年月日 = DateTime.Parse(maskedTextBox生年月日.Text);
                textBox年齢.Text = DateTimeUtils.年齢計算(生年月日).ToString();
            }
            catch
            {
            }
        }

        private void radioButton同居_CheckedChanged(object sender, EventArgs e)
        {
            maskedTextBox郵便番号.Enabled = false;
            comboBox都道府県.Enabled = false;
            textBox市区町村.Enabled = false;
            textBox番地.Enabled = false;
        }

        private void radioButton別居_CheckedChanged(object sender, EventArgs e)
        {
            maskedTextBox郵便番号.Enabled = true;
            comboBox都道府県.Enabled = true;
            textBox市区町村.Enabled = true;
            textBox番地.Enabled = true;
        }

    }
}
