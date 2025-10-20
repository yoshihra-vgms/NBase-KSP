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
    public partial class ActionCode管理Form : Form
    {
        private Dictionary<DataRow, MsActionCode> actionCodeDic = new Dictionary<DataRow, MsActionCode>();


        public ActionCode管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("ActionCode名", typeof(string)));
            dt.Columns.Add(new DataColumn("Actionテキスト", typeof(string)));

            dataGridView1.DataSource = dt;
            
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[1].ReadOnly = true;
        }


        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }


        private void Search()
        {
            List<MsActionCode> actionCodes = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                actionCodes = serviceClient.MsActionCode_SearchRecords(NBaseCommon.Common.LoginUser, textBox名.Text.Trim());
            }

            SetRows(actionCodes);
        }


        private void SetRows(List<MsActionCode> actionCodes)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            actionCodeDic.Clear();

            foreach (MsActionCode m in actionCodes)
            {
                DataRow row = dt.NewRow();

                row[0] = m.ActionCodeName;
                row[1] = m.ActionCodeText;

                dt.Rows.Add(row);
                actionCodeDic[row] = m;
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBox名.Text = string.Empty;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            actionCodeDic.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            ActionCode管理詳細Form form = new ActionCode管理詳細Form();

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
            MsActionCode m = actionCodeDic[view.Row];

            ActionCode管理詳細Form form = new ActionCode管理詳細Form(m);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }

    }
}
