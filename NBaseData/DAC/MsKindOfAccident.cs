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
    [TableAttribute("MS_KIND_OF_ACCIDENT")]
    public class MsKindOfAccident
    {
        #region データメンバ

        /// <summary>
        /// KindOfAccidentID
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIND_OF_ACCIDENT_ID", true)]
        public int KindOfAccidentID { get; set; }

        /// <summary>
        /// KindOfAccident名
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIND_OF_ACCIDENT_NAME")]
        public string KindOfAccidentName { get; set; }

       
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


        public MsKindOfAccident()
        {
            KindOfAccidentID = 0;
            DeleteFlag = false;
        }

        public static List<MsKindOfAccident> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKindOfAccident), MethodBase.GetCurrentMethod());

            List<MsKindOfAccident> ret = new List<MsKindOfAccident>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsKindOfAccident> mapping = new MappingBase<MsKindOfAccident>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<MsKindOfAccident> SearchRecords(MsUser loginUser, string kindOfAccidentName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKindOfAccident), "GetRecords");

            List<MsKindOfAccident> ret = new List<MsKindOfAccident>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsKindOfAccident> mapping = new MappingBase<MsKindOfAccident>();

            if (kindOfAccidentName != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKindOfAccident), "SearchByName");
                Params.Add(new DBParameter("KIND_OF_ACCIDENT_NAME", "%" + kindOfAccidentName + "%"));
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

            Params.Add(new DBParameter("KIND_OF_ACCIDENT_NAME", KindOfAccidentName));

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

            Params.Add(new DBParameter("KIND_OF_ACCIDENT_NAME", KindOfAccidentName));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("UPDATE_MS_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("KIND_OF_ACCIDENT_ID", KindOfAccidentID));

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
            Params.Add(new DBParameter("PK", KindOfAccidentID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return KindOfAccidentID == 0;
        }


        public override string ToString()
        {
            return KindOfAccidentName;
        }
    }
}
