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
    [TableAttribute("MS_SI_DAIKOUMOKU")]
    public class MsSiDaikoumoku : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 大項目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_DAIKOUMOKU_ID", true)]
        public int MsSiDaikoumokuID { get; set; }

        
        
        
        /// <summary>
        /// 費用科目ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_HIYOU_KAMOKU_ID")]
        public int MsSiHiyouKamokuID { get; set; }

        
        
        
        /// <summary>
        /// 大項目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME")]
        public string Name { get; set; }

        
        
        
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
        /// 費用科目名 (JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("HIYOU_KAMOKU_NAME")]
        public string HiyouKamokuName { get; set; }
        #endregion




        public static List<MsSiDaikoumoku> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiDaikoumoku), MethodBase.GetCurrentMethod());

            List<MsSiDaikoumoku> ret = new List<MsSiDaikoumoku>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiDaikoumoku> mapping = new MappingBase<MsSiDaikoumoku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


		public static List<MsSiDaikoumoku> SearchRecords(MsUser loginUser, string name, MsSiHiyouKamoku ms_sihi)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiDaikoumoku), "GetRecords");

            List<MsSiDaikoumoku> ret = new List<MsSiDaikoumoku>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiDaikoumoku> mapping = new MappingBase<MsSiDaikoumoku>();

            if (name != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiDaikoumoku), "SearchByName");
                Params.Add(new DBParameter("NAME", "%" + name + "%"));
            }

			//費用科目も検索する時は条件を追加する。
			if (ms_sihi != null)
			{
				SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiDaikoumoku), "SearchByHiyouKamoku");
				Params.Add(new DBParameter("MS_SI_HIYOU_KAMOKU_ID", ms_sihi.MsSiHiyouKamokuID));
			}

			SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiDaikoumoku), "OrderByName");

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<MsSiDaikoumoku> GetRecordsByMsSiHiyouKamokuID(MsUser loginUser, int ms_sihi_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiDaikoumoku), "GetRecords");

            List<MsSiDaikoumoku> ret = new List<MsSiDaikoumoku>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiDaikoumoku> mapping = new MappingBase<MsSiDaikoumoku>();

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiDaikoumoku), "SearchByHiyouKamoku");
            Params.Add(new DBParameter("MS_SI_HIYOU_KAMOKU_ID", ms_sihi_id));



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
                Params.Add(new DBParameter("MS_SI_DAIKOUMOKU_ID", MsSiDaikoumokuID));
            }

            Params.Add(new DBParameter("MS_SI_HIYOU_KAMOKU_ID", MsSiHiyouKamokuID));

            Params.Add(new DBParameter("NAME", Name));

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

            Params.Add(new DBParameter("MS_SI_HIYOU_KAMOKU_ID", MsSiHiyouKamokuID));

            Params.Add(new DBParameter("NAME", Name));
            
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_SI_DAIKOUMOKU_ID", MsSiDaikoumokuID));
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
        //    Params.Add(new DBParameter("PK", MsSiDaikoumokuID));

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
            Params.Add(new DBParameter("PK", MsSiDaikoumokuID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        
        public bool IsNew()
        {
            return MsSiDaikoumokuID == 0;
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
