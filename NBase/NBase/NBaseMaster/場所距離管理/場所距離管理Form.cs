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

namespace NBaseMaster.場所距離管理
{
    public partial class 場所距離管理Form : Form
    {
        List<MsBashoKyori> msBashoKyoris = new List<MsBashoKyori>();

        public 場所距離管理Form()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            場所距離管理詳細Form form = new 場所距離管理詳細Form();
            form.ShowDialog();
        }

        private void 場所距離管理Form_Load(object sender, EventArgs e)
        {
            List<MsBasho> bashos;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                bashos = serviceClient.MsBasho_GetRecords(NBaseCommon.Common.LoginUser);
            }

            MsBasho 空 = new MsBasho();
            空.BashoName = "";
            comboBox1.Items.Add(空);
            comboBox2.Items.Add(空);

            comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            foreach (MsBasho basho in bashos)
            {
                comboBox1.Items.Add(basho);
                comboBox1.AutoCompleteCustomSource.Add(basho.BashoName);
                comboBox2.Items.Add(basho);
                comboBox2.AutoCompleteCustomSource.Add(basho.BashoName);
            }
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

                MsBasho basho1 = comboBox1.SelectedItem as MsBasho;
                MsBasho basho2 = comboBox2.SelectedItem as MsBasho;

                string strbasho1 = "";
                string strbasho2 = "";
                if (basho1 != null)
                {
                    strbasho1 = basho1.MsBashoId;
                }
                if (basho2 != null)
                {
                    strbasho2 = basho2.MsBashoId;
                }
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    msBashoKyoris = serviceClient.MsBashoKyori_GetRecordsByKyori1Kyori2(NBaseCommon.Common.LoginUser, strbasho1, strbasho2);
                }

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("obj", typeof(MsBashoKyori)));
                dt.Columns.Add(new DataColumn("場所1", typeof(string)));
                dt.Columns.Add(new DataColumn("場所2", typeof(string)));
                dt.Columns.Add(new DataColumn("距離", typeof(double)));

                foreach (MsBashoKyori msBashoKyori in msBashoKyoris)
                {
                    DataRow row = dt.NewRow();
                    row["obj"] = msBashoKyori;
                    row["場所1"] = msBashoKyori.BashoName1;
                    row["場所2"] = msBashoKyori.BashoName2;
                    row["距離"] = msBashoKyori.Kyori;

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
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        /// <summary>
        /// 新規追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_button_Click(object sender, EventArgs e)
        {
            場所距離管理詳細Form DetailForm = new 場所距離管理詳細Form();

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
            MsBashoKyori msBashokyori = dataGridView1.SelectedRows[0].Cells[0].Value as MsBashoKyori;

            場所距離管理詳細Form DetailForm = new 場所距離管理詳細Form();
            DetailForm.msBashoKyori = msBashokyori;

            DetailForm.ShowDialog();
            Search();
        }

    }
}
