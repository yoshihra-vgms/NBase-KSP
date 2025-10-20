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
    public partial class 免許免状種別管理Form : Form
    {
        private Dictionary<DataRow, MsSiMenjouKind> menjouKindDic = new Dictionary<DataRow, MsSiMenjouKind>();
        private List<MsSiMenjou> menjous;


        public 免許免状種別管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox_免許免状名();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("免許／免状名", typeof(string)));
            dt.Columns.Add(new DataColumn("種別名", typeof(string)));
            dt.Columns.Add(new DataColumn("略称", typeof(string)));
            dt.Columns.Add(new DataColumn("表示順序", typeof(string)));

            dataGridView1.DataSource = dt;
            
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
        }


        private void InitComboBox_免許免状名()
        {
            menjous = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                menjous = serviceClient.MsSiMenjou_GetRecords(NBaseCommon.Common.LoginUser);
            }

            comboBox免許免状名.Items.Add(string.Empty);

            foreach (MsSiMenjou m in menjous)
            {
                comboBox免許免状名.Items.Add(m);
            }

            comboBox免許免状名.SelectedIndex = 0;
        }


        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }


        private void Search()
        {
            List<MsSiMenjouKind> menjouKinds = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                int msSiMenjouId = int.MinValue;

                if (comboBox免許免状名.SelectedIndex > 0)
                {
                    MsSiMenjou m = comboBox免許免状名.SelectedItem as MsSiMenjou;
                    msSiMenjouId = m.MsSiMenjouID;
                }

                menjouKinds = serviceClient.MsSiMenjouKind_SearchRecords(NBaseCommon.Common.LoginUser, msSiMenjouId, textBox種別名.Text.Trim(), textBox略称.Text.Trim());
            }

            SetRows(menjouKinds);
        }


        private void SetRows(List<MsSiMenjouKind> menjouKinds)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            menjouKindDic.Clear();

            foreach (MsSiMenjouKind m in menjouKinds)
            {
                DataRow row = dt.NewRow();

                row[0] = m.MenjouName;
                row[1] = m.Name;
                row[2] = m.NameAbbr;
                row[3] = m.ShowOrder;

                dt.Rows.Add(row);
                menjouKindDic[row] = m;
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            comboBox免許免状名.SelectedIndex = 0;
            textBox種別名.Text = string.Empty;
            textBox略称.Text = string.Empty;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            menjouKindDic.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            免許免状種別管理詳細Form form = new 免許免状種別管理詳細Form(menjous);

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
            MsSiMenjouKind m = menjouKindDic[view.Row];

            // 2014.02 2013年度改造
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                m.ExcludeMenjouKinds = serviceClient.MsSiExcludeMenjouKind_GetRecordsByMsSiMenjouKindID(NBaseCommon.Common.LoginUser, m.MsSiMenjouKindID);
            }

            免許免状種別管理詳細Form form = new 免許免状種別管理詳細Form(m, menjous);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }
    }
}
