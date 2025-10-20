using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.BLC;
using NBaseCommon;

namespace NBase
{
    public partial class Password : Form
    {
        public Button CloseButton
        {
            get
            {
                return button2;
            }
        }
        public Password()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 更新ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (textBox1.Text.Length == 0)
                {
                    MessageBox.Show("パスワードを入力して下さい", "パスワード", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (textBox1.Text != textBox2.Text)
                {
                    MessageBox.Show("新しいパスワードと新しいパスワード（確認用）が異なります", "パスワード", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ユーザパスワード.STATUS retStatus;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    retStatus = serviceClient.BLC_パスワード_パスワード変更(Common.LoginUser, Common.LoginUser, textBox1.Text);

                    if (retStatus == ユーザパスワード.STATUS.正常)
                    {
                        Common.LoginUser = serviceClient.BLC_ログイン処理_ログインチェック(Common.LoginUser.LoginID, textBox1.Text);
                    }
                }

                if (retStatus == ユーザパスワード.STATUS.履歴重複)
                {
                    MessageBox.Show("入力したパスワードは以前と同じパスワードです。\n異なるパスワードを入力して下さい", "パスワード", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // 指摘事項管理用
                NBaseCommon.LoginFile.Write(Common.LoginUser.LoginID, textBox1.Text);


                MessageBox.Show("パスワードを変更しました", "パスワード", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();

            }
            finally
            {
                Cursor = Cursors.Default;
            }

        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
