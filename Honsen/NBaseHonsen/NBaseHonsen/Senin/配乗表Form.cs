using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseHonsen.Senin.util;
using SyncClient;
using NBaseData.DS;
using NBaseCommon.Senin.Excel;

namespace NBaseHonsen.Senin
{
    public partial class 配乗表Form : Form
    {
        private TreeListViewDelegate配乗表 treeListViewDelegate;

        private SiHaijou haijou;


        public 配乗表Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitCheckedListBox船();

            treeListViewDelegate = new TreeListViewDelegate配乗表(treeListView1);

            Search();
        
            InitLabel前回配信日();
        }


        private void InitLabel前回配信日()
        {
            if (haijou == null)
            {
                label前回配信日.Text = "----/--/--";
            }
            else
            {
                label前回配信日.Text = haijou.HaishinDate.ToShortDateString();
            }
        }


        private void InitCheckedListBox船()
        {
            foreach (MsVessel v in SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser))
            {
                checkedListBox船.Items.Add(v);
                checkedListBox船.SetItemChecked(checkedListBox船.Items.Count - 1, true);
            }
        }


        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }


        private void Search()
        {
            List<SiHaijou> haijous = SiHaijou.GetRecords(同期Client.LOGIN_USER);

            if (haijous.Count > 0)
            {
                haijou = haijous[0];
                treeListViewDelegate.SetRows(haijou, checkedListBox船.CheckedItems);
            }
        }


        private void button配乗表_Click(object sender, EventArgs e)
        {
            string exeDir = System.IO.Directory.GetCurrentDirectory();

            saveFileDialog1.FileName = "配乗表_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string templateFilePath = exeDir + "\\Template\\Template_配乗表.xlsx";
                new 配乗表出力(templateFilePath, saveFileDialog1.FileName).CreateFile(NBaseCommon.Common.LoginUser, SeninTableCache.instance(), haijou);

                MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
