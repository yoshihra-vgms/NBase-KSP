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
        /// Int.MinValue→不明
        /// 0→男
        /// 1→女
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEX")]
        public int Sex { get; set; }

        /// <summary>
        /// 続柄
        /// </summary>
        [DataMember]
        [ColumnAttribute("ZOKUGARA")]
        public string Zokugara { get; set; }

        /// <summary>
        /// 生年月日
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIRTHDAY")]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 扶養
        /// 0→有
        /// 1→無
        /// </summary>
        [DataMember]
        [ColumnAttribute("FUYOU")]
        public int Fuyou { get; set; }

        /// <summary>
        /// TEL
        /// </summary>
        [DataMember]
        [ColumnAttribute("TEL")]
        public string Tel { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }

        
        
        
        
        
        
        
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


        public string SexStr
        {
            get
            {
                return Sex == 0 ? "男" : "女";
            }
        }


        public string FuyouStr
        {
            get
            {
                return Fuyou == 0 ? "有" : "無";
            }
        }




        public SiKazoku()
        {
            this.MsSeninID = Int32.MinValue;
        }

        
        

        public static List<SiKazoku> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKazoku), MethodBase.GetCurrentMethod());

            List<SiKazoku> ret = new List<SiKazoku>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKazoku> mapping = new MappingBase<SiKazoku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<SiKazoku> GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKazoku), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKazoku), "ByMsSeninID");

            List<SiKazoku> ret = new List<SiKazoku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKazoku> mapping = new MappingBase<SiKazoku>();
            Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));
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
           
            Params.Add(new DBParameter("SEI", Sei));
            Params.Add(new DBParameter("MEI", Mei));
            Params.Add(new DBParameter("SEI_KANA", SeiKana));
            Params.Add(new DBParameter("MEI_KANA", MeiKana));
            Params.Add(new DBParameter("SEX", Sex));
            Params.Add(new DBParameter("ZOKUGARA", Zokugara));
            Params.Add(new DBParameter("BIRTHDAY", Birthday));
            Params.Add(new DBParameter("FUYOU", Fuyou));
            Params.Add(new DBParameter("TEL", Tel));
            Params.Add(new DBParameter("BIKOU", Bikou));

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
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));

            Params.Add(new DBParameter("SEI", Sei));
            Params.Add(new DBParameter("MEI", Mei));
            Params.Add(new DBParameter("SEI_KANA", SeiKana));
            Params.Add(new DBParameter("MEI_KANA", MeiKana));
            Params.Add(new DBParameter("SEX", Sex));
            Params.Add(new DBParameter("ZOKUGARA", Zokugara));
            Params.Add(new DBParameter("BIRTHDAY", Birthday));
            Params.Add(new DBParameter("FUYOU", Fuyou));
            Params.Add(new DBParameter("TEL", Tel));
            Params.Add(new DBParameter("BIKOU", Bikou));

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

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", SiKazokuID));

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
            Params.Add(new DBParameter("PK", SiKazokuID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return SiKazokuID == null;
        }
    }
}
