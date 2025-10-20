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
    public partial class 個人予定詳細Form : SeninSearchClientForm
    {
        private SiPersonalSchedule personalSchedule;


        public 個人予定詳細Form(SiPersonalSchedule personalSchedule = null)
        {
            this.personalSchedule = personalSchedule;

            InitializeComponent();
        }

        private void 個人予定詳細Form_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            if (personalSchedule == null)
            {
                personalSchedule = new SiPersonalSchedule();

                InitComboBox職名(comboBox職名, personalSchedule.MsSiShokumeiID);
                textBox船員.Text = personalSchedule.Name;
                nullableDateTimePicker開始日.Value = DateTime.Today;
                nullableDateTimePicker終了日.Value = DateTime.Today.AddMonths(2);
            }
            else
            {
                InitComboBox職名(comboBox職名, personalSchedule.MsSiShokumeiID);
                textBox船員.Text = personalSchedule.Name;
                nullableDateTimePicker開始日.Value = personalSchedule.FromDate;
                nullableDateTimePicker終了日.Value = personalSchedule.ToDate;
                textBox内容.Text = personalSchedule.Bikou;
            }

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
                personalSchedule.DeleteFlag = 1;

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
                result = serviceClient.SiPersonalSchedule_InsertOrUpdate(NBaseCommon.Common.LoginUser, personalSchedule);
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
            else if (textBox船員.Text == null || textBox船員.Text.Length == 0)
            {
                textBox船員.BackColor = Color.Pink;
                MessageBox.Show("船員を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox船員.BackColor = Color.White;
                return false;
            }
            else if (nullableDateTimePicker開始日.Value == null)
            {
                nullableDateTimePicker開始日.BackColor = Color.Pink;
                MessageBox.Show("開始日を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nullableDateTimePicker開始日.BackColor = Color.White;
                return false;
            }
            else if (nullableDateTimePicker終了日.Value == null)
            {
                nullableDateTimePicker終了日.BackColor = Color.Pink;
                MessageBox.Show("終了日を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nullableDateTimePicker終了日.BackColor = Color.White;
                return false;
            }
            else if (nullableDateTimePicker開始日.Value != null
                      && nullableDateTimePicker終了日.Value != null
                      && (DateTime)nullableDateTimePicker開始日.Value > (DateTime)nullableDateTimePicker終了日.Value)
            {
                nullableDateTimePicker開始日.BackColor = Color.Pink;
                nullableDateTimePicker終了日.BackColor = Color.Pink;
                MessageBox.Show("開始日が終了日より後の日付です", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nullableDateTimePicker開始日.BackColor = Color.White;
                nullableDateTimePicker終了日.BackColor = Color.White;
                return false;
            }
            else if (textBox内容.Text.Length > 500)
            {
                textBox内容.BackColor = Color.Pink;
                MessageBox.Show("内容は500文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox内容.BackColor = Color.White;
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
            personalSchedule.FromDate = (DateTime)nullableDateTimePicker開始日.Value;
            personalSchedule.ToDate = (DateTime)nullableDateTimePicker終了日.Value;

            personalSchedule.Bikou = StringUtils.Escape(textBox内容.Text);

            personalSchedule.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
            personalSchedule.RenewDate = DateTime.Now;
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
            List<MsSenin> result = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                if (comboBox職名.SelectedItem is MsSiShokumei)
                    filter.MsSiShokumeiID = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;

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
            personalSchedule.MsSiShokumeiID = senin.MsSiShokumeiID;
            personalSchedule.MsSeninID = senin.MsSeninID;

            comboBox職名.SelectedItem = SeninTableCache.instance().GetMsSiShokumei(NBaseCommon.Common.LoginUser, senin.MsSiShokumeiID);
            textBox船員.Text = senin.Sei + " " + senin.Mei;

            return true;
        }
        #endregion



    }
}
