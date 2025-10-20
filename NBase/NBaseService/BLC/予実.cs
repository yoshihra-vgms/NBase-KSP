using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using NBaseData.DAC;
using ORMapping;
using System.ServiceModel;
using System.Collections.Generic;
using NBaseData.DS;
using NBaseData.BLC;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC_予算作成(MsUser loginUser, BgYosanHead yosanHead, List<BgKadouVessel> kadouVessels);
        
        [OperationContract]
        bool BLC_予算Revアップ(MsUser loginUser, BgYosanHead yosanHead);

        [OperationContract]
        bool BLC_予算保存(MsUser loginUser, List<BgYosanItem> yosanItems, BgNrkKanryou nrkKanryou);

        [OperationContract]
        bool BLC_販管費保存(MsUser loginUser, int year, BgYosanHead yosanHead, int eigyo, int kanri, int nenkan, int keiei,
                                         List<int> msVesselIds, List<decimal> amounts);

        [OperationContract]
        bool BLC_運航費保存(MsUser loginUser, int yosanHeadId, int msVesselId, int year, BgUnkouhi unkouhi, bool doCopy);

        [OperationContract]
        bool BLC_修繕費保存(MsUser loginUser, List<BgUchiwakeYosanItem> uchiwakeYosanItems, BgYosanBikou yosanBikou);

        [OperationContract]
        bool BLC_実績取込(MsUser loginUser);
    }

    public partial class Service
    {
        public bool BLC_予算作成(MsUser loginUser, BgYosanHead yosanHead, List<BgKadouVessel> kadouVessels)
        {
            return 予実.BLC_予算作成(loginUser, yosanHead, kadouVessels);
        }

        public bool BLC_予算Revアップ(MsUser loginUser, BgYosanHead yosanHead)
        {
            return 予実.BLC_予算Revアップ(loginUser, yosanHead);
        }

        public bool BLC_予算保存(MsUser loginUser, List<BgYosanItem> yosanItems, BgNrkKanryou nrkKanryou)
        {
            return 予実.BLC_予算保存(loginUser, yosanItems, nrkKanryou);
        }
        
        public bool BLC_販管費保存(MsUser loginUser, int year, BgYosanHead yosanHead, int eigyo, int kanri, int nenkan, int keiei,
                                         List<int> msVesselIds, List<decimal> amounts)
        {
            return 予実.BLC_販管費保存(loginUser, year, yosanHead, eigyo, kanri, nenkan, keiei, msVesselIds, amounts);
        }

        public bool BLC_運航費保存(MsUser loginUser, int yosanHeadId, int msVesselId, int year, BgUnkouhi unkouhi, bool doCopy)
        {
            return 予実.BLC_運航費保存(loginUser, yosanHeadId, msVesselId, year, unkouhi, doCopy);
        }

        public bool BLC_修繕費保存(MsUser loginUser, List<BgUchiwakeYosanItem> uchiwakeYosanItems, BgYosanBikou yosanBikou)
        {
            return 予実.BLC_修繕費保存(loginUser, uchiwakeYosanItems, yosanBikou);
        }

        public bool BLC_実績取込(MsUser loginUser)
        {
            return 予実.BLC_実績取込(loginUser);
        }
    }
}
