using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseCommon;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;

namespace Senin
{
    public partial class 乗り合わせ詳細Form : SeninSearchClientForm
    {
        private int searchFlag = 0;

        private SiFellowPassengers fellowPassengers;


        public 乗り合わせ詳細Form(SiFellowPassengers fellowPassengers = null)
        {
            this.fellowPassengers = fellowPassengers;
            InitializeComponent();
        }

        private void 乗り合わせ詳細Form_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            if (fellowPassengers == null)
            {
                fellowPassengers = new SiFellowPassengers();
            }

            InitComboBox職名(comboBox職名1, fellowPassengers.MsSiShokumeiID1);
            textBox船員1.Text = fellowPassengers.Name1;

            InitComboBox職名(comboBox職名2, fellowPassengers.MsSiShokumeiID2);
            textBox船員2.Text = fellowPassengers.Name2;

            textBox備考.Text = fellowPassengers.Bikou;
        }

        private void InitComboBox職名(ComboBox combo, int msSiShokumeiId)
        {
            combo.Items.Add(string.Empty);

            foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                combo.Items.Add(s);
                if (s.MsSiShokumeiID == msSiShokumeiId)
                {
                    combo.SelectedItem = s;
                }
            }
        }

        private void button船員検索_Click(object sender, EventArgs e)
        {
            if ((sender as Button) == button船員検索1)
            {
                searchFlag = 1;
            }
            else
            {
                searchFlag = 2;
            }
            船員検索Form form = new 船員検索Form(this, false);
            form.ShowDialog();
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
                fellowPassengers.DeleteFlag = 1;

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
                result = serviceClient.SiFellowPassengers_InsertOrUpdate(NBaseCommon.Common.LoginUser, fellowPassengers);
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
            if (!(comboBox職名1.SelectedItem is MsSiShokumei))
            {
                comboBox職名1.BackColor = Color.Pink;
                MessageBox.Show("職名を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox職名1.BackColor = Color.White;
                return false;
            }
            else if (textBox船員1.Text == null || textBox船員1.Text.Length == 0)
            {
                textBox船員1.BackColor = Color.Pink;
                MessageBox.Show("船員を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox船員1.BackColor = Color.White;
                return false;
            }
            else if (!(comboBox職名2.SelectedItem is MsSiShokumei))
            {
                comboBox職名2.BackColor = Color.Pink;
                MessageBox.Show("職名を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox職名2.BackColor = Color.White;
                return false;
            }
            else if (textBox船員2.Text == null || textBox船員2.Text.Length == 0)
            {
                textBox船員1.BackColor = Color.Pink;
                MessageBox.Show("船員を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox船員1.BackColor = Color.White;
                return false;
            }
            else if (textBox備考.Text.Length > 500)
            {
                textBox備考.BackColor = Color.Pink;
                MessageBox.Show("備考は500文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox備考.BackColor = Color.White;
                return false;
            }

            return true;
        }
        #endregion

        /// <summary>
        /// 入力値を取得し、乗り合わせ情報にセットする
        /// </summary>
        #region FillInstance()
        private void FillInstance()
        {
            fellowPassengers.Bikou = StringUtils.Escape(textBox備考.Text);

            fellowPassengers.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
            fellowPassengers.RenewDate = DateTime.Now;
        }
        #endregion

        /// <summary>
        /// 船員検索からコールされる船員検索の実処理
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        #region public override List<MsSenin> SearchMsSenin(MsSeninFilter filter)
        public override List<MsSenin> SearchMsSenin(MsSeninFilter filter)
        {
            if (searchFlag == 1)
            {
                if (comboBox職名1.SelectedItem is MsSiShokumei)
                    filter.MsSiShokumeiID = (comboBox職名1.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            }
            else
            {
                if (comboBox職名2.SelectedItem is MsSiShokumei)
                    filter.MsSiShokumeiID = (comboBox職名2.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            }

            List<MsSenin> result = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.MsSenin_GetRecordsByFilter(NBaseCommon.Common.LoginUser, filter);
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 船員検索からコールされる船員選択の実処理
        /// </summary>
        /// <param name="senin"></param>
        #region public override bool SetMsSenin(MsSenin senin, bool check)
        public override bool SetMsSenin(MsSenin senin, bool check)
        {
            if (searchFlag == 1)
            {
                fellowPassengers.MsSiShokumeiID1 = senin.MsSiShokumeiID;
                fellowPassengers.MsSeninID1 = senin.MsSeninID;

                comboBox職名1.SelectedItem = SeninTableCache.instance().GetMsSiShokumei(NBaseCommon.Common.LoginUser, senin.MsSiShokumeiID);
                textBox船員1.Text = senin.Sei + " " + senin.Mei;
            }
            else
            {
                fellowPassengers.MsSiShokumeiID2 = senin.MsSiShokumeiID;
                fellowPassengers.MsSeninID2 = senin.MsSeninID;

                comboBox職名2.SelectedItem = SeninTableCache.instance().GetMsSiShokumei(NBaseCommon.Common.LoginUser, senin.MsSiShokumeiID);
                textBox船員2.Text = senin.Sei + " " + senin.Mei;
            }

            return true;
        }
        #endregion



    }
}
