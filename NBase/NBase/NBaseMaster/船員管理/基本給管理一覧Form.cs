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
    public partial class 基本給管理一覧Form : Form
    {
        private Dictionary<DataRow, MsSiSalary> salaryDic = new Dictionary<DataRow, MsSiSalary>();

        public 基本給管理一覧Form()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("区分", typeof(string)));
            dt.Columns.Add(new DataColumn("開始日", typeof(string)));
            dt.Columns.Add(new DataColumn("終了日", typeof(string)));

            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 200;

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
        }

        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            List<MsSiSalary> salaries = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                salaries = serviceClient.MsSiSalary_SearchRecords(NBaseCommon.Common.LoginUser, checkBox航機通砲手.Checked, checkBox海技士.Checked, checkBox部員.Checked);
            }

            SetRows(salaries);
        }

        private void SetRows(List<MsSiSalary> salaries)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            salaryDic.Clear();

            foreach (MsSiSalary k in salaries)
            {
                DataRow row = dt.NewRow();

                row[0] = MsSiSalary.KindStr(k.Kind);
                row[1] = k.StartDate.ToShortDateString();
                if (k.EndDate != DateTime.MinValue)
                {
                    row[2] = k.EndDate.ToShortDateString();
                }
                else
                {
                    row[2] = "";
                }

                dt.Rows.Add(row);
                salaryDic[row] = k;
            }
        }

        private void button新規_Click(object sender, EventArgs e)
        {
            MsSiSalary salary = new MsSiSalary();

            // 区分および期間設定画面
            基本給新規作成Form form1 = new 基本給新規作成Form(salary);
            if (form1.ShowDialog() == DialogResult.Cancel)
                return;

            基本給管理Common.Salary = salary;
            基本給管理Common.PrevSalary = null;

            // タリフ画面
            基本給管理詳細Form1 form2 = new 基本給管理詳細Form1();
            if (form2.ShowDialog() == DialogResult.Cancel)
                return;

            Search();
        }

        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRowView view = dataGridView1.SelectedRows[0].DataBoundItem as DataRowView;
            MsSiSalary salary = salaryDic[view.Row];
            MsSiSalary prevsalary = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                salary = serviceClient.BLC_基本給取得(NBaseCommon.Common.LoginUser, salary.MsSiSalaryID); 

                if (salary.PrevMsSiSalaryID > 0)
                {
                    prevsalary = serviceClient.BLC_基本給取得(NBaseCommon.Common.LoginUser, salary.PrevMsSiSalaryID);
                }
            }
            基本給管理Common.Salary = salary;
            基本給管理Common.PrevSalary = prevsalary;

            基本給管理詳細Form1 form2 = new 基本給管理詳細Form1();
            if (form2.ShowDialog() == DialogResult.Cancel)
                return;

            Search();
        }

        private void button複製_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            DataRowView view = dataGridView1.SelectedRows[0].DataBoundItem as DataRowView;
            MsSiSalary salary = salaryDic[view.Row];

            給与計算複製作成Form form = new 給与計算複製作成Form(salary);
            if (form.ShowDialog() == DialogResult.Cancel)
                return;

            Search();
        }
    }
}
