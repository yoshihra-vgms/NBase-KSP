using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using NBaseMaster.MsCustomer;
using System.IO;
using NBaseUtil;

namespace NBaseMaster.MsCustomer
{
    public partial class 顧客管理一覧Form : Form
    {
        private List<NBaseData.DAC.MsCustomer> customerList;

        // 2014.2 2013年度改造
        private string selectedCustomerId = null;

        public 顧客管理一覧Form()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "顧客管理一覧", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
        }

        private void 顧客管理一覧Form_Load(object sender, EventArgs e)
        {
        }


        /// <summary>
        /// 「検索」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            Search();
        }

        /// <summary>
        /// 「検索条件クリア」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_button_Click(object sender, EventArgs e)
        {
            CustomerID_textBox.Text = "";
            CustomerName_textBox.Text = "";

            Client_checkBox.Checked = true;
            Agency_checkBox.Checked = true;
            Consignor_checkBox.Checked = true;
            Company_checkBox.Checked = true;
            School_checkBox.Checked = true;
            Appointed_checkBox.Checked = true;
            Inspection_checkBox.Checked = true;
        }

        /// <summary>
        /// 「新規追加」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Btn_Click(object sender, EventArgs e)
        {
            //顧客管理新規登録Form form = new 顧客管理新規登録Form();
            顧客管理詳細Form form = new 顧客管理詳細Form(new NBaseData.DAC.MsCustomer());
            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 一覧のダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string row = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            foreach (NBaseData.DAC.MsCustomer c in customerList)
            {
                if (c.MsCustomerID == row)
                {
                    // 2014.2 2013年度改造
                    selectedCustomerId = row;

                    顧客管理詳細Form form = new 顧客管理詳細Form(c);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        Search();
                    }
                }
            }
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        private void Search()
        {
            //--------------------------------------------
            // 検索条件
            //--------------------------------------------
            string customerID = CustomerID_textBox.Text;
            string customerName = CustomerName_textBox.Text;

            //-------------------------------------------
            // 検索
            //-------------------------------------------
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                customerList = serviceClient.MsCustomer_SearchRecords(NBaseCommon.Common.LoginUser, customerID, customerName, Client_checkBox.Checked, Agency_checkBox.Checked, Consignor_checkBox.Checked, Company_checkBox.Checked, School_checkBox.Checked, Appointed_checkBox.Checked, Inspection_checkBox.Checked);
            }

            //--------------------------------------------
            // 結果表示
            //--------------------------------------------
            MakeGrid(customerList);

            if (customerList.Count == 0)
            {
                Message.Show確認("該当する顧客がいません。");
            }
        }

        private void MakeGrid(List<NBaseData.DAC.MsCustomer> targetList)
        {
            // 2014.02.11 ２０１３年度改造
            #region
            //DataTable dt = new DataTable();
            //dt.Columns.Add(new DataColumn("顧客No", typeof(string)));
            //dt.Columns.Add(new DataColumn("顧客名", typeof(string)));
            //dt.Columns.Add(new DataColumn("住所", typeof(string)));
            //dt.Columns.Add(new DataColumn("建物名", typeof(string)));
            ////dt.Columns.Add(new DataColumn("種別", typeof(string)));
            //dt.Columns.Add(new DataColumn("取", typeof(string)));
            //dt.Columns.Add(new DataColumn("代", typeof(string)));
            //dt.Columns.Add(new DataColumn("荷", typeof(string)));

            //CreateRowsName(dt);
            #endregion

            #region カラムの設定
            if (dataGridView1.Columns.Count == 0)
            {
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "顧客No";
                textColumn.Width = 80;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "顧客名";
                textColumn.Width = 225;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "住所";
                textColumn.Width = 100;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "建物名";
                textColumn.Width = 100;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "取";
                textColumn.Width = 45;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "代";
                textColumn.Width = 45;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "荷";
                textColumn.Width = 45;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "企";
                textColumn.Width = 45;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "学";
                textColumn.Width = 45;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "申";
                textColumn.Width = 45;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "検";
                textColumn.Width = 45;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns.Add(textColumn);
            }
            #endregion

            SetRow();
        }

        // 2014.02.11 ２０１３年度改造　SetRowに変更
        #region SetRowに変更
        //private void CreateRowsName(DataTable dt)
        //{
        //    foreach (NBaseData.DAC.MsCustomer c in customerList)
        //    {
        //        DataRow row = dt.NewRow();

        //        row["顧客No"] = c.MsCustomerID;
        //        row["顧客名"] = c.CustomerName;
        //        row["住所"] = c.Address1;
        //        row["建物名"] = c.BuildingName;
        //        string shubetsuName = "";
        //        if (c.Shubetsu == 0)
        //        {
        //            shubetsuName = "取引先";
        //        }
        //        else if (c.Shubetsu == 1)
        //        {
        //            shubetsuName = "代理店";
        //        }
        //        else if (c.Shubetsu == 2)
        //        {
        //            shubetsuName = "荷主";
        //        }
        //        row["種別"] = shubetsuName;
        //        dt.Rows.Add(row);
        //    }
        //    dataGridView1.DataSource = dt;
        //}
        #endregion
        private void SetRow()
        {
            dataGridView1.Rows.Clear();

            foreach (NBaseData.DAC.MsCustomer c in customerList)
            {
                #region 情報を一覧にセットする

                int colNo = 0;
                object[] rowDatas = new object[11];

                rowDatas[colNo] = c.MsCustomerID;
                colNo++;
                rowDatas[colNo] = c.CustomerName;
                colNo++;
                rowDatas[colNo] = c.Address1;
                colNo++;
                rowDatas[colNo] = c.BuildingName;
                colNo++;
                rowDatas[colNo] = c.Is取引先() ? "○" : "";
                colNo++;
                rowDatas[colNo] = c.Is代理店() ? "○" : "";
                colNo++;
                rowDatas[colNo] = c.Is荷主() ? "○" : "";
                colNo++;
                rowDatas[colNo] = c.Is企業() ? "○" : "";
                colNo++;
                rowDatas[colNo] = c.Is学校() ? "○" : "";
                colNo++;
                rowDatas[colNo] = c.AppointedFlag == 1 ? "○" : "";
                colNo++;
                rowDatas[colNo] = c.InspectionFlag == 1 ? "○" : "";
                colNo++;

                dataGridView1.Rows.Add(rowDatas);

                #endregion
            }

            // 選択行はなしにしておく
            dataGridView1.CurrentCell = null;

            //
            if (selectedCustomerId != null)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string customerId = row.Cells[0].Value.ToString();
                    if (customerId == selectedCustomerId)
                    {
                        dataGridView1.CurrentCell = row.Cells[0];
                        break;
                    }
                }
            }
        }

        private void button出力_Click(object sender, EventArgs e)
        {
            if (customerList == null || customerList.Count == 0)
            {
                MessageBox.Show("データが検索されていません", "顧客管理一覧");
                return;
            }

            FileUtils.SetDesktopFolder(saveFileDialog1);

            saveFileDialog1.FileName = "顧客管理一覧_" + DateTime.Today.ToString("yyyyMMdd") + ".xlsx";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    byte[] result = null;
                    NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                    {
                        using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                        {
                            result = serviceClient.BLC_Excel_顧客管理一覧表出力(NBaseCommon.Common.LoginUser, customerList);
                        }
                    }, "顧客管理一覧を出力中です...");
                    progressDialog.ShowDialog();

                    if (result == null)
                    {
                        MessageBox.Show("顧客管理一覧の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "顧客管理一覧", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Cursor = System.Windows.Forms.Cursors.Default;
                        return;
                    }

                    System.IO.FileStream filest = new System.IO.FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    filest.Write(result, 0, result.Length);
                    filest.Close();

                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    //カーソルを通常に戻す
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    MessageBox.Show("顧客管理一覧の出力に失敗しました。\n (Err:" + ex.Message + ")", "顧客管理一覧", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

    }
}
