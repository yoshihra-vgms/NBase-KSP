using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using ServiceReferences.NBaseService;

namespace NBaseMaster
{
    public partial class 国管理Form : Form
    {
        List<MsRegional> msRegionals = new List<MsRegional>();

        public 国管理Form()
        {
            InitializeComponent();
        }

        private void 国管理Form_Load(object sender, EventArgs e)
        {
        }

        private void Search_button_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    msRegionals = serviceClient.MsRegional_SearchRecords(NBaseCommon.Common.LoginUser, RegionalCode_textBox.Text, RegionalName_textBox.Text);
                }
                    
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("obj", typeof(MsRegional)));
                dt.Columns.Add(new DataColumn("No", typeof(string)));
                dt.Columns.Add(new DataColumn("国名", typeof(string)));
                    
                foreach (MsRegional msRegional in msRegionals)
                {
                    DataRow row = dt.NewRow();
                    row["obj"] = msRegional;
                    row["No"] = msRegional.MsRegionalCode;
                    row["国名"] = msRegional.RegionalName;

                    dt.Rows.Add(row);
                }
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;

            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }


        private void Clear_button_Click(object sender, EventArgs e)
        {
            RegionalCode_textBox.Text = "";
            RegionalName_textBox.Text = "";
        }

        /// <summary>
        /// 新規追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_button_Click(object sender, EventArgs e)
        {
            国管理詳細Form DetailForm = new 国管理詳細Form();

            DetailForm.ShowDialog();

            Search();
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_button_Click(object sender, EventArgs e)
        {
            Close();
        }


        //選択
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MsRegional msRegional = dataGridView1.SelectedRows[0].Cells[0].Value as MsRegional;

            国管理詳細Form DetailForm = new 国管理詳細Form();
            DetailForm.msRegional = msRegional;

            DetailForm.ShowDialog();
            Search();
        }
    }
}
