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
    public partial class 同期詳細情報Form : Form
    {
        public 同期詳細情報Form()
        {
            InitializeComponent();
        }

        private void button閉じる_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 同期詳細情報Form_Load(object sender, EventArgs e)
        {
            label_HostName.Text = 同期Client.GetHostName();
            label_管理番号_共通.Text = 同期Client.MAX_DATA_NO_OF_ZERO.ToString();
            label_管理番号_個別.Text = 同期Client.MAX_DATA_NO.ToString();
            if (同期Client.SYNC_DATE == DateTime.MinValue)
            {
                label_最新同期.Text = "----/--/-- --:--";
                textBox_Message.Text = "同期していません";
            }
            else
            {
                label_最新同期.Text = 同期Client.SYNC_DATE.ToString("yyyy/MM/dd HH:mm");
                textBox_Message.Text = 同期Client.SYNC_MESSAGE;
            }
        }
    }
}
