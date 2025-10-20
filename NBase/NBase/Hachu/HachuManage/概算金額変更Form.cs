using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;

namespace Hachu.HachuManage
{
    public partial class 概算金額変更Form : Form
    {
        private OdJry odJry = null;

        public 概算金額変更Form(OdJry odJry)
        {
            this.odJry = odJry;
            InitializeComponent();
        }

        private void 概算金額変更Form_Load(object sender, EventArgs e)
        {
            textBox概算金額.Text = odJry.GaisanAmount.ToString();
            textBox変更理由.Text = "";
        }

        private void buttonキャンセル_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button変更_Click(object sender, EventArgs e)
        {
            if(Validation())
            {
                decimal gaisan = decimal.Parse(textBox概算金額.Text);
                string bikou = textBox変更理由.Text;

                OdJryGaisan odJryGaisan = new OdJryGaisan();
                odJryGaisan.OdJryGaisanID = Hachu.Common.CommonDefine.新規ID(false);
                odJryGaisan.OdJryID = odJry.OdJryID;
                odJryGaisan.Amount = gaisan;
                odJryGaisan.Bikou = bikou;
                odJryGaisan.RenewDate = DateTime.Now;
                odJryGaisan.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    serviceClient.OdJryGaisan_Insert(NBaseCommon.Common.LoginUser, odJryGaisan);
                }
                odJry.GaisanAmount = odJryGaisan.Amount;

                DialogResult = DialogResult.OK;
                Close();
            }   
        }

        private bool Validation()
        {
            if (textBox概算金額.Text == null || textBox概算金額.Text.Length == 0)
            {
                MessageBox.Show("概算金額を入力してください", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                decimal gaisan = decimal.Parse(textBox概算金額.Text);
            }
            catch
            {
                MessageBox.Show("概算金額は数値を入力してください", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}
