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
    public partial class ViqCodeName管理Form : Form
    {
        private Dictionary<DataRow, MsViqCodeName> viqCodeNameDic = new Dictionary<DataRow, MsViqCodeName>();

        public ViqCodeName管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("名前", typeof(string)));
            dt.Columns.Add(new DataColumn("Code説明", typeof(string)));
            dt.Columns.Add(new DataColumn("Code説明(英語)", typeof(string)));
            dt.Columns.Add(new DataColumn("表示順", typeof(int)));

            dataGridView1.DataSource = dt;
            
            dataGridView1.Columns[0].Width = 55;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].Width = 65;
            dataGridView1.Columns[3].ReadOnly = true;
        }

        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }


        private void Search()
        {
            List<MsViqCodeName> viqCodeNames = null;


            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                viqCodeNames = serviceClient.MsViqCodeName_SearchRecords(NBaseCommon.Common.LoginUser, textBoxDefectiveItem.Text.Trim());
            }

            SetRows(viqCodeNames);
        }


        private void SetRows(List<MsViqCodeName> viqCodeNames)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            viqCodeNameDic.Clear();

            foreach (MsViqCodeName m in viqCodeNames)
            {
                DataRow row = dt.NewRow();

                row[0] = m.ViqCodeName;
                row[1] = m.Description;
                row[2] = m.DescriptionEng;
                row[3] = m.OrderNo;

                dt.Rows.Add(row);
                viqCodeNameDic[row] = m;
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBoxDefectiveItem.Text = string.Empty;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            viqCodeNameDic.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            ViqCodeName管理詳細Form form = new ViqCodeName管理詳細Form();

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
            MsViqCodeName m = viqCodeNameDic[view.Row];

            ViqCodeName管理詳細Form form = new ViqCodeName管理詳細Form(m);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }
    }
}
