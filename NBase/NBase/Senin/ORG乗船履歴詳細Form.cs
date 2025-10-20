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
    public partial class ORG乗船履歴詳細Form : Form
    {
        private 船員詳細Panel parentForm;
        private SiCard card;
        private bool isNew;


        public ORG乗船履歴詳細Form(船員詳細Panel parentForm)
            : this(parentForm, new SiCard(), true)
        {
        }


        public ORG乗船履歴詳細Form(船員詳細Panel parentForm, SiCard card, bool isNew)
        {
            this.parentForm = parentForm;
            this.card = card;
            this.isNew = isNew;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox種別();
            InitComboBox船();
            InitComboBox職名();
            InitFields();
        }


        private void InitFields()
        {
            if (!isNew)
            {
                if (card.StartDate == DateTime.MinValue)
                {
                    nullableDateTimePicker開始.Value = null;
                }
                else
                {
                    nullableDateTimePicker開始.Value = card.StartDate;
                }

                if (card.EndDate == DateTime.MinValue)
                {
                    nullableDateTimePicker終了.Value = null;
                }
                else
                {
                    nullableDateTimePicker終了.Value = card.EndDate;
                }

                // 2012.03 同日転船に対応
                //textBox日数.Text = card.Days.ToString();
                if (card.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_旅行日_転船ID(NBaseCommon.Common.LoginUser) && card.Days < 0)
                {
                    textBox日数.Text = "0";
                    checkBox転船.Checked = true;
                    checkBox転船.Enabled = true;
                }
                else
                {
                    textBox日数.Text = card.Days.ToString();
                    checkBox転船.Enabled = false;
                }

                comboBox種別.SelectedItem = SeninTableCache.instance().GetMsSiShubetsu(NBaseCommon.Common.LoginUser, card.MsSiShubetsuID);

                if (SeninTableCache.instance().Is_乗船中(NBaseCommon.Common.LoginUser, card.MsSiShubetsuID))
                {
                    comboBox船.SelectedItem = SeninTableCache.instance().GetMsVessel(NBaseCommon.Common.LoginUser, card.MsVesselID);

                    foreach (SiLinkShokumeiCard link in card.SiLinkShokumeiCards)
                    {
                        for (int i = 0; i < checkedListBox職名.Items.Count; i++)
                        {
                            if (checkedListBox職名.Items[i] == SeninTableCache.instance().GetMsSiShokumei(NBaseCommon.Common.LoginUser, link.MsSiShokumeiID))
                            {
                                checkedListBox職名.SetItemChecked(i, true);
                                break;
                            }
                        }
                    }
                }

                EnableComponents();

                if (Is_休暇管理_ByKyuukaFlag((comboBox種別.SelectedItem as MsSiShubetsu).KyuukaFlag))
                {
                    comboBox種別.Enabled = false;
                    button削除.Enabled = false;
                }
            }
            else
            {
                nullableDateTimePicker開始.Value = nullableDateTimePicker終了.Value = DateTime.Now;
                checkBox転船.Enabled = false;
            }
        }


        private void InitComboBox種別()
        {
            if (!SeninTableCache.instance().Is_休暇管理(NBaseCommon.Common.LoginUser, card.MsSiShubetsuID))
            {
                foreach (MsSiShubetsu s in SeninTableCache.instance().GetMsSiShubetsuList(NBaseCommon.Common.LoginUser))
                {
                    if (!Is_休暇管理_ByKyuukaFlag(s.KyuukaFlag))
                    {
                        comboBox種別.Items.Add(s);
                    }
                }
            }
            else
            {
                foreach (MsSiShubetsu s in SeninTableCache.instance().GetMsSiShubetsuList(NBaseCommon.Common.LoginUser))
                {
                    if (Is_休暇管理_ByKyuukaFlag(s.KyuukaFlag))
                    {
                        comboBox種別.Items.Add(s);
                    }
                }
            }
        }


        private void InitComboBox船()
        {
            foreach (MsVessel v in SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser))
            {
                //comboBox船.Items.Add(v);

                //-------------------------------------------------
                //m.yoshihara 2017/6/2
                if (isNew)
                {
                    //新規の場合は機能チェックついてるものだけ表示
                    if (v.SeninEnabled == 1)
                    {
                        comboBox船.Items.Add(v);
                    }
                }
                else 
                {
                    //新規じゃない場合は実績も含め表示
                    comboBox船.Items.Add(v);
                }
                //-------------------------------------------------
            }
        }


        private void InitComboBox職名()
        {
            foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                checkedListBox職名.Items.Add(s);
            }
        }


        private void nullableDateTimePicker開始_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox転船.Checked)
                return;

            MsSiShubetsu s = comboBox種別.SelectedItem as MsSiShubetsu;

            if (s != null && nullableDateTimePicker開始.Value != null &&
                s.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_乗船休暇ID(NBaseCommon.Common.LoginUser))
            {
                nullableDateTimePicker終了.Value = DateTimeUtils.ToTo((DateTime)nullableDateTimePicker開始.Value).AddSeconds(-1);
            }

            日数計算();
        }


        private void nullableDateTimePicker終了_ValueChanged(object sender, EventArgs e)
        {
            日数計算();
        }


        private void 日数計算()
        {
            if (nullableDateTimePicker開始.Value != null && nullableDateTimePicker終了.Value != null)
            {
                textBox日数.Text = StringUtils.ToStr((DateTime)nullableDateTimePicker開始.Value, (DateTime)nullableDateTimePicker終了.Value);
            }
            else if (nullableDateTimePicker開始.Value == null)
            {
                textBox日数.Text = "0";
            }
            else if (nullableDateTimePicker終了.Value == null)
            {
                textBox日数.Text = StringUtils.ToStr((DateTime)nullableDateTimePicker開始.Value, DateTime.Now);
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

                parentForm.AddSiCard(card, isNew);

                Dispose();
            }
        }


        private bool ValidateFields()
        {
            if (nullableDateTimePicker開始.Value == null)
            {
                nullableDateTimePicker開始.BackColor = Color.Pink;
                MessageBox.Show("開始日を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nullableDateTimePicker開始.BackColor = Color.White;
                return false;
            }

            if (comboBox種別.SelectedItem == null)
            {
                comboBox種別.BackColor = Color.Pink;
                MessageBox.Show("種別を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox種別.BackColor = Color.White;
                return false;
            }

            if (!NumberUtils.Validate(textBox日数.Text))
            {
                textBox日数.BackColor = Color.Pink;
                MessageBox.Show("日数を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox日数.BackColor = Color.White;
                return false;
            }

            if (SeninTableCache.instance().Is_乗船中(NBaseCommon.Common.LoginUser, (comboBox種別.SelectedItem as MsSiShubetsu).MsSiShubetsuID) && comboBox船.SelectedItem == null)
            {
                comboBox船.BackColor = Color.Pink;
                MessageBox.Show("船を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox船.BackColor = Color.White;
                return false;
            }

            if (SeninTableCache.instance().Is_乗船中(NBaseCommon.Common.LoginUser, (comboBox種別.SelectedItem as MsSiShubetsu).MsSiShubetsuID) && checkedListBox職名.CheckedItems.Count == 0)
            {
                checkedListBox職名.BackColor = Color.Pink;
                MessageBox.Show("職名を1つ以上選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkedListBox職名.BackColor = Color.White;
                return false;
            }

            if (nullableDateTimePicker開始.Value != null && nullableDateTimePicker終了.Value != null && (DateTime)(nullableDateTimePicker開始.Value) > (DateTime)(nullableDateTimePicker終了.Value))
            {
                nullableDateTimePicker開始.BackColor = Color.Pink;
                nullableDateTimePicker終了.BackColor = Color.Pink;
                MessageBox.Show("開始日が終了日より後の日付です", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nullableDateTimePicker開始.BackColor = Color.White;
                nullableDateTimePicker終了.BackColor = Color.White;
                return false;
            }

            if (checkBox転船.Enabled && checkBox転船.Checked == true)
            {
                // 同日転船の場合、期間の重複チェックはしない
                return true;
            }

            if (!Check_期間重複())
            {
                nullableDateTimePicker開始.BackColor = Color.Pink;
                nullableDateTimePicker終了.BackColor = Color.Pink;
                MessageBox.Show("入力された期間が他の船員カードの期間と重複しています", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nullableDateTimePicker開始.BackColor = Color.White;
                nullableDateTimePicker終了.BackColor = Color.White;
                return false;
            }

            return true;
        }


        private bool Check_期間重複()
        {
            // 休暇管理のレコードは重複可.
            if (SeninTableCache.instance().Is_休暇管理(NBaseCommon.Common.LoginUser, card.MsSiShubetsuID))
            {
                return true;
            }

            DateTime start = DateTime.MinValue;
            DateTime end = DateTime.MinValue;

            if (nullableDateTimePicker開始.Value != null)
            {
                start = (DateTime)nullableDateTimePicker開始.Value;
            }

            if (nullableDateTimePicker終了.Value != null)
            {
                end = (DateTime)nullableDateTimePicker終了.Value;
            }

            // 2012.03 終了が null の場合、当日で確認しないで問題ないのか？（要確認）
            if (end == DateTime.MinValue)
            {
                end = DateTime.Now;
            }

            return parentForm.SiCard_期間重複チェック(card.SiCardID, (comboBox種別.SelectedItem as MsSiShubetsu).MsSiShubetsuID, card, start, end);
        }


        private void FillInstance()
        {

            card.MsSiShubetsuID = (comboBox種別.SelectedItem as MsSiShubetsu).MsSiShubetsuID;


            if (nullableDateTimePicker開始.Value is DateTime)
            {
                card.StartDate = DateTimeUtils.ToFrom((DateTime)nullableDateTimePicker開始.Value);
            }

            if (nullableDateTimePicker終了.Value is DateTime)
            {
                // 2012.08:乗船の時のみ本船と合わせる
                //// 2012.03 本船と合わせる
                ////card.EndDate = DateTimeUtils.ToTo((DateTime)nullableDateTimePicker終了.Value).AddSeconds(-1);
                //card.EndDate = DateTimeUtils.ToFrom((DateTime)nullableDateTimePicker終了.Value);
                if (card.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser))
                {
                    card.EndDate = DateTimeUtils.ToFrom((DateTime)nullableDateTimePicker終了.Value);
                }
                else
                {
                    card.EndDate = DateTimeUtils.ToTo((DateTime)nullableDateTimePicker終了.Value).AddSeconds(-1);
                }
            }
            else if (nullableDateTimePicker終了.Value == null)
            {
                card.EndDate = DateTime.MinValue;
            }

            // 2012.03 同日転船対応
            //card.Days = int.Parse(textBox日数.Text);
            if (checkBox転船.Checked)
            {
                card.Days = -1;
                card.EndDate = card.StartDate;
            }
            else
            {
                card.Days = int.Parse(textBox日数.Text);
            }

            Dictionary<int, SiLinkShokumeiCard> linkDic = CreateLinkDic();

            if (SeninTableCache.instance().Is_乗船中(NBaseCommon.Common.LoginUser, card.MsSiShubetsuID))
            {
                card.MsVesselID = (short)(comboBox船.SelectedItem as MsVessel).MsVesselID;

                for (int i = 0; i < checkedListBox職名.Items.Count; i++)
                {
                    MsSiShokumei s = checkedListBox職名.Items[i] as MsSiShokumei;

                    if (checkedListBox職名.GetItemChecked(i))
                    {
                        if (!linkDic.ContainsKey(s.MsSiShokumeiID))
                        {
                            SiLinkShokumeiCard link = new SiLinkShokumeiCard();

                            link.MsSiShokumeiID = s.MsSiShokumeiID;

                            card.SiLinkShokumeiCards.Add(link);
                        }
                    }
                    else
                    {
                        if (linkDic.ContainsKey(s.MsSiShokumeiID))
                        {
                            SiLinkShokumeiCard link = linkDic[s.MsSiShokumeiID];

                            link.DeleteFlag = 1;
                        }
                    }
                }
            }
            else
            {
                card.MsVesselID = short.MinValue;
            }
        }


        private Dictionary<int, SiLinkShokumeiCard> CreateLinkDic()
        {
            Dictionary<int, SiLinkShokumeiCard> linkDic = new Dictionary<int, SiLinkShokumeiCard>();

            foreach (SiLinkShokumeiCard link in card.SiLinkShokumeiCards)
            {
                linkDic[link.MsSiShokumeiID] = link;
            }

            return linkDic;
        }


        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                card.DeleteFlag = 1;

                foreach (SiLinkShokumeiCard link in card.SiLinkShokumeiCards)
                {
                    card.DeleteFlag = 1;
                }

                Save();
            }
        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        private void comboBox種別_SelectionChangeCommitted(object sender, EventArgs e)
        {
            EnableComponents();
        }


        private void EnableComponents()
        {
            MsSiShubetsu s = comboBox種別.SelectedItem as MsSiShubetsu;

            if (SeninTableCache.instance().Is_乗船中(NBaseCommon.Common.LoginUser, s.MsSiShubetsuID))
            {
                nullableDateTimePicker終了.Enabled = true;

                textBox日数.ReadOnly = true;

                comboBox船.Enabled = true;
                checkedListBox職名.Enabled = true;

                if (!SeninTableCache.instance().Is_乗船(NBaseCommon.Common.LoginUser, s.MsSiShubetsuID))
                {
                    nullableDateTimePicker終了.Value = DateTimeUtils.ToTo((DateTime)nullableDateTimePicker開始.Value).AddSeconds(-1);
                    nullableDateTimePicker終了.Enabled = false;
                }
            }
            else if (Is_休暇管理_ByKyuukaFlag(s.KyuukaFlag))
            {
                // 2010.04.01:aki
                nullableDateTimePicker開始.Enabled = false;
                nullableDateTimePicker終了.Enabled = false;
                //nullableDateTimePicker終了.Enabled = false;
                //nullableDateTimePicker終了.Value = DateTimeUtils.年度終了日();
                
                textBox日数.ReadOnly = false;

                comboBox船.Enabled = false;
                checkedListBox職名.Enabled = false;
            }
            else
            {
                nullableDateTimePicker終了.Enabled = true;

                textBox日数.ReadOnly = true;

                comboBox船.Enabled = false;
                checkedListBox職名.Enabled = false;
            }
        }


        private static bool Is_休暇管理_ByKyuukaFlag(int kyuukaFlag)
        {
            return kyuukaFlag == (int)MsSiShubetsu.KyuukaFlagKind.休暇買上 ||
                        kyuukaFlag == (int)MsSiShubetsu.KyuukaFlagKind.本年度休暇日数 ||
                        kyuukaFlag == (int)MsSiShubetsu.KyuukaFlagKind.前年度休暇繰越日数;
        }



        private void comboBox種別_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool 転船flag = false;
            if (comboBox種別.SelectedItem is MsSiShubetsu)
            {
                if ((comboBox種別.SelectedItem as MsSiShubetsu).MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_旅行日_転船ID(NBaseCommon.Common.LoginUser))
                {
                    転船flag = true;
                }
            }
            if (転船flag)
            {
                checkBox転船.Enabled = true;
            }
            else
            {
                checkBox転船.Checked = false;
                checkBox転船.Enabled = false;
            }
        }

        private void checkBox転船_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Checked)
            {
                nullableDateTimePicker終了.Value = null;
                nullableDateTimePicker終了.Enabled = false;
                textBox日数.ReadOnly = true;
                textBox日数.Text = "0";
            }
            else
            {
                EnableComponents();
                日数計算();
            }
        }
    }
}
