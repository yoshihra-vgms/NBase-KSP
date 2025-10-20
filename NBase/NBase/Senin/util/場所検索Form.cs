using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.DS;

namespace Senin.util
{
    public partial class 場所検索Form : Form
    {
        private MsBasho basho;
        public MsBasho Selected
        {
            get
            {
                return basho;
            }
        }
        public 場所検索Form()
        {
            basho = null;
            InitializeComponent();
        }

        private void button検索_Click(object sender, EventArgs e)
        {
            string key = textBoxKey.Text;

            var results = from list in SeninTableCache.instance().GetMsBashoList(NBaseCommon.Common.LoginUser)
                          where list.BashoName.Contains(key)
                          orderby list.BashoName
                          select list;

            listBoxResults.Items.Clear();
            foreach (MsBasho o in results)
            {
                listBoxResults.Items.Add(o);
            }

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!(listBoxResults.SelectedItem is MsBasho))
            {
                MessageBox.Show("場所を選択してください");
                //MessageBox.Show("Please select the Place.");
                return;
            }
            basho = listBoxResults.SelectedItem as MsBasho;
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Dispose();
        }
    }
}
