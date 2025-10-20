using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NBaseHonsen.Controls
{
    public partial class VesselUserControl : UserControl
    {
        public string 船名 { get; set; }
        public string 船長 { get; set; }
        public string 電話 { get; set; }
        public string 携帯 { get; set; }
        public string 営業 { get; set; }
        public string 工務 { get; set; }

        public VesselUserControl(string _船名, string _船長, string _電話, string _携帯, string _営業, string _工務)
        {
            InitializeComponent();

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

            //営業担当_label.Text = "営：" + 営業;
            //工務監督_label.Text = "工：" + 工務;
            営業担当_label.Text = "";
            工務監督_label.Text = "";
        }
    }
}
