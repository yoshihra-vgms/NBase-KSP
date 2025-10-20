using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceReferences.NBaseService;
using NBaseData.DAC;

namespace NBaseMaster.Doc.乗船者登録
{
    public partial class 乗船者登録Form : Form
    {
        private int 乗船Id = -1;
        private List<NBaseData.DAC.MsVessel> msVessels = null;
        private List<SiCard> siCards = new List<SiCard>();

        public 乗船者登録Form()
        {
            InitializeComponent();
        }

        private void 乗船者登録Form_Load(object sender, EventArgs e)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msVessels = serviceClient.MsVessel_GetRecordsByDocumentEnabled(NBaseCommon.Common.LoginUser);

                List<MsSiShubetsu> shubetsuList = serviceClient.MsSiShubetsu_GetRecords(NBaseCommon.Common.LoginUser);
                foreach (MsSiShubetsu s in shubetsuList)
                {
                    if (s.Name == "乗船")
                    {
                        乗船Id = s.MsSiShubetsuID;
                        break;
                    }
                }
            }
            SetVesselDDL();
        }

        /// <summary>
        /// 「検索」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void Search_button_Click(object sender, EventArgs e)
        private void Search_button_Click(object sender, EventArgs e)
        {
            MakeGrid();
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
            comboBox_Vessel.SelectedIndex = 1;
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
            if ((comboBox_Vessel.SelectedItem is NBaseData.DAC.MsVessel) == false)
            {
                MessageBox.Show("船が選択されていません");
                return;
            }

            NBaseData.DAC.MsVessel v = comboBox_Vessel.SelectedItem as NBaseData.DAC.MsVessel;
            乗船者登録詳細Form form = new 乗船者登録詳細Form(v, 乗船Id);
            form.ShowDialog();

            // 再検索
            MakeGrid();

        }
        #endregion

        /// <summary>
        /// 一覧ダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            NBaseData.DAC.MsVessel v = comboBox_Vessel.SelectedItem as NBaseData.DAC.MsVessel;
            SiCard card = dataGridView1.SelectedRows[0].Cells[0].Value as SiCard;

            乗船者登録詳細Form form = new 乗船者登録詳細Form(v, 乗船Id, card, false);
            form.ShowDialog();

            // 再検索
            MakeGrid();
        }
        #endregion

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Close_button_Click(object sender, EventArgs e)
        private void Close_button_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        /// <summary>
        /// 「船」DDLの構築
        /// </summary>
        #region private void SetVesselDDL()
        private void SetVesselDDL()
        {
            comboBox_Vessel.Items.Clear();

            foreach (NBaseData.DAC.MsVessel v in msVessels)
            {
                comboBox_Vessel.Items.Add(v);
            }
            if (msVessels.Count > 0)
                comboBox_Vessel.SelectedItem = msVessels[0];
        }
        #endregion

        /// <summary>
        /// 検索を実行し、一覧にセットする
        /// </summary>
        /// <param name="targetList"></param>
        private void MakeGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("obj", typeof(SiCard)));
            dt.Columns.Add(new DataColumn("職名", typeof(string)));
            dt.Columns.Add(new DataColumn("氏名", typeof(string)));

            dataGridView1.DataSource = dt;

            // カラム幅の設定
            dataGridView1.Columns["職名"].Width = 150;
            dataGridView1.Columns["氏名"].Width = 205;

            NBaseData.DS.SiCardFilter filter = new NBaseData.DS.SiCardFilter();
            if (comboBox_Vessel.SelectedItem is NBaseData.DAC.MsVessel)
            {
                filter.MsVesselIDs.Add((comboBox_Vessel.SelectedItem as NBaseData.DAC.MsVessel).MsVesselID);
                filter.MsSiShubetsuIDs.Add(乗船Id);
                filter.Start = filter.End = DateTime.Today;
            }
            else
            {
                MessageBox.Show("船を選択してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    siCards = serviceClient.BLC_船員カード検索2(NBaseCommon.Common.LoginUser, filter);
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            var sortedCards = from p in siCards
                              orderby p.SeninMsSiShokumeiID
                              select p;
            foreach (SiCard c in sortedCards)
            {
                DataRow row = dt.NewRow();
                row["obj"] = c;
                string s = "";
                foreach (SiLinkShokumeiCard lsc in c.SiLinkShokumeiCards)
                {
                    s = NBaseData.DS.SeninTableCache.instance().GetMsSiShokumeiName(NBaseCommon.Common.LoginUser, lsc.MsSiShokumeiID);
                    break; // １つ目を表示
                }
                row["職名"] = s;
                row["氏名"] = c.SeninName;
                dt.Rows.Add(row);
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;

        }
    }
}
