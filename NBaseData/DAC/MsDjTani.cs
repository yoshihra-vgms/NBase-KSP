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
    [Serializable]
    [DataContract()]
    [TableAttribute("MS_DJ_TANI")]
    public class MsDjTani : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 単位ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DJ_TANI_ID", true)]
        public string MsDjTaniID { get; set; }

        /// <summary>
        /// 単位名
        /// </summary>
        [DataMember]
        [ColumnAttribute("TANI_NAME")]
        public string TaniName { get; set; }

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

        public override string ToString()
        {
            return TaniName;
        }

        public MsDjTani()
        {
        }

        public static List<MsDjTani> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDjTani), MethodBase.GetCurrentMethod());
            List<MsDjTani> ret = new List<MsDjTani>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsDjTani> mapping = new MappingBase<MsDjTani>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDjTani), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_DJ_TANI_ID", MsDjTaniID));
            Params.Add(new DBParameter("TANI_NAME", TaniName));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDjTani), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("TANI_NAME", TaniName));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_DJ_TANI_ID", MsDjTaniID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
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
            Params.Add(new DBParameter("PK", MsDjTaniID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
