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
        List<NBaseData.DAC.DmKoubunshoKisokuFile> DmKoubunshoKisokuFile_GetRecords(MsUser loginUser);

        [OperationContract]
        NBaseData.DAC.DmKoubunshoKisokuFile DmKoubunshoKisokuFile_GetRecordByKoubunshoKisokuID(MsUser loginUser, string koubunshoKisokuId);

        [OperationContract]
        NBaseData.DAC.DmKoubunshoKisokuFile DmKoubunshoKisokuFile_GetRecord(MsUser loginUser, string koubunshoKisokuFileId);

        [OperationContract]
        bool DmKoubunshoKisokuFile_InsertRecord(MsUser loginUser, DmKoubunshoKisokuFile info);

        [OperationContract]
        bool DmKoubunshoKisokuFile_UpdateRecord(MsUser loginUser, DmKoubunshoKisokuFile info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.DmKoubunshoKisokuFile> DmKoubunshoKisokuFile_GetRecords(MsUser loginUser)
        {
            return NBaseData.DAC.DmKoubunshoKisokuFile.GetRecords(loginUser);
        }

        public NBaseData.DAC.DmKoubunshoKisokuFile DmKoubunshoKisokuFile_GetRecordByKoubunshoKisokuID(MsUser loginUser, string koubunshoKisokuId)
        {
            return NBaseData.DAC.DmKoubunshoKisokuFile.GetRecordByKoubunshoKisokuID(loginUser, koubunshoKisokuId);
        }

        public NBaseData.DAC.DmKoubunshoKisokuFile DmKoubunshoKisokuFile_GetRecord(MsUser loginUser, string koubunshoKisokuFileId)
        {
            return NBaseData.DAC.DmKoubunshoKisokuFile.GetRecord(loginUser, koubunshoKisokuFileId);
        }

        public bool DmKoubunshoKisokuFile_InsertRecord(MsUser loginUser, DmKoubunshoKisokuFile info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool DmKoubunshoKisokuFile_UpdateRecord(MsUser loginUser, DmKoubunshoKisokuFile info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
