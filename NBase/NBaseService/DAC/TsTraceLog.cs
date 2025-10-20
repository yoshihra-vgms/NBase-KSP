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
        bool TraceLogging(MsUser loginUser, string userId, string bumonId, string function, string hostName);

        [OperationContract]
        bool TraceStart(string hostName);

        [OperationContract]
        bool TraceEnd(MsUser loginUser, string userId, string bumonId, string hostName);
    }

    public partial class Service
    {
        public bool TraceLogging(MsUser loginUser, string userId, string bumonId, string function, string hostName)
        {
            TsTraceLog log = new TsTraceLog();

            log.AccessDate = DateTime.Now;
            log.UserId = userId;
            log.BumonId = bumonId;
            log.Function1 = function;
            log.System = "NBase";

            log.HostName = hostName;
            log.StartDate = DateTime.MinValue;
            log.EndDate = DateTime.MinValue;

            return log.InsertRecord(loginUser);
        }

        public bool TraceStart(string hostName)
        {
            MsUser loginUser = new MsUser();

            TsTraceLog log = new TsTraceLog();

            log.AccessDate = DateTime.Now;
            log.UserId = "start";
            //log.BumonId = bumonId;
            log.Function1 = "開始";
            log.System = "NBase";

            log.HostName = hostName;
            log.StartDate = DateTime.Now;
            //log.EndDate = DateTime.MinValue;

            return log.InsertRecord(loginUser);
        }
        public bool TraceEnd(MsUser loginUser, string userId, string bumonId, string hostName)
        {
            TsTraceLog log = new TsTraceLog();

            log.AccessDate = DateTime.Now;
            log.UserId = userId;
            log.BumonId = bumonId;
            log.Function1 = "終了";
            log.System = "NBase";

            log.HostName = hostName;
            //log.StartDate = DateTime.MinValue;
            log.EndDate = DateTime.Now;

            return log.InsertRecord(loginUser);
        }
    }
}
