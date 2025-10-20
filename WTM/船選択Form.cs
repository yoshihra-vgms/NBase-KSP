using NBaseData.BLC;
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
using WtmData;

namespace WTM
{
    public partial class 船選択Form : Form
    {
        public MsVessel Vessel { set; get; }
        public MsSenin Senin { set; get; }

        private DateTime TODAY = DateTime.MinValue;


        public 船選択Form()
        {
            InitializeComponent();
        }

        private void 船選択Form_Load(object sender, EventArgs e)
        {
            TODAY = DateTime.Today;

            Vessel = null;
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            comboBox1.Items.Add("選択してください");
            comboBox1.SelectedIndex = 0;

            foreach (var v in NBaseCommon.Common.VesselList)
            {
                comboBox1.Items.Add(v);
            }

            comboBox2.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is MsVessel)
            {
                Vessel = comboBox1.SelectedItem as MsVessel;

                comboBox2.Enabled = true;
                SetSenin();
            }
        }

        private void SetSenin()
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("選択してください");
            comboBox2.SelectedIndex = 0;

            //// 該当日が、当日を含む過去、または、乗船計画がない場合
            //// 乗船実績(SiCard）から
            var cards = Common.GetOnSigner(Vessel.MsVesselID, TODAY, TODAY);

            // 乗船職に置き換える
            foreach (MsSiShokumei shokumei in NBaseCommon.Common.ShokumeiList)
            {
                var targetCards = cards.Where(o => o.SiLinkShokumeiCards[0].MsSiShokumeiID == shokumei.MsSiShokumeiID);

                foreach (var c in targetCards)
                {
                    MsSenin s = new MsSenin();
                    s.MsSeninID = c.MsSeninID;
                    s.Sei = c.SeninName.Split(' ')[0];
                    s.Mei = c.SeninName.Split(' ')[1];
                    s.MsSiShokumeiID = shokumei.MsSiShokumeiID;


                    SeninDisp sd = new SeninDisp();
                    sd.Senin = s;
                    sd.ShokumeiAbbr = shokumei.NameAbbr;
                    comboBox2.Items.Add(sd);
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem is SeninDisp)
            {
                Senin = (comboBox2.SelectedItem as SeninDisp).Senin;
                System.Threading.Thread.Sleep(500);

                Close();
            }
        }


        private class SeninDisp
        {
            public MsSenin Senin;
            public string Disp;
            public string ShokumeiAbbr;

            public override string ToString()
            {

                Disp = Senin.Sei + Senin.Mei + "(" + ShokumeiAbbr + ")";
                return Disp;
            }
        }
    }
}
