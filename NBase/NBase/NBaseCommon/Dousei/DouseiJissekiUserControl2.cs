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
    public partial class DouseiJissekiUserControl2 : UserControl
    {
        public enum ModePlanResultEnum { 予定, 実績 };
        private ModePlanResultEnum ModePlanResult;

        public enum ModeTumiAgeEnum { 積み, 揚げ } ;
        private ModeTumiAgeEnum ModeTumiAge;


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


        public DouseiJissekiUserControl2()
        {
            InitializeComponent();
            SetMode(ModeTumiAgeEnum.積み);
        }
        public DouseiJissekiUserControl2(ModeTumiAgeEnum mode)
        {
            InitializeComponent();
            SetMode(mode);
        }

        public void SetMode(ModeTumiAgeEnum mode)
        {
            MsBasho_list = new List<MsBasho>();
            MsKichi_list = new List<MsKichi>();
            MsCargo_list = new List<MsCargo>();
            MsDjTani_list = new List<MsDjTani>();
            MsCustomer_list = new List<MsCustomer>();

            SetMode(ModePlanResultEnum.予定, mode, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
        }

        public void SetMode(ModePlanResultEnum modePlanResult, ModeTumiAgeEnum modeTumiAge, List<MsBasho> msBasho_list, List<MsKichi> msKichi_list, List<MsCargo> msCargo_list, List<MsDjTani> msDjTani_list, List<MsCustomer> msCustomer_list)
        {
            this.ModePlanResult = modePlanResult;
            this.ModeTumiAge = modeTumiAge;
            this.MsBasho_list = msBasho_list;
            this.MsKichi_list = msKichi_list;
            this.MsCargo_list = msCargo_list;
            this.MsDjTani_list = msDjTani_list;
            this.MsCustomer_list = msCustomer_list;

            Initialize();
        }

        private void Initialize()
        {
            if (ModeTumiAge == ModeTumiAgeEnum.積み)
            {
                Title = "【荷役：積み】";
                label_Header.Text = Title;
                panel_Base.BackColor = Common.ColorTumi;
            }
            else
            {
                Title = "【荷役：揚げ】";
                label_Header.Text = Title;
                panel_Base.BackColor = Common.ColorAge;
            }


            // 港
            if ((singleLineCombo_港.Items == null || singleLineCombo_港.Items.Count == 0) && MsBasho_list.Count > 0 )
            {
                singleLineCombo_港.Items.Clear();
                singleLineCombo_港.Items.Add("");
                foreach (MsBasho basho in MsBasho_list)
                {
                    singleLineCombo_港.Items.Add(basho);
                    singleLineCombo_港.AutoCompleteCustomSource.Add(basho.BashoName);
                }
            }
            singleLineCombo_港.Text = "";

            singleLineCombo_港.Width = 125;



            // 
            if (ModePlanResult == ModePlanResultEnum.予定)
            {
                label入港日時.Text = "　　入港日時";
                label着桟日時.Text = "　　着桟日時";
                label荷役開始日時.Text = "※ 荷役開始日時";
                label荷役終了日時.Text = "　　荷役終了日時";
                label離桟日時.Text = "　　離桟日時";
            }
            else
            {
                label入港日時.Text = "※ 入港日時";
                label着桟日時.Text = "※ 着桟日時";
                label荷役開始日時.Text = "※ 荷役開始日時";
                label荷役終了日時.Text = "※ 荷役終了日時";
                label離桟日時.Text = "※ 離桟日時";
            }


            // 基地
            if ((comboBox_基地.Items == null || comboBox_基地.Items.Count == 0) && MsKichi_list.Count > 0)
            {
                comboBox_基地.Items.Clear();
                comboBox_基地.Items.Add("");
                foreach (MsKichi kichi in MsKichi_list)
                {
                    comboBox_基地.Items.Add(kichi);
                    comboBox_基地.AutoCompleteCustomSource.Add(kichi.KichiName);
                }
                comboBox_基地.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBox_基地.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            if (comboBox_基地.Items.Count > 0)
                comboBox_基地.SelectedIndex = 0;

            // 代理店
            if ((singleLineCombo_代理店.Items == null || singleLineCombo_代理店.Items.Count == 0) && MsCustomer_list.Count > 0)
            {
                singleLineCombo_代理店.Items.Clear();
                singleLineCombo_代理店.Items.Add("");
                foreach (MsCustomer customer in MsCustomer_list)
                {
                    if (customer.Is代理店())
                    {
                        singleLineCombo_代理店.Items.Add(customer);
                        singleLineCombo_代理店.AutoCompleteCustomSource.Add(customer.CustomerName);
                    }
                }
            }
            singleLineCombo_代理店.Text = "";

            // 荷主
            if (ModePlanResult == ModePlanResultEnum.予定)
            {
                label荷主.Text = "　　荷主";
            }
            else
            {
                label荷主.Text = "※ 荷主";
            }
            if ((singleLineCombo_荷主.Items == null || singleLineCombo_荷主.Items.Count == 0) && MsCustomer_list.Count > 0)
            {
                singleLineCombo_荷主.Items.Clear();
                singleLineCombo_荷主.Items.Add("");
                foreach (MsCustomer customer in MsCustomer_list)
                {
                    if (customer.Is荷主())
                    {
                        singleLineCombo_荷主.Items.Add(customer);
                        singleLineCombo_荷主.AutoCompleteCustomSource.Add(customer.CustomerName);
                    }
                }
            }
            singleLineCombo_荷主.Text = "";

            // 初期状態で５個パネルを用意する
            if (flowLayoutPanel_Tumini.Controls == null || flowLayoutPanel_Tumini.Controls.Count == 0)
            {
                flowLayoutPanel_Tumini.Controls.Clear();
                TuminiPanelAdd();
                TuminiPanelAdd();
                TuminiPanelAdd();
                TuminiPanelAdd();
                TuminiPanelAdd();
            }
            else
            {
                foreach(TuminiUserControl2 ctrl in flowLayoutPanel_Tumini.Controls)
                {
                    ctrl.Initialize(MsCargo_list, MsDjTani_list);
                }

            }

            Dousei = new DjDousei();
        }






        public void SetDousei(DjDousei dousei)
        {
            Dousei = dousei;

            string bashoId;
            string kichiId;
            DateTime Nyuko;
            DateTime Chakusan;
            DateTime NiyakuStart;
            DateTime NiyakuEnd;
            DateTime Risan;
            DateTime Shukou;

            string dairitenId;
            string ninushiId;
            string bikou;

            List<DjDouseiCargo> cargos = null;

            if (ClearClicked == true || (Dousei.ResultNyuko == DateTime.MinValue && (Dousei.ResultMsBashoID == null || Dousei.ResultMsBashoID.Length == 0)))
            {
                bashoId = Dousei.MsBashoID;
            }
            else
            {
                bashoId = Dousei.ResultMsBashoID;
            }
            if (ClearClicked == true || (Dousei.ResultNyuko == DateTime.MinValue && (Dousei.ResultMsKichiID == null || Dousei.ResultMsKichiID.Length == 0)))
            {
                kichiId = Dousei.MsKichiID;
            }
            else
            {
                kichiId = Dousei.ResultMsKichiID;
            }


            if (ModePlanResult == ModePlanResultEnum.予定)
            {
                bashoId = Dousei.MsBashoID;
                kichiId = Dousei.MsKichiID;

                Nyuko = Dousei.PlanNyuko;
                Chakusan = Dousei.PlanChakusan;
                NiyakuStart = Dousei.PlanNiyakuStart;
                NiyakuEnd = Dousei.PlanNiyakuEnd;
                Risan = Dousei.PlanRisan;
                Shukou = Dousei.PlanShukou;

                dairitenId = Dousei.DairitenID;
                ninushiId = Dousei.NinushiID;

                bikou = Dousei.Bikou;

                cargos = Dousei.DjDouseiCargos;
            }
            else
            {
                if (Dousei.ResultNiyakuEnd == DateTime.MinValue)
                {
                    // 実績時、実績ﾃﾞｰﾀがない場合には、初期値として予定ﾃﾞｰﾀを表示する
                    bashoId = Dousei.MsBashoID;
                    kichiId = Dousei.MsKichiID;

                    Nyuko = Dousei.PlanNyuko;
                    Chakusan = Dousei.PlanChakusan;
                    NiyakuStart = Dousei.PlanNiyakuStart;
                    NiyakuEnd = Dousei.PlanNiyakuEnd;
                    Risan = Dousei.PlanRisan;
                    Shukou = Dousei.PlanShukou;

                    dairitenId = Dousei.DairitenID;
                    ninushiId = Dousei.NinushiID;

                    bikou = Dousei.Bikou;

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
                else
                {
                    bashoId = Dousei.ResultMsBashoID;
                    kichiId = Dousei.ResultMsKichiID;

                    Nyuko = Dousei.ResultNyuko;
                    Chakusan = Dousei.ResultChakusan;
                    NiyakuStart = Dousei.ResultNiyakuStart;
                    NiyakuEnd = Dousei.ResultNiyakuEnd;
                    Risan = Dousei.ResultRisan;
                    Shukou = Dousei.ResultShukou;

                    dairitenId = Dousei.ResultDairitenID;
                    ninushiId = Dousei.ResultNinushiID;

                    bikou = Dousei.ResultBikou;

                    cargos = Dousei.ResultDjDouseiCargos;
                }
            }








            var basho = MsBasho_list.Where(o => o.MsBashoId == bashoId).FirstOrDefault();
            if (basho != null)
            {
                singleLineCombo_港.Text = basho.ToString();
            }
            var kichi = MsKichi_list.Where(o => o.MsKichiId == kichiId).FirstOrDefault();
            if (kichi != null)
            {
                comboBox_基地.SelectedItem = kichi;
            }


            if (Nyuko == DateTime.MinValue)
                dateTimePicker_入港.Value = null;
            else
                dateTimePicker_入港.Value = Nyuko;

            textBox_入港.Text = 時間Format(Nyuko);


            if (Chakusan == DateTime.MinValue)
                dateTimePicker_着桟.Value = null;
            else
                dateTimePicker_着桟.Value = Chakusan;

            textBox_着桟.Text = 時間Format(Chakusan);


            if (NiyakuStart == DateTime.MinValue)
                dateTimePicker_荷役開始.Value = null;
            else
                dateTimePicker_荷役開始.Value = NiyakuStart;

            textBox_荷役開始.Text = 時間Format(NiyakuStart);


            if (NiyakuEnd == DateTime.MinValue)
                dateTimePicker_荷役終了.Value = null;
            else
                dateTimePicker_荷役終了.Value = NiyakuEnd;

            textBox_荷役終了.Text = 時間Format(NiyakuEnd);


            if (Risan == DateTime.MinValue)
                dateTimePicker_離桟.Value = null;
            else
                dateTimePicker_離桟.Value = Risan;

            textBox_離桟.Text = 時間Format(Risan);


            if (Shukou == DateTime.MinValue)
                dateTimePicker_出港.Value = null;
            else
                dateTimePicker_出港.Value = Shukou;

            textBox_出港.Text = 時間Format(Shukou);


            var dairiten = MsCustomer_list.Where(o => o.MsCustomerID == dairitenId).FirstOrDefault();
            if (dairiten != null)
            {
                singleLineCombo_代理店.Text = dairiten.CustomerName;
            }

            var ninushi = MsCustomer_list.Where(o => o.MsCustomerID == ninushiId).FirstOrDefault();
            if (ninushi != null)
            {
                singleLineCombo_荷主.Text = ninushi.CustomerName;
            }


            textBox_備考.Text = bikou;

            int cnt = 0;
            foreach (DjDouseiCargo douseiCargo in cargos)
            {
                TuminiUserControl2 ctrl = null;
                if (cnt < flowLayoutPanel_Tumini.Controls.Count)
                {
                    ctrl = flowLayoutPanel_Tumini.Controls[cnt] as TuminiUserControl2;
                }
                else
                {
                    TuminiPanelAdd();
                    ctrl = flowLayoutPanel_Tumini.Controls[cnt] as TuminiUserControl2;
                }
                cnt++;

                ctrl.SetInstance(douseiCargo);
            }

            ClearClicked = false;
        }



        public void ClearDousei()
        {
            Dousei = new DjDousei();
            Clear();
            for (int i = 0; i < flowLayoutPanel_Tumini.Controls.Count; i++)
            {
                TuminiUserControl2 ctrl = flowLayoutPanel_Tumini.Controls[i] as TuminiUserControl2;
                ctrl.ClearInstance();
            }
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
            TuminiUserControl2 ctrl = null;
            if (ModeTumiAge == ModeTumiAgeEnum.積み)
            {
                ctrl = new TuminiUserControl2(TuminiUserControl2.ModeEnum.積み, no + 1, MsCargo_list, MsDjTani_list);
            }
            else
            {
                ctrl = new TuminiUserControl2(TuminiUserControl2.ModeEnum.揚げ, no + 1, MsCargo_list, MsDjTani_list);
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

        public DjDousei GetInstanceNoValidation()
        {
            IsInput = true;
            return _GetInstance();
        }

        //public DjDousei GetInstanceNoCheck()
        //{
        //    IsInput = false;
        //    return _GetInstance();
        //}



        private DjDousei _GetInstance()
        {
            if (IsInput == false)
            {
                if (Dousei.DjDouseiID == null || Dousei.DjDouseiID == "")
                {
                    // 動静情報なし
                    return null;
                }

                if (ModePlanResult == ModePlanResultEnum.予定)
                {
                    if (Dousei.ResultNyuko != DateTime.MinValue)
                    {
                        // 実績が登録されている場合、予定情報のみクリアする
                        Dousei.DouseiDate = Dousei.ResultNiyakuEnd;

                        Dousei.MsBashoID = Dousei.ResultMsBashoID;
                        Dousei.MsKichiID = Dousei.ResultMsKichiID;

                        Dousei.PlanNyuko = DateTime.MinValue;
                        Dousei.PlanChakusan = DateTime.MinValue;
                        Dousei.PlanNiyakuStart = DateTime.MinValue;
                        Dousei.PlanNiyakuEnd = DateTime.MinValue;
                        Dousei.PlanRisan = DateTime.MinValue;
                        Dousei.PlanShukou = DateTime.MinValue;

                        Dousei.DairitenID = "";
                        Dousei.NinushiID = "";
                        Dousei.Bikou = "";
                    }
                    else
                    {
                        // 予定がない場合、予定情報を削除する
                        Dousei.DeleteFlag = NBaseCommon.Common.DeleteFlag_削除;
                    }
                }
                else
                {
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
                }
   
                // 積荷情報は削除とする
                foreach (DjDouseiCargo cargo in Dousei.ResultDjDouseiCargos)
                {
                    cargo.DeleteFlag = NBaseCommon.Common.DeleteFlag_削除;
                }
            }
            else
            {
                if (ModePlanResult == ModePlanResultEnum.予定)
                {
                    #region Plan
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


                    //if (_日時Validation("入港", dateTimePicker_入港, textBox_入港))
                    //{
                    //    IsInput = true;
                    //}

                    //Dousei.PlanNiyakuStart = 日時Format((DateTime)dateTimePicker_荷役開始.Value, textBox_荷役開始.Text);

                    if (dateTimePicker_入港.Value != null)
                        Dousei.PlanNyuko = 日時Format((DateTime)dateTimePicker_入港.Value, textBox_入港.Text);
                    else
                        Dousei.PlanNyuko = DateTime.MinValue;

                    if (dateTimePicker_着桟.Value != null)
                        Dousei.PlanChakusan = 日時Format((DateTime)dateTimePicker_着桟.Value, textBox_着桟.Text);
                    else
                        Dousei.PlanChakusan = DateTime.MinValue;

                    if (dateTimePicker_荷役開始.Value != null)
                        Dousei.PlanNiyakuStart = 日時Format((DateTime)dateTimePicker_荷役開始.Value, textBox_荷役開始.Text);
                    else
                        Dousei.PlanNiyakuStart = DateTime.MinValue;

                    if (dateTimePicker_荷役終了.Value != null)
                        Dousei.PlanNiyakuEnd = 日時Format((DateTime)dateTimePicker_荷役終了.Value, textBox_荷役終了.Text);
                    else
                        Dousei.PlanNiyakuEnd = DateTime.MinValue;

                    if (dateTimePicker_離桟.Value != null)
                        Dousei.PlanRisan = 日時Format((DateTime)dateTimePicker_離桟.Value, textBox_離桟.Text);
                    else
                        Dousei.PlanRisan = DateTime.MinValue;

                    if (dateTimePicker_出港.Value != null)
                        Dousei.PlanShukou = 日時Format((DateTime)dateTimePicker_出港.Value, textBox_出港.Text);
                    else
                        Dousei.PlanShukou = DateTime.MinValue;


                    if (Dousei.ResultNyuko == DateTime.MinValue)
                    {
                        Dousei.DouseiDate = Dousei.PlanNiyakuStart; // 予定の日時は、荷役開始日時
                    }


                    if (Dousei.DjDouseiCargos == null)
                    {
                        Dousei.DjDouseiCargos = new List<DjDouseiCargo>();
                    }
                    else
                    {
                        Dousei.DjDouseiCargos.Clear();
                    }

                    for (int i = 0; i < flowLayoutPanel_Tumini.Controls.Count; i++)
                    {
                        TuminiUserControl2 ctrl = flowLayoutPanel_Tumini.Controls[i] as TuminiUserControl2;
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
                    #endregion
                }
                else
                {
                    #region Result
                    if (singleLineCombo_港.SelectedItem is MsBasho)
                    {
                        //Dousei.MsBashoID = (singleLineCombo_港.SelectedItem as MsBasho).MsBashoId;
                        Dousei.ResultMsBashoID = (singleLineCombo_港.SelectedItem as MsBasho).MsBashoId;
                    }
                    else
                    {
                        //Dousei.MsBashoID = null;
                        Dousei.ResultMsBashoID = null;
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

                    if (dateTimePicker_入港.Value != null)
                    {
                        Dousei.ResultNyuko = 日時Format((DateTime)dateTimePicker_入港.Value, textBox_入港.Text);
                    }
                    else
                    {
                        Dousei.ResultNyuko = DateTime.MinValue;
                    }
                    if (dateTimePicker_着桟.Value != null)
                    {
                        Dousei.ResultChakusan = 日時Format((DateTime)dateTimePicker_着桟.Value, textBox_着桟.Text);
                    }
                    else
                    {
                        Dousei.ResultChakusan = DateTime.MinValue;
                    }
                    if (dateTimePicker_荷役開始.Value != null)
                    {
                        Dousei.ResultNiyakuStart = 日時Format((DateTime)dateTimePicker_荷役開始.Value, textBox_荷役開始.Text);
                    }
                    else
                    {
                        Dousei.ResultNiyakuStart = DateTime.MinValue;
                    }
                    if (dateTimePicker_荷役終了.Value != null)
                    {
                        Dousei.ResultNiyakuEnd = 日時Format((DateTime)dateTimePicker_荷役終了.Value, textBox_荷役終了.Text);
                    }
                    else
                    {
                        Dousei.ResultNiyakuEnd = DateTime.MinValue;
                    }
                    if (dateTimePicker_離桟.Value != null)
                    {
                        Dousei.ResultRisan = 日時Format((DateTime)dateTimePicker_離桟.Value, textBox_離桟.Text);
                    }
                    else
                    {
                        Dousei.ResultRisan = DateTime.MinValue;
                    }
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
                        TuminiUserControl2 ctrl = flowLayoutPanel_Tumini.Controls[i] as TuminiUserControl2;
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

                    if (Dousei.DouseiDate == DateTime.MinValue) // 実績入力がない場合、荷役開始をセットする
                        Dousei.DouseiDate = Dousei.PlanNiyakuStart;

                    #endregion
                }

            }
            return Dousei;
        }





        public bool Validation()
        {

            // 予定モードで、荷役開始日が入力されていない場合、無視する
            if (ModePlanResult == ModePlanResultEnum.予定)
            {
                if ((dateTimePicker_荷役開始.Value is DateTime) == false)
                {
                    return true;
                }
            }


            IsInput = false;

            ErrMsg = "";


            if (singleLineCombo_港.Text.Length == 0 || (singleLineCombo_港.SelectedItem is MsBasho) == false)
            {
                ErrMsg = "港を選択して下さい";
            }
            else
            {
                IsInput = true;
            }



            if (ModePlanResult == ModePlanResultEnum.予定)
            {
                if ((dateTimePicker_荷役開始.Value is DateTime) == false) 
                {
                    if (ErrMsg == "")
                        ErrMsg = "荷役開始日を選択して下さい";
                }
            }
            else
            {
                if ((dateTimePicker_入港.Value is DateTime) == false)
                {
                    if (ErrMsg == "")
                        ErrMsg = "入港日を選択して下さい";
                }
                else if ((dateTimePicker_着桟.Value is DateTime) == false)
                {
                    if (ErrMsg == "")
                        ErrMsg = "着桟日を選択して下さい";
                }
                else if((dateTimePicker_荷役開始.Value is DateTime) == false)
                {
                    if (ErrMsg == "")
                        ErrMsg = "荷役開始日を選択して下さい";
                }
                else if((dateTimePicker_荷役終了.Value is DateTime) == false)
                {
                    if (ErrMsg == "")
                        ErrMsg = "荷役終了日を選択して下さい";
                }
                else if ((dateTimePicker_離桟.Value is DateTime) == false)
                {
                    if (ErrMsg == "")
                        ErrMsg = "離桟日を選択して下さい";
                }
                else if (singleLineCombo_荷主.Text.Length == 0)
                {
                    if (ErrMsg == "")
                        ErrMsg = "荷主を選択して下さい";
                }
            }



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

            if (ModePlanResult == ModePlanResultEnum.実績)
            {
                if (ErrMsg == "")
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
            }



            if (ModePlanResult == ModePlanResultEnum.実績  && singleLineCombo_代理店.Text.Length > 0 && !(singleLineCombo_代理店.SelectedItem is MsCustomer))
            {
                if (ErrMsg == "")
                    ErrMsg = "代理店を選択して下さい\n(入力された代理店はマスタ登録されていません)";
            }
            if (singleLineCombo_荷主.Text.Length > 0 && !(singleLineCombo_荷主.SelectedItem is MsCustomer))
            {
                if (ErrMsg == "")
                    ErrMsg = "荷主を選択して下さい\n(入力された荷主はマスタ登録されていません)";
            }
            for (int i = 0; i < flowLayoutPanel_Tumini.Controls.Count; i++)
            {
                TuminiUserControl2 ctrl = flowLayoutPanel_Tumini.Controls[i] as TuminiUserControl2;
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


            //if (IsInput == true && ErrMsg != "")
            if (ErrMsg != "")
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



        //public bool IsValid()
        //{
        //    bool ret = false;

        //    if (singleLineCombo_港.Text.Length > 0 && (singleLineCombo_港.SelectedItem is MsBasho))
        //    {
        //        ret = true;
        //    }

        //    if (textBox_入港.Text.Length > 0)
        //    {
        //        ret = true;
        //    }
        //    if (textBox_着桟.Text.Length > 0)
        //    {
        //        ret = true;
        //    }
        //    if (textBox_荷役開始.Text.Length > 0)
        //    {
        //        ret = true;
        //    }
        //    if (textBox_荷役終了.Text.Length > 0)
        //    {
        //        ret = true;
        //    }
        //    if (textBox_離桟.Text.Length > 0)
        //    {
        //        ret = true;
        //    }
        //    if (textBox_出港.Text.Length > 0)
        //    {
        //        ret = true;
        //    }
        //    for (int i = 0; i < flowLayoutPanel_Tumini.Controls.Count; i++)
        //    {
        //        TuminiUserControl2 ctrl = flowLayoutPanel_Tumini.Controls[i] as TuminiUserControl2;
        //        DjDouseiCargo douseiCargo = ctrl.GetInstance();

        //        if (i > 0)
        //        {
        //            // 積荷２以降は、品目が選択されていないければ無視する
        //            if (douseiCargo.MsCargoID == int.MinValue)
        //            {
        //                continue;
        //            }
        //        }

        //        if (douseiCargo.MsCargoID != int.MinValue)
        //        {
        //            ret = true;
        //            break;
        //        }
        //        if (douseiCargo.Qtty != decimal.MinValue)
        //        {
        //            ret = true;
        //            break;
        //        }
        //        if (douseiCargo.MsDjTaniID != null)
        //        {
        //            ret = true;
        //            break;
        //        }
        //    }

        //    return ret;
        //}



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
                    ErrMsg = label + "時間は0000〜2359の範囲で入力してください";
                }
                return false;
            }
            if (Regex.IsMatch(timeStr, "\\d{4}") == false)
            {
                if (ErrMsg == "")
                {
                    ErrMsg = label + "時間は0000〜2359の範囲で入力してください";
                }
                return false;
            }
            if (TimeCheck(timeStr) == false)
            {
                if (ErrMsg == "")
                {
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
                //return DateTime.MinValue;
                time = "0000";
            }
            if (TimeCheck(time) == false)
            {
                return DateTime.MinValue;
            }
            else
            {
                string dateTimeStr = day.ToShortDateString() + " " + time.Substring(0, 2) + ":" + time.Substring(2, 2);
                return DateTime.Parse(dateTimeStr);
            }
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
            string title = ModePlanResult == ModePlanResultEnum.予定 ? "動静予定" : "動静実績";
            MessageBox.Show(Title + "の" + ErrMsg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void button_Clear_Click(object sender, EventArgs e)
        {
            Clear();
        }


        public void Clear()
        {
            singleLineCombo_港.Text = "";
            if (comboBox_基地.Items != null && comboBox_基地.Items.Count > 0)
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

            for (int i = 0; i < flowLayoutPanel_Tumini.Controls.Count; i++)
            {
                TuminiUserControl2 ctrl = flowLayoutPanel_Tumini.Controls[i] as TuminiUserControl2;
                ctrl.Clear();
            }
        }


        private void dateTimePicker_入港_ValueChanged(object sender, EventArgs e)
        {
            DateTime? date = null;

            if (dateTimePicker_入港.Value != null)
            {
                date = (DateTime)dateTimePicker_入港.Value;
            }

            dateTimePicker_着桟.Value = date;
            dateTimePicker_荷役開始.Value = date;
            dateTimePicker_荷役終了.Value = date;
            dateTimePicker_離桟.Value = date;
            dateTimePicker_出港.Value = date;
        }
    }
}
