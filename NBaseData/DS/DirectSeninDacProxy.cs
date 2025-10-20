using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace NBaseData.DS
{
    public class DirectSeninDacProxy : ISeninDacProxy
    {
        #region ISeninDacProxy メンバ

        public List<MsSiShokumei> MsSiShokumei_GetRecords(MsUser loginUser)
        {
            return MsSiShokumei.GetRecords(loginUser);
        }

        public List<MsSiShokumeiShousai> MsSiShokumeiShousai_GetRecords(MsUser loginUser)
        {
            return MsSiShokumeiShousai.GetRecords(loginUser);
        }

        public List<MsSiMenjou> MsSiMenjou_GetRecords(MsUser loginUser)
        {
            return MsSiMenjou.GetRecords(loginUser);
        }

        public List<MsSiMenjouKind> MsSiMenjouKind_GetRecords(MsUser loginUser)
        {
            return MsSiMenjouKind.GetRecords(loginUser);
        }

        public List<MsSiShubetsu> MsSiShubetsu_GetRecords(MsUser loginUser)
        {
            return MsSiShubetsu.GetRecords(loginUser);
        }
        public List<MsSiShubetsuShousai> MsSiShubetsuShousai_GetRecords(MsUser loginUser)
        {
            return MsSiShubetsuShousai.GetRecords(loginUser);
        }

        public List<MsVessel> MsVessel_GetRecordsByHachuEnabled(MsUser loginUser)
        {
            return MsVessel.GetRecordsByHachuEnabled(loginUser);
        }

        public List<MsVessel> MsVessel_GetRecordsBySeninEnabled(MsUser loginUser)
        {
            return MsVessel.GetRecordsBySeninEnabled(loginUser);
        }

        public List<MsCustomer> MsCustomer_GetRecords_代理店(MsUser loginUser)
        {
            return MsCustomer.GetRecords_代理店(loginUser);
        }

        public List<MsSiHiyouKamoku> MsSiHiyouKamoku_GetRecords(MsUser loginUser)
        {
            return MsSiHiyouKamoku.GetRecords(loginUser);
        }

        public List<MsSiDaikoumoku> MsSiDaikoumoku_GetRecords(MsUser loginUser)
        {
            return MsSiDaikoumoku.GetRecords(loginUser);
        }

        public List<MsSiMeisai> MsSiMeisai_GetRecords(MsUser loginUser)
        {
            return MsSiMeisai.GetRecords(loginUser);
        }

        public List<MsSiMeisai> MsSiMeisai_GetRecords削除を含む(MsUser loginUser)
        {
            return MsSiMeisai.GetRecords削除を含む(loginUser);
        }

        public List<MsSiKamoku> MsSiKamoku_GetRecords(MsUser loginUser)
        {
            return MsSiKamoku.GetRecords(loginUser);
        }

        public List<MsSiKoushu> MsSiKoushu_GetRecords(MsUser loginUser)
        {
            return MsSiKoushu.GetRecords(loginUser);
        }


        // 2014.03: 2013年度改造
        public List<MsTax> MsTax_GetRecords(MsUser loginUser)
        {
            return MsTax.GetRecords(loginUser);
        }



        public List<MsCrewMatrixType> MsCrewMatrixType_GetRecords(MsUser loginUser)
        {
            return MsCrewMatrixType.GetRecords(loginUser);
        }

        public List<MsSiKyuyoTeate> MsSiKyuyoTeate_GetRecords(MsUser loginUser)
        {
            return MsSiKyuyoTeate.GetRecords(loginUser);
        }

        public List<MsSiKyuyoTeateSet> MsSiKyuyoTeateSet_GetRecords(MsUser loginUser)
        {
            return MsSiKyuyoTeateSet.GetRecords(loginUser);
        }

        public List<MsCargoGroup> MsCargoGroup_GetRecords(MsUser loginUser)
        {
            return MsCargoGroup.GetRecords(loginUser);
        }   
     
        public List<MsSiAdvancedSearchCondition> MsSiAdvancedSearchCondition_GetRecords(MsUser loginUser)
        {
            return MsSiAdvancedSearchCondition.GetRecords(loginUser);
        }

        public List<MsSiPresentationItem> MsSiPresentationItem_GetRecords(MsUser loginUser)
        {
            return MsSiPresentationItem.GetRecords(loginUser);
        }

        public List<MsSeninCompany> MsSeninCompany_GetRecords(MsUser loginUser)
        {
            return MsSeninCompany.GetRecords(loginUser);
        }

        public List<MsSiOptions> MsSiOptions_GetRecords(MsUser loginUser)
        {
            return MsSiOptions.GetRecords(loginUser);
        }
        public List<MsBasho> MsBasho_GetRecords(MsUser loginUser)
        {
            return MsBasho.GetRecords(loginUser);
        }

        public List<MsSiAllowance> MsSiAllowance_GetRecords(MsUser loginUser)
        {
            return MsSiAllowance.GetRecords(loginUser);
        }

        #endregion
    }
}
