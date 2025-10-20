using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;
using SyncClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBaseHonsen.Senin
{
    public partial class tek手当一覧Form : Form
    {
        private class YearMonth
        {
            public DateTime date { set; get; }

            public override string ToString()
            {
                return date.ToString("yyyy年MM月");
            }

            public YearMonth(DateTime d)
            {
                date = d;
            }
        }

        private static tek手当一覧Form instance;

        private tek手当一覧Form()
        {
            InitializeComponent();
        }

        public static tek手当一覧Form Instance()
        {
            if (instance == null)
            {
                instance = new tek手当一覧Form();
            }

            return instance;
        }

        private void tek手当一覧Form_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            InitComboBox年月();

            Clear();
            SetRows(null);
        }

        private void InitComboBox年月()
        {
            comboBox年月.Items.Add(string.Empty);

            var strat = DateTimeUtils.ToFrom(DateTime.Today).AddMonths(1);
            for (int i = 0; i <= 12; i++)
            {
                DateTime d = strat.AddMonths(-i);
                var ym = new YearMonth(d);
                comboBox年月.Items.Add(ym);
            }
            comboBox年月.SelectedIndex = 0;
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


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            comboBox年月.SelectedIndex = 0;
            textBox手当名.Text = null;
        }

        /// <summary>
        /// 「×」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tek手当一覧Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            instance = null;
        }


        /// <summary>
        /// 検索ロジックをコールする
        /// </summary>
        internal void Search()
        {
            DateTime? date = null;
            if (comboBox年月.SelectedItem is YearMonth)
            {
                date = (comboBox年月.SelectedItem as YearMonth).date;
            }
            string allowanceName = textBox手当名.Text;

            var allowances = (List<SiAllowance>)null;

            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                string ym = null;
                if (date != null)
                    ym = ((DateTime)date).ToString("yyyyMM");

                allowances = SiAllowance.GetRecords(NBaseCommon.Common.LoginUser, ym, ym, 同期Client.LOGIN_VESSEL.MsVesselID, allowanceName);
            }, "データ取得中です...");

            progressDialog.ShowDialog();

            SetRows(allowances);
        }


        /// <summary>
        /// データ表示
        /// </summary>
        /// <param name="personalScheduleList"></param>
        private void SetRows(List<SiAllowance> allowances)
        {
            Cursor = Cursors.WaitCursor;

            dataGridView1.Rows.Clear();

            #region カラムの設定
            if (dataGridView1.Columns.Count == 0)
            {
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "対象月";
                textColumn.Width = 100;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "船名";
                textColumn.Width = 150;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "手当名";
                textColumn.Width = 400;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "作業責任者";
                textColumn.Width = 125;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "金額";
                textColumn.Width = 85;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "同期状況";
                textColumn.Width = 100;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "Obj";
                textColumn.Visible = false;
                dataGridView1.Columns.Add(textColumn);

                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[3].ReadOnly = true;
                dataGridView1.Columns[4].ReadOnly = true;
                dataGridView1.Columns[5].ReadOnly = true;

                dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            #endregion

            if (allowances != null)
            {
                int rowNo = 0;
                foreach (SiAllowance row in allowances)
                {
                    #region 情報を一覧にセットする

                    int colNo = 0;
                    object[] rowDatas = new object[7];
                    rowDatas[colNo] = $"{row.YearMonth.Substring(0, 4)}年{row.YearMonth.Substring(4, 2)}月";
                    colNo++;
                    rowDatas[colNo] = SeninTableCache.instance().GetMsVesselName(NBaseCommon.Common.LoginUser, row.MsVesselID);
                    colNo++;
                    rowDatas[colNo] = SeninTableCache.instance().GetMsSiAllowanceName(NBaseCommon.Common.LoginUser, row.MsSiAllowanceID);
                    colNo++;
                    rowDatas[colNo] = row.PersonInCharge;
                    colNo++;
                    rowDatas[colNo] = NBaseCommon.Common.金額出力(row.Allowance);
                    colNo++;
                    rowDatas[colNo] = StringUtils.ToStatusStr(row.SendFlag);
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
            SiAllowance allowance = dataGridView1.SelectedRows[0].Cells[6].Value as SiAllowance;
            tek手当詳細Form form = new tek手当詳細Form(allowance);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }

        }

        private void button追加_Click(object sender, EventArgs e)
        {
            tek手当詳細Form form = new tek手当詳細Form();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }
    }
}
