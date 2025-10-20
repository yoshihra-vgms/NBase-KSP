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

namespace NBaseMaster
{
    public partial class 乗船経験Form : Form
    {
        private Dictionary<int, string> cargoGroupDic = new Dictionary<int, string>();
        private List<MsBoardingExperience> boardingExperienceList = null;

        public 乗船経験Form()
        {
            InitializeComponent();
        }

        private void 乗船経験Form_Load(object sender, EventArgs e)
        {
            InitComboBox職名(comboBox職名);


            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                List<MsCargoGroup> cargoGroupList = serviceClient.MsCargoGroup_GetRecords(NBaseCommon.Common.LoginUser);
                foreach(MsCargoGroup cg in cargoGroupList)
                {
                    cargoGroupDic.Add(cg.MsCargoGroupID, cg.CargoGroupName);
                }
            }

            SetRows(null);
        }

        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void buttonクリア_Click(object sender, EventArgs e)
        {
            comboBox職名.SelectedIndex = 0;
        }

        private void button新規_Click(object sender, EventArgs e)
        {
            乗船経験詳細Form form = new 乗船経験詳細Form();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }

        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        private void InitComboBox職名(ComboBox combo)
        {
            combo.Items.Add(string.Empty);

            foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                combo.Items.Add(s);
            }

            combo.SelectedIndex = 0;
        }

        private void Search()
        {
            int kubun = -1;
            int vesselId = -1;
            int shokumeiId = -1;

            MsSiShokumei shokumei = this.comboBox職名.SelectedItem as MsSiShokumei;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                boardingExperienceList = serviceClient.MsBoardingExperience_SearchRecords(NBaseCommon.Common.LoginUser, kubun, vesselId, shokumeiId);
            }

            SetRows(boardingExperienceList);
        }

        /// <summary>
        /// データ表示
        /// </summary>
        /// <param name="koushus"></param>
        private void SetRows(List<MsBoardingExperience> boardingExperienceList)
        {
            Cursor = Cursors.WaitCursor;

            dataGridView1.Rows.Clear();

            #region カラムの設定
            if (dataGridView1.Columns.Count == 0)
            {
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "経験種別";
                textColumn.Width = 100;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "船";
                textColumn.Width = 100;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "職名";
                textColumn.Width = 100;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "積荷";
                textColumn.Width = 100;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "回数";
                textColumn.Width = 75;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "Obj";
                textColumn.Visible = false;
                dataGridView1.Columns.Add(textColumn);
            }
            #endregion

            #region 情報を一覧にセットする
            if (boardingExperienceList != null)
            {
                foreach (MsBoardingExperience boardingExperience in boardingExperienceList)
                {
                    AddRow(boardingExperience);
                }
            }
            #endregion

            Cursor = Cursors.Default;

        }

        private void AddRow(MsBoardingExperience boardingExperience)
        {
            int colNo = 0;
            object[] rowDatas = new object[6];
            rowDatas[colNo] = boardingExperience.Kubun == 1 ? "乗船経験" : boardingExperience.Kubun == 2 ? "積荷経験" : "外航経験";
            colNo++;
            rowDatas[colNo] = SeninTableCache.instance().GetMsVesselName(NBaseCommon.Common.LoginUser, boardingExperience.MsVesselID);
            colNo++;
            rowDatas[colNo] = SeninTableCache.instance().GetMsSiShokumeiNameAbbr(NBaseCommon.Common.LoginUser, boardingExperience.MsSiShokumeiID);
            colNo++;
            rowDatas[colNo] = boardingExperience.MsCargoGroupID > 0 ? cargoGroupDic[boardingExperience.MsCargoGroupID] : "";
            colNo++;
            rowDatas[colNo] = boardingExperience.Count.ToString();
            colNo++;
            rowDatas[colNo] = boardingExperience;

            dataGridView1.Rows.Add(rowDatas);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            MsBoardingExperience boardingExperience = dataGridView1.SelectedRows[0].Cells[5].Value as MsBoardingExperience;
            乗船経験詳細Form form = new 乗船経験詳細Form(boardingExperience);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }
    }
}
