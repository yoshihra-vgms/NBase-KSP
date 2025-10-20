using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;

namespace NBaseMaster.MsVesselItem
{
    public partial class 船用品管理一覧Form : Form
    {
        private List<NBaseData.DAC.MsVesselItem> MsVesselItemList;
        private List<NBaseData.DAC.MsVesselItemCategory> MsVesselItemCategoryList;
        private class 検索条件
        {
            public string MsVesselItemID;
            public string VesselItemName;
            public int CategoryNumber;
        }
        private 検索条件 条件 = null;

        public 船用品管理一覧Form()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "船用品管理一覧", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

            // カテゴリー
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsVesselItemCategoryList = serviceClient.MsVesselItemCategory_GetRecords(NBaseCommon.Common.LoginUser);
            }
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
            条件.MsVesselItemID = MsVesselItemId_textBox.Text;
            条件.VesselItemName = VesselItemName_textBox.Text;
            条件.CategoryNumber = Convert.ToInt32(((ListItem)Category_comboBox.SelectedItem).Value);


            //-------------------------------------------
            // 検索
            //-------------------------------------------
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsVesselItemList = serviceClient.MsVesselItem_SearchRecords(NBaseCommon.Common.LoginUser, 条件.MsVesselItemID, 条件.VesselItemName, 条件.CategoryNumber);
            }

            //--------------------------------------------
            // 結果表示
            //--------------------------------------------
            MakeGrid(MsVesselItemList);

            if (MsVesselItemList.Count == 0)
            {
                Message.Show確認("該当する船用品はありません。");
            }
        }

        /// <summary>
        /// 新規ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Btn_Click(object sender, EventArgs e)
        {
            船用品新規登録Form form = new 船用品新規登録Form();
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
        private void MakeGrid(List<NBaseData.DAC.MsVesselItem> targetList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("船用品ID", typeof(string)));
            dt.Columns.Add(new DataColumn("船用品名", typeof(string)));
            dt.Columns.Add(new DataColumn("カテゴリー", typeof(string)));
            dt.Columns.Add(new DataColumn("単位", typeof(string)));

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                if (targetList == null)
                {
                    MsVesselItemList = serviceClient.MsVesselItem_GetRecords(NBaseCommon.Common.LoginUser);
                    CreateRowsName(dt);
                }
                else
                {
                    MsVesselItemList = targetList;
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
            foreach (NBaseData.DAC.MsVesselItem data in MsVesselItemList)
            {
                DataRow row = dt.NewRow();

                row["船用品ID"] = data.MsVesselItemID;
                row["船用品名"] = data.VesselItemName;
                foreach (NBaseData.DAC.MsVesselItemCategory item in MsVesselItemCategoryList)
                {
                    if (data.CategoryNumber == item.CategoryNumber)
                    {
                        row["カテゴリー"] = item.CategoryName;
                    }
                }
                row["単位"] = data.TaniName;

                dt.Rows.Add(row);
            }
            dataGridView1.DataSource = dt;

            // カラム幅の設定
            dataGridView1.Columns["船用品名"].Width = 270;
            dataGridView1.Columns["単位"].Width = 65;
        }

        /// <summary>
        /// GridViewダブルクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string row = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            foreach (NBaseData.DAC.MsVesselItem vi in MsVesselItemList)
            {
                if (vi.MsVesselItemID == row)
                {
                    船用品詳細Form form = new 船用品詳細Form(vi);
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
            条件.MsVesselItemID = "";
            条件.VesselItemName = "";
            条件.CategoryNumber = -1;
        }

        /// <summary>
        /// 「新規」「編集｣後の一覧の再表示（条件は、最後に検索した条件を使用）
        /// </summary>
        private void 再表示()
        {
            this.Cursor = Cursors.WaitCursor;

            //検索
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsVesselItemList = serviceClient.MsVesselItem_SearchRecords(NBaseCommon.Common.LoginUser, 条件.MsVesselItemID, 条件.VesselItemName, 条件.CategoryNumber);
            }

            // 一覧へ表示
            MakeGrid(MsVesselItemList);

            this.Cursor = Cursors.Default;

            if (MsVesselItemList.Count == 0)
            {
                Message.Show確認("該当する船用品はありません。");
            }
        }

        /// <summary>
        /// ドロップダウンリストに値を設定する
        /// </summary>
        private void MakeDropDownList()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
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
            MsVesselItemId_textBox.Text = "";
            VesselItemName_textBox.Text = "";
            Category_comboBox.SelectedIndex = 0;
            Init条件();
        }
    }
}
