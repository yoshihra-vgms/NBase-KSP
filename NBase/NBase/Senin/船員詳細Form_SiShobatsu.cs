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
        private List<SiShobatsu> shobatsus = new List<SiShobatsu>();
        private TreeListViewDelegate賞罰 treeListViewDelegate賞罰;


        private void Search賞罰()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                shobatsus = serviceClient.SiShobatsu_GetRecordsByMsSeninID(NBaseCommon.Common.LoginUser, senin.MsSeninID);
            }

            treeListViewDelegate賞罰.SetRows(shobatsus);
        }

        internal void Refresh賞罰()
        {
            Search賞罰();
        }


        internal void Clear賞罰()
        {
            shobatsus.Clear();
            if (treeListViewDelegate賞罰 != null)
                treeListViewDelegate賞罰.SetRows(shobatsus);
        }


        private void button賞罰追加_Click(object sender, EventArgs e)
        {
            賞罰詳細Form form = new 賞罰詳細Form(this);
            form.ShowDialog();
        }

        private void treeListView賞罰_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs)
            {
                MouseEventArgs me = (MouseEventArgs)e;

                if (me.Y <= 16)
                {
                    return;
                }

                TreeListViewNode selected = treeListView賞罰.GetNodeAt(me.X, me.Y);

                if (selected != null)
                {
                    SiShobatsu s = selected.Tag as SiShobatsu;

                    賞罰詳細Form form = new 賞罰詳細Form(this, s, false);
                    form.ShowDialog();
                }
            }
        }
    }
}
