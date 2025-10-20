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
using NBaseData.DS;
using NBaseUtil;
using NBaseHonsen.Senin.util;
using ORMapping;
using NBaseHonsen.util;

namespace NBaseHonsen.Senin
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
            InitComboBox明細();

            LoadGetsujiShime();
            
            InitFields();
        }


        private void LoadGetsujiShime()
        {
            SiGetsujiShime getsujiShime  = SiGetsujiShime.GetRecordByLastDate(NBaseCommon.Common.LoginUser);

            if (getsujiShime == null)
            {
                MessageBox.Show("直近の月次締めレコードが見つかりません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MsSiMeisai meisai = MsSiMeisai.GetRecord(NBaseCommon.Common.LoginUser, junbikin.MsSiMeisaiID);
                if (meisai != null)
                {
                    comboBox明細.Items.Add(meisai);
                    comboBox明細.SelectedItem = meisai;
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

                // 貸方
                if (comboBox明細.SelectedItem != null && (comboBox明細.SelectedItem as MsSiMeisai).KashiKariFlag == 0)
                {
                    textBox金額.Text = junbikin.KingakuIn.ToString();
                    textBox消費税.Text = junbikin.TaxIn.ToString();
                }
                // 借方
                else
                {
                    textBox金額.Text = junbikin.KingakuOut.ToString();
                    textBox消費税.Text = junbikin.TaxOut.ToString();
                }

                textBox備考.Text = junbikin.Bikou;

                // 締めた月のレコードまたは送金情報は編集できない.
                if (junbikin.JunbikinDate < getsujiShimeDate || Is_送金(junbikin))
                {
                    dateTimePicker日付.Enabled = false;
                    comboBox明細.Enabled = false;
                    textBox金額.ReadOnly = true;
                    textBox備考.ReadOnly = true;

                    button更新.Enabled = false;
                    button削除.Enabled = false;
                }
            }
            else
            {
                dateTimePicker日付.Value = DateTime.Now;
            }
        }


        private bool Is_送金(SiJunbikin junbikin)
        {
            SiSoukin soukin = SiSoukin.GetRecordByJunbikinId(NBaseCommon.Common.LoginUser, junbikin.SiJunbikinID);

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
            else if (!NumberUtils.Validate(textBox金額.Text) || textBox金額.Text.Length > 9)
            {
                textBox金額.BackColor = Color.Pink;
                MessageBox.Show("金額は半角数字9文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox金額.BackColor = Color.White;
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
            junbikin.MsSiKamokuId = SeninTableCache.instance().GetMsSiMeisai(NBaseCommon.Common.LoginUser, junbikin.MsSiMeisaiID).MsSiKamokuId;
            junbikin.JunbikinDate = dateTimePicker日付.Value;

            int k;
            Int32.TryParse(textBox金額.Text, out k);
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
            junbikin.TourokuUserID = NBaseCommon.Common.LoginUser.MsUserID;
        }


        private bool InsertOrUpdate()
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    if (junbikin.IsNew())
                    {
                        junbikin.SiJunbikinID = System.Guid.NewGuid().ToString();
                    }

                    junbikin.RenewDate = DateTime.Now;
                    junbikin.RenewUserID = 同期Client.LOGIN_USER.MsUserID;

                    SyncTableSaver.InsertOrUpdate(junbikin, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);

                    // 本船更新情報
                    MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum koumoku;

                    if (junbikin.DeleteFlag == 0)
                    {
                        koumoku = MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.船用金明細登録;
                    }
                    else
                    {
                        koumoku = MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.船用金明細削除;
                    }

                    MsSiMeisai meisai = SeninTableCache.instance().GetMsSiMeisai(NBaseCommon.Common.LoginUser, junbikin.MsSiMeisaiID);
                    PtHonsenkoushinInfo honsenkoushinInfo = 本船更新情報.CreateInstance(junbikin, koumoku, meisai);

                    SyncTableSaver.InsertOrUpdate(honsenkoushinInfo, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);

                    dbConnect.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    dbConnect.RollBack();
                    return false;
                }
            }
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


        private void comboBox明細_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Detect大項目();
            Detect費用科目();
        }
    }
}
