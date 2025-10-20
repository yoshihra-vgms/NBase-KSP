using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace NBaseUtil
{
    /// <summary>
    /// ウォーターマークを表示するテキストボックス
    /// </summary>
    public partial class WatermarkTextbox : TextBox
    {
        /// <summary>ウォーターマーク内部値</summary>
        private string _watermarkText = string.Empty;

        /// <summary>ウォーターマーク</summary>
        public string WatermarkText
        {
            get
            {
                return _watermarkText;
            }
            set
            {
                _watermarkText = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// ウォーターマーク設定ありの場合に描画します
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            const int WM_PAINT = 0x000F;
            const int WM_LBUTTONDOWN = 0x0201;
            base.WndProc(ref m);

            if ((m.Msg == WM_PAINT || m.Msg == WM_LBUTTONDOWN) &&
                this.Enabled &&
                string.IsNullOrEmpty(this.Text) &&
                !string.IsNullOrEmpty(WatermarkText)
                )
            {
                using (Graphics g = Graphics.FromHwnd(this.Handle))
                {
                    Rectangle rect = this.ClientRectangle;
                    rect.Offset(1, 1);
                    TextRenderer.DrawText(g,
                                          WatermarkText,
                                          this.Font,
                                          rect,
                                          Color.LightGray,
                                          TextFormatFlags.Top | TextFormatFlags.Left);

                }
            }
        }

    }
}
