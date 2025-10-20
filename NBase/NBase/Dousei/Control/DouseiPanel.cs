using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dousei.Control
{
    public partial class DouseiPanel : UserControl
    {
        public DouseiHeadControl DouseiHeadControl1
        {
            get
            {
                return douseiHeadControl1;
            }
        }

        public DouseiHeadControl DouseiHeadControl2
        {
            get
            {
                return douseiHeadControl2;
            }
        }
        public DouseiDetailControl DouseiDetailControl1
        {
            get
            {
                return douseiDetailControl1;
            }
        }
        public DouseiDetailControl DouseiDetailControl2
        {
            get
            {
                return douseiDetailControl2;
            }
        }
        public DouseiDetailControl DouseiDetailControl3
        {
            get
            {
                return douseiDetailControl3;
            }
        }
        public DouseiDetailControl DouseiDetailControl4
        {
            get
            {
                return douseiDetailControl4;
            }
        }
        public DouseiDetailControl DouseiDetailControl5
        {
            get
            {
                return douseiDetailControl5;
            }
        }
        public DouseiPanel()
        {
            InitializeComponent();
        }
    }
}
