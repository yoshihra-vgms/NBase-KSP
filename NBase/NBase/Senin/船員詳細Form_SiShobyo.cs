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
        private List<SiShobyo> shobyos = new List<SiShobyo>();
        private TreeListViewDelegate傷病 treeListViewDelegate傷病;


        private void Search傷病()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                shobyos = serviceClient.SiShobyo_GetRecordsByMsSeninID(NBaseCommon.Common.LoginUser, senin.MsSeninID);
            }

            treeListViewDelegate傷病.SetRows(shobyos);
        }

        internal void Refresh傷病()
        {
            Search傷病();
        }


        internal void Clear傷病()
        {
            shobyos.Clear();
            if (treeListViewDelegate傷病 != null)
                treeListViewDelegate傷病.SetRows(shobyos);

        }

        private void button傷病追加_Click(object sender, EventArgs e)
        {
            傷病詳細Form form = new 傷病詳細Form(this);
            form.Set履歴(this.rirekis);
            form.ShowDialog();
        }

        private void treeListView傷病_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs)
            {
                MouseEventArgs me = (MouseEventArgs)e;

                if (me.Y <= 16)
                {
                    return;
                }

                TreeListViewNode selected = treeListView傷病.GetNodeAt(me.X, me.Y);

                if (selected != null)
                {
                    SiShobyo r = selected.Tag as SiShobyo;

                    傷病詳細Form form = new 傷病詳細Form(this, r, false);
                    form.Set履歴(this.rirekis);
                    form.ShowDialog();
                }
            }
        }
    }
}
