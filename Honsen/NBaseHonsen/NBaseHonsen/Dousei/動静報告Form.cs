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
using SyncClient;
using NBaseData.DS;
using ORMapping;

namespace NBaseHonsen.Dousei
{
    public partial class 動静報告Form : Form
    {
        private 動静報告一覧Form parentForm;
        private DjDouseiHoukoku DouseiHoukoku = null;

        private List<MsVessel> vesselList = null;
        private List<MsBasho> bashoList = null;
        private List<MsDjTenkou> tenkouList = null;
        private List<MsDjKazamuki> kazamukiList = null;
        private List<MsDjSentaisetsubi> sentaisetsubiList = null;
        private List<MsDjKenkoujyoutai> kenkoujyoutaiList = null;

        public List<MsBasho> BashoList { set { bashoList = value; } }
        public List<MsDjTenkou> TenkouList { set { tenkouList = value; } }
        public List<MsDjKazamuki> KazamukiList { set { kazamukiList = value; } }
        public List<MsDjSentaisetsubi> SentaisetsubiList { set { sentaisetsubiList = value; } }
        public List<MsDjKenkoujyoutai> KenkoujyoutaiList { set { kenkoujyoutaiList = value; } }


        public 動静報告Form(動静報告一覧Form parentForm)
            : this(parentForm, new DjDouseiHoukoku())
        {
        }
        public 動静報告Form(動静報告一覧Form parentForm,DjDouseiHoukoku houkoku)
        {
            this.parentForm = parentForm;
            this.DouseiHoukoku = houkoku;

            InitializeComponent();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                FillInstance();

                if (InsertOrUpdate())
                {
                    MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Dispose();
                    //parentForm.Set(DouseiHoukoku);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            //Dispose();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void 動静報告Form_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            nullableDateTimePicker_報告日.Value = DateTime.Today;
            textBox_報告時間.Text = DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00");

            textBox_船.Text = 同期Client.LOGIN_VESSEL.VesselName;
            foreach (MsBasho basho in bashoList)
            {
                comboBox_出港地.Items.Add(basho);
                comboBox_仕向地.Items.Add(basho);
            }

            nullableDateTimePicker_出港日.Value = DateTime.Today;
            nullableDateTimePicker_出港日.Value = null;
            textBox_出航時間.Text = "";

            nullableDateTimePicker_入港予定日.Value = DateTime.Today;
            nullableDateTimePicker_入港予定日.Value = null;
            textBox_入港予定時間.Text = "";

            textBox_現在地.Text = "";


            foreach (MsDjTenkou tenkou in tenkouList)
            {
                comboBox_天候.Items.Add(tenkou);
            }

            foreach (MsDjKazamuki kazamuki in kazamukiList)
            {
                comboBox_風向.Items.Add(kazamuki);
            }
            textBox_風速.Text = "";

            textBox_波.Text = "";
            textBox_うねり.Text = "";

            textBox_視程.Text = "";

            textBox_針路.Text = "";
            textBox_速力.Text = "";

            foreach (MsDjSentaisetsubi sentaisetsubi in sentaisetsubiList)
            {
                comboBox_船体設備状況.Items.Add(sentaisetsubi);
            }
            foreach (MsDjKenkoujyoutai kenkoujyoutai in kenkoujyoutaiList)
            {
                comboBox_乗組員健康状態.Items.Add(kenkoujyoutai);
            }
            textBox_乗組員数.Text = "";


            if (DouseiHoukoku != null)
            {
                nullableDateTimePicker_報告日.Value = DouseiHoukoku.HoukokuDate;
                textBox_報告時間.Text = DouseiHoukoku.HoukokuDate.Hour.ToString("00") + DouseiHoukoku.HoukokuDate.Minute.ToString("00");

                foreach (MsBasho basho in comboBox_出港地.Items)
                {
                    if (basho.MsBashoId == DouseiHoukoku.LeavePortID)
                    {
                        comboBox_出港地.SelectedItem = basho;
                        break;
                    }
                }
                if (DouseiHoukoku.LeaveDate != DateTime.MinValue)
                {
                    nullableDateTimePicker_出港日.Value = DouseiHoukoku.LeaveDate;
                    textBox_出航時間.Text = DouseiHoukoku.LeaveDate.Hour.ToString("00") + DouseiHoukoku.LeaveDate.Minute.ToString("00");

                }
                foreach (MsBasho basho in comboBox_仕向地.Items)
                {
                    if (basho.MsBashoId == DouseiHoukoku.DestinationPortID)
                    {
                        comboBox_仕向地.SelectedItem = basho;
                        break;
                    }
                }
                if (DouseiHoukoku.ArrivalDate != DateTime.MinValue)
                {
                    nullableDateTimePicker_入港予定日.Value = DouseiHoukoku.ArrivalDate;
                    textBox_入港予定時間.Text = DouseiHoukoku.ArrivalDate.Hour.ToString("00") + DouseiHoukoku.ArrivalDate.Minute.ToString("00");

                }
                textBox_現在地.Text = DouseiHoukoku.CurrentPlace;

                foreach (MsDjTenkou tenkou in comboBox_天候.Items)
                {
                    if (tenkou.MsDjTenkouId == DouseiHoukoku.MsDjTenkouID)
                    {
                        comboBox_天候.SelectedItem = tenkou;
                        break;
                    }
                }
                foreach (MsDjKazamuki kazamuki in comboBox_風向.Items)
                {
                    if (kazamuki.MsDjKazamukiId == DouseiHoukoku.MsDjKazamukiID)
                    {
                        comboBox_風向.SelectedItem = kazamuki;
                        break;
                    }
                }
                textBox_風速.Text = DouseiHoukoku.Fusoku;
                textBox_波.Text = DouseiHoukoku.Nami;
                textBox_うねり.Text = DouseiHoukoku.Uneri;
                textBox_視程.Text = DouseiHoukoku.Sitei;
                textBox_針路.Text = DouseiHoukoku.Sinro;
                textBox_速力.Text = DouseiHoukoku.Sokuryoku;
                foreach (MsDjSentaisetsubi sentaisetsubi in comboBox_船体設備状況.Items)
                {
                    if (sentaisetsubi.MsDjSentaisetsubiId == DouseiHoukoku.MsDjSentaisetsubiID)
                    {
                        comboBox_船体設備状況.SelectedItem = sentaisetsubi;
                        break;
                    }
                }
                foreach (MsDjKenkoujyoutai kenkoujyoutai in comboBox_乗組員健康状態.Items)
                {
                    if (kenkoujyoutai.MsDjKenkoujyoutaiId == DouseiHoukoku.MsDjKenkoujyoutaiID)
                    {
                        comboBox_乗組員健康状態.SelectedItem = kenkoujyoutai;
                        break;
                    }
                }
                textBox_乗組員数.Text = DouseiHoukoku.Norikumiinsu;
                textBox_備考.Text = DouseiHoukoku.Bikou;
            }

        }

        /// <summary>
        /// 入力値の確認
        /// </summary>
        /// <returns></returns>
        private bool ValidateFields()
        {
            if (nullableDateTimePicker_報告日.Value == null)
            {
                nullableDateTimePicker_報告日.BackColor = Color.Pink;
                MessageBox.Show("報告日を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nullableDateTimePicker_報告日.BackColor = Color.White;
                return false;
            }
            if (textBox_報告時間.Text == null || textBox_報告時間.Text.Length == 0 || textBox_報告時間.Text.Length != 4)
            {
                textBox_報告時間.BackColor = Color.Pink;
                MessageBox.Show("報告時間を0000〜2359の範囲で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_報告時間.BackColor = Color.White;
                return false;
            }
            if (!TimeFormatCheck(textBox_報告時間.Text))
            {
                textBox_報告時間.BackColor = Color.Pink;
                MessageBox.Show("報告時間を0000〜2359の範囲で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_報告時間.BackColor = Color.White;
                return false;
            }

            if (comboBox_出港地.Text != null && comboBox_出港地.Text.Length > 0)
            {
                string msBashoId = GetPortId(comboBox_出港地);
                if (msBashoId == null)
                {
                    comboBox_出港地.BackColor = Color.Pink;
                    MessageBox.Show("出港地を正しく選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox_出港地.BackColor = Color.White;
                    return false;
                }
            }
            if (comboBox_仕向地.Text != null && comboBox_仕向地.Text.Length > 0)
            {
                string msBashoId = GetPortId(comboBox_仕向地);
                if (msBashoId == null)
                {
                    comboBox_仕向地.BackColor = Color.Pink;
                    MessageBox.Show("仕向地を正しく選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox_仕向地.BackColor = Color.White;
                    return false;
                }
            }

            if (textBox_出航時間.Text != null && textBox_出航時間.Text.Length > 0 && !TimeFormatCheck(textBox_出航時間.Text))
            {
                textBox_出航時間.BackColor = Color.Pink;
                MessageBox.Show("出航時間を0000〜2359の範囲で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_出航時間.BackColor = Color.White;
                return false;
            }
            if (textBox_入港予定時間.Text != null && textBox_入港予定時間.Text.Length > 0 && !TimeFormatCheck(textBox_入港予定時間.Text))
            {
                textBox_入港予定時間.BackColor = Color.Pink;
                MessageBox.Show("入港予定時間を0000〜2359の範囲で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_入港予定時間.BackColor = Color.White;
                return false;
            }

            return true;
        }

        private bool TimeFormatCheck(string timeStr)
        {
            try
            {
                string hStr = timeStr.Substring(0, 2);
                string mStr = timeStr.Substring(2, 2);

                int hour = int.Parse(hStr);
                int minute = int.Parse(mStr);

                // 2013.05 : 不具合修正
                //if (hour >= 0 && hour <= 24)
                if (hour >= 0 && hour < 24)
                {
                    if (minute >= 0 && minute <= 59)
                    {
                        return true;
                    }
                }
                return false;

            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 入力値を取得し、情報にセットする
        /// </summary>
        private void FillInstance()
        {
            DouseiHoukoku.HoukokuDate = ConvertDate(nullableDateTimePicker_報告日, textBox_報告時間);
            DouseiHoukoku.VesselID = 同期Client.LOGIN_VESSEL.MsVesselID;
            DouseiHoukoku.LeavePortID = GetPortId(comboBox_出港地);
            DouseiHoukoku.LeaveDate = ConvertDate(nullableDateTimePicker_出港日, textBox_出航時間);
            
            DouseiHoukoku.DestinationPortID = GetPortId(comboBox_仕向地);
            DouseiHoukoku.ArrivalDate = ConvertDate(nullableDateTimePicker_入港予定日, textBox_入港予定時間);

            //DouseiHoukoku.CurrentPlace = textBox_現在地.Text;
            DouseiHoukoku.CurrentPlace = StringUtils.Escape(textBox_現在地.Text);

            if (comboBox_天候.SelectedItem is MsDjTenkou)
            {
                DouseiHoukoku.MsDjTenkouID = (comboBox_天候.SelectedItem as MsDjTenkou).MsDjTenkouId;
            }
            if (comboBox_風向.SelectedItem is MsDjKazamuki)
            {
                DouseiHoukoku.MsDjKazamukiID = (comboBox_風向.SelectedItem as MsDjKazamuki).MsDjKazamukiId;
            }
            //DouseiHoukoku.Fusoku = textBox_風速.Text;
            DouseiHoukoku.Fusoku = StringUtils.Escape(textBox_風速.Text);
            //DouseiHoukoku.Nami = textBox_波.Text;
            DouseiHoukoku.Nami = StringUtils.Escape(textBox_波.Text);
            //DouseiHoukoku.Uneri = textBox_うねり.Text;
            DouseiHoukoku.Uneri = StringUtils.Escape(textBox_うねり.Text);
            //DouseiHoukoku.Sitei = textBox_視程.Text;
            DouseiHoukoku.Sitei = StringUtils.Escape(textBox_視程.Text);
            //DouseiHoukoku.Sinro = textBox_針路.Text;
            DouseiHoukoku.Sinro = StringUtils.Escape(textBox_針路.Text);
            //DouseiHoukoku.Sokuryoku = textBox_速力.Text;
            DouseiHoukoku.Sokuryoku = StringUtils.Escape(textBox_速力.Text);
            if (comboBox_船体設備状況.SelectedItem is MsDjSentaisetsubi)
            {
                DouseiHoukoku.MsDjSentaisetsubiID = (comboBox_船体設備状況.SelectedItem as MsDjSentaisetsubi).MsDjSentaisetsubiId;
            }
            if (comboBox_乗組員健康状態.SelectedItem is MsDjKenkoujyoutai)
            {
                DouseiHoukoku.MsDjKenkoujyoutaiID = (comboBox_乗組員健康状態.SelectedItem as MsDjKenkoujyoutai).MsDjKenkoujyoutaiId;
            }
            //DouseiHoukoku.Norikumiinsu = textBox_乗組員数.Text;
            DouseiHoukoku.Norikumiinsu = StringUtils.Escape(textBox_乗組員数.Text);
            //DouseiHoukoku.Bikou = textBox_備考.Text;
            DouseiHoukoku.Bikou = StringUtils.Escape(textBox_備考.Text);
        }

        private DateTime ConvertDate(NullableDateTimePicker dateTimePicker, TextBox textBox)
        {
            if (dateTimePicker.Value == null)
            {
                return DateTime.MinValue;
            }

            DateTime selDay = (DateTime)dateTimePicker.Value;
            if (textBox.Text == null || textBox.Text.Length == 0)
            {
                return selDay;
            }
            String inputTime = textBox.Text;
            return new DateTime(selDay.Year, selDay.Month, selDay.Day, int.Parse(inputTime.Substring(0, 2)), int.Parse(inputTime.Substring(2, 2)), 0);
        }
        private string GetPortId(ComboBox combo)
        {
            if (combo.SelectedItem is MsBasho)
            {
                return (combo.SelectedItem as MsBasho).MsBashoId;
            }
            if (combo.Text.Length > 0)
            {
                string bashoName = combo.Text;
                foreach (MsBasho basho in combo.Items)
                {
                    if (basho.BashoName == bashoName)
                    {
                        combo.SelectedItem = basho;
                        return basho.MsBashoId;
                    }
                }
            }

            return null;
        }

        private bool InsertOrUpdate()
        {
            bool result = true;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    if (DouseiHoukoku.IsNew())
                    {
                        DouseiHoukoku.DjDouseiHoukokuID = System.Guid.NewGuid().ToString();
                    }
                    DouseiHoukoku.RenewUserID = 同期Client.LOGIN_USER.MsUserID;
                    DouseiHoukoku.RenewDate = DateTime.Now;

                    SyncTableSaver.InsertOrUpdate(DouseiHoukoku, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);
                    dbConnect.Commit();

                    DjDouseiHoukoku retHoukoku = DjDouseiHoukoku.GetRecord(同期Client.LOGIN_USER, DouseiHoukoku.DjDouseiHoukokuID);
                    if (retHoukoku != null)
                    {
                        DouseiHoukoku = retHoukoku;
                    }
                    else
                    {
                        result = false;
                    }
                    result = true;
                }
                catch (Exception e)
                {
                    dbConnect.RollBack();
                    result = false;
                }
            }
            return result;
        }
    }
}
