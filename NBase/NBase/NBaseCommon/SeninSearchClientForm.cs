using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.DS;

namespace NBaseCommon
{
    public partial class SeninSearchClientForm : Form
    {
        public virtual List<MsSenin> SearchMsSenin(MsSeninFilter filter) { return null; }
        public virtual bool SetMsSenin(MsSenin senin, bool check) { return true; }
    }
}
