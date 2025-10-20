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
    [TableAttribute("BG_VESSEL_YOSAN")]
    public class BgVesselYosan
    {
        //VESSEL_YOSAN_ID                NUMBER(9,0) NOT NULL,
        //YEAR                           NUMBER(4,0) NOT NULL,
        //YOSAN_HEAD_ID                  NUMBER(9,0),
        //MS_VESSEL_ID                   NUMBER(4,0),
        
        
        //RENEW_DATE                     DATE NOT NULL,
        //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
        //TS                             VARCHAR2(20),
        #region データメンバ

        /// <summary>
        /// 船予算ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_YOSAN_ID")]
        public int VesselYosanID { get; set; }


        /// <summary>
        /// 年度
        /// </summary>
        [DataMember]
        [ColumnAttribute("YEAR")]
        public int Year { get; set; }


        /// <summary>
        /// 予算ヘッダーID
        /// </summary>
        [DataMember]
        [ColumnAttribute("YOSAN_HEAD_ID")]
        public int YosanHeadID { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }


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
        /// コンストラクタ
        /// </summary>
        public BgVesselYosan()
        {
        }

        //-----------------------------------------
        public static List<BgVesselYosan> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgVesselYosan), MethodBase.GetCurrentMethod());

            List<BgVesselYosan> ret = new List<BgVesselYosan>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgVesselYosan> mapping = new MappingBase<BgVesselYosan>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static BgVesselYosan GetRecordByYearAndYosanHeadIdAndMsVesselId(MsUser loginUser, int year, int yosanHeadId, int msVesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgVesselYosan), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgVesselYosan), "ByYearAndYosanHeadIdAndMsVesselId");

            List<BgVesselYosan> ret = new List<BgVesselYosan>();

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("YEAR", year));
            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanHeadId));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));

            MappingBase<BgVesselYosan> mapping = new MappingBase<BgVesselYosan>();

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }

        public static List<BgVesselYosan> GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgVesselYosan), MethodBase.GetCurrentMethod());

            List<BgVesselYosan> ret = new List<BgVesselYosan>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgVesselYosan> mapping = new MappingBase<BgVesselYosan>();

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static BgVesselYosan GetRecord(MsUser loginUser, int vessel_yosan_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgVesselYosan), MethodBase.GetCurrentMethod());

            List<BgVesselYosan> ret = new List<BgVesselYosan>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgVesselYosan> mapping = new MappingBase<BgVesselYosan>();

            Params.Add(new DBParameter("VESSEL_YOSAN_ID", vessel_yosan_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgVesselYosan), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("YEAR", this.Year));
            Params.Add(new DBParameter("YOSAN_HEAD_ID", this.YosanHeadID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgVesselYosan), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            
            Params.Add(new DBParameter("YEAR", this.Year));
            Params.Add(new DBParameter("YOSAN_HEAD_ID", this.YosanHeadID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));

            Params.Add(new DBParameter("VESSEL_YOSAN_ID", this.VesselYosanID));
            Params.Add(new DBParameter("TS", this.Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }
    }
}
