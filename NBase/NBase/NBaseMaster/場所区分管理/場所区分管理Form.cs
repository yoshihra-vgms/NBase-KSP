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

namespace NBaseMaster.場所区分管理
{
    public partial class 場所区分管理Form : Form
    {
        List<MsBashoKubun> msBashoKubuns = new List<MsBashoKubun>();

        public 場所区分管理Form()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            場所区分管理詳細Form form = new 場所区分管理詳細Form();
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
                    msBashoKubuns = serviceClient.MsBashoKubun_GetRecordsByName(NBaseCommon.Common.LoginUser, BashoKubunName_textBox.Text);
                }
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("obj", typeof(MsBashoKubun)));
                dt.Columns.Add(new DataColumn("場所区分名", typeof(string)));

                foreach (MsBashoKubun msBashoKubun in msBashoKubuns)
                {
                    DataRow row = dt.NewRow();
                    row["obj"] = msBashoKubun;
                    row["場所区分名"] = msBashoKubun.BashoKubunName;

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
            BashoKubunName_textBox.Text = "";
        }

        /// <summary>
        /// 新規追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_button_Click(object sender, EventArgs e)
        {
            場所区分管理詳細Form DetailForm = new 場所区分管理詳細Form();

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
            MsBashoKubun msBashoKubun = dataGridView1.SelectedRows[0].Cells[0].Value as MsBashoKubun;

            場所区分管理詳細Form DetailForm = new 場所区分管理詳細Form();
            DetailForm.msBashoKubun = msBashoKubun;

            DetailForm.ShowDialog();
            Search();
        }
    }
}
