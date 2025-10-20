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
    public partial class 給与計算複製作成Form : Form
    {
        private MsSiSalary prevSalary = null;
        private MsSiSalary newSalary = null;

        public 給与計算複製作成Form(MsSiSalary Salary)
        {
            this.prevSalary = Salary;

            InitializeComponent();
        }

        private void 給与計算複製作成Form_Load(object sender, EventArgs e)
        {
            textBoxKubun.Text = MsSiSalary.KindStr(prevSalary.Kind); // prevSalary.Kind == (int)MsSiSalary.KIND.航機通砲手 ? "フェリー" : "内航";
            textBoxFrom.Text = prevSalary.StartDate.ToShortDateString();
            if (prevSalary.EndDate == DateTime.MinValue)
            {
                textBoxTo.Text = "";
            }
            else
            {
                textBoxTo.Text = prevSalary.EndDate.ToShortDateString();
            }

            nullableDateTimePicker_from.Value = null;
            nullableDateTimePicker_to.Value = null;

            if (prevSalary.EndDate != DateTime.MinValue)
            {
                nullableDateTimePicker_from.Value = prevSalary.EndDate.AddDays(1);
            }
        }

        private void button閉じる_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Dispose();
        }

        private void button複製_Click(object sender, EventArgs e)
        {
            if (Varidation())
            {
                newSalary = new MsSiSalary();

                newSalary.Kind = prevSalary.Kind;
                newSalary.StartDate = (DateTime)nullableDateTimePicker_from.Value;
                if (nullableDateTimePicker_to.Value != null)
                {
                    newSalary.EndDate = (DateTime)nullableDateTimePicker_to.Value;
                }
                newSalary.PrevMsSiSalaryID = prevSalary.MsSiSalaryID;

                prevSalary.EndDate = newSalary.StartDate.AddDays(-1);

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

        private bool InsertOrUpdate()
        {
            bool result = false;

            this.Cursor = Cursors.WaitCursor;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.BLC_基本給複製(NBaseCommon.Common.LoginUser, prevSalary, newSalary);
            }
            this.Cursor = Cursors.Default;

            return result;
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

        private void nullableDateTimePicker_from_ValueChanged(object sender, EventArgs e)
        {
            if (nullableDateTimePicker_from.Value != null)
            {
                DateTime endDate = ((DateTime)nullableDateTimePicker_from.Value).AddDays(-1);
                textBoxTo.Text = endDate.ToShortDateString();
            }
            else
            {
                if (prevSalary.EndDate == DateTime.MinValue)
                {
                    textBoxTo.Text = "";
                }
                else
                {
                    textBoxTo.Text = prevSalary.EndDate.ToShortDateString();
                }
            }
        }
    }
}
