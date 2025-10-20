using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ORMapping.Atts;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using NBaseData.DS;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("BG_KADOU_VESSEL")]
    public class BgKadouVessel : IGenericCloneable<BgKadouVessel>
    {
        #region データメンバ
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("KADOU_VESSEL_ID")]
        public int KadouVesselID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("YOSAN_HEAD_ID")]
        public int YosanHeadID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("KADOU_START_DATE")]
        public DateTime KadouStartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("KADOU_END_DATE")]
        public DateTime KadouEndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("NYUKYO_KIND")]
        public string NyukyoKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("NYUKYO_MONTH")]
        public int NyukyoMonth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("FUKADOUBI_1")]
        public decimal Fukadoubi1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("FUKADOUBI_2")]
        public decimal Fukadoubi2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("FUKADOUBI_3")]
        public decimal Fukadoubi3 { get; set; }


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
        /// 船名
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_NAME")]
        public string VesselName { get; set; }


        /// <summary>
        /// 年度
        /// </summary>
        [DataMember]
        [ColumnAttribute("YEAR")]
        public int Year { get; set; }


        /// <summary>
        /// 営業基礎割掛フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("EIGYOU_KISO_FLAG")]
        public int EigyouKisoFlag { get; set; }


        /// <summary>
        /// 管理基礎割掛フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("KANRI_KISO_FLAG")]
        public int KanriKisoFlag { get; set; }
        #endregion


        public static List<BgKadouVessel> GetRecords(MsUser loginUser)
        {
            List<BgKadouVessel> ret = new List<BgKadouVessel>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "ByAll");
            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgKadouVessel> mapping = new MappingBase<BgKadouVessel>();

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        //船マスタ使用チェック用
        public static List<BgKadouVessel> GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id)
        {
            List<BgKadouVessel> ret = new List<BgKadouVessel>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "GetRecordsByMsVesselID");
           
            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgKadouVessel> mapping = new MappingBase<BgKadouVessel>();

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<BgKadouVessel> GetRecordsByYosanHeadID(MsUser loginUser, int yosanHeadID)
        {
            return GetRecordsByYosanHeadID(null, loginUser, yosanHeadID);
        }


        public static List<BgKadouVessel> GetRecordsByYosanHeadID(DBConnect dbConnect, MsUser loginUser, int yosanHeadID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "ByYosanHeadID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "OrderBy");

            List<BgKadouVessel> ret = new List<BgKadouVessel>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgKadouVessel> mapping = new MappingBase<BgKadouVessel>();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanHeadID));

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<BgKadouVessel> GetRecordsByYosanHeadIDAndMsVesselID(MsUser loginUser, int yosanHeadID, int msVesselID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "ByYosanHeadID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "ByMsVesselID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "OrderBy");

            List<BgKadouVessel> ret = new List<BgKadouVessel>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgKadouVessel> mapping = new MappingBase<BgKadouVessel>();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanHeadID));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselID));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<BgKadouVessel> GetRecordsByYosanHeadIDAndMsVesselIDAndYearRange(MsUser loginUser, int yosanHeadID, int msVesselID,
            int yearStart, int yearEnd)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "ByYosanHeadID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "ByMsVesselID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "ByYearRange");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "OrderBy");

            List<BgKadouVessel> ret = new List<BgKadouVessel>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgKadouVessel> mapping = new MappingBase<BgKadouVessel>();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanHeadID));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselID));
            Params.Add(new DBParameter("YEAR_START", yearStart));
            Params.Add(new DBParameter("YEAR_END", yearEnd));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static BgKadouVessel GetRecord(MsUser loginUser, int KadouVesselID)
        {
            List<BgKadouVessel> ret = new List<BgKadouVessel>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "ByKadouVessel");
            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgKadouVessel> mapping = new MappingBase<BgKadouVessel>();

            Params.Add(new DBParameter("KADOU_VESSEL_ID", KadouVesselID));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }


		/// <summary>
		/// 予算頭と年を指定してデータを取得
		/// </summary>
		/// <param name="loginUser"></param>
		/// <param name="yosanhead"></param>
		/// <param name="year"></param>
		/// <returns></returns>
		public static List<BgKadouVessel> GetRecordsByYosanHeadAndYearRange(
			MsUser loginUser, int yosanhead, int yearStart, int yearEnd)
		{
			string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "ByYosanHeadID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "ByYearRange");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), "OrderBy");

			List<BgKadouVessel> ret = new List<BgKadouVessel>();

			ParameterConnection Params = new ParameterConnection();
			MappingBase<BgKadouVessel> mapping = new MappingBase<BgKadouVessel>();

			Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanhead));
			Params.Add(new DBParameter("YEAR_START", yearStart));
            Params.Add(new DBParameter("YEAR_END", yearEnd));

			ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

			return ret;
		}

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
        public bool InsertRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("YOSAN_HEAD_ID", this.YosanHeadID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));
            Params.Add(new DBParameter("YEAR", this.Year));
            Params.Add(new DBParameter("KADOU_START_DATE", this.KadouStartDate));
            Params.Add(new DBParameter("KADOU_END_DATE", this.KadouEndDate));
            Params.Add(new DBParameter("NYUKYO_KIND", this.NyukyoKind));
            Params.Add(new DBParameter("NYUKYO_MONTH", this.NyukyoMonth));
            Params.Add(new DBParameter("FUKADOUBI_1", this.Fukadoubi1));
            Params.Add(new DBParameter("FUKADOUBI_2", this.Fukadoubi2));
            Params.Add(new DBParameter("FUKADOUBI_3", this.Fukadoubi3));
            Params.Add(new DBParameter("EIGYOU_KISO_FLAG", this.EigyouKisoFlag));
            Params.Add(new DBParameter("KANRI_KISO_FLAG", this.KanriKisoFlag));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));
            Params.Add(new DBParameter("TS", this.Ts));

            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }


        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }


        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", this.YosanHeadID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));
            Params.Add(new DBParameter("YEAR", this.Year));
            Params.Add(new DBParameter("KADOU_START_DATE", this.KadouStartDate));
            Params.Add(new DBParameter("KADOU_END_DATE", this.KadouEndDate));
            Params.Add(new DBParameter("NYUKYO_KIND", this.NyukyoKind));
            Params.Add(new DBParameter("NYUKYO_MONTH", this.NyukyoMonth));
            Params.Add(new DBParameter("FUKADOUBI_1", this.Fukadoubi1));
            Params.Add(new DBParameter("FUKADOUBI_2", this.Fukadoubi2));
            Params.Add(new DBParameter("FUKADOUBI_3", this.Fukadoubi3));
            Params.Add(new DBParameter("EIGYOU_KISO_FLAG", this.EigyouKisoFlag));
            Params.Add(new DBParameter("KANRI_KISO_FLAG", this.KanriKisoFlag));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));

            Params.Add(new DBParameter("KADOU_VESSEL_ID", this.KadouVesselID));
            Params.Add(new DBParameter("TS", this.Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        
        public static bool UpdateRecords(MsUser loginUser, List<BgKadouVessel> kadouVessels)
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    foreach (BgKadouVessel item in kadouVessels)
                    {
                        item.UpdateRecord(dbConnect, loginUser);
                    }

                    dbConnect.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    dbConnect.RollBack();
                    return false;
                }
            }
        }


        public static bool InsertRecords_コピー(DBConnect dbConnect, MsUser loginUser,
                                                                    int yosanHeadID, int lastYosanHeadId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgKadouVessel), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanHeadID));
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("LAST_YOSAN_HEAD_ID", lastYosanHeadId));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        #region IGenericCloneable<BgKadouVessel> メンバ

        public BgKadouVessel Clone()
        {
            BgKadouVessel clone = new BgKadouVessel();

            clone.KadouVesselID = KadouVesselID;
            clone.YosanHeadID = YosanHeadID;
            clone.MsVesselID = MsVesselID;
            clone.KadouStartDate = KadouStartDate;
            clone.KadouEndDate = KadouEndDate;
            clone.NyukyoKind = NyukyoKind;
            clone.NyukyoMonth = NyukyoMonth;
            clone.Fukadoubi1 = Fukadoubi1;
            clone.Fukadoubi2 = Fukadoubi2;
            clone.Fukadoubi3 = Fukadoubi3;
            clone.EigyouKisoFlag = EigyouKisoFlag;
            clone.KanriKisoFlag = KanriKisoFlag;
            clone.RenewDate = RenewDate;
            clone.RenewUserID = RenewUserID;
            clone.Ts = Ts;
            clone.VesselName = VesselName;
            clone.Year = Year;

            return clone;
        }

        #endregion
    }
}
