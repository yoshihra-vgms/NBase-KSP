using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ORMapping.Atts;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("BG_UNKOUHI")]
    public class BgUnkouhi
    {
        #region データメンバ
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("BG_UNKOUHI_ID")]
        public long BgUnkouhiID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_YOSAN_ID")]
        public int VesselYosanID { get; set; }

        /// <summary>
        /// 運航費オブジェクト
        /// </summary>
        [DataMember]
        [ColumnAttribute("OBJECT_DATA")]
        public byte[] ObjectData { get; set; }

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
        /// 年度 (JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("YEAR")]
        public int Year { get; set; }
        #endregion


        public static List<BgUnkouhi> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgUnkouhi), MethodBase.GetCurrentMethod());

            List<BgUnkouhi> ret = new List<BgUnkouhi>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgUnkouhi> mapping = new MappingBase<BgUnkouhi>();


            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static BgUnkouhi GetRecordByYosanHeadIdAndMsVesselIdAndYear(MsUser loginUser, int yosanHeadId, int msVesselId, int year)
        {
            return GetRecordByYosanHeadIdAndMsVesselIdAndYear(null, loginUser, yosanHeadId, msVesselId, year);
        }


        public static BgUnkouhi GetRecordByYosanHeadIdAndMsVesselIdAndYear(DBConnect dbConnect, MsUser loginUser, int yosanHeadId, int msVesselId, int year)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgUnkouhi), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgUnkouhi), "ByYosanHeadIdAndMsVesselIdAndYear");

            List<BgUnkouhi> ret = new List<BgUnkouhi>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgUnkouhi> mapping = new MappingBase<BgUnkouhi>();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanHeadId));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            Params.Add(new DBParameter("YEAR", year));

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }


        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgUnkouhi), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("VESSEL_YOSAN_ID", this.VesselYosanID));
            Params.Add(new DBParameter("OBJECT_DATA", this.ObjectData));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgUnkouhi), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("VESSEL_YOSAN_ID", this.VesselYosanID));
            Params.Add(new DBParameter("OBJECT_DATA", this.ObjectData));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));

            Params.Add(new DBParameter("BG_UNKOUHI_ID", this.BgUnkouhiID));
            Params.Add(new DBParameter("TS", this.Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }


        internal static bool UpdateRecords_コピー(DBConnect dbConnect, MsUser loginUser, int yosanHeadId, int msVesselId, int year, byte[] objectData)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgUnkouhi), MethodBase.GetCurrentMethod());

            List<BgUnkouhi> ret = new List<BgUnkouhi>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgUnkouhi> mapping = new MappingBase<BgUnkouhi>();

            Params.Add(new DBParameter("OBJECT_DATA", objectData));

            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanHeadId));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            Params.Add(new DBParameter("YEAR", year));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgUnkouhi), MethodBase.GetCurrentMethod());

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


        public static bool ExistsLastRecord(DBConnect dbConnect, MsUser loginUser, int lastYosanHeadId, int msVesselId, int vesselYosanYear)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgUnkouhi), MethodBase.GetCurrentMethod());

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
