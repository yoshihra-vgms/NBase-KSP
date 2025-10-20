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
    [TableAttribute("MS_BERTH")]
    public class MsBerth : ISyncTable
    {
        #region データメンバ

        //MS_BERTH_ID                    NVARCHAR2(40) NOT NULL,
        //BERTH_NAME                     NVARCHAR2(50),
        //DELETE_FLAG                    NUMBER(1,0) NOT NULL,
        //USER_KEY                       VARCHAR2(40),
        //VESSEL_ID                      NUMBER(4,0) NOT NULL,
        //DATA_NO                        NUMBER(13,0),
        //SEND_FLAG                      NUMBER(1,0) NOT NULL,
        //RENEW_DATE                     DATE NOT NULL,
        //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
        //TS                             VARCHAR2(20),

        /// <summary>
        /// バースID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BERTH_ID", true)]
        public string MsBerthId { get; set; }

        /// <summary>
        /// バースコード
        /// </summary>
        [DataMember]
        [ColumnAttribute("BERTH_CODE", true)]
        public string BerthCode { get; set; }

        /// <summary>
        /// バース名
        /// </summary>
        [DataMember]
        [ColumnAttribute("BERTH_NAME")]
        public string BerthName { get; set; }

        /// <summary>
        /// 基地ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KICHI_ID", true)]
        public string MsKichiId { get; set; }

        /// <summary>
        /// 基地名
        /// </summary>
        [DataMember]
        [ColumnAttribute("KICHI_NAME", true)]
        public string KichiName { get; set; }

 
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
        public Int64 DataNo { get; set; }       //090731 anahara

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

        public MsBerth()
        {
        }

        public static List<MsBerth> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBerth), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBerth), "OrderBy");
            List<MsBerth> ret = new List<MsBerth>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsBerth> mapping = new MappingBase<MsBerth>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsBerth GetRecordByBerthName(NBaseData.DAC.MsUser loginUser, string berthName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBerth), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBerth), "ByBerthName");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBerth), "OrderBy");
            List<MsBerth> ret = new List<MsBerth>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("BERTH_NAME", berthName));
            MappingBase<MsBerth> mapping = new MappingBase<MsBerth>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static MsBerth GetRecordByBerthCode(NBaseData.DAC.MsUser loginUser, string berthCode)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBerth), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBerth), "ByBerthCode");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBerth), "OrderBy");
            List<MsBerth> ret = new List<MsBerth>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("BERTH_CODE", berthCode));

            MappingBase<MsBerth> mapping = new MappingBase<MsBerth>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<MsBerth> GetRecordByBerthCodeBerthName(NBaseData.DAC.MsUser loginUser, string berthCode, string berthName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBerth), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBerth), "ByBerthCodeBerthName");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBerth), "OrderBy");
            List<MsBerth> ret = new List<MsBerth>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("BERTH_CODE", "%" + berthCode + "%"));
            Params.Add(new DBParameter("BERTH_NAME", "%" + berthName + "%"));

            MappingBase<MsBerth> mapping = new MappingBase<MsBerth>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        //基地IDに関連するデータの取得
        public static List<MsBerth> GetRecordByMsKichiID(NBaseData.DAC.MsUser loginUser, string ms_kichi_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBerth), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBerth), "ByMsKichiID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBerth), "OrderBy");

            List<MsBerth> ret = new List<MsBerth>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_KICHI_ID", ms_kichi_id));

            MappingBase<MsBerth> mapping = new MappingBase<MsBerth>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
                        
            return ret;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }

        public bool InsertRecord(DBConnect dbConnect,NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBerth), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_BERTH_ID", MsBerthId));
            Params.Add(new DBParameter("BERTH_CODE", BerthCode));
            Params.Add(new DBParameter("BERTH_NAME", BerthName));
            Params.Add(new DBParameter("MS_KICHI_ID", MsKichiId));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("USER_KEY", UserKey));

            int cnt;
            //cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBerth), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("BERTH_CODE", BerthCode));
            Params.Add(new DBParameter("BERTH_NAME", BerthName));
            Params.Add(new DBParameter("MS_KICHI_ID", MsKichiId));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("USER_KEY", UserKey));

            Params.Add(new DBParameter("MS_BERTH_ID", MsBerthId));
            Params.Add(new DBParameter("TS", Ts));

            int cnt;
            //cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        #region ISyncTable メンバ


        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsBerthId));

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
            Params.Add(new DBParameter("PK", MsBerthId));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
