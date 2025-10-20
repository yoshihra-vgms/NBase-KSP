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
    [TableAttribute("MS_CARGO_GROUP")]
    public class MsCargoGroup : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 積荷グループID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CARGO_GROUP_ID", true)]
        public int MsCargoGroupID { get; set; }

        /// <summary>
        /// 積荷グループ名
        /// </summary>
        [DataMember]
        [ColumnAttribute("CARGO_GROUP_NAME")]
        public string CargoGroupName { get; set; }

        /// <summary>
        /// 更新者
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

        #endregion


        public static bool IsC5(int id)
        {
            return id == 4; // 'C5' のIDは 4
        }

        public override string ToString()
        {
            return CargoGroupName;
        }

        public static List<MsCargoGroup> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCargoGroup), MethodBase.GetCurrentMethod());

            List<MsCargoGroup> ret = new List<MsCargoGroup>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsCargoGroup> mapping = new MappingBase<MsCargoGroup>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }

        public bool InsertRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCargoGroup), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL_CLIENT)
            {
                Params.Add(new DBParameter("MS_CARGO_GROUP_ID", MsCargoGroupID));
            }
            Params.Add(new DBParameter("CARGO_GROUP_NAME", CargoGroupName));
            
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = 0;
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

        public bool UpdateRecord(DBConnect dbConnect, MsUser loginUser)
        {

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCargoGroup), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("CARGO_GROUP_NAME", CargoGroupName));

            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("DATA_NO", DataNo));

            Params.Add(new DBParameter("MS_CARGO_GROUP_ID", MsCargoGroupID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = 0;
            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
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
            Params.Add(new DBParameter("PK", MsCargoGroupID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
