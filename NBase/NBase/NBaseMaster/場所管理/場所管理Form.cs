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

namespace NBaseMaster.場所管理
{
    public partial class 場所管理Form : Form
    {
        List<MsBasho> msBashos = new List<MsBasho>();

        public 場所管理Form()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "港管理一覧", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
        }

        private void 場所管理Form_Load(object sender, EventArgs e)
        {
            #region 場所区分
            List<MsBashoKubun> msBashoKubuns = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msBashoKubuns = serviceClient.MsBashoKubun_GetRecords(NBaseCommon.Common.LoginUser);
            }
            BashoKubun_comboBox.Items.Clear();
            MsBashoKubun dmy = new MsBashoKubun();
            dmy.MsBashoKubunId = "";
            dmy.BashoKubunName = "";
            BashoKubun_comboBox.Items.Add(dmy);
            foreach (MsBashoKubun msBashoKubun in msBashoKubuns)
            {
                BashoKubun_comboBox.Items.Add(msBashoKubun);
            }
            #endregion
        }

        // 2010.03.08:aki 以下、使用していないとおもわれる
        //private void button3_Click(object sender, EventArgs e)
        //{
        //    場所管理詳細Form form = new 場所管理詳細Form();
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
                    string bashoKubunId = "";
                    MsBashoKubun bashoKubun = BashoKubun_comboBox.SelectedItem as MsBashoKubun;
                    if (bashoKubun != null)
                    {
                        bashoKubunId = bashoKubun.MsBashoKubunId;
                    }
                    msBashos = serviceClient.MsBasho_GetRecordsByBashoNoBashoNameBashoKubun(NBaseCommon.Common.LoginUser, BashoNo_textBox.Text, BashoName_textBox.Text, bashoKubunId);
                }
                    
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("obj", typeof(MsBasho)));
                dt.Columns.Add(new DataColumn("港No", typeof(string)));
                dt.Columns.Add(new DataColumn("港名", typeof(string)));
                //dt.Columns.Add(new DataColumn("場所区分名", typeof(string)));
                //dt.Columns.Add(new DataColumn("区分", typeof(string)));
                    
                foreach (MsBasho msBasho in msBashos)
                {
                    DataRow row = dt.NewRow();
                    row["obj"] = msBasho;
                    row["港No"] = msBasho.MsBashoNo;
                    row["港名"] = msBasho.BashoName;
                    //row["場所区分名"] = msBasho.BashoKubunName;
                    //if (msBasho.GaichiFlag == 1)
                    //{
                    //    row["区分"] = "外航";
                    //}
                    //else
                    //{
                    //    row["区分"] = "内航";
                    //}

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
            BashoNo_textBox.Text = "";
            BashoName_textBox.Text = "";
            BashoKubun_comboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// 新規追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_button_Click(object sender, EventArgs e)
        {
            場所管理詳細Form DetailForm = new 場所管理詳細Form();

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
            MsBasho msBasho = dataGridView1.SelectedRows[0].Cells[0].Value as MsBasho;

            場所管理詳細Form DetailForm = new 場所管理詳細Form();
            DetailForm.msBasho = msBasho;

            DetailForm.ShowDialog();
            Search();
        }
    }
}
