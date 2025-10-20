using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using NBaseData.DAC;
using NBaseData.BLC;
using NBaseUtil;
using NBaseCommon.Senin.Excel;
using NBaseData.DS;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        Dictionary<DateTime, List<SiCardPlanHead>> BLC_配乗計画_SearchRevision(MsUser loginUser, DateTime month, int vessel_kind);

        [OperationContract]
        Dictionary<DateTime, List<SiCardPlanHead>> BLC_配乗計画_SearchRevisions(MsUser loginUser,  DateTime date1, DateTime date2, int vessel_kind);

        [OperationContract]
        List<SiCardPlan> BLC_配乗計画_SearchPlan(MsUser loginUser, DateTime startdate,DateTime enddate, int vessel_kind);

        [OperationContract]
        List<SiCardPlan> BLC_配乗計画_SearchPlanByHeder(MsUser loginUser, SiCardPlanHead head, int vessel_kind);

        [OperationContract]
        MsSenin BLC_配乗計画_GetMsSenin(MsUser loginUser, string cardID, bool isPlan);

        [OperationContract]
        bool BLC_配乗計画_RevisionUp(MsUser loginUser, DateTime month, int vessel_kind);

        [OperationContract]
        bool BLC_配乗計画_Shime(MsUser loginUser, DateTime month, int vessel_kind);

        [OperationContract]
        string BLC_配乗計画_CheckValidate(MsUser loginUser, SiCardPlan plan, DateTime startdate, int pmstart, DateTime enddate, int pmend, int vessel_kind);

        [OperationContract]
        bool BLC_配乗計画_Check交代者予定(MsUser loginUser, string repracementID, MsSenin senin, DateTime startdate);

        [OperationContract]
        bool BLC_配乗計画_Is交代乗船(MsUser loginUser, string planID);

        [OperationContract]
        SiCardPlan BLC_配乗計画_InsertOrUpdate(MsUser loginUser, SiCardPlan plan, DateTime dt, int vessel_kind);

        [OperationContract]
        SiCardPlan BLC_配乗計画_Delete(MsUser loginUser, SiCardPlan plan);

        [OperationContract]
        byte[] BLC_Excel_配乗計画表出力(MsUser loginUser, DateTime date,  bool isPlan);

        [OperationContract]
        byte[] BLC_Excel_配乗計画表内航乗下船出力(MsUser loginUser, DateTime date, bool isPlan);

    }

    public partial class Service
    {
        public Dictionary<DateTime, List<SiCardPlanHead>> BLC_配乗計画_SearchRevision(MsUser loginUser, DateTime month, int vessel_kind)
        {
            return 配乗計画.BLC_配乗計画_SearchRevision(loginUser, month, vessel_kind);
        }

        public Dictionary<DateTime, List<SiCardPlanHead>> BLC_配乗計画_SearchRevisions(MsUser loginUser, DateTime date1, DateTime date2, int vessel_kind)
        {
            return 配乗計画.BLC_配乗計画_SearchRevisions(loginUser, date1, date2, vessel_kind);
        }

        public List<SiCardPlan> BLC_配乗計画_SearchPlan(MsUser loginUser, DateTime startdate, DateTime enddate, int kind)
        {
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            return 配乗計画.BLC_配乗計画_SearchPlan(loginUser, seninTableCache, startdate, enddate, kind);
        }

        public List<SiCardPlan> BLC_配乗計画_SearchPlanByHeder(MsUser loginUser, SiCardPlanHead head, int kind)
        {
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            return 配乗計画.BLC_配乗計画_SearchPlanByHeder(loginUser, seninTableCache, head, kind);
        }

        public MsSenin BLC_配乗計画_GetMsSenin( MsUser loginUser, string cardID, bool isPlan)
        {
            MsSenin ret = null;
            int seninID = -1;

            //計画
            if (isPlan)
            {
                SiCardPlan plan = SiCardPlan.GetRecord(loginUser, cardID);
                if (plan != null)
                {
                    seninID = plan.MsSeninID;
                }

            }
            else//実績
            {
                SiCard card = SiCard.GetRecord(loginUser, cardID);
                if (card != null)
                {
                    seninID = card.MsSeninID;
                }
            }
            //船員取得
            if (seninID != -1)
            {
                ret = MsSenin.GetRecord(loginUser, seninID);
            
            }

            return ret;

        }

        public bool BLC_配乗計画_RevisionUp(MsUser loginUser, DateTime month, int vessel_type)
        {
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            return 配乗計画.BLC_配乗計画_RevisionUp(loginUser, seninTableCache, month, vessel_type);
        }

        public bool BLC_配乗計画_Shime(MsUser loginUser, DateTime month, int vessel_type)
        { 
            return 配乗計画.BLC_配乗計画_Shime(loginUser, month, vessel_type);
        }

        public string BLC_配乗計画_CheckValidate(MsUser loginUser, SiCardPlan plan, DateTime startdate,int pmstart, DateTime enddate, int pmend, int vessel_type)
        {
            return 配乗計画.BLC_配乗計画_CheckValidate(loginUser, plan, startdate, pmstart, enddate, pmend, vessel_type);
        }

        public bool BLC_配乗計画_Check交代者予定(MsUser loginUser, string repracementID, MsSenin senin, DateTime startdate)
        {
            return 配乗計画.BLC_配乗計画_Check交代者予定(loginUser, repracementID, senin, startdate);
        }

        public bool BLC_配乗計画_Is交代乗船(MsUser loginUser, string planID)
        {
            return 配乗計画.BLC_配乗計画_Is交代乗船(loginUser, planID);
        }
        
        public SiCardPlan BLC_配乗計画_InsertOrUpdate(MsUser loginUser, SiCardPlan plan, DateTime dt, int vessel_kind)
        {
            return 配乗計画.BLC_配乗計画_InsertOrUpdate(loginUser, plan, dt, vessel_kind);
        }

        public SiCardPlan BLC_配乗計画_Delete(MsUser loginUser, SiCardPlan plan)
        {
            return 配乗計画.BLC_配乗計画_Delete(loginUser, plan);
        }

        public byte[] BLC_Excel_配乗計画表出力(MsUser loginUser, DateTime date,  bool isPlan)
        {
            string baseFileName = "";
                
            baseFileName = "配乗計画表";

            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            new 配乗計画表出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, date, isPlan);
            
            System.Threading.Thread.Sleep(2000);

            return FileUtils.ToBytes(outputFilePath);
        }

        public byte[] BLC_Excel_配乗計画表内航乗下船出力(MsUser loginUser, DateTime date, bool isPlan)
        {
            string baseFileName = "";

            //baseFileName = "配乗計画表内航乗下船";
            baseFileName = "配乗計画表長期";

            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            //new 配乗計画表内航乗下船出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, date, isPlan);
            new 配乗計画表出力長期(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, date, isPlan);

            System.Threading.Thread.Sleep(2000);

            return FileUtils.ToBytes(outputFilePath);
        }

    }
}