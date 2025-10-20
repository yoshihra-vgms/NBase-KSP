using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WtmModelBase;

namespace WTM
{
    public partial class DeviationPanel : UserControl
    {
        private string DspName;
        private Color FColor;
        private Color BColor;

        public DeviationPanel(string txt, Color fc, Color bc)
        {
            DspName = txt;
            FColor = fc;
            BColor = bc;
            InitializeComponent();
            
            Set();
        }

        private void Set()
        {
            //textBox1.Text = DspName;
            //textBox1.ForeColor = FColor;
            //textBox1.BackColor = BColor;

            textBox1.Text = DspName;
            textBox1.ForeColor = FColor;
            textBox1.BackColor = BColor;
            this.BackColor = BColor;

            //label1.Text = DspName;
        }
    }
}
