using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DeficiencyControl
{
    /// <summary>
    /// 待ちフォーム　WaitingStateを用いた使用が望ましい
    /// </summary>
    public partial class WaitForm : Form
    {
        public WaitForm()
        {
            InitializeComponent();                  
        }

        private void WaitForm_Load(object sender, EventArgs e)
        {
           
           
        }

        
    }

    /// <summary>
    /// 待ち状態表示  usingを使用すること これはFormクラス（最上位）での使用が望ましい。あまり奥で使用しないこと。　妥協してControlクラス程度。短すぎる時間の場合問題がおこるかもしれない
    /// </summary>
    public class WaitingState : IDisposable
    {
        /// <summary>
        /// 演出フォーム
        /// </summary>
        private WaitForm Form = null;

        /// <summary>
        /// スレッド
        /// </summary>
        Thread Th = null;

        
        /*
        public WaitingState()
        {
            this.Form = new WaitForm();            
            this.Form.Show();
        }*/

        /// <summary>
        /// 待ち
        /// </summary>
        /// <param name="pf">親</param>
        public WaitingState(Form pf)
        {
            //待ち演出をフォームの幅に合わせる。下に帯のような表示を検討
            this.Form = new WaitForm();
            this.Form.StartPosition = FormStartPosition.Manual;
            this.Form.ClientSize = pf.ClientSize;
            this.Form.TopMost = false;
            this.Form.Location = new Point(pf.Location.X + SystemInformation.FrameBorderSize.Width, pf.Location.Y + SystemInformation.FrameBorderSize.Height + SystemInformation.CaptionHeight);


            //スレッド開始
            this.Th = new Thread(this.FormThProc);
            this.Th.Start();
                
        }

        /// <summary>
        /// 削除
        /// </summary>
        public void Dispose()
        {
            try
            {
                //終了            
                this.Form.Invoke((MethodInvoker)delegate()
                {
                    this.Form.Close();
                });

                this.Th.Abort();
                this.Th.Join();
                this.Th = null;

            }
            catch
            {
            }

            
        }

        /// <summary>
        /// スレッド処理関数
        /// </summary>
        private void FormThProc()
        {
            try
            {
                Application.Run(this.Form);
            }
            catch (Exception e)
            {

            }
        }
    }
}
