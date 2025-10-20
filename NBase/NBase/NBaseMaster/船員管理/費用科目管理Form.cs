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
    public partial class 費用科目管理Form : Form
    {
        private Dictionary<DataRow, MsSiHiyouKamoku> hiyouKamokuDic = new Dictionary<DataRow, MsSiHiyouKamoku>();


        public 費用科目管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("費用科目名", typeof(string)));

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
            List<MsSiHiyouKamoku> hiyouKamokus = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                hiyouKamokus = serviceClient.MsSiHiyouKamoku_SearchRecords(NBaseCommon.Common.LoginUser, textBox名.Text.Trim());
            }

            SetRows(hiyouKamokus);
        }


        private void SetRows(List<MsSiHiyouKamoku> hiyouKamokus)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            hiyouKamokuDic.Clear();

            foreach (MsSiHiyouKamoku m in hiyouKamokus)
            {
                DataRow row = dt.NewRow();

                row[0] = m.Name;

                dt.Rows.Add(row);
                hiyouKamokuDic[row] = m;
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBox名.Text = string.Empty;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            hiyouKamokuDic.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            費用科目管理詳細Form form = new 費用科目管理詳細Form();

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
            MsSiHiyouKamoku m = hiyouKamokuDic[view.Row];

            費用科目管理詳細Form form = new 費用科目管理詳細Form(m);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }
    }
}
