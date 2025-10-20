using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hachu.Models;

namespace Hachu.HachuManage
{
    public partial class BaseUserControl : UserControl
    {
        public enum WINDOW_STYLE { 通常, 承認, 概算 }
        public int WindowStyle;

        ////
        ////Form_Activateの代わり
        ////
        //public delegate void ControlVisibleChangedEventHandler(BaseUserControl senderControl);
        //public event ControlVisibleChangedEventHandler ControlVisibleChangedEvent;

        ////Form_Closingの代わり
        //public delegate void ControlParentChangedHandler(BaseUserControl senderControl);
        //public event ControlParentChangedHandler ControlParentChangedEvent;

        //Update
        public delegate void InfoUpdateEventHandler(BaseUserControl senderControl, ListInfoBase senderInfo);
        public event InfoUpdateEventHandler InfoUpdateEvent;

        public BaseUserControl()
        {
            InitializeComponent();
        }

        ////
        ////コントロールの表示が変更された時
        ////(詳細BaseForm_Activateの代わり)
        ////
        //private void 詳細BaseControl_VisibleChanged(object sender, EventArgs e)
        //{
        //    //表示された場合だけ
        //    if (Visible == true)
        //    {
        //        BaseControl_VisibleChanged();
        //    }
        //}
        //public void BaseControl_VisibleChanged()
        //{
        //    if (this.ControlVisibleChangedEvent != null)
        //        ControlVisibleChangedEvent(this);
        //}

        ////
        ////親の状態が変更になった時
        ////(詳細BaseForm_FormClosinの代わり)
        ////
        //private void 詳細BaseControl_ParentChanged(object sender, EventArgs e)
        //{
        //    if (this.Parent == null)
        //    {
        //        ControlParentChanged();
        //    }
        //}
        //public void ControlParentChanged()
        //{
        //    if (this.ControlParentChangedEvent != null)
        //        ControlParentChangedEvent(this);
        //}

        //Update
        public void InfoUpdating(ListInfoBase info)
        {
            if (this.InfoUpdateEvent != null)
                InfoUpdateEvent(this, info);
        }

        public bool 金額入力確認(decimal 比較元金額, string 比較元Str, TextBox textBox, string 比較先Str)
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
