using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using ORMapping;
using NBaseData.DS;
using NBaseUtil;

namespace NBaseData.BLC
{

    public class 配乗計画
    {
        /// <summary>
        /// 対象月ヘッダを取得 何もなければカラをいれて返す
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="month">対象年月</param>
        /// <returns></returns>
        public static Dictionary<DateTime, List<SiCardPlanHead>> BLC_配乗計画_SearchRevision(MsUser loginUser, DateTime month, int vessel_kind)
        {
            Dictionary<DateTime, List<SiCardPlanHead>> rettbl = new Dictionary<DateTime, List<SiCardPlanHead>>();

            List<SiCardPlanHead> heads = SiCardPlanHead.GetRecordsByYearMonth(loginUser, month, vessel_kind);
            if (heads.Count == 0)
            {
                SiCardPlanHead h = new SiCardPlanHead();
                heads.Add(h);
            }
            rettbl.Add(month, heads);

            return rettbl;
        }

        /// <summary>
        /// 指定された範囲のヘッダを取得 何もなければカラをいれて返す
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="month">対象年月</param>
        /// <returns></returns>
        public static Dictionary<DateTime, List<SiCardPlanHead>> BLC_配乗計画_SearchRevisions(MsUser loginUser, DateTime date1, DateTime date2, int vessel_kind)
        {
            Dictionary<DateTime, List<SiCardPlanHead>> rettbl = new Dictionary<DateTime, List<SiCardPlanHead>>();

            DateTime dt = date1;
            while (dt <= date2)
            {
                if (dt == new DateTime(2023, 12, 1))
                {
                    int a = 0;
                }
                List<SiCardPlanHead> heads = SiCardPlanHead.GetRecordsByYearMonth(loginUser, dt, vessel_kind);
                if (heads.Count == 0)
                {
                    SiCardPlanHead h = new SiCardPlanHead();
                    heads.Add(h);
                }
                rettbl.Add(dt, heads);

                dt = dt.AddMonths(1);
            }

            return rettbl;
        }

        /// <summary>
        /// 期間で取得
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="kind"></param>
        public static List<SiCardPlan> BLC_配乗計画_SearchPlan(MsUser loginUser, SeninTableCache seninTableCache, DateTime startdate, DateTime enddate, int plan_type)
        {
            List<SiCardPlan> planlist = SiCardPlan.GetRecordsByStartEnd(loginUser, startdate,enddate, plan_type);
            List<SiCardPlan> retplans = GetPlanList(loginUser, seninTableCache, planlist, plan_type);

            return retplans;
        }

        /// <summary>
        /// ヘッダが一致するもの取得
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="head"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static List<SiCardPlan> BLC_配乗計画_SearchPlanByHeder(MsUser loginUser, SeninTableCache seninTableCache, SiCardPlanHead head, int plan_type)
        {
            List<SiCardPlan> planlist = SiCardPlan.GetRecordsByHead(loginUser, head);

            List<SiCardPlan> retplans = GetPlanList(loginUser, seninTableCache, planlist, plan_type);

            return retplans;
        }

        /// <summary>
        /// フェリー、内航にわける
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="seninTableCache"></param>
        /// <param name="planlist"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static List<SiCardPlan> GetPlanList(MsUser loginUser, SeninTableCache seninTableCache, List<SiCardPlan> planlist, int plan_type)
        {
            List<SiCardPlan> retlist = new List<SiCardPlan>();

            foreach (SiCardPlan plan in planlist)
            {
                if (plan.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船))
                {
                    MsVessel vessel = seninTableCache.GetMsVessel(loginUser, plan.MsVesselID);

                    if (vessel.IsPlanType(plan_type))
                    {
                        retlist.Add(plan);
                    }
                }
                else 
                {
                    retlist.Add(plan);
                }
            }

            return retlist;
        }


        /// <summary>
        /// リビジョンアップ
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static bool BLC_配乗計画_RevisionUp(MsUser loginUser, SeninTableCache seninTableCache, DateTime month, int plan_type)
        {
            bool ret = true;

            //最新リビジョン+1する
            List<SiCardPlanHead> heads = SiCardPlanHead.GetRecordsByYearMonth(loginUser, month, plan_type);

            SiCardPlanHead lasthead = heads.OrderBy(obj => obj.RevNo).Last();

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    SiCardPlanHead newhead = new SiCardPlanHead();
                    newhead.SiCardPlanHeadID = System.Guid.NewGuid().ToString();

                    newhead.RenewUserID = loginUser.MsUserID;
                    newhead.RenewDate = DateTime.Now;

                    newhead.YearMonth = month;
                    newhead.RevNo = lasthead.RevNo + 1;
                    newhead.ShimeFlag = 0;
                    newhead.VesselKind = plan_type;

                    //新しいリビジョン
                    ret = newhead.InsertRecord(dbConnect, loginUser);

                    //指定月planのIDのリスト
                    List<string> planIDList = new List<string>(); 
                    
                    int ret_flg = 0;
                    if (ret == true)
                    {
                        //SiCardplanを新しいリビジョンNoでコピー
                        List<SiCardPlan> plans = SiCardPlan.GetRecordsByHead(loginUser, lasthead);

                        foreach (SiCardPlan plan in plans)
                        {
                            //コピー元のplanIDを保持
                            planIDList.Add(plan.SiCardPlanID);

                            SiCardPlan newplan = plan.Clone();
                            
                            newplan.SiCardPlanID = System.Guid.NewGuid().ToString();

                            newplan.RenewUserID = loginUser.MsUserID;
                            newplan.RenewDate = DateTime.Now;

                            newplan.SiCardPlanHeadID = newhead.SiCardPlanHeadID;

                            ret = newplan.InsertRecord(dbConnect, loginUser);

                            if (ret == false)
                            {
                                ret_flg = 1;
                                break;
                            }
                        }
                    }

                    //新しいヘッダで計画を登録したときどこかでエラーの場合
                    if (ret_flg == 1) ret = false;

                    //内航なら指定月の次の月から計画と古いリビジョンを紐づける
                    if (ret == true && plan_type == MsPlanType.PlanTypeHarfPeriod)
                    {
                        DateTime date1 = month.AddMonths(1);//次の月から
                        DateTime date2 = month.AddMonths(6);//6か月

                        System.Diagnostics.Debug.WriteLine("date1 = " + date1.ToString() + "    date2 = " + date2.ToString());

                        //計画取得
                        List<SiCardPlan> plans = SiCardPlan.GetRecordsByStartEnd(loginUser, date1, date2, plan_type);
                        List<SiCardPlan> etcplans = GetPlanList(loginUser, seninTableCache, plans, plan_type);

                        ret_flg = 0;

                        //取得した計画をコピーし古いReivisionと紐づけ
                        foreach (SiCardPlan plan in etcplans)
                        {
                            //指定月のplanだった場合は既にコピー済
                            if (planIDList.Contains(plan.SiCardPlanID)) continue;
                            
                            SiCardPlan newp = plan.Clone();
                            
                            newp.SiCardPlanID = System.Guid.NewGuid().ToString();

                            newp.RenewUserID = loginUser.MsUserID;
                            newp.RenewDate = DateTime.Now;

                            newp.SiCardPlanHeadID = lasthead.SiCardPlanHeadID;
                            ret = newp.InsertRecord(dbConnect, loginUser);

                            if (ret == false)
                            {
                                ret_flg = 1;
                                break;
                            }
                        }

                        //取得した計画をコピーし古いReivisionと紐づけのどこかでエラー
                        if (ret_flg == 1) ret = false;
                    }

                    if (ret == true)
                    {
                        dbConnect.Commit();
                    }
                    else
                    {
                        dbConnect.RollBack();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    dbConnect.RollBack();
                    ret = false;
                }

            }

            return ret;
        }

        /// <summary>
        /// 月を締める
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static bool BLC_配乗計画_Shime(MsUser loginUser, DateTime month, int vessel_kind)
        {
            bool ret = true;

            //最後のリビジョン取得
            List<SiCardPlanHead> heads = SiCardPlanHead.GetRecordsByYearMonth(loginUser, month, vessel_kind);

            //更新　or 新規
            int upflg = 0;
            SiCardPlanHead lasthead = null;
            if (heads.Count > 0)
            {
                lasthead = heads.OrderBy(obj => obj.RevNo).Last();
                upflg = 1;
            }
            else
            {
                lasthead = new SiCardPlanHead();
                lasthead.SiCardPlanHeadID = System.Guid.NewGuid().ToString();

                lasthead.YearMonth = month;
                lasthead.RevNo = 0;

            }
            //締め
            lasthead.ShimeFlag = 1;

            lasthead.RenewUserID = loginUser.MsUserID;
            lasthead.RenewDate = DateTime.Now;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    if (upflg == 1)
                    {
                        //更新
                        ret = lasthead.UpdateRecord(dbConnect, loginUser);
                    }
                    else
                    {
                        //新規
                        ret = lasthead.InsertRecord(dbConnect, loginUser);
                    }

                    if (ret == true)
                    {
                        dbConnect.Commit();
                    }
                    else
                    {
                        dbConnect.RollBack();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    dbConnect.RollBack();

                }
            }

            return ret;
        }

        /// <summary>
        /// 登録前の日付チェック
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="plan">アポイント</param>
        /// <param name="startdate">DateTimePickerの開始日付</param>
        /// <param name="enddate">DateTimePickerの終了日付</param>
        /// <returns></returns>
        public static string BLC_配乗計画_CheckValidate(MsUser loginUser, SiCardPlan plan, DateTime startdate, int pmstart, DateTime enddate, int pmend, int vessel_kind)
        {
            TimeSpan 半日時間 = new TimeSpan(12, 0, 0);

            string ret = "";

            //最新リビジョンのヘッダを取得
            List<SiCardPlanHead> headList = SiCardPlanHead.GetRecordsOfLatestRev(loginUser, vessel_kind);

            //月締めチェック
            DateTime wkdate = new DateTime(startdate.Year, startdate.Month, 1);
            bool isExist = false;
            foreach (SiCardPlanHead head in headList)
            {
                if (head.YearMonth == wkdate)
                {
                    if (head.ShimeFlag == 1)
                    {
                        ret = "開始日は既に締め切られている月です。";
                    }
                    isExist = true;
                }
            }
            if (isExist == true && ret.Length>0)//開始日の月が締められていた
            {
                return ret;
            }

            //退職者のチェック
            MsSenin senin = MsSenin.GetRecord(loginUser, plan.MsSeninID);
            if (senin.RetireFlag == 1)
            {
                if (senin.RetireDate <= enddate)
                {
                    return "退職されています。(退職日：" + senin.RetireDate.ToString("yyyy/M/d") + ")";
                }
            }

            //船員で計画を取得
            List<SiCardPlan> seninPlans = SiCardPlan.GetRecordsBySenin(loginUser, plan.MsSeninID, vessel_kind);

            List<SiCardPlan> planList = new List<SiCardPlan>();
            foreach (SiCardPlanHead head in headList)
            {
                //ヘッダが該当する計画だけ取得する
                List<SiCardPlan> wklist = seninPlans.Where(obj => obj.SiCardPlanHeadID == head.SiCardPlanHeadID).ToList();
                if (wklist == null || wklist.Count == 0) continue;

                foreach (SiCardPlan p in wklist)
                {
                    //更新の場合を考慮する
                    if ((plan.SiCardPlanID != null && plan.SiCardPlanID.Length != 0) && p.SiCardPlanID == plan.SiCardPlanID)
                        continue;


                    planList.Add(p);
                }
            }



            //期間がかぶっていた場合のチェック
            var arg = planList.Where(obj => obj.LaborOnBoarding != (int)SiCardPlan.LABOR.半休 && obj.StartDate <= startdate && obj.EndDate > startdate);
            if (arg != null && arg.Count() > 0)
            {
                return "期間が重複しています。";
            }
            arg = planList.Where(obj => obj.LaborOnDisembarking != (int)SiCardPlan.LABOR.半休 && obj.StartDate < enddate && obj.EndDate >= enddate);
            if (arg != null && arg.Count() > 0)
            {
                return "期間が重複しています。";
            }

            //期間がまたがって重複の場合
            arg = planList.Where(obj => obj.StartDate >= startdate && obj.EndDate <= enddate);
            if (arg != null && arg.Count() > 0)
            {
                return "期間が重複しています。";
            }

            #region 日付の制限 前後の計画日取得
            DateTime startLimitDatetime = DateTime.MinValue; 
            DateTime endLimitDatetime = DateTime.MaxValue;
            if (planList.Count > 0 )
            {
                //開始日リミット
                List<SiCardPlan> wklistSt = planList.Where(obj => obj.EndDate <= startdate).ToList();
                if (wklistSt.Count() != 0)
                {
                    SiCardPlan wkp = wklistSt.OrderByDescending(obj => obj.EndDate).First();
                    startLimitDatetime = wkp.EndDate;
                    if (wkp.LaborOnDisembarking == (int)SiCardPlan.LABOR.半休)
                    {
                        startLimitDatetime = startLimitDatetime + new TimeSpan(11, 59, 59);
                    }
                    else
                    {
                        startLimitDatetime = startLimitDatetime + new TimeSpan(23, 59, 59);
                    }
                }

                //終了日リミット
                List<SiCardPlan> wklistEd = null;
                if (startLimitDatetime != DateTime.MinValue)
                {
                    //今見つかったレコードの次のレコードを探す
                    wklistEd = planList.Where(obj => obj.StartDate >= startLimitDatetime).ToList();
                }
                else
                {
                    // 登録予定の開始日より前のPLANがない場合、単純に終了予定より後に開始のあるレコードを探す
                    wklistEd = planList.Where(obj => obj.StartDate >= enddate).ToList();
                }

                if (wklistEd.Count() != 0)
                {
                    SiCardPlan wkp = wklistEd.OrderBy(obj => obj.StartDate).First();
                    endLimitDatetime = wkp.StartDate;
                    if (wkp.LaborOnBoarding == (int)SiCardPlan.LABOR.半休)
                    {
                        endLimitDatetime = endLimitDatetime + new TimeSpan(12, 0, 0);
                    }
                }

            }
            #endregion

            //比較する日付に時刻も加える
            DateTime startdatetime = startdate;
            if (pmstart == 1)
            {
                startdatetime = startdatetime + new TimeSpan(12, 0, 0);
            }

            DateTime enddatetime = enddate;
            if (pmend == 1)
            {
                enddatetime = enddatetime + new TimeSpan(11, 59, 59);
            }
            else
            {
                enddatetime = enddatetime + new TimeSpan(23, 59, 59);
            }

            string wkstr = "";
            if (startLimitDatetime != DateTime.MinValue )
            {
                if (startdatetime < startLimitDatetime)
                {
                    wkstr = startLimitDatetime.Date.ToShortDateString();
                    if (startLimitDatetime.TimeOfDay == new TimeSpan(11,59,59))
                    {
                        wkstr = wkstr + "の午後";
                    }
                    else
                    {
                        wkstr = startLimitDatetime.Date.AddDays(1).ToShortDateString();
                    }

                    ret = "開始日は" + wkstr + "以降を指定してください。";
                }
            }

            if (endLimitDatetime != DateTime.MaxValue)
            {
                int ng = 0;
                if (enddatetime > endLimitDatetime)
                {
                    wkstr = endLimitDatetime.Date.ToShortDateString();
                    if (endLimitDatetime.TimeOfDay == new TimeSpan(12, 00, 00))
                    {
                        wkstr = wkstr + "の午後";
                    }

                    if (ret.Length > 0)
                    {
                        ret = ret + Environment.NewLine;
                    }
                    ret = ret + "終了日は" + wkstr + "以前を指定してください。";
                } 
            }

            return ret;
        }

        public static bool BLC_配乗計画_Check交代者予定(MsUser loginUser, string repracementID, MsSenin senin, DateTime startdate)
        {
            bool ret = true;

            //交代者の予定を取得
            List<SiCardPlan> planlist = SiCardPlan.GetRecordsBySenin(loginUser, senin.MsSeninID, MsPlanType.PlanTypeHarfPeriod);

            //交代者の予定があった場合は日付でチェック
            if (planlist.Count > 0)
            {
                //指定日が含まれるレコードを検索
                if (planlist.Any(o => o.SiCardPlanID != repracementID && o.StartDate <= startdate && o.EndDate >= startdate))
                {
                    ret = false;
                }
            }

            return ret;
        }

        /// <summary>
        /// 対象の計画がほかのカードの交代なのかどうかを判定
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="plan"></param>
        /// <returns></returns>
        public static bool BLC_配乗計画_Is交代乗船(MsUser loginUser, string planID)
        {
            bool ret = true;
            SiCardPlan parentplan = SiCardPlan.GetRecordParent(loginUser, planID);
            
            if (parentplan == null)
            {
                ret =  false;
            }
            else
            {
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// 登録したり更新したり
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="plan"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static SiCardPlan BLC_配乗計画_InsertOrUpdate(MsUser loginUser, SiCardPlan plan, DateTime dt, int vessel_kind)
        {
            SiCardPlan retplan = null;
            SiCardPlan replacementPlan = null;

            // 交代者情報
            if (plan.ReplacementSeninID != 0)
            {
                // 元々交代者が設定されていた
                if (StringUtils.Empty(plan.Replacement) == false)
                {
                    replacementPlan = SiCardPlan.GetRecord(loginUser, plan.Replacement);
                    if (replacementPlan != null && replacementPlan.MsSeninID != plan.ReplacementSeninID)
                    {
                        // 交代者が変更されている場合
                        replacementPlan = null;
                    }
                }

                if (replacementPlan == null)
                {
                    // 元々交代者が設定されていなかった or 交代者が変更された
                    replacementPlan = new SiCardPlan();

                    replacementPlan.MsSeninID = plan.ReplacementSeninID;
                    replacementPlan.MsSiShubetsuID = plan.MsSiShubetsuID;
                    replacementPlan.MsVesselID = plan.MsVesselID;
                    replacementPlan.MsSiShokumeiID = plan.MsSiShokumeiID;
                    replacementPlan.MsSiShokumeiShousaiID = plan.MsSiShokumeiShousaiID;
                    replacementPlan.StartDate = plan.EndDate.AddDays(1);
                    replacementPlan.EndDate = replacementPlan.StartDate.AddDays(1);
                    replacementPlan.Replacement = "";
                    replacementPlan.MsBashoID = "";
                }
                else if (plan.LinkageReplacement)
                {
                    // 元々交代者が設定されていて連携指定されている
                    replacementPlan.StartDate = plan.EndDate.AddDays(1);
                    if (replacementPlan.EndDate <= replacementPlan.StartDate)
                    {
                        replacementPlan.EndDate = plan.StartDate.AddDays(1);
                    }
                }
                else
                {
                    replacementPlan = null;
                }

            }

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    // 交代者情報の処理
                    if (replacementPlan != null)
                    {
                        DateTime replacementDt = NBaseUtil.DateTimeUtils.ToFromMonth(replacementPlan.StartDate);

                        SiCardPlan retReplacementPlan = InnerBLC_配乗計画_InsertOrUpdate(dbConnect, loginUser, replacementPlan, replacementDt, vessel_kind);

                        plan.Replacement = retReplacementPlan.SiCardPlanID;
                    }

                    // 対象の配乗計画の処理
                    InnerBLC_配乗計画_InsertOrUpdate(dbConnect, loginUser, plan, dt, vessel_kind);

                    dbConnect.Commit();
                    retplan = SiCardPlan.GetRecord(loginUser, plan.SiCardPlanID);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    dbConnect.RollBack();
                }

            }
            return retplan;
        }

        public static SiCardPlan InnerBLC_配乗計画_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, SiCardPlan plan, DateTime dt, int vessel_kind)
        {
            try
            {
                //その月のヘッダ取得
                List<SiCardPlanHead> heads = SiCardPlanHead.GetRecordsByYearMonth(dbConnect, loginUser, dt, vessel_kind);

                plan.RenewUserID = loginUser.MsUserID;
                plan.RenewDate = DateTime.Now;

                bool ret = true;
                string planHeadID;

                //ヘッダが無い場合
                if (heads.Count() == 0)
                {
                    SiCardPlanHead head = new SiCardPlanHead();
                    head.SiCardPlanHeadID = System.Guid.NewGuid().ToString();

                    head.RenewUserID = loginUser.MsUserID;
                    head.RenewDate = DateTime.Now;

                    head.RevNo = 0;
                    head.YearMonth = dt;// NBaseUtil.DateTimeUtils.ToFromMonth(dt);
                    head.ShimeFlag = 0;

                    head.VesselKind = vessel_kind;

                    ret = head.InsertRecord(dbConnect, loginUser);

                    planHeadID = head.SiCardPlanHeadID;
                }
                else
                {
                    //最新リビジョン取得
                    planHeadID = heads.OrderBy(obj => obj.RevNo).Last().SiCardPlanHeadID;

                }

                if (ret == true)
                {

                    plan.SiCardPlanHeadID = planHeadID;

                    //ID
                    if (plan.IsNew())
                    {
                        plan.SiCardPlanID = System.Guid.NewGuid().ToString();

                        //新規登録
                        ret = plan.InsertRecord(dbConnect, loginUser);
                    }
                    else
                    {
                        //更新
                        ret = plan.UpdateRecord(dbConnect, loginUser);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return plan;
        }

        public static SiCardPlan BLC_配乗計画_Delete(MsUser loginUser, SiCardPlan plan)
        { 

            SiCardPlan retplan = null;
            bool result=true;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                plan.RenewUserID = loginUser.MsUserID;
                plan.RenewDate = DateTime.Now;

                try
                {
                    //交代親がいるなら取得する場合
                    SiCardPlan parent = SiCardPlan.GetRecordParent(loginUser, plan.SiCardPlanID);
                    if (parent != null)
                    {
                        //関係を消す
                        parent.Replacement = "";
                        parent.MsBashoID = "";

                        result =  parent.UpdateRecord(dbConnect, loginUser);
                    }
                    if (result == true)
                    {
                        //自分を消す
                        plan.RenewUserID = loginUser.MsUserID;
                        plan.RenewDate = DateTime.Now;
                        plan.DeleteFlag = 1;

                        result = plan.UpdateRecord(dbConnect, loginUser);

                    }

                    if (result == true)
                    {
                        dbConnect.Commit();
                        retplan = SiCardPlan.GetRecord(loginUser, plan.SiCardPlanID);
                    }
                    else
                    {
                        dbConnect.RollBack();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    dbConnect.RollBack();
                }
            }

                return retplan;
        }
    }
}
