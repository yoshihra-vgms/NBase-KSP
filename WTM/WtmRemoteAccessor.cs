using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WtmData;
using WtmModelBase;


namespace WTM
{
    public class RemoteAccessor : AccessorProxy
    {
        private string ConnectionKey { get; }


        public RemoteAccessor(string connectionKey)
        {
            this.ConnectionKey = connectionKey;
        }


        public List<Work> GetWorks(DateTime date1, DateTime date2, int seninId = 0, int vesselId = 0)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                var ret = serviceClient.GetWorks(ConnectionKey, date1, date2, seninId, vesselId);

                ret.ForEach(o => { o.ConvertWorkContentDetail(); });


                ret.ForEach(o => { o.ConvertDeviation(1, o.Deviation); });
                ret.ForEach(o => { o.ConvertDeviation(2, o.Deviation1Week); });
                ret.ForEach(o => { o.ConvertDeviation(3, o.Deviation4Week); });
                ret.ForEach(o => { o.ConvertDeviation(4, o.DeviationResttime); });


                return ret;
            }
        }

        public Work GetBeInWork(int seninId)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.GetBeInWork(ConnectionKey, seninId);
            }
        }

        public List<Work> GetBeInWorks(int vesselId)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.GetBeInWorks(ConnectionKey, vesselId);
            }
        }



        public List<WorkSummary> GetWorkSummaries(DateTime date1, DateTime date2, int seninId = 0, int vesselId = 0)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.GetWorkSummaries(ConnectionKey, date1, date2, seninId, vesselId);
            }
        }




        public bool StartWork(int vesselId, int seninId, DateTime d)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.StartWork(ConnectionKey, vesselId, seninId, d);
            }
        }

        public bool InsertWork(Work w)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.InsertWork(ConnectionKey, w);
            }
        }

        public bool FinshWork(Work w)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.FinshWork(ConnectionKey, w);
            }
        }

        public bool DeleteWork(Work w)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.EditWork(ConnectionKey, w, null);
            }
        }

        public bool EditWork(Work delWork, Work editWork)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.EditWork(ConnectionKey, delWork, editWork);
            }
        }


        public bool CanCopyWork(Work w, List<int> seninIds, DateTime fromDay, DateTime toDay, out List<int> errorSeninIds)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.CanCopyWork(ConnectionKey, w, seninIds, fromDay, toDay, out errorSeninIds);
            }
        }



        public bool CopyWork(Work w, List<int> seninIds, DateTime fromDay, DateTime toDay)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.CopyWork(ConnectionKey, w, seninIds, fromDay, toDay);
            }
        }

        #region Setting
        public Setting GetSetting()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.GetSetting(ConnectionKey);
            }
        }
        #endregion


        #region WorkContent
        public List<WorkContent> GetWorkContents()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.GetWorkContents(ConnectionKey);
            }
        }
        #endregion



        #region RankCategory
        public List<RankCategory> GetRankCategories()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                var ret = serviceClient.GetRankCategories(ConnectionKey);
                ret.ForEach(o => { o.ConvertRanks(); });
                return ret;
            }
        }
        #endregion

        #region Role
        public List<Role> GetRoles()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                var ret = serviceClient.GetRoles(ConnectionKey);
                ret.ForEach(o => { o.ConvertRanks(); });
                return ret;
            }
        }
        #endregion








        #region VesselMovement
        public List<VesselMovement> GetVesselMovementDispRecord(DateTime date1, DateTime date2, int vesselid = 0)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.GetVesselMovementDispRecord(ConnectionKey, date1, date2, vesselid);
            }
        }

        public bool InsertUpdateVesselMovement(VesselMovement vm)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.InsertUpdateVesselMovement(ConnectionKey, vm);
            }
        }

        #endregion


        #region  MovementInfo

        //public List<MovementInfoTek> GetMovementInfoRecord(DateTime date1, DateTime date2, int vessel_id)
        //{
        //    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
        //    {
        //        return serviceClient.GetMovementInfoRecord(ConnectionKey, date1, date2, vessel_id);
        //    }
        //}

        //public bool InsertUpdateMovementInfo(string vesselid, string dateinfo, List<MovementInfoTek> mvis)
        //{
        //    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
        //    {
        //        return serviceClient.InsertUpdateMovementInfo(ConnectionKey, vesselid, dateinfo, mvis);
        //    }
        //}

        #endregion


        #region Anchorage 

        //public List<AnchorageTek> GetAnchorageRecords(DateTime date1, DateTime date2, int vessel_id)
        //{
        //    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
        //    {
        //        return serviceClient.GetAnchorageRecords(ConnectionKey, date1, date2, vessel_id);
        //    }
        //}

        //public bool InsertUpdateAnchorage(string vesselId, string dateinfo, List<AnchorageTek> ancs)
        //{
        //    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
        //    {
        //        return serviceClient.InsertUpdateAnchorage(ConnectionKey, vesselId, dateinfo, ancs);
        //    }
        //}

        #endregion


        #region VesselApprovalMonth
        public VesselApprovalMonth GetVesselApprovalMonth(int vesselId, DateTime approvalMonth)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.GetVesselApprovalMonth(ConnectionKey, vesselId, approvalMonth);
            }
        }

        public bool InsertOrUpdateApprovalMonth(int vesselId, int crewId, DateTime approvalMonth)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.InsertOrUpdateApprovalMonth(ConnectionKey, vesselId, crewId, approvalMonth);
            }
        }
        #endregion

        #region VesselApprovalDay
        public List<VesselApprovalDay> GetVesselApprovalDay(int vesselId, DateTime approvalDay)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.GetVesselApprovalDay(ConnectionKey, vesselId, approvalDay);
            }
        }

        public int InsertOrUpdateApprovalDay(int vesselId, DateTime approvalDay, int crewId, List<int>approvedCrewIds)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.InsertOrUpdateApprovalDay(ConnectionKey, vesselId, approvalDay, crewId, approvedCrewIds);
            }
        }

        public bool DeleteApprovalDay(int vesselId, DateTime approvalDay, int removeCrewId)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.DeleteApprovalDay(ConnectionKey, vesselId, approvalDay, removeCrewId);
            }
        }
        #endregion



        #region SummaryTimes
        public List<SummaryTimes> GetSummaryTimes(int vesselId, DateTime summaryDate)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.GetSummaryTimes(ConnectionKey, vesselId, summaryDate);
            }
        }

        public bool InsertOrUpdateSummaryTimes(int crewId, int vesselId, DateTime summaryDate, string allowanceTime)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.InsertOrUpdateSummaryTimes(ConnectionKey, crewId, vesselId, summaryDate, allowanceTime);
            }
        }

        #endregion


    }
}
