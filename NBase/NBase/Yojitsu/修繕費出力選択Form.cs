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
using Yojitsu.DA;

namespace Yojitsu
{
    public partial class 修繕費出力選択Form : Form
    {
        public enum 出力種別 { 各船, 全船 }
        public 出力種別 Kind { get; private set; }


        public 修繕費出力選択Form()
        {
            this.Kind = 出力種別.各船;

            InitializeComponent();
        }


        private void button出力_Click(object sender, EventArgs e)
        {
            if (radioButton各船.Checked)
            {
                this.Kind = 出力種別.各船;
            }
            else
            {
                this.Kind = 出力種別.全船;
            }
            
            DialogResult = DialogResult.OK;
            Dispose();
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Dispose();
        }
    }
}
