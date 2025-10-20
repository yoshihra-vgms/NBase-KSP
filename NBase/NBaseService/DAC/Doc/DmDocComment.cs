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
        List<NBaseData.DAC.DmDocComment> DmDocComment_GetRecordsByLinkSakiID(MsUser loginUser, int linkSaki, string linkSakiId);

        [OperationContract]
        NBaseData.DAC.DmDocComment DmDocComment_GetRecord(MsUser loginUser, string docCommentId);

        [OperationContract]
        bool DmDocComment_InsertRecord(MsUser loginUser, DmDocComment info);

        [OperationContract]
        bool DmDocComment_UpdateRecord(MsUser loginUser, DmDocComment info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.DmDocComment> DmDocComment_GetRecordsByLinkSakiID(MsUser loginUser, int linkSaki, string linkSakiId)
        {
            return NBaseData.DAC.DmDocComment.GetRecordsByLinkSaki(loginUser, linkSaki, linkSakiId);
        }

        public NBaseData.DAC.DmDocComment DmDocComment_GetRecord(MsUser loginUser, string docCommentId)
        {
            return NBaseData.DAC.DmDocComment.GetRecord(loginUser, docCommentId);
        }

        public bool DmDocComment_InsertRecord(MsUser loginUser, DmDocComment info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool DmDocComment_UpdateRecord(MsUser loginUser, DmDocComment info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
