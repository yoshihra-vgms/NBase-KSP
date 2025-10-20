using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using ORMapping.Attrs;
using ORMapping;
using SqlMapper;
using System.Reflection;
using NBaseData.DS;
using ORMapping.Atts;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("MS_USER")]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class MsUser : ISyncTable
    {
        public enum ADMIN_FLAG { 通常ユーザ, 管理者 };
        public enum USER_KBN { 事務所, 船員 };

        #region データメンバ
        [DataMember]
        [ColumnAttribute("MS_USER_ID", true)]
        public string MsUserID { get; set; }

        [DataMember]
        [ColumnAttribute("USER_KBN")]
        public int UserKbn { get; set; }

        [DataMember]
        [ColumnAttribute("LOGIN_ID")]
        public string LoginID { get; set; }

        [DataMember]
        [ColumnAttribute("PASSWORD")]
        public string Password { get; set; }

        [DataMember]
        [ColumnAttribute("MAIL_ADDRESS")]
        public string MailAddress { get; set; }

        /// <summary>
        /// 姓
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEI")]
        public string Sei { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MEI")]
        public string Mei { get; set; }

        /// <summary>
        /// 姓（カナ）
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEI_KANA")]
        public string SeiKana { get; set; }

        /// <summary>
        /// 名（カナ）
        /// </summary>
        [DataMember]
        [ColumnAttribute("MEI_KANA")]
        public string MeiKana { get; set; }

        /// <summary>
        /// 性別
        /// Int.MinValue→不明
        /// 0→男
        /// 1→女
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEX")]
        public int Sex { get; set; }

        [DataMember]
        [ColumnAttribute("ADMIN_FLAG")]
        public int AdminFlag { get; set; }



        [DataMember]
        [ColumnAttribute("DOC_FLAG_CEO")]
        public int DocFlag_CEO { get; set; }
        
        [DataMember]
        [ColumnAttribute("DOC_FLAG_ADMIN")]
        public int DocFlag_Admin { get; set; }
       
        [DataMember]
        [ColumnAttribute("DOC_FLAG_MSI_FERRY")]
        public int DocFlag_MsiFerry { get; set; }
        
        [DataMember]
        [ColumnAttribute("DOC_FLAG_CREW_FERRY")]
        public int DocFlag_CrewFerry { get; set; }
        
        [DataMember]
        [ColumnAttribute("DOC_FLAG_TSI_FERRY")]
        public int DocFlag_TsiFerry { get; set; }

        //[DataMember]
        //[ColumnAttribute("DOC_FLAG_MSI_CARGO")]
        //public int DocFlag_MsiCargo { get; set; }

        //[DataMember]
        //[ColumnAttribute("DOC_FLAG_CREW_CARGO")]
        //public int DocFlag_CrewCargo { get; set; }

        //[DataMember]
        //[ColumnAttribute("DOC_FLAG_TSI_CARGO")]
        //public int DocFlag_TsiCargo { get; set; }

        [DataMember]
        [ColumnAttribute("DOC_FLAG_OFFICER")]
        public int DocFlag_Officer { get; set; }

        [DataMember]
        [ColumnAttribute("DOC_FLAG_GL")]
        public int DocFlag_GL { get; set; }

        [DataMember]
        [ColumnAttribute("DOC_FLAG_TL")]
        public int DocFlag_TL { get; set; }

        //[DataMember]
        //[ColumnAttribute("DOC_FLAG_SD_MANAGER")]
        //public int DocFlag_SdManager { get; set; }



        [DataMember]
        [ColumnAttribute("DELETE_FLAG")]
        public int DeleteFlag { get; set; }


        [DataMember]
        [ColumnAttribute("SEND_FLAG")]
        public int SendFlag { get; set; }

        [DataMember]
        [ColumnAttribute("VESSEL_ID")]
        public int VesselID { get; set; }

        [DataMember]
        [ColumnAttribute("DATA_NO")]
        public Int64 DataNo { get; set; }

        /// <summary>
        /// 同期:ユーザキー
        /// </summary>
        [DataMember]
        [ColumnAttribute("USER_KEY")]
        public string UserKey { get; set; }

        [DataMember]
        [ColumnAttribute("RENEW_DATE")]
        public DateTime RenewDate { get; set; }

        [DataMember]
        [ColumnAttribute("RENEW_USER_ID")]
        public String RenewUserID { get; set; }

        [DataMember]
        [ColumnAttribute("TS")]
        public string Ts { get; set; }


        /// <summary>
        /// MsUserBumon → 部門名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BUMON_BUMON_NAME")]
        public string BumonName { get; set; }

        /// <summary>
        /// MsUserBumon → 部門ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BUMON_BUMON_ID")]
        public string BumonID { get; set; }


        public string FullName
        {
            get
            {
                return Sei + " " + Mei;
            }
        }
        public string FullNameKana
        {
            get
            {
                return SeiKana + " " + MeiKana;
            }
        }

        //public int UserFlag
        //{
        //    get
        //    {
        //        int flg = NBaseData.DS.DocConstants.UserFlag一般;

        //        if (DocFlag_CEO == 1)
        //            return (int)NBaseData.DS.DocConstants.UserFlagEnum.経営責任者;
        //        else if (DocFlag_Admin == 1)
        //            return (int)NBaseData.DS.DocConstants.UserFlagEnum.管理責任者;
        //        else if (DocFlag_MsiFerry == 1)
        //            return (int)NBaseData.DS.DocConstants.UserFlagEnum.海務監督;
        //        else if (DocFlag_CrewFerry == 1)
        //            return (int)NBaseData.DS.DocConstants.UserFlagEnum.船員担当者;
        //        else if (DocFlag_TsiFerry == 1)
        //            return (int)NBaseData.DS.DocConstants.UserFlagEnum.工務監督;
        //        else if (DocFlag_Officer == 1)
        //            return (int)NBaseData.DS.DocConstants.UserFlagEnum.役員;
        //        else if (DocFlag_GL == 1)
        //            return (int)NBaseData.DS.DocConstants.UserFlagEnum.ＧＬ;
        //        else if (DocFlag_TL == 1)
        //            return (int)NBaseData.DS.DocConstants.UserFlagEnum.ＴＬ;


        //        return flg;
        //    }
        //}


        public enum LOGIN_STATUS
        {
            正常,
            パスワード有効期限切れ間近,
            パスワード有効期限切れ
        };

        [DataMember]
        public LOGIN_STATUS LoginStatus = LOGIN_STATUS.正常;

        #endregion

        #region セッションキー
        [DataMember]
        public string SessionKey { get; set; }

        #endregion

        public override string ToString()
        {
            return FullName;
        }

        public MsUser()
        {
            DocFlag_CEO = 0;
            DocFlag_Admin = 0;
            DocFlag_MsiFerry = 0;
            DocFlag_CrewFerry = 0;
            DocFlag_TsiFerry = 0;
            DocFlag_Officer = 0;
            DocFlag_GL = 0;
            DocFlag_TL = 0;

            //DocFlag_MsiCargo = 0;
            //DocFlag_CrewCargo = 0;
            //DocFlag_TsiCargo = 0;
            //DocFlag_SdManager = 0;
        }

        public static List<MsUser> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsUser), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsUser), "OrderBy");
            List<MsUser> ret = new List<MsUser>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsUser> mapping = new MappingBase<MsUser>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsUser> GetAllRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsUser), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsUser), "OrderBy");
            List<MsUser> ret = new List<MsUser>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsUser> mapping = new MappingBase<MsUser>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }



        public static List<MsUser> GetRecordsByUserKbn事務所(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsUser), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsUser), "ByUserKbn事務所");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsUser), "OrderBy");
            List<MsUser> ret = new List<MsUser>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsUser> mapping = new MappingBase<MsUser>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsUser> GetRecordsByUserKbn船員(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsUser), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsUser), "ByUserKbn船員");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsUser), "OrderBy");
            List<MsUser> ret = new List<MsUser>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsUser> mapping = new MappingBase<MsUser>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsUser GetRecordsByUserID(MsUser loginUser, string msUserID)
        {
            return GetRecordsByUserID(null, loginUser, msUserID);
        }

        public static MsUser GetRecordsByUserID(DBConnect dbConnect, MsUser loginUser, string msUserID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsUser), MethodBase.GetCurrentMethod());
            List<MsUser> ret = new List<MsUser>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsUser> mapping = new MappingBase<MsUser>();
            Params.Add(new DBParameter("MS_USER_ID", msUserID));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static MsUser GetRecordsByLoginIDPassword(string loginID, string password)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsUser), MethodBase.GetCurrentMethod());
            List<MsUser> ret = new List<MsUser>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsUser> mapping = new MappingBase<MsUser>();
            Params.Add(new DBParameter("LOGIN_ID", loginID));
            Params.Add(new DBParameter("PASSWORD", password));

            ret = mapping.FillRecrods("", SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static MsUser GetRecordsByLoginID(MsUser loginUser, string loginID)
        {
            return GetRecordsByLoginID(null, loginUser, loginID);
        }
        public static MsUser GetRecordsByLoginID(DBConnect dbConnect, MsUser loginUser, string loginID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsUser), MethodBase.GetCurrentMethod());
            List<MsUser> ret = new List<MsUser>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsUser> mapping = new MappingBase<MsUser>();
            Params.Add(new DBParameter("LOGIN_ID", loginID));

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<MsUser> SearchRecords(MsUser loginUser, string loginID, int kbnID, int adminFlag, string bumonID, string sei, string mei)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsUser), MethodBase.GetCurrentMethod());
            List<MsUser> ret = new List<MsUser>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsUser> mapping = new MappingBase<MsUser>();
           
            if (bumonID != "-1" && bumonID != "")
            {
                SQL = SQL.Replace("#INNER_JOIN_STR#", "  and MS_USER_BUMON.MS_BUMON_ID = :BUMON_ID");
                Params.Add(new DBParameter("BUMON_ID", bumonID));
            }
            else
            {
                SQL = SQL.Replace("#INNER_JOIN_STR#", "");
            }
            if (loginID != "")
            {
                SQL += " and LOGIN_ID LIKE :LOGIN_ID";
                Params.Add(new DBParameter("LOGIN_ID", "%" + loginID + "%"));
            }
            if (kbnID != 2) // 2 は、すべてを対象とする
            {
                SQL += " and USER_KBN = :USER_KBN";
                Params.Add(new DBParameter("USER_KBN", kbnID));
            }
            if (adminFlag != 2) // 2 は、すべてを対象とする
            {
                SQL += " and ADMIN_FLAG = :ADMIN_FLAG";
                Params.Add(new DBParameter("ADMIN_FLAG", adminFlag));
            }
            if (sei != "")
            {
                SQL += " and SEI LIKE :SEI";
                Params.Add(new DBParameter("SEI", "%" + sei + "%" ));
            }
            if (mei != "")
            {
                SQL += " and MEI LIKE :MEI";
                Params.Add(new DBParameter("MEI", "%" + mei + "%"));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsUser), "OrderBy");
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        public static List<MsUser> SearchRecords(MsUser loginUser, string seiKana, string meiKana)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsUser), MethodBase.GetCurrentMethod());
            List<MsUser> ret = new List<MsUser>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsUser> mapping = new MappingBase<MsUser>();
            SQL = SQL.Replace("#INNER_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsUser), "ByUserKbn船員");

            if (seiKana != "")
            {
                SQL += " and SEI_KANA LIKE :SEI_KANA";
                Params.Add(new DBParameter("SEI_KANA", "%" + seiKana + "%"));
            }
            if (meiKana != "")
            {
                SQL += " and MEI_KANA LIKE :MEI_KANA";
                Params.Add(new DBParameter("MEI_KANA", "%" + meiKana + "%"));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsUser), "OrderBy");
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public bool InsertRecord(MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }

        public bool InsertRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsUser), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_USER_ID", MsUserID));
            Params.Add(new DBParameter("USER_KBN", UserKbn));
            Params.Add(new DBParameter("LOGIN_ID", LoginID));
            Params.Add(new DBParameter("PASSWORD", Password));
            Params.Add(new DBParameter("MAIL_ADDRESS", MailAddress));
            Params.Add(new DBParameter("SEI", Sei));
            Params.Add(new DBParameter("MEI", Mei));
            Params.Add(new DBParameter("SEI_KANA", SeiKana));
            Params.Add(new DBParameter("MEI_KANA", MeiKana));
            Params.Add(new DBParameter("SEX", Sex));
            Params.Add(new DBParameter("ADMIN_FLAG", AdminFlag));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            Params.Add(new DBParameter("DOC_FLAG_CEO", DocFlag_CEO));
            Params.Add(new DBParameter("DOC_FLAG_ADMIN", DocFlag_Admin));

            Params.Add(new DBParameter("DOC_FLAG_MSI_FERRY", DocFlag_MsiFerry));
            Params.Add(new DBParameter("DOC_FLAG_CREW_FERRY", DocFlag_CrewFerry));
            Params.Add(new DBParameter("DOC_FLAG_TSI_FERRY", DocFlag_TsiFerry));

            Params.Add(new DBParameter("DOC_FLAG_OFFICER", DocFlag_Officer));
            Params.Add(new DBParameter("DOC_FLAG_GL", DocFlag_GL));
            Params.Add(new DBParameter("DOC_FLAG_TL", DocFlag_TL));

            //Params.Add(new DBParameter("DOC_FLAG_MSI_CARGO", DocFlag_MsiCargo));
            //Params.Add(new DBParameter("DOC_FLAG_CREW_CARGO", DocFlag_CrewCargo));
            //Params.Add(new DBParameter("DOC_FLAG_TSI_CARGO", DocFlag_TsiCargo));
            //Params.Add(new DBParameter("DOC_FLAG_SD_MANAGER", DocFlag_SdManager));

            DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            return true;
        }

        public bool UpdateRecord(MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }

        public bool UpdateRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsUser), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("USER_KBN", UserKbn));
            Params.Add(new DBParameter("LOGIN_ID", LoginID));
            Params.Add(new DBParameter("PASSWORD", Password));
            Params.Add(new DBParameter("MAIL_ADDRESS", MailAddress));
            Params.Add(new DBParameter("SEI", Sei));
            Params.Add(new DBParameter("MEI", Mei));
            Params.Add(new DBParameter("SEI_KANA", SeiKana));
            Params.Add(new DBParameter("MEI_KANA", MeiKana));
            Params.Add(new DBParameter("SEX", Sex));
            Params.Add(new DBParameter("ADMIN_FLAG", AdminFlag));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("DOC_FLAG_CEO", DocFlag_CEO));
            Params.Add(new DBParameter("DOC_FLAG_ADMIN", DocFlag_Admin));

            Params.Add(new DBParameter("DOC_FLAG_MSI_FERRY", DocFlag_MsiFerry));
            Params.Add(new DBParameter("DOC_FLAG_CREW_FERRY", DocFlag_CrewFerry));
            Params.Add(new DBParameter("DOC_FLAG_TSI_FERRY", DocFlag_TsiFerry));

            Params.Add(new DBParameter("DOC_FLAG_OFFICER", DocFlag_Officer));
            Params.Add(new DBParameter("DOC_FLAG_GL", DocFlag_GL));
            Params.Add(new DBParameter("DOC_FLAG_TL", DocFlag_TL));

            //Params.Add(new DBParameter("DOC_FLAG_MSI_CARGO", DocFlag_MsiCargo));
            //Params.Add(new DBParameter("DOC_FLAG_CREW_CARGO", DocFlag_CrewCargo));
            //Params.Add(new DBParameter("DOC_FLAG_TSI_CARGO", DocFlag_TsiCargo));
            //Params.Add(new DBParameter("DOC_FLAG_SD_MANAGER", DocFlag_SdManager));

            Params.Add(new DBParameter("MS_USER_ID", MsUserID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <returns></returns>
        public bool DeleteFlagRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsUser), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_USER_ID", MsUserID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region ISyncTable メンバ

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsUserID));

        //    int count = Convert.ToInt32(DBConnect.ExecuteScalar(loginUser.MsUserID, SQL, Params));

        //    return count > 0 ? true : false;
        //}
        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", MsUserID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
