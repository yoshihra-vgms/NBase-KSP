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
    public partial class 講習管理Form : Form
    {
        private Dictionary<DataRow, MsSiKoushu> koushuDic = new Dictionary<DataRow, MsSiKoushu>();


        public 講習管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("講習名", typeof(string)));
            dt.Columns.Add(new DataColumn("有効期限", typeof(string)));
            dt.Columns.Add(new DataColumn("有効期限(日数)", typeof(string)));

            dataGridView1.DataSource = dt;
            
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[2].Width = 80;
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
            List<MsSiKoushu> koushus = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                koushus = serviceClient.MsSiKoushu_SearchRecords(NBaseCommon.Common.LoginUser, textBox名.Text.Trim());
            }

            SetRows(koushus);
        }


        private void SetRows(List<MsSiKoushu> koushus)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            koushuDic.Clear();

            foreach (MsSiKoushu m in koushus)
            {
                DataRow row = dt.NewRow();

                row[0] = m.Name;
                row[1] = m.YukokigenStr;
                row[2] = m.YukokigenDays.ToString();

                dt.Rows.Add(row);
                koushuDic[row] = m;
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBox名.Text = string.Empty;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            koushuDic.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            講習管理詳細Form form = new 講習管理詳細Form();

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRowView view = dataGridView1.SelectedRows[0].DataBoundItem as DataRowView;
            MsSiKoushu k = koushuDic[view.Row];

            講習管理詳細Form form = new 講習管理詳細Form(k);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }
    }
}
