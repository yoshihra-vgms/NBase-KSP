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
    [TableAttribute("MS_VIQ_VERSION")]
    public class MsViqVersion
    {
        #region データメンバ

        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("VIQ_VERSION_ID", true)]
        public int ViqVersionID { get; set; }

        /// <summary>
        /// VIQVersion
        /// </summary>
        [DataMember]
        [ColumnAttribute("VIQ_VERSION")]
        public string ViqVersion { get; set; }

        /// <summary>
        /// 利用開始日
        /// </summary>
        [DataMember]
        [ColumnAttribute("START_DATE")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 利用終了日
        /// </summary>
        [DataMember]
        [ColumnAttribute("END_DATE")]
        public DateTime EndDate { get; set; }

       
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


        public MsViqVersion()
        {
            ViqVersionID = 0;
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MaxValue;
            DeleteFlag = false;
        }

        public static List<MsViqVersion> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsViqVersion), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsViqVersion), "OrderBy");

            List<MsViqVersion> ret = new List<MsViqVersion>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsViqVersion> mapping = new MappingBase<MsViqVersion>();
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

            Params.Add(new DBParameter("VIQ_VERSION", ViqVersion));
            Params.Add(new DBParameter("START_DATE", StartDate));
            Params.Add(new DBParameter("END_DATE", EndDate));

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

            Params.Add(new DBParameter("VIQ_VERSION", ViqVersion));
            Params.Add(new DBParameter("START_DATE", StartDate));
            Params.Add(new DBParameter("END_DATE", EndDate));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("UPDATE_MS_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("VIQ_VERSION_ID", ViqVersionID));

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
            Params.Add(new DBParameter("PK", ViqVersionID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return ViqVersionID == 0;
        }


        public override string ToString()
        {
            return ViqVersion;
        }
    }
}
