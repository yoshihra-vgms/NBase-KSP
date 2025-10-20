using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseUtil;
using System.Text.RegularExpressions;

namespace NBaseCommon
{
    public partial class DouseiJissekiUserControl : UserControl
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
        private bool IsInput = false;

        private bool ClearClicked = false;


        public DouseiJissekiUserControl()
        {
            InitializeComponent();
            SetMode(ModeEnum.積み);
        }
        public DouseiJissekiUserControl(ModeEnum mode)
        {
            InitializeComponent();
            SetMode(mode);
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

            string bashoId;
            //if (Dousei.ResultNyuko == DateTime.MinValue && (Dousei.ResultMsBashoID == null || Dousei.ResultMsBashoID.Length == 0))
            if (ClearClicked == true || (Dousei.ResultNyuko == DateTime.MinValue && (Dousei.ResultMsBashoID == null || Dousei.ResultMsBashoID.Length == 0)))
            {
                bashoId = Dousei.MsBashoID;
            }
            else
            {
                bashoId = Dousei.ResultMsBashoID;
            }
            //foreach (MsBasho basho in MsBasho_list)
            //{
            //    if (basho.MsBashoId == bashoId)
            //    {
            //        comboBox_港.SelectedItem = basho;
            //        break;
            //    }
            //}

            bashoId = Dousei.MsBashoID;
            {
                var basho = MsBasho_list.Where(o => o.MsBashoId == bashoId).FirstOrDefault();
                if (basho != null)
                {
                    singleLineCombo_港.Text = basho.ToString();
                }
            }



            string kichiId;
            //if (Dousei.ResultNyuko == DateTime.MinValue && (Dousei.ResultMsKichiID == null || Dousei.ResultMsKichiID.Length == 0))
            if (ClearClicked == true || (Dousei.ResultNyuko == DateTime.MinValue && (Dousei.ResultMsKichiID == null || Dousei.ResultMsKichiID.Length == 0)))
            {
                kichiId = Dousei.MsKichiID;
            }
            else
            {
                kichiId = Dousei.ResultMsKichiID;
            }
            foreach (MsKichi kichi in MsKichi_list)
            {
                if (kichi.MsKichiId == kichiId)
                {
                    comboBox_基地.SelectedItem = kichi;
                    break;
                }
            }
            //if (Dousei.ResultNiyakuStart != DateTime.MinValue)
            if (ClearClicked == false && Dousei.ResultNiyakuStart != DateTime.MinValue)
            {
                dateTimePicker_荷役開始.Value = Dousei.ResultNiyakuStart;
                textBox_荷役開始.Text = 時間Format(Dousei.ResultNiyakuStart);
            }
            else
            {
                dateTimePicker_荷役開始.Value = Dousei.DouseiDate;
                textBox_荷役開始.Text = "";
            }
            //if (Dousei.ResultNiyakuEnd != DateTime.MinValue)
            if (ClearClicked == false && Dousei.ResultNiyakuEnd != DateTime.MinValue)
            {
                dateTimePicker_荷役終了.Value = Dousei.ResultNiyakuEnd;
                textBox_荷役終了.Text = 時間Format(Dousei.ResultNiyakuEnd);
            }
            else
            {
                dateTimePicker_荷役終了.Value = Dousei.DouseiDate;
                textBox_荷役終了.Text = "";
            }
            //if (Dousei.ResultNyuko != DateTime.MinValue)
            if (ClearClicked == false && Dousei.ResultNyuko != DateTime.MinValue)
            {
                dateTimePicker_入港.Value = Dousei.ResultNyuko;
                textBox_入港.Text = 時間Format(Dousei.ResultNyuko);
            }
            else
            {
                dateTimePicker_入港.Value = Dousei.DouseiDate;
                textBox_入港.Text = "";
            }

            //if (Dousei.ResultChakusan != DateTime.MinValue)
            if (ClearClicked == false && Dousei.ResultChakusan != DateTime.MinValue)
            {
                dateTimePicker_着桟.Value = Dousei.ResultChakusan;
                textBox_着桟.Text = 時間Format(Dousei.ResultChakusan);
            }
            else
            {
                dateTimePicker_着桟.Value = Dousei.DouseiDate;
                textBox_着桟.Text = "";
            }
            //if (Dousei.ResultRisan != DateTime.MinValue)
            if (ClearClicked == false && Dousei.ResultRisan != DateTime.MinValue)
            {
                dateTimePicker_離桟.Value = Dousei.ResultRisan;
                textBox_離桟.Text = 時間Format(Dousei.ResultRisan);
            }
            else
            {
                dateTimePicker_離桟.Value = Dousei.DouseiDate;
                textBox_離桟.Text = "";
            }
            //if (Dousei.ResultShukou != DateTime.MinValue)
            if (ClearClicked == false && Dousei.ResultShukou != DateTime.MinValue)
            {
                dateTimePicker_出港.Value = Dousei.ResultShukou;
                textBox_出港.Text = 時間Format(Dousei.ResultShukou);
            }
            else
            {
                // 2014.11.24: 2014年11月度改造
                //dateTimePicker_出港.Value = Dousei.DouseiDate;
                if (Dousei.ResultRisan != DateTime.MinValue)
                {
                    // 離桟が入力されていて、
                    // 出港がMinValueの場合、明示的に入力なしで登録しているはずなので
                    // なにも表示しない
                    dateTimePicker_出港.Value = null;
                }
                else
                {
                    dateTimePicker_出港.Value = Dousei.DouseiDate;
                }
                textBox_出港.Text = "";
            }

            string dairitenId;
            //if (Dousei.ResultNyuko == DateTime.MinValue && (Dousei.ResultDairitenID == null || Dousei.ResultDairitenID.Length == 0))
            if (ClearClicked == true || (Dousei.ResultNyuko == DateTime.MinValue && (Dousei.ResultDairitenID == null || Dousei.ResultDairitenID.Length == 0)))
            {
                dairitenId = Dousei.DairitenID;
            }
            else
            {
                dairitenId = Dousei.ResultDairitenID;
            }
            foreach (MsCustomer customer in MsCustomer_list)
            {
                if (customer.MsCustomerID == dairitenId)
                {
                    singleLineCombo_代理店.Text = customer.CustomerName;
                    break;
                }
            }

            string ninushiId;
            //if (Dousei.ResultNyuko == DateTime.MinValue && (Dousei.ResultNinushiID == null || Dousei.ResultNinushiID.Length == 0))
            if (ClearClicked == true || (Dousei.ResultNyuko == DateTime.MinValue && (Dousei.ResultNinushiID == null || Dousei.ResultNinushiID.Length == 0)))
            {
                ninushiId = Dousei.NinushiID;
            }
            else
            {
                ninushiId = Dousei.ResultNinushiID;
            }
            foreach (MsCustomer customer in MsCustomer_list)
            {
                if (customer.MsCustomerID == ninushiId)
                {
                    singleLineCombo_荷主.Text = customer.CustomerName;
                    break;
                }
            }

            //if (Dousei.ResultNyuko == DateTime.MinValue && (Dousei.ResultBikou == null || Dousei.ResultBikou.Length == 0))
            if (ClearClicked == true || (Dousei.ResultNyuko == DateTime.MinValue && (Dousei.ResultBikou == null || Dousei.ResultBikou.Length == 0)))
            {
                textBox_備考.Text = Dousei.Bikou;
            }
            else
            {
                textBox_備考.Text = Dousei.ResultBikou;
            }

            int cnt = 0;
            List<DjDouseiCargo> cargos = null;
            //if (Dousei.ResultDjDouseiCargos.Count > 0)
            if (ClearClicked == false && Dousei.ResultDjDouseiCargos.Count > 0)
            {
                cargos = Dousei.ResultDjDouseiCargos;
            }
            else
            {
                cargos = new List<DjDouseiCargo>();
                foreach (DjDouseiCargo cargo in Dousei.DjDouseiCargos)
                {
                    DjDouseiCargo resCargo = new DjDouseiCargo();
                    resCargo.DjDouseiID = cargo.DjDouseiID;
                    resCargo.MsCargoID = cargo.MsCargoID;
                    resCargo.Qtty = cargo.Qtty;
                    resCargo.MsDjTaniID = cargo.MsDjTaniID;
                    resCargo.LineNo = cargo.LineNo;
                    resCargo.VesselID = cargo.VesselID;
                    
                    cargos.Add(resCargo);
                }
            }
            foreach (DjDouseiCargo douseiCargo in cargos)
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

            ClearClicked = false;
        }

        public void SetDay(DateTime eventDay)
        {
            dateTimePicker_入港.Value = eventDay;
            dateTimePicker_着桟.Value = eventDay;
            dateTimePicker_荷役開始.Value = eventDay;
            dateTimePicker_荷役終了.Value = eventDay;
            dateTimePicker_離桟.Value = eventDay;
            dateTimePicker_出港.Value = eventDay;
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
                if (Dousei.DjDouseiID == null || Dousei.DjDouseiID.Length == 0)
                {
                    return null;
                }
            }
            return _GetInstance();
        }
        public DjDousei GetInstanceNoCheck()
        {
            IsInput = false;
            return _GetInstance();
        }
        private DjDousei _GetInstance()
        {
            if (IsInput == false)
            {
                if (Dousei.DjDouseiID == null || Dousei.DjDouseiID == "")
                {
                    // 動静情報なし
                    return null;
                }

                if (Dousei.PlanNiyakuStart != DateTime.MinValue)
                {
                    // 予定が登録されている場合、実績情報のみクリアする
                    Dousei.DouseiDate = Dousei.PlanNiyakuStart;

                    Dousei.ResultNyuko = DateTime.MinValue;
                    Dousei.ResultChakusan = DateTime.MinValue;
                    Dousei.ResultNiyakuStart = DateTime.MinValue;
                    Dousei.ResultNiyakuEnd = DateTime.MinValue;
                    Dousei.ResultRisan = DateTime.MinValue;
                    Dousei.ResultShukou = DateTime.MinValue;

                    Dousei.ResultDairitenID = "";
                    Dousei.ResultNinushiID = "";
                    Dousei.ResultBikou = "";
                }
                else
                {
                    // 予定がない場合、予定情報を削除する
                    Dousei.DeleteFlag = NBaseCommon.Common.DeleteFlag_削除;
                }
                // 積荷情報は削除とする
                foreach (DjDouseiCargo cargo in Dousei.ResultDjDouseiCargos)
                {
                    cargo.DeleteFlag = NBaseCommon.Common.DeleteFlag_削除;
                }
            }
            else
            {

                //Dousei.ResultMsBashoID = (comboBox_港.SelectedItem as MsBasho).MsBashoId;
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
                    Dousei.ResultMsKichiID = (comboBox_基地.SelectedItem as MsKichi).MsKichiId;
                }
                else
                {
                    Dousei.ResultMsKichiID = "";
                }
                if (singleLineCombo_代理店.SelectedItem is MsCustomer)
                {
                    Dousei.ResultDairitenID = (singleLineCombo_代理店.SelectedItem as MsCustomer).MsCustomerID;
                }
                else
                {
                    Dousei.ResultDairitenID = "";
                }
                if (singleLineCombo_荷主.SelectedItem is MsCustomer)
                {
                    Dousei.ResultNinushiID = (singleLineCombo_荷主.SelectedItem as MsCustomer).MsCustomerID;
                }
                else
                {
                    Dousei.ResultNinushiID = "";
                }
                Dousei.ResultBikou = textBox_備考.Text;

                Dousei.ResultNyuko = 日時Format((DateTime)dateTimePicker_入港.Value, textBox_入港.Text);
                Dousei.ResultChakusan = 日時Format((DateTime)dateTimePicker_着桟.Value, textBox_着桟.Text);
                Dousei.ResultNiyakuStart = 日時Format((DateTime)dateTimePicker_荷役開始.Value, textBox_荷役開始.Text);
                Dousei.ResultNiyakuEnd = 日時Format((DateTime)dateTimePicker_荷役終了.Value, textBox_荷役終了.Text);
                Dousei.ResultRisan = 日時Format((DateTime)dateTimePicker_離桟.Value, textBox_離桟.Text);
                //Dousei.ResultShukou = 日時Format((DateTime)dateTimePicker_出港.Value, textBox_出港.Text);
                if (dateTimePicker_出港.Value != null)
                {
                    Dousei.ResultShukou = 日時Format((DateTime)dateTimePicker_出港.Value, textBox_出港.Text);
                }
                else
                {
                    Dousei.ResultShukou = DateTime.MinValue;
                }
                
                if (Dousei.ResultDjDouseiCargos == null)
                {
                    Dousei.ResultDjDouseiCargos = new List<DjDouseiCargo>();
                }
                else
                {
                    Dousei.ResultDjDouseiCargos.Clear();
                }
                for (int i = 0; i < flowLayoutPanel_Tumini.Controls.Count; i++)
                {
                    TuminiUserControl ctrl = flowLayoutPanel_Tumini.Controls[i] as TuminiUserControl;
                    DjDouseiCargo douseiCargo = ctrl.GetInstance();
                    douseiCargo.PlanResultFlag = (int)DjDouseiCargo.PLAN_RESULT_FLAG.RESULT;
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
                                Dousei.ResultDjDouseiCargos.Add(douseiCargo);
                                continue;
                            }
                        }
                    }
                    douseiCargo.LineNo = (i + 1).ToString();
                    Dousei.ResultDjDouseiCargos.Add(douseiCargo);
                }

                if (Dousei.MsBashoID == null || Dousei.MsBashoID == "")
                {
                    Dousei.MsBashoID = Dousei.ResultMsBashoID;
                    Dousei.MsKichiID = Dousei.ResultMsKichiID;
                }

                // 2013.03.23 : 実績日は着棧日であるとコメントを受けたので改造
                //Dousei.DouseiDate = Dousei.ResultNyuko; // 実績の日時は入港日時
                //Dousei.DouseiDate = Dousei.ResultChakusan;
                // 2013.05.14 : 実績日は荷役開始日であるとコメントを受けたので改造
                //Dousei.DouseiDate = Dousei.ResultNiyakuStart;
                // 2013.07.22 : 実績日は荷役終了日であるとコメントを受けたので改造
                Dousei.DouseiDate = Dousei.ResultNiyakuEnd;
            }
            return Dousei;
        }

        public bool Validation()
        {
            IsInput = false;

            ErrMsg = "";

            //if (comboBox_港.SelectedIndex == 0)
            //{
            //    ErrMsg = "港を選択して下さい";
            //}
            if (singleLineCombo_港.Text.Length == 0 || !(singleLineCombo_港.SelectedItem is MsBasho))
            {
                ErrMsg = "港を選択して下さい";
            }
            else
            {
                IsInput = true;
            }
            // 2011.10.04 必須項目から外す
            //if (comboBox_基地.SelectedIndex == 0)
            //{
            //    if (ErrMsg == "")
            //    {
            //        ErrMsg = "基地を選択して下さい";
            //    }
            //}
            //else
            //{
            //    IsInput = true;
            //}
            if (textBox_入港.Text.Length > 0)
            {
                if (_日時Validation("入港", dateTimePicker_入港, textBox_入港))
                {
                    IsInput = true;
                }
            }
            if (textBox_着桟.Text.Length > 0)
            {
                if (_日時Validation("着桟", dateTimePicker_着桟, textBox_着桟))
                {
                    IsInput = true;
                }
            }
            if (textBox_荷役開始.Text.Length > 0)
            {
                if (_日時Validation("荷役開始", dateTimePicker_荷役開始, textBox_荷役開始))
                {
                    IsInput = true;
                }
            }
            if (textBox_荷役終了.Text.Length > 0)
            {
                if (_日時Validation("荷役終了", dateTimePicker_荷役終了, textBox_荷役終了))
                {
                    IsInput = true;
                }
            }
            if (textBox_離桟.Text.Length > 0)
            {
                if (_日時Validation("離桟", dateTimePicker_離桟, textBox_離桟))
                {
                    IsInput = true;
                }
            }
            //if (textBox_出港.Text.Length > 0)
            if (dateTimePicker_出港.Value != null && textBox_出港.Text.Length > 0)
            {
                if (_日時Validation("出港", dateTimePicker_出港, textBox_出港))
                {
                    IsInput = true;
                }
            }

            if (IsInputTime())
            {
                DateTime d1 = 日時Format((DateTime)dateTimePicker_入港.Value, textBox_入港.Text);
                DateTime d2 = 日時Format((DateTime)dateTimePicker_着桟.Value, textBox_着桟.Text);
                DateTime d3 = 日時Format((DateTime)dateTimePicker_荷役開始.Value, textBox_荷役開始.Text);
                DateTime d4 = 日時Format((DateTime)dateTimePicker_荷役終了.Value, textBox_荷役終了.Text);
                DateTime d5 = 日時Format((DateTime)dateTimePicker_離桟.Value, textBox_離桟.Text);

                // 2014.11.25: 2014年11月度改造
                //DateTime d6 = 日時Format((DateTime)dateTimePicker_出港.Value, textBox_出港.Text);
                DateTime d6 = d5.AddDays(1); // 出港時間の入力がない場合にエラーと判定しないための処理
                if (dateTimePicker_出港.Value != null && textBox_出港.Text.Length > 0)
                {
                    d6 = 日時Format((DateTime)dateTimePicker_出港.Value, textBox_出港.Text);
                }

                if (!((d1 <= d2) && (d2 <= d3) && (d3 <= d4) && (d4 <= d5) && (d5 <= d6)))
                {
                    if (ErrMsg == "")
                        ErrMsg = "入港〜出港の日時を確認して下さい\n(日時の並びが正しくありません)";
                }
            }

            if (singleLineCombo_代理店.Text.Length > 0 && !(singleLineCombo_代理店.SelectedItem is MsCustomer))
            {
                if (ErrMsg == "")
                    ErrMsg = "代理店を選択して下さい\n(入力された代理店はマスタ登録されていません)";
            }
            if (singleLineCombo_荷主.Text.Length == 0)
            {
                if (ErrMsg == "")
                    ErrMsg = "荷主を選択して下さい";
            }
            if (singleLineCombo_荷主.Text.Length > 0 && !(singleLineCombo_荷主.SelectedItem is MsCustomer))
            {
                if (ErrMsg == "")
                    ErrMsg = "荷主を選択して下さい\n(入力された荷主はマスタ登録されていません)";
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
                    if (ErrMsg == "")
                    {
                        ErrMsg = "積荷" + (i + 1).ToString() + "を選択して下さい";
                    }
                }
                else
                {
                    IsInput = true;
                }
                if (douseiCargo.Qtty == decimal.MinValue)
                {
                    if (ErrMsg == "")
                    {
                        ErrMsg = "積荷" + (i + 1).ToString() + "の数量を正しく入力して下さい";
                    }
                }
                else
                {
                    if (NBaseCommon.Number.CheckValue((double)douseiCargo.Qtty, 4, 3) == false)
                    {
                        if (ErrMsg == "")
                        {
                            ErrMsg = "積荷" + (i + 1).ToString() + "の数量を正しく入力して下さい";
                        }
                    }
                    else
                    {
                        IsInput = true;
                    }
                }
                if (douseiCargo.MsDjTaniID == null)
                {
                    if (ErrMsg == "")
                    {
                        ErrMsg = "積荷" + (i + 1).ToString() + "の単位を選択して下さい";
                    }
                }
                else
                {
                    IsInput = true;
                }
            }


            if (IsInput == true && ErrMsg != "")
            {
                Error();
                return false;
            }
            return true;
        }

        public bool IsInputTime()
        {
            bool ret = false;

            if (textBox_入港.Text.Length > 0)
            {
                ret = true;
            }
            if (textBox_着桟.Text.Length > 0)
            {
                ret = true;
            }
            if (textBox_荷役開始.Text.Length > 0)
            {
                ret = true;
            }
            if (textBox_荷役終了.Text.Length > 0)
            {
                ret = true;
            }
            if (textBox_離桟.Text.Length > 0)
            {
                ret = true;
            }
            if (textBox_出港.Text.Length > 0)
            {
                ret = true;
            }
            return ret;
        }

        public bool IsValid()
        {
            bool ret = false;

            //if (comboBox_港.SelectedIndex > 0)
            //{
            //    ret = true;
            //}
            if (singleLineCombo_港.Text.Length > 0 && (singleLineCombo_港.SelectedItem is MsBasho))
            {
                ret = true;
            }
            // 2011.10.04 必須項目から外す
            //if (comboBox_基地.SelectedIndex > 0)
            //{
            //    ret = true;
            //}
            if (textBox_入港.Text.Length > 0)
            {
                ret = true;
            }
            if (textBox_着桟.Text.Length > 0)
            {
                ret = true;
            }
            if (textBox_荷役開始.Text.Length > 0)
            {
                ret = true;
            }
            if (textBox_荷役終了.Text.Length > 0)
            {
                ret = true;
            }
            if (textBox_離桟.Text.Length > 0)
            {
                ret = true;
            }
            if (textBox_出港.Text.Length > 0)
            {
                ret = true;
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

                if (douseiCargo.MsCargoID != int.MinValue)
                {
                    ret = true;
                    break;
                }
                if (douseiCargo.Qtty != decimal.MinValue)
                {
                    ret = true;
                    break;
                }
               if (douseiCargo.MsDjTaniID != null)
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        private bool _日時Validation(string label, NullableDateTimePicker dtp, TextBox tb)
        {
            if (dtp.Value == null)
            {
                if (ErrMsg == "")
                {
                    ErrMsg = label + "日付を選択して下さい";
                    return false;
                }
            }
            if (tb.Text == "")
            {
                if (ErrMsg == "")
                {
                    ErrMsg = label + "時間を入力して下さい";
                }
                return false;
            }
            else
            {
                if (_時間Validation(label, tb.Text) == false)
                {
                    return false;
                }
            }
            return true;
        }

        private bool _時間Validation(string label, string timeStr)
        {
            if (timeStr.Length != 4)
            {
                if (ErrMsg == "")
                {
                    //ErrMsg = label + "時間は4桁を入力して下さい";
                    ErrMsg = label + "時間は0000〜2359の範囲で入力してください";
                }
                return false;
            }
            if (Regex.IsMatch(timeStr, "\\d{4}") == false)
            {
                if (ErrMsg == "")
                {
                    //ErrMsg = label + "時間は4桁を入力して下さい";
                    ErrMsg = label + "時間は0000〜2359の範囲で入力してください";
                }
                return false;
            }
            if (TimeCheck(timeStr) == false)
            {
                if (ErrMsg == "")
                {
                    //ErrMsg = label + "時間が不正です";
                    ErrMsg = label + "時間は0000〜2359の範囲で入力してください";
                }
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
                return DateTime.MinValue;
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
            MessageBox.Show(Title + "の" + ErrMsg, "動静実績", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {

            //comboBox_港.SelectedIndex = 0;
            singleLineCombo_港.Text = "";
            comboBox_基地.SelectedIndex = 0;
            dateTimePicker_入港.Value = null;
            textBox_入港.Text = "";
            dateTimePicker_荷役開始.Value = null;
            textBox_荷役開始.Text = "";
            dateTimePicker_荷役終了.Value = null;
            textBox_荷役終了.Text = "";
            dateTimePicker_着桟.Value = null;
            textBox_着桟.Text = "";
            dateTimePicker_離桟.Value = null;
            textBox_離桟.Text = "";
            dateTimePicker_出港.Value = null;
            textBox_出港.Text = "";
            singleLineCombo_代理店.Text = "";
            singleLineCombo_荷主.Text = "";
            textBox_備考.Text = "";

            for (int i = flowLayoutPanel_Tumini.Controls.Count; i > 2; i--)
            {
                flowLayoutPanel_Tumini.Controls.RemoveAt(i - 1);
            }
            for (int i = 0; i < flowLayoutPanel_Tumini.Controls.Count; i++)
            {
                TuminiUserControl ctrl = flowLayoutPanel_Tumini.Controls[i] as TuminiUserControl;
                ctrl.Clear();
            }

            ClearClicked = true;
            SetDousei(Dousei);
        }


        public void ReadOnly荷主()
        {
            singleLineCombo_荷主.ReadOnly = false;

            if (singleLineCombo_荷主.Text != null && singleLineCombo_荷主.Text.Length > 0)
            {
                singleLineCombo_荷主.ReadOnly = true;
            }
        }
    }
}
