using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseUtil;
using NBaseData.DS;

namespace Senin
{
    public partial class 船内収支詳細Form : Form
    {
        private 船内収支Form parentForm;
        private MsVessel vessel;
        private SiJunbikin junbikin;

        private DateTime getsujiShimeDate;


        public 船内収支詳細Form(船内収支Form parentForm, MsVessel vessel)
            : this(parentForm, vessel, new SiJunbikin())
        {
        }


        public 船内収支詳細Form(船内収支Form parentForm, MsVessel vessel, SiJunbikin junbikin)
        {
            this.parentForm = parentForm;
            this.vessel = vessel;
            this.junbikin = junbikin;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            SnParameter prm = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                prm = serviceClient.SnParameter_GetRecord(NBaseCommon.Common.LoginUser);
            }
            if (prm != null && prm.Prm4 == "1")
            {
                panel_軽減税率対応.Visible = true;
                panel_内税計算.Visible = false;
            }
            else
            {
                panel_軽減税率対応.Visible = false;
                panel_内税計算.Visible = true;
            }

            InitComboBox明細();
            InitComboBox勘定科目();

            LoadGetsujiShime();

            InitFields();
        }


        private void LoadGetsujiShime()
        {
            SiGetsujiShime getsujiShime = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                getsujiShime = serviceClient.SiGetsujiShime_GetRecordByLastDate(NBaseCommon.Common.LoginUser);
            }

            if (getsujiShime == null)
            {
                MessageBox.Show("直近の月次締めレコードが見つかりません。作成してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            getsujiShimeDate = getsujiShime.月次締日.AddDays(1);
        }


        private void InitComboBox明細()
        {
            foreach (MsSiMeisai s in SeninTableCache.instance().GetMsSiMeisaiList(NBaseCommon.Common.LoginUser))
            {
                comboBox明細.Items.Add(s);
            }
        }


        private void InitComboBox勘定科目()
        {
            comboBox勘定科目.Items.Add(string.Empty);

            foreach (MsSiKamoku s in SeninTableCache.instance().GetMsSiKamokuList(NBaseCommon.Common.LoginUser))
            {
                comboBox勘定科目.Items.Add(s);
            }
        }


        private void Detect明細()
        {
            for (int i = 0; i < comboBox明細.Items.Count; i++)
            {
                if (comboBox明細.Items[i] is MsSiMeisai)
                {
                    MsSiMeisai m = comboBox明細.Items[i] as MsSiMeisai;

                    if (m.MsSiMeisaiID == junbikin.MsSiMeisaiID)
                    {
                        comboBox明細.SelectedItem = m;
                        break;
                    }
                }
            }
            // 2010.06.29 明細が削除されている場合の対応
            if (comboBox明細.SelectedItem == null)
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    MsSiMeisai meisai = serviceClient.MsSiMeisai_GetRecord(NBaseCommon.Common.LoginUser, junbikin.MsSiMeisaiID);
                    if (meisai != null)
                    {
                        comboBox明細.Items.Add(meisai);
                        comboBox明細.SelectedItem = meisai;
                    }
                }
            }
        }


        private void Detect大項目()
        {
            textBox大項目.Text = null;
            textBox大項目.Tag = null;

            MsSiMeisai m = comboBox明細.SelectedItem as MsSiMeisai;

            if (m != null)
            {
                foreach (MsSiDaikoumoku k in SeninTableCache.instance().GetMsSiDaikoumokuList(NBaseCommon.Common.LoginUser))
                {
                    if (m.MsSiDaikoumokuID == k.MsSiDaikoumokuID)
                    {
                        textBox大項目.Text = k.Name;
                        textBox大項目.Tag = k;
                    }
                }
            }
        }


        private void Detect費用科目()
        {
            textBox費用科目.Text = null;
            textBox費用科目.Tag = null;

            MsSiDaikoumoku m = textBox大項目.Tag as MsSiDaikoumoku;

            if (m != null)
            {
                foreach (MsSiHiyouKamoku k in SeninTableCache.instance().GetMsSiHiyouKamokuList(NBaseCommon.Common.LoginUser))
                {
                    if (m.MsSiHiyouKamokuID == k.MsSiHiyouKamokuID)
                    {
                        textBox費用科目.Text = k.Name;
                        textBox費用科目.Tag = k;
                    }
                }
            }
        }


        private void InitFields()
        {
            if (!junbikin.IsNew())
            {
                dateTimePicker日付.Value = junbikin.JunbikinDate;

                Detect明細();
                Detect大項目();
                Detect費用科目();

                for (int i = 0; i < comboBox勘定科目.Items.Count; i++)
                {
                    if (comboBox勘定科目.Items[i] is MsSiKamoku)
                    {
                        MsSiKamoku m = comboBox勘定科目.Items[i] as MsSiKamoku;

                        if (m.MsSiKamokuId == junbikin.MsSiKamokuId)
                        {
                            comboBox勘定科目.SelectedItem = m;
                            break;
                        }
                    }
                }

                // 貸方
                if (comboBox明細.SelectedItem != null && (comboBox明細.SelectedItem as MsSiMeisai).KashiKariFlag == 0)
                {
                    textBox金額税抜.Text = junbikin.KingakuIn.ToString();
                    textBox消費税.Text = junbikin.TaxIn.ToString();
                    textBox金額.Text = (junbikin.KingakuIn + junbikin.TaxIn).ToString();
                }
                // 借方
                else
                {
                    textBox金額税抜.Text = junbikin.KingakuOut.ToString();
                    textBox消費税.Text = junbikin.TaxOut.ToString();
                    textBox金額.Text = (junbikin.KingakuOut + junbikin.TaxOut).ToString();
                }

                textBox備考.Text = junbikin.Bikou;

                if (junbikin.FurikaeFlag == 1)
                {
                    checkBox振替.Checked = true;
                }

                // 締めた月のレコードまたは送金情報は編集できない.
                if (junbikin.JunbikinDate < getsujiShimeDate || Is_送金(junbikin))
                {
                    dateTimePicker日付.Enabled = false;
                    comboBox明細.Enabled = false;
                    comboBox勘定科目.Enabled = false;
                    textBox金額.ReadOnly = true;
                    textBox消費税.ReadOnly = true;
                    textBox備考.ReadOnly = true;
                    checkBox振替.Enabled = false;

                    button更新.Enabled = false;
                    button削除.Enabled = false;
                }
            }
            else
            {
                dateTimePicker日付.Value = DateTime.Now;
                button削除.Enabled = false;
            }
        }


        private bool Is_送金(SiJunbikin junbikin)
        {
            SiSoukin soukin = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                soukin = serviceClient.SiSoukin_GetRecordByJunbikinId(NBaseCommon.Common.LoginUser, junbikin.SiJunbikinID);
            }

            if (soukin != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void button更新_Click(object sender, EventArgs e)
        {
            Save();
        }


        private void Save()
        {
            if (ValidateFields())
            {
                FillInstance();

                if (InsertOrUpdate())
                {
                    MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dispose();
                    parentForm.Search船内準備金();
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private bool ValidateFields()
        {
            if (!(comboBox明細.SelectedItem is MsSiMeisai))
            {
                comboBox明細.BackColor = Color.Pink;
                MessageBox.Show("明細を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox明細.BackColor = Color.White;
                return false;
            }
            else if (!(comboBox勘定科目.SelectedItem is MsSiKamoku))
            {
                comboBox勘定科目.BackColor = Color.Pink;
                MessageBox.Show("勘定科目を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox勘定科目.BackColor = Color.White;
                return false;
            }
            else if (textBox金額.Text.Length > 9)
            {
                textBox金額.BackColor = Color.Pink;
                MessageBox.Show("金額は半角数字9文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox金額.BackColor = Color.White;
                return false;
            }
            else if (textBox消費税.Text.Length > 9)
            {
                textBox消費税.BackColor = Color.Pink;
                MessageBox.Show("消費税は半角数字9文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox消費税.BackColor = Color.White;
                return false;
            }
            else if (textBox備考.Text.Length > 500)
            {
                textBox備考.BackColor = Color.Pink;
                MessageBox.Show("備考は500文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox備考.BackColor = Color.White;
                return false;
            }
            else if (dateTimePicker日付.Value < getsujiShimeDate)
            {
                dateTimePicker日付.BackColor = Color.Pink;
                MessageBox.Show("既に締めた月に明細は追加できません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker日付.BackColor = Color.White;
                return false;
            }

            return true;
        }


        private void FillInstance()
        {
            junbikin.MsVesselID = junbikin.VesselID = vessel.MsVesselID;
            junbikin.MsSiHiyouKamokuID = (textBox費用科目.Tag as MsSiHiyouKamoku).MsSiHiyouKamokuID;
            junbikin.MsSiDaikoumokuID = (textBox大項目.Tag as MsSiDaikoumoku).MsSiDaikoumokuID;
            junbikin.MsSiMeisaiID = (comboBox明細.SelectedItem as MsSiMeisai).MsSiMeisaiID;
            junbikin.MsSiKamokuId = (comboBox勘定科目.SelectedItem as MsSiKamoku).MsSiKamokuId;
            junbikin.JunbikinDate = dateTimePicker日付.Value;

            int k;
            Int32.TryParse(textBox金額税抜.Text, out k);
            int t;
            Int32.TryParse(textBox消費税.Text, out t);

            // 貸方
            if ((comboBox明細.SelectedItem as MsSiMeisai).KashiKariFlag == 0)
            {
                junbikin.KingakuIn = k;
                junbikin.TaxIn = t;
            }
            // 借方
            else
            {
                junbikin.KingakuOut = k;
                junbikin.TaxOut = t;
            }
            
            //junbikin.Bikou = textBox備考.Text;
            junbikin.Bikou = StringUtils.Escape(textBox備考.Text);
            junbikin.FurikaeFlag = checkBox振替.Checked ? 1 : 0;

            if (junbikin.IsNew())
            {
                junbikin.TourokuUserID = NBaseCommon.Common.LoginUser.MsUserID;
            }

            int id;
            Int32.TryParse(textBox_taxId.Text, out id);
            junbikin.MsTaxID = id;
        }


        private bool InsertOrUpdate()
        {
            bool result = false;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.SiJunbikin_InsertOrUpdate(NBaseCommon.Common.LoginUser, junbikin);
            }

            return result;
        }


        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                junbikin.DeleteFlag = 1;

                if (InsertOrUpdate())
                {
                    MessageBox.Show(this, "削除しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dispose();
                    parentForm.Search船内準備金();
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


        private void comboBox明細_SelectedValueChanged(object sender, EventArgs e)
        {
            Detect大項目();
            Detect費用科目();

            if (comboBox明細.SelectedItem is MsSiMeisai)
            {
                for (int i = 0; i < comboBox勘定科目.Items.Count; i++)
                {
                    if (comboBox勘定科目.Items[i] is MsSiKamoku)
                    {
                        MsSiKamoku m = comboBox勘定科目.Items[i] as MsSiKamoku;

                        if (m.MsSiKamokuId == (comboBox明細.SelectedItem as MsSiMeisai).MsSiKamokuId)
                        {
                            comboBox勘定科目.SelectedItem = m;
                            return;
                        }
                    }
                }

                comboBox勘定科目.SelectedIndex = 0;
            }
            else
            {
                comboBox勘定科目.SelectedIndex = 0;
            }
        }


        private void comboBox明細_TextUpdate(object sender, EventArgs e)
        {
            if (comboBox明細.SelectedItem == null)
            {
                textBox大項目.Text = null;
                textBox費用科目.Text = null;
                comboBox勘定科目.SelectedIndex = 0;
            }
        }




        private void textBox金額税抜_TextChanged(object sender, EventArgs e)
        {
            Calc_金額();
        }

        private void textBox消費税_TextChanged(object sender, EventArgs e)
        {
            Calc_金額();
        }

        private void Calc_金額()
        {
            decimal amount_税抜;
            decimal.TryParse(textBox金額税抜.Text, out amount_税抜);
            decimal amount_税;
            decimal.TryParse(textBox消費税.Text, out amount_税);

            textBox金額.Text = (amount_税抜 + amount_税).ToString();
        }

        //private void button内税計算_Click(object sender, EventArgs e)
        //{
        //    decimal amount;
        //    decimal.TryParse(textBox金額税抜.Text, out amount);

        //    //2014.03: 2013年度改造
        //    //decimal amount_税抜 = Math.Ceiling(amount / (1m + NBaseCommon.Common.消費税率()));
        //    decimal amount_税抜 = Math.Ceiling(amount / (1m + GetTax()));
        //    decimal amount_税 = amount - amount_税抜;

        //    textBox金額税抜.Text = amount_税抜.ToString();
        //    textBox消費税.Text = amount_税.ToString();
        //}        
        
        //private decimal GetTax()
        //{
        //    DateTime date = dateTimePicker日付.Value;

        //    decimal tax = 0;
        //    foreach (MsTax msTax in SeninTableCache.instance().GetMsTaxList(NBaseCommon.Common.LoginUser))
        //    {
        //        if (msTax.StartDate < date)
        //        {
        //            tax = ((decimal)msTax.TaxRate / 100m);
        //        }
        //    }
        //    return tax;
        //}


        private MsTax GetTax()
        {
            DateTime date = dateTimePicker日付.Value;

            MsTax tax = null;
            List<MsTax> taxList = SeninTableCache.instance().GetMsTaxList(NBaseCommon.Common.LoginUser);
            // ID順で取得されるが
            // 2019/10/1 付けで　軽減8%、10% の２つがある。
            // 利用したいのは、10%のほうなので、リストを降順にする
            var sortedList = taxList.OrderByDescending(obj => obj.MsTaxID);
            foreach (MsTax msTax in sortedList)
            {
                if (msTax.StartDate < date)
                {
                    tax = msTax;
                    break;
                }
            }
            return tax;
        }



        private void button内税計算_Click(object sender, EventArgs e)
        {
            MsTax tax = GetTax();
            内税計算(tax.MsTaxID, tax.TaxRate);
        }

        private void button_内税8_Click(object sender, EventArgs e)
        {
            内税計算(2, 8);
        }
        
        private void button_内税10_Click(object sender, EventArgs e)
        {
            内税計算(3,10);
        }


        private void 内税計算(int id, decimal taxRate)
        {
            decimal amount;
            decimal.TryParse(textBox金額税抜.Text, out amount);

            decimal amount_税抜 = Math.Ceiling(amount / (1m + (taxRate / 100m)));
            decimal amount_税 = amount - amount_税抜;

            textBox金額税抜.Text = amount_税抜.ToString();
            textBox消費税.Text = amount_税.ToString();

            textBox_taxId.Text = id.ToString();
        }

    }
}
