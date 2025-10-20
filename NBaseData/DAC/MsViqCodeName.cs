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
    [TableAttribute("MS_VIQ_CODE_NAME")]
    public class MsViqCodeName
    {
        #region データメンバ

        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("VIQ_CODE_NAME_ID", true)]
        public int ViqCodeNameID { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        [DataMember]
        [ColumnAttribute("VIQ_CODE_NAME")]
        public string ViqCodeName { get; set; }

        /// <summary>
        /// Code説明
        /// </summary>
        [DataMember]
        [ColumnAttribute("DESCRIPTION")]
        public string Description { get; set; }

        /// <summary>
        /// Code説明 英名
        /// </summary>
        [DataMember]
        [ColumnAttribute("DESCRIPTION_ENG")]
        public string DescriptionEng { get; set; }

        /// <summary>
        /// 表示順序
        /// </summary>
        [DataMember]
        [ColumnAttribute("ORDER_NO")]
        public int OrderNo { get; set; }

       
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


        public MsViqCodeName()
        {
            ViqCodeNameID = 0;
            DeleteFlag = false;
        }

        public static List<MsViqCodeName> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsViqCodeName), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsViqCodeName), "OrderBy");

            List<MsViqCodeName> ret = new List<MsViqCodeName>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsViqCodeName> mapping = new MappingBase<MsViqCodeName>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<MsViqCodeName> SearchRecords(MsUser loginUser, string description)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsViqCodeName), "GetRecords");

            List<MsViqCodeName> ret = new List<MsViqCodeName>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsViqCodeName> mapping = new MappingBase<MsViqCodeName>();

            if (description != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsViqCodeName), "SearchByDescription");
                Params.Add(new DBParameter("DESCRIPTION", "%" + description + "%"));
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsViqCodeName), "OrderBy");

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

            Params.Add(new DBParameter("VIQ_CODE_NAME", ViqCodeName));
            Params.Add(new DBParameter("DESCRIPTION", Description));
            Params.Add(new DBParameter("DESCRIPTION_ENG", DescriptionEng));
            Params.Add(new DBParameter("ORDER_NO", OrderNo));

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

            Params.Add(new DBParameter("VIQ_CODE_NAME", ViqCodeName));
            Params.Add(new DBParameter("DESCRIPTION", Description));
            Params.Add(new DBParameter("DESCRIPTION_ENG", DescriptionEng));
            Params.Add(new DBParameter("ORDER_NO", OrderNo));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("UPDATE_MS_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("VIQ_CODE_NAME_ID", ViqCodeNameID));

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
            Params.Add(new DBParameter("PK", ViqCodeNameID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return ViqCodeNameID == 0;
        }


        public override string ToString()
        {
            return ViqCodeName;
        }
    }
}
