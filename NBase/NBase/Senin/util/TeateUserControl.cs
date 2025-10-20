using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.DS;

namespace Senin.util
{
    public partial class TeateUserControl : UserControl
    {
        private 給与手当申請Form parentForm = null;
        private MsUser loginUser;
        private SeninTableCache seninTableCache;
        private SiKyuyoTeate kyuyoTeate;

        public TeateUserControl()
        {
            InitializeComponent();

            this.parentForm = null;
            this.loginUser = null;
            this.seninTableCache = null;
            this.kyuyoTeate = null;
        }

        public TeateUserControl(給与手当申請Form parentForm, MsUser loginUser, SeninTableCache seninTableCache, SiKyuyoTeate kyuyoTeate)
        {
            InitializeComponent();

            this.parentForm = parentForm;
            this.loginUser = loginUser;
            this.seninTableCache = seninTableCache;
            this.kyuyoTeate = kyuyoTeate;

            if (kyuyoTeate != null)
            {
                textBox1.Text = seninTableCache.GetMsSiKyuyoTeateName(loginUser, kyuyoTeate.MsSiKyuyoTeateID);

                string kikan = "";
                if (kyuyoTeate.StartDate != DateTime.MinValue)
                    kikan = kyuyoTeate.StartDate.ToShortDateString();

                if (kyuyoTeate.StartDate != DateTime.MinValue || kyuyoTeate.EndDate != DateTime.MinValue)
                    kikan += "～";

                if (kyuyoTeate.EndDate != DateTime.MinValue)
                    kikan += kyuyoTeate.EndDate.ToShortDateString();

                textBox2.Text = kikan;


                if (!(kyuyoTeate.StartDate == DateTime.MinValue && kyuyoTeate.EndDate == DateTime.MinValue && kyuyoTeate.Days == int.MinValue))
                {
                    textBox3.Text = kyuyoTeate.Days.ToString() + " 日間";
                }

                textBox4.Text = kyuyoTeate.Kingaku != int.MinValue ? kyuyoTeate.Kingaku.ToString("0") : "";
            }
            else
            {
                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
            }
        }

        private new void DoubleClick(object sender, EventArgs e)
        {
            MsVessel vessel = seninTableCache.GetMsVessel(loginUser, kyuyoTeate.MsVesselID);

            給与手当登録Form form = new 給与手当登録Form(parentForm, vessel, kyuyoTeate);

            form.ShowDialog();
        }
    }
}
