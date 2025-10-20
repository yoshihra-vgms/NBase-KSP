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
    [TableAttribute("SI_PRESENTATION_ITEM")]
    public class SiPresentaionItem
    {
        public static int ON_OFF_FLAG_ON = 0;
        public static int ON_OFF_FLAG_OFF = 1;
    
        #region データメンバ

        /// <summary>
        /// AdvancedSearchConditionItemID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_PRESENTATION_ITEM_ID", true)]
        public string SiPresentaionItemID { get; set; }

        /// <summary>
        /// AdvancedSearchConditionHeadID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_ADVANCED_SEARCH_CONDITION_HEAD_ID")]
        public string SiAdvancedSearchConditionHeadID { get; set; }

        /// <summary>
        /// 表示する、しない
        /// </summary>
        [DataMember]
        [ColumnAttribute("ON_OFF_FLAG")]
        public int OnOffFlag { get; set; }

        /// <summary>
        /// MsSiPresentaionItemID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_PRESENTAION_ITEM_ID")]
        public int MsSiPresentaionItemID { get; set; }
   
        /// <summary>
        /// 順番
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOW_ORDER")]
        public int ShowOrder { get; set; }



        #region 共通

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



        /// <summary>
        /// 表示項目セット番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("SET_NO")]
        public int SetNo { get; set; }

        /// <summary>
        /// 順序
        /// </summary>
        [DataMember]
        [ColumnAttribute("SN")]
        public int SN { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME")]
        public string Name { get; set; }

        #endregion



        public static List<SiPresentaionItem> GetRecords(MsUser loginUser)
        {
            return GetRecords(null, loginUser);
        }


        public static List<SiPresentaionItem> GetRecords(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiPresentaionItem), MethodBase.GetCurrentMethod());

            List<SiPresentaionItem> ret = new List<SiPresentaionItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiPresentaionItem> mapping = new MappingBase<SiPresentaionItem>();
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

            Params.Add(new DBParameter("SI_PRESENTATION_ITEM_ID", SiPresentaionItemID));
            Params.Add(new DBParameter("SI_ADVANCED_SEARCH_CONDITION_HEAD_ID", SiAdvancedSearchConditionHeadID));
            Params.Add(new DBParameter("MS_SI_PRESENTAION_ITEM_ID", MsSiPresentaionItemID));
            Params.Add(new DBParameter("ON_OFF_FLAG", OnOffFlag));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiPresentaionItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("SI_ADVANCED_SEARCH_CONDITION_HEAD_ID", SiAdvancedSearchConditionHeadID));
            Params.Add(new DBParameter("MS_SI_PRESENTAION_ITEM_ID", MsSiPresentaionItemID));
            Params.Add(new DBParameter("ON_OFF_FLAG", OnOffFlag));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_PRESENTATION_ITEM_ID", SiPresentaionItemID));
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
            Params.Add(new DBParameter("PK", SiPresentaionItemID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }
    }
}
