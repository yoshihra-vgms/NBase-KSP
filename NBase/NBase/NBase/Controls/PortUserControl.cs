using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;

namespace NBase.Controls
{
    public partial class PortUserControl : UserControl
    {
        public event KaniDouseiControl2.ClickEventHandler ClickEvent;
        public event KaniDouseiControl2.AfterEditDelegate AfterEdit;

        public DateTime EventDate;
        public int MsVesselID;
        public string MsVesselName;

        public PtKanidouseiInfo PtKanidouseiInfo = new PtKanidouseiInfo();
        public bool IsSetInfo = false;
        public int Koma = 0;

        public string 簡易動静種別
        {
            get { return PtKanidouseiInfo.KanidouseiInfoShubetuName; }
            set { PtKanidouseiInfo.KanidouseiInfoShubetuName = value; }
        }

        public string 場所
        {
            get 
            {
                //if (基地 != null && 基地 != "")
                //{
                //    return PtKanidouseiInfo.BashoName + ":" + 基地;
                //}
                return PtKanidouseiInfo.BashoName; 
            }
            set { PtKanidouseiInfo.BashoName = value; }
        }

        public string 基地
        {
            get { return PtKanidouseiInfo.KitiName; }
            set { PtKanidouseiInfo.KitiName = value; }
        }

        public string 備考
        {
            get { return PtKanidouseiInfo.Bikou; }
        }

        public PortUserControl()
        {
            InitializeComponent();
        }

        private void PortUserControl_Load(object sender, EventArgs e)
        {
            SetValue();
        }

        public void SetValue()
        {
            PtKanidouseiInfo.Koma = Koma;

            if (簡易動静種別 == "")
            {
                PortName_label.Visible = false;
                pictureBox1.Visible = false;
                return;
            }

            PortName_label.Text = 場所;
            if (簡易動静種別 == MsKanidouseiInfoShubetu.積み || 簡易動静種別 == MsKanidouseiInfoShubetu.揚げ)
            {
                if (PtKanidouseiInfo.MsCargoName != null && PtKanidouseiInfo.MsCargoName.Length > 0)
                {
                    PortName_label.Text += " " + PtKanidouseiInfo.MsCargoName;
                }
                if (PtKanidouseiInfo.Qtty > 0)
                {
                    PortName_label.Text += " " + PtKanidouseiInfo.Qtty.ToString(".000");
                }
            }

            PortName_label.Visible = true;

            //--------------------------------------
            //
            //--------------------------------------

            if (備考 != "")
            {
                PortName_label.Font = new Font(PortName_label.Font.FontFamily, PortName_label.Font.Size, FontStyle.Bold);
            }
            else
            {
                PortName_label.Font = new Font(PortName_label.Font.FontFamily, PortName_label.Font.Size);
            }

            if (簡易動静種別 == MsKanidouseiInfoShubetu.積み)
            {
                BackColor = Color.FromArgb(204,231,247);
                pictureBox1.Visible = false;
            }
            else if (簡易動静種別 == MsKanidouseiInfoShubetu.揚げ)
            {
                BackColor = Color.FromArgb(166, 251, 174);
                pictureBox1.Visible = false;
            }
            else if (簡易動静種別 == MsKanidouseiInfoShubetu.入渠)
            {
                BackColor = Color.FromArgb(249, 222, 226);
                pictureBox1.Visible = false;
            }
            else if (簡易動静種別 == MsKanidouseiInfoShubetu.待機)
            {
                BackColor = Color.White;
                pictureBox1.Visible = true;
            }
            else if (簡易動静種別 == MsKanidouseiInfoShubetu.揚積)
            {
                BackColor = Color.FromArgb(251, 251, 181);
                pictureBox1.Visible = false;
            }
            else if (簡易動静種別 == MsKanidouseiInfoShubetu.不明)
            {
                BackColor = Color.FromArgb(255, 255, 255);
                pictureBox1.Visible = false;
            }
            else if (簡易動静種別 == MsKanidouseiInfoShubetu.パージ)
            {
                BackColor = Color.BurlyWood;
                pictureBox1.Visible = false;
            }
            else if (簡易動静種別 == MsKanidouseiInfoShubetu.避泊)
            {
                BackColor = Color.Yellow;
                pictureBox1.Visible = false;
            }
            else
            {
                BackColor = Color.Transparent;
            }


            if (PtKanidouseiInfo.HonsenCheckDate != DateTime.MinValue)
            {
                pictureBox2.Visible = true;
            }
        }

        private void PortUserControl_DoubleClick(object sender, EventArgs e)
        {
            PtKanidouseiInfo.VesselName = MsVesselName;
            PtKanidouseiInfo.EventDate = EventDate;
            PtKanidouseiInfo.MsVesselID = MsVesselID;
            PtKanidouseiInfo.Koma = Koma;

            // PortalForm の userControl_簡易動静_ClickEvent が Call される
            // その後、Refresh する
            ClickEvent(PtKanidouseiInfo);
        }
    }
}
