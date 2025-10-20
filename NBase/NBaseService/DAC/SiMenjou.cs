using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using System.ServiceModel;
using NBaseData.DS;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<SiMenjou> SiMenjou_GetRecordsByMsSeninID(MsUser loginUser, int msSeninId);

        [OperationContract]
        SiMenjou SiMenjou_GetRecord(MsUser loginUser, string siMenjouId);

        [OperationContract]
        List<SiMenjou> SiMenjou_GetRecordsByMsSiMenjouKindID(MsUser loginUser, int ms_si_menjyo_kind_id);

        [OperationContract]
        List<SiMenjou> SiMenjou_GetRecordsByMsSiMenjouID(MsUser loginUser, int ms_si_menjyo_id);
    }

    public partial class Service
    {
        public List<SiMenjou> SiMenjou_GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            return SiMenjou.GetRecordsByMsSeninID(loginUser, msSeninId);
        }


        public SiMenjou SiMenjou_GetRecord(MsUser loginUser, string siMenjouId)
        {
            return SiMenjou.GetRecord(loginUser, siMenjouId);
        }

        public List<SiMenjou> SiMenjou_GetRecordsByMsSiMenjouKindID(MsUser loginUser, int ms_si_menjyo_kind_id)
        {
            return SiMenjou.GetRecordsByMsSiMenjouKindID(loginUser, ms_si_menjyo_kind_id);
        }

        public List<SiMenjou> SiMenjou_GetRecordsByMsSiMenjouID(MsUser loginUser, int ms_si_menjyo_id)
        {
            return SiMenjou.GetRecordsByMsSiMenjouID(loginUser, ms_si_menjyo_id);
        }
    }
}
