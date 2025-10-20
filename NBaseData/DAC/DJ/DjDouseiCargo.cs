using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ORMapping.Atts;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using NBaseData.DAC;
using NBaseData.DS;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("DJ_DOUSEI_CARGO")]
    public class DjDouseiCargo : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 動静貨物ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("DJ_DOUSEI_CARGO_ID", true)]
        public string DjDouseiCargoID { get; set; }

        /// <summary>
        /// 動静情報ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("DJ_DOUSEI_ID")]
        public string DjDouseiID { get; set; }

        /// <summary>
        /// 貨物ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CARGO_ID")]
        public int MsCargoID { get; set; }

        /// <summary>
        /// 貨物NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("CARGO_NO")]
        public string MsCargoNo { get; set; }


        /// <summary>
        /// 貨物名
        /// </summary>
        [DataMember]
        [ColumnAttribute("CARGO_NAME")]
        public string MsCargoName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        [ColumnAttribute("QTTY")]
        public decimal Qtty { get; set; }

        /// <summary>
        /// 行番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("LINE_NO")]
        public string LineNo { get; set; }
      
        /// <summary>
        /// 単位ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DJ_TANI_ID")]
        public string MsDjTaniID { get; set; }

        /// <summary>
        /// 予定実績フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("PLAN_RESULT_FLAG")]
        public int PlanResultFlag { get; set; }

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

        public enum PLAN_RESULT_FLAG { PLAN, RESULT };

        public static List<DjDouseiCargo> GetRecords(MsUser loginUser, DjDousei djDousei)
        {
            return GetRecords(null, loginUser, djDousei);
        }
        public static List<DjDouseiCargo> GetRecords(DBConnect dbConnect, MsUser loginUser,DjDousei djDousei)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDouseiCargo), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDouseiCargo), "Where");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDouseiCargo), "OrderBy");

            List<DjDouseiCargo> ret = new List<DjDouseiCargo>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("DJ_DOUSEI_ID", djDousei.DjDouseiID));

            MappingBase<DjDouseiCargo> mapping = new MappingBase<DjDouseiCargo>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<DjDouseiCargo> GetRecordsMsCargoID(MsUser loginUser, int ms_cargo_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDouseiCargo), MethodBase.GetCurrentMethod());
  

            List<DjDouseiCargo> ret = new List<DjDouseiCargo>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_CARGO_ID", ms_cargo_id));

            MappingBase<DjDouseiCargo> mapping = new MappingBase<DjDouseiCargo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDouseiCargo), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("DJ_DOUSEI_CARGO_ID", DjDouseiCargoID));
            Params.Add(new DBParameter("DJ_DOUSEI_ID", DjDouseiID));
            Params.Add(new DBParameter("MS_CARGO_ID", MsCargoID));
            Params.Add(new DBParameter("QTTY", Qtty));
            Params.Add(new DBParameter("LINE_NO", LineNo));
            Params.Add(new DBParameter("MS_DJ_TANI_ID", MsDjTaniID));
            Params.Add(new DBParameter("PLAN_RESULT_FLAG", PlanResultFlag));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect,loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        public bool UpdateRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDouseiCargo), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("DJ_DOUSEI_ID", DjDouseiID));
            Params.Add(new DBParameter("MS_CARGO_ID", MsCargoID));
            Params.Add(new DBParameter("QTTY", Qtty));
            Params.Add(new DBParameter("LINE_NO", LineNo));
            Params.Add(new DBParameter("MS_DJ_TANI_ID", MsDjTaniID));
            Params.Add(new DBParameter("PLAN_RESULT_FLAG", PlanResultFlag));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("DJ_DOUSEI_CARGO_ID", DjDouseiCargoID));
            Params.Add(new DBParameter("TS", Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect,loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }
        public bool DeleteRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDouseiCargo), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("DJ_DOUSEI_CARGO_ID", DjDouseiCargoID));
            Params.Add(new DBParameter("TS", Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;

        }

        public static bool DeleteRecords(MsUser loginUser, DBConnect dbConnect, DjDousei djDousei)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDouseiCargo), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));
            Params.Add(new DBParameter("DJ_DOUSEI_ID", djDousei.DjDouseiID));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;

        }

        public static bool DeleteByVoyageNo(DBConnect dbConnect, MsUser loginUser, int msVesselID, string voyageNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDouseiCargo), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselID));
            Params.Add(new DBParameter("VOYAGE_NO", voyageNo));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;

        }

        public static List<DjDouseiCargo> GetRecordsByVoyageNo(DBConnect dbConnect, MsUser loginUser, int msVesselID, string voyageNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDouseiCargo), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDouseiCargo), "ByVoyageNo");

            List<DjDouseiCargo> ret = new List<DjDouseiCargo>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselID));
            Params.Add(new DBParameter("VOYAGE_NO", voyageNo));

            MappingBase<DjDouseiCargo> mapping = new MappingBase<DjDouseiCargo>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }
        public static DjDouseiCargo GetRecordsByDjDouseiIdAtFirst(MsUser loginUser, string djDouseiID)
        {
            return GetRecordsByDjDouseiIdAtFirst(null, loginUser, djDouseiID);
        }

        public static DjDouseiCargo GetRecordsByDjDouseiIdAtFirst(DBConnect dbConnect, MsUser loginUser, string djDouseiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDouseiCargo), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDouseiCargo), "ByDjDouseiId");

            List<DjDouseiCargo> ret = new List<DjDouseiCargo>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("DJ_DOUSEI_ID", djDouseiID));

            MappingBase<DjDouseiCargo> mapping = new MappingBase<DjDouseiCargo>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count > 0)
                return ret[0];
            else
                return null;
        }


        #region ISyncTable メンバ

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", DjDouseiCargoID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
