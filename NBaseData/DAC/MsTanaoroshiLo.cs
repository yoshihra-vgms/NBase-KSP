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

namespace NBaseData.DAC
{
    [DataContract()]
    public class MsTanaoroshiLo
    {
        #region データメンバ

    //MS_TANAOROSHI_LO_ID            VARCHAR2(40) NOT NULL,
    //MS_VESSEL_ID                   NUMBER(4,0),
    //MS_LO_ID                       VARCHAR2(40),
    //SEND_FLAG                      NUMBER(1,0) DEFAULT 0 NOT NULL,
    //VESSEL_ID                      NUMBER(4,0) NOT NULL,
    //DATA_NO                        NUMBER(13,0),
    //USER_KEY                       VARCHAR2(40) DEFAULT 0,
    //RENEW_DATE                     DATE NOT NULL,
    //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
    //TS                             TIMESTAMP(6),

        /// <summary>
        /// 棚卸潤滑油ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_TANAOROSHI_LO_ID")]
        public string MsTanaoroshiLoID { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 潤滑油ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_LO_ID")]
        public string MsLoID { get; set; }
        
        
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
        public decimal DataNo { get; set; }

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
        /// 船名
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_NAME")]
        public string VesselName { get; set; }

        /// <summary>
        /// 潤滑油名
        /// </summary>
        [DataMember]
        [ColumnAttribute("LO_NAME")]
        public string LoName { get; set; }

        #endregion

        public MsTanaoroshiLo()
        {
        }

        public static List<MsTanaoroshiLo> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsTanaoroshiLo), MethodBase.GetCurrentMethod());
            List<MsTanaoroshiLo> ret = new List<MsTanaoroshiLo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsTanaoroshiLo> mapping = new MappingBase<MsTanaoroshiLo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsTanaoroshiLo> GetRecord(NBaseData.DAC.MsUser loginUser, string MsTanaoroshiLoID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsTanaoroshiLo), MethodBase.GetCurrentMethod());
            List<MsTanaoroshiLo> ret = new List<MsTanaoroshiLo>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_TANAOROSHI_LO_ID", MsTanaoroshiLoID));
            MappingBase<MsTanaoroshiLo> mapping = new MappingBase<MsTanaoroshiLo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsTanaoroshiLo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_TANAOROSHI_LO_ID", MsTanaoroshiLoID));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_LO_ID", MsLoID));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsTanaoroshiLo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_LO_ID", MsLoID)); 

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_TANAOROSHI_LO_ID", MsTanaoroshiLoID));

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
    }
}
