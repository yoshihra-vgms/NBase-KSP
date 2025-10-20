using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SyncClient;

namespace NBaseHonsen
{
    public partial class SyncSplash : Form, IDataSyncObserver
    {
        public SyncSplash()
        {
            InitializeComponent();

            labelMessage2.Text = "";

            timer1.Start();
        }


        #region IDataSyncObserver メンバ

        public void SyncStart()
        {
        }

        public void SyncFinish()
        {
            try
            {
                Invoke(new MethodInvoker(
                   delegate()
                   {
                       DialogResult = DialogResult.OK;
                       Dispose();
                   }
               ));
            }
            catch (InvalidOperationException e)
            {
            }
        }


        public void Online()
        {
        }


        public void Offline()
        {
        }


        public void Message(string message)
        {
            try
            {
                Invoke(new MethodInvoker(
                    delegate()
                    {
                        labelMessage.Text = message;
                        labelMessage.Left = Width / 2 - labelMessage.Width / 2;
                    }
                ));
            }
            catch (InvalidOperationException e)
            {
            }
        }

        public void Message2(string message)
        {
            try
            {
                Invoke(new MethodInvoker(
                    delegate()
                    {
                        labelMessage2.Text = message;
                    }
                ));
            }
            catch (InvalidOperationException e)
            {
            }
        }

        public void Message3(string message)
        {
            try
            {
                Invoke(new MethodInvoker(
                    delegate()
                    {
                        if (textBoxMessage.Text.Length == 0)
                        {
                            textBoxMessage.Text += message;
                        }
                        else
                        {
                            textBoxMessage.Text += System.Environment.NewLine + message;
                        }

                        //カレット位置を末尾に移動
                        textBoxMessage.SelectionStart = textBoxMessage.Text.Length;
                        //テキストボックスにフォーカスを移動
                        textBoxMessage.Focus();
                        //カレット位置までスクロール
                        textBoxMessage.ScrollToCaret();
                    }
                ));
            }
            catch (InvalidOperationException e)
            {
            }
        }

        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (同期Client.OFFLINE)
            {
                timer1.Stop();

                DialogResult = DialogResult.OK;
                Dispose();
            }

            timer1.Stop();
        }
    }
}
