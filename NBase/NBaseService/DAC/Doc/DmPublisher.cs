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
        List<NBaseData.DAC.DmPublisher> DmPublisher_GetRecords(MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.DmPublisher> DmPublisher_GetRecordsByLinkSakiID(MsUser loginUser, string linkSakiId);

        [OperationContract]
        NBaseData.DAC.DmPublisher DmPublisher_GetRecord(MsUser loginUser, string publisherId);

        [OperationContract]
        bool DmPublisher_InsertRecord(MsUser loginUser, DmPublisher info);

        [OperationContract]
        bool DmPublisher_UpdateRecord(MsUser loginUser, DmPublisher info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.DmPublisher> DmPublisher_GetRecords(MsUser loginUser)
        {
            return NBaseData.DAC.DmPublisher.GetRecords(loginUser);
        }

        public List<NBaseData.DAC.DmPublisher> DmPublisher_GetRecordsByLinkSakiID(MsUser loginUser, string linkSakiId)
        {
            return NBaseData.DAC.DmPublisher.GetRecordsByLinkSakiID(loginUser, linkSakiId);
        }

        public NBaseData.DAC.DmPublisher DmPublisher_GetRecord(MsUser loginUser, string publisherId)
        {
            return NBaseData.DAC.DmPublisher.GetRecord(loginUser, publisherId);
        }

        public bool DmPublisher_InsertRecord(MsUser loginUser, DmPublisher info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool DmPublisher_UpdateRecord(MsUser loginUser, DmPublisher info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
