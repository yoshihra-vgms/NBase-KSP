using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WingData.DAC;
using IGT_同期Client;
using HonsenWing.Senin.util;
using LidorSystems.IntegralUI.Lists;
using WingData.DS;
using WingCommon.Senin.Excel;
using WingData.BLC;

namespace HonsenWing.Senin
{
    public partial class 船員リスト2Form : Form
    {
        private TreeListViewDelegate船員2 treeListViewDelegate;


        public 船員リスト2Form()
        {
            InitializeComponent();
            Init();
        }

        
        private void Init()
        {
            treeListView1.CheckBoxes = true;
            treeListViewDelegate = new TreeListViewDelegate船員2(treeListView1);
            Search();
        }


        private void Search()
        {
            List<SiCard> result = 船員.BLC_船員カード検索(同期Client.LOGIN_USER, SeninTableCache.instance(), CreateSiCardFilter());
            treeListViewDelegate.SetRows(result);
        }


        private SiCardFilter CreateSiCardFilter()
        {
            SiCardFilter filter = new SiCardFilter();
            filter.MsVesselIDs.Add(同期Client.LOGIN_VESSEL.MsVesselID);
            filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_乗船ID(WingCommon.Common.LoginUser));
            filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_乗船休暇ID(WingCommon.Common.LoginUser));

            filter.Start = DateTime.Now;
            filter.End = DateTime.Now;

            filter.RetireFlag = 0;

            return filter;
        }

        
        private void treeListView1_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                TreeListViewNode selected = treeListView1.GetNodeAt(me.X, me.Y);

                if (selected != null)
                {
                    SiCard s = selected.Tag as SiCard;

                    MsSenin senin = MsSenin.GetRecord(WingCommon.Common.LoginUser, s.MsSeninID);

                    船員詳細Form form = new 船員詳細Form(senin, false);
                    form.ShowDialog();
                }
            }
        }


        private void button個人情報一覧_Click(object sender, EventArgs e)
        {
            string exeDir = System.IO.Directory.GetCurrentDirectory();

            saveFileDialog1.FileName = "個人情報一覧_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string templateFilePath = exeDir + "\\Template\\Template_個人情報一覧.xls";
                new 個人情報一覧出力(templateFilePath, saveFileDialog1.FileName).CreateFile(WingCommon.Common.LoginUser, SeninTableCache.instance(), DateTime.Now, 同期Client.LOGIN_VESSEL.MsVesselID);

                MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void buttonクルーリスト_Click(object sender, EventArgs e)
        {
            string exeDir = System.IO.Directory.GetCurrentDirectory();

            saveFileDialog2.FileName = "クルーリスト_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                string templateFilePath = exeDir + "\\Template\\Template_クルーリスト.xls";
                new クルーリスト出力(templateFilePath, saveFileDialog2.FileName).CreateFile(WingCommon.Common.LoginUser, SeninTableCache.instance(), DateTime.Now, 同期Client.LOGIN_VESSEL.MsVesselID);

                MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
