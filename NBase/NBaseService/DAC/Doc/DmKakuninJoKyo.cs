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
        List<NBaseData.DAC.DmKakuninJokyo> DmKakuninJokyo_GetRecordsByLinkSaki(MsUser loginUser, int linkSaki, string linkSakiId);
        
        [OperationContract]
        NBaseData.DAC.DmKakuninJokyo DmKakuninJokyo_GetRecordByLinkSakiUser(MsUser loginUser, int linkSaki, string linkSakiId, string userId);

        [OperationContract]
        NBaseData.DAC.DmKakuninJokyo DmKakuninJokyo_GetRecord(MsUser loginUser, string kakuninJokyoId);

        [OperationContract]
        bool DmKakuninJokyo_InsertRecord(MsUser loginUser, DmKakuninJokyo info);

        [OperationContract]
        bool DmKakuninJokyo_UpdateRecord(MsUser loginUser, DmKakuninJokyo info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.DmKakuninJokyo> DmKakuninJokyo_GetRecordsByLinkSaki(MsUser loginUser, int linkSaki, string linkSakiId)
        {
            return NBaseData.DAC.DmKakuninJokyo.GetRecordsByLinkSaki(loginUser, linkSaki, linkSakiId);
        }

        public NBaseData.DAC.DmKakuninJokyo DmKakuninJokyo_GetRecordByLinkSakiUser(MsUser loginUser, int linkSaki, string linkSakiId, string userId)
        {
            return NBaseData.DAC.DmKakuninJokyo.GetRecordByLinkSakiUser(loginUser, linkSaki, linkSakiId, userId);
        }

        public NBaseData.DAC.DmKakuninJokyo DmKakuninJokyo_GetRecord(MsUser loginUser, string kakuninJokyoId)
        {
            return NBaseData.DAC.DmKakuninJokyo.GetRecord(loginUser, kakuninJokyoId);
        }

        public bool DmKakuninJokyo_InsertRecord(MsUser loginUser, DmKakuninJokyo info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool DmKakuninJokyo_UpdateRecord(MsUser loginUser, DmKakuninJokyo info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
