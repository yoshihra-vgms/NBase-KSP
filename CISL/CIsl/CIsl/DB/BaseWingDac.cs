using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;

using System.Reflection;
namespace CIsl.DB
{
    /// <summary>
    /// WingDBを使用するものの基底クラス　Select以外はしない方向性
    /// </summary>
    public abstract class BaseWingDac : DacBase
    {
        #region メンバ変数 Wing用に定義しなおし

        /// <summary>
        /// 削除フラグ
        /// </summary>
        public decimal delete_flag = EVal;


        /// <summary>
        /// 更新者
        /// </summary>
        public string renew_user_id = "";

        /// <summary>
        /// 更新日
        /// </summary>
        public DateTime renew_date = EDate;

        /// <summary>
        /// タイムスタンプ
        /// </summary>
        public string ts = "";
        #endregion
    }
}
