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
    public partial class 大項目管理Form : Form
    {
        private Dictionary<DataRow, MsSiDaikoumoku> daikoumokuDic = new Dictionary<DataRow, MsSiDaikoumoku>();
        private List<MsSiHiyouKamoku> hiyouKamokus;


        public 大項目管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            DataTable dt = new DataTable();

			dt.Columns.Add(new DataColumn("大項目名", typeof(string)));
            dt.Columns.Add(new DataColumn("費用科目名", typeof(string)));
            

            dataGridView1.DataSource = dt;
            
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;

            Load_費用科目();

			//費用科目ComboBoxの初期化 検索できるようにする。
			this.comboBox費用科目名.DropDownStyle = ComboBoxStyle.DropDown;
            this.comboBox費用科目名.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.comboBox費用科目名.AutoCompleteSource = AutoCompleteSource.CustomSource;

            //追加
            this.comboBox費用科目名.Items.Clear();
			this.comboBox費用科目名.Items.Add(string.Empty);
            foreach (MsSiHiyouKamoku himo in this.hiyouKamokus)
            {
                this.comboBox費用科目名.AutoCompleteCustomSource.Add(himo.ToString());
                this.comboBox費用科目名.Items.Add(himo);
                this.comboBox費用科目名.SelectedIndex = 0;
            }
        }


        private void Load_費用科目()
        {
            hiyouKamokus = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                hiyouKamokus = serviceClient.MsSiHiyouKamoku_GetRecords(NBaseCommon.Common.LoginUser);
            }
        }


        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }


        private void Search()
        { 
            List<MsSiDaikoumoku> menjouKinds = null;

			//選択している費用科目データを取得する。
			MsSiHiyouKamoku himo = this.comboBox費用科目名.SelectedItem as MsSiHiyouKamoku;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
				menjouKinds = serviceClient.MsSiDaikoumoku_SearchRecords(NBaseCommon.Common.LoginUser, textBox大項目名.Text.Trim(), himo);
            }

            SetRows(menjouKinds);
        }


        private void SetRows(List<MsSiDaikoumoku> daikoumokus)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            daikoumokuDic.Clear();

            foreach (MsSiDaikoumoku m in daikoumokus)
            {
                DataRow row = dt.NewRow();

				row[0] = m.Name;
                row[1] = m.HiyouKamokuName;
                

                dt.Rows.Add(row);
                daikoumokuDic[row] = m;
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBox大項目名.Text = string.Empty;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            daikoumokuDic.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            大項目管理詳細Form form = new 大項目管理詳細Form(hiyouKamokus);

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
            MsSiDaikoumoku m = daikoumokuDic[view.Row];

            大項目管理詳細Form form = new 大項目管理詳細Form(m, hiyouKamokus);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }
    }
}
