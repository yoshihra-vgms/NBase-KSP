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

namespace NBaseMaster.輸送品目管理
{
    public partial class 輸送品目管理Form : Form
    {
        List<MsYusoItem> MsYusoItems = new List<MsYusoItem>();

        public 輸送品目管理Form()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            輸送品目管理詳細Form form = new 輸送品目管理詳細Form();
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
                    MsYusoItems = serviceClient.MsYusoItem_GetRecordsByYusoItemName(NBaseCommon.Common.LoginUser, Name_textBox.Text);
                }
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("obj", typeof(MsYusoItem)));
                dt.Columns.Add(new DataColumn("輸送品目コード", typeof(string)));
                dt.Columns.Add(new DataColumn("輸送品目名", typeof(string)));
                dt.Columns.Add(new DataColumn("船種コード", typeof(string)));
                dt.Columns.Add(new DataColumn("船種名", typeof(string)));

                foreach (MsYusoItem MsYusoItem in MsYusoItems)
                {
                    DataRow row = dt.NewRow();

                    row["obj"] = MsYusoItem;
                    row["輸送品目コード"] = MsYusoItem.YusoItemCode;
                    row["輸送品目名"] = MsYusoItem.YusoItemName;
                    row["船種コード"] = MsYusoItem.SenshuCode;
                    row["船種名"] = MsYusoItem.SenshuName;

                    dt.Rows.Add(row);
                }
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;

                dataGridView1.Columns[1].Width = 75;    //輸送品目コード
                dataGridView1.Columns[2].Width = 190;   //輸送品目名
                dataGridView1.Columns[3].Width = 75;    //船種コード
                dataGridView1.Columns[4].Width = 190;   //船種名
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
            輸送品目管理詳細Form DetailForm = new 輸送品目管理詳細Form();

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
            MsYusoItem MsYusoItem = dataGridView1.SelectedRows[0].Cells[0].Value as MsYusoItem;

            輸送品目管理詳細Form DetailForm = new 輸送品目管理詳細Form();
            DetailForm.msYusoItem = MsYusoItem;

            DetailForm.ShowDialog();
            Search();
        }
    }
}
