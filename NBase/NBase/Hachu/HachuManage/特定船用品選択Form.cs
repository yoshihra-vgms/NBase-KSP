using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.BLC;

namespace Hachu.HachuManage
{
    public partial class 特定船用品選択Form : Form
    {
        // 船ID
        private int MsVesselID;

        // 船用品カテゴリ
        private MsVesselItemCategory SelectedVesselItemCategory;

        /// <summary>
        /// 選択対象とする船用品船
        /// </summary>
        public List<MsVesselItemVessel> MsVesselItemVesselList;

        /// <summary>
        /// 既に選択されている船用品船ID
        /// </summary>
        public List<string> SelectedItemIdList;

        /// <summary>
        /// 在庫数量
        /// </summary>
        public List<OdChozoShousai> OdChozoShousaiList;

        /// <summary>
        /// 選択されているRowData
        /// </summary>
        public List<DataGridViewRow> SelectedRows;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public 特定船用品選択Form(int vesselID, MsVesselItemCategory vesselItemCategory, List<string> selectedItemIdList)
        {
            MsVesselID = vesselID;
            SelectedVesselItemCategory = vesselItemCategory;
            SelectedItemIdList = selectedItemIdList;

            InitializeComponent();
        }

        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 特定船用品選択Form_Load(object sender, EventArgs e)
        {
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "特定船用品選択", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

            textBox_カテゴリ.Text = SelectedVesselItemCategory.CategoryName;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsVesselItemVesselList = serviceClient.MsVesselItemVessel_GetRecordByMsVesselIDVesselItemName2(NBaseCommon.Common.LoginUser, MsVesselID, SelectedVesselItemCategory.CategoryNumber, "");
            
                //// 前月の貯蔵レコードを取得する
                //string ym = DateTime.Today.AddMonths(-1).ToString("yyyyMM");
                //OdChozoShousaiList = serviceClient.OdChozoShousai_GetRecordsByVesselID_Date_Shubetsu(NBaseCommon.Common.LoginUser, MsVesselID, ym, (int)OdChozo.ShubetsuEnum.特定品);

                OdChozoShousaiList = serviceClient.BLC_Get特定船用品在庫(NBaseCommon.Common.LoginUser, MsVesselID);
            }

            MakeGrid();
        }

        /// <summary>
        /// 「選択」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button選択_Click(object sender, EventArgs e)
        {
            SelectedRows = new List<DataGridViewRow>();

            int checkCount = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((bool)(row.Cells["選択"].Value) == true)
                {
                    checkCount++;
                    SelectedRows.Add(row);
                }
            }

            if (checkCount == 0)
            {
                if (MessageBox.Show("特定船用品は選択されていません。よろしいですか？", "確認", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// 「キャンセル」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button閉じる_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


        private void MakeGrid()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("obj", typeof(MsVesselItemVessel)));
            dt.Columns.Add(new DataColumn("選択", typeof(bool)));
            dt.Columns.Add(new DataColumn("詳細品名", typeof(string)));
            dt.Columns.Add(new DataColumn("在庫数", typeof(string)));
            dt.Columns.Add(new DataColumn("数量", typeof(string)));

            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].Visible = false;

            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[1].HeaderCell.SortGlyphDirection = SortOrder.None;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[2].Width = 300;
            dataGridView1.Columns[2].HeaderCell.SortGlyphDirection = SortOrder.None;
            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[3].Width = 55;
            dataGridView1.Columns[3].HeaderCell.SortGlyphDirection = SortOrder.None;
            dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[4].Width = 55;
            dataGridView1.Columns[4].HeaderCell.SortGlyphDirection = SortOrder.None;
            dataGridView1.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;


            dt.Clear();
            foreach (MsVesselItemVessel obj in MsVesselItemVesselList)
            {
                if (obj.SpecificFlag != 1)
                    continue;

                if (SelectedItemIdList.Contains(obj.MsVesselItemID))
                    continue;


                int zaikoCount = 0;
                var chozoShousai = OdChozoShousaiList.Where(o => o.MsVesselItemID == obj.MsVesselItemID);
                if (chozoShousai.Count() > 0)
                {
                    zaikoCount = (chozoShousai.First()).Count;
                }
                //int count = 0;
                //if (obj.ZaikoCount - zaikoCount > 0)
                //{
                //    count = obj.ZaikoCount - zaikoCount; // 在庫数が規定在庫数に満たない場合、差分
                //}

                DataRow row = dt.NewRow();

                row[0] = obj;
                row[1] = false;
                row[2] = obj.VesselItemName;
                row[3] = zaikoCount; // 残月の在庫数
                //row[4] = count; // 数量

                dt.Rows.Add(row);
            }
        }

        #region private void button_全選択_Click(object sender, EventArgs e)
        private void button_全選択_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["選択"].Value = true;
            }
        }
        #endregion

        #region private void button_全て解除_Click(object sender, EventArgs e)
        private void button_全て解除_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["選択"].Value = false;
            }
        }
        #endregion

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gridView = sender as DataGridView;

            if (e.ColumnIndex == 4)
            {
                if (gridView.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn)
                {
                    SendKeys.Send("{F2}");
                }
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gridView = sender as DataGridView;

            //if (e.ColumnIndex == 1)
            //{
            //}
            //else if (e.ColumnIndex == 4)
            //{
            //    if (gridView.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn)
            //    {
            //        SendKeys.Send("{F2}");
            //    }
            //}
            //else
            //{
            //    SendKeys.Send("{Tab}");
            //}
            if (e.ColumnIndex == 4)
            {
                if (gridView.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn)
                {
                    SendKeys.Send("{F2}");
                }
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            //新しい行のセルでなく、セルの内容が変更されている時だけ検証する
            if (e.RowIndex == dgv.NewRowIndex || !dgv.IsCurrentCellDirty)
            {
                return;
            }
            if (e.ColumnIndex == 4)
            {
                int inputValue = 0;
                if (e.FormattedValue.ToString() != "" && int.TryParse(e.FormattedValue.ToString(), out inputValue) == false)
                {
                    //行にエラーテキストを設定
                    dgv.Rows[e.RowIndex].ErrorText = "数値を入力してください。";

                    //入力した値をキャンセルして元に戻すには、次のようにする
                    dgv.CancelEdit();

                    //キャンセルする
                    e.Cancel = true;
                }
                if (inputValue > 999)
                {
                    //行にエラーテキストを設定
                    dgv.Rows[e.RowIndex].ErrorText = "半角数字３桁を入力してください。";

                    //入力した値をキャンセルして元に戻すには、次のようにする
                    dgv.CancelEdit();

                    //キャンセルする
                    e.Cancel = true;
                }
            }
        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            ////エラーテキストを消す
            //dgv.Rows[e.RowIndex].ErrorText = null;

        }
    }
}
