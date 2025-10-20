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

namespace NBaseMaster.救命設備管理
{
    public partial class 救命設備管理Form : Form
    {
        List<MsKyumeisetsubi> msKyumeisetsubis = new List<MsKyumeisetsubi>();

        public 救命設備管理Form()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            救命設備管理詳細Form form = new 救命設備管理詳細Form();
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
                    msKyumeisetsubis = serviceClient.MsKyumeisetsubi_GetRecordsByName(NBaseCommon.Common.LoginUser, Name_textBox.Text);
                }
         
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("obj", typeof(MsKyumeisetsubi)));
                dt.Columns.Add(new DataColumn("救命設備名", typeof(string)));
                dt.Columns.Add(new DataColumn("間隔", typeof(int)));

                foreach (MsKyumeisetsubi msKyumeisetsubi in msKyumeisetsubis)
                {
                    DataRow row = dt.NewRow();
                    row["obj"] = msKyumeisetsubi;
                    row["救命設備名"] = msKyumeisetsubi.MsKyumeisetsubiName;
                    row["間隔"] = msKyumeisetsubi.Kankaku;

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
            Name_textBox.Text = "";
        }

        /// <summary>
        /// 新規追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_button_Click(object sender, EventArgs e)
        {
            救命設備管理詳細Form DetailForm = new 救命設備管理詳細Form();

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
            MsKyumeisetsubi msKyumeisetsubi = dataGridView1.SelectedRows[0].Cells[0].Value as MsKyumeisetsubi;

            救命設備管理詳細Form DetailForm = new 救命設備管理詳細Form();
            DetailForm.msKyumeisetsubi = msKyumeisetsubi;

            DetailForm.ShowDialog();
            Search();
        }
    }
}
