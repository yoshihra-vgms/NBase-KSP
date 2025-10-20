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
using Senin.util;


namespace Senin
{
    public partial class 乗船履歴詳細Form : Form
    {
        //2021/07/26 変数名parentForm→parentPanelに変更
        //private 船員詳細Panel parentForm;
        private 船員詳細Panel parentPanel;
        private SiCard card;
        private bool isNew;

        private MsBasho basho乗船場所;

        private MsBasho basho下船場所;
        private SiCard card交代者;


        public 乗船履歴詳細Form(船員詳細Panel parentPanel)//2021/07/26 parentForm→parentPanelに変更
            : this(parentPanel, new SiCard(), true)
        {
        }


        public 乗船履歴詳細Form(船員詳細Panel parentPanel, SiCard card, bool isNew)//2021/07/26 parentForm→parentPanelに変更
        {
            this.parentPanel = parentPanel;
            this.card = card;
            this.isNew = isNew;        

            InitializeComponent();
            Init();
        }

        private void 乗船履歴詳細Form_Load(object sender, EventArgs e)
        {
        }

        private void Init()
        {
            InitComboBox種別();
            InitComboBox種別詳細();
            InitComboBox船();
            InitComboBox船タイプ();
            InitComboBox職名();
            InitComboBoxCrewMatrixType();
            InitComboBoxNavigationArea();
            InitComboBox下船理由();

            panel事務所.Visible = false;

            InitFields();
        }


        private void InitFields()
        {
            textBox会社名.Text = NBaseCommon.Common.船員_自社名;

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

                if (card.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.転船) && card.Days < 0)
                {
                    textBox日数.Text = "0";
                    checkBox転船.Checked = true;
                    checkBox転船.Enabled = true;
                }
                else
                {
                    if (SeninTableCache.instance().Is_休暇管理(NBaseCommon.Common.LoginUser, card.MsSiShubetsuID))
                    {
                        textBox日数.Text = card.Days.ToString();
                    }
                    else if (card.EndDate == DateTime.MinValue || (DateTimeUtils.ToFrom(card.StartDate) <= DateTime.Now && DateTime.Now < DateTimeUtils.ToTo(card.EndDate)))
                    {
                        textBox日数.Text = StringUtils.ToStr(card.StartDate, DateTime.Now);
                    }
                    else
                    {
                        textBox日数.Text = StringUtils.ToStr(card.StartDate, card.EndDate);
                    }

                    checkBox転船.Enabled = false;
                }

                comboBox種別.SelectedItem = SeninTableCache.instance().GetMsSiShubetsu(NBaseCommon.Common.LoginUser, card.MsSiShubetsuID);

                comboBox種別詳細.SelectedItem = SeninTableCache.instance().GetMsSiShubetsuShousai(NBaseCommon.Common.LoginUser, card.MsSiShubetsuShousaiID);

                if (SeninTableCache.instance().Is_乗船中(NBaseCommon.Common.LoginUser, card.MsSiShubetsuID))
                {
                    //comboBox船.SelectedItem = SeninTableCache.instance().GetMsVessel(NBaseCommon.Common.LoginUser, card.MsVesselID);
                    if (card.MsVesselID > 0)
                    {
                        comboBox船.SelectedItem = SeninTableCache.instance().GetMsVessel(NBaseCommon.Common.LoginUser, card.MsVesselID);
                    }

                    if (card.LaborOnBoarding == (int)SiCard.LABOR.労働)
                        radioButton労働区分乗船_労働.Checked = true;
                    else if (card.LaborOnBoarding == (int)SiCard.LABOR.半休)
                        radioButton労働区分乗船_半休.Checked = true;
                    else if (card.LaborOnBoarding == (int)SiCard.LABOR.全休)
                        radioButton労働区分乗船_全休.Checked = true;

                    if (card.LaborOnDisembarking == (int)SiCard.LABOR.労働)
                        radioButton労働区分下船_労働.Checked = true;
                    else if (card.LaborOnDisembarking == (int)SiCard.LABOR.半休)
                        radioButton労働区分下船_半休.Checked = true;
                    else if (card.LaborOnDisembarking == (int)SiCard.LABOR.全休)
                        radioButton労働区分下船_全休.Checked = true;

                    // 乗船時、兼務（職名の複数選択）はなしとする
                    #region
                    //foreach (SiLinkShokumeiCard link in card.SiLinkShokumeiCards)
                    //{
                    //    for (int i = 0; i < checkedListBox職名.Items.Count; i++)
                    //    {
                    //        if (checkedListBox職名.Items[i] == SeninTableCache.instance().GetMsSiShokumei(NBaseCommon.Common.LoginUser, link.MsSiShokumeiID))
                    //        {
                    //            checkedListBox職名.SetItemChecked(i, true);
                    //            break;
                    //        }
                    //    }
                    //}
                    #endregion
                    // 乗船職を使用する用に改造
                    #region
                    //if (card.SiLinkShokumeiCards.Count == 0)
                    //{
                    //    comboBox職名.SelectedItem = SeninTableCache.instance().GetMsSiShokumei(NBaseCommon.Common.LoginUser, card.SeninMsSiShokumeiID);
                    //}
                    //else
                    //{
                    //    foreach (SiLinkShokumeiCard link in card.SiLinkShokumeiCards)
                    //    {
                    //        comboBox職名.SelectedItem = SeninTableCache.instance().GetMsSiShokumei(NBaseCommon.Common.LoginUser, link.MsSiShokumeiID);
                    //    }
                    //}
                    #endregion


                    if (card.SiLinkShokumeiCards.Count == 0)
                    {
                        comboBox職名.SelectedItem = GetShokumei(card.SeninMsSiShokumeiID, 0);
                    }
                    else
                    {
                        comboBox職名.SelectedItem = GetShokumei(card.SiLinkShokumeiCards[0].MsSiShokumeiID, card.SiLinkShokumeiCards[0].MsSiShokumeiShousaiID);
                    }
                }

                textBox船名.Text = card.VesselName;
                textBox会社名.Text = card.CompanyName;
                comboBoxCrewMatrixType.Text = card.MsCrewMatrixTypeID > 0 ? SeninTableCache.instance().GetMsCrewMatrixTypeName(NBaseCommon.Common.LoginUser, card.MsCrewMatrixTypeID) : "";

                comboBox船タイプ.Text = "";
                if (card.MsVesselTypeID != null)
                {
                    foreach (object item in comboBox船タイプ.Items)
                    {
                        if (item is MsVesselType && (item as MsVesselType).MsVesselTypeID == card.MsVesselTypeID)
                        {
                            comboBox船タイプ.SelectedItem = item;
                            break;
                        }

                    }
                }
                textBoxGRT.Text = card.GrossTon;
                textBoxオーナー.Text = card.OwnerName;
                comboBox_NavigationArea.Text = card.NavigationArea;


                checkBox兼務通信長.Checked = card.KenmTushincyo == 1 ? true : false;
                nullableDateTimePicker兼務通信長開始.Value = null;
                nullableDateTimePicker兼務通信長終了.Value = null;
                if (card.KenmTushincyoStart != DateTime.MinValue) nullableDateTimePicker兼務通信長開始.Value = card.KenmTushincyoStart;
                if (card.KenmTushincyoEnd != DateTime.MinValue) nullableDateTimePicker兼務通信長終了.Value = card.KenmTushincyoEnd;

                if (StringUtils.Empty(card.SignOnBashoID) == false)
                {
                    basho乗船場所 = SeninTableCache.instance().GetMsBasho(NBaseCommon.Common.LoginUser, card.SignOnBashoID);
                    if (basho乗船場所 != null)
                        textBox乗船場所.Text = basho乗船場所.BashoName;
                }

                comboBoxSignOffReason.Text = SiCard.ConvertSignOffReasonStrings(card.SignOffReason);
                if (StringUtils.Empty(card.SignOffBashoID) == false)
                {
                    basho下船場所 = SeninTableCache.instance().GetMsBasho(NBaseCommon.Common.LoginUser, card.SignOffBashoID);
                    if (basho下船場所 != null)
                        textBox下船場所.Text = basho下船場所.BashoName;
                }
                if (StringUtils.Empty(card.ReplacementID) == false)
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        card交代者 = serviceClient.SiCard_GetRecord(NBaseCommon.Common.LoginUser, card.ReplacementID);
                    }
                    if (card交代者 != null)
                        textBox交代者.Text = card交代者.SeninName;
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
                nullableDateTimePicker開始.Value = DateTime.Today;
                nullableDateTimePicker終了.Value = null;
                textBox日数.Text = "1";
                checkBox転船.Enabled = false;


                panel日程.Visible = false;
                this.Height = 145;

            }
        }

        #region private void InitComboBox種別()
        private void InitComboBox種別()
        {
            // 本年度休暇日数（XX)を追加できるように改造
            //if (!SeninTableCache.instance().Is_休暇管理(NBaseCommon.Common.LoginUser, card.MsSiShubetsuID))
            //{
            //    foreach (MsSiShubetsu s in SeninTableCache.instance().GetMsSiShubetsuList(NBaseCommon.Common.LoginUser))
            //    {
            //        if (!Is_休暇管理_ByKyuukaFlag(s.KyuukaFlag))
            //        {
            //            comboBox種別.Items.Add(s);
            //        }
            //    }
            //}
            //else
            //{
            //    foreach (MsSiShubetsu s in SeninTableCache.instance().GetMsSiShubetsuList(NBaseCommon.Common.LoginUser))
            //    {
            //        if (Is_休暇管理_ByKyuukaFlag(s.KyuukaFlag))
            //        {
            //            comboBox種別.Items.Add(s);
            //        }
            //    }
            //}
            foreach (MsSiShubetsu s in SeninTableCache.instance().GetMsSiShubetsuList(NBaseCommon.Common.LoginUser))
            {
                if (s.KyuukaFlag != (int)MsSiShubetsu.KyuukaFlagKind.休暇買上 || card.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.休暇買上))
                {
                    comboBox種別.Items.Add(s);
                }
            }
        }
        #endregion

        #region private void InitComboBox種別詳細()
        private void InitComboBox種別詳細()
        {
            int id = SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.陸上勤務);

            var list = SeninTableCache.instance().GetMsSiShubetsuShousaiList(NBaseCommon.Common.LoginUser).Where(o => o.MsSiShubetsuID == id);
            foreach (MsSiShubetsuShousai s in list)
            {
                comboBox種別詳細.Items.Add(s);
            }
        }
        #endregion


        #region private void InitComboBox船()
        private void InitComboBox船()
        {
            comboBox船.Items.Add("");
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
        #endregion

        #region private void InitComboBox船タイプ()
        private void InitComboBox船タイプ()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                var vesselTypeList = serviceClient.MsVesselType_GetRecords(NBaseCommon.Common.LoginUser);

                comboBox船タイプ.Items.Add("");
                foreach (MsVesselType v in vesselTypeList)
                {
                    comboBox船タイプ.Items.Add(v);
                }
            }
        }
        #endregion


        #region private void InitComboBox職名()
        private void InitComboBox職名()
        {
            //foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            //{
            //    checkedListBox職名.Items.Add(s);
            //}

            foreach (var s in SeninTableCache.instance().GetShokumeiList(NBaseCommon.Common.LoginUser, Shokumei.フェリー))
            {
                comboBox職名.Items.Add(s);
            }
        }
        #endregion

        #region private void InitComboBoxCrewMatrixType()
        private void InitComboBoxCrewMatrixType()
        {
            comboBoxCrewMatrixType.Items.Add("");
            foreach (MsCrewMatrixType t in SeninTableCache.instance().GetMsCrewMatrixTypeList(NBaseCommon.Common.LoginUser))
            {
                comboBoxCrewMatrixType.Items.Add(t);
            }
        }
        #endregion

        #region private void InitComboBoxNavigationArea()
        private void InitComboBoxNavigationArea()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                comboBox_NavigationArea.Items.Clear();
                comboBox_NavigationArea.Items.AddRange(NBaseData.DAC.MsVessel.NavigationAreaStrings.ToArray());
            }
        }
        #endregion

        #region private void InitComboBox下船理由()
        private void InitComboBox下船理由()
        {
            comboBoxSignOffReason.Items.AddRange(SiCard.SignOffReasonStrings.ToArray());
        }
        #endregion






        #region private void nullableDateTimePicker開始_ValueChanged(object sender, EventArgs e)
        private void nullableDateTimePicker開始_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox転船.Checked)
                return;

            MsSiShubetsu s = comboBox種別.SelectedItem as MsSiShubetsu;

            if (s != null && nullableDateTimePicker開始.Value != null &&
                s.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船休暇))
            {
                nullableDateTimePicker終了.Value = DateTimeUtils.ToTo((DateTime)nullableDateTimePicker開始.Value).AddSeconds(-1);
            }

            日数計算();
        }
        #endregion

        #region private void nullableDateTimePicker終了_ValueChanged(object sender, EventArgs e)
        private void nullableDateTimePicker終了_ValueChanged(object sender, EventArgs e)
        {
            日数計算();
        }
        #endregion

        #region private void 日数計算()
        private void 日数計算()
        {
            MsSiShubetsu s = comboBox種別.SelectedItem as MsSiShubetsu;
            if (s == null || Is_休暇管理_ByKyuukaFlag(s.KyuukaFlag))
                return;

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
        #endregion

        #region private void button更新_Click(object sender, EventArgs e)
        private void button更新_Click(object sender, EventArgs e)
        {
            Save();
        }
        #endregion

        #region private void Save()
        private void Save()
        {
            if (ValidateFields())
            {
                FillInstance();

                //----------------------------------------
                //2021/07/20 m.yoshihar DB更新処理追加
                if (parentPanel.InsertOrUpdate_乗船履歴(card))
                {
                    MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //---------------------------------------

                //グリッド更新
                parentPanel.Refresh船員カード();

                Dispose();
            }
        }
        #endregion

        #region private bool ValidateFields()
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

            //if (SeninTableCache.instance().Is_乗船中(NBaseCommon.Common.LoginUser, (comboBox種別.SelectedItem as MsSiShubetsu).MsSiShubetsuID) && comboBox船.SelectedItem == null)
            //{
            //    comboBox船.BackColor = Color.Pink;
            //    MessageBox.Show("船を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    comboBox船.BackColor = Color.White;
            //    return false;
            //}
            if (SeninTableCache.instance().Is_乗船中(NBaseCommon.Common.LoginUser, (comboBox種別.SelectedItem as MsSiShubetsu).MsSiShubetsuID) 
                && textBox船名.Text != null && textBox船名.Text.Length == 0)
            {
                textBox船名.BackColor = Color.Pink;
                MessageBox.Show("船を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox船名.BackColor = Color.White;
                return false;
            }

            //if (SeninTableCache.instance().Is_乗船中(NBaseCommon.Common.LoginUser, (comboBox種別.SelectedItem as MsSiShubetsu).MsSiShubetsuID) && checkedListBox職名.CheckedItems.Count == 0)
            //{
            //    checkedListBox職名.BackColor = Color.Pink;
            //    MessageBox.Show("職名を1つ以上選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    checkedListBox職名.BackColor = Color.White;
            //    return false;
            //}
            if (SeninTableCache.instance().Is_乗船中(NBaseCommon.Common.LoginUser, (comboBox種別.SelectedItem as MsSiShubetsu).MsSiShubetsuID) && comboBox職名.SelectedItem == null)
            {
                comboBox職名.BackColor = Color.Pink;
                MessageBox.Show("職名を選択してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox職名.BackColor = Color.White;
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

            if (card.DeleteFlag == 1)
            {
                // 削除の場合、期間の重複チェックはしない
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


            // 2017.12 2017年度改造
            if (checkBox兼務通信長.Checked)
            {
                if (nullableDateTimePicker兼務通信長開始.Value == null)
                {
                    checkBox兼務通信長.BackColor = Color.Pink;
                    MessageBox.Show("兼務通信長の開始日を選択してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    checkBox兼務通信長.BackColor = Color.White;
                    return false;
                }
                else if (nullableDateTimePicker兼務通信長終了.Value != null && (DateTime)(nullableDateTimePicker兼務通信長開始.Value) > (DateTime)(nullableDateTimePicker兼務通信長終了.Value))
                {
                    checkBox兼務通信長.BackColor = Color.Pink;
                    MessageBox.Show("兼務通信長の開始日が終了日より後の日付です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    checkBox兼務通信長.BackColor = Color.White;
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region private bool Check_期間重複()
        private bool Check_期間重複()
        {
            // 休暇管理のレコードは重複可.
            //if (SeninTableCache.instance().Is_休暇管理(NBaseCommon.Common.LoginUser, card.MsSiShubetsuID))
            if (SeninTableCache.instance().Is_休暇管理(NBaseCommon.Common.LoginUser, (comboBox種別.SelectedItem as MsSiShubetsu).MsSiShubetsuID))
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
                //end = DateTime.Now;
                end = DateTime.MaxValue.AddDays(-1);
            }

            return parentPanel.SiCard_期間重複チェック(card.SiCardID, (comboBox種別.SelectedItem as MsSiShubetsu).MsSiShubetsuID, card, start, end);
        }
        #endregion

        #region private void FillInstance()
        private void FillInstance()
        {
            card.MsSiShubetsuID = (comboBox種別.SelectedItem as MsSiShubetsu).MsSiShubetsuID;

            if (panel事務所.Visible)
            {
                card.MsSiShubetsuShousaiID = (comboBox種別詳細.SelectedItem is MsSiShubetsuShousai) ? (comboBox種別詳細.SelectedItem as MsSiShubetsuShousai).MsSiShubetsuShousaiID : 0;
            }
            else
            {
                card.MsSiShubetsuShousaiID = 0;
            }

            if (nullableDateTimePicker開始.Value is DateTime)
            {
                card.StartDate = DateTimeUtils.ToFrom((DateTime)nullableDateTimePicker開始.Value);
            }

            if (nullableDateTimePicker終了.Value is DateTime)
            {
                if (card.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船))
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
                if (comboBox船.SelectedItem is MsVessel)
                {
                    card.MsVesselID = (short)(comboBox船.SelectedItem as MsVessel).MsVesselID;
                }
                else
                {
                    card.MsVesselID = short.MinValue;
                }

                if (radioButton労働区分乗船_労働.Checked)
                    card.LaborOnBoarding = (int)SiCard.LABOR.労働;
                else if (radioButton労働区分乗船_半休.Checked)
                    card.LaborOnBoarding = (int)SiCard.LABOR.半休;
                else if (radioButton労働区分乗船_全休.Checked)
                    card.LaborOnBoarding = (int)SiCard.LABOR.全休;

                if (radioButton労働区分下船_労働.Checked)
                    card.LaborOnDisembarking = (int)SiCard.LABOR.労働;
                else if (radioButton労働区分下船_半休.Checked)
                    card.LaborOnDisembarking = (int)SiCard.LABOR.半休;
                else if (radioButton労働区分下船_全休.Checked)
                    card.LaborOnDisembarking = (int)SiCard.LABOR.全休;


                // 乗船時、兼務での乗船（複数の職名を選択）はなしとする
                #region
                //for (int i = 0; i < checkedListBox職名.Items.Count; i++)
                //{
                //    MsSiShokumei s = checkedListBox職名.Items[i] as MsSiShokumei;

                //    if (checkedListBox職名.GetItemChecked(i))
                //    {
                //        if (!linkDic.ContainsKey(s.MsSiShokumeiID))
                //        {
                //            SiLinkShokumeiCard link = new SiLinkShokumeiCard();

                //            link.MsSiShokumeiID = s.MsSiShokumeiID;

                //            card.SiLinkShokumeiCards.Add(link);
                //        }
                //    }
                //    else
                //    {
                //        if (linkDic.ContainsKey(s.MsSiShokumeiID))
                //        {
                //            SiLinkShokumeiCard link = linkDic[s.MsSiShokumeiID];

                //            link.DeleteFlag = 1;
                //        }
                //    }
                //}
                #endregion

                // 職名（乗船職）を選択できるように改造
                //MsSiShokumei s = (comboBox職名.SelectedItem as MsSiShokumei);

                //if (card.SiLinkShokumeiCards.Count == 0)
                //{
                //    SiLinkShokumeiCard link = new SiLinkShokumeiCard();
                //    link.MsSiShokumeiID = s.MsSiShokumeiID;
                //    card.SiLinkShokumeiCards.Add(link);
                //}
                //else
                //{
                //    card.SiLinkShokumeiCards[0].MsSiShokumeiID = s.MsSiShokumeiID;
                //}
                //card.CardMsSiShokumeiID = s.MsSiShokumeiID;

                Shokumei s = (comboBox職名.SelectedItem as Shokumei);

                if (card.SiLinkShokumeiCards.Count == 0)
                {
                    SiLinkShokumeiCard link = new SiLinkShokumeiCard();
                    link.MsSiShokumeiID = s.MsSiShokumeiID;
                    link.MsSiShokumeiShousaiID = s.MsSiShokumeiShousaiID;
                    card.SiLinkShokumeiCards.Add(link);
                }
                else
                {
                    card.SiLinkShokumeiCards[0].MsSiShokumeiID = s.MsSiShokumeiID;
                    card.SiLinkShokumeiCards[0].MsSiShokumeiShousaiID = s.MsSiShokumeiShousaiID;
                }
                card.CardMsSiShokumeiID = s.MsSiShokumeiID;


                card.VesselName = textBox船名.Text;
                card.CompanyName = textBox会社名.Text;
                if (comboBoxCrewMatrixType.SelectedItem is MsCrewMatrixType)
                {
                    card.MsCrewMatrixTypeID = (comboBoxCrewMatrixType.SelectedItem as MsCrewMatrixType).MsCrewMatrixTypeID;
                }
                else
                {
                    card.MsCrewMatrixTypeID = 0;
                }
                card.KenmTushincyo = checkBox兼務通信長.Checked ? 1 : 0;
                card.KenmTushincyoStart = DateTime.MinValue;
                card.KenmTushincyoEnd = DateTime.MinValue;
                if (checkBox兼務通信長.Checked)
                {
                    if (nullableDateTimePicker兼務通信長開始.Value is DateTime)
                    {
                        card.KenmTushincyoStart = DateTimeUtils.ToFrom((DateTime)nullableDateTimePicker兼務通信長開始.Value);
                    }
                    if (nullableDateTimePicker兼務通信長終了.Value is DateTime)
                    {
                        card.KenmTushincyoEnd = DateTimeUtils.ToFrom((DateTime)nullableDateTimePicker兼務通信長終了.Value);
                    }
                }



                card.MsVesselTypeID = (comboBox船タイプ.SelectedItem is MsVesselType ? (comboBox船タイプ.SelectedItem as MsVesselType).MsVesselTypeID : "");
                card.GrossTon = textBoxGRT.Text;
                card.NavigationArea = comboBox_NavigationArea.Text;
                card.OwnerName = textBoxオーナー.Text;



                // 乗船タブ
                #region
                if (basho乗船場所 != null)
                {
                    card.SignOnBashoID = basho乗船場所.MsBashoId;
                }
                else
                {
                    card.SignOnBashoID = null;
                }
                #endregion

                // 下船タブ
                #region

                card.SignOffReason = SiCard.ConvertSignOffReasonId(comboBoxSignOffReason.Text);

                if (basho下船場所 != null)
                {
                    card.SignOffBashoID = basho下船場所.MsBashoId;
                }
                else
                {
                    card.SignOffBashoID = null;
                }
                if (card交代者 != null)
                {
                    card.ReplacementID = card交代者.SiCardID;
                }
                else
                {
                    card.ReplacementID = null;
                }
                #endregion

            }
            else
            {
                card.MsVesselID = short.MinValue;
            }
        }
        #endregion

        #region private Dictionary<int, SiLinkShokumeiCard> CreateLinkDic()
        private Dictionary<int, SiLinkShokumeiCard> CreateLinkDic()
        {
            Dictionary<int, SiLinkShokumeiCard> linkDic = new Dictionary<int, SiLinkShokumeiCard>();

            foreach (SiLinkShokumeiCard link in card.SiLinkShokumeiCards)
            {
                linkDic[link.MsSiShokumeiID] = link;
            }

            return linkDic;
        }
        #endregion

        #region private void button削除_Click(object sender, EventArgs e)
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
        #endregion

        #region private void button閉じる_Click(object sender, EventArgs e)
        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        #endregion

        #region private void comboBox種別_SelectionChangeCommitted(object sender, EventArgs e)
        private void comboBox種別_SelectionChangeCommitted(object sender, EventArgs e)
        {
            EnableComponents();
        }
        #endregion

        #region private void EnableComponents()
        private void EnableComponents()
        {
            panel日程.Visible = true;

            MsSiShubetsu s = comboBox種別.SelectedItem as MsSiShubetsu;

            if (SeninTableCache.instance().Is_乗船中(NBaseCommon.Common.LoginUser, s.MsSiShubetsuID))
            {
                nullableDateTimePicker開始.Enabled = true;
                nullableDateTimePicker終了.Enabled = true;
                if (!SeninTableCache.instance().Is_乗船(NBaseCommon.Common.LoginUser, s.MsSiShubetsuID))
                {
                    nullableDateTimePicker終了.Value = DateTimeUtils.ToTo((DateTime)nullableDateTimePicker開始.Value).AddSeconds(-1);
                    nullableDateTimePicker終了.Enabled = false;
                }
                button役職変更.Visible = true;
                if (card.IsNew())
                {
                    button役職変更.Enabled = false;
                }
                else
                {
                    button役職変更.Enabled = true;
                }

                textBox日数.ReadOnly = true;

                panel_乗船.Visible = true;
                flowLayoutPanel労働区分乗船.Visible = true;
                flowLayoutPanel労働区分下船.Visible = true;
                panel事務所.Visible = false;

                this.Height = 584;
            }
            else if (Is_休暇管理_ByKyuukaFlag(s.KyuukaFlag))
            {
                // 本年度休暇日数（XX)を追加、編集できるようにする
                //nullableDateTimePicker開始.Enabled = false;
                //nullableDateTimePicker終了.Enabled = false;
                if (isNew)
                {
                    // 新規作成の場合、開始、終了とも編集可
                    nullableDateTimePicker開始.Enabled = true;
                    nullableDateTimePicker終了.Enabled = true;
                }
                else
                {
                    // 編集の場合、終了のみ編集可
                    nullableDateTimePicker開始.Enabled = false;
                    nullableDateTimePicker終了.Enabled = true;
                }

                textBox日数.ReadOnly = false;

                panel_乗船.Visible = false;
                flowLayoutPanel労働区分乗船.Visible = false;
                flowLayoutPanel労働区分下船.Visible = false;
                button役職変更.Visible = false;

                panel事務所.Visible = false;

                this.Height = 255;
            }
            else
            {
                nullableDateTimePicker開始.Enabled = true;
                nullableDateTimePicker終了.Enabled = true;

                textBox日数.ReadOnly = true;

                panel_乗船.Visible = false;
                flowLayoutPanel労働区分乗船.Visible = false;
                flowLayoutPanel労働区分下船.Visible = false;
                button役職変更.Visible = false;

                // 山友Verは「種別詳細」を利用しない
                //if (s.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.陸上勤務))
                //{
                //    panel事務所.Visible = true;
                //    this.Height = 300;
                //}
                //else
                //{
                //    panel事務所.Visible = false;
                //    this.Height = 255;
                //}
                panel事務所.Visible = false;
                this.Height = 255;
            }

        }
        #endregion

        #region private static bool Is_休暇管理_ByKyuukaFlag(int kyuukaFlag)
        private static bool Is_休暇管理_ByKyuukaFlag(int kyuukaFlag)
        {
            return kyuukaFlag == (int)MsSiShubetsu.KyuukaFlagKind.休暇買上 ||
                   kyuukaFlag == (int)MsSiShubetsu.KyuukaFlagKind.本年度休暇日数 ||
                   kyuukaFlag == (int)MsSiShubetsu.KyuukaFlagKind.前年度休暇繰越日数;
        }
        #endregion

        #region private void comboBox種別_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBox種別_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 川崎近海Verは「転船」はなし
            //bool 転船flag = false;
            //if (comboBox種別.SelectedItem is MsSiShubetsu)
            //{
            //    if ((comboBox種別.SelectedItem as MsSiShubetsu).MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.転船))
            //    {
            //        転船flag = true;
            //    }
            //}
            //if (転船flag)
            //{
            //    checkBox転船.Enabled = true;
            //}
            //else
            //{
            //    checkBox転船.Checked = false;
            //    checkBox転船.Enabled = false;
            //}
        }
        #endregion

        #region private void checkBox転船_CheckedChanged(object sender, EventArgs e)
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
        #endregion



        private void comboBox船_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox船.SelectedItem is MsVessel)
            {
                MsVessel vessel = comboBox船.SelectedItem as MsVessel;

                textBox船名.Text = vessel.VesselName;
                textBox会社名.Text = NBaseCommon.Common.船員_自社名;
                if (vessel.MsCrewMatrixTypeID > 0)
                {
                    comboBoxCrewMatrixType.Text = SeninTableCache.instance().GetMsCrewMatrixTypeName(NBaseCommon.Common.LoginUser, vessel.MsCrewMatrixTypeID);
                }
                else
                {
                    comboBoxCrewMatrixType.SelectedIndex = 0;
                }

                comboBox船タイプ.Text = vessel.VesselTypeName;
                textBoxGRT.Text = vessel.GRT.ToString("#####0.000");
                textBoxオーナー.Text = vessel.OwnerName;
                comboBox_NavigationArea.Text = vessel.NavigationArea;


                //if (vessel.Is内航船)
                if (vessel.IsPlanType(MsPlanType.PlanTypeHarfPeriod))
                {
                    radioButton労働区分乗船_労働.Checked = true;
                    flowLayoutPanel労働区分乗船.Enabled = false;

                    radioButton労働区分下船_労働.Checked = true;
                    flowLayoutPanel労働区分下船.Enabled = false;

                    comboBox職名.Items.Clear();
                    foreach(var s in SeninTableCache.instance().GetShokumeiList(NBaseCommon.Common.LoginUser, Shokumei.内航))
                    {
                        comboBox職名.Items.Add(s);
                    }
                }
                //else if (vessel.Isフェリー)
                else if (vessel.IsPlanType(MsPlanType.PlanTypeOneMonth))
                {
                    radioButton労働区分乗船_半休.Checked = true;
                    flowLayoutPanel労働区分乗船.Enabled = true;

                    radioButton労働区分下船_半休.Checked = true;
                    flowLayoutPanel労働区分下船.Enabled = true;

                    comboBox職名.Items.Clear();
                    foreach (var s in SeninTableCache.instance().GetShokumeiList(NBaseCommon.Common.LoginUser, Shokumei.フェリー))
                    {
                        comboBox職名.Items.Add(s);
                    }

                }
                else
                {
                    radioButton労働区分乗船_労働.Checked = true;
                    flowLayoutPanel労働区分乗船.Enabled = false;

                    radioButton労働区分下船_労働.Checked = true;
                    flowLayoutPanel労働区分下船.Enabled = false;
                }
            }
            else
            {

            }
        }


        private Shokumei GetShokumei(int msSiShokumeiId, int msSiShokumeiShousaiId)
        {
            Shokumei s = null;
            foreach(var o in comboBox職名.Items)
            {
                if ((o as Shokumei).MsSiShokumeiID == msSiShokumeiId && (o as Shokumei).MsSiShokumeiShousaiID == msSiShokumeiShousaiId)
                {
                    s = (o as Shokumei);
                }
            }
            return s;
        }




        private void button乗船場所_Click(object sender, EventArgs e)
        {
            場所検索Form form = new 場所検索Form();
            if (form.ShowDialog() == DialogResult.Yes)
            {
                basho乗船場所 = form.Selected;
                form.Dispose();

                textBox乗船場所.Text = basho乗船場所.BashoName;
            }
        }

        private void button乗船場所クリア_Click(object sender, EventArgs e)
        {
            basho乗船場所 = null;
            textBox乗船場所.Text = "";
        }


        private void button下船場所_Click(object sender, EventArgs e)
        {
            場所検索Form form = new 場所検索Form();
            if (form.ShowDialog() == DialogResult.Yes)
            {
                basho下船場所 = form.Selected;
                form.Dispose();

                textBox下船場所.Text = basho下船場所.BashoName;
            }
        }

        private void button下船場所クリア_Click(object sender, EventArgs e)
        {
            basho下船場所 = null;
            textBox下船場所.Text = "";
        }


        private void button交代者検索_Click(object sender, EventArgs e)
        {
            if (nullableDateTimePicker終了.Value == null)
            {
                MessageBox.Show("終了日を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBox船.SelectedItem == null)
            {
                MessageBox.Show("船を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBox職名.SelectedItem == null)
            {
                MessageBox.Show("職名を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            card.EndDate = DateTimeUtils.ToFrom((DateTime)nullableDateTimePicker終了.Value);
            card.MsVesselID = (short)(comboBox船.SelectedItem as MsVessel).MsVesselID;
            card.CardMsSiShokumeiID = (comboBox職名.SelectedItem as Shokumei).MsSiShokumeiID;

            交代者検索Form form = new 交代者検索Form(card);
            if (form.ShowDialog() == DialogResult.Yes)
            {
                card交代者 = form.Selected;
                form.Dispose();

                textBox交代者.Text = card交代者.SeninName;
            }
        }

        private void button交代者クリア_Click(object sender, EventArgs e)
        {
            card交代者 = null;
            textBox交代者.Text = "";
        }



        private void button役職変更_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                FillInstance();

                役職変更Form form = new 役職変更Form(parentPanel, card);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    DialogResult = DialogResult.OK;
                    Dispose();
                }
            }
        }
    }
}
