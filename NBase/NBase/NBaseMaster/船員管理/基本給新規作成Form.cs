using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;

namespace NBaseMaster.船員管理
{
    public partial class 基本給新規作成Form : Form
    {
        private MsSiSalary salary = null;

        public 基本給新規作成Form(MsSiSalary salary)
        {
            InitializeComponent();

            this.salary = salary;
            radioButton航機通砲手.Checked = true;
            nullableDateTimePicker_from.Value = null;
            nullableDateTimePicker_to.Value = null;

        }

        private void button新規_Click(object sender, EventArgs e)
        {
            if (Varidation())
            {
                salary.Kind = radioButton航機通砲手.Checked ? 0 : radioButton下級海技士.Checked ? 1 : 2;
                salary.StartDate = (DateTime)nullableDateTimePicker_from.Value;
                if (nullableDateTimePicker_to.Value != null)
                {
                    salary.EndDate = (DateTime)nullableDateTimePicker_to.Value;
                }

                DialogResult = DialogResult.OK;
            }

        }

        private void button閉じる_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Dispose();            
        }

        private bool Varidation()
        {
            if (nullableDateTimePicker_from.Value == null)
            {
                MessageBox.Show("期間（開始日）を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
