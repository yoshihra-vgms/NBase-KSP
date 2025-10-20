using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hachu.Models;

namespace Hachu.HachuManage
{
    public partial class BaseForm : Form
    {
        public enum WINDOW_STYLE { 通常, 承認, 概算 }
        public int WindowStyle;

        public delegate void FormActiveEventHandler(BaseForm senderForm);
        public event FormActiveEventHandler FormActiveEvent;
        public delegate void FormClosingEventHandler(BaseForm senderForm);
        public event FormClosingEventHandler FormClosingEvent;
        public delegate void InfoUpdateEventHandler(BaseForm senderForm, ListInfoBase senderInfo);
        public event InfoUpdateEventHandler InfoUpdateEvent;

        public BaseForm()
        {
            InitializeComponent();
            this.Location = new Point(0, 0);
        }

        private void 詳細BaseForm_Activated(object sender, EventArgs e)
        {
            BaseFormActivated();
        }
        public void BaseFormActivated()
        {
            if (this.FormActiveEvent != null)
                FormActiveEvent(this);
        }
        private void 詳細BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            BaseFormClosing();
        }

        public void BaseFormClosing()
        {
            if (this.FormClosingEvent != null)
                FormClosingEvent(this);
        }

        public void BaseFormClose()
        {
            Close();
        }

        public void InfoUpdating(ListInfoBase info)
        {
            if (this.InfoUpdateEvent != null)
                InfoUpdateEvent(this, info);
        }


        protected bool 金額入力確認(decimal 比較元金額, string 比較元Str, TextBox textBox, string 比較先Str)
        {
            bool ret = true;
            decimal 比較先金額 = 0;
            try
            {
                比較先金額 = NBaseCommon.Common.金額表示を数値へ変換(textBox.Text);
            }
            catch
            {
            }
            if (比較先金額 > 比較元金額)
            {
                MessageBox.Show(比較先Str + "は、" + 比較元Str + "以下で、入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ret = false;
            }
            return ret;
        }
    }
}
