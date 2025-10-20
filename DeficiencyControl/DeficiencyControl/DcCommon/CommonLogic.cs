using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIsl.DB.WingDAC;

namespace DcCommon
{
    /// <summary>
    /// 共通汎用ロジック
    /// </summary>
    public class CommonLogic
    {
        /// <summary>
        /// 年度開始の日を計算する
        /// </summary>
        /// <param name="year">対象年度、年ではない</param>
        /// <returns></returns>
        public static DateTime CalcuYearStart(int year)
        {
            DateTime ans = new DateTime(year, 4, 1);


            return ans;
        }

        /// <summary>
        /// 年度終了日を計算する
        /// </summary>
        /// <param name="year">対象年度、年ではない</param>
        /// <returns></returns>
        public static DateTime CalcuYearEnd(int year)
        {
            int y = year + 1;
            DateTime ans = new DateTime(y, 4, 1);
            ans = ans.AddMilliseconds(-1.0).Date;

            return ans;
        }


        /// <summary>
        /// 対象日の年度の始めの日を取得する。
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static DateTime CalcuYearStart(DateTime day)
        {
            //年をそろえる
            DateTime ydate = day.AddMonths(-3);


            DateTime ans = CalcuYearStart(ydate.Year);
            return ans;

        }


        /// <summary>
        /// 対象日の年度の終了日を取得する。
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static DateTime CalcuYearEnd(DateTime day)
        {
            //年をそろえるて、次の年の3月最終日が終了。
            DateTime ydate = day.AddMonths(-3);            

            //4月の一日前
            DateTime ans = CalcuYearEnd(ydate.Year);

            return ans;

        }


        /// <summary>
        /// 月日の表示順のリストを作成する・・・ようは4,5,6....11,12,1,2,3という風に並べた数字が欲しいだけ
        /// </summary>
        /// <returns></returns>
        public static List<int> CreateMonthOrder()
        {
            List<int> anslist = new List<int>();
            for (int i = 0; i < 12; i++)
            {
                int ans = ((i + 3) % 12) + 1;
                anslist.Add(ans);

            }

            return anslist;

        }



        /// <summary>
        /// 船の年齢を計算する
        /// </summary>
        /// <param name="date">日付</param>
        /// <param name="compdate">竣工日</param>
        /// <returns></returns>
        public static decimal CalcuVesselAge(DateTime date, DateTime compdate)
        {
            decimal ans = 0;

            //竣工日取得
            DateTime datebuild = compdate;
            if (datebuild == MsVessel.EDate)
            {
                return -1;
            }

            //差分を計算する
            TimeSpan sp = (date - datebuild);

            //この年数はうるう年を考慮していない。問題になったらなんとかすること
            ans = Convert.ToDecimal(sp.TotalDays / 365.0);


            return ans;
        }


        /// <summary>
        /// 船の平均年齢を算出
        /// </summary>
        /// <param name="date">基準日</param>        
        /// <param name="veslist">対象船</param>
        /// <returns></returns>
        public static decimal CalcuAverageVesselAge(DateTime date, List<MsVessel> veslist)
        {
            decimal aveage = 0;
            decimal vescount = 0;
            foreach (MsVessel ves in veslist)
            {
                //船齢の計算
                decimal age = DcCommon.CommonLogic.CalcuVesselAge(date, ves.completion_date);
                if (age < 0)
                {
                    //エラーなら年齢計算には含めない
                    continue;
                }

                //追加
                aveage += age;
                vescount += 1;

            }

            //平均の計算
            decimal ans = aveage / vescount;

            return ans;
        }

        /// <summary>
        /// 日付範囲チェック
        /// </summary>
        /// <param name="dt">対象日付（DateTime型）</param>
        /// <param name="from">範囲（開始）</param>
        /// <param name="to">範囲（終了）</param>
        /// <returns>結果</returns>
        public static bool IsRangeDateTime(DateTime dt, DateTime from, DateTime to)
        {
            return ((dt.CompareTo(from) >= 0) && (dt.CompareTo(to) <= 0));
        }
        

    }


    
    
}
