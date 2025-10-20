using System;
using System.Collections.Generic;
using WtmModelBase;

namespace WtmData
{
    public interface AccessorProxy
    {


        List<Work> GetWorks(DateTime date1, DateTime date2, int seninId = 0, int vessel_id = 0);

        Work GetBeInWork(int seninId);
        List<Work> GetBeInWorks(int vesselId);

        List<WorkSummary> GetWorkSummaries(DateTime date1, DateTime date2, int seninId = 0, int vessel_id = 0);


        bool StartWork(int vesselId, int seninId, DateTime d);

        bool InsertWork(Work w);
        bool FinshWork(Work w);

        bool DeleteWork(Work w);

        bool EditWork(Work delWork, Work editWork);

        bool CanCopyWork(Work w, List<int> seninIds, DateTime fromDay, DateTime toDay, out List<int> errorSeninIds);

        bool CopyWork(Work w, List<int> seninIds, DateTime fromDay, DateTime toDay);


        #region Setting
        Setting GetSetting();
        #endregion


        #region WorkContent
        List<WorkContent> GetWorkContents();
        #endregion


        #region RankCategory
        List<RankCategory> GetRankCategories();
        #endregion


        #region Role
        List<Role> GetRoles();
        #endregion



        #region VesselMovement
        List<VesselMovement> GetVesselMovementDispRecord(DateTime date1, DateTime date2, int vesselid = 0);

        bool InsertUpdateVesselMovement(VesselMovement vm);
        #endregion



        #region  MovementInfo
        //List<MovementInfoTek> GetMovementInfoRecord(DateTime date1, DateTime date2, int vessel_id);

        //bool InsertUpdateMovementInfo(string vesselid, string dateinfo, List<MovementInfoTek> mvis);
        #endregion


        #region Anchorage 
        //List<AnchorageTek> GetAnchorageRecords(DateTime date1, DateTime date2, int vessel_id);

        //bool InsertUpdateAnchorage(string vesselId, string dateinfo, List<AnchorageTek> ancs);
        #endregion




        #region VesselApprovalMonth
        VesselApprovalMonth GetVesselApprovalMonth(int vesselId, DateTime approvalMonth);

        bool InsertOrUpdateApprovalMonth(int vesselId, int crewId, DateTime approvalMonth);
        #endregion



        #region VesselApprovalDay
        List<VesselApprovalDay> GetVesselApprovalDay(int vesselId, DateTime approvalDay);

        int InsertOrUpdateApprovalDay(int vesselId, DateTime approvalDay, int crewId, List<int> approvedCrewIds);

        bool DeleteApprovalDay(int vesselId, DateTime approvalDay, int removeCrewId);
        #endregion



        #region SummaryTimes
        List<SummaryTimes> GetSummaryTimes(int vesselId, DateTime summaryDate);

        bool InsertOrUpdateSummaryTimes(int crewId, int vesselId, DateTime summaryDate, string allowanceTime);
        #endregion
    }
}
