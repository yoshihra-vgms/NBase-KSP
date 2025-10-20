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
    public partial class 検査種別管理Form : Form
    {
        private Dictionary<DataRow, MsItemKind> itemKindDic = new Dictionary<DataRow, MsItemKind>();


        public 検査種別管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("検査種別名", typeof(string)));

            dataGridView1.DataSource = dt;
            
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[0].ReadOnly = true;
        }


        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }


        private void Search()
        {
            List<MsItemKind> itemKinds = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                itemKinds = serviceClient.MsItemKind_SearchRecords(NBaseCommon.Common.LoginUser, textBox名.Text.Trim());
            }

            SetRows(itemKinds);
        }


        private void SetRows(List<MsItemKind> itemKinds)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            itemKindDic.Clear();

            foreach (MsItemKind m in itemKinds)
            {
                DataRow row = dt.NewRow();

                row[0] = m.ItemKindName;

                dt.Rows.Add(row);
                itemKindDic[row] = m;
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBox名.Text = string.Empty;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            itemKindDic.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            検査種別管理詳細Form form = new 検査種別管理詳細Form();

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
            MsItemKind m = itemKindDic[view.Row];

            検査種別管理詳細Form form = new 検査種別管理詳細Form(m);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }
    }
}
