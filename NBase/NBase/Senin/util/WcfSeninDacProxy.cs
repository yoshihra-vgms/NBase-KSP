using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseData.DS;

namespace Senin.util
{
    public class WcfSeninDacProxy : ISeninDacProxy
    {
        #region ISeninDacProxy メンバ

        public List<MsSiShokumei> MsSiShokumei_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsSiShokumei_GetRecords(loginUser);
            }
        }
        public List<MsSiShokumeiShousai> MsSiShokumeiShousai_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsSiShokumeiShousai_GetRecords(loginUser);
            }
        }

        public List<MsSiMenjou> MsSiMenjou_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsSiMenjou_GetRecords(loginUser);
            }
        }

        public List<MsSiMenjouKind> MsSiMenjouKind_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsSiMenjouKind_GetRecords(loginUser);
            }
        }

        public List<MsSiShubetsu> MsSiShubetsu_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsSiShubetsu_GetRecords(loginUser);
            }
        }
        public List<MsSiShubetsuShousai> MsSiShubetsuShousai_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsSiShubetsuShousai_GetRecords(loginUser);
            }
        }

        public List<MsVessel> MsVessel_GetRecordsByHachuEnabled(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsVessel_GetRecordsByHachuEnabled(loginUser);
            }
        }

        public List<MsVessel> MsVessel_GetRecordsBySeninEnabled(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsVessel_GetRecordsBySeninEnabled(loginUser);
            }
        }

        public List<MsCustomer> MsCustomer_GetRecords_代理店(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsCustomer_GetRecords_代理店(loginUser);
            }
        }

        public List<MsSiHiyouKamoku> MsSiHiyouKamoku_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsSiHiyouKamoku_GetRecords(loginUser);
            }
        }

        public List<MsSiDaikoumoku> MsSiDaikoumoku_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsSiDaikoumoku_GetRecords(loginUser);
            }
        }

        public List<MsSiMeisai> MsSiMeisai_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsSiMeisai_GetRecords(loginUser);
            }
        }

        public List<MsSiMeisai> MsSiMeisai_GetRecords削除を含む(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsSiMeisai_GetRecords削除を含む(loginUser);
            }
        }

        public List<MsSiKamoku> MsSiKamoku_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsSiKamoku_GetRecords(loginUser);
            }
        }

        public List<MsSiKoushu> MsSiKoushu_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsSiKoushu_GetRecords(loginUser);
            }
        }


        // 2014.03: 2013年度改造
        public List<MsTax> MsTax_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsTax_GetRecords(loginUser);
            }
        }


        public List<MsCrewMatrixType> MsCrewMatrixType_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsCrewMatrixType_GetRecords(loginUser);
            }
        }

        public List<MsSiKyuyoTeate> MsSiKyuyoTeate_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsSiKyuyoTeate_GetRecords(loginUser);
            }
        }

        public List<MsSiKyuyoTeateSet> MsSiKyuyoTeateSet_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsSiKyuyoTeateSet_GetRecords(loginUser);
            }
        }

        public List<MsCargoGroup> MsCargoGroup_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsCargoGroup_GetRecords(loginUser);
            }
        }

        public List<MsSiAdvancedSearchCondition> MsSiAdvancedSearchCondition_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsSiAdvancedSearchCondition_GetRecords(loginUser);
            }
        }

        public List<MsSiPresentationItem> MsSiPresentationItem_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsSiPresentationItem_GetRecords(loginUser);
            }
        }
        public List<MsSeninCompany> MsSeninCompany_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsSeninCompany_GetRecords(loginUser);
            }
        }
        public List<MsSiOptions> MsSiOptions_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsSiOptions_GetRecords(loginUser);
            }
        }
        public List<MsBasho> MsBasho_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsBasho_GetRecords(loginUser);
            }
        }

        public List<MsSiAllowance> MsSiAllowance_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsSiAllowance_GetRecords(loginUser);
            }
        }

        #endregion
    }
}
