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

namespace Sim
{
    public partial class WorkContentPanel : UserControl
    {
        private WorkContent WC = null;

        public WorkContentPanel(WorkContent wc)
        {
            WC = wc;
            InitializeComponent();
            Set();
        }

        private void Set()
        {
            textBox1.Text = WC.DspName;
            textBox1.ForeColor = ColorTranslator.FromHtml(WC.FgColor);
            textBox1.BackColor = ColorTranslator.FromHtml(WC.BgColor);

            label1.Text = WC.Name;
        }
    }
}
