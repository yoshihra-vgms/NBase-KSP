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

namespace NBaseMaster.基地管理
{
    public partial class 基地管理Form : Form
    {
        List<MsKichi> msKichis = new List<MsKichi>();

        public 基地管理Form()
        {
            InitializeComponent();
        }

        // 2010.03.08:aki 以下、使用していないとおもわれる
        //private void button3_Click(object sender, EventArgs e)
        //{
        //    基地管理詳細Form form = new 基地管理詳細Form();
        //    form.ShowDialog();
        //}

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
                    msKichis = serviceClient.MsKichi_GetRecordsByKichiNoKichiName(NBaseCommon.Common.LoginUser, KichiNo_textBox.Text, KichiName_textBox.Text);
                }
                    
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("obj", typeof(MsKichi)));
                dt.Columns.Add(new DataColumn("基地No", typeof(string)));
                dt.Columns.Add(new DataColumn("基地名", typeof(string)));

                foreach (MsKichi msKichi in msKichis)
                {
                    DataRow row = dt.NewRow();
                    row["obj"] = msKichi;
                    row["基地No"] = msKichi.KichiNo;
                    row["基地名"] = msKichi.KichiName;
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
            KichiNo_textBox.Text = "";
            KichiName_textBox.Text = "";
        }

        /// <summary>
        /// 新規追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_button_Click(object sender, EventArgs e)
        {
            基地管理詳細Form DetailForm = new 基地管理詳細Form();

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
            MsKichi msKichi = dataGridView1.SelectedRows[0].Cells[0].Value as MsKichi;

            基地管理詳細Form DetailForm = new 基地管理詳細Form();
            DetailForm.msKichi = msKichi;

            DetailForm.ShowDialog();
            Search();
        }
    }
}
