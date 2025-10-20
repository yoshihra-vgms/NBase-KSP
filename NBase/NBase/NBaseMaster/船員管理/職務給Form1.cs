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
    public partial class 職務給Form1 : Form
    {
        //データを編集したかどうか？
        private bool ChangeFlag = false;

        private MsSiSalaryRank rank;

        public 職務給Form1(MsSiSalaryRank rank)
        {
            InitializeComponent();
            this.rank = rank;
        }

        private void 職務Form_Load(object sender, EventArgs e)
        {
            InitComboBox職名();

            //comboBox職名.SelectedItem = SeninTableCache.instance().GetMsSiShokumeiSalary(NBaseCommon.Common.LoginUser, rank.MsSiShokumeiSalaryId);
            textBox支給額.Text = NBaseUtil.NumberUtils.ToString(rank.Allowance0);

            this.ChangeFlag = false;
        }

        #region private void InitComboBox職名()
        private void InitComboBox職名()
        {
            //foreach (MsSiShokumeiSalary s in SeninTableCache.instance().GetMsSiShokumeiSalaryList(NBaseCommon.Common.LoginUser))
            //{
            //    comboBox職名.Items.Add(s);
            //}
        }
        #endregion

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
                //MsSiShokumeiSalary s = (comboBox職名.SelectedItem as MsSiShokumeiSalary);
                //rank.MsSiShokumeiSalaryId = s.MsSiShokumeiSalaryID;
                rank.Allowance0 = NBaseUtil.StringUtils.ToDecimal(textBox支給額.Text);

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

                rank.DeleteFlag = 1;

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
            if (comboBox職名.SelectedItem == null)
            {
                MessageBox.Show("職務を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            return true;
        }

        private bool CheckDeleteUsing()
        {
            if (rank.IsNew())
                return true;

            return true;
        }
    }
}
