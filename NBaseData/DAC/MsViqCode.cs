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
    [TableAttribute("MS_VIQ_CODE")]
    public class MsViqCode
    {
        #region データメンバ

        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("VIQ_CODE_ID", true)]
        public int ViqCodeID { get; set; }

        /// <summary>
        /// 名前ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("VIQ_CODE_NAME_ID")]
        public int ViqCodeNameID { get; set; }

        /// <summary>
        /// VersionID
        /// </summary>
        [DataMember]
        [ColumnAttribute("VIQ_VERSION_ID")]
        public int ViqVersionID { get; set; }

        /// <summary>
        /// コード
        /// </summary>
        [DataMember]
        [ColumnAttribute("VIQ_CODE")]
        public string ViqCode { get; set; }

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


        public MsViqCode()
        {
            ViqCodeID = 0;
            DeleteFlag = false;
        }

        public static List<MsViqCode> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsViqCode), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsViqCode), "OrderBy");

            List<MsViqCode> ret = new List<MsViqCode>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsViqCode> mapping = new MappingBase<MsViqCode>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<MsViqCode> SearchRecords(MsUser loginUser, int viqCodeNameID, string description)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsViqCode), "GetRecords");

            List<MsViqCode> ret = new List<MsViqCode>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsViqCode> mapping = new MappingBase<MsViqCode>();
            
            if (viqCodeNameID > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsViqCode), "SearchByNameID");
                Params.Add(new DBParameter("VIQ_CODE_NAME_ID", viqCodeNameID));
            }
            if (description != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsViqCode), "SearchByDescription");
                Params.Add(new DBParameter("DESCRIPTION", "%" + description + "%"));
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsViqCode), "OrderBy");

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

            Params.Add(new DBParameter("VIQ_CODE_NAME_ID", ViqCodeNameID));
            Params.Add(new DBParameter("VIQ_VERSION_ID", ViqVersionID));
            Params.Add(new DBParameter("VIQ_CODE", ViqCode));
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

            Params.Add(new DBParameter("VIQ_CODE_NAME_ID", ViqCodeNameID));
            Params.Add(new DBParameter("VIQ_VERSION_ID", ViqVersionID));
            Params.Add(new DBParameter("VIQ_CODE", ViqCode));
            Params.Add(new DBParameter("DESCRIPTION", Description));
            Params.Add(new DBParameter("DESCRIPTION_ENG", DescriptionEng));
            Params.Add(new DBParameter("ORDER_NO", OrderNo));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("UPDATE_MS_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("VIQ_CODE_ID", ViqCodeID));

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
            Params.Add(new DBParameter("PK", ViqCodeID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return ViqCodeID == 0;
        }


        public override string ToString()
        {
            return ViqCode;
        }
    }
}
