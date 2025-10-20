using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using ServiceReferences.NBaseService;

namespace NBaseMaster.場所距離管理
{
    public partial class 場所距離管理詳細Form : Form
    {
        public MsBashoKyori msBashoKyori = null;

        //データを編集したかどうか？
        private bool ChangeFlag = false;


        public 場所距離管理詳細Form()
        {
            InitializeComponent();
        }

        private void 場所距離管理詳細Form_Load(object sender, EventArgs e)
        {
            List<MsBasho> bashos;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                bashos = serviceClient.MsBasho_GetRecords(NBaseCommon.Common.LoginUser);
            }

            comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            foreach (MsBasho basho in bashos)
            {
                comboBox1.Items.Add(basho);
                comboBox1.AutoCompleteCustomSource.Add(basho.BashoName);
                comboBox2.Items.Add(basho);
                comboBox2.AutoCompleteCustomSource.Add(basho.BashoName);
            }

            if (msBashoKyori == null)
            {
                Delete_button.Enabled = false;
            }
            else
            {
                for (int i = 0; i < comboBox1.Items.Count; i++)
                {
                    MsBasho kichi = comboBox1.Items[i] as MsBasho;
                    if (msBashoKyori.MsBashoID1 == kichi.MsBashoId)
                    {
                        comboBox1.SelectedIndex = i;
                    }
                }
                for (int i = 0; i < comboBox2.Items.Count; i++)
                {
                    MsBasho kichi = comboBox2.Items[i] as MsBasho;
                    if (msBashoKyori.MsBashoID2 == kichi.MsBashoId)
                    {
                        comboBox2.SelectedIndex = i;
                    }
                }
                Kyori_textBox.Text = msBashoKyori.Kyori.ToString();
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
            }

            this.ChangeFlag = false;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_button_Click(object sender, EventArgs e)
        {
            if (Validation() == false)
            {
                return;
            }

            MsBasho basho1 = comboBox1.SelectedItem as MsBasho;
            MsBasho basho2 = comboBox2.SelectedItem as MsBasho;

            bool is新規作成;
            if (msBashoKyori == null)
            {

                bool is重複;
                MsBashoKyori k;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    k = serviceClient.MsBashoKyori_GetRecord(NBaseCommon.Common.LoginUser, basho1.MsBashoId, basho2.MsBashoId);
                }
                if (k != null)
                {
                    is重複 = true;
                }
                else
                {
                    is重複 = false;
                }
                if (is重複 == true)
                {
                    // 2011/02/16 
                    //MessageBox.Show("距離が重複しています", "場所距離管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("場所1、場所2の組み合わせの距離はすでに登録されています", "場所距離管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                msBashoKyori = new MsBashoKyori();
                msBashoKyori.MsBashoKyoriID = Guid.NewGuid().ToString();
                msBashoKyori.RenewDate = DateTime.Now;
                msBashoKyori.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                msBashoKyori.UserKey = "1";

                is新規作成 = true;
            }
            else
            {
                is新規作成 = false;
            }

            if (basho1 != null)
            {
                msBashoKyori.MsBashoID1 = basho1.MsBashoId;
            }
            if (basho2 != null)
            {
                msBashoKyori.MsBashoID2 = basho2.MsBashoId;
            }

            msBashoKyori.Kyori = Convert.ToDouble(Kyori_textBox.Text);

            if (is新規作成 == true)
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsBashoKyori_InsertRecord(NBaseCommon.Common.LoginUser, msBashoKyori);
                }
                if (ret == true)
                {
                    MessageBox.Show("更新しました", "場所距離管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            else
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsBashoKyori_UpdateRecord(NBaseCommon.Common.LoginUser, msBashoKyori);
                }
                if (ret == true)
                {
                    MessageBox.Show("更新しました", "場所距離管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }

        private bool Validation()
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("場所1を選択して下さい", "場所距離管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("場所2を選択して下さい", "場所距離管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                Convert.ToDouble(Kyori_textBox.Text);
            }
            catch
            {
                MessageBox.Show("距離を正しく入力して下さい", "場所距離管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_button_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("削除しますか？", "場所距離管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ret;
                msBashoKyori.DeleteFlag = 1;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsBashoKyori_UpdateRecord(NBaseCommon.Common.LoginUser, msBashoKyori);
                }
                if (ret == true)
                {
                    MessageBox.Show("削除しました", "場所距離管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            //編集中に閉じようとした。
            if (this.ChangeFlag == true)
            {
                DialogResult ret = MessageBox.Show(this, "データが編集されていますが、閉じますか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question);

                if (ret == DialogResult.Cancel)
                {
                    return;
                }
            }
            Close();
        }

        //データを編集した時
        private void DataChange(object sender, EventArgs e)
        {
            this.ChangeFlag = true;
        }
    }
}
