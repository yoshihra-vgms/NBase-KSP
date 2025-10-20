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
    [TableAttribute("MS_ACCIDENT_KIND")]
    public class MsAccidentKind
    {
        #region データメンバ

        /// <summary>
        /// Accident種類ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("ACCIDENT_KIND_ID", true)]
        public int AccidentKindID { get; set; }

        /// <summary>
        /// Accident種類名
        /// </summary>
        [DataMember]
        [ColumnAttribute("ACCIDENT_KIND_NAME")]
        public string AccidentKindName { get; set; }

       
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


        public MsAccidentKind()
        {
            AccidentKindID = 0;
            DeleteFlag = false;
        }

        public static List<MsAccidentKind> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsAccidentKind), MethodBase.GetCurrentMethod());

            List<MsAccidentKind> ret = new List<MsAccidentKind>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsAccidentKind> mapping = new MappingBase<MsAccidentKind>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<MsAccidentKind> SearchRecords(MsUser loginUser, string accidentKindName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsAccidentKind), "GetRecords");

            List<MsAccidentKind> ret = new List<MsAccidentKind>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsAccidentKind> mapping = new MappingBase<MsAccidentKind>();

            if (accidentKindName != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsAccidentKind), "SearchByName");
                Params.Add(new DBParameter("ACCIDENT_KIND_NAME", "%" + accidentKindName + "%"));
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

            Params.Add(new DBParameter("ACCIDENT_KIND_NAME", AccidentKindName));

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

            Params.Add(new DBParameter("ACCIDENT_KIND_NAME", AccidentKindName));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("UPDATE_MS_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("ACCIDENT_KIND_ID", AccidentKindID));

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
            Params.Add(new DBParameter("PK", AccidentKindID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return AccidentKindID == 0;
        }


        public override string ToString()
        {
            return AccidentKindName;
        }
    }
}
