using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;

namespace NBaseMaster.MsVesselItemVessel
{
    public partial class 船用品船管理一覧Form : Form
    {
        private List<NBaseData.DAC.MsVesselItemVessel> MsVesselItemVesselList;
        private List<NBaseData.DAC.MsVessel> MsVesselList;
        private List<NBaseData.DAC.MsVesselItemCategory> MsVesselItemCategoryList;
        private class 検索条件
        {
            public int MsVesselID;
            public int CategoryNumber;
            public string VesselItemName;
        }
        private 検索条件 条件 = null;

        public 船用品船管理一覧Form()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "船用品船管理一覧", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            MakeDropDownList();
            Init条件();
            MakeGrid(null);
        }

        /// <summary>
        /// 検索ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            //--------------------------------------------
            // 検索条件
            //--------------------------------------------
            条件.MsVesselID = Convert.ToInt32(((ListItem)Vessel_comboBox.SelectedItem).Value);
            条件.CategoryNumber = Convert.ToInt32(((ListItem)Category_comboBox.SelectedItem).Value);
            条件.VesselItemName = MsVesselItem_textBox.Text;
            
            //-------------------------------------------
            // 検索
            //-------------------------------------------
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsVesselItemVesselList = serviceClient.MsVesselItemVessel_GetRecordByMsVesselIDVesselItemName2(NBaseCommon.Common.LoginUser, 条件.MsVesselID, 条件.CategoryNumber, 条件.VesselItemName);
            }

            //--------------------------------------------
            // 結果表示
            //--------------------------------------------
            MakeGrid(MsVesselItemVesselList);

            if (MsVesselItemVesselList.Count == 0)
            {
                Message.Show確認("該当する船用品船はありません。");
            }
        }

        /// <summary>
        /// 新規ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Btn_Click(object sender, EventArgs e)
        {
            船用品船新規登録Form form = new 船用品船新規登録Form(Convert.ToInt32(((ListItem)Vessel_comboBox.SelectedItem).Value));
            if (form.ShowDialog() == DialogResult.OK)
            {
                再表示();
            }
        }

        /// <summary>
        /// 戻るボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 表の作成
        /// </summary>
        /// <param name="targetList"></param>
        private void MakeGrid(List<NBaseData.DAC.MsVesselItemVessel> targetList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("船用品船", typeof(string)));
            dt.Columns.Add(new DataColumn("船名", typeof(string)));
            dt.Columns.Add(new DataColumn("カテゴリー", typeof(string)));
            dt.Columns.Add(new DataColumn("船用品名", typeof(string)));

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                if (targetList == null)
                {
                    MsVesselItemVesselList = new List<NBaseData.DAC.MsVesselItemVessel>();
                    CreateRowsName(dt);
                }
                else
                {
                    MsVesselItemVesselList = targetList;
                    CreateRowsName(dt);
                }
            }
        }

        /// <summary>
        /// 表へ値を設定
        /// </summary>
        /// <param name="dt"></param>
        private void CreateRowsName(DataTable dt)
        {
            foreach (NBaseData.DAC.MsVesselItemVessel data in MsVesselItemVesselList)
            {
                DataRow row = dt.NewRow();

                row["船用品船"] = data.MsVesselItemVesselID;
                row["船名"] = data.VesselName;
                foreach (NBaseData.DAC.MsVesselItemCategory item in MsVesselItemCategoryList)
                {
                    if (data.CategoryNumber == item.CategoryNumber)
                    {
                        row["カテゴリー"] = item.CategoryName;
                    }
                }
                row["船用品名"] = data.VesselItemName;

                dt.Rows.Add(row);
            }
            dataGridView1.DataSource = dt;

            // カラム幅の設定
            dataGridView1.Columns["船名"].Width = 150;
            dataGridView1.Columns["船用品名"].Width = 205;

            // 非表示の設定
            dataGridView1.Columns["船用品船"].Visible = false;
        }

        /// <summary>
        /// GridViewダブルクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string row = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            foreach (NBaseData.DAC.MsVesselItemVessel vi in MsVesselItemVesselList)
            {
                if (vi.MsVesselItemVesselID == row)
                {
                    船用品船詳細Form form = new 船用品船詳細Form(vi);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        再表示();
                    }
                }
            }
        }

        private void Init条件()
        {
            条件 = new 検索条件();
            条件.MsVesselID = -1;
            条件.VesselItemName = "";
            条件.CategoryNumber = -1;
        }

        /// <summary>
        /// 「新規」「編集｣後の一覧の再表示（条件は、最後に検索した条件を使用）
        /// </summary>
        private void 再表示()
        {
            this.Cursor = Cursors.WaitCursor;

            //--------------------------------------------
            // 検索条件
            //--------------------------------------------
            if (条件.MsVesselID == -1)
            {
                条件.MsVesselID = Convert.ToInt32(((ListItem)Vessel_comboBox.SelectedItem).Value);
                条件.CategoryNumber = Convert.ToInt32(((ListItem)Category_comboBox.SelectedItem).Value);
                条件.VesselItemName = MsVesselItem_textBox.Text;
            }

            //検索
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsVesselItemVesselList = serviceClient.MsVesselItemVessel_GetRecordByMsVesselIDVesselItemName2(NBaseCommon.Common.LoginUser, 条件.MsVesselID, 条件.CategoryNumber, 条件.VesselItemName);
            }

            // 一覧へ表示
            MakeGrid(MsVesselItemVesselList);

            this.Cursor = Cursors.Default;

            if (MsVesselItemVesselList.Count == 0)
            {
                Message.Show確認("該当する船用品船はありません。");
            }
        }

        /// <summary>
        /// ドロップダウンリストに値を設定する
        /// </summary>
        private void MakeDropDownList()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                // 船
                MsVesselList = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);
                foreach (NBaseData.DAC.MsVessel item in MsVesselList)
                {
                    Vessel_comboBox.Items.Add(new ListItem(item.VesselName, item.MsVesselID.ToString()));
                }
                Vessel_comboBox.SelectedIndex = 0;

                // カテゴリー
                MsVesselItemCategoryList = serviceClient.MsVesselItemCategory_GetRecords(NBaseCommon.Common.LoginUser);
                Category_comboBox.Items.Add(new ListItem("", int.MinValue.ToString()));
                foreach (NBaseData.DAC.MsVesselItemCategory item in MsVesselItemCategoryList)
                {
                    Category_comboBox.Items.Add(new ListItem(item.CategoryName, item.CategoryNumber.ToString()));
                }
                Category_comboBox.SelectedIndex = 0;
            }
        }

        private void Clear_button_Click(object sender, EventArgs e)
        {
            Vessel_comboBox.SelectedIndex = 0;
            Category_comboBox.SelectedIndex = 0;
            MsVesselItem_textBox.Text = "";
            Init条件();
        }


    }
}
