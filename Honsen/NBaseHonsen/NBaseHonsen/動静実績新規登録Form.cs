using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SyncClient;
using NBaseData.DAC;
using NBaseData.BLC;
using NBaseCommon;

namespace NBaseHonsen
{
    public partial class 動静実績新規登録Form : Form
    {
        public PtKanidouseiInfo KanidouseiInfo;
        private DjDousei Dousei;

        public List<MsKanidouseiInfoShubetu> MsKanidouseiInfoShubetu_List;
        private List<MsBasho> MsBasho_list = null;
        private List<MsKichi> MsKichi_list = null;
        private List<MsCargo> MsCargo_list = null;
        private List<MsDjTani> MsDjTani_list = null;
        private List<MsCustomer> MsCustomer_list = null;

        public 動静実績新規登録Form()
        {
            InitializeComponent();
        }
        public 動静実績新規登録Form(PtKanidouseiInfo p)
        {
            InitializeComponent();
            KanidouseiInfo = p;
            Dousei = null;
        }
        public 動静実績新規登録Form(PtKanidouseiInfo p, DjDousei d)
        {
            InitializeComponent();
            KanidouseiInfo = p;
            Dousei = d;
        }

        private void 動静実績新規登録Form_Load(object sender, EventArgs e)
        {
            if (KanidouseiInfo == null)
            {
                Close();
            }

            MsKanidouseiInfoShubetu_List = MsKanidouseiInfoShubetu.GetRecords(同期Client.LOGIN_USER);
            MsBasho_list = MsBasho.GetRecordsBy港(同期Client.LOGIN_USER);
            MsKichi_list = MsKichi.GetRecords(同期Client.LOGIN_USER);
            MsCargo_list = MsCargo.GetRecords(同期Client.LOGIN_USER);
            MsDjTani_list = MsDjTani.GetRecords(同期Client.LOGIN_USER);
            MsCustomer_list = MsCustomer.GetRecords(同期Client.LOGIN_USER);
            MakeDropDownList();


            if (Dousei == null)
            {
                // デフォルト表示
                radioButton_TumiAge.Checked = true;

                button_登録.Enabled = true;
                button_削除.Enabled = false;
            }
            else
            {
                if (KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.積み).MsKanidouseiInfoShubetuId)
                {
                    radioButton_TumiAge.Checked = true;
                }
                else if (KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.揚げ).MsKanidouseiInfoShubetuId)
                {
                    radioButton_TumiAge.Checked = true;
                }
                else if (KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.待機).MsKanidouseiInfoShubetuId)
                {
                    radioButton_Taiki.Checked = true;
                }
                else if (KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.避泊).MsKanidouseiInfoShubetuId)
                {
                    radioButton_Hihaku.Checked = true;
                }
                else if (KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.パージ).MsKanidouseiInfoShubetuId)
                {
                    radioButton_Purge.Checked = true;
                }
                else if (KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.その他).MsKanidouseiInfoShubetuId)
                {
                    radioButton_Etc.Checked = true;
                }
            }
        }

        #region private void MakeDropDownList()
        private void MakeDropDownList()
        {
            // 港
            comboBox_港.Items.Clear();
            comboBox_港.Items.Add("");
            foreach (MsBasho basho in MsBasho_list)
            {
                comboBox_港.Items.Add(basho);
                comboBox_港.AutoCompleteCustomSource.Add(basho.BashoName);
            }
            comboBox_港.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox_港.AutoCompleteSource = AutoCompleteSource.CustomSource;
            comboBox_港.SelectedIndex = 0;

            // 基地
            comboBox_基地.Items.Clear();
            comboBox_基地.Items.Add("");
            foreach (MsKichi kichi in MsKichi_list)
            {
                comboBox_基地.Items.Add(kichi);
                comboBox_基地.AutoCompleteCustomSource.Add(kichi.KichiName);
            }
            comboBox_基地.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox_基地.AutoCompleteSource = AutoCompleteSource.CustomSource;
            comboBox_基地.SelectedIndex = 0;
        }
        #endregion

        /// <summary>
        /// パネル初期化（積み揚げ）
        /// </summary>
        #region private void Init積揚()
        private void Init積揚()
        {
            panel_待機入渠パージ.Visible = false;

            panel_積揚.Visible = true;
            panel_積揚.Location = new Point(12, 100);

            this.Size = new Size(892, 568);

            douseiJissekiUserControl1.SetMode(DouseiJissekiUserControl.ModeEnum.積み, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            douseiJissekiUserControl2.SetMode(DouseiJissekiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            if (Dousei == null)
            {
                douseiJissekiUserControl1.SetDay(KanidouseiInfo.EventDate);
                douseiJissekiUserControl2.SetDay(KanidouseiInfo.EventDate);
            }
            else
            {
                if (Dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.積みID)
                {
                    douseiJissekiUserControl1.SetDousei(Dousei);
                    douseiJissekiUserControl2.SetDay(KanidouseiInfo.EventDate);
                }
                else
                {
                    douseiJissekiUserControl1.SetDay(KanidouseiInfo.EventDate);
                    douseiJissekiUserControl2.SetDousei(Dousei);
                }
            }
        }
        #endregion

        /// <summary>
        /// パネル初期化（待機/入渠/パージ/避泊）
        /// </summary>
        #region private void Init待機入渠パージ()
        private void Init待機入渠パージ()
        {
            panel_積揚.Visible = false;

            panel_待機入渠パージ.Visible = true;
            panel_待機入渠パージ.Location = new Point(9, 60);

            this.Size = new Size(892, 202);

            if (Dousei == null)
            {
                dateTimePicker1.Value = KanidouseiInfo.EventDate;
                dateTimePicker2.Value = KanidouseiInfo.EventDate;
            }
            else
            {
                Dousei.DjDouseis = DjDousei.GetRecordsByVoaygeNo(同期Client.LOGIN_USER, Dousei);
                DateTime startDay = DateTime.MinValue;
                bool isSet = false;
                foreach (DjDousei dousei in Dousei.DjDouseis)
                {
                    if (startDay == DateTime.MinValue)
                    {
                        dateTimePicker1.Value = dousei.DouseiDate;
                        dateTimePicker2.Value = dousei.DouseiDate;
                        startDay = dousei.DouseiDate;
                    }
                    else if (startDay < dousei.DouseiDate)
                    {
                        dateTimePicker2.Value = dousei.DouseiDate;
                    }
                    else
                    {
                        dateTimePicker1.Value = dousei.DouseiDate;
                        startDay = dousei.DouseiDate;
                    }
                    if (isSet == false)
                    {
                        foreach (MsBasho basho in MsBasho_list)
                        {
                            if (basho.MsBashoId == dousei.MsBashoID)
                            {
                                comboBox_港.SelectedItem = basho;
                                break;
                            }
                        }
                        foreach (MsKichi kichi in MsKichi_list)
                        {
                            if (kichi.MsKichiId == dousei.MsKichiID)
                            {
                                comboBox_基地.SelectedItem = kichi;
                                break;
                            }
                        }
                        textBox_備考.Text = dousei.Bikou;
                        isSet = true;
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 「積／揚」ラジオボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void radioButton_TumiAge_CheckedChanged(object sender, EventArgs e)
        private void radioButton_TumiAge_CheckedChanged(object sender, EventArgs e)
        {
            Init積揚();
        }
        #endregion

        /// <summary>
        /// 「待機」　ラジオボタンクリック
        /// 「入渠」　ラジオボタンクリック
        /// 「パージ」ラジオボタンクリック
        /// 「避泊」ラジオボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void radioButton_TaikiNyukyoParge_CheckedChanged(object sender, EventArgs e)
        private void radioButton_TaikiNyukyoParge_CheckedChanged(object sender, EventArgs e)
        {
            Init待機入渠パージ();
        }
        #endregion

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_閉じる_Click(object sender, EventArgs e)
        private void button_閉じる_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            Close();
        }
        #endregion

        /// <summary>
        /// 「削除」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_削除_Click(object sender, EventArgs e)
        private void button_削除_Click(object sender, EventArgs e)
        {
            if (KanidouseiInfo == null)
            {
                return;
            }

            // 削除処理
            bool ret = true;
            動静処理 logic = new 動静処理();
            ret = logic.Honsen動静実績削除(同期Client.LOGIN_USER, KanidouseiInfo);
            if (ret == true)
            {
                KanidouseiInfo.DeleteFlag = NBaseCommon.Common.DeleteFlag_削除;

                DialogResult = DialogResult.OK;
                Close();
            }
        }
        #endregion

        /// <summary>
        /// 「登録」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_登録_Click(object sender, EventArgs e)
        private void button_登録_Click(object sender, EventArgs e)
        {
            bool ret = true;
            if (panel_待機入渠パージ.Visible)
            {
                ret = 待機_入渠_パージ_登録();
            }
            else
            {
                ret = 積_揚_登録();
            }
            if (ret == true)
            {
                KanidouseiInfo.HonsenCheckDate = DateTime.Now;

                KanidouseiInfo.MsVesselID = Dousei.MsVesselID;
                KanidouseiInfo.MsBashoId = Dousei.MsBashoID;
                KanidouseiInfo.MsKitiId = Dousei.MsKichiID;
                if (panel_待機入渠パージ.Visible)
                {
                    KanidouseiInfo.EventDate = Dousei.DouseiDate;
                }
                else
                {
                    // 2013.03.23 : 実績日は着棧日であるとコメントを受けたので改造
                    // 積み揚げの場合、簡易動静の日時は、実績入港日時
                    //KanidouseiInfo.EventDate = Dousei.ResultNyuko;
                    KanidouseiInfo.EventDate = Dousei.ResultChakusan;
                    //if (Dousei.DjDouseiCargos.Count > 0)
                    //{
                    //    KanidouseiInfo.MsCargoName = Dousei.DjDouseiCargos[0].MsCargoName;
                    //    KanidouseiInfo.Qtty = Dousei.DjDouseiCargos[0].Qtty;
                    //}

                    KanidouseiInfo.MsBashoId = Dousei.ResultMsBashoID;
                    KanidouseiInfo.MsKitiId = Dousei.ResultMsKichiID;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
        }
        #endregion

        /// <summary>
        /// 登録処理（待機/入渠/パージ/避泊）
        /// </summary>
        /// <returns></returns>
        #region private bool 待機_入渠_パージ_登録()
        private bool 待機_入渠_パージ_登録()
        {
            if (Validation_待機_入渠_パージ() == false)
            {
                return false;
            }
            List<DjDousei> douseiInfos = Fill_待機_入渠_パージ();

            bool ret = true;
            動静処理 logic = new 動静処理();
            ret = logic.Honsen動静実績登録(同期Client.LOGIN_USER, douseiInfos);
            Dousei = douseiInfos[0];
            return ret;
        }
        #endregion

        /// <summary>
        /// 入力値の検証を行う（待機/入渠/パージ/避泊）
        /// </summary>
        /// <returns></returns>
        #region private bool Validation_待機_入渠_パージ()
        private bool Validation_待機_入渠_パージ()
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("日付を正しく選択して下さい", "動静予定", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (comboBox_港.SelectedIndex == 0)
            {
                MessageBox.Show("港を選択して下さい", "動静予定", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //if (comboBox_基地.SelectedIndex == 0)
            //{
            //    MessageBox.Show("基地を選択して下さい", "動静予定", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
            return true;
        }
        #endregion

        #region private List<DjDousei> Fill_待機_入渠_パージ()
        private List<DjDousei> Fill_待機_入渠_パージ()
        {
            List<DjDousei> ret = new List<DjDousei>();

            DateTime date1 = DateTime.Parse(dateTimePicker1.Value.ToShortDateString());
            DateTime date2 = DateTime.Parse(dateTimePicker2.Value.ToShortDateString());
            MsBasho basho = comboBox_港.SelectedItem as MsBasho;
            string kichiId = null;
            if (comboBox_基地.SelectedItem is MsKichi)
            {
                MsKichi kichi = comboBox_基地.SelectedItem as MsKichi;
                kichiId = kichi.MsKichiId;
            }
            String bikou = textBox_備考.Text;

            for (DateTime setDay = date1; setDay <= date2; setDay = setDay.AddDays(1))
            {
                DjDousei dousei = new DjDousei();

                dousei.MsVesselID = KanidouseiInfo.MsVesselID;
                dousei.DouseiDate = setDay;
                dousei.MsBashoID = basho.MsBashoId;
                dousei.MsKichiID = kichiId;
                dousei.MsKanidouseiInfoShubetuID = GrtMsKanidouseiInfoShubetuId();
                dousei.Bikou = bikou;

                ret.Add(dousei);
            }

            return ret;
        }
        #endregion


        /// <summary>
        /// 登録処理（積み揚げ）
        /// </summary>
        /// <returns></returns>
        #region private bool 積_揚_登録()
        private bool 積_揚_登録()
        {
            if (Validation_積_揚() == false)
            {
                return false;
            }
            List<DjDousei> douseiInfos = Fill_積_揚();

            bool ret = true;
            if (douseiInfos.Count > 0)
            {
                動静処理 logic = new 動静処理();
                ret = logic.Honsen動静実績登録(同期Client.LOGIN_USER, douseiInfos);
                Dousei = douseiInfos[0];
            }
            return ret;
        }
        #endregion

        /// <summary>
        /// 入力値の検証を行う（積み揚げ）
        /// </summary>
        /// <returns></returns>
        #region private bool Validation_積_揚()
        private bool Validation_積_揚()
        {
            if (douseiJissekiUserControl1.IsValid() && douseiJissekiUserControl1.Validation() == false) // 積み１
            {
                return false;
            }
            if (douseiJissekiUserControl2.IsValid() && douseiJissekiUserControl2.Validation() == false) // 揚げ１
            {
                return false;
            }

            return true;
        }
        #endregion

        #region private List<DjDousei> Fill_積_揚()
        private List<DjDousei> Fill_積_揚()
        {
            List<DjDousei> ret = new List<DjDousei>();

            DjDousei dousei = null;

            // 積み１
            if (douseiJissekiUserControl1.IsValid())
            {
                dousei = douseiJissekiUserControl1.GetInstance();
                if (dousei != null)
                {
                    dousei.MsVesselID = KanidouseiInfo.MsVesselID;
                    dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.積み).MsKanidouseiInfoShubetuId;
                    ret.Add(dousei);
                }
            }

            // 揚げ１
            if (douseiJissekiUserControl2.IsValid())
            {
                dousei = douseiJissekiUserControl2.GetInstance();
                if (dousei != null)
                {
                    dousei.MsVesselID = KanidouseiInfo.MsVesselID;
                    dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.揚げ).MsKanidouseiInfoShubetuId;
                    ret.Add(dousei);
                }
            }
            return ret;
        }
        #endregion

        /// <summary>
        /// ラジオボタンに対応する種別IDを取得する
        /// </summary>
        /// <returns></returns>
        #region private string GrtMsKanidouseiInfoShubetuId()
        private string GrtMsKanidouseiInfoShubetuId()
        {
            if (radioButton_TumiAge.Checked == true)
            {
                return MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.揚積).MsKanidouseiInfoShubetuId;
            }
            else if (radioButton_Taiki.Checked == true)
            {
                return MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.待機).MsKanidouseiInfoShubetuId;
            }

            else if (radioButton_Purge.Checked == true)
            {
                return MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.パージ).MsKanidouseiInfoShubetuId;
            }
            else if (radioButton_Hihaku.Checked == true)
            {
                return MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.避泊).MsKanidouseiInfoShubetuId;
            }
            else if (radioButton_Etc.Checked == true)
            {
                return MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.その他).MsKanidouseiInfoShubetuId;
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}
