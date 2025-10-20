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
    [TableAttribute("SI_ADVANCED_SEARCH_CONDITION_ITEM")]
    public class SiAdvancedSearchConditionItem
    {
        public static int AND_OR_FLAG_AND = 0;
        public static int AND_OR_FLAG_OR = 1;

        #region データメンバ

        /// <summary>
        /// AdvancedSearchConditionItemID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_ADVANCED_SEARCH_CONDITION_ITEM_ID", true)]
        public string SiAdvancedSearchConditionItemID { get; set; }

        /// <summary>
        /// AdvancedSearchConditionHeadID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_ADVANCED_SEARCH_CONDITION_HEAD_ID")]
        public string SiAdvancedSearchConditionHeadID { get; set; }

        /// <summary>
        /// And条件か、Or条件か
        /// </summary>
        [DataMember]
        [ColumnAttribute("AND_OR_FLAG")]
        public int AndOrFlag { get; set; }

        /// <summary>
        /// MsSiAdvancedSearchConditionID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_ADVANCED_SEARCH_CONDITION_ID")]
        public int MsSiAdvancedSearchConditionID { get; set; }
   
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


        /// <summary>
        /// 階層
        /// </summary>
        [DataMember]
        [ColumnAttribute("LAYER")]
        public int Layer { get; set; }


        #endregion

        public bool IsNew()
        {
            return Ts == null ? true : false;
        }


        public static List<SiAdvancedSearchConditionItem> GetRecords(MsUser loginUser, string headId)
        {
            return GetRecords(null, loginUser, headId);
        }


        public static List<SiAdvancedSearchConditionItem> GetRecords(DBConnect dbConnect, MsUser loginUser, string headId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiAdvancedSearchConditionItem), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiAdvancedSearchConditionItem), "ByHeadId");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiAdvancedSearchConditionItem), "OrderBy");

            List<SiAdvancedSearchConditionItem> ret = new List<SiAdvancedSearchConditionItem>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SI_ADVANCED_SEARCH_CONDITION_HEAD_ID", headId));

            MappingBase<SiAdvancedSearchConditionItem> mapping = new MappingBase<SiAdvancedSearchConditionItem>();
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

            Params.Add(new DBParameter("SI_ADVANCED_SEARCH_CONDITION_ITEM_ID", SiAdvancedSearchConditionItemID));

            Params.Add(new DBParameter("SI_ADVANCED_SEARCH_CONDITION_HEAD_ID", SiAdvancedSearchConditionHeadID));
            Params.Add(new DBParameter("AND_OR_FLAG", AndOrFlag));
            Params.Add(new DBParameter("MS_SI_ADVANCED_SEARCH_CONDITION_ID", MsSiAdvancedSearchConditionID));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiAdvancedSearchConditionItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("SI_ADVANCED_SEARCH_CONDITION_HEAD_ID", SiAdvancedSearchConditionHeadID));
            Params.Add(new DBParameter("AND_OR_FLAG", AndOrFlag));
            Params.Add(new DBParameter("MS_SI_ADVANCED_SEARCH_CONDITION_ID", MsSiAdvancedSearchConditionID));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_ADVANCED_SEARCH_CONDITION_ITEM_ID", SiAdvancedSearchConditionItemID));
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
            Params.Add(new DBParameter("PK", SiAdvancedSearchConditionItemID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }


        public static bool DeleteRecords(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string headId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiAdvancedSearchConditionItem), MethodBase.GetCurrentMethod());

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
