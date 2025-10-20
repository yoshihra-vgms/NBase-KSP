using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;

namespace WcfServiceDeficiencyControl.Output
{
    /// <summary>
    /// 帳票データ収集クラス基底
    /// </summary>
    public abstract class BaseOutputCollector
    {
        public BaseOutputCollector(NpgsqlConnection wing, NpgsqlConnection defcon)
        {
            this.WingCone = wing;
            this.DefCone = defcon;
        }

        /// <summary>
        /// wing DBへの接続
        /// </summary>
        protected NpgsqlConnection WingCone = null;

        /// <summary>
        /// Deficiency DBへの接続
        /// </summary>
        protected NpgsqlConnection DefCone = null;


    
    }
}