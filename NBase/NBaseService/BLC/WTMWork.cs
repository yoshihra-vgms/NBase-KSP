using NBaseData.BLC;
using NBaseData.DAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using WtmModelBase;
using NBaseCommon;
using NBaseData.DS;
using static NBaseData.BLC.SimulationProc;

namespace NBaseService
{
    public partial interface IService
    {
        //[OperationContract]
        //int WTM_GetWorks_Exec(MsUser loginUser, string appName);

        //[OperationContract]
        //List<WorkContent> BLC_GetWorkContents(MsUser loginUser, string appName);

        //[OperationContract]
        //List<Work> BLC_GetWorks(MsUser loginUser, string appName, int vesselId, DateTime date1, DateTime date2);



        [OperationContract]
        List<DeviationAlarmInfo> BLC_GetDeviationInfos(MsUser loginUser, string appName, int vesselId, DateTime date1, DateTime date2, List<DjDousei> douseis);
    }

    public partial class Service
    {
        //public int WTM_GetWorks_Exec(MsUser loginUser, string appName)
        //{
        //    return WTMGetProc.Excecute(loginUser, appName);
        //}


        //public List<WorkContent> BLC_GetWorkContents(MsUser loginUser, string appName)
        //{
        //    return WTMGetProc.GetWorkContents(loginUser, appName);
        //}

        //public List<Work> BLC_GetWorks(MsUser loginUser, string appName, int vesselId, DateTime date1, DateTime date2)
        //{
        //    if (date2 == DateTime.MinValue)
        //        LogFile.Write(loginUser.FullName, $"BLC_GetWorks({appName}, {date1.ToShortDateString()}, {vesselId.ToString()}");
        //    else
        //        LogFile.Write(loginUser.FullName, $"BLC_GetWorks({appName}, {date1.ToShortDateString()}, {date2.ToShortDateString()}, {vesselId.ToString()}");

        //    List<Work> ret = null;

        //    try
        //    {
        //        ret = WTMGetProc.GetWorks(loginUser, appName, vesselId, date1, date2);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return ret;
        //}

        public List<DeviationAlarmInfo> BLC_GetDeviationInfos(MsUser loginUser, string appName, int vesselId, DateTime date1, DateTime date2, List<DjDousei> douseis)
        {
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            return SimulationProc.GetDeviationInfos(loginUser, seninTableCache, appName, vesselId, date1, date2, douseis);
        }
    }
}