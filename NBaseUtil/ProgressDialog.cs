using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NBaseUtil
{
    public partial class ProgressDialog : Form
    {
        private Action longWork;
        

        public ProgressDialog(Action longWork, string message)
        {
            this.longWork = longWork;
            
            InitializeComponent();

            label1.Text = message;
            label1.Left = Width / 2 - label1.Width / 2;
        }

        
        private void ProgressDialog_Load(object sender, EventArgs e)
        {
            //Application.UseWaitCursor = !Application.UseWaitCursor;
            //backgroundWorker1.RunWorkerAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            longWork();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                Invoke(new MethodInvoker(
                    delegate()
                    {
                        Application.UseWaitCursor = !Application.UseWaitCursor;
                        Dispose();
                    }
                ));
            }
            catch (InvalidOperationException)
            {
            }
        }

        private void ProgressDialog_Shown(object sender, EventArgs e)
        {
            this.Refresh();
            Application.UseWaitCursor = !Application.UseWaitCursor;
            backgroundWorker1.RunWorkerAsync();
        }
    }
}
