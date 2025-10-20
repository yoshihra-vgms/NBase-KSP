using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using SyncClient;
using NBaseCommon;

namespace NBaseHonsen.Document
{
    public partial class 管理記録一覧Form : ExForm
    {
        private string DIALOG_TITLE = "管理記録";

        List<MsDmBunrui> msDmBunruis = new List<MsDmBunrui>();
        List<MsDmShoubunrui> msDmShoubunruis = new List<MsDmShoubunrui>();
        List<HoukokushoKanriKiroku> houkokushoKanriKirokus = new List<HoukokushoKanriKiroku>();

        public 管理記録一覧Form()
        {
            InitializeComponent();
        }

        private void 管理記録一覧Form_Load(object sender, EventArgs e)
        {
            InitForm();

            this.ChangeParentFormSize(FormWindowState.Minimized);
            this.Activate();
        }

        private void 管理記録一覧Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ChangeParentFormSize(FormWindowState.Normal);
        }

        /// <summary>
        /// 
        /// </summary>
        #region private void InitForm()
        private void InitForm()
        {
            msDmBunruis = MsDmBunrui.GetRecords(同期Client.LOGIN_USER);
            msDmShoubunruis = MsDmShoubunrui.GetRecords(同期Client.LOGIN_USER);
            SetBunruiDDL();
            SetShoubunruiDDL();
        }
        #endregion

        /// <summary>
        /// 「検索」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void Search_button_Click(object sender, EventArgs e)
        private void Search_button_Click(object sender, EventArgs e)
        {
            Search();
        }
        #endregion

        /// <summary>
        /// 「検索条件クリア」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void Clear_button_Click(object sender, EventArgs e)
        private void Clear_button_Click(object sender, EventArgs e)
        {
            comboBox_Bunrui.SelectedIndex = 0;
            comboBox_Shoubunrui.SelectedIndex = 0;
            textBox_BunshoNo.Text = "";
            textBox_BunshoName.Text = "";
        }
        #endregion

        /// <summary>
        /// 「分類」DDL選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox_Bunrui_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBox_Bunrui_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetShoubunruiDDL();
        }
        #endregion

        /// <summary>
        /// 「新規追加」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void Add_button_Click(object sender, EventArgs e)
        private void Add_button_Click(object sender, EventArgs e)
        {
            Open管理記録登録();
        }
        #endregion

        /// <summary>
        /// 一覧ダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Open管理記録登録();
        }
        #endregion


        /// <summary>
        /// 「管理記録登録」画面を開く
        /// </summary>
        #region private void Open管理記録登録()
        private void Open管理記録登録()
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("報告書が選択されていません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            HoukokushoKanriKiroku houkokushoKanriKiroku = dataGridView1.SelectedRows[0].Cells[0].Value as HoukokushoKanriKiroku;

            管理記録登録Form form = new 管理記録登録Form(houkokushoKanriKiroku);
            form.parentForm = this; // 2012.02 : Add
            form.ShowDialog();

            this.WindowState = FormWindowState.Normal; // 2012.02 : Add
            Search();
        }
        #endregion

        /// <summary>
        /// 「分類」DDL構築
        /// </summary>
        #region private void SetBunruiDDL()
        private void SetBunruiDDL()
        {
            MsDmBunrui dmy = new MsDmBunrui();
            dmy.MsDmBunruiID = "";
            dmy.Name = "";

            comboBox_Bunrui.Items.Clear();
            comboBox_Bunrui.Items.Add(dmy);
            foreach (MsDmBunrui msDmBunrui in msDmBunruis)
            {
                comboBox_Bunrui.Items.Add(msDmBunrui);
            }
            comboBox_Bunrui.SelectedIndex = 0;
        }
        #endregion

        /// <summary>
        /// 「小分類」DDL構築
        /// </summary>
        #region private void SetShoubunruiDDL()
        private void SetShoubunruiDDL()
        {
            string bunruiId = (comboBox_Bunrui.SelectedItem as MsDmBunrui).MsDmBunruiID;
            var msDmSoubunruisBybunruiId = from p in msDmShoubunruis
                                           where p.MsDmBunruiID == bunruiId
                                           orderby p.Code, p.Name
                                           select p;

            MsDmShoubunrui dmy = new MsDmShoubunrui();
            dmy.MsDmShoubunruiID = "";
            dmy.Name = "";

            comboBox_Shoubunrui.Items.Clear();
            comboBox_Shoubunrui.Items.Add(dmy);
            foreach (MsDmShoubunrui msDmShoubunrui in msDmSoubunruisBybunruiId)
            {
                comboBox_Shoubunrui.Items.Add(msDmShoubunrui);
            }
            comboBox_Shoubunrui.SelectedIndex = 0;
        }
        #endregion

        /// <summary>
        /// 検索＆一覧表示
        /// </summary>
        #region private void Search()
        private void Search()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                #region 検索条件を取得する

                string bunruiId = "";
                string shoubunruiId = "";
                string bunshoNo = "";
                string bunshoName = "";

                if (comboBox_Bunrui.SelectedItem is MsDmBunrui)
                {
                    bunruiId = (comboBox_Bunrui.SelectedItem as MsDmBunrui).MsDmBunruiID;
                }
                if (comboBox_Shoubunrui.SelectedItem is MsDmShoubunrui)
                {
                    shoubunruiId = (comboBox_Shoubunrui.SelectedItem as MsDmShoubunrui).MsDmShoubunruiID;
                }
                bunshoNo = textBox_BunshoNo.Text;
                bunshoName = textBox_BunshoName.Text;

                #endregion
                if (bunruiId == null || bunruiId.Length == 0)
                {
                    MessageBox.Show("分類名を選択して下さい", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                #region 検索処理

                houkokushoKanriKirokus = HoukokushoKanriKiroku.SearchRecords(同期Client.LOGIN_USER, bunruiId, shoubunruiId, bunshoNo, bunshoName, 同期Client.LOGIN_VESSEL.MsVesselID);

                #endregion

                #region カラムの設定

                dataGridView1.Font = new Font(dataGridView1.Font.FontFamily, 12);
               
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("obj", typeof(HoukokushoKanriKiroku)));
                dt.Columns.Add(new DataColumn("文書番号", typeof(string)));
                dt.Columns.Add(new DataColumn("文書名", typeof(string)));
                dt.Columns.Add(new DataColumn("提出周期", typeof(string)));
                dt.Columns.Add(new DataColumn("最新登録日", typeof(string)));

                #endregion

                #region 情報を一覧にセットする

                foreach (HoukokushoKanriKiroku houkokushoKanriKiroku in houkokushoKanriKirokus)
                {
                    DataRow row = dt.NewRow();
                    row["obj"] = houkokushoKanriKiroku;
                    row["文書番号"] = houkokushoKanriKiroku.BunshoNo;
                    row["文書名"] = houkokushoKanriKiroku.BunshoName;
                    row["提出周期"] = houkokushoKanriKiroku.Shuki != null ? houkokushoKanriKiroku.Shuki : "";
                    row["最新登録日"] = houkokushoKanriKiroku.IssueDate != DateTime.MinValue ? houkokushoKanriKiroku.IssueDate.ToShortDateString() : "";
                    dt.Rows.Add(row);
                }
                dataGridView1.DataSource = dt;

                #endregion

                #region データをセット後にカラム幅を調整

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 110;   //文書番号
                dataGridView1.Columns[2].Width = 350;   //文書名
                dataGridView1.Columns[3].Width = 200;   //提出周期
                dataGridView1.Columns[4].Width = 120;   //最新登録日
                
                #endregion

                if (houkokushoKanriKirokus.Count == 0)
                {
                    MessageBox.Show("該当する報告書がありません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion
    }
}
