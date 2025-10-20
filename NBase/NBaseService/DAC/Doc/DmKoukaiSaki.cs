using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<NBaseData.DAC.DmKoukaiSaki> DmKoukaiSaki_GetRecords(MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.DmKoukaiSaki> DmKoukaiSaki_GetRecordsByLinkSakiID(MsUser loginUser, string linkSakiId);

        [OperationContract]
        NBaseData.DAC.DmKoukaiSaki DmKoukaiSaki_GetRecord(MsUser loginUser, string koukaiSakiId);

        [OperationContract]
        bool DmKoukaiSaki_InsertRecord(MsUser loginUser, DmKoukaiSaki info);

        [OperationContract]
        bool DmKoukaiSaki_UpdateRecord(MsUser loginUser, DmKoukaiSaki info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.DmKoukaiSaki> DmKoukaiSaki_GetRecords(MsUser loginUser)
        {
            return NBaseData.DAC.DmKoukaiSaki.GetRecords(loginUser);
        }

        public List<NBaseData.DAC.DmKoukaiSaki> DmKoukaiSaki_GetRecordsByLinkSakiID(MsUser loginUser, string linkSakiId)
        {
            return NBaseData.DAC.DmKoukaiSaki.GetRecordsByLinkSakiID(loginUser, linkSakiId);
        }

        public NBaseData.DAC.DmKoukaiSaki DmKoukaiSaki_GetRecord(MsUser loginUser, string koukaiSakiId)
        {
            return NBaseData.DAC.DmKoukaiSaki.GetRecord(loginUser, koukaiSakiId);
        }

        public bool DmKoukaiSaki_InsertRecord(MsUser loginUser, DmKoukaiSaki info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool DmKoukaiSaki_UpdateRecord(MsUser loginUser, DmKoukaiSaki info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
