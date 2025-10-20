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
    [TableAttribute("MS_SI_ADVENCED_SEARCH_CONDITION")]
    public class MsSiAdvancedSearchCondition : ISyncTable
    {
        public static string COMPONENT_TYPE_COMBO = "Combo";
        public static string COMPONENT_TYPE_CHECKED_COMBO = "CheckedCombo";
        public static string COMPONENT_TYPE_TEXT = "Text";
        public static string COMPONENT_TYPE_TEXT_PANEL = "TextFromToPanel";
        public static string COMPONENT_TYPE_DATE_PANEL = "DateFromToPanel";


        public static int ID_BASIC = 1;
        public static int ID_SIGN_ON_OFF = 2;
        public static int ID_LICENSE = 3;
        public static int ID_INJURIES = 4;
        public static int ID_CREW_MATRIX = 5;
        public static int ID_MEDICAL = 6;
        public static int ID_EXPERIENCE_CARGO = 7;
        public static int ID_TRAINING = 8;

        public static int ID_RANK = 9;
        public static int ID_NAME = 10;
        public static int ID_AGE = 11;
        public static int ID_VESSEL = 12;
        public static int ID_VESSEL_ALL = 13;
        public static int ID_DAYS = 14;
        public static int ID_LICENSE_TYPE = 15;
        public static int ID_INJURIES_EXISTENCE = 16;
        public static int ID_YEARS_IN_RANK = 17;
        public static int ID_YEARS_OF_TANKER = 18;
        public static int ID_YEARS_IN_OPERATOR = 19;
        public static int ID_MEDICAL_KIND = 20;
        public static int ID_CARGO_GROUP = 21;
        public static int ID_TRAINING_NAME = 22;

        public static int ID_LICENSE_GRADE = 23;
        public static int ID_CONSULTATION_DATE = 24;
        public static int ID_MEDICAL_EXPIRE = 25;
        public static int ID_MEDICAL_RESULT = 26;

        public static int ID_LICENSE_EXISTENCE = 27;
        public static int ID_LICENSE_EXPIRE = 28;
        public static int ID_LICENSE_ISSUE = 29;


        #region データメンバ

        /// <summary>
        /// AdvancedSearchConditionID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_ADVANCED_SEARCH_CONDITION_ID", true)]
        public int MsSiAdvancedSearchConditionID { get; set; }
        
        /// <summary>
        /// 階層
        /// </summary>
        [DataMember]
        [ColumnAttribute("LAYER")]
        public int Layer { get; set; }
    
        /// <summary>
        /// 順序
        /// </summary>
        [DataMember]
        [ColumnAttribute("SN")]
        public int SN { get; set; }

        /// <summary>
        /// 親ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("PARENT_ID")]
        public int ParentID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 子供のコンポーネントタイプ
        /// </summary>
        [DataMember]
        [ColumnAttribute("CHILDREN_COMPONENT")]
        public string ChildrenComponent { get; set; }

        /// <summary>
        /// 値のコンポーネントタイプ
        /// </summary>
        [DataMember]
        [ColumnAttribute("VALUE_COMPONENT1")]
        public string ValueComponent1 { get; set; }

        /// <summary>
        /// 値のコンポーネントタイプ
        /// </summary>
        [DataMember]
        [ColumnAttribute("VALUE_COMPONENT2")]
        public string ValueComponent2 { get; set; }

        /// <summary>
        /// 表示項目セット番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("PRESENTATION_ITEM_SET_NO")]
        public int PresentationItemSetNo { get; set; }


        #region 共通項目

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


        #endregion


        public override string ToString()
        {
            return Name;
        }

        public static List<MsSiAdvancedSearchCondition> GetRecords(MsUser loginUser)
        {
            return GetRecords(null, loginUser);
        }


        public static List<MsSiAdvancedSearchCondition> GetRecords(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiAdvancedSearchCondition), MethodBase.GetCurrentMethod());
            SQL += " ORDER BY LAYER,PARENT_ID, SN";

            List<MsSiAdvancedSearchCondition> ret = new List<MsSiAdvancedSearchCondition>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiAdvancedSearchCondition> mapping = new MappingBase<MsSiAdvancedSearchCondition>();
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

        #region ISyncTable メンバ

        public bool InsertRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            return true;
        }

        public bool UpdateRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            return true;
        }

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            return true;
        }

        #endregion

    }
}
