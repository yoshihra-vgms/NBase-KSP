using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DS;

namespace NBaseMaster
{
    public partial class 乗船資格Form : Form
    {
        public List<NBaseData.DAC.MsVesselRankLicense> vesselRankLicenseList;

        private static 乗船資格Form instance;

        public static 乗船資格Form Instance()
        {
            if (instance == null)
            {
                instance = new 乗船資格Form();
            }

            return instance;
        }

        public 乗船資格Form()
        {
            InitializeComponent();
        }


        public void Show(int vesselId, int shokumeiId)
        {
            this.Show();

            foreach (object item in comboBox船.Items)
            {
                if (item is NBaseData.DAC.MsVessel && (item as NBaseData.DAC.MsVessel).MsVesselID == vesselId)
                {
                    comboBox船.SelectedItem = item;
                    break;
                }
            }
            foreach (object item in listBox職名.Items)
            {
                if (item is NBaseData.DAC.MsSiShokumei && (item as NBaseData.DAC.MsSiShokumei).MsSiShokumeiID == shokumeiId)
                {
                    listBox職名.SelectedItem = item;
                    break;
                }
            }      
            
            Search();
        }


        private void 乗船資格Form_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void 乗船資格Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            instance = null;
        }

        private void Init()
        {
            vesselRankLicenseList = new List<NBaseData.DAC.MsVesselRankLicense>();
            InitComboBox船();
            InitListBox職名();
            SetRows(null);
        }

        #region private void InitComboBox船()
        private void InitComboBox船()
        {
            comboBox船.Items.Add(string.Empty);

            foreach (NBaseData.DAC.MsVessel v in SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser))
            {
                comboBox船.Items.Add(v);
            }

            comboBox船.SelectedIndex = 0;
        }
        #endregion

        #region private void InitListBox職名()
        private void InitListBox職名()
        {
            foreach (NBaseData.DAC.MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                listBox職名.Items.Add(s);
            }

            listBox職名.SelectedIndex = 0;
        }
        #endregion

        private void comboBox船_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
            SetRows();
        }

        private void listBox職名_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetRows();
        }

        private void Search()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                vesselRankLicenseList = serviceClient.BLC_乗船資格_GetRecords(NBaseCommon.Common.LoginUser);
            }
        }

        private void SetRows()
        {
            NBaseData.DAC.MsVessel vessel = comboBox船.SelectedItem as NBaseData.DAC.MsVessel;
            NBaseData.DAC.MsSiShokumei rank = listBox職名.SelectedItem as NBaseData.DAC.MsSiShokumei;

            if (vessel == null || rank == null)
                return;

            if (vesselRankLicenseList.Any(obj => obj.MsVesselID == vessel.MsVesselID && obj.MsSiShokumeiID == rank.MsSiShokumeiID))
            {
                var rankLicenseList = vesselRankLicenseList.Where(obj => obj.MsVesselID == vessel.MsVesselID && obj.MsSiShokumeiID == rank.MsSiShokumeiID).ToList();
                SetRows(rankLicenseList);
            }
            else
            {
                SetRows(null);
            }
        }

        /// <summary>
        /// データ表示
        /// </summary>
        /// <param name="koushus"></param>
        private void SetRows(List<NBaseData.DAC.MsVesselRankLicense> vesselRankLicenseList)
        {
            Cursor = Cursors.WaitCursor;

            dataGridView1.Rows.Clear();

            #region カラムの設定
            if (dataGridView1.Columns.Count == 0)
            {
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "免許／免状名";
                textColumn.Width = 200;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "種別名";
                textColumn.Width = 200;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "Obj";
                textColumn.Visible = false;
                dataGridView1.Columns.Add(textColumn);
            }
            #endregion

            #region 情報を一覧にセットする
            if (vesselRankLicenseList != null)
            {
                foreach (NBaseData.DAC.MsVesselRankLicense vesselRankLicense in vesselRankLicenseList)
                {
                    if (vesselRankLicense.DeleteFlag == 0)
                        AddRow(vesselRankLicense);
                }
            }
            #endregion

            Cursor = Cursors.Default;

        }

        private void button免許追加_Click(object sender, EventArgs e)
        {
            乗船資格詳細Form form = new 乗船資格詳細Form();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                NBaseData.DAC.MsVesselRankLicense vesselRankLicense = form.vesselRankLicense;

                vesselRankLicense.MsVesselID = (comboBox船.SelectedItem as NBaseData.DAC.MsVessel).MsVesselID;
                vesselRankLicense.MsSiShokumeiID = (listBox職名.SelectedItem as NBaseData.DAC.MsSiShokumei).MsSiShokumeiID;

                bool ret = false;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.BLC_乗船資格_InsertOrUpdate(NBaseCommon.Common.LoginUser, vesselRankLicense);
                }

                if (ret)
                {
                    AddRow(vesselRankLicense);
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddRow(NBaseData.DAC.MsVesselRankLicense vesselRankLicense)
        {
            int colNo = 0;
            object[] rowDatas = new object[3];
            rowDatas[colNo] = SeninTableCache.instance().GetMsSiMenjouName(NBaseCommon.Common.LoginUser, vesselRankLicense.MsSiMenjouID);
            colNo++;
            rowDatas[colNo] = SeninTableCache.instance().GetMsSiMenjouKindName(NBaseCommon.Common.LoginUser, vesselRankLicense.MsSiMenjouKindID);
            colNo++;
            rowDatas[colNo] = vesselRankLicense;

            dataGridView1.Rows.Add(rowDatas);
        }

        private void button免許削除_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            if (MessageBox.Show("選択されている免許/免状を削除しますか？") == System.Windows.Forms.DialogResult.OK)
            {
                // 削除処理
                NBaseData.DAC.MsVesselRankLicense info = dataGridView1.SelectedRows[0].Cells[2].Value as NBaseData.DAC.MsVesselRankLicense;

                info.DeleteFlag = 1;

                bool ret = false;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.BLC_乗船資格_InsertOrUpdate(NBaseCommon.Common.LoginUser, info);
                }

                if (ret)
                {
                    Search();
                    SetRows();
                }
                else
                {
                    MessageBox.Show(this, "削除に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
