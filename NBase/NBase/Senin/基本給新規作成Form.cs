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
    public partial class 基本給新規作成Form : Form
    {
        private 船員詳細Panel parentForm;
        private SiSalary salary = null;

        public 基本給新規作成Form(船員詳細Panel parentForm)
            : this(parentForm, new SiSalary())
        {
        }

        public 基本給新規作成Form(船員詳細Panel parentForm, SiSalary salary)
        {
            this.parentForm = parentForm;
            this.salary = salary;

            InitializeComponent();

            radioButton航機通砲手.Checked = true;
            nullableDateTimePicker_from.Value = null;
            nullableDateTimePicker_to.Value = null;


            Init期間();

        }
        private void Init期間()
        {
            nullableDateTimePicker_from.Value = DateTimeUtils.年度開始日();
            nullableDateTimePicker_to.Value = DateTimeUtils.年度終了日();
        }

        private void button新規_Click(object sender, EventArgs e)
        {
            if (Varidation())
            {
                FillInstance();

                MsSiSalary mst = null;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    mst = serviceClient.BLC_基本給検索(NBaseCommon.Common.LoginUser, salary.Kind, salary.StartDate, salary.EndDate);
                }
                if (mst == null || mst.IsNew())
                {
                    MessageBox.Show("マスタが整備されていません。（基本給マスタ）", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Dispose();


                基本給詳細Form form = new 基本給詳細Form(this.parentForm, salary, mst);
                DialogResult = form.ShowDialog();
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

        private void FillInstance()
        {
            salary.Kind = radioButton航機通砲手.Checked ? 0 : radioButton航機通砲手.Checked ? 1 : 2;
            salary.StartDate = (DateTime)nullableDateTimePicker_from.Value;
            if (nullableDateTimePicker_to.Value != null)
            {
                salary.EndDate = (DateTime)nullableDateTimePicker_to.Value;
            }

        }
    }
}
