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
    [TableAttribute("MS_BASHO")]
    public class MsBasho : ISyncTable
    {
        #region データメンバ

        //MS_BASHO_ID                    NVARCHAR2(40) NOT NULL,
        //BASHO_NAME                     NVARCHAR2(50),
        //MS_BASHO_KUBUN_ID              NVARCHAR2(40),
        //DELETE_FLAG                    NUMBER(1,0) NOT NULL,
        //SEND_FLAG                      NUMBER(1,0) NOT NULL,
        //VESSEL_ID                      NUMBER(4,0) NOT NULL,
        //DATA_NO                        NUMBER(13,0),
        //USER_KEY                       VARCHAR2(40),
        //RENEW_DATE                     DATE NOT NULL,
        //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
        //TS                             VARCHAR2(20),

        /// <summary>
        /// 場所ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BASHO_ID", true)]
        public string MsBashoId { get; set; }

        /// <summary>
        /// 場所NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BASHO_NO", true)]
        public string MsBashoNo { get; set; }


        /// <summary>
        /// 場所名
        /// </summary>
        [DataMember]
        [ColumnAttribute("BASHO_NAME")]
        public string BashoName { get; set; }

        /// <summary>
        /// 場所区分ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BASHO_KUBUN_ID")]
        public string MsBashoKubunId { get; set; }

        /// <summary>
        /// 場所区分 >> 場所区分名
        /// </summary>
        [DataMember]
        [ColumnAttribute("BASHO_KUBUN_NAME")]
        public string BashoKubunName { get; set; }

        /// <summary>
        /// 場所区分 >> 場所区分名
        /// </summary>
        [DataMember]
        [ColumnAttribute("AKASAKA_PORT_NO")]
        public string AkasakaPortNo { get; set; }

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

        /// <summary>
        /// 外地フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("GAICHI_FLAG")]
        public int GaichiFlag { get; set; }

        #endregion

        public MsBasho()
        {
            MsBashoId = "";
        }

        public override string ToString()
        {
            return BashoName;
        }

        internal static MsBasho GetRecord(MsUser loginUser, string bashoNO)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "GetRecord");

            List<MsBasho> ret = new List<MsBasho>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_BASHO_NO", bashoNO));

            MappingBase<MsBasho> mapping = new MappingBase<MsBasho>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<MsBasho> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBasho), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "OrderBy");
            List<MsBasho> ret = new List<MsBasho>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsBasho> mapping = new MappingBase<MsBasho>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        public static List<MsBasho> GetRecordsBy港(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBasho), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "OrderBy");
            List<MsBasho> ret = new List<MsBasho>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsBasho> mapping = new MappingBase<MsBasho>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsBasho GetRecordsByAkasakaPortNo(NBaseData.DAC.MsUser loginUser,string AkasakaPortNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "ByAkasakaPortNo");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "OrderBy");

            List<MsBasho> ret = new List<MsBasho>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("AKASAKA_PORT_NO", AkasakaPortNo));
            
            MappingBase<MsBasho> mapping = new MappingBase<MsBasho>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count > 0)
            {
                return ret[0];
            }

            return null;
        }

        public static MsBasho GetRecordByBashoName(NBaseData.DAC.MsUser loginUser, string bashoName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "ByBashoName");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "OrderBy");
            List<MsBasho> ret = new List<MsBasho>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("BASHO_NAME", bashoName));
            MappingBase<MsBasho> mapping = new MappingBase<MsBasho>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<MsBasho> GetRecordsByBashoNoBashoNameBashoKubun(NBaseData.DAC.MsUser loginUser,string bashoNo, string bashoName, string bashoKubunId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "GetRecords");

            List<MsBasho> ret = new List<MsBasho>();
            ParameterConnection Params = new ParameterConnection();

            if (bashoNo.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "LikeBashoNo");
                Params.Add(new DBParameter("MS_BASHO_NO", "%" + bashoNo + "%"));
            }
            if (bashoName.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "LikeBashoName");
                Params.Add(new DBParameter("BASHO_NAME", "%" + bashoName + "%"));
            }
            if (bashoKubunId.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "ByBashoKubun");
                Params.Add(new DBParameter("BASHO_KUBUN_ID", bashoKubunId));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "OrderBy");
            
            MappingBase<MsBasho> mapping = new MappingBase<MsBasho>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsBasho> GetRecordLikeBashoName(NBaseData.DAC.MsUser loginUser, string bashoName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "LikeBashoName");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "OrderBy");
            List<MsBasho> ret = new List<MsBasho>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("BASHO_NAME", bashoName+"%"));
            MappingBase<MsBasho> mapping = new MappingBase<MsBasho>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        //場所区分に関連するデータすべて取得
        public static List<MsBasho> GetRecordsByBashoKubun(NBaseData.DAC.MsUser loginUser, string bashoKubunId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "GetRecords");

            List<MsBasho> ret = new List<MsBasho>();
            ParameterConnection Params = new ParameterConnection();


            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "ByBashoKubun");
            Params.Add(new DBParameter("BASHO_KUBUN_ID", bashoKubunId));


            MappingBase<MsBasho> mapping = new MappingBase<MsBasho>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbcone, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_BASHO_ID", MsBashoId));
            Params.Add(new DBParameter("MS_BASHO_NO", MsBashoNo));
            Params.Add(new DBParameter("BASHO_NAME", BashoName));
            Params.Add(new DBParameter("MS_BASHO_KUBUN_ID", MsBashoKubunId));
            Params.Add(new DBParameter("AKASAKA_PORT_NO", AkasakaPortNo));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("GAICHI_FLAG", GaichiFlag));

            //cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            cnt = DBConnect.ExecuteNonQuery(dbcone, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }


        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }


        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_BASHO_NO", MsBashoNo));
            Params.Add(new DBParameter("BASHO_NAME", BashoName));
            Params.Add(new DBParameter("MS_BASHO_KUBUN_ID", MsBashoKubunId));
            Params.Add(new DBParameter("AKASAKA_PORT_NO", AkasakaPortNo));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("GAICHI_FLAG", GaichiFlag));

            Params.Add(new DBParameter("MS_BASHO_ID", MsBashoId));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region ISyncTable メンバ


        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsBashoId));

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
            Params.Add(new DBParameter("PK", MsBashoId));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect,loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

    }
}
