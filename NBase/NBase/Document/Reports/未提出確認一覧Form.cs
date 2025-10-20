using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Document.Reports
{
    public partial class 未提出確認一覧Form : Form
    {
        public 未提出確認一覧Form()
        {
            InitializeComponent();

            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "未提出確認一覧", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

            int year = NBaseUtil.DateTimeUtils.年度開始日().Year;
            for (int i = 0; i < 3; i++)
            {
                comboBox年.Items.Add(year - i);
            }
            comboBox年.SelectedIndex = 0;
        }

        private void button出力_Click(object sender, EventArgs e)
        {
            int businessYear = (int)comboBox年.SelectedItem;

            Reports.未提出確認一覧 reporter = new Document.Reports.未提出確認一覧();
            reporter.Output(businessYear);
        }
    }
}
