using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;

namespace Senin
{
    public partial class 家族表示順設定Form : Form
    {
        private int seninId = -1;
        private Dictionary<int, SiKazoku> kazokus_dic = null;

        public 家族表示順設定Form()
        {
            InitializeComponent();
        }

        private void 家族表示順設定Form_Load(object sender, EventArgs e)
        {

        }

        private void button更新_Click(object sender, EventArgs e)
        {
            List<SiKazoku> retKazokus = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                retKazokus = serviceClient.SiKazoku_InsertOrUpdate(NBaseCommon.Common.LoginUser, seninId, GetList());
            }
            if (retKazokus.Count == 0)
            {
                MessageBox.Show("表示順序の更新に失敗しました");
                return;
            }

            MessageBox.Show("表示順序を更新しました");

            kazokus_dic.Clear();
            int index = 0;
            foreach (SiKazoku k in retKazokus)
            {
                kazokus_dic.Add(index, k);
                index++;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void button閉じる_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Dispose();
        }

        private void button上_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            int index = dataGridView1.SelectedRows[0].Index;
            if (index == 0)
            {
                return;
            }
            SiKazoku k1 = kazokus_dic[index];
            SiKazoku k2 = kazokus_dic[index - 1];

            kazokus_dic[index] = k2;
            kazokus_dic[index - 1] = k1;

            SetGrid();

            dataGridView1.ClearSelection();
            dataGridView1.Rows[index - 1].Selected = true;
        }

        private void button下_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            int index = dataGridView1.SelectedRows[0].Index;
            if (index + 1 == dataGridView1.Rows.Count)
            {
                return;
            }
            SiKazoku k1 = kazokus_dic[index];
            SiKazoku k2 = kazokus_dic[index + 1];

            kazokus_dic[index] = k2;
            kazokus_dic[index + 1] = k1;

            SetGrid();

            dataGridView1.ClearSelection();
            dataGridView1.Rows[index + 1].Selected = true;
        }

        public void SetList(int seninId, List<SiKazoku> kazokus)
        {

            this.seninId = seninId;

            kazokus_dic = new Dictionary<int, SiKazoku>();

            int index = 0;
            foreach(SiKazoku k in kazokus)
            {
                kazokus_dic.Add(index, k);
                index++;
            }

            #region カラムの設定
            if (dataGridView1.Columns.Count == 0)
            {
                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("表示順", typeof(string)));
                dt.Columns.Add(new DataColumn("氏名", typeof(string)));
                dt.Columns.Add(new DataColumn("氏名(カナ)", typeof(string)));
                dt.Columns.Add(new DataColumn("性別", typeof(string)));
                dt.Columns.Add(new DataColumn("続柄", typeof(string)));
                dt.Columns.Add(new DataColumn("obj", typeof(object)));
                dataGridView1.DataSource = dt;

                int idx = 0;
                dataGridView1.Columns[idx].Width = 50;
                dataGridView1.Columns[idx].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns[idx].SortMode = DataGridViewColumnSortMode.NotSortable;
                idx++;
                dataGridView1.Columns[idx].Width = 100;
                dataGridView1.Columns[idx].SortMode = DataGridViewColumnSortMode.NotSortable;
                idx++;
                dataGridView1.Columns[idx].Width = 100;
                dataGridView1.Columns[idx].SortMode = DataGridViewColumnSortMode.NotSortable;
                idx++;
                dataGridView1.Columns[idx].Width = 45;
                dataGridView1.Columns[idx].SortMode = DataGridViewColumnSortMode.NotSortable;
                idx++;
                dataGridView1.Columns[idx].Width = 75;
                dataGridView1.Columns[idx].SortMode = DataGridViewColumnSortMode.NotSortable;
                idx++;
                dataGridView1.Columns[idx].Visible = false;
            }
            #endregion

            SetGrid();

        }

        public void SetGrid()
        {
            Cursor = Cursors.WaitCursor;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();

            for (int i = 0; i < kazokus_dic.Count; i++ )
            {
                SiKazoku k = kazokus_dic[i];

                #region 情報を一覧にセットする

                int colNo = 0;
                DataRow row = dt.NewRow();
                row[colNo] = (i + 1).ToString();
                colNo++;
                row[colNo] = k.Sei + " " + k.Mei;
                colNo++;
                row[colNo] = k.SeiKana + " " + k.MeiKana;
                colNo++;
                row[colNo] = k.SexStr;
                colNo++;
                row[colNo] = SeninTableCache.instance().GetMsSiOptionsName(NBaseCommon.Common.LoginUser, k.Tuzukigara);
                colNo++;
                row[colNo] = k;

                dt.Rows.Add(row);

                #endregion
            }

            Cursor = Cursors.Default;

        }

        public List<SiKazoku> GetList()
        {
            List<SiKazoku> retList = new List<SiKazoku>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                SiKazoku k = (row.Cells[5].Value as SiKazoku);
                k.ShowOrder = row.Index + 1;
                retList.Add(k);
            }
            return retList;
        }
    }
}
