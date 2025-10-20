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
        private List<SiRireki> rirekis = new List<SiRireki>();
        private TreeListViewDelegate履歴 treeListViewDelegate履歴;


        private void Search履歴()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                rirekis = serviceClient.SiRireki_GetRecordsByMsSeninID(NBaseCommon.Common.LoginUser, senin.MsSeninID);
            }

            treeListViewDelegate履歴.SetRows(rirekis);
        }

        //引数なしにした　2021/07/29 m.yoshihara 
        //internal void Refresh履歴(SiRireki rireki)
        internal void Refresh履歴()
        {
            // まるっと検索するように変更　2021/07/29 m.yoshihara 
            //if (rireki.IsNew() && !rirekis.Contains(rireki))
            //{
            //    rirekis.Add(rireki);
            //}
            
            //treeListViewDelegate履歴.SetRows(rirekis);

            Search履歴();
        }

        internal void Clear履歴()
        {
            rirekis.Clear();
            treeListViewDelegate履歴.SetRows(rirekis);
        }


        private void button履歴追加_Click(object sender, EventArgs e)
        {
            履歴詳細Form form = new 履歴詳細Form(this);
            form.ShowDialog();
        }


        private void treeListView履歴_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs)
            {
                MouseEventArgs me = (MouseEventArgs)e;

                if (me.Y <= 16)
                {
                    return;
                }

                TreeListViewNode selected = treeListView履歴.GetNodeAt(me.X, me.Y);

                if (selected != null)
                {
                    SiRireki r = selected.Tag as SiRireki;

                    履歴詳細Form form = new 履歴詳細Form(this, r, false);
                    form.ShowDialog();
                }
            }
        }
    }
}
