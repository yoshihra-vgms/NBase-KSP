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
    public partial class DeficiencyCode管理Form : Form
    {
        private Dictionary<DataRow, MsDeficiencyCode> deficiencyCodeDic = new Dictionary<DataRow, MsDeficiencyCode>();
        private List<MsDeficiencyCategory> categorys;


        public DeficiencyCode管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox_カテゴリ();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("カテゴリ", typeof(string)));
            dt.Columns.Add(new DataColumn("Code名", typeof(string)));
            dt.Columns.Add(new DataColumn("DefectiveItem", typeof(string)));

            dataGridView1.DataSource = dt;
            
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].Width = 300;
            dataGridView1.Columns[2].ReadOnly = true;
        }

        private void InitComboBox_カテゴリ()
        {
            categorys = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                categorys = serviceClient.MsDeficiencyCategory_GetRecords(NBaseCommon.Common.LoginUser);
            }

            comboBoxカテゴリ.Items.Add(string.Empty);

            foreach (MsDeficiencyCategory o in categorys)
            {
                comboBoxカテゴリ.Items.Add(o);
            }

            comboBoxカテゴリ.SelectedIndex = 0;
        }

        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }


        private void Search()
        {
            List<MsDeficiencyCode> deficiencyCodes = null;

            int categoryId = -1;
            if (comboBoxカテゴリ.SelectedItem is MsDeficiencyCategory)
            {
                categoryId = (comboBoxカテゴリ.SelectedItem as MsDeficiencyCategory).DeficiencyCategoryID;
            }
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                deficiencyCodes = serviceClient.MsDeficiencyCode_SearchRecords(NBaseCommon.Common.LoginUser, categoryId, null, textBoxDefectiveItem.Text.Trim());

                if (deficiencyCodes.Count() > 0)
                    deficiencyCodes = deficiencyCodes.OrderBy(obj => obj.DeficiencyCategoryID).ThenBy(obj => obj.DeficiencyCodeID).ToList();
            }

            SetRows(deficiencyCodes);
        }


        private void SetRows(List<MsDeficiencyCode> deficiencyCodes)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            deficiencyCodeDic.Clear();

            foreach (MsDeficiencyCode m in deficiencyCodes)
            {
                DataRow row = dt.NewRow();

                string categoryName = "";
                if (categorys.Any(obj => obj.DeficiencyCategoryID == m.DeficiencyCategoryID))
                {
                    categoryName = categorys.Where(obj => obj.DeficiencyCategoryID == m.DeficiencyCategoryID).First().DeficiencyCategoryName;
                }
                row[0] = categoryName;
                row[1] = m.DeficiencyCodeName;
                row[2] = m.DefectiveItem;

                dt.Rows.Add(row);
                deficiencyCodeDic[row] = m;
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBoxDefectiveItem.Text = string.Empty;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            deficiencyCodeDic.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            DeficiencyCode管理詳細Form form = new DeficiencyCode管理詳細Form(categorys);

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
            MsDeficiencyCode m = deficiencyCodeDic[view.Row];

            DeficiencyCode管理詳細Form form = new DeficiencyCode管理詳細Form(categorys, m);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }
    }
}
