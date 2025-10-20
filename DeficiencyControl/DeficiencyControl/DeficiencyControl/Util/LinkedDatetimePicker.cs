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
    public class LinkedDatetimePicker : DateTimePicker
    {
        public LinkedDatetimePicker()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// リンク先
        /// </summary>
        private DateTimePicker LinkedPicker = null;

        /// <summary>
        /// リンク先
        /// </summary>
        [Description("リンクするDatetimePicker")]
        public DateTimePicker LinkDatetimePicker
        {
            get
            {
                return this.LinkedPicker;
            }
            set
            {
                this.LinkedPicker = value;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LinkedDatetimePicker
            // 
            this.ShowCheckBox = true;
            this.ValueChanged += new System.EventHandler(this.LinkedDatetimePicker_ValueChanged);
            this.ResumeLayout(false);

        }

        private void LinkedDatetimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (this.LinkedPicker != null)
            {
                this.LinkedPicker.Checked = this.Checked;
            }
        }
        
    }
}
