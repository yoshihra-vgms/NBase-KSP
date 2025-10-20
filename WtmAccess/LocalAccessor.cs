using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WtmModelBase;
using WtmModels;

namespace WtmData
{
    public class LocalAccessor : AccessorProxy
    {
        private string ConnectionKey { get; }
        private string TableKey { get; }


        public LocalAccessor(string connectionKey)
        {
            this.ConnectionKey = connectionKey;
        }


        public List<Work> GetWorks(DateTime date1, DateTime date2, int seninId = 0, int vesselId = 0)
        {
            //AccessorCommon.ConnectionKey = ConnectionKey;
            //return WtmDac.GetWorks(date1, date2, seninId, vesselId);

            AccessorCommon.ConnectionKey = ConnectionKey;
            var works = WtmDac.GetWorks(date1, date2, seninId, vesselId);
            //var crewNos = works.Select(o => o.CrewNo).Distinct();
            ////=======================================================================
            //var deleteList = new List<string>();
            //foreach (var crewNo in crewNos)
            //{
            //    var seninWorks = works.Where(o => o.CrewNo == crewNo).OrderBy(o => o.StartWork).ThenBy(o => o.FinishWork);
            //    foreach (var item in seninWorks)
            //    {
            //        var start = item.StartWork;
            //        var end = item.FinishWork;

            //        var query = seninWorks.Where(o => o.StartWork < end && o.FinishWork > start);
            //        foreach (var item2 in query)
            //        {
            //            if (item2 == item)
            //            { }
            //            else if (item.SquenceDate < item2.SquenceDate)
            //            {
            //                deleteList.Add(item.WorkID);
            //            }
            //            else
            //            {
            //                deleteList.Add(item2.WorkID);
            //            }
            //        }
            //    }

            //}
            //foreach (var item in deleteList)
            //{
            //    works.RemoveAll(o => o.WorkID.Equals(item));
            //}
            ////=======================================================================

            return works;
        }

        public Work GetBeInWork(int seninId)
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.GetBeInWork(seninId);
        }
        public List<Work> GetBeInWorks(int vesselId)
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.GetBeInWorks(vesselId);
        }


        public List<WorkSummary> GetWorkSummaries(DateTime date1, DateTime date2, int seninId = 0, int vesselId = 0)
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.GetWorkSummaries(date1, date2, seninId, vesselId);
        }




        public bool StartWork(int vesselId, int seninId, DateTime d)
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.StartWork(vesselId, seninId, d);
        }

        public bool InsertWork(Work w)
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.InsertWork(w);
        }

        public bool FinshWork(Work w)
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.FinshWork(w);
        }

        public bool DeleteWork(Work w)
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.EditWork(w, null);
        }

        public bool EditWork(Work delWork, Work editWork)
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.EditWork(delWork, editWork);
        }


        public bool CanCopyWork(Work w, List<int> seninIds, DateTime fromDay, DateTime toDay, out List<int> errorSeninIds)
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.CanCopyWork(w, seninIds, fromDay, toDay, out errorSeninIds);
        }


        public bool CopyWork(Work w, List<int> seninIds, DateTime fromDay, DateTime toDay)
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.CopyWork(w, seninIds, fromDay, toDay);
        }







        #region Setting
        public Setting GetSetting()
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.GetSetting();
        }
        #endregion

        #region WorkContent
        public List<WorkContent> GetWorkContents()
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.GetWorkContents();
        }
        #endregion



        #region RankCategory
        public List<RankCategory> GetRankCategories()
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.GetRankCategories();
        }
        #endregion

        #region Role
        public List<Role> GetRoles()
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.GetRoles();
        }
        #endregion




        #region VesselMovement
        public List<VesselMovement> GetVesselMovementDispRecord(DateTime date1, DateTime date2, int vesselid = 0)
        {
            //AccessorCommon.ConnectionKey = ConnectionKey;
            //return WtmDac.GetVesselMovementDispRecord(date1, date2, vesselid);

            AccessorCommon.ConnectionKey = ConnectionKey;
            var vesselMovements = WtmDac.GetVesselMovementDispRecord(date1, date2, vesselid);
            {
                foreach (var vm in vesselMovements)
                {
                    if (string.IsNullOrEmpty(vm.AnchorageS1) == false && string.IsNullOrEmpty(vm.AnchorageF1) == false)
                    {
                        if (vm.Anchorages == null)
                            vm.Anchorages = new List<Anchorage>();

                        var anc1 = new Anchorage();
                        anc1.Start = vm.AnchorageS1;
                        anc1.Finish = vm.AnchorageF1;

                        vm.Anchorages.Add(anc1);
                    }
                    if (string.IsNullOrEmpty(vm.AnchorageS2) == false && string.IsNullOrEmpty(vm.AnchorageF2) == false)
                    {
                        if (vm.Anchorages == null)
                            vm.Anchorages = new List<Anchorage>();

                        var anc2 = new Anchorage();
                        anc2.Start = vm.AnchorageS2;
                        anc2.Finish = vm.AnchorageF2;

                        vm.Anchorages.Add(anc2);
                    }
                }
            }
            return vesselMovements;
        }

        public bool InsertUpdateVesselMovement(VesselMovement vm)
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.InsertUpdateVesselMovement(vm);
        }

        #endregion


        #region  MovementInfo
        //public List<MovementInfoTek> GetMovementInfoRecord(DateTime date1, DateTime date2, int vessel_id)
        //{
        //    AccessorCommon.ConnectionKey = ConnectionKey;
        //    return WtmDac.GetMovementInfoRecord(date1, date2, vessel_id);
        //}

        //public bool InsertUpdateMovementInfo(string vesselid, string dateinfo, List<MovementInfoTek> mvis)
        //{
        //    AccessorCommon.ConnectionKey = ConnectionKey;
        //    return WtmDac.InsertUpdateMovementInfo(vesselid, dateinfo, mvis);
        //}
        #endregion


        #region Anchorage 

        //public List<AnchorageTek> GetAnchorageRecords(DateTime date1, DateTime date2, int vessel_id)
        //{
        //    AccessorCommon.ConnectionKey = ConnectionKey;
        //    return WtmDac.GetAnchorageRecords(date1, date2, vessel_id);
        //}

        //public bool InsertUpdateAnchorage(string vesselId, string dateinfo, List<AnchorageTek> ancs)
        //{
        //    AccessorCommon.ConnectionKey = ConnectionKey;
        //    return WtmDac.InsertUpdateAnchorage(vesselId, dateinfo, ancs);
        //}

        #endregion






        #region VesselApprovalMonth
        public VesselApprovalMonth GetVesselApprovalMonth(int vesselId, DateTime approvalMonth)
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.GetVesselApprovalMonth(vesselId, approvalMonth);
        }

        public bool InsertOrUpdateApprovalMonth(int vesselId, int crewId, DateTime approvalMonth)
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.InsertOrUpdateApprovalMonth(vesselId, crewId, approvalMonth);
        }
        #endregion

        #region VesselApprovalDay
        public List<VesselApprovalDay> GetVesselApprovalDay(int vesselId, DateTime approvalDay)
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.GetVesselApprovalDay(vesselId, approvalDay);
        }

        public int InsertOrUpdateApprovalDay(int vesselId, DateTime approvalDay, int crewId, List<int>approvedCrewIds)
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.InsertOrUpdateApprovalDay(vesselId, approvalDay, crewId, approvedCrewIds);
        }

        public bool DeleteApprovalDay(int vesselId, DateTime approvalDay, int removeCrewId)
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.DeleteApprovalDay(vesselId, approvalDay, removeCrewId);
        }
        #endregion



        #region SummaryTimes
        public List<SummaryTimes> GetSummaryTimes(int vesselId, DateTime summaryDate)
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.GetSummaryTimes(vesselId, summaryDate);
        }

        public bool InsertOrUpdateSummaryTimes(int crewId, int vesselId, DateTime summaryDate, string allowanceTime)
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.InsertOrUpdateSummaryTimes(crewId, vesselId, summaryDate, allowanceTime);
        }

        #endregion




        #region SyncTables
        public List<SyncTables> GetSyncTables()
        {
            AccessorCommon.ConnectionKey = ConnectionKey;
            return WtmDac.GetSyncTables();
        }
        #endregion


    }
}
