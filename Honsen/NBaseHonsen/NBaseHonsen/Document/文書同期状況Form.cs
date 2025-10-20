using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NBaseHonsen.Document
{
    public partial class 文書同期状況Form : Form
    {
        public 文書同期状況Form()
        {
            InitializeComponent();
        }

        private void 文書同期状況Form_Load(object sender, EventArgs e)
        {
            SyncClient.文書同期Client.同期最新状況();
            状況表示();
        }

        private void 状況表示()
        {
            int 報告書雛形_未同期 = SyncClient.文書同期Client.報告書雛形_未同期;
            int 報告書雛形_対象 = SyncClient.文書同期Client.報告書雛形_対象;
            double 報告書雛形_同期率 = 0;
            if (報告書雛形_対象 > 0)
            {
                報告書雛形_同期率 = (double)(報告書雛形_対象 - 報告書雛形_未同期) / (double)報告書雛形_対象 * 100;
            }

            int 管理記録_未送信 = SyncClient.文書同期Client.管理記録_未送信;
            int 管理記録_未同期 = SyncClient.文書同期Client.管理記録_未同期;
            int 管理記録_対象 = SyncClient.文書同期Client.管理記録_対象;
            double 管理記録_同期率 = 0;
            if (管理記録_対象 > 0)
            {
                管理記録_同期率 = (double)(管理記録_対象 - 管理記録_未同期) / (double)管理記録_対象 * 100;
            }

            int 公文書規則_未送信 = SyncClient.文書同期Client.公文書規則_未送信;
            int 公文書規則_未同期 = SyncClient.文書同期Client.公文書規則_未同期;
            int 公文書規則_対象 = SyncClient.文書同期Client.公文書規則_対象;
            double 公文書規則_同期率 = 0;
            if (公文書規則_対象 > 0)
            {
                公文書規則_同期率 = (double)(公文書規則_対象 - 公文書規則_未同期) / (double)公文書規則_対象 * 100;
            }

            textBox_雛形_未同期.Text = 報告書雛形_未同期.ToString();
            textBox_雛形_対象.Text = 報告書雛形_対象.ToString();
            textBox_雛形_同期率.Text = 報告書雛形_同期率.ToString("##0");

            textBox_管理記録_未送信.Text = 管理記録_未送信.ToString();
            textBox_管理記録_未同期.Text = 管理記録_未同期.ToString();
            textBox_管理記録_対象.Text = 管理記録_対象.ToString();
            textBox_管理記録_同期率.Text = 管理記録_同期率.ToString("##0");

            textBox_公文書規則_未送信.Text = 公文書規則_未送信.ToString();
            textBox_公文書規則_未同期.Text = 公文書規則_未同期.ToString();
            textBox_公文書規則_対象.Text = 公文書規則_対象.ToString();
            textBox_公文書規則_同期率.Text = 公文書規則_同期率.ToString("##0");

            textBox_Msg.Text = SyncClient.文書同期Client.SYNC_MESSAGE;
        }

        private void button_最新状況_Click(object sender, EventArgs e)
        {
            SyncClient.文書同期Client.同期最新状況();
            状況表示();
        }

        private void button_閉じる_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
