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

namespace NBaseMaster.バース管理
{
    public partial class バース管理Form : Form
    {
        List<MsBerth> msBerths = new List<MsBerth>();

        public バース管理Form()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            バース管理詳細Form form = new バース管理詳細Form();
            form.ShowDialog();
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
                    msBerths = serviceClient.MsBerth_GetRecordByBerthCodeBerthName(NBaseCommon.Common.LoginUser, BerthCode_textBox.Text, BerthName_textBox.Text);
                }

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("obj", typeof(MsBerth)));
                dt.Columns.Add(new DataColumn("バースコード", typeof(string)));
                dt.Columns.Add(new DataColumn("バース名", typeof(string)));
                dt.Columns.Add(new DataColumn("基地名", typeof(string)));

                foreach (MsBerth msBerth in msBerths)
                {
                    DataRow row = dt.NewRow();
                    row["obj"] = msBerth;
                    row["バースコード"] = msBerth.BerthCode;
                    row["バース名"] = msBerth.BerthName;
                    row["基地名"] = msBerth.KichiName;

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
            BerthCode_textBox.Text = "";
            BerthName_textBox.Text = "";
        }

        /// <summary>
        /// 新規追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_button_Click(object sender, EventArgs e)
        {
            バース管理詳細Form DetailForm = new バース管理詳細Form();

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
            MsBerth msBerth = dataGridView1.SelectedRows[0].Cells[0].Value as MsBerth;

            バース管理詳細Form DetailForm = new バース管理詳細Form();
            DetailForm.msBerth = msBerth;

            DetailForm.ShowDialog();
            Search();
        }
    }
}
