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
        bool BLC_報告書処理_登録(MsUser loginUser, MsDmHoukokusho houkokusyo, MsDmTemplateFile templateFile, List<DmPublisher> publishers, List<DmKoukaiSaki> koukaiSakis);
        
        [OperationContract]
        bool BLC_報告書処理_更新(MsUser loginUser, MsDmHoukokusho houkokusyo, MsDmTemplateFile templateFile, List<DmPublisher> publishers, List<DmKoukaiSaki> koukaiSakis);
        
        [OperationContract]
        bool BLC_報告書処理_削除(MsUser loginUser, MsDmHoukokusho houkokusyo);
    }

    public partial class Service
    {
        public bool BLC_報告書処理_登録(MsUser loginUser, MsDmHoukokusho houkokusyo, MsDmTemplateFile templateFile, List<DmPublisher> publishers, List<DmKoukaiSaki> koukaiSakis)
        {
            return 報告書処理.登録(loginUser, houkokusyo, templateFile, publishers, koukaiSakis);
        }

        public bool BLC_報告書処理_更新(MsUser loginUser, MsDmHoukokusho houkokusyo, MsDmTemplateFile templateFile, List<DmPublisher> publishers, List<DmKoukaiSaki> koukaiSakis)
        {
            return 報告書処理.更新(loginUser, houkokusyo, templateFile, publishers, koukaiSakis);
        }

        public bool BLC_報告書処理_削除(MsUser loginUser, MsDmHoukokusho houkokusyo)
        {
            return 報告書処理.削除(loginUser, houkokusyo);
        }
    }

}
