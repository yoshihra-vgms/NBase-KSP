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
    [TableAttribute("MS_ACTION_CODE")]
    public class MsActionCode
    {
        #region データメンバ

        /// <summary>
        /// ActionコードID
        /// </summary>
        [DataMember]
        [ColumnAttribute("ACTION_CODE_ID", true)]
        public int ActionCodeID { get; set; }

        /// <summary>
        /// Actionコード名
        /// </summary>
        [DataMember]
        [ColumnAttribute("ACTION_CODE_NAME")]
        public string ActionCodeName { get; set; }

        /// <summary>
        /// Actionテキスト
        /// </summary>
        [DataMember]
        [ColumnAttribute("ACTION_CODE_TEXT")]
        public string ActionCodeText { get; set; }

       
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


        public MsActionCode()
        {
            ActionCodeID = 0;
            DeleteFlag = false;
        }

        public static List<MsActionCode> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsActionCode), MethodBase.GetCurrentMethod());

            List<MsActionCode> ret = new List<MsActionCode>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsActionCode> mapping = new MappingBase<MsActionCode>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<MsActionCode> SearchRecords(MsUser loginUser, string ActionCodeName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsActionCode), "GetRecords");

            List<MsActionCode> ret = new List<MsActionCode>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsActionCode> mapping = new MappingBase<MsActionCode>();

            if (ActionCodeName != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsActionCode), "SearchByName");
                Params.Add(new DBParameter("ACTION_CODE_NAME", "%" + ActionCodeName + "%"));
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

            Params.Add(new DBParameter("ACTION_CODE_NAME", ActionCodeName));
            Params.Add(new DBParameter("ACTION_CODE_TEXT", ActionCodeText));

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

            Params.Add(new DBParameter("ACTION_CODE_NAME", ActionCodeName));
            Params.Add(new DBParameter("ACTION_CODE_TEXT", ActionCodeText));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("UPDATE_MS_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("ACTION_CODE_ID", ActionCodeID));

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
            Params.Add(new DBParameter("PK", ActionCodeID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return ActionCodeID == 0;
        }


        public override string ToString()
        {
            return ActionCodeName;
        }
    }
}
