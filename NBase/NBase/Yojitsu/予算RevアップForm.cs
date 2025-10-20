using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using Yojitsu.DA;
using NBaseUtil;

namespace Yojitsu
{
    public partial class 予算RevアップForm : Form
    {
        private BgYosanHead yosanHead;
        
        
        public 予算RevアップForm(BgYosanHead yosanHead)
        {
            this.yosanHead = yosanHead;
            
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("", "予算Revアップ", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            Init();
        }

        private void Init()
        {
            textBox年度.Text = yosanHead.Year.ToString();
            textBox予算種別.Text = BgYosanSbt.ToName(yosanHead.YosanSbtID);
            textBoxRevNo.Text = (yosanHead.Revision + 1).ToString();
            textBox更新日.Text = DateTime.Now.ToString("yyyy/MM/dd");
            textBox更新者.Text = NBaseCommon.Common.LoginUser.FullName;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            bool result = false;
            string message = yosanHead.Year + "年度 [" + BgYosanSbt.ToName(yosanHead.YosanSbtID) + "予算" +
                                " Rev." + yosanHead.Revision + "] ";

            if (ValidateFields() && MessageBox.Show(message + "\nの Rev をアップします。よろしいですか？",
                                "Wing - 予算 Rev アップ",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question) == DialogResult.OK)
            {
                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                    result = DbAccessorFactory.FACTORY.BLC_予算Revアップ(NBaseCommon.Common.LoginUser, CreateNewYosanHead());
                }, "予算を Rev アップしています...");

                progressDialog.ShowDialog();

                if (result)
                {
                    if (MessageBox.Show(message + "\nの Rev をアップしました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        DialogResult = DialogResult.OK;
                        Dispose();
                    }
                }
                else
                {
                    MessageBox.Show(message + "の Rev のアップに失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private bool ValidateFields()
        {
            if (textBox備考.Text.Length > 1000)
            {
                MessageBox.Show("備考は1000文字以内で入力してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        
        private BgYosanHead CreateNewYosanHead()
        {
            BgYosanHead h = new BgYosanHead();

            h.Year = yosanHead.Year;
            h.YosanSbtID = yosanHead.YosanSbtID;
            h.Revision = yosanHead.Revision + 1;
            //h.RevisionBikou = textBox備考.Text;
            h.RevisionBikou = StringUtils.Escape(textBox備考.Text);
            h.RenewDate = DateTime.Now;
            h.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            return h;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
