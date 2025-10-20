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
    public partial class スケジュール種別管理Form : Form
    {
        private Dictionary<DataRow, MsScheduleKind> scheduleKindDic = new Dictionary<DataRow, MsScheduleKind>();
        private List<MsScheduleCategory> categorys;


        public スケジュール種別管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox_カテゴリ();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("区分", typeof(string)));
            dt.Columns.Add(new DataColumn("詳細名", typeof(string)));

            dataGridView1.DataSource = dt;
            
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[1].ReadOnly = true;
        }

        private void InitComboBox_カテゴリ()
        {
            categorys = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                categorys = serviceClient.MsScheduleCategory_GetRecords(NBaseCommon.Common.LoginUser);
            }

            comboBoxカテゴリ.Items.Add(string.Empty);

            foreach (MsScheduleCategory o in categorys)
            {
                comboBoxカテゴリ.Items.Add(o);
            }

            comboBoxカテゴリ.SelectedIndex = 0;
        }

        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }


        private void Search()
        {
            List<MsScheduleKind> scheduleKinds = null;

            int categoryId = -1;
            if (comboBoxカテゴリ.SelectedItem is MsScheduleCategory)
            {
                categoryId = (comboBoxカテゴリ.SelectedItem as MsScheduleCategory).ScheduleCategoryID;
            }
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                scheduleKinds = serviceClient.MsScheduleKind_SearchRecords(NBaseCommon.Common.LoginUser, categoryId, textBox名.Text.Trim());
            }

            SetRows(scheduleKinds);
        }


        private void SetRows(List<MsScheduleKind> scheduleKinds)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            scheduleKindDic.Clear();

            foreach (MsScheduleKind m in scheduleKinds)
            {
                DataRow row = dt.NewRow();

                string categoryName = "";
                if (categorys.Any(obj => obj.ScheduleCategoryID == m.ScheduleCategoryID))
                {
                    categoryName = categorys.Where(obj => obj.ScheduleCategoryID == m.ScheduleCategoryID).First().ScheduleCategoryName;
                }
                row[0] = categoryName;
                row[1] = m.ScheduleKindName;

                dt.Rows.Add(row);
                scheduleKindDic[row] = m;
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBox名.Text = string.Empty;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            scheduleKindDic.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            スケジュール種別管理詳細Form form = new スケジュール種別管理詳細Form(categorys);

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
            MsScheduleKind m = scheduleKindDic[view.Row];

            スケジュール種別管理詳細Form form = new スケジュール種別管理詳細Form(categorys, m);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }
    }
}
