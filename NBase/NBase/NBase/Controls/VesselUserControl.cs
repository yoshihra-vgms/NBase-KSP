using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NBase.Controls
{
    public partial class VesselUserControl : UserControl
    {
        public delegate void ClickEventHandler(int vesselId);
        public event ClickEventHandler ClickEvent;


        public int VesselId { get; set; }
        public string 船名 { get; set; }
        public string 船長 { get; set; }
        public string 電話 { get; set; }
        public string 携帯 { get; set; }
        public string 営業 { get; set; }
        public string 工務 { get; set; }

        public VesselUserControl(int _VesselId, string _船名, string _船長, string _電話, string _携帯, string _営業, string _工務)
        {
            InitializeComponent();

            VesselId = _VesselId;
            船名 = _船名;
            船長 = _船長;
            電話 = _電話;
            携帯 = _携帯;
            営業 = _営業;
            工務 = _工務;

            船名_label.Text = 船名;
            船長_label.Text = 船長;
            電話_label.Text = 電話;
            携帯_label.Text = 携帯;

            //営、工、表示なしにする 2021/08/10 m.yoshihara
            //営業担当_label.Text = "営：" + 営業;
            //工務監督_label.Text = "工：" + 工務;
            営業担当_label.Text = "";
            工務監督_label.Text = "";

        }

        private void VesselUserControl_Click(object sender, EventArgs e)
        {
            ClickEvent(VesselId);
        }
    }
}
