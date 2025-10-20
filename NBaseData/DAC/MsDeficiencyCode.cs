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
    [TableAttribute("MS_DEFICIENCY_CODE")]
    public class MsDeficiencyCode
    {
        #region データメンバ

        /// <summary>
        /// DeficiencyCodeID
        /// </summary>
        [DataMember]
        [ColumnAttribute("DEFICIENCY_CODE_ID", true)]
        public int DeficiencyCodeID { get; set; }
        
        /// <summary>
        /// DeficiencyCode名
        /// </summary>
        [DataMember]
        [ColumnAttribute("DEFICIENCY_CODE_NAME")]
        public string DeficiencyCodeName { get; set; }
        
        /// <summary>
        /// Defective Item
        /// </summary>
        [DataMember]
        [ColumnAttribute("DEFECTIVE_ITEM")]
        public string DefectiveItem { get; set; }

        /// <summary>
        /// DeficiencyカテゴリID
        /// </summary>
        [DataMember]
        [ColumnAttribute("DEFICIENCY_CATEGORY_ID")]
        public int DeficiencyCategoryID { get; set; }


       
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


        public MsDeficiencyCode()
        {
            DeficiencyCodeID = 0;
            DeleteFlag = false;
        }

        public static List<MsDeficiencyCode> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDeficiencyCode), MethodBase.GetCurrentMethod());

            List<MsDeficiencyCode> ret = new List<MsDeficiencyCode>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsDeficiencyCode> mapping = new MappingBase<MsDeficiencyCode>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<MsDeficiencyCode> SearchRecords(MsUser loginUser, int deficiencyCategoryID, string deficiencyCodeName, string defectiveItem)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDeficiencyCode), "GetRecords");

            List<MsDeficiencyCode> ret = new List<MsDeficiencyCode>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsDeficiencyCode> mapping = new MappingBase<MsDeficiencyCode>();

            if (deficiencyCategoryID > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDeficiencyCode), "SearchByCategoryID");
                Params.Add(new DBParameter("DEFICIENCY_CATEGORY_ID", deficiencyCategoryID));
            }
            if (deficiencyCodeName != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDeficiencyCode), "SearchByName");
                Params.Add(new DBParameter("DEFICIENCY_CODE_NAME", "%" + deficiencyCodeName + "%"));
            }
            if (defectiveItem != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDeficiencyCode), "SearchByDefectiveItem");
                Params.Add(new DBParameter("DEFECTIVE_ITEM", "%" + defectiveItem + "%"));
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

            Params.Add(new DBParameter("DEFICIENCY_CODE_NAME", DeficiencyCodeName));
            Params.Add(new DBParameter("DEFECTIVE_ITEM", DefectiveItem));
            Params.Add(new DBParameter("DEFICIENCY_CATEGORY_ID", DeficiencyCategoryID));

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
            
            Params.Add(new DBParameter("DEFICIENCY_CODE_NAME", DeficiencyCodeName));
            Params.Add(new DBParameter("DEFECTIVE_ITEM", DefectiveItem));
            Params.Add(new DBParameter("DEFICIENCY_CATEGORY_ID", DeficiencyCategoryID));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("UPDATE_MS_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("DEFICIENCY_CODE_ID", DeficiencyCodeID));

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
            Params.Add(new DBParameter("PK", DeficiencyCodeID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return DeficiencyCodeID == 0;
        }


        public override string ToString()
        {
            return DeficiencyCodeName;
        }
    }
}
