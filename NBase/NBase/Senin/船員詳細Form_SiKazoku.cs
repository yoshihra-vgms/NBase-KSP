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
        private List<SiKazoku> kazokus = new List<SiKazoku>();
        private TreeListViewDelegate家族情報 treeListViewDelegate家族情報;


        private void Search家族情報()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                kazokus = serviceClient.SiKazoku_GetRecordsByMsSeninID(NBaseCommon.Common.LoginUser, senin.MsSeninID);
            }

            treeListViewDelegate家族情報.SetRows(kazokus);
        }

        internal void Refresh家族情報()
        {            
            Search家族情報();
        }

        internal void Clear家族情報()
        {
            kazokus.Clear();
            if (treeListViewDelegate家族情報 != null)
                treeListViewDelegate家族情報.SetRows(kazokus);
        }


        private void button家族情報追加_Click(object sender, EventArgs e)
        {
            家族情報Form form = new 家族情報Form(this);
            form.ShowDialog();
        }


        private void treeListView家族情報_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs)
            {
                MouseEventArgs me = (MouseEventArgs)e;

                if (me.Y <= 16)
                {
                    return;
                }

                TreeListViewNode selected = treeListView家族情報.GetNodeAt(me.X, me.Y);

                if (selected != null)
                {
                    SiKazoku k = selected.Tag as SiKazoku;

                    家族情報Form form = new 家族情報Form(this, k, false);
                    form.ShowDialog();
                }
            }
        }

        private void button表示順設定_Click(object sender, EventArgs e)
        {
            家族表示順設定Form form = new 家族表示順設定Form();
            form.SetList(senin.MsSeninID, kazokus);
            if (form.ShowDialog() == DialogResult.OK)
            {
                kazokus = form.GetList();
                treeListViewDelegate家族情報.SetRows(kazokus);
            }
        }
    }
}
