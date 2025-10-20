using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.ServiceModel;
using System.Collections.Generic;
using NBaseData.DAC;
using ORMapping;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<MsBerth> MsBerth_GetRecords(MsUser loginUser);
        [OperationContract]
        List<MsBerth> MsBerth_GetRecordByBerthCodeBerthName(NBaseData.DAC.MsUser loginUser, string berthCode, string berthName);

        [OperationContract]
        MsBerth MsBerth_GetRecordByBerthCode(NBaseData.DAC.MsUser loginUser, string berthCode);

        [OperationContract]
        bool MsBerth_InsertRecord(NBaseData.DAC.MsUser loginUser, MsBerth berth);

        [OperationContract]
        bool MsBerth_UpdateRecord(NBaseData.DAC.MsUser loginUser, MsBerth berth);

        [OperationContract]
        List<MsBerth> MsBerth_GetRecordByMsKichiID(NBaseData.DAC.MsUser loginUser, string ms_kichi_id);
    }

    public partial class Service
    {
        public List<MsBerth> MsBerth_GetRecords(MsUser loginUser)
        {
            return MsBerth.GetRecords(loginUser);
        }

        public List<MsBerth> MsBerth_GetRecordByBerthCodeBerthName(NBaseData.DAC.MsUser loginUser, string berthCode, string berthName)
        {
            return MsBerth.GetRecordByBerthCodeBerthName(loginUser, berthCode, berthName);
        }

        public MsBerth MsBerth_GetRecordByBerthCode(NBaseData.DAC.MsUser loginUser, string berthCode)
        {
            return MsBerth.GetRecordByBerthCode(loginUser, berthCode);
        }

        public bool MsBerth_InsertRecord(NBaseData.DAC.MsUser loginUser, MsBerth berth)
        {
            return berth.InsertRecord(loginUser);
        }

        public bool MsBerth_UpdateRecord(NBaseData.DAC.MsUser loginUser, MsBerth berth)
        {
            return berth.UpdateRecord(loginUser);
        }


        public List<MsBerth> MsBerth_GetRecordByMsKichiID(NBaseData.DAC.MsUser loginUser, string ms_kichi_id)
        {
            return MsBerth.GetRecordByMsKichiID(loginUser, ms_kichi_id);
        }
    }
}
