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
using NBaseUtil;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("SI_KAZOKU")]
    public class SiKazoku : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 家族ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_KAZOKU_ID", true)]
        public string SiKazokuID { get; set; }


        /// <summary>
        /// 船員ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }


        /// <summary>
        /// 区分
        /// ０→家族
        /// １→家族外
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIND")]
        public int Kind { get; set; }


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
        /// 姓カナ
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEI_KANA")]
        public string SeiKana { get; set; }

        /// <summary>
        /// 名カナ
        /// </summary>
        [DataMember]
        [ColumnAttribute("MEI_KANA")]
        public string MeiKana { get; set; }

        /// <summary>
        /// 性別
        /// 1→男
        /// 2→女
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEX")]
        public int Sex { get; set; }

        /// <summary>
        /// 続柄
        /// </summary>
        [DataMember]
        [ColumnAttribute("TUZUKIGARA")]
        public string Tuzukigara { get; set; }

        /// <summary>
        /// 生年月日
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIRTHDAY")]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// TEL
        /// </summary>
        [DataMember]
        [ColumnAttribute("TEL")]
        public string Tel { get; set; }

        /// <summary>
        /// 職業
        /// </summary>
        [DataMember]
        [ColumnAttribute("OCCUPATION")]
        public string Occupation { get; set; }

        /// <summary>
        /// 緊急連絡先：
        /// </summary>
        [DataMember]
        [ColumnAttribute("EMERGENCY_KIND")]
        public string EmergencyKind { get; set; }

        /// <summary>
        /// 同居
        /// 0→同居
        /// 1→別居
        /// </summary>
        [DataMember]
        [ColumnAttribute("LIVING_TOGETHER")]
        public int LivingTogether { get; set; }

        /// <summary>
        /// 〒番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("ZIP_CODE")]
        public string ZipCode { get; set; }

        /// <summary>
        /// 都道府県
        /// </summary>
        [DataMember]
        [ColumnAttribute("PREFECTURES")]
        public string Prefectures { get; set; }

        /// <summary>
        /// 市区町村名
        /// </summary>
        [DataMember]
        [ColumnAttribute("CITY_TOWN")]
        public string CityTown { get; set; }

        /// <summary>
        /// 番地、町名
        /// </summary>
        [DataMember]
        [ColumnAttribute("STREET")]
        public string Street { get; set; }

        /// <summary>
        /// 船員保険被扶養者区分
        /// 0→無
        /// 1→有
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEAMEN_S_INSURANCE_DEPENDENT")]
        public int SeamensInsuranceDependent { get; set; }

        /// <summary>
        /// 扶養
        /// 0→無
        /// 1→該当
        /// </summary>
        [DataMember]
        [ColumnAttribute("DEPENDENT")]
        public int Dependent { get; set; }

        /// <summary>
        /// 老年者
        /// 0→無
        /// 1→該当
        /// /// </summary>
        [DataMember]
        [ColumnAttribute("ELDERLY")]
        public int Elderly { get; set; }

        /// <summary>
        /// 障害者
        /// 0→無
        /// 1→一般
        /// 1→特別
        /// </summary>
        [DataMember]
        [ColumnAttribute("HANDICAPPED")]
        public int Handicapped { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("REMARKS")]
        public string Remarks { get; set; }


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

        public bool EditFlag = false;

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



        public string KindStr
        {
            get
            {
                return Kind == 0 ? "家族" : "家族外";
            }
        }
        public string SexStr
        {
            get
            {
                return Sex == 1 ? "男" : "女";
            }
        }




        public string LivingTogetherStr
        {
            get
            {
                return LivingTogether == 0 ? "同居" : "別居";
            }
        }

        public string SeamensInsuranceDependentStr
        {
            get
            {
                return SeamensInsuranceDependent == 0 ? "無" : "有";
            }
        } 
        
        public string DependentStr
        {
            get
            {
                return Dependent == 0 ? "無" : "該当";
            }
        }

        public string ElderlyStr
        {
            get
            {
                return Elderly == 0 ? "無" : "該当";
            }
        }

        public string HandicappedStr
        {
            get
            {
                return Handicapped == 0 ? "無" : Handicapped == 1 ? "一般" : "特別";
            }
        }



        public string AgeStr
        {
            get
            {
                if (Birthday != DateTime.MinValue)
                    return DateTimeUtils.年齢計算(Birthday).ToString();
                else
                    return "";
            }
        }


        public string PrefecturesStr { get; set; }

        public void MakeFullAddress(List<MsSiOptions> prefecturesList)
        {
            PrefecturesStr = "";
            var p = prefecturesList.Where(o => o.MsSiOptionsID == Prefectures).FirstOrDefault();
            if (p != null)
            {
                PrefecturesStr += p.Name;
            }
            if (PrefecturesStr.Length > 0)
            {
                PrefecturesStr += " ";
            }
            PrefecturesStr += CityTown;
        }




        public SiKazoku()
        {
            this.MsSeninID = Int32.MinValue;
        }

        public static SiKazoku GetRecord(MsUser loginUser, string siKazukuId)
        {
            return GetRecord(null, loginUser, siKazukuId);
        }
        public static SiKazoku GetRecord(ORMapping.DBConnect dbConnect, MsUser loginUser, string siKazukuId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKazoku), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKazoku), "By有効データ");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKazoku), "BySiKazokuID");

            List<SiKazoku> ret = new List<SiKazoku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKazoku> mapping = new MappingBase<SiKazoku>();
            Params.Add(new DBParameter("SI_KAZOKU_ID", siKazukuId));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret != null)
                return ret.First();
            else
                return null;
        }

        
        

        public static List<SiKazoku> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKazoku), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKazoku), "By有効データ");

            List<SiKazoku> ret = new List<SiKazoku>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKazoku> mapping = new MappingBase<SiKazoku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<SiKazoku> GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKazoku), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKazoku), "By有効データ");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKazoku), "ByMsSeninID");

            List<SiKazoku> ret = new List<SiKazoku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKazoku> mapping = new MappingBase<SiKazoku>();
            Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<SiKazoku> GetRecordsByDataNo(MsUser loginUser, Int64 dataNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKazoku), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKazoku), "ByDataNo");

            List<SiKazoku> ret = new List<SiKazoku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKazoku> mapping = new MappingBase<SiKazoku>();
            Params.Add(new DBParameter("DATA_NO", dataNo));
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

            Params.Add(new DBParameter("SI_KAZOKU_ID", SiKazokuID));

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));

            Params.Add(new DBParameter("KIND", Kind));
            Params.Add(new DBParameter("SEI", Sei));
            Params.Add(new DBParameter("MEI", Mei));
            Params.Add(new DBParameter("SEI_KANA", SeiKana));
            Params.Add(new DBParameter("MEI_KANA", MeiKana));
            Params.Add(new DBParameter("SEX", Sex));
            Params.Add(new DBParameter("TUZUKIGARA", Tuzukigara));
            Params.Add(new DBParameter("BIRTHDAY", Birthday));
            Params.Add(new DBParameter("TEL", Tel));
            Params.Add(new DBParameter("OCCUPATION", Occupation));
            Params.Add(new DBParameter("EMERGENCY_KIND", EmergencyKind));
            Params.Add(new DBParameter("LIVING_TOGETHER", LivingTogether));
            Params.Add(new DBParameter("ZIP_CODE", ZipCode));
            Params.Add(new DBParameter("PREFECTURES", Prefectures));
            Params.Add(new DBParameter("CITY_TOWN", CityTown));
            Params.Add(new DBParameter("STREET", Street));
            Params.Add(new DBParameter("SEAMEN_S_INSURANCE_DEPENDENT", SeamensInsuranceDependent));
            Params.Add(new DBParameter("DEPENDENT", Dependent));
            Params.Add(new DBParameter("ELDERLY", Elderly));
            Params.Add(new DBParameter("HANDICAPPED", Handicapped));
            Params.Add(new DBParameter("REMARKS", Remarks));
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

        public bool UpdateRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            return UpdateRecord(dbConnect, loginUser, false);
        }

        public bool UpdateRecord(ORMapping.DBConnect dbConnect, MsUser loginUser, bool is同期 = false)
        {
            if (is同期 == false)
            {
                UserKey = null;
            }

            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));

            Params.Add(new DBParameter("KIND", Kind));
            Params.Add(new DBParameter("SEI", Sei));
            Params.Add(new DBParameter("MEI", Mei));
            Params.Add(new DBParameter("SEI_KANA", SeiKana));
            Params.Add(new DBParameter("MEI_KANA", MeiKana));
            Params.Add(new DBParameter("SEX", Sex));
            Params.Add(new DBParameter("TUZUKIGARA", Tuzukigara));
            Params.Add(new DBParameter("BIRTHDAY", Birthday));
            Params.Add(new DBParameter("TEL", Tel));
            Params.Add(new DBParameter("OCCUPATION", Occupation));
            Params.Add(new DBParameter("EMERGENCY_KIND", EmergencyKind));
            Params.Add(new DBParameter("LIVING_TOGETHER", LivingTogether));
            Params.Add(new DBParameter("ZIP_CODE", ZipCode));
            Params.Add(new DBParameter("PREFECTURES", Prefectures));
            Params.Add(new DBParameter("CITY_TOWN", CityTown));
            Params.Add(new DBParameter("STREET", Street));
            Params.Add(new DBParameter("SEAMEN_S_INSURANCE_DEPENDENT", SeamensInsuranceDependent));
            Params.Add(new DBParameter("DEPENDENT", Dependent));
            Params.Add(new DBParameter("ELDERLY", Elderly));
            Params.Add(new DBParameter("HANDICAPPED", Handicapped));
            Params.Add(new DBParameter("REMARKS", Remarks));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_KAZOKU_ID", SiKazokuID));
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
            Params.Add(new DBParameter("PK", SiKazokuID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return SiKazokuID == null;
        }


        public bool Edited(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            SiKazoku org = SiKazoku.GetRecord(dbConnect, loginUser, SiKazokuID);
            if (org == null)
                return true;

            return org.Equals(this) == false;
        }

        public override bool Equals(object obj)
        {
            bool ret = true;
            SiKazoku dst = obj as SiKazoku;

            if (this.Kind != dst.Kind ||
                this.Sei != dst.Sei ||
                this.Mei != dst.Mei ||
                this.SeiKana != dst.SeiKana ||
                this.MeiKana != dst.MeiKana ||
                this.Sex != dst.Sex ||
                this.Tuzukigara != dst.Tuzukigara ||
                this.Birthday != dst.Birthday ||
                this.Tel != dst.Tel ||
                this.Occupation != dst.Occupation ||
                this.EmergencyKind != dst.EmergencyKind ||
                this.LivingTogether != dst.LivingTogether ||
                this.ZipCode != dst.ZipCode ||
                this.Prefectures != dst.Prefectures ||
                this.CityTown != dst.CityTown ||
                this.Street != dst.Street ||
                this.SeamensInsuranceDependent != dst.SeamensInsuranceDependent ||
                this.Dependent != dst.Dependent ||
                this.Elderly != dst.Elderly ||
                this.Handicapped != dst.Handicapped ||
                this.Remarks != dst.Remarks ||
                this.ShowOrder != dst.ShowOrder ||
                this.DeleteFlag != dst.DeleteFlag
                )
            {
                ret = false;
            }

            return ret;
        }
    }
}
