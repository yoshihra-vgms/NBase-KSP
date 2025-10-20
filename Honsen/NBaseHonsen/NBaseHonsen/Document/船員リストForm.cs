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

namespace NBaseHonsen.Document
{
    public partial class 船員リストForm : Form
    {
        private TreeListViewDelegate船員2 treeListViewDelegate;
        public List<string> CheckedUserIds = null;

        public 船員リストForm()
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
            filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船));
            filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船休暇));

            filter.Start = DateTime.Now;
            filter.End = DateTime.Now;

            filter.RetireFlag = 0;

            return filter;
        }

        private void buttonすべて選択_Click(object sender, EventArgs e)
        {
            treeListViewDelegate.AllCheck();
        }

        private void button確認_Click(object sender, EventArgs e)
        {
            //CheckedUserIds = treeListViewDelegate.GetCheckedUserIds();

            CheckedUserIds = new List<string>();

            List<SiCard> checkedUserCards = treeListViewDelegate.GetCheckedUserCards();
            List<SiCard> notRegistUsers = new List<SiCard>();
            foreach (SiCard c in checkedUserCards)
            {
                if (c.MsUserID == null || c.MsUserID.Length == 0)
                {
                    notRegistUsers.Add(c);
                }
                else
                {
                    CheckedUserIds.Add(c.MsUserID);
                }
            }

            if (notRegistUsers.Count() > 0)
            {
                this.Hide();

                船員エラーForm f = new 船員エラーForm(notRegistUsers);
                f.ShowDialog();
            }

            Close();
        }

        private void 船員リストForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //CheckedUserIds = treeListViewDelegate.GetCheckedUserIds();
        }
    }
}
