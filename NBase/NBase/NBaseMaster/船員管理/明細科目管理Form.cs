using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;

namespace NBaseMaster.船員管理
{
    public partial class 明細科目管理Form : Form
    {
        private Dictionary<DataRow, MsSiKamoku> siKamokuDic = new Dictionary<DataRow, MsSiKamoku>();
        private List<MsKamoku> kamokus;


        public 明細科目管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("勘定科目名", typeof(string)));
            dt.Columns.Add(new DataColumn("明細科目名", typeof(string)));
            dt.Columns.Add(new DataColumn("課税", typeof(string)));
            dt.Columns.Add(new DataColumn("費用種別", typeof(string)));

            dataGridView1.DataSource = dt;
            
            dataGridView1.Columns[0].Width = 300;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;

            Load_費用科目();
        }


        private void Load_費用科目()
        {
            kamokus = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                kamokus = serviceClient.MsKamoku_GetRecords(NBaseCommon.Common.LoginUser);
            }
        }


        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }


        private void Search()
        {
            List<MsSiKamoku> meisais = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                meisais = serviceClient.MsSiKamoku_SearchRecords(NBaseCommon.Common.LoginUser, textBox科目名.Text.Trim());
            }

            SetRows(meisais);
        }


        private void SetRows(List<MsSiKamoku> meisais)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            siKamokuDic.Clear();

            foreach (MsSiKamoku m in meisais)
            {
                DataRow row = dt.NewRow();

                row[0] = m.MsKamokuName;
                if (m.MsUtiwakeKamokuName != null && m.MsUtiwakeKamokuName.Length > 0)
                {
                    row[0] += " : " + m.MsUtiwakeKamokuName;
                }
                row[1] = m.KamokuName;
                row[2] = m.TaxFlag == (int)MsSiKamoku.税金フラグ.課税 ? "○" : string.Empty;
                row[3] = m.HiyouKind == (int)MsSiKamoku.費用種別.船員費用 ? "船員費用" : "全社費用";

                dt.Rows.Add(row);
                siKamokuDic[row] = m;
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBox科目名.Text = string.Empty;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            siKamokuDic.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            明細科目管理詳細Form form = new 明細科目管理詳細Form(kamokus);

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
            MsSiKamoku m = siKamokuDic[view.Row];

            明細科目管理詳細Form form = new 明細科目管理詳細Form(m, kamokus);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }
    }
}
