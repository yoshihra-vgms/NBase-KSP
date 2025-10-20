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
        List<NBaseData.DAC.DmKanriKiroku> DmKanriKiroku_GetRecords(MsUser loginUser);

        [OperationContract]
        NBaseData.DAC.DmKanriKiroku DmKanriKiroku_GetRecord(MsUser loginUser, string kanriKirokuId);
        
        [OperationContract]
        List<NBaseData.DAC.DmKanriKiroku> DmKanriKiroku_GetRecordsByHoukokushoID(MsUser loginUser, string msDmHoukokushoId);
        
        //[OperationContract]
        //NBaseData.DAC.DmKanriKiroku DmKanriKiroku_GetLatestRecord(MsUser loginUser, string houkokushoId, string userId);

        [OperationContract]
        //List<NBaseData.DAC.DmKanriKiroku> DmKanriKiroku_GetPastRecords(MsUser loginUser, string houkokushoId, string userId, bool kaicho_shacho, bool sekininsha);
        List<NBaseData.DAC.DmKanriKiroku> DmKanriKiroku_GetPastRecords(MsUser loginUser, string houkokushoId, string userId, int role);

        [OperationContract]
        bool DmKanriKiroku_InsertRecord(MsUser loginUser, DmKanriKiroku info);

        [OperationContract]
        bool DmKanriKiroku_UpdateRecord(MsUser loginUser, DmKanriKiroku info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.DmKanriKiroku> DmKanriKiroku_GetRecords(MsUser loginUser)
        {
            return NBaseData.DAC.DmKanriKiroku.GetRecords(loginUser);
        }

        public NBaseData.DAC.DmKanriKiroku DmKanriKiroku_GetRecord(MsUser loginUser, string kanriKirokuId)
        {
            return NBaseData.DAC.DmKanriKiroku.GetRecord(loginUser, kanriKirokuId);
        }

        public List<NBaseData.DAC.DmKanriKiroku> DmKanriKiroku_GetRecordsByHoukokushoID(MsUser loginUser, string msDmHoukokushoId)
        {
            return NBaseData.DAC.DmKanriKiroku.GetRecordsByHoukokushoID(loginUser, msDmHoukokushoId);
        }

        //public NBaseData.DAC.DmKanriKiroku DmKanriKiroku_GetLatestRecord(MsUser loginUser, string houkokushoId, string userId)
        //{
        //    return NBaseData.DAC.DmKanriKiroku.GetLatestRecord(loginUser, houkokushoId, userId);
        //}

        //public List<NBaseData.DAC.DmKanriKiroku> DmKanriKiroku_GetPastRecords(MsUser loginUser, string houkokushoId, string userId, bool kaicho_shacho, bool sekininsha)
        //{
        //    return NBaseData.DAC.DmKanriKiroku.GetPastRecords(loginUser, houkokushoId, userId, kaicho_shacho, sekininsha);
        //}
        public List<NBaseData.DAC.DmKanriKiroku> DmKanriKiroku_GetPastRecords(MsUser loginUser, string houkokushoId, string userId, int role)
        {
            return NBaseData.DAC.DmKanriKiroku.GetPastRecords(loginUser, houkokushoId, userId, role);
        }

        public bool DmKanriKiroku_InsertRecord(MsUser loginUser, DmKanriKiroku info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool DmKanriKiroku_UpdateRecord(MsUser loginUser, DmKanriKiroku info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
