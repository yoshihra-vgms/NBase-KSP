using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WtmModelBase;

namespace WTM
{
    public partial class WorkContentLabel : UserControl
    {
        public WorkContent WC = null;
        private Font LabelFont; 

        // 親へイベントをデリゲートするためのイベントハンドラを定義
        public delegate void OnSelectedEventHandler(OnSelectedEventArgs e);
        public event OnSelectedEventHandler OnSelected;

        // デリゲートの引数を定義
        public class OnSelectedEventArgs : EventArgs
        {
            public WorkContent workcontent;
        }

        public WorkContentLabel(WorkContent wc)
        {
            WC = wc;
            InitializeComponent();
            Set();
            LabelFont = label1.Font;
        }

        private void Set()
        {
            label1.Text = WC.Name;
            label1.ForeColor = ColorTranslator.FromHtml(WC.FgColor);
            label1.BackColor = ColorTranslator.FromHtml(WC.BgColor);
            panel1.BackColor = this.BackColor;

            label1.Click += new EventHandler(Label_Click);
        }

        /// <summary>
        /// 選択、非選択のセット
        /// </summary>
        /// <param name="b"></param>
        public void SetSelectContent(bool b)
        {
            if (b)
            {
                panel1.BackColor = Color.DimGray;
                label1.Font = new Font(LabelFont.FontFamily,LabelFont.Size+1, FontStyle.Bold);
            }
            else
            {
                panel1.BackColor = this.BackColor;
                label1.Font = LabelFont;
            }
        }

        private void Label_Click(object sender, EventArgs e)
        {
            // イベントの引数を設定
            var arg = new OnSelectedEventArgs
            {
                workcontent = WC
            };
            // イベントの発火
            OnSelected(arg);
        }
    }
}
