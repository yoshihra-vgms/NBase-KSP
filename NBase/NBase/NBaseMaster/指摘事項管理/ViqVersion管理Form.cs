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
    public partial class ViqVersion管理Form : Form
    {
        private Dictionary<DataRow, MsViqVersion> viqVersionDic = new Dictionary<DataRow, MsViqVersion>();

        public ViqVersion管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Version", typeof(string)));
            dt.Columns.Add(new DataColumn("開始日", typeof(string)));
            dt.Columns.Add(new DataColumn("終了日", typeof(string)));

            dataGridView1.DataSource = dt;
            
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[2].ReadOnly = true;

            Search();
        }

        //private void button検索_Click(object sender, EventArgs e)
        //{
        //    Search();
        //}

        private void Search()
        {
            List<MsViqVersion> viqVersions = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                viqVersions = serviceClient.MsViqVersion_GetRecords(NBaseCommon.Common.LoginUser);
            }

            SetRows(viqVersions);
        }

        private void SetRows(List<MsViqVersion> viqVersions)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            viqVersionDic.Clear();

            foreach (MsViqVersion m in viqVersions)
            {
                DataRow row = dt.NewRow();

                row[0] = m.ViqVersion;
                row[1] = m.StartDate != DateTime.MinValue ? m.StartDate.ToShortDateString() : "";
                row[2] = m.EndDate != DateTime.MaxValue ? m.EndDate.ToShortDateString() : "";

                dt.Rows.Add(row);
                viqVersionDic[row] = m;
            }
        }

        //private void buttonクリア_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = dataGridView1.DataSource as DataTable;
        //    dt.Clear();
        //    viqVersionDic.Clear();
        //}


        private void button新規_Click(object sender, EventArgs e)
        {
            ViqVersion管理詳細Form form = new ViqVersion管理詳細Form();

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
            MsViqVersion m = viqVersionDic[view.Row];

            ViqVersion管理詳細Form form = new ViqVersion管理詳細Form(m);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }
    }
}
