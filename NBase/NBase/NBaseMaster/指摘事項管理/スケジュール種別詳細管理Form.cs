using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;

namespace NBaseMaster.指摘事項管理
{
    public partial class スケジュール種別詳細管理Form : Form
    {
        private Dictionary<DataRow, MsScheduleKindDetail> scheduleKindDetailDic = new Dictionary<DataRow, MsScheduleKindDetail>();
        private List<MsScheduleKind> kinds;


        public スケジュール種別詳細管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox_カテゴリ();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("種別", typeof(string)));
            dt.Columns.Add(new DataColumn("詳細名", typeof(string)));
            dt.Columns.Add(new DataColumn("背景色", typeof(string)));

            dataGridView1.DataSource = dt;
            
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].Width = 65;
            dataGridView1.Columns[2].ReadOnly = true;
        }

        private void InitComboBox_カテゴリ()
        {
            kinds = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                kinds = serviceClient.MsScheduleKind_GetRecords(NBaseCommon.Common.LoginUser);
            }

            comboBox種別.Items.Add(string.Empty);

            foreach (MsScheduleKind o in kinds)
            {
                comboBox種別.Items.Add(o);
            }

            comboBox種別.SelectedIndex = 0;
        }

        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }


        private void Search()
        {
            List<MsScheduleKindDetail> scheduleKindDetails = null;

            int kindId = -1;
            if (comboBox種別.SelectedItem is MsScheduleKind)
            {
                kindId = (comboBox種別.SelectedItem as MsScheduleKind).ScheduleKindID;
            }
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                scheduleKindDetails = serviceClient.MsScheduleKindDetail_SearchRecords(NBaseCommon.Common.LoginUser, kindId, textBox名.Text.Trim());
            }

            SetRows(scheduleKindDetails);
        }


        private void SetRows(List<MsScheduleKindDetail> scheduleKindDetails)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            scheduleKindDetailDic.Clear();

            foreach (MsScheduleKindDetail m in scheduleKindDetails)
            {
                DataRow row = dt.NewRow();

                string kindName = "";
                if (kinds.Any(obj => obj.ScheduleKindID == m.ScheduleKindID))
                {
                    kindName = kinds.Where(obj => obj.ScheduleKindID == m.ScheduleKindID).First().ScheduleKindName;
                }
                row[0] = kindName;
                row[1] = m.ScheduleKindDetailName;
                row[2] = "";

                dt.Rows.Add(row);
                scheduleKindDetailDic[row] = m;
            }

            int rowNo = 0;
            foreach (MsScheduleKindDetail m in scheduleKindDetails)
            {
                if (m.ColorR != -1 && m.ColorG != -1 && m.ColorB != -1)
                    dataGridView1.Rows[rowNo].Cells[2].Style.BackColor = Color.FromArgb(m.ColorR, m.ColorG, m.ColorB);

                rowNo++;
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBox名.Text = string.Empty;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            scheduleKindDetailDic.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            スケジュール種別詳細管理詳細Form form = new スケジュール種別詳細管理詳細Form(kinds);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRowView view = dataGridView1.SelectedRows[0].DataBoundItem as DataRowView;
            MsScheduleKindDetail m = scheduleKindDetailDic[view.Row];

            スケジュール種別詳細管理詳細Form form = new スケジュール種別詳細管理詳細Form(kinds, m);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }
    }
}
