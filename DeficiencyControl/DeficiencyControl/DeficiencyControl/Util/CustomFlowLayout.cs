using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeficiencyControl.Util
{
    [Designer("System.Windows.Forms.Design.PanelDesigner, System.Design")]
    public class CustomFlowLayout : FlowLayoutPanel
    {
        public CustomFlowLayout()
        {
            this.DoubleBuffered = true;
        }
    }



    [Designer("System.Windows.Forms.Design.PanelDesigner, System.Design")]
    public class CustomTableLayout : TableLayoutPanel
    {
        public CustomTableLayout()
        {
            this.DoubleBuffered = true;
        }
    }
}
