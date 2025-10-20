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
        List<PtAlarmInfo> PtAlarmInfo_GetRecords(MsUser loginUser);

        [OperationContract]
        List<PtAlarmInfo> PtAlarmInfo_GetRecordByCondition(MsUser loginUser, PtAlarmInfoCondition condition);

        [OperationContract]
        List<PtAlarmInfo> PtAlarmInfo_GetRecordsBySanshoumotoId(MsUser loginUser, string sanshoumotoId);

        [OperationContract]
        bool PtAlarmInfo_UpdateRecord(MsUser loginUser, PtAlarmInfo alarmInfo);
    }

    public partial class Service
    {
        public List<PtAlarmInfo> PtAlarmInfo_GetRecords(MsUser loginUser)
        {
            return PtAlarmInfo.GetRecords(loginUser);
        }

        public List<PtAlarmInfo> PtAlarmInfo_GetRecordByCondition(MsUser loginUser, PtAlarmInfoCondition condition)
        {
            return PtAlarmInfo.GetRecordByCondition(loginUser, condition);
        }

        public List<PtAlarmInfo> PtAlarmInfo_GetRecordsBySanshoumotoId(MsUser loginUser, string sanshoumotoId)
        {
            return PtAlarmInfo.GetRecordsBySanshoumotoId(loginUser, sanshoumotoId);
        }

        public bool PtAlarmInfo_UpdateRecord(MsUser loginUser, PtAlarmInfo alarmInfo)
        {
            return alarmInfo.UpdateRecord(loginUser);
        }
    }
}