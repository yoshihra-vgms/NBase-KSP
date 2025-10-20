using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using WtmModelBase;
using WtmModels;
using WtmData;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<Work> GetWorks(string connectionKey, DateTime date1, DateTime date2, int seninId = 0, int vesselId = 0);

        [OperationContract]
        Work GetBeInWork(string connectionKey, int seninId);

        [OperationContract]
        List<Work> GetBeInWorks(string connectionKey, int vesselId);

        [OperationContract]
        List<WorkSummary> GetWorkSummaries(string connectionKey, DateTime date1, DateTime date2, int seninId = 0, int vesselId = 0);


        [OperationContract]
        bool StartWork(string connectionKey, int vesselId, int seninId, DateTime d);

        [OperationContract]
        bool InsertWork(string connectionKey, Work w);

        [OperationContract]
        bool FinshWork(string connectionKey, Work w);

        [OperationContract]
        bool DeleteWork(string connectionKey, Work w);

        [OperationContract]
        bool EditWork(string connectionKey, Work delWork, Work editWork);

        [OperationContract]
        bool CanCopyWork(string connectionKey, Work w, List<int> seninIds, DateTime fromDay, DateTime toDay, out List<int> errorSeninIds);


        [OperationContract]
        bool CopyWork(string connectionKey, Work w, List<int> seninIds, DateTime fromDay, DateTime toDay);


        [OperationContract]
        Setting GetSetting(string connectionKey);



        [OperationContract]
        List<WorkContent> GetWorkContents(string connectionKey);



        [OperationContract]
        List<RankCategory> GetRankCategories(string connectionKey);

        [OperationContract]
        List<Role> GetRoles(string connectionKey);






        [OperationContract]
        List<VesselMovement> GetVesselMovementDispRecord(string connectionKey, DateTime date1, DateTime date2, int vesselid = 0);

        [OperationContract]
        bool InsertUpdateVesselMovement(string connectionKey, VesselMovement vm);

        //[OperationContract]
        //List<MovementInfoTek> GetMovementInfoRecord(string connectionKey, DateTime date1, DateTime date2, int vessel_id);

        //[OperationContract]
        //bool InsertUpdateMovementInfo(string connectionKey, string vesselid, string dateinfo, List<MovementInfoTek> mvis);


        //[OperationContract]
        //List<AnchorageTek> GetAnchorageRecords(string connectionKey, DateTime date1, DateTime date2, int vessel_id);

        //[OperationContract]
        //bool InsertUpdateAnchorage(string connectionKey, string vesselId, string dateinfo, List<AnchorageTek> ancs);




        [OperationContract]
        VesselApprovalMonth GetVesselApprovalMonth(string connectionKey, int vesselId, DateTime approvalMonth);

        [OperationContract]
        bool InsertOrUpdateApprovalMonth(string connectionKey, int vesselId, int crewId, DateTime approvalMonth);

        [OperationContract]
        List<VesselApprovalDay> GetVesselApprovalDay(string connectionKey, int vesselId, DateTime approvalDay);

        [OperationContract]
        int InsertOrUpdateApprovalDay(string connectionKey, int vesselId, DateTime approvalDay, int crewId, List<int> approvedCrewIds);

        [OperationContract]
        bool DeleteApprovalDay(string connectionKey, int vesselId, DateTime approvalDay, int removeCrewId);



        [OperationContract]
        List<SummaryTimes> GetSummaryTimes(string connectionKey, int vesselId, DateTime summaryDate);

        [OperationContract]
        bool InsertOrUpdateSummaryTimes(string connectionKey, int crewId, int vesselId, DateTime summaryDate, string allowanceTime);
    }




    public partial class Service
    {
        public List<Work> GetWorks(string connectionKey, DateTime date1, DateTime date2, int seninId = 0, int vesselId = 0)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            return WtmAccessor.Instance().GetWorks(date1, date2, seninId, vesselId);
        }

        public Work GetBeInWork(string connectionKey, int seninId)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            return WtmAccessor.Instance().GetBeInWork(seninId);
        }

        public List<Work> GetBeInWorks(string connectionKey, int vesselId)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            return WtmAccessor.Instance().GetBeInWorks(vesselId);
        }

        public List<WorkSummary> GetWorkSummaries(string connectionKey, DateTime date1, DateTime date2, int seninId = 0, int vesselId = 0)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            var ret = WtmAccessor.Instance().GetWorkSummaries(date1, date2, seninId, vesselId);
            return ret;
        }




        public bool StartWork(string connectionKey, int vesselId, int seninId, DateTime d)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            WtmDac.SITE_ID = connectionKey.ToUpper();
            return WtmAccessor.Instance().StartWork(vesselId, seninId, d);
        }

        public bool InsertWork(string connectionKey, Work w)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            WtmDac.SITE_ID = connectionKey.ToUpper();
            return WtmAccessor.Instance().InsertWork(w);
        }

        public bool FinshWork(string connectionKey, Work w)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            WtmDac.SITE_ID = connectionKey.ToUpper();
            return WtmAccessor.Instance().FinshWork(w);
        }

        public bool DeleteWork(string connectionKey, Work w)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            WtmDac.SITE_ID = connectionKey.ToUpper();
            return WtmAccessor.Instance().EditWork(w, null);
        }

        public bool EditWork(string connectionKey, Work delWork, Work editWork)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            WtmDac.SITE_ID = connectionKey.ToUpper();
            return WtmAccessor.Instance().EditWork(delWork, editWork);
        }


        public bool CanCopyWork(string connectionKey, Work w, List<int> seninIds, DateTime fromDay, DateTime toDay, out List<int> errorSeninIds)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            return WtmAccessor.Instance().CanCopyWork(w, seninIds, fromDay, toDay, out errorSeninIds);
        }



        public bool CopyWork(string connectionKey, Work w, List<int> seninIds, DateTime fromDay, DateTime toDay)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            WtmDac.SITE_ID = connectionKey.ToUpper();
            return WtmAccessor.Instance().CopyWork(w, seninIds, fromDay, toDay);
        }



        #region Setting
        public Setting GetSetting(string connectionKey)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            return WtmAccessor.Instance().GetSetting();
        }
        #endregion


        #region WorkContent
        public List<WorkContent> GetWorkContents(string connectionKey)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            return WtmAccessor.Instance().GetWorkContents();
        }
        #endregion



        #region RankCategory
        public List<RankCategory> GetRankCategories(string connectionKey)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            return WtmAccessor.Instance().GetRankCategories();
        }
        #endregion

        #region Role
        public List<Role> GetRoles(string connectionKey)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            return WtmAccessor.Instance().GetRoles();
        }
        #endregion





        #region VesselMovement
        public List<VesselMovement> GetVesselMovementDispRecord(string connectionKey, DateTime date1, DateTime date2, int vesselid = 0)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            return WtmAccessor.Instance().GetVesselMovementDispRecord(date1, date2, vesselid);
        }

        public bool InsertUpdateVesselMovement(string connectionKey, VesselMovement vm)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            WtmDac.SITE_ID = connectionKey.ToUpper();
            return WtmAccessor.Instance().InsertUpdateVesselMovement(vm);
        }

        #endregion


        #region  MovementInfo

        //public List<MovementInfoTek> GetMovementInfoRecord(string connectionKey, DateTime date1, DateTime date2, int vessel_id)
        //{
        //    WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
        //    return WtmAccessor.Instance().GetMovementInfoRecord(date1, date2, vessel_id);
        //}

        //public bool InsertUpdateMovementInfo(string connectionKey, string vesselid, string dateinfo, List<MovementInfoTek> mvis)
        //{
        //    WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
        //    return WtmAccessor.Instance().InsertUpdateMovementInfo(vesselid, dateinfo, mvis);
        //}

        #endregion


        #region Anchorage 

        //public List<AnchorageTek> GetAnchorageRecords(string connectionKey, DateTime date1, DateTime date2, int vessel_id)
        //{
        //    WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
        //    return WtmAccessor.Instance().GetAnchorageRecords(date1, date2, vessel_id);
        //}

        //public bool InsertUpdateAnchorage(string connectionKey, string vesselId, string dateinfo, List<AnchorageTek> ancs)
        //{
        //    WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
        //    return WtmAccessor.Instance().InsertUpdateAnchorage(vesselId, dateinfo, ancs);
        //}

        #endregion





        #region VesselApprovalMonth
        public VesselApprovalMonth GetVesselApprovalMonth(string connectionKey, int vesselId, DateTime approvalMonth)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            return WtmAccessor.Instance().GetVesselApprovalMonth(vesselId, approvalMonth);
        }

        public bool InsertOrUpdateApprovalMonth(string connectionKey, int vesselId, int crewId, DateTime approvalMonth)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            WtmDac.SITE_ID = connectionKey.ToUpper();
            return WtmAccessor.Instance().InsertOrUpdateApprovalMonth(vesselId, crewId, approvalMonth);
        }
        #endregion

        #region VesselApprovalDay
        public List<VesselApprovalDay> GetVesselApprovalDay(string connectionKey, int vesselId, DateTime approvalDay)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            return WtmAccessor.Instance().GetVesselApprovalDay(vesselId, approvalDay);
        }

        public int InsertOrUpdateApprovalDay(string connectionKey, int vesselId, DateTime approvalDay, int crewId, List<int>approvedCrewIds)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            WtmDac.SITE_ID = connectionKey.ToUpper();
            return WtmAccessor.Instance().InsertOrUpdateApprovalDay(vesselId, approvalDay, crewId, approvedCrewIds);
        }

        public bool DeleteApprovalDay(string connectionKey, int vesselId, DateTime approvalDay, int removeCrewId)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            return WtmAccessor.Instance().DeleteApprovalDay(vesselId, approvalDay, removeCrewId);
        }
        #endregion



        #region SummaryTimes
        public List<SummaryTimes> GetSummaryTimes(string connectionKey, int vesselId, DateTime summaryDate)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            return WtmAccessor.Instance().GetSummaryTimes(vesselId, summaryDate);
        }

        public bool InsertOrUpdateSummaryTimes(string connectionKey, int crewId, int vesselId, DateTime summaryDate, string allowanceTime)
        {
            WtmAccessor.Instance().DacProxy = new LocalAccessor(connectionKey);
            WtmDac.SITE_ID = connectionKey.ToUpper();
            return WtmAccessor.Instance().InsertOrUpdateSummaryTimes(crewId, vesselId, summaryDate, allowanceTime);
        }

        #endregion


    }
}
