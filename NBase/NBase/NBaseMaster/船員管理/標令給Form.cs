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

namespace NBaseMaster.船員管理
{
    public partial class 標令給Form : Form
    {
        private MsSiSalaryHyorei hyorei;
      
        //データを編集したかどうか？
        private bool ChangeFlag = false;

        public 標令給Form(MsSiSalaryHyorei hyorei)
        {
            InitializeComponent();
            this.hyorei = hyorei;
        }

        private void 評価Form_Load(object sender, EventArgs e)
        {

            textBox標令.Text = NBaseUtil.NumberUtils.ToString(hyorei.Hyorei);
            textBox支給額.Text = hyorei.Allowance.ToString();
            textBox加算額.Text = hyorei.AdditionalAmount.ToString();

            this.ChangeFlag = false;
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

        private void button更新_Click(object sender, EventArgs e)
        {
            if (Varidation())
            {
                hyorei.Hyorei = NBaseUtil.StringUtils.ToDecimal(textBox標令.Text);
                hyorei.Allowance = decimal.Parse(textBox支給額.Text);
                hyorei.AdditionalAmount = decimal.Parse(textBox加算額.Text);


                DialogResult = DialogResult.OK;
                Dispose();
            }
        }

        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                //削除チェック
                bool result = this.CheckDeleteUsing();
                if (result == false)
                {
                    MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                hyorei.DeleteFlag = 1;

                DialogResult = DialogResult.OK;
                Dispose();
            }
        }

        //データを編集した時
        private void DataChange(object sender, EventArgs e)
        {
            this.ChangeFlag = true;
        }

        private bool Varidation()
        {
            if (textBox標令.Text.Length == 0)
            {
                MessageBox.Show("標令を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox支給額.Text.Length == 0)
            {
                MessageBox.Show("支給額を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                decimal d = decimal.Parse(textBox支給額.Text);
            }
            catch
            {
                MessageBox.Show("支給額を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                decimal d = decimal.Parse(textBox加算額.Text);
            }
            catch
            {
                MessageBox.Show("加算額を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool CheckDeleteUsing()
        {
            if (hyorei.IsNew())
                return true;

            return true;
        }
    }
}
