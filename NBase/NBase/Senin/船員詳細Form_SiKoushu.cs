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
        private List<SiKoushu> koushus = new List<SiKoushu>();
        private TreeListViewDelegate講習 treeListViewDelegate講習;


        private void Search講習()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                if (senin.IsNew())
                {
                    koushus = new List<SiKoushu>();
                }
                else
                {
                    SiKoushuFilter filter = new SiKoushuFilter();
                    filter.MsSeninID = senin.MsSeninID;
                    // 2013年度改造
                    filter.Flag = 0; // 講習管理および船員詳細からの検索
                    koushus = serviceClient.SiKoushu_GetRecordsByFilter(NBaseCommon.Common.LoginUser, filter);
                }
            }

            treeListViewDelegate講習.SetRows(koushus);
        }


        internal void Refresh講習(SiKoushu koushu)
        {
            if (koushu.IsNew() && !koushus.Contains(koushu))
            {
                koushus.Add(koushu);
            }

            treeListViewDelegate講習.SetRows(koushus);
        }


        internal void Clear講習()
        {
            koushus.Clear();
            if (treeListViewDelegate講習 != null)
                treeListViewDelegate講習.SetRows(koushus);
        }
    }
}
