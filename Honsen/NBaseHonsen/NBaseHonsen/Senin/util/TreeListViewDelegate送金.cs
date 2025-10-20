using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseUtil;
using LidorSystems.IntegralUI.Lists;
using System.Windows.Forms;
using NBaseData.DAC;
using System.Drawing;
using ORMapping;
using NBaseData.DS;
using SyncClient;
using NBaseHonsen.util;

namespace NBaseHonsen.Senin.util
{
    internal class TreeListViewDelegate送金 : TreeListViewDelegate
    {
        private List<SiSoukin> soukins;


        internal TreeListViewDelegate送金(TreeListView treeListView)
            : base(treeListView)
        {
            SubItemFont = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);

            treeListView.SuspendUpdate();

            SetColumns(CreateColumns());

            treeListView.ResumeUpdate();

            treeListView.HoverNodeStyle.BackColor = Color.DarkTurquoise;
            treeListView.FocusedNodeStyle.BackColor = Color.Lime;
            treeListView.SelectedNodeStyle.BackColor = Color.Lime;
        }


        private List<Column> CreateColumns()
        {
            List<TreeListViewDelegate.Column> columns = new List<TreeListViewDelegate.Column>();

            TreeListViewDelegate.Column h = new TreeListViewDelegate.Column();
            h.headerContent = "送金日";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "送金者";
            h.width = 110;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "金額";
            h.width = 100;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "代理店";
            h.width = 150;
            columns.Add(h);

            return columns;
        }


        internal void SetRows(List<SiSoukin> soukins)
        {
            this.soukins = soukins;

            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            for (int i = 0; i < soukins.Count; i++)
            {
                SiSoukin s = soukins[i];

                TreeListViewNode node = CreateNode();

                AddSubItem(node, StringUtils.ToStr(s.SoukinDate), true);
                AddSubItem(node, s.SoukinUserName, true);
                AddSubItem(node, NBaseCommon.Common.金額出力(s.Kingaku), true);
                AddSubItem(node, s.CustomerName, true);

                node.Tag = s;

                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        }


        internal bool 送金受入()
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    if (treeListView.SelectedNode != null)
                    {
                        SiSoukin s = treeListView.SelectedNode.Tag as SiSoukin;

                        // 準備金
                        SiJunbikin junbikin = new SiJunbikin();

                        junbikin.SiJunbikinID = Guid.NewGuid().ToString();
                        junbikin.MsVesselID = junbikin.VesselID = s.MsVesselID;
                        junbikin.TourokuUserID = s.SoukinUserID;
                        junbikin.JunbikinDate = s.SoukinDate;
                        junbikin.KingakuIn = s.Kingaku;
                        junbikin.Bikou = s.Bikou;
                        junbikin.MsSiKamokuId = SeninTableCache.instance().MsSiKamoku_船用金補給ID(同期Client.LOGIN_USER);
                        junbikin.RenewDate = DateTime.Now;
                        junbikin.RenewUserID = 同期Client.LOGIN_USER.MsUserID;

                        SyncTableSaver.InsertOrUpdate(junbikin, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);
   
                        s.SiJunbikinID = junbikin.SiJunbikinID;
                        s.UkeireDate = s.RenewDate = DateTime.Now;
                        s.UkeireUserID = s.RenewUserID = 同期Client.LOGIN_USER.MsUserID;

                        SyncTableSaver.InsertOrUpdate(s, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);

                        if (s.AlarmInfoList.Count > 0)
                        {
                            PtAlarmInfo alarm = s.AlarmInfoList[0];

                            alarm.AlarmShowFlag = 1;
                            alarm.AlarmStopDate = alarm.RenewDate = DateTime.Now;
                            alarm.AlarmStopUser = alarm.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

                            SyncTableSaver.InsertOrUpdate(alarm, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);
                        }

                        // 本船更新情報
                        PtHonsenkoushinInfo honsenkoushinInfo = 本船更新情報.CreateInstance(s, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.送金受入);

                        SyncTableSaver.InsertOrUpdate(honsenkoushinInfo, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);
                    }

                    dbConnect.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    dbConnect.RollBack();
                    return false;
                }
            }
        }
    }
}
