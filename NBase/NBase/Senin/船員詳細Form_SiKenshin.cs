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
        private List<SiKenshin> kenshins = new List<SiKenshin>();
        private TreeListViewDelegate健康診断 treeListViewDelegate健康診断;


        private void Search健康診断()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                kenshins = serviceClient.SiKenshin_GetRecordsByMsSeninID(NBaseCommon.Common.LoginUser, senin.MsSeninID);
            }

            treeListViewDelegate健康診断.SetRows(kenshins);
        }


        internal void Refresh健康診断()
        {
            Search健康診断();
        }

        internal void Clear健康診断()
        {
            kenshins.Clear();
            if (treeListViewDelegate健康診断 != null)
                treeListViewDelegate健康診断.SetRows(kenshins);
        }


        private void button健康診断追加_Click(object sender, EventArgs e)
        {
            健康診断詳細Form form = new 健康診断詳細Form(this, senin);
            form.ShowDialog();
        }

        private void treeListView健康診断_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs)
            {
                MouseEventArgs me = (MouseEventArgs)e;

                if (me.Y <= 16)
                {
                    return;
                }

                TreeListViewNode selected = treeListView健康診断.GetNodeAt(me.X, me.Y);

                if (selected != null)
                {
                    SiKenshin k = selected.Tag as SiKenshin;

                    健康診断詳細Form form = new 健康診断詳細Form(this, senin, k, false);
                    form.ShowDialog();
                }
            }
        }
    }
}
