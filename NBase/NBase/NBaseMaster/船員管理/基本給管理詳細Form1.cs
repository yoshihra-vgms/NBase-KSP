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

namespace NBaseMaster.船員管理
{
    public partial class 基本給管理詳細Form1 : Form
    {
        private Dictionary<DataRow, MsSiSalaryHyorei> salaryHyoreiDic = new Dictionary<DataRow, MsSiSalaryHyorei>();
        private Dictionary<DataRow, MsSiSalaryRank> salaryRankDic = new Dictionary<DataRow, MsSiSalaryRank>();


        public 基本給管理詳細Form1()
        {
            InitializeComponent();
        }

        private void 基本給管理詳細Form1_Load(object sender, EventArgs e)
        {
            label区分.Text = MsSiSalary.KindStr(基本給管理Common.Salary.Kind); 
            nullableDateTimePicker_from.Value = 基本給管理Common.Salary.StartDate;
            if (基本給管理Common.Salary.EndDate == DateTime.MinValue)
            {
                nullableDateTimePicker_to.Value = null;
            }
            else
            {
                nullableDateTimePicker_to.Value = 基本給管理Common.Salary.EndDate;
            }

            DataTable dt標令 = new DataTable();

            dt標令.Columns.Add(new DataColumn("標令", typeof(string)));
            dt標令.Columns.Add(new DataColumn("支給額", typeof(string)));
            dt標令.Columns.Add(new DataColumn("加算額", typeof(string)));

            dataGridView標令.DataSource = dt標令;

            dataGridView標令.Columns[0].Width = 90;
            dataGridView標令.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView標令.Columns[1].Width = 90;
            dataGridView標令.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView標令.Columns[2].Width = 90;
            dataGridView標令.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView標令.Columns[0].ReadOnly = true;
            dataGridView標令.Columns[1].ReadOnly = true;
            dataGridView標令.Columns[2].ReadOnly = true;

            標令一覧表示();


            TabPage targetTab = tabControl1.TabPages[1];
            tabControl1.TabPages.Remove(targetTab);

            //DataTable dt職務 = new DataTable();

            //dt職務.Columns.Add(new DataColumn("職務", typeof(string)));
            //dt職務.Columns.Add(new DataColumn("支給額", typeof(string)));

            //dataGridView職務.DataSource = dt職務;

            //dataGridView職務.Columns[0].Width = 100;
            //dataGridView職務.Columns[1].Width = 100;

            //dataGridView職務.Columns[0].ReadOnly = true;
            //dataGridView職務.Columns[1].ReadOnly = true;
            //dataGridView職務.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //職務一覧表示();
        }


        private void button更新_Click(object sender, EventArgs e)
        {
            Save();
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

        private void Save()
        {
            if (Varidation())
            {
                基本給管理Common.Salary.StartDate = DateTimeUtils.ToFrom((DateTime)nullableDateTimePicker_from.Value);
                if (nullableDateTimePicker_to.Value != null)
                {
                    基本給管理Common.Salary.EndDate = DateTimeUtils.ToFrom((DateTime)nullableDateTimePicker_to.Value);
                }
                else
                {
                    基本給管理Common.Salary.EndDate = DateTime.MinValue;
                }

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
                int msSiKyuyoId = serviceClient.BLC_基本給登録(NBaseCommon.Common.LoginUser, 基本給管理Common.Salary);
                if (msSiKyuyoId > 0)
                {
                    result = true;
                }
            }
            this.Cursor = Cursors.Default;

            return result;
        }

        private void button閉じる_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Dispose();
        }

        private void button追加_標令_Click(object sender, EventArgs e)
        {
            MsSiSalaryHyorei salaryHyorei = new MsSiSalaryHyorei();
            標令給Form form = new 標令給Form(salaryHyorei);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (基本給管理Common.Salary.SalaryHyoreiList == null)
                {
                    基本給管理Common.Salary.SalaryHyoreiList = new List<MsSiSalaryHyorei>();
                }
                基本給管理Common.Salary.SalaryHyoreiList.Add(salaryHyorei);

                標令一覧表示();
            }
        }

        private void dataGridView標令_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRowView view = dataGridView標令.SelectedRows[0].DataBoundItem as DataRowView;
            MsSiSalaryHyorei salaryHyorei = salaryHyoreiDic[view.Row];

            標令給Form form = new 標令給Form(salaryHyorei);
            if (form.ShowDialog() == DialogResult.OK)
            {
                salaryHyoreiDic[view.Row] = salaryHyorei;

                標令一覧表示();
            }
        }

        private void 標令一覧表示()
        {
            DataTable dt標令 = dataGridView標令.DataSource as DataTable;
            dt標令.Clear();
            salaryHyoreiDic.Clear();
            if (基本給管理Common.Salary.SalaryHyoreiList != null)
            {
                var sortedList = 基本給管理Common.Salary.SalaryHyoreiList.Where(obj => obj.DeleteFlag == 0).OrderBy(obj => obj.Hyorei);
                foreach (MsSiSalaryHyorei o in sortedList)
                {
                    DataRow row = dt標令.NewRow();

                    row[0] = o.Hyorei;
                    row[1] = 基本給管理Common.金額出力(o.Allowance);
                    row[2] = 基本給管理Common.金額出力(o.AdditionalAmount);

                    dt標令.Rows.Add(row);
                    salaryHyoreiDic[row] = o;
                }
            }
        }


        private void button追加_職務_Click(object sender, EventArgs e)
        {
            MsSiSalaryRank rank = new MsSiSalaryRank();
            職務給Form1 form = new 職務給Form1(rank);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (基本給管理Common.Salary.SalaryRankList == null)
                {
                    基本給管理Common.Salary.SalaryRankList = new List<MsSiSalaryRank>();
                }
                基本給管理Common.Salary.SalaryRankList.Add(rank);

                職務一覧表示();
            }
        }

        private void dataGridView職務_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRowView view = dataGridView職務.SelectedRows[0].DataBoundItem as DataRowView;
            MsSiSalaryRank salaryRank = salaryRankDic[view.Row];

            職務給Form1 form = new 職務給Form1(salaryRank);
            if (form.ShowDialog() == DialogResult.OK)
            {
                salaryRankDic[view.Row] = salaryRank;

                職務一覧表示();
            }
        }

        private void 職務一覧表示()
        {
            DataTable dt職務 = dataGridView職務.DataSource as DataTable;
            dt職務.Clear();
            salaryRankDic.Clear();
            if (基本給管理Common.Salary.SalaryRankList != null)
            {
                var sortedList = from l in 基本給管理Common.Salary.SalaryRankList
                                 orderby l.MsSiShokumeiSalaryId
                                 select l;
                foreach (MsSiSalaryRank o in sortedList)
                {
                    if (o.DeleteFlag == 1)
                        continue;

                    DataRow row = dt職務.NewRow();

                    //row[0] = SeninTableCache.instance().GetMsSiShokumeiSalaryName(NBaseCommon.Common.LoginUser, o.MsSiShokumeiSalaryId);
                    row[1] = 基本給管理Common.金額出力(o.Allowance0);

                    dt職務.Rows.Add(row);
                    salaryRankDic[row] = o;
                }
            }
        }
    }
}
