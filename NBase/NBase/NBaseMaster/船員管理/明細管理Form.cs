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
    public partial class 明細管理Form : Form
    {
        private Dictionary<DataRow, MsSiMeisai> meisaiDic = new Dictionary<DataRow, MsSiMeisai>();
        private List<MsSiHiyouKamoku> hiyouKamokus;
        private List<MsSiDaikoumoku> daikoumokus;
        private List<MsSiKamoku> siKamokus;


        public 明細管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("費用科目名", typeof(string)));
            dt.Columns.Add(new DataColumn("大項目名", typeof(string)));
            dt.Columns.Add(new DataColumn("明細名", typeof(string)));
            dt.Columns.Add(new DataColumn("明細科目名", typeof(string)));

            dataGridView1.DataSource = dt;
            
            dataGridView1.Columns[0].Width = 160;
            dataGridView1.Columns[1].Width = 160;
            dataGridView1.Columns[2].Width = 160;
            dataGridView1.Columns[3].Width = 160;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;

            Load_費用科目();
            Load_大項目();
            Load_明細科目();

			//検索条件Comboの初期化
			this.ComboBox初期化();
        }

        private void Load_費用科目()
        {
            hiyouKamokus = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                hiyouKamokus = serviceClient.MsSiHiyouKamoku_GetRecords(NBaseCommon.Common.LoginUser);
            }
        }


        private void Load_大項目()
        {
            daikoumokus = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                daikoumokus = serviceClient.MsSiDaikoumoku_GetRecords(NBaseCommon.Common.LoginUser);
            }
        }


        private void Load_明細科目()
        {
            siKamokus = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                siKamokus = serviceClient.MsSiKamoku_GetRecords(NBaseCommon.Common.LoginUser);
            }
        }

		/// <summary>
		/// 検索条件ComboBoxの初期化（費用科目、大項目）
		/// </summary>
		/// <returns></returns>
		private bool ComboBox初期化()
		{
			//**********************************************************************
			//費用科目Comboの初期化
			this.Combo費用科目.DropDownStyle = ComboBoxStyle.DropDown;
			this.Combo費用科目.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.Combo費用科目.AutoCompleteSource = AutoCompleteSource.CustomSource;

			//追加
			this.Combo費用科目.Items.Clear();
			this.Combo費用科目.AutoCompleteCustomSource.Clear();

			this.Combo費用科目.Items.Add(string.Empty);
			foreach (MsSiHiyouKamoku himo in this.hiyouKamokus)
			{
				this.Combo費用科目.AutoCompleteCustomSource.Add(himo.ToString());
				this.Combo費用科目.Items.Add(himo);
				this.Combo費用科目.SelectedIndex = 0;
			}


			//**********************************************************************
			//**********************************************************************
			//大項目Comboの初期化
			this.Combo大項目名.DropDownStyle = ComboBoxStyle.DropDown;
			this.Combo大項目名.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.Combo大項目名.AutoCompleteSource = AutoCompleteSource.CustomSource;

			//追加
			this.Combo大項目名.Items.Clear();
			this.Combo大項目名.AutoCompleteCustomSource.Clear();

			this.Combo大項目名.Items.Add(string.Empty);
			foreach (MsSiDaikoumoku komoku in this.daikoumokus)
			{
				this.Combo大項目名.AutoCompleteCustomSource.Add(komoku.ToString());
				this.Combo大項目名.Items.Add(komoku);
				this.Combo大項目名.SelectedIndex = 0;
			}


			return true;
		}

        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }


        private void Search()
        {
            List<MsSiMeisai> meisais = null;


			//Combo検索条件ありますか？
			MsSiDaikoumoku koumoku = this.Combo大項目名.SelectedItem as MsSiDaikoumoku;
			MsSiHiyouKamoku himo = this.Combo費用科目.SelectedItem as MsSiHiyouKamoku;
						

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                meisais = serviceClient.MsSiMeisai_SearchRecords(NBaseCommon.Common.LoginUser, textBox明細名.Text.Trim(), himo, koumoku);
            }

            SetRows(meisais);
        }


        private void SetRows(List<MsSiMeisai> meisais)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            meisaiDic.Clear();

            foreach (MsSiMeisai m in meisais)
            {
                DataRow row = dt.NewRow();

                row[0] = m.HiyouKamokuName;
                row[1] = m.DaikoumokuName;
                row[2] = m.Name;
                row[3] = m.KamokuName;

                dt.Rows.Add(row);
                meisaiDic[row] = m;
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBox明細名.Text = string.Empty;

			this.Combo費用科目.SelectedIndex = 0;
			this.Combo大項目名.SelectedIndex = 0;

            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();
            meisaiDic.Clear();
        }


        private void button新規_Click(object sender, EventArgs e)
        {
            明細管理詳細Form form = new 明細管理詳細Form(hiyouKamokus, daikoumokus, siKamokus);

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
            MsSiMeisai m = meisaiDic[view.Row];

            明細管理詳細Form form = new 明細管理詳細Form(m, hiyouKamokus, daikoumokus, siKamokus);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }

		//大項目の選択アイテムが変更されたとき
		private void Combo大項目名_SelectedIndexChanged(object sender, EventArgs e)
		{
			//関連する費用科目を設定する。

			MsSiDaikoumoku komoku = this.Combo大項目名.SelectedItem as MsSiDaikoumoku;

			if (komoku == null)
			{
				this.Combo費用科目.SelectedIndex = 0;
				return;
			}

			//項目名を設定する。
			this.Combo費用科目.Text = komoku.HiyouKamokuName;
		}
    }
}
