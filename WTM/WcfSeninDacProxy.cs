using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseData.DS;

namespace WTM
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
            return null;

        }

        public List<MsSiMenjou> MsSiMenjou_GetRecords(MsUser loginUser)
        {
            return null;

        }

        public List<MsSiMenjouKind> MsSiMenjouKind_GetRecords(MsUser loginUser)
        {
            return null;

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
            return null;

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
            return null;
        }

        public List<MsSiHiyouKamoku> MsSiHiyouKamoku_GetRecords(MsUser loginUser)
        {
            return null;

        }

        public List<MsSiDaikoumoku> MsSiDaikoumoku_GetRecords(MsUser loginUser)
        {
            return null;

        }

        public List<MsSiMeisai> MsSiMeisai_GetRecords(MsUser loginUser)
        {
            return null;

        }

        public List<MsSiMeisai> MsSiMeisai_GetRecords削除を含む(MsUser loginUser)
        {
            return null;

        }

        public List<MsSiKamoku> MsSiKamoku_GetRecords(MsUser loginUser)
        {
            return null;

        }

        public List<MsSiKoushu> MsSiKoushu_GetRecords(MsUser loginUser)
        {
            return null;

        }

        public List<MsTax> MsTax_GetRecords(MsUser loginUser)
        {
            return null;

        }

        public List<MsCrewMatrixType> MsCrewMatrixType_GetRecords(MsUser loginUser)
        {
            return null;

        }
        public List<MsSiKyuyoTeate> MsSiKyuyoTeate_GetRecords(MsUser loginUser)
        {
            return null;

        }

        public List<MsSiKyuyoTeateSet> MsSiKyuyoTeateSet_GetRecords(MsUser loginUser)
        {
            return null;

        }

        public List<MsCargoGroup> MsCargoGroup_GetRecords(MsUser loginUser)
        {
            return null;

        }

        public List<MsSiAdvancedSearchCondition> MsSiAdvancedSearchCondition_GetRecords(MsUser loginUser)
        {
            return null;

        }

        public List<MsSiPresentationItem> MsSiPresentationItem_GetRecords(MsUser loginUser)
        {
            return null;

        }
        public List<MsSeninCompany> MsSeninCompany_GetRecords(MsUser loginUser)
        {
            return null;

        }
        public List<MsSiOptions> MsSiOptions_GetRecords(MsUser loginUser)
        {
            return null;

        }

        public List<MsBasho> MsBasho_GetRecords(MsUser loginUser)
        {
            return null;

        }

        #endregion
    }
}
