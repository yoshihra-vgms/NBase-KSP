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
    [TableAttribute("MS_DEFICIENCY_CATEGORY")]
    public class MsDeficiencyCategory
    {
        #region データメンバ

        /// <summary>
        /// DeficiencyカテゴリID
        /// </summary>
        [DataMember]
        [ColumnAttribute("DEFICIENCY_CATEGORY_ID", true)]
        public int DeficiencyCategoryID { get; set; }

        /// <summary>
        /// DeficiencyカテゴリNo
        /// </summary>
        [DataMember]
        [ColumnAttribute("DEFICIENCY_CATEGORY_NO")]
        public string DeficiencyCategoryNo { get; set; }

        /// <summary>
        /// Deficiencyカテゴリ名
        /// </summary>
        [DataMember]
        [ColumnAttribute("DEFICIENCY_CATEGORY_NAME")]
        public string DeficiencyCategoryName { get; set; }

       
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


        public MsDeficiencyCategory()
        {
            DeficiencyCategoryID = 0;
            DeleteFlag = false;
        }

        public static List<MsDeficiencyCategory> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDeficiencyCategory), MethodBase.GetCurrentMethod());

            List<MsDeficiencyCategory> ret = new List<MsDeficiencyCategory>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsDeficiencyCategory> mapping = new MappingBase<MsDeficiencyCategory>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<MsDeficiencyCategory> SearchRecords(MsUser loginUser, string deficiencyCategoryName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDeficiencyCategory), "GetRecords");

            List<MsDeficiencyCategory> ret = new List<MsDeficiencyCategory>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsDeficiencyCategory> mapping = new MappingBase<MsDeficiencyCategory>();

            if (deficiencyCategoryName != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDeficiencyCategory), "SearchByName");
                Params.Add(new DBParameter("DEFICIENCY_CATEGORY_NAME", "%" + deficiencyCategoryName + "%"));
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

            Params.Add(new DBParameter("DEFICIENCY_CATEGORY_NO", DeficiencyCategoryNo));
            Params.Add(new DBParameter("DEFICIENCY_CATEGORY_NAME", DeficiencyCategoryName));

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

            Params.Add(new DBParameter("DEFICIENCY_CATEGORY_NO", DeficiencyCategoryNo));
            Params.Add(new DBParameter("DEFICIENCY_CATEGORY_NAME", DeficiencyCategoryName));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("UPDATE_MS_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("DEFICIENCY_CATEGORY_ID", DeficiencyCategoryID));

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
            Params.Add(new DBParameter("PK", DeficiencyCategoryID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return DeficiencyCategoryID == 0;
        }


        public override string ToString()
        {
            return DeficiencyCategoryName;
        }
    }
}
