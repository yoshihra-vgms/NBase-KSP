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
    [TableAttribute("MS_INSPECTION_CATEGORY")]
    public class MsInspectionCategory
    {
        #region データメンバ

        /// <summary>
        /// 検船種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("INSPECTION_CATEGORY_ID", true)]
        public int InspectionCategoryID { get; set; }

        /// <summary>
        /// 検船種別名
        /// </summary>
        [DataMember]
        [ColumnAttribute("INSPECTION_CATEGORY_NAME")]
        public string InspectionCategoryName { get; set; }

       
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


        public MsInspectionCategory()
        {
            InspectionCategoryID = 0;
            DeleteFlag = false;
        }

        public static List<MsInspectionCategory> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsInspectionCategory), MethodBase.GetCurrentMethod());

            List<MsInspectionCategory> ret = new List<MsInspectionCategory>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsInspectionCategory> mapping = new MappingBase<MsInspectionCategory>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<MsInspectionCategory> SearchRecords(MsUser loginUser, string inspectionCategoryName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsInspectionCategory), "GetRecords");

            List<MsInspectionCategory> ret = new List<MsInspectionCategory>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsInspectionCategory> mapping = new MappingBase<MsInspectionCategory>();

            if (inspectionCategoryName != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsInspectionCategory), "SearchByName");
                Params.Add(new DBParameter("INSPECTION_CATEGORY_NAME", "%" + inspectionCategoryName + "%"));
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

            Params.Add(new DBParameter("INSPECTION_CATEGORY_NAME", InspectionCategoryName));

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

            Params.Add(new DBParameter("INSPECTION_CATEGORY_NAME", InspectionCategoryName));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("UPDATE_MS_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("INSPECTION_CATEGORY_ID", InspectionCategoryID));

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
            Params.Add(new DBParameter("PK", InspectionCategoryID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return InspectionCategoryID == 0;
        }


        public override string ToString()
        {
            return InspectionCategoryName;
        }
    }
}
