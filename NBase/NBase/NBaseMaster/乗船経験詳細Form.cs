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
    public partial class 乗船経験詳細Form : Form
    {
        public MsBoardingExperience boardingExperience;


        public 乗船経験詳細Form(MsBoardingExperience boardingExperience = null)
        {
            this.boardingExperience = boardingExperience;

            InitializeComponent();
        }

        private void 乗船経験詳細Form_Load(object sender, EventArgs e)
        {
            InitComboBox船();
            InitComboBox職名();
            InitComboBox積荷();

            if (boardingExperience == null)
            {
                boardingExperience = new MsBoardingExperience();

                // 乗船経験
                radioButton_乗船経験.Checked = true;
            }
            else
            {
                if (boardingExperience.Kubun == 1)
                {
                    // 乗船経験
                    radioButton_乗船経験.Checked = true;
                }
                else if (boardingExperience.Kubun == 2)
                {
                    // 積荷経験
                    radioButton_積荷経験.Checked = true;
                }
                else if (boardingExperience.Kubun == 3)
                {
                    // 外航経験
                    radioButton_外航経験.Checked = true;
                }
                foreach (object item in comboBox船.Items)
                {
                    if (item is NBaseData.DAC.MsVessel && (item as NBaseData.DAC.MsVessel).MsVesselID == boardingExperience.MsVesselID)
                    {
                        comboBox船.SelectedItem = item;
                        break;
                    }
                }
                foreach (object item in comboBox職名.Items)
                {
                    if (item is NBaseData.DAC.MsSiShokumei && (item as NBaseData.DAC.MsSiShokumei).MsSiShokumeiID == boardingExperience.MsSiShokumeiID)
                    {
                        comboBox職名.SelectedItem = item;
                        break;
                    }
                }
                foreach (object item in comboBox積荷.Items)
                {
                    if (item is NBaseData.DAC.MsCargoGroup && (item as NBaseData.DAC.MsCargoGroup).MsCargoGroupID == boardingExperience.MsCargoGroupID)
                    {
                        comboBox積荷.SelectedItem = item;
                        break;
                    }
                }

                textBox日数.Text = boardingExperience.Count.ToString();
            }
        }

        private void InitComboBox船()
        {
            comboBox船.Items.Add(string.Empty);

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                List<NBaseData.DAC.MsVessel> vesselList = serviceClient.MsVessel_GetRecordsBySeninEnabled(NBaseCommon.Common.LoginUser);
                foreach (NBaseData.DAC.MsVessel v in vesselList)
                {
                    comboBox船.Items.Add(v);
                }
                comboBox船.SelectedIndex = 0;
            }
        }

        private void InitComboBox職名()
        {
            comboBox職名.Items.Add(string.Empty);

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                List<NBaseData.DAC.MsSiShokumei> shokumeiList = serviceClient.MsSiShokumei_GetRecords(NBaseCommon.Common.LoginUser);
                foreach (NBaseData.DAC.MsSiShokumei s in shokumeiList)
                {
                    comboBox職名.Items.Add(s);
                }
                comboBox職名.SelectedIndex = 0;
            }
        }

        private void InitComboBox積荷()
        {
            comboBox積荷.Items.Add(string.Empty);

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                List<MsCargoGroup> cargoGroupList = serviceClient.MsCargoGroup_GetRecords(NBaseCommon.Common.LoginUser);
                foreach (MsCargoGroup c in cargoGroupList)
                {
                    comboBox積荷.Items.Add(c);
                }
            }
            comboBox積荷.SelectedIndex = 0;
        }

        /// <summary>
        /// 「更新」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button更新_Click(object sender, EventArgs e)
        private void button更新_Click(object sender, EventArgs e)
        {
            Save();
        }
        #endregion

        /// <summary>
        /// 「削除」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button削除_Click(object sender, EventArgs e)
        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                boardingExperience.DeleteFlag = 1;

                if (InsertOrUpdate())
                {
                    MessageBox.Show(this, "削除しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Dispose();
                }
                else
                {
                    MessageBox.Show(this, "削除に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button閉じる_Click(object sender, EventArgs e)
        private void button閉じる_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Dispose();
        }
        #endregion
        
        /// <summary>
        /// 登録ロジック
        /// </summary>
        #region private void Save()
        private void Save()
        {
            if (ValidateFields())
            {
                FillInstance();

                if (InsertOrUpdate())
                {
                    MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Dispose();
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region private bool InsertOrUpdate()
        private bool InsertOrUpdate()
        {
            bool result = true;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.MsBoardingExperience_InsertOrUpdate(NBaseCommon.Common.LoginUser, boardingExperience);
            }

            return result;
        }
        #endregion

        /// <summary>
        /// 入力値の確認
        /// </summary>
        /// <returns></returns>
        #region private bool ValidateFields()
        private bool ValidateFields()
        {
            if (!(comboBox職名.SelectedItem is MsSiShokumei))
            {
                comboBox職名.BackColor = Color.Pink;
                MessageBox.Show("職名を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox職名.BackColor = Color.White;
                return false;
            }

            return true;
        }
        #endregion



        /// <summary>
        /// 入力値を取得し、講習情報にセットする
        /// </summary>
        #region FillInstance()
        private void FillInstance()
        {
            if (radioButton_乗船経験.Checked)
            {
                boardingExperience.Kubun = 1;
                boardingExperience.MsVesselID = (comboBox船.SelectedItem as NBaseData.DAC.MsVessel).MsVesselID;
                boardingExperience.MsSiShokumeiID = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
                boardingExperience.Count = int.Parse(textBox日数.Text);

                boardingExperience.MsCargoGroupID = -1;
            }
            else if (radioButton_積荷経験.Checked)
            {
                boardingExperience.Kubun = 2;
                boardingExperience.MsVesselID = (comboBox船.SelectedItem as NBaseData.DAC.MsVessel).MsVesselID;
                boardingExperience.MsSiShokumeiID = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
                boardingExperience.MsCargoGroupID = (comboBox積荷.SelectedItem as MsCargoGroup).MsCargoGroupID;
                boardingExperience.Count = int.Parse(textBox日数.Text);
            }
            else if (radioButton_外航経験.Checked)
            {
                boardingExperience.Kubun = 3;
                boardingExperience.MsSiShokumeiID = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
                boardingExperience.Count = int.Parse(textBox日数.Text);

                boardingExperience.MsVesselID = -1;
                boardingExperience.MsCargoGroupID = -1;
            }
        }
        #endregion


        private void radioButton_乗船経験_CheckedChanged(object sender, EventArgs e)
        {
            comboBox船.Enabled = true;
            label船名.Text = "※船名";

            comboBox職名.Enabled = true;
            label職名.Text = "※職名";

            comboBox積荷.Enabled = false;
            label積荷.Text = "  積荷";
        }

        private void radioButton_積荷経験_CheckedChanged(object sender, EventArgs e)
        {
            comboBox船.Enabled = true;
            label船名.Text = "※船名";

            comboBox職名.Enabled = true;
            label職名.Text = "※職名";

            comboBox積荷.Enabled = true;
            label積荷.Text = "※積荷";
        }

        private void radioButton_外航経験_CheckedChanged(object sender, EventArgs e)
        {
            comboBox職名.Enabled = true;
            label職名.Text = "※職名";

            comboBox船.Enabled = false;
            label船名.Text = "  船名";

            comboBox積荷.Enabled = false;
            label積荷.Text = "  積荷";

        }

    }
}
