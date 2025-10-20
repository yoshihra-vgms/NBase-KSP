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

namespace NBaseMaster.貨物管理
{
    public partial class 貨物管理Form : Form
    {
        List<MsCargo> msCargos = new List<MsCargo>();

        public 貨物管理Form()
        {
            InitializeComponent();
        }

        private void 貨物管理Form_Load(object sender, EventArgs e)
        {
            #region 輸送品目
            //List<MsYusoItem> msYusoItems = null;
            //using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            //{
            //    msYusoItems = serviceClient.MsYusoItem_GetRecords(NBaseCommon.Common.LoginUser);
            //}
            //YusoItem_comboBox.Items.Clear();
            //MsYusoItem dmy = new MsYusoItem();
            //dmy.MsYusoItemID = -1;
            //dmy.YusoItemName = "";
            //YusoItem_comboBox.Items.Add(dmy);
            //foreach (MsYusoItem msYusoItem in msYusoItems)
            //{
            //    YusoItem_comboBox.Items.Add(msYusoItem);
            //}
            #endregion
        }

        // 2010.03.08:aki 以下、使用していないとおもわれる
        //private void button3_Click(object sender, EventArgs e)
        //{
        //    貨物管理詳細Form form = new 貨物管理詳細Form();
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
                    int yusoItemId = -1;
                    //if (YusoItem_comboBox.SelectedItem is MsYusoItem)
                    //{
                    //    MsYusoItem y = YusoItem_comboBox.SelectedItem as MsYusoItem;
                    //    yusoItemId = y.MsYusoItemID;
                    //}
                    msCargos = serviceClient.MsCargo_GetRecordsByCargoNoAndCargoName(NBaseCommon.Common.LoginUser, CargoNo_textBox.Text, CargoName_textBox.Text, yusoItemId);
                }
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("obj", typeof(MsCargo)));
                dt.Columns.Add(new DataColumn("貨物No", typeof(string)));
                dt.Columns.Add(new DataColumn("貨物名", typeof(string)));
                dt.Columns.Add(new DataColumn("荷主名", typeof(string)));
                //dt.Columns.Add(new DataColumn("輸送品目", typeof(string)));

                foreach (MsCargo cargo in msCargos)
                {
                    DataRow row = dt.NewRow();
                    row["obj"] = cargo;
                    row["貨物No"] = cargo.CargoNo;
                    row["貨物名"] = cargo.CargoName;
                    row["荷主名"] = cargo.Ninushi;
                    //row["輸送品目"] = cargo.MsYusoItemName;

                    dt.Rows.Add(row);
                }
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;

                dataGridView1.Columns[1].Width = 75;    //貨物No
                dataGridView1.Columns[2].Width = 190;   //貨物名
                dataGridView1.Columns[3].Width = 190;   //荷主名
                //dataGridView1.Columns[4].Width = 190;   //輸送品目
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }


        private void Clear_button_Click(object sender, EventArgs e)
        {
            CargoNo_textBox.Text = "";
            CargoName_textBox.Text = "";
        }

        /// <summary>
        /// 新規追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_button_Click(object sender, EventArgs e)
        {
            貨物管理詳細Form DetailForm = new 貨物管理詳細Form();

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
            MsCargo cargo = dataGridView1.SelectedRows[0].Cells[0].Value as MsCargo;

            貨物管理詳細Form DetailForm = new 貨物管理詳細Form();
            DetailForm.Cargo = cargo;

            DetailForm.ShowDialog();
            Search();
        }
    }
}
