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
    public partial class 発生状況管理Form : Form
    {
        private Dictionary<DataRow, MsAccidentSituation> accidentSituationDic = new Dictionary<DataRow, MsAccidentSituation>();


        public 発生状況管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("発生状況名", typeof(string)));

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
            List<MsAccidentSituation> accidentSituations = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                accidentSituations = serviceClient.MsAccidentSituation_SearchRecords(NBaseCommon.Common.LoginUser, textBox名.Text.Trim());
            }

            SetRows(accidentSituations);
        }


        private void SetRows(List<MsAccidentSituation> accidentSituations)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            accidentSituationDic.Clear();

            foreach (MsAccidentSituation m in accidentSituations)
            {
                DataRow row = dt.NewRow();

                row[0] = m.AccidentSituationName;

                dt.Rows.Add(row);
                accidentSituationDic[row] = m;
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBox名.Text = string.Empty;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            accidentSituationDic.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            発生状況管理詳細Form form = new 発生状況管理詳細Form();

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
            MsAccidentSituation m = accidentSituationDic[view.Row];

            発生状況管理詳細Form form = new 発生状況管理詳細Form(m);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }
    }
}
