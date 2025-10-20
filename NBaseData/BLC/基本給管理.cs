using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using ORMapping;

namespace NBaseData.BLC
{
    public class 基本給管理
    {
        public static MsSiSalary BLC_基本給検索(MsUser loginUser, int kubun, DateTime start, DateTime end)
        {
            MsSiSalary ret = null;

            List<MsSiSalary> kyuyoList = MsSiSalary.SearchRecords(loginUser, kubun);

System.Diagnostics.Debug.WriteLine("Search Start = " + start.ToShortDateString() + " " + start.ToShortTimeString() + " , End = " + end.ToShortDateString() + " " + end.ToShortTimeString());

            //var tmpList1 = from l in kyuyoList
            //               where l.StartDate <= start
            //               orderby l.StartDate
            //               select l;
            var tmpList1 = kyuyoList.Where(obj => obj.StartDate <= start).OrderBy(obj => obj.StartDate);

            if (tmpList1.Count<MsSiSalary>() > 0)
            {

System.Diagnostics.Debug.WriteLine(" tmpList1========================");
foreach(MsSiSalary k in tmpList1)
{
    System.Diagnostics.Debug.WriteLine("id = " + k.MsSiSalaryID.ToString() + " , Start = " + k.StartDate.ToShortDateString() + " " + k.StartDate.ToShortTimeString() + " , End = " + k.EndDate.ToShortDateString() + " " + k.EndDate.ToShortTimeString());
}

                if (end != DateTime.MinValue)
                {
                    //var tmpList2 = from l in tmpList1
                    //               where l.EndDate >= end || l.EndDate == DateTime.MinValue
                    //               orderby l.StartDate
                    //               select l;
                    var tmpList2 = tmpList1.Where(obj => (DateTime.Parse(obj.EndDate.ToShortDateString()) >= DateTime.Parse(end.ToShortDateString()) || obj.EndDate == DateTime.MinValue)).OrderBy(obj => obj.StartDate);

if (tmpList2.Count<MsSiSalary>() > 0)
{
    System.Diagnostics.Debug.WriteLine(" tmpList2========================");
    foreach (MsSiSalary k in tmpList2)
    {
        System.Diagnostics.Debug.WriteLine("id = " + k.MsSiSalaryID.ToString() + " , Start = " + k.StartDate.ToShortDateString() + " " + k.StartDate.ToShortTimeString() + " , End = " + k.EndDate.ToShortDateString() + " " + k.EndDate.ToShortTimeString());
    }
}
                    if (tmpList2.Count<MsSiSalary>() > 0)
                    {
                        ret = tmpList2.First<MsSiSalary>();
                    }
                }
                else
                {
                    if (tmpList1.Count<MsSiSalary>() > 0)
                    {
                        ret = tmpList1.Last<MsSiSalary>();
                    }
                }
            }
            if (ret != null)
            {
                ret = BLC_基本給取得(loginUser, ret.MsSiSalaryID);
            }

            return ret;
        }


        public static MsSiSalary BLC_基本給取得(MsUser loginUser, int msSiSalaryId)
        {

            System.Diagnostics.Debug.WriteLine("Search msSiSalaryId = " + msSiSalaryId.ToString());

            MsSiSalary ret = MsSiSalary.GetRecord(loginUser, msSiSalaryId);
            if (ret == null)
            {
                ret = new MsSiSalary();
            }
            else
            {
                ret.SalaryHyoreiList = MsSiSalaryHyorei.GetRecords(loginUser, msSiSalaryId);
                ret.SalaryRankList = MsSiSalaryRank.GetRecords(loginUser, msSiSalaryId);
            }

            return ret;
        }

        public static int BLC_基本給登録(MsUser loginUser, MsSiSalary kyuyo)
        {
            int msSiSalaryId = -1;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    msSiSalaryId = MsSiSalary_InsertOrUpdate(dbConnect, loginUser, kyuyo);
                    if (msSiSalaryId > 0)
                    {
                        if (kyuyo.SalaryHyoreiList != null)
                        {
                            foreach (MsSiSalaryHyorei obj in kyuyo.SalaryHyoreiList)
                            {
                                obj.MsSiSalaryId = msSiSalaryId;
                                bool ret = MsSiSalaryHyorei_InsertOrUpdate(dbConnect, loginUser, obj);
                            }
                        }
                        if (kyuyo.SalaryRankList != null)
                        {
                            foreach (MsSiSalaryRank obj in kyuyo.SalaryRankList)
                            {
                                obj.MsSiSalaryId = msSiSalaryId;
                                bool ret = MsSiSalaryRank_InsertOrUpdate(dbConnect, loginUser, obj);
                            }
                        }

                    }
                    dbConnect.Commit();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    dbConnect.RollBack();
                }
            }
            return msSiSalaryId;
        }

        private static int MsSiSalary_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, MsSiSalary k)
        {
            k.RenewUserID = loginUser.MsUserID;
            k.RenewDate = DateTime.Now;

            if (k.IsNew())
            {
                if (k.InsertRecord(dbConnect, loginUser))
                {
                    return Sequences.GetSequenceId(dbConnect, loginUser, "SEQ_MS_SI_SALARY_ID");
                }
            }
            else
            {
                if (k.UpdateRecord(dbConnect, loginUser))
                {
                    return k.MsSiSalaryID;
                }
            }

            return -1;
        }


        private static bool MsSiSalaryHyorei_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, MsSiSalaryHyorei obj)
        {
            obj.RenewUserID = loginUser.MsUserID;
            obj.RenewDate = DateTime.Now;

            if (obj.IsNew())
            {
                bool ret = obj.InsertRecord(dbConnect, loginUser);
                obj.MsSiSalaryHyoreiId = Sequences.GetSequenceId(dbConnect, loginUser, "SEQ_MS_SI_SALARY_HYOREI_ID");

                return ret;
            }
            else
            {
                return obj.UpdateRecord(dbConnect, loginUser);
            }
        }

        private static bool MsSiSalaryRank_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, MsSiSalaryRank obj)
        {
            obj.RenewUserID = loginUser.MsUserID;
            obj.RenewDate = DateTime.Now;

            if (obj.IsNew())
            {
                bool ret = obj.InsertRecord(dbConnect, loginUser);
                obj.MsSiSalaryRankId = Sequences.GetSequenceId(dbConnect, loginUser, "SEQ_MS_SI_SALARY_RANK_ID");

                return ret;
            }
            else
            {
                return obj.UpdateRecord(dbConnect, loginUser);
            }
        }

        public static bool BLC_基本給複製(MsUser loginUser, MsSiSalary prevSalary, MsSiSalary newSalary)
        {
            bool ret = true;

            if (MsSiSalary_InsertOrUpdate(null, loginUser, prevSalary) == -1)
            {
                ret = false;
            }
            else
            {
                MsSiSalary work = BLC_基本給取得(loginUser, prevSalary.MsSiSalaryID);


                using (DBConnect dbConnect = new DBConnect())
                {
                    dbConnect.BeginTransaction();

                    try
                    {
                        int msSiSalaryId = MsSiSalary_InsertOrUpdate(dbConnect, loginUser, newSalary);

                        if (msSiSalaryId > 0)
                        {
                            if (work.SalaryHyoreiList != null)
                            {
                                foreach (MsSiSalaryHyorei obj in work.SalaryHyoreiList)
                                {
                                    MsSiSalaryHyorei newObj = new MsSiSalaryHyorei();

                                    newObj.MsSiSalaryId = msSiSalaryId;
                                    newObj.Hyorei = obj.Hyorei;
                                    newObj.Allowance = obj.Allowance;
                                }
                            }
                            if (work.SalaryRankList != null)
                            {
                                foreach (MsSiSalaryRank obj in work.SalaryRankList)
                                {
                                    MsSiSalaryRank newObj = new MsSiSalaryRank();

                                    newObj.MsSiSalaryId = msSiSalaryId;
                                    newObj.MsSiShokumeiSalaryId = obj.MsSiShokumeiSalaryId;
                                    newObj.Allowance0 = obj.Allowance0;
                                    newObj.Allowance1 = obj.Allowance1;
                                    newObj.Allowance2 = obj.Allowance2;
                                    newObj.Allowance3 = obj.Allowance3;
                                    newObj.Allowance4 = obj.Allowance4;
                                    newObj.Allowance5 = obj.Allowance5;
                                }
                            }

                            dbConnect.Commit();
                        }
                        else
                        {
                            ret = false;
                            dbConnect.RollBack();
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                        dbConnect.RollBack();
                    }
                }
            }



            return ret;
        }
    }
}
