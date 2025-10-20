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

namespace NBaseMaster.検船管理
{
    public partial class 検船管理Form : Form
    {
        List<MsKensen> msKensens = new List<MsKensen>();

        public 検船管理Form()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            検船管理詳細Form form = new 検船管理詳細Form();
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
                    msKensens = serviceClient.MsKensen_GetRecordsByName(NBaseCommon.Common.LoginUser, Name_textBox.Text);
                }
  
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("obj", typeof(MsKensen)));
                dt.Columns.Add(new DataColumn("検船名", typeof(string)));
                dt.Columns.Add(new DataColumn("間隔", typeof(int)));

                foreach (MsKensen msKensen in msKensens)
                {
                    DataRow row = dt.NewRow();
                    row["obj"] = msKensen;
                    row["検船名"] = msKensen.MsKensenName;
                    row["間隔"] = msKensen.Kankaku;

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
            検船管理詳細Form DetailForm = new 検船管理詳細Form();

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
            MsKensen msKensen = dataGridView1.SelectedRows[0].Cells[0].Value as MsKensen;

            検船管理詳細Form DetailForm = new 検船管理詳細Form();
            DetailForm.msKensen = msKensen;

            DetailForm.ShowDialog();
            Search();
        }
    }
}
