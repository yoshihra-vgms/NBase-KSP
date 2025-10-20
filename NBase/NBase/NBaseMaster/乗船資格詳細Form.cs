using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DS;

namespace NBaseMaster
{
    public partial class 乗船資格詳細Form : Form
    {
        public NBaseData.DAC.MsVesselRankLicense vesselRankLicense;


        public 乗船資格詳細Form()
        {
            InitializeComponent();
        }

        private void 乗船資格詳細Form_Load(object sender, EventArgs e)
        {
            vesselRankLicense = null;
            InitComboBox免許免状();
        }

        private void InitComboBox免許免状()
        {
            foreach (NBaseData.DAC.MsSiMenjou m in SeninTableCache.instance().GetMsSiMenjouList(NBaseCommon.Common.LoginUser))
            {
                comboBox免許免状.Items.Add(m);
            }

            comboBox免許免状.SelectedIndex = 0;
        }

        private void comboBox免許免状_SelectedIndexChanged(object sender, EventArgs e)
        {
            NBaseData.DAC.MsSiMenjou menjou = comboBox免許免状.SelectedItem as NBaseData.DAC.MsSiMenjou;

            comboBox免許免状種別.Items.Clear();

            foreach (NBaseData.DAC.MsSiMenjouKind m in SeninTableCache.instance().GetMsSiMenjouKindList(NBaseCommon.Common.LoginUser, menjou.MsSiMenjouID))
            {
                comboBox免許免状種別.Items.Add(m);
            }
        }

        private void button更新_Click(object sender, EventArgs e)
        {
            vesselRankLicense = new NBaseData.DAC.MsVesselRankLicense();

            vesselRankLicense.MsSiMenjouID = (comboBox免許免状.SelectedItem as NBaseData.DAC.MsSiMenjou).MsSiMenjouID;
            if (comboBox免許免状種別.Items.Count > 1)
                vesselRankLicense.MsSiMenjouKindID = (comboBox免許免状種別.SelectedItem as NBaseData.DAC.MsSiMenjouKind).MsSiMenjouKindID;






            DialogResult = System.Windows.Forms.DialogResult.OK;
            Dispose();
        }

        private void button閉じる_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Dispose();
        }


    }
}
