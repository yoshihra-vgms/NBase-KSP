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
    [TableAttribute("SI_KENSHIN_PMH_KA")]
    public class SiKenshinPmhKa : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 健康診断ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_KENSHIN_PMH_KA_ID", true)]
        public string SiKenshinPmhKaID { get; set; }

        /// <summary>
        /// 船員ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }

        /// <summary>
        /// alcID
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALC_ID")]
        public int AlcId { get; set; }

        /// <summary>
        /// 喫煙
        /// </summary>
        [DataMember]
        [ColumnAttribute("SMOKING")]
        public int Smoking { get; set; }


        /// <summary>
        /// 既往歴１
        /// </summary>
        [DataMember]
        [ColumnAttribute("PMH1")]
        public string Pmh1 { get; set; }

        /// <summary>
        /// 既往歴２
        /// </summary>
        [DataMember]
        [ColumnAttribute("PMH2")]
        public string Pmh2 { get; set; }

        /// <summary>
        /// 既往歴３
        /// </summary>
        [DataMember]
        [ColumnAttribute("PMH3")]
        public string Pmh3 { get; set; }

        /// <summary>
        /// 既往歴４
        /// </summary>
        [DataMember]
        [ColumnAttribute("PMH4")]
        public string Pmh4 { get; set; }

        /// <summary>
        /// 既往歴５
        /// </summary>
        [DataMember]
        [ColumnAttribute("PMH5")]
        public string Pmh5 { get; set; }


        /// <summary>
        /// 卵アレルギー
        /// </summary>
        [DataMember]
        [ColumnAttribute("EGG_ALLERGY")]
        public int EggAllergy { get; set; }

        /// <summary>
        /// 乳アレルギー
        /// </summary>
        [DataMember]
        [ColumnAttribute("MILK_ALLERGY")]
        public int MilkAllergy { get; set; }

        /// <summary>
        /// 小麦アレルギー
        /// </summary>
        [DataMember]
        [ColumnAttribute("WHEAT_ALLERGY")]
        public int WheatAllergy { get; set; }

        /// <summary>
        /// えびアレルギー
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHRIMP_ALLERGY")]
        public int ShrimpAllergy { get; set; }

        /// <summary>
        /// カニアレルギー
        /// </summary>
        [DataMember]
        [ColumnAttribute("CRAB_ALLERGY")]
        public int CrabAllergy { get; set; }

        /// <summary>
        /// 落花生アレルギー
        /// </summary>
        [DataMember]
        [ColumnAttribute("PEANUT_ALLERGY")]
        public int PeanutAllergy { get; set; }

        /// <summary>
        /// そばアレルギー
        /// </summary>
        [DataMember]
        [ColumnAttribute("BUCKWHEAT_ALLERGY")]
        public int BuckwheatAllergy { get; set; }

        /// <summary>
        /// その他１アレルギー
        /// </summary>
        [DataMember]
        [ColumnAttribute("ETC_ALLERGY1")]
        public int EtcAllergy1 { get; set; }

        /// <summary>
        /// その他２アレルギー
        /// </summary>
        [DataMember]
        [ColumnAttribute("ETC_ALLERGY2")]
        public int EtcAllergy2 { get; set; }

        /// <summary>
        /// その他３アレルギー
        /// </summary>
        [DataMember]
        [ColumnAttribute("ETC_ALLERGY3")]
        public int EtcAllergy3 { get; set; }

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


        [DataMember]
        public List<PtAlarmInfo> AlarmInfoList { get; set; }


        #endregion

        public bool EditFlag = false;


        public SiKenshinPmhKa()
        {
            this.MsSeninID = Int32.MinValue;
         }

        
        

        public static List<SiKenshinPmhKa> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKenshinPmhKa), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshinPmhKa), "By有効データ");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");

            List<SiKenshinPmhKa> ret = new List<SiKenshinPmhKa>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKenshinPmhKa> mapping = new MappingBase<SiKenshinPmhKa>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static SiKenshinPmhKa GetRecord(MsUser loginUser, string siKenshinID)
        {
            return GetRecord(null, loginUser, siKenshinID);
        }
        public static SiKenshinPmhKa GetRecord(ORMapping.DBConnect dbConnect, MsUser loginUser, string siKenshinID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKenshinPmhKa), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshinPmhKa), "By有効データ");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshinPmhKa), "BySiKenshinPmhKaID");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");

            List<SiKenshinPmhKa> ret = new List<SiKenshinPmhKa>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SI_KENSHIN_PMH_KA_ID", siKenshinID));
            MappingBase<SiKenshinPmhKa> mapping = new MappingBase<SiKenshinPmhKa>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;

            return ret[0];
        }


        public static SiKenshinPmhKa GetRecordByMsSeninID(MsUser loginUser, int msSeninId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKenshinPmhKa), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshinPmhKa), "By有効データ");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshinPmhKa), "ByMsSeninID");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");

            List<SiKenshinPmhKa> ret = new List<SiKenshinPmhKa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKenshinPmhKa> mapping = new MappingBase<SiKenshinPmhKa>();
            Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;

            return ret[0];
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

            Params.Add(new DBParameter("SI_KENSHIN_PMH_KA_ID", SiKenshinPmhKaID));

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));

            Params.Add(new DBParameter("ALC_ID", AlcId));           
            Params.Add(new DBParameter("SMOKING", Smoking));
            Params.Add(new DBParameter("PMH1", Pmh1));
            Params.Add(new DBParameter("PMH2", Pmh2));
            Params.Add(new DBParameter("PMH3", Pmh3));
            Params.Add(new DBParameter("PMH4", Pmh4));
            Params.Add(new DBParameter("PMH5", Pmh5));
            Params.Add(new DBParameter("EGG_ALLERGY", EggAllergy));
            Params.Add(new DBParameter("MILK_ALLERGY", MilkAllergy));
            Params.Add(new DBParameter("WHEAT_ALLERGY", WheatAllergy));
            Params.Add(new DBParameter("SHRIMP_ALLERGY", ShrimpAllergy));
            Params.Add(new DBParameter("CRAB_ALLERGY", CrabAllergy));
            Params.Add(new DBParameter("PEANUT_ALLERGY", PeanutAllergy));
            Params.Add(new DBParameter("BUCKWHEAT_ALLERGY", BuckwheatAllergy));
            Params.Add(new DBParameter("ETC_ALLERGY1", EtcAllergy1));
            Params.Add(new DBParameter("ETC_ALLERGY2", EtcAllergy2));
            Params.Add(new DBParameter("ETC_ALLERGY3", EtcAllergy3));

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

        public bool UpdateRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            return _UpdateRecord(dbConnect, loginUser, true, false);
        }

        public bool UpdateRecordWithoutAttachFile(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            return _UpdateRecord(dbConnect, loginUser, false, true);
        }

        public bool _UpdateRecord(ORMapping.DBConnect dbConnect, MsUser loginUser, bool isUpdateAttach, bool is同期)
        {
            if (is同期 == false)
            {
                UserKey = null;
            }
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), "UpdateRecord");

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));

            Params.Add(new DBParameter("ALC_ID", AlcId));
            Params.Add(new DBParameter("SMOKING", Smoking));
            Params.Add(new DBParameter("PMH1", Pmh1));
            Params.Add(new DBParameter("PMH2", Pmh2));
            Params.Add(new DBParameter("PMH3", Pmh3));
            Params.Add(new DBParameter("PMH4", Pmh4));
            Params.Add(new DBParameter("PMH5", Pmh5));
            Params.Add(new DBParameter("EGG_ALLERGY", EggAllergy));
            Params.Add(new DBParameter("MILK_ALLERGY", MilkAllergy));
            Params.Add(new DBParameter("WHEAT_ALLERGY", WheatAllergy));
            Params.Add(new DBParameter("SHRIMP_ALLERGY", ShrimpAllergy));
            Params.Add(new DBParameter("CRAB_ALLERGY", CrabAllergy));
            Params.Add(new DBParameter("PEANUT_ALLERGY", PeanutAllergy));
            Params.Add(new DBParameter("BUCKWHEAT_ALLERGY", BuckwheatAllergy));
            Params.Add(new DBParameter("ETC_ALLERGY1", EtcAllergy1));
            Params.Add(new DBParameter("ETC_ALLERGY2", EtcAllergy2));
            Params.Add(new DBParameter("ETC_ALLERGY3", EtcAllergy3));
            
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_KENSHIN_PMH_KA_ID", SiKenshinPmhKaID));
            Params.Add(new DBParameter("TS", Ts));

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
            Params.Add(new DBParameter("PK", SiKenshinPmhKaID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return SiKenshinPmhKaID == null;
        }

        public bool Edited(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            SiKenshinPmhKa org = SiKenshinPmhKa.GetRecord(dbConnect, loginUser, SiKenshinPmhKaID);
            if (org == null)
                return true;
            return org.Equals(dbConnect, loginUser, this) == false;
        }

        public bool Equals(ORMapping.DBConnect dbConnect, MsUser loginUser, SiKenshinPmhKa dst)
        {
            bool ret = true;

            if (
                this.AlcId != dst.AlcId ||
                this.Smoking != dst.Smoking ||
                this.Pmh1 != dst.Pmh1 ||
                this.Pmh2 != dst.Pmh2 ||
                this.Pmh3 != dst.Pmh3 ||
                this.Pmh4 != dst.Pmh4 ||
                this.Pmh5 != dst.Pmh5 ||
                this.EggAllergy != dst.EggAllergy ||
                this.MilkAllergy != dst.MilkAllergy ||
                this.WheatAllergy != dst.WheatAllergy ||
                this.ShrimpAllergy != dst.ShrimpAllergy ||
                this.CrabAllergy != dst.CrabAllergy ||
                this.PeanutAllergy != dst.PeanutAllergy ||
                this.BuckwheatAllergy != dst.BuckwheatAllergy ||
                this.EtcAllergy1 != dst.EtcAllergy1 ||
                this.EtcAllergy2 != dst.EtcAllergy2 ||
                this.EtcAllergy3 != dst.EtcAllergy3 ||

                this.DeleteFlag != dst.DeleteFlag
                )
            {
                ret = false;
            }

            return ret;
        }
    }
}
