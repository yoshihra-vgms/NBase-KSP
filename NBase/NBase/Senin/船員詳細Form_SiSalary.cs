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
        private List<SiSalary> salaries = new List<SiSalary>();
        private TreeListViewDelegate基本給 treeListViewDelegate基本給;

        private void button基本給_検索_Click(object sender, EventArgs e)
        {
            Search基本給();
        }

        private void Search基本給()
        {
            DateTime start = dateTimePicker基本給_期間開始.Value;
            DateTime end = dateTimePicker基本給_期間終了.Value;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                salaries = serviceClient.SiSalary_SearchRecords(NBaseCommon.Common.LoginUser, senin.MsSeninID, start, end, checkBox基本給区分_航機通砲手.Checked, checkBox基本給区分_下級海技士.Checked, checkBox基本給区分_部員.Checked);
            }

            treeListViewDelegate基本給.SetRows(salaries);
        }

        internal void Refresh基本給()
        {
            Search基本給();
        }


        internal void Clear基本給()
        {
            salaries.Clear();
            if (treeListViewDelegate基本給 != null)
                treeListViewDelegate基本給.SetRows(salaries);
        }


        private void button基本給追加_Click(object sender, EventArgs e)
        {
            基本給新規作成Form form = new 基本給新規作成Form(this);
            form.ShowDialog();
        }

        private void treeListView基本給_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs)
            {
                MouseEventArgs me = (MouseEventArgs)e;

                if (me.Y <= 16)
                {
                    return;
                }

                TreeListViewNode selected = treeListView基本給.GetNodeAt(me.X, me.Y);

                if (selected != null)
                {
                    SiSalary r = selected.Tag as SiSalary;


                    MsSiSalary mst = null;

                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        mst = serviceClient.BLC_基本給取得(NBaseCommon.Common.LoginUser, r.MsSiSalaryID);
                    }

                    基本給詳細Form form = new 基本給詳細Form(this, r, mst);
                    form.ShowDialog();
                }
            }
        }
    }
}
