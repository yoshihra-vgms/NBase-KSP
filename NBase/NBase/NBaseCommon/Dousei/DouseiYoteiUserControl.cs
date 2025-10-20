using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using System.Text.RegularExpressions;

namespace NBaseCommon
{
    public partial class DouseiYoteiUserControl : UserControl
    {
        public enum ModeEnum { 積み, 揚げ } ;
        private ModeEnum Mode;
        private List<MsBasho> MsBasho_list = null;
        private List<MsKichi> MsKichi_list = null;
        private List<MsCargo> MsCargo_list = null;
        private List<MsDjTani> MsDjTani_list = null;
        private List<MsCustomer> MsCustomer_list = null;

        private DjDousei Dousei;

        private int TuminiMax = 5;
        private string Title;
        private string ErrMsg;

        public DouseiYoteiUserControl()
        {
            InitializeComponent();

            //singleLineCombo_代理店.selected += new NBaseUtil.SingleLineCombo.SelectedEventHandler();
            //singleLineCombo_荷主.selected += new NBaseUtil.SingleLineCombo.SelectedEventHandler();

            SetMode(ModeEnum.積み);
        }

        public void SetMode(ModeEnum mode)
        {
            MsBasho_list = new List<MsBasho>();
            MsKichi_list = new List<MsKichi>();
            MsCargo_list = new List<MsCargo>();
            MsDjTani_list = new List<MsDjTani>();
            MsCustomer_list = new List<MsCustomer>();

            SetMode(ModeEnum.積み, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
        }
        public void SetMode(ModeEnum mode, List<MsBasho> msBasho_list, List<MsKichi> msKichi_list, List<MsCargo> msCargo_list, List<MsDjTani> msDjTani_list, List<MsCustomer> msCustomer_list)
        {
            this.Mode = mode;
            this.MsBasho_list = msBasho_list;
            this.MsKichi_list = msKichi_list;
            this.MsCargo_list = msCargo_list;
            this.MsDjTani_list = msDjTani_list;
            this.MsCustomer_list = msCustomer_list;

            Initialize();
        }

        private void Initialize()
        {
            if (Mode == ModeEnum.積み)
            {
                Title = "【荷役：積み】";
                label_Header.Text = Title;
                button_Add.Text = "積荷追加";
                //panel_Base.BackColor = Color.FromArgb(204, 231, 247);
                panel_Base.BackColor = Common.ColorTumi;
            }
            else
            {
                Title = "【荷役：揚げ】";
                label_Header.Text = Title;
                button_Add.Text = "揚荷追加";
                //panel_Base.BackColor = Color.FromArgb(166, 251, 174);
                panel_Base.BackColor = Common.ColorAge;
            }


            // 港
            //comboBox_港.Items.Clear();
            //comboBox_港.Items.Add("");
            //foreach (MsBasho basho in MsBasho_list)
            //{
            //    comboBox_港.Items.Add(basho);
            //    comboBox_港.AutoCompleteCustomSource.Add(basho.BashoName);
            //}
            //comboBox_港.AutoCompleteMode = AutoCompleteMode.Suggest;
            //comboBox_港.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //comboBox_港.SelectedIndex = 0;

            singleLineCombo_港.Items.Clear();
            singleLineCombo_港.Items.Add("");
            foreach (MsBasho basho in MsBasho_list)
            {
                singleLineCombo_港.Items.Add(basho);
                singleLineCombo_港.AutoCompleteCustomSource.Add(basho.BashoName);
            }
            singleLineCombo_港.Text = "";

            singleLineCombo_港.Width = 125;


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

            // 代理店
            singleLineCombo_代理店.Items.Clear();
            singleLineCombo_代理店.Items.Add("");
            foreach (MsCustomer customer in MsCustomer_list)
            {
                // 2014.02.10 ２０１３年度改造
                //if (customer.Shubetsu == (int)MsCustomer.種別.代理店)
                if (customer.Is代理店())
                {
                    singleLineCombo_代理店.Items.Add(customer);
                    singleLineCombo_代理店.AutoCompleteCustomSource.Add(customer.CustomerName);
                }
            }
            singleLineCombo_代理店.Text = "";

            // 荷主
            singleLineCombo_荷主.Items.Clear();
            singleLineCombo_荷主.Items.Add("");
            foreach (MsCustomer customer in MsCustomer_list)
            {
                // 2014.02.10 ２０１３年度改造
                //if (customer.Shubetsu == (int)MsCustomer.種別.荷主)
                if (customer.Is荷主())
                {
                    singleLineCombo_荷主.Items.Add(customer);
                    singleLineCombo_荷主.AutoCompleteCustomSource.Add(customer.CustomerName);
                }
            }
            singleLineCombo_荷主.Text = "";

            // 初期状態で２個パネルを用意する
            flowLayoutPanel_Tumini.Controls.Clear();
            TuminiPanelAdd();
            TuminiPanelAdd();

            Dousei = new DjDousei();
        }

        public void SetDousei(DjDousei dousei)
        {
            Dousei = dousei;

            if (Dousei.PlanNiyakuStart == DateTime.MinValue)
                return;

            dateTimePicker.Value = Dousei.PlanNiyakuStart;
            //foreach (MsBasho basho in MsBasho_list)
            //{
            //    if (basho.MsBashoId == Dousei.MsBashoID)
            //    {
            //        comboBox_港.SelectedItem = basho;
            //        break;
            //    }
            //}
            {
                var basho = MsBasho_list.Where(o => o.MsBashoId == Dousei.MsBashoID).FirstOrDefault();
                if (basho != null)
                {
                    singleLineCombo_港.Text = basho.ToString();
                }
            }

            foreach (MsKichi kichi in MsKichi_list)
            {
                if (kichi.MsKichiId == Dousei.MsKichiID)
                {
                    comboBox_基地.SelectedItem = kichi;
                    break;
                }
            }
            foreach (MsCustomer customer in MsCustomer_list)
            {
                if (customer.MsCustomerID == Dousei.DairitenID)
                {
                    singleLineCombo_代理店.Text = customer.CustomerName;
                    break;
                }
            }
            foreach (MsCustomer customer in MsCustomer_list)
            {
                if (customer.MsCustomerID == Dousei.NinushiID)
                {
                    singleLineCombo_荷主.Text = customer.CustomerName;
                    break;
                }
            }
            textBox_備考.Text = Dousei.Bikou;

            textBox_荷役開始.Text = 時間Format(Dousei.PlanNiyakuStart);
            textBox_入港.Text = 時間Format(Dousei.PlanNyuko);
            textBox_着桟.Text = 時間Format(Dousei.PlanChakusan);
            textBox_離桟.Text = 時間Format(Dousei.PlanRisan);
            textBox_出港.Text = 時間Format(Dousei.PlanShukou);

            int cnt = 0;
            foreach (DjDouseiCargo douseiCargo in Dousei.DjDouseiCargos)
            {
                TuminiUserControl ctrl = null;
                if (cnt < flowLayoutPanel_Tumini.Controls.Count)
                {
                    ctrl = flowLayoutPanel_Tumini.Controls[cnt] as TuminiUserControl;
                }
                else
                {
                    TuminiPanelAdd();
                    ctrl = flowLayoutPanel_Tumini.Controls[cnt] as TuminiUserControl;
                }
                cnt++;

                ctrl.SetInstance(douseiCargo);
            }
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            TuminiPanelAdd();
        }

        public void TuminiPanelAdd()
        {
            int no = flowLayoutPanel_Tumini.Controls.Count;
            if (no >= TuminiMax)
            {
                ErrMsg = "追加できる積荷は、最大 " + TuminiMax.ToString() + " 品目までです";
                Error();
                return;
            }
            TuminiUserControl ctrl = null;
            if (Mode == ModeEnum.積み)
            {
                ctrl = new TuminiUserControl(TuminiUserControl.ModeEnum.積み, no + 1, MsCargo_list, MsDjTani_list);
            }
            else
            {
                ctrl = new TuminiUserControl(TuminiUserControl.ModeEnum.揚げ, no + 1, MsCargo_list, MsDjTani_list);
            }
            flowLayoutPanel_Tumini.Controls.Add(ctrl);
        }

        public DjDousei GetInstance()
        {
            if (Validation() == false)
            {
                return null;
            }
            if (dateTimePicker.Value == null)
            {
                if (Dousei.DjDouseiID == null || Dousei.DjDouseiID == "")
                {
                    // 動静情報なし
                    return null;
                }

                if (Dousei.ResultNyuko != DateTime.MinValue)
                {
                    // 2013.03.23 : 実績日は着棧日であるとコメントを受けたので改造
                    // 実績が登録されている場合、予定情報のみクリアする
                    //Dousei.DouseiDate = Dousei.ResultNyuko;
                    Dousei.DouseiDate = Dousei.ResultChakusan;
                    Dousei.MsBashoID = Dousei.ResultMsBashoID;
                    Dousei.MsKichiID = Dousei.ResultMsKichiID;

                    Dousei.PlanNiyakuStart = DateTime.MinValue;
                    Dousei.PlanNyuko = DateTime.MinValue;
                    Dousei.PlanChakusan = DateTime.MinValue;
                    Dousei.PlanRisan = DateTime.MinValue;
                    Dousei.PlanShukou = DateTime.MinValue;
                    
                    Dousei.DairitenID = "";
                    Dousei.NinushiID = "";
                    Dousei.Bikou = "";
                }
                else
                {
                    // 実績がない場合、予定情報を削除する
                    Dousei.DeleteFlag = NBaseCommon.Common.DeleteFlag_削除;
                }
                // 積荷情報は削除とする
                foreach (DjDouseiCargo cargo in Dousei.DjDouseiCargos)
                {
                    cargo.DeleteFlag = NBaseCommon.Common.DeleteFlag_削除;
                }

            }
            else
            {
                DateTime baseDate = (DateTime)dateTimePicker.Value;

                if (Dousei.ResultNyuko == DateTime.MinValue)
                {
                    Dousei.DouseiDate = 日時Format(baseDate, textBox_荷役開始.Text);　// 予定の日時は、荷役開始日時
                }

                //Dousei.MsBashoID = (comboBox_港.SelectedItem as MsBasho).MsBashoId;
                if (singleLineCombo_港.SelectedItem is MsBasho)
                {
                    Dousei.MsBashoID = (singleLineCombo_港.SelectedItem as MsBasho).MsBashoId;
                }
                else
                {
                    Dousei.MsBashoID = null;
                }

                if (comboBox_基地.SelectedItem is MsKichi)
                {
                    Dousei.MsKichiID = (comboBox_基地.SelectedItem as MsKichi).MsKichiId;
                }
                else
                {
                    Dousei.MsKichiID = "";
                }
                if (singleLineCombo_代理店.SelectedItem is MsCustomer)
                {
                    Dousei.DairitenID = (singleLineCombo_代理店.SelectedItem as MsCustomer).MsCustomerID;
                }
                else
                {
                    Dousei.DairitenID = "";
                }
                if (singleLineCombo_荷主.SelectedItem is MsCustomer)
                {
                    Dousei.NinushiID = (singleLineCombo_荷主.SelectedItem as MsCustomer).MsCustomerID;
                }
                else
                {
                    Dousei.NinushiID = "";
                }
                Dousei.Bikou = textBox_備考.Text;

                Dousei.PlanNiyakuStart = 日時Format(baseDate, textBox_荷役開始.Text);
                Dousei.PlanNyuko = 日時Format(baseDate, textBox_入港.Text);
                Dousei.PlanChakusan = 日時Format(baseDate, textBox_着桟.Text);
                Dousei.PlanRisan = 日時Format(baseDate, textBox_離桟.Text);
                Dousei.PlanShukou = 日時Format(baseDate, textBox_出港.Text);

                // 2013.05.20 -->
                //Dousei.DjDouseiCargos.Clear();
                if (Dousei.DjDouseiCargos != null)
                {
                    Dousei.DjDouseiCargos.Clear();
                }
                else
                {
                    Dousei.DjDouseiCargos = new List<DjDouseiCargo>();
                }
                // <--

                for (int i = 0; i < flowLayoutPanel_Tumini.Controls.Count; i++)
                {
                    TuminiUserControl ctrl = flowLayoutPanel_Tumini.Controls[i] as TuminiUserControl;
                    DjDouseiCargo douseiCargo = ctrl.GetInstance();
                    douseiCargo.PlanResultFlag = (int)DjDouseiCargo.PLAN_RESULT_FLAG.PLAN;
                    if (i > 0)
                    {
                        if (douseiCargo.MsCargoID == int.MinValue)
                        {
                            // 積荷２以降は、品目が選択されていないければ無視する
                            if (douseiCargo.DjDouseiCargoID == null || douseiCargo.DjDouseiCargoID == "")
                            {
                                continue;
                            }
                            else
                            {
                                douseiCargo.DeleteFlag = NBaseCommon.Common.DeleteFlag_削除;
                                Dousei.DjDouseiCargos.Add(douseiCargo);
                                continue;
                            }
                        }
                    }
                    douseiCargo.LineNo = (i + 1).ToString();
                    Dousei.DjDouseiCargos.Add(douseiCargo);
                }
            }

            return Dousei;
        }

        public bool Validation()
        {
            // 日付が入力されている場合のみ有効とする
            if (dateTimePicker.Value != null)
            {
                //if (comboBox_港.SelectedIndex == 0)
                //{
                //    ErrMsg = "港を選択して下さい";
                //    Error();
                //    return false;
                //}
                //// 2013.05.20 -->
                //if (!(comboBox_港.SelectedItem is MsBasho))
                //{
                //    ErrMsg = "港を選択して下さい";
                //    Error();
                //    return false;
                //}

                if (singleLineCombo_港.Text.Length > 0 && !(singleLineCombo_港.SelectedItem is MsBasho))
                {
                    ErrMsg = "港を選択して下さい";
                    Error();
                    return false;
                }

                // <--
                // 2011.10.04 必須項目から外す
                //if (comboBox_基地.SelectedIndex == 0)
                //{
                //    ErrMsg = "基地を選択して下さい";
                //    Error();
                //    return false;
                //}
                if (textBox_荷役開始.Text == "")
                {
                    // 2013.07.23 : 必須から外す
                    //ErrMsg = "荷役開始予定時間を入力して下さい";
                    //Error();
                    //return false;
                }
                else
                {
                    if (_時間Validation("荷役開始予定時間", textBox_荷役開始.Text) == false)
                    {
                        Error();
                        return false;
                    }
                }
                if (textBox_入港.Text != "")
                {
                    if (_時間Validation("入港予定時間", textBox_入港.Text) == false)
                    {
                        Error();
                        return false;
                    }
                }
                if (textBox_着桟.Text != "")
                {
                    if (_時間Validation("着桟予定時間", textBox_着桟.Text) == false)
                    {
                        Error();
                        return false;
                    }
                }
                if (textBox_離桟.Text != "")
                {
                    if (_時間Validation("離桟予定時間", textBox_離桟.Text) == false)
                    {
                        Error();
                        return false;
                    }
                }
                if (textBox_出港.Text != "")
                {
                    if (_時間Validation("出港予定時間", textBox_出港.Text) == false)
                    {
                        Error();
                        return false;
                    }
                }
                if (singleLineCombo_代理店.Text.Length > 0 && !(singleLineCombo_代理店.SelectedItem is MsCustomer))
                {
                    ErrMsg = "代理店を選択して下さい\n(入力された代理店はマスタ登録されていません)";
                    Error();
                    return false;
                }
                if (singleLineCombo_荷主.Text.Length > 0 && !(singleLineCombo_荷主.SelectedItem is MsCustomer))
                {
                    ErrMsg = "荷主を選択して下さい\n(入力された荷主はマスタ登録されていません)";
                    Error();
                    return false;
                }
                for (int i = 0; i < flowLayoutPanel_Tumini.Controls.Count; i++)
                {
                    TuminiUserControl ctrl = flowLayoutPanel_Tumini.Controls[i] as TuminiUserControl;
                    DjDouseiCargo douseiCargo = ctrl.GetInstance();

                    if (i > 0)
                    {
                        // 積荷２以降は、品目が選択されていないければ無視する
                        if (douseiCargo.MsCargoID == int.MinValue)
                        {
                            continue;
                        }
                    }

                    if (douseiCargo.MsCargoID == int.MinValue)
                    {
                        ErrMsg = "積荷" + (i + 1).ToString() + "を選択して下さい";
                        Error();
                        return false;
                    }
                    if (douseiCargo.Qtty == decimal.MinValue)
                    {
                        ErrMsg = "積荷" + (i + 1).ToString() + "の数量を正しく入力して下さい";
                        Error();
                        return false;
                    }
                    if (NBaseCommon.Number.CheckValue((double)douseiCargo.Qtty, 4, 3) == false)
                    {
                        ErrMsg = "積荷" + (i + 1).ToString() + "の数量を正しく入力して下さい";
                        Error();
                        return false;
                    }
                    if (douseiCargo.MsDjTaniID == null)
                    {
                        ErrMsg = "積荷" + (i + 1).ToString() + "の単位を選択して下さい";
                        Error();
                        return false;
                    }
                }
            }
            return true;
        }

        private bool _時間Validation( string 時間Label, string timeStr)
        {
            if (timeStr.Length != 4)
            {
                //ErrMsg = Title + "の" + 時間Label + "は4桁を入力して下さい";
                ErrMsg = 時間Label + "は0000〜2359の範囲で入力してください";
                return false;
            }
            if (Regex.IsMatch(timeStr, "\\d{4}") == false)
            {
                //ErrMsg = Title + "の" + 時間Label + "4桁を入力して下さい";
                ErrMsg = 時間Label + "は0000〜2359の範囲で入力してください";
                return false;
            }
            if (TimeCheck(timeStr) == false)
            {
                //ErrMsg = Title + "の" + 時間Label + "が不正です";
                ErrMsg = 時間Label + "は0000〜2359の範囲で入力してください";
                return false;
            }
            return true;
        }

        private bool TimeCheck(string TIME)
        {
            try
            {
                int h = int.Parse(TIME.Substring(0, 2));
                if (h >= 24)
                    return false;

                int m = int.Parse(TIME.Substring(2, 2));
                if (m >= 60)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        private DateTime 日時Format(DateTime day, string time)
        {
            if (time.Length != 4)
            {
                // 2013.07.22 : 時間は必須としないとコメントを受ける→時間の入力なしの場合、"00:00"とする
                //return DateTime.MinValue;
                time = "0000";
            }

            string dateTimeStr = day.ToShortDateString() + " " + time.Substring(0, 2) + ":" + time.Substring(2, 2);
            return DateTime.Parse(dateTimeStr);
        }

        private string 時間Format(DateTime datetime)
        {
            if (datetime == DateTime.MinValue)
            {
                return "";
            }

            string TimeStr = datetime.ToShortTimeString();
            string[] TimeStrArray = TimeStr.Split(':');
            return (Convert.ToInt16(TimeStrArray[0])).ToString("00") + (Convert.ToInt16(TimeStrArray[1])).ToString("00");
        }

        private void Error()
        {
            //MessageBox.Show(ErrMsg, "動静予定", MessageBoxButtons.OK, MessageBoxIcon.Error);
            MessageBox.Show(Title + "の" + ErrMsg, "動静予定", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            dateTimePicker.Value = null;
            //comboBox_港.SelectedIndex = 0;
            singleLineCombo_港.Text = "";
            comboBox_基地.SelectedIndex = 0;
            singleLineCombo_代理店.Text = "";
            singleLineCombo_荷主.Text = "";
            textBox_備考.Text = "";
            textBox_荷役開始.Text = "";
            textBox_入港.Text = "";
            textBox_着桟.Text = "";
            textBox_離桟.Text = "";
            textBox_出港.Text = "";

            for (int i = flowLayoutPanel_Tumini.Controls.Count; i > 2; i--)
            {
                flowLayoutPanel_Tumini.Controls.RemoveAt(i-1);
            }
            for (int i = 0; i < flowLayoutPanel_Tumini.Controls.Count; i++)
            {
                TuminiUserControl ctrl = flowLayoutPanel_Tumini.Controls[i] as TuminiUserControl;
                ctrl.Clear();
            }
        }

        public void ReadOnly()
        {
            button_Add.Visible = false;
            button_Clear.Visible = false;

            dateTimePicker.Enabled = false;
            //comboBox_港.Enabled = false;
            singleLineCombo_港.Enabled = false;
            comboBox_基地.Enabled = false;
            singleLineCombo_代理店.Enabled = false;
            singleLineCombo_荷主.Enabled = false;
            textBox_備考.ReadOnly = true;
            textBox_荷役開始.ReadOnly = true;
            textBox_入港.ReadOnly = true;
            textBox_着桟.ReadOnly = true;
            textBox_離桟.ReadOnly = true;
            textBox_出港.ReadOnly = true;

            for (int i = 0; i < flowLayoutPanel_Tumini.Controls.Count; i++)
            {
                TuminiUserControl ctrl = flowLayoutPanel_Tumini.Controls[i] as TuminiUserControl;
                ctrl.ReadOnly();
            }
        }


        //============================================
        // 2014年11月度改造
        //============================================
        public void SetDate(DateTime date)
        {
            dateTimePicker.Value = date;
        }
        public void ClearDousei()
        {
            Dousei = new DjDousei();
        }
        public void ClearDate()
        {
            dateTimePicker.Value = null;

            textBox_荷役開始.Text = "";
            textBox_入港.Text = "";
            textBox_着桟.Text = "";
            textBox_離桟.Text = "";
            textBox_出港.Text = "";
        }
        public void ClearTumini()
        {
            // 2014.12.05 積荷はすべてクリアせず、積荷、単位はそのまま、数量のみクリアしたい
            //for (int i = flowLayoutPanel_Tumini.Controls.Count; i > 2; i--)
            //{
            //    flowLayoutPanel_Tumini.Controls.RemoveAt(i - 1);
            //}
            //for (int i = 0; i < flowLayoutPanel_Tumini.Controls.Count; i++)
            //{
            //    TuminiUserControl ctrl = flowLayoutPanel_Tumini.Controls[i] as TuminiUserControl;
            //    ctrl.SetInstance(new DjDouseiCargo());
            //    ctrl.Clear();
            //}
            for (int i = 0; i < flowLayoutPanel_Tumini.Controls.Count; i++)
            {
                TuminiUserControl ctrl = flowLayoutPanel_Tumini.Controls[i] as TuminiUserControl;
                ctrl.CopyInstance();
            }
        }


    }
}
