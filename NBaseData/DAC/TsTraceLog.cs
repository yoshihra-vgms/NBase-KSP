using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using ORMapping.Attrs;
using ORMapping;
using SqlMapper;
using System.Reflection;
using ORMapping.Atts;
using NBaseData.DS;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("TS_TRACE_LOG")]
    public class TsTraceLog
    {
        #region データメンバ

        /// <summary>
        /// アクセス日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("ACCESS_DATE", true)]
        public DateTime AccessDate { get; set; }

        /// <summary>
        /// システム名
        /// </summary>
        [DataMember]
        [ColumnAttribute("SYSTEM")]
        public string System { get; set; }

        /// <summary>
        /// ユーザID
        /// </summary>
        [DataMember]
        [ColumnAttribute("USER_ID")]
        public string UserId { get; set; }

        /// <summary>
        /// 部門ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("BUMON_ID")]
        public string BumonId { get; set; }

        /// <summary>
        /// 機能名１
        /// </summary>
        [DataMember]
        [ColumnAttribute("FUNCTION1")]
        public string Function1 { get; set; }

        /// <summary>
        /// 機能名２
        /// </summary>
        [DataMember]
        [ColumnAttribute("FUNCTION2")]
        public string Function2 { get; set; }

        /// <summary>
        /// 機能名３
        /// </summary>
        [DataMember]
        [ColumnAttribute("FUNCTION3")]
        public string Function3 { get; set; }


        /// <summary>
        /// 開始日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("START_DATE")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 終了日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("END_DATE")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// コンピュータ名
        /// </summary>
        [DataMember]
        [ColumnAttribute("HOST_NAME")]
        public string HostName { get; set; }




        #endregion


        public TsTraceLog()
        {
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }

        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(TsTraceLog), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("ACCESS_DATE", AccessDate));
            Params.Add(new DBParameter("SYSTEM", System));
            Params.Add(new DBParameter("USER_ID", UserId));
            Params.Add(new DBParameter("BUMON_ID", BumonId));
            Params.Add(new DBParameter("FUNCTION1", Function1));
            Params.Add(new DBParameter("FUNCTION2", Function2));
            Params.Add(new DBParameter("FUNCTION3", Function3));

            Params.Add(new DBParameter("START_DATE", StartDate));
            //if (StartDate == DateTime.MinValue)
            //{
            //    Params.Add(new DBParameter("START_DATE", "NULL"));
            //}
            //else
            //{
            //    Params.Add(new DBParameter("START_DATE", StartDate));
            //}
            Params.Add(new DBParameter("END_DATE", EndDate));
            //if (EndDate == DateTime.MinValue)
            //{
            //    Params.Add(new DBParameter("END_DATE", "NULL"));
            //}
            //else
            //{
            //    Params.Add(new DBParameter("END_DATE", EndDate));
            //}
            Params.Add(new DBParameter("HOST_NAME", HostName));

            if (dbConnect == null)
            {
                cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            }
            else
            {
                cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            }
            if (cnt == 0)
                return false;
            return true;
        }

    }
}
