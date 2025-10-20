using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;
using NBaseData.DAC;
using NBaseData.BLC;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC_公文書規則処理_登録(MsUser loginUser, DmKoubunshoKisoku koubunshoKisoku, DmKoubunshoKisokuFile koubunshoKisokuFile, List<DmPublisher> publishers, List<DmKoukaiSaki> koukaiSakis);

        [OperationContract]
        bool BLC_公文書規則処理_公開先更新(MsUser loginUser, string koubunshoKisokuId, List<DmKoukaiSaki> koukaiSakis);
        
        //[OperationContract]
        //bool BLC_公文書規則処理_更新(MsUser loginUser, DmKoubunshoKisoku koubunshoKisoku, DmKoubunshoKisokuFile koubunshoKisokuFile, List<DmPublisher> publishers, List<DmKoukaiSaki> koukaiSakis);
        
        //[OperationContract]
        //bool BLC_公文書規則処理_削除(MsUser loginUser, DmKoubunshoKisoku koubunshoKisoku);
    }

    public partial class Service
    {
        public bool BLC_公文書規則処理_登録(MsUser loginUser, DmKoubunshoKisoku koubunshoKisoku, DmKoubunshoKisokuFile koubunshoKisokuFile, List<DmPublisher> publishers, List<DmKoukaiSaki> koukaiSakis)
        {
            return 公文書規則処理.登録(loginUser, koubunshoKisoku, koubunshoKisokuFile, publishers, koukaiSakis);
        }

        public bool BLC_公文書規則処理_公開先更新(MsUser loginUser, string koubunshoKisokuId, List<DmKoukaiSaki> koukaiSakis)
        {
            return 公文書規則処理.公開先更新(loginUser, koubunshoKisokuId, koukaiSakis);
        }

        //public bool BLC_公文書規則処理_更新(MsUser loginUser, DmKoubunshoKisoku koubunshoKisoku, DmKoubunshoKisokuFile koubunshoKisokuFile, List<DmPublisher> publishers, List<DmKoukaiSaki> koukaiSakis)
        //{
        //    return 公文書規則処理.更新(loginUser, koubunshoKisoku, koubunshoKisokuFile, publishers, koukaiSakis);
        //}

        //public bool BLC_公文書規則処理_削除(MsUser loginUser, DmKoubunshoKisoku koubunshoKisoku)
        //{
        //    return 公文書規則処理.削除(loginUser, koubunshoKisoku);
        //}
    }

}
