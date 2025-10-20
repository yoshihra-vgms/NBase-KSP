using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBaseCommon
{
    public class 配乗計画Func
    {
        static public TimeSpan 半日時間 = new TimeSpan(12, 0, 0);

        /// <summary>
        /// 開始日を登録用から表示用に変換
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        #region private DateTime 開始日_Obj2Disp(DateTime dt, int 半休)
        //static public DateTime 開始日_Obj2Disp(DateTime dt, int 半休)
        //{
        //    DateTime ret =dt;

        //    if (半休 == 1)
        //    {
        //        ret = ret + 半日時間;
        //    }
        //    return ret;
        //}
        #endregion

        /// <summary>
        /// 終了日を登録用から表示用に変換
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        #region private DateTime 終了日_Obj2Disp(DateTime dt, int 半休)
        static public DateTime 終了日_Obj2Disp(DateTime dt, int 半休)
        {
            DateTime ret = dt;
            if (半休 == 0)
            {
                ret = ret.AddDays(1);
            }
            
            return ret;
        }
        #endregion

        /// <summary>
        /// 終了日を表示用から登録用に変換
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        #region private DateTime 終了日_Disp2Obj(DateTime dt, int 半休)
        static public  DateTime 終了日_Disp2Obj(DateTime dt, int 半休)
        {
            DateTime ret = DateTime.MinValue;

            DateTime 日付のみ = dt.Date;
            TimeSpan 時刻のみ = dt.TimeOfDay;

            if (時刻のみ >= 半日時間)
            {
                ret = 日付のみ;

            }
            else
            {
                ret = 日付のみ.AddDays(-1);
            }
            return ret;
        }
        #endregion

        
    }
}
