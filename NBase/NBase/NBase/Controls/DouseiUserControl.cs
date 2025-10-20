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
    public partial class DouseiUserControl : UserControl
    {
        public event KaniDouseiControl2.ClickEventHandler ClickEvent;
        public event KaniDouseiControl2.AfterEditDelegate AfterEdit;

        public PortUserControl puc1 = new PortUserControl();
        public PortUserControl puc2 = new PortUserControl();
        public PortUserControl puc3 = new PortUserControl();

        public bool IsJiseki = false;
        public List<MsKanidouseiInfoShubetu> MsKanidouseiInfoShubetu_List;

        public DouseiUserControl(DateTime eventDate,int MsVesselID,string VesselName)
        {
            InitializeComponent();
            puc1.Koma = 0;
            puc1.EventDate = eventDate;
            puc1.MsVesselID = MsVesselID;
            puc1.MsVesselName = VesselName;

            puc2.Koma = 1;
            puc2.EventDate = eventDate;
            puc2.MsVesselID = MsVesselID;
            puc2.MsVesselName = VesselName;

            puc3.Koma = 2;
            puc3.EventDate = eventDate;
            puc3.MsVesselID = MsVesselID;
            puc3.MsVesselName = VesselName;

            if (eventDate < DateTime.Today)
            {
                IsJiseki = true;
            }
        }

        public DouseiUserControl(PortalForm.PortName port1, PortalForm.PortName port2, PortalForm.PortName port3, bool isJiseki, List<MsKanidouseiInfoShubetu> list)
        {
            InitializeComponent();

            IsJiseki = isJiseki;
            MsKanidouseiInfoShubetu_List = list;

            puc1.PtKanidouseiInfo = port1.PtKanidouseiInfo;
            puc2.PtKanidouseiInfo = port2.PtKanidouseiInfo;
            puc3.PtKanidouseiInfo = port3.PtKanidouseiInfo;
        }

        private void Init()
        {
            puc1.AfterEdit += new KaniDouseiControl2.AfterEditDelegate(this.On_AfterEdit);
            puc2.AfterEdit += new KaniDouseiControl2.AfterEditDelegate(this.On_AfterEdit);
            puc3.AfterEdit += new KaniDouseiControl2.AfterEditDelegate(this.On_AfterEdit);

            puc1.ClickEvent += new KaniDouseiControl2.ClickEventHandler(this.On_Click);
            puc2.ClickEvent += new KaniDouseiControl2.ClickEventHandler(this.On_Click);
            puc3.ClickEvent += new KaniDouseiControl2.ClickEventHandler(this.On_Click);

            this.Invoke((MethodInvoker)delegate()
            {
                flowLayoutPanel1.Controls.Add(puc1);
                flowLayoutPanel1.Controls.Add(puc2);
                flowLayoutPanel1.Controls.Add(puc3);
            });

            if (IsJiseki == true)
            {
                BackColor = Color.LightGray;
            }
            else
            {
                BackColor = Color.White;
            }
        }

        private void On_Click(PtKanidouseiInfo PtKanidouseiInfo)
        {
            ClickEvent(PtKanidouseiInfo);
        }

        private void On_AfterEdit(DateTime EventDate, int MsVesselID,int Koma, PtKanidouseiInfo PtKanidouseiInfo)
        {
            AfterEdit(EventDate, MsVesselID,Koma, PtKanidouseiInfo);
        }

        private void DouseiUserControl_Load(object sender, EventArgs e)
        {
            Init();
        }
    }
}
