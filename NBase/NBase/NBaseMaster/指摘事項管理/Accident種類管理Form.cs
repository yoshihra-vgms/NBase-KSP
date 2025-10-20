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
    public partial class Accident種類管理Form : Form
    {
        private Dictionary<DataRow, MsAccidentKind> accidentKindDic = new Dictionary<DataRow, MsAccidentKind>();


        public Accident種類管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Accident種類名", typeof(string)));

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
            List<MsAccidentKind> accidentKinds = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                accidentKinds = serviceClient.MsAccidentKind_SearchRecords(NBaseCommon.Common.LoginUser, textBox名.Text.Trim());
            }

            SetRows(accidentKinds);
        }


        private void SetRows(List<MsAccidentKind> accidentKinds)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            accidentKindDic.Clear();

            foreach (MsAccidentKind m in accidentKinds)
            {
                DataRow row = dt.NewRow();

                row[0] = m.AccidentKindName;

                dt.Rows.Add(row);
                accidentKindDic[row] = m;
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBox名.Text = string.Empty;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            accidentKindDic.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            Accident種類管理詳細Form form = new Accident種類管理詳細Form();

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
            MsAccidentKind m = accidentKindDic[view.Row];

            Accident種類管理詳細Form form = new Accident種類管理詳細Form(m);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }
    }
}
