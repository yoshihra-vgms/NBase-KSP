using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NBaseMaster.MsLo
{
    public partial class 潤滑油管理一覧Form : Form
    {
        private List<NBaseData.DAC.MsLo> MsLoList;
        private List<NBaseData.DAC.MsVesselItemCategory> MsVesselItemCategoryList;
        private class 検索条件
        {
            public string MsLoID;
            public string LoName;
        }
        private 検索条件 条件 = null;

        public 潤滑油管理一覧Form()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "潤滑油管理一覧", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
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
            条件.MsLoID = MsLoId_textBox.Text;
            条件.LoName = LoName_textBox.Text;


            //-------------------------------------------
            // 検索
            //-------------------------------------------
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsLoList = serviceClient.MsLo_SearchRecords(NBaseCommon.Common.LoginUser, 条件.MsLoID, 条件.LoName);
            }

            //--------------------------------------------
            // 結果表示
            //--------------------------------------------
            MakeGrid(MsLoList);

            if (MsLoList.Count == 0)
            {
                Message.Show確認("該当する潤滑油はありません。");
            }
        }

        /// <summary>
        /// 新規ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Btn_Click(object sender, EventArgs e)
        {
            潤滑油新規登録Form form = new 潤滑油新規登録Form();
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
        private void MakeGrid(List<NBaseData.DAC.MsLo> targetList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("潤滑油ID", typeof(string)));
            dt.Columns.Add(new DataColumn("潤滑油名", typeof(string)));
            dt.Columns.Add(new DataColumn("単位", typeof(string)));

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                if (targetList == null)
                {
                    MsLoList = serviceClient.MsLo_GetRecords(NBaseCommon.Common.LoginUser);
                    CreateRowsName(dt);
                }
                else
                {
                    MsLoList = targetList;
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
            foreach (NBaseData.DAC.MsLo data in MsLoList)
            {
                DataRow row = dt.NewRow();

                row["潤滑油ID"] = data.MsLoID;
                row["潤滑油名"] = data.LoName;
                row["単位"] = data.MsTaniName;

                dt.Rows.Add(row);
            }
            dataGridView1.DataSource = dt;

            // カラム幅の設定
            dataGridView1.Columns["潤滑油名"].Width = 370;
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
            foreach (NBaseData.DAC.MsLo lo in MsLoList)
            {
                if (lo.MsLoID == row)
                {
                    潤滑油詳細Form form = new　潤滑油詳細Form(lo);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        再表示();
                    }
                }
            }
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
                MsLoList = serviceClient.MsLo_SearchRecords(NBaseCommon.Common.LoginUser, 条件.MsLoID, 条件.LoName);
            }

            // 一覧へ表示
            MakeGrid(MsLoList);

            this.Cursor = Cursors.Default;

            if (MsLoList.Count == 0)
            {
                Message.Show確認("該当する潤滑油はありません。");
            }
        }

        private void Init条件()
        {
            条件 = new 検索条件();
            条件.MsLoID = "";
            条件.LoName = "";
        }

        private void Clear_button_Click(object sender, EventArgs e)
        {
            MsLoId_textBox.Text = "";
            LoName_textBox.Text = "";
            Init条件();
        }
    }
}
