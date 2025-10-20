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
using NBaseHonsen.Controls;

namespace NBaseHonsen
{
    public partial class 動静実績登録Form : Form
    {
        public event KaniDouseiControl2.AfterEditDelegate AfterEdit;
        
        public List<PtKanidouseiInfo> KanidouseiInfo_List;
        public PtKanidouseiInfo KanidouseiInfo;
        private DjDousei Dousei;

        private int TotalCount;
        private int CurrentIndex;

        public List<MsKanidouseiInfoShubetu> MsKanidouseiInfoShubetu_List;
        private List<MsBasho> MsBasho_list = null;
        private List<MsKichi> MsKichi_list = null;
        private List<MsCargo> MsCargo_list = null;
        private List<MsDjTani> MsDjTani_list = null;
        private List<MsCustomer> MsCustomer_list = null;

        public 動静実績登録Form()
        {
            InitializeComponent();
        }
        public 動静実績登録Form(PtKanidouseiInfo p)
        {
            InitializeComponent();
            KanidouseiInfo = p;
        }

        private void 動静実績登録Form_Load(object sender, EventArgs e)
        {
            if (KanidouseiInfo == null)
            {
                Close();
            }

            DateTime targetDay = DateTime.Parse(KanidouseiInfo.EventDate.ToShortDateString());
            ////KanidouseiInfo_List = PtKanidouseiInfo.GetRecordByEventDate(同期Client.LOGIN_USER, targetDay, targetDay.AddDays(1).AddSeconds(-1));
            //KanidouseiInfo_List = PtKanidouseiInfo.GetRecordsByEventDateVessel(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID, targetDay, targetDay.AddDays(1).AddSeconds(-1));
            KanidouseiInfo_List = PtKanidouseiInfo.GetRecordByEventDate(同期Client.LOGIN_USER, targetDay, targetDay.AddDays(1).AddSeconds(-1), 同期Client.LOGIN_VESSEL.MsVesselID);

            MsKanidouseiInfoShubetu_List = MsKanidouseiInfoShubetu.GetRecords(同期Client.LOGIN_USER);
            MsBasho_list = MsBasho.GetRecordsBy港(同期Client.LOGIN_USER);
            MsKichi_list = MsKichi.GetRecords(同期Client.LOGIN_USER);
            MsCargo_list = MsCargo.GetRecords(同期Client.LOGIN_USER);
            MsDjTani_list = MsDjTani.GetRecords(同期Client.LOGIN_USER);
            MsCustomer_list = MsCustomer.GetRecords(同期Client.LOGIN_USER);
            MakeDropDownList();

            TotalCount = KanidouseiInfo_List.Count();
            CurrentIndex = 0;
            foreach (PtKanidouseiInfo pki in KanidouseiInfo_List)
            {
                if (pki.PtKanidouseiInfoId == KanidouseiInfo.PtKanidouseiInfoId)
                {
                    break;
                }
                CurrentIndex = CurrentIndex + 1;
            }

            label_Day.Text = KanidouseiInfo.EventDate.ToShortDateString();

            Dousei = DjDousei.GetRecord(同期Client.LOGIN_USER, KanidouseiInfo.DjDouseiID);

            SetCountLabel();
            SetButton();

            if (Dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.積みID
                || Dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.揚げID)
            {
                Init積揚();
            }
            else
            {
                Init待機入渠パージ();
            }
        }

        #region private void SetCountLabel()
        private void SetCountLabel()
        {
            label_Count.Text = "(" + (CurrentIndex + 1).ToString() + "/" + TotalCount.ToString() + ")";

            if (Dousei.DeleteFlag == NBaseCommon.Common.DeleteFlag_削除)
            {
                label_DeleteMessage.Visible = true;
            }
            else
            {
                label_DeleteMessage.Visible = false;
            }
        }
        #endregion
        
        #region private void SetButton()
        private void SetButton()
        {
            if (Dousei.PlanNiyakuStart != DateTime.MinValue)
            {
                // 予定がある場合、削除はできない
                button_削除.Enabled = false;
            }
            else
            {
                button_削除.Enabled = true;
            }
            if (Dousei.DeleteFlag == NBaseCommon.Common.DeleteFlag_削除)
            {
                // 削除された場合、登録、削除ともにできない
                button_登録.Enabled = false;
                button_削除.Enabled = false;
            }
        }
        #endregion

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
            panel_積揚.Location = new Point(5, 104);

            this.Size = new Size(925, 604);
            
            if ( Dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.積みID )
            {
                douseYoteiReadOnlyUserControl1.SetMode(DouseYoteiReadOnlyUserControl.ModeEnum.積み, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
                douseJissekiUserControl1.SetMode(DouseiJissekiUserControl.ModeEnum.積み, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            }
            else
            {
                douseYoteiReadOnlyUserControl1.SetMode(DouseYoteiReadOnlyUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
                douseJissekiUserControl1.SetMode(DouseiJissekiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            }
            douseYoteiReadOnlyUserControl1.SetDousei(Dousei);
            douseJissekiUserControl1.SetDousei(Dousei);

            douseJissekiUserControl1.ReadOnly荷主();
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
            panel_待機入渠パージ.Location = new Point(5, 78);

            this.Size = new Size(925, 235);

            if (KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.待機).MsKanidouseiInfoShubetuId)
            {
                label_待機入渠パージ.Text = "種別：待機";
            }
            else if (KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.パージ).MsKanidouseiInfoShubetuId)
            {
                label_待機入渠パージ.Text = "種別：パージ";
            }
            else if (KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.避泊).MsKanidouseiInfoShubetuId)
            {
                label_待機入渠パージ.Text = "種別：避泊";
            }
            else if (KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.その他).MsKanidouseiInfoShubetuId)
            {
                label_待機入渠パージ.Text = "種別：その他";
            }

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
        #endregion

        /// <summary>
        /// 「↑」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_UP_Click(object sender, EventArgs e)
        private void button_UP_Click(object sender, EventArgs e)
        {
            if (CurrentIndex == 0)
            {
                return;
            }
            CurrentIndex = CurrentIndex - 1;

            KanidouseiInfo = KanidouseiInfo_List[CurrentIndex];

            Dousei = DjDousei.GetRecord(同期Client.LOGIN_USER, KanidouseiInfo.DjDouseiID);
            
            SetCountLabel();
            SetButton();

            if (Dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.積みID
                || Dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.揚げID)
            {
                Init積揚();
            }
            else
            {
                Init待機入渠パージ();
            }
        }
        #endregion

        /// <summary>
        /// 「↓」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_Down_Click(object sender, EventArgs e)
        private void button_Down_Click(object sender, EventArgs e)
        {
            if (CurrentIndex + 1 == TotalCount)
            {
                return;
            }
            CurrentIndex = CurrentIndex + 1;

            KanidouseiInfo = KanidouseiInfo_List[CurrentIndex];

            Dousei = DjDousei.GetRecord(同期Client.LOGIN_USER, KanidouseiInfo.DjDouseiID);

            SetCountLabel();
            SetButton();

            if (Dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.積みID
                || Dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.揚げID)
            {
                Init積揚();
            }
            else
            {
                Init待機入渠パージ();
            }
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
            DialogResult = DialogResult.No;
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
            if (MessageBox.Show("この動静実績を削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No )
            {
                return;
            }

            // 削除処理
            bool ret = true;
            動静処理 logic = new 動静処理();
            ret = logic.Honsen動静実績削除(同期Client.LOGIN_USER, KanidouseiInfo);
            if (ret == true)
            {
                MessageBox.Show("削除しました", "確認", MessageBoxButtons.OK);

                KanidouseiInfo.DeleteFlag = NBaseCommon.Common.DeleteFlag_削除;

                AfterEdit(KanidouseiInfo.EventDate, KanidouseiInfo.MsVesselID, KanidouseiInfo.Koma, KanidouseiInfo);

                KanidouseiInfo_List[CurrentIndex] = KanidouseiInfo;
            }
            else
            {
                MessageBox.Show("削除に失敗しました", "確認", MessageBoxButtons.OK);
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
            if (panel_積揚.Visible)
            {
                ret = 積_揚_登録();
            }
            else
            {
                ret = 待機_入渠_パージ_登録();
            }
            if (ret == true)
            {

                DateTime eventDate = KanidouseiInfo.EventDate;
                int vesselId = KanidouseiInfo.MsVesselID;
                int koma = KanidouseiInfo.Koma;

                KanidouseiInfo.EventDate = Dousei.DouseiDate;
                if (Dousei.ResultMsBashoID != null && Dousei.ResultMsBashoID.Length > 0)
                {
                    KanidouseiInfo.MsBashoId = Dousei.ResultMsBashoID;
                }
                else
                {
                    KanidouseiInfo.MsBashoId = Dousei.MsBashoID;
                }
                if (Dousei.ResultMsKichiID != null && Dousei.ResultMsKichiID.Length > 0)
                {
                    KanidouseiInfo.MsKitiId = Dousei.ResultMsKichiID;
                }
                else
                {
                    KanidouseiInfo.MsKitiId = Dousei.MsKichiID;
                }
                //if (panel_積揚.Visible)
                //{
                //    if (Dousei.DjDouseiCargos.Count > 0)
                //    {
                //        KanidouseiInfo.MsCargoName = Dousei.DjDouseiCargos[0].MsCargoName;
                //        KanidouseiInfo.Qtty = Dousei.DjDouseiCargos[0].Qtty;
                //    }
                //}

                AfterEdit(eventDate, vesselId, koma, KanidouseiInfo);
                
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
            Fill_待機_入渠_パージ();

            bool ret = true;
            動静処理 logic = new 動静処理();
            ret = logic.Honsen動静実績登録(同期Client.LOGIN_USER, Dousei.DjDouseis);
            if (ret)
            {
                MessageBox.Show("登録しました", "確認", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("登録に失敗しました", "確認", MessageBoxButtons.OK);
            }
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

        /// <summary>
        /// 入力データをクラスにセットする（待機/入渠/パージ/避泊）
        /// </summary>
        /// <returns></returns>
        #region private void Fill_待機_入渠_パージ()
        private void Fill_待機_入渠_パージ()
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
            string bikou = textBox_備考.Text;

            int nowDouseiCount = Dousei.DjDouseis.Count();
            int setDouseiCount = 0;
            for (DateTime setDay = date1; setDay <= date2; setDay = setDay.AddDays(1))
            {
                DjDousei dousei = null;
                if (setDouseiCount >= nowDouseiCount)
                {
                    dousei = new DjDousei();
                    dousei.MsVesselID = KanidouseiInfo.MsVesselID;
                    dousei.MsKanidouseiInfoShubetuID = KanidouseiInfo.MsKanidouseiInfoShubetuId;
                    Dousei.DjDouseis.Add(dousei);
                }
                else
                {
                    dousei = Dousei.DjDouseis[setDouseiCount];
                }
                dousei.DouseiDate = setDay;
                dousei.MsBashoID = basho.MsBashoId;
                dousei.MsKichiID = kichiId;
                dousei.Bikou = bikou;

                setDouseiCount++;
            }
            if (nowDouseiCount > setDouseiCount)
            {
                for (int i = setDouseiCount; i < nowDouseiCount; i++)
                {
                    DjDousei dousei = Dousei.DjDouseis[setDouseiCount];
                    dousei.DeleteFlag = 1;
                }
            }

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
            Fill_積_揚();

            bool ret = true;
            動静処理 logic = new 動静処理();
            ret = logic.Honsen動静実績登録(同期Client.LOGIN_USER, Dousei.DjDouseis);
            if(ret)
            {
                MessageBox.Show("登録しました", "確認", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("登録に失敗しました", "確認", MessageBoxButtons.OK);
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
            if (douseJissekiUserControl1.Validation() == false)
            {
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 入力データをクラスにセットする（積み揚げ）
        /// </summary>
        #region private void Fill_積_揚()
        private void Fill_積_揚()
        {
            List<DjDousei> ret = new List<DjDousei>();
            DjDousei dousei = douseJissekiUserControl1.GetInstance();
            Dousei.DjDouseis.Clear();
            Dousei.DjDouseis.Add(dousei);
        }
        #endregion
    }
}
