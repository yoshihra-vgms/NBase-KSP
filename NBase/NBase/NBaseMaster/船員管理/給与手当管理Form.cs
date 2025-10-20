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
    public partial class 給与手当管理Form : Form
    {
        public List<MsSiKyuyoTeate> kyuyoTeateList = null;
        public List<MsSiShokumei> shokumeiList = null;

        public 給与手当管理Form()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            InitComboBox給与手当();
            InitComboBox職名();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("給与手当", typeof(string)));
            dt.Columns.Add(new DataColumn("職名", typeof(string)));
            dt.Columns.Add(new DataColumn("単価", typeof(string)));
            dt.Columns.Add(new DataColumn("obj", typeof(MsSiKyuyoTeateSet)));

            dataGridView1.DataSource = dt;
            
            dataGridView1.Columns[0].Width = 250;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].Visible = false;
        }


        private void InitComboBox給与手当()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                kyuyoTeateList = serviceClient.MsSiKyuyoTeate_GetRecords(NBaseCommon.Common.LoginUser);
            }

            comboBox給与手当.Items.Add(string.Empty);
            foreach (MsSiKyuyoTeate m in kyuyoTeateList)
            {
                comboBox給与手当.Items.Add(m);
            }

            comboBox給与手当.SelectedIndex = 0;
        }

        private void InitComboBox職名()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                shokumeiList = serviceClient.MsSiShokumei_GetRecords(NBaseCommon.Common.LoginUser);
            }

            comboBox職名.Items.Add(string.Empty);
            foreach (MsSiShokumei s in shokumeiList)
            {
                comboBox職名.Items.Add(s);
            }

            comboBox職名.SelectedIndex = 0;
        }

        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }


        private void Search()
        {
            List<MsSiKyuyoTeateSet> kyuyoTeateSet = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                int kyuyoTeateId = -1;
                int shokumeiId = -1;

                if (comboBox給与手当.SelectedItem is MsSiKyuyoTeate)
                {
                    kyuyoTeateId = (comboBox給与手当.SelectedItem as MsSiKyuyoTeate).MsSiKyuyoTeateID;
                }
                if (comboBox職名.SelectedItem is MsSiShokumei)
                {
                    shokumeiId = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
                }

                kyuyoTeateSet = serviceClient.MsSiKyuyoTeateSet_SearchRecords(NBaseCommon.Common.LoginUser, kyuyoTeateId, shokumeiId);
            }

            SetRows(kyuyoTeateSet);
        }


        private void SetRows(List<MsSiKyuyoTeateSet> kyuyoTeateSet)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();

            foreach (MsSiKyuyoTeateSet o in kyuyoTeateSet)
            {
                DataRow row = dt.NewRow();

                row[0] = kyuyoTeateList.Where(obj => obj.MsSiKyuyoTeateID == o.MsSiKyuyoTeateID).First().Name;
                row[1] = shokumeiList.Where(obj => obj.MsSiShokumeiID == o.MsSiShokumeiID).First().Name;
                row[2] = o.Tanka;
                row[3] = o;

                dt.Rows.Add(row);
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            comboBox給与手当.SelectedIndex = 0;
            comboBox職名.SelectedIndex = 0;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            給与手当管理詳細Form form = new 給与手当管理詳細Form(kyuyoTeateList, shokumeiList);

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
            MsSiKyuyoTeateSet kyuuyoTeateSet = dataGridView1.SelectedRows[0].Cells[3].Value as MsSiKyuyoTeateSet;

            給与手当管理詳細Form form = new 給与手当管理詳細Form(kyuyoTeateList, shokumeiList, kyuuyoTeateSet);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }
    }
}
