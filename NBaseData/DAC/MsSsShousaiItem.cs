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
    [TableAttribute("MS_SS_SHOUSAI_ITEM")]
    public class MsSsShousaiItem : ISyncTable
    {
        #region データメンバ
    //MS_SS_SHOUSAI_ITEM_ID          VARCHAR2(40) NOT NULL,
    //MS_VESSEL_ID                   NUMBER(4,0),
    //SHOUSAI_ITEM_NAME              NVARCHAR2(100),
    //SEND_FLAG                      NUMBER(1,0) DEFAULT 0 NOT NULL,
    //VESSEL_ID                      NUMBER(4,0) NOT NULL,
    //DATA_NO                        NUMBER(13,0),
    //USER_KEY                       VARCHAR2(40) DEFAULT 0,
    //RENEW_DATE                     DATE NOT NULL,
    //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
    //TS                             TIMESTAMP(6),

        /// <summary>
        /// 小修理詳細品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SS_SHOUSAI_ITEM_ID", true)]
        public string MsSsShousaiItemID { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 詳細品目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOUSAI_ITEM_NAME")]
        public string ShousaiItemName { get; set; }
  
        
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

        public MsSsShousaiItem()
        {
        }

        public static List<MsSsShousaiItem> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSsShousaiItem), MethodBase.GetCurrentMethod());
            List<MsSsShousaiItem> ret = new List<MsSsShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSsShousaiItem> mapping = new MappingBase<MsSsShousaiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsSsShousaiItem> GetRecordsByMsVesselID(NBaseData.DAC.MsUser loginUser, int MsVesselID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSsShousaiItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSsShousaiItem), "whereMsVesselID");

            List<MsSsShousaiItem> ret = new List<MsSsShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            MappingBase<MsSsShousaiItem> mapping = new MappingBase<MsSsShousaiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsSsShousaiItem> GetRecordsByShousaiItemName(NBaseData.DAC.MsUser loginUser, string shousaiItemName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSsShousaiItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSsShousaiItem), "whereShousaiItemName");

            List<MsSsShousaiItem> ret = new List<MsSsShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SHOUSAI_ITEM_NAME", shousaiItemName));
            MappingBase<MsSsShousaiItem> mapping = new MappingBase<MsSsShousaiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsSsShousaiItem GetRecord(NBaseData.DAC.MsUser loginUser, string MsSsShousaiItemID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSsShousaiItem), MethodBase.GetCurrentMethod());
            List<MsSsShousaiItem> ret = new List<MsSsShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_SS_SHOUSAI_ITEM_ID", MsSsShousaiItemID));
            MappingBase<MsSsShousaiItem> mapping = new MappingBase<MsSsShousaiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSsShousaiItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_SS_SHOUSAI_ITEM_ID", MsSsShousaiItemID));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("SHOUSAI_ITEM_NAME", ShousaiItemName));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSsShousaiItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("SHOUSAI_ITEM_NAME", ShousaiItemName));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_SS_SHOUSAI_ITEM_ID", MsSsShousaiItemID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region ISyncTable メンバ

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSsShousaiItem), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsSsShousaiItemID));

        //    int count = Convert.ToInt32(DBConnect.ExecuteScalar(loginUser.MsUserID, SQL, Params));

        //    return count > 0 ? true : false;
        //}
        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSsShousaiItem), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", MsSsShousaiItemID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
}
}
