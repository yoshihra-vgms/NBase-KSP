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
    public partial class DouseYoteiReadOnlyUserControl : UserControl
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


        public DouseYoteiReadOnlyUserControl()
        {
            InitializeComponent();
            SetMode(ModeEnum.積み);
        }
        public DouseYoteiReadOnlyUserControl(ModeEnum mode)
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
                //panel_Base.BackColor = Color.LightBlue;
                panel_Base.BackColor = Color.FromArgb(204,231,247);
            }
            else
            {
                Title = "【荷役：揚げ】";
                label_Header.Text = Title;
                //panel_Base.BackColor = Color.Khaki;//LemonChiffon;
                panel_Base.BackColor = Color.FromArgb(166, 251, 174);
            }
            Dousei = new DjDousei();
        }

        public void SetDousei(DjDousei dousei)
        {
            Dousei = dousei;

            if (Dousei.PlanNiyakuStart == DateTime.MinValue)
                return;

            textBox_港.Text = "";
            foreach (MsBasho basho in MsBasho_list)
            {
                if (basho.MsBashoId == dousei.MsBashoID)
                {
                    textBox_港.Text = basho.BashoName;
                    break;
                }
            }
            textBox_基地.Text = "";
            foreach (MsKichi kichi in MsKichi_list)
            {
                if (kichi.MsKichiId == Dousei.MsKichiID)
                {
                    textBox_基地.Text = kichi.KichiName;
                    break;
                }
            }
            textBox_日付.Text = Dousei.PlanNiyakuStart.ToShortDateString();
            textBox_入港.Text = 時間Format(Dousei.PlanNyuko);
            textBox_着桟.Text = 時間Format(Dousei.PlanChakusan);
            textBox_荷役開始.Text = 時間Format(Dousei.PlanNiyakuStart);
            textBox_離桟.Text = 時間Format(Dousei.PlanRisan);
            textBox_出港.Text = 時間Format(Dousei.PlanShukou);
            
            textBox_代理店.Text = "";
            foreach (MsCustomer customer in MsCustomer_list)
            {
                if (customer.MsCustomerID == Dousei.DairitenID)
                {
                    textBox_代理店.Text = customer.CustomerName;
                    break;
                }
            }
            textBox_荷主.Text = "";
            foreach (MsCustomer customer in MsCustomer_list)
            {
                if (customer.MsCustomerID == Dousei.NinushiID)
                {
                    textBox_荷主.Text = customer.CustomerName;
                    break;
                }
            }
            textBox_備考.Text = Dousei.Bikou;

            int cnt = 0;
            flowLayoutPanel_Tumini.Controls.Clear();
            foreach (DjDouseiCargo douseiCargo in Dousei.DjDouseiCargos)
            {
                TuminiUserControl ctrl = null;
                if (Mode == ModeEnum.積み)
                {
                    ctrl = new TuminiUserControl(TuminiUserControl.ModeEnum.積み, cnt + 1, MsCargo_list, MsDjTani_list);
                }
                else
                {
                    ctrl = new TuminiUserControl(TuminiUserControl.ModeEnum.揚げ, cnt + 1, MsCargo_list, MsDjTani_list);
                }
                ctrl.SetInstance(douseiCargo);
                ctrl.ReadOnly();
                flowLayoutPanel_Tumini.Controls.Add(ctrl);
                cnt++;
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
    }
}
