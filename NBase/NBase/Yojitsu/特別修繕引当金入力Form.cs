using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseUtil;
using NBaseData.DS;

namespace Yojitsu
{
    public partial class 特別修繕引当金入力Form : Form
    {
        private NenjiForm nenjiForm;

        private BgYosanHead yosanHead;
        
        
        public 特別修繕引当金入力Form(NenjiForm nenjiForm, BgYosanHead yosanHead)
        {
            this.nenjiForm = nenjiForm;
            this.yosanHead = yosanHead;
            
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            int yearRange = NBaseData.BLC.予実.GetYearRange(yosanHead.YosanSbtID);
            for (int i = 0; i < yearRange; i++)
            {
                comboBox年.Items.Add(yosanHead.Year + i);
            }

            comboBox年.SelectedIndex = 0;

            for (int i = 0; i < DateTimeUtils.instance().MONTH.Length; i++)
            {
                comboBox月.Items.Add(DateTimeUtils.instance().MONTH[i]);
            }

            comboBox月.SelectedIndex = 0;
        }


        private void button入力_Click(object sender, EventArgs e)
        {
            decimal totalAmount;

            if (decimal.TryParse(textBox総額.Text, out totalAmount))
            {
                string year = comboBox年.Text;
                string month = Constants.PADDING_MONTHS[comboBox月.SelectedIndex];

                nenjiForm.Set特別修繕引当金(year, month, totalAmount);
                Dispose();
            }
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
