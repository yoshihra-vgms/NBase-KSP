using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WtmModelBase;
using WtmModels;

namespace WtmData
{
    public class WtmAccessor
    {
        private static readonly WtmAccessor INSTANCE = new WtmAccessor();

        public AccessorProxy DacProxy { get; set; }


        private WtmAccessor()
        {
        }
        public static WtmAccessor Instance()
        {
            return INSTANCE;
        }
        public static WtmAccessor Instance(bool cached)
        {
            if (cached)
            {
                return INSTANCE;
            }
            else
            {
                return new WtmAccessor();
            }
        }

        public List<Work> GetWorks(DateTime date1, DateTime date2, int seninId = 0, int vesselId = 0)
        {
            return DacProxy.GetWorks(date1, date2, seninId, vesselId);
        }

        public Work GetBeInWork(int seninId)
        {
            return DacProxy.GetBeInWork(seninId);
        }
        public List<Work> GetBeInWorks(int vesselId)
        {
            return DacProxy.GetBeInWorks(vesselId);
        }

        public List<WorkSummary> GetWorkSummaries(DateTime date1, DateTime date2, int seninId = 0, int vesselId = 0)
        {
            return DacProxy.GetWorkSummaries(date1, date2, seninId, vesselId);
        }



        public bool StartWork(int vesselId, int seninId, DateTime d)
        {
            return DacProxy.StartWork(vesselId, seninId, d);
        }

        public bool InsertWork(Work w)
        {
            return DacProxy.InsertWork(w);
        }

        public bool FinshWork(Work w)
        {
            return DacProxy.FinshWork(w);
        }

        public bool DeleteWork(Work w)
        {
            return DacProxy.EditWork(w, null);
        }

        public bool EditWork(Work delWork, Work editWork)
        {
            return DacProxy.EditWork(delWork, editWork);
        }


        public bool CanCopyWork(Work w, List<int> seninIds, DateTime fromDay, DateTime toDay, out List<int> errorSeninIds)
        {
            return DacProxy.CanCopyWork(w, seninIds, fromDay, toDay, out errorSeninIds);
        }



        public bool CopyWork(Work w, List<int> seninIds, DateTime fromDay, DateTime toDay)
        {
            return DacProxy.CopyWork(w, seninIds, fromDay, toDay);
        }


        #region Setting
        public Setting GetSetting()
        {
            return DacProxy.GetSetting();
        }
        #endregion

        #region WorkContent
        public List<WorkContent> GetWorkContents()
        {
            return DacProxy.GetWorkContents();
        }
        #endregion

        #region RankCategory
        public List<RankCategory> GetRankCategories()
        {
            return DacProxy.GetRankCategories();
        }
        #endregion

        #region Role
        public List<Role> GetRoles()
        {
            return DacProxy.GetRoles();
        }
        #endregion




        #region VesselMovement
        public List<VesselMovement> GetVesselMovementDispRecord(DateTime date1, DateTime date2, int vesselid = 0)
        {
            return DacProxy.GetVesselMovementDispRecord(date1, date2, vesselid);
        }

        public bool InsertUpdateVesselMovement(VesselMovement vm)
        {
            return DacProxy.InsertUpdateVesselMovement(vm);
        }

        #endregion


        #region  MovementInfo

        //public List<MovementInfoTek> GetMovementInfoRecord(DateTime date1, DateTime date2, int vessel_id)
        //{
        //    return DacProxy.GetMovementInfoRecord(date1, date2, vessel_id);
        //}

        //public bool InsertUpdateMovementInfo(string vesselid, string dateinfo, List<MovementInfoTek> mvis)
        //{
        //    return DacProxy.InsertUpdateMovementInfo(vesselid, dateinfo, mvis);
        //}

        #endregion


        #region Anchorage 

        //public List<AnchorageTek> GetAnchorageRecords(DateTime date1, DateTime date2, int vessel_id)
        //{
        //    return DacProxy.GetAnchorageRecords(date1, date2, vessel_id);
        //}

        //public bool InsertUpdateAnchorage(string vesselId, string dateinfo, List<AnchorageTek> ancs)
        //{
        //    return DacProxy.InsertUpdateAnchorage(vesselId, dateinfo, ancs);
        //}

        #endregion





        #region VesselApprovalMonth
        public VesselApprovalMonth GetVesselApprovalMonth(int vesselId, DateTime approvalMonth)
        {
            return DacProxy.GetVesselApprovalMonth(vesselId, approvalMonth);
        }

        public bool InsertOrUpdateApprovalMonth(int vesselId, int crewId, DateTime approvalMonth)
        {
            return DacProxy.InsertOrUpdateApprovalMonth(vesselId, crewId, approvalMonth);
        }
        #endregion

        #region VesselApprovalDay
        public List<VesselApprovalDay> GetVesselApprovalDay(int vesselId, DateTime approvalDay)
        {
            return DacProxy.GetVesselApprovalDay(vesselId, approvalDay);
        }

        public int InsertOrUpdateApprovalDay(int vesselId, DateTime approvalDay, int crewId, List<int>approvedCrewIds)
        {
            return DacProxy.InsertOrUpdateApprovalDay(vesselId, approvalDay, crewId, approvedCrewIds);
        }

        public bool DeleteApprovalDay(int vesselId, DateTime approvalDay, int removeCrewId)
        {
            return DacProxy.DeleteApprovalDay(vesselId, approvalDay, removeCrewId);
        }
        #endregion



        #region SummaryTimes
        public List<SummaryTimes> GetSummaryTimes(int vesselId, DateTime summaryDate)
        {
            return DacProxy.GetSummaryTimes(vesselId, summaryDate);
        }

        public bool InsertOrUpdateSummaryTimes(int crewId, int vesselId, DateTime summaryDate, string allowanceTime)
        {
            return DacProxy.InsertOrUpdateSummaryTimes(crewId, vesselId, summaryDate, allowanceTime);
        }

        #endregion


    }
}
