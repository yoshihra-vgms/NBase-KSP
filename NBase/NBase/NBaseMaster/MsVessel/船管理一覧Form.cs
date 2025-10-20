using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;

namespace NBaseMaster.MsVessel
{
    public partial class 船管理一覧Form : Form
    {
        private List<NBaseData.DAC.MsVessel> vesselList;
        private List<NBaseData.DAC.MsVesselScheduleKindDetailEnable> vesselScheduleKindDetailEnableList;

        public 船管理一覧Form()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "船管理一覧", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
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
            NBaseData.DAC.MsVessel MsVessel = new NBaseData.DAC.MsVessel();
            MsVessel.VesselNo = VesselNo_textBox.Text;
            MsVessel.VesselName = VesselName_textBox.Text;


            //-------------------------------------------
            // 検索
            //-------------------------------------------
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                vesselList = serviceClient.MsVessel_SearchRecords(NBaseCommon.Common.LoginUser, MsVessel.VesselNo, MsVessel.VesselName);

                vesselScheduleKindDetailEnableList = serviceClient.MsVesselScheduleKindDetailEnable_GetRecords(NBaseCommon.Common.LoginUser);
            }

            //--------------------------------------------
            // 結果表示
            //--------------------------------------------
            MakeGrid(vesselList);

            if (vesselList.Count == 0)
            {
                Message.Show確認("該当する船がありません。");
            }
        }

        /// <summary>
        /// 新規ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Btn_Click(object sender, EventArgs e)
        {
            船管理新規登録Form form = new 船管理新規登録Form();
            if (form.ShowDialog() == DialogResult.OK)
            {
                MakeGrid(null);
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
        private void MakeGrid(List<NBaseData.DAC.MsVessel> targetList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("船ID", typeof(string)));
            dt.Columns.Add(new DataColumn("船No", typeof(string)));
            dt.Columns.Add(new DataColumn("船名", typeof(string)));
            dt.Columns.Add(new DataColumn("DWT(L/T)", typeof(string)));
            dt.Columns.Add(new DataColumn("定員数", typeof(string)));
            dt.Columns.Add(new DataColumn("携帯電話", typeof(string)));
            dt.Columns.Add(new DataColumn("電話番号", typeof(string)));
            dt.Columns.Add(new DataColumn("船タイプ", typeof(string)));


            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                if (targetList == null)
                {
					this.SearchBtn_Click(this.SearchBtn, null);
                }
                else
                {
                    vesselList = targetList;
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
            foreach (NBaseData.DAC.MsVessel data in vesselList)
            {
                DataRow row = dt.NewRow();

                row["船ID"] = data.MsVesselID.ToString();
                row["船No"] = data.VesselNo;
                row["船名"] = data.VesselName;
                row["DWT(L/T)"] = data.DWT.ToString();
                row["定員数"] = data.Capacity.ToString();
                row["携帯電話"] = data.HpTel;
                row["電話番号"] = data.Tel;
                row["船タイプ"] = data.VesselTypeName;

                dt.Rows.Add(row);
            }
            dataGridView1.DataSource = dt;
        }

        /// <summary>
        /// GridViewダブルクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string row = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            foreach (NBaseData.DAC.MsVessel v in vesselList)
            {
                if (v.MsVesselID.ToString() == row)
                {
                    List<MsVesselScheduleKindDetailEnable> list = new List<MsVesselScheduleKindDetailEnable>();
                    if (vesselScheduleKindDetailEnableList.Any(obj => obj.MsVesselID == v.MsVesselID))
                    {
                        list = vesselScheduleKindDetailEnableList.Where(obj => obj.MsVesselID == v.MsVesselID).ToList();
                    }

                    船管理詳細Form form = new 船管理詳細Form(v, list);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        MakeGrid(null);
                    }
                }
            }
        }

        private void Clear_button_Click(object sender, EventArgs e)
        {
            VesselNo_textBox.Text = "";
            VesselName_textBox.Text = "";
        }
    }
}
