using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using SyncClient;
using NBaseHonsen.Senin.util;
using LidorSystems.IntegralUI.Lists;
using NBaseData.DS;
using NBaseCommon.Senin.Excel;
using NBaseData.BLC;

namespace NBaseHonsen.Senin
{
    public partial class 船員リストForm : Form
    {
        private TreeListViewDelegate船員 treeListViewDelegate;


        public 船員リストForm()
        {
            InitializeComponent();
            Init();
        }

        
        private void Init()
        {
            treeListViewDelegate = new TreeListViewDelegate船員(treeListView1);
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
            filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船));
            filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船休暇));

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

                    MsSenin senin = MsSenin.GetRecord(NBaseCommon.Common.LoginUser, s.MsSeninID);

                    船員詳細Form form = new 船員詳細Form(senin, false);
                    form.ShowDialog();
                }
            }
        }


        private void button個人情報一覧_Click(object sender, EventArgs e)
        {
            string exeDir = System.IO.Directory.GetCurrentDirectory();

            saveFileDialog1.FileName = "個人情報一覧_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string templateFilePath = exeDir + "\\Template\\Template_個人情報一覧.xlsx";
                new 個人情報一覧出力(templateFilePath, saveFileDialog1.FileName).CreateFile(NBaseCommon.Common.LoginUser, SeninTableCache.instance(), DateTime.Now, 同期Client.LOGIN_VESSEL.MsVesselID);

                MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void buttonクルーリスト_Click(object sender, EventArgs e)
        {
            string exeDir = System.IO.Directory.GetCurrentDirectory();

            saveFileDialog2.FileName = "クルーリスト_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";

            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                string templateFilePath = exeDir + "\\Template\\Template_クルーリスト.xlsx";
                new クルーリスト出力(templateFilePath, saveFileDialog2.FileName).CreateFile(NBaseCommon.Common.LoginUser, SeninTableCache.instance(), DateTime.Now, 同期Client.LOGIN_VESSEL.MsVesselID);

                MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
