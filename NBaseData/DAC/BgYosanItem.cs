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
    [TableAttribute("BG_YOSAN_ITEM")]
    public class BgYosanItem : IYojitsu
    {
         //YOSAN_ITEM_ID NUMBER(12) NOT NULL,
         //VESSEL_YOSAN_ID NUMBER(9),
         //NENGETSU NUMBER(9),
         //SHOW_ORDER NUMBER(4),
         //MS_HIMOKU_ID NUMBER(9),
         //YEN_AMOUNT NUMBER(16,3),
         //DOLLER_AMOUNT NUMBER(16,3),
         //AMOUNT NUMBER(16,3) NOT NULL,
         //BIKOU NVARCHAR2(50),
         //RENEW_DATE DATE NOT NULL,
         //RENEW_USER_ID VARCHAR2(40) NOT NULL,
         //TS VARCHAR2(20)
        #region データメンバ

        /// <summary>
        /// 予算項目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("YOSAN_ITEM_ID")]
        public long YsanItemID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_YOSAN_ID")]
        public int VesselYosanID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("NENGETSU")]
        public string Nengetsu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_HIMOKU_ID")]
        public int MsHimokuID { get; set; }

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
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }


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

        public BgYosanItem()
        {
        }
        ////////////////////////////////////////////////////
        public static List<BgYosanItem> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanItem), MethodBase.GetCurrentMethod());

            List<BgYosanItem> ret = new List<BgYosanItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgYosanItem> mapping = new MappingBase<BgYosanItem>();


            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static BgYosanItem GetRecord(MsUser loginUser, long yosan_item_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanItem), MethodBase.GetCurrentMethod());

            List<BgYosanItem> ret = new List<BgYosanItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgYosanItem> mapping = new MappingBase<BgYosanItem>();

            //取得条件設定
            Params.Add(new DBParameter("YOSAN_ITEM_ID", yosan_item_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }

        
        public static List<BgYosanItem> GetRecords_年単位_船(MsUser loginUser, int yosanHeadId, int msVesselId, string yearStart, string yearEnd)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanItem), MethodBase.GetCurrentMethod());

            List<BgYosanItem> ret = new List<BgYosanItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgYosanItem> mapping = new MappingBase<BgYosanItem>();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanHeadId));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            Params.Add(new DBParameter("YEAR_START", yearStart));
            Params.Add(new DBParameter("YEAR_END", yearEnd));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<BgYosanItem> GetRecords_月単位(MsUser loginUser, int yosanHeadId, int msVesselId, string yearStart, string yearEnd)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanItem), MethodBase.GetCurrentMethod());

            List<BgYosanItem> ret = new List<BgYosanItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgYosanItem> mapping = new MappingBase<BgYosanItem>();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanHeadId));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            Params.Add(new DBParameter("YEAR_START", yearStart));
            Params.Add(new DBParameter("YEAR_END", yearEnd));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<BgYosanItem> GetRecords_月単位_グループ(MsUser loginUser, int yosanHeadId, string msVesselTypeId, string yearStart, string yearEnd)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanItem), MethodBase.GetCurrentMethod());

            List<BgYosanItem> ret = new List<BgYosanItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgYosanItem> mapping = new MappingBase<BgYosanItem>();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanHeadId));
            Params.Add(new DBParameter("MS_VESSEL_TYPE_ID", msVesselTypeId));
            Params.Add(new DBParameter("YEAR_START", yearStart));
            Params.Add(new DBParameter("YEAR_END", yearEnd));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<BgYosanItem> GetRecords_月単位_全社(MsUser loginUser, int yosanHeadId, string yearStart, string yearEnd)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanItem), MethodBase.GetCurrentMethod());

            List<BgYosanItem> ret = new List<BgYosanItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgYosanItem> mapping = new MappingBase<BgYosanItem>();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanHeadId));
            Params.Add(new DBParameter("YEAR_START", yearStart));
            Params.Add(new DBParameter("YEAR_END", yearEnd));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        /// <summary>
        /// 年月と船IDとHIMOKU_IDでデータを取得する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="year"></param>
        /// <param name="himokuid"></param>
        /// <param name="vesselid"></param>
        /// <returns></returns>
        public static BgYosanItem GetRecordByYearHimokuIDMsVesselID(MsUser loginUser, int yosanheadid, int year, int himokuid, int vesselid) 
        {
            return GetRecordByYearHimokuIDMsVesselID(null, loginUser, yosanheadid, year, himokuid, vesselid);
        }


        /// <summary>
        /// 年月と船IDとHIMOKU_IDでデータを取得する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="year"></param>
        /// <param name="himokuid"></param>
        /// <param name="vesselid"></param>
        /// <returns></returns>
        public static BgYosanItem GetRecordByYearHimokuIDMsVesselID(DBConnect dbConnect, MsUser loginUser, int yosanheadid, int year, int himokuid, int vesselid)
        {
            return GetRecordByYearHimokuIDMsVesselID(dbConnect, loginUser, yosanheadid, year, -1, himokuid, vesselid);
        }
        public static BgYosanItem GetRecordByYearHimokuIDMsVesselID(MsUser loginUser, int yosanheadid, int year, int month, int himokuid, int vesselid)
        {
            return GetRecordByYearHimokuIDMsVesselID(null, loginUser, yosanheadid, year, month, himokuid, vesselid);
        }
        public static BgYosanItem GetRecordByYearHimokuIDMsVesselID(DBConnect dbConnect, MsUser loginUser, int yosanheadid, int year, int month, int himokuid, int vesselid)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanItem), MethodBase.GetCurrentMethod());

            List<BgYosanItem> ret = new List<BgYosanItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgYosanItem> mapping = new MappingBase<BgYosanItem>();

            //取得条件設定
            if (month == -1)
            {
                Params.Add(new DBParameter("NENGETSU", year.ToString()));
            }
            else
            {
                Params.Add(new DBParameter("NENGETSU", year.ToString() + month.ToString("00")));
            }
            Params.Add(new DBParameter("HIMOKU_ID", himokuid));
            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanheadid));
            Params.Add(new DBParameter("MSVESSEL_ID", vesselid));

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }



		/// <summary>
		/// 船タイプごとの合計を費目別に取得する
		/// 条件：予算頭id、期間、船タイプ、部門ＩＤ
		/// </summary>
		/// <param name="loginUser"></param>
		/// <param name="yosanheadid"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="vesseltype"></param>
		/// <returns></returns>
		public static List<BgYosanItem> GetRecordsByYosanHeadPriodVesselTypeBumonIDHimokus(
			MsUser loginUser, int yosanheadid, string start, string end, string vesseltype, int bumonid)
		{
			string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanItem), MethodBase.GetCurrentMethod());

			List<BgYosanItem> ret = new List<BgYosanItem>();

			/*条件追加*/
			ParameterConnection Params = new ParameterConnection();
			MappingBase<BgYosanItem> mapping = new MappingBase<BgYosanItem>();

			Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanheadid));
			Params.Add(new DBParameter("STAET_DATE", start));
			Params.Add(new DBParameter("END_DATE", end));
			Params.Add(new DBParameter("TYPE_ID", vesseltype));
			Params.Add(new DBParameter("BUMON_ID", bumonid));

			ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

			return ret;
		}



		/// <summary>
		/// 船タイプごとの合計を費目別に取得する
		/// 条件：予算頭id、期間、船タイプ
		/// </summary>
		/// <param name="loginUser"></param>
		/// <param name="yosanheadid"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="vesseltype"></param>
		/// <returns></returns>
		public static List<BgYosanItem> GetRecordsByYosanHeadPriodVesselTypeHimokus(
			MsUser loginUser, int yosanheadid, string start, string end, string vesseltype)
		{
			string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanItem), MethodBase.GetCurrentMethod());

			List<BgYosanItem> ret = new List<BgYosanItem>();

			/*条件追加*/
			ParameterConnection Params = new ParameterConnection();
			MappingBase<BgYosanItem> mapping = new MappingBase<BgYosanItem>();

			Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanheadid));
			Params.Add(new DBParameter("STAET_DATE", start));
			Params.Add(new DBParameter("END_DATE", end));
			Params.Add(new DBParameter("TYPE_ID", vesseltype));


			ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

			return ret;
		}


        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanItem), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("VESSEL_YOSAN_ID", this.VesselYosanID));
            Params.Add(new DBParameter("NENGETSU", this.Nengetsu));
            Params.Add(new DBParameter("MS_HIMOKU_ID", this.MsHimokuID));
            Params.Add(new DBParameter("YEN_AMOUNT", this.YenAmount));
            Params.Add(new DBParameter("DOLLER_AMOUNT", this.DollerAmount));
            Params.Add(new DBParameter("AMOUNT", this.Amount));
            Params.Add(new DBParameter("BIKOU", this.Bikou));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanItem), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("VESSEL_YOSAN_ID", this.VesselYosanID));
            Params.Add(new DBParameter("NENGETSU", this.Nengetsu));
            Params.Add(new DBParameter("MS_HIMOKU_ID", this.MsHimokuID));
            Params.Add(new DBParameter("YEN_AMOUNT", this.YenAmount));
            Params.Add(new DBParameter("DOLLER_AMOUNT", this.DollerAmount));
            Params.Add(new DBParameter("AMOUNT", this.Amount));
            Params.Add(new DBParameter("BIKOU", this.Bikou));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));            

            Params.Add(new DBParameter("YOSAN_ITEM_ID", this.YsanItemID));
            Params.Add(new DBParameter("TS", this.Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        
        public static bool UpdateRecords(MsUser loginUser, List<BgYosanItem> yosanItems)
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    foreach (BgYosanItem item in yosanItems)
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

        
        public static List<BgYosanItem> GetRecords_年単位_全社(MsUser loginUser, int yosanHeadId, string yearStart, string yearEnd)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanItem), MethodBase.GetCurrentMethod());

            List<BgYosanItem> ret = new List<BgYosanItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgYosanItem> mapping = new MappingBase<BgYosanItem>();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanHeadId));
            Params.Add(new DBParameter("YEAR_START", yearStart));
            Params.Add(new DBParameter("YEAR_END", yearEnd));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        
        public static List<BgYosanItem> GetRecords_年単位_グループ(MsUser loginUser, int yosanHeadId, string msVesselTypeId, string yearStart, string yearEnd)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanItem), MethodBase.GetCurrentMethod());

            List<BgYosanItem> ret = new List<BgYosanItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgYosanItem> mapping = new MappingBase<BgYosanItem>();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanHeadId));
            Params.Add(new DBParameter("MS_VESSEL_TYPE_ID", msVesselTypeId));
            Params.Add(new DBParameter("YEAR_START", yearStart));
            Params.Add(new DBParameter("YEAR_END", yearEnd));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static bool InsertRecords_新規(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int vesselYosanId, string nengetsu)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanItem), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("VESSEL_YOSAN_ID", vesselYosanId));
            Params.Add(new DBParameter("NENGETSU", nengetsu));
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }


        public static bool InsertRecords_コピー(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser,
                                                                        int vesselYosanId, int lastYosanHeadId,
                                                                        int msVesselId, int vesselYosanYear)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanItem), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("VESSEL_YOSAN_ID", vesselYosanId));
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("LAST_YOSAN_HEAD_ID", lastYosanHeadId));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            Params.Add(new DBParameter("VESSEL_YOSAN_YEAR", vesselYosanYear));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }


        public static bool ExistsLastRecords(DBConnect dbConnect, MsUser loginUser, int lastYosanHeadId, int msVesselId, int vesselYosanYear)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanItem), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("LAST_YOSAN_HEAD_ID", lastYosanHeadId));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            Params.Add(new DBParameter("VESSEL_YOSAN_YEAR", vesselYosanYear));
            #endregion

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }
    }
}
