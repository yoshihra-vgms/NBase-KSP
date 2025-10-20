using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace NBaseData.DS
{
    public interface ISeninDacProxy
    {
        List<MsSiShokumei> MsSiShokumei_GetRecords(MsUser loginUser);
        List<MsSiShokumeiShousai> MsSiShokumeiShousai_GetRecords(MsUser loginUser);

        List<MsSiMenjou> MsSiMenjou_GetRecords(MsUser loginUser);

        List<MsSiMenjouKind> MsSiMenjouKind_GetRecords(MsUser loginUser);

        List<MsSiShubetsu> MsSiShubetsu_GetRecords(MsUser loginUser);
        List<MsSiShubetsuShousai> MsSiShubetsuShousai_GetRecords(MsUser loginUser);

        List<MsVessel> MsVessel_GetRecordsByHachuEnabled(MsUser loginUser);

        List<MsVessel> MsVessel_GetRecordsBySeninEnabled(MsUser loginUser);//m.yoshihara 2017/6/1

        List<MsCustomer> MsCustomer_GetRecords_代理店(MsUser loginUser);

        List<MsSiHiyouKamoku> MsSiHiyouKamoku_GetRecords(MsUser loginUser);

        List<MsSiDaikoumoku> MsSiDaikoumoku_GetRecords(MsUser loginUser);

        List<MsSiMeisai> MsSiMeisai_GetRecords(MsUser loginUser);

        List<MsSiMeisai> MsSiMeisai_GetRecords削除を含む(MsUser loginUser);

        List<MsSiKamoku> MsSiKamoku_GetRecords(MsUser loginUser);

        List<MsSiKoushu> MsSiKoushu_GetRecords(MsUser loginUser);

        // 2014.03: 2013年度改造
        List<MsTax> MsTax_GetRecords(MsUser loginUser);

        List<MsCrewMatrixType> MsCrewMatrixType_GetRecords(MsUser loginUser);
        List<MsSiKyuyoTeate> MsSiKyuyoTeate_GetRecords(MsUser loginUser);
        List<MsSiKyuyoTeateSet> MsSiKyuyoTeateSet_GetRecords(MsUser loginUser);
        List<MsCargoGroup> MsCargoGroup_GetRecords(MsUser loginUser);
        List<MsSiAdvancedSearchCondition> MsSiAdvancedSearchCondition_GetRecords(MsUser loginUser);
        List<MsSiPresentationItem> MsSiPresentationItem_GetRecords(MsUser loginUser);

        List<MsSeninCompany> MsSeninCompany_GetRecords(MsUser loginUser);

        List<MsSiOptions> MsSiOptions_GetRecords(MsUser loginUser);

        List<MsBasho> MsBasho_GetRecords(MsUser loginUser);

        List<MsSiAllowance> MsSiAllowance_GetRecords(MsUser loginUser);

    }
}
