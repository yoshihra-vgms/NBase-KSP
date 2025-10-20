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
    [TableAttribute("BG_JISEKI")]
    public class BgJiseki : IYojitsu
    {
        //JISEKI_ID                      NUMBER(12,0) NOT NULL,
        //MS_VESSEL_ID                   NUMBER(4,0),
        //MS_KAMOKU_ID                   NUMBER(9,0),
        //JISEKI_DATE                    DATE NOT NULL,
        //KIKAN_NO                       VARCHAR2(20),
        //YEN_AMOUNT                     NUMBER(16,3),
        //DOLLER_AMOUNT                  NUMBER(16,3),
        //AMOUNT                         NUMBER(16,3) NOT NULL,
        //OD_SHR_ID                      VARCHAR2(40),
        //RENEW_DATE                     DATE NOT NULL,
        //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
        //TS                             VARCHAR2(20),
        
        #region データメンバ

        /// <summary>
        /// 実績項目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("JISEKI_ID")]
        public long JisekiID { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KAMOKU_ID")]
        public int MsKamokuID { get; set; }


        /// <summary>
        /// 実績日
        /// </summary>
        [DataMember]
        [ColumnAttribute("JISEKI_DATE")]
        public string JisekiDate { get; set; }

        /// <summary>
        /// 基幹番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIKAN_NO")]
        public string KikanNo { get; set; }

        /// <summary>
        /// 円金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("YEN_AMOUNT")]
        public decimal YenAmount { get; set; }

        /// <summary>
        /// ドル金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("DOLLER_AMOUNT")]
        public decimal DollerAmount { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("AMOUNT")]
        public decimal Amount { get; set; }


        /// <summary>
        /// 前リビジョン 円金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("PRE_YEN_AMOUNT")]
        public decimal PreYenAmount { get; set; }

        /// <summary>
        /// 前リビジョン ドル金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("PRE_DOLLER_AMOUNT")]
        public decimal PreDollerAmount { get; set; }


        /// <summary>
        /// 前リビジョン 金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("PRE_AMOUNT")]
        public decimal PreAmount { get; set; }

        /// <summary>
        /// 支払いID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_SHR_ID")]
        public string OdShrID { get; set; }
        
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
        /// 
        /// </summary>
        public string Nengetsu {
            get
            {
                return JisekiDate;
            }

            set
            {
                JisekiDate = value;
            }
        }

        ////////////////////////////////////////////////
        public BgJiseki()
        {
        }


        public static List<BgJiseki> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgJiseki), MethodBase.GetCurrentMethod());

            List<BgJiseki> ret = new List<BgJiseki>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgJiseki> mapping = new MappingBase<BgJiseki>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<BgJiseki> GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgJiseki), MethodBase.GetCurrentMethod());

            List<BgJiseki> ret = new List<BgJiseki>();

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));

            MappingBase<BgJiseki> mapping = new MappingBase<BgJiseki>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }



        public static List<BgJiseki> GetRecords_年単位_船(MsUser loginUser, int msVesselId, string jisekiDateStart, string jisekiDateEnd)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgJiseki), MethodBase.GetCurrentMethod());

            List<BgJiseki> ret = new List<BgJiseki>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgJiseki> mapping = new MappingBase<BgJiseki>();

            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            Params.Add(new DBParameter("JISEKI_DATE_START", jisekiDateStart));
            Params.Add(new DBParameter("JISEKI_DATE_END", jisekiDateEnd));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<BgJiseki> GetRecords_月単位_船(MsUser loginUser, int msVesselId, string jisekiDateStart, string jisekiDateEnd)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgJiseki), MethodBase.GetCurrentMethod());

            List<BgJiseki> ret = new List<BgJiseki>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgJiseki> mapping = new MappingBase<BgJiseki>();

            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            Params.Add(new DBParameter("JISEKI_DATE_START", jisekiDateStart));
            Params.Add(new DBParameter("JISEKI_DATE_END", jisekiDateEnd));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        
        public static BgJiseki GetRecord(MsUser loginUser, long jiseki_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgJiseki), MethodBase.GetCurrentMethod());

            List<BgJiseki> ret = new List<BgJiseki>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgJiseki> mapping = new MappingBase<BgJiseki>();

            Params.Add(new DBParameter("JISEKI_ID", jiseki_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }

        public static List<BgJiseki> GetRecordsByJisekiDate(MsUser loginUser, string jisekiDate)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgJiseki), MethodBase.GetCurrentMethod());

            List<BgJiseki> ret = new List<BgJiseki>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgJiseki> mapping = new MappingBase<BgJiseki>();

            Params.Add(new DBParameter("JISEKI_DATE", jisekiDate));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


		/// <summary>
		/// 種船タイプごとの費目別合計を取得する
		/// 条件：船タイプ、期間、部門ＩＤ
		/// </summary>
		/// <param name="loginUser"></param>
		/// <param name="vesseltype"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="bumonid"></param>
		/// <returns></returns>
		public static List<BgJiseki> GetRecordsByVesselTypePriodBumonHimokus(
			MsUser loginUser, string vesseltype, string start, string end, int bumonid)
		{
			string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgJiseki), MethodBase.GetCurrentMethod());

			List<BgJiseki> ret = new List<BgJiseki>();

			ParameterConnection Params = new ParameterConnection();
			MappingBase<BgJiseki> mapping = new MappingBase<BgJiseki>();

			Params.Add(new DBParameter("VESSEL_TYPE", vesseltype));
			Params.Add(new DBParameter("START_DATE", start));
			Params.Add(new DBParameter("END_DATE", end));
			Params.Add(new DBParameter("BUMON_ID", bumonid));

			ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

			return ret;
		}


		/// <summary>
		/// 種船タイプごとの費目別合計を取得する
		/// 条件：船タイプ、期間、部門ＩＤ
		/// </summary>
		/// <param name="loginUser"></param>
		/// <param name="vesseltype"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="bumonid"></param>
		/// <returns></returns>
		public static List<BgJiseki> GetRecordsByVesselTypePriodHimokus(
			MsUser loginUser, string vesseltype, string start, string end)
		{
			string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgJiseki), MethodBase.GetCurrentMethod());

			List<BgJiseki> ret = new List<BgJiseki>();

			ParameterConnection Params = new ParameterConnection();
			MappingBase<BgJiseki> mapping = new MappingBase<BgJiseki>();

			Params.Add(new DBParameter("VESSEL_TYPE", vesseltype));
			Params.Add(new DBParameter("START_DATE", start));
			Params.Add(new DBParameter("END_DATE", end));

			ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

			return ret;
		}




        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgJiseki), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
     
            //Params.Add(new DBParameter("JISEKI_ID", this.JisekiID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));
            Params.Add(new DBParameter("MS_KAMOKU_ID", this.MsKamokuID));
            Params.Add(new DBParameter("JISEKI_DATE", this.JisekiDate));
            Params.Add(new DBParameter("KIKAN_NO", this.KikanNo));
            Params.Add(new DBParameter("YEN_AMOUNT", this.YenAmount));
            Params.Add(new DBParameter("DOLLER_AMOUNT", this.DollerAmount));
            Params.Add(new DBParameter("AMOUNT", this.Amount));
            Params.Add(new DBParameter("OD_SHR_ID", this.OdShrID));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));
            Params.Add(new DBParameter("TS", this.Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        public bool InsertRecord(DBConnect dbcone, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgJiseki), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            //Params.Add(new DBParameter("JISEKI_ID", this.JisekiID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));
            Params.Add(new DBParameter("MS_KAMOKU_ID", this.MsKamokuID));
            Params.Add(new DBParameter("JISEKI_DATE", this.JisekiDate));
            Params.Add(new DBParameter("KIKAN_NO", this.KikanNo));
            Params.Add(new DBParameter("YEN_AMOUNT", this.YenAmount));
            Params.Add(new DBParameter("DOLLER_AMOUNT", this.DollerAmount));
            Params.Add(new DBParameter("AMOUNT", this.Amount));
            Params.Add(new DBParameter("OD_SHR_ID", this.OdShrID));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));
            Params.Add(new DBParameter("TS", this.Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbcone, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgJiseki), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));
            Params.Add(new DBParameter("MS_KAMOKU_ID", this.MsKamokuID));
            Params.Add(new DBParameter("JISEKI_DATE", this.JisekiDate));
            Params.Add(new DBParameter("KIKAN_NO", this.KikanNo));
            Params.Add(new DBParameter("YEN_AMOUNT", this.YenAmount));
            Params.Add(new DBParameter("DOLLER_AMOUNT", this.DollerAmount));
            Params.Add(new DBParameter("AMOUNT", this.Amount));
            Params.Add(new DBParameter("OD_SHR_ID", this.OdShrID));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));

            Params.Add(new DBParameter("JISEKI_ID", this.JisekiID));
            Params.Add(new DBParameter("TS", this.Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        public bool DeleteByJisekiID(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgJiseki), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("JISEKI_ID", this.JisekiID));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        public static bool DeleteByMsKamoku_KikanFlag(DBConnect dbcone, NBaseData.DAC.MsUser loginUser, string JisekiDate)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgJiseki), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            #endregion

            Params.Add(new DBParameter("JISEKI_DATE", JisekiDate));
            int cnt = DBConnect.ExecuteNonQuery(dbcone, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        public static long GetMaxJisekiID(DBConnect dbcone, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgJiseki), MethodBase.GetCurrentMethod());

            List<BgJiseki> ret = new List<BgJiseki>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgJiseki> mapping = new MappingBase<BgJiseki>();

            ret = mapping.FillRecrods(dbcone, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return 0;
            }

            return ret[0].JisekiID;
        }

        public static List<BgJiseki> GetRecords_年単位_全社(MsUser loginUser, string yearStart, string yearEnd)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgJiseki), MethodBase.GetCurrentMethod());

            List<BgJiseki> ret = new List<BgJiseki>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgJiseki> mapping = new MappingBase<BgJiseki>();

            Params.Add(new DBParameter("JISEKI_DATE_START", yearStart));
            Params.Add(new DBParameter("JISEKI_DATE_END", yearEnd));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        
        public static List<BgJiseki> GetRecords_年単位_グループ(MsUser loginUser, string msVesselTypeId, string yearStart, string yearEnd)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgJiseki), MethodBase.GetCurrentMethod());

            List<BgJiseki> ret = new List<BgJiseki>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgJiseki> mapping = new MappingBase<BgJiseki>();

            Params.Add(new DBParameter("MS_VESSEL_TYPE_ID", msVesselTypeId));
            Params.Add(new DBParameter("JISEKI_DATE_START", yearStart));
            Params.Add(new DBParameter("JISEKI_DATE_END", yearEnd));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }



        public static List<BgJiseki> GetRecords_月単位_全社(MsUser loginUser, string yearStart, string yearEnd)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgJiseki), MethodBase.GetCurrentMethod());

            List<BgJiseki> ret = new List<BgJiseki>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgJiseki> mapping = new MappingBase<BgJiseki>();

            Params.Add(new DBParameter("JISEKI_DATE_START", yearStart));
            Params.Add(new DBParameter("JISEKI_DATE_END", yearEnd));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<BgJiseki> GetRecords_月単位_グループ(MsUser loginUser, string msVesselTypeId, string yearStart, string yearEnd)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgJiseki), MethodBase.GetCurrentMethod());

            List<BgJiseki> ret = new List<BgJiseki>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgJiseki> mapping = new MappingBase<BgJiseki>();

            Params.Add(new DBParameter("MS_VESSEL_TYPE_ID", msVesselTypeId));
            Params.Add(new DBParameter("JISEKI_DATE_START", yearStart));
            Params.Add(new DBParameter("JISEKI_DATE_END", yearEnd));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
    }   
}
