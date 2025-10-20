using NBaseData.DAC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Senin.util
{
    public partial class NightSettingDetailUserControl : UserControl
    {
        private List<MsVessel> vesselList = null;
        public List<MsVessel> VesselList
        {
            set 
            {
                comboBox船.Items.Clear();
                vesselList = new List<MsVessel>();

                comboBox船.Items.Add("");
                value.ForEach(obj => { comboBox船.Items.Add(obj); vesselList.Add(obj); });
            }
        }

        private SiNightSetting nightSetting = null;
        public SiNightSetting NightSetting
        {
            set { Set(value); }
            get { return Get(); }
        }



        public NightSettingDetailUserControl()
        {
            InitializeComponent();
        }

        private void Set(SiNightSetting setting)
        {
            nightSetting = setting;
            if (vesselList != null)
            {
                var v = vesselList.Where(obj => obj.MsVesselID == nightSetting.MsVesselID).FirstOrDefault();
                if (v != null)
                    comboBox船.SelectedItem = v;
            }

            textBox1.Text = nightSetting.StartTime.ToString("0000");
            textBox2.Text = nightSetting.EndTime.ToString("0000");
        }

        private SiNightSetting Get()
        {
            if (nightSetting == null)
            {
                nightSetting = new SiNightSetting();
            }

            if (comboBox船.SelectedItem is MsVessel)
            {
                nightSetting.MsVesselID = (comboBox船.SelectedItem as MsVessel).MsVesselID;
            }
            else
            {
                nightSetting.MsVesselID = -1;
            }
            int val = 0;
            int.TryParse(textBox1.Text, out val);
            nightSetting.StartTime = val;
            int.TryParse(textBox2.Text, out val);
            nightSetting.EndTime = val;

            return nightSetting;
        }

        /// <summary>
        /// 数字と[back space]のみ入力可にする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //0～9と、バックスペース以外の時は、イベントをキャンセルする
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txtb = sender as TextBox;
                txtb.Text = txtb.Text.PadRight(4, '0');
            }
        }
    }
}
