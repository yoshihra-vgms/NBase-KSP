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
        private List<SiSimulationDetail> details = new List<SiSimulationDetail>();
        private TreeListViewDelegateCrewMatrix treeListViewDelegateCrewMatrix;


        private void SearchCrewMatrix()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                details = serviceClient.BLC_GetCrewMatrix(NBaseCommon.Common.LoginUser, senin.MsSeninID);
            }

            treeListViewDelegateCrewMatrix.SetRows(details);
        }

        internal void ClearCrewMatrix()
        {
            details.Clear();
            if (treeListViewDelegateCrewMatrix != null)
                treeListViewDelegateCrewMatrix.SetRows(details);
        }
    }
}
