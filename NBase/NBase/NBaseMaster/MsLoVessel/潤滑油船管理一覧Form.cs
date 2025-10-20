using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;

namespace NBaseMaster.MsLoVessel
{
    public partial class 潤滑油船管理一覧Form : Form
    {
        private List<NBaseData.DAC.MsLoVessel> MsLoVesselList;
        private List<NBaseData.DAC.MsVessel> MsVesselList;

        private class 検索条件
        {
            public int MsVesselID;
            public string LoName;
        }
        private 検索条件 条件 = null;

        public 潤滑油船管理一覧Form()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "潤滑油品船管理一覧", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
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
            条件.LoName = MsLo_textBox.Text;

            //-------------------------------------------
            // 検索
            //-------------------------------------------
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsLoVesselList = serviceClient.MsLoVessel_GetRecordsByMsVesselIDAndLoName(NBaseCommon.Common.LoginUser, 条件.MsVesselID, 条件.LoName);
            }
            MsLoVesselList = 区分フィルター(MsLoVesselList);

            //--------------------------------------------
            // 結果表示
            //--------------------------------------------
            MakeGrid(MsLoVesselList);

            if (MsLoVesselList.Count == 0)
            {
                Message.Show確認("該当する潤滑油船はありません。");
            }
        }

        /// <summary>
        /// 新規ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Btn_Click(object sender, EventArgs e)
        {
            潤滑油船新規登録Form form = new 潤滑油船新規登録Form();
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
        private void MakeGrid(List<NBaseData.DAC.MsLoVessel> targetList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("潤滑油船ID", typeof(string)));
            dt.Columns.Add(new DataColumn("船名", typeof(string)));
            dt.Columns.Add(new DataColumn("潤滑油ID", typeof(string)));
            dt.Columns.Add(new DataColumn("潤滑油名", typeof(string)));

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                if (targetList == null)
                {
                    //MsVesselItemVesselList = serviceClient.MsVesselItemVessel_GetRecords(NBaseCommon.Common.LoginUser);
                    MsLoVesselList = new List<NBaseData.DAC.MsLoVessel>();
                    CreateRowsName(dt);
                }
                else
                {
                    MsLoVesselList = targetList;
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
            foreach (NBaseData.DAC.MsLoVessel data in MsLoVesselList)
            {
                DataRow row = dt.NewRow();

                row["潤滑油船ID"] = data.MsLoVesselID;
                row["船名"] = data.VesselName;
                row["潤滑油ID"] = data.MsLoID;
                row["潤滑油名"] = data.MsLoName;

                dt.Rows.Add(row);
            }
            dataGridView1.DataSource = dt;

            // カラム幅の設定
            dataGridView1.Columns["船名"].Width = 150;
            dataGridView1.Columns["潤滑油名"].Width = 270;

            // 非表示の設定
            dataGridView1.Columns["潤滑油船ID"].Visible = false;
        }

        /// <summary>
        /// GridViewダブルクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string row = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            foreach (NBaseData.DAC.MsLoVessel vi in MsLoVesselList)
            {
                if (vi.MsLoVesselID == row)
                {
                    潤滑油船詳細Form form = new 潤滑油船詳細Form(vi);
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
            条件.LoName = "";
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
                MsLoVesselList = serviceClient.MsLoVessel_GetRecordsByMsVesselIDAndLoName(NBaseCommon.Common.LoginUser, 条件.MsVesselID, 条件.LoName);
            }
            MsLoVesselList = 区分フィルター(MsLoVesselList);

            // 一覧へ表示
            MakeGrid(MsLoVesselList);

            this.Cursor = Cursors.Default;

            if (MsLoVesselList.Count == 0)
            {
                Message.Show確認("該当する潤滑油船はありません。");
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
                if (Vessel_comboBox.Items.Count > 0)
                    Vessel_comboBox.SelectedIndex = 0;
            }
        }

        private List<NBaseData.DAC.MsLoVessel> 区分フィルター(List<NBaseData.DAC.MsLoVessel> msLoVesselList)
        {
            List<NBaseData.DAC.MsLoVessel> ret = new List<NBaseData.DAC.MsLoVessel>();
            foreach (NBaseData.DAC.MsLoVessel item in msLoVesselList)
            {
                if (lo_checkBox.Checked == false)
                {
                    if (-1 < item.MsLoID.IndexOf("LO"))
                    {
                        continue;
                    }
                }
                if (etc_checkBox.Checked == false)
                {
                    if (-1 < item.MsLoID.IndexOf("ETC"))
                    {
                        continue;
                    }
                }

                ret.Add(item);
            }
            return ret;
        }

        private void Clear_button_Click(object sender, EventArgs e)
        {
            Vessel_comboBox.SelectedIndex = 0;
            MsLo_textBox.Text = "";
            lo_checkBox.Checked = true;
            etc_checkBox.Checked = true;
            Init条件();
        }
    }
}
