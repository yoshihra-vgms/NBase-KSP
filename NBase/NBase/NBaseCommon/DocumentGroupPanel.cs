using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NBaseCommon
{
    public partial class DocumentGroupPanel : UserControl
    {
        public DocumentGroupPanel()
        {
            InitializeComponent();

            Clear();
        }
        public void Clear()
        {
            label_Multi1.Visible = false;
            label_Multi2.Visible = false;
            label_Single.Visible = false;
        }
        public void Set項目名(string koumokuName)
        {
            label_Multi1.Visible = false;
            label_Multi2.Visible = false;
            label_Single.Visible = true;

            label_Single.Text = koumokuName;

            int x = (this.Width - label_Single.Width) / 2;
            label_Single.Location = new Point(x, label_Single.Location.Y);
        }
        public void Set項目名(string koumokuName1, string koumokuName2)
        {
            label_Multi1.Visible = true;
            label_Multi2.Visible = true;
            label_Single.Visible = false;

            label_Multi1.Text = koumokuName1;
            label_Multi2.Text = koumokuName2;

            int x1 = (this.Width - label_Multi1.Width) / 2;
            label_Multi1.Location = new Point(x1, label_Multi1.Location.Y);
            int x2 = (this.Width - label_Multi2.Width) / 2;
            label_Multi2.Location = new Point(x2, label_Multi2.Location.Y);
        }
    }
}
