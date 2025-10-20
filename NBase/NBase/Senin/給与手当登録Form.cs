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
using NBaseUtil;

namespace Senin
{
    public partial class 給与手当登録Form : Form
    {
        private 給与手当申請Form parentForm = null;

        private List<CrewInfo> crewInfoList;
        
        private SiKyuyoTeate siKyuyoTeate = null;
        private MsVessel vessel = null;
        private string ym = null;

        
        private class CrewInfo
        {
            public SiCard card;
             
            public CrewInfo(SiCard card)
            {
                this.card = card;
            }
            public override string ToString()
            {
                return card.SeninName;
            }
        }


        public 給与手当登録Form(給与手当申請Form parentForm, MsVessel vessel, int year, int month, List<SiCard> cards)
        {
            InitializeComponent();

            this.parentForm = parentForm;

            this.crewInfoList = new List<CrewInfo>();
            foreach (SiCard card in cards)
            {
                if (crewInfoList.Any(obj => obj.card.MsSeninID == card.MsSeninID))
                    continue;

                this.crewInfoList.Add(new CrewInfo(card));
            }
            this.vessel = vessel;
            this.ym = year.ToString() + month.ToString("00");

            this.textBoxTitle.Text = vessel.VesselName + "  " + year.ToString() + "年" + month.ToString() + "月 給与手当登録";
        }

        public 給与手当登録Form(給与手当申請Form parentForm, MsVessel vessel, SiKyuyoTeate siKyuyoTeate)
        {
            InitializeComponent();

            this.parentForm = parentForm;
            this.siKyuyoTeate = siKyuyoTeate;
            this.crewInfoList = new List<CrewInfo>();
            SiCard card = new SiCard();
            card.MsSeninID = siKyuyoTeate.MsSeninID;
            card.SeninName = siKyuyoTeate.SeninName;

            CrewInfo cInfo = new CrewInfo(card);
            this.crewInfoList = new List<CrewInfo>();
            this.crewInfoList.Add(cInfo);
           

            this.vessel = vessel;
            this.ym = siKyuyoTeate.YM;

            this.textBoxTitle.Text = vessel.VesselName + "  " + siKyuyoTeate.YM.Substring(0, 4) + "年" + siKyuyoTeate.YM.Substring(4, 2) + "月 給与手当登録";
        }

        private void 給与手当登録Form_Load(object sender, EventArgs e)
        {
            InitComboBox職名();
            InitComboBox給与手当();
            nullableDateTimePicker1.Value = DateTime.Today;
            nullableDateTimePicker2.Value = DateTime.Today;
            nullableDateTimePicker1.Value = null;
            nullableDateTimePicker2.Value = null;
            textBox日数.Text = "";

            if (siKyuyoTeate != null)
            {
                comboBox船員.Items.Clear();
                comboBox船員.Items.Add(crewInfoList[0]);
                comboBox船員.SelectedItem = crewInfoList[0];
                comboBox船員.Enabled = false;


                comboBox給与手当.SelectedItem = SeninTableCache.instance().GetMsSiKyuyoTeate(NBaseCommon.Common.LoginUser, siKyuyoTeate.MsSiKyuyoTeateID);

                if (siKyuyoTeate.StartDate != DateTime.MinValue)
                    nullableDateTimePicker1.Value = siKyuyoTeate.StartDate;
                if (siKyuyoTeate.EndDate != DateTime.MinValue)
                    nullableDateTimePicker2.Value = siKyuyoTeate.EndDate;
                textBox日数.Text = siKyuyoTeate.Days != int.MinValue ? siKyuyoTeate.Days.ToString() : "";
                textBox金額.Text = siKyuyoTeate.Kingaku != int.MinValue ? siKyuyoTeate.Kingaku.ToString() : "";

            }
            else
            {
                button削除.Enabled = false;
            }
        }

        private void InitComboBox職名()
        {
            if (siKyuyoTeate != null)
            {
                comboBox職名.SelectedValueChanged -= new System.EventHandler(this.comboBox職名_SelectedIndexChanged);
            }

            comboBox職名.Items.Add(string.Empty);
            foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                comboBox職名.Items.Add(s);
                if (siKyuyoTeate != null && s.MsSiShokumeiID == siKyuyoTeate.MsSiShokumeiID)
                {
                    comboBox職名.SelectedItem = s;
                }
            }

            if (siKyuyoTeate == null)
            {
                comboBox職名.SelectedIndex = 0;
            }
            else
            {
                comboBox職名.Enabled = false;
            }
        }
        
        private void InitComboBox給与手当()
        {
            comboBox給与手当.Items.Add(string.Empty);
            foreach (MsSiKyuyoTeate m in SeninTableCache.instance().GetMsSiKyuyoTeateList(NBaseCommon.Common.LoginUser))
            {
                comboBox給与手当.Items.Add(m);
            }

            comboBox給与手当.SelectedIndex = 0;
        }

        private void comboBox職名_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(comboBox職名.SelectedItem is MsSiShokumei))
                return;

            MsSiShokumei shokumei = comboBox職名.SelectedItem as MsSiShokumei;

            comboBox船員.Items.Clear();
            var targetCrews = crewInfoList.Where(obj => obj.card.SiLinkShokumeiCards != null && obj.card.SiLinkShokumeiCards.Count() > 0 && obj.card.SiLinkShokumeiCards[0].MsSiShokumeiID == shokumei.MsSiShokumeiID);
            if (targetCrews.Count() > 0)
            {
                foreach (CrewInfo c in targetCrews)
                {
                    comboBox船員.Items.Add(c);
                }
            }
            else
            {
                comboBox船員.Items.Add(string.Empty);
            }

            comboBox船員.SelectedIndex = 0;
        }

        private void comboBox給与手当_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(comboBox職名.SelectedItem is MsSiShokumei))
                return;

            if (!(comboBox給与手当.SelectedItem is MsSiKyuyoTeate))
                return;

            MsSiShokumei shokumei = comboBox職名.SelectedItem as MsSiShokumei;
            MsSiKyuyoTeate kyuyoTeate = comboBox給与手当.SelectedItem as MsSiKyuyoTeate;

            List<MsSiKyuyoTeateSet> kyuyoTeateSetList = SeninTableCache.instance().GetMsSiKyuyoTeateSetList(NBaseCommon.Common.LoginUser);
            if (kyuyoTeateSetList.Any(obj => obj.MsSiKyuyoTeateID == kyuyoTeate.MsSiKyuyoTeateID && obj.MsSiShokumeiID == shokumei.MsSiShokumeiID))
            {
                var kyuyoTeateSet = kyuyoTeateSetList.Where(obj => obj.MsSiKyuyoTeateID == kyuyoTeate.MsSiKyuyoTeateID && obj.MsSiShokumeiID == shokumei.MsSiShokumeiID).First();

                textBox単価.Text = kyuyoTeateSet.Tanka.ToString();
            }
            else
            {
                // 2018.02.07 マスタ設定ないものは、「０」でなく空としたい
                //textBox単価.Text = "0";
                textBox単価.Text = "";
            }

        }

        private void button更新_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                siKyuyoTeate.CancelFlag = 1;
                //siKyuyoTeate.DeleteFlag = 1;  // DeleteFlagはHonsenWingで操作された場合のみ立てる

                if (InsertOrUpdate())
                {
                    MessageBox.Show(this, "削除しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dispose();

                    parentForm.SetSiKyuyoTeate(siKyuyoTeate);
                }
                else
                {
                    MessageBox.Show(this, "削除に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }


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
                    Dispose();

                    parentForm.SetSiKyuyoTeate(siKyuyoTeate);

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
                SiKyuyoTeate retKuyoTeate = serviceClient.SiKyuyoTeate_InsertOrUpdate(NBaseCommon.Common.LoginUser, siKyuyoTeate);
                if (retKuyoTeate != null)
                {
                    siKyuyoTeate = retKuyoTeate;
                }
                else
                {
                    result = false;
                }
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
            else if (!(comboBox船員.SelectedItem is CrewInfo))
            {
                comboBox船員.BackColor = Color.Pink;
                MessageBox.Show("船員を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox船員.BackColor = Color.White;
                return false;
            }
            else if (nullableDateTimePicker1.Value != null && nullableDateTimePicker2.Value != null 
                && (DateTime)nullableDateTimePicker1.Value > (DateTime)nullableDateTimePicker2.Value)
            {
                nullableDateTimePicker1.BackColor = Color.Pink;
                nullableDateTimePicker2.BackColor = Color.Pink;
                MessageBox.Show("開始日が終了日より後の日付です", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nullableDateTimePicker1.BackColor = Color.White;
                nullableDateTimePicker2.BackColor = Color.White;
                return false;
            }
            else if (textBox日数.Text != null && textBox日数.Text.Length != 0 && NumberUtils.Validate(textBox日数.Text) == false)
            {
                textBox日数.BackColor = Color.Pink;
                MessageBox.Show("日数を正しく入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox日数.BackColor = Color.White;
                return false;
            }
            else if (textBox金額.Text == null || textBox金額.Text.Length == 0 || NumberUtils.Validate(textBox金額.Text) == false)
            {
                textBox金額.BackColor = Color.Pink;
                MessageBox.Show("金額を正しく入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox金額.BackColor = Color.White;
                return false;
            }

            return true;
        }
        #endregion

        /// <summary>
        /// 入力値を取得し、給与手当情報にセットする
        /// </summary>
        #region FillInstance()
        private void FillInstance()
        {
            if (siKyuyoTeate == null)
            {
                siKyuyoTeate = new SiKyuyoTeate();

                siKyuyoTeate.MsVesselID = vessel.MsVesselID;
                siKyuyoTeate.YM = ym;
                siKyuyoTeate.MsSeninID = (comboBox船員.SelectedItem as CrewInfo).card.MsSeninID;
                siKyuyoTeate.MsSiShokumeiID = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            }

            siKyuyoTeate.MsSiKyuyoTeateID = (comboBox給与手当.SelectedItem as MsSiKyuyoTeate).MsSiKyuyoTeateID;
            siKyuyoTeate.Tanka = (textBox単価.Text != null && textBox単価.Text.Length > 0) ? int.Parse(textBox単価.Text) : 0;
            siKyuyoTeate.StartDate = nullableDateTimePicker1.Value != null ? (DateTime)nullableDateTimePicker1.Value : DateTime.MinValue;
            siKyuyoTeate.EndDate = nullableDateTimePicker2.Value != null ? (DateTime)nullableDateTimePicker2.Value : DateTime.MinValue;
            if (textBox日数.Text != null && textBox日数.Text.Length > 0)
            {
                siKyuyoTeate.Days = int.Parse(textBox日数.Text);
            }
            else
            {
                siKyuyoTeate.Days = 0;
            }
            siKyuyoTeate.Kingaku = int.Parse(textBox金額.Text);

            siKyuyoTeate.VesselID = vessel.MsVesselID;
        }
        #endregion

        #region private void nullableDateTimePicker_ValueChanged(object sender, EventArgs e)
        private void nullableDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            日数計算();
        }
        #endregion

        private void 日数計算()
        {
            if (nullableDateTimePicker1.Value != null && nullableDateTimePicker2.Value != null)
            {
                textBox日数.Text = StringUtils.ToStr((DateTime)nullableDateTimePicker1.Value, (DateTime)nullableDateTimePicker2.Value);
            }
            else if (nullableDateTimePicker1.Value == null)
            {
                textBox日数.Text = "0";
            }
            else if (nullableDateTimePicker2.Value == null)
            {
                textBox日数.Text = StringUtils.ToStr((DateTime)nullableDateTimePicker1.Value, DateTime.Now);
            }
        }

        private void textBox日数_TextChanged(object sender, EventArgs e)
        {
            金額計算();
        }

        private void 金額計算()
        {
            if (textBox単価.Text == null || textBox単価.Text.Length == 0)
            {
                return;
            }
            int unitPrice = 0;
            int.TryParse(textBox単価.Text, out unitPrice);

            int days = 0;
            if (int.TryParse(textBox日数.Text, out days) == false)
            {
                MessageBox.Show("日付を正しく入力してください");
                return;
            }
            textBox金額.Text = (unitPrice * days).ToString();
        }

    }
}
