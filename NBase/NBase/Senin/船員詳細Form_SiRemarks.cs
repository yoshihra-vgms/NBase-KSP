using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DS;
using NBaseData.DAC;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using Senin.util;

namespace Senin
{
    partial class 船員詳細Panel
    {
        private List<SiRemarks> remarks = new List<SiRemarks>();
        private TreeListViewDelegate特記事項 treeListViewDelegate特記事項;


        private void Search特記事項()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                remarks = serviceClient.SiRemarks_GetRecordsByMsSeninID(NBaseCommon.Common.LoginUser, senin.MsSeninID);
            }

            treeListViewDelegate特記事項.SetRows(remarks);
        }

        internal void Refresh特記事項()
        {
            Search特記事項();
        }


        internal void Clear特記事項()
        {
            remarks.Clear();
            if (treeListViewDelegate特記事項 != null)
                treeListViewDelegate特記事項.SetRows(remarks);
        }


        private void button特記事項追加_Click(object sender, EventArgs e)
        {
            特記事項詳細Form form = new 特記事項詳細Form(this);
            form.ShowDialog();
        }

        private void treeListView特記事項_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs)
            {
                MouseEventArgs me = (MouseEventArgs)e;

                if (me.Y <= 16)
                {
                    return;
                }

                TreeListViewNode selected = treeListView特記事項.GetNodeAt(me.X, me.Y);

                if (selected != null)
                {
                    SiRemarks s = selected.Tag as SiRemarks;

                    特記事項詳細Form form = new 特記事項詳細Form(this, s, false);
                    form.ShowDialog();
                }
            }
        }
    }
}
