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
using NBaseData.DS;

namespace Senin
{
    public partial class 基本給詳細Form : Form
    {
        private 船員詳細Panel parentForm;
        private SiSalary salary;
        private MsSiSalary msSiSalary;
        private bool isNew;


        public 基本給詳細Form(船員詳細Panel parentForm, SiSalary salary, MsSiSalary msSiSalary)
        {
            this.parentForm = parentForm;
            this.salary = salary;
            this.msSiSalary = msSiSalary;

            InitializeComponent();
        }

        private void 基本給詳細Form_Load(object sender, EventArgs e)
        {
            dateTimePicker開始日.Value = salary.StartDate;
            nullableDateTimePicker終了日.Value = null;
            if (salary.EndDate != DateTime.MinValue)
            {
                nullableDateTimePicker終了日.Value = salary.EndDate;
            }

            textBox標令.Text = salary.Hyorei > 0 ? salary.Hyorei.ToString() : "";
            textBox標令給.Text = 金額出力(salary.HyoreiAllowance);

            InitComboBox職名();
            //foreach (MsSiSalaryRank obj in comboBox職務.Items)
            //{
            //    if (obj.MsSiShokumeiSalaryId == salary.MsSiShokumeiSalaryId)
            //    {
            //        comboBox職務.SelectedItem = obj;
            //        break;
            //    }
            //}
            textBox経験年数.Text = salary.Experience >= 0 ? salary.Experience.ToString() : "";
            textBox職務給.Text = 金額出力(salary.RankAllowance);

            textBox資格給.Text = 金額出力(salary.QualificationAllowance);

            textBox基本給.Text = 金額出力(salary.BasicSalary);
            textBox組合費.Text = 金額出力(salary.UnionDues);

            textBox_SharedRemarks.Text = salary.SharedRemarks;
            textBox_FiscalYearRemarks.Text = salary.FiscalYearRemarks;
        }

        private void InitComboBox職名()
        {
            //foreach (var obj in msSiSalary.SalaryRankList)
            //{
            //    comboBox職務.Items.Add(obj);
            //}
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

                if (parentForm.InsertOrUpdate_基本給(salary))
                {
                    MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                parentForm.Refresh基本給();
                Dispose();
            }
        }


        private bool ValidateFields()
        {
            if (NumberUtils.ValidateDecimal(textBox標令.Text, 4) == false)
            {
                textBox標令.BackColor = Color.Pink;
                MessageBox.Show("標令は小数点以下4桁以内で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox標令.BackColor = Color.White;
                return false;
            }
            var val = 金額取得(textBox標令給.Text);
            if (val > 1000000) // 100万より大きい場合
            {
                textBox標令給.BackColor = Color.Pink;
                MessageBox.Show("標令給は半角数字6文字以内で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox標令給.BackColor = Color.White;
                return false;
            }

            if (NumberUtils.ValidateDecimal(textBox経験年数.Text, 3) == false)
            {
                textBox経験年数.BackColor = Color.Pink;
                MessageBox.Show("経験年数は小数点以下3桁以内で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox経験年数.BackColor = Color.White;
                return false;
            }
            val = 金額取得(textBox職務給.Text);
            if (val > 1000000) // 100万より大きい場合
            {
                textBox職務給.BackColor = Color.Pink;
                MessageBox.Show("職務給は半角数字6文字以内で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox職務給.BackColor = Color.White;
                return false;
            }

            val = 金額取得(textBox資格給.Text);
            if (val > 1000000) // 100万より大きい場合
            {
                textBox資格給.BackColor = Color.Pink;
                MessageBox.Show("資格給は半角数字6文字以内で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox資格給.BackColor = Color.White;
                return false;
            }

            val = 金額取得(textBox基本給.Text);
            if (val > 1000000) // 100万より大きい場合
            {
                textBox基本給.BackColor = Color.Pink;
                MessageBox.Show("基本給は半角数字6文字以内で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox基本給.BackColor = Color.White;
                return false;
            }

            val = 金額取得(textBox組合費.Text);
            if (val > 1000000) // 100万より大きい場合
            {
                textBox組合費.BackColor = Color.Pink;
                MessageBox.Show("組合費は半角数字6文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox組合費.BackColor = Color.White;
                return false;
            }

            return true;
        }


        private void FillInstance()
        {
            //salary.Kind セットされている
            salary.MsSiSalaryID = msSiSalary.MsSiSalaryID;

            salary.StartDate = dateTimePicker開始日.Value;

            if (DateTimeUtils.Validate(nullableDateTimePicker終了日.Text))
            {
                salary.EndDate = (DateTime)nullableDateTimePicker終了日.Value;
            }
            else
            {
                salary.EndDate = DateTime.MinValue;
            }

            decimal work;
            decimal.TryParse(textBox標令.Text, out work);
            salary.Hyorei = work;

            salary.HyoreiAllowance = 金額取得(textBox標令給.Text); ;

            //var msr = (comboBox職務.SelectedItem as MsSiSalaryRank);
            //salary.MsSiShokumeiSalaryId = msr.MsSiShokumeiSalaryId;

            decimal.TryParse(textBox経験年数.Text, out work);
            salary.Experience = work;

            salary.RankAllowance = 金額取得(textBox職務給.Text);
            salary.QualificationAllowance = 金額取得(textBox資格給.Text);

            salary.BasicSalary = 金額取得(textBox基本給.Text); 

            salary.UnionDues = 金額取得(textBox組合費.Text);

            salary.SharedRemarks = textBox_SharedRemarks.Text;
            salary.FiscalYearRemarks = textBox_FiscalYearRemarks.Text;
        }


        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                salary.DeleteFlag = 1;
                Save();
            }
        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        private void textBox標令_Leave(object sender, EventArgs e)
        {
            標令給計算();
            基本給計算();
        }

        private void comboBox職務_SelectedIndexChanged(object sender, EventArgs e)
        {
            //職務給計算();
            基本給計算();
        }

        private void textBox経験年数_Leave(object sender, EventArgs e)
        {
            //if (salary.Kind == (int)MsSiSalary.KIND.フェリー)
            //    return;

            //職務給計算();
            基本給計算();
        }



        private void 標令給計算()
        {
            if (NumberUtils.ValidateDecimal(textBox標令.Text,4) == false)
            {
                MessageBox.Show("標令は、数字２桁、少数点以下４桁位内で入力してください");
                return;
            }
            decimal hyorei = 0;
            decimal.TryParse(textBox標令.Text, out hyorei);

            decimal integer = Math.Truncate(hyorei);


            var msh = msSiSalary.SalaryHyoreiList.Where(obj => obj.Hyorei <= integer).OrderByDescending(obj => obj.Hyorei).FirstOrDefault();

            if (msh == null)
            {
                MessageBox.Show("マスタが整備されていません。（基本給マスタ）", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 小数部の計算
            decimal fractional = hyorei - integer;

            decimal fractionalAmount = 0;

            //if (salary.Kind == (int)MsSiSalary.KIND.フェリー)
            //{
            //    // フェリーの場合、加算額は、一つ上の標令の値を使用する
            //    var msh2 = msSiSalary.SalaryHyoreiList.Where(obj => obj.Hyorei > hyorei).OrderBy(obj => obj.Hyorei).FirstOrDefault();
            //    if (fractional != 0 && msh2.AdditionalAmount > 0)
            //    {
            //        //fractionalAmount = msh2.AdditionalAmount * fractional;
            //        fractionalAmount = Math.Ceiling((msh2.AdditionalAmount / 10) * fractional) * 10;
            //    }
            //}
            //else
            //{
                // 内航の場合、同列の値を使用する
                if (fractional != 0 && msh.AdditionalAmount > 0)
                {
                    fractionalAmount = Math.Round((msh.AdditionalAmount / 10) * fractional) * 10;
                }
            //}

            textBox標令給.Text = 金額出力(msh.Allowance + fractionalAmount);
        }


        //private void 職務給計算()
        //{
        //    var msr = (comboBox職務.SelectedItem as MsSiSalaryRank);
        //    if (msr == null)
        //        return;

        //    decimal exp = 0;
        //    decimal integer = 0;
        //    decimal fractional = 0;

        //    if (salary.Kind == (int)MsSiSalary.KIND.内航)
        //    {
        //        decimal.TryParse(textBox経験年数.Text, out exp);

        //        // 整数部の計算
        //        integer = Math.Truncate(exp);

        //        // 小数部の計算
        //        fractional = exp - integer;
        //    }

        //    decimal integerAmount = 0;
        //    decimal fractionalAmount = 0;

        //    if (integer == 0)
        //    {
        //        integerAmount = msr.Allowance0;
        //        if (fractional > 0)
        //        {
        //            fractionalAmount = Math.Round(((msr.Allowance1 - msr.Allowance0) / 10) * fractional) * 10;
        //        }
        //    }
        //    else if (integer == 1)
        //    {
        //        integerAmount = msr.Allowance1;
        //        if (fractional > 0)
        //        {
        //            fractionalAmount = Math.Round(((msr.Allowance2 - msr.Allowance1) / 10) * fractional) * 10;
        //        }
        //    }
        //    else if (integer == 2)
        //    {
        //        integerAmount = msr.Allowance2;
        //        if (fractional > 0)
        //        {
        //            fractionalAmount = Math.Round(((msr.Allowance3 - msr.Allowance2) / 10) * fractional) * 10;
        //        }
        //    }
        //    else if (integer == 3)
        //    {
        //        integerAmount = msr.Allowance3;
        //        if (fractional > 0)
        //        {
        //            fractionalAmount = Math.Round(((msr.Allowance4 - msr.Allowance3) / 10) * fractional) * 10;
        //        }
        //    }
        //    else if (integer == 4)
        //    {
        //        integerAmount = msr.Allowance4;
        //        if (fractional > 0)
        //        {
        //            fractionalAmount = Math.Round(((msr.Allowance5 - msr.Allowance4) / 10) * fractional) * 10;
        //        }
        //    }
        //    else if (integer == 5)
        //    {
        //        integerAmount = msr.Allowance5;
        //        if (fractional > 0)
        //        {
        //            fractionalAmount = Math.Round(((msr.Allowance6 - msr.Allowance5) / 10) * fractional) * 10;
        //        }
        //    }
        //    else
        //    {
        //        integerAmount = msr.Allowance6;

        //    }


        //    textBox職務給.Text = 金額出力(integerAmount + fractionalAmount);
        //}


        private void 基本給計算()
        {
            decimal amount1 = 金額取得(textBox標令給.Text);

            decimal amount2 = 金額取得(textBox職務給.Text);

            decimal amount3 = 金額取得(textBox資格給.Text);


            textBox基本給.Text = 金額出力(amount1 + amount2 + amount3);
        }



        public string 金額出力(decimal val)
        {
            if (val > 0)
                return val.ToString("N0");
            else
                return null;
        }

        public decimal 金額取得(string val)
        {
            if (val == null || val.Length == 0)
                return 0;
            else if (val.IndexOf(",") > 0)
                return decimal.Parse(val, System.Globalization.NumberStyles.Currency);
            else
                return decimal.Parse(val);
        }

        private void textBox基本給_Leave(object sender, EventArgs e)
        {
            var val = 金額取得(textBox基本給.Text);
            textBox基本給.Text = 金額出力(val);
        }

        private void textBox組合費_Leave(object sender, EventArgs e)
        {
            var val = 金額取得(textBox組合費.Text);
            textBox組合費.Text = 金額出力(val);
        }

        private void textBox資格給_Leave(object sender, EventArgs e)
        {
            var val = 金額取得(textBox資格給.Text);
            textBox資格給.Text = 金額出力(val);

            基本給計算();
        }


        private void textBox標令給_Leave(object sender, EventArgs e)
        {
            var val = 金額取得(textBox標令給.Text);
            textBox標令給.Text = 金額出力(val);

            基本給計算();
        }

        private void textBox職務給_Leave(object sender, EventArgs e)
        {
            var val = 金額取得(textBox職務給.Text);
            textBox職務給.Text = 金額出力(val);

            基本給計算();
        }


        public virtual void textBox_Decimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = InputCheck(sender, e, true, false);
        }

        public bool InputCheck(object sender, KeyPressEventArgs e, bool 少数点許可, bool マイナス可)
        {
            // 制御文字は入力可
            if (char.IsControl(e.KeyChar))
            {
                return false;
            }

            // 数字(0-9)は入力可
            if (char.IsDigit(e.KeyChar))
            {
                return false;
            }

            if (マイナス可 && e.KeyChar == '-')
            {
                TextBox target = sender as TextBox;
                if (target.SelectionStart == 0)
                {
                    // １文字目のマイナス入力以外はNG
                    return false;
                }
            }

            // 少数点許可 で 小数点は１つだけ入力可
            if (少数点許可 && e.KeyChar == '.')
            {
                TextBox target = sender as TextBox;
                if (target.Text.IndexOf('.') < 0)
                {
                    // 複数のピリオド入力はNG
                    return false;
                }
            }

            // 上記以外は入力不可
            return true;
        }

    }
}
