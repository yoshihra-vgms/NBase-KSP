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
    public partial class ViqCode管理Form : Form
    {
        private Dictionary<DataRow, MsViqCode> viqCodeDic = new Dictionary<DataRow, MsViqCode>();
        private List<MsViqCodeName> viqCodeNames;
        private List<MsViqVersion> viqVersions;

        public ViqCode管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            viqVersions = null;
            viqCodeNames = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                viqVersions = serviceClient.MsViqVersion_GetRecords(NBaseCommon.Common.LoginUser);
                viqCodeNames = serviceClient.MsViqCodeName_GetRecords(NBaseCommon.Common.LoginUser);
            }

            InitComboBox_Version();
            InitComboBox_CodeName();


            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("VIQ Version", typeof(string)));
            dt.Columns.Add(new DataColumn("VIQ Code名前", typeof(string)));
            dt.Columns.Add(new DataColumn("VIQ Code", typeof(string)));
            dt.Columns.Add(new DataColumn("Code説明", typeof(string)));
            dt.Columns.Add(new DataColumn("Code説明(英語)", typeof(string)));
            dt.Columns.Add(new DataColumn("表示順", typeof(int)));

            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].Width = 110;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].Width = 55;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].Width = 250;
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[5].Width = 65;
            dataGridView1.Columns[5].ReadOnly = true;
        }

        private void InitComboBox_Version()
        {
            comboBoxVersion.Items.Add(string.Empty);

            foreach (MsViqVersion o in viqVersions)
            {
                comboBoxVersion.Items.Add(o);
            }

            comboBoxVersion.SelectedIndex = 0;
        }

        private void InitComboBox_CodeName()
        {
            comboBoxCodeName.Items.Add(string.Empty);

            foreach (MsViqCodeName o in viqCodeNames)
            {
                comboBoxCodeName.Items.Add(o);
            }

            comboBoxCodeName.SelectedIndex = 0;
        }

        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }


        private void Search()
        {
            List<MsViqCode> viqCodes = null;

            int codeNameId = -1;
            if (comboBoxCodeName.SelectedItem is MsViqCodeName)
            {
                codeNameId = (comboBoxCodeName.SelectedItem as MsViqCodeName).ViqCodeNameID;
            }
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                viqCodes = serviceClient.MsViqCode_SearchRecords(NBaseCommon.Common.LoginUser, codeNameId, textBoxDefectiveItem.Text.Trim());
            }

            if (comboBoxVersion.SelectedItem is MsViqVersion)
            {
                int versionId = (comboBoxVersion.SelectedItem as MsViqVersion).ViqVersionID;

                viqCodes = viqCodes.Where(obj => obj.ViqVersionID == versionId).ToList();
            }
            SetRows(viqCodes);
        }


        private void SetRows(List<MsViqCode> viqCodes)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            viqCodeDic.Clear();

            foreach (MsViqCode m in viqCodes)
            {
                DataRow row = dt.NewRow();

                string viqVersion = "";
                if (viqVersions.Any(obj => obj.ViqVersionID == m.ViqVersionID))
                {
                    viqVersion = viqVersions.Where(obj => obj.ViqVersionID == m.ViqVersionID).First().ViqVersion;
                } 
                row[0] = viqVersion;
                
                string viqCodeName = "";
                if (viqCodeNames.Any(obj => obj.ViqCodeNameID == m.ViqCodeNameID))
                {
                    viqCodeName = viqCodeNames.Where(obj => obj.ViqCodeNameID == m.ViqCodeNameID).First().ViqCodeName;
                }
                row[1] = viqCodeName;
                row[2] = m.ViqCode;
                row[3] = m.Description;
                row[4] = m.DescriptionEng;
                row[5] = m.OrderNo;

                dt.Rows.Add(row);
                viqCodeDic[row] = m;
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            comboBoxVersion.SelectedIndex = 0;
            comboBoxCodeName.SelectedIndex = 0;

            textBoxDefectiveItem.Text = string.Empty;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            viqCodeDic.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            ViqCode管理詳細Form form = new ViqCode管理詳細Form(viqCodeNames, viqVersions);

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
            MsViqCode m = viqCodeDic[view.Row];

            ViqCode管理詳細Form form = new ViqCode管理詳細Form(viqCodeNames, viqVersions, m);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }
    }
}
