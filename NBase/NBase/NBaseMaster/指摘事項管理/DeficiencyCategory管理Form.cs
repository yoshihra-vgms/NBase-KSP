using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;

namespace NBaseMaster.指摘事項管理
{
    public partial class DeficiencyCategory管理Form : Form
    {
        private Dictionary<DataRow, MsDeficiencyCategory> deficiencyCategoryDic = new Dictionary<DataRow, MsDeficiencyCategory>();


        public DeficiencyCategory管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("名前", typeof(string)));

            dataGridView1.DataSource = dt;
            
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[1].ReadOnly = true;
        }


        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }


        private void Search()
        {
            List<MsDeficiencyCategory> deficiencyCategorys = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                deficiencyCategorys = serviceClient.MsDeficiencyCategory_SearchRecords(NBaseCommon.Common.LoginUser, textBox名.Text.Trim());
            }

            SetRows(deficiencyCategorys);
        }


        private void SetRows(List<MsDeficiencyCategory> deficiencyCategorys)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            deficiencyCategoryDic.Clear();

            foreach (MsDeficiencyCategory m in deficiencyCategorys)
            {
                DataRow row = dt.NewRow();

                row[0] = m.DeficiencyCategoryNo;
                row[1] = m.DeficiencyCategoryName;

                dt.Rows.Add(row);
                deficiencyCategoryDic[row] = m;
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBox名.Text = string.Empty;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            deficiencyCategoryDic.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            DeficiencyCategory管理詳細Form form = new DeficiencyCategory管理詳細Form();

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
            MsDeficiencyCategory m = deficiencyCategoryDic[view.Row];

            DeficiencyCategory管理詳細Form form = new DeficiencyCategory管理詳細Form(m);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }

    }
}
