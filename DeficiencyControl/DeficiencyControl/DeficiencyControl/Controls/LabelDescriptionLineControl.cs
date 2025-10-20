using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeficiencyControl.Controls
{
    public partial class LabelDescriptionLineControl : LabelDescriptionControl
    {
        public LabelDescriptionLineControl()
        {
            InitializeComponent();

            this.flowLayoutPanelText.AutoSize = false;
            this.flowLayoutPanelText.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            this.flowLayoutPanelText.FlowDirection = FlowDirection.LeftToRight;

            
        }

        private void LabelDescriptionLineControl_Load(object sender, EventArgs e)
        {

        }
    }
}
