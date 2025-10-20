using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DcCommon.DB.DAC;
using DcCommon;
using CIsl.DB.WingDAC;

namespace DeficiencyControl.Schedule.ListGrid
{
    /// <summary>
    /// 月ごとのデータ一式
    /// </summary>
    public class MonthVesselData
    {
        /// <summary>
        /// 今回表示する月
        /// </summary>
        public int Month = 0;

        /// <summary>
        /// 表示物一覧 船ID, これの月の予定
        /// </summary>
        public Dictionary<decimal, List<DcSchedulePlan>> VesselDic = new Dictionary<decimal, List<DcSchedulePlan>>();

        /// <summary>
        /// その他データ
        /// </summary>
        public List<DcScheduleOther> OtherList = new List<DcScheduleOther>();



        /// <summary>
        /// 今月のデータが一番多い場所を調べる 最低でも1
        /// </summary>
        /// <returns></returns>
        public int CalcuMaxDataCount()
        {
            int ans = 0;

            //データ数を計算
            {
                int md = -999;
                //予定
                foreach (List<DcSchedulePlan> plist in this.VesselDic.Values)
                {
                    if (md < plist.Count)
                    {
                        md = plist.Count;
                    }
                }

                //その他
                if (md < this.OtherList.Count)
                {
                    md = this.OtherList.Count;
                }



                //最低でも一つは確保する
                if (md <= 0)
                {
                    md = 1;
                }

                ans += md;

            }


            return ans;

        }



        /// <summary>
        /// 対象月の船ごとのデータを作成する
        /// </summary>
        /// <param name="month">月</param>
        /// <param name="plist">対象月の予定実績一覧</param>
        /// <param name="otlist">対象月のその他一覧</param>
        /// <param name="veslist">船一式</param>
        /// <returns></returns>
        private static MonthVesselData CreateMonthVesselData(int month, List<DcSchedulePlan> plist, List<DcScheduleOther> otlist, List<MsVessel> veslist)
        {
            MonthVesselData ans = new MonthVesselData();
            ans.Month = month;
            ans.OtherList = otlist; //その他はそのまま適応

            //船ごとのデータを作成する
            ans.VesselDic = new Dictionary<decimal, List<DcSchedulePlan>>();
            foreach (MsVessel ves in veslist)
            {
                //対象の船の予定実績を抽出
                var n = from f in plist where f.ms_vessel_id == ves.ms_vessel_id select f;
                List<DcSchedulePlan> planlist = n.ToList();

                //ADD
                ans.VesselDic.Add(ves.ms_vessel_id, planlist);
            }


            return ans;
        }



        /// <summary>
        /// 月ごとに船ごとに並び替えたデータを作成する・・・これはスケジュールグリッド用。
        /// </summary>
        /// <param name="plist">対象年度の予定実績一覧</param>
        /// <param name="otlist">対象年度のその他一覧</param>
        /// <param name="veslist">作成船一式</param>
        /// <returns></returns>
        public static Dictionary<int, MonthVesselData> CreateMonthVesselDataDic(int year, List<DcSchedulePlan> plist, List<DcScheduleOther> otlist, List<MsVessel> veslist)
        {
            Dictionary<int, MonthVesselData> ansdic = new Dictionary<int, MonthVesselData>();

            //月順を取得
            List<int> monthlist = CommonLogic.CreateMonthOrder();

            foreach (int month in monthlist)
            {
                //対象月のデータを抽出
                List<DcSchedulePlan> mplist = null;
                List<DcScheduleOther> molist = null;

                //年度を考慮した年にする
                int cyear = year;
                if (month < 4)
                {
                    cyear += 1;
                }

                //対象月のデータを抽出
                //予定実績
                var mp = from f in plist where f.estimate_date.Year == cyear && f.estimate_date.Month == month select f;
                mplist = mp.ToList();
                //日付順とする
                mplist.Sort((x, y) =>
                {
                    long a = (x.estimate_date.Date - y.estimate_date.Date).Ticks;
                    if (a < 0)
                    {
                        return -1;

                    } if (a > 0)
                    {
                        return 1;
                    } 
                    return 0;
                });

                //その他
                var mo = from f in otlist where f.estimate_date.Year == cyear && f.estimate_date.Month == month select f;
                molist = mo.ToList();
                molist.Sort((x, y) =>
                {
                    long a = (x.estimate_date.Date - y.estimate_date.Date).Ticks;
                    if (a < 0)
                    {
                        return -1;

                    } if (a > 0)
                    {
                        return 1;
                    }
                    return 0;
                });


                //作成とADD
                MonthVesselData vdata = CreateMonthVesselData(month, mplist, molist, veslist);
                ansdic.Add(month, vdata);
            }


            return ansdic;
        }
    }
}
