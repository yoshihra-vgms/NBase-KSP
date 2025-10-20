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
        List<NBaseData.DAC.DmKanryoInfo> DmKanryoInfo_GetRecordsByLinkSakiID(MsUser loginUser, int linkSaki, string linkSakiId);

        [OperationContract]
        NBaseData.DAC.DmKanryoInfo DmKanryoInfo_GetRecord(MsUser loginUser, string kanryoInfoId);

        [OperationContract]
        bool DmKanryoInfo_InsertRecord(MsUser loginUser, DmKanryoInfo info);

        [OperationContract]
        bool DmKanryoInfo_UpdateRecord(MsUser loginUser, DmKanryoInfo info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.DmKanryoInfo> DmKanryoInfo_GetRecordsByLinkSakiID(MsUser loginUser, int linkSaki, string linkSakiId)
        {
            return NBaseData.DAC.DmKanryoInfo.GetRecordsByLinkSaki(loginUser, linkSaki, linkSakiId);
        }

        public NBaseData.DAC.DmKanryoInfo DmKanryoInfo_GetRecord(MsUser loginUser, string kanryoInfoId)
        {
            return NBaseData.DAC.DmKanryoInfo.GetRecord(loginUser, kanryoInfoId);
        }

        public bool DmKanryoInfo_InsertRecord(MsUser loginUser, DmKanryoInfo info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool DmKanryoInfo_UpdateRecord(MsUser loginUser, DmKanryoInfo info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
