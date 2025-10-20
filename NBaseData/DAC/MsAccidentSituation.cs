using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping.Atts;
using NBaseData.DS;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using System.Runtime.Serialization;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("MS_ACCIDENT_SITUATION")]
    public class MsAccidentSituation
    {
        #region データメンバ

        /// <summary>
        /// 発生状況ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("ACCIDENT_SITUATION_ID", true)]
        public int AccidentSituationID { get; set; }

        /// <summary>
        /// 発生状況名
        /// </summary>
        [DataMember]
        [ColumnAttribute("ACCIDENT_SITUATION_NAME")]
        public string AccidentSituationName { get; set; }

       
        /// <summary>
        /// 削除フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DELETE_FLAG")]
        public bool DeleteFlag { get; set; }

        /// <summary>
        /// 作成者
        /// </summary>
        [DataMember]
        [ColumnAttribute("CREATE_MS_USER_ID")]
        public string CreateMsUserID { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        [DataMember]
        [ColumnAttribute("UPDATE_MS_USER_ID")]
        public string UpdateMsUserID { get; set; }

        /// <summary>
        /// 作成日
        /// </summary>
        [DataMember]
        [ColumnAttribute("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("UPDATE_DATE")]
        public DateTime UpdateDate { get; set; }

        #endregion


        public MsAccidentSituation()
        {
            AccidentSituationID = 0;
            DeleteFlag = false;
        }

        public static List<MsAccidentSituation> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsAccidentSituation), MethodBase.GetCurrentMethod());

            List<MsAccidentSituation> ret = new List<MsAccidentSituation>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsAccidentSituation> mapping = new MappingBase<MsAccidentSituation>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<MsAccidentSituation> SearchRecords(MsUser loginUser, string accidentSituationName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsAccidentSituation), "GetRecords");

            List<MsAccidentSituation> ret = new List<MsAccidentSituation>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsAccidentSituation> mapping = new MappingBase<MsAccidentSituation>();

            if (accidentSituationName != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsAccidentSituation), "SearchByName");
                Params.Add(new DBParameter("ACCIDENT_SITUATION_NAME", "%" + accidentSituationName + "%"));
            }

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }




        public bool InsertRecord(MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool UpdateRecord(MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }


        #region ISyncTable メンバ

        public bool InsertRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("ACCIDENT_SITUATION_NAME", AccidentSituationName));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("CREATE_MS_USER_ID", loginUser.MsUserID));
            Params.Add(new DBParameter("UPDATE_MS_USER_ID", loginUser.MsUserID));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool UpdateRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("ACCIDENT_SITUATION_NAME", AccidentSituationName));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("UPDATE_MS_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("ACCIDENT_SITUATION_ID", AccidentSituationID));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }


        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", AccidentSituationID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return AccidentSituationID == 0;
        }


        public override string ToString()
        {
            return AccidentSituationName;
        }
    }
}
