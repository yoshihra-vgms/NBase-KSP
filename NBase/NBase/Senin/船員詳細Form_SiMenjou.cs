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
        private List<SiMenjou> menjous = new List<SiMenjou>();
        private TreeListViewDelegate免状免許 treeListViewDelegate免状免許;


        private void Search免状免許()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                menjous = serviceClient.SiMenjou_GetRecordsByMsSeninID(NBaseCommon.Common.LoginUser, senin.MsSeninID);
            }

            treeListViewDelegate免状免許.SetRows(menjous);
        }

        //2021/07/29 m.yoshihara 
        internal void Refresh免状免許()
        {

            // まるっと検索するように変更　2021/07/29 m.yoshihara 
            //if (menjou.IsNew() && !menjous.Contains(menjou))
            //{
            //    //追加
            //    menjous.Add(menjou);
            //}
           
            //treeListViewDelegate免状免許.SetRows(menjous);

            Search免状免許();
        }
       
        internal void Clear免状免許()
        {
            menjous.Clear();
            if (treeListViewDelegate免状免許 != null)
                treeListViewDelegate免状免許.SetRows(menjous);
        }

        private void button免状免許追加_Click(object sender, EventArgs e)
        {
            //免状免許詳細Form form = new 免状免許詳細Form(this, textBox姓.Text + " " + textBox名.Text);
            免状免許詳細Form form = new 免状免許詳細Form(this, textBox姓.Text + " " + textBox名.Text);
            form.ShowDialog();
        }


        private void treeListView免状免許_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs)
            {
                MouseEventArgs me = (MouseEventArgs)e;

                if (me.Y <= 16)
                {
                    return;
                }

                TreeListViewNode selected = treeListView免状免許.GetNodeAt(me.X, me.Y);

                if (selected != null)
                {
                    SiMenjou m = selected.Tag as SiMenjou;

                    //免状免許詳細Form form = new 免状免許詳細Form(this, textBox姓.Text + " " + textBox名.Text, m, false);
                    免状免許詳細Form form = new 免状免許詳細Form(this, textBox姓.Text + " " + textBox名.Text, m, false);
                    form.ShowDialog();
                }
            }
        }
    }
}
