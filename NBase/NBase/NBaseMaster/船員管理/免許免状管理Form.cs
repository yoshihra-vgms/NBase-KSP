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
    public partial class 免許免状管理Form : Form
    {
        private Dictionary<DataRow, MsSiMenjou> menjouDic = new Dictionary<DataRow, MsSiMenjou>();


        public 免許免状管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("免許／免状名", typeof(string)));
            dt.Columns.Add(new DataColumn("略称", typeof(string)));
            dt.Columns.Add(new DataColumn("表示順序", typeof(string)));

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
            List<MsSiMenjou> menjous = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                menjous = serviceClient.MsSiMenjou_SearchRecords(NBaseCommon.Common.LoginUser, textBox名.Text.Trim(), textBox略称.Text.Trim());
            }

            SetRows(menjous);
        }


        private void SetRows(List<MsSiMenjou> menjous)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            menjouDic.Clear();

            foreach (MsSiMenjou m in menjous)
            {
                DataRow row = dt.NewRow();

                row[0] = m.Name;
                row[1] = m.NameAbbr;
                row[2] = m.ShowOrder;

                dt.Rows.Add(row);
                menjouDic[row] = m;
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBox名.Text = string.Empty;
            textBox略称.Text = string.Empty;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            menjouDic.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            免許免状管理詳細Form form = new 免許免状管理詳細Form();

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
            MsSiMenjou m = menjouDic[view.Row];

            免許免状管理詳細Form form = new 免許免状管理詳細Form(m);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }
    }
}
