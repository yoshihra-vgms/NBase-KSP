using System;
using System.Windows.Forms;
using WtmData;
using WTM;


namespace WTM
{
    public partial class MenuForm : Form
    {

        public MenuForm()
        {
            if (WtmCommon.VesselMode)
            {
                this.Font = new System.Drawing.Font(this.Font.FontFamily.Name, Common.VesselFontSize);
            }
            InitializeComponent();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            if (NBaseCommon.Common.IsLocal)
            {
                label_Mode.Text = "起動モード：ローカルDB";
            }
            else
            {
                label_Mode.Text = "起動モード：サーバ接続";
            }

            label_Mode.Text += WtmCommon.VesselMode ? "、船起動" : "、事務所起動";


            if (WtmCommon.VesselMode)
            {
                button2.Visible = true;
                button5.Visible = false;
            }
            else
            {
                button2.Visible = false;
                button5.Visible = true;
            }

            label_Mode.Visible = false;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            出退勤登録Form frm = new 出退勤登録Form();
            frm.Vessel = WTM.Common.Vessel;
            frm.ShowDialog();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            個人表示Form frm = new 個人表示Form();
            frm.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            日表示Form frm = new 日表示Form();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            月表示Form frm = new 月表示Form();
            frm.ShowDialog();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OfficePortalForm f = new OfficePortalForm();
            f.ShowDialog();
        }
    }
}
