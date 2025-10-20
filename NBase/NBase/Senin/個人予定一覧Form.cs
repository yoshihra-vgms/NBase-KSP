using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;

namespace Senin
{
    public partial class 個人予定一覧Form : Form
    {
        List<SiPersonalSchedule> personalScheduleList = null;

        private static 個人予定一覧Form instance;

        private 個人予定一覧Form()
        {
            InitializeComponent();
        }

        public static 個人予定一覧Form Instance()
        {
            if (instance == null)
            {
                instance = new 個人予定一覧Form();
            }

            return instance;
        }


        public void Show(DateTime fromDate, DateTime toDate)
        {
            Show();
            nullableDateTimePicker開始日.Value = fromDate;
            nullableDateTimePicker終了日.Value = toDate;
            Search();
        }


        private void 個人予定一覧Form_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            InitComboBox職名();
            textBox氏名.Text = "";
            nullableDateTimePicker開始日.Value = DateTime.Today;
            nullableDateTimePicker終了日.Value = DateTime.Today.AddMonths(2);

            SetRows(null);
        }

        private void InitComboBox職名()
        {
            comboBox職名.Items.Add(string.Empty);

            foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                comboBox職名.Items.Add(s);
            }

            comboBox職名.SelectedIndex = 0;
        }


        /// <summary>
        /// 「検索」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }


        /// <summary>
        /// 「追加」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button追加_Click(object sender, EventArgs e)
        {
            個人予定詳細Form form = new 個人予定詳細Form();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button閉じる_Click(object sender, EventArgs e)
        {
            Close();
            instance = null;
        }

        /// <summary>
        /// 「×」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 個人予定一覧Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            instance = null;
        }


        /// <summary>
        /// 「Excel」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button出力_Click(object sender, EventArgs e)
        {
            if (personalScheduleList == null || personalScheduleList.Count == 0)
            {
                MessageBox.Show("データが検索されていません", "個人予定一覧");
                return;
            }

            FileUtils.SetDesktopFolder(saveFileDialog1);

            saveFileDialog1.FileName = "個人予定一覧表_" + DateTime.Today.ToString("yyyyMMdd") + ".xlsx";

            if(saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    byte[] result = null;
                    NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                    {
                        using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                        {
                            result = serviceClient.BLC_Excel_個人予定一覧表出力(NBaseCommon.Common.LoginUser, personalScheduleList);
                        }
                    }, "個人予定一覧表を出力中です...");
                    progressDialog.ShowDialog();

                    if (result == null)
                    {
                        MessageBox.Show("個人予定一覧表の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "個人予定一覧", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("個人予定一覧表の出力に失敗しました。\n (Err:" + ex.Message + ")", "個人予定一覧", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }





        /// <summary>
        /// 検索ロジックをコールする
        /// </summary>
        internal void Search()
        {
            int msSiShokumeiId = -1;
            if (comboBox職名.SelectedItem is MsSiShokumei)
            {
                msSiShokumeiId = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            }
            string name = null;
            if (textBox氏名.Text.Length > 0)
            {
                name = textBox氏名.Text;
            }
            DateTime fromDate = DateTime.MinValue;
            if (nullableDateTimePicker開始日.Value != null)
            {
                fromDate = (DateTime)nullableDateTimePicker開始日.Value;
            }
            DateTime toDate = DateTime.MinValue;
            if (nullableDateTimePicker終了日.Value != null)
            {
                toDate = (DateTime)nullableDateTimePicker終了日.Value;
            }
            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    personalScheduleList = serviceClient.SiPersonalSchedule_SearchRecords(NBaseCommon.Common.LoginUser, msSiShokumeiId, name, fromDate, toDate);
                }
            }, "データ取得中です...");

            progressDialog.ShowDialog();

            SetRows(personalScheduleList);
        }


        /// <summary>
        /// データ表示
        /// </summary>
        /// <param name="personalScheduleList"></param>
        private void SetRows(List<SiPersonalSchedule> personalScheduleList)
        {
            Cursor = Cursors.WaitCursor;

            dataGridView1.Rows.Clear();

            #region カラムの設定
            if (dataGridView1.Columns.Count == 0)
            {
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "職名";
                textColumn.Width = 100;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "氏名";
                textColumn.Width = 100;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "乗船不可期間";
                textColumn.Width = 200;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "内容";
                textColumn.Width = 200;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "Obj";
                textColumn.Visible = false;
                dataGridView1.Columns.Add(textColumn);
            }
            #endregion

            if (personalScheduleList != null)
            {
                int rowNo = 0;
                foreach (SiPersonalSchedule row in personalScheduleList)
                {
                    #region 情報を一覧にセットする

                    int colNo = 0;
                    object[] rowDatas = new object[5];
                    rowDatas[colNo] = SeninTableCache.instance().GetMsSiShokumeiNameAbbr(NBaseCommon.Common.LoginUser, row.MsSiShokumeiID);
                    colNo++;
                    rowDatas[colNo] = row.Name;
                    colNo++;
                    rowDatas[colNo] = row.FromDate.ToShortDateString() + "～" + row.ToDate.ToShortDateString();
                    colNo++;
                    rowDatas[colNo] = row.Bikou;
                    colNo++;
                    rowDatas[colNo] = row;

                    dataGridView1.Rows.Add(rowDatas);

                    rowNo++;

                    #endregion
                }
            }
            Cursor = Cursors.Default;

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            SiPersonalSchedule personalSchedule = dataGridView1.SelectedRows[0].Cells[4].Value as SiPersonalSchedule;
            個人予定詳細Form form = new 個人予定詳細Form(personalSchedule);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }

        }

    }
}
