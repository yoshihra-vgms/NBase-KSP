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
        List<NBaseData.DAC.MsDmTemplateFile> MsDmTemplateFile_GetRecords(MsUser loginUser);

        [OperationContract]
        NBaseData.DAC.MsDmTemplateFile MsDmTemplateFile_GetRecordByHoukokushoID(MsUser loginUser, string houkokushoId);

        [OperationContract]
        NBaseData.DAC.MsDmTemplateFile MsDmTemplateFile_GetRecord(MsUser loginUser, string houkokushoId);

        [OperationContract]
        bool MsDmTemplateFile_InsertRecord(MsUser loginUser, MsDmTemplateFile info);

        [OperationContract]
        bool MsDmTemplateFile_UpdateRecord(MsUser loginUser, MsDmTemplateFile info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.MsDmTemplateFile> MsDmTemplateFile_GetRecords(MsUser loginUser)
        {
            return NBaseData.DAC.MsDmTemplateFile.GetRecords(loginUser);
        }

        public NBaseData.DAC.MsDmTemplateFile MsDmTemplateFile_GetRecordByHoukokushoID(MsUser loginUser, string houkokushoId)
        {
            return NBaseData.DAC.MsDmTemplateFile.GetRecordByHoukokushoID(loginUser, houkokushoId);
        }

        public NBaseData.DAC.MsDmTemplateFile MsDmTemplateFile_GetRecord(MsUser loginUser, string houkokushoId)
        {
            return NBaseData.DAC.MsDmTemplateFile.GetRecord(loginUser, houkokushoId);
        }

        public bool MsDmTemplateFile_InsertRecord(MsUser loginUser, MsDmTemplateFile info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool MsDmTemplateFile_UpdateRecord(MsUser loginUser, MsDmTemplateFile info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
