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
    [TableAttribute("SI_FELLOW_PASSENGERS")]
    public class SiFellowPassengers : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 乗り合わせID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_FELLOW_PASSENGERS_ID", true)]
        public string SiFellowPassengersID { get; set; }



        /// <summary>
        /// 職名ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_ID_1")]
        public int MsSiShokumeiID1 { get; set; }

        /// <summary>
        /// 船員ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID_1")]
        public int MsSeninID1 { get; set; }

        /// <summary>
        /// 職名ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_ID_2")]
        public int MsSiShokumeiID2 { get; set; }

        /// <summary>
        /// 船員ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID_2")]
        public int MsSeninID2 { get; set; }
       
        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }

        /// <summary>
        /// 船員氏名
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME_1")]
        public string Name1 { get; set; }

        /// <summary>
        /// 船員氏名
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME_2")]
        public string Name2 { get; set; }

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




        public SiFellowPassengers()
        {
            this.MsSiShokumeiID1 = Int32.MinValue;
            this.MsSeninID1 = Int32.MinValue;
            this.MsSiShokumeiID2 = Int32.MinValue;
            this.MsSeninID2 = Int32.MinValue;
        }

        
        

        public static List<SiFellowPassengers> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiFellowPassengers), MethodBase.GetCurrentMethod());

            List<SiFellowPassengers> ret = new List<SiFellowPassengers>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiFellowPassengers> mapping = new MappingBase<SiFellowPassengers>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<SiFellowPassengers> GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiFellowPassengers), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiFellowPassengers), "ByMsSeninID");

            List<SiFellowPassengers> ret = new List<SiFellowPassengers>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiFellowPassengers> mapping = new MappingBase<SiFellowPassengers>();
            Params.Add(new DBParameter("MS_SENIN_ID_1", msSeninId));
            Params.Add(new DBParameter("MS_SENIN_ID_2", msSeninId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<SiFellowPassengers> SearchRecords(MsUser loginUser, int msSiShokumeiId, string name)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiFellowPassengers), "GetRecords");

            if (msSiShokumeiId > 0)
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiFellowPassengers), "ByMsSiShokumeiID");

            if (name != null)
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiFellowPassengers), "ByName");

            List<SiFellowPassengers> ret = new List<SiFellowPassengers>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiFellowPassengers> mapping = new MappingBase<SiFellowPassengers>();

            if (msSiShokumeiId > 0)
            {
                Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID_1", msSiShokumeiId));
                Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID_2", msSiShokumeiId));
            }
            if (name != null)
            {
                Params.Add(new DBParameter("NAME", "%" + name + "%"));
            }

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

            Params.Add(new DBParameter("SI_FELLOW_PASSENGERS_ID", SiFellowPassengersID));

            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID_1", MsSiShokumeiID1));
            Params.Add(new DBParameter("MS_SENIN_ID_1", MsSeninID1));

            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID_2", MsSiShokumeiID2));
            Params.Add(new DBParameter("MS_SENIN_ID_2", MsSeninID2));
           
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

            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID_1", MsSiShokumeiID1));
            Params.Add(new DBParameter("MS_SENIN_ID_1", MsSeninID1));

            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID_2", MsSiShokumeiID2));
            Params.Add(new DBParameter("MS_SENIN_ID_2", MsSeninID2));

            Params.Add(new DBParameter("BIKOU", Bikou));


            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_FELLOW_PASSENGERS_ID", SiFellowPassengersID));
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
            Params.Add(new DBParameter("PK", SiFellowPassengersID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return SiFellowPassengersID == null;
        }
    }
}
