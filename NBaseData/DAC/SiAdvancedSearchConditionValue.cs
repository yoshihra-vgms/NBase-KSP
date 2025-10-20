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
    [TableAttribute("SI_ADVANCED_SEARCH_CONDITION_VALUE")]
    public class SiAdvancedSearchConditionValue
    {
        public static int ITEM_VALUE_FLAG_ITEM = 0;
        public static int ITEM_VALUE_FLAG_VALUE = 1;


        public static string VALUE_EXISTENCE_EXISTS = "有";
        public static string VALUE_EXISTENCE_NOT_EXISTS = "無";

        public static string VALUE_LICENSE_EXISTS = "取得済";
        public static string VALUE_LICENSE_NOT_EXISTS = "未取得";

        public static string VALUE_TRAINING_EXISTS = "受講済";
        public static string VALUE_TRAINING_NOT_EXISTS = "未受講";


        #region データメンバ

        /// <summary>
        /// AdvancedSearchConditionItemID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_ADVANCED_SEARCH_CONDITION_VALUE_ID", true)]
        public string SiAdvancedSearchConditionValueID { get; set; }

        /// <summary>
        /// AdvancedSearchConditionHeadID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_ADVANCED_SEARCH_CONDITION_HEAD_ID")]
        public string SiAdvancedSearchConditionHeadID { get; set; }


        /// <summary>
        /// AdvancedSearchConditionItemID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_ADVANCED_SEARCH_CONDITION_ITEM_ID")]
        public string SiAdvancedSearchConditionItemID { get; set; }

        /// <summary>
        /// And条件か、Or条件か
        /// </summary>
        [DataMember]
        [ColumnAttribute("ITEM_VALUE_FLAG")]
        public int ItemValueFlag { get; set; }

        /// <summary>
        /// ComponentType
        /// </summary>
        [DataMember]
        [ColumnAttribute("COMPONENT_TYPE")]
        public string ComponentType { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        [DataMember]
        [ColumnAttribute("VALUE")]
        public string Value { get; set; }
   
        /// <summary>
        /// 順番
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOW_ORDER")]
        public int ShowOrder { get; set; }
        
        
        /// <summary>
        /// 削除フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DELETE_FLAG")]
        public int DeleteFlag { get; set; }

        /// <summary>
        /// 同期:送信フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEND_FLAG")]
        public int SendFlag { get; set; }

        /// <summary>
        /// 同期:船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_ID")]
        public int VesselID { get; set; }

        /// <summary>
        /// 同期:データNO
        /// </summary>
        [DataMember]
        [ColumnAttribute("DATA_NO")]
        public Int64 DataNo { get; set; }

        /// <summary>
        /// 同期:ユーザキー
        /// </summary>
        [DataMember]
        [ColumnAttribute("USER_KEY")]
        public string UserKey { get; set; }

        /// <summary>
        /// 更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_DATE")]
        public DateTime RenewDate { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_USER_ID")]
        public string RenewUserID { get; set; }

        /// <summary>
        /// 排他制御
        /// </summary>
        [DataMember]
        [ColumnAttribute("TS")]
        public string Ts { get; set; }



        #endregion

        public bool IsNew()
        {
            return Ts == null ? true : false;
        }


        public static List<SiAdvancedSearchConditionValue> GetRecords(MsUser loginUser, string headId)
        {
            return GetRecords(null, loginUser, headId);
        }


        public static List<SiAdvancedSearchConditionValue> GetRecords(DBConnect dbConnect, MsUser loginUser, string headId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiAdvancedSearchConditionValue), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiAdvancedSearchConditionValue), "ByHeadId");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiAdvancedSearchConditionValue), "OrderBy");

            List<SiAdvancedSearchConditionValue> ret = new List<SiAdvancedSearchConditionValue>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SI_ADVANCED_SEARCH_CONDITION_HEAD_ID", headId));

            MappingBase<SiAdvancedSearchConditionValue> mapping = new MappingBase<SiAdvancedSearchConditionValue>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

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

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }

        public bool InsertRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("SI_ADVANCED_SEARCH_CONDITION_VALUE_ID", SiAdvancedSearchConditionValueID));

            Params.Add(new DBParameter("SI_ADVANCED_SEARCH_CONDITION_HEAD_ID", SiAdvancedSearchConditionHeadID));
            Params.Add(new DBParameter("SI_ADVANCED_SEARCH_CONDITION_ITEM_ID", SiAdvancedSearchConditionItemID));
            Params.Add(new DBParameter("ITEM_VALUE_FLAG", ItemValueFlag));
            Params.Add(new DBParameter("COMPONENT_TYPE", ComponentType));
            Params.Add(new DBParameter("VALUE", Value));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiAdvancedSearchConditionValue), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("SI_ADVANCED_SEARCH_CONDITION_HEAD_ID", SiAdvancedSearchConditionHeadID));
            Params.Add(new DBParameter("SI_ADVANCED_SEARCH_CONDITION_ITEM_ID", SiAdvancedSearchConditionItemID));
            Params.Add(new DBParameter("ITEM_VALUE_FLAG", ItemValueFlag));
            Params.Add(new DBParameter("COMPONENT_TYPE", ComponentType));
            Params.Add(new DBParameter("VALUE", Value));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_ADVANCED_SEARCH_CONDITION_VALUE_ID", SiAdvancedSearchConditionValueID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", SiAdvancedSearchConditionValueID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }



        public static bool DeleteRecords(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string headId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiAdvancedSearchConditionValue), MethodBase.GetCurrentMethod());

            int cnt = 0;

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("DELETE_FLAG", 1));
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("SI_ADVANCED_SEARCH_CONDITION_HEAD_ID", headId));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
    }
}
