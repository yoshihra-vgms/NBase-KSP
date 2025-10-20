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
    [TableAttribute("MS_SI_MEISAI")]
    public class MsSiMeisai : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 明細ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_MEISAI_ID", true)]
        public int MsSiMeisaiID { get; set; }

        
        
        
        /// <summary>
        /// 大項目ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_DAIKOUMOKU_ID")]
        public int MsSiDaikoumokuID { get; set; }

        /// <summary>
        /// 船員科目ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_KAMOKU_ID")]
        public int MsSiKamokuId { get; set; }

        
        
        
        /// <summary>
        /// 明細名
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 明細入力No
        /// </summary>
        [DataMember]
        [ColumnAttribute("NYUURYOKU_NO")]
        public int NyuuryokuNo { get; set; }

        /// <summary>
        /// 貸方借方フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("KASHIKARI_FLAG")]
        public int KashiKariFlag { get; set; }

        
        
        
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




        /// <summary>
        /// 費用科目ID (JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_HIYOU_KAMOKU_ID")]
        public int MsSiHiyouKamokuID { get; set; }

        /// <summary>
        /// 費用科目名 (JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("HIYOU_KAMOKU_NAME")]
        public string HiyouKamokuName { get; set; }

        /// <summary>
        /// 大項目名 (JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("DAIKOUMOKU_NAME")]
        public string DaikoumokuName { get; set; }

        /// <summary>
        /// 明細科目名 (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("KAMOKU_NAME")]
        public string KamokuName { get; set; }
        #endregion




        public static List<MsSiMeisai> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiMeisai), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMeisai), "_削除を除く");
            List<MsSiMeisai> ret = new List<MsSiMeisai>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiMeisai> mapping = new MappingBase<MsSiMeisai>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsSiMeisai> GetRecords削除を含む(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiMeisai), "GetRecords");
            List<MsSiMeisai> ret = new List<MsSiMeisai>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiMeisai> mapping = new MappingBase<MsSiMeisai>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsSiMeisai> GetRecordsByMsSiKamokuID(MsUser loginUser, int ms_sikamoku_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiMeisai), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMeisai), "_削除を除く");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMeisai), "ByMsSiKamokuID");

            List<MsSiMeisai> ret = new List<MsSiMeisai>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiMeisai> mapping = new MappingBase<MsSiMeisai>();
            Params.Add(new DBParameter("MS_SI_KAMOKU_ID", ms_sikamoku_id));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsSiMeisai GetRecord(MsUser loginUser, int msSiMeisaiId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiMeisai), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMeisai), "ByMsSiMeisaiID");

            List<MsSiMeisai> ret = new List<MsSiMeisai>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiMeisai> mapping = new MappingBase<MsSiMeisai>();
            Params.Add(new DBParameter("MS_SI_MEISAI_ID", msSiMeisaiId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }


		public static List<MsSiMeisai> SearchRecords(MsUser loginUser, string name, MsSiHiyouKamoku himo, MsSiDaikoumoku kou)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiMeisai), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMeisai), "_削除を除く");

            List<MsSiMeisai> ret = new List<MsSiMeisai>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiMeisai> mapping = new MappingBase<MsSiMeisai>();

            if (name != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMeisai), "SearchByName");
                Params.Add(new DBParameter("NAME", "%" + name + "%"));
            }
			//費用科目が検索条件に入っていた
			if (himo != null)
			{
				//検索条件の追加
				SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMeisai), "SearchByKamoku");
				Params.Add(new DBParameter("MS_SI_HIYOU_KAMOKU_ID", himo.MsSiHiyouKamokuID));
			}
			//大項目が検索条件に入っていた。
			if (kou != null)
			{
				//検索条件の追加
				SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMeisai), "SearchByDaikoumoku");
				Params.Add(new DBParameter("MS_SI_DAIKOUMOKU_ID", kou.MsSiDaikoumokuID));
			}
			SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMeisai), "OrderByName");

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        //大項目に関連しているものを取得する。
        public static List<MsSiMeisai> GetRecordsByMsSiDaikoumokuID(MsUser loginUser, int sidaikoumoku_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiMeisai), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMeisai), "_削除を除く");

            List<MsSiMeisai> ret = new List<MsSiMeisai>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiMeisai> mapping = new MappingBase<MsSiMeisai>();

            //検索条件
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMeisai), "SearchByDaikoumoku");
            Params.Add(new DBParameter("MS_SI_DAIKOUMOKU_ID", sidaikoumoku_id));
            


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

            if (!IsNew())
            {
                Params.Add(new DBParameter("MS_SI_MEISAI_ID", MsSiMeisaiID));
            }

            Params.Add(new DBParameter("MS_SI_DAIKOUMOKU_ID", MsSiDaikoumokuID));
            Params.Add(new DBParameter("MS_SI_KAMOKU_ID", MsSiKamokuId));

            Params.Add(new DBParameter("NAME", Name));
            Params.Add(new DBParameter("NYUURYOKU_NO", NyuuryokuNo));
            Params.Add(new DBParameter("KASHIKARI_FLAG", KashiKariFlag));

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

            Params.Add(new DBParameter("MS_SI_DAIKOUMOKU_ID", MsSiDaikoumokuID));
            Params.Add(new DBParameter("MS_SI_KAMOKU_ID", MsSiKamokuId));

            Params.Add(new DBParameter("NAME", Name));
            Params.Add(new DBParameter("NYUURYOKU_NO", NyuuryokuNo));
            Params.Add(new DBParameter("KASHIKARI_FLAG", KashiKariFlag));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_SI_MEISAI_ID", MsSiMeisaiID));
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
        //    Params.Add(new DBParameter("PK", MsSiMeisaiID));

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
            Params.Add(new DBParameter("PK", MsSiMeisaiID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return MsSiMeisaiID == 0;
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
