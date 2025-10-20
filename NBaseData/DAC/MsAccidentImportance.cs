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
    [TableAttribute("MS_ACCIDENT_IMPORTANCE")]
    public class MsAccidentImportance
    {
        #region データメンバ

        /// <summary>
        /// AccidentImportanceID
        /// </summary>
        [DataMember]
        [ColumnAttribute("ACCIDENT_IMPORTANCE_ID", true)]
        public int AccidentImportanceID { get; set; }

        /// <summary>
        /// AccidentImportance名
        /// </summary>
        [DataMember]
        [ColumnAttribute("ACCIDENT_IMPORTANCE_NAME")]
        public string AccidentImportanceName { get; set; }

       
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


        public MsAccidentImportance()
        {
            AccidentImportanceID = 0;
            DeleteFlag = false;
        }

        public static List<MsAccidentImportance> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsAccidentImportance), MethodBase.GetCurrentMethod());

            List<MsAccidentImportance> ret = new List<MsAccidentImportance>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsAccidentImportance> mapping = new MappingBase<MsAccidentImportance>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<MsAccidentImportance> SearchRecords(MsUser loginUser, string accidentImportanceName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsAccidentImportance), "GetRecords");

            List<MsAccidentImportance> ret = new List<MsAccidentImportance>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsAccidentImportance> mapping = new MappingBase<MsAccidentImportance>();

            if (accidentImportanceName != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsAccidentImportance), "SearchByName");
                Params.Add(new DBParameter("ACCIDENT_IMPORTANCE_NAME", "%" + accidentImportanceName + "%"));
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

            Params.Add(new DBParameter("ACCIDENT_IMPORTANCE_NAME", AccidentImportanceName));

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

            Params.Add(new DBParameter("ACCIDENT_IMPORTANCE_NAME", AccidentImportanceName));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("UPDATE_MS_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("ACCIDENT_IMPORTANCE_ID", AccidentImportanceID));

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
            Params.Add(new DBParameter("PK", AccidentImportanceID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return AccidentImportanceID == 0;
        }


        public override string ToString()
        {
            return AccidentImportanceName;
        }
    }
}
