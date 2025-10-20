using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Senin
{
    public partial class 勤怠管理情報Form : Form
    {
        private SiLaborManagementRecordBook laborManagementRecordBook;
        private List<SiRequiredNumberOfDays> requiredNumberOfDaysList;
        private List<SiNightSetting> nightSettingList;

        public 勤怠管理情報Form()
        {
            InitializeComponent();
        }

        private void button登録_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void button閉じる_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Az勤怠管理情報Form_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            DateTime BisunessYearStart = DateTimeUtils.年度開始日();
            dateTimePicker_基準労働期間起算日.Value = BisunessYearStart;
            dateTimePicker_基準労働期間末日.Value = BisunessYearStart.AddYears(1).AddDays(-1);

            InitComboBox所属会社();

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                laborManagementRecordBook = serviceClient.SiLaborManagementRecordBook_GetRecord(NBaseCommon.Common.LoginUser);
                requiredNumberOfDaysList = serviceClient.SiRequiredNumberOfDays_GetRecords(NBaseCommon.Common.LoginUser);
                nightSettingList = serviceClient.SiNightSetting_GetRecords(NBaseCommon.Common.LoginUser);
            }

            //======================================
            // 労務管理記録簿
            //======================================
            if (laborManagementRecordBook != null)
            {
                radioButton_時間外労働協定_有.Checked = laborManagementRecordBook.OvertimeWorkAgreement == 1 ? true : false;
                radioButton_時間外労働協定_無.Checked = laborManagementRecordBook.OvertimeWorkAgreement == 0 ? true : false;

                radioButton_補償休日労働協定_有.Checked = laborManagementRecordBook.CompensationHolidayLaborAgreement == 1 ? true : false;
                radioButton_補償休日労働協定_無.Checked = laborManagementRecordBook.CompensationHolidayLaborAgreement == 0 ? true : false;

                radioButton_休息時間分割協定_有.Checked = laborManagementRecordBook.BreakTimeDivisionAgreement == 1 ? true : false;
                radioButton_休息時間分割協定_無.Checked = laborManagementRecordBook.BreakTimeDivisionAgreement == 0 ? true : false;

                textBox_基準労働期間.Text = laborManagementRecordBook.StandardWorkingPeriod.ToString();

                dateTimePicker_基準労働期間起算日.Value = laborManagementRecordBook.StartStandardWorkingPeriod;
                dateTimePicker_基準労働期間末日.Value = laborManagementRecordBook.LastStandardWorkingPeriod;

                var vals = requiredNumberOfDaysList.Where(obj => obj.Kind == 0);
                if (vals != null && vals.Count()>0)
                {
                    comboBox_所属会社_T1.SelectedItem = SeninTableCache.instance().GetMsSeninCompanyList(NBaseCommon.Common.LoginUser).Where(o => o.MsSeninCompanyID == vals.First().MsSeninCompanyID).FirstOrDefault();
                    textBox_必須日数_T1.Text = vals.First().Days.ToString();

                    if (vals.First().SiRequiredNumberOfDaysID != vals.Last().SiRequiredNumberOfDaysID)
                    {
                        comboBox_所属会社_T2.SelectedItem = SeninTableCache.instance().GetMsSeninCompanyList(NBaseCommon.Common.LoginUser).Where(o => o.MsSeninCompanyID == vals.Last().MsSeninCompanyID).FirstOrDefault();
                        textBox_必須日数_T2.Text = vals.Last().Days.ToString();

                    }
                }

                vals = requiredNumberOfDaysList.Where(obj => obj.Kind == 1);
                if (vals != null && vals.Count() > 0)
                {
                    comboBox_所属会社_A1.SelectedItem = SeninTableCache.instance().GetMsSeninCompanyList(NBaseCommon.Common.LoginUser).Where(o => o.MsSeninCompanyID == vals.First().MsSeninCompanyID).FirstOrDefault();
                    textBox_必須日数_A1.Text = vals.First().Days.ToString();

                    if (vals.First().SiRequiredNumberOfDaysID != vals.Last().SiRequiredNumberOfDaysID)
                    {
                        comboBox_所属会社_A2.SelectedItem = SeninTableCache.instance().GetMsSeninCompanyList(NBaseCommon.Common.LoginUser).Where(o => o.MsSeninCompanyID == vals.Last().MsSeninCompanyID).FirstOrDefault();
                        textBox_必須日数_A2.Text = vals.Last().Days.ToString();

                    }
                }
            }

            //======================================
            // 夜間設定
            //======================================
            nightSettingPanelUserControl1.SeninCompanyList = SeninTableCache.instance().GetMsSeninCompanyList(NBaseCommon.Common.LoginUser);
            nightSettingPanelUserControl1.VesselList = SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser).Where(o => (o.SeninEnabled == 1 || o.SeninResults == 1)).ToList();
            nightSettingPanelUserControl2.SeninCompanyList = SeninTableCache.instance().GetMsSeninCompanyList(NBaseCommon.Common.LoginUser);
            nightSettingPanelUserControl2.VesselList = SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser).Where(o => (o.SeninEnabled == 1 || o.SeninResults == 1)).ToList();

            if (nightSettingList != null)
            {
                int counter = 0;
                var seninCompanyIds = nightSettingList.Select(o => o.MsSeninCompanyID).Distinct();
                foreach(string id in seninCompanyIds)
                {
                    var vals = nightSettingList.Where(o => o.MsSeninCompanyID == id);

                    if (counter == 0) nightSettingPanelUserControl1.NightSettings = vals.ToList();
                    else nightSettingPanelUserControl2.NightSettings = vals.ToList();

                    counter++;

                    if (counter == 2)
                        break;
                }
            }


            // 「夜間設定」タブは非表示
            tabControl1.TabPages.RemoveAt(1);

        }

        private void InitComboBox所属会社()
        {
            comboBox_所属会社_T1.Items.Clear();
            comboBox_所属会社_T2.Items.Clear();
            comboBox_所属会社_A1.Items.Clear();
            comboBox_所属会社_A2.Items.Clear();

            comboBox_所属会社_T1.Items.Add("");
            comboBox_所属会社_T2.Items.Add("");
            comboBox_所属会社_A1.Items.Add("");
            comboBox_所属会社_A2.Items.Add("");

            foreach (MsSeninCompany s in SeninTableCache.instance().GetMsSeninCompanyList(NBaseCommon.Common.LoginUser))
            {
                comboBox_所属会社_T1.Items.Add(s);
                comboBox_所属会社_T2.Items.Add(s);
                comboBox_所属会社_A1.Items.Add(s);
                comboBox_所属会社_A2.Items.Add(s);
            }

            comboBox_所属会社_T1.SelectedIndex = 0;
            comboBox_所属会社_T2.SelectedIndex = 0;
            comboBox_所属会社_A1.SelectedIndex = 0;
            comboBox_所属会社_A2.SelectedIndex = 0;
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
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 入力値の確認
        /// </summary>
        /// <returns></returns>
        #region private bool ValidateFields()
        private bool ValidateFields()
        {

            //======================================
            // 労務管理記録簿
            //======================================
            //基準労働期間数値チェック
            //if(textBox_基準労働期間.Text)

            //基準労働期間　決算日～末日
            if (dateTimePicker_基準労働期間起算日.Value >= dateTimePicker_基準労働期間末日.Value)
            {
                MessageBox.Show(this, "[労務管理記録簿]労働期間の末日は起算日よりあとを指定してください。");
                return false;
            }

            //陸上休暇
            if( textBox_必須日数_T1.Text.Length >0 && !(comboBox_所属会社_T1.SelectedItem is MsSeninCompany) )
            {
                MessageBox.Show(this, "[労務管理記録簿]陸上休暇(合計)で船が選択されていません。");
                return false;
            }
            if (textBox_必須日数_T2.Text.Length > 0 && !(comboBox_所属会社_T2.SelectedItem is MsSeninCompany))
            {
                MessageBox.Show(this, "[労務管理記録簿]陸上休暇(合計)で船が選択されていません。");
                return false;
            }
            if (comboBox_所属会社_T1.SelectedItem is MsSeninCompany && comboBox_所属会社_T2.SelectedItem is MsSeninCompany
             && (comboBox_所属会社_T1.SelectedItem as MsSeninCompany).MsSeninCompanyID == (comboBox_所属会社_T2.SelectedItem as MsSeninCompany).MsSeninCompanyID)
            {
                MessageBox.Show(this, "[労務管理記録簿]陸上休暇(合計)で同じ所属会社が選択されています。");
                return false;
            }
            if (comboBox_所属会社_T1.SelectedItem is MsSeninCompany && !Validate日数(textBox_必須日数_T1))
            {
                MessageBox.Show(this, "[労務管理記録簿]陸上休暇(合計)の日数を入力してください。");
                return false;
            }
            if (comboBox_所属会社_T2.SelectedItem is MsSeninCompany && !Validate日数(textBox_必須日数_T2))
            {
                MessageBox.Show(this, "[労務管理記録簿]陸上休暇(合計)の日数を入力してください。");
                return false;
            }

            //陸上休暇A
            if (textBox_必須日数_A1.Text.Length > 0 && !(comboBox_所属会社_A1.SelectedItem is MsSeninCompany))
            {
                MessageBox.Show(this, "[労務管理記録簿]陸上休暇Aで船が選択されていません。");
                return false;
            }
            if (textBox_必須日数_A2.Text.Length > 0 && !(comboBox_所属会社_A2.SelectedItem is MsSeninCompany))
            {
                MessageBox.Show(this, "[労務管理記録簿]陸上休暇Aで船が選択されていません。");
                return false;
            }
            if (comboBox_所属会社_A1.SelectedItem is MsSeninCompany && comboBox_所属会社_A2.SelectedItem is MsSeninCompany
             && (comboBox_所属会社_A1.SelectedItem as MsSeninCompany).MsSeninCompanyID == (comboBox_所属会社_A2.SelectedItem as MsSeninCompany).MsSeninCompanyID)
            {
                MessageBox.Show(this, "[労務管理記録簿]陸上休暇Aで同じ所属会社が選択されています。");
                return false;
            }
            if (comboBox_所属会社_A1.SelectedItem is MsSeninCompany && !Validate日数(textBox_必須日数_A1))
            {
                MessageBox.Show(this, "[労務管理記録簿]陸上休暇Aの日数を入力してください。");
                return false;
            }
            if (comboBox_所属会社_A2.SelectedItem is MsSeninCompany && !Validate日数(textBox_必須日数_A2))
            {
                MessageBox.Show(this, "[労務管理記録簿]陸上休暇Aの日数を入力してください。");
                return false;
            }


            //======================================
            // 夜間設定
            //======================================
            List< SiNightSetting> settings1 =  nightSettingPanelUserControl1.NightSettings;
            List<SiNightSetting> settings2 = nightSettingPanelUserControl2.NightSettings;

            //所属会社が選択されていなければチェックの必要なし
            if (nightSettingPanelUserControl1.SelectCompany == "" && nightSettingPanelUserControl2.SelectCompany == "") return true;

            if (nightSettingPanelUserControl1.SelectCompany == nightSettingPanelUserControl2.SelectCompany)
            {
                MessageBox.Show(this, "[夜間設定]同じ所属会社が選択されています。");
                return false;
            }
            if (nightSettingPanelUserControl1.SelectCompany != "" && settings1.Count == 0)
            {
                MessageBox.Show(this, "[夜間設定]船が選択されていません。");
                return false;
            }
            if (nightSettingPanelUserControl2.SelectCompany != "" && settings2.Count == 0)
            {
                MessageBox.Show(this, "[夜間設定]船が選択されていません。");
                return false;
            }

            //所属会社内で同じ船か
            if (settings1.Count > 0)
            {
                if (!Validate同船(settings1)) return false;
            }
            if (settings2.Count > 0)
            {
                if (!Validate同船(settings2)) return false;
            }
            //船ごと時間チェック
            if (settings1.Count > 0 )
            {
                if (!Validate時刻(settings1)) return false;
            }
            if (settings2.Count > 0)
            {
                if (!Validate時刻(settings2)) return false;
            }
            return true;
        }
        #endregion


        private bool Validate日数(TextBox txtb)
        {
            if ( txtb.Text == "")
            {
                return false;
            }
            //数値の範囲チェック

            return true;
        }

        #region  夜間設定タブで使用するValidate
        private bool Validate同船(List<SiNightSetting> settings)
        {
            List<int> vesselIDs = new List<int>();

            foreach (SiNightSetting setting in settings)
            {
                if (vesselIDs.Contains(setting.MsVesselID))
                {
                    MessageBox.Show(this, "[夜間設定]所属会社内で同じ船が選択されています。");
                    return false;
                }
                vesselIDs.Add(setting.MsVesselID);
            }
            if (vesselIDs.Count == 0)
            {
                MessageBox.Show(this, "[夜間設定]船が選択されていません。");
                return false;
            }
            
            return true;
        }

        private bool Validate時刻(List<SiNightSetting> settings)
        {
            foreach (SiNightSetting setting in settings)
            {
                if( !isTime(setting.StartTime))
                {
                    MessageBox.Show(this, "[夜間設定]開始時刻が不正です。");
                    return false;
                }
                if (!isTime(setting.EndTime))
                {
                    MessageBox.Show(this, "[夜間設定]終了時刻が不正です。");
                    return false;
                }

                //if (setting.StartTime >= setting.EndTime)
                //{
                //    MessageBox.Show(this, "[夜間設定]終了時刻は開始時刻より大きい数値を入力してください。");
                //    return false;
                //}
            }
            return true;
        }

        private bool isTime(int timenum)
        {
            int h = timenum / 100;
            int m = timenum % 100;

            if (h < 0 || h > 23)
            {
                return false;
            }
            if (m < 0 || m > 59)
            {
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 入力値を取得し、情報にセットする
        /// </summary>
        #region FillInstance()
        private void FillInstance()
        {

            //======================================
            // 労務管理記録簿
            //======================================
            if (laborManagementRecordBook == null)
                laborManagementRecordBook = new SiLaborManagementRecordBook();

            laborManagementRecordBook.OvertimeWorkAgreement = radioButton_時間外労働協定_有.Checked ? 1 : 0;
            laborManagementRecordBook.CompensationHolidayLaborAgreement = radioButton_補償休日労働協定_有.Checked ? 1 : 0;
            laborManagementRecordBook.BreakTimeDivisionAgreement = radioButton_休息時間分割協定_有.Checked ? 1 : 0;

            int val = 0;

            int.TryParse(textBox_基準労働期間.Text, out val);
            laborManagementRecordBook.StandardWorkingPeriod = val;

            laborManagementRecordBook.StartStandardWorkingPeriod = dateTimePicker_基準労働期間起算日.Value;
            laborManagementRecordBook.LastStandardWorkingPeriod = dateTimePicker_基準労働期間末日.Value;


            // 一旦、すべて削除フラグを立てる
            requiredNumberOfDaysList.ForEach(o => { o.DeleteFlag = 1; });

            if (comboBox_所属会社_T1.SelectedItem is MsSeninCompany)
            {
                var seninCompanyId = (comboBox_所属会社_T1.SelectedItem as MsSeninCompany).MsSeninCompanyID;

                var info = requiredNumberOfDaysList.Where(o => o.Kind == 0 && o.MsSeninCompanyID == seninCompanyId　&& o.DeleteFlag == 1).FirstOrDefault();
                if (info == null)
                {
                    info = new SiRequiredNumberOfDays();
                    requiredNumberOfDaysList.Add(info);

                    info.Kind = 0;
                    info.MsSeninCompanyID = seninCompanyId;
                }
                else
                {
                    info.DeleteFlag = 0;
                }
                int.TryParse(textBox_必須日数_T1.Text, out val);
                info.Days = val;
            }
            if (comboBox_所属会社_T2.SelectedItem is MsSeninCompany)
            {
                var seninCompanyId = (comboBox_所属会社_T2.SelectedItem as MsSeninCompany).MsSeninCompanyID;

                var info = requiredNumberOfDaysList.Where(o => o.Kind == 0 && o.MsSeninCompanyID == seninCompanyId && o.DeleteFlag == 1).FirstOrDefault();
                if (info == null)
                {
                    info = new SiRequiredNumberOfDays();
                    requiredNumberOfDaysList.Add(info);

                    info.Kind = 0;
                    info.MsSeninCompanyID = seninCompanyId;
                }
                else
                {
                    info.DeleteFlag = 0;
                }
                int.TryParse(textBox_必須日数_T2.Text, out val);
                info.Days = val;
            }

            if (comboBox_所属会社_A1.SelectedItem is MsSeninCompany)
            {
                var seninCompanyId = (comboBox_所属会社_A1.SelectedItem as MsSeninCompany).MsSeninCompanyID;

                var info = requiredNumberOfDaysList.Where(o => o.Kind == 1 && o.MsSeninCompanyID == seninCompanyId && o.DeleteFlag == 1).FirstOrDefault();
                if (info == null)
                {
                    info = new SiRequiredNumberOfDays();
                    requiredNumberOfDaysList.Add(info);

                    info.Kind = 1;
                    info.MsSeninCompanyID = seninCompanyId;
                }
                else
                {
                    info.DeleteFlag = 0;
                }
                int.TryParse(textBox_必須日数_A1.Text, out val);
                info.Days = val;
            }
            if (comboBox_所属会社_A2.SelectedItem is MsSeninCompany)
            {
                var seninCompanyId = (comboBox_所属会社_A2.SelectedItem as MsSeninCompany).MsSeninCompanyID;

                var info = requiredNumberOfDaysList.Where(o => o.Kind == 1 && o.MsSeninCompanyID == seninCompanyId && o.DeleteFlag == 1).FirstOrDefault();
                if (info == null)
                {
                    info = new SiRequiredNumberOfDays();
                    requiredNumberOfDaysList.Add(info);

                    info.Kind = 1;
                    info.MsSeninCompanyID = seninCompanyId;
                }
                else
                {
                    info.DeleteFlag = 0;
                }
                int.TryParse(textBox_必須日数_A2.Text, out val);
                info.Days = val;
            }


            //======================================
            // 夜間設定
            //======================================

            var nightSettings1 = nightSettingPanelUserControl1.NightSettings;
            var nightSettings2 = nightSettingPanelUserControl2.NightSettings;

            List<SiNightSetting> upList = new List<SiNightSetting>();
            //upList.AddRange(nightSettings1);
            //upList.AddRange(nightSettings2);

            nightSettingList = upList;
        }
        #endregion


        #region private bool InsertOrUpdate()
        private bool InsertOrUpdate()
        {
            bool result = true;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.BLC_勤怠管理_InsertOrUpdate(NBaseCommon.Common.LoginUser, laborManagementRecordBook, requiredNumberOfDaysList, nightSettingList);
            }

            return result;
        }
        #endregion

        private void textBox数値_KeyPress(object sender, KeyPressEventArgs e)
        {
            //0～9と、バックスペース以外の時は、イベントをキャンセルする
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void comboBox_所属会社_TextChanged(object sender, EventArgs e)
        {
            if (!(sender is ComboBox)) return;

            ComboBox combo = sender as ComboBox;

            if (combo.Name.Contains("T1"))
            {
                if (!(combo.SelectedItem is MsSeninCompany))
                {
                    textBox_必須日数_T1.Text = "";
                }
            }
            else if (combo.Name.Contains("T2"))
            {
                if (!(combo.SelectedItem is MsSeninCompany))
                {
                    textBox_必須日数_T2.Text = "";
                }
            }
            else if (combo.Name.Contains("A1"))
            {
                if (!(combo.SelectedItem is MsSeninCompany))
                {
                    textBox_必須日数_A1.Text = "";
                }
            }
            else if (combo.Name.Contains("A2"))
            {
                if (!(combo.SelectedItem is MsSeninCompany))
                {
                    textBox_必須日数_A2.Text = "";
                }
            }
        }
    }
}
