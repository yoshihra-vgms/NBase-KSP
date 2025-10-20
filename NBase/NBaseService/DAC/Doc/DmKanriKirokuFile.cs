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
        List<NBaseData.DAC.DmKanriKirokuFile> DmKanriKirokuFile_GetRecords(MsUser loginUser);

        [OperationContract]
        NBaseData.DAC.DmKanriKirokuFile DmKanriKirokuFile_GetRecordByKanriKirokuID(MsUser loginUser, string kanriKirokuId);

        [OperationContract]
        NBaseData.DAC.DmKanriKirokuFile DmKanriKirokuFile_GetRecord(MsUser loginUser, string kanriKirokuFileId);

        [OperationContract]
        bool DmKanriKirokuFile_InsertRecord(MsUser loginUser, DmKanriKirokuFile info);

        [OperationContract]
        bool DmKanriKirokuFile_UpdateRecord(MsUser loginUser, DmKanriKirokuFile info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.DmKanriKirokuFile> DmKanriKirokuFile_GetRecords(MsUser loginUser)
        {
            return NBaseData.DAC.DmKanriKirokuFile.GetRecords(loginUser);
        }

        public NBaseData.DAC.DmKanriKirokuFile DmKanriKirokuFile_GetRecordByKanriKirokuID(MsUser loginUser, string kanriKirokuId)
        {
            return NBaseData.DAC.DmKanriKirokuFile.GetRecordByKanriKirokuID(loginUser, kanriKirokuId);
        }

        public NBaseData.DAC.DmKanriKirokuFile DmKanriKirokuFile_GetRecord(MsUser loginUser, string kanriKirokuFileId)
        {
            return NBaseData.DAC.DmKanriKirokuFile.GetRecord(loginUser, kanriKirokuFileId);
        }

        public bool DmKanriKirokuFile_InsertRecord(MsUser loginUser, DmKanriKirokuFile info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool DmKanriKirokuFile_UpdateRecord(MsUser loginUser, DmKanriKirokuFile info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
