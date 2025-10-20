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
        List<NBaseData.DAC.MsDmHoukokusho> MsDmHoukokusho_GetRecords(MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.MsDmHoukokusho> MsDmHoukokusho_SearchRecords(MsUser loginUser, string bunruiId, string shoubunruiId, string bunshoNo, string bunshoName);

        [OperationContract]
        NBaseData.DAC.MsDmHoukokusho MsDmHoukokusho_GetRecord(MsUser loginUser, string msDmHoukokushoId);

        [OperationContract]
        bool MsDmHoukokusho_InsertRecord(MsUser loginUser, MsDmHoukokusho info);

        [OperationContract]
        bool MsDmHoukokusho_UpdateRecord(MsUser loginUser, MsDmHoukokusho info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.MsDmHoukokusho> MsDmHoukokusho_GetRecords(MsUser loginUser)
        {
            return NBaseData.DAC.MsDmHoukokusho.GetRecords(loginUser);
        }

        public List<NBaseData.DAC.MsDmHoukokusho> MsDmHoukokusho_SearchRecords(MsUser loginUser, string bunruiId, string shoubunruiId, string bunshoNo, string bunshoName)
        {
            return NBaseData.DAC.MsDmHoukokusho.SearchRecords(loginUser, bunruiId, shoubunruiId, bunshoNo, bunshoName);
        }

        public NBaseData.DAC.MsDmHoukokusho MsDmHoukokusho_GetRecord(MsUser loginUser, string msDmHoukokushoId)
        {
            return NBaseData.DAC.MsDmHoukokusho.GetRecord(loginUser, msDmHoukokushoId);
        }

        public bool MsDmHoukokusho_InsertRecord(MsUser loginUser, MsDmHoukokusho info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool MsDmHoukokusho_UpdateRecord(MsUser loginUser, MsDmHoukokusho info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
