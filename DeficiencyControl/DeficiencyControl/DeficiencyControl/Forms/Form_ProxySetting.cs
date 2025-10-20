using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

using DeficiencyControl.Files;

namespace DeficiencyControl.Forms
{
    /// <summary>
    /// プロキシ設定画面
    /// </summary>
    public partial class Form_ProxySetting : Form
    {
        private ProxySetting settings = null;
        private bool isEdited = false;


        public Form_ProxySetting()
        {
            InitializeComponent();
        }

        private void Form_ProxySetting_Load(object sender, EventArgs e)
        {
            settings = ProxySetting.Read();

            if (CountChar(settings.ProxyURL, ':') == 2)
            {
                string[] splitStr = settings.ProxyURL.Split(':');
                textBox_Address.Text = splitStr[0] + ":" + splitStr[1];
                textBox_Port.Text = splitStr[2];
            }
            textBox_UserID.Text = settings.ProxyUserID;
            textBox_Password1.Text = settings.ProxyPassword;


            this.textBox_Address.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBox_Port.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBox_UserID.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBox_Password1.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBox_Password2.TextChanged += new System.EventHandler(this.textBox_TextChanged);
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                settings = new ProxySetting();

                if (textBox_Address.Text.Length > 0)
                    settings.ProxyURL = textBox_Address.Text;
                if (textBox_Port.Text.Length > 0)
                    settings.ProxyURL += ":" + textBox_Port.Text;

                if (textBox_UserID.Text.Length > 0)
                    settings.ProxyUserID = textBox_UserID.Text;
                if (textBox_Password1.Text.Length > 0)
                    settings.ProxyPassword = textBox_Password1.Text;


                if (textBox_Password1.Text != textBox_Password2.Text)
                {
                    //MessageBox.Show("パスワードとパスワード（確認）が異なります", "環境設定", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //DcMes.ShowMessage(this, EMessageID.MI_76);
                    return;
                }

                if (ProxySetting.Write(settings))
                {
                    //MessageBox.Show("設定を保存しました", "環境設定", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DcMes.ShowMessage(this, EMessageID.MI_1);
                }
                else
                {
                    //MessageBox.Show("設定の保存に失敗しました", "環境設定", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DcMes.ShowMessage(this, EMessageID.MI_2);
                }
                DialogResult = DialogResult.OK;
                Close();

            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            //編集中に閉じようとした。
            if (isEdited)
            {
                //DialogResult ret = MessageBox.Show(this, "データが編集されていますが、閉じますか？",
                //                            "環境設定",
                //                            MessageBoxButtons.OKCancel,
                //                            MessageBoxIcon.Question);
                DialogResult ret = DcMes.ShowMessage(this, EMessageID.MI_3, MessageBoxButtons.OKCancel);
                if (ret == DialogResult.Cancel)
                {
                    return;
                }
            }
            DialogResult = DialogResult.Cancel;
            Dispose();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            isEdited = true;
        }

        public static int CountChar(string s, char c)
        {
            if (s == null)
                return 0;
            else
                return s.Length - s.Replace(c.ToString(), "").Length;
        }
    }
}
