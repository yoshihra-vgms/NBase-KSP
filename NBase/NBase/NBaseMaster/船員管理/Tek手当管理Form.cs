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
    public partial class Tek手当管理Form : Form
    {
        public List<MsSiAllowance> allowanceList = null;
        public List<NBaseData.DAC.MsVessel> vesselList = null;
        public List<MsSiShokumei> shokumeiList = null;

        public Tek手当管理Form()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                allowanceList = serviceClient.MsSiAllowance_GetRecords(NBaseCommon.Common.LoginUser);

                vesselList = serviceClient.MsVessel_GetRecords(NBaseCommon.Common.LoginUser);
                shokumeiList = serviceClient.MsSiShokumei_GetRecords(NBaseCommon.Common.LoginUser);
            }

            textBox手当名.Text = null;

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("obj", typeof(MsSiAllowance)));
            dt.Columns.Add(new DataColumn("手当名", typeof(string)));
            dt.Columns.Add(new DataColumn("作業内容", typeof(string)));
            dt.Columns.Add(new DataColumn("部署", typeof(string)));
            dt.Columns.Add(new DataColumn("金額", typeof(string)));

            dataGridView1.DataSource = dt;
            
            dataGridView1.Columns[0].Visible = false;

            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[3].Width = 100;

            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
        }


        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }


        private void Search()
        {
            List<MsSiAllowance> allowance = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                allowance = serviceClient.MsSiAllowance_SearchRecords(NBaseCommon.Common.LoginUser, textBox手当名.Text);
            }

            SetRows(allowance);
        }


        private void SetRows(List<MsSiAllowance> allowance)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();

            foreach (MsSiAllowance o in allowance)
            {
                DataRow row = dt.NewRow();

                row[0] = o;
                row[1] = o.Name;
                row[2] = o.Contents;
                row[3] = o.Department == 0 ? "全員" : o.Department == 1 ? "甲板部" : "機関部";
                row[4] = NBaseCommon.Common.金額出力(o.Allowance);

                dt.Rows.Add(row);
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBox手当名.Text = null;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            Tek手当管理詳細Form form = new Tek手当管理詳細Form(allowanceList, vesselList, shokumeiList);

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
            MsSiAllowance allowance = dataGridView1.SelectedRows[0].Cells[0].Value as MsSiAllowance;

            Tek手当管理詳細Form form = new Tek手当管理詳細Form(allowanceList, vesselList, shokumeiList, allowance);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }
    }
}
