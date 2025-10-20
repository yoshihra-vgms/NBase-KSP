using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DcCommon.DB;
using DcCommon.DB.DAC;
using CIsl.DB;

namespace DcCommon.Files
{
    /// <summary>
    /// コメント一覧ファイル用データ
    /// </summary>
    public class CommentListFileData : BaseCommentItemData
    {
        public int No = BaseDac.EVal;
        public decimal Vessel = BaseDac.EVal;
        public int VesselType = BaseDac.EVal;
        public int Kind = BaseDac.EVal;
        public DateTime Date = BaseDac.EDate;

        public string Port = "";
        public string Nation = "";
        public int MOU = BaseDac.EVal;        
        public string Remarks = "";

        public int FlagReporting = BaseDac.EVal;
        public int FlagReportingResult = BaseDac.EVal;
        
        public int RightShipReportResult = BaseDac.EVal;

        public int DeciencyCode = BaseDac.EVal;
        public int VIQCode = BaseDac.EVal;
        public int PIC = BaseDac.EVal;

        public int DeficiencyNO = BaseDac.EVal;
        public string Deficiency = "";
        public string CauseofDeficiency = "";
        public string CrectiveAction = "";
        public string PreventiveAction = "";
        public string ActionTakenByCompany = "";

        public int Importance = BaseDac.EVal;
        public DateTime DueDate = BaseDac.EDate;
        public string Convention = "";
        

        public string NonConformityNo = "";

        
        
        public string ClassInvolved = "";
        
        
        
        public string ComRemarks = "";
        public bool DPCheck = false;
        public DateTime DPCheckDate = BaseDac.EDate;
        public bool FileClose = false;
        public DateTime FileCloseDate = BaseDac.EDate;


        public List<DcActionCodeHistory> ActionCodeList = null;


    }
}
