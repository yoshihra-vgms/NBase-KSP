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
    public partial class ViqNo管理Form : Form
    {
        private Dictionary<DataRow, MsViqNo> viqCodeDic = new Dictionary<DataRow, MsViqNo>();
        private List<MsViqCode> viqCodes;

        public ViqNo管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox_Code();


            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("VIQ Code", typeof(string)));
            dt.Columns.Add(new DataColumn("VIQ No", typeof(string)));
            dt.Columns.Add(new DataColumn("Code説明", typeof(string)));
            dt.Columns.Add(new DataColumn("Code説明(英語)", typeof(string)));
            dt.Columns.Add(new DataColumn("表示順", typeof(int)));

            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].Width = 55;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].Width = 350;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].Width = 65;
            dataGridView1.Columns[4].ReadOnly = true;
        }

        private void InitComboBox_Code()
        {
            viqCodes = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                viqCodes = serviceClient.MsViqCode_GetRecords(NBaseCommon.Common.LoginUser);
            }

            comboBoxCode.Items.Add(string.Empty);

            foreach (MsViqCode o in viqCodes)
            {
                comboBoxCode.Items.Add(o);
            }

            comboBoxCode.SelectedIndex = 0;
        }

        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }


        private void Search()
        {
            List<MsViqNo> viqNos = null;

            int codeNameId = -1;
            if (comboBoxCode.SelectedItem is MsViqCode)
            {
                codeNameId = (comboBoxCode.SelectedItem as MsViqCode).ViqCodeID;
            }
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                viqNos = serviceClient.MsViqNo_SearchRecords(NBaseCommon.Common.LoginUser, codeNameId, textBoxDefectiveItem.Text.Trim());
            }

            SetRows(viqNos);
        }


        private void SetRows(List<MsViqNo> viqNos)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            viqCodeDic.Clear();

            foreach (MsViqNo m in viqNos)
            {
                DataRow row = dt.NewRow();

                string viqCodeName = "";
                if (viqCodes.Any(obj => obj.ViqCodeID == m.ViqCodeID))
                {
                    viqCodeName = viqCodes.Where(obj => obj.ViqCodeID == m.ViqCodeID).First().ViqCode;
                }
                row[0] = viqCodeName;
                row[1] = m.ViqNo;
                row[2] = m.Description;
                row[3] = m.DescriptionEng;
                row[4] = m.OrderNo;

                dt.Rows.Add(row);
                viqCodeDic[row] = m;
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBoxDefectiveItem.Text = string.Empty;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            viqCodeDic.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            ViqNo管理詳細Form form = new ViqNo管理詳細Form(viqCodes);

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
            MsViqNo m = viqCodeDic[view.Row];

            ViqNo管理詳細Form form = new ViqNo管理詳細Form(viqCodes, m);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }
    }
}
