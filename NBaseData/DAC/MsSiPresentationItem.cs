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
    [TableAttribute("MS_SI_PRESENTATION_ITEM")]
    public class MsSiPresentationItem : ISyncTable
    {
        public static int ID_AGE = 15;

        public static int ID_SIGN_ON_RANK = 16;
        public static int ID_SIGN_ON = 17;
        public static int ID_SIGN_OFF = 18;
        public static int ID_VESSEL_ALL = 19;

        public static int ID_DAYS = 20;

        public static int ID_TYPE = 21;
        public static int ID_GRADE = 22;
        public static int ID_ISSUE_DATE = 23;
        public static int ID_EXPIRY_DATE = 24;

        public static int ID_INJURIES_KIND = 25;
        public static int ID_INJURIES_STATUS = 26;
        public static int ID_INJURIES_DATE = 27;

        public static int ID_YEARS_IN_OPERATOR = 28;
        public static int ID_YEARS_IN_RANK = 29;
        public static int ID_YEARS_OF_TANKER = 30;

        public static int ID_MEDICAL_KIND = 31;
        public static int ID_MEDICAL_CONSULTATION = 32;
        public static int ID_MEDICAL_EXPIRE = 33;
        public static int ID_MEDICAL_RESULT = 34;

        public static int ID_EXPERIENCE_CARGO = 35;

        public static int ID_TRAINING = 36;
        public static int ID_TRAINING_START = 37;
        public static int ID_TRAINING_END = 38;



        #region データメンバ

        /// <summary>
        /// AdvancedSearchConditionID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_PRESENTATION_ITEM_ID", true)]
        public int MsSiPresentationItemID { get; set; }


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


        public static List<MsSiPresentationItem> GetRecords(MsUser loginUser)
        {
            return GetRecords(null, loginUser);
        }


        public static List<MsSiPresentationItem> GetRecords(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiPresentationItem), MethodBase.GetCurrentMethod());
            SQL += " ORDER BY SET_NO, SN";

            List<MsSiPresentationItem> ret = new List<MsSiPresentationItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiPresentationItem> mapping = new MappingBase<MsSiPresentationItem>();
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
